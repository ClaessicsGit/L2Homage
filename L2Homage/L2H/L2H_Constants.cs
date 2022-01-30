using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace L2Homage
{
    public enum Popup_Choice_Selection { item_type, etcitem_type, consume_type, default_action, condition_equip, condition_situation, condition_use }
    public enum Set_Slot_Type { head_A, head_B, chest, legs_A, legs_B, gloves_A, gloves_B, feet_A, feet_B, additional_A, additional_B }
    public static class L2H_Constants
    {
        //L2H
        public static string Color_Log_Thread_Begin = "#c4621b";
        public static string Color_Add = "#308700";
        public static string Color_Category = "#c4621b";
        public static string Color_Parameter_Name = "#c4c4c4";
        public static string Color_Remove = "#d61c1c";
        public static string Color_Multi_Droplist = "#00a000";
        public static string Color_Single_Droplist = "#a0a000";
        public static string Color_Modify = "#f0c800";
        public static string Color_Worldmap_Edges = "#4f4f4f";
        public static string Color_Worldmap_Edge_Highlight = "#ff0000";
        public static int Tasks_Per_Thread = 1000;
        public static int Multisells_Per_Thread = 10;
        public static string L2H_Settings_Path = "Data\\L2H\\L2H_Settings.txt";

        //Server Paths
        public static string server_Itemdata_Path = "Data\\Server\\itemdata.txt";
        public static string server_Recipes_Path = "Data\\Server\\recipe.txt";
        public static string server_Item_PCH_Path = "Data\\server\\item_pch.txt";
        public static string server_NPCdata_Path = "Data\\server\\npcdata.txt";
        public static string server_NpcposPath = "Data\\server\\npcpos.txt";
        public static string server_NPC_PCH_Path = "Data\\server\\npc_pch.txt";
        public static string server_MultisellPath = "Data\\server\\multisell.txt";
        public static string server_Skilldata_Path = "Data\\server\\skilldata.txt";
        public static string server_Skill_PCH_Path = "Data\\server\\skill_pch.txt";
        public static string server_Skill_PCH2_Path = "Data\\server\\skill_pch2.txt";
        public static string server_Skillacquire_Path = "Data\\server\\skillacquire.txt";
        public static string server_SkillenchantcostPath = "Data\\server\\skillenchantcost.txt";
        public static string server_SkillenchantdataPath = "Data\\server\\skillenchantdata.txt";
        public static string server_ServerSettingPath = "Data\\server\\setting.txt";
        public static string server_PC_parametersPath = "Data\\server\\PC_parameter.txt";
        public static string server_ai_classesPath = "Data\\AI\\classes.txt";
        public static string server_expdata_Path = "Data\\server\\expdata.txt";

        //Decrypted Client Paths
        public static string client_Decrypted_Folder_Path = "Data\\Decrypted Client Files";
        public static string client_Weapons_Path = "Data\\Decrypted Client Files\\weapongrp.txt";
        public static string client_Etcs_Path = "Data\\decrypted client files\\EtcItemgrp.txt";
        public static string client_Armors_Path = "Data\\decrypted client files\\Armorgrp.txt";
        public static string client_Itemnames_Path = "Data\\Decrypted Client Files\\ItemName-e.txt";
        public static string client_NPCs_Path = "Data\\decrypted client files\\Npcgrp.txt";
        public static string client_NPCnames_Path = "Data\\decrypted client files\\NpcName-e.txt";
        public static string client_NPCstrings_Path = "Data\\decrypted client files\\npcstring-e.txt";
        public static string client_L2ini_Path = "Data\\decrypted client files\\l2.ini";
        public static string client_Recipes_Path = "Data\\decrypted client files\\Recipe-c.txt";
        public static string client_Skills_Path = "Data\\decrypted client files\\skillgrp.txt";
        public static string client_Skillnames_Path = "Data\\decrypted client files\\skillname-e.txt";
        public static string client_Skillsounds_Path = "Data\\decrypted client files\\skillsoundgrp.txt";
        public static string client_Mobskillanimgrp_Path = "Data\\decrypted client files\\mobskillanimgrp.txt";
        public static string client_Charcreategrp_Path = "Data\\decrypted client files\\charcreategrp.txt";
        public static string client_Classinfo_Path = "Data\\decrypted client files\\classinfo-e.txt";
        public static string client_Eula_Path = "Data\\decrypted client files\\eula-e.txt";
        public static string client_Gametip_Path = "Data\\decrypted client files\\gametip-e.txt";
        public static string client_Huntingzone_Path = "Data\\decrypted client files\\huntingzone-e.txt";
        public static string client_Instantzonedata_Path = "Data\\decrypted client files\\instantzonedata-e.txt";
        public static string client_Obscene_Path = "Data\\decrypted client files\\obscene-e.txt";
        public static string client_Questname_Path = "Data\\decrypted client files\\questname-e.txt";
        public static string client_Raiddata_Path = "Data\\decrypted client files\\raiddata-e.txt";
        public static string client_Sysstring_Path = "Data\\decrypted client files\\sysstring-e.txt";
        public static string client_Systemmsg_Path = "Data\\decrypted client files\\systemmsg-e.txt";
        public static string client_Zonename_Path = "Data\\decrypted client files\\zonename-e.txt";


        //Export Client Paths
        public static string export_Client_Folder_Path = "Export\\Client";
        public static string export_Client_Itemnames_Path = "Export\\client\\itemnames-e.dat";
        public static string export_Client_Weapons_Path = "Export\\client\\weapongrp.dat";
        public static string export_Client_Armors_Path = "Export\\client\\armorgrp.dat";
        public static string export_Client_Etcs_Path = "Export\\client\\etcitemgrp.dat";
        public static string export_Client_Mobskillanimgrp_Path = "Export\\client\\mobskillanimgrp.dat";
        public static string export_Client_NPCs_Path = "Export\\client\\npcgrp.dat";
        public static string export_Client_NPCnames_Path = "Export\\client\\npcname-e.dat";
        public static string export_client_NPCstrings_Path = "Export\\client\\npcstring-e.dat";
        public static string export_Client_Recipes_Path = "Export\\client\\recipe-c.dat";
        public static string export_Client_Skills_Path = "Export\\client\\skillgrp.dat";
        public static string export_Client_Skillnames_Path = "Export\\client\\skillname-e.dat";
        public static string export_Client_Skillsounds_Path = "Export\\client\\skillsoundgrp.dat";


        //Export Server Paths
        public static string export_Server_Folder_Path = "Export\\Server";
        public static string export_Server_NPCdata_Diablomized_Path = "Export\\Server\\npcdata_diablomized.txt";
        public static string export_Server_NPCdata_Path = "Export\\Server\\npcdata.txt";
        public static string export_Server_NPC_PCH_Path = "Export\\Server\\npc_pch.txt";
        public static string export_Server_NPCpos_Path = "Export\\Server\\npcpos.txt";
        public static string export_Server_Item_PCH_Path = "Export\\Server\\item_pch.txt";
        public static string export_Server_Itemdata_Path = "Export\\Server\\itemdata.txt";
        public static string export_Server_Recipes_Path = "Export\\server\\recipe.txt";
        public static string export_Server_Multisell_Path = "Export\\server\\multisell.txt";
        public static string export_Server_Skilldata_Path = "Export\\server\\skilldata.txt";
        public static string export_Server_Skill_PCH_Path = "Export\\server\\skill_pch.txt";
        public static string export_Server_Skill_PCH2_Path = "Export\\server\\skill_pch2.txt";
        public static string export_Server_Skill_Acquire_Path = "Export\\server\\skillacquire.txt";
        public static string export_Server_Skill_Enchant_Cost_Path = "Export\\server\\skillenchantcost.txt";
        public static string export_Server_Skill_Enchant_Data_Path = "Export\\server\\skillenchantdata.txt";
        public static string export_Server_Setting_Path = "Export\\server\\setting.txt";
        public static string export_Server_PC_Parameter_Path = "Export\\server\\PC_parameter.txt";
        public static string export_Server_Expdata_Path = "Export\\server\\expdata.txt";

        //Tools
        public static string encdec_Tool_Path = "Tools\\mxencdec\\mxencdec.exe";
        public static string encdec_Client_Itemnames_Path = "Tools\\mxencdec\\itemnames-e.dat";
        public static string encdec_Client_Weapons_Path = "Tools\\mxencdec\\weapongrp.dat";
        public static string encdec_Client_Armors_Path = "Tools\\mxencdec\\armorgrp.dat";
        public static string encdec_Client_Etcs_Path = "Tools\\mxencdec\\etcitemgrp.dat";
        public static string encdec_Client_Mobskillanimgrp_Path = "Tools\\mxencdec\\mobskillanimgrp.dat";
        public static string encdec_Client_NPCs_Path = "Tools\\mxencdec\\npcgrp.dat";
        public static string encdec_Client_NPCnames_Path = "Tools\\mxencdec\\npcname-e.dat";
        public static string encdec_Client_Recipes_Path = "Tools\\mxencdec\\recipe-c.dat";
        public static string encdec_Client_Skills_Path = "Tools\\mxencdec\\skillgrp.dat";
        public static string encdec_Client_Skillnames_Path = "Tools\\mxencdec\\skillname-e.dat";
        public static string encdec_Client_Skillsounds_Path = "Tools\\mxencdec\\skillsoundgrp.dat";


        //Custom Content
        public static string L2H_Single_Droplists_Path = "Data\\L2H\\single_droplists.txt";
        public static string L2H_Multi_Droplists_Path = "Data\\L2H\\multi_droplists.txt";
        public static string L2H_NPC_Droplists_Pointers_Path = "Data\\L2H\\npc_droplist_pointers.txt";
        public static string L2H_NPC_Template_Pointers_Path = "Data\\L2H\\npc_template_pointers.txt";
        public static string L2H_Custom_Exp_Table_Path = "Data\\L2H\\custom_expdata.txt";


        public static string L2H_Skills_Template_Pointers_Path = "Data\\L2H\\skills_template_pointers.txt";
        public static string L2H_Item_Template_Pointers_Path = "Data\\L2H\\item_template_pointers.txt";
        public static string L2H_Set_Template_Pointers_Path = "Data\\L2H\\set_template_pointers.txt";
        public static string original_npcpos_path = "Data\\L2H\\original_npcpos.txt";
        public static string custom_npcpos_path = "Data\\L2H\\custom_npcpos.txt";

        public static string logs_path = "Data\\Logs";

        public static List<Int64> OriginalExpTable = new List<Int64>
        {
            0,
            68,
            363,
            1168,
            2884,
            6038,
            11287,
            19423,
            31378,
            48229,
            71202,
            101677,
            141193,
            191454,
            254330,
            331867,
            426288,
            540000,
            675596,
            835862,
            1023784,
            1242546,
            1495543,
            1786379,
            2118876,
            2497077,
            2925250,
            3407897,
            3949754,
            4555796,
            5231246,
            5981576,
            6812513,
            7730044,
            8740422,
            9850166,
            11066072,
            12395215,
            13844951,
            15422929,
            17137087,
            18995665,
            21007203,
            23180550,
            25524868,
            28049635,
            30764654,
            33680052,
            36806289,
            40154162,
            45525133,
            51262490,
            57383988,
            63907911,
            70853089,
            80700831,
            91162654,
            102265881,
            114038596,
            126509653,
            146308200,
            167244337,
            189364894,
            212717908,
            237352644,
            271975263,
            308443198,
            346827154,
            387199547,
            429634523,
            474207979,
            532694979,
            606322775,
            696381369,
            804225364,
            931275828,
            1151275834,
            1511275834,
            2044287599,
            3075966164,
            4295351949,
            5766985062,
            7793077345,
            10235368963,
            13180481103,
            16890558729,
            21557174854,
            27195361799,
            34022739130,
            43058351257,
            53151614123,
            65302637571,
            80495367980,
            98790738178,
            120828895972,
            148356863187,
            181543472585,
            221588007243,
            269993856477,
            329163864405
        };

        public static string GetSelectionsTitle(Popup_Choice_Selection selection)
        {
            string returnString = "Select ";
            switch (selection)
            {
                case Popup_Choice_Selection.item_type:
                    returnString += "Item Type";
                    break;
                case Popup_Choice_Selection.etcitem_type:
                    returnString += "Etc Type";
                    break;
                case Popup_Choice_Selection.consume_type:
                    returnString += "Consume Type";
                    break;
                case Popup_Choice_Selection.default_action:
                    returnString += "Default Action";
                    break;
                default:
                    break;
            }

            return returnString;
        }

        public static List<string> GetSelectionsList(Popup_Choice_Selection selectionType)
        {
            List<string> returnList = new List<string>();
            switch (selectionType)
            {
                case Popup_Choice_Selection.item_type:
                    returnList.Add("weapon");
                    returnList.Add("none");
                    returnList.Add("etcitem");
                    returnList.Add("arrow");
                    returnList.Add("armor");
                    returnList.Add("asset");
                    returnList.Add("potion");
                    returnList.Add("accessary");
                    returnList.Add("questitem");
                    returnList.Add("scrl_enchant_wp");
                    returnList.Add("scrl_enchant_am");
                    returnList.Add("scroll");
                    returnList.Add("recipe");
                    returnList.Add("material");
                    returnList.Add("pet_collar");
                    returnList.Add("castle_guard");
                    returnList.Add("lotto");
                    returnList.Add("race_ticket");
                    returnList.Add("dye");
                    returnList.Add("seed");
                    returnList.Add("crop");
                    returnList.Add("maturecrop");
                    returnList.Add("harvest");
                    returnList.Add("seed2");
                    returnList.Add("ticket_of_lord");
                    returnList.Add("lure");
                    returnList.Add("bless_scrl_enchant_wp");
                    returnList.Add("bless_scrl_enchant_am");
                    returnList.Add("coupon");
                    returnList.Add("elixir");
                    returnList.Add("scrl_enchant_attr");
                    returnList.Add("bolt");
                    returnList.Add("scrl_inc_enchant_prop_wp");
                    returnList.Add("scrl_inc_enchant_prop_am");
                    returnList.Add("teleportbookmark");
                    returnList.Add("ancient_crystal_enchant_wp");
                    returnList.Add("ancient_crystal_enchant_am");
                    returnList.Add("rune_select");
                    returnList.Add("rune");

                    break;
                case Popup_Choice_Selection.etcitem_type:
                    returnList.Add("none");
                    returnList.Add("arrow");
                    returnList.Add("potion");
                    returnList.Add("scrl_enchant_wp");
                    returnList.Add("scrl_enchant_am");
                    returnList.Add("scroll");
                    returnList.Add("recipe");
                    returnList.Add("material");
                    returnList.Add("pet_collar");
                    returnList.Add("castle_guard");
                    returnList.Add("lotto");
                    returnList.Add("race_ticket");
                    returnList.Add("dye");
                    returnList.Add("seed");
                    returnList.Add("crop");
                    returnList.Add("maturecrop");
                    returnList.Add("harvest");
                    returnList.Add("seed2");
                    returnList.Add("ticket_of_lord");
                    returnList.Add("lure");
                    returnList.Add("bless_scrl_enchant_wp");
                    returnList.Add("bless_scrl_enchant_am");
                    returnList.Add("coupon");
                    returnList.Add("elixir");
                    returnList.Add("scrl_enchant_attr");
                    returnList.Add("bolt");
                    returnList.Add("scrl_inc_enchant_prop_wp");
                    returnList.Add("scrl_inc_enchant_prop_am");
                    returnList.Add("teleportbookmark");
                    returnList.Add("ancient_crystal_enchant_wp");
                    returnList.Add("ancient_crystal_enchant_am");
                    returnList.Add("rune_select");
                    returnList.Add("rune");
                    break;
                case Popup_Choice_Selection.consume_type:
                    returnList.Add("consume_type_normal");
                    returnList.Add("consume_type_stackable");
                    returnList.Add("consume_type_asset");
                    break;
                case Popup_Choice_Selection.default_action:
                    returnList.Add("action_equip");
                    returnList.Add("action_peel");
                    returnList.Add("action_none");
                    returnList.Add("action_skill_reduce");
                    returnList.Add("action_soulshot");
                    returnList.Add("action_recipe");
                    returnList.Add("action_skill_maintain");
                    returnList.Add("action_spiritshot");
                    returnList.Add("action_dice");
                    returnList.Add("action_calc");
                    returnList.Add("action_seed");
                    returnList.Add("action_harvest");
                    returnList.Add("action_capsule");
                    returnList.Add("action_xmas_open");
                    returnList.Add("action_show_html");
                    returnList.Add("action_show_ssq_status");
                    returnList.Add("action_fishingshot");
                    returnList.Add("action_summon_soulshot");
                    returnList.Add("action_summon_spiritshot");
                    returnList.Add("action_call_skill");
                    returnList.Add("action_show_adventurer_guide_book");
                    returnList.Add("action_keep_exp");
                    returnList.Add("action_create_mpcc");
                    returnList.Add("action_nick_color");
                    returnList.Add("action_hide_name");
                    returnList.Add("action_start_quest");
                    break;

                case Popup_Choice_Selection.condition_equip:
                    break;
                case Popup_Choice_Selection.condition_situation:
                    break;
                case Popup_Choice_Selection.condition_use:
                    break;
                default:
                    break;
            }
            return returnList;
        }

        public static System.Collections.ObjectModel.ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> GetColorList()
        {
            System.Collections.ObjectModel.ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> ColorList = new System.Collections.ObjectModel.ObservableCollection<Xceed.Wpf.Toolkit.ColorItem>();
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF0000FF"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF799BB0"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF808080"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFD36656"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFFFC78A"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF00FFFF"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFE627E6"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF2E7256"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF007293"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF008C62"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFFF8000"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF6BC6FF"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFC0C0C0"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF00FF80"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFFF89D2"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFFDDD00"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFFF46A3"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF25CD97"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF1111FF"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFFF0080"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFFFFFFF"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFB2B2B2"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFCB3AEF"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF77C763"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF4080FF"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFE7438C"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFA0D7FE"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFA351D9"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF0080FF"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFFF00FF"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF489FF0"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFF5DE4C"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF729C3E"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFB2B05A"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF5742E8"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF80FF80"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF51E445"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF1C21F2"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF408000"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFD7D053"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF25D700"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF0000FB"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFF19696"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFFCFCFC"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FFF000FF"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF8080FF"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF3F3F9E"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF4F4FF9"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF44CFAA"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF22AAEE"), ""));
            ColorList.Add(new Xceed.Wpf.Toolkit.ColorItem((Color)ColorConverter.ConvertFromString("#FF6666FF"), ""));

            return ColorList;
        }

        /// <summary>
        /// This would be the most accurate way of determining if skill batch editing should use floats or ints. 
        /// If anyone has the mental strength to manually go through all these effects and define which parts use floats, be my guest.
        /// Until then, I'll just try to estimate which skills use floats and which skills don't.
        /// </summary>
        /// <param name="propertyID"></param>
        /// <param name="valueIndex"></param>
        /// <returns></returns>
        public static bool SkillPropertyValueInteger(string propertyID, int valueIndex)
        {
            switch (propertyID)
            {
                case "c_chameleon_rest":
                    {
                        break;
                    }//	;-2	;5
                case "c_fake_death":
                    {
                        break;
                    }//	;-10	;5
                case "c_hp":
                    {
                        break;
                    }//	;-12	;2
                case "c_mp":
                    {
                        break;
                    }//	;-9	;5
                case "c_mp_by_level":
                    {
                        if (valueIndex == 0)
                            return false;
                        break;
                    }//	;-0.4	;5
                case "c_rest":
                    {
                        break;
                    }//	;-1	;3
                case "cub_attack_speed":
                    {
                        break;
                    }//	;-23	;per
                case "cub_heal":
                    {
                        break;
                    }//	;201
                case "cub_hp":
                    {
                        break;
                    }//	;-41	;5	;diff
                case "cub_hp_drain":
                    {
                        break;
                    }//	;543	;40
                case "cub_m_attack":
                    {
                        break;
                    }//	;436
                case "cub_physical_attack":
                    {
                        break;
                    }//	;-23	;per
                case "cub_physical_defence":
                    {
                        break;
                    }//	;-23	;per
                case "i_abnormal_time_change":
                    {
                        break;
                    }//	;br_event_buf1	;300	;diff
                case "i_add_hate":
                    {
                        break;
                    }//	;-132
                case "i_add_max_entrance_inzone":
                    {
                        break;
                    }//	;1	;1
                case "i_align_direction":
                    {
                        break;
                    }//	;80
                case "i_backstab":
                    {
                        break;
                    }//	;1107	;400	;5
                case "i_betray":
                    {
                        break;
                    }//	;80	;30
                case "i_blink":
                    {
                        break;
                    }//	;_front	;500
                case "i_bookmark_add_slot":
                    {
                        break;
                    }//	;3
                case "i_call_pc":
                    {
                        break;
                    }//	;[crystal_of_summon]	;1
                case "i_change_face":
                    {
                        break;
                    }//	;0
                case "i_change_hair_color":
                    {
                        break;
                    }//	;0
                case "i_change_hair_style":
                    {
                        break;
                    }//	;0
                case "i_change_skill_level":
                    {
                        break;
                    }//	;5076	;-1
                case "i_collecting":
                    {
                        break;
                    }//	;1
                case "i_confuse":
                    {
                        break;
                    }//	;20	;20
                case "i_cp":
                    {
                        break;
                    }//	;-7	;per
                case "i_death":
                    {
                        break;
                    }//	;0	;5
                case "i_death_link":
                    {
                        break;
                    }//	;68
                case "i_defuse_trap":
                    {
                        break;
                    }//	;18
                case "i_delete_hate":
                    {
                        break;
                    }//	;40
                case "i_delete_hate_of_me":
                    {
                        break;
                    }//	;80
                case "i_despawn":
                    {
                        break;
                    }//	;100
                case "i_detect_trap":
                    {
                        break;
                    }//	;18
                case "i_dismount_for_event":
                    {
                        break;
                    }//	;[grown_up_wolf_event]
                case "i_dispel_by_category":
                    {
                        break;
                    }//	;slot_buff	;25	;5
                case "i_dispel_by_slot":
                    {
                        break;
                    }//	;poison	;3
                case "i_dispel_by_slot_myself":
                    {
                        break;
                    }//	;physical_stance
                case "i_dispel_by_slot_probability":
                    {
                        break;
                    }//	;attack_time_down	;40
                case "i_distrust":
                    {
                        break;
                    }//	;20	;20
                case "i_enchant_armor_rate":
                    {
                        break;
                    }//	;35
                case "i_enchant_attribute":
                    {
                        break;
                    }//	;ia_fire	;ia_water	;6
                case "i_enchant_weapon_rate":
                    {
                        break;
                    }//	;20
                case "i_energy_attack":
                    {
                        break;
                    }//	;918	;15	;0	;0	;0
                case "i_fatal_blow":
                    {
                        break;
                    }//	;73	;200	;0
                case "i_fishing_pumping":
                    {
                        break;
                    }//	;0.66
                case "i_fishing_reeling":
                    {
                        break;
                    }//	;0.66
                case "i_fishing_shot":
                    {
                        break;
                    }//	;100
                case "i_fly_away":
                    {
                        break;
                    }//	;curve	;push	;600
                case "i_focus_energy":
                    {
                        break;
                    }//	;1
                case "i_focus_max_energy":
                    {
                        break;
                    }//	;8
                case "i_focus_soul":
                    {
                        break;
                    }//	;1
                case "i_food_for_pet":
                    {
                        break;
                    }//	;100	;100	;0
                case "i_give_contribution":
                    {
                        break;
                    }//	;10
                case "i_heal":
                    {
                        break;
                    }//	;143
                case "i_hp":
                    {
                        break;
                    }//	;435	;diff
                case "i_hp_by_level_self":
                    {
                        break;
                    }//	;3
                case "i_hp_drain":
                    {
                        break;
                    }//	;20	;20
                case "i_hp_per_max":
                    {
                        break;
                    }//	;100
                case "i_hp_self":
                    {
                        break;
                    }//	;-11.98
                case "i_m_attack":
                    {
                        break;
                    }//	;47
                case "i_m_attack_by_abnormal":
                    {
                        break;
                    }//	;96
                case "i_m_attack_by_dist":
                    {
                        break;
                    }//	;52
                case "i_m_attack_by_range":
                    {
                        break;
                    }//	;100	;far
                case "i_m_attack_mp":
                    {
                        break;
                    }//	;16	;1	;1600
                case "i_m_attack_over_hit":
                    {
                        break;
                    }//	;128
                case "i_m_attack_range":
                    {
                        break;
                    }//	;112	;40
                case "i_m_soul_attack":
                    {
                        break;
                    }//	;69	;0
                case "i_mount_for_event":
                    {
                        break;
                    }//	;[grown_up_wolf_event]
                case "i_mp":
                    {
                        break;
                    }//	;36	;diff
                case "i_mp_by_level":
                    {
                        break;
                    }//	;49
                case "i_mp_by_level_self":
                    {
                        break;
                    }//	;1
                case "i_mp_per_max":
                    {
                        break;
                    }//	;100
                case "i_npc_kill":
                    {
                        break;
                    }//	;80
                case "i_p_attack":
                    {
                        break;
                    }//	;517	;15	;1	;0
                case "i_p_attack_by_dist":
                    {
                        break;
                    }//	;68	;0	;2	;0
                case "i_p_soul_attack":
                    {
                        break;
                    }//	;108	;0	;0	;0
                case "i_pcbang_point_up":
                    {
                        break;
                    }//	;7000
                case "i_physical_attack_hp_link":
                    {
                        break;
                    }//	;2908	;0	;0	;0
                case "i_pledge_send_system_message":
                    {
                        break;
                    }//	;1923
                case "i_randomize_hate":
                    {
                        break;
                    }//	;80
                case "i_real_damage":
                    {
                        break;
                    }//	;100	;15	;2
                case "i_refuel_airship":
                    {
                        break;
                    }//	;25
                case "i_restoration":
                    {
                        break;
                    }//	;[g_lucky_key]	;1
                case "i_restoration_random":
                    {
                        break;
                    }//	;{{{{[mithril_arrow]	;700}}	;30
                case "i_resurrection":
                    {
                        break;
                    }//	;70
                case "i_run_away":
                    {
                        break;
                    }//	;30	;20
                case "i_set_skill":
                    {
                        break;
                    }//	;755	;1
                case "i_skill_turning":
                    {
                        break;
                    }//	;1	;50
                case "i_soul_blow":
                    {
                        break;
                    }//	;1853	;200	;0
                case "i_soul_shot":
                    {
                        break;
                    }//	;100
                case "i_sp":
                    {
                        break;
                    }//	;500	;diff
                case "i_spirit_shot":
                    {
                        break;
                    }//	;100	;40	;1.0
                case "i_steal_abnormal":
                    {
                        break;
                    }//	;slot_buff	;3	;5
                case "i_summon":
                    {
                        break;
                    }//	;[siege_golem]	;100	;[gemstone_c]	;40	;1200
                case "i_summon_agathion":
                    {
                        break;
                    }//	;1	;1
                case "i_summon_cubic":
                    {
                        break;
                    }//	;1	;1
                case "i_summon_npc":
                    {
                        break;
                    }//	;[skill_defence_up]	;1
                case "i_summon_soul_shot":
                    {
                        break;
                    }//	;100
                case "i_summon_spirit_shot":
                    {
                        break;
                    }//	;100	;40	;1.0
                case "i_summon_trap":
                    {
                        break;
                    }//	;[explosion_trap1]
                case "i_target_cancel":
                    {
                        break;
                    }//	;80
                case "i_target_me":
                    {
                        break;
                    }//	;40
                case "i_teleport":
                    {
                        break;
                    }//	;146818	;25807	;-2008
                case "i_transfer_hate":
                    {
                        break;
                    }//	;80
                case "i_unlock":
                    {
                        break;
                    }//	;bypc	;30	;0	;0
                case "i_vp_up":
                    {
                        break;
                    }//	;10000
                case "p_2h_sword_bonus":
                    {
                        break;
                    }//	;7	;per	;2	;diff
                case "p_area_damage":
                    {
                        break;
                    }//	;-40	;per
                case "p_attack_attribute":
                    {
                        break;
                    }//	;attr_holy	;10
                case "p_attack_range":
                    {
                        break;
                    }//	;{bow}	;200	;diff
                case "p_attack_speed":
                    {
                        break;
                    }//	;{armor_none	;armor_light	;armor_heavy}	;-20	;per
                case "p_attack_speed_by_hp2":
                    {
                        break;
                    }//	;{all}	;13	;per
                case "p_attack_speed_by_weapon":
                    {
                        break;
                    }//	;{bow}	;8	;per
                case "p_attack_trait":
                    {
                        break;
                    }//	;trait_bug_weakness	;30
                case "p_avoid":
                    {
                        break;
                    }//	;{all}	;-2	;diff
                case "p_avoid_agro":
                    {
                        break;
                    }//	;70
                case "p_avoid_by_move_mode":
                    {
                        break;
                    }//	;run	;4	;diff
                case "p_avoid_rate_by_hp1":
                    {
                        break;
                    }//	;{all}	;15	;diff
                case "p_avoid_rate_by_hp2":
                    {
                        return false;
                        break;
                    }//	;{all}	;7.63	;diff
                case "p_avoid_skill":
                    {
                        break;
                    }//	;0	;40
                case "p_block_buff_slot":
                    {
                        break;
                    }//	;{improve_pa_pd_up	;pd_up}
                case "p_block_getdamage":
                    {
                        break;
                    }//	;block_hp
                case "p_breath":
                    {
                        break;
                    }//	;180	;diff
                case "p_change_fishing_mastery":
                    {
                        break;
                    }//	;8	;2.15
                case "p_counter_skill":
                    {
                        break;
                    }//	;0	;20
                case "p_cp_regen":
                    {
                        break;
                    }//	;{all}	;30	;per
                case "p_create_common_item":
                    {
                        break;
                    }//	;1
                case "p_create_item":
                    {
                        break;
                    }//	;1
                case "p_critical_damage":
                    {
                        break;
                    }//	;{dualfist}	;20	;per
                case "p_critical_damage_position":
                    {
                        break;
                    }//	;front	;-30	;per
                case "p_critical_rate":
                    {
                        break;
                    }//	;{all}	;20	;per
                case "p_critical_rate_by_hp2":
                    {
                        break;
                    }//	;{all}	;10	;diff
                case "p_critical_rate_position_bonus":
                    {
                        break;
                    }//	;front	;-30	;per
                case "p_crystal_grade_modify":
                    {
                        break;
                    }//	;3
                case "p_crystallize":
                    {
                        break;
                    }//	;d
                case "p_cubic_mastery":
                    {
                        break;
                    }//	;2
                case "p_damage_shield":
                    {
                        break;
                    }//	;10
                case "p_defence_attribute":
                    {
                        break;
                    }//	;attr_fire	;-2
                case "p_defence_critical_damage":
                    {
                        break;
                    }//	;{armor_heavy}	;-15	;per
                case "p_defence_critical_rate":
                    {
                        break;
                    }//	;{all}	;10	;per
                case "p_defence_trait":
                    {
                        break;
                    }//	;trait_hold	;100
                case "p_enlarge_abnormal_slot":
                    {
                        break;
                    }//	;1
                case "p_enlarge_storage":
                    {
                        break;
                    }//	;inventory_normal	;1
                case "p_exp_modify":
                    {
                        break;
                    }//	;30
                case "p_expand_deco_slot":
                    {
                        break;
                    }//	;1
                case "p_fatal_blow_rate":
                    {
                        break;
                    }//	;60	;per
                case "p_fishing_mastery":
                    {
                        break;
                    }//	;0.66
                case "p_heal_effect":
                    {
                        break;
                    }//	;{all}	;30	;per
                case "p_hit":
                    {
                        break;
                    }//	;{dualfist}	;3	;diff
                case "p_hit_at_night":
                    {
                        break;
                    }//	;3	;diff
                case "p_hit_number":
                    {
                        break;
                    }//	;5	;diff
                case "p_hp_regen":
                    {
                        break;
                    }//	;{all}	;30	;per
                case "p_hp_regen_by_move_mode":
                    {
                        break;
                    }//	;sit	;1.9	;diff
                case "p_limit_hp":
                    {
                        break;
                    }//	;30	;per
                case "p_limit_mp":
                    {
                        break;
                    }//	;30	;per
                case "p_limit_cp":
                    {
                        break;
                    }//	;30	;per
                case "p_magic_critical_rate":
                    {
                        break;
                    }//	;{all}	;100	;per
                case "p_magic_mp_cost":
                    {
                        break;
                    }//	;1	;-30	;per
                case "p_magic_speed":
                    {
                        break;
                    }//	;{armor_none	;armor_light	;armor_heavy}	;-50	;per
                case "p_magical_attack":
                    {
                        break;
                    }//	;{all}	;1.9	;diff
                case "p_magical_attack_add":
                    {
                        break;
                    }//	;{armor_sigil}	;5	;per
                case "p_magical_defence":
                    {
                        break;
                    }//	;{all}	;15	;per
                case "p_mana_charge":
                    {
                        break;
                    }//	;22
                case "p_max_cp":
                    {
                        break;
                    }//	;{all}	;10	;diff	;0
                case "p_max_hp":
                    {
                        break;
                    }//	;{all}	;10	;per	;1
                case "p_max_mp":
                    {
                        break;
                    }//	;{all}	;30	;diff	;0
                case "p_max_mp_add":
                    {
                        break;
                    }//	;{armor_sigil}	;5	;per
                case "p_mp_regen":
                    {
                        break;
                    }//	;{all}	;10	;per
                case "p_mp_regen_add":
                    {
                        break;
                    }//	;{armor_sigil}	;17	;per
                case "p_mp_regen_by_move_mode":
                    {
                        break;
                    }//	;sit	;1.9	;diff
                case "p_mp_vampiric_attack":
                    {
                        break;
                    }//	;10
                case "p_physical_attack":
                    {
                        break;
                    }//	;{all}	;-5	;per
                case "p_physical_attack_by_hp1":
                    {
                        break;
                    }//	;{all}	;32.9	;diff
                case "p_physical_defence":
                    {
                        break;
                    }//	;{all}	;-5	;per
                case "p_physical_defence_by_hp1":
                    {
                        break;
                    }//	;{all}	;116.875	;diff
                case "p_physical_shield_defence":
                    {
                        break;
                    }//	;1	;per
                case "p_pvp_magical_skill_defence_bonus":
                    {
                        break;
                    }//	;30	;per
                case "p_pvp_magical_skill_dmg_bonus":
                    {
                        break;
                    }//	;-50	;per
                case "p_pvp_physical_attack_dmg_bonus":
                    {
                        break;
                    }//	;5	;per
                case "p_recovery_vp":
                    {
                        break;
                    }//	;1
                case "p_reduce_cancel":
                    {
                        break;
                    }//	;-40	;diff
                case "p_reduce_drop_penalty":
                    {
                        break;
                    }//	;pk	;-30	;-30
                case "p_reflect_dd":
                    {
                        break;
                    }//	;20
                case "p_reflect_skill":
                    {
                        break;
                    }//	;30	;30	;0	;30	;0	;0
                case "p_remove_equip_penalty":
                    {
                        break;
                    }//	;a
                case "p_resist_abnormal_by_category":
                    {
                        break;
                    }//	;speed_down	;-30	;per
                case "p_resist_dd_magic":
                    {
                        break;
                    }//	;5	;per
                case "p_resist_dispel_by_category":
                    {
                        break;
                    }//	;slot_buff	;-80	;per
                case "p_resurrection_special":
                    {
                        break;
                    }//	;100	;100
                case "p_reuse_delay":
                    {
                        break;
                    }//	;1	;-10	;per
                case "p_safe_fall_height":
                    {
                        break;
                    }//	;60	;diff
                case "p_shield_defence_rate":
                    {
                        break;
                    }//	;50
                case "p_skill_critical":
                    {
                        break;
                    }//	;str
                case "p_skill_power":
                    {
                        break;
                    }//	;30	;per
                case "p_soul_eating":
                    {
                        break;
                    }//	;73	;5
                case "p_sp_modify":
                    {
                        break;
                    }//	;30
                case "p_speed":
                    {
                        break;
                    }//	;{all}	;40	;diff
                case "p_stat_up":
                    {
                        break;
                    }//	;1	;1
                case "p_transfer_damage_pc":
                    {
                        break;
                    }//	;90
                case "p_transfer_damage_summon":
                    {
                        break;
                    }//	;50
                case "p_transform":
                    {
                        break;
                    }//	;251
                case "p_trigger_skill_by_attack":
                    {
                        break;
                    }//	;{enemy_all	;1	;100}	;1	;{1	;50	;diff}	;[s_attack_trigger_blunt_mastery1]	;self	;{sword	;blunt}
                case "p_trigger_skill_by_avoid":
                    {
                        break;
                    }//	;10	;[s_trigger_evasion_counter1]	;target
                case "p_trigger_skill_by_dmg":
                    {
                        break;
                    }//	;{pk	;1	;100
                case "p_trigger_skill_by_skill":
                    {
                        break;
                    }//	;997	;33	;[s_trigger_dwarf_crt_bouns1]	;self
                case "p_vampiric_attack":
                    {
                        break;
                    }//	;9
                case "p_weight_limit":
                    {
                        break;
                    }//	;100	;per
                case "p_weight_penalty":
                    {
                        break;
                    }//	;3000
                case "t_hp":
                    {
                        break;
                    }//	;-50	;5	;diff
                case "t_hp_fatal":
                    {
                        break;
                    }//	;-20	;5	;diff
                case "t_mp":
                    {
                        break;
                    }//	;10	;5	;diff
                default:
                    break;
            }
            return true;
        }
    }
}
