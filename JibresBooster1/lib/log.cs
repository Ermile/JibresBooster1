using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JibresBooster1.lib
{
    class log
    {
        public static string logPath;
        public static void info(string _data)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(_data);
            Console.ResetColor();
        }


        public static void warn(string _data)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(_data);
            Console.ResetColor();
        }


        public static void danger(string _data)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(_data);
            Console.ResetColor();
        }


        public static void init(string _data)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(_data);
            Console.ResetColor();
        }


        public static void save(string _data)
        {
            logPathDetector();
            file.save(logPath, _data);
        }


        public static void logPathDetector()
        {
            // try to create log addr
            if (string.IsNullOrEmpty(logPath))
            {
                var appLoc = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
                var ermileLoc = Path.Combine(appLoc, "Ermile");
                var jibresLoc = Path.Combine(ermileLoc, "Jibres");
                var jibresLogLoc = Path.Combine(jibresLoc, "log");

                //try to create folder location
                try
                {
                    // create Ermile folder
                    if (!Directory.Exists(ermileLoc))
                    {
                        Directory.CreateDirectory(ermileLoc);
                    }
                    // create Jibres folder
                    if (!Directory.Exists(jibresLoc))
                    {
                        Directory.CreateDirectory(jibresLoc);
                    }
                    // create Jibres folder
                    if (!Directory.Exists(jibresLogLoc))
                    {
                        Directory.CreateDirectory(jibresLogLoc);
                    }
                }
                catch (Exception ex)
                {
                    // error on create folder
                    Console.WriteLine(ex.Message);
                }


                var fileName = "log" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".txt";
                logPath = Path.Combine(jibresLogLoc, fileName);
            }
        }
    }
}
