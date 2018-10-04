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
        private string SerialNo;
        private string AcceptorId;
        private string TerminalId;
        private string cmbCom;
        private string Amount;
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
            else if (_args.ContainsKey("test"))
            {
                Amount = "1200";
                Console.WriteLine("use test amount \t" + Amount);
            }
            else
            {
                Console.WriteLine("sum is empty !");
                Console.Beep(1000, 200);
            }


            // SerialNo
            if (_args.ContainsKey("serial"))
            {
                SerialNo = _args["serial"];
            }
            else if (_args.ContainsKey("test"))
            {
                SerialNo = "5000054981";
                Console.WriteLine("use test serial \t" + SerialNo);
            }
            else
            {
                Console.WriteLine("serial is empty !");
                Console.Beep(100, 100);
            }


            // AcceptorId
            if (_args.ContainsKey("acceptor"))
            {
                AcceptorId = _args["acceptor"];
            }
            else if (_args.ContainsKey("test"))
            {
                AcceptorId = "062006362145616";
                Console.WriteLine("use test acceptor \t" + AcceptorId);
            }
            else
            {
                Console.WriteLine("acceptor is empty !");
                Console.Beep(100, 100);
            }


            // TerminalId
            if (_args.ContainsKey("terminal"))
            {
                TerminalId = _args["terminal"];
            }
            else if (_args.ContainsKey("test"))
            {
                TerminalId = "06151815";
                Console.WriteLine("use test terminal \t" + TerminalId);
            }
            else
            {
                Console.WriteLine("terminal is empty !");
                Console.Beep(100, 100);
            }

            
            // cmbCom
            if (_args.ContainsKey("port"))
            {
                cmbCom = _args["port"];
            }
            else if (_args.ContainsKey("test"))
            {
                cmbCom = "com3";
                Console.WriteLine("use test port \t\t" + cmbCom);
            }
            else
            {
                Console.WriteLine("port is empty !");
                Console.Beep(100, 100);
            }
        }





        public void sale()
        {
            myKiccc = new SerialIngenico();
            
            // try to connect
            try
            {
                // Initiate Service
                myKiccc.InitiateService(SerialNo, AcceptorId, TerminalId, cmbCom, 115200, 8, SerialPortStopBit.One, SerialPortParity.None, timeout);
                Console.WriteLine("\nConnected to Kiccc pos successfully.");
                //Console.Beep();
                Console.Beep(10000, 100);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception : {0}\r\nInner Exception : {1}", ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                System.Media.SystemSounds.Exclamation.Play();
            }

            // try to sale
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

            // free resource
            myKiccc.Dispose();

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
