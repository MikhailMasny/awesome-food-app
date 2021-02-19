document.addEventListener('click', function(event) {
    if (event.target.dataset.cartId != undefined) {
        var array = [];
        const localStorageItems = JSON.parse(localStorage.getItem("pizzaCart"));
        if (localStorageItems !== null)
        {
            array = localStorageItems;
        }
        console.log(array);
        array.push(event.target.dataset.cartId);
        localStorage.setItem("pizzaCart", JSON.stringify(array));
    }
});