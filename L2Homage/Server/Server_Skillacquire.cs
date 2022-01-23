using System.Collections.Generic;

namespace L2Homage
{
    public class Server_Skillacquire
    {
        public string class_begin;
        public string includeClass;

        string skill_begin = "skill_begin";

        string skill_name_textstart = "skill_name = [";
        public string skill_name;
        string skill_name_textend = "]";

        public string get_lv;
        string get_lv_textstart = "get_lv = ";
        string get_lv_textend = "";

        public string lv_up_sp;
        string lv_up_sp_textstart = "lv_up_sp = ";
        string lv_up_sp_textend = "";

        public string autoget;
        string autoget_textstart = "auto_get = ";
        string autoget_textend = "";

        public List<string> item_needed_id;
        public List<string> item_needed_amount;

        public string item_needed;
        string item_needed_textstart = "item_needed = ";
        string item_needed_textend = "";

        public string social_class = "";
        public string social_class_textstart = "social_class = ";
        public string social_class_textend = "";

        public string pledge_type = "";
        public string pledge_type_textstart = "pledge_type = [";
        public string pledge_type_textend = "]";

        public string race = "";
        public string race_textstart = "race = ";
        public string race_textend = "";

        public string prerequisite_skill = "";
        public string prerequisite_skill_textstart = "prerequisite_skill = ";
        public string prerequisite_skill_textend = "";

        public string quest_needed = "";
        public string quest_needed_textstart = "quest_needed = ";
        public string quest_needed_textend = "";

        string skill_end = "skill_end";

        public Server_Skillacquire() //Empty for adding through popup
        {

        }
        public Server_Skillacquire(string[] splitDataline, string activeClass, string includeClass)
        {
            class_begin = activeClass;
            this.includeClass = includeClass;
            skill_begin = splitDataline[0];

            int skipLines = 0;
            if (splitDataline[1].Contains("/*"))
                skipLines++;

            skill_name = StripExcessServerText(skill_name_textstart, splitDataline[1 + skipLines], skill_name_textend);
            get_lv = StripExcessServerText(get_lv_textstart, splitDataline[2 + skipLines], get_lv_textend);
            lv_up_sp = StripExcessServerText(lv_up_sp_textstart, splitDataline[3 + skipLines], lv_up_sp_textend);
            autoget = StripExcessServerText(autoget_textstart, splitDataline[4 + skipLines], autoget_textend);
            item_needed = StripExcessServerText(item_needed_textstart, splitDataline[5 + skipLines], item_needed_textend);
            if (string.IsNullOrEmpty(item_needed))
                item_needed = "{}";

            for (int i = 6 + skipLines; i < splitDataline.Length; i++)
            {
                FindAndInitializeVariable(splitDataline[i]);
            }

        }

        private string StripExcessServerText(string startText, string variable, string endText)
        {
            string returnString = "";
            returnString = variable;
            returnString = returnString.Replace(startText, "");
            if (endText.Length > 0)
                returnString = returnString.Replace(endText, "");

            return returnString;
        }

        private string ConvertToServerText(string startText, string variable, string endText)
        {
            return startText + variable + endText;
        }

        public string GetExportString()
        {
            string exportString = "";

            exportString += skill_begin + "\t" +
                            ConvertToServerText(skill_name_textstart, skill_name, skill_name_textend) + "\t" +
                            ConvertToServerText(get_lv_textstart, get_lv, get_lv_textend) + "\t" +
                            ConvertToServerText(lv_up_sp_textstart, lv_up_sp, lv_up_sp_textend) + "\t" +
                            ConvertToServerText(autoget_textstart, autoget, autoget_textend) + "\t" +
                            ConvertToServerText(item_needed_textstart, item_needed, item_needed_textend) + "\t";
            if (!string.IsNullOrEmpty(social_class))
                exportString += ConvertToServerText(social_class_textstart, social_class, social_class_textend) + "\t";
            if (!string.IsNullOrEmpty(pledge_type))
                exportString += ConvertToServerText(pledge_type_textstart, pledge_type, pledge_type_textend) + "\t";
            if (!string.IsNullOrEmpty(race))
                exportString += ConvertToServerText(race_textstart, race, race_textend) + "\t";
            if (!string.IsNullOrEmpty(prerequisite_skill))
                exportString += ConvertToServerText(prerequisite_skill_textstart, prerequisite_skill, prerequisite_skill_textend) + "\t";
            if (!string.IsNullOrEmpty(quest_needed))
                exportString += ConvertToServerText(quest_needed_textstart, quest_needed, quest_needed_textend) + "\t";

            exportString += skill_end;

            return exportString;
        }

        private void FindAndInitializeVariable(string variableName)
        {

            if (variableName == "skill_end")
                return;

            if (string.IsNullOrEmpty(variableName))
                return;

            if (variableName.Contains("social_class"))
                social_class = StripExcessServerText(social_class_textstart, variableName, social_class_textend);
            if (variableName.Contains("pledge_type"))
                pledge_type = StripExcessServerText(pledge_type_textstart, variableName, pledge_type_textend);
            if (variableName.Contains("race"))
                race = StripExcessServerText(race_textstart, variableName, race_textend);
            if (variableName.Contains("prerequisite_skill"))
                prerequisite_skill = StripExcessServerText(prerequisite_skill_textstart, variableName, prerequisite_skill_textend);
            if (variableName.Contains("quest_needed"))
                quest_needed = StripExcessServerText(quest_needed_textstart, variableName, quest_needed_textend);


        }

    }
}
