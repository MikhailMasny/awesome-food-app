function getTotalPrice() {
    let priceArray = [];
    document.querySelectorAll(".productPrice")
        .forEach((price) => priceArray.push(parseFloat(price.innerText)));

    return priceArray.reduce((a, b) => a + b, 0);
}

function showTotalPrice() {
    alert(`Total price is: ${getTotalPrice()} (without promo code)`);
}

function main() {
    const totalPriceButton = document.getElementById("totalPriceButton");

    if (totalPriceButton) {
        totalPriceButton.addEventListener("click", showTotalPrice);
    }
}

document.addEventListener("DOMContentLoaded", main);
