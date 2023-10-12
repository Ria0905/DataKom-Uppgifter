using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


class Program
{
    static async Task Main(string[] args)
    {
        var uri = new Uri("ws://localhost:7166/api/websocket");

        using (var client = new ClientWebSocket())
        {
            await client.ConnectAsync(uri, CancellationToken.None);

            _ = Receive(client);

            Console.WriteLine("Connected to WebSocket server. Type a message and press Enter to send.");

            while (true)
            {
                var message = Console.ReadLine();
                await Send(client, message);
            }
        }
    }

    static async Task Send(ClientWebSocket client, string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);
        await client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    static async Task Receive(ClientWebSocket client)
    {
        var buffer = new byte[1024];

        while (client.State == WebSocketState.Open)
        {
            var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);

            Console.WriteLine($"Received message from server: {receivedMessage}");
        }
    }
}
