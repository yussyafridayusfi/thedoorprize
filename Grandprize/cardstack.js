var $card = $('.card1');
var lastCard = $(".card-list .card").length - 1;

setInterval(() => {
    document.getElementById('btn-next').click()
}, 200)

$('.next').click(function() {
    // console.log('hit!')
  var prependList = function() {
    if ($('.card1').hasClass('activeNow')) {
      var $slicedCard = $('.card1').slice(lastCard).removeClass('transformThis activeNow');
      $('.card-list').prepend($slicedCard);
    }
  }
  $('li').last().removeClass('transformPrev').addClass('transformThis').prev().addClass('activeNow');
  setTimeout(function() {
    prependList();
  }, 150);
});

$('.prev').click(function() {
  var appendToList = function() {
    if ($('.card1').hasClass('activeNow')) {
      var $slicedCard = $('.card1').slice(0, 1).addClass('transformPrev');
      $('.card-list').append($slicedCard);
    }
  }

  $('li').removeClass('transformPrev').last().addClass('activeNow').prevAll().removeClass('activeNow');
  setTimeout(function() {
    appendToList();
  }, 150);
});

var $card = $('.card2');
var lastCard = $(".card2-list .card").length - 1;

$('.next').click(function() {
  var prependList = function() {
    if ($('.card2').hasClass('activeNow')) {
      var $slicedCard = $('.card2').slice(lastCard).removeClass('transformThis activeNow');
      $('.card2-list').prepend($slicedCard);
    }
  }
  $('li').last().removeClass('transformPrev').addClass('transformThis').prev().addClass('activeNow');
  setTimeout(function() {
    prependList();
  }, 150);
});

$('.prev').click(function() {
  var appendToList = function() {
    if ($('.card2').hasClass('activeNow')) {
      var $slicedCard = $('.card2').slice(0, 1).addClass('transformPrev');
      $('.card2-list').append($slicedCard);
    }
  }

  $('li').removeClass('transformPrev').last().addClass('activeNow').prevAll().removeClass('activeNow');
  setTimeout(function() {
    appendToList();
  }, 150);
});