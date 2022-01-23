using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Skillname
    {
        public string id;
        public string level;
        public string name;
        public string description;
        public bool description_u = false;
        public string desc_add1;
        public bool desc_add1_u = false;
        public string desc_add2;
        public bool desc_add2_u = false;

        public Client_Skillname(string dataline)
        {
            string[] splitDataline = dataline.Split('\t');

            id = splitDataline[0];
            level = splitDataline[1];
            name = splitDataline[2];
            name = name.Remove(0, 2);
            name = name.Remove(name.Length - 2, 2);
            description = splitDataline[3];
            if (description.Length > 0)
            {
                if (description[0] == 'u')
                    description_u = true;

                description = description.Remove(0, 2);
                if (description.Length > 0)
                    description = description.Remove(description.Length - 2, 2);
            }

            desc_add1 = splitDataline[4];
            if (desc_add1.Length > 0)
            {
                if (desc_add1[0] == 'u')
                    desc_add1_u = true;

                desc_add1 = desc_add1.Remove(0, 2);
                if (desc_add1.Length > 0)
                    desc_add1 = desc_add1.Remove(desc_add1.Length - 2, 2);
            }

            desc_add2 = splitDataline[5];
            if (desc_add2.Length > 0)
            {
                if (desc_add2[0] == 'u')
                    desc_add2_u = true;

                desc_add2 = desc_add2.Remove(0, 2);
                if (desc_add2.Length > 0)
                    desc_add2 = desc_add2.Remove(desc_add2.Length - 2, 2);
            }
        }

        public string GetExportString()
        {
            string exportString = "";

            string replacedDescription = "";
            if (description_u)
                replacedDescription += "u,";
            else
                replacedDescription += "a,";

            replacedDescription += description;
            if (!string.IsNullOrEmpty(description))
                replacedDescription = replacedDescription + @"\0";


            string replacedDesc_add1 = "";
            if (desc_add1_u)
                replacedDesc_add1 = "u," + desc_add1 + @"\0";
            else
                replacedDesc_add1 = "a," + desc_add1 + @"\0";

            string replacedDesc_add2 = "";
            if (desc_add2_u)
                replacedDesc_add2 = "u," + desc_add2 + @"\0";
            else
                replacedDesc_add2 = "a," + desc_add2 + @"\0";

            exportString += id + "\t" + level + "\t" +
                                        "a," + name + @"\0" + "\t" +
                                        replacedDescription + "\t" +
                                        replacedDesc_add1 + "\t" +
                                        replacedDesc_add2;

            return exportString;

        }

    }
}
