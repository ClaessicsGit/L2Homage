using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Template_Pointer
    {
        public string id;
        public string templateId;
        public string extraName;

        public L2H_Template_Pointer(string importString)
        {
            string[] s = importString.Split('\t');
            id = s[0];
            templateId = s[1];

            if (s.Length > 2)
                extraName = s[2];

        }

        public L2H_Template_Pointer(string id, string templateItemId, string extraName = null)
        {
            this.id = id;
            this.templateId = templateItemId;

            if (extraName != null)
                this.extraName = extraName;

        }

        public string GetExportString()
        {
            if (extraName != null)
                return id + '\t' + templateId + '\t' + extraName;
            else
                return id + '\t' + templateId;
        }
    }
}