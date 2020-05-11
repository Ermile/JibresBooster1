using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace JibresBooster1.lib
{
    class StartUpManager
    {
        public static string CurrentUserStartup(string appName, string request)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (request == "set")
                {
                    key.DeleteValue(appName, false);
                    key.SetValue(appName, Application.ExecutablePath.ToString());
                }
                else if (request == "delete")
                {
                    key.DeleteValue(appName, false);
                }
                else
                {
                    var myStatus = key.GetValue(appName);
                    if (myStatus == null)
                    {
                        return "";
                    }
                    return myStatus.ToString();
                }

                return "true";
            }
        }


        public static string AllUserStartup(string appName, string request)
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (request == "set")
                {
                    key.DeleteValue(appName, false);
                    key.SetValue(appName, Application.ExecutablePath.ToString());
                }
                else if (request == "delete")
                {
                    key.DeleteValue(appName, false);
                }
                else
                {
                    var myStatus = key.GetValue(appName);
                    if(myStatus == null)
                    {
                        return "";
                    }
                    return myStatus.ToString();
                }

                return "";
            }
        }


        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                isAdmin = false;
            }
            catch (Exception)
            {
                isAdmin = false;
            }
            return isAdmin;
        }
    }
}
