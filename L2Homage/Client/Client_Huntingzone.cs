using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Huntingzone : Client_File
    {
        public string ID;
        public string hunting_type;
        public string level;
        public string unk_1;
        public string loc_x;
        public string loc_y;
        public string loc_z;
        public string extra;
        public string affiliated_area_id;
        public string name;

        bool u_extra;

        public Client_Huntingzone(string dataString)
        {
            string[] splitDatastring = dataString.Split('\t');
            ID = splitDatastring[0];
            hunting_type = splitDatastring[1];
            level = splitDatastring[2];
            unk_1 = splitDatastring[3];
            loc_x = splitDatastring[4];
            loc_y = splitDatastring[5];
            loc_z = splitDatastring[6];
            if (splitDatastring[7].Length > 0)
                if (splitDatastring[7][0] == 'u')
                    u_extra = true;
            if (splitDatastring[7].Length > 1)
                splitDatastring[7] = splitDatastring[7].Remove(0, 2);
            if (splitDatastring[7].Length > 1)
                splitDatastring[7] = splitDatastring[7].Remove(splitDatastring[7].Length - 2, 2);
            extra = splitDatastring[7];

            affiliated_area_id = splitDatastring[8];
            if (splitDatastring[9].Length > 1)
                splitDatastring[9] = splitDatastring[9].Remove(0, 2);
            if (splitDatastring[9].Length > 1)
                splitDatastring[9] = splitDatastring[9].Remove(splitDatastring[9].Length - 2, 2);
            name = splitDatastring[9];
        }

        public string GetExportString()
        {
            string replacedExtra = "";
            if (u_extra)
            {
                replacedExtra = "u," + extra;
            }
            else
                replacedExtra = "a," + extra;
            if (extra.Length > 0)
                replacedExtra += @"\0";

            string replacedName = "a," + name;
            if (name.Length > 0)
                replacedName += @"\0";


            string returnString = ID + "\t" +
                                  hunting_type + "\t" +
                                  level + "\t" +
                                  unk_1 + "\t" +
                                  loc_x + "\t" +
                                  loc_y + "\t" +
                                  loc_z + "\t" +
                                  replacedExtra + "\t" +
                                  affiliated_area_id + "\t" +
                                  replacedName;
            return returnString;
        }
    }

}
