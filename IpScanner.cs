using System;
using System.Net;
using System.Net.Sockets;

class IpScanner : ServiceScanner
{
    protected readonly string _ipAddress;
    protected readonly int _port;

    public IpScanner(IPAddress ipAddress, int port = 80, NetworkCredential credentials = null)
        : base(ipAddress.ToString(), port, credentials)
    {
        _ipAddress = ipAddress.ToString();
        _port = port;
    }

    protected override void Scan() {
    // Build a string with the IP address and port
    var endpoint = $"{_ipAddress}:{_port}";

    try
    {
        // Create a TCP client and connect to the endpoint
        using (var client = new TcpClient())
        {
            client.ReceiveTimeout = 5000;
            client.SendTimeout = 5000;
            client.Connect(_ipAddress, _port);
            Console.WriteLine($"Connected to {endpoint}");
        }
    }
    catch (SocketException ex)
    {
        Console.WriteLine($"Error connecting to {endpoint}: {ex.Message}");
    }
}
}
