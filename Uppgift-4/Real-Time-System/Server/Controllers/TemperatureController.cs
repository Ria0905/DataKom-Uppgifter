using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private readonly IHubContext<TemperatureHub> _hubContext;

        public TemperatureController(IHubContext<TemperatureHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("send-temperature")]
        public IActionResult SendTemperature([FromBody] TemperatureData temperatureData)
        {
            // Proses data suhu dan kirim notifikasi ke perangkat melalui SignalR
            _hubContext.Clients.All.SendAsync("ReceiveTemperature", temperatureData.Value);

            return Ok();
        }
    }
}
