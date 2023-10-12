using System;
using System.IO;
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
        TcpListener server = new TcpListener(IPAddress.Any, 1089);
        server.Start();
        Console.WriteLine("Server is listening on port 1089...");

        while (true)
        {
            using (TcpClient client = server.AcceptTcpClient())
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                // Receive JSON request from the client
                string jsonRequest = reader.ReadLine();
                Request request = JsonConvert.DeserializeObject<Request>(jsonRequest);
                Console.WriteLine($"Client requested: Operation={request.Operation}, Data={request.Data}");

                // Process the request (implement your logic here)
                Response response = new Response
                {
                    Success = true,
                    Message = $"Request '{request.Operation}' processed successfully."
                };

                // Send JSON response back to the client
                string jsonResponse = JsonConvert.SerializeObject(response);
                writer.WriteLine(jsonResponse);
                writer.Flush();
            }
        }
    }
}