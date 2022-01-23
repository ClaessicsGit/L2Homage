using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Weapon : Client_File
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
        string UNK_1;
        string UNK_2_cnt;
        string UNK_2_tab;
        string UNK_3;
        string timetab;
        public string body_part;
        public string handness;
        string wpn_mesh_cnt;
        public string[] wpn_mesh; //2 total, 0-1
        string[] wpn_unkval; //2 total, 0-1
        string wpn_tex_cnt;
        public string[] wpn_tex; //4 total, 0-3
        string item_sound_cnt;
        public string[] item_sound; //4 total, 0-3
        public string drop_sound;
        public string equip_sound;
        public string effect;
        public string random_damage;
        public string patt;
        public string matt;
        public string weapon_type;
        public string crystal_type;
        public string critical;
        public string hit_mod;
        public string avoid_mod;
        public string shield_pdef;
        public string shield_rate;
        public string speed;
        public string mp_consume;
        public string SS_count;
        public string SPS_count;
        string curvature;
        string UNK_4;
        public string is_hero;
        string UNK_5;
        string effA;
        string effB;
        string[] junk1A;//5 total, 0-4
        string[] junk1B;//5 total, 0-4
        public string rangeA;
        public string rangeB;
        string[] junk2A;//6 total, 0-5
        string[] junk2B;//6 total, 0-5
        string[] junk3;//6 total, 0-5
        string[] icons;//4 total, 0-3

        public Client_Weapon(string line)
        {
            string[] weaponLine = line.Split('\t');

            tag = weaponLine[0];
            id = weaponLine[1];
            drop_type = weaponLine[2];
            drop_anim_type = weaponLine[3];
            drop_radius = weaponLine[4];
            drop_height = weaponLine[5];
            UNK_0 = weaponLine[6];
            drop_mesh1 = weaponLine[7];
            drop_mesh2 = weaponLine[8];
            drop_mesh3 = weaponLine[9];
            drop_tex1 = weaponLine[10];
            drop_tex2 = weaponLine[11];
            drop_tex3 = weaponLine[12];
            drop_extratex1 = weaponLine[13];

            newdata = new string[8];
            newdata[0] = weaponLine[14];
            newdata[1] = weaponLine[15];
            newdata[2] = weaponLine[16];
            newdata[3] = weaponLine[17];
            newdata[4] = weaponLine[18];
            newdata[5] = weaponLine[19];
            newdata[6] = weaponLine[20];
            newdata[7] = weaponLine[21];

            icon = new string[5];
            icon[0] = weaponLine[22];
            icon[1] = weaponLine[23];
            icon[2] = weaponLine[24];
            icon[3] = weaponLine[25];
            icon[4] = weaponLine[26];

            durability = weaponLine[27];
            weight = weaponLine[28];
            material = weaponLine[29];
            crystallizable = weaponLine[30];
            UNK_1 = weaponLine[31];
            UNK_2_cnt = weaponLine[32];
            UNK_2_tab = weaponLine[33];
            UNK_3 = weaponLine[34];
            timetab = weaponLine[35];
            body_part = weaponLine[36];
            handness = weaponLine[37];
            wpn_mesh_cnt = weaponLine[38];

            wpn_mesh = new string[2];
            wpn_mesh[0] = weaponLine[39];
            wpn_mesh[1] = weaponLine[40];//2 total, 0-1

            wpn_unkval = new string[2];
            wpn_unkval[0] = weaponLine[41]; //2 total, 0-1
            wpn_unkval[1] = weaponLine[42];

            wpn_tex_cnt = weaponLine[43];

            wpn_tex = new string[4];
            wpn_tex[0] = weaponLine[44];//4 total, 0-3
            wpn_tex[1] = weaponLine[45];
            wpn_tex[2] = weaponLine[46];
            wpn_tex[3] = weaponLine[47];

            item_sound_cnt = weaponLine[48];

            item_sound = new string[4];
            item_sound[0] = weaponLine[49];//4 total, 0-3
            item_sound[1] = weaponLine[50];
            item_sound[2] = weaponLine[51];
            item_sound[3] = weaponLine[52];

            drop_sound = weaponLine[53];
            equip_sound = weaponLine[54];
            effect = weaponLine[55];
            random_damage = weaponLine[56];
            patt = weaponLine[57];
            matt = weaponLine[58];
            weapon_type = weaponLine[59];
            crystal_type = weaponLine[60];
            critical = weaponLine[61];
            hit_mod = weaponLine[62];
            avoid_mod = weaponLine[63];
            shield_pdef = weaponLine[64];
            shield_rate = weaponLine[65];
            speed = weaponLine[66];
            mp_consume = weaponLine[67];
            SS_count = weaponLine[68];
            SPS_count = weaponLine[69];
            curvature = weaponLine[70];
            UNK_4 = weaponLine[71];
            is_hero = weaponLine[72];
            UNK_5 = weaponLine[73];
            effA = weaponLine[74];
            effB = weaponLine[75];

            junk1A = new string[5];
            junk1A[0] = weaponLine[76];//5 total, 0-4
            junk1A[1] = weaponLine[77];
            junk1A[2] = weaponLine[78];
            junk1A[3] = weaponLine[79];
            junk1A[4] = weaponLine[80];

            junk1B = new string[5];//5 total, 0-4
            junk1B[0] = weaponLine[81];//5 total, 0-4
            junk1B[1] = weaponLine[82];
            junk1B[2] = weaponLine[83];
            junk1B[3] = weaponLine[84];
            junk1B[4] = weaponLine[85];

            rangeA = weaponLine[86];
            rangeB = weaponLine[87];

            junk2A = new string[6];//6 total, 0-5
            junk2A[0] = weaponLine[88];
            junk2A[1] = weaponLine[89];
            junk2A[2] = weaponLine[90];
            junk2A[3] = weaponLine[91];
            junk2A[4] = weaponLine[92];
            junk2A[5] = weaponLine[93];

            junk2B = new string[6];
            junk2B[0] = weaponLine[94];
            junk2B[1] = weaponLine[95];
            junk2B[2] = weaponLine[96];
            junk2B[3] = weaponLine[97];
            junk2B[4] = weaponLine[98];
            junk2B[5] = weaponLine[99];

            junk3 = new string[6];//6 total, 0-5
            junk3[0] = weaponLine[100];
            junk3[1] = weaponLine[101];
            junk3[2] = weaponLine[102];
            junk3[3] = weaponLine[103];
            junk3[4] = weaponLine[104];
            junk3[5] = weaponLine[105];

            icons = new string[4];//4 total, 0-3
            icons[0] = weaponLine[106];
            icons[1] = weaponLine[107];
            icons[2] = weaponLine[108];
            icons[3] = weaponLine[109];
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
                UNK_1 + '\t' +
                UNK_2_cnt + '\t' +
                UNK_2_tab + '\t' +
                UNK_3 + '\t' +
                timetab + '\t' +
                body_part + '\t' +
                handness + '\t' +
                wpn_mesh_cnt + '\t' +
                GetArrayString(wpn_mesh) + '\t' +
                GetArrayString(wpn_unkval) + '\t' +
                wpn_tex_cnt + '\t' +
                GetArrayString(wpn_tex) + '\t' +
                item_sound_cnt + '\t' +
                GetArrayString(item_sound) + '\t' +
                drop_sound + '\t' +
                equip_sound + '\t' +
                effect + '\t' +
                random_damage + '\t' +
                patt + '\t' +
                matt + '\t' +
                weapon_type + '\t' +
                crystal_type + '\t' +
                critical + '\t' +
                hit_mod + '\t' +
                avoid_mod + '\t' +
                shield_pdef + '\t' +
                shield_rate + '\t' +
                speed + '\t' +
                mp_consume + '\t' +
                SS_count + '\t' +
                SPS_count + '\t' +
                curvature + '\t' +
                UNK_4 + '\t' +
                is_hero + '\t' +
                UNK_5 + '\t' +
                effA + '\t' +
                effB + '\t' +
                GetArrayString(junk1A) + '\t' +
                GetArrayString(junk1B) + '\t' +
                rangeA + '\t' +
                rangeB + '\t' +
                GetArrayString(junk2A) + '\t' +
                GetArrayString(junk2B) + '\t' +
                GetArrayString(junk3) + '\t' +
                GetArrayString(icons);
        }
    }
}
