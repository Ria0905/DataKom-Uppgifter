// temperature.js

var connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7084/chathub")
    .build();


connection.on("Device1", function (temperature) {

    console.log(`Received temperature update for device 1: ${temperature}°C`);
});

connection.start()
    .then(function () {
        console.log("Connected to hub");
    })
    .catch(function (err) {
        return console.error(err.toString());
    });
