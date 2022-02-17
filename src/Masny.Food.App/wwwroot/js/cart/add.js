document.addEventListener("click", function (event) {
    if (event.target.dataset.cartId != undefined) {
        const url = "https://localhost:5001/cart/add";

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
                alert("Add");
            });
    }
});
