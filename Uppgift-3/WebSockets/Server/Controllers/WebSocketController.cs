using Microsoft.AspNetCore.Mvc;
using Server.Classes;
using Server.Controllers;
using System.Threading.Tasks;

[Route("api/[controller]")]
public class WebSocketController : Controller
{
    private readonly WebSocketHandler _webSocketHandler;

    public WebSocketController(WebSocketHandler webSocketHandler)
    {
        _webSocketHandler = webSocketHandler;
    }

    [HttpGet]
    public async Task Get()
    {
        var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        await _webSocketHandler.OnConnected(socket);
    }

    [HttpPost]
    public async Task Post([FromBody] YourComplexModel model)
    {
        await _webSocketHandler.SendMessageToAllAsync(model);
    }
}

