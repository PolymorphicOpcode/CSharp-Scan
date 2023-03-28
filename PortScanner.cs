using System;
using System.Net;
using System.Net.Sockets;

class PortScanner : ServiceScanner
{
    private readonly int _port;
    private string Hostname;

    public PortScanner(string hostname, int port) : base(hostname)
    {
        _port = port;
        Hostname = hostname;
    }

    protected override void Scan()
    {
        try
        {
            // Create a TCP client and connect to the host & port
            using (var client = new TcpClient())
            {
                client.Connect(Hostname, _port);
                Console.WriteLine($"Port {_port} is open.");
            }
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"Port {_port} is closed. Error message: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scanning port {_port}: {ex.Message}");
        }
    }
}
