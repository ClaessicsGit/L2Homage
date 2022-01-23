using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Filter_Npc_Type
    {
        public string readableText { get; set; }
        public string filteringText { get; set; }

        public bool IsSelected { get; set; }
        public L2H_Filter_Npc_Type Instance { get { return this; } }

        public override string ToString()
        {
            return readableText;
        }

    }
}
