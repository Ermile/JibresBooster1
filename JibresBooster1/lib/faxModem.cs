using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JibresBooster1.lib
{
    class faxModem
    {
        public static SerialPort port;
        public static String sReadData = "";
        public static String sNumberRead = "";
        public static String sData = "AT#CID=1";


        public static void fire()
        {
            //callTest();
            Call();

            //SetModem();

            //ReadModem();

            //log.save("Read data" + sReadData);
        }



        public static void callTest()
        {
            var myPort = lib.port.faxModem();
            SerialPort SP = new SerialPort(myPort);
            SP.BaudRate = 9600;
            SP.Parity = Parity.None;
            SP.DataBits = 8;
            SP.StopBits = StopBits.One;
            SP.RtsEnable = true;
            SP.DtrEnable = true;
            SP.Encoding = System.Text.Encoding.Unicode;
            SP.ReceivedBytesThreshold = 1;
            SP.NewLine = Environment.NewLine;
            SP.Open();

            string cmd = "AT";
            SP.WriteLine(cmd + "\r");
            SP.Write(cmd + "\r");
            Thread.Sleep(500);
            string ss = SP.ReadExisting();
            if (ss.EndsWith("\r\nOK\r\n"))
            {
                log.save("Modem is connected");
            }
            log.save("status " + ss.ToString());


            SP.Write("ATDT 09357269759" + Environment.NewLine);

            SP.Close();
        }


        private static void Call()
        {
            var myPort = lib.port.faxModem();
            SerialPort celu = new SerialPort();
            celu.PortName = myPort; // You have check what port your phone is using here, and replace it
            celu.Open();
            string cmd = "ATD";  // Here you put your AT command
            string phoneNumber = "09357269759"; // Here you put the phone number, for me it worked just with the phone number, not adding any other area code or something like that
            celu.WriteLine(cmd + phoneNumber + ";\r");
            Thread.Sleep(500);
            string ss = celu.ReadExisting();
            if (ss.EndsWith("\r\nOK\r\n"))
            {
                log.save("Modem is connected \r Calling : " + phoneNumber);
            }
            log.save("Call status " + ss);
            celu.Close();
        }





        public static void SetModem()
        {
            var myPort = lib.port.faxModem();
            port = new SerialPort(myPort, 115200, Parity.None, 8, StopBits.One);
            port.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);

            

            if (port.IsOpen == false)
            {
                port.Open();
                log.save("Open port of fax modem");
            }

            port.WriteLine(sData + System.Environment.NewLine);
            port.BaudRate = 115200;
            port.DtrEnable = true;
            port.RtsEnable = true;

            port.DataReceived += port_DataReceived;
        }



        public static string ReadModem()
        {
            try
            {
                sReadData = port.ReadExisting().ToString();
                log.save("Read modem" + sReadData);

                return (sReadData);
            }
            catch (Exception ex)
            {
                String errorMessage;
                errorMessage = "Error in Reading ";
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

        static string dataReceived = string.Empty;
        private delegate void SetTextDeleg(string text);

        static void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Thread.Sleep(500);
                string x = port.ReadLine(); // will read to the first carriage return
                si_DataReceived(x);
                log.save("d2" + x);
            }
            catch
            { }
        }

        private static void si_DataReceived(string data)
        {
            dataReceived = data.Trim();
            log.save("Trim " + dataReceived);
            // Do whatever with the data that is coming in.
        }



        static void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //For e.g. display your incoming data in RichTextBox
            log.save("Data Received " + port.ReadLine());

            //OR
            ReadModem();
        }


    }
}
