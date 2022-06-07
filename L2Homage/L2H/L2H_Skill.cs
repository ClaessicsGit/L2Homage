using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace L2Homage
{

    public class L2H_Skill : INotifyPropertyChanged
    {
        public Server_Skilldata server_Skilldata { get; set; }
        public Server_Skill_pch server_Skill_Pch { get; set; }
        public Server_Skillenchantcost server_Skillenchantcost { get; set; }
        public Server_Skillenchantdata server_Skillenchantdata { get; set; }
        public Client_Skill client_Skill { get; set; }
        public Client_Skillname client_Skillname { get; set; }
        public Client_Skillsound client_Skillsound { get; set; }
        public bool IsSelected { get; set; }
        public bool IsSelectedTemp { get; set; }
        public L2H_Skill Instance { get { return this; } }
        public bool IsCustom { get; set; }

        public BitmapImage GetSkillIcon
        {
            get
            {
                if (client_Skill == null)
                {
                    return L2H_Parser.GetSkillImage("");
                }
                if (string.IsNullOrEmpty(client_Skill.icon_name))
                {
                    return L2H_Parser.GetSkillImage("");
                }

                return L2H_Parser.GetSkillImage(client_Skill.icon_name);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //Properties
        public override string ToString()
        {
            return client_Skillname.name + " (ID: " + client_Skillname.id + ")";
        }

        public string Name_And_Level
        {
            get
            {
                return client_Skillname.name + "\nLvl: " + client_Skill.level + "\n(ID: " + client_Skillname.id + ")";
            }
        }
        public string Variant_Name
        {
            get
            {
                if (string.IsNullOrEmpty(client_Skillname.desc_add1))
                    return client_Skillname.name + "\nOriginal";
                else
                    return client_Skillname.name + "\nEnchanted: " + client_Skillname.desc_add1;
            }
        }

        public string ID
        {
            get
            {
                return client_Skillname.id;
            }
        }
        public string Skill_Name
        {
            get
            {
                if (client_Skillname == null)
                    return "";
                else
                    return client_Skillname.name;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Name", client_Skillname.name, value);
                client_Skillname.name = value;
            }
        }
        public string Skill_Description
        {
            get
            {
                return client_Skillname.description;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Description", client_Skillname.description, value);
                client_Skillname.description = value;
            }
        }
        public string Skill_Description_Additional
        {
            get
            {
                return client_Skillname.desc_add1;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Additional Description", client_Skillname.desc_add1, value);
                client_Skillname.desc_add1 = value;
            }
        }
        public string Skill_Description_Additional2
        {
            get
            {
                return client_Skillname.desc_add2;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Additional Description 2", client_Skillname.desc_add2, value);
                client_Skillname.desc_add2 = value;
            }
        }
        public string Skill_ID
        {
            get
            {
                return client_Skill.id;
            }
        }
        public string Skill_Name_ID
        {
            get
            {
                return server_Skilldata.skill_name;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Name ID", server_Skilldata.skill_name, value);
                server_Skilldata.skill_name = value;
            }
        }
        public string Skill_Level
        {
            get
            {
                return server_Skilldata.level;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Level", server_Skilldata.level, value);
                server_Skilldata.level = value;
                client_Skillname.level = value;
                client_Skill.level = value;
            }
        }
        public int Skill_Client_Operate_Type
        {
            get
            {
                return int.Parse(client_Skill.oper_type);
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Client Operate Type", client_Skill.oper_type, value.ToString());
                client_Skill.oper_type = value.ToString(); //Holup
            }
        }
        public string Skill_Magic_Level
        {
            get
            {
                return server_Skilldata.magic_level;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Magic Level", server_Skilldata.magic_level, value);
                server_Skilldata.magic_level = value;
            }
        }
        public string Skill_Enchanted_Skill_ID
        {
            get
            {
                return client_Skill.ench_skill_id;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Enchanted Skill ID", client_Skill.ench_skill_id, value);
                client_Skill.ench_skill_id = value;
                server_Skillenchantdata.enchant_id = value;
            }
        }
        public bool Skill_Is_Attribute
        {
            get
            {
                if (string.IsNullOrEmpty(server_Skilldata.attribute))
                    return false;
                else
                    return true;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Using Attribute", "False", "True");
                    if (string.IsNullOrEmpty(server_Skilldata.attribute))
                        server_Skilldata.attribute = "attr_none;0";
                }
                else
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Using Attribute", "True", "False");
                    server_Skilldata.attribute = "";
                }


            }
        }
        public int Skill_Attribute_Type
        {
            get
            {
                if (!Skill_Is_Attribute)
                    return 0;

                string[] AttributeDetails = L2H_Parser.GetAttributeAndPercentage(server_Skilldata.attribute);
                return L2H_Parser.GetAttributeDropdownIndexFromString(AttributeDetails[0]);
            }
            set
            {
                if (Skill_Is_Attribute)
                {
                    string[] AttributeDetails = L2H_Parser.GetAttributeAndPercentage(server_Skilldata.attribute);

                    L2H_Log.Instance.Log_Skill_Change(this, "Attribute Type", AttributeDetails[0], L2H_Parser.GetAttributeDropdownStringFromIndex(value));

                    AttributeDetails[0] = L2H_Parser.GetAttributeDropdownStringFromIndex(value);

                    server_Skilldata.attribute = AttributeDetails[0] + ";" + AttributeDetails[1];
                }
            }
        }
        public string Skill_Attribute_Value
        {
            get
            {
                if (!Skill_Is_Attribute)
                    return "";

                string[] AttributeDetails = L2H_Parser.GetAttributeAndPercentage(server_Skilldata.attribute);
                return AttributeDetails[1];
            }
            set
            {
                string[] AttributeDetails = L2H_Parser.GetAttributeAndPercentage(server_Skilldata.attribute);

                L2H_Log.Instance.Log_Skill_Change(this, "Attribute Value", AttributeDetails[1], value);
                AttributeDetails[1] = value;


                AttributeDetails[1] = value;
                server_Skilldata.attribute = AttributeDetails[0] + ";" + AttributeDetails[1];
            }

        }
        public string Skill_Character_Animation
        {
            get
            {
                return client_Skill.ani_char;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Character Animation", client_Skill.ani_char, value);
                client_Skill.ani_char = value;
            }
        }
        public string Skill_Cast_Style
        {
            get
            {
                return client_Skill.cast_style;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Cast Style", client_Skill.cast_style, value);
                client_Skill.cast_style = value;
            }
        }
        public string Skill_Desc
        {
            get
            {
                return client_Skill.desc;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Caster Animation Skill Effect", client_Skill.desc, value);
                client_Skill.desc = value;
            }
        }
        public string Skill_Abnormal_Visual_Effect
        {
            get
            {
                return server_Skilldata.abnormal_visual_effect;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Target Animation Skill Effect", server_Skilldata.abnormal_visual_effect, value);
                server_Skilldata.abnormal_visual_effect = value;
            }
        }
        public string Skill_Icon1
        {
            get
            {
                return client_Skill.icon_name;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Icon 1", client_Skill.icon_name, value);
                client_Skill.icon_name = value;
            }
        }
        public string Skill_Icon2
        {
            get
            {
                return client_Skill.icon_name2;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Icon 2", client_Skill.icon_name2, value);
                client_Skill.icon_name2 = value;
            }
        }
        public int Skill_Server_Operate_Type
        {
            get
            {
                return L2H_Parser.Get_Server_Operate_Type_Index_From_Value(server_Skilldata.operate_type);
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Server Operate Type", server_Skilldata.operate_type, L2H_Parser.Get_Server_Operate_Type_Value_From_Index(value));
                server_Skilldata.operate_type = L2H_Parser.Get_Server_Operate_Type_Value_From_Index(value);
            }
        }
        public string Skill_Start_Effect
        {
            get
            {
                return server_Skilldata.start_effect;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Start Effect", server_Skilldata.start_effect, value);
                server_Skilldata.start_effect = value;
            }
        }
        public string Skill_Effect
        {
            get
            {
                return server_Skilldata.effect;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Effect", server_Skilldata.effect, value);
                server_Skilldata.effect = value;
            }
        }
        public string Skill_Self_Effect
        {
            get
            {
                return server_Skilldata.self_effect;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Self Effect", server_Skilldata.self_effect, value);
                server_Skilldata.self_effect = value;
            }
        }
        public string Skill_Tick_Effect
        {
            get
            {
                return server_Skilldata.tick_effect;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Tick Effect", server_Skilldata.tick_effect, value);
                server_Skilldata.tick_effect = value;
            }
        }
        public string Skill_Attached_Skill
        {
            get
            {
                return server_Skilldata.attached_skill;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Attached Skill", server_Skilldata.attached_skill, value);
                server_Skilldata.attached_skill = value;
            }
        }
        public string Skill_Tick_Interval
        {
            get
            {
                return server_Skilldata.tick_interval;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Tick Interval", server_Skilldata.tick_interval, value);
                server_Skilldata.tick_interval = value;
                OnPropertyChanged();
            }
        }
        public string Skill_End_Effect
        {
            get
            {
                return server_Skilldata.end_effect;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "End Effect", server_Skilldata.end_effect, value);
                server_Skilldata.end_effect = value;
            }
        }
        public string Skill_PVP_Effect
        {
            get
            {
                return server_Skilldata.pvp_effect;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "PVP Effect", server_Skilldata.pvp_effect, value);
                server_Skilldata.pvp_effect = value;
            }
        }
        public string Skill_PVE_Effect
        {
            get
            {
                return server_Skilldata.pve_effect;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "PVE Effect", server_Skilldata.pve_effect, value);
                server_Skilldata.pve_effect = value;
            }
        }
        public string Skill_MP_Cost1
        {
            get
            {
                return server_Skilldata.mp_consume1;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "MP Cost (Initial)", server_Skilldata.mp_consume1, value);
                server_Skilldata.mp_consume1 = value;
                OnPropertyChanged();
            }
        }
        public string Skill_MP_Cost2
        {
            get
            {
                return server_Skilldata.mp_consume2;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "MP Cost (Finished)", server_Skilldata.mp_consume2, value);
                server_Skilldata.mp_consume2 = value;
                OnPropertyChanged();
            }
        }
        public string Skill_MP_Cost_Tick
        {
            get
            {
                return server_Skilldata.mp_consume_tick;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "MP Cost Tick", server_Skilldata.mp_consume_tick, value);
                server_Skilldata.mp_consume_tick = value;
                OnPropertyChanged();
            }
        }
        public string Skill_HP_Cost
        {
            get
            {
                return server_Skilldata.hp_consume;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "HP Cost Tick", server_Skilldata.hp_consume, value);
                server_Skilldata.hp_consume = value;
                OnPropertyChanged();
            }
        }
        public string Skill_Item_Cost
        {
            get
            {
                return server_Skilldata.item_consume;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Item Cost", server_Skilldata.item_consume, value);
                server_Skilldata.item_consume = value;
            }
        }
        public string Skill_Etc_Cost
        {
            get
            {
                return server_Skilldata.consume_etc;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "ETC Cost", server_Skilldata.consume_etc, value);
                server_Skilldata.consume_etc = value;
            }
        }
        public string Skill_Cast_Range
        {
            get
            {
                return server_Skilldata.cast_range;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Cast Range", server_Skilldata.cast_range, value);
                server_Skilldata.cast_range = value;
                client_Skill.cast_range = value;
                OnPropertyChanged();
            }
        }
        public string Skill_Cast_Effective_Range
        {
            get
            {
                return server_Skilldata.effective_range;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Effective Range", server_Skilldata.effective_range, value);
                server_Skilldata.effective_range = value;
                OnPropertyChanged();
            }
        }
        public string Skill_Cast_Time
        {
            get
            {
                return server_Skilldata.skill_hit_time;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Cast Time", server_Skilldata.skill_hit_time, value);
                server_Skilldata.skill_hit_time = value;
                client_Skill.hit_time = value;
                OnPropertyChanged();
            }
        }
        public string Skill_Cast_Cool_Time
        {
            get
            {
                return server_Skilldata.skill_cool_time;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Cool Time", server_Skilldata.skill_cool_time, value);
                server_Skilldata.skill_cool_time = value;
                OnPropertyChanged();
            }
        }
        public string Skill_Hit_Cancel_Time
        {
            get
            {
                return server_Skilldata.skill_hit_cancel_time;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Cancel Time", server_Skilldata.skill_hit_cancel_time, value);
                server_Skilldata.skill_hit_cancel_time = value;
            }
        }
        public string Skill_Hit_Rate
        {
            get
            {
                return server_Skilldata.activate_rate;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Hit Rate", server_Skilldata.activate_rate, value);
                server_Skilldata.activate_rate = value;
            }
        }
        public string Skill_Lv_Bonus_Rate
        {
            get
            {
                return server_Skilldata.lv_bonus_rate;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Level Bonus Rate", server_Skilldata.lv_bonus_rate, value);
                server_Skilldata.lv_bonus_rate = value;
            }
        }
        public string Skill_Reuse_Delay
        {
            get
            {
                return server_Skilldata.reuse_delay;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Reuse Delay", server_Skilldata.reuse_delay, value);
                server_Skilldata.reuse_delay = value;
                OnPropertyChanged();
            }
        }
        public string Skill_Operate_Condition
        {
            get
            {
                return server_Skilldata.operate_cond;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Operate Condition", server_Skilldata.operate_cond, value);
                server_Skilldata.operate_cond = value;
            }
        }
        public string Skill_Target_Operate_Condition
        {
            get
            {
                return server_Skilldata.target_operate_cond;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Target Operate Condition", server_Skilldata.target_operate_cond, value);
                server_Skilldata.target_operate_cond = value;
            }
        }
        public string Skill_Passive_Conditions
        {
            get
            {
                return server_Skilldata.passive_conditions;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Passive Condition", server_Skilldata.passive_conditions, value);
                server_Skilldata.passive_conditions = value;
            }
        }
        public string Skill_Basic_Property
        {
            get
            {
                return server_Skilldata.basic_property;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Basic Property", server_Skilldata.basic_property, value);
                server_Skilldata.basic_property = value;
            }
        }
        public string Skill_Ride_State
        {
            get
            {
                return server_Skilldata.ride_state;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Ride State", server_Skilldata.ride_state, value);
                server_Skilldata.ride_state = value;
            }
        }

        public int Skill_Target_Type
        {
            get
            {
                return L2H_Parser.Get_Skill_Target_Type_Index_From_Value(server_Skilldata.target_type);
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Target Type", server_Skilldata.target_type, L2H_Parser.Get_Skill_Target_Type_Value_From_Index(value));
                server_Skilldata.target_type = L2H_Parser.Get_Skill_Target_Type_Value_From_Index(value);
            }
        }
        public int Skill_Affect_Object
        {
            get
            {
                return L2H_Parser.Get_Skill_Affect_Object_Index_From_Value(server_Skilldata.affect_object);
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Affect Object", server_Skilldata.affect_object, L2H_Parser.Get_Skill_Affect_Object_Value_From_Index(value));
                server_Skilldata.affect_object = L2H_Parser.Get_Skill_Affect_Object_Value_From_Index(value);
            }
        }
        public int Skill_Affect_Scope
        {
            get
            {
                return L2H_Parser.Get_Skill_Affect_Scope_Index_From_Value(server_Skilldata.affect_scope);
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Affect Scope", server_Skilldata.affect_scope, L2H_Parser.Get_Skill_Affect_Scope_Value_From_Index(value));
                server_Skilldata.affect_scope = L2H_Parser.Get_Skill_Affect_Scope_Value_From_Index(value);
            }
        }
        public string Skill_Effect_Point
        {
            get
            {
                return server_Skilldata.effect_point;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Aggro Generation", server_Skilldata.effect_point, value);
                server_Skilldata.effect_point = value;
                OnPropertyChanged();
            }
        }
        public string Skill_Affect_Range
        {
            get
            {
                return server_Skilldata.affect_range;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Affect Range", server_Skilldata.affect_range, value);
                server_Skilldata.affect_range = value;
                OnPropertyChanged();
            }
        }
        public string Skill_Fan_Range
        {
            get
            {
                return server_Skilldata.fan_range;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Fan Range", server_Skilldata.fan_range, value);
                server_Skilldata.fan_range = value;
            }
        }
        public string Skill_Scope_Height
        {
            get
            {
                return server_Skilldata.affect_scope_height;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Scope Height", server_Skilldata.affect_scope_height, value);
                server_Skilldata.affect_scope_height = value;
            }
        }
        public string Skill_Affect_Limit
        {
            get
            {
                return server_Skilldata.affect_limit;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Affect Limit", server_Skilldata.affect_limit, value);
                server_Skilldata.affect_limit = value;
            }
        }
        public string Skill_Abnormal_Type
        {
            get
            {
                return server_Skilldata.abnormal_type;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Abnormal Type", server_Skilldata.abnormal_type, value);
                server_Skilldata.abnormal_type = value;
            }
        }
        public string Skill_Abnormal_Time
        {
            get
            {
                return server_Skilldata.abnormal_time;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Abnormal Time", server_Skilldata.abnormal_time, value);
                server_Skilldata.abnormal_time = value;
                OnPropertyChanged();
            }
        }
        public string Skill_Abnormal_Trait
        {
            get
            {
                return server_Skilldata.trait;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Trait", server_Skilldata.trait, value);
                server_Skilldata.trait = value;
            }
        }
        public string Skill_Abnormal_Level
        {
            get
            {
                return server_Skilldata.abnormal_lv;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Abnormal Level", server_Skilldata.abnormal_lv, value);
                server_Skilldata.abnormal_lv = value;
            }
        }
        public string Skill_Buff_Protect_Level
        {
            get
            {
                return server_Skilldata.buff_protect_level;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Buff Protect Level", server_Skilldata.buff_protect_level, value);
                server_Skilldata.buff_protect_level = value;
            }
        }
        public string Skill_Next_Action
        {
            get
            {
                return server_Skilldata.next_action;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Next Action", server_Skilldata.next_action, value);
                server_Skilldata.next_action = value;
            }
        }
        public string Skill_Transform_Type
        {
            get
            {
                return server_Skilldata.transform_type;
            }
            set
            {
                L2H_Log.Instance.Log_Skill_Change(this, "Transform Type", server_Skilldata.transform_type, value);
                server_Skilldata.transform_type = value;
            }
        }
        public string Skill_Enchanted_Enchant_ID ///hmmmm
        {
            get
            {
                if (!Skill_Is_Enchanted)
                    return "";
                //return "TBD";
                return server_Skillenchantdata.enchant_id;
            }
        }
        public string Skill_Enchanted_Original_Skill
        {
            get
            {
                if (!Skill_Is_Enchanted)
                    return "";
                //return "TBD";
                return server_Skillenchantdata.original_skill;
            }
        }
        public string Skill_Enchanted_Route_ID
        {
            get
            {
                if (!Skill_Is_Enchanted)
                    return "";
                //return "TBD";
                return server_Skillenchantdata.route_id;
            }
        }
        public string Skill_Enchanted_Skill_Level
        {
            get
            {
                if (!Skill_Is_Enchanted)
                    return "";
                //return "TBD";
                return server_Skillenchantdata.skill_level;
            }
        }
        public string Skill_Enchanted_Importance
        {
            get
            {
                if (!Skill_Is_Enchanted)
                    return "";
                //return "TBD";
                return server_Skillenchantdata.importance;
            }
        }
        public string Skill_Enchanted_Required_Item
        {
            get
            {
                if (!Skill_Is_Enchanted)
                    return "";
                //return "TBD";
                return server_Skillenchantdata.required_item;
            }
            set
            {
                server_Skillenchantdata.required_item = value;
            }
        }

        public string Skill_Enchanted_Adena_Cost
        {
            get
            {
                return "TBD";
                //return server_Skillenchantcost.adenaCosts.ToString();
            }
            set
            {
                //server_Skillenchantcost.adenaCosts = value.ToString();
            }
        }
        public string Skill_Enchanted_SP_Cost
        {
            get
            {
                return "TBD";
                //return server_Skillenchantdata.required_item;
            }
            set
            {
                //server_Skillenchantdata.required_item = value;
            }
        }

        public bool Skill_Is_Magic
        {
            get
            {
                if (server_Skilldata.is_magic == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Magic Skill", "False", "True");
                    server_Skilldata.is_magic = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Magic Skill", "True", "False");
                    server_Skilldata.is_magic = "0";
                }
            }
        }
        public bool Skill_Is_Enchanted
        {
            get
            {
                if (server_Skilldata.level.Length > 2)
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    client_Skill.is_ench = "1";
                else
                    client_Skill.is_ench = "0";
            }
        }
        public bool Skill_Reuse_Delay_Lock
        {
            get
            {
                if (server_Skilldata.reuse_delay_lock == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Reuse Delay Lock", "False", "True");
                    server_Skilldata.reuse_delay_lock = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Reuse Delay Lock", "True", "False");
                    server_Skilldata.reuse_delay_lock = "0";
                }
            }
        }
        public bool Skill_Is_Debuff
        {
            get
            {
                if (server_Skilldata.debuff == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Is Debuff", "False", "True");
                    server_Skilldata.debuff = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Is Debuff", "True", "False");
                    server_Skilldata.debuff = "0";
                }
            }
        }
        public bool Skill_Irreplacable_Buff
        {
            get
            {
                if (server_Skilldata.irreplaceable_buff == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Irreplacable", "False", "True");
                    server_Skilldata.irreplaceable_buff = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Irreplacable", "True", "False");
                    server_Skilldata.irreplaceable_buff = "0";
                }
            }
        }
        public bool Skill_Abnormal_Instant
        {
            get
            {
                if (server_Skilldata.abnormal_instant == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Abnormal Instant", "False", "True");
                    server_Skilldata.abnormal_instant = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Abnormal Instant", "True", "False");
                    server_Skilldata.abnormal_instant = "0";
                }
            }
        }
        public bool Skill_Abnormal_Delete_Leaveworld
        {
            get
            {
                if (server_Skilldata.abnormal_delete_leaveworld == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Remove Buff On Logout", "False", "True");
                    server_Skilldata.abnormal_delete_leaveworld = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Skill_Change(this, "Remove Buff On Logout", "True", "False");
                    server_Skilldata.abnormal_delete_leaveworld = "0";
                }
            }
        }



        public string GeneratePCHExportString()
        {
            long generatedSkillID = (int.Parse(ID) * 65536) + int.Parse(Skill_Level);
            if (Skill_Name_ID == "d2_affix_50000")
            {
                string assss = "sdfg";
            }
            return "[" + Skill_Name_ID + "]\t=\t" + generatedSkillID.ToString();
        }

        public string GeneratePCH2ExportString()
        {
            long pch_id;
            string cast_range;
            string hp_consume;
            string mps_combined;
            string target_type;
            string effect_point;
            string abnormal_type;
            string abnormal_lv;
            //string skill_hit_time;
            //string reuse_delay;
            string is_magic;
            string attribute;
            string endtext;

            pch_id = (int.Parse(server_Skilldata.skill_id) * 65536) + int.Parse(server_Skilldata.level);
            if (!string.IsNullOrEmpty(server_Skilldata.cast_range))
                cast_range = server_Skilldata.cast_range;
            else
                cast_range = "0";

            if (!string.IsNullOrEmpty(server_Skilldata.hp_consume))
                hp_consume = server_Skilldata.hp_consume;
            else
                hp_consume = "0";

            float mp_consume1 = 0;
            float mp_consume2 = 0;
            float mp_consume_tick = 0;

            if (!string.IsNullOrEmpty(server_Skilldata.mp_consume1))
                mp_consume1 = float.Parse(server_Skilldata.mp_consume1);

            if (!string.IsNullOrEmpty(server_Skilldata.mp_consume2))
                mp_consume2 = float.Parse(server_Skilldata.mp_consume2);

            if (!string.IsNullOrEmpty(server_Skilldata.mp_consume_tick))
                mp_consume_tick = float.Parse(server_Skilldata.mp_consume_tick);

            mps_combined = (mp_consume1 + mp_consume2 + mp_consume_tick).ToString();

            target_type = Get_PCH2_Target_Type();

            if (!string.IsNullOrEmpty(server_Skilldata.effect_point))
                effect_point = server_Skilldata.effect_point;
            else
                effect_point = "0";

            abnormal_type = Get_PCH2_Abnormal();

            if (!string.IsNullOrEmpty(server_Skilldata.abnormal_lv))
                abnormal_lv = server_Skilldata.abnormal_lv;
            else
                abnormal_lv = "0";

            double skill_cool_time = 0;
            if (!string.IsNullOrEmpty(server_Skilldata.skill_cool_time))
                skill_cool_time = double.Parse(server_Skilldata.skill_cool_time);

            double skill_hit_cancel_time = 0;
            if (!string.IsNullOrEmpty(server_Skilldata.skill_hit_cancel_time))
                skill_hit_cancel_time = double.Parse(server_Skilldata.skill_hit_cancel_time);

            double skill_hit_time = 0;
            double skill_hit_time_temp = 0;
            if (!string.IsNullOrEmpty(server_Skilldata.skill_hit_time))
            {

                skill_hit_time = double.Parse(server_Skilldata.skill_hit_time);
                skill_hit_time_temp = double.Parse(server_Skilldata.skill_hit_time);
                double i = skill_hit_time + skill_cool_time;


                if ((i - (int)i) >= 0.5)
                {

                    skill_hit_time = Math.Ceiling(i);

                }
                else
                {
                    skill_hit_time = (int)i;
                }
            }


            double reuse_delay = 0;
            if (!string.IsNullOrEmpty(server_Skilldata.reuse_delay))
            {
                reuse_delay = double.Parse(server_Skilldata.reuse_delay);

                double f = reuse_delay - (skill_hit_time_temp + skill_cool_time);

                if ((Math.Abs(f) - Math.Abs((int)(f))) >= 0.5)
                {
                    if (f >= 0)
                    {
                        reuse_delay = Math.Ceiling(f);
                    }

                    else
                    {
                        reuse_delay = Math.Floor(f);
                    }
                }
                else
                {
                    reuse_delay = (int)f;
                }

                if (reuse_delay < 0)
                {
                    reuse_delay = reuse_delay + 1;
                }
            }

            is_magic = "0";
            if (!string.IsNullOrEmpty(server_Skilldata.is_magic))
                is_magic = server_Skilldata.is_magic;


            attribute = Get_PCH2_Attribute();
            endtext = "-12345";

            return pch_id + " " + cast_range + " " + hp_consume + " " + mps_combined + " " + target_type + " " + effect_point + " " + abnormal_type + " " +
                   abnormal_lv + " " + (int)Math.Ceiling(skill_hit_time) + " " + (int)reuse_delay + " " + is_magic + " " + attribute + " " + endtext;

        }

        string Get_PCH2_Target_Type()
        {
            switch (server_Skilldata.target_type)
            {

                case "none": { return "0"; } break;

                case "self": { return "0"; } break;

                case "target": { return "1"; } break;

                case "others": { return "2"; } break;

                case "my_party": { return "3"; } break;

                case "enemy": { return "4"; } break;

                case "enemy_only": { return "5"; } break;

                case "item": { return "6"; } break;

                case "summon": { return "7"; } break;

                case "holything": { return "8"; } break;

                case "my_pledge": { return "9"; } break;

                case "door_treasure": { return "10"; } break;

                case "pc_body": { return "11"; } break;

                case "npc_body": { return "12"; } break;

                case "wyvern_target": { return "13"; } break;

                case "ground": { return "14"; } break;

                case "fortress_flagpole": { return "16"; } break;

                case "artillery": { return "15"; } break;

                case "advance_base": { return " 18"; } break;

                case "enemy_not":
                    {
                        return " 20";
                    }
                    break;
                default:
                    {
                        return "0";
                    }

            }

        }

        string Get_PCH2_Attribute()
        {
            if (string.IsNullOrEmpty(server_Skilldata.attribute))
                return "-2";

            if (server_Skilldata.attribute.Contains("attr_none"))
                return "-2";
            else if (server_Skilldata.attribute.Contains("attr_fire"))
                return "0";
            else if (server_Skilldata.attribute.Contains("attr_water"))
                return "1";
            else if (server_Skilldata.attribute.Contains("attr_wind"))
                return "2";
            else if (server_Skilldata.attribute.Contains("attr_earth"))
                return "3";
            else if (server_Skilldata.attribute.Contains("attr_holy"))
                return "4";
            else if (server_Skilldata.attribute.Contains("attr_unholy"))
                return "5";
            else
                return "-2";

        }

        string Get_PCH2_Abnormal()
        {
            switch (server_Skilldata.abnormal_type)
            {
                case "none": { return "-1"; }

                case "ab_hawk_eye": { return "0"; }

                case "all_attack_down": { return "1"; }

                case "all_attack_up": { return "2"; }

                case "all_speed_down": { return "3"; }

                case "all_speed_up": { return "4"; }

                case "antaras_debuff": { return "5"; }

                case "armor_earth": { return "6"; }

                case "armor_fire": { return "7"; }

                case "armor_holy": { return "8"; }

                case "armor_unholy": { return "9"; }

                case "armor_water": { return "10"; }

                case "armor_wind": { return "11"; }

                case "attack_speed_up_bow": { return "12"; }

                case "attack_time_down": { return "13"; }

                case "attack_time_up": { return "14"; }

                case "avoid_down": { return "15"; }

                case "avoid_up": { return "16"; }

                case "avoid_up_special": { return "17"; }

                case "berserker": { return "18"; }

                case "big_body": { return "19"; }

                case "big_head": { return "20"; }

                case "bleeding": { return "21"; }

                case "bow_range_up": { return "22"; }

                case "buff_queen_of_cat": { return "23"; }

                case "buff_unicorn_seraphim": { return "24"; }

                case "cancel_prob_down": { return "25"; }

                case "casting_time_down": { return "26"; }

                case "casting_time_up": { return "27"; }

                case "cheap_magic": { return "28"; }

                case "critical_dmg_down": { return "29"; }

                case "critical_dmg_up": { return "30"; }

                case "critical_prob_down": { return "31"; }

                case "critical_prob_up": { return "32"; }

                case "dance_of_aqua_guard": { return "33"; }

                case "dance_of_concentration": { return "34"; }

                case "dance_of_earth_guard": { return "35"; }

                case "dance_of_fire": { return "36"; }

                case "dance_of_fury": { return "37"; }

                case "dance_of_inspiration": { return "38"; }

                case "dance_of_light": { return "39"; }

                case "dance_of_mystic": { return "40"; }

                case "dance_of_protection": { return "41"; }

                case "dance_of_shadow": { return "42"; }

                case "dance_of_siren": { return "43"; }

                case "dance_of_vampire": { return "44"; }

                case "dance_of_warrior": { return "45"; }

                case "debuff_nightshade": { return "46"; }

                case "debuff_shield": { return "47"; }

                case "decrease_weight_penalty": { return "48"; }

                case "derangement": { return "49"; }

                case "detect_weakness": { return "50"; }

                case "dmg_shield": { return "51"; }

                case "dot_attr": { return "52"; }

                case "dot_mp": { return "53"; }

                case "dragon_breath": { return "54"; }

                case "duelist_spirit": { return "55"; }

                case "fatal_poison": { return "56"; }

                case "fishing_mastery_down": { return "57"; }

                case "fly_away": { return "58"; }

                case "focus_dagger": { return "59"; }

                case "heal_effect_down": { return "60"; }

                case "heal_effect_up": { return "61"; }

                case "hero_buff": { return "62"; }

                case "hero_debuff": { return "63"; }

                case "hit_down": { return "64"; }

                case "hit_up": { return "65"; }

                case "holy_attack": { return "66"; }

                case "hp_recover": { return "67"; }

                case "hp_regen_down": { return "68"; }

                case "hp_regen_up": { return "69"; }

                case "life_force_orc": { return "70"; }

                case "life_force_others": { return "71"; }

                case "magic_critical_up": { return "72"; }

                case "majesty": { return "73"; }

                case "max_breath_up": { return "74"; }

                case "max_hp_down": { return "75"; }

                case "max_hp_up": { return "76"; }

                case "max_mp_up": { return "77"; }

                case "ma_down": { return "78"; }

                case "ma_up": { return "79"; }

                case "ma_up_herb": { return "80"; }

                case "md_down": { return "81"; }

                case "md_up": { return "82"; }

                case "md_up_attr": { return "83"; }

                case "might_mortal": { return "84"; }

                case "mp_cost_down": { return "85"; }

                case "mp_cost_up": { return "86"; }

                case "mp_recover": { return "87"; }

                case "mp_regen_up": { return "88"; }

                case "multi_buff": { return "89"; }

                case "multi_debuff": { return "90"; }

                case "paralyze": { return "91"; }

                case "pa_down": { return "92"; }

                case "pa_pd_up": { return "93"; }

                case "pa_up": { return "94"; }

                case "pa_up_herb": { return "95"; }

                case "pa_up_special": { return "96"; }

                case "pd_down": { return "97"; }

                case "pd_up": { return "98"; }

                case "pd_up_bow": { return "99"; }

                case "pd_up_special": { return "100"; }

                case "pinch": { return "101"; }

                case "poison": { return "102"; }

                case "polearm_attack": { return "103"; }

                case "possession": { return "104"; }

                case "preserve_abnormal": { return "105"; }

                case "public_slot": { return "106"; }

                case "rage_might": { return "107"; }

                case "reduce_drop_penalty": { return "108"; }

                case "reflect_abnormal": { return "109"; }

                case "resist_bleeding": { return "110"; }

                case "resist_debuff_dispel": { return "111"; }

                case "resist_derangement": { return "112"; }

                case "resist_holy_unholy": { return "113"; }

                case "resist_poison": { return "114"; }

                case "resist_shock": { return "115"; }

                case "resist_spiritless": { return "116"; }

                case "reuse_delay_down": { return "117"; }

                case "reuse_delay_up": { return "118"; }

                case "root_physically": { return "119"; }

                case "root_magically": { return "120"; }

                case "shield_defence_up": { return "121"; }

                case "shield_prob_up": { return "122"; }

                case "silence": { return "123"; }

                case "silence_all": { return "124"; }

                case "silence_physical": { return "125"; }

                case "sleep": { return "126"; }

                case "snipe": { return "127"; }

                case "song_of_champion": { return "128"; }

                case "song_of_earth": { return "129"; }

                case "song_of_flame_guard": { return "130"; }

                case "song_of_hunter": { return "131"; }

                case "song_of_invocation": { return "132"; }

                case "song_of_life": { return "133"; }

                case "song_of_meditation": { return "134"; }

                case "song_of_renewal": { return "135"; }

                case "song_of_storm_guard": { return "136"; }

                case "song_of_vengeance": { return "137"; }

                case "song_of_vitality": { return "138"; }

                case "song_of_warding": { return "139"; }

                case "song_of_water": { return "140"; }

                case "song_of_wind": { return "141"; }

                case "spa_disease_a": { return "142"; }

                case "spa_disease_b": { return "143"; }

                case "spa_disease_c": { return "144"; }

                case "spa_disease_d": { return "145"; }

                case "speed_down": { return "146"; }

                case "speed_up": { return "147"; }

                case "speed_up_special": { return "148"; }

                case "ssq_town_blessing": { return "149"; }

                case "ssq_town_curse": { return "150"; }

                case "stealth": { return "151"; }

                case "stun": { return "152"; }

                case "thrill_fight": { return "153"; }

                case "touch_of_death": { return "154"; }

                case "touch_of_life": { return "155"; }

                case "turn_flee": { return "156"; }

                case "turn_passive": { return "157"; }

                case "turn_stone": { return "158"; }

                case "ultimate_buff": { return "159"; }

                case "ultimate_debuff": { return "160"; }

                case "valakas_item": { return "161"; }

                case "vampiric_attack": { return "162"; }

                case "watcher_gaze": { return "163"; }

                case "resurrection_special": { return "164"; }

                case "counter_skill": { return "165"; }

                case "avoid_skill": { return "166"; }

                case "cp_up": { return "167"; }

                case "cp_down": { return "168"; }

                case "cp_regen_up": { return "169"; }

                case "cp_regen_down": { return "170"; }

                case "invincibility": { return "171"; }

                case "abnormal_invincibility": { return "172"; }

                case "physical_stance": { return "173"; }

                case "magical_stance": { return "174"; }

                case "combination": { return "175"; }

                case "anesthesia": { return "176"; }

                case "critical_poison": { return "177"; }

                case "seizure_penalty": { return "178"; }

                case "abnormal_item": { return "179"; }

                case "seizure_a": { return "180"; }

                case "seizure_b": { return "181"; }

                case "seizure_c": { return "182"; }

                case "force_meditation": { return "183"; }

                case "mirage": { return "184"; }

                case "potion_of_genesis": { return "185"; }

                case "pvp_dmg_up": { return "186"; }

                case "pvp_dmg_down": { return "187"; }

                case "iron_shield": { return "188"; }

                case "transfer_damage": { return "189"; }

                case "song_of_elemental": { return "190"; }

                case "dance_of_alignment": { return "191"; }

                case "archer_special": { return "192"; }

                case "spoil_bomb": { return "193"; }

                case "fire_dot": { return "194"; }

                case "water_dot": { return "195"; }

                case "wind_dot": { return "196"; }

                case "earth_dot": { return "197"; }

                case "heal_power_up": { return "198"; }

                case "recharge_up": { return "199"; }

                case "normal_attack_block": { return "200"; }

                case "disarm": { return "201"; }

                case "death_mark": { return "202"; }

                case "kamael_special": { return "203"; }

                case "transform": { return "204"; }

                case "dark_seed": { return "205"; }

                case "real_target": { return "206"; }

                case "freezing": { return "207"; }

                case "time_check": { return "208"; }

                case "ma_md_up": { return "209"; }

                case "death_clack": { return "210"; }

                case "hot_ground": { return "211"; }

                case "evil_blood": { return "212"; }

                case "all_regen_up": { return "213"; }

                case "all_regen_down": { return "214"; }

                case "iron_shield_i": { return "215"; }

                case "archer_special_i": { return "216"; }

                case "t_crt_rate_up": { return "217"; }

                case "t_crt_rate_down": { return "218"; }

                case "t_crt_dmg_up": { return "219"; }

                case "t_crt_dmg_down": { return "220"; }

                case "instinct": { return "221"; }

                case "oblivion": { return "222"; }

                case "weak_constitution": { return "223"; }

                case "thin_skin": { return "224"; }

                case "enervation": { return "225"; }

                case "spite": { return "226"; }

                case "mental_impoverish": { return "227"; }

                case "attribute_potion": { return "228"; }

                case "talisman": { return "229"; }

                case "multi_debuff_fire": { return "230"; }

                case "multi_debuff_water": { return "231"; }

                case "multi_debuff_wind": { return "232"; }

                case "multi_debuff_earth": { return "233"; }

                case "multi_debuff_holy": { return "234"; }

                case "multi_debuff_unholy": { return "235"; }

                case "life_force_kamael": { return "236"; }

                case "ma_up_special": { return "237"; }

                case "pk_protect": { return "238"; }

                case "maximum_ability": { return "239"; }

                case "target_lock": { return "240"; }

                case "protection": { return "241"; }

                case "will": { return "242"; }

                case "seed_of_knight": { return "243"; }

                case "expose_weak_point": { return "244"; }

                case "force_of_destruction": { return "245"; }

                case "elemental_armor": { return "246"; }

                case "summon_condition": { return "247"; }

                case "improve_pa_pd_up": { return "248"; }

                case "improve_ma_md_up": { return "249"; }

                case "improve_hp_mp_up": { return "250"; }

                case "improve_crt_rate_dmg_up": { return "251"; }

                case "improve_shield_rate_defence_up": { return "252"; }

                case "improve_speed_avoid_up": { return "253"; }

                case "limit": { return "254"; }

                case "multi_debuff_soul": { return "255"; }

                case "curse_life_flow": { return "256"; }

                case "betrayal_mark": { return "257"; }

                case "transform_hangover": { return "258"; }

                case "transform_scrifice": { return "259"; }

                case "song_of_windstorm": { return "260"; }

                case "dance_of_bladestorm": { return "261"; }

                case "improve_vampiric_haste": { return "262"; }

                case "weapon_mastery": { return "263"; }

                case "apella": { return "264"; }

                case "transform_scrifice_p": { return "265"; }

                case "sub_trigger_haste": { return "266"; }

                case "sub_trigger_defence": { return "267"; }

                case "sub_trigger_crt_rate_up": { return "268"; }

                case "sub_trigger_spirit": { return "269"; }

                case "mirage_trap": { return "270"; }

                case "death_penalty": { return "271"; }

                case "entry_for_game": { return "272"; }

                case "blood_constract": { return "273"; }

                case "dwarf_attack_buff": { return "274"; }

                case "dwarf_defence_buff": { return "275"; }

                case "evasion_buff": { return "276"; }

                case "bless_the_blood": { return "277"; }

                case "pvp_weapon_buff": { return "278"; }

                case "pvp_weapon_debuff": { return "279"; }

                case "seed_of_critical": { return "280"; }

                case "vp_up": { return "281"; }

                case "bot_penalty": { return "282"; }

                case "hide": { return "283"; }

                case "dd_resist": { return "284"; }

                case "song_of_purification": { return "285"; }

                case "dance_of_berserker": { return "286"; }

                case "reflect_magic_dd": { return "287"; }

                case "final_secret": { return "288"; }

                case "stigma_of_silen": { return "289"; }

                case "counter_critical": { return "301"; }

                case "counter_critical_trigger": { return "302"; }

                case "attack_time_down_special": { return "303"; }

                case "block_speed_up": { return "304"; }

                case "block_shield_up": { return "305"; }

                case "deathworm": { return "306"; }

                case "multi_debuff_a": { return "307"; }

                case "multi_debuff_b": { return "308"; }

                case "multi_debuff_c": { return "309"; }

                case "multi_debuff_d": { return "310"; }

                case "multi_debuff_e": { return "311"; }

                case "multi_debuff_f": { return "312"; }

                case "multi_debuff_g": { return "313"; }

                case "multi_buff_a": { return "315"; }

                case "stigma_a": { return "314"; }

                case "vampiric_attack_special": { return "316"; }

                case "block_resurrection": { return "317"; }

                case "improve_hit_defence_crt_rate_up": { return "318"; }

                case "event_gawi": { return "320"; }

                case "event_win": { return "323"; }

                case "event_territory": { return "324"; }

                case "event_santa_reward": { return "325"; }

                case "br_event_buf1": { return "326"; }

                case "br_event_buf2": { return "327"; }

                case "br_event_buf3": { return "328"; }

                case "dance_defence_motion1": { return "337"; }

                case "song_battle_whisper": { return "338"; }

                case "max_hp_cp_up": { return "339"; }

                case "signal_a": { return "340"; }

                case "signal_b": { return "341"; }

                case "signal_c": { return "342"; }

                case "signal_d": { return "343"; }

                case "signal_e": { return "344"; }

                case "soa_buff1": { return "345"; }

                case "soa_buff2": { return "346"; }

                case "soa_buff3": { return "347"; }

                case "damage_amplify": { return "348"; }

                case "vibration": { return "349"; }

                case "block_transform": { return "350"; }

                case "skill_ignore": { return "351"; }

                case "wp_change_event": { return "352"; }

                case "knight_aura": { return "353"; }

                case "patience": { return "354"; }

                case "vote": { return "355"; }

                case "vp_keep": { return "356"; }

                case "dragon_buff": { return "357"; }

                case "transfer_damage_mp": { return "358"; }

                case "max": { return "359"; }

                case "multi_elements": { return "83"; }

                default: { return "-1"; }

            }
        }

    }
}
