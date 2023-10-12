using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

public class Request
{
    public string Operation { get; set; }
    public string Data { get; set; }
}

public class Response
{
    public bool Success { get; set; }
    public string Message { get; set; }
}

class Program
{
    static void Main()
    {
        using (TcpClient client = new TcpClient("localhost", 1089))
        using (NetworkStream stream = client.GetStream())
        using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        {
            // Create and send JSON request to the server
            Request request = new Request
            {
                Operation = "GET_DATA",
                Data = "Requesting data"
            };

            string jsonRequest = JsonConvert.SerializeObject(request);
            writer.WriteLine(jsonRequest);
            writer.Flush();

            // Receive JSON response from the server
            string jsonResponse = reader.ReadLine();
            Response response = JsonConvert.DeserializeObject<Response>(jsonResponse);
            Console.WriteLine($"Server responded: Success={response.Success}, Message={response.Message}");
        }
    }
}
