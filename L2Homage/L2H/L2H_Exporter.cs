using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using L2Homage.Pages;

namespace L2Homage
{
    public class L2H_Exporter
    {
        private static L2H_Exporter instance;

        MainWindow mainWindow;
        OverviewPage overviewPage;
        DroplistsPage droplistsPage;
        ItemsPage itemsPage;
        NPCsPage NPCsPage;
        SpawnsPage spawnsPage;
        RecipesPage recipesPage;
        SkillsPage skillsPage;
        MultisellPage multisellPage;
        SystemTextPage systemTextPage;
        ExpPage expPage;
        ClassesPage classesPage;

        int numberOfFilesToExport = 23;
        int numberOfThreadsCompleted;

        bool exporting = false;

        public static L2H_Exporter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new L2H_Exporter();
                }
                return instance;
            }
        }

        private void GrabPages()
        {
            mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;
            overviewPage = mainWindow.GetPageOfType(typeof(OverviewPage)) as OverviewPage;
            droplistsPage = mainWindow.GetPageOfType(typeof(DroplistsPage)) as DroplistsPage;
            itemsPage = mainWindow.GetPageOfType(typeof(ItemsPage)) as ItemsPage;
            NPCsPage = mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage;
            spawnsPage = mainWindow.GetPageOfType(typeof(SpawnsPage)) as SpawnsPage;
            recipesPage = mainWindow.GetPageOfType(typeof(RecipesPage)) as RecipesPage;
            skillsPage = mainWindow.GetPageOfType(typeof(SkillsPage)) as SkillsPage;
            multisellPage = mainWindow.GetPageOfType(typeof(MultisellPage)) as MultisellPage;
            systemTextPage = mainWindow.GetPageOfType(typeof(SystemTextPage)) as SystemTextPage;
            expPage = mainWindow.GetPageOfType(typeof(ExpPage)) as ExpPage;
            classesPage = mainWindow.GetPageOfType(typeof(ClassesPage)) as ClassesPage;
        }

        public void Save_All(bool export)
        {
            if (exporting || !(System.Windows.Application.Current.MainWindow as MainWindow).finishedLoading)
                return;

            exporting = true;
            GrabPages();

            mainWindow.UpdateLog("Initializing export thread..", L2H_Constants.Color_Log_Thread_Begin);
            ThreadWorker exportingThread = new ThreadWorker();
            exportingThread.Job += (sender, e) => SaveItems(export);
            if (export)
                exportingThread.ThreadDone += (sender, e) => HandleThreadDone(sender, e, "");
            else
                exportingThread.ThreadDone += (sender, e) => HandleThreadDone(sender, e, "Save Complete");
            Thread thread_ExportAll = new Thread(exportingThread.ThreadProc);

            thread_ExportAll.Start();


        }

        private void HandleThreadDone(object sender, EventArgs e, string logMessage)
        {
            if (!string.IsNullOrEmpty(logMessage))
                mainWindow.UpdateLog(logMessage, L2H_Constants.Color_Add);

            exporting = false;

        }

        private void SaveItems(bool export)
        {
            mainWindow.UpdateLog("Gathering all data to save..", L2H_Constants.Color_Log_Thread_Begin);

            //This can be improved by flagging each section, so a section is only saved if a user has changed data in it.
            //Saving data doesn't take a long time though, the only real time-saver would be flagging sections for export

            //First instantiate lists for holding all generated export data. By readying all data before saving, there's a smaller risk of saving something that corrupts your data.
            //L2Homage crashes instead of saving half your work.
            //Items
            List<L2H_Item> items = itemsPage.L2H_Items.OrderBy(i => i.ID).ToList();
            //Client
            List<string> client_Itemname_Lines = new List<string>();
            List<string> client_Weapons_Lines = new List<string>();
            List<string> client_Armors_Lines = new List<string>();
            List<string> client_Etc_Lines = new List<string>();
            //L2H
            List<string> L2H_itemTemplatePointers_Lines = new List<string>();
            List<string> L2H_setTemplate_Pointers_Lines = new List<string>();
            //Server
            List<string> server_Item_PCH_Lines = new List<string>();
            List<string> server_Sets_Lines = new List<string>();
            List<string> server_Itemdata_Lines = new List<string>();

            //Recipes
            //Client
            List<string> client_Recipe_Lines = new List<string>();
            //Server
            List<string> server_Recipe_Lines = new List<string>();

            //Skills
            //Client
            List<string> client_Skills_Lines = new List<string>();
            List<string> client_Skillsounds_Lines = new List<string>();
            List<string> client_Skillnames_Lines = new List<string>();
            //L2H
            List<string> L2H_skillTemplate_Pointers_Lines = new List<string>();
            //Server
            List<string> server_Skill_PCH_Lines = new List<string>();
            List<string> server_Skill_PCH2_Lines = new List<string>();
            List<string> server_Skilldata_Lines = new List<string>();
            List<string> server_Skillacquire_Lines = new List<string>();
            List<string> server_Skillenchantdata_Lines = new List<string>();

            //NPCs
            //Client
            List<string> client_Mobskillanimgrp_Lines = new List<string>();
            List<string> client_NPC_Names_lines = new List<string>();
            List<string> client_NPCs_Lines = new List<string>();
            //L2H
            List<string> L2H_npcTemplate_Pointers_Lines = new List<string>();
            List<string> L2H_NPC_Droplists_Pointers_Lines = new List<string>();
            //Server
            List<string> server_NPCdata_Lines = new List<string>();
            List<string> server_NPC_PCH_Lines = new List<string>();

            //Droplists
            //L2H
            List<string> L2H_Single_Droplists_Lines = new List<string>();
            List<string> L2H_Multi_Droplists_Lines = new List<string>();

            //Multisell
            List<string> server_Multisell_Lines = new List<string>();

            //System Text
            List<string> client_EULA_Lines = new List<string>();
            List<string> client_Chatfilter_Lines = new List<string>();
            List<string> client_Gametips_Lines = new List<string>();
            List<string> client_NPC_Strings_Lines = new List<string>();
            List<string> client_System_Messages_Lines = new List<string>();
            List<string> client_System_Strings_Lines = new List<string>();
            List<string> client_Charcreategrp_Lines = new List<string>();
            List<string> client_Classinfo_Lines = new List<string>();

            //Zones
            List<string> client_Huntingzones_Lines = new List<string>();
            List<string> client_Zonenames_Lines = new List<string>();
            List<string> client_Raids_Lines = new List<string>();


            //Startlines
            client_Itemname_Lines.Add(itemsPage.itemNamesStartLine);
            client_Weapons_Lines.Add(itemsPage.weaponsStartLine);
            client_Armors_Lines.Add(itemsPage.armorsStartLine);
            client_Etc_Lines.Add(itemsPage.etcsStartLine);
            client_Mobskillanimgrp_Lines.Add(NPCsPage.client_Mobskillanim_Startline);
            client_NPCs_Lines.Add(NPCsPage.client_NPCs_Startline);
            client_NPC_Names_lines.Add(NPCsPage.client_NPC_Names_Startline);
            client_Skills_Lines.Add(skillsPage.client_Skills_Startline);
            client_Skillnames_Lines.Add(skillsPage.client_Skillnames_Startline);
            client_Skillsounds_Lines.Add(skillsPage.client_Skillsounds_Startline);
            client_Recipe_Lines.Add(recipesPage.client_Recipes_Startline);

            client_Chatfilter_Lines.Add(systemTextPage.obsceneStartLine);
            client_Gametips_Lines.Add(systemTextPage.gametipStartLine);
            client_NPC_Strings_Lines.Add(systemTextPage.npcStringStartLine);
            client_System_Messages_Lines.Add(systemTextPage.systemMessageStartLine);
            client_System_Strings_Lines.Add(systemTextPage.systemStringStartLine);
            client_EULA_Lines.Add(systemTextPage.eulaStartLine);
            client_EULA_Lines.Add(systemTextPage.client_Eula.GetExportString());

            client_Huntingzones_Lines.Add(spawnsPage.huntingzoneStartLine);
            client_Zonenames_Lines.Add(spawnsPage.zonenameStartLine);
            client_Raids_Lines.Add(spawnsPage.raidsStartLine);


            //Grab all export strings from loaded data. Then save it. 
            //If export is true, export server data and encrypt client data and move it all to export folder.
            //Templates

            for (int i = 0; i < itemsPage.item_Template_Pointers.Count; i++)
            {
                L2H_itemTemplatePointers_Lines.Add(itemsPage.item_Template_Pointers[i].GetExportString());
            }
            for (int i = 0; i < itemsPage.set_Template_Pointers.Count; i++)
            {
                L2H_setTemplate_Pointers_Lines.Add(itemsPage.set_Template_Pointers[i].GetExportString());
            }
            for (int i = 0; i < skillsPage.skill_Template_Pointers.Count; i++)
            {
                L2H_skillTemplate_Pointers_Lines.Add(skillsPage.skill_Template_Pointers[i].GetExportString());
            }
            for (int i = 0; i < NPCsPage.npcTemplate_Pointers.Count; i++)
            {
                L2H_npcTemplate_Pointers_Lines.Add(NPCsPage.npcTemplate_Pointers[i].GetExportString());
            }
            for (int i = 0; i < droplistsPage.npc_Droplist_Pointers.Count; i++)
            {
                L2H_NPC_Droplists_Pointers_Lines.Add(droplistsPage.npc_Droplist_Pointers[i].GetExportString());
            }

            //Items
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].client_Itemname != null)
                    client_Itemname_Lines.Add(items[i].client_Itemname.GetExportString());
                if (items[i].client_Weapon != null)
                    client_Weapons_Lines.Add(items[i].client_Weapon.GetExportString());
                if (items[i].client_Armor != null)
                    client_Armors_Lines.Add(items[i].client_Armor.GetExportString());
                if (items[i].client_Etc != null)
                    client_Etc_Lines.Add(items[i].client_Etc.GetExportString());
                if (items[i].server_Itemdata != null)
                {
                    server_Itemdata_Lines.Add(items[i].server_Itemdata.GetExportString());
                    server_Item_PCH_Lines.Add(items[i].server_Itemdata.GetPCHString());
                }

            }


            //Sets
            List<L2H_Set> sets = itemsPage.L2H_Sets.OrderBy(i => i.ID).ToList();
            for (int i = 0; i < sets.Count; i++)
            {
                server_Sets_Lines.Add(sets[i].server_Itemdata.GetExportString());
            }

            //Recipes
            List<L2H_Recipe> recipes = recipesPage.L2H_Recipes.OrderBy(i => i.ID).ToList();
            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipes[i].client_Recipe != null)
                    client_Recipe_Lines.Add(recipes[i].client_Recipe.GetExportString());
                if (recipes[i].server_Recipe != null)
                    server_Recipe_Lines.Add(recipes[i].server_Recipe.GetExportString());
            }

            //NPCs
            List<L2H_NPC> npcs = NPCsPage.L2H_Npcs.OrderBy(i => i.ID).ToList();
            for (int i = 0; i < npcs.Count; i++)
            {
                if (npcs[i].server_Npcdata != null)
                {

                    server_NPCdata_Lines.Add(npcs[i].server_Npcdata.GetExportString());
                    server_NPC_PCH_Lines.Add(npcs[i].server_Npcdata.GetPCHExportString());
                }
                if (npcs[i].client_Npc != null)
                    client_NPCs_Lines.Add(npcs[i].client_Npc.GetExportString());
                if (npcs[i].client_Npcname != null)
                    client_NPC_Names_lines.Add(npcs[i].client_Npcname.GetExportString());

            }

            //Droplists
            List<L2H_Droplist> droplists = droplistsPage.L2H_Droplists;
            for (int i = 0; i < droplists.Count; i++)
            {
                if (droplists[i].server_Droplist != null)
                    L2H_Single_Droplists_Lines.Add(droplists[i].server_Droplist.GetCustomExportString());
                if (droplists[i].server_Multi_Droplist != null)
                    L2H_Multi_Droplists_Lines.Add(droplists[i].server_Multi_Droplist.GetCustomExportString());
            }

            //Spawns
            List<string> server_Npcpos_originalStrings = spawnsPage.server_Npcpos_Original.GetFullExport();
            List<string> server_Npcpos_customStrings = spawnsPage.server_Npcpos_Custom.GetFullExport();
            List<string> server_Npcpos_fullStrings = (server_Npcpos_originalStrings.Concat(server_Npcpos_customStrings).ToList());

            //Skills
            List<L2H_Skill> skills = skillsPage.Get_L2H_Skills;

            skills = skills.OrderBy(i => int.Parse(i.ID)).ToList();

            //for (int i = 0; i < skillsPage.skill_Template_Pointers.Count; i++)
            //{
            //    L2H_skillTemplate_Pointers_Lines.Add(skillsPage.skill_Template_Pointers[i].GetExportString());
            //}

            File.WriteAllText(L2H_Constants.client_Skills_Path, String.Empty);
            File.WriteAllText(L2H_Constants.client_Skillnames_Path, String.Empty);
            File.WriteAllText(L2H_Constants.client_Skillsounds_Path, String.Empty);
            File.WriteAllText(L2H_Constants.server_Skilldata_Path, String.Empty);
            File.WriteAllText(L2H_Constants.server_Skill_PCH_Path, String.Empty);
            File.WriteAllText(L2H_Constants.server_Skill_PCH2_Path, String.Empty);

            for (int i = 0; i < skills.Count; i++)
            {
                
                if (skills[i].client_Skill != null)
                    client_Skills_Lines.Add(skills[i].client_Skill.GetExportString());
                if (skills[i].client_Skillname != null)
                    client_Skillnames_Lines.Add(skills[i].client_Skillname.GetExportString());
                if (skills[i].client_Skillsound != null)
                {
                    string exportedSoundString = skills[i].client_Skillsound.GetExportString();
                    if (!client_Skillsounds_Lines.Exists(x => x == exportedSoundString))
                        client_Skillsounds_Lines.Add(exportedSoundString);
                }
                if (skills[i].server_Skilldata != null)
                    server_Skilldata_Lines.Add(skills[i].server_Skilldata.GetExportString());

                server_Skill_PCH_Lines.Add(skills[i].GeneratePCHExportString());
                server_Skill_PCH2_Lines.Add(skills[i].GeneratePCH2ExportString());

                if (i % 100 == 0 || i == skills.Count - 1)
                {
                    File.AppendAllLines(L2H_Constants.client_Skills_Path, client_Skills_Lines.ToArray(), Encoding.GetEncoding(65001));
                    File.AppendAllLines(L2H_Constants.client_Skillnames_Path, client_Skillnames_Lines.ToArray(), Encoding.GetEncoding(65001));
                    File.AppendAllLines(L2H_Constants.client_Skillsounds_Path, client_Skillsounds_Lines.ToArray(), Encoding.GetEncoding(65001));
                    File.AppendAllLines(L2H_Constants.server_Skilldata_Path, server_Skilldata_Lines, Encoding.GetEncoding(1200));
                    File.AppendAllLines(L2H_Constants.server_Skill_PCH_Path, server_Skill_PCH_Lines, Encoding.GetEncoding(1200));
                    File.AppendAllLines(L2H_Constants.server_Skill_PCH2_Path, server_Skill_PCH2_Lines, Encoding.GetEncoding(1200));

                    client_Skills_Lines.Clear();
                    client_Skillnames_Lines.Clear();
                    client_Skillsounds_Lines.Clear();
                    server_Skilldata_Lines.Clear();
                    server_Skill_PCH_Lines.Clear();
                    server_Skill_PCH2_Lines.Clear();
                }

            }
            for (int i = 0; i < skillsPage.server_Skillenchantdata.Count; i++)
            {
                server_Skillenchantdata_Lines.Add(skillsPage.server_Skillenchantdata[i].GetExportString());
            }

            //Skillacquires

            string currentActiveSkillAcquireClass = "";
            string currentActiveSkillAcquireInclude = "";


            for (int i = 0; i < classesPage.server_Skillacquires.Count; i++)
            {
                if (currentActiveSkillAcquireClass != classesPage.server_Skillacquires[i].class_begin)
                {
                    if (!string.IsNullOrEmpty(currentActiveSkillAcquireClass))
                    {
                        string activeSkillAcquireClassEndString = "";
                        activeSkillAcquireClassEndString = currentActiveSkillAcquireClass.Replace("_begin", "_end");
                        server_Skillacquire_Lines.Add(activeSkillAcquireClassEndString);
                    }
                    currentActiveSkillAcquireClass = classesPage.server_Skillacquires[i].class_begin;
                    server_Skillacquire_Lines.Add(currentActiveSkillAcquireClass);

                    currentActiveSkillAcquireInclude = "";

                    if (!string.IsNullOrEmpty(classesPage.server_Skillacquires[i].includeClass))
                    {
                        currentActiveSkillAcquireInclude = classesPage.server_Skillacquires[i].includeClass;
                        server_Skillacquire_Lines.Add(currentActiveSkillAcquireInclude);
                    }
                }

                server_Skillacquire_Lines.Add(classesPage.server_Skillacquires[i].GetExportString());

            }

            string activeSkillAcquireClassEndStringLast = "";
            activeSkillAcquireClassEndStringLast = currentActiveSkillAcquireClass.Replace("_begin", "_end");
            server_Skillacquire_Lines.Add(activeSkillAcquireClassEndStringLast);

            for (int i = 0; i < classesPage.client_Charcreates.Count; i++)
            {
                client_Charcreategrp_Lines.Add(classesPage.client_Charcreates[i].GetExportString());
            }
            for (int i = 0; i < classesPage.client_Classinfos.Count; i++)
            {
                client_Classinfo_Lines.Add(classesPage.client_Classinfos[i].GetExportString());
            }


            //System Texts
            for (int i = 0; i < systemTextPage.client_Obscenes.Count; i++)
            {
                client_Chatfilter_Lines.Add(systemTextPage.client_Obscenes[i].GetExportString());
            }
            for (int i = 0; i < systemTextPage.client_Gametips.Count; i++)
            {
                client_Gametips_Lines.Add(systemTextPage.client_Gametips[i].GetExportString());
            }
            for (int i = 0; i < systemTextPage.client_Npc_Strings.Count; i++)
            {
                client_NPC_Strings_Lines.Add(systemTextPage.client_Npc_Strings[i].GetExportString());
            }
            for (int i = 0; i < systemTextPage.client_System_Messages.Count; i++)
            {
                client_System_Messages_Lines.Add(systemTextPage.client_System_Messages[i].GetExportString());
            }
            for (int i = 0; i < systemTextPage.client_System_Strings.Count; i++)
            {
                client_System_Strings_Lines.Add(systemTextPage.client_System_Strings[i].GetExportString());
            }

            //Zones
            for (int i = 0; i < spawnsPage.client_Huntingzones.Count; i++)
            {
                client_Huntingzones_Lines.Add(spawnsPage.client_Huntingzones[i].GetExportString());
            }
            for (int i = 0; i < spawnsPage.client_Zonenames.Count; i++)
            {
                client_Zonenames_Lines.Add(spawnsPage.client_Zonenames[i].GetExportString());
            }
            for (int i = 0; i < spawnsPage.client_Raids.Count; i++)
            {
                client_Raids_Lines.Add(spawnsPage.client_Raids[i].GetExportString());
            }

            //Multisell
            for (int i = 0; i < multisellPage.L2H_Multisells.Count; i++)
            {
                server_Multisell_Lines.Add(multisellPage.L2H_Multisells[i].Server_Multisell.GetExportString());
            }

            mainWindow.UpdateLog("Gathering complete", L2H_Constants.Color_Add);

            mainWindow.UpdateLog("Saving gathered data..", L2H_Constants.Color_Log_Thread_Begin);


            //Saving data to decrypted .dat client files
            File.WriteAllLines(L2H_Constants.client_Itemnames_Path, client_Itemname_Lines.ToArray(), Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Weapons_Path, client_Weapons_Lines.ToArray(), Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Armors_Path, client_Armors_Lines.ToArray(), Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Etcs_Path, client_Etc_Lines.ToArray(), Encoding.GetEncoding(65001));
            //File.WriteAllLines(L2H_Constants.client_Skills_Path, client_Skills_Lines.ToArray(), Encoding.GetEncoding(65001));
            //File.WriteAllLines(L2H_Constants.client_Skillnames_Path, client_Skillnames_Lines.ToArray(), Encoding.GetEncoding(65001));
            //File.WriteAllLines(L2H_Constants.client_Skillsounds_Path, client_Skillsounds_Lines.ToArray(), Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_NPCs_Path, client_NPCs_Lines.ToArray(), Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_NPCnames_Path, client_NPC_Names_lines.ToArray(), Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Recipes_Path, client_Recipe_Lines.ToArray(), Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Obscene_Path, client_Chatfilter_Lines.ToArray(), Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Gametip_Path, client_Gametips_Lines.ToArray(), Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_NPCstrings_Path, client_NPC_Strings_Lines.ToArray(), Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Systemmsg_Path, client_System_Messages_Lines.ToArray(), Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Sysstring_Path, client_System_Strings_Lines.ToArray(), Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Eula_Path, client_EULA_Lines, Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Huntingzone_Path, client_Huntingzones_Lines, Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Zonename_Path, client_Zonenames_Lines, Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Raiddata_Path, client_Raids_Lines, Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Charcreategrp_Path, client_Charcreategrp_Lines, Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_Classinfo_Path, client_Classinfo_Lines, Encoding.GetEncoding(65001));
            File.WriteAllLines(L2H_Constants.client_L2ini_Path, mainWindow.client_L2Ini.GetExportStrings(overviewPage.L2H_Settings.serverAddress));

            //saving data to server files
            File.WriteAllLines(L2H_Constants.server_Item_PCH_Path, server_Item_PCH_Lines, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.server_Itemdata_Path, server_Itemdata_Lines, Encoding.GetEncoding(1200));
            File.AppendAllLines(L2H_Constants.server_Itemdata_Path, server_Sets_Lines, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.server_NPCdata_Path, server_NPCdata_Lines, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.server_NPC_PCH_Path, server_NPC_PCH_Lines, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.original_npcpos_path, server_Npcpos_originalStrings, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.custom_npcpos_path, server_Npcpos_customStrings, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.server_Recipes_Path, server_Recipe_Lines, Encoding.GetEncoding(1200));
            //File.WriteAllLines(L2H_Constants.server_Skilldata_Path, server_Skilldata_Lines, Encoding.GetEncoding(1200));
            //File.WriteAllLines(L2H_Constants.server_Skill_PCH_Path, server_Skill_PCH_Lines, Encoding.GetEncoding(1200));
            //File.WriteAllLines(L2H_Constants.server_Skill_PCH2_Path, server_Skill_PCH2_Lines, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.server_SkillenchantdataPath, server_Skillenchantdata_Lines, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.L2H_Single_Droplists_Path, L2H_Single_Droplists_Lines, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.L2H_Multi_Droplists_Path, L2H_Multi_Droplists_Lines, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.L2H_NPC_Droplists_Pointers_Path, L2H_NPC_Droplists_Pointers_Lines, Encoding.GetEncoding(1200));
            //File.WriteAllLines(skillenchantcostPath, skillenchantcostLines, Encoding.GetEncoding(1200)); Needs to be added through extender in H5 >:(
            File.WriteAllLines(L2H_Constants.L2H_Item_Template_Pointers_Path, L2H_itemTemplatePointers_Lines, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.L2H_Set_Template_Pointers_Path, L2H_setTemplate_Pointers_Lines, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.L2H_NPC_Template_Pointers_Path, L2H_npcTemplate_Pointers_Lines, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.L2H_Skills_Template_Pointers_Path, L2H_skillTemplate_Pointers_Lines, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.server_MultisellPath, server_Multisell_Lines, Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.server_PC_parametersPath, classesPage.pc_Parameters.GetExportStrings(), Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.server_ServerSettingPath, classesPage.serverSetting.GetExportStrings(), Encoding.GetEncoding(1200));
            File.WriteAllLines(L2H_Constants.server_Skillacquire_Path, server_Skillacquire_Lines, Encoding.GetEncoding(1200));



            mainWindow.UpdateLog("Save Complete", L2H_Constants.Color_Add);

            if (export)
            {
                Directory.CreateDirectory(L2H_Constants.export_Server_Folder_Path);
                mainWindow.UpdateLog("Exporting server files to Export/Server...", L2H_Constants.Color_Log_Thread_Begin);
                //Exporting server files
                File.Copy(L2H_Constants.server_Item_PCH_Path, L2H_Constants.export_Server_Item_PCH_Path, true);
                File.Copy(L2H_Constants.server_Itemdata_Path, L2H_Constants.export_Server_Itemdata_Path, true);
                File.Copy(L2H_Constants.server_NPCdata_Path, L2H_Constants.export_Server_NPCdata_Path, true);
                File.Copy(L2H_Constants.server_NPC_PCH_Path, L2H_Constants.export_Server_NPC_PCH_Path, true);
                File.Copy(L2H_Constants.server_Recipes_Path, L2H_Constants.export_Server_Recipes_Path, true);
                File.Copy(L2H_Constants.server_Skilldata_Path, L2H_Constants.export_Server_Skilldata_Path, true);
                File.Copy(L2H_Constants.server_Skill_PCH_Path, L2H_Constants.export_Server_Skill_PCH_Path, true);
                File.Copy(L2H_Constants.server_Skill_PCH2_Path, L2H_Constants.export_Server_Skill_PCH2_Path, true);
                File.Copy(L2H_Constants.server_SkillenchantdataPath, L2H_Constants.export_Server_Skill_Enchant_Data_Path, true);
                File.Copy(L2H_Constants.server_Skillacquire_Path, L2H_Constants.export_Server_Skill_Acquire_Path, true);
                File.Copy(L2H_Constants.server_MultisellPath, L2H_Constants.export_Server_Multisell_Path, true);
                File.Copy(L2H_Constants.server_ServerSettingPath, L2H_Constants.export_Server_Setting_Path, true);
                File.Copy(L2H_Constants.server_PC_parametersPath, L2H_Constants.export_Server_PC_Parameter_Path, true);
                File.Copy(L2H_Constants.server_expdata_Path, L2H_Constants.export_Server_Expdata_Path, true);

                if (overviewPage.L2H_Settings.ExportOnlyCustomSpawnAreas)
                {
                    File.WriteAllLines(L2H_Constants.export_Server_NPCpos_Path, server_Npcpos_customStrings, Encoding.GetEncoding(1200));
                }
                else
                {
                    File.WriteAllLines(L2H_Constants.export_Server_NPCpos_Path, server_Npcpos_fullStrings, Encoding.GetEncoding(1200));
                }

                mainWindow.UpdateLog("Server files exported", L2H_Constants.Color_Add);
                mainWindow.UpdateLog("Exporting client files to Export/Client...", L2H_Constants.Color_Log_Thread_Begin);
                //Exporting client files
                Directory.CreateDirectory(L2H_Constants.export_Client_Folder_Path);
                numberOfThreadsCompleted = 0;

                Export_Client_File("itemname-e");
                Export_Client_File("weapongrp");
                Export_Client_File("armorgrp");
                Export_Client_File("etcitemgrp");
                Export_Client_File("mobskillanimgrp");
                Export_Client_File("npcgrp");
                Export_Client_File("npcname-e");
                Export_Client_File("npcstring-e");
                Export_Client_File("recipe-c");
                Export_Client_File("skillgrp");
                Export_Client_File("skillname-e");
                Export_Client_File("skillsoundgrp");
                Export_Client_File("eula-e");
                Export_Client_File("huntingzone-e");
                Export_Client_File("gametip-e");
                Export_Client_File("zonename-e");
                Export_Client_File("raiddata-e");
                Export_Client_File("sysstring-e");
                Export_Client_File("systemmsg-e");
                Export_Client_File("obscene-e");
                Export_Client_File("classinfo-e");
                Export_Client_File("charcreategrp");
                Export_Client_File("l2ini");


            }
        }



        private void Export_Client_File(string fileName)
        {
            Thread_Client_File_Exporter thread_Client_File_Exporter = new Thread_Client_File_Exporter();
            thread_Client_File_Exporter.ThreadDone += (b, bb) => HandleClientThreadDone(b, bb, "Completed Client File Export: " + fileName + ".dat");
            thread_Client_File_Exporter.EncDecFile += (b, bb) => EncFile(b, bb, fileName);

            Thread t = new Thread(new ThreadStart(thread_Client_File_Exporter.ThreadProc));
            t.Start();
        }

        void HandleClientThreadDone(object sender, EventArgs e, string message)
        {
            Interlocked.Increment(ref numberOfThreadsCompleted);

            mainWindow.UpdateLog(message, L2H_Constants.Color_Add);

            if (numberOfFilesToExport == numberOfThreadsCompleted)
            {
                mainWindow.UpdateLog("Export Complete", L2H_Constants.Color_Add);
            }
        }
        void EncFile(object sender, EventArgs e, string fileName)
        {
            Process p = new Process();

            FileInfo dec_FileInfo;
            FileInfo existingExport_FileInfo = null;
            string existingExport_LastWriteTime = "";
            FileInfo exp_FileInfo;

            bool isIni = false;

            if (fileName == "l2ini")
            {
                isIni = true;
                dec_FileInfo = new FileInfo(L2H_Constants.client_Decrypted_Folder_Path + "\\l2.ini");

                if (File.Exists(L2H_Constants.export_Client_Folder_Path + "\\l2.ini"))
                {
                    existingExport_FileInfo = new FileInfo(L2H_Constants.export_Client_Folder_Path + "\\l2.ini");
                    existingExport_LastWriteTime = existingExport_FileInfo.LastWriteTime.ToString();
                }
            }
            else
            {
                dec_FileInfo = new FileInfo(L2H_Constants.client_Decrypted_Folder_Path + "\\" + fileName + ".txt");

                if (File.Exists(L2H_Constants.export_Client_Folder_Path + "\\" + fileName + ".dat"))
                {
                    existingExport_FileInfo = new FileInfo(L2H_Constants.export_Client_Folder_Path + "\\" + fileName + ".dat");
                    existingExport_LastWriteTime = existingExport_FileInfo.LastWriteTime.ToString();
                }
            }

            if (dec_FileInfo == null)
            {
                System.Windows.MessageBox.Show("Exception Occurred : Could not find decrypted file: " + fileName);
            }

            try
            {
                string targetDir;
                targetDir = string.Format("Tools\\mxencdec");
                string prefix = "encrypt_";

                p.StartInfo.UseShellExecute = true;
                p.StartInfo.WorkingDirectory = targetDir;
                p.StartInfo.FileName = prefix + fileName + ".bat";
                p.StartInfo.CreateNoWindow = false;
                p.Start();
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Exception Occurred : " + ex.Message);
            }

            if (!isIni)
            {
                exp_FileInfo = new FileInfo(L2H_Constants.export_Client_Folder_Path + "\\" + fileName + ".dat");
            }
            else
            {
                exp_FileInfo = new FileInfo(L2H_Constants.export_Client_Folder_Path + "\\l2.ini");
            }

            if (!string.IsNullOrEmpty(existingExport_LastWriteTime))
            {
                if (existingExport_LastWriteTime == exp_FileInfo.LastWriteTime.ToString())
                {
                    System.Windows.MessageBox.Show("File Export Error: " + fileName + "\n\nSomething went wrong exporting this file. Check your logs and see if you wrote something incorrect, or used an extra space or symbol.");
                }
            }
            if (dec_FileInfo.Length == exp_FileInfo.Length)
            {
                System.Windows.MessageBox.Show("File Encryption Error: " + fileName + "\n\nSomething went wrong encrypting this file. Check your logs and see if you wrote something incorrect, or used an extra space or symbol.");
            }

        }
    }

    public class Thread_Client_File_Exporter
    {
        public event EventHandler ThreadDone;
        public event EventHandler EncDecFile;

        public string fileName;

        public Thread_Client_File_Exporter()
        {

        }

        public void ThreadProc()
        {

            EncDecFile(this, EventArgs.Empty);

            if (ThreadDone != null)
            {
                ThreadDone(this, EventArgs.Empty);
            }
        }
    }


}
