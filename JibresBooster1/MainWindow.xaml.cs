using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Web.Script.Serialization;
using System.IO.Ports;
using System.Management;


namespace JibresBooster1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                lib.listener.runListener();
            }
            catch
            {
                Console.WriteLine("Error on running program!");
            }


            //var portsList = SerialPort.GetPortNames();
            //var portsObj = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'");
            //var portsDetail = portsObj.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

            //// Display each port name to the console.
            //foreach (string portName in portsList)
            //{
            //    Console.WriteLine("Port... "+ portName);
            //    var portNames = portsDetail.FirstOrDefault(s => s.Contains(portName));
            //    Console.WriteLine(portName);
            //}


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

                    if (startIndex > 1)
                    {
                        string portName = portFullName.Substring(startIndex, endIndex);
                        portsList.Add(portName, portDesc);
                    }
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("Error on get port detail. " + e.Message);
            }


        }

        private void btnJibresWebsite_Click(object sender, RoutedEventArgs e)
        {
            Console.Beep();
            System.Diagnostics.Process.Start("https://jibres.com");
        }

    }

}
