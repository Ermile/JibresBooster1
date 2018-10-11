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
        private static SerialIngenico myKiccc;
        private static Boolean INIT;
        private static Boolean BUSY;

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
        private int timeout = 300;



        public void fire(Dictionary<string, string> _args)
        {
            // if in BUSY mode do nothing and say cancel old request
            if (BUSY)
            {
                log.save("Please cancel old request before send new one! " + BUSY);
                //return;
            }

            if(!INIT)
            {
                // create new instance
                myKiccc = new SerialIngenico();
                
                // define received function to get async result
                myKiccc.ResponseReceived += (s, ev) =>
                {
                    // get response and send it to server to save
                    //ev.Response.ToString()
                    
                    log.save("Pos response is" + reader.xmlReadable(ev.Response.ToString()));

                    BUSY = false;
                    log.save("BUSY " + BUSY);
                };

                // set init to true for next times
                INIT = true;
            }
            // get current state
            state();


            if (_args.ContainsKey("reset"))
            {
                reset();
            }
            else if (_args.ContainsKey("terminate"))
            {
                terminate();
            }
            else if (_args.ContainsKey("twice"))
            {

            }
            else if (_args.ContainsKey("state"))
            {
                state();
            }
            else
            {
                // check input value and fill with default values
                fill(_args);
                // try to connect to device
                if (connect())
                {
                    // try to sale
                    if (saleAsync())
                    {
                        // send to server
                        log.save("\t\t\t\t\t... Waiting response ...");
                    }
                    else
                    {
                        log.save("Error on send sale transaction!");
                    }
                }
            }                
        }



        private void fill(Dictionary<string, string> _args)
        {
            if (_args.ContainsKey("test"))
            {
                log.save("TEST MODE");
            }

            // amount
            if (_args.ContainsKey("sum"))
            {
                Amount = _args["sum"];
            }
            else if (_args.ContainsKey("test"))
            {
                Amount = "1200";
                log.save("\t test amount \t" + Amount);
            }
            else
            {
                log.save("sum is empty !");
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
                log.save("\t test serial \t" + SerialNo);
            }
            else
            {
                log.save("serial is empty !");
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
                log.save("\t test acceptor \t" + AcceptorId);
            }
            else
            {
                log.save("acceptor is empty !");
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
                log.save("\t test terminal \t" + TerminalId);
            }
            else
            {
                log.save("terminal is empty !");
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
                if(string.IsNullOrEmpty(cmbCom))
                {
                    log.save("*** PcPos is not detected ***");
                    return false;
                }
                if(port.exist(cmbCom))
                {
                    terminate();
                    // Initiate Service
                    myKiccc.InitiateService(SerialNo, AcceptorId, TerminalId, cmbCom, 115200, 8, SerialPortStopBit.One, SerialPortParity.None, timeout);
                    log.save("Connected to Kiccc pos successfully:)");
                    Console.Beep(10000, 100);
                    return true;
                }
                else
                {
                    log.save("This port is not active :|");
                    return false;
                }

            }
            catch (Exception ex)
            {
                log.save(string.Format("Error on connect to pos : {0}\t Inner Exception : {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                System.Media.SystemSounds.Exclamation.Play();
            }

            return false;
        }


        private string state()
        {
            try
            {
                // reset old connection before create new one
                var myState = myKiccc.State.ToString();
                log.save("State = " + myState);
                return myState;
            }
            catch (Exception ex)
            {
                log.save(string.Format("Error on get state Exception : {0}\t Inner Exception : {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                System.Media.SystemSounds.Exclamation.Play();
            }

            return "Unknown";
        }


        private Boolean reset()
        {
            try
            {
                if(state() == "InitializeRequired")
                {
                    return false;
                }

                // reset old connection before create new one
                myKiccc.ResetService();
                
                Thread.Sleep(1000);
                return true;
            }
            catch (Exception ex)
            {
                log.save(string.Format("Error on reset connection : {0}\t Inner Exception : {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : string.Empty));
                System.Media.SystemSounds.Exclamation.Play();
            }

            return false;
        }


        private Boolean terminate()
        {
            try
            {
                if (state() == "InitializeRequired")
                {
                    return false;
                }

                // reset old connection before create new one
                myKiccc.TerminateService();
                Thread.Sleep(500);
                return true;
            }
            catch (Exception ex)
            {
                log.save(string.Format("Error on terminate Exception : {0}\t Inner Exception : {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : string.Empty));
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
                    log.save(res);
                    log.save(lib.str.fromDic(xml));

                    return true;
                }
                else
                {
                    log.save("\n\n\t Info1 \t" + info1);
                    log.save("\t Info2 \t" + info2);
                    log.save("\t Info3 \t" + info3);
                    log.save("\t Info4 \t" + info4);
                    var res = myKiccc.SaleWithExtraParamAndPrintableInfo(Amount, "1", info1, info2, info3, info4);
                    log.save(res);

                    return true;
                }
            }
            catch (Exception ex)
            {
                log.save(string.Format("Exception on send sale: {0}\r\nInner Exception : {1}", ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty));
            }
            return false;
        }


        private Boolean saleAsync()
        {
            BUSY = true;
            // try to sale
            log.save("BUSY " + BUSY);
            try
            {
                if (string.IsNullOrEmpty(info1))
                {
                    var res = myKiccc.BeginSale(Amount);
                    log.save("Async sale result " + res);
                    

                    return true;
                }
                else
                {
                    log.save("\n\n\t Info1 \t" + info1);
                    log.save("\t Info2 \t" + info2);
                    log.save("\t Info3 \t" + info3);
                    log.save("\t Info4 \t" + info4);
                    var res = myKiccc.BeginSaleWithExtraParamAndPrintableInfo(Amount, "1", info1, info2, info3, info4);
                    log.save("Async sale result with info " + res);

                    return true;
                }
            }
            catch (Exception ex)
            {
                log.save(string.Format("Exception on send async sale : {0}\r\nInner Exception : {1}", ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message : string.Empty));
            }
            return false;
        }

    }
}
