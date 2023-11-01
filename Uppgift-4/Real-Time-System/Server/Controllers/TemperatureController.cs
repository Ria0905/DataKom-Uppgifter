//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Server.Hubs;

//namespace Server.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TemperatureController : ControllerBase
//    {
//        private readonly IHubContext<TemperatureHub> _hubContext;

//        public TemperatureController(IHubContext<TemperatureHub> hubContext)
//        {
//            _hubContext = hubContext;
//        }


//        [HttpPost("send-temperature")]
//        public IActionResult SendTemperature([FromBody] TemperatureData temperatureData)
//        {
//            // Process temperature data and send notifications to devices via SignalR
//            _hubContext.Clients.All.SendAsync("ReceiveTemperature", temperatureData.DeviceId, temperatureData.Value);

//            return Ok();
//        }


//    }
//}
