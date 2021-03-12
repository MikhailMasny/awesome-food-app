import requestService from "../services/requestService.js";

document.addEventListener("click", function (event) {
    if (event.target.dataset.cartId != undefined) {

        const item = {
            productId: event.target.dataset.cartId,
        };

        requestService(
            "https://localhost:5001/cart/remove",
            "POST",
            item,
            false,
            false,
            ""
        );

        event.srcElement.parentElement.parentElement.remove();
        
        const count = document.querySelectorAll(".productInCart").length;

        if (count === 0) {
            document.location.href = "/";
        }
    }
});