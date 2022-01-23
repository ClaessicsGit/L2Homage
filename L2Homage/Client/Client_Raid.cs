using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Raid
    {
        public string ID;
        public string npc_id;
        public string npc_level;
        public string affiliated_area_id;
        public string loc_x;
        public string loc_y;
        public string loc_z;
        public string raid_desc;

        public Client_Raid(string datastring)
        {
            string[] splitDatastring = datastring.Split('\t');

            ID = splitDatastring[0];
            npc_id = splitDatastring[1];
            npc_level = splitDatastring[2];
            affiliated_area_id = splitDatastring[3];
            loc_x = splitDatastring[4];
            loc_y = splitDatastring[5];
            loc_z = splitDatastring[6];
            if (splitDatastring[7].Length > 1)
                splitDatastring[7] = splitDatastring[7].Remove(0, 2);
            if (splitDatastring[7].Length > 1)
                splitDatastring[7] = splitDatastring[7].Remove(splitDatastring[7].Length - 2, 2);
            raid_desc = splitDatastring[7];
        }

        public string GetExportString()
        {
            string replacedRaid_desc = "a," + raid_desc;
            if (raid_desc.Length > 0)
                replacedRaid_desc += @"\0";

            string returnString = ID + "\t" + npc_id + "\t" + npc_level + "\t" + affiliated_area_id + "\t" + loc_x + "\t" + loc_y + "\t" + loc_z + "\t" + replacedRaid_desc;


            return returnString;

        }
    }
}
