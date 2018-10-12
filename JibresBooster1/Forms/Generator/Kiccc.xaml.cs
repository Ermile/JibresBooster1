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
using System.Windows.Shapes;

namespace JibresBooster1.Forms.Generator
{
    /// <summary>
    /// Interaction logic for Kiccc.xaml
    /// </summary>
    public partial class Kiccc : Window
    {
        public Kiccc()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            // get active port for kiccc
            var myPort = lib.port.kiccc();
            if (!string.IsNullOrEmpty(myPort))
            {
                txtPort.Text = myPort;
                txtPort.ToolTip = "خودکار شناسایی شده";
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            // show sale box

            string str = "";
            str += "?serial=" + txtSerial.Text;
            str += "&terminal=" + txtTerminal.Text;
            str += "&acceptor=" + txtAcceptor.Text;
            str += "&port=" + txtPort.Text;


        }
    }
}
