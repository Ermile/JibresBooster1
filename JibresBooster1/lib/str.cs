using System;
using System.Collections.Generic;
using System.Linq;


namespace JibresBooster1.lib
{
    public static class str
    {
        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            value = value.Trim();
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength ? value : value.Substring(0, maxLength));
        }


        public static string fromDic(Dictionary<string, string> _dic, string _seperator = "\n")
        {
            string result = "\n" + string.Join(_seperator, _dic.Select(x => x.Key + "\t=\t" + x.Value).ToArray()) + "\n";
            //String result = _dic.Select(x => x.Key + "=" + x.Value).Aggregate((s1, s2) => s1 + ";" + s2);

            return result;
        }
    }
}
