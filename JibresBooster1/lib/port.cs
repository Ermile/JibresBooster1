using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using JibresBooster1.lib;

namespace JibresBooster1.lib
{
    class port
    {
        //https://msdn.microsoft.com/en-us/library/aa394413(v=vs.85).aspx
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

                        log.save(" - " + portName + " - " + portDesc);
                    }
                }
            }
            catch (ManagementException e)
            {
                log.save("Error on get port detail. " + e.Message);
            }

            return portsList;
        }



        public static string kiccc()
        {
            string detectedPort = null;
            foreach (KeyValuePair<string, string> myPort in list())
            {
                if (myPort.Value == "PI USB to Serial")
                {
                    detectedPort = myPort.Key;
                }
            }
            log.save("Kiccc Port\t\t\t" + detectedPort);
            
            return detectedPort;
        }


        public static string faxModem()
        {
            string detectedPort = null;
            foreach (KeyValuePair<string, string> myPort in list())
            {
                if (myPort.Value == "Communications Port")
                {
                    detectedPort = myPort.Key;
                }
            }
            log.save("Fax Modem Port\t\t\t" + detectedPort);

            return detectedPort;
        }


        public static Boolean exist(string _port)
        {
            string[] existPorts = SerialPort.GetPortNames();
            Boolean exist = false;
            foreach (string myPort in existPorts)
            {
                if (myPort == _port)
                {
                    exist = true;
                }
            }

            return exist;
        }
    }
}
