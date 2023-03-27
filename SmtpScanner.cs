using System;
using System.Net;
using System.Net.Mail;

class SMTPScanner : ServiceScanner {
    public SMTPScanner() {

    }

    public override void PerformScan() {
        Console.Write("Enter the SMTP server address: ");
        string server = Console.ReadLine();

        Console.Write("Enter the username: ");
        string username = Console.ReadLine();

        Console.Write("Enter the password: ");
        string password = Console.ReadLine();

        try {
            using (SmtpClient client = new SmtpClient(server)) {
                client.Credentials = new NetworkCredential(username, password);
                client.Timeout = 5000;

                client.Send(new MailMessage("test@test.com", "test@test.com", "Test subject", "Test body"));

                Console.WriteLine("SMTP login successful.");
            }
        } catch (Exception ex) {
            Console.WriteLine($"SMTP login failed: {ex.Message}");
        }
    }
}
