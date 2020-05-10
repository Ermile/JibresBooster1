using System;
using System.Collections.Generic;
using PosInterface;

namespace JibresBooster1.lib.PcPos
{
    //class Asnapardakht
    public partial class Asnapardakht : ITransactionDoneHandler
    {
        private static readonly PCPos pcPos = new PCPos();
        private static string IP;
        private static string COMPORT;
        private static string AMOUNT;
        private static string INVOICE;

        public void fire(Dictionary<string, string> _args)
        {
            Asnapardakht myAsanpardakht = new Asnapardakht();

            // check input value and fill with default values
            fill(_args);


            if (initLan(IP))
            {
                if (myAsanpardakht.saleAsync(AMOUNT, INVOICE))
                {

                }
            }
        }


        public void OnFinish(string _message)
        {
            log.save("Asanpardatkh finished. " + _message);
        }


        public void OnTransactionDone(TransactionResult _result)
        {
            log.save("Asanpardatkh Done. " + _result.ToString());
        }


        public bool saleAsync(string _sum, string _invoice)
        {
            if (string.IsNullOrEmpty(_sum))
            {
                return false;
            }
            if (string.IsNullOrEmpty(_invoice))
            {
                return false;
            }

            pcPos.DoASyncPayment(_sum, string.Empty, _invoice, DateTime.Now, this);
            log.save("Request sended to pcpos. Amount " + _sum + " - Invoice " + _invoice);
            return true;
        }


        private static bool initLan(string _ip, int _port = 17000)
        {
            if (string.IsNullOrEmpty(_ip))
            {
                return false;
            }
            pcPos.InitLAN(_ip, _port);
            log.save("init asanpardatkh on lan with ip address " + _ip);

            return true;
        }


        private static bool initSerial(string _com)
        {
            if (string.IsNullOrEmpty(_com))
            {
                return false;
            }
            pcPos.InitSerial(_com, 115200);
            log.save("init asanpardatkh on com " + _com);

            return true;
        }


        private static void fill(Dictionary<string, string> _args)
        {
            // AMOUNT
            if (_args.ContainsKey("sum"))
            {
                AMOUNT = _args["sum"];
            }
            else if (_args.ContainsKey("test"))
            {
                AMOUNT = "1200";
                log.save("\t test amount \t" + AMOUNT);
            }
            else
            {
                log.save("sum is empty !");
                Console.Beep(1000, 200);
            }


            // INVOICE
            if (_args.ContainsKey("invoice"))
            {
                INVOICE = _args["invoice"];
            }
            else if (_args.ContainsKey("test"))
            {
                INVOICE = "11";
                log.save("\t test invoice \t" + INVOICE);
            }
            else
            {
                log.save("invoice is empty !");
                Console.Beep(1000, 200);
            }


            // IP
            if (_args.ContainsKey("ip"))
            {
                IP = _args["ip"];
            }
            else if (_args.ContainsKey("test"))
            {
                IP = "3.3.3.34";
                log.save("\t finded ip \t" + IP);
            }
            else
            {
                log.save("ip is empty !");
                Console.Beep(100, 100);

                // cmbCom
                if (_args.ContainsKey("port"))
                {
                    COMPORT = _args["port"];
                }
                else
                {
                    //COMPORT = lib.port.asanpardakht();
                }
            }
        }

    }
}
