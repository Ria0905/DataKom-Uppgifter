using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using WebApp.Models;
using WebApp.Controllers;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext<TemperatureHub> _hubContext;

        public HomeController(IHubContext<TemperatureHub> hubContext)
        {
            _hubContext = hubContext;
        }


        public IActionResult Index()
        {
            // Ambil data temperatur dari server, gantilah dengan logika sesuai kebutuhan Anda
            var temperatureData = GetTemperatureData();

            // Kirim data temperatur ke tampilan
            return View("Temperature", temperatureData);
        }

        // Metode sederhana untuk mendapatkan data temperatur (gantilah dengan logika sesuai kebutuhan)
        private List<int> GetTemperatureData()
        {
            // Implementasikan logika untuk mendapatkan data temperatur dari server
            // Misalnya, panggil metode di proyek konsol aplikasi yang mengirim data temperatur melalui SignalR

            // Sebagai contoh, kita akan menggunakan nilai statis
            return new List<int> { 25, 26, 27, 24, 23 };
        }
    }
}