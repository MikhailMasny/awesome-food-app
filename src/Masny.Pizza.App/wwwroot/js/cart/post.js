async function sendRequestAsync(model, url) {
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(model)
    });
    const data = await response.json();
    console.log(data);
}

async function req() {
    const data = localStorage.getItem("pizzaCart");
    //console.log(data);
    //console.log(JSON.parse(data));

    const model = {
        array: JSON.parse(data)
    }    
    await sendRequestAsync(model, "https://localhost:5001/cart/order")
}

function main() {
    const cartMainButton = document.getElementById("cartMainButton");
    cartMainButton.addEventListener("click", req);
}

document.addEventListener("DOMContentLoaded", req);