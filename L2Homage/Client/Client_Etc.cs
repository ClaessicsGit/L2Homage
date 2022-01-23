using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Etc : Client_File
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
        string[] UNK_2_tab;//6 total, 0-5
        string UNK_3;
        string fort;
        string mesh_tex_pair_cntm;
        string[] mesh_tex_pair_m;//2 total, 0-1
        string mesh_tex_pair_cntt;
        string[] mesh_tex_pair_t;//4 total, 0-3
        public string item_sound;
        public string equip_sound;
        public string stackable;
        public string family;
        public string grade;

        public Client_Etc(string line)
        {
            string[] etcLine = line.Split('\t');

            tag = etcLine[0];
            id = etcLine[1];

            drop_type = etcLine[2];
            drop_anim_type = etcLine[3];
            drop_radius = etcLine[4];
            drop_height = etcLine[5];
            UNK_0 = etcLine[6];
            drop_mesh1 = etcLine[7];
            drop_mesh2 = etcLine[8];
            drop_mesh3 = etcLine[9];
            drop_tex1 = etcLine[10];
            drop_tex2 = etcLine[11];
            drop_tex3 = etcLine[12];
            drop_extratex1 = etcLine[13];

            newdata = new string[8];
            newdata[0] = etcLine[14];
            newdata[1] = etcLine[15];
            newdata[2] = etcLine[16];
            newdata[3] = etcLine[17];
            newdata[4] = etcLine[18];
            newdata[5] = etcLine[19];
            newdata[6] = etcLine[20];
            newdata[7] = etcLine[21];

            icon = new string[5];
            icon[0] = etcLine[22];
            icon[1] = etcLine[23];
            icon[2] = etcLine[24];
            icon[3] = etcLine[25];
            icon[4] = etcLine[26];

            durability = etcLine[27];
            weight = etcLine[28];
            material = etcLine[29];
            crystallizable = etcLine[30];
            UNK_1 = etcLine[31];
            UNK_2_cnt = etcLine[32];

            UNK_2_tab = new string[6];//6 total, 0-5
            UNK_2_tab[0] = etcLine[33];
            UNK_2_tab[1] = etcLine[34];
            UNK_2_tab[2] = etcLine[35];
            UNK_2_tab[3] = etcLine[36];
            UNK_2_tab[4] = etcLine[37];
            UNK_2_tab[5] = etcLine[38];

            UNK_3 = etcLine[39];
            fort = etcLine[40];
            mesh_tex_pair_cntm = etcLine[41];

            mesh_tex_pair_m = new string[2];//2 total, 0-1
            mesh_tex_pair_m[0] = etcLine[42];
            mesh_tex_pair_m[1] = etcLine[43];

            mesh_tex_pair_cntt = etcLine[44];

            mesh_tex_pair_t = new string[4];//4 total, 0-3
            mesh_tex_pair_t[0] = etcLine[45];
            mesh_tex_pair_t[1] = etcLine[46];
            mesh_tex_pair_t[2] = etcLine[47];
            mesh_tex_pair_t[3] = etcLine[48];

            item_sound = etcLine[49];
            equip_sound = etcLine[50];
            stackable = etcLine[51];
            family = etcLine[52];
            grade = etcLine[53];
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
                GetArrayString(UNK_2_tab) + '\t' +
                UNK_3 + '\t' +
                fort + '\t' +
                mesh_tex_pair_cntm + '\t' +
                GetArrayString(mesh_tex_pair_m) + '\t' +
                mesh_tex_pair_cntt + '\t' +
                GetArrayString(mesh_tex_pair_t) + '\t' +
                item_sound + '\t' +
                equip_sound + '\t' +
                stackable + '\t' +
                family + '\t' +
                grade;
        }

    }
}
