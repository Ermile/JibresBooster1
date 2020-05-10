using System;
using System.Diagnostics;

namespace JibresBooster1.lib
{
    class manage
    {
        static void RestartAsAdmin()
        {
            var startInfo = new ProcessStartInfo("JibresBooster1.exe") { Verb = "runas" };
            Process.Start(startInfo);
            Environment.Exit(0);
        }
    }
}
