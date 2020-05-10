using System;
using System.Windows;
using System.Windows.Input;

namespace JibresBooster1.Forms.Generator
{
    /// <summary>
    /// Interaction logic for Kiccc.xaml
    /// </summary>
    public partial class Kiccc : Window
    {
        private string testLink;
        public Kiccc()
        {
            InitializeComponent();
            genereateLink();
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
            genereateLink();
        }


        private void genereateLink()
        {
            // show sale box
            string linkStr = lib.listener.JibresLocalServer;
            linkStr += "?type=PcPosKiccc";
            linkStr += "&serial=" + txtSerial.Text;
            linkStr += "&terminal=" + txtTerminal.Text;
            linkStr += "&acceptor=" + txtAcceptor.Text;
            linkStr += "&port=" + txtPort.Text;
            linkStr += "&sum=" + txtSum.Text;

            txtLink.Text = linkStr;
            testLink = linkStr;
        }


        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(testLink);
        }
    }
}
