using System.Collections.Generic;

namespace L2Homage
{
    public class Server_PC_Parameter
    {
        public Base_Parameter_Single_Value base_physical_attack;
        public Base_Parameter_Single_Value base_critical;
        public Base_Parameter_Single_Value base_attack_type;
        public Base_Parameter_Single_Value base_attack_speed;
        public Base_Parameter_Single_Line_Multivalue base_defend;
        public Base_Parameter_Single_Value base_magic_attack;
        public Base_Parameter_Single_Line_Multivalue base_magic_defend;
        public Base_Parameter_Single_Value base_can_penetrate;
        public Base_Parameter_Single_Value base_attack_range;
        public Base_Parameter_Single_Line_Multivalue base_damage_range;
        public Base_Parameter_Single_Value base_rand_dam;
        public Base_Parameter_Multiple_Line_Multivalue level_bonus;
        public Base_Parameter_Multiple_Line_Multivalue str_bonus;
        public Base_Parameter_Multiple_Line_Multivalue int_bonus;
        public Base_Parameter_Multiple_Line_Multivalue con_bonus;
        public Base_Parameter_Multiple_Line_Multivalue men_bonus;
        public Base_Parameter_Multiple_Line_Multivalue dex_bonus;
        public Base_Parameter_Multiple_Line_Multivalue wit_bonus;
        public Base_Parameter_Single_Line_Multivalue org_hp_regen;
        public Base_Parameter_Single_Line_Multivalue org_mp_regen;
        public Base_Parameter_Single_Line_Multivalue org_cp_regen;
        public Base_Parameter_Single_Line_Multivalue moving_speed;
        public Base_Parameter_Multiple_Line_Multivalue org_hp_regen_weight;
        public Base_Parameter_Multiple_Line_Multivalue org_mp_regen_weight;
        public Base_Parameter_Multiple_Line_Multivalue org_cp_regen_weight;
        public Base_Parameter_Multiple_Line_Multivalue noise_move_mode_bonus;
        public Base_Parameter_Single_Value org_jump;
        public Base_Parameter_Multiple_Line_Multivalue hit_cond_bonus;
        public List<Base_Parameter_Multiple_Line_Multivalue> hp_tables;
        public List<Base_Parameter_Multiple_Line_Multivalue> mp_tables;
        public List<Base_Parameter_Multiple_Line_Multivalue> cp_tables;
        public Base_Parameter_Single_Value pc_breath_bonus_table;
        public Base_Parameter_Single_Value pc_safe_fall_height_table;
        public Base_Parameter_Multiple_Line_Multivalue pc_collision_box_table;
        public Base_Parameter_Multiple_Line_Multivalue pc_karma_increase_table;
        public Base_Parameter_Multiple_Line_Multivalue pc_karma_increase_constant;

        public Server_PC_Parameter()
        {
            hp_tables = new List<Base_Parameter_Multiple_Line_Multivalue>();
            mp_tables = new List<Base_Parameter_Multiple_Line_Multivalue>();
            cp_tables = new List<Base_Parameter_Multiple_Line_Multivalue>();
        }

