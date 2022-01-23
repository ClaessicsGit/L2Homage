using System.Collections.Generic;

namespace L2Homage
{
    public class Server_Npcdata
    {
        //temp
        string tempNormDropListString = "";
        string tempSpoilDroplistString = "";
        string tempMultiDroplistString = "";
        string tempExtraDroplistString = "";

        string beginning_text; //npc_begin
        public string npcType; //warrior
        public string npcId;  //20001
        public string npcName; //[gremlin]

        public Client_Npcname npcname_e;
        public string npcIngameName;
        public string description;

        string npcName_textstart = "[";
        string npcName_textend = "]";
        public string category;
        string category_textstart = "category={";
        string category_textend = "}";
        public string level;
        string level_textstart = "level=";
        string level_textend = "";
        public string exp;
        string exp_textstart = "exp=";
        string exp_textend = "";
        public string ex_crt_effect;
        string ex_crt_effect_textstart = "ex_crt_effect=";
        string ex_crt_effect_textend = "";
        public string unique;
        string unique_textstart = "unique=";
        string unique_textend = "";
        public string s_npc_prop_hp_rate;
        string s_npc_prop_hp_rate_textstart = "s_npc_prop_hp_rate=";
        string s_npc_prop_hp_rate_textend = "";
        public string race;
        string race_textstart = "race=";
        string race_textend = "";
        public string sex;
        string sex_textstart = "sex=";
        string sex_textend = "";
        public string[] skill_list;
        public string skill_list_string;
        string skill_list_textstart = "skill_list={";
        string skill_list_textend = "}";
        public string slot_chest;
        string slot_chest_textstart = "slot_chest=[";
        string slot_chest_textend = "]";
        public string slot_rhand;
        string slot_rhand_textstart = "slot_rhand=[";
        string slot_rhand_textend = "]";
        public string slot_lhand;
        string slot_lhand_textstart = "slot_lhand=[";
        string slot_lhand_textend = "]";
        public string collision_radius;
        string collision_radius_textstart = "collision_radius={";
        string collision_radius_textend = "}";
        public string collision_height;
        string collision_height_textstart = "collision_height={";
        string collision_height_textend = "}";
        public string hit_time_factor;
        string hit_time_factor_textstart = "hit_time_factor=";
        string hit_time_factor_textend = "";
        public string hit_time_factor_skill;
        string hit_time_factor_skill_textstart = "hit_time_factor_skill=";
        string hit_time_factor_skill_textend = "";
        public string ground_high;
        string ground_high_textstart = "ground_high={";
        string ground_high_textend = "}";
        public string ground_low;
        string ground_low_textstart = "ground_low={";
        string ground_low_textend = "}";
        public string strength;
        string strength_textstart = "str=";
        string strength_textend = "";
        public string intelligence;
        string intelligence_textstart = "int=";
        string intelligence_textend = "";
        public string dexterity;
        string dexterity_textstart = "dex=";
        string dexterity_textend = "";
        public string wit;
        string wit_textstart = "wit=";
        string wit_textend = "";
        public string con;
        string con_textstart = "con=";
        string con_textend = "";
        public string men;
        string men_textstart = "men=";
        string men_textend = "";
        public string org_hp;
        string org_hp_textstart = "org_hp=";
        string org_hp_textend = "";
        public string org_hp_regen;
        string org_hp_regen_textstart = "org_hp_regen=";
        string org_hp_regen_textend = "";
        public string org_mp;
        string org_mp_textstart = "org_mp=";
        string org_mp_textend = "";
        public string org_mp_regen;
        string org_mp_regen_textstart = "org_mp_regen=";
        string org_mp_regen_textend = "";
        public string base_attack_type;
        string base_attack_type_textstart = "base_attack_type=";
        string base_attack_type_textend = "";
        public string base_attack_range;
        string base_attack_range_textstart = "base_attack_range=";
        string base_attack_range_textend = "";
        public string base_damage_range;
        string base_damage_range_textstart = "base_damage_range={";
        string base_damage_range_textend = "}";
        public string base_rand_dam;
        string base_rand_dam_textstart = "base_rand_dam=";
        string base_rand_dam_textend = "";
        public string base_physical_attack;
        string base_physical_attack_textstart = "base_physical_attack=";
        string base_physical_attack_textend = "";
        public string base_critical;
        string base_critical_textstart = "base_critical=";
        string base_critical_textend = "";
        public string physical_hit_modify;
        string physical_hit_modify_textstart = "physical_hit_modify=";
        string physical_hit_modify_textend = "";
        public string base_attack_speed;
        string base_attack_speed_textstart = "base_attack_speed=";
        string base_attack_speed_textend = "";
        public string base_reuse_delay;
        string base_reuse_delay_textstart = "base_reuse_delay=";
        string base_reuse_delay_textend = "";
        public string base_magic_attack;
        string base_magic_attack_textstart = "base_magic_attack=";
        string base_magic_attack_textend = "";
        public string base_defend;
        string base_defend_textstart = "base_defend=";
        string base_defend_textend = "";
        public string base_magic_defend;
        string base_magic_defend_textstart = "base_magic_defend=";
        string base_magic_defend_textend = "";
        public string base_attribute_attack;
        string base_attribute_attack_textstart = "base_attribute_attack={";
        string base_attribute_attack_textend = "}";
        public string base_attribute_defend;
        string base_attribute_defend_textstart = "base_attribute_defend={";
        string base_attribute_defend_textend = "}";
        public string physical_avoid_modify;
        string physical_avoid_modify_textstart = "physical_avoid_modify=";
        string physical_avoid_modify_textend = "";
        public string shield_defense_rate;
        string shield_defense_rate_textstart = "shield_defense_rate=";
        string shield_defense_rate_textend = "";
        public string shield_defense;
        string shield_defense_textstart = "shield_defense=";
        string shield_defense_textend = "";
        public string safe_height;
        string safe_height_textstart = "safe_height=";
        string safe_height_textend = "";
        public string soulshot_count;
        string soulshot_count_textstart = "soulshot_count=";
        string soulshot_count_textend = "";
        public string spiritshot_count;
        string spiritshot_count_textstart = "spiritshot_count=";
        string spiritshot_count_textend = "";
        public string clan;
        string clan_textstart = "clan={";
        string clan_textend = "}";
        public string ignore_clan_list;
        string ignore_clan_list_textstart = "ignore_clan_list={";
        string ignore_clan_list_textend = "}";
        public string clan_help_range;
        string clan_help_range_textstart = "clan_help_range=";
        string clan_help_range_textend = "";
        public string undying;
        string undying_textstart = "undying=";
        string undying_textend = "";
        public string can_be_attacked;
        string can_be_attacked_textstart = "can_be_attacked=";
        string can_be_attacked_textend = "";
        public string corpse_time;
        string corpse_time_textstart = "corpse_time=";
        string corpse_time_textend = "";
        public string no_sleep_mode;
        string no_sleep_mode_textstart = "no_sleep_mode=";
        string no_sleep_mode_textend = "";
        public string agro_range;
        string agro_range_textstart = "agro_range=";
        string agro_range_textend = "";
        public string passable_door;
        string passable_door_textstart = "passable_door=";
        string passable_door_textend = "";
        public string can_move;
        string can_move_textstart = "can_move=";
        string can_move_textend = "";
        public string flying;
        string flying_textstart = "flying=";
        string flying_textend = "";
        public string has_summoner;
        string has_summoner_textstart = "has_summoner=";
        string has_summoner_textend = "";
        public string targetable;
        string targetable_textstart = "targetable=";
        string targetable_textend = "";
        public string show_name_tag;
        string show_name_tag_textstart = "show_name_tag=";
        string show_name_tag_textend = "";

