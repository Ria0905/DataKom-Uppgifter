using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Server.Hubs
{
    public class TemperatureHub : Hub
    {
        // Metode ini akan dipanggil ketika klien terhubung ke hub
        public async Task OnConnectedAsync()
        {
            // Implementasikan logika ketika klien terhubung
            await Clients.All.SendAsync("ReceiveMessage", "Server", "A client has connected");
        }

        // Metode ini akan dipanggil ketika klien mengirim pesan ke hub
        public async Task SendMessage(string user, string message)
        {
            // Implementasikan logika untuk menanggapi pesan yang diterima
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // Tambahkan metode lain sesuai kebutuhan Anda

        public async Task SendTemperatureUpdate(string deviceId, double temperature)
        {
            // Kirim pembaruan suhu ke semua klien yang terhubung
            await Clients.All.SendAsync("ReceiveTemperatureUpdate", deviceId, temperature);
        }
    }
}
