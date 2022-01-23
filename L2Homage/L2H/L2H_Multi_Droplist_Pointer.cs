using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Multi_Droplist_Pointer
    {
        public string droplistID;
        public List<string> multipartIDs;
        public List<string> multipartProbabilities;
        public bool isCustom;

        public L2H_Multi_Droplist_Pointer(string droplistID, bool isCustom, List<string> multipartIDs, List<string> multipartProbabilities)
        {
            this.multipartIDs = new List<string>();
            this.multipartProbabilities = new List<string>();
            this.droplistID = droplistID;
            this.multipartIDs = multipartIDs;
            this.multipartProbabilities = multipartProbabilities;
            this.isCustom = isCustom;
        }

        public string GetExportString()
        {
            string custom = "0";
            if (isCustom)
                custom = "1";
            string s = "";

            s = droplistID + '\t' + custom + '\t';

            for (int i = 0; i < multipartIDs.Count; i++)
            {
                s = s + multipartIDs[i] + '\t' + multipartProbabilities[i];

                if (i != multipartIDs.Count - 1)
                    s = s + '\t';
            }


            return s;
        }
    }
}
