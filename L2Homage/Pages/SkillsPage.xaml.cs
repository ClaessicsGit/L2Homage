using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace L2Homage.Pages
{

    /// <summary>
    /// Interaction logic for SkillsPage.xaml
    /// </summary>
    public partial class SkillsPage : Page
    {
        MainWindow mainWindow;
        List<List<L2H_Skill>> L2H_Skills_Lists;
        List<L2H_Skill> L2H_Skills;
        public List<Server_Skilldata> server_Skilldata;
        public List<Client_Skill> client_Skills;
        public List<Client_Skillname> client_Skillnames;
        public List<Client_Skillsound> client_Skillsounds;
        public List<L2H_Template_Pointer> skill_Template_Pointers;
        public List<Server_Skillenchantdata> server_Skillenchantdata;

        private ICollectionView skillView;
        private ICollectionView skillLevelsView;
        private ICollectionView skillVariantssView;

        public string client_Skills_Startline;
        public string client_Skillnames_Startline;
        public string client_Skillsounds_Startline;

        int numberOfThreadsForGeneratingL2HSkills = 0;
        int numberOfThreadsCompleted = 0;

        int numberOfThreadsForHookingEnchantedL2HSkills = 0;


        private bool searchIsShowing = true;
        public bool allSkillsLoaded = false;
        public bool forceWaiting = false;

        L2H_Skill active_L2H_Skill;
        ToggleButton active_Property_Button;
        ToggleButton activeSkillTypeToggleButton;

        public List<L2H_Skill> Get_L2H_Skills
        {
            get
            {
                return L2H_Skills;
            }
        }

        public SkillsPage()
        {
            InitializeComponent();
            mainWindow = Application.Current.MainWindow as MainWindow;

            L2H_Skills = new List<L2H_Skill>();
            L2H_Skills_Lists = new List<List<L2H_Skill>>();
            server_Skilldata = new List<Server_Skilldata>();
            client_Skills = new List<Client_Skill>();
            client_Skillnames = new List<Client_Skillname>();
            client_Skillsounds = new List<Client_Skillsound>();
            skill_Template_Pointers = new List<L2H_Template_Pointer>();
            server_Skillenchantdata = new List<Server_Skillenchantdata>();

            IList<L2H_Skill> skills = L2H_Skills;
            skillView = CollectionViewSource.GetDefaultView(skills);
            skillView.Filter = Filter_Skill_Name;

            IList<L2H_Skill> skills2 = L2H_Skills;
            skillLevelsView = CollectionViewSource.GetDefaultView(skills2);
            skillLevelsView.Filter = Filter_Skill_Levels;

            IList<L2H_Skill> skills3 = L2H_Skills;
            skillVariantssView = CollectionViewSource.GetDefaultView(skills3);
            skillVariantssView.Filter = Filter_Skill_Variants;

            activeSkillTypeToggleButton = Skill_Filter_Skill_Type_All;

        }


        public void LoadSkills()
        {
            Load_Skills_Template_Pointers();
            Load_Server_Skills();
            Load_Client_Skills();
            Load_Enchanted_Skill_Data();

            numberOfThreadsCompleted = 0;
            numberOfThreadsForGeneratingL2HSkills = (server_Skilldata.Count / L2H_Constants.Tasks_Per_Thread) + 1;
            for (int i = 0; i < numberOfThreadsForGeneratingL2HSkills; i++)
            {
                int value = i;
                ThreadWorker_Generate_L2H_Skills threadWorker_Generate_L2H_Skills = new ThreadWorker_Generate_L2H_Skills();
                threadWorker_Generate_L2H_Skills.ThreadDone += (b, bb) => HandleThreadDone(b, bb, "Completed Thread: Generate L2H Skills: " + (value * L2H_Constants.Tasks_Per_Thread).ToString() + " to " + (((value + 1) * L2H_Constants.Tasks_Per_Thread) - 1).ToString() + " of " + server_Skilldata.Count);
                threadWorker_Generate_L2H_Skills.startIndex = value * L2H_Constants.Tasks_Per_Thread;
                threadWorker_Generate_L2H_Skills.endIndex = ((value + 1) * L2H_Constants.Tasks_Per_Thread);
                threadWorker_Generate_L2H_Skills.L2H_Skills = L2H_Skills;
                threadWorker_Generate_L2H_Skills.L2H_Skills_Lists =L2H_Skills_Lists;

                threadWorker_Generate_L2H_Skills.server_Skilldata = server_Skilldata;
                threadWorker_Generate_L2H_Skills.client_Skills = client_Skills;
                threadWorker_Generate_L2H_Skills.client_Skillnames = client_Skillnames;
                threadWorker_Generate_L2H_Skills.client_Skillsounds = client_Skillsounds;
                threadWorker_Generate_L2H_Skills.templates = skill_Template_Pointers;

                Thread t = new Thread(new ThreadStart(threadWorker_Generate_L2H_Skills.ThreadProc));
                t.Start();
            }


            SpinWait.SpinUntil(() => numberOfThreadsForGeneratingL2HSkills == numberOfThreadsCompleted, Timeout.Infinite);

            for (int i = 0; i < L2H_Skills_Lists.Count; i++)
            {
                L2H_Skills.AddRange(L2H_Skills_Lists[i]);
            }

            numberOfThreadsCompleted = 0;
            numberOfThreadsForHookingEnchantedL2HSkills = (server_Skillenchantdata.Count / L2H_Constants.Tasks_Per_Thread) + 1;
            for (int i = 0; i < numberOfThreadsForHookingEnchantedL2HSkills; i++)
            {
                int value = i;
                ThreadWorker_Hook_Enchanted_L2H_Skills threadWorker_Hook_Enchanted_Skills = new ThreadWorker_Hook_Enchanted_L2H_Skills();
                threadWorker_Hook_Enchanted_Skills.ThreadDone += (b, bb) => HandleThreadDone(b, bb, "Completed Thread: Hooked Enchanted L2H Skills: " + (value * L2H_Constants.Tasks_Per_Thread).ToString() + " to " + (((value + 1) * L2H_Constants.Tasks_Per_Thread) - 1).ToString() + " of " + server_Skillenchantdata.Count);
                threadWorker_Hook_Enchanted_Skills.startIndex = value * L2H_Constants.Tasks_Per_Thread;
                threadWorker_Hook_Enchanted_Skills.endIndex = ((value + 1) * L2H_Constants.Tasks_Per_Thread);
                threadWorker_Hook_Enchanted_Skills.L2H_Skills = L2H_Skills;
                threadWorker_Hook_Enchanted_Skills.server_Skilldata = server_Skilldata;
                threadWorker_Hook_Enchanted_Skills.server_Skillenchantdata = server_Skillenchantdata;

                Thread t = new Thread(new ThreadStart(threadWorker_Hook_Enchanted_Skills.ThreadProc));
                t.Start();
            }


            SpinWait.SpinUntil(() => numberOfThreadsForHookingEnchantedL2HSkills == numberOfThreadsCompleted, Timeout.Infinite);


            allSkillsLoaded = true;

            Dispatcher.Invoke(() =>
                {
                    L2H_Skills = L2H_Skills.OrderBy(x => x != null ? x.Skill_Name : null).ToList();
                    Skills_Name_Listview.ItemsSource = L2H_Skills;
                    Skill_Levels_Listview.ItemsSource = L2H_Skills;
                    CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Skills_Name_Listview.ItemsSource);
                    view.Filter = Filter_Skill_Name;

                    var newView = new CollectionViewSource() { Source = L2H_Skills };
                    var listViewCollection2 = (ListCollectionView)newView.View;
                    listViewCollection2.Filter = Filter_Skill_Levels;
                    Skill_Levels_Listview.ItemsSource = listViewCollection2;
                    CollectionViewSource.GetDefaultView(Skill_Levels_Listview.ItemsSource).Refresh();

                    var newView2 = new CollectionViewSource() { Source = L2H_Skills };
                    var listViewCollection3 = (ListCollectionView)newView2.View;
                    listViewCollection3.Filter = Filter_Skill_Variants;
                    Skill_Variants_Listview.ItemsSource = listViewCollection3;
                    CollectionViewSource.GetDefaultView(Skill_Variants_Listview.ItemsSource).Refresh();

                    mainWindow.UpdateLog("Skills Loaded: " + server_Skilldata.Count, L2H_Constants.Color_Add);
                    mainWindow.UpdateLog("Enchanted Skills Loaded: " + server_Skillenchantdata.Count, L2H_Constants.Color_Add);
                    mainWindow.UpdateLoadedNumber(LoadedTypes.Skills);
                    mainWindow.UpdateLoadedNumber(LoadedTypes.Enchanted_Skills);

                });






        }

        private void Load_Client_Skills()
        {
            client_Skills.Clear();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Skills_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Skill newSkillgrp = new Client_Skill(line);
                    if (newSkillgrp.id != "skill_id")
                    {
                        client_Skills.Add(newSkillgrp);
                    }

                    else
                    {
                        client_Skills_Startline = line;
                    }
                }
            }

            // Skillnames
            client_Skillnames.Clear();

            using (TextReader textReader = new StreamReader(L2H_Constants.client_Skillnames_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Skillname newSkillname = new Client_Skillname(line);
                    if (newSkillname.id != "id")
                        client_Skillnames.Add(newSkillname);
                    else
                    {
                        client_Skillnames_Startline = line;
                    }
                }
            }

            // Sounds
            client_Skillsounds.Clear();


            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Skillsounds_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Skillsound newSkillsoundgrp = new Client_Skillsound(line);
                    if (newSkillsoundgrp.id != "skill_id")
                        client_Skillsounds.Add(newSkillsoundgrp);
                    else
                    {
                        client_Skillsounds_Startline = line;
                    }
                }
            }

        }

        private void Load_Skills_Template_Pointers()
        {
            if (File.Exists(L2H_Constants.L2H_Skills_Template_Pointers_Path))
            {
                using (TextReader textReader = new StreamReader(L2H_Constants.L2H_Skills_Template_Pointers_Path, Encoding.GetEncoding(65001)))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        L2H_Template_Pointer newTemplate_Pointer = new L2H_Template_Pointer(line);
                        if (newTemplate_Pointer.id != "id")
                            skill_Template_Pointers.Add(newTemplate_Pointer);
                    }
                }
            }
            else
            {
                File.Create(L2H_Constants.L2H_Skills_Template_Pointers_Path).Dispose();
            }
        }

        private void Load_Server_Skills()
        {
            server_Skilldata.Clear();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.server_Skilldata_Path, Encoding.GetEncoding(1200)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Server_Skilldata newSkilldata = new Server_Skilldata(line);

                    if (!string.IsNullOrEmpty(newSkilldata.skill_name))
                        server_Skilldata.Add(newSkilldata);
                }
            }

            string testtest = "la;";

        }

        private void Load_Enchanted_Skill_Data()
        {
            server_Skillenchantdata.Clear();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.server_SkillenchantdataPath, Encoding.GetEncoding(1200)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Server_Skillenchantdata newSkilldata = new Server_Skillenchantdata(line);
                    server_Skillenchantdata.Add(newSkilldata);
                }
            }

        }


        private bool Filter_Skill_Name(object item)
        {
            L2H_Skill filteredSkill = item as L2H_Skill;

            if (filteredSkill == null)
                return false;

            if (int.Parse(filteredSkill.Skill_Level) > 1)
                return false;

            if (filteredSkill.client_Skill == null)
                return false;

            if (!string.IsNullOrEmpty(Skill_Filter_Name.Text))
            {
                return (filteredSkill.client_Skillname.name.IndexOf(Skill_Filter_Name.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            if (!string.IsNullOrEmpty(Skill_Filter_ID.Text))
            {
                return (filteredSkill.ID.IndexOf(Skill_Filter_ID.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            if (Skill_Filter_Custom_Toggle.IsChecked == true)
                return filteredSkill.IsCustom;

            if (activeSkillTypeToggleButton == Skill_Filter_Skill_Type_All)
            {
                return true;
            }
            else if (activeSkillTypeToggleButton == Skill_Filter_Skill_Type_Active)
            {
                if (filteredSkill.server_Skilldata.operate_type == "A1" ||
                    filteredSkill.server_Skilldata.operate_type == "A2" ||
                    filteredSkill.server_Skilldata.operate_type == "A3" ||
                    filteredSkill.server_Skilldata.operate_type == "A4" ||
                    filteredSkill.server_Skilldata.operate_type == "CA1" ||
                    filteredSkill.server_Skilldata.operate_type == "CA5" ||
                    filteredSkill.server_Skilldata.operate_type == "DA1" ||
                    filteredSkill.server_Skilldata.operate_type == "DA2")
                    return true;
                else
                    return false;
            }
            else if (activeSkillTypeToggleButton == Skill_Filter_Skill_Type_Passive)
            {
                if (filteredSkill.server_Skilldata.operate_type == "P")
                    return true;
                else
                    return false;
            }
            else if (activeSkillTypeToggleButton == Skill_Filter_Skill_Type_Toggle)
            {
                if (filteredSkill.server_Skilldata.operate_type == "T")
                    return true;
                else
                    return false;
            }

            return true;

        }
        private bool Filter_Skill_Levels(object item)
        {
            L2H_Skill filteredSkill = item as L2H_Skill;

            if (active_L2H_Skill == null)
                return false;

            if (filteredSkill == null)
                return false;


            if (filteredSkill.ID != active_L2H_Skill.ID)
                return false;

            if (filteredSkill.Skill_Enchanted_Skill_ID != active_L2H_Skill.Skill_Enchanted_Skill_ID)
                return false;

            if (filteredSkill.Skill_Level.Length > 2)
                if (filteredSkill.Skill_Level[0] != active_L2H_Skill.Skill_Level[0])
                    return false;

            if (filteredSkill.client_Skill == null)
                return false;


            return true;

        }
        private bool Filter_Skill_Variants(object item)
        {
            L2H_Skill filteredSkill = item as L2H_Skill;

            if (active_L2H_Skill == null)
                return false;

            if (filteredSkill == null)
                return false;

            if (filteredSkill.ID != active_L2H_Skill.ID)
                return false;

            if (filteredSkill.client_Skill == null)
                return false;

            if (filteredSkill.Skill_Level == "1" || filteredSkill.Skill_Level.Contains("01"))
                return true;
            else
                return false;

            //return true;

        }
        void HandleThreadDone(object sender, EventArgs e, string message)
        {
            Interlocked.Increment(ref numberOfThreadsCompleted);
        }

        private void Skill_Clone_Clicked(object sender, RoutedEventArgs e)
        {
            CloneSkill(sender, e, false);
        }

        private void Skill_Delete_Clicked(object sender, RoutedEventArgs e)
        {
            DeleteSkill(active_L2H_Skill);
        }

        private void Skill_Filter_Changed(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Skills_Name_Listview.ItemsSource).Refresh();
        }

        private void Skill_Filter_Changed(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Skills_Name_Listview.ItemsSource).Refresh();
        }


        private void ToggleSearchVisibility(object sender, RoutedEventArgs e)
        {
            if (searchIsShowing)
            {
                Skills_Main_Grid.ColumnDefinitions[0].Width = new GridLength(0);
                Skills_Main_Grid.ColumnDefinitions[1].Width = new GridLength(0);
                searchIsShowing = false;
                SearchIsShowingButton.Content = "Show Search";
            }
            else
            {
                Skills_Main_Grid.ColumnDefinitions[0].Width = new GridLength(220);
                Skills_Main_Grid.ColumnDefinitions[1].Width = new GridLength(220);
                searchIsShowing = true;
                SearchIsShowingButton.Content = "Hide Search";
            }

        }

        private void Skill_Name_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Skill skillClicked = vm as L2H_Skill;

            if (active_L2H_Skill != null)
            {
                if (active_L2H_Skill == skillClicked)
                {
                    if (!active_L2H_Skill.IsSelected)
                    {
                        Skills_Details_Grid.Visibility = Visibility.Hidden;
                        active_L2H_Skill.IsSelected = false;
                        active_L2H_Skill = null;
                        return;
                    }
                    else
                    {
                        Skills_Details_Grid.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    active_L2H_Skill.IsSelected = false;
                    active_L2H_Skill = skillClicked;
                    active_L2H_Skill.IsSelected = true;
                }
            }
            else
            {
                active_L2H_Skill = skillClicked;
                active_L2H_Skill.IsSelected = true;
            }


            Refresh_Skill_Details(active_L2H_Skill);
        }

        private void Skill_Level_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Skill skillClicked = vm as L2H_Skill;

            if (skillClicked == active_L2H_Skill)
            {
                skillClicked.IsSelected = true;
                RefreshViews();
                return;
            }

            Skill_Name_Clicked(sender, e);
        }
        private void Skill_Variant_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            Skill_Name_Clicked(sender, e);
        }
        void Refresh_Skill_Details(L2H_Skill skill)
        {

            Skill_Details_Preview_Name.DataContext = skill;
            Skill_Details_Preview_Icon.Source = L2H_Parser.GetSkillImage(skill.client_Skill.icon_name);


            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Skills_Details_Grid))
            {
                tb.DataContext = skill;
            }
            foreach (ToggleButton tb in L2H_Parser.FindVisualChildren<ToggleButton>(Skills_Details_Grid))
            {
                tb.DataContext = skill;
            }
            foreach (ComboBox cb in L2H_Parser.FindVisualChildren<ComboBox>(Skills_Details_Grid))
            {
                cb.DataContext = skill;
            }

            Skills_Details_Grid.Visibility = Visibility.Visible;


            CollectionViewSource.GetDefaultView(Skills_Name_Listview.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(Skill_Levels_Listview.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(Skill_Variants_Listview.ItemsSource).Refresh();

        }

        private void Skill_Property_Category_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            ToggleButton tb = (sender as ToggleButton);

            if (active_Property_Button == null)
            {
                active_Property_Button = tb;
                active_Property_Button.IsChecked = true;
            }
            else
            {
                active_Property_Button.IsChecked = false;
                active_Property_Button = tb;
                active_Property_Button.IsChecked = true;
            }

            switch (tb.Tag) 
            {
                case "casting":
                    Casting_Panel.Visibility = Visibility.Visible;
                    Targeting_Panel.Visibility = Visibility.Hidden;
                    Type_And_Effects_Properties_Panel.Visibility = Visibility.Hidden;
                    Buffs_Debuffs_Properties_Panel.Visibility = Visibility.Hidden;
                    Conditions_Panel.Visibility = Visibility.Hidden;
                    Misc_Properties_Panel.Visibility = Visibility.Hidden;
                    Enchanted_Panel.Visibility = Visibility.Hidden;
                    break;
                case "targeting":
                    Casting_Panel.Visibility = Visibility.Hidden;
                    Targeting_Panel.Visibility = Visibility.Visible;
                    Type_And_Effects_Properties_Panel.Visibility = Visibility.Hidden;
                    Buffs_Debuffs_Properties_Panel.Visibility = Visibility.Hidden;
                    Conditions_Panel.Visibility = Visibility.Hidden;
                    Misc_Properties_Panel.Visibility = Visibility.Hidden;
                    Enchanted_Panel.Visibility = Visibility.Hidden;
                    break;
                case "effects":
                    Casting_Panel.Visibility = Visibility.Hidden;
                    Targeting_Panel.Visibility = Visibility.Hidden;
                    Type_And_Effects_Properties_Panel.Visibility = Visibility.Visible;
                    Buffs_Debuffs_Properties_Panel.Visibility = Visibility.Hidden;
                    Conditions_Panel.Visibility = Visibility.Hidden;
                    Misc_Properties_Panel.Visibility = Visibility.Hidden;
                    Enchanted_Panel.Visibility = Visibility.Hidden;
                    break;
                case "buffs":
                    Casting_Panel.Visibility = Visibility.Hidden;
                    Targeting_Panel.Visibility = Visibility.Hidden;
                    Type_And_Effects_Properties_Panel.Visibility = Visibility.Hidden;
                    Buffs_Debuffs_Properties_Panel.Visibility = Visibility.Visible;
                    Conditions_Panel.Visibility = Visibility.Hidden;
                    Misc_Properties_Panel.Visibility = Visibility.Hidden;
                    Enchanted_Panel.Visibility = Visibility.Hidden;
                    break;
                case "conditions":
                    Casting_Panel.Visibility = Visibility.Hidden;
                    Targeting_Panel.Visibility = Visibility.Hidden;
                    Type_And_Effects_Properties_Panel.Visibility = Visibility.Hidden;
                    Buffs_Debuffs_Properties_Panel.Visibility = Visibility.Hidden;
                    Conditions_Panel.Visibility = Visibility.Visible;
                    Misc_Properties_Panel.Visibility = Visibility.Hidden;
                    Enchanted_Panel.Visibility = Visibility.Hidden;
                    break;
                case "misc":
                    Casting_Panel.Visibility = Visibility.Hidden;
                    Targeting_Panel.Visibility = Visibility.Hidden;
                    Type_And_Effects_Properties_Panel.Visibility = Visibility.Hidden;
                    Buffs_Debuffs_Properties_Panel.Visibility = Visibility.Hidden;
                    Conditions_Panel.Visibility = Visibility.Hidden;
                    Misc_Properties_Panel.Visibility = Visibility.Visible;
                    Enchanted_Panel.Visibility = Visibility.Hidden;
                    break;
                case "enchanted":
                    Casting_Panel.Visibility = Visibility.Hidden;
                    Targeting_Panel.Visibility = Visibility.Hidden;
                    Type_And_Effects_Properties_Panel.Visibility = Visibility.Hidden;
                    Buffs_Debuffs_Properties_Panel.Visibility = Visibility.Hidden;
                    Conditions_Panel.Visibility = Visibility.Hidden;
                    Misc_Properties_Panel.Visibility = Visibility.Hidden;
                    Enchanted_Panel.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }



        }

        public void CloneSkill(object sender, EventArgs e, bool cloneAsNewLevel)
        {
            if (active_L2H_Skill == null)
                return;

            //Grab relevant information and create a deep copy
            Client_Skill newSkill = ObjectExtensions.Copy(active_L2H_Skill.client_Skill);
            Server_Skilldata newSkillData = ObjectExtensions.Copy(active_L2H_Skill.server_Skilldata);
            Client_Skillname newSkillName = ObjectExtensions.Copy(active_L2H_Skill.client_Skillname);
            Client_Skillsound newSkillSound = ObjectExtensions.Copy(active_L2H_Skill.client_Skillsound);

            //Assign new ID:
            if (!cloneAsNewLevel)
            {
                string newSkillID = (mainWindow.GetPageOfType(typeof(Pages.OverviewPage)) as OverviewPage).L2H_Settings.NewSkillIndexStart; // "50000";

                if (skill_Template_Pointers.Count > 0)
                {
                    newSkillID = (Convert.ToInt32(skill_Template_Pointers[skill_Template_Pointers.Count - 1].id) + 1).ToString();
                }

                newSkill.id = newSkillID;
                newSkillData.skill_id = newSkillID;
                newSkillName.id = newSkillID;

                newSkill.level = "1";
                newSkillData.level = "1";
                newSkillName.level = "1";
                

                if (newSkillSound != null)
                {
                    newSkillSound.id = newSkillID;
                    newSkillSound.level = "1";
                    client_Skillsounds.Add(newSkillSound);
                }

                skill_Template_Pointers.Add(new L2H_Template_Pointer(newSkillID, active_L2H_Skill.ID));
            }
            else
            {
                string newSkillLevel = L2H_Parser.GetNextSkillLevel(client_Skills, active_L2H_Skill.ID, newSkill.level);
                if (int.Parse(newSkillLevel) > 99)
                    return;
                newSkill.level = newSkillLevel;
                newSkillData.level = newSkillLevel;
                newSkillName.level = newSkillLevel;
            }


            newSkillData.skill_name = L2H_Parser.GetUniqueSkillNameID(server_Skilldata, newSkillData.skill_name);

            L2H_Skill newL2HSkill = new L2H_Skill()
            {
                client_Skill = newSkill,
                server_Skilldata = newSkillData,
                client_Skillname = newSkillName,
                client_Skillsound = newSkillSound,
                IsCustom = true
            };

            //Add to lists
            client_Skills.Add(newSkill);
            server_Skilldata.Add(newSkillData);
            client_Skillnames.Add(newSkillName);
            L2H_Skills.Add(newL2HSkill);
            L2H_Log.Instance.Log_Skill_Clone(active_L2H_Skill, newL2HSkill);


        }

        public void RefreshViews()
        {
            CollectionViewSource.GetDefaultView(Skills_Name_Listview.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(Skill_Levels_Listview.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(Skill_Variants_Listview.ItemsSource).Refresh();
        }

        public void Force_Skill_Selection(L2H_Skill targetSkill)
        {
            if (active_L2H_Skill != null)
            {
                if (active_L2H_Skill == targetSkill)
                    return;
                else
                {
                    active_L2H_Skill.IsSelected = false;
                }
            }

            active_L2H_Skill = targetSkill;
            active_L2H_Skill.IsSelected = true;

            Refresh_Skill_Details(active_L2H_Skill);

            Skills_Details_Grid.Visibility = Visibility.Visible;

            RefreshViews();
        }

        private void DeleteSkill(L2H_Skill skill)
        {

            if (skill != null)
            {
                if (skill.IsCustom == false)
                {
                    MessageBox.Show("Cannot delete original data");
                    return;
                }
                //Remove all references of item

                L2H_Log.Instance.Log_Skill_Delete(skill);

                client_Skills.Remove(skill.client_Skill);
                server_Skilldata.Remove(skill.server_Skilldata);
                client_Skillnames.Remove(skill.client_Skillname);

                //Only remove skilltemplate pointer if there's no more skills left that uses it
                List<L2H_Template_Pointer> pointers = new List<L2H_Template_Pointer>();
                pointers = skill_Template_Pointers.FindAll(x => x.id == skill.ID);
                if (pointers.Count == 1)
                {
                    skill_Template_Pointers.Remove(skill_Template_Pointers.Find(x => x.id == skill.ID));
                    Client_Skillsound targetSkillSound = client_Skillsounds.Find(x => x.id == skill.ID);
                    if (targetSkillSound != null)
                    {
                        client_Skillsounds.Remove(targetSkillSound);
                    }
                }


                L2H_Skills.Remove(skill);
                active_L2H_Skill = null;
                Skills_Details_Grid.Visibility = Visibility.Hidden;


                CollectionViewSource.GetDefaultView(Skills_Name_Listview.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(Skill_Levels_Listview.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(Skill_Variants_Listview.ItemsSource).Refresh();


            }
        }

        private void Skill_Add_Levels_Clicked(object sender, RoutedEventArgs e)
        {
            Popup_Skill_Add_Levels newPopup = new Popup_Skill_Add_Levels();
            newPopup.Initialize(this);
        }


        private void Validate_Lower_Case_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Name_ID(e.Text);
        }
        private void Validate_Integer_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Integer(e.Text);
        }
        private void TextBox_Float_No_Spaces_Allowed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;

            if (L2H_Textbox_Input_Restrictions.Is_Valid_Float(e.Key.ToString()))
                L2H_Textbox_Input_Restrictions.Check_If_Dot_Exists_In_Float_TextBox(sender, e);
        }
        private void Validate_Any_Case_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Ingame_Name(e.Text);
        }
        private void Validate_Clientfile_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Clientfile_Entry(e.Text);
        }
        private void Validate_Lower_Case_And_Symbols_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Lower_Case_And_Symbols(e.Text);
        }

        private void TextBox_No_Spaces_Allowed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;

            L2H_Textbox_Input_Restrictions.Check_If_Dot_Exists_In_Float_TextBox(sender, e);
        }

        private void Skill_Is_Attribute_Clicked(object sender, RoutedEventArgs e)
        {
            //Bind or unbind here
        }

        private void Skill_Batch_Clicked(object sender, RoutedEventArgs e)
        {

            List<L2H_Skill> skillLevels = new List<L2H_Skill>();

            foreach (var x in Skill_Levels_Listview.Items)
            {
                skillLevels.Add((L2H_Skill)x);
            }

            Popup_Skill_Batch sb = new Popup_Skill_Batch(skillLevels);

            sb.ShowDialog();
        }

        private void Skill_Is_Enchanted_Clicked(object sender, RoutedEventArgs e)
        {
            L2H_Skill s = (sender as ToggleButton).DataContext as L2H_Skill;

            s.Skill_Is_Enchanted = (bool)(sender as ToggleButton).IsChecked;
        }

        private void Skill_Type_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            if (activeSkillTypeToggleButton != sender as ToggleButton)
                activeSkillTypeToggleButton.IsChecked = false;

            activeSkillTypeToggleButton = sender as ToggleButton;
            activeSkillTypeToggleButton.IsChecked = true;

            RefreshViews();
        }

        private void Skill_Force_Select_Waiting(object sender, RoutedEventArgs e)
        {
            if (forceWaiting)
            {
                Refresh_Skill_Details(active_L2H_Skill);
                forceWaiting = false;
            }
        }
        void ClearFocus()
        {
            if (Keyboard.FocusedElement != null)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;
                Keyboard.ClearFocus();
                focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
            }
        }
    }

    public class ThreadWorker_Generate_L2H_Skills
    {
        public event EventHandler ThreadDone;

        public int startIndex;
        public int endIndex;
        public List<L2H_Skill> L2H_Skills;
        public List<List<L2H_Skill>> L2H_Skills_Lists;
        public List<L2H_Skill> temp_Skills;
        public List<Server_Skilldata> server_Skilldata;
        public List<Client_Skill> client_Skills;
        public List<Client_Skillname> client_Skillnames;
        public List<Client_Skillsound> client_Skillsounds;
        public List<L2H_Template_Pointer> templates;

        public ThreadWorker_Generate_L2H_Skills()
        {
            temp_Skills = new List<L2H_Skill>();
        }

        public void ThreadProc()
        {

            if (endIndex > server_Skilldata.Count)
                endIndex = server_Skilldata.Count;

            for (int i = startIndex; i < endIndex; i++)
            {
                L2H_Skill new_L2H_Skill = new L2H_Skill()
                {
                    server_Skilldata = server_Skilldata[i],
                    client_Skill = client_Skills.Find(x => x.id == server_Skilldata[i].skill_id && x.level == server_Skilldata[i].level),
                    client_Skillname = client_Skillnames.Find(x => x.id == server_Skilldata[i].skill_id && x.level == server_Skilldata[i].level),
                    client_Skillsound = client_Skillsounds.Find(x => x.id == server_Skilldata[i].skill_id),

                };

                if (templates.Exists(x => x.id == server_Skilldata[i].skill_id))
                    new_L2H_Skill.IsCustom = true;

                temp_Skills.Add(new_L2H_Skill);

            }

            if (ThreadDone != null)
            {
                L2H_Skills_Lists.Add(temp_Skills);

                ThreadDone(this, EventArgs.Empty);
            }
        }
    }

    public class ThreadWorker_Hook_Enchanted_L2H_Skills
    {
        public event EventHandler ThreadDone;

        public int startIndex;
        public int endIndex;
        public List<L2H_Skill> L2H_Skills;
        public List<Server_Skilldata> server_Skilldata;
        public List<Server_Skillenchantdata> server_Skillenchantdata;

        public ThreadWorker_Hook_Enchanted_L2H_Skills()
        {

        }

        public void ThreadProc()
        {

            if (endIndex > server_Skillenchantdata.Count)
                endIndex = server_Skillenchantdata.Count;

            for (int i = startIndex; i < endIndex; i++)
            {
                if (string.IsNullOrEmpty(server_Skillenchantdata[i].original_skill_id))
                {
                    L2H_Skill originalSkill = L2H_Skills.Find(x => x.Skill_Name_ID == server_Skillenchantdata[i].original_skill);

                    List<Server_Skillenchantdata> sameOriginalSkillEnchantedSkills = server_Skillenchantdata.FindAll(x => x.original_skill == originalSkill.Skill_Name_ID);

                    for (int j = 0; j < sameOriginalSkillEnchantedSkills.Count; j++)
                    {
                        sameOriginalSkillEnchantedSkills[j].original_skill_id = originalSkill.Skill_ID;
                        L2H_Skills.Find(x => x.Skill_ID == originalSkill.Skill_ID && x.Skill_Level == sameOriginalSkillEnchantedSkills[j].skill_level).server_Skillenchantdata = sameOriginalSkillEnchantedSkills[j];
                    }

                }

            }



            if (ThreadDone != null)
            {

                ThreadDone(this, EventArgs.Empty);
            }
        }
    }
}
