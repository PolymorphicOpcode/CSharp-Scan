using System;
using System.Net;
using System.Net.NetworkInformation;

class IPScanner : ServiceScanner {
    private List<string> _results = new List<string>();

    public IPScanner() {

    }

    public override void PerformScan() {
        Console.Write("Enter the IP address to scan: ");
        string ipAddress = Console.ReadLine();

        try {
            Ping ping = new Ping();
            PingReply reply = ping.Send(IPAddress.Parse(ipAddress));

            if (reply.Status == IPStatus.Success) {
                Console.WriteLine($"{ipAddress} is up.");
            } else {
                Console.WriteLine($"{ipAddress} is down.");
            }
        } catch (Exception ex) {
            Console.WriteLine($"Error scanning IP address: {ex.Message}");
        }
    }

    public bool Ping(IPAddress address) {
        using (Ping ping = new Ping()) {
            PingReply reply = ping.Send(address);
            return (reply.Status == IPStatus.Success);
        }
    }


}
