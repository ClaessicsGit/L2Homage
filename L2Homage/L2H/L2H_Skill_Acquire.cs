using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Skill_Acquire
    {
        public Server_Skillacquire server_Skillacquire { get; set; }
        public L2H_Skill L2H_Skill { get; set; }
        public L2H_Character_Class L2H_Character_Class { get; set; }
        public bool IsSelected { get; set; }
        public L2H_Skill_Acquire Instance { get { return this; } }


        public override string ToString()
        {

            if (L2H_Skill != null)
                if (L2H_Skill.client_Skillname != null)
                    return "lvl " + server_Skillacquire.get_lv + " - " + L2H_Skill.client_Skillname.name;
                else
                    return "lvl " + server_Skillacquire.get_lv + " - " + server_Skillacquire.skill_name;
            else
                return "lvl " + server_Skillacquire.get_lv + " - " + server_Skillacquire.skill_name;
        }

        public string Skill_Name_ID
        {
            get
            {
                return server_Skillacquire.skill_name;
            }
        }

        public System.Windows.Media.Imaging.BitmapImage Get_Skill_Image
        {
            get
            {
                if (L2H_Skill != null)
                    if (L2H_Skill.client_Skill != null)
                        return L2H_Parser.GetSkillImage(L2H_Skill.client_Skill.icon_name);
                    else
                        return new System.Windows.Media.Imaging.BitmapImage(new Uri("/Images/ImageNotFound.png", UriKind.Relative));
                else
                    return new System.Windows.Media.Imaging.BitmapImage(new Uri("/Images/ImageNotFound.png", UriKind.Relative));
            }
        }

        public string Required_Level
        {
            get
            {
                if (!string.IsNullOrEmpty(server_Skillacquire.get_lv))
                    return server_Skillacquire.get_lv;
                else
                    return "1";
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    server_Skillacquire.get_lv = "1";
                else
                    server_Skillacquire.get_lv = value;
            }
        }
        public string SP_Cost
        {
            get
            {
                if (!string.IsNullOrEmpty(server_Skillacquire.lv_up_sp))
                    return server_Skillacquire.lv_up_sp;
                else
                    return "0";
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    server_Skillacquire.lv_up_sp = "0";
                else
                    server_Skillacquire.lv_up_sp = value;
            }
        }
        public bool Auto_Get
        {
            get
            {
                if (server_Skillacquire.autoget == "true")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    server_Skillacquire.autoget = "true";
                else
                    server_Skillacquire.autoget = "false";
            }
        }
        public string Item_Needed
        {
            get
            {
                if (!string.IsNullOrEmpty(server_Skillacquire.item_needed))
                    return server_Skillacquire.item_needed;
                else
                    return "";
            }
            set
            {
                server_Skillacquire.item_needed = value;
            }
        }
        public string Quest_Needed
        {
            get
            {
                if (!string.IsNullOrEmpty(server_Skillacquire.quest_needed))
                    return server_Skillacquire.quest_needed;
                else
                    return "";
            }
            set
            {
                server_Skillacquire.quest_needed = value;
            }
        }
        public string Prerequisite_Skill
        {
            get
            {
                if (!string.IsNullOrEmpty(server_Skillacquire.prerequisite_skill))
                    return server_Skillacquire.prerequisite_skill;
                else
                    return "";
            }
            set
            {
                server_Skillacquire.prerequisite_skill = value;
            }
        }
        public string Social_Type
        {
            get
            {
                if (!string.IsNullOrEmpty(server_Skillacquire.social_class))
                    return server_Skillacquire.social_class;
                else
                    return "";
            }
            set
            {
                server_Skillacquire.social_class = value;
            }
        }
        public string Pledge_Type
        {
            get
            {
                if (!string.IsNullOrEmpty(server_Skillacquire.pledge_type))
                    return server_Skillacquire.pledge_type;
                else
                    return "";
            }
            set
            {
                server_Skillacquire.pledge_type = value;
            }
        }
        public string Race
        {
            get
            {
                if (!string.IsNullOrEmpty(server_Skillacquire.race))
                    return server_Skillacquire.race;
                else
                    return "";
            }
            set
            {
                server_Skillacquire.race = value;
            }
        }


    }
}
