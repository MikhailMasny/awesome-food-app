const confirmOrderButton = document.getElementById("confirmOrder");
const nameInput = document.getElementById("Name");
const phoneInput = document.getElementById("Phone");
const addressInput = document.getElementById("Address");

function checkValues(event) {
    if (nameInput.value === '') {
        event.preventDefault();
        alert("Name is empty");
        return;
    }

    if (phoneInput.value === '') {
        event.preventDefault();
        alert("Phone is empty");
        return;
    }

    if (addressInput.value === '') {
        event.preventDefault();
        alert("Address is empty");
        return;
    }
}

confirmOrderButton.addEventListener("click", checkValues);
