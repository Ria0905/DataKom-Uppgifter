using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

public class Request
{
    public string? Operation { get; set; }
    public string? Data { get; set; }
}

public class Response
{
    public bool Success { get; set; }
    public string? Message { get; set; }
}

class Program
{
    static void Main()
    {
        try
        {
            UdpClient udpServer = new UdpClient(1089);
            IPEndPoint clientEndpoint = new IPEndPoint(IPAddress.Any, 1089);

            Console.WriteLine("Server is listening on port 1089...");

            while (true)
            {
                byte[] receivedBytes = udpServer.Receive(ref clientEndpoint);
                string receivedData = Encoding.UTF8.GetString(receivedBytes);

                Request request = JsonConvert.DeserializeObject<Request>(receivedData);
                Console.WriteLine($"Client requested: Operation={request.Operation}, Data={request.Data}");

                // Process the request (implement your logic here)
                Response response;

                switch (request.Operation)
                {
                    case "GET_DATA":
                        // In a real application, you would retrieve data from a database or another source.
                        response = new Response
                        {
                            Success = true,
                            Message = "Data retrieved successfully."
                        };
                        break;

                    case "SEND_DATA":
                        // In a real application, you would store the received data.
                        response = new Response
                        {
                            Success = true,
                            Message = "Data received and stored successfully."
                        };
                        break;

                    default:
                        // If the requested operation is not recognized.
                        response = new Response
                        {
                            Success = false,
                            Message = "Invalid operation."
                        };
                        break;
                }

                // Serialize the response object to JSON
                string jsonResponse = JsonConvert.SerializeObject(response);
                byte[] responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

                udpServer.Send(responseBytes, responseBytes.Length, clientEndpoint);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}