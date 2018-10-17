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
using JibresBooster1.lib;


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
                log.save("Application started.");
                lib.listener.runListener();
            }
            catch(Exception e)
            {
                log.save("Error on running program! " + e.Message);
            }

            
        }

        private void BtnIranKishTest_Click(object sender, RoutedEventArgs e)
        {
            var kicccWindow = new Forms.Generator.Kiccc();
            kicccWindow.Show();
        }

        private void BtnFaxModem_Click(object sender, RoutedEventArgs e)
        {
            lib.faxModem.fire();
        }

        private void ImgLogo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.Beep();
            System.Diagnostics.Process.Start("https://jibres.com");
        }

        private void btnAsanPardakht_Click(object sender, RoutedEventArgs e)
        {
            lib.PcPos.Asnapardakht.fire();
        }
    }

}
