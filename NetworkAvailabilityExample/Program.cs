using System;
using System.Net.NetworkInformation;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        NetworkChange.NetworkAvailabilityChanged += OnNetworkAvailabilityChanged;
        Console.WriteLine("Listening for network availability changes...");
        Console.ReadLine();

        NetworkChange.NetworkAvailabilityChanged -= OnNetworkAvailabilityChanged;

        NetworkChange.NetworkAddressChanged += OnNetworkAddressChanged;
        Console.WriteLine("Listening for network address changes...");
        Console.ReadLine();

        NetworkChange.NetworkAddressChanged -= OnNetworkAddressChanged;

        ShowStatistics(NetworkInterfaceComponent.IPv4);
        ShowStatistics(NetworkInterfaceComponent.IPv6);

        PingRemoteHost("fdu.edu").Wait();
    }

    static void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
    {
        Console.WriteLine($"Network Available: {e.IsAvailable}");
    }

    static void OnNetworkAddressChanged(object sender, EventArgs e)
    {
        foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            Console.WriteLine($"{ni.Name} - {ni.OperationalStatus}");
        }
    }

    static void ShowStatistics(NetworkInterfaceComponent version)
    {
        var properties = IPGlobalProperties.GetIPGlobalProperties();
        var stats = version == NetworkInterfaceComponent.IPv4
            ? properties.GetTcpIPv4Statistics()
            : properties.GetTcpIPv6Statistics();

        Console.WriteLine($"TCP {version} Stats:");
        Console.WriteLine($"Connections: {stats.CurrentConnections}");
    }

    static async Task PingRemoteHost(string host)
    {
        Ping ping = new Ping();
        PingReply reply = await ping.SendPingAsync(host);

        Console.WriteLine($"Ping Status: {reply.Status}");
    }
}