// gallery functions

document.addEventListener("DOMContentLoaded", () => {
    const images = document.querySelectorAll(".grid_container_gallery > img, .grid_container_gallery_nuoma > img");
    const modal = document.querySelector(".modal");
    const next = document.querySelector(".next");
    const previous = document.querySelector(".previous");
    const modalImg = document.querySelector(".modalImg");
    const close = document.querySelector(".close");

    let currentImageIndex = 0;

    images.forEach((image, index) => {
        image.addEventListener("click", () => {
            currentImageIndex = index;
            modalImg.src = image.src;
            modal.classList.add("appear");
        });
    });

    // Showing the next image
    next.addEventListener("click", () => {
        currentImageIndex = (currentImageIndex + 1) % images.length;
        modalImg.src = images[currentImageIndex].src;
        });

    // Showing the previous image
    previous.addEventListener("click", () => {
        currentImageIndex = (currentImageIndex - 1 + images.length) % images.length;
        modalImg.src = images[currentImageIndex].src;
        });

    close.addEventListener("click", () => {
        modal.classList.remove("appear");
    });
});

 // dropdown menu
function toggleDropdown() {
    var dropdownContent = document.querySelector('.dropdown-content');

    if (dropdownContent.style.display == "block") {
        dropdownContent.style.display = "none";
    } else {
        dropdownContent.style.display = "block";
        dropdownContent.style.overflow = "visible";
    }
};

 // dropdown menu
  function toggleDropdown_1() {
    var dropdownContentmobile = document.querySelector('.dropdown-content-mobile');
    if (dropdownContentmobile.style.display == "flex") {
            dropdownContentmobile.classList.remove("active");
            dropdownContentmobile.style.display = "none";
    } else {
        dropdownContentmobile.classList.add("active");
        dropdownContentmobile.style.display = "flex";
        dropdownContentmobile.style.backgroundcolor = "blue";
     }
}; 
