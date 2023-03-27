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
                    DNSScanner dnsScanner = new DNSScanner();
                    dnsScanner.PerformScan();
                    break;
                case 2:
                    Console.WriteLine("Server to scan: ");
                    string host = Console.ReadLine();
                    FTPScanner ftpScanner = new FTPScanner(host);
                    ftpScanner.PerformScan();
                    break;
                case 3:
                    HTTPScanner httpScanner = new HTTPScanner();
                    httpScanner.PerformScan();
                    break;
                case 4:
                    IPScanner ipScanner = new IPScanner();
                    ipScanner.PerformScan();
                    break;
                case 5:
                    NetworkScanner networkScanner = new NetworkScanner();
                    networkScanner.PerformScan();
                    break;
                case 6:
                    PortScanner portScanner = new PortScanner();
                    portScanner.PerformScan();
                    break;
                case 7:
                    SMTPScanner smtpScanner = new SMTPScanner();
                    smtpScanner.PerformScan();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please enter a valid number.");
                    break;
            }
        }
    }
}
