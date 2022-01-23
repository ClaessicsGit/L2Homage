using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Npc_Droplist_Pointer
    {
        public string npc_id;
        public string droplist_normal_id;
        public string droplist_spoil_id;
        public string droplist_multi_id;
        public string droplist_extra_id;

        public L2H_Npc_Droplist_Pointer()
        {

        }


        public L2H_Npc_Droplist_Pointer(string npcId, string droplist_normal_Id, string droplist_spoil_Id, string droplist_multi_Id, string droplist_extra_Id)
        {
            npc_id = npcId;
            droplist_normal_id = droplist_normal_Id;
            droplist_spoil_id = droplist_spoil_Id;
            droplist_multi_id = droplist_multi_Id;
            droplist_extra_id = droplist_extra_Id;
        }

        public L2H_Npc_Droplist_Pointer(string importString)
        {
            string[] s = importString.Split('\t');
            npc_id = s[0];
            droplist_normal_id = s[1];
            droplist_spoil_id = s[2];
            droplist_multi_id = s[3];
            droplist_extra_id = s[4];
        }

        public string GetExportString()
        {
            return npc_id + '\t' + droplist_normal_id + '\t' + droplist_spoil_id + '\t' + droplist_multi_id + '\t' + droplist_extra_id;
        }


    }
}
