$(document).ready(function () {
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

    }
    );

    $("#email").on("blur", function () {
        var email = $('#email').val().trim();
        if (!/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(email)) {
            $("#email-error").text("Error: Email is Invalid");
        }
        else {
            $("#email-error").text("");
        }
    }
    );

    $("#phone").on("blur", function () {
        var phone = $('#phone').val().trim();
        if (!/^\d{3}\d{3}\d{4}$/.test(phone)) {
            $("#phone-error").text("Error: Phone is Invalid");
        }
        else {
            $("#phone-error").text("");
        }
    }
    );

}
);