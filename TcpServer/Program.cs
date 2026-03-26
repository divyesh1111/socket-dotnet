using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string hostName = Dns.GetHostName();
        IPHostEntry host = await Dns.GetHostEntryAsync(hostName);
        IPAddress ipAddress = host.AddressList[0];

        IPEndPoint endpoint = new IPEndPoint(ipAddress, 11000);

        Socket listener = new Socket(
            endpoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);

        listener.Bind(endpoint);
        listener.Listen(10);

        Console.WriteLine("Server started...");

        Socket handler = await listener.AcceptAsync();

        byte[] buffer = new byte[1024];
        int received = await handler.ReceiveAsync(buffer);

        string data = Encoding.UTF8.GetString(buffer, 0, received);
        Console.WriteLine("Received: " + data);

        string response = "ACK";
        byte[] msg = Encoding.UTF8.GetBytes(response);

        await handler.SendAsync(msg);

        Console.WriteLine("Response sent");

        Console.ReadLine();
    }
}