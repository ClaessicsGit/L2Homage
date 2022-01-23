using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Droplist_Connection
    {
        public L2H_Droplist L2H_Droplist { get; set; }

        public L2H_Droplist_Connection Instance { get { return this; } }

        public bool IsSelected { get; set; }

        public override string ToString()
        {
            return L2H_Droplist.ToString();
        }

        public System.Windows.Media.SolidColorBrush GetDroplistColor
        {
            get
            {
                return L2H_Droplist.GetDroplistColor;
            }
        }
    }
    public class L2H_Droplist_NPC_Connection
    {
        public L2H_NPC L2H_Npc { get; set; }
        public bool IsSelected { get; set; }

        public L2H_Droplist_NPC_Connection Instance { get { return this; } }

        public override string ToString()
        {
            return L2H_Npc.client_Npcname.name + "\nID(" + L2H_Npc.ID.ToString() + ")";
        }
    }

}
