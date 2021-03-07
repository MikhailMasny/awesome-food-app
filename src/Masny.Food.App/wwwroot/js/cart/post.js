function addItem(uri, data) {
    const item = {
        id: data,
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => {
            alert("add")
        })
        //.then(response => response.json())
        //.then(() => {
        //    getItems();
        //    addNameTextbox.value = '';
        //})
        .catch(error => console.error('Unable to add item.', error));
}


document.addEventListener('click', function (event) {
    if (event.target.dataset.cartId != undefined) {

        const uri = 'https://localhost:5001/cart/add';
        addItem(uri, event.target.dataset.cartId);
        //var array = [];
        //const localStorageItems = JSON.parse(localStorage.getItem("pizzaCart"));
        //if (localStorageItems !== null) {
        //    array = localStorageItems;
        //}
        //console.log(array);
        //array.push(event.target.dataset.cartId);
        //localStorage.setItem("pizzaCart", JSON.stringify(array));
    }
});