export default function request(uri, type, data, showAlert, alertMessage) {
    fetch(uri,
    {
        method: type,
        headers:
        {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
    .then(() => {
        if (showAlert) {
            alert(alertMessage);
        }
    })
    .catch(error => console.error("Unable to do action.", error));
}
