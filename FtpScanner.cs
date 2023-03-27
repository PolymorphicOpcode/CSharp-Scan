using System;
using System.Net;

class FTPScanner : ServiceScanner
{
    private readonly string _hostname;
    private readonly int _port;
    private readonly NetworkCredential _credentials;
    private bool _anonymousLoginAllowed;

    public FTPScanner(string hostname, int port = 21, NetworkCredential credentials = null)
    {
        _hostname = hostname;
        _port = port;
        _credentials = credentials;
    }

    public override void PerformScan()
    {
        try
        {
            // Build a URI based on the FTP protocol, host and port
            var uri = new UriBuilder("ftp", _hostname, _port).Uri;
            var request = (FtpWebRequest)WebRequest.Create(uri);

            request.Credentials = _credentials ?? new NetworkCredential("anonymous", "anonymous");
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Timeout = 5000;

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                _anonymousLoginAllowed = true;
                Console.WriteLine("Anonymous login allowed.");
            }
        }
        catch (WebException ex)
        {
            var response = (FtpWebResponse)ex.Response;
            if (response.StatusCode == FtpStatusCode.NotLoggedIn)
            {
                _anonymousLoginAllowed = false;
                Console.WriteLine("Anonymous login not allowed.");
            }
            else if (ex.Status == WebExceptionStatus.Timeout)
            {
                Console.WriteLine("Timeout scanning FTP server.");
            }
            else
            {
                Console.WriteLine($"Error scanning FTP server: {ex.Message}");
            }
        }
    }
}
