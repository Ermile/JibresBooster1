using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JibresBooster1.lib
{
    class faxModem
    {
        public SerialPort port = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
        public String sReadData = "";
        public String sNumberRead = "";
        public String sData = "AT#CID=1";


        private void fire(object sender, EventArgs e)
        {
            SetModem();

            ReadModem();

            log.save(sReadData);
        }

        public void SetModem1()
        {

            if (port.IsOpen == false)
            {
                port.Open();
            }

            port.WriteLine(sData + System.Environment.NewLine);
            port.BaudRate = 9600;
            port.DtrEnable = true;
            port.RtsEnable = true;

        }

        public string ReadModem()
        {

            try
            {
                sReadData = port.ReadExisting().ToString();

                return (sReadData);
            }
            catch (Exception ex)
            {
                String errorMessage;
                errorMessage = "Error in Reading: ";
                errorMessage = String.Concat(errorMessage, ex.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, ex.Source);

                log.save(errorMessage + "Error");
                return "";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            port.Close();
            //Close();
        }


        public void SetModem()
        {

            if (port.IsOpen == false)
            {
                port.Open();
            }

            port.WriteLine(sData + System.Environment.NewLine);
            port.BaudRate = 9600;
            port.DtrEnable = true;
            port.RtsEnable = true;

            port.DataReceived += port_DataReceived;

        }

        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //For e.g. display your incoming data in RichTextBox
            log.save(port.ReadLine());

            //OR
            ReadModem();
        }


    }
}
