using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IOT_Client;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

class Program
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("YourSuperSecretK"); 
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("YourInitVectorHe");


    static async Task Main(string[] args)
    {
        var hubUrl = "https://localhost:7084/chatHub";

        var connection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .Build();

        await connection.StartAsync();


        // Send temperature data every 5 seconds
        while (true)
        {
            var temperature = GenerateRandomTemperature();
            var encryptedTemperature = EncryptTemperature(temperature);
            var dto = new DTO { Temperature = encryptedTemperature, TimeStamp = DateTime.Now };

            var jsonToSend = JsonConvert.SerializeObject(dto);



            await connection.SendAsync("Device1", jsonToSend);
            await connection.SendAsync("Device2", jsonToSend);
            await Task.Delay(5000);
        }
    }

    static double GenerateRandomTemperature()
    {
        // Logic for generating temperature data randomly
        return new Random().NextDouble() * 40.0;
        //return random.Next(0, 100);
    }

    static string EncryptTemperature(double temperature)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(temperature.ToString());
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }



    //    public void SendTemperatureData()
    //{
    //    var encryptionService = new EncryptionService("my_secret_key");
    //    var temperatureData = "temperature_data_to_encrypt";
    //    var encryptedData = encryptionService.Encrypt(temperatureData);

    //    // Send encryptedData to server via SignalR or HTTP
    }
}


