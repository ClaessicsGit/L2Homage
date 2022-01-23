using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Skill
    {
        public string id;
        public string level;
        public string oper_type;
        public string mp_consume;
        public string cast_range;
        public string cast_style;
        public string UNK_0;
        public string hit_time;
        public string is_magic;
        public string ani_char;
        public string desc;
        public string icon_name;
        public string icon_name2;
        public string is_ench;
        public string ench_skill_id;
        public string hp_consume;
        public string foo;
        public string UNK_1;
        public string UNK_2;
        public string UNK_3;
        public string UNK_4;
        public string nonetext2;

        public Client_Skill(string line)
        {
            string[] skillgrp_eLine = line.Split('\t');

            id = skillgrp_eLine[0];
            level = skillgrp_eLine[1];
            oper_type = skillgrp_eLine[2];
            mp_consume = skillgrp_eLine[3];
            cast_range = skillgrp_eLine[4];
            cast_style = skillgrp_eLine[5];
            UNK_0 = skillgrp_eLine[6];
            hit_time = skillgrp_eLine[7];
            is_magic = skillgrp_eLine[8];
            ani_char = skillgrp_eLine[9];
            desc = skillgrp_eLine[10];
            icon_name = skillgrp_eLine[11];
            icon_name2 = skillgrp_eLine[12];
            is_ench = skillgrp_eLine[13];
            ench_skill_id = skillgrp_eLine[14];
            hp_consume = skillgrp_eLine[15];
            foo = skillgrp_eLine[16];
            UNK_1 = skillgrp_eLine[17];
            UNK_2 = skillgrp_eLine[18];
            UNK_3 = skillgrp_eLine[19];
            UNK_4 = skillgrp_eLine[20];
            nonetext2 = skillgrp_eLine[21];
        }

        public string GetExportString()
        {
            string exportString = "";

            exportString += id + "\t" + level + "\t" + oper_type + "\t" + mp_consume + "\t" + cast_range + "\t" + cast_style + "\t" + UNK_0
                 + "\t" + hit_time + "\t" + is_magic + "\t" + ani_char + "\t" + desc + "\t" + icon_name + "\t" + icon_name2 + "\t" + is_ench
                 + "\t" + ench_skill_id + "\t" + hp_consume + "\t" + foo + "\t" + UNK_1 + "\t" + UNK_2 + "\t" + UNK_3 + "\t" + UNK_4 + "\t" + nonetext2;

            return exportString;
        }
    }
}
