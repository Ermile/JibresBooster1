using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JibresBooster1.lib
{
    class reader
    {
        public static Dictionary<string, string> xml(string _str)
        {
            if(string.IsNullOrEmpty(_str))
            {
                return null;
            }
            // create new instance
            XmlDocument myDoc = new XmlDocument();

            Dictionary<string, string> xmlParser = new Dictionary<string, string>();
            // try to load it
            myDoc.LoadXml(_str);
                        

            foreach (XmlNode node in myDoc.DocumentElement.ChildNodes)
            {
                xmlParser.Add(node.Name, node.InnerText);
            }

            return xmlParser;
        }


        public static string xmlReadable(string _str)
        {
            var xmlDic = xml(_str);
            var prettyResult = lib.str.fromDic(xmlDic, "\n\t");
            return prettyResult;
        }

    }
}
