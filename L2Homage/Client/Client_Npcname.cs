using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Npcname
    {
        public string id;
        public string name;
        public string description;
        string rgb_0_;
        string rgb_1_;
        string rgb_2_;
        string reserved1;

        public Client_Npcname(string line)
        {
            string[] npcname_eLine = line.Split('\t');

            id = npcname_eLine[0];
            name = npcname_eLine[1];
            name = name.Replace("a,", "");
            name = name.Replace(@"\0", "");
            description = npcname_eLine[2];
            description = description.Remove(0, 2);
            description = description.Replace(@"\0", "");
            rgb_0_ = npcname_eLine[3];
            rgb_1_ = npcname_eLine[4];
            rgb_2_ = npcname_eLine[5];
            reserved1 = npcname_eLine[6];
        }

        public string GetExportString()
        {
            string replacedName = "a," + name;
            if (!string.IsNullOrEmpty(name))
                replacedName = replacedName + @"\0";
            string replacedDescription = "a," + description;
            if (!string.IsNullOrEmpty(description))
                replacedDescription = replacedDescription + @"\0";
            return id + '\t' + replacedName + '\t' + replacedDescription + '\t' + rgb_0_ + '\t' + rgb_1_ + '\t' + rgb_2_ + '\t' + reserved1;
        }

        public override string ToString()
        {
            return name + " (" + id + ")";
        }

    }
}
