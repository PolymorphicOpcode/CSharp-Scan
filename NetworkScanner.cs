using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

class NetworkScanner : IpScanner {
    public NetworkScanner(IPAddress ipAddress) : base(ipAddress){}
    
    public static void GetNetworkAndSubnetMaskFromAddress(IPAddress address, out IPAddress network, out IPAddress subnetMask) {
        byte[] ipBytes = address.GetAddressBytes();
        byte[] subnetMaskBytes = new byte[ipBytes.Length];
        int bits = ipBytes.Length * 8;
        int mask = bits;
        for (int i = 0; i < ipBytes.Length; i++) {
            int bitCount = Math.Min(8, mask);
            subnetMaskBytes[i] = (byte) (255 << (8 - bitCount));
            mask -= bitCount;
        }
        network = new IPAddress(ipBytes.Zip(subnetMaskBytes, (a, b) => (byte) (a & b)).ToArray());
        subnetMask = new IPAddress(subnetMaskBytes);
    }


    protected IPAddress GetIpAddressFromNetworkAndOffset(IPAddress networkAddress, int subnetMaskLength, int offset) {
        byte[] networkBytes = networkAddress.GetAddressBytes();
        int hostBitsLength = 32 - subnetMaskLength;
        if (BitConverter.IsLittleEndian) {
            Array.Reverse(networkBytes);
        }
        int ip = BitConverter.ToInt32(networkBytes, 0);
        ip += offset;
        byte[] resultBytes = BitConverter.GetBytes(ip);
        if (BitConverter.IsLittleEndian) {
            Array.Reverse(resultBytes);
        }
        Array.Resize(ref resultBytes, 4);
        return new IPAddress(resultBytes);
    }


    public new void PerformScan() {
        Console.Write("Enter the IP address range to scan (CIDR notation allowed): ");
        string range = Console.ReadLine();

        IPAddress ipAddress;
        if (!IPAddress.TryParse(range, out ipAddress)) {
            Console.WriteLine("Invalid IP address range.");
            return;
        }

        IPAddress networkAddress;
        int subnetMaskLength;
        IPAddress subnetMask;
        GetNetworkAndSubnetMaskFromAddress(ipAddress, out networkAddress, out subnetMask);

        Console.WriteLine($"Scanning network: {networkAddress}/{subnetMask}");

        for (int i = 1; i < (1 << (32 - subnetMask)); i++) {
            IPAddress currentIpAddress = GetIpAddressFromNetworkAndOffset(networkAddress, subnetMaskLength, i);

            Thread thread = new Thread(() => ScanIpAddress(currentIpAddress));
            thread.Start();
        }
    }

    private void ScanIpAddress(IPAddress ipAddress) {
        Ping ping = new Ping();
        PingReply pingReply = ping.Send(ipAddress);

        if (pingReply.Status == IPStatus.Success) {
            Console.WriteLine($"Host found: {ipAddress}");
        }
    }
}
