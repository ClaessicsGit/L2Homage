using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Skillsound
    {
        public string id;
        public string level;
        public string spelleffect_sound_1;
        public string spelleffect_sound_2;
        public string spelleffect_sound_3;
        public string spelleffect_sound_vol_1;
        public string spelleffect_sound_rad_1;
        public string spelleffect_sound_vol_2;
        public string spelleffect_sound_rad_2;
        public string spelleffect_sound_vol_3;
        public string spelleffect_sound_rad_3;
        public string shoteffect_sound_1;
        public string shoteffect_sound_2;
        public string shoteffect_sound_3;
        public string shoteffect_sound_vol_1;
        public string shoteffect_sound_rad_1;
        public string shoteffect_sound_vol_2;
        public string shoteffect_sound_rad_2;
        public string shoteffect_sound_vol_3;
        public string shoteffect_sound_rad_3;
        public string expeffect_sound_1;
        public string expeffect_sound_2;
        public string expeffect_sound_3;
        public string expeffect_sound_vol_1;
        public string expeffect_sound_rad_1;
        public string expeffect_sound_vol_2;
        public string expeffect_sound_rad_2;
        public string expeffect_sound_vol_3;
        public string expeffect_sound_rad_3;
        public string mfighter_sub;
        public string ffighter_sub;
        public string mdarkelf_sub;
        public string fdarkelf_sub;
        public string mdwarf_sub;
        public string fdwarf_sub;
        public string melf_sub;
        public string felf_sub;
        public string mmagic_sub;
        public string fmagic_sub;
        public string morc_sub;
        public string forc_sub;
        public string mshaman_sub;
        public string fshaman_sub;
        public string mkamael_sub;
        public string fkamael_sub;
        public string mfighter_throw;
        public string ffighter_throw;
        public string mdarkelf_throw;
        public string fdarkelf_throw;
        public string mdwarf_throw;
        public string fdwarf_throw;
        public string melf_throw;
        public string felf_throw;
        public string mmagic_throw;
        public string fmagic_throw;
        public string morc_throw;
        public string forc_throw;
        public string mshaman_throw;
        public string fshaman_throw;
        public string mkamael_throw;
        public string fkamael_throw;
        public string mextra_throw;
        public string fextra_throw;
        public string sound_vol;
        public string sound_rad;

        public Client_Skillsound(string dataline)
        {
            string[] splitDataline = dataline.Split('\t');

            id = splitDataline[0];
            level = splitDataline[1];
            spelleffect_sound_1 = splitDataline[2];
            spelleffect_sound_2 = splitDataline[3];
            spelleffect_sound_3 = splitDataline[4];
            spelleffect_sound_vol_1 = splitDataline[5];
            spelleffect_sound_rad_1 = splitDataline[6];
            spelleffect_sound_vol_2 = splitDataline[7];
            spelleffect_sound_rad_2 = splitDataline[8];
            spelleffect_sound_vol_3 = splitDataline[9];
            spelleffect_sound_rad_3 = splitDataline[10];
            shoteffect_sound_1 = splitDataline[11];
            shoteffect_sound_2 = splitDataline[12];
            shoteffect_sound_3 = splitDataline[13];
            shoteffect_sound_vol_1 = splitDataline[14];
            shoteffect_sound_rad_1 = splitDataline[15];
            shoteffect_sound_vol_2 = splitDataline[16];
            shoteffect_sound_rad_2 = splitDataline[17];
            shoteffect_sound_vol_3 = splitDataline[18];
            shoteffect_sound_rad_3 = splitDataline[19];
            expeffect_sound_1 = splitDataline[20];
            expeffect_sound_2 = splitDataline[21];
            expeffect_sound_3 = splitDataline[22];
            expeffect_sound_vol_1 = splitDataline[23];
            expeffect_sound_rad_1 = splitDataline[24];
            expeffect_sound_vol_2 = splitDataline[25];
            expeffect_sound_rad_2 = splitDataline[26];
            expeffect_sound_vol_3 = splitDataline[27];
            expeffect_sound_rad_3 = splitDataline[28];
            mfighter_sub = splitDataline[29];
            ffighter_sub = splitDataline[30];
            mdarkelf_sub = splitDataline[31];
            fdarkelf_sub = splitDataline[32];
            mdwarf_sub = splitDataline[33];
            fdwarf_sub = splitDataline[34];
            melf_sub = splitDataline[35];
            felf_sub = splitDataline[36];
            mmagic_sub = splitDataline[37];
            fmagic_sub = splitDataline[38];
            morc_sub = splitDataline[39];
            forc_sub = splitDataline[40];
            mshaman_sub = splitDataline[41];
            fshaman_sub = splitDataline[42];
            mkamael_sub = splitDataline[43];
            fkamael_sub = splitDataline[44];
            mfighter_throw = splitDataline[45];
            ffighter_throw = splitDataline[46];
            mdarkelf_throw = splitDataline[47];
            fdarkelf_throw = splitDataline[48];
            mdwarf_throw = splitDataline[49];
            fdwarf_throw = splitDataline[50];
            melf_throw = splitDataline[51];
            felf_throw = splitDataline[52];
            mmagic_throw = splitDataline[53];
            fmagic_throw = splitDataline[54];
            morc_throw = splitDataline[55];
            forc_throw = splitDataline[56];
            mshaman_throw = splitDataline[57];
            fshaman_throw = splitDataline[58];
            mkamael_throw = splitDataline[59];
            fkamael_throw = splitDataline[60];
            mextra_throw = splitDataline[61];
            fextra_throw = splitDataline[62];
            sound_vol = splitDataline[63];
            sound_rad = splitDataline[64];
        }


        public string GetExportString()
        {
            string exportString = "";

            exportString += id + "\t" +
                            level + "\t" +
                            spelleffect_sound_1 + "\t" +
                            spelleffect_sound_2 + "\t" +
                            spelleffect_sound_3 + "\t" +
                            spelleffect_sound_vol_1 + "\t" +
                            spelleffect_sound_rad_1 + "\t" +
                            spelleffect_sound_vol_2 + "\t" +
                            spelleffect_sound_rad_2 + "\t" +
                            spelleffect_sound_vol_3 + "\t" +
                            spelleffect_sound_rad_3 + "\t" +
                            shoteffect_sound_1 + "\t" +
                            shoteffect_sound_2 + "\t" +
                            shoteffect_sound_3 + "\t" +
                            shoteffect_sound_vol_1 + "\t" +
                            shoteffect_sound_rad_1 + "\t" +
                            shoteffect_sound_vol_2 + "\t" +
                            shoteffect_sound_rad_2 + "\t" +
                            shoteffect_sound_vol_3 + "\t" +
                            shoteffect_sound_rad_3 + "\t" +
                            expeffect_sound_1 + "\t" +
                            expeffect_sound_2 + "\t" +
                            expeffect_sound_3 + "\t" +
                            expeffect_sound_vol_1 + "\t" +
                            expeffect_sound_rad_1 + "\t" +
                            expeffect_sound_vol_2 + "\t" +
                            expeffect_sound_rad_2 + "\t" +
                            expeffect_sound_vol_3 + "\t" +
                            expeffect_sound_rad_3 + "\t" +
                            mfighter_sub + "\t" +
                            ffighter_sub + "\t" +
                            mdarkelf_sub + "\t" +
                            fdarkelf_sub + "\t" +
                            mdwarf_sub + "\t" +
                            fdwarf_sub + "\t" +
                            melf_sub + "\t" +
                            felf_sub + "\t" +
                            mmagic_sub + "\t" +
                            fmagic_sub + "\t" +
                            morc_sub + "\t" +
                            forc_sub + "\t" +
                            mshaman_sub + "\t" +
                            fshaman_sub + "\t" +
                            mkamael_sub + "\t" +
                            fkamael_sub + "\t" +
                            mfighter_throw + "\t" +
                            ffighter_throw + "\t" +
                            mdarkelf_throw + "\t" +
                            fdarkelf_throw + "\t" +
                            mdwarf_throw + "\t" +
                            fdwarf_throw + "\t" +
                            melf_throw + "\t" +
                            felf_throw + "\t" +
                            mmagic_throw + "\t" +
                            fmagic_throw + "\t" +
                            morc_throw + "\t" +
                            forc_throw + "\t" +
                            mshaman_throw + "\t" +
                            fshaman_throw + "\t" +
                            mkamael_throw + "\t" +
                            fkamael_throw + "\t" +
                            mextra_throw + "\t" +
                            fextra_throw + "\t" +
                            sound_vol + "\t" +
                            sound_rad;

            return exportString;

        }
    }
}
