var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub?userId=" + userId).build();
connection.on("sendToUser", (targetCode, value) => {
    var heading = document.createElement("h3");
    heading.textContent = targetCode;
    var p = document.createElement("p");
    p.innerText = value;
    var div = document.createElement("div");
    div.appendChild(heading);
    div.appendChild(p);
    document.getElementById("locsList").appendChild(div);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
}).then(function () {
    //document.getElementById("user").innerHTML = "UserId: " + userId;
    connection.invoke('GetConnectionId').then(function (connectionId) {
        document.getElementById('signalRConnectionId').innerHTML = connectionId;
    })
});