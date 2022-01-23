using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_NPC_AI_Parameter
    {
        L2H_NPC npc;
        string parameter_value;
        string name;
        public bool isEnabled;

        public L2H_NPC_AI_Parameter(L2H_NPC npc, string name, string value)
        {
            this.npc = npc;
            this.name = name;
            parameter_value = value;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                npc.server_Npcdata.npc_ai_values[npc.server_Npcdata.npc_ai_variables.FindIndex(x => x == Name)] = value;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return npc.server_Npcdata.npc_ai_variables.Exists(x=>x == Name);// isEnabled;
            }
            set
            {
                isEnabled = value;

                if (!isEnabled)
                {
                    if (npc.server_Npcdata.npc_ai_variables.Contains(Name))
                    {
                        int indexToRemove = npc.server_Npcdata.npc_ai_variables.FindIndex(x => x == Name);

                        npc.server_Npcdata.npc_ai_variables.RemoveAt(indexToRemove);
                        npc.server_Npcdata.npc_ai_values.RemoveAt(indexToRemove);
                    }
                }
                else
                {
                    npc.server_Npcdata.npc_ai_variables.Add(Name);
                    npc.server_Npcdata.npc_ai_values.Add(Parameter_Value);
                }
            }
        }

        public string Parameter_Value
        {
            get
            {
                if (npc.server_Npcdata.npc_ai_variables.Exists(x => x == Name))
                    if (npc.server_Npcdata.npc_ai_variables.Count == npc.server_Npcdata.npc_ai_values.Count)
                        return npc.server_Npcdata.npc_ai_values[npc.server_Npcdata.npc_ai_variables.FindIndex(x => x == Name)];
                    else
                        return parameter_value;
                else
                    return parameter_value;
            }
            set
            {
                parameter_value = value;
                npc.server_Npcdata.npc_ai_values[npc.server_Npcdata.npc_ai_variables.FindIndex(x => x == Name)] = value;
            }
        }
    }
}
