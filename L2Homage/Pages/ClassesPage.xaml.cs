using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace L2Homage.Pages
{
    /// <summary>
    /// Interaction logic for ClassesPage.xaml
    /// </summary>
    public partial class ClassesPage : Page
    {
        public List<L2H_Skill_Acquire> L2H_Skill_Acquires;
        public List<L2H_Character_Class> L2H_Character_Classes;

        public List<Client_Charcreate> client_Charcreates;
        public List<Client_Classinfo> client_Classinfos;

        public List<Server_Skillacquire> server_Skillacquires;
        public Server_PC_Parameter pc_Parameters;
        public ServerSetting serverSetting;

        L2H_Character_Class active_L2H_Character_Class;
        L2H_Skill_Acquire active_L2H_Skill_Acquire;
        ToggleButton active_Class_Type;

        public string charcreategrpsStartLine;
        public string classinfoStartLine;
        private bool searchIsShowing;

        public bool finishedLoading;

        MainWindow mainWindow;
        ItemsPage itemsPage;
        int numberOfThreadsToUseForHookingSkillsToSkillAcquires = 0;
        int numberOfThreadsCompleted = 0;

        public ClassesPage()
        {
            InitializeComponent();

            active_Class_Type = Class_Detail_Type_Base;
            Class_Base_Details_Grid.Visibility = Visibility.Visible;

            mainWindow = ((MainWindow)Application.Current.MainWindow);
            itemsPage = (((MainWindow)Application.Current.MainWindow).GetPageOfType(typeof(ItemsPage)) as ItemsPage);

        }

        public void Load_Classes_ServerSettings_SkillAcquire()
        {
            LoadCharcreategrp();
            LoadClassinfo_e();
            LoadPC_Parameters();
            LoadServerSetting();
            LoadSkillacquire();
        }

        private void LoadCharcreategrp()
        {
            client_Charcreates = new List<Client_Charcreate>();
            client_Charcreates.Clear();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Charcreategrp_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Charcreate newCharcreategrp = new Client_Charcreate(line);
                    if (newCharcreategrp.flts_0 != "flts[0]")
                    {
                        client_Charcreates.Add(newCharcreategrp);
                    }
                    else
                    {
                        charcreategrpsStartLine = line;
                    }
                }
            }
        }
        private void LoadClassinfo_e()
        {
            client_Classinfos = new List<Client_Classinfo>();
            client_Classinfos.Clear();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Classinfo_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Classinfo newClassinfo = new Client_Classinfo(line);
                    if (newClassinfo.id != "id")
                    {
                        client_Classinfos.Add(newClassinfo);
                    }

                    else
                    {
                        classinfoStartLine = line;
                    }
                }
            }
        }
        private void LoadPC_Parameters()
        {
            //List<string> serverSetting = new List<string>();
            pc_Parameters = new Server_PC_Parameter();

            string activeParameterType = "";

            using (TextReader textReader = new StreamReader(L2H_Constants.server_PC_parametersPath, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    if (line.Contains("_hp_table_begin"))
                    {
                        activeParameterType = line;
                        activeParameterType = activeParameterType.Replace("\t", "");
                    }
                    else if (line.Contains("_mp_table_begin"))
                    {
                        activeParameterType = line;
                        activeParameterType = activeParameterType.Replace("\t", "");
                    }
                    else if (line.Contains("_cp_table_begin"))
                    {
                        activeParameterType = line;
                        activeParameterType = activeParameterType.Replace("\t", "");
                    }
                    else if (line.Contains("_end"))
                    {
                        if (line.Replace("\t", "") != "area_end" && line.Replace("\t", "") != "point_end")
                        {
                            activeParameterType = activeParameterType.Replace("\t", "");
                            activeParameterType = "";
                        }
                    }
                    else if (activeParameterType.Contains("_hp_table_begin"))
                    {
                        pc_Parameters.AddData(line, activeParameterType, activeParameterType.Replace("_hp_table_begin", ""));
                    }
                    else if (activeParameterType.Contains("_mp_table_begin"))
                    {
                        pc_Parameters.AddData(line, activeParameterType, activeParameterType.Replace("_mp_table_begin", ""));
                    }
                    else if (activeParameterType.Contains("_cp_table_begin"))
                    {
                        pc_Parameters.AddData(line, activeParameterType, activeParameterType.Replace("_cp_table_begin", ""));
                    }
                    else if (line.Contains("_begin"))
                    {
                        if (activeParameterType != line && line.Replace("\t", "") != "area_begin" && line.Replace("\t", "") != "point_begin")
                        {
                            activeParameterType = activeParameterType.Replace("\t", "");
                            activeParameterType = line;
                        }
                    }

                    else
                    {
                        pc_Parameters.AddData(line, activeParameterType);
                    }
                }

            }

        }

        private void LoadServerSetting()
        {
            serverSetting = new ServerSetting();
            serverSetting.L2H_Items = itemsPage.L2H_Items;

            string activeServerSettingType = "";

            using (TextReader textReader = new StreamReader(L2H_Constants.server_ServerSettingPath, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    if (activeServerSettingType.Contains("cleft_setting_begin") ||
                        activeServerSettingType.Contains("pvpmatch_setting_start") ||
                        activeServerSettingType.Contains("hero_general_setting_start") ||
                        activeServerSettingType.Contains("olympiad_genaral_setting_begin") ||
                        activeServerSettingType.Contains("olympiad_arena_begin") ||
                        activeServerSettingType.Contains("give_pvppoint_time_start") ||
                        activeServerSettingType.Contains("restart_point_begin"))
                    {
                        if (!line.Contains("cleft_setting_end") &&
                            !line.Contains("pvpmatch_setting_end") &&
                            !line.Contains("hero_general_setting_end") &&
                            !line.Contains("olympiad_general_setting_end") &&
                            !line.Contains("olympiad_arena_end") &&
                            !line.Contains("restart_point_end"))
                        {
                            serverSetting.AddData(line, activeServerSettingType);
                        }
                        else
                        {
                            activeServerSettingType = "";
                        }

                    }
                    else if (line.Contains("_begin") || line.Contains("_start"))
                    {
                        if (activeServerSettingType != line &&
                            line.Replace("\t", "") != "area_begin" &&
                            line.Replace("\t", "") != "point_begin")
                        {
                            activeServerSettingType = line;
                        }
                        else
                        {
                            serverSetting.AddData(line.Replace("\t", ""), activeServerSettingType);
                        }
                    }
                    else if (line.Contains("_end"))
                    {
                        if (line.Replace("\t", "") != "area_end" && line.Replace("\t", "") != "point_end")
                            activeServerSettingType = "";
                        else if (line.Replace("\t", "") == "area_end")
                            serverSetting.AddData(line.Replace("\t", ""), activeServerSettingType);
                        else if (line.Contains("give_pvppoint_time_start") ||
                                line.Contains("red_start_point_end") ||
                                line.Contains("blue_start_point_end") ||
                                line.Contains("red_banish_point_end") ||
                                line.Contains("blue_banish_point_end") ||
                                line.Contains("give_pvppoint_end_time") ||
                                line.Contains("give_pvppoint_time_end"))
                        {
                            serverSetting.AddData(line.Replace("\t", ""), activeServerSettingType);
                        }
                    }
                    else
                    {
                        serverSetting.AddData(line.Replace("\t", ""), activeServerSettingType);
                    }

                }

            }

        }

        private void LoadSkillacquire()
        {
            server_Skillacquires = new List<Server_Skillacquire>();
            server_Skillacquires.Clear();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.server_Skillacquire_Path, Encoding.GetEncoding(1200)))
            {
                L2H_Character_Classes = new List<L2H_Character_Class>();

                L2H_Skill_Acquires = new List<L2H_Skill_Acquire>();

                // Load the text line by line
                string line = string.Empty;
                string activeClass = "";
                string includeClass = "";
                while ((line = textReader.ReadLine()) != null)
                {
                    string[] splitDataline = line.Split('\t');

                    if (splitDataline.Length > 0)
                    {
                        if (splitDataline[0].Contains("_begin") && splitDataline[0] != "skill_begin")
                        {
                            activeClass = splitDataline[0];
                            includeClass = "";
                        }
                        else if (splitDataline[0].Contains("include_"))
                        {
                            includeClass = splitDataline[0];
                        }
                        else
                        {
                            if (splitDataline.Length > 1)
                                if (splitDataline[1] != "")
                                {
                                    Server_Skillacquire newSkillacquiredata = new Server_Skillacquire(splitDataline, activeClass, includeClass);
                                    server_Skillacquires.Add(newSkillacquiredata);

                                    L2H_Skill_Acquires.Add(new L2H_Skill_Acquire() { server_Skillacquire = newSkillacquiredata });

                                    if (!L2H_Character_Classes.Exists(x => x.classID == newSkillacquiredata.class_begin.Replace("_begin", "")))
                                    {
                                        L2H_Character_Class l2H_Character_Class = new L2H_Character_Class() { classID = newSkillacquiredata.class_begin.Replace("_begin", "") };
                                        L2H_Character_Classes.Add(l2H_Character_Class);
                                    }
                                }
                        }
                    }
                }

                //Multithreading to find L2H_Skill for each skill_acquire
                numberOfThreadsCompleted = 0;
                numberOfThreadsToUseForHookingSkillsToSkillAcquires = (L2H_Skill_Acquires.Count / L2H_Constants.Tasks_Per_Thread) + 1;
                                

                for (int i = 0; i < numberOfThreadsToUseForHookingSkillsToSkillAcquires; i++)
                {
                    int value = i;
                    ThreadWorker_Hook_Skills_To_SkillAcquires threadWorker_Hook_Skills_To_SkillAcquires = new ThreadWorker_Hook_Skills_To_SkillAcquires();
                    threadWorker_Hook_Skills_To_SkillAcquires.ThreadDone += (b, bb) => HandleThreadDone(b, bb, "Completed Thread: Hook Skills to SkillAcquires: " + (value * L2H_Constants.Tasks_Per_Thread).ToString() + " to " + (((value + 1) * L2H_Constants.Tasks_Per_Thread) - 1).ToString() + " of " + server_Skillacquires.Count);
                    threadWorker_Hook_Skills_To_SkillAcquires.startIndex = value * L2H_Constants.Tasks_Per_Thread;
                    threadWorker_Hook_Skills_To_SkillAcquires.endIndex = ((value + 1) * L2H_Constants.Tasks_Per_Thread);
                    threadWorker_Hook_Skills_To_SkillAcquires.L2H_Skills = (mainWindow.GetPageOfType(typeof(Pages.SkillsPage)) as SkillsPage).Get_L2H_Skills;
                    threadWorker_Hook_Skills_To_SkillAcquires.L2H_Skill_Acquires = L2H_Skill_Acquires;
                    Thread t = new Thread(new ThreadStart(threadWorker_Hook_Skills_To_SkillAcquires.ThreadProc));
                    t.Start();
                }

                Dispatcher.Invoke(() =>
                {
                    (Application.Current.MainWindow as MainWindow).UpdateLog("Skills hooked to SkillAcquires: " + server_Skillacquires.Count, L2H_Constants.Color_Add);
                });

                SpinWait.SpinUntil(() => numberOfThreadsToUseForHookingSkillsToSkillAcquires == numberOfThreadsCompleted, Timeout.Infinite);

                L2H_Character_Classes = L2H_Character_Classes.OrderBy(x => x != null ? x.classID : null).ToList();

                for (int i = 0; i < L2H_Character_Classes.Count; i++)
                {
                    L2H_Character_Classes[i].L2H_Skill_Acquires.Clear();
                    L2H_Character_Classes[i].L2H_Skill_Acquires.AddRange(L2H_Skill_Acquires.FindAll(x => x.server_Skillacquire.class_begin.Replace("_begin", "") == L2H_Character_Classes[i].classID));

                }

                Dispatcher.Invoke(() =>
                {
                    var newView = new CollectionViewSource() { Source = L2H_Character_Classes };
                    var listViewCollection2 = (ListCollectionView)newView.View;
                    listViewCollection2.Filter = Filter_Class_Name;
                    Class_IDs_Listview.ItemsSource = listViewCollection2;
                    CollectionViewSource.GetDefaultView(Class_IDs_Listview.ItemsSource).Refresh();
                    mainWindow.UpdateLog("Classes Loaded: " + L2H_Character_Classes.Count, L2H_Constants.Color_Add);

                });
            }

            finishedLoading = true;

        }
        void HandleThreadDone(object sender, EventArgs e, string message)
        {
            Interlocked.Increment(ref numberOfThreadsCompleted);
        }

        private void Class_Include_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            Popup_Class_Selection dialog = new Popup_Class_Selection(L2H_Character_Classes, active_L2H_Character_Class);

            dialog.ShowDialog();
        }

        private void Class_Name_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Class_Name_Listview_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Class_Skills_Acquire_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Skill_Acquire skillClicked = vm as L2H_Skill_Acquire;

            if (active_L2H_Skill_Acquire != null)
            {
                if (active_L2H_Skill_Acquire == skillClicked)
                {
                    if (!active_L2H_Skill_Acquire.IsSelected)
                    {
                        Class_Specific_Details_Acquire_Skill_Details_Grid.Visibility = Visibility.Hidden;
                        active_L2H_Skill_Acquire.IsSelected = false;
                        active_L2H_Skill_Acquire = null;
                        return;
                    }
                    else
                    {
                        Class_Specific_Details_Acquire_Skill_Details_Grid.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    active_L2H_Skill_Acquire.IsSelected = false;
                    active_L2H_Skill_Acquire = skillClicked;
                    active_L2H_Skill_Acquire.IsSelected = true;
                }
            }
            else
            {
                active_L2H_Skill_Acquire = skillClicked;
                active_L2H_Skill_Acquire.IsSelected = true;
            }


            Refresh_Skill_Acquire_Details(active_L2H_Skill_Acquire);
        }



        void Refresh_Skill_Acquire_Details(L2H_Skill_Acquire skill)
        {


            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Class_Specific_Details_Acquire_Skill_Details_Grid))
            {
                tb.DataContext = skill;
            }
            foreach (ToggleButton tb in L2H_Parser.FindVisualChildren<ToggleButton>(Class_Specific_Details_Acquire_Skill_Details_Grid))
            {
                tb.DataContext = skill;
            }
            foreach (ComboBox cb in L2H_Parser.FindVisualChildren<ComboBox>(Class_Specific_Details_Acquire_Skill_Details_Grid))
            {
                cb.DataContext = skill;
            }

            Class_Specific_Details_Acquire_Skill_Details_Grid.Visibility = Visibility.Visible;


            CollectionViewSource.GetDefaultView(Class_Skills_Acquire_Listview.ItemsSource).Refresh();

        }

        private void ToggleSearchVisibility(object sender, RoutedEventArgs e)
        {
            if (searchIsShowing)
            {
                Classes_Main_Grid.ColumnDefinitions[0].Width = new GridLength(0);
                Classes_Main_Grid.ColumnDefinitions[1].Width = new GridLength(0);
                searchIsShowing = false;
                SearchIsShowingButton.Content = "Show Search";
            }
            else
            {
                Classes_Main_Grid.ColumnDefinitions[0].Width = new GridLength(220);
                Classes_Main_Grid.ColumnDefinitions[1].Width = new GridLength(220);
                searchIsShowing = true;
                SearchIsShowingButton.Content = "Hide Search";
            }
        }

        private void Class_ID_Click(object sender, RoutedEventArgs e)
        {
            ClearFocus();

            if (active_L2H_Character_Class != null)
            {
                active_L2H_Character_Class.IsSelected = false;

            }

            if (active_L2H_Character_Class == (sender as FrameworkElement).DataContext as L2H_Character_Class)
            {
                active_L2H_Character_Class = null;
                Class_Skills_Acquire_Listview.ItemsSource = null;
                Class_Image.DataContext = null;
                Class_Name.DataContext = null;
                CollectionViewSource.GetDefaultView(Class_IDs_Listview.ItemsSource).Refresh();
                return;
            }

            active_L2H_Character_Class = (sender as FrameworkElement).DataContext as L2H_Character_Class;

            if (active_L2H_Character_Class != null)
                Class_Specific_Details_Grid.Visibility = Visibility.Visible;

            Class_Specific_Details_Acquire_Skill_Details_Grid.Visibility = Visibility.Hidden;
            Class_Skills_Acquire_Listview.ItemsSource = active_L2H_Character_Class.L2H_Skill_Acquires;
            Refresh_Skill_Acquire_Data();

            Class_Image.DataContext = active_L2H_Character_Class;
            Class_Name.DataContext = active_L2H_Character_Class;

            CollectionViewSource.GetDefaultView(Class_IDs_Listview.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(Class_Skills_Acquire_Listview.ItemsSource).Refresh();
        }
        public void Refresh_Skill_Acquire_Data()
        {
            if (active_L2H_Character_Class.L2H_Skill_Acquires != null)
                if (active_L2H_Character_Class.L2H_Skill_Acquires.Count > 0)
                    if (!string.IsNullOrEmpty(active_L2H_Character_Class.L2H_Skill_Acquires[0].server_Skillacquire.includeClass))
                        Class_Specific_Details_Include_Class_Button.Content = active_L2H_Character_Class.L2H_Skill_Acquires[0].server_Skillacquire.includeClass.Replace("include_", "");
                    else
                        Class_Specific_Details_Include_Class_Button.Content = "Add Include Class";

            CollectionViewSource.GetDefaultView(Class_Skills_Acquire_Listview.ItemsSource).Refresh();

        }
        private void Class_Filter_ID_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Class_IDs_Listview.ItemsSource).Refresh();
        }

        private void Class_Filter_Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Class_Property_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            string s = "";
            if (((sender as Button).Content as TextBlock) != null)
                s = ((sender as Button).Content as TextBlock).Text;
            else
                s = (sender as Button).Content.ToString();

            switch (s)
            {
                case "Minimum Stats":
                    {
                        Popup_Class_Stats dialog = new Popup_Class_Stats(serverSetting.minimum_stat, "Minimum Stats");
                        dialog.Show();
                    }
                    break;
                case "Maximum Stats":
                    {
                        Popup_Class_Stats dialog = new Popup_Class_Stats(serverSetting.maximum_stat, "Maximum Stats");
                        dialog.Show();
                    }
                    break;
                case "Recommended Stats":
                    {
                        Popup_Class_Stats dialog = new Popup_Class_Stats(serverSetting.recommended_stat, "Recommended Stats");
                        dialog.Show();
                    }
                    break;
                case "HP Table":
                    {
                        if (active_L2H_Character_Class == null)
                            return;
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.hp_tables.Find(x => x.classID == active_L2H_Character_Class.classID));
                        dialog.ShowDialog();
                    }
                    break;
                case "MP Table":
                    {
                        if (active_L2H_Character_Class == null)
                            return;
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.mp_tables.Find(x => x.classID == active_L2H_Character_Class.classID));
                        dialog.ShowDialog();
                    }
                    break;
                case "CP Table":
                    {
                        if (active_L2H_Character_Class == null)
                            return;
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.cp_tables.Find(x => x.classID == active_L2H_Character_Class.classID));
                        dialog.ShowDialog();
                    }
                    break;
                case "Physical Attack":
                    {
                        Popup_Class_Single_Value dialog = new Popup_Class_Single_Value(pc_Parameters.base_physical_attack);
                        dialog.ShowDialog();
                    }
                    break;
                case "Attack Speed":
                    {
                        Popup_Class_Single_Value dialog = new Popup_Class_Single_Value(pc_Parameters.base_attack_speed);
                        dialog.ShowDialog();
                    }
                    break;
                case "Magic Attack":
                    {
                        Popup_Class_Single_Value dialog = new Popup_Class_Single_Value(pc_Parameters.base_magic_attack);
                        dialog.ShowDialog();
                    }
                    break;
                case "Critical":
                    {
                        Popup_Class_Single_Value dialog = new Popup_Class_Single_Value(pc_Parameters.base_critical);
                        dialog.ShowDialog();
                    }
                    break;
                case "Attack Type":
                    {
                        Popup_Class_Single_Value dialog = new Popup_Class_Single_Value(pc_Parameters.base_attack_type);
                        dialog.ShowDialog();
                    }
                    break;
                case "Attack Range":
                    {
                        Popup_Class_Single_Value dialog = new Popup_Class_Single_Value(pc_Parameters.base_attack_range);
                        dialog.ShowDialog();
                    }
                    break;
                case "Damage Range":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.base_damage_range, 0);
                        dialog.ShowDialog();
                    }
                    break;
                case "Rand Damage":
                    {
                        Popup_Class_Single_Value dialog = new Popup_Class_Single_Value(pc_Parameters.base_rand_dam);
                        dialog.ShowDialog();
                    }
                    break;
                case "Physical Defense":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.base_defend, 0);
                        dialog.ShowDialog();
                    }
                    break;
                case "Magic Defense":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.base_magic_defend, 0);
                        dialog.ShowDialog();
                    }
                    break;
                case "HP Regen":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.org_hp_regen, 0);
                        dialog.ShowDialog();
                    }
                    break;
                case "MP Regen":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.org_mp_regen, 0);
                        dialog.ShowDialog();
                    }
                    break;
                case "CP Regen":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.org_cp_regen, 0);
                        dialog.ShowDialog();
                    }
                    break;
                case "Move Speed":
                    {
                        Popup_Class_Movement_Speed dialog = new Popup_Class_Movement_Speed(pc_Parameters.moving_speed);
                        dialog.ShowDialog();
                    }
                    break;

                case "Strength Bonus":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.str_bonus);
                        dialog.ShowDialog();
                    }
                    break;
                case "Intelligence Bonus":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.int_bonus);
                        dialog.ShowDialog();
                    }
                    break;
                case "Constitution Bonus":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.con_bonus);
                        dialog.ShowDialog();
                    }
                    break;
                case "Men Bonus":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.men_bonus);
                        dialog.ShowDialog();
                    }
                    break;
                case "Dexterity Bonus":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.dex_bonus);
                        dialog.ShowDialog();
                    }
                    break;
                case "Wit Bonus":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.wit_bonus);
                        dialog.ShowDialog();
                    }
                    break;
                case "Level Bonus":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.level_bonus);
                        dialog.ShowDialog();
                    }
                    break;

                case "Jump":
                    {
                        Popup_Class_Single_Value dialog = new Popup_Class_Single_Value(pc_Parameters.org_jump);
                        dialog.ShowDialog();
                    }
                    break;
                case "Breath Bonus":
                    {
                        Popup_Class_Single_Value dialog = new Popup_Class_Single_Value(pc_Parameters.pc_breath_bonus_table);
                        dialog.ShowDialog();
                    }
                    break;
                case "Safe Fall Height":
                    {
                        Popup_Class_Single_Value dialog = new Popup_Class_Single_Value(pc_Parameters.pc_safe_fall_height_table);
                        dialog.ShowDialog();
                    }
                    break;

                case "Can Penetrate":
                    {
                        Popup_Class_Single_Value dialog = new Popup_Class_Single_Value(pc_Parameters.base_can_penetrate);
                        dialog.ShowDialog();
                    }
                    break;

                case "Character Start Equipment":
                    {
                        Popup_Class_Start_Equipment dialog = new Popup_Class_Start_Equipment(serverSetting.initialEquipment, serverSetting.initialCustomEquipment, (((MainWindow)Application.Current.MainWindow).GetPageOfType(typeof(ItemsPage)) as ItemsPage).L2H_Items);
                        dialog.ShowDialog();
                    }
                    break;
                case "Character Creation Lobby Equipment":
                    {
                        Popup_Class_Create_Equipment dialog = new Popup_Class_Create_Equipment(client_Charcreates, (((MainWindow)Application.Current.MainWindow).GetPageOfType(typeof(ItemsPage)) as ItemsPage).L2H_Items);
                        dialog.ShowDialog();
                    }
                    break;
                case "Character Creation Description":
                    {
                        Popup_Class_Description dialog = new Popup_Class_Description(client_Classinfos);
                        dialog.ShowDialog();
                    }
                    break;
                case "HP Regen Weight":
                    {
                        Popup_Class_Weight_Parameters dialog = new Popup_Class_Weight_Parameters(pc_Parameters.org_hp_regen_weight);
                        dialog.ShowDialog();
                    }
                    break;
                case "MP Regen Weight":
                    {
                        Popup_Class_Weight_Parameters dialog = new Popup_Class_Weight_Parameters(pc_Parameters.org_mp_regen_weight);
                        dialog.ShowDialog();
                    }
                    break;
                case "CP Regen Weight":
                    {
                        Popup_Class_Weight_Parameters dialog = new Popup_Class_Weight_Parameters(pc_Parameters.org_cp_regen_weight);
                        dialog.ShowDialog();
                    }
                    break;
                case "Karma Increase Table":
                    {
                        Popup_Character_Table dialog = new Popup_Character_Table(pc_Parameters.pc_karma_increase_table);
                        dialog.ShowDialog();
                    }
                    break;
                case "Karma Increase Constant":
                    {
                        Popup_Class_Karma_Constant dialog = new Popup_Class_Karma_Constant(pc_Parameters.pc_karma_increase_constant);
                        dialog.ShowDialog();
                    }
                    break;
                default:
                    break;

                    //Unused atm
                    //case "Collision Box":
                    //    {
                    //        BaseClassesSingleValuesManyBoxesEdit dialog = new BaseClassesSingleValuesManyBoxesEdit("Collision Box", pc_parameters.pc_collision_box_table);
                    //        DialogResult dr = dialog.ShowDialog(this);
                    //    }
                    //    break;
                    //case "Starting Point":
                    //    {
                    //        List<string> dataStrings = new List<string>();

                    //        for (int i = 0; i < serverSetting.initialStartPoints.Count; i++)
                    //        {
                    //            dataStrings.AddRange(serverSetting.initialStartPoints[i].GetExportStrings());

                    //        }

                    //        BaseClassesTextBoxMultipleLinesEdit dialog = new BaseClassesTextBoxMultipleLinesEdit(dataStrings, serverSetting);
                    //        DialogResult dr = dialog.ShowDialog(this);
                    //    }
                    //    break;
                    //case "Character Creation Position":
                    //    {
                    //        BaseClassesSelectClass dialog = new BaseClassesSelectClass(charcreategrps, "Character Creation Position", this, true);
                    //        DialogResult dr = dialog.ShowDialog(this);
                    //        //charcreategrp
                    //    }
                    //    break;
            }

        }


        private void Classes_Type_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            if ((sender as ToggleButton) != active_Class_Type)
            {
                active_Class_Type.IsChecked = false;
                active_Class_Type = (sender as ToggleButton);
            }
            else
                active_Class_Type.IsChecked = true;

            switch ((sender as FrameworkElement).Tag)
            {
                case "base":
                    Class_Specific_Details_Grid.Visibility = Visibility.Hidden;
                    Class_Base_Details_Grid.Visibility = Visibility.Visible;
                    Classes_Main_Grid.ColumnDefinitions[1].Width = new GridLength(0);
                    SearchIsShowingButton.Visibility = Visibility.Hidden;
                    break;
                case "specific":
                    Class_Base_Details_Grid.Visibility = Visibility.Hidden;
                    if (active_L2H_Character_Class != null)
                        Class_Specific_Details_Grid.Visibility = Visibility.Visible;
                    else
                        Class_Specific_Details_Grid.Visibility = Visibility.Hidden;
                    Classes_Main_Grid.ColumnDefinitions[1].Width = new GridLength(220);
                    SearchIsShowingButton.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private bool Filter_Class_Name(object item)
        {
            L2H_Character_Class filteredObject = item as L2H_Character_Class;

            if (filteredObject == null)
                return false;

            if (!string.IsNullOrEmpty(Class_Filter_ID.Text))
            {
                return (filteredObject.classID.IndexOf(Class_Filter_ID.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return true;

        }

        private void Add_Acquire_Skill_Button_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            Popup_Skill_Selection dialog = new Popup_Skill_Selection((((MainWindow)Application.Current.MainWindow).GetPageOfType(typeof(Pages.SkillsPage)) as SkillsPage).Get_L2H_Skills);
            dialog.Initialize_For_Skillacquire(active_L2H_Character_Class);
            dialog.Show();
        }

        private void Class_Skills_Acquire_Remove_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            L2H_Skill_Acquire sa = (sender as FrameworkElement).DataContext as L2H_Skill_Acquire;

            if (sa != null)
            {
                Popup_Confirmation_Only_Text dialog = new Popup_Confirmation_Only_Text();
                dialog.Confirmation_Action += (b, bb) => active_L2H_Character_Class.L2H_Skill_Acquires.Remove(sa);
                dialog.InitializeConfirmation("Delete Skill Acquire: " + sa.L2H_Skill.Skill_Name + "\nFrom Class ID: " + active_L2H_Character_Class.classID + "?");
            }
                ;
        }
        private void Validate_Integer_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Integer(e.Text);
        }
        private void Validate_Float_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Float(e.Text);

        }
        private void Validate_Lower_Case_And_Symbols_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Lower_Case_And_Symbols(e.Text);
        }

        private void TextBox_No_Spaces_Allowed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void Skill_Acquire_Go_To_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();

            if (active_L2H_Skill_Acquire != null)
            {
                (mainWindow.GetPageOfType(typeof(SkillsPage)) as SkillsPage).Force_Skill_Selection(active_L2H_Skill_Acquire.L2H_Skill);
                mainWindow.Category_Force(MenuCategories.Skills);
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

    public class ThreadWorker_Hook_Skills_To_SkillAcquires
    {
        public event EventHandler ThreadDone;

        public int startIndex;
        public int endIndex;
        public List<L2H_Skill> L2H_Skills;
        public List<L2H_Skill_Acquire> L2H_Skill_Acquires;

        public ThreadWorker_Hook_Skills_To_SkillAcquires()
        {

        }

        public void ThreadProc()
        {

            if (endIndex > L2H_Skill_Acquires.Count - 1)
                endIndex = L2H_Skill_Acquires.Count;

            for (int i = startIndex; i < endIndex; i++)
            {
                L2H_Skill_Acquire targetSkillAcquire = L2H_Skill_Acquires[i];

                targetSkillAcquire.L2H_Skill = L2H_Skills.Find(x => x.Skill_Name_ID == targetSkillAcquire.Skill_Name_ID);

            }

            if (ThreadDone != null)
            {
                ThreadDone(this, EventArgs.Empty);
            }
        }
    }
}
