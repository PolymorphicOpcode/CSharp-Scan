using System;
using System.Net;

class HTTPScanner : ServiceScanner
{
    private new readonly string _hostname;
    private new readonly int _port;

    public HTTPScanner(string hostname, int port = 80, NetworkCredential credentials = null) 
        : base(hostname, port, credentials) 
    {
        _hostname = hostname;
        _port = port;
    }

    protected override void Scan()
    {
        try
        {
            // Build a URI based on the HTTP protocol, host and port
            var uri = new UriBuilder("http", _hostname, _port).Uri;
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "HEAD";
            request.Timeout = 5000;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"HTTP server at {_hostname}:{_port} returned status code {response.StatusCode}.");
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine($"Error scanning HTTP server: {ex.Message}");
        }
        catch (System.TimeoutException ex)
        {
            Console.WriteLine($"Timeout scanning HTTP server: {ex.Message}");
        }
    }
}
