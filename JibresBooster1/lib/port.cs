using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace JibresBooster1.lib
{
    class port
    {
        public static Dictionary<string, string> list()
        {
            Dictionary<string, string> portList = new Dictionary<string, string>();


            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                var portnames = SerialPort.GetPortNames();
                var ports2 = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

                var portList2   = portnames.Select(n => n + " - " + ports2.FirstOrDefault(s => s.Contains(n))).ToList();

                Console.WriteLine(portnames);
                Console.WriteLine(ports2);
                Console.WriteLine(portList2);

                //foreach (string s in portList2)
                //{ 
                //    portList.Add(node.Name, node.InnerText);
                //    Console.WriteLine(s);
                //}
            }

            

            return portList;
        }
    }
}
