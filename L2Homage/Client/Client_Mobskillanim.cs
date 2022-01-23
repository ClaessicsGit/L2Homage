using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Mobskillanim
    {
        public string npc_id;
        public string skill_id;
        public string seq_name;
        public string skill_name;
        public string npc_name;
        public string npc_class;

        bool u_class;

        public Client_Mobskillanim(string dataString)
        {
            string[] splitDataString = dataString.Split('\t');

            npc_id = splitDataString[0];
            skill_id = splitDataString[1];
            seq_name = splitDataString[2];

            if (splitDataString[3].Length > 2)
                splitDataString[3] = splitDataString[3].Remove(0, 2);
            if (splitDataString[3].Length > 2)
                splitDataString[3] = splitDataString[3].Remove(splitDataString[3].Length - 2, 2);

            skill_name = splitDataString[3];   //

            if (splitDataString[4].Length > 1)
                splitDataString[4] = splitDataString[4].Remove(0, 2);
            if (splitDataString[4].Length > 1)
                splitDataString[4] = splitDataString[4].Remove(splitDataString[4].Length - 2, 2);

            npc_name = splitDataString[4];    /// 

            if (splitDataString[5][0] == 'u')
                u_class = true;

            if (splitDataString[5].Length > 1)
                splitDataString[5] = splitDataString[5].Remove(0, 2);
            if (splitDataString[5].Length > 1)
                splitDataString[5] = splitDataString[5].Remove(splitDataString[5].Length - 2, 2);
            npc_class = splitDataString[5];      //

        }

        public string GetExportString()
        {
            string exportString = "";

            string replacementSkill_name = "a," + skill_name;
            if (skill_name.Length > 0)
                replacementSkill_name += @"\0";

            string replacementNpc_name = "a," + npc_name;
            if (replacementNpc_name == "a,")
            {

            }
            else if (npc_name.Length > 0)
                replacementNpc_name += @"\0";

            string replacementNpc_class = "";
            if (u_class)
                replacementNpc_class = "u," + npc_class;
            else
                replacementNpc_class = "a," + npc_class;

            if (npc_class.Length > 0)
                replacementNpc_class += @"\0";

            exportString += npc_id + "\t" + skill_id + "\t" + seq_name + "\t" + replacementSkill_name + "\t" + replacementNpc_name + "\t" + replacementNpc_class;


            return exportString;
        }
    }
}
