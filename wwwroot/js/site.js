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

$("#fname").on("blur", function () {
    var firstName = $('#fname').val().trim();
    if (firstName.length < 2 || !/^[a-zA-Z]+$/.test(firstName)) {
        $("#fname-error").text("Error: First name must be at least 2 characters and contain only letters.");
    }
    else {
        $("#fname-error").text("");
    }
}
);

$("#lname").on("blur", function () {
    var lastName = $('#lname').val().trim();
    if (lastName.length < 2 || !/^[a-zA-Z]+$/.test(lastName)) {
        $("#lname-error").text("Error: Last name must be at least 2 characters and contain only letters.");
    }
    else {
        $("#lname-error").text("");
    }
}
);

$("#email").on("blur", function () {
    var email = $('#email').val().trim();
    if (!/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(email)) {
        $("#email-error").text("Error: Email must be in the format");
    }
    else {
        $("#email-error").text("");
    }
}
);


$("#classtype").on("blur", function () {
    var classtype = $('#classtype').val().trim();
    if (classtype.includes("kids")) {
        // Show the parent name label and input field when the class type includes 'kids'
        $("label[for='pname']").show();
        $("#pname").show();
        $("#pname").prop('disabled', false);
    }

    else {
        // Hide the parent name label and input field when the class type does not include 'kids'
        $("label[for='pname']").hide();
        $("#pname").hide();
        $("#pname").prop('disabled', true);
    }

}
);
