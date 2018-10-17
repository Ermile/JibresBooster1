using PosInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JibresBooster1.lib.PcPos
{
    //class Asnapardakht
    public partial class Asnapardakht : ITransactionDoneHandler
    {
        private static PCPos pcPos = new PCPos();
        private static string IP;
        private static string COMPORT;
        private static string AMOUNT;
        private static string INVOICE;

        public void fire(Dictionary<string, string> _args)
        {
            var myAsanpardakht = new Asnapardakht();

            // check input value and fill with default values
            fill(_args);


            initLan(IP);
            myAsanpardakht.saleAsync(AMOUNT, INVOICE);
        }


        public void OnFinish(string _message)
        {
            log.save("Asanpardatkh finished. " + _message);
        }


        public void OnTransactionDone(TransactionResult _result)
        {
            log.save("Asanpardatkh Done. " + _result.ToString());
        }


        public void saleAsync(string _sum, string _invoice)
        {
            pcPos.DoASyncPayment(_sum, string.Empty, _invoice, DateTime.Now, this);
        }


        private static void initLan(string _ip, int _port = 17000)
        {
            pcPos.InitLAN(_ip, _port);
            log.save("init asanpardatkh on lan with ip address " + _ip);
        }


        private static void initSerial(string _com)
        {
            pcPos.InitSerial(_com, 115200);
            log.save("init asanpardatkh on com " + _com);
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
