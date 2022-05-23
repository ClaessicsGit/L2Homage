using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_NPC : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public Client_Npc client_Npc { get; set; }
        public Client_Npcname client_Npcname { get; set; }
        public Server_Npcdata server_Npcdata { get; set; }
        public L2H_Template_Pointer template { get; set; }
        public bool IsSelected { get; set; }
        public bool IsSelectedTemp { get; set; }
        public bool IsCustom { get; set; }

        public L2H_NPC Instance { get { return this; } }

        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override string ToString()
        {
            return client_Npcname.name + "\n(ID:" + ID + " Lv:" + server_Npcdata.level + ")";
        }

        public System.Windows.Media.Imaging.BitmapImage GetNPCImage()
        {
            if (template != null)
                return L2H_Parser.GetNPCImage(template.templateId);
            else
                return L2H_Parser.GetNPCImage(ID.ToString());
        }

        #region Properties
        public string NPC_ID
        {
            get
            {
                return ID.ToString();
            }
        }
        public string NPC_Template
        {
            get
            {
                if (template != null)
                    return template.templateId;
                else
                    return "";
            }
        }
        public string NPC_Name_ID
        {
            get
            {
                return server_Npcdata.npcName;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Name ID", server_Npcdata.npcName, value);
                server_Npcdata.npcName = value;
            }
        }
        public string NPC_Name
        {
            get
            {
                return client_Npcname.name;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Name", client_Npcname.name, value);
                client_Npcname.name = value;
            }
        }
        public string NPC_Category
        {
            get
            {
                return server_Npcdata.category;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Category", server_Npcdata.category, value);
                server_Npcdata.category = value;
            }
        }
        public string NPC_Description
        {
            get
            {
                return client_Npcname.description;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Description", client_Npcname.description, value);
                client_Npcname.description = value;
            }
        }
        public string NPC_Equipment_Chest
        {
            get
            {
                return server_Npcdata.slot_chest;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Chest Slot", server_Npcdata.slot_chest, value);
                server_Npcdata.slot_chest = value;
            }
        }
        public string NPC_Equipment_Rhand
        {
            get
            {
                return server_Npcdata.slot_rhand;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Right Hand Slot", server_Npcdata.slot_rhand, value);
                server_Npcdata.slot_rhand = value;
            }
        }
        public string NPC_Equipment_Lhand
        {
            get
            {
                return server_Npcdata.slot_lhand;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Left Hand Slot", server_Npcdata.slot_lhand, value);
                server_Npcdata.slot_lhand = value;
            }
        }
        public string NPC_Stats_STR
        {
            get
            {
                return server_Npcdata.strength;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "STR", server_Npcdata.strength, value);
                server_Npcdata.strength = value;
            }
        }
        public string NPC_Stats_INT
        {
            get
            {
                return server_Npcdata.intelligence;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "INT", server_Npcdata.intelligence, value);
                server_Npcdata.intelligence = value;
            }
        }
        public string NPC_Stats_DEX
        {
            get
            {
                return server_Npcdata.dexterity;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "DEX", server_Npcdata.dexterity, value);
                server_Npcdata.dexterity = value;
            }
        }
        public string NPC_Stats_WIT
        {
            get
            {
                return server_Npcdata.wit;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "WIT", server_Npcdata.wit, value);
                server_Npcdata.wit = value;
            }
        }
        public string NPC_Stats_CON
        {
            get
            {
                return server_Npcdata.con;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "CON", server_Npcdata.con, value);
                server_Npcdata.con = value;
            }
        }
        public string NPC_Stats_MEN
        {
            get
            {
                return server_Npcdata.men;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "MEN", server_Npcdata.men, value);
                server_Npcdata.men = value;
            }
        }
        public string NPC_Stats_HP
        {
            get
            {
                return server_Npcdata.org_hp;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Base HP", server_Npcdata.org_hp_regen, value);
                server_Npcdata.org_hp = value;
            }
        }
        public string NPC_Stats_HP_Regen
        {
            get
            {
                return server_Npcdata.org_hp_regen;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Base HP Regen", server_Npcdata.org_hp_regen, value);
                server_Npcdata.org_hp_regen = value;
            }
        }
        public string NPC_Stats_MP
        {
            get
            {
                return server_Npcdata.org_mp;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Base MP", server_Npcdata.org_mp, value);
                server_Npcdata.org_mp = value;
            }
        }
        public string NPC_Stats_MP_Regen
        {
            get
            {
                return server_Npcdata.org_mp_regen;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Base MP Regen", server_Npcdata.org_mp_regen, value);
                server_Npcdata.org_mp_regen = value;
            }
        }
        public string NPC_Level
        {
            get
            {
                return server_Npcdata.level;
            }
            set
            {
                //L2H_Log.Instance.Log_NPC_Changed(this, "Level", server_Npcdata.level, value);
                //server_Npcdata.level = value;
                //server_Npcdata.acquire_exp_rate = L2H_Parser.GetNPCExpSpRpRate(server_Npcdata, NPC_Kill_Experience, "xp");
                //server_Npcdata.exp = L2H_Parser.GetExpByLevel(value);
            }
        }
        public string NPC_Experience
        {
            get
            {
                return server_Npcdata.exp;
            }
            set
            {
                //L2H_Log.Instance.Log_NPC_Changed(this, "Experience", server_Npcdata.exp, value);
                //server_Npcdata.exp = value;
                //server_Npcdata.level = L2H_Parser.GetLevelByExp(value);
                //server_Npcdata.acquire_exp_rate = L2H_Parser.GetNPCExpSpRpRate(server_Npcdata, NPC_Kill_Experience, "xp");
            }
        }
        public string NPC_Kill_Experience
        {
            get
            {
                return L2H_Parser.GetNPCExpSpRpOnKill(this, "xp");
            }
            set
            {
                //if (string.IsNullOrEmpty(value))
                //{
                //    server_Npcdata.acquire_exp_rate = server_Npcdata.acquire_exp_rate;
                //}
                //else
                //{
                //    L2H_Log.Instance.Log_NPC_Changed(this, "EXP ON KILL", L2H_Parser.GetNPCExpSpRpOnKill(this, "xp"), L2H_Parser.GetNPCExpSpRpRate(server_Npcdata, value, "xp"));
                //    server_Npcdata.acquire_exp_rate = L2H_Parser.GetNPCExpSpRpRate(server_Npcdata, value, "xp");
                //}
            }
        }
        public string NPC_Kill_Skill_Points
        {
            get
            {
                return L2H_Parser.GetNPCExpSpRpOnKill(this, "sp");// server_Npcdata.exp;
            }
            set
            {
                //if (string.IsNullOrEmpty(value))
                //{
                //    server_Npcdata.acquire_sp = server_Npcdata.acquire_sp;
                //}
                //else
                //{
                //    L2H_Log.Instance.Log_NPC_Changed(this, "SP ON KILL", L2H_Parser.GetNPCExpSpRpOnKill(this, "sp"), L2H_Parser.GetNPCExpSpRpRate(server_Npcdata, value, "sp"));
                //    server_Npcdata.acquire_sp = L2H_Parser.GetNPCExpSpRpRate(server_Npcdata, value, "sp");
                //}
            }
        }
        public string NPC_Kill_Reputation_Points
        {
            get
            {
                return L2H_Parser.GetNPCExpSpRpOnKill(this, "rp");// server_Npcdata.exp;
            }
            set
            {
                //if (string.IsNullOrEmpty(value))
                //{
                //    server_Npcdata.acquire_rp = server_Npcdata.acquire_rp;
                //}
                //else
                //{
                //    L2H_Log.Instance.Log_NPC_Changed(this, "RP ON KILL", L2H_Parser.GetNPCExpSpRpOnKill(this, "rp"), L2H_Parser.GetNPCExpSpRpRate(server_Npcdata, value, "rp"));
                //    server_Npcdata.acquire_rp = L2H_Parser.GetNPCExpSpRpRate(server_Npcdata, value, "rp");

                //}
            }
        }
        public string NPC_P_Atk
        {
            get
            {
                return server_Npcdata.base_physical_attack;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "P Atk", server_Npcdata.base_physical_attack, value);
                server_Npcdata.base_physical_attack = value;
            }
        }
        public string NPC_M_Atk
        {
            get
            {
                return server_Npcdata.base_magic_attack;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "M Atk", server_Npcdata.base_magic_attack, value);
                server_Npcdata.base_magic_attack = value;
            }
        }
        public string NPC_Atk_Type
        {
            get
            {
                return server_Npcdata.base_attack_type;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Attack Type", server_Npcdata.base_attack_type, value);
                server_Npcdata.base_attack_type = value;
            }
        }
        public string NPC_Range
        {
            get
            {
                return server_Npcdata.base_attack_range;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Range", server_Npcdata.base_attack_range, value);
                server_Npcdata.base_attack_range = value;
            }
        }
        public string NPC_Damage_Range
        {
            get
            {
                return server_Npcdata.base_damage_range;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Damage Range", server_Npcdata.base_damage_range, value);
                server_Npcdata.base_damage_range = value;
            }
        }
        public string NPC_Damage_Rand
        {
            get
            {
                return server_Npcdata.base_rand_dam;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Random Damage", server_Npcdata.base_rand_dam, value);
                server_Npcdata.base_rand_dam = value;
            }
        }
        public string NPC_Critical
        {
            get
            {
                return server_Npcdata.base_critical;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Critical Rate", server_Npcdata.base_critical, value);
                server_Npcdata.base_critical = value;
            }
        }
        public string NPC_Hit_Modify
        {
            get
            {
                return server_Npcdata.physical_hit_modify;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Hit Chance", server_Npcdata.physical_hit_modify, value);
                server_Npcdata.physical_hit_modify = value;
            }
        }
        public string NPC_Reuse_Delay
        {
            get
            {
                return server_Npcdata.base_reuse_delay;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Reuse Delay", server_Npcdata.base_reuse_delay, value);
                server_Npcdata.base_reuse_delay = value;
            }
        }
        public int NPC_Attribute_Type
        {
            get
            {
                string[] AttributeDetails = L2H_Parser.GetAttributeAndPercentage(server_Npcdata.base_attribute_attack);
                return L2H_Parser.GetAttributeDropdownIndexFromString(AttributeDetails[0]);
            }
            set
            {
                string[] AttributeDetails = L2H_Parser.GetAttributeAndPercentage(server_Npcdata.base_attribute_attack);
                L2H_Log.Instance.Log_NPC_Changed(this, "Attack Attribute Type", AttributeDetails[0], L2H_Parser.GetAttributeDropdownStringFromIndex(value));
                AttributeDetails[0] = L2H_Parser.GetAttributeDropdownStringFromIndex(value);
            }
        }
        public string NPC_Attribute_Percent
        {
            get
            {
                string[] AttributeDetails = L2H_Parser.GetAttributeAndPercentage(server_Npcdata.base_attribute_attack);
                return AttributeDetails[1];
            }
            set
            {
                string[] AttributeDetails = L2H_Parser.GetAttributeAndPercentage(server_Npcdata.base_attribute_attack);
                L2H_Log.Instance.Log_NPC_Changed(this, "Attribute Percent", AttributeDetails[1], value);
                AttributeDetails[1] = value;


                AttributeDetails[1] = value;
                server_Npcdata.base_attribute_attack = AttributeDetails[0] + ";" + AttributeDetails[1];
            }

        }
        public string NPC_Attack_Speed
        {
            get
            {
                return server_Npcdata.base_attack_speed;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Attack Speed", server_Npcdata.base_attack_speed, value);
                server_Npcdata.base_attack_speed = value;
            }
        }
        public string NPC_P_Def
        {
            get
            {
                return server_Npcdata.base_defend;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "P Def", server_Npcdata.base_defend, value);
                server_Npcdata.base_defend = value;
            }
        }
        public string NPC_M_Def
        {
            get
            {
                return server_Npcdata.base_magic_defend;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "M Def", server_Npcdata.base_magic_defend, value);
                server_Npcdata.base_magic_defend = value;
            }
        }
        public string NPC_Evasion
        {
            get
            {
                return server_Npcdata.physical_avoid_modify;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Evasion", server_Npcdata.physical_avoid_modify, value);
                server_Npcdata.physical_avoid_modify = value;
            }
        }
        public string NPC_Shield_Rate
        {
            get
            {
                return server_Npcdata.shield_defense_rate;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Shield Rate", server_Npcdata.shield_defense_rate, value);
                server_Npcdata.shield_defense_rate = value;
            }
        }
        public string NPC_Shield_Defense
        {
            get
            {
                return server_Npcdata.shield_defense;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Shield P Def", server_Npcdata.shield_defense, value);
                server_Npcdata.shield_defense = value;
            }
        }
        public string NPC_Defense_Attribute_Fire
        {
            get
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Npcdata.base_attribute_defend);
                return resistances[0];
            }
            set
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Npcdata.base_attribute_defend);
                L2H_Log.Instance.Log_NPC_Changed(this, "Attribute Defense Fire", resistances[0], value);
                resistances[0] = value;
                server_Npcdata.base_attribute_defend = resistances[0] + ";" + resistances[1] + ";" + resistances[2] + ";" + resistances[3] + ";" + resistances[4] + ";" + resistances[5];
            }
        }
        public string NPC_Defense_Attribute_Water
        {
            get
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Npcdata.base_attribute_defend);
                return resistances[1];
            }
            set
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Npcdata.base_attribute_defend);
                L2H_Log.Instance.Log_NPC_Changed(this, "Attribute Defense Water", resistances[1], value);
                resistances[1] = value;
                server_Npcdata.base_attribute_defend = resistances[0] + ";" + resistances[1] + ";" + resistances[2] + ";" + resistances[3] + ";" + resistances[4] + ";" + resistances[5];
            }
        }
        public string NPC_Defense_Attribute_Wind
        {
            get
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Npcdata.base_attribute_defend);
                return resistances[2];
            }
            set
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Npcdata.base_attribute_defend);
                L2H_Log.Instance.Log_NPC_Changed(this, "Attribute Defense Wind", resistances[2], value);
                resistances[2] = value;
                server_Npcdata.base_attribute_defend = resistances[0] + ";" + resistances[1] + ";" + resistances[2] + ";" + resistances[3] + ";" + resistances[4] + ";" + resistances[5];
            }
        }
        public string NPC_Defense_Attribute_Earth
        {
            get
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Npcdata.base_attribute_defend);
                return resistances[3];
            }
            set
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Npcdata.base_attribute_defend);
                L2H_Log.Instance.Log_NPC_Changed(this, "Attribute Defense Earth", resistances[3], value);
                resistances[3] = value;
                server_Npcdata.base_attribute_defend = resistances[0] + ";" + resistances[1] + ";" + resistances[2] + ";" + resistances[3] + ";" + resistances[4] + ";" + resistances[5];
            }
        }
        public string NPC_Defense_Attribute_Holy
        {
            get
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Npcdata.base_attribute_defend);
                return resistances[4];
            }
            set
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Npcdata.base_attribute_defend);
                L2H_Log.Instance.Log_NPC_Changed(this, "Attribute Defense Holy", resistances[4], value);
                resistances[4] = value;
                server_Npcdata.base_attribute_defend = resistances[0] + ";" + resistances[1] + ";" + resistances[2] + ";" + resistances[3] + ";" + resistances[4] + ";" + resistances[5];
            }
        }
        public string NPC_Defense_Attribute_Unholy
        {
            get
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Npcdata.base_attribute_defend);
                return resistances[5];
            }
            set
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Npcdata.base_attribute_defend);
                L2H_Log.Instance.Log_NPC_Changed(this, "Attribute Defense Unholy", resistances[5], value);
                resistances[5] = value;
                server_Npcdata.base_attribute_defend = resistances[0] + ";" + resistances[1] + ";" + resistances[2] + ";" + resistances[3] + ";" + resistances[4] + ";" + resistances[5];
            }
        }
        public string NPC_Race
        {
            get
            {
                return server_Npcdata.race;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Race", server_Npcdata.race, value);
                server_Npcdata.race = value;
            }
        }
        public string NPC_Sex
        {
            get
            {
                return server_Npcdata.sex;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Sex", server_Npcdata.sex, value);
                server_Npcdata.sex = value;
            }
        }
        public string NPC_Clan
        {
            get
            {
                return server_Npcdata.clan;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Clan", server_Npcdata.clan, value);
                server_Npcdata.clan = value;
            }
        }
        public string NPC_Clan_Ignore
        {
            get
            {
                return server_Npcdata.ignore_clan_list;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Clan Ignore", server_Npcdata.ignore_clan_list, value);
                server_Npcdata.ignore_clan_list = value;
            }
        }
        public string NPC_Clan_Help_Range
        {
            get
            {
                return server_Npcdata.clan_help_range;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Clan Help Range", server_Npcdata.clan_help_range, value);
                server_Npcdata.clan_help_range = value;
            }
        }
        public string NPC_Corpse_Time
        {
            get
            {
                return server_Npcdata.corpse_time;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Corpse Duration", server_Npcdata.corpse_time, value);
                server_Npcdata.corpse_time = value;
            }
        }
        public bool NPC_No_Sleep_Mode
        {
            get
            {
                if (server_Npcdata.no_sleep_mode == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "No Sleep Mode", "False", "True");
                    server_Npcdata.no_sleep_mode = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "No Sleep Mode", "True", "False");
                    server_Npcdata.no_sleep_mode = "0";
                }
            }
        }
        public string NPC_Agro_Range
        {
            get
            {
                return server_Npcdata.agro_range;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Aggro Range", server_Npcdata.agro_range, value);
                server_Npcdata.agro_range = value;
            }
        }
        public string NPC_Skills
        {
            get
            {
                return server_Npcdata.skill_list_string;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Skills", server_Npcdata.skill_list_string, value);
                server_Npcdata.skill_list_string = value;
            }
        }
        public bool NPC_Has_Summoner
        {
            get
            {
                if (server_Npcdata.has_summoner == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Has Summoner", "False", "True");
                    server_Npcdata.has_summoner = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Has Summoner", "True", "False");
                    server_Npcdata.has_summoner = "0";
                }
            }
        }
        public bool NPC_Unsowing
        {
            get
            {
                if (server_Npcdata.unsowing == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Unsowing", "False", "True");
                    server_Npcdata.unsowing = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Unsowing", "True", "False");
                    server_Npcdata.unsowing = "0";
                }
            }
        }

        public bool NPC_Undead
        {
            get
            {
                if (server_Npcdata.undying == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Undead", "False", "True");
                    server_Npcdata.undying = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Undead", "True", "False");
                    server_Npcdata.undying = "0";
                }
            }
        }
        public bool NPC_Can_Move
        {
            get
            {
                if (server_Npcdata.can_move == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Can Move", "False", "True");
                    server_Npcdata.can_move = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Can Move", "True", "False");
                    server_Npcdata.can_move = "0";
                }
            }
        }
        public bool NPC_Flying
        {
            get
            {
                if (server_Npcdata.flying == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Flying", "False", "True");
                    server_Npcdata.flying = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Flying", "True", "False");
                    server_Npcdata.flying = "0";
                }
            }
        }
        public bool NPC_Targetable
        {
            get
            {
                if (server_Npcdata.targetable == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Targetable", "False", "True");
                    server_Npcdata.targetable = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Targetable", "True", "False");
                    server_Npcdata.targetable = "0";
                }
            }
        }
        public bool NPC_Attackable
        {
            get
            {
                if (server_Npcdata.can_be_attacked == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Attackable", "False", "True");
                    server_Npcdata.can_be_attacked = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Attackable", "True", "False");
                    server_Npcdata.can_be_attacked = "0";
                }
            }
        }
        public bool NPC_Show_Name_Tag
        {
            get
            {
                if (server_Npcdata.show_name_tag == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Show_Name_Tag", "False", "True");
                    server_Npcdata.show_name_tag = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Show_Name_Tag", "True", "False");
                    server_Npcdata.show_name_tag = "0";
                }
            }
        }
        public bool NPC_Event_Flag
        {
            get
            {
                if (server_Npcdata.event_flag == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Event Flag", "False", "True");
                    server_Npcdata.event_flag = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Event Flag", "True", "False");
                    server_Npcdata.event_flag = "0";
                }
            }
        }
        public bool NPC_Unique
        {
            get
            {
                if (server_Npcdata.unique == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Unique", "False", "True");
                    server_Npcdata.unique = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_NPC_Changed(this, "Unique", "True", "False");
                    server_Npcdata.unique = "0";
                }
            }
        }

        #endregion

        #region Droplists

        public string NPC_Droplist_Normal_ID
        {
            get
            {
                return server_Npcdata.additional_make_list_droplist.id;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Droplist Normal ID", server_Npcdata.additional_make_list_droplist.id, value);
                server_Npcdata.additional_make_list_droplist.id = value;
                OnPropertyChanged();
            }
        }
        public string NPC_Droplist_Spoil_ID
        {
            get
            {
                return server_Npcdata.corpse_make_list_droplist.id;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Droplist Spoil ID", server_Npcdata.corpse_make_list_droplist.id, value);
                server_Npcdata.corpse_make_list_droplist.id = value;
                OnPropertyChanged();
            }
        }
        public string NPC_Droplist_Multi_ID
        {
            get
            {
                return server_Npcdata.additional_make_multi_list_droplist.id;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Droplist Multi ID", server_Npcdata.additional_make_multi_list_droplist.id, value);
                server_Npcdata.additional_make_multi_list_droplist.id = value;
                OnPropertyChanged();
            }
        }
        public string NPC_Droplist_Extra_ID
        {
            get
            {
                return server_Npcdata.ex_item_drop_list_droplist.id;
            }
            set
            {
                L2H_Log.Instance.Log_NPC_Changed(this, "Droplist Extra ID", server_Npcdata.ex_item_drop_list_droplist.id, value);
                server_Npcdata.ex_item_drop_list_droplist.id = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
