using System;
using System.Diagnostics;
using System.Security.Principal;

namespace JibresBooster1.lib
{
    internal class manage
    {
        public static void RestartAsAdmin()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("JibresBooster1.exe") { Verb = "runas" };
            Process.Start(startInfo);
            Environment.Exit(0);
        }

        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
