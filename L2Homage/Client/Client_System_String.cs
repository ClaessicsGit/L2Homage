using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_System_String
    {
        public string ID;
        public string text;

        public bool u_string;

        public Client_System_String(string ID, string text)
        {
            this.ID = ID;
            this.text = text;
            u_string = false;
        }

        public Client_System_String(string datastring)
        {
            string[] splitDatastring = datastring.Split('\t');

            ID = splitDatastring[0];

            if (splitDatastring[1].Length > 0)
                if (splitDatastring[1][0] == 'u')
                    u_string = true;

            if (splitDatastring[1].Length > 1)
                splitDatastring[1] = splitDatastring[1].Remove(0, 2);
            if (splitDatastring[1].Length > 1)
                splitDatastring[1] = splitDatastring[1].Remove(splitDatastring[1].Length - 2, 2);
            text = splitDatastring[1];



        }

        public string GetExportString()
        {
            string replacedName = "";
            if (u_string)
            {
                replacedName = "u," + text;
            }
            else
                replacedName = "a," + text;
            if (text.Length > 0)
                replacedName += @"\0";

            string returnString = ID + "\t" + replacedName;
            return returnString;
        }
    }
}
