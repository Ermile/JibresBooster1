using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;

namespace JibresBooster1.lib
{
    class port
    {
        public static Dictionary<string, string> list()
        {
            Dictionary<string, string> portsList = new Dictionary<string, string>();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    string portFullName = queryObj["Caption"].ToString();
                    string portDesc = queryObj["Description"].ToString();

                    int startIndex = portFullName.LastIndexOf("(") + 1;
                    int endIndex = portFullName.Length - startIndex - 1;

                    if (startIndex > 1 & endIndex > 1)
                    {
                        string portName = portFullName.Substring(startIndex, endIndex);
                        portsList.Add(portName, portDesc);

                        Console.WriteLine(portName);
                        Console.WriteLine(portDesc);
                    }
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("Error on get port detail. " + e.Message);
            }

            return portsList;
        }
    }
}
