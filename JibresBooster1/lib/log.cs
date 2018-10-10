using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JibresBooster1.lib
{
    class log
    {
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
    }
}
