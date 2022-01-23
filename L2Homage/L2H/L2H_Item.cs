using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public enum L2H_Item_Category { Weapon, Armor, Etc, Sets }
    public class L2H_Item
    {
        public int ID { get; set; }
        public bool IsSelected { get; set; }
        public bool IsSelectedTemp { get; set; }
        public L2H_Item_Category item_Category { get; set; }
        public Client_Itemname client_Itemname { get; set; }
        public Server_Itemdata server_Itemdata { get; set; }
        public Client_Weapon client_Weapon { get; set; }
        public Client_Armor client_Armor { get; set; }
        public Client_Etc client_Etc { get; set; }
        public bool IsCustom { get; set; }

        public L2H_Item Instance { get { return this; } }

        public List<L2H_Capsuled_Item> capsuled_Items
        {
            get; set;
        }

        public override string ToString()
        {
            return client_Itemname.name + "\n(ID: " + ID + ")";
        }


        public string GetIconString()
        {
            switch (item_Category)
            {
                case L2H_Item_Category.Weapon:
                    return client_Weapon.icon[0];

                case L2H_Item_Category.Armor:
                    return client_Armor.icon[0];
                case L2H_Item_Category.Etc:
                    return client_Etc.icon[0];
                default:
                    return "";

            }
        }

        public System.Windows.Media.Imaging.BitmapImage GetItemIcon
        {
            get
            {
                return L2H_Parser.GetItemImage(GetIconString());
            }
        }

        #region Shared Properties
        public string Item_ID
        {
            get
            {
                return ID.ToString();
            }
        }
        public string Item_Name
        {
            get
            {
                return client_Itemname.name;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Name", client_Itemname.name, value);

                client_Itemname.name = value;
            }
        }
        public string Item_Description
        {
            get
            {
                return client_Itemname.description;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Description", client_Itemname.description, value);

                client_Itemname.description = value;
            }
        }
        public string Item_Type
        {
            get
            {
                return server_Itemdata.type;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Type", server_Itemdata.type, value);

                server_Itemdata.type = value;
            }
        }
        public string Item_Additional_Name
        {
            get
            {
                return client_Itemname.add_name;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Additional Name", client_Itemname.add_name, value);


                client_Itemname.add_name = value;
            }
        }
        public string Item_Name_ID
        {
            get
            {
                return server_Itemdata.itemName;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Name ID", server_Itemdata.itemName, value);
                server_Itemdata.itemName = value;
            }
        }
        public string Item_Price
        {
            get
            {
                return server_Itemdata.default_price;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Price", server_Itemdata.default_price, value);

                server_Itemdata.default_price = value;
            }
        }
        public string Item_Crystal_Count
        {
            get
            {
                return server_Itemdata.crystal_count;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Crystal Count", server_Itemdata.crystal_count, value);

                server_Itemdata.crystal_count = value;
            }
        }
        public string Item_Period
        {
            get
            {
                return server_Itemdata.period;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Duration", server_Itemdata.period, value);

                server_Itemdata.period = value;
            }
        }
        public string Item_Enchanted
        {
            get
            {
                return server_Itemdata.enchanted;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Enchanted", server_Itemdata.enchanted, value);

                server_Itemdata.enchanted = value;
            }
        }
        public string Item_Multi_Skills
        {
            get
            {
                return server_Itemdata.item_multi_skill_list;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Multi Skills", server_Itemdata.item_multi_skill_list, value);

                server_Itemdata.item_multi_skill_list = value;
            }
        }
        public string Item_Item_Skill
        {
            get
            {
                return server_Itemdata.item_skill;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Skill", server_Itemdata.item_skill, value);

                server_Itemdata.item_skill = value;
            }
        }
        public string Item_Magic_Skill
        {
            get
            {
                return server_Itemdata.magic_skill;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Magic Skill", server_Itemdata.magic_skill, value);

                server_Itemdata.magic_skill = value;
            }
        }
        public string Item_Magic_Skill_Chance
        {
            get
            {
                return server_Itemdata.magic_skill_percentage;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Magic Skill Chance", server_Itemdata.magic_skill_percentage, value);

                server_Itemdata.magic_skill_percentage = value;
            }
        }
        public string Item_Use_Skill_Distime
        {
            get
            {
                return server_Itemdata.use_skill_distime;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Skill MP Cost", server_Itemdata.use_skill_distime, value);

                server_Itemdata.use_skill_distime = value;
            }
        }
        public string Item_Reuse_Equip_Delay
        {
            get
            {
                return server_Itemdata.equip_reuse_delay;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Equip Reuse Delay", server_Itemdata.equip_reuse_delay, value);

                server_Itemdata.equip_reuse_delay = value;
            }
        }
        public string Item_Reuse_Delay
        {
            get
            {
                return server_Itemdata.reuse_delay;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Reuse Delay", server_Itemdata.reuse_delay, value);

                server_Itemdata.reuse_delay = value;
            }
        }
        public string Item_Reuse_Delay_Group
        {
            get
            {
                return server_Itemdata.delay_share_group;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Reuse Delay Group", server_Itemdata.delay_share_group, value);

                server_Itemdata.delay_share_group = value;
            }
        }
        public string Item_Unequip_Skill
        {
            get
            {
                return server_Itemdata.unequip_skill;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Unequip Skill", server_Itemdata.unequip_skill, value);

                server_Itemdata.unequip_skill = value;
            }
        }
        public string Item_Enchanted_To_Four_Skill
        {
            get
            {
                return server_Itemdata.item_skill_enchanted_four;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Enchanted To +4 Skill", server_Itemdata.item_skill_enchanted_four, value);

                server_Itemdata.item_skill_enchanted_four = value;
            }
        }

        public bool Item_Droppable
        {
            get
            {
                if (server_Itemdata.is_drop == "1")
                    return true;
                else
                    return false;
            }
            set
            {

                if (value == true)
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Droppable", "False", "True");
                    server_Itemdata.is_drop = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Droppable", "True", "False");
                    server_Itemdata.is_drop = "0";
                }
            }
        }
        public bool Item_Tradable
        {
            get
            {
                if (server_Itemdata.is_trade == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Tradable", "False", "True");
                    server_Itemdata.is_trade = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Tradable", "True", "False");
                    server_Itemdata.is_trade = "0";
                }
            }
        }
        public bool Item_Private_Store
        {
            get
            {
                if (server_Itemdata.is_private_store == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Private Store-able", "False", "True");
                    server_Itemdata.is_private_store = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Private Store-able", "True", "False");
                    server_Itemdata.is_private_store = "0";
                }
            }
        }
        public bool Item_Destruct
        {
            get
            {
                if (server_Itemdata.is_destruct == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Destructible", "False", "True");
                    server_Itemdata.is_destruct = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Destructible", "True", "False");
                    server_Itemdata.is_destruct = "0";
                }
            }
        }
        public bool Item_Enchantable
        {
            get
            {
                if (server_Itemdata.enchant_enable == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Enchantable", "False", "True");
                    server_Itemdata.enchant_enable = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Enchantable", "True", "False");
                    server_Itemdata.enchant_enable = "0";
                }
            }
        }
        public bool Item_Elemental
        {
            get
            {
                if (server_Itemdata.elemental_enable == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Elemental", "False", "True");
                    server_Itemdata.elemental_enable = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Elemental", "True", "False");
                    server_Itemdata.elemental_enable = "0";
                }
            }
        }
        public bool Item_NPC_Only
        {
            get
            {
                if (server_Itemdata.for_npc == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item For NPC", "False", "True");
                    server_Itemdata.for_npc = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item For NPC", "True", "False");
                    server_Itemdata.for_npc = "0";
                }
            }
        }
        public bool Item_Oly
        {
            get
            {
                if (server_Itemdata.is_olympiad_can_use == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Olympiad Enabled", "False", "True");
                    server_Itemdata.is_olympiad_can_use = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Olympiad Enabled", "True", "False");
                    server_Itemdata.is_olympiad_can_use = "0";
                }
            }
        }
        public bool Item_Premium
        {
            get
            {
                if (server_Itemdata.is_premium == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Premium", "False", "True");
                    server_Itemdata.is_premium = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Premium", "True", "False");
                    server_Itemdata.is_premium = "0";
                }
            }
        }
        public string Item_Equip_Condition
        {
            get
            {
                return server_Itemdata.equip_condition;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Equip Condition", server_Itemdata.equip_condition, value);

                server_Itemdata.equip_condition = value;
            }
        }
        public string Item_Use_Condition
        {
            get
            {
                return server_Itemdata.use_condition;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Use Condition", server_Itemdata.use_condition, value);

                server_Itemdata.use_condition = value;
            }
        }
        public string Item_Equip_Situational
        {
            get
            {
                return server_Itemdata.item_equip_option;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Item Equip Situational Condition", server_Itemdata.equip_condition, value);

                server_Itemdata.item_equip_option = value;
            }
        }

        public string Item_Defense_Attribute_Fire
        {
            get
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Itemdata.base_attribute_defend);
                return resistances[0];
            }
            set
            {

                string[] resistances = L2H_Parser.GetResistancesArray(server_Itemdata.base_attribute_defend);
                L2H_Log.Instance.Log_Item_Change(this, "Item Defense Attribute Fire", resistances[0], value);
                resistances[0] = value;
                server_Itemdata.base_attribute_defend = resistances[0] + ";" + resistances[1] + ";" + resistances[2] + ";" + resistances[3] + ";" + resistances[4] + ";" + resistances[5];
            }
        }
        public string Item_Defense_Attribute_Water
        {
            get
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Itemdata.base_attribute_defend);
                return resistances[1];
            }
            set
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Itemdata.base_attribute_defend);
                L2H_Log.Instance.Log_Item_Change(this, "Item Defense Attribute Water", resistances[1], value);
                resistances[1] = value;
                server_Itemdata.base_attribute_defend = resistances[0] + ";" + resistances[1] + ";" + resistances[2] + ";" + resistances[3] + ";" + resistances[4] + ";" + resistances[5];
            }
        }
        public string Item_Defense_Attribute_Wind
        {
            get
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Itemdata.base_attribute_defend);
                return resistances[2];
            }
            set
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Itemdata.base_attribute_defend);
                L2H_Log.Instance.Log_Item_Change(this, "Item Defense Attribute Wind", resistances[2], value);
                resistances[2] = value;
                server_Itemdata.base_attribute_defend = resistances[0] + ";" + resistances[1] + ";" + resistances[2] + ";" + resistances[3] + ";" + resistances[4] + ";" + resistances[5];
            }
        }
        public string Item_Defense_Attribute_Earth
        {
            get
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Itemdata.base_attribute_defend);
                return resistances[3];
            }
            set
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Itemdata.base_attribute_defend);
                L2H_Log.Instance.Log_Item_Change(this, "Item Defense Attribute Earth", resistances[3], value);
                resistances[3] = value;
                server_Itemdata.base_attribute_defend = resistances[0] + ";" + resistances[1] + ";" + resistances[2] + ";" + resistances[3] + ";" + resistances[4] + ";" + resistances[5];
            }
        }
        public string Item_Defense_Attribute_Holy
        {
            get
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Itemdata.base_attribute_defend);
                return resistances[4];
            }
            set
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Itemdata.base_attribute_defend);
                L2H_Log.Instance.Log_Item_Change(this, "Item Defense Attribute Holy", resistances[4], value);
                resistances[4] = value;
                server_Itemdata.base_attribute_defend = resistances[0] + ";" + resistances[1] + ";" + resistances[2] + ";" + resistances[3] + ";" + resistances[4] + ";" + resistances[5];
            }
        }
        public string Item_Defense_Attribute_Unholy
        {
            get
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Itemdata.base_attribute_defend);
                return resistances[5];
            }
            set
            {
                string[] resistances = L2H_Parser.GetResistancesArray(server_Itemdata.base_attribute_defend);
                L2H_Log.Instance.Log_Item_Change(this, "Item Defense Attribute Unholy", resistances[5], value);
                resistances[5] = value;
                server_Itemdata.base_attribute_defend = resistances[0] + ";" + resistances[1] + ";" + resistances[2] + ";" + resistances[3] + ";" + resistances[4] + ";" + resistances[5];
            }
        }
        public string Item_Evasion
        {
            get
            {
                if (client_Weapon != null)
                    return client_Weapon.avoid_mod;
                else if (client_Armor != null)
                    return client_Armor.avoid_mod;
                else
                    return "";
            }
            set
            {
                if (client_Weapon != null)
                    client_Weapon.avoid_mod = value;
                if (client_Armor != null)
                    client_Armor.avoid_mod = value;


                L2H_Log.Instance.Log_Item_Change(this, "Item Evasion", server_Itemdata.avoid_modify, value);

                server_Itemdata.avoid_modify = value;
            }
        }
        public string Item_MP_Bonus // Can be applied to weapons and armors, but only shows for weapons by default
        {
            get
            {
                return server_Itemdata.mp_bonus;
            }
            set
            {

                L2H_Log.Instance.Log_Item_Change(this, "Item MP Bonus", server_Itemdata.mp_bonus, value);

                server_Itemdata.mp_bonus = value;
                if (client_Armor != null)
                    client_Armor.mpbonus = value;
            }
        }
        public string Item_Durability
        {
            get
            {
                if (client_Weapon != null)
                    return client_Weapon.durability;
                else if (client_Armor != null)
                    return client_Armor.durability;
                else
                    return client_Etc.durability;
            }
            set
            {
                if (client_Weapon != null)
                    client_Weapon.durability = value;
                else if (client_Armor != null)
                    client_Armor.durability = value;
                else
                    client_Etc.durability = value;


                L2H_Log.Instance.Log_Item_Change(this, "Item Durability", server_Itemdata.durability, value);

                server_Itemdata.durability = value;
            }
        }
        public string Item_Weight
        {
            get
            {
                if (client_Weapon != null)
                    return client_Weapon.weight;
                else if (client_Armor != null)
                    return client_Armor.weight;
                else
                    return client_Etc.weight;
            }
            set
            {
                if (client_Weapon != null)
                    client_Weapon.weight = value;
                else if (client_Armor != null)
                    client_Armor.weight = value;
                else
                    client_Etc.weight = value;

                L2H_Log.Instance.Log_Item_Change(this, "Item Weight", server_Itemdata.weight, value);

                server_Itemdata.weight = value;
            }
        }
        public int Item_Crystal_Type
        {
            get
            {
                if (client_Weapon != null)
                    return (int.Parse(client_Weapon.crystal_type));
                else if (client_Armor != null)
                    return (int.Parse(client_Armor.crystal_type));
                else
                    return (int.Parse(client_Etc.grade));
            }
            set
            {
                if (client_Weapon != null)
                    client_Weapon.crystal_type = value.ToString();
                else if (client_Armor != null)
                    client_Armor.crystal_type = value.ToString();
                else
                    client_Etc.grade = value.ToString();


                L2H_Log.Instance.Log_Item_Change(this, "Item Grade", server_Itemdata.crystal_type, L2H_Parser.GetGradeStringFromCrystalTypeID(value.ToString()));

                server_Itemdata.crystal_type = L2H_Parser.GetGradeStringFromCrystalTypeID(value.ToString());
            }
        }

        public bool Item_Crystallizable
        {
            get
            {
                if (client_Weapon != null)
                {
                    if (client_Weapon.crystallizable == "1")
                        return true;
                    else
                        return false;
                }
                else if (client_Armor != null)
                {
                    if (client_Armor.crystallizable == "1")
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (client_Etc.crystallizable == "1")
                        return true;
                    else
                        return false;
                }
            }
            set
            {
                if (value == true)
                    L2H_Log.Instance.Log_Item_Change(this, "Item Crystallizable", "False", "True");
                else
                    L2H_Log.Instance.Log_Item_Change(this, "Item Crystallizable", "True", "False");

                if (client_Weapon != null)
                {
                    if (value == true)
                        client_Weapon.crystallizable = "1";
                    else
                        client_Weapon.crystallizable = "0";
                }
                else if (client_Armor != null)
                {
                    if (value == true)
                        client_Armor.crystallizable = "1";
                    else
                        client_Armor.crystallizable = "0";
                }
                else
                {
                    if (value == true)
                        client_Etc.crystallizable = "1";
                    else
                        client_Etc.crystallizable = "0";
                }
            }
        }
        public bool Item_Magic_Weapon
        {
            get
            {
                if (server_Itemdata.magic_weapon == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Magic Weapon", "False", "True");
                    server_Itemdata.magic_weapon = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Magic Weapon", "True", "False");
                    server_Itemdata.magic_weapon = "0";
                }
            }
        }

        #endregion

        #region Weapon Properties

        public System.Windows.Media.Imaging.BitmapImage Weapon_Image
        {
            get
            {
                return L2H_Parser.GetItemImage(client_Weapon.icon[0]);
            }
        }

        public string Weapon_Type
        {
            get
            {
                return L2H_Parser.GetItemTypeStringFromTagID(client_Weapon.tag);
            }
        }

        public string Weapon_Sub_Type
        {
            get
            {
                return L2H_Parser.GetWeaponSubtypeStringFromWeaponTypeID(client_Weapon.weapon_type);
            }
        }

        public string Weapon_Slot_Type
        {
            get
            {
                if (server_Itemdata.slot_bit_type == "lhand")
                    return "Left Hand";
                if (server_Itemdata.slot_bit_type == "rhand")
                    return "Right Hand";
                if (server_Itemdata.slot_bit_type == "lrhand")
                    return "Two-Handed";
                if (server_Itemdata.slot_bit_type == "none")
                    return "Cannot Equip";
                
                return server_Itemdata.slot_bit_type;
            }
        }


        public string Weapon_Mesh_1
        {
            get
            {
                return client_Weapon.wpn_mesh[0];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Mesh 1", client_Weapon.wpn_mesh[0], value);

                client_Weapon.wpn_mesh[0] = value;
            }
        }
        public string Weapon_Mesh_2
        {
            get
            {
                return client_Weapon.wpn_mesh[1];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Mesh 2", client_Weapon.wpn_mesh[1], value);

                client_Weapon.wpn_mesh[1] = value;
            }
        }
        public string Weapon_Texture_1
        {
            get
            {
                return client_Weapon.wpn_tex[0];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Texture 1", client_Weapon.wpn_tex[0], value);

                client_Weapon.wpn_tex[0] = value;
            }
        }
        public string Weapon_Texture_2
        {
            get
            {
                return client_Weapon.wpn_tex[1];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Texture 2", client_Weapon.wpn_tex[1], value);

                client_Weapon.wpn_tex[1] = value;
            }
        }
        public string Weapon_Texture_3
        {
            get
            {
                return client_Weapon.wpn_tex[2];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Texture 3", client_Weapon.wpn_tex[2], value);

                client_Weapon.wpn_tex[2] = value;
            }
        }
        public string Weapon_Texture_4
        {
            get
            {
                return client_Weapon.wpn_tex[3];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Texture 4", client_Weapon.wpn_tex[3], value);

                client_Weapon.wpn_tex[3] = value;
            }
        }
        public string Weapon_Sound_1
        {
            get
            {
                return client_Weapon.item_sound[0];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Sound 1", client_Weapon.item_sound[0], value);

                client_Weapon.item_sound[0] = value;
            }
        }
        public string Weapon_Sound_2
        {
            get
            {
                return client_Weapon.item_sound[1];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Sound 2", client_Weapon.item_sound[1], value);

                client_Weapon.item_sound[1] = value;
            }
        }
        public string Weapon_Sound_3
        {
            get
            {
                return client_Weapon.item_sound[2];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Sound 3", client_Weapon.item_sound[2], value);

                client_Weapon.item_sound[2] = value;
            }
        }
        public string Weapon_Sound_4
        {
            get
            {
                return client_Weapon.item_sound[3];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Sound 4", client_Weapon.item_sound[3], value);

                client_Weapon.item_sound[3] = value;
            }
        }
        public string Weapon_Drop_Sound
        {
            get
            {
                return client_Weapon.drop_sound;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Drop Sound", client_Weapon.drop_sound, value);

                client_Weapon.drop_sound = value;
            }
        }
        public string Weapon_Equip_Sound
        {
            get
            {
                return client_Weapon.equip_sound;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Equip Sound", client_Weapon.equip_sound, value);

                client_Weapon.equip_sound = value;
            }
        }
        public string Weapon_Effect
        {
            get
            {
                return client_Weapon.effect;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Effect", client_Weapon.effect, value);

                client_Weapon.effect = value;
            }
        }
        public string Weapon_Icon_1
        {
            get
            {
                return client_Weapon.icon[0];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Icon 1", client_Weapon.icon[0], value);

                client_Weapon.icon[0] = value;
            }
        }
        public string Weapon_Icon_2
        {
            get
            {
                return client_Weapon.icon[1];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Icon 2", client_Weapon.icon[1], value);

                client_Weapon.icon[1] = value;
            }
        }
        public string Weapon_Icon_3
        {
            get
            {
                return client_Weapon.icon[2];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Icon 3", client_Weapon.icon[2], value);

                client_Weapon.icon[2] = value;
            }
        }
        public string Weapon_Icon_4
        {
            get
            {
                return client_Weapon.icon[3];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Icon 4", client_Weapon.icon[3], value);

                client_Weapon.icon[3] = value;
            }
        }
        public string Weapon_Icon_5
        {
            get
            {
                return client_Weapon.icon[3];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Icon 5", client_Weapon.icon[4], value);

                client_Weapon.icon[3] = value;
            }
        }
        public string Weapon_Drop_Mesh_1
        {
            get
            {
                return client_Weapon.drop_mesh1;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Drop Mesh 1", client_Weapon.drop_mesh1, value);

                client_Weapon.drop_mesh1 = value;
            }
        }
        public string Weapon_Drop_Mesh_2
        {
            get
            {
                return client_Weapon.drop_mesh2;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Drop Mesh 2", client_Weapon.drop_mesh2, value);

                client_Weapon.drop_mesh2 = value;
            }
        }
        public string Weapon_Drop_Mesh_3
        {
            get
            {
                return client_Weapon.drop_mesh3;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Drop Mesh 3", client_Weapon.drop_mesh3, value);

                client_Weapon.drop_mesh3 = value;
            }
        }
        public string Weapon_Drop_Texture_1
        {
            get
            {
                return client_Weapon.drop_tex1;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Drop Texture 1", client_Weapon.drop_tex1, value);

                client_Weapon.drop_tex1 = value;
            }
        }
        public string Weapon_Drop_Texture_2
        {
            get
            {
                return client_Weapon.drop_tex2;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Drop Texture 2", client_Weapon.drop_tex2, value);

                client_Weapon.drop_tex2 = value;
            }
        }
        public string Weapon_Drop_Texture_3
        {
            get
            {
                return client_Weapon.drop_tex3;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Drop Texture 3", client_Weapon.drop_tex3, value);

                client_Weapon.drop_tex3 = value;
            }
        }
        public string Weapon_Drop_Texture_Extra
        {
            get
            {
                return client_Weapon.drop_extratex1;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Drop Texture 4", client_Weapon.drop_extratex1, value);

                client_Weapon.drop_extratex1 = value;
            }
        }
        public string Weapon_P_Atk
        {
            get
            {
                return client_Weapon.patt;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon P Atk", client_Weapon.patt, value);

                client_Weapon.patt = value;
                server_Itemdata.physical_damage = value;
            }
        }
        public string Weapon_M_Atk
        {
            get
            {
                return client_Weapon.matt;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon M Atk", client_Weapon.matt, value);

                client_Weapon.matt = value;
                server_Itemdata.magical_damage = value;
            }
        }
        public string Weapon_Crit_Rate
        {
            get
            {
                return client_Weapon.critical;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Crit Rate", client_Weapon.critical, value);

                client_Weapon.critical = value;
                server_Itemdata.critical = value;
            }
        }
        public string Weapon_Hit_Rate
        {
            get
            {
                return client_Weapon.hit_mod;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Hit Rate", client_Weapon.hit_mod, value);

                client_Weapon.hit_mod = value;
                server_Itemdata.hit_modify = value;
            }
        }
        public string Weapon_Speed
        {
            get
            {
                return client_Weapon.speed;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Attack Speed", client_Weapon.speed, value);

                client_Weapon.speed = value;
                server_Itemdata.attack_speed = value;
            }
        }
        public string Weapon_Attack_Range
        {
            get
            {
                return server_Itemdata.attack_range;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Range", server_Itemdata.attack_range, value);

                server_Itemdata.attack_range = value;
            }
        }
        public string Weapon_Damage_Range
        {
            get
            {
                return server_Itemdata.damage_range;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Damage Range", server_Itemdata.damage_range, value);

                server_Itemdata.damage_range = value;
            }
        }
        public string Weapon_Damage_Random
        {
            get
            {
                return server_Itemdata.random_damage;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Random Damage", server_Itemdata.random_damage, value);

                server_Itemdata.random_damage = value;
            }
        }
        public int Weapon_Attack_Attribute_Type
        {
            get
            {
                string[] details = L2H_Parser.GetAttributeAndPercentage(server_Itemdata.base_attribute_attack);
                return L2H_Parser.GetAttributeDropdownIndexFromString(details[0]);
            }
            set
            {

                string[] details = L2H_Parser.GetAttributeAndPercentage(server_Itemdata.base_attribute_attack);
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Attack Attribute Type", details[0], L2H_Parser.GetAttributeDropdownStringFromIndex(value));
                details[0] = L2H_Parser.GetAttributeDropdownStringFromIndex(value);

                server_Itemdata.base_attribute_attack = details[0] + ";" + details[1];
            }
        }
        public string Weapon_Attack_Attribute_Value
        {
            get
            {
                string[] details = L2H_Parser.GetAttributeAndPercentage(server_Itemdata.base_attribute_attack);
                return details[1];
            }
            set
            {
                string[] details = L2H_Parser.GetAttributeAndPercentage(server_Itemdata.base_attribute_attack);
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Attack Attribute Value", details[1], value);
                details[1] = value;

                server_Itemdata.base_attribute_attack = details[0] + ";" + details[1];
            }
        }

        public string Weapon_Shield_Pdef
        {
            get
            {
                return client_Weapon.shield_pdef;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Shield P Def.", client_Weapon.shield_pdef, value);

                client_Weapon.shield_pdef = value;
                server_Itemdata.shield_defense = value;
            }
        }
        public string Weapon_Shield_Rate
        {
            get
            {
                return client_Weapon.shield_rate;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Shield Rate", client_Weapon.shield_rate, value);

                client_Weapon.shield_rate = value;
                server_Itemdata.shield_defense_rate = value;
            }
        }
        public string Weapon_Attack_Skill
        {
            get
            {
                return server_Itemdata.attack_skill;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Attack Skill", server_Itemdata.attack_skill, value);

                server_Itemdata.attack_skill = value;

            }
        }
        public string Weapon_MP_Consume
        {
            get
            {
                return server_Itemdata.mp_consume;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon MP Consume", server_Itemdata.mp_consume, value);

                server_Itemdata.mp_consume = value;
                client_Weapon.mp_consume = value;
            }
        }
        public string Weapon_Soulshot_Consume
        {
            get
            {
                return server_Itemdata.soulshot_count;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Soulshot Consume", server_Itemdata.soulshot_count, value);

                server_Itemdata.soulshot_count = value;
                client_Weapon.SS_count = value;
            }
        }
        public string Weapon_Spiritshot_Consume
        {
            get
            {
                return server_Itemdata.spiritshot_count;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Spiritshot Consume", server_Itemdata.spiritshot_count, value);

                server_Itemdata.spiritshot_count = value;
                client_Weapon.SPS_count = value;
            }
        }
        public string Weapon_Critical_Attack_Skill
        {
            get
            {
                return server_Itemdata.critical_attack_skill;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Weapon Critical Attack Skill", server_Itemdata.critical_attack_skill, value);

                server_Itemdata.critical_attack_skill = value;
            }
        }
        public string Weapon_Reduced_Soulshot_Chance
        {
            get
            {
                if (string.IsNullOrEmpty(server_Itemdata.reduced_soulshot))
                    return "";

                string[] soulshotTrigger = L2H_Parser.GetChanceAndAmountFromTrigger(server_Itemdata.reduced_soulshot);
                return soulshotTrigger[0];
            }
            set
            {

                string[] soulshotTrigger;

                if (string.IsNullOrEmpty(server_Itemdata.reduced_soulshot))
                {
                    soulshotTrigger = new string[2];
                    soulshotTrigger[0] = "0";
                    soulshotTrigger[1] = "0";

                }
                else
                    soulshotTrigger = L2H_Parser.GetChanceAndAmountFromTrigger(server_Itemdata.reduced_soulshot);

                L2H_Log.Instance.Log_Item_Change(this, "Weapon Reduced Soulshot Chance", soulshotTrigger[0], value);

                soulshotTrigger[0] = value;
                server_Itemdata.reduced_soulshot = soulshotTrigger[0] + ";" + soulshotTrigger[1];
            }
        }
        public string Weapon_Reduced_Soulshot_Count
        {
            get
            {
                if (string.IsNullOrEmpty(server_Itemdata.reduced_soulshot))
                    return "";

                string[] soulshotTrigger = L2H_Parser.GetChanceAndAmountFromTrigger(server_Itemdata.reduced_soulshot);
                return soulshotTrigger[1];
            }
            set
            {

                string[] soulshotTrigger;

                if (string.IsNullOrEmpty(server_Itemdata.reduced_soulshot))
                {
                    soulshotTrigger = new string[2];
                    soulshotTrigger[0] = "0";
                    soulshotTrigger[1] = "0";

                }
                else
                    soulshotTrigger = L2H_Parser.GetChanceAndAmountFromTrigger(server_Itemdata.reduced_soulshot);

                L2H_Log.Instance.Log_Item_Change(this, "Weapon Reduced Soulshot Count", soulshotTrigger[1], value);
                soulshotTrigger[1] = value;
                server_Itemdata.reduced_soulshot = soulshotTrigger[0] + ";" + soulshotTrigger[1];
            }
        }
        public string Weapon_Reduced_Spiritshot_Chance
        {
            get
            {
                if (string.IsNullOrEmpty(server_Itemdata.reduced_spiritshot))
                    return "";

                string[] soulshotTrigger = L2H_Parser.GetChanceAndAmountFromTrigger(server_Itemdata.reduced_spiritshot);
                return soulshotTrigger[0];
            }
            set
            {

                string[] soulshotTrigger;

                if (string.IsNullOrEmpty(server_Itemdata.reduced_spiritshot))
                {
                    soulshotTrigger = new string[2];
                    soulshotTrigger[0] = "0";
                    soulshotTrigger[1] = "0";

                }
                else
                    soulshotTrigger = L2H_Parser.GetChanceAndAmountFromTrigger(server_Itemdata.reduced_spiritshot);

                L2H_Log.Instance.Log_Item_Change(this, "Weapon Reduced Spiritshot Chance", soulshotTrigger[0], value);
                soulshotTrigger[0] = value;
                server_Itemdata.reduced_spiritshot = soulshotTrigger[0] + ";" + soulshotTrigger[1];
            }
        }
        public string Weapon_Reduced_Spiritshot_Count
        {
            get
            {
                if (string.IsNullOrEmpty(server_Itemdata.reduced_spiritshot))
                    return "";

                string[] soulshotTrigger = L2H_Parser.GetChanceAndAmountFromTrigger(server_Itemdata.reduced_spiritshot);
                return soulshotTrigger[1];
            }
            set
            {
                string[] soulshotTrigger;

                if (string.IsNullOrEmpty(server_Itemdata.reduced_spiritshot))
                {
                    soulshotTrigger = new string[2];
                    soulshotTrigger[0] = "0";
                    soulshotTrigger[1] = "0";

                }
                else
                    soulshotTrigger = L2H_Parser.GetChanceAndAmountFromTrigger(server_Itemdata.reduced_spiritshot);

                L2H_Log.Instance.Log_Item_Change(this, "Weapon Reduced Spiritshot Count", soulshotTrigger[1], value);
                soulshotTrigger[1] = value;
                server_Itemdata.reduced_spiritshot = soulshotTrigger[0] + ";" + soulshotTrigger[1];
            }
        }
        public bool Weapon_Is_Hero
        {
            get
            {
                if (client_Weapon.is_hero == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Magic Weapon", "False", "True");
                    client_Weapon.is_hero = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Item Magic Weapon", "True", "False");
                    client_Weapon.is_hero = "0";
                }
            }
        }



        #endregion

        #region Armor Properties

        public int Armor_Type
        {
            get
            {
                return int.Parse(client_Armor.armor_type);
            }
            set
            {

                L2H_Log.Instance.Log_Item_Change(this, "Armor Type", client_Armor.armor_type, value.ToString());

                client_Armor.armor_type = value.ToString();
            }
        }
        public string Armor_Sound_1
        {
            get
            {
                return client_Armor.item_sound[0];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Sound 1", client_Armor.item_sound[0], value);

                client_Armor.item_sound[0] = value;
            }
        }
        public string Armor_Sound_2
        {
            get
            {
                return client_Armor.item_sound[1];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Sound 2", client_Armor.item_sound[1], value);

                client_Armor.item_sound[1] = value;
            }
        }
        public string Armor_Sound_3
        {
            get
            {
                return client_Armor.item_sound[2];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Sound 3", client_Armor.item_sound[2], value);

                client_Armor.item_sound[2] = value;
            }
        }
        public string Armor_Sound_4
        {
            get
            {
                return client_Armor.item_sound[3];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Sound 4", client_Armor.item_sound[3], value);

                client_Armor.item_sound[3] = value;
            }
        }
        public string Armor_Drop_Sound
        {
            get
            {
                return client_Armor.drop_sound;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Drop Sound", client_Armor.drop_sound, value);

                client_Armor.drop_sound = value;
            }
        }
        public string Armor_Equip_Sound
        {
            get
            {
                return client_Armor.equip_sound;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Equip Sound", client_Armor.equip_sound, value);

                client_Armor.equip_sound = value;
            }
        }
        public string Armor_Icon_1
        {
            get
            {
                return client_Armor.icon[0];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Icon 1", client_Armor.icon[0], value);

                client_Armor.icon[0] = value;
            }
        }
        public string Armor_Icon_2
        {
            get
            {
                return client_Armor.icon[1];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Icon 2", client_Armor.icon[1], value);

                client_Armor.icon[1] = value;
            }
        }
        public string Armor_Icon_3
        {
            get
            {
                return client_Armor.icon[2];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Icon 3", client_Armor.icon[2], value);

                client_Armor.icon[2] = value;
            }
        }
        public string Armor_Icon_4
        {
            get
            {
                return client_Armor.icon[3];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Icon 4", client_Armor.icon[3], value);

                client_Armor.icon[3] = value;
            }
        }
        public string Armor_Icon_5
        {
            get
            {
                return client_Armor.icon[4];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Icon 5", client_Armor.icon[4], value);

                client_Armor.icon[4] = value;
            }
        }
        public string Armor_Drop_Mesh_1
        {
            get
            {
                return client_Armor.drop_mesh1;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Drop Mesh 1", client_Armor.drop_mesh1, value);

                client_Armor.drop_mesh1 = value;
            }
        }
        public string Armor_Drop_Mesh_2
        {
            get
            {
                return client_Armor.drop_mesh2;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Drop Mesh 2", client_Armor.drop_mesh2, value);

                client_Armor.drop_mesh2 = value;
            }
        }
        public string Armor_Drop_Mesh_3
        {
            get
            {
                return client_Armor.drop_mesh3;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Drop Mesh 3", client_Armor.drop_mesh3, value);

                client_Armor.drop_mesh3 = value;
            }
        }
        public string Armor_Drop_Texture_1
        {
            get
            {
                return client_Armor.drop_tex1;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Drop Texture 1", client_Armor.drop_tex1, value);

                client_Armor.drop_tex1 = value;
            }
        }
        public string Armor_Drop_Texture_2
        {
            get
            {
                return client_Armor.drop_tex2;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Drop Texture 2", client_Armor.drop_tex2, value);

                client_Armor.drop_tex2 = value;
            }
        }
        public string Armor_Drop_Texture_3
        {
            get
            {
                return client_Armor.drop_tex3;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Drop Texture 3", client_Armor.drop_tex3, value);

                client_Armor.drop_tex3 = value;
            }
        }
        public string Armor_Drop_Texture_4
        {
            get
            {
                return client_Armor.drop_extratex1;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor Drop Texture 4", client_Armor.drop_extratex1, value);

                client_Armor.drop_extratex1 = value;
            }
        }
        public string Armor_P_Def
        {
            get
            {
                return client_Armor.pdef;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor P Def.", client_Armor.pdef, value);

                client_Armor.pdef = value;
                server_Itemdata.physical_defense = value;
            }
        }
        public string Armor_M_Def
        {
            get
            {
                return client_Armor.mdef;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Armor M Def.", client_Armor.mdef, value);

                client_Armor.mdef = value;
                server_Itemdata.magical_defense = value;
            }
        }

        #endregion

        #region ETC
        public string Etc_Type
        {
            get
            {
                return server_Itemdata.etcitem_type;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Type", server_Itemdata.etcitem_type, value);
                server_Itemdata.etcitem_type = value;
            }
        }
        public string Etc_Sound
        {
            get
            {
                return client_Etc.item_sound;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Sound", client_Etc.item_sound, value);

                client_Etc.item_sound = value;
            }
        }
        public string Etc_Equip_Sound
        {
            get
            {
                return client_Etc.equip_sound;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Equip Sound", client_Etc.equip_sound, value);

                client_Etc.equip_sound = value;
            }
        }
        public string Etc_Icon_1
        {
            get
            {
                return client_Etc.icon[0];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Icon 1", client_Etc.icon[0], value);

                client_Etc.icon[0] = value;
            }
        }
        public string Etc_Icon_2
        {
            get
            {
                return client_Etc.icon[1];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Icon 2", client_Etc.icon[1], value);

                client_Etc.icon[1] = value;
            }
        }
        public string Etc_Icon_3
        {
            get
            {
                return client_Etc.icon[2];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Icon 3", client_Etc.icon[2], value);

                client_Etc.icon[2] = value;
            }
        }
        public string Etc_Icon_4
        {
            get
            {
                return client_Etc.icon[3];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Icon 4", client_Etc.icon[3], value);

                client_Etc.icon[3] = value;
            }
        }
        public string Etc_Icon_5
        {
            get
            {
                return client_Etc.icon[4];
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Icon 5", client_Etc.icon[4], value);

                client_Etc.icon[4] = value;
            }
        }
        public string Etc_Drop_Mesh_1
        {
            get
            {
                return client_Etc.drop_mesh1;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Drop Mesh 1", client_Etc.drop_mesh1, value);

                client_Etc.drop_mesh1 = value;
            }
        }
        public string Etc_Drop_Mesh_2
        {
            get
            {
                return client_Etc.drop_mesh2;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Drop Mesh 2", client_Etc.drop_mesh2, value);

                client_Etc.drop_mesh2 = value;
            }
        }
        public string Etc_Drop_Mesh_3
        {
            get
            {
                return client_Etc.drop_mesh3;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Drop Mesh 3", client_Etc.drop_mesh3, value);

                client_Etc.drop_mesh3 = value;
            }
        }
        public string Etc_Drop_Texture_1
        {
            get
            {
                return client_Etc.drop_tex1;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Drop Texture 1", client_Etc.drop_tex1, value);

                client_Etc.drop_tex1 = value;
            }
        }
        public string Etc_Drop_Texture_2
        {
            get
            {
                return client_Etc.drop_tex2;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Drop Texture 2", client_Etc.drop_tex2, value);

                client_Etc.drop_tex2 = value;
            }
        }
        public string Etc_Drop_Texture_3
        {
            get
            {
                return client_Etc.drop_tex3;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Drop Texture 3", client_Etc.drop_tex3, value);

                client_Etc.drop_tex3 = value;
            }
        }
        public string Etc_Drop_Texture_4
        {
            get
            {
                return client_Etc.drop_extratex1;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Drop Texture 4", client_Etc.drop_extratex1, value);

                client_Etc.drop_extratex1 = value;
            }
        }
        public bool Etc_Stackable
        {
            get
            {
                if (client_Etc.stackable == "1")
                    return true;
                else
                    return false;
            }
            set
            {

                if (value == true)
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Etc Stackable", "False", "True");
                    client_Etc.stackable = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Item_Change(this, "Etc Stackable", "True", "False");
                    client_Etc.stackable = "0";
                }
            }
        }
        public string Etc_Initial_Count
        {
            get
            {
                return server_Itemdata.initial_count;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Min Count", server_Itemdata.initial_count, value);

                server_Itemdata.initial_count = value;
            }
        }
        public string Etc_Max_Count
        {
            get
            {
                return server_Itemdata.maximum_count;
            }
            set
            {
                L2H_Log.Instance.Log_Item_Change(this, "Etc Max Count", server_Itemdata.maximum_count, value);

                server_Itemdata.maximum_count = value;
            }

        }
        #endregion


    }
}
