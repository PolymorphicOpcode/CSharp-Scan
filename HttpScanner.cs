using System;
using System.Collections.Generic;
using System.Net;

class HTTPScanner : ServiceScanner {
    private List<string> _results = new List<string>();

    public HTTPScanner() {

    }

    public override void PerformScan() {
        Console.Write("Enter the website to scan (e.g. www.example.com): ");
        string website = Console.ReadLine();

        try {
            // Check if index.html exists
            string indexUrl = $"http://{website}/index.html";
            HttpWebRequest indexRequest = (HttpWebRequest)WebRequest.Create(indexUrl);
            indexRequest.Method = "HEAD";
            using (HttpWebResponse indexResponse = (HttpWebResponse)indexRequest.GetResponse()) {
                if (indexResponse.StatusCode == HttpStatusCode.OK) {
                    _results.Add($"Index file found at {indexUrl}");
                }
            }

            // Check if robots.txt exists
            string robotsUrl = $"http://{website}/robots.txt";
            HttpWebRequest robotsRequest = (HttpWebRequest)WebRequest.Create(robotsUrl);
            robotsRequest.Method = "HEAD";
            using (HttpWebResponse robotsResponse = (HttpWebResponse)robotsRequest.GetResponse()) {
                if (robotsResponse.StatusCode == HttpStatusCode.OK) {
                    _results.Add($"Robots file found at {robotsUrl}");
                }
            }

            if (_results.Count > 0) {
                Console.WriteLine("HTTP Results:");
                foreach (string result in _results) {
                    Console.WriteLine(result);
                }
            }
            else {
                Console.WriteLine("No index or robots files found.");
            }
        }
        catch (Exception ex) {
            Console.WriteLine($"Error scanning HTTP service: {ex.Message}");
        }
    }
}
