export default function request(uri, type, data, promoCodeAlert, showAlert, alertMessage) {
    fetch(uri, {
        method: type,
        headers:
        {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    }).then(({ status }) => {
        let message = "";
        switch (status) {
            case 201:
            case 200:
                message = "Promo code successfully applied!";
                break;
            case 400:
            case 404:
            case 500:
            default:
                message = "The entered promotional code was not found..";
                break;
        }

        if (promoCodeAlert) {
            alert(message);
        }

        if (showAlert) {
            alert(alertMessage);
        }
    })
}
