using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Recipe : Client_File
    {
        public string name; //name id //same as nameID in recipe-s
        public string id_mk; //server recipe id (starts at 1) //same as ID in recipe-s
        public string id_recipe; //item recipe id (itemdata id) // same as item_id in recipe-s
        public string level;
        public string id_item; 
        public string count;
        public string mp_cost;
        public string success_rate;
        public string materials_cnt;
        public string materials_extra;
        public string materials_m_0_id;
        public string materials_m_0_cnt;
        public string materials_m_1_id;
        public string materials_m_1_cnt;
        public string materials_m_2_id;
        public string materials_m_2_cnt;
        public string materials_m_3_id;
        public string materials_m_3_cnt;
        public string materials_m_4_id;
        public string materials_m_4_cnt;
        public string materials_m_5_id;
        public string materials_m_5_cnt;
        public string materials_m_6_id;
        public string materials_m_6_cnt;
        public string materials_m_7_id;
        public string materials_m_7_cnt;
        public string materials_m_8_id;
        public string materials_m_8_cnt;
        public string materials_m_9_id;
        public string materials_m_9_cnt;
        public string materials_m_10_id;
        public string materials_m_10_cnt;

        public Client_Recipe(string line)
        {
            string[] splitLine = line.Split('\t');

            name = splitLine[0];
            name = name.Replace("a,", "");
            name = name.Replace(@"\0", "");
            name = name.Replace("\0", "");

            id_mk = splitLine[1];
            id_recipe = splitLine[2];
            level = splitLine[3];
            id_item = splitLine[4];
            count = splitLine[5];
            mp_cost = splitLine[6];
            success_rate = splitLine[7];
            materials_cnt = splitLine[8];
            materials_extra = splitLine[9];


            materials_m_0_id = splitLine[10];
            materials_m_0_cnt = splitLine[11];
            materials_m_1_id = splitLine[12];
            materials_m_1_cnt = splitLine[13];
            materials_m_2_id = splitLine[14];
            materials_m_2_cnt = splitLine[15];
            materials_m_3_id = splitLine[16];
            materials_m_3_cnt = splitLine[17];
            materials_m_4_id = splitLine[18];
            materials_m_4_cnt = splitLine[19];
            materials_m_5_id = splitLine[20];
            materials_m_5_cnt = splitLine[21];
            materials_m_6_id = splitLine[22];
            materials_m_6_cnt = splitLine[23];
            materials_m_7_id = splitLine[24];
            materials_m_7_cnt = splitLine[25];
            materials_m_8_id = splitLine[26];
            materials_m_8_cnt = splitLine[27];
            materials_m_9_id = splitLine[28];
            materials_m_9_cnt = splitLine[29];
            materials_m_10_id = splitLine[30];
            materials_m_10_cnt = splitLine[31];
        }

        public string GetExportString()
        {
            string exportString = "";

            exportString += "a," + name + @"\0" + "\t" + id_mk + "\t" + id_recipe + "\t" + level + "\t" + id_item + "\t" + count + "\t" + mp_cost + "\t" + success_rate + "\t" + materials_cnt + "\t" + materials_extra + "\t" +
                            materials_m_0_id + "\t" + materials_m_0_cnt + "\t" +
                            materials_m_1_id + "\t" + materials_m_1_cnt + "\t" +
                            materials_m_2_id + "\t" + materials_m_2_cnt + "\t" +
                            materials_m_3_id + "\t" + materials_m_3_cnt + "\t" +
                            materials_m_4_id + "\t" + materials_m_4_cnt + "\t" +
                            materials_m_5_id + "\t" + materials_m_5_cnt + "\t" +
                            materials_m_6_id + "\t" + materials_m_6_cnt + "\t" +
                            materials_m_7_id + "\t" + materials_m_7_cnt + "\t" +
                            materials_m_8_id + "\t" + materials_m_8_cnt + "\t" +
                            materials_m_9_id + "\t" + materials_m_9_cnt + "\t" +
                            materials_m_10_id + "\t" + materials_m_10_cnt;

            return exportString;
        }
    }
}
