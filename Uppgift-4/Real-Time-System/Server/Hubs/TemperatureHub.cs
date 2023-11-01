using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Server.Hubs
{
    public class TemperatureHub : Hub
    {

        public async Task SendTemperature(string deviceId, string dto)
        {

            var messageRecieved = JsonConvert.DeserializeObject<DTO>(dto);
            var decryptedTemperature = DecryptTemperature(messageRecieved.Temperature);

            //var decryptedTemperature = messageRecieved.Temperature;

            // Här kan du hantera den dekrypterade temperaturen, t.ex. lagra den i en databas, logga den, etc.

            await Clients.All.SendAsync("UpdateTemperature", deviceId, decryptedTemperature);
        }


        private string DecryptTemperature(string encryptedTemp)
        {

            byte[] Key = Encoding.UTF8.GetBytes("YourSuperSecretK"); 
            byte[] IV = Encoding.UTF8.GetBytes("YourInitVectorHe"); 


            byte[] cipherTextBytes = Convert.FromBase64String(encryptedTemp);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public async Task Device1(string dto)
        {

            var messageRecieved = JsonConvert.DeserializeObject<DTO>(dto);
            var decryptedTemperature = DecryptTemperature(messageRecieved.Temperature);
            await Clients.All.SendAsync("Device1", decryptedTemperature);
        }

        public async Task Device2(string dto)
        {

            var messageRecieved = JsonConvert.DeserializeObject<DTO>(dto);
            var decryptedTemperature = DecryptTemperature(messageRecieved.Temperature);
            await Clients.All.SendAsync("Device2", decryptedTemperature);
        }



        //// This method will be called when a client connects to the hub
        //public async Task OnConnectedAsync()
        //{
        //    // Implement the logic when the client connects
        //    await Clients.All.SendAsync("ReceiveMessage", "Server", "A client has connected");
        //}

        //// This method will be called when the client sends a message to the hub
        //public async Task SendMessage(string user, string message)
        //{
        //    // Implement logic to respond to received messages
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        

        //public async Task SendTemperatureUpdate(string deviceId, double temperature)
        //{
        //    // Send temperature updates to all connected clients
        //    await Clients.All.SendAsync("ReceiveTemperatureUpdate", deviceId, temperature);
        //}
    }
}
