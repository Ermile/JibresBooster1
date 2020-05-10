using System.Collections.Generic;
using System.Xml;

namespace JibresBooster1.lib
{
    internal class reader
    {
        public static Dictionary<string, string> xml(string _str)
        {
            if (string.IsNullOrEmpty(_str))
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
            Dictionary<string, string> xmlDic = xml(_str);
            string prettyResult = lib.str.fromDic(xmlDic, "\n\t");
            return prettyResult;
        }

    }
}
