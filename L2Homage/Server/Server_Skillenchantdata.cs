namespace L2Homage
{
    public class Server_Skillenchantdata
    {
        string enchant_skill_begin = "enchant_skill_begin";

        public string original_skill_id = "";

        public string original_skill;
        string original_skill_textstart = "original_skill = [";
        string original_skill_textend = "]";

        public string route_id;
        string route_id_textstart = "route_id = ";
        string route_id_textend = "";

        public string enchant_id;
        string enchant_id_textstart = "enchant_id = ";
        string enchant_id_textend = "";

        public string skill_level;
        string skill_level_textstart = "skill_level = ";
        string skill_level_textend = "";

        public string importance;
        string importance_textstart = "importance = ";
        string importance_textend = "";

        public string required_item;
        string required_item_textstart = "required_item = ";
        string required_item_textend = "";

        string enchant_skill_end = "enchant_skill_end";

        public Server_Skillenchantdata(string dataline)
        {
            string[] splitDataline = dataline.Split('\t');

            original_skill = StripExcessServerText(original_skill_textstart, splitDataline[1], original_skill_textend);
            route_id = StripExcessServerText(route_id_textstart, splitDataline[2], route_id_textend);
            enchant_id = StripExcessServerText(enchant_id_textstart, splitDataline[3], enchant_id_textend);
            skill_level = StripExcessServerText(skill_level_textstart, splitDataline[4], skill_level_textend);
            importance = StripExcessServerText(importance_textstart, splitDataline[5], importance_textend);
            required_item = StripExcessServerText(required_item_textstart, splitDataline[6], required_item_textend);
        }

        private string StripExcessServerText(string startText, string variable, string endText)
        {

            string returnString = variable;
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

            exportString += enchant_skill_begin + "\t" +
                            ConvertToServerText(original_skill_textstart, original_skill, original_skill_textend) + "\t" +
                            ConvertToServerText(route_id_textstart, route_id, route_id_textend) + "\t" +
                            ConvertToServerText(enchant_id_textstart, enchant_id, enchant_id_textend) + "\t" +
                            ConvertToServerText(skill_level_textstart, skill_level, skill_level_textend) + "\t" +
                            ConvertToServerText(importance_textstart, importance, importance_textend) + "\t" +
                            ConvertToServerText(required_item_textstart, required_item, required_item_textend) + "\t" +
                            enchant_skill_end;


            return exportString;
        }
    }
}
