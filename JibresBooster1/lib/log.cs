using System;
using System.IO;

namespace JibresBooster1.lib
{
    internal class log
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
            _data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t\t" + _data;
            file.save(logPath, _data);
        }


        public static void logPathDetector()
        {
            // try to create log addr
            if (string.IsNullOrEmpty(logPath))
            {
                string appLoc = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString();
                string jibresLoc = Path.Combine(appLoc, "Jibres");
                string jibresLogLoc = Path.Combine(jibresLoc, "log");

                //try to create folder location
                try
                {
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

                string fileName = "log" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".log";
                logPath = Path.Combine(jibresLogLoc, fileName);
            }
        }
    }
}
