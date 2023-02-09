function displayEmailSuccessToast() {
    $(".toast").toast("show");
}


window.onload = function () {
    const URLParameters = new URLSearchParams(window.location.search);
    const SuccessfulEmail = URLParameters.get("SuccessfulEmail");
    
    if (SuccessfulEmail) {
        displayEmailSuccessToast();
    }
};