using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Itemname : Client_File
    {
        public string id;
        public string name;
        public string add_name;
        public string description;
        public string description_textstart;
        public string description_textend = @"\0";
        string popup;
        public string supercnt0;
        public string cnt0;
        public string[] set_ids;//5 total, 0-4
        public string set_bonus_desc;
        public string supercnt1;
        public string cnt1;
        public string set_extra_ids;
        public string set_extra_desc;
        string[] unk1;//9 total, 0-8
        public string special_enchant_amount;
        public string special_enchant_desc;
        string unk2;

        public Client_Itemname(string line)
        {
            string[] itemName_eLine = line.Split('\t');

            id = itemName_eLine[0];
            name = itemName_eLine[1];
            add_name = itemName_eLine[2];
            description = itemName_eLine[3];
            description_textstart = description.Substring(0, 2);
            description = description.Remove(0, 2);
            description = description.Replace(@"\0", "");

            popup = itemName_eLine[4];
            supercnt0 = itemName_eLine[5];
            cnt0 = itemName_eLine[6];

            set_ids = new string[5];
            set_ids[0] = itemName_eLine[7];
            set_ids[1] = itemName_eLine[8];
            set_ids[2] = itemName_eLine[9];
            set_ids[3] = itemName_eLine[10];
            set_ids[4] = itemName_eLine[11];

            set_bonus_desc = itemName_eLine[12];
            set_bonus_desc = set_bonus_desc.Remove(0, 2);
            set_bonus_desc = set_bonus_desc.Replace(@"\0", "");

            supercnt1 = itemName_eLine[13];
            cnt1 = itemName_eLine[14];
            set_extra_ids = itemName_eLine[15];
            set_extra_desc = itemName_eLine[16];
            set_extra_desc = set_extra_desc.Remove(0, 2);
            set_extra_desc = set_extra_desc.Replace(@"\0", "");

            unk1 = new string[9];
            unk1[0] = itemName_eLine[17];
            unk1[1] = itemName_eLine[18];
            unk1[2] = itemName_eLine[19];
            unk1[3] = itemName_eLine[20];
            unk1[4] = itemName_eLine[21];
            unk1[5] = itemName_eLine[22];
            unk1[6] = itemName_eLine[23];
            unk1[7] = itemName_eLine[24];
            unk1[8] = itemName_eLine[25];

            special_enchant_amount = itemName_eLine[26];
            special_enchant_desc = itemName_eLine[27];
            special_enchant_desc = special_enchant_desc.Remove(0, 2);
            special_enchant_desc = special_enchant_desc.Replace(@"\0", "");
            unk2 = itemName_eLine[28];
        }

        public string GetExportString()
        {
            //start by creating the array string
            #region array strings
            string set_id_string = "";
            string unk1_string = GetArrayString(unk1);

            //Description is a bit special, so a section of it's own
            string description_string = description_textstart;

            if (description != "")
            {
                description_string = description_textstart + description + description_textend;
            }

            string set_bonus_desc_string = "a,";

            if (!string.IsNullOrEmpty(set_bonus_desc))
            {
                set_bonus_desc_string += set_bonus_desc + @"\0";
            }

            string set_extra_desc_string = "a,";

            if (!string.IsNullOrEmpty(set_extra_desc))
            {
                set_extra_desc_string += set_extra_desc + @"\0";
            }

            string special_enchant_desc_string = "a,";

            if (!string.IsNullOrEmpty(special_enchant_desc))
            {
                special_enchant_desc_string += special_enchant_desc + @"\0";
            }


            //Calculating set items numbers for client
            int numberOfBaseSetItems = 0;
            int numberOfExtraSetItems = 0;

            if (!string.IsNullOrEmpty(set_ids[0]))
            {
                numberOfBaseSetItems++;
                set_id_string += set_ids[0] + "\t";
            }
            if (!string.IsNullOrEmpty(set_ids[1]))
            {
                numberOfBaseSetItems++;
                set_id_string += set_ids[1] + "\t";
            }
            if (!string.IsNullOrEmpty(set_ids[2]))
            {
                numberOfBaseSetItems++;
                set_id_string += set_ids[2] + "\t";
            }
            if (!string.IsNullOrEmpty(set_ids[3]))
            {
                numberOfBaseSetItems++;
                set_id_string += set_ids[3] + "\t";
            }
            if (!string.IsNullOrEmpty(set_ids[4]))
            {
                numberOfBaseSetItems++;
                set_id_string += set_ids[4] + "\t";
            }

            if (!string.IsNullOrEmpty(set_extra_ids))
            {
                numberOfExtraSetItems++;
            }

            for (int i = 0; i < 5 - numberOfBaseSetItems; i++)
            {
                set_id_string += "\t";
            }


            #endregion
            //after array string, return full string
            if (numberOfBaseSetItems > 0)
            {
                return
                    id + '\t' +
                    name + '\t' +
                    add_name + '\t' +
                    description_string + '\t' +
                    popup + '\t' +
                    numberOfBaseSetItems.ToString() + '\t' +
                    numberOfBaseSetItems.ToString() + '\t' +
                    set_id_string +
                    set_bonus_desc_string + '\t' +
                    numberOfExtraSetItems.ToString() + '\t' +
                    numberOfExtraSetItems.ToString() + '\t' +
                    set_extra_ids + '\t' +
                    set_extra_desc_string + '\t' +
                    unk1_string + '\t' +
                    special_enchant_amount + '\t' +
                    special_enchant_desc_string + '\t' +
                    unk2;
            }
            else
            {
                set_id_string = GetArrayString(set_ids);

                return
                    id + '\t' +
                    name + '\t' +
                    add_name + '\t' +
                    description_string + '\t' +
                    popup + '\t' +
                    numberOfBaseSetItems.ToString() + '\t' +
                    numberOfBaseSetItems.ToString() + '\t' +
                    set_id_string + '\t' +
                    set_bonus_desc_string + '\t' +
                    numberOfExtraSetItems.ToString() + '\t' +
                    numberOfExtraSetItems.ToString() + '\t' +
                    set_extra_ids + '\t' +
                    set_extra_desc_string + '\t' +
                    unk1_string + '\t' +
                    special_enchant_amount + '\t' +
                    special_enchant_desc_string + '\t' +
                    unk2;
            }
        }

    }
}
