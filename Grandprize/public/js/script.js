$(() => {
  
  $(document).on('keydown', (e) => { 
    
    if(e.code === 'F5') {
      e.preventDefault();
      return;
    }else if (e.keyCode == 34){
      clickStart() 
    }
    
  });

  // document.addEventListener('keydown', function(event){
  //   console.log(event);
  //   if(event.code === 'F5') {
  //     event.preventDefault();
  //     return;
  //   }else{
  //     switch(event.keyCode){

  //       case 33: //left or previous
  //         alert('prev');
  //         return;
  //       case 34: //right or next
  //         clickStart();
  //         return;
  //       case 27: //start or play
  //         alert('home');
  //         return;
  //       case 116: //stop or exit
  //         alert('stop');
  //         return;

  //     }
  //   }
  // });

});

var doLoop;
var continue_spinning = true;
var stillRolling = false;
var datas = [];


var clickStart = async function(){
  if (continue_spinning && !stillRolling) {
    await initData();
    $('.photo-card-div').css('height', $('.main-container').height() - $('.bottom-card-div').height() + 'px' );
    continue_spinning = false;
    stillRolling = true;
    doLoop = loop();
    console.log('start');
  }
  else if (!continue_spinning) {
    continue_spinning = true;
    doLoop.slowDownAndStop();
    console.log('stop');
  }
}


var initData = async function() {

  var htmlData = ''

  datas = [
    {
      "npk"       : "8416",
      "name"      : "Aditya Chandra Isdhianto",
      "company"   : "Mulia",
      "division"  : "IT"
    },
    {
      "npk"       : "8414",
      "name"      : "Yussy",
      "company"   : "Mulia",
      "division"  : "IT"
    },
    {
      "npk"       : "0023",
      "name"      : "Manusia",
      "company"   : "Mulia",
      "division"  : "Entah"
    },
    {
      "npk"       : "0026",
      "name"      : "Manusia - 2",
      "company"   : "Mulia",
      "division"  : "Entah"
    },
    {
      "npk"       : "6152",
      "name"      : "Manusia - 3",
      "company"   : "Mulia",
      "division"  : "Entah"
    },
    {
      "npk"       : "5830",
      "name"      : "RIZKY SUDARMANTO",
      "company"   : "Mulia",
      "division"  : "IT"
    }
  ]
  
  datas = await shuffle(datas);

  htmlData = genHtmlData();

  $('.reel').html( htmlData );
}



var genHtmlData = function() {

  var html = '';

  datas.forEach( (data, index) => {
    html += `
      <div class="reel-person"  id="person-${ index + 1 }">
        <div class="row auto-height">
          <div class="col-12 photo-card-div">
            <img src="./photos/${ data.npk }.jpg" class="img-fluid" style="width:100%" onerror="this.src='./images/winner.png';" alt="The Winner">
          </div>
          <div class="col-12 bg-info text-dark text-bold bottom-card-div">
            <h5 class="m-0 fw-bolder px-2">${ data.name }</h5>
            <h6 class="m-0 fw-bolder">${ data.company } ( ${ data.division } ) - ${ data.npk } </h6>
          </div>
        </div>
      </div>
    ` 
  });

  datas.push({
    "npk"       : datas[0].npk,
    "name"      : datas[0].name,
    "company"   : datas[0].company,
    "division"  : datas[0].division
  });

  console.log(datas);

  html += `
    <div class="reel-person" id="person-1">
      <div class="row auto-height">
        <div class="col-12 photo-card-div">
          <img src="./photos/${ datas[0].npk }.jpg" class="img-fluid"  style="width:100%" onerror="this.src='./images/winner.png';" alt="The Winner">
        </div>
        <div class="col-12 bg-info text-dark text-bold bottom-card-div">
          <h5 class="m-0 fw-bolder px-2">${ datas[0].name }</h5>
          <h6 class="m-0 fw-bolder">${ datas[0].company } ( ${ datas[0].division } ) - ${ datas[0].npk } </h6>
        </div>
      </div>
    </div>
  `

  return html;

}

