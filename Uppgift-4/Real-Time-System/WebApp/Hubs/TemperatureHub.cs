using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace WebApp.Hubs
{
    public class TemperatureHub : Hub
    {
        public async Task SendTemperatureUpdate(string deviceId, double temperature)
        {
            // Kirim pembaruan suhu ke semua klien yang terhubung
            await Clients.All.SendAsync("ReceiveTemperatureUpdate", deviceId, temperature);
        }
    }
}