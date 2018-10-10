using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
