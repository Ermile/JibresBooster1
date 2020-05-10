using System;
using System.IO;

namespace JibresBooster1.lib
{
    class file
    {
        public static void save(string _loc, string _data)
        {
            try
            {
                using (StreamWriter myFile = File.AppendText(_loc))
                {
                    myFile.WriteLine(_data);
                }
            }
            catch (Exception ex)
            {
                // error on create folder
                Console.WriteLine(ex.Message);
            }
        }
    }
}
