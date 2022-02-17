function checkPromoCode() {
    const promoCodeInput = document.getElementById("PromoCode");

    const url = "https://localhost:5001/order/checkpromocode";

    const data = {
        value: promoCodeInput.value,
    }

    const fetchData = {
        method: 'POST',
        body: JSON.stringify(data),
        headers: new Headers({
            'Content-Type': 'application/json; charset=UTF-8'
        })
    }

    fetch(url, fetchData)
        .then(function (response) {
            if (response.ok) {
                alert("Promo code successfully applied!");
            }
            else {
                alert("The entered promo code was not found..");
            }
        })
        .catch(function (error) {
            console.log('error', error);
        });
}

function main() {
    const promoCodeButton = document.getElementById("promoCodeButton");

    if (promoCodeButton) {
        promoCodeButton.addEventListener("click", checkPromoCode);
    }
}

document.addEventListener("DOMContentLoaded", main);
