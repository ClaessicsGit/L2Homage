using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Classinfo : Client_File
    {
        public string id;
        public string description;

        public Client_Classinfo(string datastring)
        {
            string[] splitDatastring = datastring.Split('\t');

            id = splitDatastring[0];
            description = splitDatastring[1];
            if (description.Length > 2)
                description = description.Remove(0, 2);
            if (description.Length > 2)
                description = description.Remove(description.Length - 2, 2);

            description = description.Replace(@"\r\n", Environment.NewLine);
        }

        public string GetExportString()
        {
            string exportString = "";

            exportString += id + "\t" + "a," + description.Replace(Environment.NewLine, @"\r\n") + @"\0";

            return exportString;
        }
    }
}
