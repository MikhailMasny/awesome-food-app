document.addEventListener("click", function (event) {
    if (event.target.dataset.cartId != undefined) {
        const url = "https://localhost:5001/cart/remove";

        const data = {
            productId: event.target.dataset.cartId,
        }

        const fetchData = {
            method: 'POST',
            body: JSON.stringify(data),
            headers: new Headers({
                'Content-Type': 'application/json; charset=UTF-8'
            })
        }

        fetch(url, fetchData)
            .then(function () {
                alert("Remove");
            });

        event.srcElement.parentElement.parentElement.remove();

        const count = document.querySelectorAll(".productInCart").length;

        if (count === 0) {
            document.location.href = "/";
        }
    }
});