using System;
using System.Threading.Tasks;
using IoTSimulator;
using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var hubUrl = "https://127.0.01/chatHub";

        var connection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .Build();

        connection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await connection.StartAsync();
        };

        await connection.StartAsync();

        // Send temperature data every 5 seconds
        while (true)
        {
            var temperature = GenerateRandomTemperature();
            await connection.InvokeAsync("SendTemperatureUpdate", "deviceId", temperature);
            Console.WriteLine($"Temperature sent: {temperature}");

            await Task.Delay(5000);
        }
    }

    private static int GenerateRandomTemperature()
    {
        // Logic for generating temperature data randomly
        var random = new Random();
        return random.Next(0, 100);
    }

    public void SendTemperatureData()
    {
        var encryptionService = new EncryptionService("my_secret_key");
        var temperatureData = "temperature_data_to_encrypt";
        var encryptedData = encryptionService.Encrypt(temperatureData);

        // Send encryptedData to server via SignalR or HTTP
    }
}


