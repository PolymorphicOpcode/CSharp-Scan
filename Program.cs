using System;

class Program {
    static void Main(string[] args) {
        while (true) {
            Console.WriteLine("Please choose a scan type:\n1. DNS Scanner\n2. FTP Scanner\n3. HTTP Scanner\n4. IP Scanner\n5. Network Scanner\n6. Port Scanner\n7. SMTP Scanner\n0. Exit");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice)) {
                Console.WriteLine("Invalid choice, please enter a number.");
                continue;
            }

            switch (choice) {
                case 0:
                    return;
                case 1:
                    Console.Write("Host: ");
                    string host = Console.ReadLine();
                    DnsScanner dnsScanner = new DnsScanner(host);
                    dnsScanner.PerformScan();
                    break;
                case 2:
                    Console.Write("Server to scan: ");
                    string ftphost = Console.ReadLine();
                    FtpScanner ftpScanner = new FtpScanner(ftphost);
                    ftpScanner.PerformScan();
                    break;
                case 3:
                    Console.Write("Host");
                    string httphost = Console.ReadLine();
                    HTTPScanner httpScanner = new HTTPScanner(httphost);
                    httpScanner.PerformScan();
                    break;
                case 4:
                    Console.Write("IP address to scan: ");
                    string ipAddress = Console.ReadLine();
                    IpScanner ipScanner = new IpScanner(System.Net.IPAddress.Parse(ipAddress));
                    ipScanner.PerformScan();
                    break;
                case 5:
                    Console.Write("Enter Network");
                    string nethost = Console.ReadLine();
                    NetworkScanner networkScanner = new NetworkScanner(System.Net.IPAddress.Parse(nethost));
                    networkScanner.PerformScan();
                    break;
                case 6:
                    Console.Write("Host: ");
                    string portIP = Console.ReadLine();
                    Console.Write("Port: ");
                    int port = Int32.Parse(Console.ReadLine());
                    PortScanner portScanner = new PortScanner(portIP, port);
                    portScanner.PerformScan();
                    break;
                case 7:
                    Console.Write("Host: ");
                    string smtphost = Console.ReadLine();
                    SMTPScanner smtpScanner = new SMTPScanner(smtphost);
                    smtpScanner.PerformScan();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please enter a valid number.");
                    break;
            }
        }
    }
}