        //ai
        public string npc_ai_id;
        public List<string> npc_ai_variables;
        public List<string> npc_ai_values;
        public string npc_ai_privates_string;

        public string npc_ai;
        string npc_ai_textstart = "npc_ai=";
        string npc_ai_textend = "";
        public string event_flag;
        string event_flag_textstart = "event_flag={";
        string event_flag_textend = "}";
        public string unsowing;
        string unsowing_textstart = "unsowing=";
        string unsowing_textend = "";

        public string private_respawn_log; //h5
        string private_respawn_log_textstart = "private_respawn_log=";
        string private_respawn_log_textend = "";

        public string acquire_exp_rate;
        string acquire_exp_rate_textstart = "acquire_exp_rate=";
        string acquire_exp_rate_textend = "";
        public string acquire_sp;
        string acquire_sp_textstart = "acquire_sp=";
        string acquire_sp_textend = "";
        public string acquire_rp;
        string acquire_rp_textstart = "acquire_rp=";
        string acquire_rp_textend = "";

        public Server_Droplist corpse_make_list_droplist;
        string corpse_make_list;
        string corpse_make_list_textstart = "corpse_make_list={";
        string corpse_make_list_textend = "}";
        public string target_corpse_droplist_id;

        public Server_Droplist additional_make_list_droplist;
        string additional_make_list;
        string additional_make_list_textstart = "additional_make_list={";
        string additional_make_list_textend = "}";
        public string target_additional_make_list_droplist_id;

