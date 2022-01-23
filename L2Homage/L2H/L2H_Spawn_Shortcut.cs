using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Spawn_Shortcut
    {
        public L2H_Spawn_Selection_Block L2H_Spawn_Selection_Block { get; set; }
        public string Zone { get; set; }
        public string Shortcut_Name { get; set; }
        public int Layer { get; set; }


        public L2H_Spawn_Shortcut(List<L2H_Spawn_Selection_Block> blocks, string name, string zone, int layer = 0)
        {
            this.L2H_Spawn_Selection_Block = blocks.Find(x=>x.ZoneID == zone);
            this.Shortcut_Name = name;
            this.Zone = zone;
            this.Layer = layer;
        }

        public string Get_Display_Name()
        {
            return Shortcut_Name;
        }

        
    }
}
