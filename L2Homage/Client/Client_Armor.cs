using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Armor : Client_File
    {
        public string tag;
        public string id;
        string drop_type;
        string drop_anim_type;
        string drop_radius;
        string drop_height;
        string UNK_0;
        public string drop_mesh1;
        public string drop_mesh2;
        public string drop_mesh3;
        public string drop_tex1;
        public string drop_tex2;
        public string drop_tex3;
        public string drop_extratex1;
        string[] newdata; //8 totalt, 0-7
        public string[] icon; //5 total, 0-4
        public string durability;
        public string weight;
        string material;
        public string crystallizable;
        string UNK_3;
        string UNK_4_cnt;
        string UNK_4_tab;
        string UNK_5;
        string timetab;
        public string body_part;
        string[] weirdValues;

        string NPC_cntm;
        string[] NPC_m;// 1 total, 0
        string NPC_cntt;
        string[] NPC_t;//2 total, 0-1
        string NPC_add_cntm;
        string[] NPC_add_mU;//1 total, 0

        string NPC_add_mB1;//[0][1] and [0][2]
        string NPC_add_mB2;//These are the only two anomalies

        string NPC_add_cntt;
        string[] NPC_add_t;//1 total, 0
        string NPC_add_tE;
        public string att_eff;
        string item_sound_cnt;
        public string[] item_sound;//4 total, 0-3
        public string drop_sound;
        public string equip_sound;
        string UNK_6;
        string UNK_7;
        public string armor_type;
        public string crystal_type;
        public string avoid_mod;
        public string pdef;
        public string mdef;
        public string mpbonus;
        string UNK_8;



        public Client_Armor(string line)
        {
            string[] armorLine = line.Split('\t');

            tag = armorLine[0];
            id = armorLine[1];
            drop_type = armorLine[2];
            drop_anim_type = armorLine[3];
            drop_radius = armorLine[4];
            drop_height = armorLine[5];
            UNK_0 = armorLine[6];
            drop_mesh1 = armorLine[7];
            drop_mesh2 = armorLine[8];
            drop_mesh3 = armorLine[9];
            drop_tex1 = armorLine[10];
            drop_tex2 = armorLine[11];
            drop_tex3 = armorLine[12];
            drop_extratex1 = armorLine[13];

            newdata = new string[8];
            newdata[0] = armorLine[14];
            newdata[1] = armorLine[15];
            newdata[2] = armorLine[16];
            newdata[3] = armorLine[17];
            newdata[4] = armorLine[18];
            newdata[5] = armorLine[19];
            newdata[6] = armorLine[20];
            newdata[7] = armorLine[21];

            icon = new string[5];
            icon[0] = armorLine[22];
            icon[1] = armorLine[23];
            icon[2] = armorLine[24];
            icon[3] = armorLine[25];
            icon[4] = armorLine[26];

            durability = armorLine[27];
            weight = armorLine[28];
            material = armorLine[29];
            crystallizable = armorLine[30];

            UNK_3 = armorLine[31];
            UNK_4_cnt = armorLine[32];
            UNK_4_tab = armorLine[33];
            UNK_5 = armorLine[34];
            timetab = armorLine[35];
            body_part = armorLine[36];

            weirdValues = new string[593];

            for (int i = 0; i < weirdValues.Length; i++)
            {
                weirdValues[i] = armorLine[37 + i];
            }

            NPC_cntm = armorLine[631];

            NPC_m = new string[1];
            NPC_m[0] = armorLine[632];

            NPC_cntt = armorLine[633];

            NPC_t = new string[2];
            NPC_t[0] = armorLine[634];
            NPC_t[1] = armorLine[635];

            NPC_add_cntm = armorLine[636];
            NPC_add_mU = new string[1];
            NPC_add_mU[0] = armorLine[637];

            NPC_add_mB1 = armorLine[638]; //strange ones
            NPC_add_mB2 = armorLine[639]; //strange ones, anomaly

            NPC_add_cntt = armorLine[640];
            NPC_add_t = new string[1];
            NPC_add_t[0] = armorLine[641];

            NPC_add_tE = armorLine[642];
            att_eff = armorLine[643];
            item_sound_cnt = armorLine[644];

            item_sound = new string[4];
            item_sound[0] = armorLine[645];
            item_sound[1] = armorLine[646];
            item_sound[2] = armorLine[647];
            item_sound[3] = armorLine[648];

            drop_sound = armorLine[649];
            equip_sound = armorLine[650];
            UNK_6 = armorLine[651];
            UNK_7 = armorLine[652];
            armor_type = armorLine[653];
            crystal_type = armorLine[654];
            avoid_mod = armorLine[655];
            pdef = armorLine[656];
            mdef = armorLine[657];
            mpbonus = armorLine[658];
            UNK_8 = armorLine[659];

        }

        public string GetExportString()
        {
            return
                tag + '\t' +
                id + '\t' +
                drop_type + '\t' +
                drop_anim_type + '\t' +
                drop_radius + '\t' +
                drop_height + '\t' +
                UNK_0 + '\t' +
                drop_mesh1 + '\t' +
                drop_mesh2 + '\t' +
                drop_mesh3 + '\t' +
                drop_tex1 + '\t' +
                drop_tex2 + '\t' +
                drop_tex3 + '\t' +
                drop_extratex1 + '\t' +
                GetArrayString(newdata) + '\t' +
                GetArrayString(icon) + '\t' +
                durability + '\t' +
                weight + '\t' +
                material + '\t' +
                crystallizable + '\t' +
                UNK_3 + '\t' +
                UNK_4_cnt + '\t' +
                UNK_4_tab + '\t' +
                UNK_5 + '\t' +
                timetab + '\t' +
                body_part + '\t' +
                GetArrayString(weirdValues) + '\t' + '\t' +
                NPC_cntm + '\t' +
                GetArrayString(NPC_m, "fucker") + '\t' + // This fucking array string
                NPC_cntt + '\t' +
                GetArrayString(NPC_t) + '\t' +
                NPC_add_cntm + '\t' +
                GetArrayString(NPC_add_mU) + '\t' +
                NPC_add_mB1 + '\t' +
                NPC_add_mB2 + '\t' +
                NPC_add_cntt + '\t' +
                GetArrayString(NPC_add_t) + '\t' +
                NPC_add_tE + '\t' +
                att_eff + '\t' +
                item_sound_cnt + '\t' +
                GetArrayString(item_sound) + '\t' +
                drop_sound + '\t' +
                equip_sound + '\t' +
                UNK_6 + '\t' +
                UNK_7 + '\t' +
                armor_type + '\t' +
                crystal_type + '\t' +
                avoid_mod + '\t' +
                pdef + '\t' +
                mdef + '\t' +
                mpbonus + '\t' +
                UNK_8;
        }

    }
}
