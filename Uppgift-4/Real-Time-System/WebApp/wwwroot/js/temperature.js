// temperature.js

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/temperatureHub") // Sesuaikan dengan path hub Anda
    .build();

// Tangkap pembaruan suhu dari server
connection.on("ReceiveTemperatureUpdate", function (deviceId, temperature) {
    // Lakukan sesuatu dengan pembaruan suhu yang diterima
    console.log(`Received temperature update for device ${deviceId}: ${temperature}°C`);
});

connection.start()
    .then(function () {
        console.log("Connected to hub");
    })
    .catch(function (err) {
        return console.error(err.toString());
    });
