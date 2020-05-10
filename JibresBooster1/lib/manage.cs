using System;
using System.Diagnostics;

namespace JibresBooster1.lib
{
    internal class manage
    {
        private static void RestartAsAdmin()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("JibresBooster1.exe") { Verb = "runas" };
            Process.Start(startInfo);
            Environment.Exit(0);
        }
    }
}
