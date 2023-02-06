function displayResetPasswordToast() {
    $('.toast').toast();
    $('.toast').toast('show');
}


function passwordReset(userID, currentUserID) {

    var pageurl = document.URL;
    
    $.ajax({
        url: pageurl+ '?handler=ResetPassword&id=' + userID + "&currentUserID=" + currentUserID,
        async: false,
        type: "GET"
    })
        .done(displayResetPasswordToast());
}