        public Server_Multi_Droplist additional_make_multi_list_droplist;
        string additional_make_multi_list;
        string additional_make_multi_list_textstart = "additional_make_multi_list={";
        string additional_make_multi_list_textend = "}";
        public string target_additional_make_multi_list_droplist_id;

        public Server_Multi_Droplist ex_item_drop_list_droplist;
        string ex_item_drop_list;
        string ex_item_drop_list_textstart = "ex_item_drop_list={";
        string ex_item_drop_list_textend = "}";
        public string target_ex_item_drop_list_droplist_id;

        public string fake_class_id; //h5
        string fake_class_id_textstart = "fake_class_id=";
        string fake_class_id_textend = "";

        string endText;//npc_end

        public string invalidString = "";
        public bool invalidNpc = false;

        public bool useDropModule;
        public L2H_Module dropModule;


        public Server_Npcdata()
        {

        }

        public Server_Npcdata(string line, Pages.NPCsPage npcsPage)
        {
            string[] dataLine = line.Split('\t');

            beginning_text = dataLine[0];
            npcType = dataLine[1];
            npcId = dataLine[2];
            npcName = StripExcessServerText(npcName_textstart, dataLine[3], npcName_textend).Replace(" ", "");

            if (CheckIfInvalidNpc())
            {
                invalidNpc = true;
                return;
            }


            category = StripExcessServerText(category_textstart, dataLine[4], category_textend);
            level = StripExcessServerText(level_textstart, dataLine[5], level_textend);
            exp = StripExcessServerText(exp_textstart, dataLine[6], exp_textend);
            ex_crt_effect = StripExcessServerText(ex_crt_effect_textstart, dataLine[7], ex_crt_effect_textend);
            unique = StripExcessServerText(unique_textstart, dataLine[8], unique_textend);
            s_npc_prop_hp_rate = StripExcessServerText(s_npc_prop_hp_rate_textstart, dataLine[9], s_npc_prop_hp_rate_textend);
            race = StripExcessServerText(race_textstart, dataLine[10], race_textend);
            sex = StripExcessServerText(sex_textstart, dataLine[11], sex_textend);
            skill_list_string = StripExcessServerText(skill_list_textstart, dataLine[12], skill_list_textend);
            slot_chest = StripExcessServerText(slot_chest_textstart, dataLine[13], slot_chest_textend);
            slot_rhand = StripExcessServerText(slot_rhand_textstart, dataLine[14], slot_rhand_textend);
            slot_lhand = StripExcessServerText(slot_lhand_textstart, dataLine[15], slot_lhand_textend);
            collision_radius = StripExcessServerText(collision_radius_textstart, dataLine[16], collision_radius_textend);
            collision_height = StripExcessServerText(collision_height_textstart, dataLine[17], collision_height_textend);
            hit_time_factor = StripExcessServerText(hit_time_factor_textstart, dataLine[18], hit_time_factor_textend);
            hit_time_factor_skill = StripExcessServerText(hit_time_factor_skill_textstart, dataLine[19], hit_time_factor_skill_textend);
            ground_high = StripExcessServerText(ground_high_textstart, dataLine[20], ground_high_textend);
            ground_low = StripExcessServerText(ground_low_textstart, dataLine[21], ground_low_textend);
            strength = StripExcessServerText(strength_textstart, dataLine[22], strength_textend);
            intelligence = StripExcessServerText(intelligence_textstart, dataLine[23], intelligence_textend);
            dexterity = StripExcessServerText(dexterity_textstart, dataLine[24], dexterity_textend);
            wit = StripExcessServerText(wit_textstart, dataLine[25], wit_textend);
            con = StripExcessServerText(con_textstart, dataLine[26], con_textend);
            men = StripExcessServerText(men_textstart, dataLine[27], men_textend);
            org_hp = StripExcessServerText(org_hp_textstart, dataLine[28], org_hp_textend);
            org_hp_regen = StripExcessServerText(org_hp_regen_textstart, dataLine[29], org_hp_regen_textend);
            org_mp = StripExcessServerText(org_mp_textstart, dataLine[30], org_mp_textend);
            org_mp_regen = StripExcessServerText(org_mp_regen_textstart, dataLine[31], org_mp_regen_textend);
            base_attack_type = StripExcessServerText(base_attack_type_textstart, dataLine[32], base_attack_type_textend);
            base_attack_range = StripExcessServerText(base_attack_range_textstart, dataLine[33], base_attack_range_textend);
            base_damage_range = StripExcessServerText(base_damage_range_textstart, dataLine[34], base_damage_range_textend);
            base_rand_dam = StripExcessServerText(base_rand_dam_textstart, dataLine[35], base_rand_dam_textend);
            base_physical_attack = StripExcessServerText(base_physical_attack_textstart, dataLine[36], base_physical_attack_textend);
            base_critical = StripExcessServerText(base_critical_textstart, dataLine[37], base_critical_textend);
            physical_hit_modify = StripExcessServerText(physical_hit_modify_textstart, dataLine[38], physical_hit_modify_textend);
            base_attack_speed = StripExcessServerText(base_attack_speed_textstart, dataLine[39], base_attack_speed_textend);
            base_reuse_delay = StripExcessServerText(base_reuse_delay_textstart, dataLine[40], base_reuse_delay_textend);
            base_magic_attack = StripExcessServerText(base_magic_attack_textstart, dataLine[41], base_magic_attack_textend);
            base_defend = StripExcessServerText(base_defend_textstart, dataLine[42], base_defend_textend);
            base_magic_defend = StripExcessServerText(base_magic_defend_textstart, dataLine[43], base_magic_defend_textend);
            base_attribute_attack = StripExcessServerText(base_attribute_attack_textstart, dataLine[44], base_attribute_attack_textend);
            base_attribute_defend = StripExcessServerText(base_attribute_defend_textstart, dataLine[45], base_attribute_defend_textend);
            physical_avoid_modify = StripExcessServerText(physical_avoid_modify_textstart, dataLine[46], physical_avoid_modify_textend);
            shield_defense_rate = StripExcessServerText(shield_defense_rate_textstart, dataLine[47], shield_defense_rate_textend);
            shield_defense = StripExcessServerText(shield_defense_textstart, dataLine[48], shield_defense_textend);
            safe_height = StripExcessServerText(safe_height_textstart, dataLine[49], safe_height_textend);
            soulshot_count = StripExcessServerText(soulshot_count_textstart, dataLine[50], soulshot_count_textend);
            spiritshot_count = StripExcessServerText(spiritshot_count_textstart, dataLine[51], spiritshot_count_textend);
            clan = StripExcessServerText(clan_textstart, dataLine[52], clan_textend);
            ignore_clan_list = StripExcessServerText(ignore_clan_list_textstart, dataLine[53], ignore_clan_list_textend);
            clan_help_range = StripExcessServerText(clan_help_range_textstart, dataLine[54], clan_help_range_textend);
            undying = StripExcessServerText(undying_textstart, dataLine[55], undying_textend);
            can_be_attacked = StripExcessServerText(can_be_attacked_textstart, dataLine[56], can_be_attacked_textend);
            corpse_time = StripExcessServerText(corpse_time_textstart, dataLine[57], corpse_time_textend);
            no_sleep_mode = StripExcessServerText(no_sleep_mode_textstart, dataLine[58], no_sleep_mode_textend);
            agro_range = StripExcessServerText(agro_range_textstart, dataLine[59], agro_range_textend);
            passable_door = StripExcessServerText(passable_door_textstart, dataLine[60], passable_door_textend);
            can_move = StripExcessServerText(can_move_textstart, dataLine[61], can_move_textend);
            flying = StripExcessServerText(flying_textstart, dataLine[62], flying_textend);
            has_summoner = StripExcessServerText(has_summoner_textstart, dataLine[63], has_summoner_textend);
            targetable = StripExcessServerText(targetable_textstart, dataLine[64], targetable_textend);
            show_name_tag = StripExcessServerText(show_name_tag_textstart, dataLine[65], show_name_tag_textend);

            npc_ai_variables = new List<string>();
            npc_ai_values = new List<string>();

            GetAIVariables(dataLine[66], out npc_ai_id, out npc_ai_variables, out npc_ai_values);

            event_flag = StripExcessServerText(event_flag_textstart, dataLine[67], event_flag_textend);
            unsowing = StripExcessServerText(unsowing_textstart, dataLine[68], unsowing_textend);
            private_respawn_log = StripExcessServerText(private_respawn_log_textstart, dataLine[69], private_respawn_log_textend);
            acquire_exp_rate = StripExcessServerText(acquire_exp_rate_textstart, dataLine[70], acquire_exp_rate_textend);
            acquire_sp = StripExcessServerText(acquire_sp_textstart, dataLine[71], acquire_sp_textend);
            acquire_rp = StripExcessServerText(acquire_rp_textstart, dataLine[72], acquire_rp_textend);

            tempSpoilDroplistString = StripExcessServerText(corpse_make_list_textstart, dataLine[73], corpse_make_list_textend);
            tempNormDropListString = StripExcessServerText(additional_make_list_textstart, dataLine[74], additional_make_list_textend);
            tempMultiDroplistString = StripExcessServerText(additional_make_multi_list_textstart, dataLine[75], additional_make_multi_list_textend);
            tempExtraDroplistString = StripExcessServerText(ex_item_drop_list_textstart, dataLine[76], ex_item_drop_list_textend);


            fake_class_id = StripExcessServerText(fake_class_id_textstart, dataLine[77], fake_class_id_textend);

            endText = dataLine[78];

        }

