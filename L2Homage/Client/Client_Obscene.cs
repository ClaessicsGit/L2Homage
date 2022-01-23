using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Obscene
    {
        public string ID;
        public string text;

        public Client_Obscene(string ID, string text)
        {
            this.ID = ID;
            this.text = text;
        }

        public Client_Obscene(string datastring)
        {
            string[] splitDatastring = datastring.Split('\t');
            ID = splitDatastring[0];
            if (splitDatastring[1].Length > 1)
                splitDatastring[1] = splitDatastring[1].Remove(0, 2);
            if (splitDatastring[1].Length > 1)
                splitDatastring[1] = splitDatastring[1].Remove(splitDatastring[1].Length - 2, 2);
            text = splitDatastring[1];
        }

        public string GetExportString()
        {
            string replacedText = "a," + text;
            if (text.Length > 0)
                replacedText += @"\0";

            string returnString = ID + "\t" + replacedText;
            return returnString;
        }
    }
}
