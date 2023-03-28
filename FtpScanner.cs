using System;
using System.Net;

class FtpScanner : ServiceScanner
{
    public FtpScanner(string hostname, int port = 21, NetworkCredential credentials = null)
        : base(hostname, port, credentials)
    {
    }

    protected override void Scan()
    {
        try
        {
            var uri = new UriBuilder("ftp", _hostname, _port).Uri;
            var request = (FtpWebRequest)WebRequest.Create(uri);

            request.Credentials = _credentials ?? new NetworkCredential("anonymous", "anonymous");
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine("Anonymous login allowed.");
            }
        }
        catch (WebException ex)
        {
            var response = (FtpWebResponse)ex.Response;
            if (response.StatusCode == FtpStatusCode.NotLoggedIn)
            {
                Console.WriteLine("Anonymous login not allowed.");
            }
            else
            {
                Console.WriteLine($"Error scanning FTP server: {ex.Message}");
            }
        }
    }
}
