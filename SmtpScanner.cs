using System.Net;
using System.Net.Mail;

class SMTPScanner : ServiceScanner
{
    public SMTPScanner(string hostname, int port = 25, NetworkCredential credentials = null)
        : base(hostname, port, credentials)
    {
    }

    protected override void Scan()
    {
        try
        {
            using (var client = new SmtpClient(base._hostname, base._port))
            {
                if (base._credentials != null)
                {
                    client.Credentials = base._credentials;
                }
                client.Timeout = 5000;
                client.EnableSsl = true;
                client.Send(new MailMessage());
                Console.WriteLine($"SMTP server {base._hostname} is up.");
                PerformScan();
            }
        }
        catch (SmtpException ex)
        {
            Console.WriteLine($"Error connecting to SMTP server {base._hostname}: {ex.Message}");
        }
    }
}