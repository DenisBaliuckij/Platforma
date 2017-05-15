using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPRanger
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args==null || args.Count()<2)
            {
                Console.WriteLine("Please inter IP Address and subnet mask");
                return;
            }
            var splittedAddress = args[0].Split('.');
            var splittedSubnetMask = args[1].Split('.');
            var firstOctet = int.Parse(splittedAddress[0]);
            var secondOctet = int.Parse(splittedAddress[1]);
            var thirdOctet = int.Parse(splittedAddress[2]);
            var fourthOctet = int.Parse(splittedAddress[3]);
            var firstOctetSubnet = int.Parse(splittedSubnetMask[0]);
            var secondOctetSubnet = int.Parse(splittedSubnetMask[1]);
            var thirdOctetSubnet = int.Parse(splittedSubnetMask[2]);
            var fourthOctetSubnet = int.Parse(splittedSubnetMask[3]);
            IPClasses? ipClass = null;
            if (firstOctet == 127)
            {
                Console.WriteLine("You have entered reserved loopback address");
                return;
            }
            if (firstOctet >= 1 && firstOctet <= 126)
                ipClass = IPClasses.A;
            else if (firstOctet >= 128 && firstOctet <= 192)
                ipClass = IPClasses.B;
            else if (firstOctet >= 193 && firstOctet <= 223)
                ipClass = IPClasses.C;
            int[] defaultSubnetBroadcast = new int[4];
            int[] subnetForThatClass = new int[4];
            if (!ipClass.HasValue)
            {
                Console.WriteLine("E and F classes are used only for scientific purpose");
            }
            switch (ipClass.Value)
            {
                case IPClasses.A:
                    defaultSubnetBroadcast[0] = firstOctet;
                    subnetForThatClass[0] = 255;
                    break;
                case IPClasses.B:
                    defaultSubnetBroadcast[0] = firstOctet;
                    defaultSubnetBroadcast[1] = secondOctet;
                    subnetForThatClass[0] = 255;
                    subnetForThatClass[1] = 255;
                    break;
                case IPClasses.C:
                    defaultSubnetBroadcast[0] = firstOctet;
                    defaultSubnetBroadcast[1] = secondOctet;
                    defaultSubnetBroadcast[2] = thirdOctet;
                    subnetForThatClass[0] = 255;
                    subnetForThatClass[1] = 255;
                    subnetForThatClass[2] = 255;
                    break;
            }
            var byteIP = IPAddress.Parse(args[0]);
            var byteSubnet = IPAddress.Parse(args[1]);
            var calculatedBytesIP =  byteIP.GetAddressBytes();
            var calculatedBytesSubnet = byteSubnet.GetAddressBytes();
            List<int> defaultBroadcast = new List<int>();
            for (int i = 0; i < calculatedBytesIP.Count(); i++)
            {
                defaultBroadcast.Add(calculatedBytesIP[i] & calculatedBytesSubnet[i]);
                Console.Write(calculatedBytesIP[i] & calculatedBytesSubnet[i] );
                Console.Write('.');
                
            }

            Console.WriteLine();
            Console.WriteLine("Default broadcast");
            //Тут я хотел вывести каждый сабнет с броадкастом и мультикастом
            Console.ReadKey();
        }
    }

    enum IPClasses
    {
        A,
        B,
        C,
        D,
        E
    }
}
