using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Gametip
    {
        public string ID;
        public string int1;
        public string int2;
        public string enable_;
        public string tip;

        public bool u_string;

        public Client_Gametip(string ID, string text)
        {
            this.ID = ID;
            int1 = "1";
            int2 = "0";
            enable_ = "1";
            this.tip = text;
            u_string = false;
        }
        public Client_Gametip(string datastring)
        {
            string[] splitDatastring = datastring.Split('\t');

            ID = splitDatastring[0];
            int1 = splitDatastring[1];
            int2 = splitDatastring[2];
            enable_ = splitDatastring[3];

            if ((splitDatastring[4][0] + "" + splitDatastring[4][1]) == "u,")
            {
                u_string = true;
            }

            if (splitDatastring[4].Length > 1)
                splitDatastring[4] = splitDatastring[4].Remove(0, 2);
            if (splitDatastring[4].Length > 1)
                splitDatastring[4] = splitDatastring[4].Remove(splitDatastring[4].Length - 2, 2);
            tip = splitDatastring[4];
        }

        public string GetExportString()
        {
            string replacedTip = "";

            if (u_string)
                replacedTip = "u," + tip;
            else
                replacedTip = "a," + tip;

            if (tip.Length > 0)
                replacedTip += @"\0";

            string returnString = ID + "\t" + int1 + "\t" + int2 + "\t" + enable_ + "\t" + replacedTip;

            return returnString;
        }

    }
}