        public void HandleDroplists(Pages.DroplistsPage droplistsPage)
        {
            //Droplists!
            if (droplistsPage.existingDroplistsFound)
            {
                L2H_Npc_Droplist_Pointer p = droplistsPage.npc_Droplist_Pointers.Find(x => x.npc_id == npcId);

                additional_make_list_droplist = droplistsPage.Get_Single_Droplist(npcId, DroplistSlot.normal, p);
                corpse_make_list_droplist = droplistsPage.Get_Single_Droplist(npcId, DroplistSlot.spoil, p);
                additional_make_multi_list_droplist = droplistsPage.Get_Multi_Droplist(npcId, DroplistSlot.multi, p);
                ex_item_drop_list_droplist = droplistsPage.Get_Multi_Droplist(npcId, DroplistSlot.extra, p);

            }
            else
            {
                //If originalDroplists aren't loaded, this is when they're created
                additional_make_list_droplist = droplistsPage.Create_Single_Droplist(npcId + "_" + "normal", false, tempNormDropListString);
                corpse_make_list_droplist = droplistsPage.Create_Single_Droplist(npcId + "_" + "spoil", false, tempSpoilDroplistString);
                additional_make_multi_list_droplist = droplistsPage.Create_Multi_Droplist(npcId + "_" + "multi", false, tempMultiDroplistString);
                ex_item_drop_list_droplist = droplistsPage.Create_Multi_Droplist(npcId + "_" + "extra", false, tempExtraDroplistString);

                droplistsPage.multi_Droplist_Pointers.Add(new L2H_Multi_Droplist_Pointer(additional_make_multi_list_droplist.id, additional_make_multi_list_droplist.isCustom, additional_make_multi_list_droplist.separateDroplistIDs, additional_make_multi_list_droplist.separateDroplistChances));
                droplistsPage.multi_Droplist_Pointers.Add(new L2H_Multi_Droplist_Pointer(ex_item_drop_list_droplist.id, ex_item_drop_list_droplist.isCustom, ex_item_drop_list_droplist.separateDroplistIDs, ex_item_drop_list_droplist.separateDroplistChances));

                for (int i = 0; i < additional_make_multi_list_droplist.separateDroplists.Count; i++)
                {
                    droplistsPage.single_Droplist_Data.Add(additional_make_multi_list_droplist.separateDroplists[i]);
                }
                for (int i = 0; i < ex_item_drop_list_droplist.separateDroplists.Count; i++)
                {
                    droplistsPage.single_Droplist_Data.Add(ex_item_drop_list_droplist.separateDroplists[i]);
                }

                droplistsPage.npc_Droplist_Pointers.Add(new L2H_Npc_Droplist_Pointer(npcId, additional_make_list_droplist.id, corpse_make_list_droplist.id, additional_make_multi_list_droplist.id, ex_item_drop_list_droplist.id));

            }
        }

