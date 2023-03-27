using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

class NetworkScanner : ServiceScanner {
    private List<string> _results = new List<string>();

    public NetworkScanner() {

    }

    public override void PerformScan() {
        Console.Write("Enter the network to scan in CIDR format (e.g. 192.168.1.0/24): ");
        string network = Console.ReadLine();

        try {
            IPAddress networkAddress = IPAddress.Parse(network.Substring(0, network.IndexOf("/")));
            int subnetMaskLength = int.Parse(network.Substring(network.IndexOf("/") + 1));

            byte[] subnetMaskBytes = Enumerable.Range(0, 4)
                .Select(i => i < subnetMaskLength / 8 ? (byte)255 : (byte)(255 << (8 - subnetMaskLength % 8)))
                .ToArray();

            IPAddress subnetMask = new IPAddress(subnetMaskBytes);
            uint subnetMaskValue = BitConverter.ToUInt32(subnetMaskBytes.Reverse().ToArray(), 0);

            byte[] networkAddressBytes = networkAddress.GetAddressBytes();
            uint networkAddressValue = BitConverter.ToUInt32(networkAddressBytes.Reverse().ToArray(), 0);

            uint broadcastAddressValue = networkAddressValue | ~subnetMaskValue;

            IPAddress broadcastAddress = new IPAddress(BitConverter.GetBytes(broadcastAddressValue).Reverse().ToArray());

            Console.WriteLine($"Scanning network {network}...");

            for (uint i = networkAddressValue + 1; i < broadcastAddressValue; i++) {
                PingReply reply = null;
                byte[] addressBytes = BitConverter.GetBytes(i).Reverse().ToArray();
                IPAddress address = new IPAddress(addressBytes);

                IPScanner scanner = new IPScanner();
                bool isSuccessful = scanner.Ping(address);
                if (isSuccessful) {
                    Console.WriteLine($"{address} is up.");
                }


                if (reply.Status == IPStatus.Success) {
                    Console.WriteLine($"{address} is up.");
                }
            }
        } catch (Exception ex) {
            Console.WriteLine($"Error scanning network: {ex.Message}");
        }
    }
}
