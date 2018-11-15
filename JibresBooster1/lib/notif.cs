using JibresBooster1.translation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
            if (_title == null)
            {
                _title = getDefault("title");
            }
            if (_desc == null)
            {
                _desc = getDefault("desc");
            }
            myNotif.ShowBalloonTip(1000, _title, _desc, System.Windows.Forms.ToolTipIcon.Info);
        }

        public static void say(string _title, string _desc = "")
        {
            if (_title == null)
            {
                _title = getDefault("title");
            }
            if (_desc == null)
            {
                _desc = getDefault("desc");
            }
            myNotif.ShowBalloonTip(1000, _title, _desc, System.Windows.Forms.ToolTipIcon.None);
        }

        public static void warn(string _title, string _desc)
        {
            if (_title == null)
            {
                _title = getDefault("title");
            }
            if (_desc == null)
            {
                _desc = getDefault("desc");
            }
            myNotif.ShowBalloonTip(1000, _title, _desc, System.Windows.Forms.ToolTipIcon.Warning);
        }

        public static void error(string _title, string _desc)
        {
            if (_title == null)
            {
                _title = getDefault("title");
            }
            if (_desc == null)
            {
                _desc = getDefault("desc");
            }
            myNotif.ShowBalloonTip(1000, _title, _desc, System.Windows.Forms.ToolTipIcon.Error);
        }


        private static string getDefault(string _type)
        {
            string result = "";
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            var fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            if(_type == "desc")
            {
                result = fieVersionInfo.CompanyName;
            }
            else if (_type == "title")
            {
                result = fieVersionInfo.ProductName;
            }

            return result;
        }



        public static void appStart()
        {
            say(T.get("Hi"), T.get("MsgAppStart"));
        }

        public static void appErrorServer()
        {
            error(T.get("ErrorInit"), T.get("MsgAppErrorServer"));
        }

    }
}
