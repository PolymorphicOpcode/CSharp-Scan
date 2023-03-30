using System;
using System.Net;

class DnsScanner : ServiceScanner
{
    private new readonly string _hostname;
    private new readonly int _port;

    public DnsScanner(string hostname, int port = 53, NetworkCredential credentials = null)
        : base(hostname, port, credentials)
    {
        _hostname = hostname;
        _port = port;
    }

    protected override void Scan()
    {
        try
        {
            var entry = Dns.GetHostEntry(_hostname);
            Console.WriteLine($"DNS resolution succeeded: {entry.HostName} ({entry.AddressList[0]})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DNS resolution failed: {ex.Message}");
        }
    }
}