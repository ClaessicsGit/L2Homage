using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for DroplistsPage.xaml
    /// </summary>
    public partial class DroplistsPage : Page
    {
        public List<L2H_Droplist> L2H_Droplists;
        public L2H_Droplist active_L2H_Droplist;
        public L2H_Droplist_Connection active_L2H_Droplist_Connection;

        public List<Server_Droplist> single_Droplist_Data;
        public List<Server_Multi_Droplist> multi_Droplist_Data;
        public List<L2H_Multi_Droplist_Pointer> multi_Droplist_Pointers;
        public List<L2H_Npc_Droplist_Pointer> npc_Droplist_Pointers;
        MainWindow mainWindow;

        public bool existingDroplistsFound;
        public bool existing_Single_Droplists_Loaded;
        public bool existing_Multi_Droplists_Loaded;

        int numberOfThreadsToUseForHookingDroplistsTogether = 0;
        int numberOfThreadsToUseForHookingItemsToDroplists = 0;
        int numberOfThreadsCompleted = 0;
        int numberOfItemThreadsCompleted = 0;

        public bool droplistsFullyLoaded;
        public bool multiDroplistsLoaded;
        public bool originalDroplistsLoaded;

        bool searchIsShowing = true;
        public bool forceWaiting = false;

        //Filter
        ToggleButton active_Filter_Droplist_Type;
        private ICollectionView droplistView;

        public ICollectionView Droplists_View
        {
            get { return droplistView; }
        }


        public DroplistsPage()
        {
            InitializeComponent();
            mainWindow = Application.Current.MainWindow as MainWindow;

            L2H_Droplists = new List<L2H_Droplist>();
            single_Droplist_Data = new List<Server_Droplist>();
            multi_Droplist_Data = new List<Server_Multi_Droplist>();
            multi_Droplist_Pointers = new List<L2H_Multi_Droplist_Pointer>();
            npc_Droplist_Pointers = new List<L2H_Npc_Droplist_Pointer>();

            IList<L2H_Droplist> droplists = L2H_Droplists;
            droplistView = CollectionViewSource.GetDefaultView(droplists);
            droplistView.Filter = Filter_Droplist_Name;

            active_Filter_Droplist_Type = Droplist_Filter_Droplist_Type_All;

        }

        public void LoadDroplists()
        {
            List<Server_Npcdata> npcdata = (mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage).server_Npcdata;

            //Load pointers and single droplists first
            Load_Multi_Droplist_Pointers();
            Load_Npc_Droplist_Pointers();
            Load_Existing_Single_Droplists();
            Load_Existing_Multi_Droplists();

            SpinWait.SpinUntil(() => numberOfThreadsToUseForHookingDroplistsTogether == numberOfThreadsCompleted, Timeout.Infinite);

            if (!existingDroplistsFound)
            {
                ExportOriginalDroplistsAndPointers();
            }

            for (int i = 0; i < single_Droplist_Data.Count; i++)
            {
                L2H_Droplist new_L2H_Droplist = new L2H_Droplist()
                {
                    ID = single_Droplist_Data[i].id,
                    IsCustom = single_Droplist_Data[i].isCustom,
                    IsSelected = false,
                    Droplist_Type = L2H_Droplist_Type.single,
                    server_Droplist = single_Droplist_Data[i],
                    ConnectedDroplists = new List<L2H_Droplist>(),
                    ConnectedNpcs = new List<L2H_NPC>()
                };

                single_Droplist_Data[i].L2H_Droplist = new_L2H_Droplist;

                L2H_Droplists.Add(new_L2H_Droplist);
            }

            Dispatcher.Invoke(() =>
            {
                (Application.Current.MainWindow as MainWindow).UpdateLog("Single Droplists Loaded: " + single_Droplist_Data.Count, L2H_Constants.Color_Add);
            });

            for (int i = 0; i < multi_Droplist_Data.Count; i++)
            {
                L2H_Droplist new_L2H_Droplist = new L2H_Droplist()
                {
                    ID = multi_Droplist_Data[i].id,
                    IsCustom = multi_Droplist_Data[i].isCustom,
                    IsSelected = false,
                    Droplist_Type = L2H_Droplist_Type.multi,
                    server_Multi_Droplist = multi_Droplist_Data[i],
                    ConnectedDroplists = new List<L2H_Droplist>(),
                    ConnectedNpcs = new List<L2H_NPC>()
                };

                multi_Droplist_Data[i].L2H_Droplist = new_L2H_Droplist;

                L2H_Droplists.Add(new_L2H_Droplist);

            }

            Dispatcher.Invoke(() =>
            {
                (Application.Current.MainWindow as MainWindow).UpdateLog("Multi Droplists Loaded: " + multi_Droplist_Data.Count, L2H_Constants.Color_Add);
            });

            //find l2h_npc for each droplist in npc_droplist_pointers
            numberOfThreadsCompleted = 0;
            numberOfThreadsToUseForHookingDroplistsTogether = (npc_Droplist_Pointers.Count / L2H_Constants.Tasks_Per_Thread) + 1;
            for (int i = 0; i < numberOfThreadsToUseForHookingDroplistsTogether; i++)
            {
                int value = i;
                ThreadWorker_Hook_NPCs_To_Droplists threadWorker_Hook_Npcs_To_Droplists = new ThreadWorker_Hook_NPCs_To_Droplists();
                threadWorker_Hook_Npcs_To_Droplists.ThreadDone += (b, bb) => HandleThreadDone(b, bb, "Completed Thread: Hook Droplists to NPCs: " + (value * L2H_Constants.Tasks_Per_Thread).ToString() + " to " + (((value + 1) * L2H_Constants.Tasks_Per_Thread) - 1).ToString() + " of " + npc_Droplist_Pointers.Count);
                threadWorker_Hook_Npcs_To_Droplists.startIndex = value * L2H_Constants.Tasks_Per_Thread;
                threadWorker_Hook_Npcs_To_Droplists.endIndex = ((value + 1) * L2H_Constants.Tasks_Per_Thread);
                threadWorker_Hook_Npcs_To_Droplists.l2H_Droplists = L2H_Droplists;
                threadWorker_Hook_Npcs_To_Droplists.npc_Droplist_Pointers = npc_Droplist_Pointers;
                threadWorker_Hook_Npcs_To_Droplists.L2H_Npcs = (mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage).L2H_Npcs;
                Thread t = new Thread(new ThreadStart(threadWorker_Hook_Npcs_To_Droplists.ThreadProc));
                t.Start();
            }
            Dispatcher.Invoke(() =>
            {
                (Application.Current.MainWindow as MainWindow).UpdateLog("Droplists hooked to NPCs: " + npc_Droplist_Pointers.Count, L2H_Constants.Color_Add);
            });

            SpinWait.SpinUntil(() => numberOfThreadsToUseForHookingDroplistsTogether == numberOfThreadsCompleted, Timeout.Infinite);


            //find l2h_item for each item in single_droplist_data
            numberOfItemThreadsCompleted = 0;
            numberOfThreadsToUseForHookingItemsToDroplists = (single_Droplist_Data.Count / L2H_Constants.Tasks_Per_Thread) + 1;
            for (int i = 0; i < numberOfThreadsToUseForHookingItemsToDroplists; i++)
            {
                int value = i;
                ThreadWorker_Hook_Items_To_Single_Droplists threadWorker_Hook_Items_To_Single_Droplists = new ThreadWorker_Hook_Items_To_Single_Droplists();
                threadWorker_Hook_Items_To_Single_Droplists.ThreadDone += (b, bb) => HandleItemHookedThreadDone(b, bb, "Completed Thread: Hook Items To Single Droplists: " + (value * L2H_Constants.Tasks_Per_Thread).ToString() + " to " + (((value + 1) * L2H_Constants.Tasks_Per_Thread) - 1).ToString() + " of " + single_Droplist_Data.Count);
                threadWorker_Hook_Items_To_Single_Droplists.startIndex = value * L2H_Constants.Tasks_Per_Thread;
                threadWorker_Hook_Items_To_Single_Droplists.endIndex = ((value + 1) * L2H_Constants.Tasks_Per_Thread);
                threadWorker_Hook_Items_To_Single_Droplists.single_Droplist_Data = single_Droplist_Data;
                threadWorker_Hook_Items_To_Single_Droplists.L2H_Items = (mainWindow.GetPageOfType(typeof(ItemsPage)) as ItemsPage).L2H_Items;
                Thread t = new Thread(new ThreadStart(threadWorker_Hook_Items_To_Single_Droplists.ThreadProc));
                t.Start();
            }

            Dispatcher.Invoke(() =>
            {
                (Application.Current.MainWindow as MainWindow).UpdateLog("Hooked " + (mainWindow.GetPageOfType(typeof(ItemsPage)) as ItemsPage).L2H_Items.Count + " Items to " + single_Droplist_Data.Count + " Single Droplists", L2H_Constants.Color_Add);
            });

            SpinWait.SpinUntil(() => numberOfThreadsToUseForHookingItemsToDroplists == numberOfItemThreadsCompleted, 40000); //This one doesn't always trigger for some reason.


            //All droplists finished loading here, including cross references with single/multi and NPCs
            Dispatcher.Invoke(() =>
            {
                
                Droplists_Name_Listview.ItemsSource = Droplists_View;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Droplists_View);
                view.Filter = Filter_Droplist_Name;
                mainWindow.UpdateLoadedNumber(LoadedTypes.Droplists);

            });
            droplistsFullyLoaded = true;

        }


        void Load_Multi_Droplist_Pointers()
        {

            if (!File.Exists(L2H_Constants.L2H_Multi_Droplists_Path))
            {
                File.Create(L2H_Constants.L2H_Multi_Droplists_Path).Dispose();
            }
            else
            {
                // Load text
                using (TextReader textReader = new StreamReader(L2H_Constants.L2H_Multi_Droplists_Path, Encoding.GetEncoding(1200)))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        string[] splitString = line.Split('\t');

                        string droplistID = splitString[0];
                        List<string> multiPartIds = new List<string>();
                        List<string> multiPartProbabilities = new List<string>();


                        int completedParts = 0;
                        int numberOfItems = (splitString.Length - 2) / 2;

                        for (int i = 2; i < numberOfItems + 2; i++)
                        {
                            multiPartIds.Add(splitString[i + completedParts]);
                            multiPartProbabilities.Add(splitString[i + completedParts + 1]);
                            completedParts++;
                        }

                        bool custom = false;
                        if (splitString[1] == "1")
                            custom = true;

                        multi_Droplist_Pointers.Add(new L2H_Multi_Droplist_Pointer(splitString[0], custom, multiPartIds, multiPartProbabilities));

                    }
                }
            }
        }

        void Load_Npc_Droplist_Pointers()
        {
            if (!File.Exists(L2H_Constants.L2H_NPC_Droplists_Pointers_Path))
            {
                File.Create(L2H_Constants.L2H_NPC_Droplists_Pointers_Path).Dispose();
            }
            else
            {
                // Load text
                using (TextReader textReader = new StreamReader(L2H_Constants.L2H_NPC_Droplists_Pointers_Path, Encoding.GetEncoding(1200)))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        L2H_Npc_Droplist_Pointer x = new L2H_Npc_Droplist_Pointer(line);


                        npc_Droplist_Pointers.Add(x);
                    }
                }
            }
        }


        /// <summary>
        /// Loading the raw droplists before NPCs are loaded
        /// </summary>
        void Load_Existing_Single_Droplists()
        {
            if (!File.Exists(L2H_Constants.L2H_Single_Droplists_Path))
            {
                File.Create(L2H_Constants.L2H_Single_Droplists_Path).Close();
            }
            else
            {
                existingDroplistsFound = true;

                // Load text
                using (TextReader textReader = new StreamReader(L2H_Constants.L2H_Single_Droplists_Path, Encoding.GetEncoding(1200)))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        string[] splitLine = line.Split('\t');

                        bool isCustom = false;
                        if (splitLine[1] == "True" || splitLine[1] == "1")
                            isCustom = true;

                        Server_Droplist newDroplist = new Server_Droplist(splitLine[0], isCustom, splitLine[2]);

                        single_Droplist_Data.Add(newDroplist);

                    }
                }

                existing_Single_Droplists_Loaded = true;
            }
        }

        void Load_Existing_Multi_Droplists()
        {
            if (!File.Exists(L2H_Constants.L2H_Multi_Droplists_Path))
            {
                File.Create(L2H_Constants.L2H_Multi_Droplists_Path).Close();
            }
            else
            {
                using (TextReader textReader = new StreamReader(L2H_Constants.L2H_Multi_Droplists_Path, Encoding.GetEncoding(1200)))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        string[] splitLine = line.Split('\t');

                        bool isCustom = false;
                        if (splitLine[1] == "True" || splitLine[1] == "1")
                            isCustom = true;

                        List<string> separateDroplistIDs = new List<string>();
                        List<string> separateDroplistChances = new List<string>();

                        int numberOfSeparateDroplists = (splitLine.Length - 2) / 2;
                        int processedPointers = 2;

                        for (int i = 0; i < numberOfSeparateDroplists; i++)
                        {
                            separateDroplistIDs.Add(splitLine[processedPointers]);
                            separateDroplistChances.Add(splitLine[processedPointers + 1]);
                            processedPointers += 2;
                        }


                        Server_Multi_Droplist newDroplist = new Server_Multi_Droplist(splitLine[0], isCustom, separateDroplistIDs, separateDroplistChances);//(splitLine[4], splitLine[2], t, splitLine[0]);

                        multi_Droplist_Data.Add(newDroplist);

                    }
                }

                numberOfThreadsToUseForHookingDroplistsTogether = (multi_Droplist_Data.Count / L2H_Constants.Tasks_Per_Thread) + 1;
                for (int i = 0; i < numberOfThreadsToUseForHookingDroplistsTogether; i++)
                {
                    int value = i;
                    ThreadWorker_Hook_Droplists_To_Multi_Droplists threadWorker_HookDroplists = new ThreadWorker_Hook_Droplists_To_Multi_Droplists();
                    threadWorker_HookDroplists.ThreadDone += (b, bb) => HandleThreadDone(b, bb, "Completed Thread: Hook Multi Droplists to Single Droplists: " + (value * L2H_Constants.Tasks_Per_Thread).ToString() + " to " + (((value + 1) * L2H_Constants.Tasks_Per_Thread) - 1).ToString() + " of " + multi_Droplist_Data.Count);
                    threadWorker_HookDroplists.startIndex = value * L2H_Constants.Tasks_Per_Thread;
                    threadWorker_HookDroplists.endIndex = ((value + 1) * L2H_Constants.Tasks_Per_Thread) - 1;
                    threadWorker_HookDroplists.multi_Droplist_Data = multi_Droplist_Data;
                    threadWorker_HookDroplists.single_Droplist_Data = single_Droplist_Data;
                    Thread t = new Thread(new ThreadStart(threadWorker_HookDroplists.ThreadProc));
                    t.Start();
                }


                existing_Multi_Droplists_Loaded = true;
            }
        }

        private void ExportOriginalDroplistsAndPointers()
        {
            List<Server_Npcdata> npcdata = (mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage).server_Npcdata;

            Dispatcher.Invoke(() => { mainWindow.UpdateLog("No droplist pointers found. Creating pointers database from NPCdata. " + npcdata.Count.ToString() + " NPCs Found.", L2H_Constants.Color_Category); });


            for (int i = 0; i < npcdata.Count; i++)
            {
                npcdata[i].HandleDroplists(this);
            }

            string[] npc_droplist_pointers_lines = new string[npc_Droplist_Pointers.Count];
            string[] single_Droplist_Lines = new string[single_Droplist_Data.Count];
            string[] multi_Droplist_Pointers_Lines = new string[multi_Droplist_Pointers.Count];

            Dispatcher.Invoke(() => { mainWindow.UpdateLog("Creating NPC droplist pointer database..", L2H_Constants.Color_Log_Thread_Begin); });
            for (int i = 0; i < npc_droplist_pointers_lines.Length; i++)
            {
                npc_droplist_pointers_lines[i] = npc_Droplist_Pointers[i].GetExportString();
            }
            File.WriteAllLines(L2H_Constants.L2H_NPC_Droplists_Pointers_Path, npc_droplist_pointers_lines, Encoding.GetEncoding(1200));
            Dispatcher.Invoke(() => { mainWindow.UpdateLog("NPC droplist pointers completed.", L2H_Constants.Color_Add); });
            Dispatcher.Invoke(() => { mainWindow.UpdateLog("Creating single droplist pointer database..", L2H_Constants.Color_Log_Thread_Begin); });
            for (int i = 0; i < single_Droplist_Lines.Length; i++)
            {
                single_Droplist_Lines[i] = single_Droplist_Data[i].GetCustomExportString();
            }
            File.WriteAllLines(L2H_Constants.L2H_Single_Droplists_Path, single_Droplist_Lines, Encoding.GetEncoding(1200));
            Dispatcher.Invoke(() => { mainWindow.UpdateLog("Single droplist pointers completed.", L2H_Constants.Color_Add); });
            Dispatcher.Invoke(() => { mainWindow.UpdateLog("Creating multi droplist pointer database..", L2H_Constants.Color_Log_Thread_Begin); });
            for (int i = 0; i < multi_Droplist_Pointers_Lines.Length; i++)
            {
                multi_Droplist_Pointers_Lines[i] = multi_Droplist_Pointers[i].GetExportString();
            }
            File.WriteAllLines(L2H_Constants.L2H_Multi_Droplists_Path, multi_Droplist_Pointers_Lines, Encoding.GetEncoding(1200));
            Dispatcher.Invoke(() => { mainWindow.UpdateLog("Multi droplist pointers completed.", L2H_Constants.Color_Add); });


            Dispatcher.Invoke(() => { mainWindow.UpdateLog("Droplist pointers successfully created.", L2H_Constants.Color_Add); });


        }

        public Server_Droplist Get_Single_Droplist(string npcID, DroplistSlot slot, L2H_Npc_Droplist_Pointer p)
        {
            if (p != null)
            {
                Server_Droplist droplistToFind;
                switch (slot)
                {
                    case DroplistSlot.normal:
                        droplistToFind = single_Droplist_Data.Find(x => x.id == p.droplist_normal_id);
                        break;
                    case DroplistSlot.spoil:
                        droplistToFind = single_Droplist_Data.Find(x => x.id == p.droplist_spoil_id);
                        break;
                    default:
                        return null;
                }


                if (droplistToFind != null)
                    return droplistToFind;
                else
                    return null;
            }
            else
                return null;

        }

        public Server_Multi_Droplist Get_Multi_Droplist(string npcId, DroplistSlot slot, L2H_Npc_Droplist_Pointer p)
        {
            if (p != null)
            {
                Server_Multi_Droplist droplistToFind;
                switch (slot)
                {
                    case DroplistSlot.multi:
                        droplistToFind = multi_Droplist_Data.Find(x => x.id == p.droplist_multi_id);
                        break;
                    case DroplistSlot.extra:
                        droplistToFind = multi_Droplist_Data.Find(x => x.id == p.droplist_extra_id);
                        break;
                    default:
                        return null;
                }


                if (droplistToFind != null)
                    return droplistToFind;
                else
                    return null;
            }
            else
                return null;

        }

        public Server_Droplist Create_Single_Droplist(string droplist_ID, bool isCustom, string drop_string)
        {
            Server_Droplist newDroplist = new Server_Droplist(droplist_ID, isCustom, drop_string);

            single_Droplist_Data.Add(newDroplist);
            return newDroplist;
        }

        public Server_Multi_Droplist Create_Multi_Droplist(string droplist_ID, bool isCustom, string drop_string)
        {
            Server_Multi_Droplist newMultiDroplist = new Server_Multi_Droplist(droplist_ID, isCustom, drop_string);

            multi_Droplist_Data.Add(newMultiDroplist);
            return newMultiDroplist;
        }

        void HandleThreadDone(object sender, EventArgs e, string message)
        {
            Interlocked.Increment(ref numberOfThreadsCompleted);
        }

        void HandleItemHookedThreadDone(object sender, EventArgs e, string message)
        {
            Interlocked.Increment(ref numberOfItemThreadsCompleted);
        }

        private bool Filter_Droplist_Name(object item)
        {
            L2H_Droplist filteredItem = item as L2H_Droplist;

            if (filteredItem == null)
                return false;

            if (Droplist_Filter_Droplist_Type_All.IsChecked == false)
            {
                if (Droplist_Filter_Droplist_Type_Multi.IsChecked == true)
                {
                    if (filteredItem.Droplist_Type == L2H_Droplist_Type.single)
                        return false;
                }
                else if (Droplist_Filter_Droplist_Type_Single.IsChecked == true)
                {
                    if (filteredItem.Droplist_Type == L2H_Droplist_Type.multi)
                        return false;
                }
            }


            if (Droplist_Filter_Hide_Empty.IsChecked == true)
            {
                switch (filteredItem.Droplist_Type)
                {
                    case L2H_Droplist_Type.single:
                        if (filteredItem.server_Droplist.itemDrops.Count == 0)
                            return false;
                        break;
                    case L2H_Droplist_Type.multi:
                        bool returnValue = false;
                        for (int i = 0; i < filteredItem.server_Multi_Droplist.separateDroplists.Count; i++)
                        {
                            if (filteredItem.server_Multi_Droplist.separateDroplists[i].itemDrops.Count > 0)
                                returnValue = true;
                        }
                        if (!returnValue)
                            return false;
                        else
                            break;
                    default:
                        break;
                }
            }


            if (!string.IsNullOrEmpty(Droplist_Filter_ID.Text))
            {
                if (!filteredItem.ID.Contains(Droplist_Filter_ID.Text))
                    return false;
            }

            if (!string.IsNullOrEmpty(Droplist_Filter_NPC_Ref.Text))
            {
                switch (filteredItem.Droplist_Type)
                {
                    case L2H_Droplist_Type.single:
                        return filteredItem.ConnectedNpcs.Exists(x => x.client_Npcname.name.ToLower().Contains(Droplist_Filter_NPC_Ref.Text.ToLower()));
                    case L2H_Droplist_Type.multi:
                        return filteredItem.ConnectedNpcs.Exists(x => x.client_Npcname.name.ToLower().Contains(Droplist_Filter_NPC_Ref.Text.ToLower()));
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(Droplist_Filter_Contains_Item.Text))
            {
                //Droplists need to contain references to L2H_Items for filtering purposes
                //Update: They do now! Suck it, past me
                switch (filteredItem.Droplist_Type)
                {
                    case L2H_Droplist_Type.single:
                        if (!filteredItem.server_Droplist.itemDrops.Exists(x => x.l2h_Item.client_Itemname.name.ToLower().Contains(Droplist_Filter_Contains_Item.Text.ToLower())))
                            return false;
                        break;
                    case L2H_Droplist_Type.multi:
                        {
                            bool foundItem = false;

                            for (int i = 0; i < filteredItem.server_Multi_Droplist.separateDroplists.Count; i++)
                            {
                                if (filteredItem.server_Multi_Droplist.separateDroplists[i].itemDrops.Exists(x => x.l2h_Item.client_Itemname.name.ToLower().Contains(Droplist_Filter_Contains_Item.Text.ToLower())))
                                {
                                    foundItem = true;
                                    break;
                                }
                            }

                            return foundItem;
                        }
                    default:
                        break;
                }
            }

            if (Droplist_Filter_Custom_Toggle.IsChecked == true)
                if (!filteredItem.IsCustom)
                    return false;



            return true;

        }

        private void Droplist_Filter_Changed(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Droplists_View).Refresh();
        }


        private void Droplist_Filter_Changed(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Droplists_View).Refresh();
        }

        public void Refresh_Drop_Details(L2H_Droplist targetDroplist)
        {

            if (targetDroplist.Droplist_Type == L2H_Droplist_Type.single)
            {
                List<L2H_Drop_Details_Single> drops = new List<L2H_Drop_Details_Single>();

                for (int i = 0; i < targetDroplist.server_Droplist.itemDrops.Count; i++)
                {
                    drops.Add(new L2H_Drop_Details_Single()
                    {
                        L2H_Item = targetDroplist.server_Droplist.itemDrops[i].l2h_Item,
                        itemDrop = targetDroplist.server_Droplist.itemDrops[i]
                    });
                }
                Droplists_Drops_Listview.ItemsSource = drops;
                Droplists_Details_Grid_Multi_Drops_Grid.Visibility = Visibility.Hidden;
                Droplists_Details_Grid_Single_Drops_Grid.Visibility = Visibility.Visible;
                Droplists_Details_Grid_Single_Drops_Add_Single_Drop_Button.Visibility = Visibility.Visible;
                Droplists_Details_Grid_Connected_Droplists_Add_Connection_Button.Visibility = Visibility.Hidden;
            }
            else
            {

                List<L2H_Drop_Details_Multi> drop_Details_Multis = new List<L2H_Drop_Details_Multi>();

                for (int i = 0; i < targetDroplist.ConnectedDroplists.Count; i++)
                {
                    if (targetDroplist.ConnectedDroplists[i].Droplist_Type == L2H_Droplist_Type.single)
                    {
                        L2H_Drop_Details_Multi new_Details = new L2H_Drop_Details_Multi()
                        {
                            L2H_Droplist = targetDroplist.ConnectedDroplists[i],
                            Chance = targetDroplist.server_Multi_Droplist.separateDroplistChances[i],
                            ID = targetDroplist.server_Multi_Droplist.separateDroplistIDs[i]
                        };

                        List<L2H_Drop_Details_Single> drops = new List<L2H_Drop_Details_Single>();

                        for (int j = 0; j < new_Details.L2H_Droplist.server_Droplist.itemDrops.Count; j++)
                        {
                            drops.Add(new L2H_Drop_Details_Single()
                            {
                                L2H_Item = new_Details.L2H_Droplist.server_Droplist.itemDrops[j].l2h_Item,
                                itemDrop = new_Details.L2H_Droplist.server_Droplist.itemDrops[j]
                            });
                        }

                        new_Details.drops_details = drops;
                        drop_Details_Multis.Add(new_Details);

                    }
                }


                Droplists_Details_Grid_Multi_Drops_Listview.ItemsSource = drop_Details_Multis;
                Droplists_Details_Grid_Single_Drops_Grid.Visibility = Visibility.Hidden;
                Droplists_Details_Grid_Multi_Drops_Grid.Visibility = Visibility.Visible;
                Droplists_Details_Grid_Single_Drops_Add_Single_Drop_Button.Visibility = Visibility.Hidden;
                Droplists_Details_Grid_Connected_Droplists_Add_Connection_Button.Visibility = Visibility.Visible;
            }

            List<L2H_Droplist_Connection> droplist_Connections = new List<L2H_Droplist_Connection>();
            List<L2H_Droplist_NPC_Connection> NPC_Connections = new List<L2H_Droplist_NPC_Connection>();
            for (int i = 0; i < targetDroplist.ConnectedDroplists.Count; i++)
            {
                droplist_Connections.Add(new L2H_Droplist_Connection() { L2H_Droplist = targetDroplist.ConnectedDroplists[i] });
            }

            for (int i = 0; i < targetDroplist.ConnectedNpcs.Count; i++)
            {
                NPC_Connections.Add(new L2H_Droplist_NPC_Connection() { L2H_Npc = targetDroplist.ConnectedNpcs[i] });
            }

            Droplists_Connected_Droplists_Listview.ItemsSource = droplist_Connections;
            Droplists_Connected_NPCs_Listview.ItemsSource = NPC_Connections;

            Droplists_Details_Grid.Visibility = Visibility.Visible;

            CollectionViewSource.GetDefaultView(Droplists_View).Refresh();
            CollectionViewSource.GetDefaultView(Droplists_Connected_Droplists_Listview.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(Droplists_Connected_NPCs_Listview.ItemsSource).Refresh();

            Droplist_Details_Title_Text.Text = "Details: " + active_L2H_Droplist.ID;
        }

        private void Droplist_Name_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();

            var vm = (sender as FrameworkElement).DataContext;
            L2H_Droplist droplistClicked = vm as L2H_Droplist;

            if (active_L2H_Droplist_Connection != null)
            {
                active_L2H_Droplist_Connection.IsSelected = false;
                active_L2H_Droplist_Connection = null;
            }

            if (active_L2H_Droplist != null)
            {
                if (active_L2H_Droplist == droplistClicked)
                {
                    if (!active_L2H_Droplist.IsSelected)
                    {
                        Droplists_Details_Grid.Visibility = Visibility.Hidden;
                        active_L2H_Droplist.IsSelected = false;
                        active_L2H_Droplist = null;
                        return;
                    }
                    else
                    {
                        Droplists_Details_Grid.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    active_L2H_Droplist.IsSelected = false;
                    active_L2H_Droplist = droplistClicked;
                    active_L2H_Droplist.IsSelected = true;
                }
            }
            else
            {
                active_L2H_Droplist = droplistClicked;
                active_L2H_Droplist.IsSelected = true;
            }


            Refresh_Drop_Details(active_L2H_Droplist);


        }

        private void Droplist_Connection_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Droplist_Connection droplist_Connection_Clicked = vm as L2H_Droplist_Connection;

            if (active_L2H_Droplist_Connection != null)
            {
                if (active_L2H_Droplist_Connection == droplist_Connection_Clicked)
                {
                    if (!active_L2H_Droplist_Connection.IsSelected)
                    {
                        Droplists_Details_Grid_Single_Drops_Grid.Visibility = Visibility.Hidden;
                        active_L2H_Droplist_Connection.IsSelected = false;
                        active_L2H_Droplist_Connection = null;
                        return;
                    }
                    else
                    {
                        Droplists_Details_Grid_Single_Drops_Grid.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    active_L2H_Droplist_Connection.IsSelected = false;
                    active_L2H_Droplist_Connection = droplist_Connection_Clicked;
                    active_L2H_Droplist_Connection.IsSelected = true;
                }
            }
            else
            {
                active_L2H_Droplist_Connection = droplist_Connection_Clicked;
                active_L2H_Droplist_Connection.IsSelected = true;
            }


            Refresh_Drop_Details(active_L2H_Droplist_Connection.L2H_Droplist);

            CollectionViewSource.GetDefaultView(Droplists_Connected_Droplists_Listview.ItemsSource).Refresh();

            Droplist_Details_Title_Text.Text = "Details: " + active_L2H_Droplist_Connection.L2H_Droplist.ID;

        }

        private void Droplist_Multi_Remove_Single_Droplist_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Droplist droplistToRemove = (vm as L2H_Drop_Details_Multi).L2H_Droplist;
            L2H_Droplist droplistToRemoveFrom;

            if (active_L2H_Droplist.Droplist_Type == L2H_Droplist_Type.multi)
            {
                droplistToRemoveFrom = active_L2H_Droplist;
            }
            else
            {
                droplistToRemoveFrom = active_L2H_Droplist_Connection.L2H_Droplist;
            }

            int indexToRemove = droplistToRemoveFrom.ConnectedDroplists.FindIndex(x => x == droplistToRemove);

            droplistToRemoveFrom.ConnectedDroplists.RemoveAt(indexToRemove);
            droplistToRemoveFrom.server_Multi_Droplist.separateDroplistChances.RemoveAt(indexToRemove);
            droplistToRemoveFrom.server_Multi_Droplist.separateDroplistIDs.RemoveAt(indexToRemove);

            if (active_L2H_Droplist.Droplist_Type == L2H_Droplist_Type.multi)
                Refresh_Drop_Details(active_L2H_Droplist);
            else
                Refresh_Drop_Details(active_L2H_Droplist_Connection.L2H_Droplist);

        }

        private void ToggleSearchVisibility(object sender, RoutedEventArgs e)
        {
            if (searchIsShowing)
            {
                Droplists_Main_Grid.ColumnDefinitions[0].Width = new GridLength(0);
                Droplists_Main_Grid.ColumnDefinitions[1].Width = new GridLength(0);
                searchIsShowing = false;
                SearchIsShowingButton.Content = "Show Search";
            }
            else
            {
                Droplists_Main_Grid.ColumnDefinitions[0].Width = new GridLength(220);
                Droplists_Main_Grid.ColumnDefinitions[1].Width = new GridLength(220);
                searchIsShowing = true;
                SearchIsShowingButton.Content = "Hide Search";
            }
        }

        private void Droplist_NPC_Connection_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Droplist_NPC_Connection npc_Connection = vm as L2H_Droplist_NPC_Connection;

            if (npc_Connection != null)
            {
                (mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage).forceWaiting = true;
                (mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage).Force_NPC_Selection(npc_Connection.L2H_Npc);
                mainWindow.Category_Force(MenuCategories.NPCs);

            }
        }

        private void Droplist_Filter_Droplist_Type_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            ToggleButton tb = sender as ToggleButton;

            if (active_Filter_Droplist_Type != null)
            {
                active_Filter_Droplist_Type.IsChecked = false;
                active_Filter_Droplist_Type = tb;
                tb.IsChecked = true;
            }
            else
                active_Filter_Droplist_Type = tb;

            CollectionViewSource.GetDefaultView(Droplists_View).Refresh();
        }

        private void Droplists_Details_Grid_Single_Drops_Add_Single_Drop_Button_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();

            Popup_Item_Selection item_Selection_Popup = new Popup_Item_Selection((mainWindow.GetPageOfType(typeof(ItemsPage)) as ItemsPage).L2H_Items);

            if (active_L2H_Droplist.Droplist_Type == L2H_Droplist_Type.multi)
            {
                item_Selection_Popup.Initialize_For_Droplists(active_L2H_Droplist_Connection.L2H_Droplist);
            }
            else
            {
                item_Selection_Popup.Initialize_For_Droplists(active_L2H_Droplist);
            }
        }

        private void Droplists_Details_Grid_Connected_Droplists_Add_Connection_Button_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            Popup_Droplist_Selection selection_Popup = new Popup_Droplist_Selection(L2H_Droplists);
            if (active_L2H_Droplist.Droplist_Type == L2H_Droplist_Type.multi)
            {
                selection_Popup.Initialize_For_Picking_Single_Droplist(active_L2H_Droplist);
            }
            else
            {
                selection_Popup.Initialize_For_Picking_Single_Droplist(active_L2H_Droplist_Connection.L2H_Droplist);
            }

        }

        private void Create_Multi_Droplist_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            Server_Multi_Droplist new_Server_Multi_Droplist = new Server_Multi_Droplist(Get_Unique_Custom_Droplist_ID(true), true);

            L2H_Droplist new_L2H_Droplist = new L2H_Droplist()
            {
                ID = new_Server_Multi_Droplist.id,
                IsCustom = new_Server_Multi_Droplist.isCustom,
                IsSelected = false,
                Droplist_Type = L2H_Droplist_Type.multi,
                server_Multi_Droplist = new_Server_Multi_Droplist,
                ConnectedDroplists = new List<L2H_Droplist>(),
                ConnectedNpcs = new List<L2H_NPC>()
            };

            new_Server_Multi_Droplist.L2H_Droplist = new_L2H_Droplist;

            L2H_Log.Instance.Log_Droplist_Add_Droplist(new_L2H_Droplist);

            L2H_Droplists.Add(new_L2H_Droplist);

            //Droplists_Name_Listview.ItemsSource = L2H_Droplists;
            Droplist_Filter_Hide_Empty.IsChecked = false;

            if (active_L2H_Droplist != null)
                active_L2H_Droplist.IsSelected = false;

            active_L2H_Droplist = new_L2H_Droplist;
            new_L2H_Droplist.IsSelected = true;

            CollectionViewSource.GetDefaultView(Droplists_View).Refresh();

            Droplists_Name_Listview.SelectedIndex = Droplists_Name_Listview.Items.Count - 1;


            if (VisualTreeHelper.GetChildrenCount(Droplists_Name_Listview) > 0)
            {
                Border border = (Border)VisualTreeHelper.GetChild(Droplists_Name_Listview, 0);
                ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            }


            CollectionViewSource.GetDefaultView(Droplists_View).Refresh();
            Refresh_Drop_Details(new_L2H_Droplist);
        }

        private void Create_Single_Droplist_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            Server_Droplist new_Server_Single_Droplist = new Server_Droplist(Get_Unique_Custom_Droplist_ID(false), true);

            L2H_Droplist new_L2H_Droplist = new L2H_Droplist()
            {
                ID = new_Server_Single_Droplist.id,
                IsCustom = new_Server_Single_Droplist.isCustom,
                IsSelected = false,
                Droplist_Type = L2H_Droplist_Type.single,
                server_Droplist = new_Server_Single_Droplist,
                ConnectedDroplists = new List<L2H_Droplist>(),
                ConnectedNpcs = new List<L2H_NPC>()
            };

            new_Server_Single_Droplist.L2H_Droplist = new_L2H_Droplist;

            L2H_Log.Instance.Log_Droplist_Add_Droplist(new_L2H_Droplist);

            L2H_Droplists.Add(new_L2H_Droplist);

            Droplist_Filter_Hide_Empty.IsChecked = false;

            if (active_L2H_Droplist != null)
                active_L2H_Droplist.IsSelected = false;

            active_L2H_Droplist = new_L2H_Droplist;
            new_L2H_Droplist.IsSelected = true;

            CollectionViewSource.GetDefaultView(Droplists_View).Refresh();

            Droplists_Name_Listview.SelectedIndex = Droplists_Name_Listview.Items.Count - 1;


            if (VisualTreeHelper.GetChildrenCount(Droplists_Name_Listview) > 0)
            {
                Border border = (Border)VisualTreeHelper.GetChild(Droplists_Name_Listview, 0);
                ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            }


            CollectionViewSource.GetDefaultView(Droplists_View).Refresh();
            Refresh_Drop_Details(new_L2H_Droplist);
        }

        string Get_Unique_Custom_Droplist_ID(bool multi, int currentValue = 0, L2H_Droplist parentDroplist = null)
        {
            if (parentDroplist != null)
            {
                int suffix_value = 0;

                if (L2H_Droplists.Exists(x => x.ID == (parentDroplist.ID + "_" + suffix_value)))
                {
                    suffix_value = suffix_value + 1;
                    return Get_Unique_Custom_Droplist_ID(multi, suffix_value, parentDroplist);
                }
                else
                    return parentDroplist.ID + "_" + suffix_value;
            }
            else
            {
                string name_suffix = "_single_custom";

                if (multi)
                    name_suffix = "_multi_custom";

                if (currentValue == 0)
                    currentValue = 50000;
                else
                    currentValue = currentValue + 1;


                if (L2H_Droplists.Exists(x => x.ID == (currentValue.ToString() + name_suffix)))
                    return Get_Unique_Custom_Droplist_ID(multi, currentValue);
                else
                    return currentValue.ToString() + name_suffix;
            }
        }

        private void Droplists_Details_Grid_Go_To_Item_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Drop_Details_Single item_Go_To_Clicked = vm as L2H_Drop_Details_Single;

            if (item_Go_To_Clicked != null)
            {
                (mainWindow.GetPageOfType(typeof(ItemsPage)) as ItemsPage).forceWaiting = true;
                (mainWindow.GetPageOfType(typeof(ItemsPage)) as ItemsPage).Force_Item_Selection(item_Go_To_Clicked.L2H_Item);

                mainWindow.Category_Force(MenuCategories.Items);
            }
        }

        private void Droplist_Single_Droplist_Remove_Drop_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Item item = vm as L2H_Item;
            L2H_Log.Instance.Log_Droplist_Remove_Single_Drop(active_L2H_Droplist, item);
            active_L2H_Droplist.server_Droplist.itemDrops.Remove(active_L2H_Droplist.server_Droplist.itemDrops.Find(x => x.l2h_Item == item));
            Refresh_Drop_Details(active_L2H_Droplist);
        }

        public void Force_Droplist_Selection(string droplistID)
        {
            L2H_Droplist droplist = L2H_Droplists.Find(x => x.ID == droplistID);
            if (droplist == null)
                return;

            if (active_L2H_Droplist != null)
            {
                if (active_L2H_Droplist == droplist)
                {
                    if (!active_L2H_Droplist.IsSelected)
                    {
                        Droplists_Details_Grid.Visibility = Visibility.Hidden;
                        active_L2H_Droplist.IsSelected = false;
                        active_L2H_Droplist = null;
                        return;
                    }
                    else
                    {
                        Droplists_Details_Grid.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    active_L2H_Droplist.IsSelected = false;
                    active_L2H_Droplist = droplist;
                    active_L2H_Droplist.IsSelected = true;
                }
            }
            else
            {
                active_L2H_Droplist = droplist;
                active_L2H_Droplist.IsSelected = true;
            }


            Refresh_Drop_Details(active_L2H_Droplist);
        }

        private void Droplist_Force_Select_Waiting(object sender, RoutedEventArgs e)
        {
            if (forceWaiting)
            {
                Refresh_Drop_Details(active_L2H_Droplist);
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

    public class ThreadWorker_Hook_Droplists_To_Multi_Droplists
    {
        public event EventHandler ThreadDone;

        public int startIndex;
        public int endIndex;
        public List<Server_Multi_Droplist> multi_Droplist_Data;
        public List<Server_Droplist> single_Droplist_Data;


        public ThreadWorker_Hook_Droplists_To_Multi_Droplists()
        {

        }

        public void ThreadProc()
        {

            if (endIndex > multi_Droplist_Data.Count - 1)
                endIndex = multi_Droplist_Data.Count - 1;

            for (int i = startIndex; i < endIndex; i++)
            {
                for (int j = 0; j < multi_Droplist_Data[i].separateDroplistIDs.Count; j++)
                {
                    multi_Droplist_Data[i].separateDroplists.Add(single_Droplist_Data.Find(x => x.id == multi_Droplist_Data[i].separateDroplistIDs[j]));
                }
            }

            if (ThreadDone != null)
            {
                ThreadDone(this, EventArgs.Empty);
            }
        }
    }

    public class ThreadWorker_Hook_Items_To_Single_Droplists
    {
        public event EventHandler ThreadDone;

        public int startIndex;
        public int endIndex;
        public List<L2H_Item> L2H_Items;
        public List<Server_Droplist> single_Droplist_Data;



        public ThreadWorker_Hook_Items_To_Single_Droplists()
        {

        }

        public void ThreadProc()
        {

            if (endIndex > single_Droplist_Data.Count - 1)
                endIndex = single_Droplist_Data.Count - 1;

            for (int i = startIndex; i < endIndex; i++)
            {
                for (int j = 0; j < single_Droplist_Data[i].itemDrops.Count; j++)
                {
                    single_Droplist_Data[i].itemDrops[j].l2h_Item = L2H_Items.Find(x => x.server_Itemdata.itemName == single_Droplist_Data[i].itemDrops[j].itemID);
                }
            }

            if (ThreadDone != null)
            {
                ThreadDone(this, EventArgs.Empty);
            }
        }
    }

    public class ThreadWorker_Hook_NPCs_To_Droplists
    {
        public event EventHandler ThreadDone;

        public int startIndex;
        public int endIndex;
        public List<L2H_Droplist> l2H_Droplists;
        public List<L2H_Npc_Droplist_Pointer> npc_Droplist_Pointers;
        public List<L2H_NPC> L2H_Npcs;

        public ThreadWorker_Hook_NPCs_To_Droplists()
        {

        }

        public void ThreadProc()
        {

            if (endIndex > npc_Droplist_Pointers.Count - 1)
                endIndex = npc_Droplist_Pointers.Count;

            for (int i = startIndex; i < endIndex; i++)
            {
                L2H_NPC targetNpc = L2H_Npcs.Find(x => x.ID.ToString() == npc_Droplist_Pointers[i].npc_id);


                L2H_Droplist normalDroplist = l2H_Droplists.Find(x => x.ID == npc_Droplist_Pointers[i].droplist_normal_id);
                normalDroplist.ConnectedNpcs.Add(targetNpc);
                targetNpc.server_Npcdata.additional_make_list_droplist = normalDroplist.server_Droplist;

                L2H_Droplist spoilDroplist = l2H_Droplists.Find(x => x.ID == npc_Droplist_Pointers[i].droplist_spoil_id);
                spoilDroplist.ConnectedNpcs.Add(targetNpc);
                targetNpc.server_Npcdata.corpse_make_list_droplist = spoilDroplist.server_Droplist;


                L2H_Droplist multi_Droplist = l2H_Droplists.Find(x => x.ID == npc_Droplist_Pointers[i].droplist_multi_id);
                multi_Droplist.ConnectedNpcs.Add(targetNpc);
                targetNpc.server_Npcdata.additional_make_multi_list_droplist = multi_Droplist.server_Multi_Droplist;


                for (int j = 0; j < multi_Droplist.server_Multi_Droplist.separateDroplists.Count; j++)
                {
                    if (!multi_Droplist.ConnectedDroplists.Contains(multi_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist))
                    {
                        multi_Droplist.ConnectedDroplists.Add(multi_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist);
                        if (!multi_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist.ConnectedNpcs.Contains(targetNpc))
                            multi_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist.ConnectedNpcs.Add(targetNpc);
                    }
                    if (!multi_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist.ConnectedDroplists.Contains(multi_Droplist))
                    {
                        multi_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist.ConnectedDroplists.Add(multi_Droplist);
                        if (!multi_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist.ConnectedNpcs.Contains(targetNpc))
                            multi_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist.ConnectedNpcs.Add(targetNpc);
                    }
                }

                L2H_Droplist extra_Droplist = l2H_Droplists.Find(x => x.ID == npc_Droplist_Pointers[i].droplist_extra_id);
                extra_Droplist.ConnectedNpcs.Add(targetNpc);
                targetNpc.server_Npcdata.ex_item_drop_list_droplist = extra_Droplist.server_Multi_Droplist;

                for (int j = 0; j < extra_Droplist.server_Multi_Droplist.separateDroplists.Count; j++)
                {
                    if (!extra_Droplist.ConnectedDroplists.Contains(extra_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist))
                    {
                        extra_Droplist.ConnectedDroplists.Add(extra_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist);
                        if (!extra_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist.ConnectedNpcs.Contains(targetNpc))
                            extra_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist.ConnectedNpcs.Add(targetNpc);
                    }
                    if (!extra_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist.ConnectedDroplists.Contains(extra_Droplist))
                    {
                        extra_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist.ConnectedDroplists.Add(extra_Droplist);
                        if (!extra_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist.ConnectedNpcs.Contains(targetNpc))
                            extra_Droplist.server_Multi_Droplist.separateDroplists[j].L2H_Droplist.ConnectedNpcs.Add(targetNpc);
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


