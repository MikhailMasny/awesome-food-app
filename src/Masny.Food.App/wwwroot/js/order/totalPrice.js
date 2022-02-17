function getTotalPrice() {
    let priceArray = [];
    document.querySelectorAll(".productPrice")
        .forEach((price) => priceArray.push(parseFloat(price.innerText)));

    return priceArray.reduce((productOne, productTwo) => productOne + productTwo, 0);
}

function showTotalPrice() {
    const totalPrice = getTotalPrice();
    const promoCodeInput = document.getElementById("PromoCode");

    const url = "https://localhost:5001/order/gettotalprice";

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
        .then(
            function (response) {
                if (response.status !== 200) {
                    console.log('status code: ' + response.status);
                    return;
                }

                response.json().then(function (data) {

                    console.log(data);
                    const totalPriceWithPromoCode = data.totalPriceWithPromoCode;
                    const promoCodeValue = data.promoCodeValue;

                    let message = `Total price is: ${totalPrice}`;

                    if (totalPriceWithPromoCode != 0 && totalPrice != totalPriceWithPromoCode) {
                        message += ` (with promo code: ${totalPriceWithPromoCode}, ${promoCodeValue}%)`;
                    }

                    alert(message);
                });
            }
        )
        .catch(function (error) {
            console.log('Error!', error);
        });
}

function main() {
    const totalPriceButton = document.getElementById("totalPriceButton");

    if (totalPriceButton) {
        totalPriceButton.addEventListener("click", showTotalPrice);
    }
}

document.addEventListener("DOMContentLoaded", main);