        public void AddData(string data, string parameterType, string classID = "")
        {

            if (string.IsNullOrEmpty(classID))
            {
                switch (parameterType)
                {
                    case "base_physical_attack_begin":
                        {
                            if (base_physical_attack == null)
                                base_physical_attack = new Base_Parameter_Single_Value("base_physical_attack_begin");
                            string[] splitData = data.Split('=');
                            base_physical_attack.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "base_critical_begin":
                        {
                            if (base_critical == null)
                                base_critical = new Base_Parameter_Single_Value("base_critical_begin");
                            string[] splitData = data.Split('=');
                            base_critical.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "base_attack_type_begin":
                        {
                            if (base_attack_type == null)
                                base_attack_type = new Base_Parameter_Single_Value("base_attack_type_begin");
                            string[] splitData = data.Split('=');
                            base_attack_type.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "base_attack_speed_begin":
                        {
                            if (base_attack_speed == null)
                                base_attack_speed = new Base_Parameter_Single_Value("base_attack_speed_begin");
                            string[] splitData = data.Split('=');
                            base_attack_speed.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "base_defend_begin":
                        {
                            if (base_defend == null)
                                base_defend = new Base_Parameter_Single_Line_Multivalue("base_defend_begin");
                            string[] splitData = data.Split('=');
                            base_defend.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "base_magic_attack_begin":
                        {
                            if (base_magic_attack == null)
                                base_magic_attack = new Base_Parameter_Single_Value("base_magic_attack_begin");
                            string[] splitData = data.Split('=');
                            base_magic_attack.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "base_magic_defend_begin":
                        {
                            if (base_magic_defend == null)
                                base_magic_defend = new Base_Parameter_Single_Line_Multivalue("base_magic_defend_begin");
                            string[] splitData = data.Split('=');
                            base_magic_defend.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "base_can_penetrate_begin":
                        {
                            if (base_can_penetrate == null)
                                base_can_penetrate = new Base_Parameter_Single_Value("base_can_penetrate_begin");
                            string[] splitData = data.Split('=');
                            base_can_penetrate.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "base_attack_range_begin":
                        {
                            if (base_attack_range == null)
                                base_attack_range = new Base_Parameter_Single_Value("base_attack_range_begin");
                            string[] splitData = data.Split('=');
                            base_attack_range.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "base_damage_range_begin":
                        {
                            if (base_damage_range == null)
                                base_damage_range = new Base_Parameter_Single_Line_Multivalue("base_damage_range_begin");
                            string[] splitData = data.Split('=');
                            base_damage_range.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "base_rand_dam_begin":
                        {
                            if (base_rand_dam == null)
                                base_rand_dam = new Base_Parameter_Single_Value("base_rand_dam_begin");
                            string[] splitData = data.Split('=');
                            base_rand_dam.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "level_bonus_begin":
                        {
                            if (level_bonus == null)
                                level_bonus = new Base_Parameter_Multiple_Line_Multivalue("level_bonus_begin");
                            level_bonus.AddValue(data);
                        }
                        break;
                    case "str_bonus_begin":
                        {
                            if (str_bonus == null)
                                str_bonus = new Base_Parameter_Multiple_Line_Multivalue("str_bonus_begin");
                            str_bonus.AddValue(data);
                        }
                        break;
                    case "int_bonus_begin":
                        {
                            if (int_bonus == null)
                                int_bonus = new Base_Parameter_Multiple_Line_Multivalue("int_bonus_begin");
                            int_bonus.AddValue(data);
                        }
                        break;
                    case "con_bonus_begin":
                        {
                            if (con_bonus == null)
                                con_bonus = new Base_Parameter_Multiple_Line_Multivalue("con_bonus_begin");
                            con_bonus.AddValue(data);
                        }
                        break;
                    case "men_bonus_begin":
                        {
                            if (men_bonus == null)
                                men_bonus = new Base_Parameter_Multiple_Line_Multivalue("men_bonus_begin");
                            men_bonus.AddValue(data);
                        }
                        break;
                    case "dex_bonus_begin":
                        {
                            if (dex_bonus == null)
                                dex_bonus = new Base_Parameter_Multiple_Line_Multivalue("dex_bonus_begin");
                            dex_bonus.AddValue(data);
                        }
                        break;
                    case "wit_bonus_begin":
                        {
                            if (wit_bonus == null)
                                wit_bonus = new Base_Parameter_Multiple_Line_Multivalue("wit_bonus_begin");
                            wit_bonus.AddValue(data);
                        }
                        break;
                    case "org_hp_regen_begin":
                        {
                            if (org_hp_regen == null)
                                org_hp_regen = new Base_Parameter_Single_Line_Multivalue("org_hp_regen_begin");
                            string[] splitData = data.Split('=');
                            org_hp_regen.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "org_mp_regen_begin":
                        {
                            if (org_mp_regen == null)
                                org_mp_regen = new Base_Parameter_Single_Line_Multivalue("org_mp_regen_begin");
                            string[] splitData = data.Split('=');
                            org_mp_regen.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "org_cp_regen_begin":
                        {
                            if (org_cp_regen == null)
                                org_cp_regen = new Base_Parameter_Single_Line_Multivalue("org_cp_regen_begin");
                            string[] splitData = data.Split('=');
                            org_cp_regen.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "moving_speed_begin":
                        {
                            if (moving_speed == null)
                                moving_speed = new Base_Parameter_Single_Line_Multivalue("moving_speed_begin");
                            string[] splitData = data.Split('=');
                            moving_speed.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "org_hp_regen_weight_begin":
                        {
                            if (org_hp_regen_weight == null)
                                org_hp_regen_weight = new Base_Parameter_Multiple_Line_Multivalue("org_hp_regen_weight_begin");
                            org_hp_regen_weight.AddValue(data);
                        }
                        break;
                    case "org_mp_regen_weight_begin":
                        {
                            if (org_mp_regen_weight == null)
                                org_mp_regen_weight = new Base_Parameter_Multiple_Line_Multivalue("org_mp_regen_weight_begin");
                            org_mp_regen_weight.AddValue(data);
                        }
                        break;
                    case "org_cp_regen_weight_begin":
                        {
                            if (org_cp_regen_weight == null)
                                org_cp_regen_weight = new Base_Parameter_Multiple_Line_Multivalue("org_cp_regen_weight_begin");
                            org_cp_regen_weight.AddValue(data);
                        }
                        break;
                    case "noise_move_mode_bonus_begin":
                        {
                            if (noise_move_mode_bonus == null)
                                noise_move_mode_bonus = new Base_Parameter_Multiple_Line_Multivalue("noise_move_mode_bonus_begin");
                            noise_move_mode_bonus.AddValue(data);
                        }
                        break;
                    case "org_jump_begin":
                        {
                            if (org_jump == null)
                                org_jump = new Base_Parameter_Single_Value("org_jump_begin");
                            string[] splitData = data.Split('=');
                            org_jump.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "hit_cond_bonus_begin":
                        {
                            if (hit_cond_bonus == null)
                                hit_cond_bonus = new Base_Parameter_Multiple_Line_Multivalue("hit_cond_bonus_begin");
                            hit_cond_bonus.AddValue(data);
                        }
                        break;
                    case "pc_breath_bonus_table_begin":
                        {
                            if (pc_breath_bonus_table == null)
                                pc_breath_bonus_table = new Base_Parameter_Single_Value("pc_breath_bonus_table_begin");
                            string[] splitData = data.Split('=');
                            pc_breath_bonus_table.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "pc_safe_fall_height_table_begin":
                        {
                            if (pc_safe_fall_height_table == null)
                                pc_safe_fall_height_table = new Base_Parameter_Single_Value("pc_safe_fall_height_table_begin");
                            string[] splitData = data.Split('=');
                            pc_safe_fall_height_table.SetValue((splitData[0].Replace("\t", "").Replace(" ", "")), splitData[1]);
                        }
                        break;
                    case "pc_collision_box_table_begin":
                        {
                            if (pc_collision_box_table == null)
                                pc_collision_box_table = new Base_Parameter_Multiple_Line_Multivalue("pc_collision_box_table_begin");
                            pc_collision_box_table.AddValue(data);
                        }
                        break;
                    case "pc_karma_increase_table_begin":
                        {
                            if (pc_karma_increase_table == null)
                            {
                                pc_karma_increase_table = new Base_Parameter_Multiple_Line_Multivalue("pc_karma_increase_table_begin");
                            }
                            pc_karma_increase_table.AddValue(data);
                        }
                        break;
                    case "pc_karma_increase_constant_begin":
                        {
                            if (pc_karma_increase_constant == null)
                                pc_karma_increase_constant = new Base_Parameter_Multiple_Line_Multivalue("pc_karma_increase_constant_begin");
                            pc_karma_increase_constant.AddValue(data);
                        }
                        break;
                    default:
                        {                            
                            break;
                        }
                }
            }

            else if (!string.IsNullOrEmpty(classID))
            {
                if (parameterType.Contains("_hp_table_begin"))
                {
                    if (!hp_tables.Exists(x => x.classID == parameterType.Replace("_hp_table_begin", "").Replace("\t", "").Replace(" ", "")))
                    {
                        Base_Parameter_Multiple_Line_Multivalue newBaseParameter = new Base_Parameter_Multiple_Line_Multivalue(parameterType.Replace("\t", "").Replace(" ", ""));
                        newBaseParameter.classID = parameterType.Replace("_hp_table_begin", "");
                        hp_tables.Add(newBaseParameter);
                    }

                    Base_Parameter_Multiple_Line_Multivalue targetMultivalue = hp_tables.Find(x => x.classID == classID);
                    targetMultivalue.AddValue(data);
                }
                else if (parameterType.Contains("_mp_table_begin"))
                {
                    if (!mp_tables.Exists(x => x.classID == parameterType.Replace("_mp_table_begin", "").Replace("\t", "").Replace(" ", "")))
                    {
                        Base_Parameter_Multiple_Line_Multivalue newBaseParameter = new Base_Parameter_Multiple_Line_Multivalue(parameterType.Replace("\t", "").Replace(" ", ""));
                        newBaseParameter.classID = parameterType.Replace("_mp_table_begin", "");
                        mp_tables.Add(newBaseParameter);
                    }

                    Base_Parameter_Multiple_Line_Multivalue targetMultivalue = mp_tables.Find(x => x.classID == classID);
                    targetMultivalue.AddValue(data);
                }
                else if (parameterType.Contains("_cp_table_begin"))
                {
                    if (!cp_tables.Exists(x => x.classID == parameterType.Replace("_cp_table_begin", "").Replace("\t", "").Replace(" ", "")))
                    {
                        Base_Parameter_Multiple_Line_Multivalue newBaseParameter = new Base_Parameter_Multiple_Line_Multivalue(parameterType.Replace("\t", "").Replace(" ", ""));
                        newBaseParameter.classID = parameterType.Replace("_cp_table_begin", "");
                        cp_tables.Add(newBaseParameter);
                    }

                    Base_Parameter_Multiple_Line_Multivalue targetMultivalue = cp_tables.Find(x => x.classID == classID);
                    targetMultivalue.AddValue(data);
                }
            }
        }

        public string[] GetExportStrings()
        {
            List<string> exportStrings = new List<string>();
            string[] strings;

            exportStrings.Add("base_physical_attack_begin");
            strings = base_physical_attack.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("base_physical_attack_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("base_critical_begin");
            strings = base_critical.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("base_critical_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("base_attack_type_begin");
            strings = base_attack_type.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("base_attack_type_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("base_attack_speed_begin");
            strings = base_attack_speed.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("base_attack_speed_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("base_defend_begin");
            strings = base_defend.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("base_defend_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("base_magic_attack_begin");
            strings = base_magic_attack.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("base_magic_attack_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("base_magic_defend_begin");
            strings = base_magic_defend.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("base_magic_defend_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("base_can_penetrate_begin");
            strings = base_can_penetrate.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("base_can_penetrate_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("base_attack_range_begin");
            strings = base_attack_range.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("base_attack_range_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("base_damage_range_begin");
            strings = base_damage_range.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("base_damage_range_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("base_rand_dam_begin");
            strings = base_rand_dam.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("base_rand_dam_end");
            exportStrings.Add(System.Environment.NewLine);



            exportStrings.Add("level_bonus_begin");
            strings = level_bonus.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("level_bonus_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("str_bonus_begin");
            strings = str_bonus.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("str_bonus_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("int_bonus_begin");
            strings = int_bonus.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("int_bonus_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("con_bonus_begin");
            strings = con_bonus.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("con_bonus_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("men_bonus_begin");
            strings = men_bonus.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("men_bonus_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("dex_bonus_begin");
            strings = dex_bonus.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("dex_bonus_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("wit_bonus_begin");
            strings = wit_bonus.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("wit_bonus_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("org_hp_regen_begin");
            strings = org_hp_regen.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("org_hp_regen_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("org_mp_regen_begin");
            strings = org_mp_regen.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("org_mp_regen_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("org_cp_regen_begin");
            strings = org_cp_regen.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("org_cp_regen_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("moving_speed_begin");
            strings = moving_speed.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("moving_speed_end");
            exportStrings.Add(System.Environment.NewLine);



            exportStrings.Add("org_hp_regen_weight_begin");
            strings = org_hp_regen_weight.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("org_hp_regen_weight_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("org_mp_regen_weight_begin");
            strings = org_mp_regen_weight.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("org_mp_regen_weight_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("org_cp_regen_weight_begin");
            strings = org_cp_regen_weight.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("org_cp_regen_weight_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("noise_move_mode_bonus_begin");
            strings = noise_move_mode_bonus.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("noise_move_mode_bonus_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("org_jump_begin");
            strings = org_jump.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("org_jump_end");
            exportStrings.Add(System.Environment.NewLine);


            exportStrings.Add("hit_cond_bonus_begin");
            strings = hit_cond_bonus.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("hit_cond_bonus_end");
            exportStrings.Add(System.Environment.NewLine);


            for (int i = 0; i < hp_tables.Count; i++)
            {
                exportStrings.Add(hp_tables[i].parameterID);
                strings = hp_tables[i].GetExportStrings();
                for (int j = 0; j < strings.Length; j++)
                {
                    exportStrings.Add(strings[j]);
                }
                exportStrings.Add(hp_tables[i].parameterID.Replace("_begin", "_end"));
                exportStrings.Add(System.Environment.NewLine);

            }


            for (int i = 0; i < mp_tables.Count; i++)
            {
                exportStrings.Add(mp_tables[i].parameterID);
                strings = mp_tables[i].GetExportStrings();
                for (int j = 0; j < strings.Length; j++)
                {
                    exportStrings.Add(strings[j]);
                }
                exportStrings.Add(mp_tables[i].parameterID.Replace("_begin", "_end"));
                exportStrings.Add(System.Environment.NewLine);

            }


            for (int i = 0; i < cp_tables.Count; i++)
            {
                exportStrings.Add(cp_tables[i].parameterID);
                strings = cp_tables[i].GetExportStrings();
                for (int j = 0; j < strings.Length; j++)
                {
                    exportStrings.Add(strings[j]);
                }
                exportStrings.Add(cp_tables[i].parameterID.Replace("_begin", "_end"));
                exportStrings.Add(System.Environment.NewLine);

            }


            exportStrings.Add("pc_breath_bonus_table_begin");
            strings = pc_breath_bonus_table.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("pc_breath_bonus_table_end");
            exportStrings.Add(System.Environment.NewLine);



            exportStrings.Add("pc_safe_fall_height_table_begin");
            strings = pc_safe_fall_height_table.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("pc_safe_fall_height_table_end");
            exportStrings.Add(System.Environment.NewLine);



            exportStrings.Add("pc_collision_box_table_begin");
            strings = pc_collision_box_table.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("pc_collision_box_table_end");
            exportStrings.Add(System.Environment.NewLine);



            exportStrings.Add("pc_karma_increase_table_begin");
            strings = pc_karma_increase_table.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("pc_karma_increase_table_end");
            exportStrings.Add(System.Environment.NewLine);



            exportStrings.Add("pc_karma_increase_constant_begin");
            strings = pc_karma_increase_constant.GetExportStrings();
            for (int i = 0; i < strings.Length; i++)
            {
                exportStrings.Add(strings[i]);
            }
            exportStrings.Add("pc_karma_increase_constant_end");
            exportStrings.Add(System.Environment.NewLine);




            return exportStrings.ToArray();
        }

    }

    public class Base_Parameter_Single_Value
    {
        public string base_Parameter_ID;
        public string FFighter_Value;
        public string MFighter_Value;
        public string FMagic_Value;
        public string MMagic_Value;
        public string FElfFighter_Value;
        public string MElfFighter_Value;
        public string FElfMagic_Value;
        public string MElfMagic_Value;
        public string FDarkelfFighter_Value;
        public string MDarkelfFighter_Value;
        public string FDarkelfMagic_Value;
        public string MDarkelfMagic_Value;
        public string FOrcFighter_Value;
        public string MOrcFighter_Value;
        public string FShaman_Value;
        public string MShaman_Value;
        public string FDwarfFighter_Value;
        public string MDwarfFighter_Value;
        public string FKamaelSoldier_Value;
        public string MKamaelSoldier_Value;

        public string otherValue;

        public Base_Parameter_Single_Value(string base_Parameter_ID)
        {
            this.base_Parameter_ID = base_Parameter_ID;

        }
        public string FromIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return FFighter_Value;
                case 1:
                    return MFighter_Value;
                case 2:
                    return FMagic_Value;
                case 3:
                    return MMagic_Value;
                case 4:
                    return FElfFighter_Value;
                case 5:
                    return MElfFighter_Value;
                case 6:
                    return FElfMagic_Value;
                case 7:
                    return MElfMagic_Value;
                case 8:
                    return FDarkelfFighter_Value;
                case 9:
                    return MDarkelfFighter_Value;
                case 10:
                    return FDarkelfMagic_Value;
                case 11:
                    return MDarkelfMagic_Value;
                case 12:
                    return FOrcFighter_Value;
                case 13:
                    return MOrcFighter_Value;
                case 14:
                    return FShaman_Value;
                case 15:
                    return MShaman_Value;
                case 16:
                    return FDwarfFighter_Value;
                case 17:
                    return MDwarfFighter_Value;
                case 18:
                    return FKamaelSoldier_Value;
                case 19:
                    return MKamaelSoldier_Value;
                default:
                    return FFighter_Value;
            }
        }
        public void SetValueFromIndex(int index, string value)
        {
            switch (index)
            {
                case 0:
                    {
                        FFighter_Value = value;
                    }
                    break;
                case 1:
                    { MFighter_Value = value; }
                    break;
                case 2:
                    { FMagic_Value = value; }
                    break;
                case 3:
                    { MMagic_Value = value; }
                    break;
                case 4:
                    { FElfFighter_Value = value; }
                    break;
                case 5:
                    { MElfFighter_Value = value; }
                    break;
                case 6:
                    { FElfMagic_Value = value; }
                    break;
                case 7:
                    { MElfMagic_Value = value; }
                    break;
                case 8:
                    { FDarkelfFighter_Value = value; }
                    break;
                case 9:
                    { MDarkelfFighter_Value = value; }
                    break;
                case 10:
                    { FDarkelfMagic_Value = value; }
                    break;
                case 11:
                    { MDarkelfMagic_Value = value; }
                    break;
                case 12:
                    { FOrcFighter_Value = value; }
                    break;
                case 13:
                    { MOrcFighter_Value = value; }
                    break;
                case 14:
                    { FShaman_Value = value; }
                    break;
                case 15:
                    { MShaman_Value = value; }
                    break;
                case 16:
                    { FDwarfFighter_Value = value; }
                    break;
                case 17:
                    { MDwarfFighter_Value = value; }
                    break;
                case 18:
                    { FKamaelSoldier_Value = value; }
                    break;
                case 19:
                    { MKamaelSoldier_Value = value; }
                    break;
                default:
                    {
                        otherValue = value;
                    }
                    break;
            }
        }
        public void SetValue(string raceClass, string value)
        {
            switch (raceClass)
            {
                case "FFighter":
                    {
                        FFighter_Value = value;
                    }
                    break;
                case "MFighter":
                    { MFighter_Value = value; }
                    break;
                case "FMagic":
                    { FMagic_Value = value; }
                    break;
                case "MMagic":
                    { MMagic_Value = value; }
                    break;
                case "FElfFighter":
                    { FElfFighter_Value = value; }
                    break;
                case "MElfFighter":
                    { MElfFighter_Value = value; }
                    break;
                case "FElfMagic":
                    { FElfMagic_Value = value; }
                    break;
                case "MElfMagic":
                    { MElfMagic_Value = value; }
                    break;
                case "FDarkelfFighter":
                    { FDarkelfFighter_Value = value; }
                    break;
                case "MDarkelfFighter":
                    { MDarkelfFighter_Value = value; }
                    break;
                case "FDarkelfMagic":
                    { FDarkelfMagic_Value = value; }
                    break;
                case "MDarkelfMagic":
                    { MDarkelfMagic_Value = value; }
                    break;
                case "FOrcFighter":
                    { FOrcFighter_Value = value; }
                    break;
                case "MOrcFighter":
                    { MOrcFighter_Value = value; }
                    break;
                case "FShaman":
                    { FShaman_Value = value; }
                    break;
                case "MShaman":
                    { MShaman_Value = value; }
                    break;
                case "FDwarfFighter":
                    { FDwarfFighter_Value = value; }
                    break;
                case "MDwarfFighter":
                    { MDwarfFighter_Value = value; }
                    break;
                case "FKamaelSoldier":
                    { FKamaelSoldier_Value = value; }
                    break;
                case "MKamaelSoldier":
                    { MKamaelSoldier_Value = value; }
                    break;
                default:
                    {
                        otherValue = value;
                    }
                    break;
            }
        }

        public string[] GetExportStrings()
        {
            List<string> exportStrings = new List<string>();

            exportStrings.Add("\tFFighter=" + FFighter_Value);
            exportStrings.Add("\tMFighter=" + MFighter_Value);
            exportStrings.Add("\tFMagic=" + FMagic_Value);
            exportStrings.Add("\tMMagic=" + MMagic_Value);
            exportStrings.Add("\tFElfFighter=" + FElfFighter_Value);
            exportStrings.Add("\tMElfFighter=" + MElfFighter_Value);
            exportStrings.Add("\tFElfMagic=" + FElfMagic_Value);
            exportStrings.Add("\tMElfMagic=" + MElfMagic_Value);
            exportStrings.Add("\tFDarkelfFighter=" + FDarkelfFighter_Value);
            exportStrings.Add("\tMDarkelfFighter=" + MDarkelfFighter_Value);
            exportStrings.Add("\tFDarkelfMagic=" + FDarkelfMagic_Value);
            exportStrings.Add("\tMDarkelfMagic=" + MDarkelfMagic_Value);
            exportStrings.Add("\tFOrcFighter=" + FOrcFighter_Value);
            exportStrings.Add("\tMOrcFighter=" + MOrcFighter_Value);
            exportStrings.Add("\tFShaman=" + FShaman_Value);
            exportStrings.Add("\tMShaman=" + MShaman_Value);
            exportStrings.Add("\tFDwarfFighter=" + FDwarfFighter_Value);
            exportStrings.Add("\tMDwarfFighter=" + MDwarfFighter_Value);
            exportStrings.Add("\tFKamaelSoldier=" + FKamaelSoldier_Value);
            exportStrings.Add("\tMKamaelSoldier=" + MKamaelSoldier_Value);

            return exportStrings.ToArray();

        }

    }
    public class Base_Parameter_Single_Line_Multivalue
    {
        public string base_Parameter_ID;
        public Single_Line_Multivalue FFighter_Value;
        public Single_Line_Multivalue MFighter_Value;
        public Single_Line_Multivalue FMagic_Value;
        public Single_Line_Multivalue MMagic_Value;
        public Single_Line_Multivalue FElfFighter_Value;
        public Single_Line_Multivalue MElfFighter_Value;
        public Single_Line_Multivalue FElfMagic_Value;
        public Single_Line_Multivalue MElfMagic_Value;
        public Single_Line_Multivalue FDarkelfFighter_Value;
        public Single_Line_Multivalue MDarkelfFighter_Value;
        public Single_Line_Multivalue FDarkelfMagic_Value;
        public Single_Line_Multivalue MDarkelfMagic_Value;
        public Single_Line_Multivalue FOrcFighter_Value;
        public Single_Line_Multivalue MOrcFighter_Value;
        public Single_Line_Multivalue FShaman_Value;
        public Single_Line_Multivalue MShaman_Value;
        public Single_Line_Multivalue FDwarfFighter_Value;
        public Single_Line_Multivalue MDwarfFighter_Value;
        public Single_Line_Multivalue FKamaelSoldier_Value;
        public Single_Line_Multivalue MKamaelSoldier_Value;

        public Single_Line_Multivalue other_Value;


        public Base_Parameter_Single_Line_Multivalue(string baseParameterID)
        {
            this.base_Parameter_ID = baseParameterID;
        }

        public Single_Line_Multivalue FromIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return FFighter_Value;
                case 1:
                    return MFighter_Value;
                case 2:
                    return FMagic_Value;
                case 3:
                    return MMagic_Value;
                case 4:
                    return FElfFighter_Value;
                case 5:
                    return MElfFighter_Value;
                case 6:
                    return FElfMagic_Value;
                case 7:
                    return MElfMagic_Value;
                case 8:
                    return FDarkelfFighter_Value;
                case 9:
                    return MDarkelfFighter_Value;
                case 10:
                    return FDarkelfMagic_Value;
                case 11:
                    return MDarkelfMagic_Value;
                case 12:
                    return FOrcFighter_Value;
                case 13:
                    return MOrcFighter_Value;
                case 14:
                    return FShaman_Value;
                case 15:
                    return MShaman_Value;
                case 16:
                    return FDwarfFighter_Value;
                case 17:
                    return MDwarfFighter_Value;
                case 18:
                    return FKamaelSoldier_Value;
                case 19:
                    return MKamaelSoldier_Value;
                default:
                    return FFighter_Value;
            }
        }

        public void SetValue(string raceClass, string value)
        {
            switch (raceClass)
            {
                case "FFighter":
                    {
                        FFighter_Value = new Single_Line_Multivalue("FFighter", value);
                    }
                    break;
                case "MFighter":
                    { MFighter_Value = new Single_Line_Multivalue("MFighter", value); }
                    break;
                case "FMagic":
                    { FMagic_Value = new Single_Line_Multivalue("FMagic", value); }
                    break;
                case "MMagic":
                    { MMagic_Value = new Single_Line_Multivalue("MMagic", value); }
                    break;
                case "FElfFighter":
                    { FElfFighter_Value = new Single_Line_Multivalue("FElfFighter", value); }
                    break;
                case "MElfFighter":
                    { MElfFighter_Value = new Single_Line_Multivalue("MElfFighter", value); }
                    break;
                case "FElfMagic":
                    { FElfMagic_Value = new Single_Line_Multivalue("FElfMagic", value); }
                    break;
                case "MElfMagic":
                    { MElfMagic_Value = new Single_Line_Multivalue("MElfMagic", value); }
                    break;
                case "FDarkelfFighter":
                    { FDarkelfFighter_Value = new Single_Line_Multivalue("FDarkelfFighter", value); }
                    break;
                case "MDarkelfFighter":
                    { MDarkelfFighter_Value = new Single_Line_Multivalue("MDarkelfFighter", value); }
                    break;
                case "FDarkelfMagic":
                    { FDarkelfMagic_Value = new Single_Line_Multivalue("FDarkelfMagic", value); }
                    break;
                case "MDarkelfMagic":
                    { MDarkelfMagic_Value = new Single_Line_Multivalue("MDarkelfMagic", value); }
                    break;
                case "FOrcFighter":
                    { FOrcFighter_Value = new Single_Line_Multivalue("FOrcFighter", value); }
                    break;
                case "MOrcFighter":
                    { MOrcFighter_Value = new Single_Line_Multivalue("MOrcFighter", value); }
                    break;
                case "FShaman":
                    { FShaman_Value = new Single_Line_Multivalue("FShaman", value); }
                    break;
                case "MShaman":
                    { MShaman_Value = new Single_Line_Multivalue("MShaman", value); }
                    break;
                case "FDwarfFighter":
                    { FDwarfFighter_Value = new Single_Line_Multivalue("FDwarfFighter", value); }
                    break;
                case "MDwarfFighter":
                    { MDwarfFighter_Value = new Single_Line_Multivalue("MDwarfFighter", value); }
                    break;
                case "FKamaelSoldier":
                    { FKamaelSoldier_Value = new Single_Line_Multivalue("FKamaelSoldier", value); }
                    break;
                case "MKamaelSoldier":
                    { MKamaelSoldier_Value = new Single_Line_Multivalue("MKamaelSoldier", value); }
                    break;
                default:
                    {
                        other_Value = new Single_Line_Multivalue(raceClass, value);
                    }
                    break;
            }
        }

        public string[] GetExportStrings()
        {
            List<string> exportStrings = new List<string>();

            exportStrings.Add("\tFFighter=" + GetMultipleValuesString(FFighter_Value.values));
            exportStrings.Add("\tMFighter=" + GetMultipleValuesString(MFighter_Value.values));
            exportStrings.Add("\tFMagic=" + GetMultipleValuesString(FMagic_Value.values));
            exportStrings.Add("\tMMagic=" + GetMultipleValuesString(MMagic_Value.values));
            exportStrings.Add("\tFElfFighter=" + GetMultipleValuesString(FElfFighter_Value.values));
            exportStrings.Add("\tMElfFighter=" + GetMultipleValuesString(MElfFighter_Value.values));
            exportStrings.Add("\tFElfMagic=" + GetMultipleValuesString(FElfMagic_Value.values));
            exportStrings.Add("\tMElfMagic=" + GetMultipleValuesString(MElfMagic_Value.values));
            exportStrings.Add("\tFDarkelfFighter=" + GetMultipleValuesString(FDarkelfFighter_Value.values));
            exportStrings.Add("\tMDarkelfFighter=" + GetMultipleValuesString(MDarkelfFighter_Value.values));
            exportStrings.Add("\tFDarkelfMagic=" + GetMultipleValuesString(FDarkelfMagic_Value.values));
            exportStrings.Add("\tMDarkelfMagic=" + GetMultipleValuesString(MDarkelfMagic_Value.values));
            exportStrings.Add("\tFOrcFighter=" + GetMultipleValuesString(FOrcFighter_Value.values));
            exportStrings.Add("\tMOrcFighter=" + GetMultipleValuesString(MOrcFighter_Value.values));
            exportStrings.Add("\tFShaman=" + GetMultipleValuesString(FShaman_Value.values));
            exportStrings.Add("\tMShaman=" + GetMultipleValuesString(MShaman_Value.values));
            exportStrings.Add("\tFDwarfFighter=" + GetMultipleValuesString(FDwarfFighter_Value.values));
            exportStrings.Add("\tMDwarfFighter=" + GetMultipleValuesString(MDwarfFighter_Value.values));
            exportStrings.Add("\tFKamaelSoldier=" + GetMultipleValuesString(FKamaelSoldier_Value.values));
            exportStrings.Add("\tMKamaelSoldier=" + GetMultipleValuesString(MKamaelSoldier_Value.values));

            return exportStrings.ToArray();
        }

        string GetMultipleValuesString(List<string> sourceValues)
        {
            string returnString = "{";
            for (int i = 0; i < sourceValues.Count; i++)
            {
                returnString += sourceValues[i];

                if (i < sourceValues.Count - 1)
                    returnString += ";";
            }
            returnString += "}";

            return returnString;
        }

    }

    public class Single_Line_Multivalue
    {
        public string classID = "";
        public List<string> values;

        public Single_Line_Multivalue(string classID, string data)
        {
            this.classID = classID;

            values = new List<string>();

            string trimmedData = data.Replace("{", "");
            trimmedData = trimmedData.Replace("}", "");
            string[] splitData = trimmedData.Split(';');


            for (int i = 0; i < splitData.Length; i++)
            {
                values.Add(splitData[i]);
            }
        }


    }

    public class Base_Parameter_Multiple_Line_Multivalue
    {
        public string parameterID;
        public string classID;
        public List<string> levelIDs;
        public List<string> values;

        public Base_Parameter_Multiple_Line_Multivalue(string parameterID)
        {
            this.parameterID = parameterID;
            levelIDs = new List<string>();
            values = new List<string>();
        }

        public void AddValue(string dataline)
        {
            if (string.IsNullOrEmpty(dataline))
                return;

            string[] trimmedDataline = dataline.Split('=');
            levelIDs.Add(trimmedDataline[0].Replace("\t", "").Replace(" ", ""));
            values.Add(trimmedDataline[1].Replace("\t", "").Replace(" ", ""));
        }

        public void SetValue(string levelID, string value)
        {

            values[int.Parse(levelID)] = value;
        }

        public string[] GetExportStrings()
        {
            List<string> exportStrings = new List<string>();

            for (int i = 0; i < levelIDs.Count; i++)
            {
                exportStrings.Add("\t" + levelIDs[i] + " = " + values[i]);
            }


            return exportStrings.ToArray();
        }
    }

}