        public string GetExportString(L2H_Settings settings = null)
        {
            if (invalidNpc)
                return invalidString;

            string returnString = beginning_text + '\t' +
                npcType + '\t' +
                npcId + '\t' +
                ConvertToServerText(npcName_textstart, npcName, npcName_textend) + '\t' +
                ConvertToServerText(category_textstart, category, category_textend) + '\t' +
                ConvertToServerText(level_textstart, level, level_textend) + '\t' +
                ConvertToServerText(exp_textstart, exp, exp_textend) + '\t' +
                ConvertToServerText(ex_crt_effect_textstart, ex_crt_effect, ex_crt_effect_textend) + '\t' +
                ConvertToServerText(unique_textstart, unique, unique_textend) + '\t' +
                ConvertToServerText(s_npc_prop_hp_rate_textstart, s_npc_prop_hp_rate, s_npc_prop_hp_rate_textend) + '\t' +
                ConvertToServerText(race_textstart, race, race_textend) + '\t' +
                ConvertToServerText(sex_textstart, sex, sex_textend) + '\t' +
                ConvertToServerText(skill_list_textstart, skill_list_string, skill_list_textend) + '\t' +
                ConvertToServerText(slot_chest_textstart, slot_chest, slot_chest_textend) + '\t' +
                ConvertToServerText(slot_rhand_textstart, slot_rhand, slot_rhand_textend) + '\t' +
                ConvertToServerText(slot_lhand_textstart, slot_lhand, slot_lhand_textend) + '\t' +
                ConvertToServerText(collision_radius_textstart, collision_radius, collision_radius_textend) + '\t' +
                ConvertToServerText(collision_height_textstart, collision_height, collision_height_textend) + '\t' +
                ConvertToServerText(hit_time_factor_textstart, hit_time_factor, hit_time_factor_textend) + '\t' +
                ConvertToServerText(hit_time_factor_skill_textstart, hit_time_factor_skill, hit_time_factor_skill_textend) + '\t' +
                ConvertToServerText(ground_high_textstart, ground_high, ground_high_textend) + '\t' +
                ConvertToServerText(ground_low_textstart, ground_low, ground_low_textend) + '\t' +
                ConvertToServerText(strength_textstart, strength, strength_textend) + '\t' +
                ConvertToServerText(intelligence_textstart, intelligence, intelligence_textend) + '\t' +
                ConvertToServerText(dexterity_textstart, dexterity, dexterity_textend) + '\t' +
                ConvertToServerText(wit_textstart, wit, wit_textend) + '\t' +
                ConvertToServerText(con_textstart, con, con_textend) + '\t' +
                ConvertToServerText(men_textstart, men, men_textend) + '\t' +
                ConvertToServerText(org_hp_textstart, org_hp, org_hp_textend) + '\t' +
                ConvertToServerText(org_hp_regen_textstart, org_hp_regen, org_hp_regen_textend) + '\t' +
                ConvertToServerText(org_mp_textstart, org_mp, org_mp_textend) + '\t' +
                ConvertToServerText(org_mp_regen_textstart, org_mp_regen, org_mp_regen_textend) + '\t' +
                ConvertToServerText(base_attack_type_textstart, base_attack_type, base_attack_type_textend) + '\t' +
                ConvertToServerText(base_attack_range_textstart, base_attack_range, base_attack_range_textend) + '\t' +
                ConvertToServerText(base_damage_range_textstart, base_damage_range, base_damage_range_textend) + '\t' +
                ConvertToServerText(base_rand_dam_textstart, base_rand_dam, base_rand_dam_textend) + '\t' +
                ConvertToServerText(base_physical_attack_textstart, base_physical_attack, base_physical_attack_textend) + '\t' +
                ConvertToServerText(base_critical_textstart, base_critical, base_critical_textend) + '\t' +
                ConvertToServerText(physical_hit_modify_textstart, physical_hit_modify, physical_hit_modify_textend) + '\t' +
                ConvertToServerText(base_attack_speed_textstart, base_attack_speed, base_attack_speed_textend) + '\t' +
                ConvertToServerText(base_reuse_delay_textstart, base_reuse_delay, base_reuse_delay_textend) + '\t' +
                ConvertToServerText(base_magic_attack_textstart, base_magic_attack, base_magic_attack_textend) + '\t' +
                ConvertToServerText(base_defend_textstart, base_defend, base_defend_textend) + '\t' +
                ConvertToServerText(base_magic_defend_textstart, base_magic_defend, base_magic_defend_textend) + '\t' +
                ConvertToServerText(base_attribute_attack_textstart, base_attribute_attack, base_attribute_attack_textend) + '\t' +
                ConvertToServerText(base_attribute_defend_textstart, base_attribute_defend, base_attribute_defend_textend) + '\t' +
                ConvertToServerText(physical_avoid_modify_textstart, physical_avoid_modify, physical_avoid_modify_textend) + '\t' +
                ConvertToServerText(shield_defense_rate_textstart, shield_defense_rate, shield_defense_rate_textend) + '\t' +
                ConvertToServerText(shield_defense_textstart, shield_defense, shield_defense_textend) + '\t' +
                ConvertToServerText(safe_height_textstart, safe_height, safe_height_textend) + '\t' +
                ConvertToServerText(soulshot_count_textstart, soulshot_count, soulshot_count_textend) + '\t' +
                ConvertToServerText(spiritshot_count_textstart, spiritshot_count, spiritshot_count_textend) + '\t' +
                ConvertToServerText(clan_textstart, clan, clan_textend) + '\t' +
                ConvertToServerText(ignore_clan_list_textstart, ignore_clan_list, ignore_clan_list_textend) + '\t' +
                ConvertToServerText(clan_help_range_textstart, clan_help_range, clan_help_range_textend) + '\t' +
                ConvertToServerText(undying_textstart, undying, undying_textend) + '\t' +
                ConvertToServerText(can_be_attacked_textstart, can_be_attacked, can_be_attacked_textend) + '\t' +
                ConvertToServerText(corpse_time_textstart, corpse_time, corpse_time_textend) + '\t' +
                ConvertToServerText(no_sleep_mode_textstart, no_sleep_mode, no_sleep_mode_textend) + '\t' +
                ConvertToServerText(agro_range_textstart, agro_range, agro_range_textend) + '\t' +
                ConvertToServerText(passable_door_textstart, passable_door, passable_door_textend) + '\t' +
                ConvertToServerText(can_move_textstart, can_move, can_move_textend) + '\t' +
                ConvertToServerText(flying_textstart, flying, flying_textend) + '\t' +
                ConvertToServerText(has_summoner_textstart, has_summoner, has_summoner_textend) + '\t' +
                ConvertToServerText(targetable_textstart, targetable, targetable_textend) + '\t' +
                ConvertToServerText(show_name_tag_textstart, show_name_tag, show_name_tag_textend) + '\t' +

                ConvertAIToServerText(npc_ai_textstart, npc_ai_id, npc_ai_variables, npc_ai_values, npc_ai_textend) + '\t' +
              
                ConvertToServerText(event_flag_textstart, event_flag, event_flag_textend) + '\t' +
                ConvertToServerText(unsowing_textstart, unsowing, unsowing_textend) + '\t' +
                ConvertToServerText(private_respawn_log_textstart, private_respawn_log, private_respawn_log_textend) + '\t' +
                ConvertToServerText(acquire_exp_rate_textstart, acquire_exp_rate, acquire_exp_rate_textend) + '\t' +
                ConvertToServerText(acquire_sp_textstart, acquire_sp, acquire_sp_textend) + '\t' +
                ConvertToServerText(acquire_rp_textstart, acquire_rp, acquire_rp_textend) + '\t';


            if (useDropModule && npcType == "warrior" && int.Parse(npcId) > 37699)
            {
                int tryGetLevel = 0;
                int.TryParse(level, out tryGetLevel);
                returnString += "corpse_make_list={}" + "\t" + "additional_make_list={}" + "\t" + "additional_make_multi_list=" + dropModule.GetDropsForLevel(tryGetLevel) + "\t" + "ex_item_drop_list={}" + "\t";
            }
            else
            {
                returnString +=
                ConvertDroplistToServertext(corpse_make_list_textstart, corpse_make_list_droplist, corpse_make_list_textend, settings) + '\t' +
                ConvertDroplistToServertext(additional_make_list_textstart, additional_make_list_droplist, additional_make_list_textend, settings) + '\t' +
                ConvertMultiDroplistToServerText(additional_make_multi_list_textstart, additional_make_multi_list_droplist, additional_make_multi_list_textend, settings) + '\t' +
                ConvertMultiDroplistToServerText(ex_item_drop_list_textstart, ex_item_drop_list_droplist, ex_item_drop_list_textend, settings) + '\t';
            }

            returnString += ConvertToServerText(fake_class_id_textstart, fake_class_id, fake_class_id_textend) + '\t';

            returnString += endText;

            return returnString;

        }

