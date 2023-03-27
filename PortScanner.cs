using System;
using System.Net;
using System.Net.Sockets;

class PortScanner : ServiceScanner {
    public override void PerformScan() {
        Console.Write("Enter the IP address to scan: ");
        string ipAddress = Console.ReadLine();

        int[] ports = new int[] {80, 22, 443};

        foreach (int port in ports) {
            try {
                using (TcpClient tcpClient = new TcpClient()) {
                    tcpClient.Connect(ipAddress, port);
                    Console.WriteLine($"Port {port} is open.");
                }
            } catch (Exception) {
                Console.WriteLine($"Port {port} is closed.");
            }
        }
    }
}