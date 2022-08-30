using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Server_Itemdata
    {
        // There are two data structures in Itemdata.
        // One is for items of all types; the other is for item sets
        // This means we'll have to account for two separate data structures here
        public bool invalidItem;
        string tempString;

        // Item data structure
        public string beginning_text;// item_begin // or set_begin
        public string type;// weapon // etcitem, armor, accessary, questitem
        public string itemId;// 1 //
        public string itemName;// [small_sword] // small caps, _ instead of space, surrounded by []
        public string item_type;// item_type=weapon // armor etcitem accessary questitem 
        public string slot_bit_type;// slot_bit_type={rhand} // lhand lrhand(bows fx) feet legs chest head gloves onepiece none rfinger;lfinger rear;lear neck underwear back hair hair2 alldress lbracelet deco1 waist
        public string armor_type;// armor_type=none //
        public string etcitem_type;// etcitem_type=none //
        public string delay_share_group;// delay_share_group=-1 //
        public string item_multi_skill_list;// item_multi_skill_list={s_vari_passive_pa_up10; s_vari_passive_ma_up10} // {} 
        public string recipe_id;// recipe_id=0 //
        public string blessed;// blessed=0 //
        public string weight;// weight=1600 //
        public string default_action;// default_action=action_equip //
        public string consume_type;// consume_type=consume_type_normal //
        public string initial_count;// initial_count=1 //
        public string maximum_count;// maximum_count=1 //
        public string soulshot_count;// soulshot_count=1 //
        public string spiritshot_count;// spiritshot_count=1 //
        public string reduced_soulshot;// reduced_soulshot={} //
        public string reduced_spiritshot;// reduced_spiritshot={} //
        public string reduced_mp_consume;// reduced_mp_consume={} //
        string immediate_effect;// immediate_effect=1 
        string ex_immediate_effect;// ex_immediate_effect=0
        string drop_period;// drop_period=10 
        public string duration;// duration={-1;0}
        public string use_skill_distime;// use_skill_distime=0
        public string period;// period=0
        public string equip_reuse_delay;// equip_reuse_delay=0
        string price;// price=0 
        public string default_price;// default_price=590
        public string item_skill;// item_skill=[none] 
        public string critical_attack_skill;// critical_attack_skill=[none]
        public string attack_skill;// attack_skill=[none]
        public string attack_skill_percentage = "";
        public string magic_skill;// magic_skill=[none]
        public string magic_skill_percentage = "";
        public string item_skill_enchanted_four;// item_skill_enchanted_four=[none] 
        public string capsuled_items;// capsuled_items={} 
        string material_type;// material_type=steel 
        public string crystal_type;// crystal_type=none 
        public string crystal_count;// crystal_count=0 
        public string is_trade;// is_trade=1 
        public string is_drop;// is_drop=1 
        public string is_destruct;// is_destruct=1 
        public string is_private_store;
        string keep_type;// keep_type=15 
        public string physical_damage;// physical_damage=8 
        public string random_damage;// random_damage=10 
        public string weapon_type;// weapon_type=sword
        string can_penetrate;// can_penetrate=0
        public string critical;// critical=8
        public string hit_modify;// hit_modify=0
        public string avoid_modify;// avoid_modify=0
        string dual_fhit_rate;// dual_fhit_rate=0
        public string shield_defense;// shield_defense=0
        public string shield_defense_rate;// shield_defense_rate=0
        public string attack_range;// attack_range=40
        public string damage_range;// damage_range={0;0;40;120}
        public string attack_speed;// attack_speed=379
        public string reuse_delay;// reuse_delay=0
        public string mp_consume;// mp_consume=0
        public string magical_damage;// magical_damage=6
        public string durability;// durability=-1
        string damaged;// damaged=0
        public string physical_defense;// physical_defense=0
        public string magical_defense;// magical_defense=0
        public string mp_bonus;// mp_bonus=0
        string category;// category={}
        public string enchanted;// enchanted=0
        public string base_attribute_attack;// base_attribute_attack={none;0}
        public string base_attribute_defend;// base_attribute_defend={0;0;0;0;0;0}
        public string html;// html=[item_default.htm]
        public string magic_weapon;// magic_weapon=0
        public string enchant_enable;// enchant_enable=0
        public string elemental_enable;// elemental_enable=0
        public string unequip_skill;// unequip_skill={}
        public string for_npc;// for_npc=0
        public string item_equip_option;// item_equip_option={}
        public string use_condition;// use_condition={}
        public string equip_condition;// equip_condition={}
        public string is_olympiad_can_use;
        string can_move;// can_move=0
        public string is_premium;// is_premium=0


        //Item sets data structure
        public string set_id;
        public string slot_chest;
        public string slot_legs;
        public string slot_head;
        public string slot_gloves;
        public string slot_feet;
        public string slot_lhand;
        public string slot_additional;
        public string set_skill;
        public string effect_skill;
        public string additional_effect_skill;
        public string additional2_condition;
        public string additional2_effect_skill;
        public string str_inc;
        public string con_inc;
        public string dex_inc;
        public string int_inc;
        public string men_inc;
        public string wit_inc;

        //Text start and endings
        string itemName_textstart = "[";// [small_sword] // small caps, _ instead of space, surrounded by []
        string itemName_textend = "]";// [small_sword] // small caps, _ instead of space, surrounded by []
        string item_type_textstart = "item_type=";// item_type=weapon // armor etcitem accessary questitem 
        string item_type_textend = "";// item_type=weapon // armor etcitem accessary questitem 
        string slot_bit_type_textstart = "slot_bit_type={";// slot_bit_type={rhand} // lhand lrhand(bows fx) feet legs chest head gloves onepiece none rfinger;lfinger rear;lear neck underwear back hair hair2 alldress lbracelet deco1 waist
        string slot_bit_type_textend = "}";// slot_bit_type={rhand} // lhand lrhand(bows fx) feet legs chest head gloves onepiece none rfinger;lfinger rear;lear neck underwear back hair hair2 alldress lbracelet deco1 waist
        string armor_type_textstart = "armor_type=";// armor_type=none //
        string armor_type_textend = "";// armor_type=none //
        string etcitem_type_textstart = "etcitem_type=";// etcitem_type=none //
        string etcitem_type_textend = "";// etcitem_type=none //
        string delay_share_group_textstart = "delay_share_group=";// delay_share_group=-1 //
        string delay_share_group_textend = "";// delay_share_group=-1 //
        string item_multi_skill_list_textstart = "item_multi_skill_list={";// item_multi_skill_list={s_vari_passive_pa_up10; s_vari_passive_ma_up10} // {} 
        string item_multi_skill_list_textend = "}";// item_multi_skill_list={s_vari_passive_pa_up10; s_vari_passive_ma_up10} // {} 
        string recipe_id_textstart = "recipe_id=";// recipe_id=0 //
        string recipe_id_textend = "";// recipe_id=0 //
        string blessed_textstart = "blessed=";// blessed=0 //
        string blessed_textend = "";// blessed=0 //
        string weight_textstart = "weight=";// weight=1600 //
        string weight_textend = "";// weight=1600 //
        string default_action_textstart = "default_action=";// default_action=action_equip //
        string default_action_textend = "";// default_action=action_equip //
        string consume_type_textstart = "consume_type=";// consume_type=consume_type_normal //
        string consume_type_textend = "";// consume_type=consume_type_normal //
        string initial_count_textstart = "initial_count=";// initial_count=1 //
        string initial_count_textend = "";// initial_count=1 //
        string maximum_count_textstart = "maximum_count=";// maximum_count=1 //
        string maximum_count_textend = "";// maximum_count=1 //
        string soulshot_count_textstart = "soulshot_count=";// soulshot_count=1 //
        string soulshot_count_textend = "";// soulshot_count=1 //
        string spiritshot_count_textstart = "spiritshot_count=";// spiritshot_count=1 //
        string spiritshot_count_textend = "";// spiritshot_count=1 //
        string reduced_soulshot_textstart = "reduced_soulshot={";// reduced_soulshot={} //
        string reduced_soulshot_textend = "}";// reduced_soulshot={} //
        string reduced_spiritshot_textstart = "reduced_spiritshot={";// reduced_spiritshot={} //
        string reduced_spiritshot_textend = "}";// reduced_spiritshot={} //
        string reduced_mp_consume_textstart = "reduced_mp_consume={";// reduced_mp_consume={} //
        string reduced_mp_consume_textend = "}";// reduced_mp_consume={} //
        string immediate_effect_textstart = "immediate_effect=";// immediate_effect=1 
        string immediate_effect_textend = "";// immediate_effect=1 
        string ex_immediate_effect_textstart = "ex_immediate_effect=";// ex_immediate_effect=0
        string ex_immediate_effect_textend = "";// ex_immediate_effect=0
        string drop_period_textstart = "drop_period=";// drop_period=10 
        string drop_period_textend = "";// drop_period=10 
        string duration_textstart = "duration=";// duration={-1;0}
        string duration_textend = "";// duration={-1;0}
        string use_skill_distime_textstart = "use_skill_distime=";// use_skill_distime=0
        string use_skill_distime_textend = "";// use_skill_distime=0
        string period_textstart = "period=";// period=0
        string period_textend = "";// period=0
        string equip_reuse_delay_textstart = "equip_reuse_delay=";// equip_reuse_delay=0
        string equip_reuse_delay_textend = "";// equip_reuse_delay=0
        string price_textstart = "price=";// price=0 
        string price_textend = "";// price=0 
        string default_price_textstart = "default_price=";// default_price=590
        string default_price_textend = "";// default_price=590
        string item_skill_textstart = "item_skill=[";// item_skill=[none] 
        string item_skill_textend = "]";// item_skill=[none] 
        string critical_attack_skill_textstart = "critical_attack_skill=";// critical_attack_skill=[none]
        string critical_attack_skill_textend = "";// critical_attack_skill=[none]
        string attack_skill_textstart = "attack_skill=[";// attack_skill=[none]
        string attack_skill_textend = "]";// attack_skill=[none]
        string magic_skill_textstart = "magic_skill=[";// magic_skill=[none]
        string magic_skill_textend = "]";// magic_skill=[none]
        string item_skill_enchanted_four_textstart = "item_skill_enchanted_four=[";// item_skill_enchanted_four=[none] 
        string item_skill_enchanted_four_textend = "]";// item_skill_enchanted_four=[none] 
        string capsuled_items_textstart = "capsuled_items=";// capsuled_items={} 
        string capsuled_items_textend = "";// capsuled_items={} 
        string material_type_textstart = "material_type=";// material_type=steel 
        string material_type_textend = "";// material_type=steel 
        string crystal_type_textstart = "crystal_type=";// crystal_type=none 
        string crystal_type_textend = "";// crystal_type=none 
        string crystal_count_textstart = "crystal_count=";// crystal_count=0 
        string crystal_count_textend = "";// crystal_count=0 
        string is_trade_textstart = "is_trade=";// is_trade=1 
        string is_trade_textend = "";// is_trade=1 
        string is_drop_textstart = "is_drop=";// is_drop=1 
        string is_drop_textend = "";// is_drop=1 
        string is_destruct_textstart = "is_destruct=";// is_destruct=1 
        string is_destruct_textend = "";// is_destruct=1 
        string is_private_store_textstart = "is_private_store=";
        string is_private_store_textend = "";
        string keep_type_textstart = "keep_type=";// keep_type=15 
        string keep_type_textend = "";// keep_type=15 
        string physical_damage_textstart = "physical_damage=";// physical_damage=8 
        string physical_damage_textend = "";// physical_damage=8 
        string random_damage_textstart = "random_damage=";// random_damage=10 
        string random_damage_textend = "";// random_damage=10 
        string weapon_type_textstart = "weapon_type=";// weapon_type=sword
        string weapon_type_textend = "";// weapon_type=sword
        string can_penetrate_textstart = "can_penetrate=";// can_penetrate=0
        string can_penetrate_textend = "";// can_penetrate=0
        string critical_textstart = "critical=";// critical=8
        string critical_textend = "";// critical=8
        string hit_modify_textstart = "hit_modify=";// hit_modify=0
        string hit_modify_textend = "";// hit_modify=0
        string avoid_modify_textstart = "avoid_modify=";// avoid_modify=0
        string avoid_modify_textend = "";// avoid_modify=0
        string dual_fhit_rate_textstart = "dual_fhit_rate=";// dual_fhit_rate=0
        string dual_fhit_rate_textend = "";// dual_fhit_rate=0
        string shield_defense_textstart = "shield_defense=";// shield_defense=0
        string shield_defense_textend = "";// shield_defense=0
        string shield_defense_rate_textstart = "shield_defense_rate=";// shield_defense_rate=0
        string shield_defense_rate_textend = "";// shield_defense_rate=0
        string attack_range_textstart = "attack_range=";// attack_range=40
        string attack_range_textend = "";// attack_range=40
        string damage_range_textstart = "damage_range={";// damage_range={0;0;40;120}
        string damage_range_textend = "}";// damage_range={0;0;40;120}
        string attack_speed_textstart = "attack_speed=";// attack_speed=379
        string attack_speed_textend = "";// attack_speed=379
        string reuse_delay_textstart = "reuse_delay=";// reuse_delay=0
        string reuse_delay_textend = "";// reuse_delay=0
        string mp_consume_textstart = "mp_consume=";// mp_consume=0
        string mp_consume_textend = "";// mp_consume=0
        string magical_damage_textstart = "magical_damage=";// magical_damage=6
        string magical_damage_textend = "";// magical_damage=6
        string durability_textstart = "durability=";// durability=-1
        string durability_textend = "";// durability=-1
        string damaged_textstart = "damaged=";// damaged=0
        string damaged_textend = "";// damaged=0
        string physical_defense_textstart = "physical_defense=";// physical_defense=0
        string physical_defense_textend = "";// physical_defense=0
        string magical_defense_textstart = "magical_defense=";// magical_defense=0
        string magical_defense_textend = "";// magical_defense=0
        string mp_bonus_textstart = "mp_bonus=";// mp_bonus=0
        string mp_bonus_textend = "";// mp_bonus=0
        string category_textstart = "category={";// category={}
        string category_textend = "}";// category={}
        string enchanted_textstart = "enchanted=";// enchanted=0
        string enchanted_textend = "";// enchanted=0
        string base_attribute_attack_textstart = "base_attribute_attack={";// base_attribute_attack={none;0}
        string base_attribute_attack_textend = "}";// base_attribute_attack={none;0}
        string base_attribute_defend_textstart = "base_attribute_defend={";// base_attribute_defend={0;0;0;0;0;0}
        string base_attribute_defend_textend = "}";// base_attribute_defend={0;0;0;0;0;0}
        string html_textstart = "html=[";// html=[item_default.htm]
        string html_textend = "]";// html=[item_default.htm]
        string magic_weapon_textstart = "magic_weapon=";// magic_weapon=0
        string magic_weapon_textend = "";// magic_weapon=0
        string enchant_enable_textstart = "enchant_enable=";// enchant_enable=0
        string enchant_enable_textend = "";// enchant_enable=0
        string elemental_enable_textstart = "elemental_enable=";// elemental_enable=0
        string elemental_enable_textend = "";// elemental_enable=0
        string unequip_skill_textstart = "unequip_skill={";// unequip_skill={}
        string unequip_skill_textend = "}";// unequip_skill={}
        string for_npc_textstart = "for_npc=";// for_npc=0
        string for_npc_textend = "";// for_npc=0
        string item_equip_option_textstart = "item_equip_option={";// item_equip_option={}
        string item_equip_option_textend = "}";// item_equip_option={}
        string use_condition_textstart = "use_condition=";// use_condition={}
        string use_condition_textend = "";// use_condition={}
        string equip_condition_textstart = "equip_condition=";// equip_condition={}
        string equip_condition_textend = "";// equip_condition={}
        string is_olympiad_can_use_textstart = "is_olympiad_can_use=";
        string is_olympiad_can_use_textend = "";
        string can_move_textstart = "can_move=";// can_move=0
        string can_move_textend = "";// can_move=0
        string is_premium_textstart = "is_premium=";// is_premium=0
        string is_premium_textend = "";// is_premium=0
        string slot_chest_textstart = "slot_chest={";
        string slot_chest_textend = "}";
        string slot_legs_textstart = "slot_legs={";
        string slot_legs_textend = "}";
        string slot_head_textstart = "slot_head={";
        string slot_head_textend = "}";
        string slot_gloves_textstart = "slot_gloves={";
        string slot_gloves_textend = "}";
        string slot_feet_textstart = "slot_feet={";
        string slot_feet_textend = "}";
        string slot_lhand_textstart = "slot_lhand={";
        string slot_lhand_textend = "}";
        string slot_additional_textstart = "slot_additional=[";
        string slot_additional_textend = "]";
        string set_skill_textstart = "set_skill=[";
        string set_skill_textend = "]";
        string set_effect_skill_textstart = "set_effect_skill=[";
        string set_effect_skill_textend = "]";
        string set_additional_effect_skill_textstart = "set_additional_effect_skill=[";
        string set_additional_effect_skill_textend = "]";
        string set_additional2_condition_textstart = "set_additional2_condition=";
        string set_additional2_condition_textend = "";
        string set_additional2_effect_skill_textstart = "set_additional2_effect_skill=[";
        string set_additional2_effect_skill_textend = "]";
        string str_inc_textstart = "str_inc={";
        string str_inc_textend = "}";
        string con_inc_textstart = "con_inc={";
        string con_inc_textend = "}";
        string dex_inc_textstart = "dex_inc={";
        string dex_inc_textend = "}";
        string int_inc_textstart = "int_inc={";
        string int_inc_textend = "}";
        string men_inc_textstart = "men_inc={";
        string men_inc_textend = "}";
        string wit_inc_textstart = "wit_inc={";
        string wit_inc_textend = "}";
        string set_end_text;


        string korean;
        string endingText;


        public Server_Itemdata(string line)
        {
            string[] dataLine = line.Split('\t');

            beginning_text = dataLine[0];

            bool setDataStructure = false;
            if (beginning_text == "set_begin")
            {
                setDataStructure = true;
            }
            if (!setDataStructure)
            {
                int emptyVariables = 0;

                type = dataLine[1];
                itemId = dataLine[2];


                itemName = StripExcessServerText(itemName_textstart, dataLine[3], itemName_textend);
                item_type = StripExcessServerText(item_type_textstart, dataLine[4], item_type_textend);
                slot_bit_type = StripExcessServerText(slot_bit_type_textstart, dataLine[5], slot_bit_type_textend);
                armor_type = StripExcessServerText(armor_type_textstart, dataLine[6], armor_type_textend);
                etcitem_type = StripExcessServerText(etcitem_type_textstart, dataLine[7], etcitem_type_textend);
                delay_share_group = StripExcessServerText(delay_share_group_textstart, dataLine[8], delay_share_group_textend);
                item_multi_skill_list = StripExcessServerText(item_multi_skill_list_textstart, dataLine[9], item_multi_skill_list_textend);
                recipe_id = StripExcessServerText(recipe_id_textstart, dataLine[10], recipe_id_textend);
                blessed = StripExcessServerText(blessed_textstart, dataLine[11], blessed_textend);
                weight = StripExcessServerText(weight_textstart, dataLine[12], weight_textend);
                default_action = StripExcessServerText(default_action_textstart, dataLine[13], default_action_textend);
                consume_type = StripExcessServerText(consume_type_textstart, dataLine[14], consume_type_textend);
                initial_count = StripExcessServerText(initial_count_textstart, dataLine[15], initial_count_textend);

                if (dataLine[16].Contains(maximum_count_textstart))
                    maximum_count = StripExcessServerText(maximum_count_textstart, dataLine[16], maximum_count_textend);
                else
                {
                    maximum_count = "";
                    emptyVariables++;
                }
                soulshot_count = StripExcessServerText(soulshot_count_textstart, dataLine[17 - emptyVariables], soulshot_count_textend);
                spiritshot_count = StripExcessServerText(spiritshot_count_textstart, dataLine[18 - emptyVariables], spiritshot_count_textend);
                reduced_soulshot = StripExcessServerText(reduced_soulshot_textstart, dataLine[19 - emptyVariables], reduced_soulshot_textend);
                reduced_spiritshot = StripExcessServerText(reduced_spiritshot_textstart, dataLine[20 - emptyVariables], reduced_spiritshot_textend);
                reduced_mp_consume = StripExcessServerText(reduced_mp_consume_textstart, dataLine[21 - emptyVariables], reduced_mp_consume_textend);
                immediate_effect = StripExcessServerText(immediate_effect_textstart, dataLine[22 - emptyVariables], immediate_effect_textend);
                ex_immediate_effect = StripExcessServerText(ex_immediate_effect_textstart, dataLine[23 - emptyVariables], ex_immediate_effect_textend);
                drop_period = StripExcessServerText(drop_period_textstart, dataLine[24 - emptyVariables], drop_period_textend);
                duration = StripExcessServerText(duration_textstart, dataLine[25 - emptyVariables], duration_textend);
                use_skill_distime = StripExcessServerText(use_skill_distime_textstart, dataLine[26 - emptyVariables], use_skill_distime_textend);
                period = StripExcessServerText(period_textstart, dataLine[27 - emptyVariables], period_textend);
                equip_reuse_delay = StripExcessServerText(equip_reuse_delay_textstart, dataLine[28 - emptyVariables], equip_reuse_delay_textend);
                price = StripExcessServerText(price_textstart, dataLine[29 - emptyVariables], price_textend);
                default_price = StripExcessServerText(default_price_textstart, dataLine[30 - emptyVariables], default_price_textend);
                item_skill = StripExcessServerText(item_skill_textstart, dataLine[31 - emptyVariables], item_skill_textend);
                critical_attack_skill = StripExcessServerText(critical_attack_skill_textstart, dataLine[32 - emptyVariables], critical_attack_skill_textend);
                attack_skill = StripExcessServerText(attack_skill_textstart, dataLine[33 - emptyVariables], attack_skill_textend);
                magic_skill = StripExcessServerText(magic_skill_textstart, dataLine[34 - emptyVariables], magic_skill_textend);

                if (!string.IsNullOrEmpty(magic_skill))
                    if (magic_skill != "none")
                    {
                        string[] splitMagicSkill = magic_skill.Split(';');
                        magic_skill = splitMagicSkill[0];
                        magic_skill_percentage = splitMagicSkill[1];
                    }

                item_skill_enchanted_four = StripExcessServerText(item_skill_enchanted_four_textstart, dataLine[35 - emptyVariables], item_skill_enchanted_four_textend);
                capsuled_items = StripExcessServerText(capsuled_items_textstart, dataLine[36 - emptyVariables], capsuled_items_textend);
                material_type = StripExcessServerText(material_type_textstart, dataLine[37 - emptyVariables], material_type_textend);
                crystal_type = StripExcessServerText(crystal_type_textstart, dataLine[38 - emptyVariables], crystal_type_textend);
                crystal_count = StripExcessServerText(crystal_count_textstart, dataLine[39 - emptyVariables], crystal_count_textend);
                is_trade = StripExcessServerText(is_trade_textstart, dataLine[40 - emptyVariables], is_trade_textend);
                is_drop = StripExcessServerText(is_drop_textstart, dataLine[41 - emptyVariables], is_drop_textend);
                is_destruct = StripExcessServerText(is_destruct_textstart, dataLine[42 - emptyVariables], is_destruct_textend);


                if (dataLine[43 - emptyVariables].Contains(is_private_store_textstart))
                    is_private_store = StripExcessServerText(is_private_store_textstart, dataLine[43 - emptyVariables], is_private_store_textend);
                else
                {
                    is_private_store = "0";
                    emptyVariables++;
                }

                keep_type = StripExcessServerText(keep_type_textstart, dataLine[44 - emptyVariables], keep_type_textend);
                physical_damage = StripExcessServerText(physical_damage_textstart, dataLine[45 - emptyVariables], physical_damage_textend);
                random_damage = StripExcessServerText(random_damage_textstart, dataLine[46 - emptyVariables], random_damage_textend);
                weapon_type = StripExcessServerText(weapon_type_textstart, dataLine[47 - emptyVariables], weapon_type_textend);

                if (dataLine[48 - emptyVariables].Contains(can_penetrate_textstart))
                    can_penetrate = StripExcessServerText(can_penetrate_textstart, dataLine[48 - emptyVariables], can_penetrate_textend);
                else
                {
                    can_penetrate = "0";
                    emptyVariables++;
                }




                critical = StripExcessServerText(critical_textstart, dataLine[49 - emptyVariables], critical_textend);
                hit_modify = StripExcessServerText(hit_modify_textstart, dataLine[50 - emptyVariables], hit_modify_textend);
                avoid_modify = StripExcessServerText(avoid_modify_textstart, dataLine[51 - emptyVariables], avoid_modify_textend);
                dual_fhit_rate = StripExcessServerText(dual_fhit_rate_textstart, dataLine[52 - emptyVariables], dual_fhit_rate_textend);
                shield_defense = StripExcessServerText(shield_defense_textstart, dataLine[53 - emptyVariables], shield_defense_textend);
                shield_defense_rate = StripExcessServerText(shield_defense_rate_textstart, dataLine[54 - emptyVariables], shield_defense_rate_textend);
                attack_range = StripExcessServerText(attack_range_textstart, dataLine[55 - emptyVariables], attack_range_textend);
                damage_range = StripExcessServerText(damage_range_textstart, dataLine[56 - emptyVariables], damage_range_textend);
                attack_speed = StripExcessServerText(attack_speed_textstart, dataLine[57 - emptyVariables], attack_speed_textend);
                reuse_delay = StripExcessServerText(reuse_delay_textstart, dataLine[58 - emptyVariables], reuse_delay_textend);
                mp_consume = StripExcessServerText(mp_consume_textstart, dataLine[59 - emptyVariables], mp_consume_textend);
                magical_damage = StripExcessServerText(magical_damage_textstart, dataLine[60 - emptyVariables], magical_damage_textend);
                durability = StripExcessServerText(durability_textstart, dataLine[61 - emptyVariables], durability_textend);
                damaged = StripExcessServerText(damaged_textstart, dataLine[62 - emptyVariables], damaged_textend);
                physical_defense = StripExcessServerText(physical_defense_textstart, dataLine[63 - emptyVariables], physical_defense_textend);
                magical_defense = StripExcessServerText(magical_defense_textstart, dataLine[64 - emptyVariables], magical_defense_textend);
                mp_bonus = StripExcessServerText(mp_bonus_textstart, dataLine[65 - emptyVariables], mp_bonus_textend);
                category = StripExcessServerText(category_textstart, dataLine[66 - emptyVariables], category_textend);
                enchanted = StripExcessServerText(enchanted_textstart, dataLine[67 - emptyVariables], enchanted_textend);
                base_attribute_attack = StripExcessServerText(base_attribute_attack_textstart, dataLine[68 - emptyVariables], base_attribute_attack_textend);
                base_attribute_defend = StripExcessServerText(base_attribute_defend_textstart, dataLine[69 - emptyVariables], base_attribute_defend_textend);
                html = StripExcessServerText(html_textstart, dataLine[70 - emptyVariables], html_textend);
                magic_weapon = StripExcessServerText(magic_weapon_textstart, dataLine[71 - emptyVariables], magic_weapon_textend);
                enchant_enable = StripExcessServerText(enchant_enable_textstart, dataLine[72 - emptyVariables], enchant_enable_textend);
                elemental_enable = StripExcessServerText(elemental_enable_textstart, dataLine[73 - emptyVariables], elemental_enable_textend);
                unequip_skill = StripExcessServerText(unequip_skill_textstart, dataLine[74 - emptyVariables], unequip_skill_textend);
                for_npc = StripExcessServerText(for_npc_textstart, dataLine[75 - emptyVariables], for_npc_textend);
                item_equip_option = StripExcessServerText(item_equip_option_textstart, dataLine[76 - emptyVariables], item_equip_option_textend);
                use_condition = StripExcessServerText(use_condition_textstart, dataLine[77 - emptyVariables], use_condition_textend);

                equip_condition = dataLine[78 - emptyVariables].Replace(equip_condition_textstart, "");
                is_olympiad_can_use = StripExcessServerText(is_olympiad_can_use_textstart, dataLine[79 - emptyVariables], is_olympiad_can_use_textend);

                if (dataLine[80 - emptyVariables].Contains(is_olympiad_can_use_textstart))
                    emptyVariables -= 1;

                can_move = StripExcessServerText(can_move_textstart, dataLine[80 - emptyVariables], can_move_textend);
                is_premium = StripExcessServerText(is_premium_textstart, dataLine[81 - emptyVariables], is_premium_textend);
                korean = dataLine[82 - emptyVariables];
                endingText = dataLine[83 - emptyVariables];
            }
            else
            {
                int slotSkips = 0;
                set_id = dataLine[1];

                if (dataLine[2].Contains("slot_chest"))
                    slot_chest = StripExcessServerText(slot_chest_textstart, dataLine[2], slot_chest_textend);
                else
                    slotSkips++;

                if (dataLine[3 - slotSkips].Contains("slot_legs"))
                    slot_legs = StripExcessServerText(slot_legs_textstart, dataLine[3 - slotSkips], slot_legs_textend);
                else
                    slotSkips++;

                if (dataLine[4 - slotSkips].Contains("slot_head"))
                    slot_head = StripExcessServerText(slot_head_textstart, dataLine[4 - slotSkips], slot_head_textend);
                else
                    slotSkips++;

                if (dataLine[5 - slotSkips].Contains("slot_gloves"))
                    slot_gloves = StripExcessServerText(slot_gloves_textstart, dataLine[5 - slotSkips], slot_gloves_textend);
                else
                    slotSkips++;

                if (dataLine[6 - slotSkips].Contains("slot_feet"))
                    slot_feet = StripExcessServerText(slot_feet_textstart, dataLine[6 - slotSkips], slot_feet_textend);
                else
                    slotSkips++;

                if (dataLine[7 - slotSkips].Contains("slot_lhand"))
                    slot_lhand = StripExcessServerText(slot_lhand_textstart, dataLine[7 - slotSkips], slot_lhand_textend);
                else
                    slotSkips++;

                if (dataLine[8 - slotSkips].Contains("slot_additional"))
                    slot_additional = StripExcessServerText(slot_additional_textstart, dataLine[8 - slotSkips], slot_additional_textend);
                else
                    slotSkips++;


                set_skill = StripExcessServerText(set_skill_textstart, dataLine[9 - slotSkips], set_skill_textend);
                effect_skill = StripExcessServerText(set_effect_skill_textstart, dataLine[10 - slotSkips], set_effect_skill_textend);
                additional_effect_skill = StripExcessServerText(set_additional_effect_skill_textstart, dataLine[11 - slotSkips], set_additional_effect_skill_textend);

                if (dataLine[12 - slotSkips].Contains("set_additional2_condition"))
                    additional2_condition = StripExcessServerText(set_additional2_condition_textstart, dataLine[12 - slotSkips], set_additional2_condition_textend);
                else
                    slotSkips++;

                if (dataLine[13 - slotSkips].Contains("set_additional2_effect_skill"))
                    additional2_effect_skill = StripExcessServerText(set_additional2_effect_skill_textstart, dataLine[13 - slotSkips], set_additional2_effect_skill_textend);
                else
                    slotSkips++;

                str_inc = StripExcessServerText(str_inc_textstart, dataLine[14 - slotSkips], str_inc_textend);
                con_inc = StripExcessServerText(con_inc_textstart, dataLine[15 - slotSkips], con_inc_textend);
                dex_inc = StripExcessServerText(dex_inc_textstart, dataLine[16 - slotSkips], dex_inc_textend);
                int_inc = StripExcessServerText(int_inc_textstart, dataLine[17 - slotSkips], int_inc_textend);
                men_inc = StripExcessServerText(men_inc_textstart, dataLine[18 - slotSkips], men_inc_textend);
                wit_inc = StripExcessServerText(wit_inc_textstart, dataLine[19 - slotSkips], wit_inc_textend);
                endingText = dataLine[20 - slotSkips];
            }
        }
        public string GetPCHString()
        {
            return "[" + itemName + "]\t=\t" + itemId;
        }

        public string GetExportString()
        {
            //Stupid fucking exceptions
            string endingTextWhiteSpace = "";
            {
                if (itemId == 13808.ToString() || itemId == 13809.ToString())
                    endingTextWhiteSpace = "";
                else
                    endingTextWhiteSpace = "\t";
            }

            if (invalidItem)
                return tempString;

            string returnMagicSkillString = magic_skill;
            if (returnMagicSkillString != "none")
            {
                returnMagicSkillString = ConvertToServerText(magic_skill_textstart, magic_skill, magic_skill_textend);
                returnMagicSkillString += ";" + magic_skill_percentage;
            }
            else
            {
                returnMagicSkillString = ConvertToServerText(magic_skill_textstart, magic_skill, magic_skill_textend);
            }


            if (beginning_text == "item_begin")
            {
                string returnString = "";
                returnString +=
                    beginning_text + '\t' +
                    type + '\t' +
                    itemId + '\t' +
                    ConvertToServerText(itemName_textstart, itemName, itemName_textend) + '\t' +
                    ConvertToServerText(item_type_textstart, item_type, item_type_textend) + '\t' +
                    ConvertToServerText(slot_bit_type_textstart, slot_bit_type, slot_bit_type_textend) + '\t' +
                    ConvertToServerText(armor_type_textstart, armor_type, armor_type_textend) + '\t' +
                    ConvertToServerText(etcitem_type_textstart, etcitem_type, etcitem_type_textend) + '\t' +
                    ConvertToServerText(delay_share_group_textstart, delay_share_group, delay_share_group_textend) + '\t' +
                    ConvertToServerText(item_multi_skill_list_textstart, item_multi_skill_list, item_multi_skill_list_textend) + '\t' +
                    ConvertToServerText(recipe_id_textstart, recipe_id, recipe_id_textend) + '\t' +
                    ConvertToServerText(blessed_textstart, blessed, blessed_textend) + '\t' +
                    ConvertToServerText(weight_textstart, weight, weight_textend) + '\t' +
                    ConvertToServerText(default_action_textstart, default_action, default_action_textend) + '\t' +
                    ConvertToServerText(consume_type_textstart, consume_type, consume_type_textend) + '\t' +
                    ConvertToServerText(initial_count_textstart, initial_count, initial_count_textend) + '\t';

                if (!string.IsNullOrEmpty(maximum_count))
                    returnString += ConvertToServerText(maximum_count_textstart, maximum_count, maximum_count_textend) + '\t';

                returnString +=
                    ConvertToServerText(soulshot_count_textstart, soulshot_count, soulshot_count_textend) + '\t' +
                    ConvertToServerText(spiritshot_count_textstart, spiritshot_count, spiritshot_count_textend) + '\t' +
                    ConvertToServerText(reduced_soulshot_textstart, reduced_soulshot, reduced_soulshot_textend) + '\t' +
                    ConvertToServerText(reduced_spiritshot_textstart, reduced_spiritshot, reduced_spiritshot_textend) + '\t' +
                    ConvertToServerText(reduced_mp_consume_textstart, reduced_mp_consume, reduced_mp_consume_textend) + '\t' +
                    ConvertToServerText(immediate_effect_textstart, immediate_effect, immediate_effect_textend) + '\t' +
                    ConvertToServerText(ex_immediate_effect_textstart, ex_immediate_effect, ex_immediate_effect_textend) + '\t' +
                    ConvertToServerText(drop_period_textstart, drop_period, drop_period_textend) + '\t' +
                    ConvertToServerText(duration_textstart, duration, duration_textend) + '\t' +
                    ConvertToServerText(use_skill_distime_textstart, use_skill_distime, use_skill_distime_textend) + '\t' +
                    ConvertToServerText(period_textstart, period, period_textend) + '\t' +
                    ConvertToServerText(equip_reuse_delay_textstart, equip_reuse_delay, equip_reuse_delay_textend) + '\t' +
                    ConvertToServerText(price_textstart, price, price_textend) + '\t' +
                    ConvertToServerText(default_price_textstart, default_price, default_price_textend) + '\t' +
                    ConvertToServerText(item_skill_textstart, item_skill, item_skill_textend) + '\t' +
                    ConvertToServerText(critical_attack_skill_textstart, critical_attack_skill, critical_attack_skill_textend) + '\t' +
                    ConvertToServerText(attack_skill_textstart, attack_skill, attack_skill_textend) + '\t' +
                    returnMagicSkillString + '\t' +
                    ConvertToServerText(item_skill_enchanted_four_textstart, item_skill_enchanted_four, item_skill_enchanted_four_textend) + '\t' +
                    ConvertToServerText(capsuled_items_textstart, capsuled_items, capsuled_items_textend) + '\t' +
                    ConvertToServerText(material_type_textstart, material_type, material_type_textend) + '\t' +
                    ConvertToServerText(crystal_type_textstart, crystal_type, crystal_type_textend) + '\t' +
                    ConvertToServerText(crystal_count_textstart, crystal_count, crystal_count_textend) + '\t' +
                    ConvertToServerText(is_trade_textstart, is_trade, is_trade_textend) + '\t' +
                    ConvertToServerText(is_drop_textstart, is_drop, is_drop_textend) + '\t' +
                    ConvertToServerText(is_destruct_textstart, is_destruct, is_destruct_textend) + '\t' +
                    ConvertToServerText(is_private_store_textstart, is_private_store, is_private_store_textend) + '\t' +
                    ConvertToServerText(keep_type_textstart, keep_type, keep_type_textend) + '\t' +
                    ConvertToServerText(physical_damage_textstart, physical_damage, physical_damage_textend) + '\t' +
                    ConvertToServerText(random_damage_textstart, random_damage, random_damage_textend) + '\t' +
                    ConvertToServerText(weapon_type_textstart, weapon_type, weapon_type_textend) + '\t' +
                    //ConvertToServerText(can_penetrate_textstart, can_penetrate, can_penetrate_textend) + '\t' +//
                    ConvertToServerText(critical_textstart, critical, critical_textend) + '\t' +
                    ConvertToServerText(hit_modify_textstart, hit_modify, hit_modify_textend) + '\t' +
                    ConvertToServerText(avoid_modify_textstart, avoid_modify, avoid_modify_textend) + '\t' +
                    ConvertToServerText(dual_fhit_rate_textstart, dual_fhit_rate, dual_fhit_rate_textend) + '\t' +
                    ConvertToServerText(shield_defense_textstart, shield_defense, shield_defense_textend) + '\t' +
                    ConvertToServerText(shield_defense_rate_textstart, shield_defense_rate, shield_defense_rate_textend) + '\t' +
                    ConvertToServerText(attack_range_textstart, attack_range, attack_range_textend) + '\t' +
                    ConvertToServerText(damage_range_textstart, damage_range, damage_range_textend) + '\t' +
                    ConvertToServerText(attack_speed_textstart, attack_speed, attack_speed_textend) + '\t' +
                    ConvertToServerText(reuse_delay_textstart, reuse_delay, reuse_delay_textend) + '\t' +
                    ConvertToServerText(mp_consume_textstart, mp_consume, mp_consume_textend) + '\t' +
                    ConvertToServerText(magical_damage_textstart, magical_damage, magical_damage_textend) + '\t' +
                    ConvertToServerText(durability_textstart, durability, durability_textend) + '\t' +
                    ConvertToServerText(damaged_textstart, damaged, damaged_textend) + '\t' +
                    ConvertToServerText(physical_defense_textstart, physical_defense, physical_defense_textend) + '\t' +
                    ConvertToServerText(magical_defense_textstart, magical_defense, magical_defense_textend) + '\t' +
                    ConvertToServerText(mp_bonus_textstart, mp_bonus, mp_bonus_textend) + '\t' +
                    ConvertToServerText(category_textstart, category, category_textend) + '\t' +
                    ConvertToServerText(enchanted_textstart, enchanted, enchanted_textend) + '\t' +
                    ConvertToServerText(base_attribute_attack_textstart, base_attribute_attack, base_attribute_attack_textend) + '\t' +
                    ConvertToServerText(base_attribute_defend_textstart, base_attribute_defend, base_attribute_defend_textend) + '\t' +
                    ConvertToServerText(html_textstart, html, html_textend) + '\t' +
                    ConvertToServerText(magic_weapon_textstart, magic_weapon, magic_weapon_textend) + '\t' +
                    ConvertToServerText(enchant_enable_textstart, enchant_enable, enchant_enable_textend) + '\t' +
                    ConvertToServerText(elemental_enable_textstart, elemental_enable, elemental_enable_textend) + '\t' +
                    ConvertToServerText(unequip_skill_textstart, unequip_skill, unequip_skill_textend) + '\t' +
                    ConvertToServerText(for_npc_textstart, for_npc, for_npc_textend) + '\t' +
                    ConvertToServerText(item_equip_option_textstart, item_equip_option, item_equip_option_textend) + '\t' +
                    ConvertToServerText(use_condition_textstart, use_condition, use_condition_textend) + '\t' +
                    ConvertToServerText(equip_condition_textstart, equip_condition, equip_condition_textend) + '\t' +
                    ConvertToServerText(is_olympiad_can_use_textstart, is_olympiad_can_use, is_olympiad_can_use_textend) + '\t' +
                    ConvertToServerText(can_move_textstart, can_move, can_move_textend) + '\t' +
                    ConvertToServerText(is_premium_textstart, is_premium, is_premium_textend) + '\t' +
                    korean + '\t' +
                    endingText + endingTextWhiteSpace;

                return returnString;
            }
            else
            {
                string whiteSpaceAfterEndingText = "";
                for (int i = 0; i < 62; i++) // Yes,fucking 62 '\t' after it ends.
                {
                    whiteSpaceAfterEndingText = whiteSpaceAfterEndingText + '\t';
                }

                string slot_chest_string = "";
                string slot_legs_string = "";
                string slot_head_string = "";
                string slot_gloves_string = "";
                string slot_feet_string = "";
                string slot_lhand_string = "";
                string slot_additional_string = "";

                string set_additional2_condition_string = "";
                string set_additional2_effect_skill_string = "";


                if (slot_chest != "" && slot_chest != null)
                {
                    slot_chest_string = ConvertToServerText(slot_chest_textstart, slot_chest, slot_chest_textend) + '\t';
                }
                else
                    whiteSpaceAfterEndingText = whiteSpaceAfterEndingText + '\t';

                if (slot_legs != "" && slot_legs != null)
                {
                    slot_legs_string = ConvertToServerText(slot_legs_textstart, slot_legs, slot_legs_textend) + '\t';
                }
                else
                    whiteSpaceAfterEndingText = whiteSpaceAfterEndingText + '\t';

                if (slot_head != "" && slot_head != null)
                {
                    slot_head_string = ConvertToServerText(slot_head_textstart, slot_head, slot_head_textend) + '\t';
                }
                else
                    whiteSpaceAfterEndingText = whiteSpaceAfterEndingText + '\t';

                if (slot_gloves != "" && slot_gloves != null)
                {
                    slot_gloves_string = ConvertToServerText(slot_gloves_textstart, slot_gloves, slot_gloves_textend) + '\t';
                }
                else
                    whiteSpaceAfterEndingText = whiteSpaceAfterEndingText + '\t';

                if (slot_feet != "" && slot_feet != null)
                {
                    slot_feet_string = ConvertToServerText(slot_feet_textstart, slot_feet, slot_feet_textend) + '\t';
                }
                else
                    whiteSpaceAfterEndingText = whiteSpaceAfterEndingText + '\t';

                if (slot_lhand != "" && slot_lhand != null)
                {
                    slot_lhand_string = ConvertToServerText(slot_lhand_textstart, slot_lhand, slot_lhand_textend) + '\t';
                }
                else
                    whiteSpaceAfterEndingText = whiteSpaceAfterEndingText + '\t';

                if (slot_additional != "" && slot_additional != null)
                {
                    slot_additional_string = ConvertToServerText(slot_additional_textstart, slot_additional, slot_additional_textend) + '\t';
                }
                else
                    whiteSpaceAfterEndingText = whiteSpaceAfterEndingText + '\t';

                if (additional2_condition != "" && additional2_condition != null)
                {
                    set_additional2_condition_string = ConvertToServerText(set_additional2_condition_textstart, additional2_condition, set_additional2_condition_textend) + '\t';
                }
                else
                    whiteSpaceAfterEndingText = whiteSpaceAfterEndingText + '\t';

                if (additional2_effect_skill != "" && additional2_effect_skill != null)
                {
                    set_additional2_effect_skill_string = ConvertToServerText(set_additional2_effect_skill_textstart, additional2_effect_skill, set_additional2_effect_skill_textend) + '\t';
                }
                else
                    whiteSpaceAfterEndingText = whiteSpaceAfterEndingText + '\t';

                return
                    beginning_text + '\t' +
                    set_id + '\t' +
                    slot_chest_string +
                    slot_legs_string +
                    slot_head_string +
                    slot_gloves_string +
                    slot_feet_string +
                    slot_lhand_string +
                    slot_additional_string +
                    ConvertToServerText(set_skill_textstart, set_skill, set_skill_textend) + '\t' +
                    ConvertToServerText(set_effect_skill_textstart, effect_skill, set_effect_skill_textend) + '\t' +
                    ConvertToServerText(set_additional_effect_skill_textstart, additional_effect_skill, set_additional_effect_skill_textend) + '\t' +
                    set_additional2_condition_string +
                    set_additional2_effect_skill_string +
                    ConvertToServerText(str_inc_textstart, str_inc, str_inc_textend) + '\t' +
                    ConvertToServerText(con_inc_textstart, con_inc, con_inc_textend) + '\t' +
                    ConvertToServerText(dex_inc_textstart, dex_inc, dex_inc_textend) + '\t' +
                    ConvertToServerText(int_inc_textstart, int_inc, int_inc_textend) + '\t' +
                    ConvertToServerText(men_inc_textstart, men_inc, men_inc_textend) + '\t' +
                    ConvertToServerText(wit_inc_textstart, wit_inc, wit_inc_textend) + '\t' +
                    endingText + whiteSpaceAfterEndingText;


            }
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

    }
}
