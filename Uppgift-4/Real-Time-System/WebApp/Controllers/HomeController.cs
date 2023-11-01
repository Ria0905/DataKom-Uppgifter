using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<WebApp.Hubs.TemperatureHub> _hubContext;

        //public HomeController(ILogger<WebApp.Hubs.TemperatureHub> hubContext)
        //{
        //    _hubContext = hubContext;
        //}

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    // Get temperature data from the server
        //    var temperatureData = GetTemperatureData();

        //    // Send temperature data to the display
        //    return View("Temperature", temperatureData);
        //}


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }





        // Metode sederhana untuk mendapatkan data temperatur (gantilah dengan logika sesuai kebutuhan)
        //private List<int> GetTemperatureData()
        //{
        //    // Implementasikan logika untuk mendapatkan data temperatur dari server
        //    // Misalnya, panggil metode di proyek konsol aplikasi yang mengirim data temperatur melalui SignalR

        //    // Sebagai contoh, kita akan menggunakan nilai statis

        //    //call a method in the application console project that sends temperature data via SignalR, as an example, I will use a static value
        //    return new List<int> { 25, 26, 27, 24, 23 };
        //}
    }
}