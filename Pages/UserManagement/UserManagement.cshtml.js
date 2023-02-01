function displayResetPasswordToast() {
    //const elem = document.getElementById('para');

    $('.toast').toast();
    $('.toast').toast('show');
}


function passwordReset(userID) {

    var pageurl = document.URL;
    
    $.ajax({
        url: pageurl+ '?handler=ResetPassword&id=' + userID,
        async: false,
        type: "GET"
    })
        .done(displayResetPasswordToast());
}