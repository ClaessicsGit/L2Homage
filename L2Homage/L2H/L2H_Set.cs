using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Set
    {
        public L2H_Item slot_chest;
        public L2H_Item slot_head_A;
        public L2H_Item slot_legs_A;
        public L2H_Item slot_gloves_A;
        public L2H_Item slot_feet_A;
        public L2H_Item slot_additional_A;
        public L2H_Item slot_head_B;
        public L2H_Item slot_legs_B;
        public L2H_Item slot_gloves_B;
        public L2H_Item slot_feet_B;
        public L2H_Item slot_additional_B;

        public int ID { get; set; }
        public string template { get; set; }
        public string name_ID { get; set; }
        public string Name { get; set; }
        public string additional_slot_type { get; set; }
        public bool IsSelected { get; set; }
        public Server_Itemdata server_Itemdata { get; set; }
        public Client_Itemname client_Itemname { get; set; }


        public L2H_Item Slot_chest
        {
            get => slot_chest;
            set
            {
                if (slot_chest != null)
                {
                    if (value == null)
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Chest", slot_chest.Item_Name + " (" + slot_chest.ID + ")", "No Chest Assigned");
                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Chest", slot_chest.Item_Name + " (" + slot_chest.ID + ")", (value.Item_Name) + " (" + value.ID + ")");
                    }

                }
                else
                {
                    if (value == null)
                    {

                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Chest", "No Chest Assigned", (value.Item_Name) + " (" + value.ID + ")");
                    }
                }
                slot_chest = value;
                UpdateServer_Itemdata();
            }
        }
        public L2H_Item Slot_head_A
        {
            get => slot_head_A;
            set
            {
                if (slot_head_A != null)
                {
                    if (value == null)
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Head A", slot_head_A.Item_Name + " (" + slot_head_A.ID + ")", "No Head A Assigned");
                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Head A", slot_head_A.Item_Name + " (" + slot_head_A.ID + ")", (value.Item_Name) + " (" + value.ID + ")");
                    }

                }
                else
                {
                    if (value == null)
                    {

                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Head A", "No Head A Assigned", (value.Item_Name) + " (" + value.ID + ")");
                    }
                }
                slot_head_A = value;
                UpdateServer_Itemdata();
            }
        }
        public L2H_Item Slot_legs_A
        {
            get => slot_legs_A;
            set
            {
                if (slot_legs_A != null)
                {
                    if (value == null)
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Legs A", slot_legs_A.Item_Name + " (" + slot_legs_A.ID + ")", "No Legs A Assigned");
                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Legs A", slot_legs_A.Item_Name + " (" + slot_legs_A.ID + ")", (value.Item_Name) + " (" + value.ID + ")");
                    }

                }
                else
                {
                    if (value == null)
                    {

                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Legs A", "No Legs A Assigned", (value.Item_Name) + " (" + value.ID + ")");
                    }
                }
                slot_legs_A = value;
                UpdateServer_Itemdata();
            }
        }
        public L2H_Item Slot_gloves_A
        {
            get => slot_gloves_A;
            set
            {
                if (slot_gloves_A != null)
                {
                    if (value == null)
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Gloves A", slot_gloves_A.Item_Name + " (" + slot_gloves_A.ID + ")", "No Gloves A Assigned");
                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Gloves A", slot_gloves_A.Item_Name + " (" + slot_gloves_A.ID + ")", (value.Item_Name) + " (" + value.ID + ")");
                    }

                }
                else
                {
                    if (value == null)
                    {

                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Gloves A", "No Gloves A Assigned", (value.Item_Name) + " (" + value.ID + ")");
                    }
                }
                slot_gloves_A = value;
                UpdateServer_Itemdata();
            }
        }
        public L2H_Item Slot_feet_A
        {
            get => slot_feet_A;
            set
            {
                if (slot_feet_A != null)
                {
                    if (value == null)
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Feet A", slot_feet_A.Item_Name + " (" + slot_feet_A.ID + ")", "No Feet A Assigned");
                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Feet A", slot_feet_A.Item_Name + " (" + slot_feet_A.ID + ")", (value.Item_Name) + " (" + value.ID + ")");
                    }

                }
                else
                {
                    if (value == null)
                    {

                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Feet A", "No Feet A Assigned", (value.Item_Name) + " (" + value.ID + ")");
                    }
                }
                slot_feet_A = value;
                UpdateServer_Itemdata();
            }
        }
        public L2H_Item Slot_additional_A
        {
            get => slot_additional_A;
            set
            {
                if (slot_additional_A != null)
                {
                    if (value == null)
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Additional A", slot_additional_A.Item_Name + " (" + slot_additional_A.ID + ")", "No Additional A Assigned");
                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Additional A", slot_additional_A.Item_Name + " (" + slot_additional_A.ID + ")", (value.Item_Name) + " (" + value.ID + ")");
                    }

                }
                else
                {
                    if (value == null)
                    {

                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Additional A", "No Additional A Assigned", (value.Item_Name) + " (" + value.ID + ")");
                    }
                }
                slot_additional_A = value;
                UpdateServer_Itemdata();
            }
        }

        public L2H_Item Slot_head_B
        {
            get => slot_head_B;
            set
            {
                if (slot_head_B != null)
                {
                    if (value == null)
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Head B", slot_head_B.Item_Name + " (" + slot_head_B.ID + ")", "No Head B Assigned");
                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Head B", slot_head_B.Item_Name + " (" + slot_head_B.ID + ")", (value.Item_Name) + " (" + value.ID + ")");
                    }

                }
                else
                {
                    if (value == null)
                    {

                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Head B", "No Head B Assigned", (value.Item_Name) + " (" + value.ID + ")");
                    }
                }
                slot_head_B = value;
                UpdateServer_Itemdata();
            }
        }
        public L2H_Item Slot_legs_B
        {
            get => slot_legs_B;
            set
            {
                if (slot_legs_B != null)
                {
                    if (value == null)
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Legs B", slot_legs_B.Item_Name + " (" + slot_legs_B.ID + ")", "No Legs B Assigned");
                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Legs B", slot_legs_B.Item_Name + " (" + slot_legs_B.ID + ")", (value.Item_Name) + " (" + value.ID + ")");
                    }

                }
                else
                {
                    if (value == null)
                    {

                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Legs B", "No Legs B Assigned", (value.Item_Name) + " (" + value.ID + ")");
                    }
                }
                slot_legs_B = value;
                UpdateServer_Itemdata();
            }
        }
        public L2H_Item Slot_gloves_B
        {
            get => slot_gloves_B;
            set
            {
                if (slot_gloves_B != null)
                {
                    if (value == null)
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Gloves B", slot_gloves_B.Item_Name + " (" + slot_gloves_B.ID + ")", "No Gloves B Assigned");
                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Gloves B", slot_gloves_B.Item_Name + " (" + slot_gloves_B.ID + ")", (value.Item_Name) + " (" + value.ID + ")");
                    }

                }
                else
                {
                    if (value == null)
                    {

                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Gloves B", "No Gloves B Assigned", (value.Item_Name) + " (" + value.ID + ")");
                    }
                }
                slot_gloves_B = value;
                UpdateServer_Itemdata();
            }
        }
        public L2H_Item Slot_feet_B
        {
            get => slot_feet_B;
            set
            {
                if (slot_feet_B != null)
                {
                    if (value == null)
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Feet B", slot_feet_B.Item_Name + " (" + slot_feet_B.ID + ")", "No Feet B Assigned");
                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Feet B", slot_feet_B.Item_Name + " (" + slot_feet_B.ID + ")", (value.Item_Name) + " (" + value.ID + ")");
                    }

                }
                else
                {
                    if (value == null)
                    {

                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Feet B", "No Feet B Assigned", (value.Item_Name) + " (" + value.ID + ")");
                    }
                }
                slot_feet_B = value;
                UpdateServer_Itemdata();
            }
        }
        public L2H_Item Slot_additional_B
        {
            get => slot_additional_B;
            set
            {
                if (slot_additional_B != null)
                {
                    if (value == null)
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Additional B", slot_additional_B.Item_Name + " (" + slot_additional_B.ID + ")", "No Additional B Assigned");
                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Additional B", slot_additional_B.Item_Name + " (" + slot_additional_B.ID + ")", (value.Item_Name) + " (" + value.ID + ")");
                    }

                }
                else
                {
                    if (value == null)
                    {

                    }
                    else
                    {
                        L2H_Log.Instance.Log_Set_Change(this, "Set Additional B", "No Additional B Assigned", (value.Item_Name) + " (" + value.ID + ")");
                    }
                }
                slot_additional_B = value;                 
                UpdateServer_Itemdata();
            }
        }

        public L2H_Set Instance { get { return this; } }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
                return "(ID: " + ID + ")";
            else
                return Name + "\n(ID: " + ID + ")";
        }

        #region Properties

        public string Set_ID
        {
            get
            {
                return server_Itemdata.set_id;
            }
            set
            {
                server_Itemdata.set_id = value;
            }
        }

        public string Set_Name
        {
            get
            {
                return "TBD";
            }

        }
        public string Set_Skill
        {
            get
            {
                return server_Itemdata.set_skill;
            }
            set
            {
                server_Itemdata.set_skill = value;
                L2H_Log.Instance.Log_Set_Change(this, "Set Skill", server_Itemdata.set_skill, value);
            }
        }
        public string Set_Effect_Skill
        {
            get
            {
                return server_Itemdata.effect_skill;
            }
            set
            {
                L2H_Log.Instance.Log_Set_Change(this, "Set Effect Skill", server_Itemdata.effect_skill, value);
                server_Itemdata.effect_skill = value;
            }
        }
        public string Set_Effect_Skill_Additional
        {
            get
            {
                return server_Itemdata.additional_effect_skill;
            }
            set
            {
                L2H_Log.Instance.Log_Set_Change(this, "Set Effect Skill Additional", server_Itemdata.additional_effect_skill, value);
                server_Itemdata.additional_effect_skill = value;
            }
        }
        public string Set_Skill_Additional2
        {
            get
            {
                return server_Itemdata.additional2_effect_skill;
            }
            set
            {
                L2H_Log.Instance.Log_Set_Change(this, "Set Enchanted Skill", server_Itemdata.additional2_effect_skill, value);
                server_Itemdata.additional2_effect_skill = value;
            }
        }
        public string Set_Skill_Additional2_Condition
        {
            get
            {
                return server_Itemdata.additional2_condition;
            }
            set
            {
                L2H_Log.Instance.Log_Set_Change(this, "Set Enchanted Skill Requirement #", server_Itemdata.additional2_condition, value);
                server_Itemdata.additional2_condition = value;
                client_Itemname.special_enchant_amount = value;
            }
        }
        public string Set_Description
        {
            get
            {
                if (client_Itemname == null)
                    return "No description";
                return client_Itemname.set_bonus_desc;
            }
            set
            {
                L2H_Log.Instance.Log_Set_Change(this, "Set Description", client_Itemname.set_bonus_desc, value);
                client_Itemname.set_bonus_desc = value;
            }
        }
        public string Set_Description_Additional
        {
            get
            {
                if (client_Itemname == null)
                    return "No Additional description";
                return client_Itemname.set_extra_desc;
            }
            set
            {
                L2H_Log.Instance.Log_Set_Change(this, "Set Description Additional", client_Itemname.set_extra_desc, value);
                client_Itemname.set_extra_desc = value;
            }
        }
        public string Set_Description_Additional2
        {
            get
            {
                if (client_Itemname == null)
                    return "No Enchanted description";
                return client_Itemname.special_enchant_desc;
            }
            set
            {
                L2H_Log.Instance.Log_Set_Change(this, "Set Enchanted Description", client_Itemname.special_enchant_desc, value);
                client_Itemname.special_enchant_desc = value;
            }
        }

        public string Set_Bonus_STR
        {
            get
            {
                return server_Itemdata.str_inc;
            }
            set
            {
                L2H_Log.Instance.Log_Set_Change(this, "Set STR Bonus", server_Itemdata.str_inc, value);
                server_Itemdata.str_inc = value;
            }
        }
        public string Set_Bonus_CON
        {
            get
            {
                return server_Itemdata.con_inc;
            }
            set
            {
                L2H_Log.Instance.Log_Set_Change(this, "Set CON Bonus", server_Itemdata.con_inc, value);
                server_Itemdata.con_inc = value;
            }
        }
        public string Set_Bonus_DEX
        {
            get
            {
                return server_Itemdata.dex_inc;
            }
            set
            {
                L2H_Log.Instance.Log_Set_Change(this, "Set DEX Bonus", server_Itemdata.dex_inc, value);
                server_Itemdata.dex_inc = value;
            }
        }
        public string Set_Bonus_INT
        {
            get
            {
                return server_Itemdata.int_inc;
            }
            set
            {
                L2H_Log.Instance.Log_Set_Change(this, "Set INT Bonus", server_Itemdata.int_inc, value);
                server_Itemdata.int_inc = value;
            }
        }
        public string Set_Bonus_MEN
        {
            get
            {
                return server_Itemdata.men_inc;
            }
            set
            {
                L2H_Log.Instance.Log_Set_Change(this, "Set MEN Bonus", server_Itemdata.men_inc, value);
                server_Itemdata.men_inc = value;
            }
        }
        public string Set_Bonus_WIT
        {
            get
            {
                return server_Itemdata.wit_inc;
            }
            set
            {
                L2H_Log.Instance.Log_Set_Change(this, "Set WIT Bonus", server_Itemdata.wit_inc, value);
                server_Itemdata.wit_inc = value;
            }
        }


        #endregion

        void UpdateServer_Itemdata()
        {
            if (slot_chest != null)
                server_Itemdata.slot_chest = slot_chest.Item_ID;

            server_Itemdata.slot_head = ServerSlotExportString(slot_head_A, slot_head_B);
            server_Itemdata.slot_legs = ServerSlotExportString(slot_legs_A, slot_legs_B);
            server_Itemdata.slot_gloves = ServerSlotExportString(slot_gloves_A, slot_gloves_B);
            server_Itemdata.slot_feet = ServerSlotExportString(slot_feet_A, slot_feet_B);
            server_Itemdata.slot_additional = ServerSlotExportString(slot_additional_A, slot_additional_B);
        }

        string ServerSlotExportString(L2H_Item slotA, L2H_Item slotB)
        {
            string returnString = "";

            if (slotA != null)
            {
                returnString = slotA.Item_ID;
                if (slotB != null)
                    returnString += ";" + slotB.Item_ID;
            }
            else
            {
                if (slotB != null)
                    returnString = slotB.Item_ID;
            }



            return returnString;

        }


    }
}