        private bool CheckIfInvalidNpc()
        {
            List<string> invalidNpcIDs = new List<string>();

            invalidNpcIDs.Add("37501");
            invalidNpcIDs.Add("37502");
            invalidNpcIDs.Add("37503");
            invalidNpcIDs.Add("37504");
            invalidNpcIDs.Add("37505");
            invalidNpcIDs.Add("37506");
            invalidNpcIDs.Add("37507");
            invalidNpcIDs.Add("37508");
            invalidNpcIDs.Add("37509");
            invalidNpcIDs.Add("37510");
            invalidNpcIDs.Add("37511");
            invalidNpcIDs.Add("37512");
            invalidNpcIDs.Add("37513");
            invalidNpcIDs.Add("37514");
            invalidNpcIDs.Add("37515");
            invalidNpcIDs.Add("37516");
            invalidNpcIDs.Add("37517");
            invalidNpcIDs.Add("37518");

            return (invalidNpcIDs.Exists(x => x == this.npcId));


        }

        #region utility
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

        public void GetAIVariables(string sourceString, out string id, out List<string> variableNames, out List<string> variableValues)
        {
            sourceString = sourceString.Replace(" ", "");
            string[] splitStrings = sourceString.Split(';');

            string ai_id = splitStrings[0].Remove(0, 9);
            ai_id = ai_id.Remove(ai_id.Length - 1, 1);
            id = ai_id;


            List<string> aiVariableNames = new List<string>();
            List<string> aiVariableValues = new List<string>();

            bool privatesFound = false;

            for (int i = 1; i < splitStrings.Length; i++)
            {
                if (!privatesFound)
                {
                    string[] splitVariable = splitStrings[i].Split('=');

                    //If extra } is found, don't count it as a variable
                    if (splitVariable[0].Length > 2)
                    {
                        string trimmedVariableName = splitVariable[0].Remove(0, 2);
                        trimmedVariableName = trimmedVariableName.Remove(trimmedVariableName.Length - 1, 1);
                        if (trimmedVariableName != "Privates" && trimmedVariableName != "Privates]")
                        {
                            aiVariableNames.Add(trimmedVariableName);

                            if (i == splitStrings.Length - 1)
                            {
                                aiVariableValues.Add(splitVariable[1].Remove(splitVariable[1].Length - 2, 2));
                            }
                            else
                            {
                                aiVariableValues.Add(splitVariable[1].Remove(splitVariable[1].Length - 1, 1));
                            }
                        }
                        else
                        {
                            privatesFound = true;

                        }
                    }
                }
                else
                {
                    //Figure out what to do with the privates here
                    //npc_ai_privates_string = npc_ai_privates_string + splitStrings[i];
                }
            }

            if (privatesFound)
            {
                npc_ai_privates_string = sourceString.Substring(sourceString.LastIndexOf("[Privates]"));
            }

            variableNames = aiVariableNames;
            variableValues = aiVariableValues;

            id = id.Replace("]", "");

        }


