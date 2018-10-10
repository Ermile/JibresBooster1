using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Text.RegularExpressions;
using Kiccc.Ing.PcPos;
using Kiccc.Ing.PcPos.Serial;
using JibresBooster1.lib;
using System.Threading;

namespace JibresBooster1.lib.PcPos
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
        private string info1;
        private string info2;
        private string info3;
        private string info4;
        private int timeout = 60;



        public void fire(Dictionary<string, string> _args)
        {
            // amount
            if (_args.ContainsKey("reset"))
            {
                myKiccc.ResetService();
            }
            else if (_args.ContainsKey("terminate"))
            {
                myKiccc.TerminateService();
            }
            else if (_args.ContainsKey("twice"))
            {

            }
            else
            {
                // create new instance
                myKiccc = new SerialIngenico();
                
                // define received function to get async result
                myKiccc.ResponseReceived += (s, ev) =>
                {
                    Console.WriteLine(111);
                    Console.WriteLine(ev.Response);
                };

                // check input value and fill with default values
                fill(_args);
                // terminate old connection
                myKiccc.TerminateService();
                // try to connect to device
                if(connect())
                {
                    // try to sale
                    if (saleAsync())
                    {
                        // send to server
                        Console.WriteLine("Successfully finish transaction !");
                    }
                    else
                    {
                        Console.WriteLine("Error on transaction !");
                    }

                    // free resource
                    // myKiccc.Dispose();
                    // terminate connection
                    // myKiccc.TerminateService();
                }

                
            }
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
                log.init("use test amount \t" + Amount);
            }
            else
            {
                log.init("sum is empty !");
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
                log.init("use test serial \t" + SerialNo);
            }
            else
            {
                log.init("serial is empty !");
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
                log.init("use test acceptor \t" + AcceptorId);
            }
            else
            {
                log.init("acceptor is empty !");
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
                log.init("use test terminal \t" + TerminalId);
            }
            else
            {
                log.init("terminal is empty !");
                Console.Beep(100, 100);
            }

            
            // cmbCom
            if (_args.ContainsKey("port"))
            {
                cmbCom = _args["port"];
            }
            else
            {
                cmbCom = lib.port.kiccc();
                log.init("Detected port\t\t" + cmbCom);
            }


            // info1
            if (_args.ContainsKey("info1"))
            {
                info1 = _args["info1"];
            }
            else if (_args.ContainsKey("info"))
            {
                info1 = "ارمایل ارائه دهنده راهکارهای مدرن نرم افزاری";
                info1 = "1";
            }
            info1 = str.Left(info1, 24);

            // info2
            if (_args.ContainsKey("info2"))
            {
                info2 = _args["info2"];
            }
            else if (_args.ContainsKey("info"))
            {
                info2 = "2";
            }
            info2 = str.Left(info2, 24);

            // info3
            if (_args.ContainsKey("info3"))
            {
                info3 = _args["info3"];
            }
            else if (_args.ContainsKey("info"))
            {
                info3 = "3";
            }
            info3 = str.Left(info3, 24);

            // info4
            if (_args.ContainsKey("info4"))
            {
                info4 = _args["info4"];
            }
            else if (_args.ContainsKey("info"))
            {
                info4 = "ارمایل ارائه دهنده راهکارهای مدرن نرم افزاری";
            }
            info4 = str.Left(info4, 24);
        }



        private Boolean connect()
        {
            // try to connect
            try
            {
                reset();
                // Initiate Service
                myKiccc.InitiateService(SerialNo, AcceptorId, TerminalId, cmbCom, 115200, 8, SerialPortStopBit.One, SerialPortParity.None, timeout);
                Console.WriteLine("\nConnected to Kiccc pos successfully.");
                //Console.Beep();
                Console.Beep(10000, 100);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception : {0}\r\nInner Exception : {1}", ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                System.Media.SystemSounds.Exclamation.Play();
            }

            return false;
        }


        private Boolean reset()
        {
            try
            {
                // reset old connection before create new one
                myKiccc.ResetService();
                Thread.Sleep(5000);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error on reset Exception : {0}\r\nInner Exception : {1}", ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                System.Media.SystemSounds.Exclamation.Play();
            }

            return false;
        }


        private Boolean sale()
        {
            // try to sale
            try
            {
                if (string.IsNullOrEmpty(info1))
                {
                    var res = myKiccc.Sale(Amount);
                    var xml = lib.reader.xml(res);
                    Console.WriteLine(res);
                    Console.WriteLine(lib.str.fromDic(xml));

                    return true;
                }
                else
                {
                    Console.WriteLine("\n\n\t Info1 \t" + info1);
                    Console.WriteLine("\t Info2 \t" + info2);
                    Console.WriteLine("\t Info3 \t" + info3);
                    Console.WriteLine("\t Info4 \t" + info4);
                    var res = myKiccc.SaleWithExtraParamAndPrintableInfo(Amount, "1", info1, info2, info3, info4);
                    Console.WriteLine(res);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception : {0}\r\nInner Exception : {1}", ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty));
            }
            return false;
        }


        private Boolean saleAsync()
        {
            // try to sale
            try
            {
                if (string.IsNullOrEmpty(info1))
                {
                    var res = myKiccc.BeginSale(Amount);
                    Console.WriteLine("Async sale result " + res);

                    return true;
                }
                else
                {
                    Console.WriteLine("\n\n\t Info1 \t" + info1);
                    Console.WriteLine("\t Info2 \t" + info2);
                    Console.WriteLine("\t Info3 \t" + info3);
                    Console.WriteLine("\t Info4 \t" + info4);
                    var res = myKiccc.BeginSaleWithExtraParamAndPrintableInfo(Amount, "1", info1, info2, info3, info4);
                    Console.WriteLine("Async sale result with info " + res);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception : {0}\r\nInner Exception : {1}", ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty));
            }
            return false;
        }

    }
}
