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
            // Initialize UDP Client
            UdpClient udpClient = new UdpClient();
            IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1089);

            // Create and send a JSON request to the server
            Request request = new Request
            {
                Operation = "GET_DATA",
                Data = "Requesting data"
            };

            // Serialize the request object to JSON
            string jsonRequest = JsonConvert.SerializeObject(request);
            byte[] requestBytes = Encoding.UTF8.GetBytes(jsonRequest);

            // Send the request to the server
            udpClient.Send(requestBytes, requestBytes.Length, serverEndpoint);

            // Receive the JSON response from the server
            byte[] receivedBytes = udpClient.Receive(ref serverEndpoint);
            string receivedData = Encoding.UTF8.GetString(receivedBytes);

            // Deserialize the received JSON data into a Response object
            Response response = JsonConvert.DeserializeObject<Response>(receivedData);

            // Display the server's response
            Console.WriteLine($"Server responded: Success={response.Success}, Message={response.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}