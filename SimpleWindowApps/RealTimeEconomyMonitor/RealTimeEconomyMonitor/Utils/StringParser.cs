using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeEconomyMonitor.Utils
{
    class StringParser
    {
        public string HTMLParser(string str)
        {
            char[] seperator = { '\n', '\t', ' ', '\r' };
            str = str.Replace("<br>", "  ");
            str = str.Replace("</br>", "  ");
            str = str.Replace("<b>", "  ");
            str = str.Replace("</b>", "  ");
            str = str.Replace("&quot;", "  ");
           // str = str.Replace("N/A", "  ");
            string[] splits = str.Split(seperator);

            string ret = "";
            foreach (string el in splits)
                ret += el + ' ';
            
            ret = ret.Replace("+", "  +");
            ret =ret.Replace("-", "  -");

            return ret;
        }
    }
}
