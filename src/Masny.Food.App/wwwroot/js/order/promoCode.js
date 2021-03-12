import requestService from "../services/requestService.js";

function checkPromoCode() {
    const promoCodeInput = document.getElementById("PromoCode");

    const item = {
        value: promoCodeInput.value,
    };

    requestService(
        "https://localhost:5001/order/checkpromocode",
        "POST",
        {
            value: promoCodeInput.value,
        },
        true,
        false,
        "");
}

function main() {
    const promoCodeButton = document.getElementById("promoCodeButton");

    if (promoCodeButton) {
        promoCodeButton.addEventListener("click", checkPromoCode);
    }
}

document.addEventListener("DOMContentLoaded", main);
