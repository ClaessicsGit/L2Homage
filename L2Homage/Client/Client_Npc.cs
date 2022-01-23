using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Npc
    {
        public string tag;
        public string npcClass;
        public string mesh;

        public string cnt_tex1;
        public string tex1_0_;
        public string tex1_1_;
        public string tex1_2_;
        public string tex1_3_;
        public string tex1_4_;
        public string tex1_5_;
        public string tex1_6_;
        public string cnt_tex2;
        public string tex1_0_2;
        public string tex1_1_2;
        public string cnt_dtab1;
        public string dtab1_0_;
        public string dtab1_1_;
        public string dtab1_2_;
        public string dtab1_3_;
        public string dtab1_4_;
        public string dtab1_5_;
        public string dtab1_6_;
        public string dtab1_7_;
        public string dtab1_8_;
        public string dtab1_9_;
        public string dtab1_10_;
        public string dtab1_11_;
        public string dtab1_12_;
        public string dtab1_13_;
        public string dtab1_14_;
        public string dtab1_15_;
        public string dtab1_16_;
        public string dtab1_17_;
        public string dtab1_18_;
        public string dtab1_19_;
        public string dtab1_20_;
        public string dtab1_21_;
        public string dtab1_22_;
        public string dtab1_23_;
        public string dtab1_24_;
        public string dtab1_25_;
        public string dtab1_26_;
        public string dtab1_27_;
        public string dtab1_28_;
        public string dtab1_29_;
        public string dtab1_30_;
        public string dtab1_31_;
        public string dtab1_32_;
        public string dtab1_33_;
        public string npc_speed;
        //public string unk0_cnt; Epilogue
        //public string unk0_tab; Epilogue
        public string unk_0; //H5
        public string cnt_snd1;
        public string snd1_0_;
        public string snd1_1_;
        public string snd1_2_;
        public string cnt_snd2;
        public string snd2_0_;
        public string snd2_1_;
        public string snd2_2_;
        public string snd2_3_;
        public string snd2_4_;
        public string cnt_snd3;
        public string snd3_0_;
        public string snd3_1_;
        public string snd3_2_;
        public string rb_effect_on;
        public string rb_effect;
        public string rb_effect_fl;
        public string unk1_cnt;
        string unk1_tab_0_;
        string unk1_tab_1_;
        string unk1_tab_2_;
        string unk1_tab_3_;
        string unk1_tab_4_;
        string unk1_tab_5_;
        string unk1_tab_6_;
        string unk1_tab_7_;
        string unk1_tab_8_;
        string unk1_tab_9_;
        string unk1_tab_10_;
        string unk1_tab_11_;
        string unk1_tab_12_;
        string unk1_tab_13_;
        string unk1_tab_14_;
        string unk1_tab_15_;
        string unk1_tab_16_;
        string unk1_tab_17_;
        string unk1_tab_18_;
        string unk1_tab_19_;
        string unk1_tab_20_;
        string unk1_tab_21_;
        string unk1_tab_22_;
        string unk1_tab_23_;
        string unk1_tab_24_;
        string unk1_tab_25_;
        string unk1_tab_26_;
        string unk1_tab_27_;
        string unk1_tab_28_;
        string unk1_tab_29_;
        string unk1_tab_30_;
        string unk1_tab_31_;
        string unk1_tab_32_;
        string unk1_tab_33_;
        string unk1_tab_34_;
        string unk1_tab_35_;
        string unk1_tab_36_;
        string unk1_tab_37_;
        string unk1_tab_38_;
        string unk1_tab_39_;
        string unk1_tab_40_;
        string unk1_tab_41_;
        string unk2_cnt;
        string unk2_tab_0_;
        string unk2_tab_1_;
        string unk2_tab_2_;
        string unk2_tab_3_;
        string unk2_tab_4_;
        string unk2_tab_5_;
        string unk2_tab_6_;
        string unk2_tab_7_;
        string unk2_tab_8_;
        string unk2_tab_9_;
        string unk2_tab_10_;
        string unk2_tab_11_;
        string unk2_tab_12_;
        string unk2_tab_13_;
        string unk2_tab_14_;
        string unk2_tab_15_;
        string unk2_tab_16_;
        string unk2_tab_17_;
        string unk2_tab_18_;
        string unk2_tab_19_;
        string unk2_tab_20_;
        string unk2_tab_21_;
        string unk2_tab_22_;
        string unk2_tab_23_;
        string unk2_tab_24_;
        string unk2_tab_25_;
        string unk2_tab_26_;
        string unk2_tab_27_;
        string unk2_tab_28_;
        string unk2_tab_29_;
        string unk2_tab_30_;
        string unk2_tab_31_;
        string unk2_tab_32_;
        string unk2_tab_33_;
        string unk2_tab_34_;
        string unk2_tab_35_;
        string unk2_tab_36_;
        string unk2_tab_37_;
        string unk2_tab_38_;
        string unk2_tab_39_;
        string unk2_tab_40_;
        string unk2_tab_41_;
        public string effect;
        public string UNK_3;
        public string sound_rad;
        public string sound_vol;
        public string sound_rnd;
        public string quest_be;
        public string class_lim_;
        public string npcend_cnt;
        public string npcend_0_;
        public string npcend_1_;
        public string npcend_2_; 
        public string npcend_3_;
        public string npcend_4_;
        public string npcend_cnt2;

        public Client_Npc(string line)
        {
            string[] npcgrpLine = line.Split('\t');

            tag = npcgrpLine[0];
            npcClass = npcgrpLine[1];
            mesh = npcgrpLine[2];
            cnt_tex1 = npcgrpLine[3];
            tex1_0_ = npcgrpLine[4];
            tex1_1_ = npcgrpLine[5];
            tex1_2_ = npcgrpLine[6];
            tex1_3_ = npcgrpLine[7];
            tex1_4_ = npcgrpLine[8];
            tex1_5_ = npcgrpLine[9];
            tex1_6_ = npcgrpLine[10];
            cnt_tex2 = npcgrpLine[11];
            tex1_0_2 = npcgrpLine[12];
            tex1_1_2 = npcgrpLine[13];
            cnt_dtab1 = npcgrpLine[14];
            dtab1_0_ = npcgrpLine[15];
            dtab1_1_ = npcgrpLine[16];
            dtab1_2_ = npcgrpLine[17];
            dtab1_3_ = npcgrpLine[18];
            dtab1_4_ = npcgrpLine[19];
            dtab1_5_ = npcgrpLine[20];
            dtab1_6_ = npcgrpLine[21];
            dtab1_7_ = npcgrpLine[22];
            dtab1_8_ = npcgrpLine[23];
            dtab1_9_ = npcgrpLine[24];
            dtab1_10_ = npcgrpLine[25];
            dtab1_11_ = npcgrpLine[26];
            dtab1_12_ = npcgrpLine[27];
            dtab1_13_ = npcgrpLine[28];
            dtab1_14_ = npcgrpLine[29];
            dtab1_15_ = npcgrpLine[30];
            dtab1_16_ = npcgrpLine[31];
            dtab1_17_ = npcgrpLine[32];
            dtab1_18_ = npcgrpLine[33];
            dtab1_19_ = npcgrpLine[34];
            dtab1_20_ = npcgrpLine[35];
            dtab1_21_ = npcgrpLine[36];
            dtab1_22_ = npcgrpLine[37];
            dtab1_23_ = npcgrpLine[38];
            dtab1_24_ = npcgrpLine[39];
            dtab1_25_ = npcgrpLine[40];
            dtab1_26_ = npcgrpLine[41];
            dtab1_27_ = npcgrpLine[42];
            dtab1_28_ = npcgrpLine[43];
            dtab1_29_ = npcgrpLine[44];
            dtab1_30_ = npcgrpLine[45];
            dtab1_31_ = npcgrpLine[46];
            dtab1_32_ = npcgrpLine[47];
            dtab1_33_ = npcgrpLine[48];
            npc_speed = npcgrpLine[49];
            unk_0 = npcgrpLine[50];
            cnt_snd1    = npcgrpLine[51];
            snd1_0_     = npcgrpLine[52];
            snd1_1_     = npcgrpLine[53];
            snd1_2_     = npcgrpLine[54];
            cnt_snd2    = npcgrpLine[55];
            snd2_0_     = npcgrpLine[56];
            snd2_1_     = npcgrpLine[57];
            snd2_2_     = npcgrpLine[58];
            snd2_3_     = npcgrpLine[59];
            snd2_4_     = npcgrpLine[60];
            cnt_snd3    = npcgrpLine[61];
            snd3_0_     = npcgrpLine[62];
            snd3_1_     = npcgrpLine[63];
            snd3_2_     = npcgrpLine[64];
            rb_effect_on = npcgrpLine[65];
            rb_effect = npcgrpLine[66];
            rb_effect_fl = npcgrpLine[67];
            unk1_cnt = npcgrpLine[68];
            unk1_tab_0_ = npcgrpLine[69];
            unk1_tab_1_ = npcgrpLine[70];
            unk1_tab_2_ = npcgrpLine[71];
            unk1_tab_3_ = npcgrpLine[72];
            unk1_tab_4_ = npcgrpLine[73];
            unk1_tab_5_ = npcgrpLine[74];
            unk1_tab_6_ = npcgrpLine[75];
            unk1_tab_7_ = npcgrpLine[76];
            unk1_tab_8_ = npcgrpLine[77];
            unk1_tab_9_ = npcgrpLine[78];
            unk1_tab_10_ = npcgrpLine[79];
            unk1_tab_11_ = npcgrpLine[80];
            unk1_tab_12_ = npcgrpLine[81];
            unk1_tab_13_ = npcgrpLine[82];
            unk1_tab_14_ = npcgrpLine[83];
            unk1_tab_15_ = npcgrpLine[84];
            unk1_tab_16_ = npcgrpLine[85];
            unk1_tab_17_ = npcgrpLine[86];
            unk1_tab_18_ = npcgrpLine[87];
            unk1_tab_19_ = npcgrpLine[88];
            unk1_tab_20_ = npcgrpLine[89];
            unk1_tab_21_ = npcgrpLine[90];
            unk1_tab_22_ = npcgrpLine[91];
            unk1_tab_23_ = npcgrpLine[92];
            unk1_tab_24_ = npcgrpLine[93];
            unk1_tab_25_ = npcgrpLine[94];
            unk1_tab_26_ = npcgrpLine[95];
            unk1_tab_27_ = npcgrpLine[96];
            unk1_tab_28_ = npcgrpLine[97];
            unk1_tab_29_ = npcgrpLine[98];
            unk1_tab_30_ = npcgrpLine[99];
            unk1_tab_31_ = npcgrpLine[100];
            unk1_tab_32_ = npcgrpLine[101];
            unk1_tab_33_ = npcgrpLine[102];
            unk1_tab_34_ = npcgrpLine[103];
            unk1_tab_35_ = npcgrpLine[104];
            unk1_tab_36_ = npcgrpLine[105];
            unk1_tab_37_ = npcgrpLine[106];
            unk1_tab_38_ = npcgrpLine[107];
            unk1_tab_39_ = npcgrpLine[108];
            unk1_tab_40_ = npcgrpLine[109];
            unk1_tab_41_ = npcgrpLine[110];
            unk2_cnt = npcgrpLine[111];
            unk2_tab_0_ = npcgrpLine[112];
            unk2_tab_1_ = npcgrpLine[113];
            unk2_tab_2_ = npcgrpLine[114];
            unk2_tab_3_ = npcgrpLine[115];
            unk2_tab_4_ = npcgrpLine[116];
            unk2_tab_5_ = npcgrpLine[117];
            unk2_tab_6_ = npcgrpLine[118];
            unk2_tab_7_ = npcgrpLine[119];
            unk2_tab_8_ = npcgrpLine[120];
            unk2_tab_9_ = npcgrpLine[121];
            unk2_tab_10_ = npcgrpLine[122];
            unk2_tab_11_ = npcgrpLine[123];
            unk2_tab_12_ = npcgrpLine[124];
            unk2_tab_13_ = npcgrpLine[125];
            unk2_tab_14_ = npcgrpLine[126];
            unk2_tab_15_ = npcgrpLine[127];
            unk2_tab_16_ = npcgrpLine[128];
            unk2_tab_17_ = npcgrpLine[129];
            unk2_tab_18_ = npcgrpLine[130];
            unk2_tab_19_ = npcgrpLine[131];
            unk2_tab_20_ = npcgrpLine[132];
            unk2_tab_21_ = npcgrpLine[133];
            unk2_tab_22_ = npcgrpLine[134];
            unk2_tab_23_ = npcgrpLine[135];
            unk2_tab_24_ = npcgrpLine[136];
            unk2_tab_25_ = npcgrpLine[137];
            unk2_tab_26_ = npcgrpLine[138];
            unk2_tab_27_ = npcgrpLine[139];
            unk2_tab_28_ = npcgrpLine[140];
            unk2_tab_29_ = npcgrpLine[141];
            unk2_tab_30_ = npcgrpLine[142];
            unk2_tab_31_ = npcgrpLine[143];
            unk2_tab_32_ = npcgrpLine[144];
            unk2_tab_33_ = npcgrpLine[145];
            unk2_tab_34_ = npcgrpLine[146];
            unk2_tab_35_ = npcgrpLine[147];
            unk2_tab_36_ = npcgrpLine[148];
            unk2_tab_37_ = npcgrpLine[149];
            unk2_tab_38_ = npcgrpLine[150];
            unk2_tab_39_ = npcgrpLine[151];
            unk2_tab_40_ = npcgrpLine[152];
            unk2_tab_41_ = npcgrpLine[153];
            effect = npcgrpLine[154];
            UNK_3 = npcgrpLine[155];
            sound_rad = npcgrpLine[156];
            sound_vol = npcgrpLine[157];
            sound_rnd = npcgrpLine[158];
            quest_be = npcgrpLine[159];
            class_lim_ = npcgrpLine[160];
            npcend_cnt = npcgrpLine[161];
            npcend_0_ = npcgrpLine[162];
            npcend_1_ = npcgrpLine[163];
            npcend_2_ = npcgrpLine[164];
            npcend_3_ = npcgrpLine[165];
            npcend_4_ = npcgrpLine[166];
            npcend_cnt2 = npcgrpLine[167];

            
        }

        public string GetExportString()
        {
            string returnString = "";

            returnString +=
            tag + '\t' +
            npcClass + '\t' +
            mesh + '\t' +
            cnt_tex1 + '\t' +
            tex1_0_ + '\t' +
            tex1_1_ + '\t' +
            tex1_2_ + '\t' +
            tex1_3_ + '\t' +
            tex1_4_ + '\t' +
            tex1_5_ + '\t' +
            tex1_6_ + '\t' +
            cnt_tex2 + '\t' +
            tex1_0_2 + '\t' +
            tex1_1_2 + '\t' +
            cnt_dtab1 + '\t' +
            dtab1_0_ + '\t' +
            dtab1_1_ + '\t' +
            dtab1_2_ + '\t' +
            dtab1_3_ + '\t' +
            dtab1_4_ + '\t' +
            dtab1_5_ + '\t' +
            dtab1_6_ + '\t' +
            dtab1_7_ + '\t' +
            dtab1_8_ + '\t' +
            dtab1_9_ + '\t' +
            dtab1_10_ + '\t' +
            dtab1_11_ + '\t' +
            dtab1_12_ + '\t' +
            dtab1_13_ + '\t' +
            dtab1_14_ + '\t' +
            dtab1_15_ + '\t' +
            dtab1_16_ + '\t' +
            dtab1_17_ + '\t' +
            dtab1_18_ + '\t' +
            dtab1_19_ + '\t' +
            dtab1_20_ + '\t' +
            dtab1_21_ + '\t' +
            dtab1_22_ + '\t' +
            dtab1_23_ + '\t' +
            dtab1_24_ + '\t' +
            dtab1_25_ + '\t' +
            dtab1_26_ + '\t' +
            dtab1_27_ + '\t' +
            dtab1_28_ + '\t' +
            dtab1_29_ + '\t' +
            dtab1_30_ + '\t' +
            dtab1_31_ + '\t' +
            dtab1_32_ + '\t' +
            dtab1_33_ + '\t' +
            npc_speed + '\t' +
            unk_0 + '\t' +
            //unk0_cnt + '\t' +
            //unk0_tab + '\t' +
            cnt_snd1 + '\t' +
            snd1_0_ + '\t' +
            snd1_1_ + '\t' +
            snd1_2_ + '\t' +
            cnt_snd2 + '\t' +
            snd2_0_ + '\t' +
            snd2_1_ + '\t' +
            snd2_2_ + '\t' +
            snd2_3_ + '\t' +
            snd2_4_ + '\t' +
            cnt_snd3 + '\t' +
            snd3_0_ + '\t' +
            snd3_1_ + '\t' +
            snd3_2_ + '\t' +
            rb_effect_on + '\t' +
            rb_effect + '\t' +
            rb_effect_fl + '\t' +
            unk1_cnt + '\t' +
            unk1_tab_0_ + '\t' +
            unk1_tab_1_ + '\t' +
            unk1_tab_2_ + '\t' +
            unk1_tab_3_ + '\t' +
            unk1_tab_4_ + '\t' +
            unk1_tab_5_ + '\t' +
            unk1_tab_6_ + '\t' +
            unk1_tab_7_ + '\t' +
            unk1_tab_8_ + '\t' +
            unk1_tab_9_ + '\t' +
            unk1_tab_10_ + '\t' +
            unk1_tab_11_ + '\t' +
            unk1_tab_12_ + '\t' +
            unk1_tab_13_ + '\t' +
            unk1_tab_14_ + '\t' +
            unk1_tab_15_ + '\t' +
            unk1_tab_16_ + '\t' +
            unk1_tab_17_ + '\t' +
            unk1_tab_18_ + '\t' +
            unk1_tab_19_ + '\t' +
            unk1_tab_20_ + '\t' +
            unk1_tab_21_ + '\t' +
            unk1_tab_22_ + '\t' +
            unk1_tab_23_ + '\t' +
            unk1_tab_24_ + '\t' +
            unk1_tab_25_ + '\t' +
            unk1_tab_26_ + '\t' +
            unk1_tab_27_ + '\t' +
            unk1_tab_28_ + '\t' +
            unk1_tab_29_ + '\t' +
            unk1_tab_30_ + '\t' +
            unk1_tab_31_ + '\t' +
            unk1_tab_32_ + '\t' +
            unk1_tab_33_ + '\t' +
            unk1_tab_34_ + '\t' +
            unk1_tab_35_ + '\t' +
            unk1_tab_36_ + '\t' +
            unk1_tab_37_ + '\t' +
            unk1_tab_38_ + '\t' +
            unk1_tab_39_ + '\t' +
            unk1_tab_40_ + '\t' +
            unk1_tab_41_ + '\t' +
            unk2_cnt + '\t' +
            unk2_tab_0_ + '\t' +
            unk2_tab_1_ + '\t' +
            unk2_tab_2_ + '\t' +
            unk2_tab_3_ + '\t' +
            unk2_tab_4_ + '\t' +
            unk2_tab_5_ + '\t' +
            unk2_tab_6_ + '\t' +
            unk2_tab_7_ + '\t' +
            unk2_tab_8_ + '\t' +
            unk2_tab_9_ + '\t' +
            unk2_tab_10_ + '\t' +
            unk2_tab_11_ + '\t' +
            unk2_tab_12_ + '\t' +
            unk2_tab_13_ + '\t' +
            unk2_tab_14_ + '\t' +
            unk2_tab_15_ + '\t' +
            unk2_tab_16_ + '\t' +
            unk2_tab_17_ + '\t' +
            unk2_tab_18_ + '\t' +
            unk2_tab_19_ + '\t' +
            unk2_tab_20_ + '\t' +
            unk2_tab_21_ + '\t' +
            unk2_tab_22_ + '\t' +
            unk2_tab_23_ + '\t' +
            unk2_tab_24_ + '\t' +
            unk2_tab_25_ + '\t' +
            unk2_tab_26_ + '\t' +
            unk2_tab_27_ + '\t' +
            unk2_tab_28_ + '\t' +
            unk2_tab_29_ + '\t' +
            unk2_tab_30_ + '\t' +
            unk2_tab_31_ + '\t' +
            unk2_tab_32_ + '\t' +
            unk2_tab_33_ + '\t' +
            unk2_tab_34_ + '\t' +
            unk2_tab_35_ + '\t' +
            unk2_tab_36_ + '\t' +
            unk2_tab_37_ + '\t' +
            unk2_tab_38_ + '\t' +
            unk2_tab_39_ + '\t' +
            unk2_tab_40_ + '\t' +
            unk2_tab_41_ + '\t' +
            effect + '\t' +
            UNK_3 + '\t' +
            sound_rad + '\t' +
            sound_vol + '\t' +
            sound_rnd + '\t' +
            quest_be + '\t' +
            class_lim_ + '\t' +
            npcend_cnt + '\t' +
            npcend_0_ + '\t' +
            npcend_1_ + '\t' +
            npcend_2_ + '\t' +
            npcend_3_ + '\t' +
            npcend_4_ + '\t' +
            npcend_cnt2;

            return returnString;
        }

    }
}
