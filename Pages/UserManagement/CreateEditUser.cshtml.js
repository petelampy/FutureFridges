const ViewStockCheckbox = document.getElementById("viewStockCheckbox");
const AddStockCheckbox = document.getElementById("addStockCheckbox");
const RemoveStockCheckbox = document.getElementById("removeStockCheckbox");

function showHideCheckboxes() {
    if (ViewStockCheckbox.checked) {
        AddStockCheckbox.disabled = false;
        RemoveStockCheckbox.disabled = false;
    } else {
        AddStockCheckbox.disabled = true;
        RemoveStockCheckbox.disabled = true;
    }
}

ViewStockCheckbox.addEventListener('change', showHideCheckboxes);

window.onload = function () { showHideCheckboxes() };