        public string ConvertAIToServerText(string textstart, string id, List<string> npc_ai_variables, List<string> npc_ai_values, string npc_ai_textend)
        {
            string returnString = textstart;
            returnString = returnString + "{[" + id + "]";

            if (npc_ai_variables.Count > 0)
            {
                returnString = returnString + ";";
            }
            else
                returnString = returnString + "}";

            for (int i = 0; i < npc_ai_variables.Count; i++)
            {
                returnString = returnString + "{[" + npc_ai_variables[i] + "]=" + npc_ai_values[i] + "}";
                if (i != npc_ai_variables.Count - 1)
                    returnString = returnString + ";";
                else
                    returnString = returnString + "}";
            }

            if (!string.IsNullOrEmpty(npc_ai_privates_string))
            {
                returnString = returnString.Remove(returnString.Length - 1, 1);
                returnString += ";{" + npc_ai_privates_string;
            }

            return returnString;
        }

        public string ConvertDroplistToServertext(string textstart, Server_Droplist targetDroplist, string textend, L2H_Settings settings = null)
        {

            string returnString = textstart;

            for (int i = 0; i < targetDroplist.itemDrops.Count; i++)
            {
                returnString = returnString + "{[" +
                    targetDroplist.itemDrops[i].itemID + "];" +
                    targetDroplist.itemDrops[i].minimumAmount + ";" +
                    targetDroplist.itemDrops[i].maximumAmount + ";" +
                    targetDroplist.itemDrops[i].probability + "}";

                if (i != targetDroplist.itemDrops.Count - 1)
                {
                    returnString = returnString + ";";
                }
            }

            return returnString + textend;
        }

