using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_System_Message
    {
        public string ID;
        public string UNK_0;        // Not sure, but it's always 1
        public string message;
        public string group;        // 0 - none - messages without source (or class if you prefer that term)
                                    // 0 - siege - castle/fortress siege related messages
                                    // 1 - battle - nondamage combat messages(successfull defense, critical hit, miss, etc.)
                                    // 2 - server - general server messages
                                    // 3 - damage - didn't need explanation (you make damage, you take damage)
                                    // 4 - popup - message is displayed in popup window
                                    // 5 - error - if any action produces error, here you go(cancel trade, failed invitation, failed login, etc.)
                                    // 6 - petition - use of petition system
                                    // 7 - useitems - didn't need explanation (eg. use shots, potions, enabled shots, equip armor, etc.)
        public string rgba_0;       // red hex value
        public string rgba_1;       // green hex value
        public string rgba_2;       // blue hex value
        public string rgba_3;       // Opacity
        public string item_sound;   // sound played when message is displayed
        public string sys_msg_ref;  // Source of message
        public string UNK_1_0;      // Position on screen. 1 for top left, 2 for top mid, 3 for top right, 4 for mid left, 5 for mid, 6 for mid right, 7 for bot mid, 8 for bot right, 0 for inside system ui box
        public string UNK_1_1;      // Not sure, but it's always 0
        public string UNK_1_2;      // duration of message in seconds.
        public string UNK_1_3;      // transition speed 0 - quick // 1 - slow // 11 - very slow
        public string UNK_1_4;      // Add decorative effect to message, 1 for true, 0 for false
        public string sub_msg;      // 
        public string type;         //none - messages without source (or class if you prefer that term)
                                    //siege - castle/fortress siege related messages
                                    //battle - nondamage combat messages(successfull defense, critical hit, miss, etc.)
                                    //server - general server messages
                                    //damage - didn't need explanation (you make damage, you take damage)
                                    //popup - message is displayed in popup window
                                    //error - if any action produces error, here you go(cancel trade, failed invitation, failed login, etc.)
                                    //petition - use of petition system
                                    //useitems - didn't need explanation (eg. use shots, potions, enabled shots, equip armor, etc.)

        public bool u_string;
        bool u_sub_msg;

        public Client_System_Message(string ID, string text)
        {
            this.ID = ID;
            this.message = text;
            UNK_0 = "1";
            group = "0";
            rgba_0 = "FF";
            rgba_1 = "FF";
            rgba_2 = "FF";
            rgba_3 = "FF";
            item_sound = "";
            sys_msg_ref = "";
            UNK_1_0 = "0";
            UNK_1_1 = "0";
            UNK_1_2 = "3";
            UNK_1_3 = "0";
            UNK_1_4 = "0";
            sub_msg = "";
            type = "none";
            u_string = false;

        }

        public Client_System_Message(string datastring)
        {
            string[] splitDatastring = datastring.Split('\t');

            ID = splitDatastring[0];
            UNK_0 = splitDatastring[1];
            if (splitDatastring[2].Length > 0)
                if (splitDatastring[2][0] == 'u')
                    u_string = true;
            if (splitDatastring[2].Length > 1)
                splitDatastring[2] = splitDatastring[2].Remove(0, 2);
            if (splitDatastring[2].Length > 1)
                splitDatastring[2] = splitDatastring[2].Remove(splitDatastring[2].Length - 2, 2);
            message = splitDatastring[2];



            group = splitDatastring[3];
            rgba_0 = splitDatastring[4];
            rgba_1 = splitDatastring[5];
            rgba_2 = splitDatastring[6];
            rgba_3 = splitDatastring[7];
            if (splitDatastring[8].Length > 1)
                splitDatastring[8] = splitDatastring[8].Remove(0, 2);
            if (splitDatastring[8].Length > 1)
                splitDatastring[8] = splitDatastring[8].Remove(splitDatastring[8].Length - 2, 2);
            item_sound = splitDatastring[8];
            if (splitDatastring[9].Length > 1)
                splitDatastring[9] = splitDatastring[9].Remove(0, 2);
            if (splitDatastring[9].Length > 1)
                splitDatastring[9] = splitDatastring[9].Remove(splitDatastring[9].Length - 2, 2);
            sys_msg_ref = splitDatastring[9];
            UNK_1_0 = splitDatastring[10];
            UNK_1_1 = splitDatastring[11];
            UNK_1_2 = splitDatastring[12];
            UNK_1_3 = splitDatastring[13];
            UNK_1_4 = splitDatastring[14];
            if (splitDatastring[15].Length > 0)
                if (splitDatastring[15][0] == 'u')
                    u_sub_msg = true;
            if (splitDatastring[15].Length > 1)
                splitDatastring[15] = splitDatastring[15].Remove(0, 2);
            if (splitDatastring[15].Length > 1)
                splitDatastring[15] = splitDatastring[15].Remove(splitDatastring[15].Length - 2, 2);
            sub_msg = splitDatastring[15];


            if (splitDatastring[16].Length > 1)
                splitDatastring[16] = splitDatastring[16].Remove(0, 2);
            if (splitDatastring[16].Length > 1)
                splitDatastring[16] = splitDatastring[16].Remove(splitDatastring[16].Length - 2, 2);
            type = splitDatastring[16];


        }

        public string GetExportString()
        {
            string replacedMessage = "";
            if (u_string)
            {
                replacedMessage = "u," + message;
            }
            else
                replacedMessage = "a," + message;

            if (message.Length > 0)
                replacedMessage += @"\0";

            string replacedItem_sound = "a," + item_sound;
            if (item_sound.Length > 0)
                replacedItem_sound += @"\0";
            string replacedSys_msg_ref = "a," + sys_msg_ref;
            if (sys_msg_ref.Length > 0)
                replacedSys_msg_ref += @"\0";
            string replacedSub_msg = "";// "a," + sub_msg;

            if (u_string)
            {
                replacedSub_msg = "u," + sub_msg;
            }
            else
                replacedSub_msg = "a," + sub_msg;


            if (sub_msg.Length > 0)
                replacedSub_msg += @"\0";
            string replacedType = "a," + type;
            if (type.Length > 0)
                replacedType += @"\0";
            string returnString = ID + "\t" +
                                    UNK_0 + "\t" +
                                    replacedMessage + "\t" +
                                    group + "\t" +
                                    rgba_0 + "\t" +
                                    rgba_1 + "\t" +
                                    rgba_2 + "\t" +
                                    rgba_3 + "\t" +
                                    replacedItem_sound + "\t" +
                                    replacedSys_msg_ref + "\t" +
                                    UNK_1_0 + "\t" +
                                    UNK_1_1 + "\t" +
                                    UNK_1_2 + "\t" +
                                    UNK_1_3 + "\t" +
                                    UNK_1_4 + "\t" +
                                    replacedSub_msg + "\t" +
                                    replacedType;

            return returnString;
        }

    }
}
