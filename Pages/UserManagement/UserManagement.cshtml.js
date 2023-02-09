function displayResetPasswordToast() {
    $(".toast").toast("show");
}


function passwordReset(userID, currentUserID) {

    const pageurl = document.URL;
    
    $.ajax({
        url: pageurl+ '?handler=ResetPassword&id=' + userID + "&currentUserID=" + currentUserID,
        async: false,
        type: "GET"
    })
        .done(displayResetPasswordToast());
}