        public string ConvertMultiDroplistToServerText(string textstart, Server_Multi_Droplist targetDroplist, string textend, L2H_Settings settings = null)
        {
            string returnString = textstart;
            bool hadValidDroplist = false;
            for (int i = 0; i < targetDroplist.separateDroplists.Count; i++)
            {
                if (targetDroplist.separateDroplists[i].itemDrops.Count > 0)
                {
                    returnString = returnString + "{{";


                    for (int j = 0; j < targetDroplist.separateDroplists[i].itemDrops.Count; j++)
                    {
                        returnString = returnString + "{[" +
                        targetDroplist.separateDroplists[i].itemDrops[j].itemID + "];" +
                        targetDroplist.separateDroplists[i].itemDrops[j].minimumAmount + ";" +
                        targetDroplist.separateDroplists[i].itemDrops[j].maximumAmount + ";" +
                        targetDroplist.separateDroplists[i].itemDrops[j].probability + "}";

                        if (j != targetDroplist.separateDroplists[i].itemDrops.Count - 1)
                        {
                            returnString = returnString + ";";
                        }
                    }

                    returnString = returnString + "};" + targetDroplist.separateDroplistChances[i];



                    if (i != targetDroplist.separateDroplists.Count - 1)
                    {
                        returnString = returnString + "};";
                    }
                    else
                    {
                        returnString = returnString + "}";
                    }

                    hadValidDroplist = true;
                }
                else
                {
                    if (hadValidDroplist)
                    {
                        if (i == targetDroplist.separateDroplists.Count - 1 && targetDroplist.separateDroplists.Count > 1)
                            returnString = returnString.Remove(returnString.Length - 1, 1);
                    }
                    else
                    {

                    }
                }
            }

            returnString = returnString + textend;
            return returnString;

        }

        public string GetPCHExportString()
        {
            return "[" + npcName + "]" + " = 10" + npcId;
        }

        #endregion

    }
}
