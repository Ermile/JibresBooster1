using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Text.RegularExpressions;
using Kiccc.Ing.PcPos;
using Kiccc.Ing.PcPos.Serial;

namespace JibresBooster1.PcPos
{
    class JibresKiccc
    {
        private SerialIngenico myKiccc;
        // default value
        private string SerialNo = "5000054981";
        private string AcceptorId = "062006362145616";
        private string TerminalId = "06151815";
        private string cmbCom = "com3";
        private string Amount = "1000";
        private int timeout = 60;



        public void fire(Dictionary<string, string> _args)
        {
            // check input value and fill with default values
            fill(_args);
            // try to connect and sale
            sale();
        }



        private void fill(Dictionary<string, string> _args)
        {
            // amount
            if (_args.ContainsKey("sum"))
            {
                Amount = _args["sum"];
            }
            else
            {
                Amount = "1200";
                Console.WriteLine("sum is empty !");
            }


            // SerialNo
            if (_args.ContainsKey("serial"))
            {
                SerialNo = _args["serial"];
            }
            else
            {
                SerialNo = "5000054981";
                Console.WriteLine("serial is empty !");
            }


            // AcceptorId
            if (_args.ContainsKey("acceptor"))
            {
                AcceptorId = _args["acceptor"];
            }
            else
            {
                AcceptorId = "062006362145616";
                Console.WriteLine("acceptor is empty !");
            }


            // TerminalId
            if (_args.ContainsKey("terminal"))
            {
                TerminalId = _args["terminal"];
            }
            else
            {
                TerminalId = "06151815";
                Console.WriteLine("terminal is empty !");
            }

            
            // cmbCom
            if (_args.ContainsKey("port"))
            {
                cmbCom = _args["port"];
            }
            else
            {
                cmbCom = "com3";
                Console.WriteLine("port is empty !");
            }
        }





        public void sale()
        {
            myKiccc = new SerialIngenico();
            
            try
            {
                // Initiate Service
                myKiccc.InitiateService(SerialNo, AcceptorId, TerminalId, cmbCom, 115200, 8, SerialPortStopBit.One, SerialPortParity.None, timeout);
                Console.WriteLine("Connected to Kiccc pos successfully.");
                //Console.Beep();
                Console.Beep(10000, 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception : {0}\r\nInner Exception : {1}", ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                System.Media.SystemSounds.Exclamation.Play();
            }

            try
            {
                var res = myKiccc.Sale(Amount);
                Console.WriteLine(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception : {0}\r\nInner Exception : {1}", ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty));
            }

            Console.WriteLine("Finish transaction !");
        }


        public string dget(Dictionary<string, string> _dic, string _key)
        {
            string result;
            if (!_dic.TryGetValue(_key, out result))
                return null;
            return result;
        }
    }
}
