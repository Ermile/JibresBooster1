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


            string[] ports = SerialPort.GetPortNames();

            Console.WriteLine("The following serial ports were found:");

            // Display each port name to the console.
            foreach (string port in ports)
            {
                Console.WriteLine("Port... "+ port);
            }


            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
            {
                var portnames = SerialPort.GetPortNames();
                var ports2 = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

                var portList = portnames.Select(n => n + " - " + ports2.FirstOrDefault(s => s.Contains(n))).ToList();

                foreach (string s in portList)
                {
                    Console.WriteLine(s);
                }
            }


            Console.WriteLine("----------------");
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine(queryObj["Availability"]);
                    Console.WriteLine(queryObj["Caption"]);
                    Console.WriteLine(queryObj["Description"]);
                    Console.WriteLine(queryObj["DeviceID"]);
                    Console.WriteLine(queryObj["InstallDate"]);
                    Console.WriteLine(queryObj["LastErrorCode"]);
                    Console.WriteLine("1");
                    Console.WriteLine(queryObj["Name"]);
                    Console.WriteLine(queryObj["Status"]);
                    Console.WriteLine(queryObj["SystemName"]);

                    Console.WriteLine("2");
                    Console.WriteLine(queryObj["CreationClassName"]);
                    Console.WriteLine(queryObj["PNPDeviceID"]);
                    //Console.WriteLine(queryObj["ProtocolSupported"]);
                    //Console.WriteLine(queryObj["ProviderType"]);
                    //Console.WriteLine(queryObj["SystemCreationClassName"]);
                    Console.WriteLine("3");
                    //Console.WriteLine(queryObj["ProviderType"]);
                    //Console.WriteLine(queryObj["TimeOfLastReset"]);
                    //Console.WriteLine(queryObj["Binary"]);


                    Console.WriteLine("*******");
                }

            }
            catch (ManagementException e)
            {
                MessageBox.Show(e.Message);
            }


        }

        private void btnJibresWebsite_Click(object sender, RoutedEventArgs e)
        {
            Console.Beep();
            System.Diagnostics.Process.Start("https://jibres.com");
        }

    }

}
