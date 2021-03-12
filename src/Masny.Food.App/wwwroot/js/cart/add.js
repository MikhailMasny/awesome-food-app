import requestService from "../services/requestService.js";

document.addEventListener("click", function (event) {
    if (event.target.dataset.cartId != undefined) {

        const item = {
            productId: event.target.dataset.cartId,
        };

        requestService(
            "https://localhost:5001/cart/add",
            "POST",
            item,
            true,
            "Add"
        );
    }
});
