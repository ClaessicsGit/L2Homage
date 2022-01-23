using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Charcreate : Client_File
    {
        public string flts_0;
        public string flts_1;
        public string flts_2;
        public string flts_3;
        public string ints_0;
        public string ints_1;
        public string ints_2;
        public string ints_3;
        public string ints_4;
        public string ints_5;

        public Client_Charcreate(string datastring)
        {
            string[] splitDatastring = datastring.Split('\t');
            flts_0 = splitDatastring[0];
            flts_1 = splitDatastring[1];
            flts_2 = splitDatastring[2];
            flts_3 = splitDatastring[3];
            ints_0 = splitDatastring[4];
            ints_1 = splitDatastring[5];
            ints_2 = splitDatastring[6];
            ints_3 = splitDatastring[7];
            ints_4 = splitDatastring[8];
            ints_5 = splitDatastring[9];
        }

        public string GetExportString()
        {
            string exportString = "";

            exportString += flts_0 + "\t" + flts_1 + "\t" + flts_2 + "\t" + flts_3 + "\t" + ints_0 + "\t" + ints_1 + "\t" + ints_2 + "\t" + ints_3 + "\t" + ints_4 + "\t" + ints_5;

            return exportString;
        }
    }
}
