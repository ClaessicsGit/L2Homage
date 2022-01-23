using System.Collections.Generic;
using System.Linq;

namespace L2Homage
{
    public class Server_Skillenchantcost
    {
        public string skillID;
        string skillID_textstart = "skill=";
        public string levels;
        string levels_textstart = "levels=";

        string levelID_textstart = "level=";
        public List<string> levelIDs;
        string adenaCost_textstart = "adena=";
        public List<string> adenaCosts;
        string spCost_textstart = "sp=";
        public List<string> spCosts;

        public Server_Skillenchantcost(string skillID, string levels, string[] levelIDs, string[] adenaCosts, string[] spCosts)
        {
            this.skillID = skillID;
            this.levels = levels;
            this.levelIDs = levelIDs.ToList();
            this.adenaCosts = adenaCosts.ToList();
            this.spCosts = spCosts.ToList();
        }

        private string ConvertToServerText(string startText, string variable, string endText)
        {
            return startText + variable + endText;
        }

        public string GetExportString()
        {
            string exportString = "";

            exportString += ConvertToServerText(skillID_textstart, skillID, "") + " " +
                            ConvertToServerText(levels_textstart, levels, "") + "\n";

            for (int i = 0; i < levelIDs.Count; i++)
            {
                exportString += ConvertToServerText(levelID_textstart, levelIDs[i], "") + " " +
                                ConvertToServerText(adenaCost_textstart, adenaCosts[i], "") + " " +
                                ConvertToServerText(spCost_textstart, spCosts[i], "");

                if (i != levelIDs.Count - 1)
                {
                    exportString += "\n";
                }
            }

            return exportString;
        }

    }
}
