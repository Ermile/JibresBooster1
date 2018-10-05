using System;


namespace JibresBooster1.PcPos
{
    public static class str
    {
        public static string Left(this string value, int maxLength)
        {
            value = value.Trim();
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength ? value : value.Substring(0, maxLength));
        }
    }
}
