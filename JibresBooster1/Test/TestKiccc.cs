using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JibresBooster1.Test
{
    public partial class TestKiccc: Form
    {
        public TestKiccc()
        {
            InitializeComponent();
        }

        private void TestKiccc_Load(object sender, EventArgs e)
        {
            // get active port for kiccc
            var myPort = lib.port.kiccc();
            if (!string.IsNullOrEmpty(myPort))
            {
                txtPort.Text = myPort;
            }
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            // show sale box
            groupSale.Visible = true;

            string str = "";
            str += "?serial=" + txtSerial.Text;
            str += "&terminal=" + txtTerminal.Text;
            str += "&acceptor=" + txtAcceptor.Text;
            str += "&port=" + txtPort.Text;

        }
    }
}
