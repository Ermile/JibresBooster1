﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;


namespace JibresBooster1.translation
{
    class T
    {
        public static string get(string _str)
        {

            //System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("fa");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("fa");

            string currnetLang = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToString();
            currnetLang = "fa";
            ResourceManager myTranslation;
            myTranslation = new ResourceManager("JibresBooster1.translation." + currnetLang, Assembly.GetExecutingAssembly());
            string translatedTxt = myTranslation.GetString(_str);

            return translatedTxt;
        }

    }
}
