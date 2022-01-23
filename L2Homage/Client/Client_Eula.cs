using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Eula
    {
        public string eula;
        public string fin1;
        public string fin2;
        public string fin3;

        public Client_Eula(string datastring)
        {
            string[] splitDatastring = datastring.Split('\t');
            eula = splitDatastring[0].Replace(@"\r\n", Environment.NewLine);
            fin1 = splitDatastring[1];
            fin2 = splitDatastring[2];
            fin3 = splitDatastring[3];
        }

        public string GetExportString()
        {
            string returnString = eula.Replace(Environment.NewLine, @"\r\n") + "\t" + fin1 + "\t" + fin2 + "\t" + fin3;
            return returnString;
        }
    }
}
