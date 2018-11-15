using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JibresBooster1.lib
{
    class notif
    {
        private static System.Windows.Forms.NotifyIcon myNotif;

        public static void init(System.Windows.Forms.NotifyIcon _notif)
        {
            myNotif = _notif;
        }


        public static void info(string _title, string _desc)
        {
            myNotif.ShowBalloonTip(1000, _title, _desc, System.Windows.Forms.ToolTipIcon.Info);
        }

        public static void say(string _title, string _desc)
        {
            myNotif.ShowBalloonTip(1000, _title, _desc, System.Windows.Forms.ToolTipIcon.None);
        }

        public static void warn(string _title, string _desc)
        {
            myNotif.ShowBalloonTip(1000, _title, _desc, System.Windows.Forms.ToolTipIcon.Warning);
        }

        public static void error(string _title, string _desc)
        {
            myNotif.ShowBalloonTip(1000, _title, _desc, System.Windows.Forms.ToolTipIcon.Error);
        }
    }
}
