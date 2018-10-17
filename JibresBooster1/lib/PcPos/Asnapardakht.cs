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
        private PCPos pcPos = new PCPos();

        public static void fire()
        {
            

        }

        public void OnFinish(string _message)
        {
            log.save("Asanpardatkh finished. " + _message);
            throw new NotImplementedException();
        }

        public void OnTransactionDone(TransactionResult _result)
        {
            log.save("Asanpardatkh Done. " + _result.ToString());
            throw new NotImplementedException();
        }

        public void saleAsync(string _sum, string _invoice)
        {
            pcPos.DoASyncPayment(_sum, string.Empty, _invoice, DateTime.Now, this);
        }


        private void initLan(string _ip, int _port = 17000)
        {
            pcPos.InitLAN(_ip, _port);
            log.save("init asanpardatkh on lan with ip address " + _ip);
        }


        private void initSerial(string _com)
        {
            pcPos.InitSerial(_com, 115200);
            log.save("init asanpardatkh on com " + _com);
        }

    }
}
