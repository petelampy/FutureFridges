const AddRemovePermissions = document.getElementById("addRemoveStockPermissions");
const ViewStockCheckbox = document.getElementById("viewStockCheckbox");

function showHideCheckboxes() {
    if (ViewStockCheckbox.checked) {
        AddRemovePermissions.style.display = "block";
    } else {
        AddRemovePermissions.style.display = "none";
    }
}

ViewStockCheckbox.addEventListener('change', showHideCheckboxes);

window.onload = function () { showHideCheckboxes() };