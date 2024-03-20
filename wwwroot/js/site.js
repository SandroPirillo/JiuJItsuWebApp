// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Fade in effect for elements
document.addEventListener("DOMContentLoaded", function () {
    const elements = document.querySelectorAll('.fade-in');
    const offset = 100; // Adjust this value to control when the transition starts

    function fadeInOnScroll() {
        elements.forEach(function (element) {
            const bounding = element.getBoundingClientRect();
            if (
                bounding.top >= 0 && bounding.top <= (window.innerHeight || document.documentElement.clientHeight) - offset
            ) {
                element.classList.add('visible');
            }
        });
    }

    // Initial check when the page loads
    fadeInOnScroll();

    // Listen for scroll events
    window.addEventListener('scroll', fadeInOnScroll);
});


