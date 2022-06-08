using L2Homage.Viewmodels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
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
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class NPCsPage : Page
    {

        public List<Server_Npcdata> server_Npcdata;
        public List<Client_Npc> client_Npcs;
        public List<Client_Npcname> client_Npcnames;
        public List<L2H_NPC> L2H_Npcs;
        public List<Client_Mobskillanim> client_Mobskillanims;

        public string client_NPC_Names_Startline;
        public string client_NPCs_Startline;
        public string client_Mobskillanim_Startline;
        public List<L2H_Template_Pointer> npcTemplate_Pointers;
        MainWindow mainWindow;
        ExpPage expPage;

        L2H_NPC activeNPC;
        ToggleButton activeNPCToggleButton;
        ListViewItem activeCategory;

        List<L2H_Filter_Npc_Type> activeNPCFilteringCategories;

        public ViewModel viewModel;

        public bool allNPCsLoaded = false;
        public List<L2H_Filter_Npc_Type> l2HNpcTypeFilters_Left;
        public List<L2H_Filter_Npc_Type> l2HNpcTypeFilters_Right;
        List<ToggleButton> activeToggleButtons;

        bool isLoadingDetails = false;
        bool searchIsShowing = true;
        public bool forceWaiting = false;

        public NPCsPage()
        {
            InitializeComponent();


            viewModel = new ViewModel();

            mainWindow = Application.Current.MainWindow as MainWindow;
            expPage = mainWindow.GetPageOfType(typeof(ExpPage)) as ExpPage;
            server_Npcdata = new List<Server_Npcdata>();
            client_Npcs = new List<Client_Npc>();
            client_Npcnames = new List<Client_Npcname>();
            npcTemplate_Pointers = new List<L2H_Template_Pointer>();
            client_Mobskillanims = new List<Client_Mobskillanim>();
            L2H_Npcs = new List<L2H_NPC>();
            activeNPCFilteringCategories = new List<L2H_Filter_Npc_Type>();
            l2HNpcTypeFilters_Left = new List<L2H_Filter_Npc_Type>();
            l2HNpcTypeFilters_Right = new List<L2H_Filter_Npc_Type>();
            activeToggleButtons = new List<ToggleButton>();

            NPC_Details_Grid.Visibility = Visibility.Hidden;
        }

        public void LoadNpcs()
        {
            Dispatcher.Invoke(() => { mainWindow.UpdateLog("Initializing Thread: Load NPCs..", L2H_Constants.Color_Log_Thread_Begin); });
            
            Load_NPCs_Template_Pointers();


            client_NPCs_Startline = File.ReadLines(L2H_Constants.client_NPCs_Path).First();
            client_NPC_Names_Startline = File.ReadLines(L2H_Constants.client_NPCnames_Path).First();
            client_Mobskillanim_Startline = File.ReadLines(L2H_Constants.client_Mobskillanimgrp_Path).First();
            


            ThreadWorker_Tests threadWorker_LoadNpcs = new ThreadWorker_Tests(viewModel.NPCsDataTableCollection, this, server_Npcdata, client_Npcs, client_Npcnames, npcTemplate_Pointers, (mainWindow.GetPageOfType(typeof(Pages.DroplistsPage)) as DroplistsPage).npc_Droplist_Pointers, (mainWindow.GetPageOfType(typeof(Pages.NPCsPage)) as NPCsPage), L2H_Npcs, client_Mobskillanims);
            threadWorker_LoadNpcs.ThreadDone += (sender, e) => HandleThreadDone(sender, e, "NPCs Loaded: " + server_Npcdata.Count);
            threadWorker_LoadNpcs.AddChild += (sender, e) => HandleAddNPCType(sender, e);
            Thread thread_LoadNpcs = new Thread(new ThreadStart(threadWorker_LoadNpcs.ThreadProc));
            thread_LoadNpcs.Start();
        }

        private void Load_NPCs_Template_Pointers()
        {
            if (!File.Exists(L2H_Constants.L2H_NPC_Template_Pointers_Path))
            {
                File.Create(L2H_Constants.L2H_NPC_Template_Pointers_Path).Dispose();
            }
            else
            {
                using (TextReader textReader = new StreamReader(L2H_Constants.L2H_NPC_Template_Pointers_Path, Encoding.GetEncoding(65001)))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        L2H_Template_Pointer newTemplate_Pointer = new L2H_Template_Pointer(line);
                        if (newTemplate_Pointer.id != "id")
                            npcTemplate_Pointers.Add(newTemplate_Pointer);
                    }
                }
            }
        }

        private void HandleThreadDone(object sender, EventArgs e, string logMessage)
        {
            Dispatcher.Invoke(() =>
            {

                (Application.Current.MainWindow as MainWindow).UpdateLog(logMessage, L2H_Constants.Color_Add);
                BindData();

                (Application.Current.MainWindow as MainWindow).UpdateLoadedNumber(LoadedTypes.NPCs);

            });
        }

        private void HandleAddNPCType(object sender, EventArgs e)
        {

            Dispatcher.Invoke(() =>
            {

                List<StackPanel> horizontalStackPanels = new List<StackPanel>();

                List<string> reorderedTypes = new List<string>();

                for (int i = 0; i < ((ThreadWorker_Tests)sender).NPC_Types.Count; i++)
                {
                    reorderedTypes.Add(L2H_Parser.GetReadableNPCType((NPCTypes)Enum.Parse(typeof(NPCTypes), ((ThreadWorker_Tests)sender).NPC_Types[i])));
                }

                reorderedTypes.Sort();

                L2H_Filter_Npc_Type newFilter = new L2H_Filter_Npc_Type() { filteringText = "All", readableText = "All" };
                l2HNpcTypeFilters_Left.Add(newFilter);

                for (int i = 0; i < reorderedTypes.Count; i++)
                {
                    newFilter = new L2H_Filter_Npc_Type() { filteringText = L2H_Parser.GetFilteringNPCTypesString(reorderedTypes[i]), readableText = reorderedTypes[i] };
                    if (i % 2 == 1)
                        l2HNpcTypeFilters_Left.Add(newFilter);
                    else
                        l2HNpcTypeFilters_Right.Add(newFilter);
                }


                NPC_Type_Filters_ListView_Left.ItemsSource = l2HNpcTypeFilters_Left;
                NPC_Type_Filters_ListView_Right.ItemsSource = l2HNpcTypeFilters_Right;

            });
        }




        public void BindData()
        {

            L2H_Npcs = L2H_Npcs.OrderBy(x => x.client_Npcname.name).ToList();

            NPC_Name_Listview.ItemsSource = L2H_Npcs;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(NPC_Name_Listview.ItemsSource);
            view.Filter = Filter_NPC_Name;

            allNPCsLoaded = true;

        }

        private bool Filter_NPC_Name(object item)
        {
            var filteredNpc = item as object;

            if (string.IsNullOrEmpty(NPC_Filter_ID.Text) && string.IsNullOrEmpty(NPC_Filter_Name.Text) && string.IsNullOrEmpty(NPC_Filter_Level_Max.Text) && string.IsNullOrEmpty(NPC_Filter_Level_Min.Text) && activeNPCFilteringCategories.Exists(x => x.readableText == "All"))
                return true;

            if (!string.IsNullOrEmpty(NPC_Filter_ID.Text))
            {
                if (!(item as L2H_NPC).ID.ToString().Contains(NPC_Filter_ID.Text))
                    return false;
            }

            if (!string.IsNullOrEmpty(NPC_Filter_Level_Min.Text))
            {
                int targetLevel = 0;
                int npcLevel = 0;
                try
                {
                    npcLevel = int.Parse((item as L2H_NPC).server_Npcdata.level);
                    targetLevel = int.Parse(NPC_Filter_Level_Min.Text);

                    if (npcLevel < targetLevel)
                        return false;
                }
                catch
                {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(NPC_Filter_Level_Max.Text))
            {
                int targetLevel = 0;
                int npcLevel = 0;
                try
                {
                    npcLevel = int.Parse((item as L2H_NPC).server_Npcdata.level);
                    targetLevel = int.Parse(NPC_Filter_Level_Max.Text);

                    if (npcLevel >= targetLevel)
                        return false;
                }
                catch
                {
                    return false;
                }
            }

            bool foundInCategories = false;

            for (int i = 0; i < activeNPCFilteringCategories.Count; i++)
            {
                if (activeNPCFilteringCategories[i].readableText == "All")
                {
                    foundInCategories = true;
                }
                else
                {
                    if ((filteredNpc as L2H_NPC).server_Npcdata.npcType.IndexOf(activeNPCFilteringCategories[i].filteringText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        foundInCategories = true;
                    }
                }
            }

            if (!foundInCategories)
                return false;

            if (!string.IsNullOrEmpty(NPC_Filter_Name.Text))
            {
                return ((filteredNpc as L2H_NPC).client_Npcname.name.IndexOf(NPC_Filter_Name.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            else
                return true;

        }


        public void UpdateDetailsView(L2H_NPC SelectedNPC)
        {

            NPC_Details_Name.DataContext = activeNPC;

            try
            {
                NPC_Details_Image.Source = SelectedNPC.GetNPCImage();
            }
            catch
            {
                NPC_Details_Image.Source = new BitmapImage(new Uri("/Images/ImageNotFound.png", UriKind.Relative));
            }

            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(NPC_Details_Grid))
            {
                tb.DataContext = activeNPC;
            }
            foreach (ToggleButton tb in L2H_Parser.FindVisualChildren<ToggleButton>(NPC_Details_Grid))
            {
                tb.DataContext = activeNPC;
            }
            foreach (Button b in L2H_Parser.FindVisualChildren<Button>(NPC_Details_Grid))
            {
                b.DataContext = activeNPC;
            }
        }

        //Triggers

        private void NPC_Filter_Name_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(NPC_Name_Listview.ItemsSource).Refresh();
        }

        private void NPC_Name_Listview_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void NPC_Name_Click(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            var vm = (sender as FrameworkElement).DataContext;

            if (activeNPC != null)
            {
                if (activeNPC == (L2H_NPC)vm)
                    return;
                else
                {
                    if (activeNPCToggleButton != null)
                        activeNPCToggleButton.IsChecked = false;
                    activeNPC.IsSelected = false;
                }
            }

            activeNPC = (L2H_NPC)vm;
            activeNPCToggleButton = (ToggleButton)sender;
            activeNPC.IsSelected = true;

            isLoadingDetails = true;
            UpdateDetailsView(activeNPC);

            isLoadingDetails = false;

            NPC_Details_Grid.Visibility = Visibility.Visible;


            CollectionViewSource.GetDefaultView(NPC_Name_Listview.ItemsSource).Refresh();

        }

        private void NPC_Filter_Type_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            L2H_Filter_Npc_Type typeFilter;
            ToggleButton tbutt = (ToggleButton)sender;

            typeFilter = ((ToggleButton)sender).DataContext as L2H_Filter_Npc_Type;


            if (typeFilter.readableText == "All")
            {
                if (activeNPCFilteringCategories.Exists(x => x.readableText == "All"))
                {
                    typeFilter.IsSelected = false;
                    tbutt.IsChecked = false;
                    activeNPCFilteringCategories.Remove(typeFilter);
                }
                else
                {
                    for (int i = 0; i < activeNPCFilteringCategories.Count; i++)
                    {
                        activeNPCFilteringCategories[i].IsSelected = false;
                    }
                    for (int i = 0; i < activeToggleButtons.Count; i++)
                    {
                        activeToggleButtons[i].IsChecked = false;
                    }

                    activeToggleButtons.Clear();
                    activeNPCFilteringCategories.Clear();
                    activeNPCFilteringCategories.Add(typeFilter);
                    typeFilter.IsSelected = true;
                    activeToggleButtons.Add(tbutt);
                    tbutt.IsChecked = true;
                }
            }
            else
            {
                if (!activeNPCFilteringCategories.Contains(typeFilter))
                {
                    if (activeNPCFilteringCategories.Exists(x => x.readableText == "All"))
                    {
                        L2H_Filter_Npc_Type allButton = activeNPCFilteringCategories.Find(x => x.readableText == "All");
                        allButton.IsSelected = false;
                        activeToggleButtons.Find(x => (string)x.Tag == "AllButton").IsChecked = false;
                        activeNPCFilteringCategories.Remove(allButton);
                        activeToggleButtons.Remove(activeToggleButtons.Find(x => (string)x.Tag == "AllButton"));
                    }

                    activeNPCFilteringCategories.Add(typeFilter);
                    activeToggleButtons.Add(tbutt);
                    tbutt.IsChecked = true;
                    typeFilter.IsSelected = true;
                }
                else
                {
                    typeFilter.IsSelected = false;
                    tbutt.IsChecked = false;
                    activeNPCFilteringCategories.Remove(typeFilter);
                    activeToggleButtons.Remove(tbutt);
                }
            }

            CollectionViewSource.GetDefaultView(NPC_Name_Listview.ItemsSource).Refresh();
        }

        private void NPC_Filter_Toggle_Button_Loaded(object sender, RoutedEventArgs e)
        {
            ToggleButton t = (ToggleButton)sender;

            if (((L2H_Filter_Npc_Type)t.DataContext).readableText == "All")
            {
                t.IsChecked = true;
                ((L2H_Filter_Npc_Type)t.DataContext).IsSelected = true;
                activeNPCFilteringCategories.Add((L2H_Filter_Npc_Type)t.DataContext);
                activeToggleButtons.Add(t);
                t.Tag = "AllButton";

                CollectionViewSource.GetDefaultView(NPC_Name_Listview.ItemsSource).Refresh();

            }


        }

        private void NPC_Property_Textbox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            MessageBox.Show("test");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessageBox.Show(sender.ToString() + " changed");
        }

        private void ToggleSearchVisibility(object sender, RoutedEventArgs e)
        {
            if (searchIsShowing)
            {
                NPC_Main_Grid.ColumnDefinitions[0].Width = new GridLength(0);
                NPC_Main_Grid.ColumnDefinitions[1].Width = new GridLength(0);
                searchIsShowing = false;
                SearchIsShowingButton.Content = "Show Search";
            }
            else
            {
                NPC_Main_Grid.ColumnDefinitions[0].Width = new GridLength(220);
                NPC_Main_Grid.ColumnDefinitions[1].Width = new GridLength(220);
                searchIsShowing = true;
                SearchIsShowingButton.Content = "Hide Search";
            }
        }

        public void Force_NPC_Selection(L2H_NPC targetNPC)
        {
            if (activeNPC != null)
            {
                if (activeNPC == targetNPC)
                    return;
                else
                {
                    if (activeNPCToggleButton != null)
                        activeNPCToggleButton.IsChecked = false;
                    activeNPC.IsSelected = false;
                }
            }

            activeNPC = targetNPC;
            activeNPC.IsSelected = true;

            isLoadingDetails = true;
            NPC_Details_Grid.Visibility = Visibility.Visible;
            UpdateDetailsView(activeNPC);

            isLoadingDetails = false;

            NPC_Details_Grid.Visibility = Visibility.Visible;

            CollectionViewSource.GetDefaultView(NPC_Name_Listview.ItemsSource).Refresh();
        }

        private void Validate_Lower_Case_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Name_ID(e.Text);
        }
        private void Validate_Integer_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Integer(e.Text);
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

        private void Validate_Float_Input(object sender, KeyEventArgs e)
        {
            L2H_Textbox_Input_Restrictions.Check_If_Dot_Exists_In_Float_TextBox(sender, e);
        }
        

        private void NPC_Clone_Clicked(object sender, RoutedEventArgs e)
        {
            if (activeNPC == null)
            {
                MessageBox.Show("No NPC selected");
                return;
            }

            //Grab relevant information and create a deep copy
            Server_Npcdata newNpcData = ObjectExtensions.Copy(server_Npcdata.Find(x => x.npcId == activeNPC.NPC_ID));

            //Assign new ID:
            string newNpcId = (mainWindow.GetPageOfType(typeof(Pages.OverviewPage)) as OverviewPage).L2H_Settings.NewNPCIndexStart;// "37700";

            int newNpcIdNumber = L2H_Parser.GrabHighestIDFromNPCPointersList(server_Npcdata);

            if (newNpcIdNumber != 0)
                newNpcId = (newNpcIdNumber + 1).ToString();

            if (npcTemplate_Pointers.Count > 0)
            {
                newNpcId = (Convert.ToInt32(npcTemplate_Pointers[npcTemplate_Pointers.Count - 1].id) + 1).ToString();
            }

            newNpcData.npcId = newNpcId;
            newNpcData.npcName = L2H_Parser.GetUniqueNPCNameID(server_Npcdata, newNpcData.npcName);

            //Copying references
            //Client side name and description
            Client_Npcname newName = ObjectExtensions.Copy(client_Npcnames.Find(x => x.id == activeNPC.NPC_ID));
            newName.id = newNpcId;
            client_Npcnames.Add(newName);
            Client_Npc newNpcGrp = ObjectExtensions.Copy(client_Npcs.Find(x => x.tag == activeNPC.NPC_ID));
            newNpcGrp.tag = newNpcId;
            client_Npcs.Add(newNpcGrp);

            //Serverside droplist (normal/spoil/parts)
            L2H_Npc_Droplist_Pointer newPointer = ObjectExtensions.Copy((mainWindow.GetPageOfType(typeof(Pages.DroplistsPage)) as DroplistsPage).npc_Droplist_Pointers.Find(x => x.npc_id == activeNPC.NPC_ID));
            newPointer.npc_id = newNpcId;
            (mainWindow.GetPageOfType(typeof(Pages.DroplistsPage)) as DroplistsPage).npc_Droplist_Pointers.Add(newPointer);

            List<Client_Mobskillanim> existingMobskillanimgrp = client_Mobskillanims.FindAll(x => x.npc_id == activeNPC.NPC_ID);

            if (existingMobskillanimgrp.Count > 0)
                for (int i = 0; i < existingMobskillanimgrp.Count; i++)
                {
                    Client_Mobskillanim targetMobskillanimgrp = ObjectExtensions.Copy(existingMobskillanimgrp[i]);
                    targetMobskillanimgrp.npc_id = newNpcId;

                    client_Mobskillanims.Add(targetMobskillanimgrp);

                }

            //Add to lists
            server_Npcdata.Add(newNpcData);

            L2H_Template_Pointer tp = new L2H_Template_Pointer(newNpcId, activeNPC.NPC_ID);
            if (activeNPC.template != null)
            {
                tp.templateId = activeNPC.template.templateId;
            }
            npcTemplate_Pointers.Add(tp);

            L2H_NPC new_L2H_Npc = new L2H_NPC
            {
                ID = (int.Parse(newNpcId)),
                server_Npcdata = newNpcData,
                client_Npc = newNpcGrp,
                client_Npcname = newName,
                template = tp,
                IsCustom = true
            };

            

            L2H_Npcs.Add(new_L2H_Npc);

            L2H_Log.Instance.Log_NPC_Clone(activeNPC, new_L2H_Npc);

            CollectionViewSource.GetDefaultView(NPC_Name_Listview.ItemsSource).Refresh();

        }

        private void NPC_Delete_Clicked(object sender, RoutedEventArgs e)
        {
            if (activeNPC == null)
            {
                MessageBox.Show("No NPC selected");
                return;
            }

            if (!npcTemplate_Pointers.Exists(x => x.id == activeNPC.NPC_ID))
            {
                MessageBox.Show("Only custom NPCs can be deleted");
                return;
            }

            #region Sioner droplist pointer removed fix
            List<L2H_Npc_Droplist_Pointer> dropPointers = (mainWindow.GetPageOfType(typeof(Pages.DroplistsPage)) as DroplistsPage).npc_Droplist_Pointers;
            L2H_Npc_Droplist_Pointer droplistPointer = dropPointers.FirstOrDefault(p => activeNPC.NPC_ID.ToString().Equals(p.npc_id));

            if (droplistPointer != null)
            {
                dropPointers.Remove(droplistPointer);
            }
            #endregion


            if (activeNPC.template != null)
                npcTemplate_Pointers.Remove(activeNPC.template);

            L2H_Log.Instance.Log_NPC_Delete(activeNPC);

            //Remove all references of item
            server_Npcdata.Remove(activeNPC.server_Npcdata);
            client_Npcnames.Remove(activeNPC.client_Npcname);
            client_Npcs.Remove(activeNPC.client_Npc);

            L2H_Npcs.Remove(activeNPC);

            NPC_Details_Grid.Visibility = Visibility.Hidden;
            CollectionViewSource.GetDefaultView(NPC_Name_Listview.ItemsSource).Refresh();


        }

        private void NPC_AI_Clicked(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Skills must be loaded before cloning NPCs");

            if (activeNPC != null)
            {
                Popup_NPC_AI_Parameters dialog = new Popup_NPC_AI_Parameters(activeNPC);
                dialog.Show();
            }
            else
            {
                MessageBox.Show("No Active NPC loaded at the moment... This shouldn't happen :S");
            }
        }

        private void NPC_Level_Specifics_Lost_Focus(object sender, RoutedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            switch (t.Name)
            {
                case "NPC_Details_Property_Level":
                    {
                        if (!string.IsNullOrEmpty(NPC_Details_Property_Level.Text))
                        {
                            if (activeNPC.server_Npcdata.level == NPC_Details_Property_Level.Text)
                                return;

                            L2H_Log.Instance.Log_NPC_Changed(activeNPC, "Level", activeNPC.server_Npcdata.level, NPC_Details_Property_Level.Text);

                            activeNPC.server_Npcdata.level = NPC_Details_Property_Level.Text;
                            activeNPC.server_Npcdata.acquire_exp_rate = L2H_Parser.GetNPCExpSpRpRate(activeNPC.server_Npcdata, NPC_Details_Property_Kill_Experience.Text, "xp");

                            Int64 thisLevelEXP = Int64.Parse(expPage.GetExpByLevel(activeNPC.server_Npcdata.level));
                            Int64 existingEXP = Int64.Parse(activeNPC.server_Npcdata.exp);
                            Int64 nextLevelEXP = Int64.Parse(expPage.GetExpByLevel(activeNPC.server_Npcdata.level + 1));

                            NPC_Details_Property_Level.Text = activeNPC.NPC_Level;

                            if (existingEXP < thisLevelEXP || existingEXP > nextLevelEXP)
                            {
                                activeNPC.server_Npcdata.exp = expPage.GetExpByLevel(activeNPC.server_Npcdata.level);
                                NPC_Details_Property_Experience.Text = activeNPC.NPC_Experience;
                            }
                            NPC_Details_Property_Kill_Experience.Text = L2H_Parser.GetNPCExpSpRpOnKill(activeNPC, "xp");


                        }
                        else
                        {
                            NPC_Details_Property_Level.Text = activeNPC.server_Npcdata.level;
                        }
                        break;
                    }
                case "NPC_Details_Property_Experience":
                    {
                        if (!string.IsNullOrEmpty(NPC_Details_Property_Experience.Text))
                        {
                            if (activeNPC.server_Npcdata.exp == NPC_Details_Property_Experience.Text)
                                return;


                            L2H_Log.Instance.Log_NPC_Changed(activeNPC, "Experience", activeNPC.server_Npcdata.exp, NPC_Details_Property_Kill_Experience.Text);

                            activeNPC.server_Npcdata.exp = NPC_Details_Property_Experience.Text;
                            activeNPC.server_Npcdata.level = expPage.GetLevelByExp(activeNPC.server_Npcdata.exp);
                            activeNPC.server_Npcdata.acquire_exp_rate = L2H_Parser.GetNPCExpSpRpRate(activeNPC.server_Npcdata, NPC_Details_Property_Kill_Experience.Text, "xp");

                            NPC_Details_Property_Level.Text = activeNPC.NPC_Level;

                            NPC_Details_Property_Experience.Text = activeNPC.NPC_Experience;
                            NPC_Details_Property_Kill_Experience.Text = L2H_Parser.GetNPCExpSpRpOnKill(activeNPC, "xp");


                        }
                        else
                            NPC_Details_Property_Experience.Text = activeNPC.server_Npcdata.exp;
                        break;
                    }
                case "NPC_Details_Property_Kill_Experience":
                    {
                        if (string.IsNullOrEmpty(NPC_Details_Property_Kill_Experience.Text))
                        {
                            NPC_Details_Property_Kill_Experience.Text = L2H_Parser.GetNPCExpSpRpOnKill(activeNPC, "xp");
                        }
                        else
                        {
                            if (NPC_Details_Property_Kill_Experience.Text == L2H_Parser.GetNPCExpSpRpOnKill(activeNPC, "xp"))
                                return;

                            L2H_Log.Instance.Log_NPC_Changed(activeNPC, "EXP ON KILL", L2H_Parser.GetNPCExpSpRpOnKill(activeNPC, "xp"), NPC_Details_Property_Kill_Experience.Text);
                            activeNPC.server_Npcdata.acquire_exp_rate = L2H_Parser.GetNPCExpSpRpRate(activeNPC.server_Npcdata, NPC_Details_Property_Kill_Experience.Text, "xp");
                        }
                        break;
                    }
                case "NPC_Details_Property_Kill_Skill_Points":
                    {
                        if (string.IsNullOrEmpty(NPC_Details_Property_Kill_Skill_Points.Text))
                        {
                            NPC_Details_Property_Kill_Skill_Points.Text = L2H_Parser.GetNPCExpSpRpOnKill(activeNPC, "sp");
                        }
                        else
                        {
                            L2H_Log.Instance.Log_NPC_Changed(activeNPC, "SP ON KILL", L2H_Parser.GetNPCExpSpRpOnKill(activeNPC, "sp"), NPC_Details_Property_Kill_Skill_Points.Text);
                            activeNPC.server_Npcdata.acquire_sp = L2H_Parser.GetNPCExpSpRpRate(activeNPC.server_Npcdata, NPC_Details_Property_Kill_Skill_Points.Text, "sp");
                        }
                        break;
                    }
                case "NPC_Details_Property_Kill_Reputation_Points":
                    {
                        if (string.IsNullOrEmpty(NPC_Details_Property_Kill_Reputation_Points.Text))
                        {
                            NPC_Details_Property_Kill_Reputation_Points.Text = L2H_Parser.GetNPCExpSpRpOnKill(activeNPC, "rp");
                        }
                        else
                        {
                            L2H_Log.Instance.Log_NPC_Changed(activeNPC, "RP ON KILL", L2H_Parser.GetNPCExpSpRpOnKill(activeNPC, "rp"), NPC_Details_Property_Kill_Reputation_Points.Text);
                            activeNPC.server_Npcdata.acquire_rp = L2H_Parser.GetNPCExpSpRpRate(activeNPC.server_Npcdata, NPC_Details_Property_Kill_Reputation_Points.Text, "rp");
                        }
                        break;
                    }

            }
        }

        private void NPC_Force_Select_Waiting(object sender, RoutedEventArgs e)
        {
            if (forceWaiting)
            {
                UpdateDetailsView(activeNPC);
                forceWaiting = false;
            }
        }

        private void NPC_Droplist_GoTo_Clicked(object sender, RoutedEventArgs e)
        {
            string tag = (sender as FrameworkElement).Tag.ToString();

            (mainWindow.GetPageOfType(typeof(DroplistsPage)) as DroplistsPage).forceWaiting = true;

            switch (tag)
            {
                case "normal":
                    (mainWindow.GetPageOfType(typeof(DroplistsPage)) as DroplistsPage).Force_Droplist_Selection(activeNPC.NPC_Droplist_Normal_ID);
                    break;
                case "spoil":
                    (mainWindow.GetPageOfType(typeof(DroplistsPage)) as DroplistsPage).Force_Droplist_Selection(activeNPC.NPC_Droplist_Spoil_ID);
                    break;
                case "multi":
                    (mainWindow.GetPageOfType(typeof(DroplistsPage)) as DroplistsPage).Force_Droplist_Selection(activeNPC.NPC_Droplist_Multi_ID);
                    break;
                case "extra":
                    (mainWindow.GetPageOfType(typeof(DroplistsPage)) as DroplistsPage).Force_Droplist_Selection(activeNPC.NPC_Droplist_Extra_ID);
                    break;
                default:
                    break;
            }

            mainWindow.Category_Force(MenuCategories.Droplists);
        }

        private void NPC_Droplist_Clicked(object sender, RoutedEventArgs e)
        {
            string tag = (sender as FrameworkElement).Tag.ToString();
            Popup_Droplist_Selection dialog = new Popup_Droplist_Selection((mainWindow.GetPageOfType(typeof(DroplistsPage)) as DroplistsPage).L2H_Droplists);

            switch (tag)
            {
                case "normal":
                    dialog.Initialize_For_Picking_Single_Droplist(activeNPC, DroplistSlot.normal);
                    break;
                case "spoil":
                    dialog.Initialize_For_Picking_Single_Droplist(activeNPC, DroplistSlot.spoil);
                    break;
                case "multi":
                    dialog.Initialize_For_Picking_Multi_Droplist(activeNPC, DroplistSlot.multi);
                    break;
                case "extra":
                    dialog.Initialize_For_Picking_Multi_Droplist(activeNPC, DroplistSlot.extra);
                    break;
                default:
                    break;
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

    class ThreadWorker_Tests
    {
        public EventHandler ThreadDone;
        public EventHandler AddChild;

        DataTable dataTable;
        public List<Server_Npcdata> server_Npcdata;
        public List<Client_Npc> client_Npcs;
        public List<Client_Npcname> client_Npcnames;
        public List<L2H_Npc_Droplist_Pointer> npc_Droplist_Pointers;
        public List<L2H_Template_Pointer> npcTemplate_Pointers;
        public List<L2H_NPC> L2HNpcs;
        public List<Client_Mobskillanim> client_Mobskillanims;

        NPCsPage n;

        public List<string> NPC_Types;

        public ThreadWorker_Tests(DataTable dataTable, NPCsPage n, List<Server_Npcdata> server_Npcdata, List<Client_Npc> client_Npcs, List<Client_Npcname> client_Npcnames, List<L2H_Template_Pointer> npcTemplate_Pointers, List<L2H_Npc_Droplist_Pointer> npc_Droplist_Pointers, NPCsPage z, List<L2H_NPC> L2HNpcs, List<Client_Mobskillanim> client_Mobskillanims)
        {
            this.dataTable = dataTable;
            this.server_Npcdata = server_Npcdata;
            this.client_Npcs = client_Npcs;
            this.client_Npcnames = client_Npcnames;
            this.npc_Droplist_Pointers = npc_Droplist_Pointers;
            this.npcTemplate_Pointers = npcTemplate_Pointers;
            this.n = n;
            this.L2HNpcs = L2HNpcs;
            this.client_Mobskillanims = client_Mobskillanims;
            NPC_Types = new List<string>();
        }

        public void ThreadProc()
        {

            //Loading NPCNames first
            using (TextReader textReader = new StreamReader(L2H_Constants.client_NPCnames_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Npcname newNpcName = new Client_Npcname(line);
                    if (newNpcName.id != "id")
                        client_Npcnames.Add(newNpcName);
                }
            }

            //Then loading NpcGrp
            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_NPCs_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Npc newNpcGrp = new Client_Npc(line);
                    if (newNpcGrp.tag != "tag")
                        client_Npcs.Add(newNpcGrp);
                }
            }

            //Then loading mobskillanims
            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Mobskillanimgrp_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Mobskillanim newMobskillanimgrp = new Client_Mobskillanim(line);
                    if (newMobskillanimgrp.npc_id != "npc_id")
                    {
                        client_Mobskillanims.Add(newMobskillanimgrp);
                    }

                }
            }


            //Finally Load Server NpcData
            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.server_NPCdata_Path, Encoding.GetEncoding(1200)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    if (line.Contains("//"))
                        continue;

                    Server_Npcdata newNpcData = new Server_Npcdata(line, n);

                    if (newNpcData.invalidNpc)
                    {
                        newNpcData.invalidString = line;
                    }

                    newNpcData.npcname_e = client_Npcnames.Find(x => x.id == newNpcData.npcId);

                    if (newNpcData.npcname_e != null)
                    {
                        newNpcData.npcIngameName = newNpcData.npcname_e.name;
                        newNpcData.description = client_Npcnames.Find(x => x.id == newNpcData.npcId).description;
                    }
                    else
                    {
                        newNpcData.npcIngameName = "No ingame name found";
                        newNpcData.description = "No ingame description found";
                    }

                    server_Npcdata.Add(newNpcData);
                }
            }



            for (int i = 0; i < server_Npcdata.Count; i++)
            {
                Server_Npcdata targetNpcData = server_Npcdata[i];

                if (targetNpcData.invalidNpc)
                    continue;

                string template_P = "";
                string template_N = "";

                L2H_Template_Pointer targetPointer = npcTemplate_Pointers.Find(x => x.id == server_Npcdata[i].npcId);

                if (targetPointer != null)
                {
                    template_P = targetPointer.templateId;
                    if (targetPointer.extraName != null)
                        template_N = targetPointer.extraName;

                    if (targetPointer.templateId != null)
                        if (!npcTemplate_Pointers.Exists(x => x.id == targetPointer.id))
                            npcTemplate_Pointers.Add(targetPointer);
                }


                if (!NPC_Types.Contains(targetNpcData.npcType))
                {
                    NPC_Types.Add(targetNpcData.npcType);
                }

                int testID = (int.Parse(targetNpcData.npcId));
                Server_Npcdata testd = targetNpcData;
                Client_Npc client_Npc = client_Npcs.Find(x => x.tag == targetNpcData.npcId);
                Client_Npcname ass = client_Npcnames.Find(x => x.id == targetNpcData.npcId);

                L2H_NPC new_L2H_NPC = new L2H_NPC()
                {
                    ID = (int.Parse(targetNpcData.npcId)),
                    server_Npcdata = targetNpcData,
                    client_Npc = client_Npcs.Find(x => x.tag == targetNpcData.npcId),
                    client_Npcname = client_Npcnames.Find(x => x.id == targetNpcData.npcId)
                };

                if (targetPointer != null)
                    new_L2H_NPC.template = targetPointer;

                L2HNpcs.Add(new_L2H_NPC);

            }


            if (AddChild != null)
                AddChild.Invoke(this, EventArgs.Empty);

            if (ThreadDone != null)
                ThreadDone.Invoke(this, EventArgs.Empty);
        }
    }
}
