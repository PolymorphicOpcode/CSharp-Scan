using System;
using System.Net;

public abstract class ServiceScanner
{
    protected readonly string _hostname;
    protected readonly int _port;
    protected readonly NetworkCredential _credentials;

    protected ServiceScanner(string hostname, int port = 0, NetworkCredential credentials = null)
    {
        _hostname = hostname;
        _port = port;
        _credentials = credentials;
    }

    public virtual void PerformScan()
    {
        Console.WriteLine($"Scanning {_hostname}:{_port}...");
        Scan();
        Console.WriteLine("Scan completed.");
    }

    protected abstract void Scan();
}