var loop = function() {
  var initSpeed = 100; //banter mutere
  var start = 1;
  var slow = false;
  var stopLoop = 1;
  let firstElement = $('.reel-container:first').find('.reel-person:eq(0)');
  var containerHeight = $('.reel-person').height();

  function run() {

    new Audio('sounds/click-1.wav').play();

    $('.container-slot').css({
      'animation' : 'shake ' + stopLoop * 0.5 + 's',
      'animation-iteration-count' : 'infinite'
     });

    if (start >= datas.length) {
        start = 0;
        firstElement.animate({marginTop: `-${start * containerHeight}px`}, stopLoop * 0);
        firstElement.animate({marginTop: `-${++start * containerHeight}px`}, stopLoop * initSpeed);
    }else{
      firstElement.animate({marginTop: `-${start * containerHeight}px`}, stopLoop * initSpeed);
    }

    start++;

    if( !slow )
      timer = setTimeout(run, initSpeed);
    else {

      clearTimeout(timer);
      timer = setTimeout(run, stopLoop++ * initSpeed);

      if(stopLoop == 10) {
        clearTimeout(timer); 
        timer = 0;
        $('.container-slot').css({
          'animation' : 'none',
          'animation-iteration-count' : 'none'
        });

        setTimeout(function (){
  
          var heightOfRell = $('.reel-person').height();
          var heightOfReels = $('.reel-person').height() * datas.length;
          var marginTopValue = parseFloat( $('.reel-container:first').find('.reel-person:eq(0)').css('margin-top') ) * -1;
          var winnerIndex = datas.length - Math.round( ( (heightOfReels - marginTopValue) / heightOfRell ) );

          var winnerData = datas[winnerIndex];

          console.log(winnerData);


          $('#thewinner').html(
            `<div class="card mb-3" style="max-width: 540px;">
              <div class="row g-0">
                <div class="col-md-4">
                  <img src="./photos/${ winnerData.npk }.jpg" class="img-fluid rounded-start" onerror="this.src='./images/winner.png';" alt="The Winner">
                </div>
                <div class="col-md-8">
                  <div class="card-body">
                    <h5 class="card-title">${ winnerData.name }</h5>
                    <p class="card-text">${ winnerData.company } - ${ winnerData.division }</p>
                    <p class="card-text">${ winnerData.npk }
                    was wes wos <br />
                    was wes wos <br />
                    was wes wos <br />
                    was wes wos <br />
                    </p>
                  </div>
                </div>
              </div>
            </div>`
          )

          new Audio('sounds/yeay.wav').play();
          stillRolling = false;

          $(document).off('keydown');
          $('.backdrop-winner').removeClass('d-none').addClass('d-block');

          $(document).on('keydown', (e) =>  {

            if(e.code === 'F5') {

              e.preventDefault();
              return;

            }else if(e.keyCode == 33){

              $(document).off('keydown');
  
              console.log('created');
  
              $('.backdrop-winner').removeClass('d-block').addClass('d-none');
              
              $(document).on('keydown', (e) => { 

                if(e.code === 'F5') {
                  e.preventDefault();
                  return;
                }else if (e.keyCode == 34){
                  clickStart() 
                }
                
              });
  
              $('.backdrop-winner').off('dblclick');

            }


          });

          // $('.backdrop-winner').on('dblclick', () =>  {
          //   console.log('created');

          //   $('.backdrop-winner').removeClass('d-block').addClass('d-none');
          //   $(document).on('click', () => { 
          //     clickStart();
          //   });

          //   $('.backdrop-winner').off('dblclick');

          // });

                    
        }, stopLoop * initSpeed + 2000);
        
      } 

    }

  }
  
  timer = setTimeout(run, 100);

  return {
    slowDownAndStop: slowDownAndStop
  }

  function slowDownAndStop() {
    if (timer) {
        clearTimeout(timer);
        slow = true
        timer = setTimeout(run, 100);
    }
  }
}

function shuffle(array) {
  var m = array.length, t, i;
    
  // While there remain elements to shuffle…
  while (m) {
    
    // Pick a remaining element…
    i = Math.floor(Math.random() * m--);
    
    // And swap it with the current element.
    t = array[m];
    array[m] = array[i];
    array[i] = t;
  }
    
  return array;
}