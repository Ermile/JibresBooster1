using System.Diagnostics;
using System.Reflection;
using JibresBooster1.translation;

namespace JibresBooster1.lib
{
    internal class notif
    {
        private static System.Windows.Forms.NotifyIcon myNotif;

        public static void init(System.Windows.Forms.NotifyIcon _notif)
        {
            myNotif = _notif;
        }


        public static void info(string _title, string _desc, bool _hidden = false)
        {
            if (_title == null)
            {
                _title = getDefault("title");
            }
            if (_desc == null)
            {
                _desc = getDefault("desc");
            }
            if (_hidden)
            {

            }
            else
            {
                myNotif.ShowBalloonTip(1000, _title, _desc, System.Windows.Forms.ToolTipIcon.Info);
            }
        }

        public static void say(string _title, string _desc = "", bool _hidden = false)
        {
            if (_title == null)
            {
                _title = getDefault("title");
            }
            if (_desc == null)
            {
                _desc = getDefault("desc");
            }
            if (_hidden)
            {

            }
            else
            {
                myNotif.ShowBalloonTip(1000, _title, _desc, System.Windows.Forms.ToolTipIcon.None);
            }

        }

        public static void warn(string _title, string _desc, bool _hidden = false)
        {
            if (_title == null)
            {
                _title = getDefault("title");
            }
            if (_desc == null)
            {
                _desc = getDefault("desc");
            }
            if (_hidden)
            {

            }
            else
            {
                myNotif.ShowBalloonTip(1000, _title, _desc, System.Windows.Forms.ToolTipIcon.Warning);
            }
        }

        public static void error(string _title, string _desc, bool _hidden = false)
        {
            if (_title == null)
            {
                _title = getDefault("title");
            }
            if (_desc == null)
            {
                _desc = getDefault("desc");
            }
            if (_hidden)
            {

            }
            else
            {
                myNotif.ShowBalloonTip(1000, _title, _desc, System.Windows.Forms.ToolTipIcon.Error);
            }
        }


        private static string getDefault(string _type)
        {
            string result = "";
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fieVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            if (_type == "desc")
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

        public static void appExit()
        {
            say(T.get("Bye"), T.get("MsgAppExit"));
        }

        public static void appErrorServer()
        {
            error(T.get("ErrorInit"), T.get("MsgAppErrorServer"));
        }

    }
}
