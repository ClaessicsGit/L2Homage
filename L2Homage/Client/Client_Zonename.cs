using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Zonename
    {
        public string nbr;
        public string zone_color_id;
        public string x_world_grid;
        public string y_world_grid;
        public string top_z;
        public string bottom_z;
        public string zone_name;
        public string coord_0;
        public string coord_1;
        public string coord_2;
        public string coord_3;
        public string coord_4;
        public string coord_5;
        public string unk02;
        public string map;
        public string dupa;

        public Client_Zonename(string datastring)
        {
            string[] splitDatastring = datastring.Split('\t');

            nbr = splitDatastring[0];
            zone_color_id = splitDatastring[1];
            x_world_grid = splitDatastring[2];
            y_world_grid = splitDatastring[3];
            top_z = splitDatastring[4];
            bottom_z = splitDatastring[5];
            if (splitDatastring[6].Length > 1)
                splitDatastring[6] = splitDatastring[6].Remove(0, 2);
            if (splitDatastring[6].Length > 1)
                splitDatastring[6] = splitDatastring[6].Remove(splitDatastring[6].Length - 2, 2);
            zone_name = splitDatastring[6];
            coord_0 = splitDatastring[7];
            coord_1 = splitDatastring[8];
            coord_2 = splitDatastring[9];
            coord_3 = splitDatastring[10];
            coord_4 = splitDatastring[11];
            coord_5 = splitDatastring[12];
            unk02 = splitDatastring[13];
            if (splitDatastring[14].Length > 1)
                splitDatastring[14] = splitDatastring[14].Remove(0, 2);
            if (splitDatastring[14].Length > 1)
                splitDatastring[14] = splitDatastring[14].Remove(splitDatastring[14].Length - 2, 2);
            map = splitDatastring[14];
            dupa = splitDatastring[15];

        }

        public string GetExportString()
        {
            string replacedZone_name = "a," + zone_name;
            if (zone_name.Length > 0)
                replacedZone_name += @"\0";

            string replacedMap = "a," + map;
            if (map.Length > 0)
                replacedMap += @"\0";


            string returnString = nbr + "\t" +
                                zone_color_id + "\t" +
                                x_world_grid + "\t" +
                                y_world_grid + "\t" +
                                top_z + "\t" +
                                bottom_z + "\t" +
                                replacedZone_name + "\t" +
                                coord_0 + "\t" +
                                coord_1 + "\t" +
                                coord_2 + "\t" +
                                coord_3 + "\t" +
                                coord_4 + "\t" +
                                coord_5 + "\t" +
                                unk02 + "\t" +
                                replacedMap + "\t" +
                                dupa;


            return returnString;
        }
    }
}
