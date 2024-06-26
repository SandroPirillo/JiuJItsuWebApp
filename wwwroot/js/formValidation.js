$(document).ready(function () {
    // Function to disable the submit button
    function disableSubmitButton() {
        $("#submit").prop('disabled', true);
    }

    // Function to enable the submit button
    function enableSubmitButton() {
        // Check if there are any error messages present
        if ($(".error-text").length === 0) {
            $("#submit").prop('disabled', false);
        }
    }

    $("#fname").on("blur", function () {
        var firstName = $('#fname').val().trim();
        if (firstName.length < 2 || !/^[a-zA-Z]+$/.test(firstName)) {
            if ($("#fname-error").length === 0) {
                $('<div id="fname-error" class="error-text">Error: First name must be at least 2 characters and contain only letters.</div>').insertAfter('#fname');
                disableSubmitButton();
            }
        }
        else {
            $("#fname-error").remove();
            enableSubmitButton();
        }
    });

    $("#lname").on("blur", function () {
        var lastName = $('#lname').val().trim();
        if (lastName.length < 2 || !/^[a-zA-Z]+$/.test(lastName)) {
            if ($("#lname-error").length === 0) {
                $('<div id="lname-error" class="error-text">Error: Last name must be at least 2 characters and contain only letters.</div>').insertAfter('#lname');
                disableSubmitButton();
            }
        }
        else {
            $("#lname-error").remove();
            enableSubmitButton();
        }
    });


    $("#pname").on("blur", function () {
        var parentName = $('#pname').val().trim();
        var nameParts = parentName.split(' ');

        if (nameParts.length != 2 || !/^[a-zA-Z]{2,}$/.test(nameParts[0]) || !/^[a-zA-Z]{2,}$/.test(nameParts[1])) {
            if ($("#pname-error").length === 0) {
                $('<div id="pname-error" class="error-text">Error: Parent name must be two words of at least 2 characters each and contain only letters.</div>').insertAfter('#pname');
                disableSubmitButton();
            }
        }
        else {
            $("#pname-error").remove();
            enableSubmitButton();
        }
    });

    $("#classtype").on("blur", function () {
        var classtype = $('#classtype').val().trim();
        if (classtype.includes("kids")) {
            // Show the parent name label and input field when the class type includes 'kids'
            $("label[for='pname']").show();
            $("#pname").show();
            $("#pname").prop('disabled', false);
            $("#pname").prop('required', true);
        }

        else {
            // Hide the parent name label and input field when the class type does not include 'kids'
            $("label[for='pname']").hide();
            $("#pname").hide();
            $("#pname").prop('disabled', true);
            $("#pname").prop('required', false);
        }

        enableSubmitButton();
    });

    $("#email").on("blur", function () {
        var email = $('#email').val().trim();
        if (!/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(email)) {
            if ($("#email-error").length === 0) {
                $('<div id="email-error" class="error-text">Error: Email is Invalid</div>').insertAfter('#email');
                disableSubmitButton();
            }
        }
        else {
            $("#email-error").remove();
            enableSubmitButton();
        }
    });

    $("#phone").on("blur", function () {
        var phone = $('#phone').val().trim();
        if (!/^\d{3}\d{3}\d{4}$/.test(phone)) {
            if ($("#phone-error").length === 0) {
                $('<div id="phone-error" class="error-text">Error: Phone is Invalid</div>').insertAfter('#phone');
                disableSubmitButton();
            }
        }
        else {
            $("#phone-error").remove();
            enableSubmitButton();
        }
    });


    $("#password").on("blur", function () {
        var password = $('#password').val().trim();
        if (password.length < 6) {
            if ($("#password-error").length === 0) {
                $('<div id="password-error" class="error-text">Error: Password must be 6 or more characters.</div>').insertAfter('#password');
                disableSubmitButton();
            }
        }
        else {
            $("#password-error").remove();
            enableSubmitButton();
        }
    });

    $("#confirmPassword").on("blur", function () {
        var password = $('#password').val().trim();
        var confirmPassword = $('#confirmPassword').val().trim();
        if (password != confirmPassword) {
            if ($("#confirmPassword-error").length === 0) {
                $('<div id="confirmPassword-error" class="error-text">Error: Passwords do not match.</div>').insertAfter('#confirmPassword');
                disableSubmitButton();
            }
        }
        else {
            $("#confirmPassword-error").remove();
            enableSubmitButton();
        }
    });
});