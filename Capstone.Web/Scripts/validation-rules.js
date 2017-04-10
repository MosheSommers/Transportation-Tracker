$(document).ready(function () {

    $("#login-form, #register-form").validate({

        rules: {

            EmailAddress: {
                required: true
            },
            Password: {
                required: true,
                minlength: 8,
                strongpassword: true
            },
            ConfirmedPassword: {
                equalTo: "#password"
            }
          
            

        },
        messages: {

        },

        errorClass: "error",
        validClass: "valid"


    });
   
});

$.validator.addMethod("strongpassword", function (value, index) {
    return value.match(/[A-Z]/) && value.match(/[A-z]/) && value.match(/\d/);  //check for one capital letter, one lower case letter, one num
}, "Please enter a strong password (one capital, one lower case, and one number");

$.validator.addMethod("phoneValidation",
    function (value, element) {
        return /^[A-Za-z\d=#$%@_ -]+$/.test(value);
    },
    "Please enter a valid phone number"
);