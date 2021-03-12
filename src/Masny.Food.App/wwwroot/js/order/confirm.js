function checkValues(event) {
    const nameInput = document.getElementById("Name");
    const phoneInput = document.getElementById("Phone");
    const addressInput = document.getElementById("Address");

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

function main() {
    const confirmOrderButton = document.getElementById("confirmOrderButton");

    if (confirmOrderButton) {
        confirmOrderButton.addEventListener("click", checkValues);
    }
}

document.addEventListener("DOMContentLoaded", main);
