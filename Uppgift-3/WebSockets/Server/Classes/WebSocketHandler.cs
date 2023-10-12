
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Server.Classes;

namespace Server.Classes
{
    public class WebSocketHandler
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public async Task OnConnected(WebSocket socket)
        {
            // Create a unique ID for each connection (can use GUID)
            var socketId = Guid.NewGuid().ToString();

            // Add socket to the list
            _sockets.TryAdd(socketId, socket);

            // Implement logic when connection is successful
            Console.WriteLine($"Client with ID {socketId} connected.");

            // Send messages to connected clients
            await SendMessageToAllAsync($"Client with ID {socketId} connected.");

            // Handles messages received from connected clients
            await Receive(socket, async (result, buffer) =>
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"Received message from client {socketId}: {message}");

                    // Handle the message logic here
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await OnDisconnected(socketId);
                }
            });
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            try
            {
                while (socket.State == WebSocketState.Open)
                {
                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    handleMessage(result, buffer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Receive: {ex.Message}");
                // Handle error or disconnect socket here if needed
            }
        }

        public async Task OnDisconnected(string socketId)
        {
            WebSocket socket;
            _sockets.TryRemove(socketId, out socket);

            Console.WriteLine($"Client with ID {socketId} disconnected.");

            // Sends a message to the connected client that the connection is lost
            await SendMessageToAllAsync($"Client with ID {socketId} disconnected.");
        }

        public async Task SendMessageToAllAsync(object data)
        {
            foreach (var socket in _sockets.Values)
            {
                await SendMessageAsync(socket, data);
            }
        }

        private async Task SendMessageAsync(WebSocket socket, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var buffer = Encoding.UTF8.GetBytes(json);
                await socket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendMessageAsync: {ex.Message}");
                // Handle error or disconnect socket here if needed
            }
        }
    }
}

