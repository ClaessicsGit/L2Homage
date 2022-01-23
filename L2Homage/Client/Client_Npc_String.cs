using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_Npc_String
    {
        public string ID;
        public string text;

        public bool u_string;

        public Client_Npc_String(string ID, string text)
        {
            this.ID = ID;
            this.text = text;
            u_string = false;
        }

        public Client_Npc_String(string source)
        {
            
            if (source.Contains("\ta,"))
            {
                string[] splitString = source.Split(new string[] { "\ta," }, StringSplitOptions.RemoveEmptyEntries);

                ID = splitString[0];
                if (splitString.Length > 1)
                    text = splitString[1].Replace(@"\0", "");
                else
                    text = "";

                u_string = false;
            }
            else if (source.Contains("\tu,"))
            {
                string[] splitString = source.Split(new string[] { "\tu," }, StringSplitOptions.RemoveEmptyEntries);

                ID = splitString[0];
                if (splitString.Length > 1)
                    text = splitString[1].Replace(@"\0", "");
                else
                    text = "";

                u_string = true;
            }
            else
            {
                string[] splitString = source.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);

                ID = splitString[0];
                text = splitString[1].Replace(@"\0", "");
            }
                
        }

        public string GetExportString()
        {
            string prefix = "a,";

            if (u_string)
                prefix = "u,";

            return ID + "\t" + prefix + text + @"\0";
        }

    }
}
