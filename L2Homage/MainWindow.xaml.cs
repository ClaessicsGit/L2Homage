//L2Homage - By Bumble (Claessic)

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace L2Homage
{
    public enum LoadedTypes
    {
        Empty,
        Weapons, Armors, Etcs, Sets, Recipes, NPCs, Droplists,
        Domains, Spawn_Areas, NPC_Makers, NPC_Ex_Makers, NPC_Begins, NPC_Ex_Begins, Domains_Custom, Spawn_Areas_Custom, NPC_Makers_Custom, NPC_Ex_Makers_Custom, NPC_Begins_Custom, NPC_Ex_Begins_Custom,
        Skills, Enchanted_Skills,
        Chat_Filters, Gametips, NPC_Strings, System_Messages, System_Strings, Huntingzones, Zonenames, Raids, Multisell
    }
    public enum DroplistType { single, multi }
    public enum DroplistSlot { normal, spoil, multi, extra }

    enum LoadingPhases { None, Phase_1, Phase_2, Phase_3, Phase_4 }
    public enum SkillType { normal, enchanted, sound, mobskillanim };
    public enum NPCTypes { none, warrior, citizen, treasure, boss, zzoldagu, world_trap, collection, merchant, warehouse_keeper, teleporter, guild_master, guild_coach, guard, blacksmith, mrkeeper, monrace, package_keeper, holything, siege_attacker, ownthing, summon, pet, xmastree, pc_trap, doppelganger }
    public enum MenuCategories { Overview, Items, Recipes, Droplists, NPCs, Spawns, Skills }
    public enum LogAction { Modify, Clone, Add, Delete }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Page> pages;
        public Client_L2ini client_L2Ini;

        MenuItem activeMenuItem;
        DateTime startLoadTime;

        bool decryptFailed = false;
        bool decryptMessageShown = false;

        public bool finishedLoading = false;

        public MainWindow()
        {
            InitializeComponent();

            pages = new List<Page>();
            HeaderIcon.Source = BitmapFrame.Create(new Uri("pack://application:,,,/Images/icon.png", UriKind.RelativeOrAbsolute));
            HeaderTitle.Text = "L2 Homage H5 Edition";

            //Initializing Pages
            var instantiatedPage = Activator.CreateInstance(typeof(Pages.OverviewPage));
            pages.Add(instantiatedPage as Page);
            instantiatedPage = Activator.CreateInstance(typeof(Pages.ItemsPage));
            pages.Add(instantiatedPage as Page);
            instantiatedPage = Activator.CreateInstance(typeof(Pages.RecipesPage));
            pages.Add(instantiatedPage as Page);
            instantiatedPage = Activator.CreateInstance(typeof(Pages.DroplistsPage));
            pages.Add(instantiatedPage as Page);
            instantiatedPage = Activator.CreateInstance(typeof(Pages.ExpPage));
            pages.Add(instantiatedPage as Page);
            instantiatedPage = Activator.CreateInstance(typeof(Pages.NPCsPage));
            pages.Add(instantiatedPage as Page);
            instantiatedPage = Activator.CreateInstance(typeof(Pages.SpawnsPage));
            pages.Add(instantiatedPage as Page);
            instantiatedPage = Activator.CreateInstance(typeof(Pages.SkillsPage));
            pages.Add(instantiatedPage as Page);
            instantiatedPage = Activator.CreateInstance(typeof(Pages.ClassesPage));
            pages.Add(instantiatedPage as Page);
            instantiatedPage = Activator.CreateInstance(typeof(Pages.MultisellPage));
            pages.Add(instantiatedPage as Page);
            instantiatedPage = Activator.CreateInstance(typeof(Pages.SystemTextPage));
            pages.Add(instantiatedPage as Page);

            //MultisellPage

            PageContentFrame.Content = pages.Find(x => x.GetType() == (typeof(Pages.OverviewPage)));
            activeMenuItem = Overview_Menu_Item_Overview;
            activeMenuItem.IsChecked = true;

            ClearMiniLog();



        }

        #region ResizeWindows
        bool ResizeInProcess = false;
        private void Resize_Init(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = true;
                senderRect.CaptureMouse();
            }
        }

        private void Resize_End(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = false; ;
                senderRect.ReleaseMouseCapture();
            }
        }

        private void Resizeing_Form(object sender, MouseEventArgs e)
        {
            if (ResizeInProcess)
            {
                Rectangle senderRect = sender as Rectangle;
                Window mainWindow = senderRect.Tag as Window;
                if (senderRect != null)
                {
                    double width = e.GetPosition(mainWindow).X;
                    double height = e.GetPosition(mainWindow).Y;
                    senderRect.CaptureMouse();
                    if (senderRect.Name.ToLower().Contains("right"))
                    {
                        width += 5;
                        if (width > 0)
                            mainWindow.Width = width;
                    }
                    if (senderRect.Name.ToLower().Contains("left"))
                    {
                        width -= 5;
                        mainWindow.Left += width;
                        width = mainWindow.Width - width;
                        if (width > 0)
                        {
                            mainWindow.Width = width;
                        }
                    }
                    if (senderRect.Name.ToLower().Contains("bottom"))
                    {
                        height += 5;
                        if (height > 0)
                            mainWindow.Height = height;
                    }
                    if (senderRect.Name.ToLower().Contains("top"))
                    {
                        height -= 5;
                        mainWindow.Top += height;
                        height = mainWindow.Height - height;
                        if (height > 0)
                        {
                            mainWindow.Height = height;
                        }
                    }
                }
            }
        }
        #endregion
        #region TitleButtons
        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeClick(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2)
                {
                    AdjustWindowSize();
                }
                else
                {
                    App.Current.MainWindow.DragMove();
                }
            }
        }

        private void AdjustWindowSize()
        {
            if (App.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                App.Current.MainWindow.WindowState = WindowState.Normal;
                MaximizeButton.Content = "";
            }
            else if (App.Current.MainWindow.WindowState == WindowState.Normal)
            {
                App.Current.MainWindow.WindowState = WindowState.Maximized;
                MaximizeButton.Content = "";
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }
        #endregion

        private void Category_Click(object sender, RoutedEventArgs e)
        {
            Type pageType = ((MenuItem)sender).Tag as Type;

            if (activeMenuItem != null)
            {
                if (activeMenuItem == (MenuItem)sender)
                {
                    return;
                }
                else
                {
                    activeMenuItem.IsChecked = false;
                }
            }

            activeMenuItem = (MenuItem)sender;
            activeMenuItem.IsChecked = true;



            if (pages.Exists(x => x.GetType() == pageType))
            {
                PageContentFrame.Content = pages.Find(x => x.GetType() == pageType);
            }
            else
            {
                var pg = Activator.CreateInstance(pageType);
                PageContentFrame.Content = pg;
                pages.Add(pg as Page);
            }

        }

        public void Category_Force(MenuCategories menuCategory)
        {
            MenuItem targetMenuItem;

            switch (menuCategory)
            {
                case MenuCategories.Overview:
                    targetMenuItem = Overview_Menu_Item_Overview;
                    break;
                case MenuCategories.Items:
                    targetMenuItem = Overview_Menu_Item_Items;
                    break;
                case MenuCategories.Recipes:
                    targetMenuItem = Overview_Menu_Item_Recipes;
                    break;
                case MenuCategories.Droplists:
                    targetMenuItem = Overview_Menu_Item_Droplists;
                    break;
                case MenuCategories.NPCs:
                    targetMenuItem = Overview_Menu_Item_NPCs;
                    break;
                case MenuCategories.Spawns:
                    targetMenuItem = Overview_Menu_Item_Spawns;
                    break;
                case MenuCategories.Skills:
                    targetMenuItem = Overview_Menu_Item_Skills;
                    break;
                default:
                    targetMenuItem = null;
                    break;
            }


            targetMenuItem.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));

        }

        public void Initialize()
        {

            UpdateLog("Initializing Loading..");
            startLoadTime = DateTime.Now;

            Decrypt_Client_Files();

            if (decryptFailed)
            {
                MessageBox.Show("Decryption failed, cannot continue.");
                Application.Current.Shutdown();
                return;
            }

            StartLoadingPhase_1();


        }



        private void LoadL2ini()
        {
            List<string> l2iniData = new List<string>();
            if (!File.Exists(L2H_Constants.client_L2ini_Path))
            {
                MessageBox.Show("Couldn't find l2.ini in decrypted client files folder :(");
            }
            else
            {
                using (TextReader textReader = new StreamReader(L2H_Constants.client_L2ini_Path, Encoding.GetEncoding(65001)))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        l2iniData.Add(line);
                    }

                }

                client_L2Ini = new Client_L2ini(l2iniData.ToArray());
            }

        }

        private void Decrypt_Client_Files()
        {
            if (!File.Exists(L2H_Constants.client_Armors_Path))
            {
                DecFile("armorgrp");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Armors_Path);
            }
            if (!File.Exists(L2H_Constants.client_Etcs_Path))
            {
                DecFile("etcitemgrp");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Etcs_Path);
            }
            if (!File.Exists(L2H_Constants.client_Itemnames_Path))
            {
                DecFile("itemname-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Itemnames_Path);
            }
            if (!File.Exists(L2H_Constants.client_Mobskillanimgrp_Path))
            {
                DecFile("mobskillanimgrp");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Mobskillanimgrp_Path);
            }
            if (!File.Exists(L2H_Constants.client_NPCnames_Path))
            {
                DecFile("npcname-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_NPCnames_Path);
            }
            if (!File.Exists(L2H_Constants.client_NPCs_Path))
            {
                DecFile("npcgrp");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_NPCs_Path);
            }
            if (!File.Exists(L2H_Constants.client_NPCstrings_Path))
            {
                DecFile("npcstring-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_NPCstrings_Path);
            }
            if (!File.Exists(L2H_Constants.client_Recipes_Path))
            {
                DecFile("recipe-c");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Recipes_Path);
            }
            if (!File.Exists(L2H_Constants.client_Skillnames_Path))
            {
                DecFile("skillname-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Skillnames_Path);
            }
            if (!File.Exists(L2H_Constants.client_Skillsounds_Path))
            {
                DecFile("skillsoundgrp");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Skillsounds_Path);
            }
            if (!File.Exists(L2H_Constants.client_Skills_Path))
            {
                DecFile("skillgrp");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Skills_Path);
            }
            if (!File.Exists(L2H_Constants.client_Weapons_Path))
            {
                DecFile("weapongrp");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Weapons_Path);
            }
            if (!File.Exists(L2H_Constants.client_Charcreategrp_Path))
            {
                DecFile("charcreategrp");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Charcreategrp_Path);
            }
            if (!File.Exists(L2H_Constants.client_Classinfo_Path))
            {
                DecFile("classinfo-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Classinfo_Path);
            }
            if (!File.Exists(L2H_Constants.client_Eula_Path))
            {
                DecFile("eula-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Eula_Path);
            }
            if (!File.Exists(L2H_Constants.client_Gametip_Path))
            {
                DecFile("gametip-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Gametip_Path);
            }
            if (!File.Exists(L2H_Constants.client_Huntingzone_Path))
            {
                DecFile("huntingzone-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Huntingzone_Path);
            }
            if (!File.Exists(L2H_Constants.client_Instantzonedata_Path))
            {
                DecFile("instantzonedata-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Instantzonedata_Path);
            }
            if (!File.Exists(L2H_Constants.client_L2ini_Path))
            {
                DecFile("l2ini");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_L2ini_Path);
            }
            if (!File.Exists(L2H_Constants.client_Obscene_Path))
            {
                DecFile("obscene-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Obscene_Path);
            }
            if (!File.Exists(L2H_Constants.client_Questname_Path))
            {
                DecFile("questname-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Questname_Path);
            }
            if (!File.Exists(L2H_Constants.client_Raiddata_Path))
            {
                DecFile("raiddata-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Raiddata_Path);
            }
            if (!File.Exists(L2H_Constants.client_Sysstring_Path))
            {
                DecFile("sysstring-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Sysstring_Path);
            }
            if (!File.Exists(L2H_Constants.client_Systemmsg_Path))
            {
                DecFile("systemmsg-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Systemmsg_Path);
            }
            if (!File.Exists(L2H_Constants.client_Zonename_Path))
            {
                DecFile("zonename-e");
                CheckIfDecryptionWasSuccessful(L2H_Constants.client_Zonename_Path);
            }

        }

        void CheckIfDecryptionWasSuccessful(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("Decryption Failed for " + path + "\n\nCannot Continue");
                Environment.Exit(0);
            }
            else
            {
                UpdateLog("Decryption Successful: " + path, L2H_Constants.Color_Add);
            }
        }

        private void StartLoadingPhase_1()
        {
            UpdateLog("Loading L2.ini..", L2H_Constants.Color_Log_Thread_Begin);
            LoadL2ini();
            UpdateLog("L2.ini loaded", L2H_Constants.Color_Add);

            UpdateLog("Initializing Thread: Item Loaders..", L2H_Constants.Color_Log_Thread_Begin);
            ThreadWorker ThreadWorker_LoadItems = new ThreadWorker();
            ThreadWorker_LoadItems.Job += (sender, e) => LoadItems(sender, e);
            ThreadWorker_LoadItems.ThreadDone += (sender, e) => HandleThreadDone(sender, e, "", LoadingPhases.Phase_2);
            Thread thread_LoadItems = new Thread(ThreadWorker_LoadItems.ThreadProc);


            UpdateLog("Initializing Thread: Skill Loaders..", L2H_Constants.Color_Log_Thread_Begin);
            ThreadWorker ThreadWorker_LoadSkills = new ThreadWorker();
            ThreadWorker_LoadSkills.Job += (sender, e) => LoadSkills(sender, e);
            ThreadWorker_LoadSkills.ThreadDone += (sender, e) => HandleThreadDone(sender, e, "");
            Thread thread_LoadSkills = new Thread(ThreadWorker_LoadSkills.ThreadProc);

            thread_LoadItems.Start();
            thread_LoadSkills.Start();

            (pages.Find(x => x.GetType() == typeof(Pages.SystemTextPage)) as Pages.SystemTextPage).LoadSystemTexts();
        }

        private void StartLoadingPhase_2()
        {
            SpinWait.SpinUntil(() => (pages.Find(x => x.GetType() == typeof(Pages.ItemsPage)) as Pages.ItemsPage).AllItemsAreLoaded, Timeout.Infinite);
            if ((pages.Find(x => x.GetType() == typeof(Pages.DroplistsPage)) as Pages.DroplistsPage).existingDroplistsFound)
            {
                SpinWait.SpinUntil(() => (pages.Find(x => x.GetType() == typeof(Pages.DroplistsPage)) as Pages.DroplistsPage).existing_Single_Droplists_Loaded, Timeout.Infinite);
                SpinWait.SpinUntil(() => (pages.Find(x => x.GetType() == typeof(Pages.DroplistsPage)) as Pages.DroplistsPage).existing_Multi_Droplists_Loaded, Timeout.Infinite);
            }

            (pages.Find(x => x.GetType() == typeof(Pages.ItemsPage)) as Pages.ItemsPage).LoadSets();

            UpdateLog("Initializing Thread: Recipe Loaders..", L2H_Constants.Color_Log_Thread_Begin);
            ThreadWorker ThreadWorker_LoadRecipes = new ThreadWorker();
            ThreadWorker_LoadRecipes.Job += (sender, e) => LoadRecipes(sender, e);
            ThreadWorker_LoadRecipes.ThreadDone += (sender, e) => HandleThreadDone(sender, e, "");
            Thread thread_LoadRecipes = new Thread(ThreadWorker_LoadRecipes.ThreadProc);

            thread_LoadRecipes.Start();


            UpdateLog("Initializing Thread: NPC Loaders..", L2H_Constants.Color_Log_Thread_Begin);
            ThreadWorker ThreadWorker_LoadNpcs = new ThreadWorker();
            ThreadWorker_LoadNpcs.Job += (sender, e) => LoadNpcs(sender, e);
            ThreadWorker_LoadNpcs.ThreadDone += (sender, e) => HandleThreadDone(sender, e, "", LoadingPhases.Phase_3);
            Thread thread_LoadNpcs = new Thread(ThreadWorker_LoadNpcs.ThreadProc);

            thread_LoadNpcs.Start();
        }

        private void StartLoadingPhase_3()
        {
            SpinWait.SpinUntil(() => (pages.Find(x => x.GetType() == typeof(Pages.NPCsPage)) as Pages.NPCsPage).allNPCsLoaded, Timeout.Infinite);

            UpdateLog("Initializing Thread: Droplist Loaders..", L2H_Constants.Color_Log_Thread_Begin);
            ThreadWorker ThreadWorker_LoadDroplists = new ThreadWorker();
            ThreadWorker_LoadDroplists.Job += (sender, e) => LoadDroplists(sender, e);
            ThreadWorker_LoadDroplists.ThreadDone += (sender, e) => HandleThreadDone(sender, e, "");
            Thread thread_LoadDroplists = new Thread(ThreadWorker_LoadDroplists.ThreadProc);
            thread_LoadDroplists.Start();

            UpdateLog("Initializing Thread: Spawn Loaders..", L2H_Constants.Color_Log_Thread_Begin);
            ThreadWorker ThreadWorker_LoadSpawns = new ThreadWorker();
            ThreadWorker_LoadSpawns.Job += (sender, e) => LoadSpawns(sender, e);
            ThreadWorker_LoadSpawns.ThreadDone += (sender, e) => HandleThreadDone(sender, e, "");
            Thread thread_LoadSpawns = new Thread(ThreadWorker_LoadSpawns.ThreadProc);
            thread_LoadSpawns.Start();

            SpinWait.SpinUntil(() => (pages.Find(x => x.GetType() == typeof(Pages.DroplistsPage)) as Pages.DroplistsPage).droplistsFullyLoaded, Timeout.Infinite);
            SpinWait.SpinUntil(() => (pages.Find(x => x.GetType() == typeof(Pages.RecipesPage)) as Pages.RecipesPage).recipesLoaded, Timeout.Infinite);

            UpdateLog("Initializing Thread: Skill Acquires and Server Settings..", L2H_Constants.Color_Log_Thread_Begin);
            ThreadWorker ThreadWorker_LoadSkillAcquireAndServerSettings = new ThreadWorker();
            ThreadWorker_LoadSkillAcquireAndServerSettings.Job += (sender, e) => Load_Classes_ServerSettings_SkillAcquire(sender, e);
            ThreadWorker_LoadSkillAcquireAndServerSettings.ThreadDone += (sender, e) => HandleThreadDone(sender, e, "");
            Thread thread_LoadSkillAcquireAndServerSettings = new Thread(ThreadWorker_LoadSkillAcquireAndServerSettings.ThreadProc);
            thread_LoadSkillAcquireAndServerSettings.Start();

            ////Multisells loading..
            UpdateLog("Initializing Thread: Multisells..", L2H_Constants.Color_Log_Thread_Begin);
            ThreadWorker ThreadWorker_LoadMultisells = new ThreadWorker();
            ThreadWorker_LoadMultisells.Job += (sender, e) => LoadMultisells(sender, e);
            ThreadWorker_LoadMultisells.ThreadDone += (sender, e) => HandleThreadDone(sender, e, "");
            Thread thread_LoadMultisells = new Thread(ThreadWorker_LoadMultisells.ThreadProc);
            thread_LoadMultisells.Start();


            SpinWait.SpinUntil(() => (pages.Find(x => x.GetType() == typeof(Pages.ClassesPage)) as Pages.ClassesPage).finishedLoading, Timeout.Infinite);
            SpinWait.SpinUntil(() => (pages.Find(x => x.GetType() == typeof(Pages.MultisellPage)) as Pages.MultisellPage).multisellsLoaded, Timeout.Infinite);

            Dispatcher.Invoke(() =>
            {
                TimeSpan loadingTime = DateTime.Now - startLoadTime;
                UpdateLog("Loading Complete: " + loadingTime.TotalSeconds.ToString("N2"), L2H_Constants.Color_Modify);
                finishedLoading = true;
            });


        }


        private void LoadItems(object sender, EventArgs e)
        {
            (pages.Find(x => x.GetType() == typeof(Pages.ItemsPage)) as Pages.ItemsPage).LoadItems();
        }

        private void LoadSkills(object sender, EventArgs e)
        {
            (pages.Find(x => x.GetType() == typeof(Pages.SkillsPage)) as Pages.SkillsPage).LoadSkills();
        }

        private void LoadRecipes(object sender, EventArgs e)
        {
            (pages.Find(x => x.GetType() == typeof(Pages.RecipesPage)) as Pages.RecipesPage).LoadRecipes();
        }
        private void LoadDroplists(object sender, EventArgs e)
        {
            (pages.Find(x => x.GetType() == typeof(Pages.DroplistsPage)) as Pages.DroplistsPage).LoadDroplists();
        }
        private void HookDroplists(object sender, EventArgs e)
        {

        }

        private void LoadNpcs(object sender, EventArgs e)
        {
            (pages.Find(x => x.GetType() == typeof(Pages.NPCsPage)) as Pages.NPCsPage).LoadNpcs();
        }
        private void LoadSpawns(object sender, EventArgs e)
        {
            (pages.Find(x => x.GetType() == typeof(Pages.SpawnsPage)) as Pages.SpawnsPage).LoadSpawns();
        }
        private void Load_Classes_ServerSettings_SkillAcquire(object sender, EventArgs e)
        {
            (pages.Find(x => x.GetType() == typeof(Pages.ClassesPage)) as Pages.ClassesPage).Load_Classes_ServerSettings_SkillAcquire();
        }
        private void LoadMultisells(object sender, EventArgs e)
        {
            (pages.Find(x => x.GetType() == typeof(Pages.MultisellPage)) as Pages.MultisellPage).LoadMultisells();
        }


        public Page GetPageOfType(Type type)
        {
            try
            {
                return pages.Find(x => x.GetType() == type);
            }
            catch
            {
                MessageBox.Show("Page not found");
                return null;
            }
        }

        public void UpdateLog(string content, string color = null)
        {
            Dispatcher.Invoke(() =>
            { (pages.Find(x => x.GetType() == typeof(Pages.OverviewPage)) as Pages.OverviewPage).UpdateLogSimple(content, color); });

        }

        public void UpdateLogAdvanced(Paragraph paragraph)
        {
            Dispatcher.Invoke(() =>
            { (pages.Find(x => x.GetType() == typeof(Pages.OverviewPage)) as Pages.OverviewPage).UpdateLogAdvanced(paragraph); });
        }

        public void UpdateMiniLog(LogAction action, string actionText, string target, string property = "", string value = "")
        {
            switch (action)
            {
                case LogAction.Modify:
                    Mini_Log_Action.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(L2H_Constants.Color_Modify));
                    Mini_Log_Action.Text = actionText;
                    break;
                case LogAction.Clone:
                    Mini_Log_Action.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(L2H_Constants.Color_Add));
                    Mini_Log_Action.Text = actionText;
                    break;
                case LogAction.Add:
                    Mini_Log_Action.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(L2H_Constants.Color_Add));
                    Mini_Log_Action.Text = actionText;
                    break;
                case LogAction.Delete:
                    Mini_Log_Action.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(L2H_Constants.Color_Remove));
                    Mini_Log_Action.Text = actionText;
                    break;
                default:
                    break;
            }

            Mini_Log_Target.Text = target;
            //Mini_Log_Property.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(L2H_Constants.Color_Category));
            Mini_Log_Property.Text = property;
            Mini_Log_Value.Text = value;
        }

        public void ClearMiniLog()
        {
            Mini_Log_Action.Text = "";
            Mini_Log_Property.Text = "";
            Mini_Log_Target.Text = "";
            Mini_Log_Value.Text = "";
        }

        public void UpdateLoadedNumber(LoadedTypes type)
        {
            (pages.Find(x => x.GetType() == typeof(Pages.OverviewPage)) as Pages.OverviewPage).UpdateLoadedNumber(type);
        }

        private void HandleThreadDone(object sender, EventArgs e, string logMessage, LoadingPhases nextLoadingPhase = LoadingPhases.None)
        {
            if (!string.IsNullOrEmpty(logMessage))
                Dispatcher.Invoke(() => { UpdateLog(logMessage, L2H_Constants.Color_Add); });

            if (nextLoadingPhase != LoadingPhases.None)
            {
                switch (nextLoadingPhase)
                {
                    case LoadingPhases.None:

                        break;
                    case LoadingPhases.Phase_1:
                        break;
                    case LoadingPhases.Phase_2:
                        Thread t2 = new Thread(StartLoadingPhase_2);
                        t2.Start();
                        break;
                    case LoadingPhases.Phase_3:
                        Thread t3 = new Thread(StartLoadingPhase_3);
                        t3.Start();
                        break;
                    default:
                        break;
                }
            }


        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.FocusedElement != null)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;
                Keyboard.ClearFocus();
                focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (e.Key == Key.Enter)
                {
                    if (Keyboard.FocusedElement != null)
                    {
                        IInputElement focusedElement = Keyboard.FocusedElement;
                        Keyboard.ClearFocus();
                        focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
                    }
                }
            }
        }

        void DecFile(string fileName)
        {
            Process p = new Process();
            if (!decryptMessageShown)
            {
                decryptMessageShown = true;
                MessageBox.Show("One or more files need to be decrypted.\nThe descryption process only needs to happen once per file.\n\nClick to start decryption.");
            }

            try
            {
                string targetDir;
                targetDir = string.Format("Tools\\mxencdec");
                string prefix = "decrypt_";

                p.StartInfo.UseShellExecute = true;
                p.StartInfo.WorkingDirectory = targetDir;
                p.StartInfo.FileName = prefix + fileName + ".bat";
                p.StartInfo.CreateNoWindow = false;
                p.Start();
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                decryptFailed = true;
                System.Windows.MessageBox.Show("Exception Occurred : " + ex.Message);

            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.BorderThickness = new System.Windows.Thickness(4);
            }
            else
            {
                this.BorderThickness = new System.Windows.Thickness(0);
            }
        }
    }

    public class ThreadWorker
    {
        public event EventHandler Job;
        public event EventHandler ThreadDone;


        public ThreadWorker()
        {

        }

        public void ThreadProc()
        {
            if (Job != null)
            {
                Job(this, EventArgs.Empty);
            }

            if (ThreadDone != null)
            {
                ThreadDone(this, EventArgs.Empty);
            }
        }
    }
}
