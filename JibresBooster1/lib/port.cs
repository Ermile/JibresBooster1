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
                        //Console.WriteLine(portName + ": " + portDesc);
                    }
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("Error on get port detail. " + e.Message);
            }

            return portsList;
        }


        private static string HasOpenPort(string _port)
        {
            var portState = "123";

            
            if (_port != string.Empty & !string.IsNullOrEmpty(_port))
            {
                using (SerialPort serialPort = new SerialPort(_port))
                {
                    foreach (var itm in SerialPort.GetPortNames())
                    {
                        if (itm.Contains(serialPort.PortName))
                        {
                            serialPort.Close();
                            if (serialPort.IsOpen) { portState = "Open"; }
                            else { portState = "Close"; }
                        }
                    }
                }
            }

            else { System.Windows.Forms.MessageBox.Show("Error: No Port Specified."); }

            return portState;
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
            log.info("Kiccc Port " + detectedPort);
            
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
