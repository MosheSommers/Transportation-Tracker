$(document).ready(function () {

    $("#register-form").validate({

        rules: {

            EmailAddress: {
                required: true
            },
            Password: {
                required: true,
                minlength: 8
            },
            ConfirmedPassword: {
                equalTo: "#password"
            },
            //we don't really need this client-side
            //Phone: {
            //    required: true,
            //    phoneValidation: true,
            //    phoneUS: true
            //}

        },

    });
    //$.validator.addMethod("phoneValidation",
    //    function (value, element) {
    //        return /^[A-Za-z\d=#$%@_ -]+$/.test(value);
    //    },
    //    "Please enter a valid phone number"
    //);
});

