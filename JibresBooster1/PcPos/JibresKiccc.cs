﻿using System;
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
        private string SerialNo = "5000054981";
        private string TerminalId = "06151815";
        private string AcceptorId = "062006362145616";
        private string cmbCom = "com1";


        public void init()
        {
            myKiccc = new SerialIngenico();
            
            try
            {
                // Initiate Service
                myKiccc.InitiateService(SerialNo, AcceptorId, TerminalId, cmbCom, 115200, 8, SerialPortStopBit.One, SerialPortParity.None);
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
        }


        public void connect()
        {

        }
    }
}
