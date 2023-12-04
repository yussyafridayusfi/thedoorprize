<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <meta http-equiv="X-UA-Compatible" content="ie=edge">
  <title>Swiper with JSON Data</title>
  <link rel="stylesheet" href="https://unpkg.com/swiper@7/swiper-bundle.min.css">
  <link rel="stylesheet" href="style.css">
</head>
<body oncontextmenu="return false">
  <div class="swiper">
    <div class="swiper-wrapper" id="swiper-wrapper">
      <!-- Slide elements will be populated dynamically -->
    </div>
    <!-- Navigation buttons -->
    <div class="swiper-button-prev"></div>
    <div class="swiper-button-next"></div>
  </div>
  <script src="https://unpkg.com/swiper@7/swiper-bundle.min.js"></script>
  <script>
    // JSON data embedded within the HTML file
    const data = [
      { "imageUrl": "1.jfif", "name": "Ronne Galle", "role": "Project Manager", "quote": "Nam libero tempore..." },
      // Add other JSON objects here
    ];

    // Iterate through JSON data to create swiper slides
    const swiperWrapper = document.getElementById('swiper-wrapper');
    data.forEach(item => {
      const slide = document.createElement('div');
      slide.classList.add('swiper-slide');
      slide.innerHTML = `
        <div class="slide-card text-center">
          <img class="slide-card-img-top" src="${item.imageUrl}" alt="" />
          <div class="slide-card-body">
            <h5>${item.name}<br /><span>${item.role}</span></h5>
            <p class="slide-card-text">${item.quote}</p>
          </div>
        </div>
      `;
      swiperWrapper.appendChild(slide);
    });

    // Initialize Swiper after dynamically creating slides
    const swiper = new Swiper(".swiper", {
      navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev"
      },
      slidesPerView: 1,
      loop: true,
      centeredSlides: true,
      spaceBetween: 10,
      breakpoints: {
        320: { slidesPerView: 1, spaceBetween: 20 },
        480: { slidesPerView: 2, spaceBetween: 30 },
        640: { slidesPerView: 3, spaceBetween: 40 }
      }
    });
  </script>
</body>
</html>
