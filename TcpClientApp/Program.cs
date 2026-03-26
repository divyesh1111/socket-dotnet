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

        Socket client = new Socket(
            endpoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);

        await client.ConnectAsync(endpoint);

        string message = "Hello Server";
        byte[] data = Encoding.UTF8.GetBytes(message);

        await client.SendAsync(data);

        byte[] buffer = new byte[1024];
        int received = await client.ReceiveAsync(buffer);

        Console.WriteLine("Server says: " +
            Encoding.UTF8.GetString(buffer, 0, received));

        Console.ReadLine();
    }
}