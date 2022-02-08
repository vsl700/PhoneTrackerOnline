// Set up the state notificators
var onlineDescs = document.getElementsByClassName("online-desc");

var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub?userId=" + userId).build();
connection.on("sendToUser", (targetCode, value) => {
    for (i = 0; i < onlineDescs.length; i++) {
        if (onlineDescs[i].id == targetCode) {
            onlineDescs[i].style = null; // null because otherwise it sets the display to block, which separates the text from the name, while that way it resets the style as if it has never been set, which is the working way in the case
            break;
        }
    }
});

connection.start().catch(function (err) {
    return console.error(err.toString());
}).then(function () {
    connection.invoke('GetConnectionId').then(function (connectionId) {
        document.getElementById('signalRConnectionId').innerHTML = connectionId;
    })
});