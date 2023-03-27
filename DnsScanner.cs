using System;
using System.Collections.Generic;
using System.Net;
using DnsClient;

class DNSScanner : ServiceScanner {
public override void PerformScan()
{
    Console.Write("Enter the domain name to scan: ");
    string domainName = Console.ReadLine();

    try
    {
        LookupClient client = new LookupClient();
        client.UseCache = true;

        var result = client.Query(domainName, QueryType.A);

        var dnsRecords = result.Answers.ARecords();

        var ips = dnsRecords.Select(r => r.Address.ToString());

        Console.WriteLine($"IP addresses for {domainName}:");
        foreach (var ip in ips)
        {
            Console.WriteLine(ip);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error scanning DNS records: {ex.Message}");
    }
}

}
