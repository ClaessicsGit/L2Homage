using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ItemsPage.xaml
    /// </summary>
    public partial class ItemsPage : Page
    {
        MainWindow mainWindow;

        public List<Server_Itemdata> itemdata;
        public List<Client_Armor> client_Armors;
        public List<Client_Weapon> client_Weapons;
        public List<Client_Etc> client_Etcs;
        public List<Client_Itemname> client_Itemnames;

        L2H_Item active_L2H_Item;
        L2H_Set active_L2H_Set;

        List<L2H_Item> weapons;
        List<L2H_Item> armors;
        List<L2H_Item> etcs;

        public List<L2H_Item> L2H_Items;
        public List<L2H_Set> L2H_Sets;
        ToggleButton active_Item_Type_Toggle;
        ToggleButton active_Weapon_Subtype_Toggle;
        ToggleButton active_Armor_Subtype_Toggle;
        ToggleButton active_Armor_BodyPart_Toggle;
        ToggleButton active_Etc_Subtype_Toggle;

        public List<L2H_Template_Pointer> item_Template_Pointers;
        public List<L2H_Template_Pointer> set_Template_Pointers;

        public bool itemdataIsLoaded = false;
        public bool weaponsLoaded = false;

        public bool itemdataIsLoading = false;
        public bool AllItemsAreLoaded = false;
        public bool AllSetsLoaded = false;

        bool searchIsShowing = true;

        string existingValue;


        int clientFilesToLoad = 0;
        int clientFilesLoaded = 0;

        public string itemNamesStartLine;
        public string weaponsStartLine;
        public string armorsStartLine;
        public string etcsStartLine;

        public bool forceWaiting = false;
        //public List<Item>
        public ItemsPage()
        {
            InitializeComponent();

            itemdata = new List<Server_Itemdata>();
            client_Armors = new List<Client_Armor>();
            client_Weapons = new List<Client_Weapon>();
            client_Etcs = new List<Client_Etc>();
            client_Itemnames = new List<Client_Itemname>();
            weapons = new List<L2H_Item>();
            armors = new List<L2H_Item>();
            etcs = new List<L2H_Item>();

            item_Template_Pointers = new List<L2H_Template_Pointer>();
            set_Template_Pointers = new List<L2H_Template_Pointer>();

            L2H_Items = new List<L2H_Item>();
            L2H_Sets = new List<L2H_Set>();

            mainWindow = Application.Current.MainWindow as MainWindow;

            active_Item_Type_Toggle = Item_Type_All_Button;
            active_Armor_Subtype_Toggle = Subtype_Armor_SubType_All_Toggle;
            active_Armor_BodyPart_Toggle = Subtype_Armor_BodyPart_All_Toggle;
            active_Weapon_Subtype_Toggle = Subtype_Weapon_All_Toggle;
            active_Etc_Subtype_Toggle = Subtype_Etc_All_Toggle;
            Item_Name_Listview.Visibility = Visibility.Visible;





        }

        private void Item_Type_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton t = (ToggleButton)sender;

            if (Keyboard.FocusedElement != null)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;
                Keyboard.ClearFocus();
                focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
            }

            if (active_Item_Type_Toggle != null)
            {
                if (active_Item_Type_Toggle == t)
                {
                    t.IsChecked = true;
                    return;
                }

                if (active_Item_Type_Toggle.IsChecked == true)
                    active_Item_Type_Toggle.IsChecked = false;
                else
                {
                    active_Item_Type_Toggle.IsChecked = true;
                }
            }

            switch (t.Tag)
            {
                case "All":
                    {
                        if (t.IsChecked == true)
                        {
                            Item_Type_Armors_Button.IsChecked = false;
                            Item_Type_Weapons_Button.IsChecked = false;
                            Item_Type_Etcs_Button.IsChecked = false;
                            Item_Type_Sets_Button.IsChecked = false;

                            Filter_Weapon_Subtypes_Grid.Visibility = Visibility.Hidden;
                            Filter_Armor_Subtypes_Grid.Visibility = Visibility.Hidden;
                            Filter_Etc_Subtypes_Grid.Visibility = Visibility.Hidden;
                            Toggle_Grid_Details(false);
                        }
                        Item_Name_Listview.Visibility = Visibility.Visible;
                        Item_Set_Listview.Visibility = Visibility.Hidden;
                    }
                    break;
                case "Weapons":
                    {
                        if (t.IsChecked == true)
                        {
                            Item_Type_All_Button.IsChecked = false;
                            Item_Type_Armors_Button.IsChecked = false;
                            Item_Type_Etcs_Button.IsChecked = false;
                            Item_Type_Sets_Button.IsChecked = false;

                            Filter_Weapon_Subtypes_Grid.Visibility = Visibility.Visible;
                            Filter_Armor_Subtypes_Grid.Visibility = Visibility.Hidden;
                            Filter_Etc_Subtypes_Grid.Visibility = Visibility.Hidden;
                            Toggle_Grid_Details(false);
                        }
                        else
                        {
                            Filter_Weapon_Subtypes_Grid.Visibility = Visibility.Hidden;
                        }
                        Item_Name_Listview.Visibility = Visibility.Visible;
                        Item_Set_Listview.Visibility = Visibility.Hidden;
                    }
                    break;
                case "Armors":
                    {
                        if (t.IsChecked == true)
                        {
                            Item_Type_All_Button.IsChecked = false;
                            Item_Type_Weapons_Button.IsChecked = false;
                            Item_Type_Etcs_Button.IsChecked = false;
                            Item_Type_Sets_Button.IsChecked = false;
                            Filter_Weapon_Subtypes_Grid.Visibility = Visibility.Hidden;
                            Filter_Armor_Subtypes_Grid.Visibility = Visibility.Visible;
                            Filter_Etc_Subtypes_Grid.Visibility = Visibility.Hidden;
                            Toggle_Grid_Details(false);
                        }
                        else
                        {
                            Filter_Armor_Subtypes_Grid.Visibility = Visibility.Hidden;
                        }
                        Item_Name_Listview.Visibility = Visibility.Visible;
                        Item_Set_Listview.Visibility = Visibility.Hidden;
                    }
                    break;
                case "Etcs":
                    {
                        if (t.IsChecked == true)
                        {
                            Item_Type_All_Button.IsChecked = false;
                            Item_Type_Armors_Button.IsChecked = false;
                            Item_Type_Weapons_Button.IsChecked = false;
                            Item_Type_Sets_Button.IsChecked = false;

                            Filter_Weapon_Subtypes_Grid.Visibility = Visibility.Hidden;
                            Filter_Armor_Subtypes_Grid.Visibility = Visibility.Hidden;
                            Filter_Etc_Subtypes_Grid.Visibility = Visibility.Visible;

                            Toggle_Grid_Details(false);

                            Item_Details_Sets_Grid.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            Filter_Etc_Subtypes_Grid.Visibility = Visibility.Hidden;
                        }
                        Item_Name_Listview.Visibility = Visibility.Visible;
                        Item_Set_Listview.Visibility = Visibility.Hidden;
                    }
                    break;
                case "Sets":
                    {
                        if (t.IsChecked == true)
                        {
                            Item_Type_All_Button.IsChecked = false;
                            Item_Type_Armors_Button.IsChecked = false;
                            Item_Type_Weapons_Button.IsChecked = false;
                            Item_Type_Etcs_Button.IsChecked = false;

                            Toggle_Grid_Details(true);

                            Filter_Weapon_Subtypes_Grid.Visibility = Visibility.Hidden;
                            Filter_Armor_Subtypes_Grid.Visibility = Visibility.Hidden;
                            Filter_Etc_Subtypes_Grid.Visibility = Visibility.Hidden;
                        }
                        else
                        {

                        }
                        Item_Name_Listview.Visibility = Visibility.Hidden;
                        Item_Set_Listview.Visibility = Visibility.Visible;
                    }
                    break;
                default:
                    break;
            }

            active_Item_Type_Toggle = t;

            if (active_Item_Type_Toggle == Item_Type_Sets_Button)
                Item_Filter_Name.IsEnabled = false;
            else
                Item_Filter_Name.IsEnabled = true;

            CollectionViewSource.GetDefaultView(Item_Name_Listview.ItemsSource).Refresh();

        }

        public void LoadItems()
        {
            if (!AllItemsAreLoaded && !itemdataIsLoading)
            {
                itemdataIsLoading = true;

                UpdateLog("Initializing Thread: Load Itemdata.txt and Itemnames-e.txt..", L2H_Constants.Color_Log_Thread_Begin);

                itemdata.Clear();
                client_Itemnames.Clear();

                clientFilesToLoad++;
                Thread_Load_Itemdata_Itemnames thread_Load_Itemdata_Itemnames = new Thread_Load_Itemdata_Itemnames(itemdata, client_Itemnames);
                thread_Load_Itemdata_Itemnames.ThreadDone += (sender, e) => HandleThreadDone(sender, e, "Server Itemdata Entries Loaded: " + itemdata.Count + "\nClient Itemnames Loaded: " + client_Itemnames.Count, LoadedTypes.Empty, LoadClientFiles);
                Thread thread_itemdata_itemnames = new Thread(new ThreadStart(thread_Load_Itemdata_Itemnames.ThreadProc));
                thread_itemdata_itemnames.Start();

            }


        }

        private void LoadClientFiles(object sender, EventArgs e)
        {
            LoadStartLines();

            //Preparing threads
            //Weapons
            UpdateLog("Initializing Thread: Load Client Weapon Data..", L2H_Constants.Color_Log_Thread_Begin);
            clientFilesToLoad++;
            Thread_LoadClientWeapons thread_LoadClientWeapons = new Thread_LoadClientWeapons(itemdata, client_Itemnames, client_Weapons, item_Template_Pointers, weapons);
            thread_LoadClientWeapons.ThreadDone += (b, bb) => HandleThreadDone(b, bb, "Client Weapons Loaded: " + client_Weapons.Count, LoadedTypes.Weapons, HandleLoadClientFilesDone);
            Thread t = new Thread(new ThreadStart(thread_LoadClientWeapons.ThreadProc));

            //Armors
            UpdateLog("Initializing Thread: Load Client Armor Data..", L2H_Constants.Color_Log_Thread_Begin);
            clientFilesToLoad++;
            Thread_LoadClientArmors thread_LoadClientArmors = new Thread_LoadClientArmors(itemdata, client_Itemnames, client_Armors, item_Template_Pointers, armors);
            thread_LoadClientArmors.ThreadDone += (b, bb) => HandleThreadDone(b, bb, "Client Armors Loaded: " + client_Armors.Count, LoadedTypes.Armors, HandleLoadClientFilesDone);
            Thread t2 = new Thread(new ThreadStart(thread_LoadClientArmors.ThreadProc));

            //Etcs
            UpdateLog("Initializing Thread: Load Client Etc Data..", L2H_Constants.Color_Log_Thread_Begin);
            clientFilesToLoad++;
            Thread_LoadClientEtcs thread_LoadClientEtcs = new Thread_LoadClientEtcs(itemdata, client_Itemnames, client_Etcs, item_Template_Pointers, etcs);
            thread_LoadClientEtcs.ThreadDone += (b, bb) => HandleThreadDone(b, bb, "Client Etcs Loaded: " + client_Etcs.Count, LoadedTypes.Etcs, HandleLoadClientFilesDone);
            Thread t3 = new Thread(new ThreadStart(thread_LoadClientEtcs.ThreadProc));




            //Starting threads
            t.Start();
            t2.Start();
            t3.Start();
        }


        public void LoadSets()
        {
            //Sets
            UpdateLog("Initializing Thread: Load Item Sets..", L2H_Constants.Color_Log_Thread_Begin);
            clientFilesToLoad++;
            Thread_LoadSets thread_LoadSets = new Thread_LoadSets(L2H_Items, L2H_Sets, itemdata, client_Itemnames, item_Template_Pointers, set_Template_Pointers);
            thread_LoadSets.ThreadDone += (b, bb) => HandleThreadDone(b, bb, "Item Sets Loaded: " + L2H_Sets.Count, LoadedTypes.Sets);
            Thread t4 = new Thread(new ThreadStart(thread_LoadSets.ThreadProc));

            t4.Start();
        }

        void LoadStartLines()
        {
            weaponsStartLine = File.ReadLines(L2H_Constants.client_Weapons_Path).First();
            armorsStartLine = File.ReadLines(L2H_Constants.client_Armors_Path).First();
            etcsStartLine = File.ReadLines(L2H_Constants.client_Etcs_Path).First();
            itemNamesStartLine = File.ReadLines(L2H_Constants.client_Itemnames_Path).First();
        }

        private void HandleThreadDone(object sender, EventArgs e, string logMessage, LoadedTypes loadedTypes = LoadedTypes.Empty, EventHandler eh = null)
        {
            UpdateLog(logMessage, L2H_Constants.Color_Add);

            clientFilesLoaded++;

            if (eh != null)
            {
                eh.Invoke(sender, e);
            }

            if (loadedTypes != LoadedTypes.Empty)
            {
                Dispatcher.Invoke(() =>
                {
                    mainWindow.UpdateLoadedNumber(loadedTypes);
                });

                if (loadedTypes == LoadedTypes.Sets)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Item_Set_Listview.ItemsSource = L2H_Sets;
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Item_Set_Listview.ItemsSource);
                        view.Filter = Filter_Set_Name;


                        AllSetsLoaded = true;
                    });
                }
            }




        }

        private void HandleLoadClientFilesDone(object sender, EventArgs e)
        {


            if (clientFilesLoaded == clientFilesToLoad)
            {
                UpdateLog("Client item files loaded, parsing to L2H format..", L2H_Constants.Color_Log_Thread_Begin);

                L2H_Items.AddRange(weapons);
                L2H_Items.AddRange(armors);
                L2H_Items.AddRange(etcs);

                L2H_Items = L2H_Items.OrderBy(x => x != null ? x.client_Itemname.name : null).ToList();


                BindData();

                UpdateLog("Client item files to L2H format parsing complete.", L2H_Constants.Color_Add);



                //AllItemsAreLoaded = true;
                itemdataIsLoading = false;
            }

        }


        public void BindData()
        {
            Dispatcher.Invoke(() =>
            {
                var newView = new CollectionViewSource() { Source = L2H_Items };
                var listViewCollection2 = (ListCollectionView)newView.View;
                listViewCollection2.Filter = Filter_Item_Name;
                Item_Name_Listview.ItemsSource = listViewCollection2;
                CollectionViewSource.GetDefaultView(Item_Name_Listview.ItemsSource).Refresh();


                AllItemsAreLoaded = true;
            });

        }

        private bool Filter_Item_Name(object item)
        {
            L2H_Item filteredItem = item as L2H_Item;

            if (filteredItem == null)
                return false;

            bool subType_Active = false;
            bool subType_Selected = false;

            bool bodyPart_Active = false;
            bool bodyPart_Selected = false;

            if (Item_Type_All_Button.IsChecked == false)
            {
                if (Item_Type_Weapons_Button.IsChecked == true)
                {
                    if (filteredItem.item_Category != L2H_Item_Category.Weapon)
                        return false;

                    if (active_Weapon_Subtype_Toggle != null)
                    {
                        if (active_Weapon_Subtype_Toggle.Tag.ToString() != "all")
                        {
                            subType_Active = true;
                            subType_Selected = filteredItem.client_Weapon.weapon_type == active_Weapon_Subtype_Toggle.Tag.ToString();
                        }
                        else
                        {
                            subType_Active = true;
                            subType_Selected = true;
                        }
                    }

                }
                else if (Item_Type_Armors_Button.IsChecked == true)
                {
                    if (filteredItem.item_Category != L2H_Item_Category.Armor)
                        return false;

                    if (active_Armor_Subtype_Toggle != null)
                    {
                        if (active_Armor_Subtype_Toggle.Tag.ToString() != "all")
                        {
                            subType_Active = true;
                            subType_Selected = filteredItem.client_Armor.armor_type == active_Armor_Subtype_Toggle.Tag.ToString();
                        }
                        else
                        {
                            subType_Active = true;
                            subType_Selected = true;
                        }
                    }
                    if (active_Armor_BodyPart_Toggle != null)
                    {
                        if (active_Armor_BodyPart_Toggle.Tag.ToString() != "all")
                        {
                            bodyPart_Active = true;
                            if (active_Armor_BodyPart_Toggle.Tag.ToString() == "bracelet")
                                bodyPart_Selected = filteredItem.server_Itemdata.slot_bit_type.Contains("bracelet");
                            else
                                bodyPart_Selected = filteredItem.server_Itemdata.slot_bit_type == active_Armor_BodyPart_Toggle.Tag.ToString();
                        }
                        else
                        {
                            bodyPart_Active = true;
                            bodyPart_Selected = true;
                        }
                    }
                }
                else if (Item_Type_Etcs_Button.IsChecked == true)
                {
                    if (filteredItem.item_Category != L2H_Item_Category.Etc)
                        return false;

                    if (active_Etc_Subtype_Toggle != null)
                    {
                        if (active_Etc_Subtype_Toggle.Tag.ToString() != "all")
                        {
                            subType_Active = true;
                            if (active_Etc_Subtype_Toggle.Tag.ToString() == "crop")
                            {
                                if (filteredItem.server_Itemdata.etcitem_type == "crop" ||
                                    filteredItem.server_Itemdata.etcitem_type == "maturecrop")
                                    subType_Selected = true;
                            }
                            else if (active_Etc_Subtype_Toggle.Tag.ToString() == "ticket")
                            {
                                if (filteredItem.server_Itemdata.etcitem_type == "lotto" ||
                                    filteredItem.server_Itemdata.etcitem_type == "castle_guard" ||
                                    filteredItem.server_Itemdata.etcitem_type == "ticket_of_lord" ||
                                    filteredItem.server_Itemdata.etcitem_type == "race_ticket"
                                    )
                                    subType_Selected = true;
                            }
                            else if (active_Etc_Subtype_Toggle.Tag.ToString() == "rune")
                            {
                                if (filteredItem.server_Itemdata.etcitem_type == "rune" ||
                                    filteredItem.server_Itemdata.etcitem_type == "rune_select"
                                    )
                                    subType_Selected = true;
                            }
                            else if (active_Etc_Subtype_Toggle.Tag.ToString() == "scroll")
                            {
                                if (filteredItem.server_Itemdata.etcitem_type.Contains("enchant") || filteredItem.server_Itemdata.etcitem_type == "scroll")
                                    subType_Selected = true;
                            }
                            else if (active_Etc_Subtype_Toggle.Tag.ToString() == "seed")
                            {
                                if (filteredItem.server_Itemdata.etcitem_type == "seed" ||
                                    filteredItem.server_Itemdata.etcitem_type == "seed2"
                                    )
                                    subType_Selected = true;
                            }
                            else if (active_Etc_Subtype_Toggle.Tag.ToString() == "arrow")
                            {
                                if (filteredItem.server_Itemdata.etcitem_type == "arrow" ||
                                    filteredItem.server_Itemdata.etcitem_type == "bolt"
                                    )
                                    subType_Selected = true;
                            }
                            else
                            {
                                subType_Selected = filteredItem.server_Itemdata.etcitem_type == active_Etc_Subtype_Toggle.Tag.ToString();
                            }

                        }
                        else
                        {
                            subType_Active = true;
                            subType_Selected = true;
                        }
                    }
                }
            }

            if (subType_Active && !subType_Selected)
                return false;

            if (bodyPart_Active && !bodyPart_Selected)
                return false;

            if (Filter_Grade.SelectedIndex != 9)
                if (Filter_Grade.SelectedIndex != int.Parse(L2H_Parser.GetGradeIndexFromGradeString(filteredItem.server_Itemdata.crystal_type)))
                    return false;

            if (!string.IsNullOrEmpty(Item_Filter_ID.Text))
            {
                if (filteredItem.ID.ToString().IndexOf(Item_Filter_ID.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                {

                }
                else
                {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(Item_Filter_Name.Text))
            {
                return (filteredItem.client_Itemname.name.IndexOf(Item_Filter_Name.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (Item_Filter_Custom_Toggle.IsChecked == true)
            {
                if (!filteredItem.IsCustom)
                    return false;
            }

            return true;

        }

        private bool Filter_Set_Name(object set)
        {
            L2H_Set filteredItem = set as L2H_Set;

            if (filteredItem == null)
                return false;


            if (!string.IsNullOrEmpty(Item_Filter_ID.Text))
            {
                if (filteredItem.ID.ToString().IndexOf(Item_Filter_ID.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                {

                }
                else
                {
                    return false;
                }
            }

            if (Filter_Grade.SelectedIndex != 9)
            {
                if (filteredItem.slot_chest == null)
                    return false;
                else if (Filter_Grade.SelectedIndex != int.Parse(L2H_Parser.GetGradeIndexFromGradeString(filteredItem.slot_chest.server_Itemdata.crystal_type)))
                    return false;
            }

            return true;


        }


        private void UpdateLog(string content, string color = null)
        {
            Dispatcher.Invoke(() => { mainWindow.UpdateLog(content, color); });
        }


        private void Item_Filter_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Item_Name_Listview.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(Item_Set_Listview.ItemsSource).Refresh();
        }

        private void Item_Filter_Grade_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (Item_Name_Listview != null)
                CollectionViewSource.GetDefaultView(Item_Name_Listview.ItemsSource).Refresh();
            if (Item_Set_Listview != null)
                CollectionViewSource.GetDefaultView(Item_Set_Listview.ItemsSource).Refresh();
        }

        private void Item_Name_Listview_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Item_Set_Clicked(object sender, RoutedEventArgs e)
        {
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Set setClicked = vm as L2H_Set;

            if (Keyboard.FocusedElement != null)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;
                Keyboard.ClearFocus();
                focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
            }

            if (active_L2H_Set != null)
            {
                if (active_L2H_Set == setClicked)
                {
                    if (!active_L2H_Set.IsSelected)
                    {
                        Item_Details_Sets_Grid.Visibility = Visibility.Hidden;
                        active_L2H_Set.IsSelected = false;
                        active_L2H_Set = null;
                        Item_Details_Sets_Grid_Add_New_Set.Visibility = Visibility.Visible;
                        return;
                    }
                    else
                    {
                        Item_Details_Sets_Grid.Visibility = Visibility.Visible;
                        Item_Details_Sets_Grid_Add_New_Set.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    active_L2H_Set.IsSelected = false;
                    active_L2H_Set = setClicked;
                    active_L2H_Set.IsSelected = true;
                    Item_Details_Sets_Grid_Add_New_Set.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                active_L2H_Set = setClicked;
                active_L2H_Set.IsSelected = true;
            }

            RefreshGridInterface(L2H_Item_Category.Sets);


        }

        private void Item_Name_Clicked(object sender, RoutedEventArgs e)
        {

            if (Keyboard.FocusedElement != null)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;
                Keyboard.ClearFocus();
                focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
            }


            var vm = (sender as FrameworkElement).DataContext;
            L2H_Item itemClicked = vm as L2H_Item;


            if (active_L2H_Item != null)
            {
                if (active_L2H_Item == itemClicked)
                {
                    if (!active_L2H_Item.IsSelected)
                    {
                        Item_Details_Armor_Grid.Visibility = Visibility.Hidden;
                        Item_Details_Etc_Grid.Visibility = Visibility.Hidden;
                        Item_Details_Weapon_Grid.Visibility = Visibility.Hidden;
                        active_L2H_Item = null;
                        return;
                    }
                    else
                    {
                        switch (active_L2H_Item.item_Category)
                        {
                            case L2H_Item_Category.Weapon:
                                Item_Details_Armor_Grid.Visibility = Visibility.Hidden;
                                Item_Details_Etc_Grid.Visibility = Visibility.Hidden;
                                Item_Details_Weapon_Grid.Visibility = Visibility.Visible;
                                Item_Details_Sets_Grid.Visibility = Visibility.Hidden;
                                break;
                            case L2H_Item_Category.Armor:
                                Item_Details_Armor_Grid.Visibility = Visibility.Visible;
                                Item_Details_Etc_Grid.Visibility = Visibility.Hidden;
                                Item_Details_Weapon_Grid.Visibility = Visibility.Hidden;
                                Item_Details_Sets_Grid.Visibility = Visibility.Hidden;
                                break;
                            case L2H_Item_Category.Etc:
                                Item_Details_Armor_Grid.Visibility = Visibility.Hidden;
                                Item_Details_Etc_Grid.Visibility = Visibility.Visible;
                                Item_Details_Weapon_Grid.Visibility = Visibility.Hidden;
                                Item_Details_Sets_Grid.Visibility = Visibility.Hidden;
                                break;
                            case L2H_Item_Category.Sets:
                                Item_Details_Armor_Grid.Visibility = Visibility.Hidden;
                                Item_Details_Etc_Grid.Visibility = Visibility.Hidden;
                                Item_Details_Weapon_Grid.Visibility = Visibility.Hidden;
                                Item_Details_Sets_Grid.Visibility = Visibility.Visible;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    active_L2H_Item.IsSelected = false;
                    active_L2H_Item = itemClicked;
                    active_L2H_Item.IsSelected = true;
                }
            }
            else
            {
                active_L2H_Item = itemClicked;
                active_L2H_Item.IsSelected = true;
            }

            switch (itemClicked.item_Category)
            {
                case L2H_Item_Category.Weapon:
                    RefreshGridInterface(L2H_Item_Category.Weapon, active_L2H_Item);
                    break;
                case L2H_Item_Category.Armor:
                    RefreshGridInterface(L2H_Item_Category.Armor, active_L2H_Item);
                    break;
                case L2H_Item_Category.Etc:
                    RefreshGridInterface(L2H_Item_Category.Etc, active_L2H_Item);
                    break;
                default:
                    break;
            }

            CollectionViewSource.GetDefaultView(Item_Name_Listview.ItemsSource).Refresh();
        }

        private void Weapon_SubType_Click(object sender, RoutedEventArgs e)
        {

            if (Keyboard.FocusedElement != null)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;
                Keyboard.ClearFocus();
                focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
            }

            if (active_Weapon_Subtype_Toggle != null)
            {
                if (active_Weapon_Subtype_Toggle != sender as ToggleButton)
                    active_Weapon_Subtype_Toggle.IsChecked = false;

                active_Weapon_Subtype_Toggle = sender as ToggleButton;

            }
            else
            {
                active_Weapon_Subtype_Toggle = sender as ToggleButton;
            }

            active_Weapon_Subtype_Toggle.IsChecked = true;

            CollectionViewSource.GetDefaultView(Item_Name_Listview.ItemsSource).Refresh();

        }

        private void Armor_SubType_Click(object sender, RoutedEventArgs e)
        {

            if (Keyboard.FocusedElement != null)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;
                Keyboard.ClearFocus();
                focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
            }
            if (active_Armor_Subtype_Toggle != null)
            {
                if (active_Armor_Subtype_Toggle != sender as ToggleButton)
                    active_Armor_Subtype_Toggle.IsChecked = false;

                active_Armor_Subtype_Toggle = sender as ToggleButton;

            }
            else
            {
                active_Armor_Subtype_Toggle = sender as ToggleButton;
            }

            active_Armor_Subtype_Toggle.IsChecked = true;

            CollectionViewSource.GetDefaultView(Item_Name_Listview.ItemsSource).Refresh();

        }

        private void Armor_BodyPart_Click(object sender, RoutedEventArgs e)
        {

            if (Keyboard.FocusedElement != null)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;
                Keyboard.ClearFocus();
                focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
            }
            if (active_Armor_BodyPart_Toggle != null)
            {
                if (active_Armor_BodyPart_Toggle != sender as ToggleButton)
                    active_Armor_BodyPart_Toggle.IsChecked = false;

                active_Armor_BodyPart_Toggle = sender as ToggleButton;

            }
            else
            {
                active_Armor_BodyPart_Toggle = sender as ToggleButton;
            }

            active_Armor_BodyPart_Toggle.IsChecked = true;

            CollectionViewSource.GetDefaultView(Item_Name_Listview.ItemsSource).Refresh();

        }



        private void Etc_SubType_Click(object sender, RoutedEventArgs e)
        {

            if (Keyboard.FocusedElement != null)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;
                Keyboard.ClearFocus();
                focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
            }
            if (active_Etc_Subtype_Toggle != null)
            {
                if (active_Etc_Subtype_Toggle != sender as ToggleButton)
                    active_Etc_Subtype_Toggle.IsChecked = false;

                active_Etc_Subtype_Toggle = sender as ToggleButton;

            }
            else
            {
                active_Etc_Subtype_Toggle = sender as ToggleButton;
            }

            active_Etc_Subtype_Toggle.IsChecked = true;

            CollectionViewSource.GetDefaultView(Item_Name_Listview.ItemsSource).Refresh();

        }

        private void ToggleSearchVisibility(object sender, RoutedEventArgs e)
        {

            if (searchIsShowing)
            {
                ItemsPage_Main_Grid.ColumnDefinitions[0].Width = new GridLength(0);
                ItemsPage_Main_Grid.ColumnDefinitions[1].Width = new GridLength(0);
                searchIsShowing = false;
                SearchIsShowingButton.Content = "Show Search";
            }
            else
            {
                ItemsPage_Main_Grid.ColumnDefinitions[0].Width = new GridLength(220);
                ItemsPage_Main_Grid.ColumnDefinitions[1].Width = new GridLength(220);
                searchIsShowing = true;
                SearchIsShowingButton.Content = "Hide Search";
            }
        }

        private void WeaponLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //Do log stuff here
        }

        private void Item_Details_Etc_Property_Triggers_Capsuled_Items_Edit_Button_Clicked(object sender, RoutedEventArgs e)
        {
            if (active_L2H_Item == null)
                return;
            if (active_L2H_Item.client_Etc == null)
                return;



            Popup_Capsuled_Items popup_Capsuled_Items = new Popup_Capsuled_Items(active_L2H_Item);
            popup_Capsuled_Items.ShowDialog();


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


        private void Create_Popup_Single_Choice(object sender, RoutedEventArgs e)
        {

            Popup_Multiple_Selections_Single_Choice choiceSelector = new Popup_Multiple_Selections_Single_Choice(sender as Button, active_L2H_Item);
            choiceSelector.ShowDialog();
        }

        private void Set_Slot_Clicked(object sender, RoutedEventArgs e)
        {
            Popup_Item_Selection set_Item_Selection_Popup = new Popup_Item_Selection(L2H_Items);
            set_Item_Selection_Popup.Initialize_For_Sets(sender as Button, active_L2H_Set, (Set_Slot_Type)Enum.Parse(typeof(Set_Slot_Type), (sender as Button).Tag.ToString()));

        }

        /// <summary>
        /// Toggles grid interface on/off depending on active item or set.
        /// Doesn't rebind data like RefreshGridInterface
        /// </summary>
        public void Toggle_Grid_Details(bool sets)
        {
            if (sets)
            {
                if (active_L2H_Set != null)
                {
                    Item_Details_Weapon_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Armor_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Etc_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Sets_Grid.Visibility = Visibility.Visible;
                    Item_Details_Sets_Grid_Add_New_Set.Visibility = Visibility.Hidden;
                }
                else
                {
                    Item_Details_Weapon_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Armor_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Etc_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Sets_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Sets_Grid_Add_New_Set.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (active_L2H_Item != null)
                {
                    switch (active_L2H_Item.item_Category)
                    {
                        case L2H_Item_Category.Weapon:
                            Item_Details_Weapon_Grid.Visibility = Visibility.Visible;
                            Item_Details_Armor_Grid.Visibility = Visibility.Hidden;
                            Item_Details_Etc_Grid.Visibility = Visibility.Hidden;
                            Item_Details_Sets_Grid.Visibility = Visibility.Hidden;
                            Item_Details_Sets_Grid_Add_New_Set.Visibility = Visibility.Hidden;
                            break;
                        case L2H_Item_Category.Armor:
                            Item_Details_Weapon_Grid.Visibility = Visibility.Hidden;
                            Item_Details_Armor_Grid.Visibility = Visibility.Visible;
                            Item_Details_Etc_Grid.Visibility = Visibility.Hidden;
                            Item_Details_Sets_Grid.Visibility = Visibility.Hidden;
                            Item_Details_Sets_Grid_Add_New_Set.Visibility = Visibility.Hidden;
                            break;
                        case L2H_Item_Category.Etc:
                            Item_Details_Weapon_Grid.Visibility = Visibility.Hidden;
                            Item_Details_Armor_Grid.Visibility = Visibility.Hidden;
                            Item_Details_Etc_Grid.Visibility = Visibility.Visible;
                            Item_Details_Sets_Grid.Visibility = Visibility.Hidden;
                            Item_Details_Sets_Grid_Add_New_Set.Visibility = Visibility.Hidden;
                            break;
                        case L2H_Item_Category.Sets:
                            return;
                        default:
                            return;
                    }

                    return;
                }
                else
                {
                    Item_Details_Weapon_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Armor_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Etc_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Sets_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Sets_Grid_Add_New_Set.Visibility = Visibility.Hidden;
                }
            }


        }

        public void RefreshGridInterface(L2H_Item_Category category, L2H_Item itemClicked = null)
        {
            if (itemClicked == null)
            {
                Item_Details_Armor_Grid.Visibility = Visibility.Hidden;
                Item_Details_Etc_Grid.Visibility = Visibility.Hidden;
                Item_Details_Weapon_Grid.Visibility = Visibility.Hidden;

                if (active_L2H_Set == null)
                {
                    Item_Details_Sets_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Sets_Grid_Add_New_Set.Visibility = Visibility.Visible;
                    return;
                }
                else
                    Item_Details_Sets_Grid_Add_New_Set.Visibility = Visibility.Hidden;
            }

            switch (category)
            {
                case L2H_Item_Category.Weapon:

                    Item_Details_Weapon_Preview_Icon.Source = itemClicked.Weapon_Image;
                    Item_Details_Weapon_Property_Crystal_Type.SelectedIndex = (int.Parse(itemClicked.client_Weapon.crystal_type));
                    Item_Details_Weapon_Property_Offense_Base_Attribute_Attack.DataContext = itemClicked;


                    foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Item_Details_Weapon_Grid))
                    {
                        tb.DataContext = itemClicked;
                    }
                    foreach (ToggleButton tb in L2H_Parser.FindVisualChildren<ToggleButton>(Item_Details_Weapon_Grid))
                    {
                        tb.DataContext = itemClicked;
                    }
                    foreach (ComboBox cb in L2H_Parser.FindVisualChildren<ComboBox>(Item_Details_Weapon_Grid))
                    {
                        cb.DataContext = itemClicked;
                    }
                    foreach (Button b in L2H_Parser.FindVisualChildren<Button>(Item_Details_Weapon_Grid))
                    {
                        b.DataContext = itemClicked;
                    }



                    Item_Details_Armor_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Etc_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Weapon_Grid.Visibility = Visibility.Visible;

                    break;
                case L2H_Item_Category.Armor:

                    Item_Details_Armor_Preview_Icon.Source = L2H_Parser.GetItemImage(itemClicked.client_Armor.icon[0]);
                    Item_Details_Armor_Property_Base_Body_Part.Text = L2H_Parser.GetBodyPartStringFromBodyPartID(itemClicked.client_Armor.body_part);

                    foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Item_Details_Armor_Grid))
                    {
                        tb.DataContext = itemClicked;
                    }
                    foreach (ToggleButton tb in L2H_Parser.FindVisualChildren<ToggleButton>(Item_Details_Armor_Grid))
                    {
                        tb.DataContext = itemClicked;
                    }
                    foreach (ComboBox cb in L2H_Parser.FindVisualChildren<ComboBox>(Item_Details_Armor_Grid))
                    {
                        cb.DataContext = itemClicked;
                    }
                    foreach (Button b in L2H_Parser.FindVisualChildren<Button>(Item_Details_Armor_Grid))
                    {
                        b.DataContext = itemClicked;
                    }

                    //Toggling details grids
                    Item_Details_Armor_Grid.Visibility = Visibility.Visible;
                    Item_Details_Etc_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Weapon_Grid.Visibility = Visibility.Hidden;

                    break;
                case L2H_Item_Category.Etc:
                    //Base
                    //Image Thumbnail
                    Item_Details_Etc_Preview_Icon.Source = L2H_Parser.GetItemImage(itemClicked.client_Etc.icon[0]);

                    foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Item_Details_Etc_Grid))
                    {
                        tb.DataContext = itemClicked;
                    }
                    foreach (ToggleButton tb in L2H_Parser.FindVisualChildren<ToggleButton>(Item_Details_Etc_Grid))
                    {
                        tb.DataContext = itemClicked;
                    }
                    foreach (ComboBox cb in L2H_Parser.FindVisualChildren<ComboBox>(Item_Details_Etc_Grid))
                    {
                        cb.DataContext = itemClicked;
                    }
                    foreach (Button b in L2H_Parser.FindVisualChildren<Button>(Item_Details_Etc_Grid))
                    {
                        b.DataContext = itemClicked;
                    }

                    Item_Details_Etc_Property_Triggers_Default_Action.Content = itemClicked.server_Itemdata.default_action;
                    Item_Details_Etc_Property_Triggers_Consume_Type.Content = itemClicked.server_Itemdata.consume_type;



                    string[] capsuledItems = L2H_Parser.GetCapsuledItems(itemClicked.server_Itemdata.capsuled_items);

                    if (capsuledItems != null)
                    {
                        int numberOfCapsuledItems = capsuledItems.Length / 4;
                        Item_Details_Etc_Property_Triggers_Capsuled_Items_Count.Text = numberOfCapsuledItems.ToString();
                    }
                    else
                    {
                        Item_Details_Etc_Property_Triggers_Capsuled_Items_Count.Text = "0";
                    }

                    //Toggling details grids
                    Item_Details_Armor_Grid.Visibility = Visibility.Hidden;
                    Item_Details_Etc_Grid.Visibility = Visibility.Visible;
                    Item_Details_Weapon_Grid.Visibility = Visibility.Hidden;
                    break;
                case L2H_Item_Category.Sets:
                    #region set images
                    if (active_L2H_Set.Slot_head_A != null)
                    {
                        Set_Details_Preview_Helmet_Icon_A.Source = L2H_Parser.GetItemImage(active_L2H_Set.Slot_head_A.client_Armor.icon[0]);
                        Set_Details_Preview_Helmet_Name_A.Text = active_L2H_Set.Slot_head_A.client_Itemname.name + " (ID: " + active_L2H_Set.Slot_head_A.ID.ToString() + ")";
                        Set_Details_Preview_Helmet_Icon_A.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Set_Details_Preview_Helmet_Icon_A.Visibility = Visibility.Hidden;
                        Set_Details_Preview_Helmet_Name_B.Text = "";
                    }

                    if (active_L2H_Set.Slot_head_B != null)
                    {
                        Set_Details_Preview_Helmet_Icon_B.Source = L2H_Parser.GetItemImage(active_L2H_Set.Slot_head_B.client_Armor.icon[0]);
                        Set_Details_Preview_Helmet_Name_B.Text = active_L2H_Set.Slot_head_B.client_Itemname.name + " (ID: " + active_L2H_Set.Slot_head_B.ID.ToString() + ")";
                        Set_Details_Preview_Helmet_Icon_B.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Set_Details_Preview_Helmet_Icon_B.Visibility = Visibility.Hidden;
                        Set_Details_Preview_Helmet_Name_B.Text = "";
                    }

                    if (active_L2H_Set.Slot_chest != null)
                    {
                        Set_Details_Preview_Chest_Icon.Source = L2H_Parser.GetItemImage(active_L2H_Set.Slot_chest.client_Armor.icon[0]);
                        Set_Details_Preview_Chest_Name.Text = active_L2H_Set.Slot_chest.client_Itemname.name + " (ID: " + active_L2H_Set.Slot_chest.ID.ToString() + ")";
                        Set_Details_Preview_Chest_Icon.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Set_Details_Preview_Chest_Icon.Visibility = Visibility.Hidden;
                        Set_Details_Preview_Chest_Name.Text = "";
                    }

                    if (active_L2H_Set.Slot_legs_A != null)
                    {
                        Set_Details_Preview_Legs_Icon_A.Source = L2H_Parser.GetItemImage(active_L2H_Set.Slot_legs_A.client_Armor.icon[0]);
                        Set_Details_Preview_Legs_Name_A.Text = active_L2H_Set.Slot_legs_A.client_Itemname.name + " (ID: " + active_L2H_Set.Slot_legs_A.ID.ToString() + ")";
                        Set_Details_Preview_Legs_Icon_A.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Set_Details_Preview_Legs_Icon_A.Visibility = Visibility.Hidden;
                        Set_Details_Preview_Legs_Name_A.Text = "";
                    }

                    if (active_L2H_Set.Slot_legs_B != null)
                    {
                        Set_Details_Preview_Legs_Icon_B.Source = L2H_Parser.GetItemImage(active_L2H_Set.Slot_legs_B.client_Armor.icon[0]);
                        Set_Details_Preview_Legs_Name_B.Text = active_L2H_Set.Slot_legs_B.client_Itemname.name + " (ID: " + active_L2H_Set.Slot_legs_B.ID.ToString() + ")";
                        Set_Details_Preview_Legs_Icon_B.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Set_Details_Preview_Legs_Icon_B.Visibility = Visibility.Hidden;
                        Set_Details_Preview_Legs_Name_B.Text = "";
                    }

                    if (active_L2H_Set.Slot_gloves_A != null)
                    {
                        Set_Details_Preview_Gloves_Icon_A.Source = L2H_Parser.GetItemImage(active_L2H_Set.Slot_gloves_A.client_Armor.icon[0]);
                        Set_Details_Preview_Gloves_Name_A.Text = active_L2H_Set.Slot_gloves_A.client_Itemname.name + " (ID: " + active_L2H_Set.Slot_gloves_A.ID.ToString() + ")";
                        Set_Details_Preview_Gloves_Icon_A.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Set_Details_Preview_Gloves_Icon_A.Visibility = Visibility.Hidden;
                        Set_Details_Preview_Gloves_Name_A.Text = "";
                    }

                    if (active_L2H_Set.Slot_gloves_B != null)
                    {
                        Set_Details_Preview_Gloves_Icon_B.Source = L2H_Parser.GetItemImage(active_L2H_Set.Slot_gloves_B.client_Armor.icon[0]);
                        Set_Details_Preview_Gloves_Name_B.Text = active_L2H_Set.Slot_gloves_B.client_Itemname.name + " (ID: " + active_L2H_Set.Slot_gloves_B.ID.ToString() + ")";
                        Set_Details_Preview_Gloves_Icon_B.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Set_Details_Preview_Gloves_Icon_B.Visibility = Visibility.Hidden;
                        Set_Details_Preview_Gloves_Name_B.Text = "";
                    }

                    if (active_L2H_Set.Slot_feet_A != null)
                    {
                        Set_Details_Preview_Feet_Icon_A.Source = L2H_Parser.GetItemImage(active_L2H_Set.Slot_feet_A.client_Armor.icon[0]);
                        Set_Details_Preview_Feet_Name_A.Text = active_L2H_Set.Slot_feet_A.client_Itemname.name + " (ID: " + active_L2H_Set.Slot_feet_A.ID.ToString() + ")";
                        Set_Details_Preview_Feet_Icon_A.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Set_Details_Preview_Feet_Icon_A.Visibility = Visibility.Hidden;
                        Set_Details_Preview_Feet_Name_A.Text = "";
                    }

                    if (active_L2H_Set.Slot_feet_B != null)
                    {
                        Set_Details_Preview_Feet_Icon_B.Source = L2H_Parser.GetItemImage(active_L2H_Set.Slot_feet_B.client_Armor.icon[0]);
                        Set_Details_Preview_Feet_Name_B.Text = active_L2H_Set.Slot_feet_B.client_Itemname.name + " (ID: " + active_L2H_Set.Slot_feet_B.ID.ToString() + ")";
                        Set_Details_Preview_Feet_Icon_B.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Set_Details_Preview_Feet_Icon_B.Visibility = Visibility.Hidden;
                        Set_Details_Preview_Feet_Name_B.Text = "";
                    }

                    //These two slots can use shields as well, so it needs to check client_Weapon first to see if it's null
                    if (active_L2H_Set.Slot_additional_A != null)
                    {
                        if (active_L2H_Set.Slot_additional_A.client_Weapon != null)
                        {
                            if (active_L2H_Set.Slot_additional_A != null)
                            {
                                Set_Details_Preview_Additional_Icon_A.Source = L2H_Parser.GetItemImage(active_L2H_Set.Slot_additional_A.client_Weapon.icon[0]);
                                Set_Details_Preview_Additional_Name_A.Text = active_L2H_Set.Slot_additional_A.client_Itemname.name + " (ID: " + active_L2H_Set.Slot_additional_A.ID.ToString() + ")";
                                Set_Details_Preview_Additional_Icon_A.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Set_Details_Preview_Additional_Icon_A.Visibility = Visibility.Hidden;

                                Set_Details_Preview_Additional_Name_A.Text = "";
                            }

                            if (active_L2H_Set.Slot_additional_B != null)
                            {
                                Set_Details_Preview_Additional_Icon_B.Source = L2H_Parser.GetItemImage(active_L2H_Set.Slot_additional_B.client_Weapon.icon[0]);
                                Set_Details_Preview_Additional_Name_B.Text = active_L2H_Set.Slot_additional_B.client_Itemname.name + " (ID: " + active_L2H_Set.Slot_additional_B.ID.ToString() + ")";
                                Set_Details_Preview_Additional_Icon_B.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Set_Details_Preview_Additional_Icon_B.Visibility = Visibility.Hidden;
                                Set_Details_Preview_Additional_Name_B.Text = "";
                            }
                        }
                        else
                        {
                            if (active_L2H_Set.Slot_additional_A != null)
                            {
                                Set_Details_Preview_Additional_Icon_A.Source = L2H_Parser.GetItemImage(active_L2H_Set.Slot_additional_A.client_Armor.icon[0]);
                                Set_Details_Preview_Additional_Name_A.Text = active_L2H_Set.Slot_additional_A.client_Itemname.name + " (ID: " + active_L2H_Set.Slot_additional_A.ID.ToString() + ")";
                                Set_Details_Preview_Additional_Icon_A.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Set_Details_Preview_Additional_Icon_A.Visibility = Visibility.Hidden;
                                Set_Details_Preview_Additional_Name_A.Text = "";
                            }

                            if (active_L2H_Set.Slot_additional_B != null)
                            {
                                Set_Details_Preview_Additional_Icon_B.Source = L2H_Parser.GetItemImage(active_L2H_Set.Slot_additional_B.client_Armor.icon[0]);
                                Set_Details_Preview_Additional_Name_B.Text = active_L2H_Set.Slot_additional_B.client_Itemname.name + " (ID: " + active_L2H_Set.Slot_additional_B.ID.ToString() + ")";
                                Set_Details_Preview_Additional_Icon_B.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Set_Details_Preview_Additional_Icon_B.Visibility = Visibility.Hidden;
                                Set_Details_Preview_Additional_Name_B.Text = "";
                            }
                        }
                    }
                    else
                    {
                        Set_Details_Preview_Additional_Icon_A.Visibility = Visibility.Hidden;
                        Set_Details_Preview_Additional_Name_A.Text = "";
                        Set_Details_Preview_Additional_Icon_B.Visibility = Visibility.Hidden;
                        Set_Details_Preview_Additional_Name_B.Text = "";
                    }

                    #endregion

                    foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Item_Details_Sets_Grid))
                    {
                        tb.DataContext = active_L2H_Set;
                    }
                    foreach (ToggleButton tb in L2H_Parser.FindVisualChildren<ToggleButton>(Item_Details_Sets_Grid))
                    {
                        tb.DataContext = active_L2H_Set;
                    }


                    Item_Details_Sets_Grid.Visibility = Visibility.Visible;


                    CollectionViewSource.GetDefaultView(Item_Set_Listview.ItemsSource).Refresh();

                    break;
                default:
                    break;
            }

        }

        private void Set_Slot_RightClicked(object sender, MouseButtonEventArgs e)
        {
            Popup_Confirmation newPopup = new Popup_Confirmation();


            L2H_Item target_Item;

            switch ((Set_Slot_Type)Enum.Parse(typeof(Set_Slot_Type), (sender as Button).Tag.ToString()))
            {
                case Set_Slot_Type.head_A:
                    target_Item = active_L2H_Set.Slot_head_A;
                    newPopup.Confirmation_Action += (b, bb) => active_L2H_Set.Slot_head_A = null;
                    break;
                case Set_Slot_Type.head_B:
                    target_Item = active_L2H_Set.Slot_head_B;
                    newPopup.Confirmation_Action += (b, bb) => active_L2H_Set.Slot_head_B = null;
                    break;
                case Set_Slot_Type.chest:
                    target_Item = active_L2H_Set.Slot_chest;
                    newPopup.Confirmation_Action += (b, bb) => active_L2H_Set.Slot_chest = null;
                    break;
                case Set_Slot_Type.legs_A:
                    target_Item = active_L2H_Set.Slot_legs_A;
                    newPopup.Confirmation_Action += (b, bb) => active_L2H_Set.Slot_legs_A = null;
                    break;
                case Set_Slot_Type.legs_B:
                    target_Item = active_L2H_Set.Slot_legs_B;
                    newPopup.Confirmation_Action += (b, bb) => active_L2H_Set.Slot_legs_B = null;
                    break;
                case Set_Slot_Type.gloves_A:
                    target_Item = active_L2H_Set.Slot_gloves_A;
                    newPopup.Confirmation_Action += (b, bb) => active_L2H_Set.Slot_gloves_A = null;
                    break;
                case Set_Slot_Type.gloves_B:
                    target_Item = active_L2H_Set.Slot_gloves_B;
                    newPopup.Confirmation_Action += (b, bb) => active_L2H_Set.Slot_gloves_B = null;
                    break;
                case Set_Slot_Type.feet_A:
                    target_Item = active_L2H_Set.Slot_feet_A;
                    newPopup.Confirmation_Action += (b, bb) => active_L2H_Set.Slot_feet_A = null;
                    break;
                case Set_Slot_Type.feet_B:
                    target_Item = active_L2H_Set.Slot_feet_B;
                    newPopup.Confirmation_Action += (b, bb) => active_L2H_Set.Slot_feet_B = null;
                    break;
                case Set_Slot_Type.additional_A:
                    target_Item = active_L2H_Set.Slot_additional_A;
                    newPopup.Confirmation_Action += (b, bb) => active_L2H_Set.Slot_additional_A = null;
                    break;
                case Set_Slot_Type.additional_B:
                    target_Item = active_L2H_Set.Slot_additional_B;
                    newPopup.Confirmation_Action += (b, bb) => active_L2H_Set.Slot_additional_B = null;
                    break;
                default:
                    target_Item = active_L2H_Set.Slot_head_A;
                    break;
            }


            if (target_Item == null)
            {
                newPopup.Close();
                return;
            }

            newPopup.Post_Confirmation_Action += (b, bb) => RefreshGridInterface(L2H_Item_Category.Sets);
            newPopup.InitializeConfirmation(target_Item.client_Itemname.name, "Set ID: " + active_L2H_Set.ID.ToString() + "?", target_Item.GetIconString(), target_Item);


        }

        public void Force_Item_Selection(L2H_Item target)
        {
            if (active_L2H_Item != null)
            {
                if (active_L2H_Item == target)
                    return;
                else
                {
                    active_L2H_Item.IsSelected = false;
                }
            }

            if (active_L2H_Set != null)
            {
                active_L2H_Set.IsSelected = false;
            }

            active_L2H_Item = target;
            active_L2H_Item.IsSelected = true;

            itemdataIsLoading = true;
            RefreshGridInterface(target.item_Category, target);

            itemdataIsLoading = false;

            CollectionViewSource.GetDefaultView(Item_Name_Listview.ItemsSource).Refresh();

        }


        private void Clone_Item_Click(object sender, RoutedEventArgs e)
        {
            if (active_L2H_Item == null)
                MessageBox.Show("No Item selected.");

            //Grab relevant information and create a deep copy            
            Server_Itemdata newItemData = ObjectExtensions.Copy(active_L2H_Item.server_Itemdata);
            Client_Itemname newItemName = ObjectExtensions.Copy(active_L2H_Item.client_Itemname);


            //Assign new ID:
            string newItemID = (mainWindow.GetPageOfType(typeof(Pages.OverviewPage)) as OverviewPage).L2H_Settings.NewItemIndexStart;// "50000";

            if (item_Template_Pointers.Count > 0)
            {
                newItemID = (Convert.ToInt32(item_Template_Pointers[item_Template_Pointers.Count - 1].id) + 1).ToString();
            }

            newItemData.itemId = newItemID;
            newItemData.itemName = L2H_Parser.GetUniqueItemNameID(itemdata, newItemData.itemName);
            newItemName.id = newItemID;

            itemdata.Add(newItemData);
            client_Itemnames.Add(newItemName);
            item_Template_Pointers.Add(new L2H_Template_Pointer(newItemID, active_L2H_Item.ID.ToString()));

            L2H_Item new_L2H_Item = new L2H_Item()
            {
                ID = int.Parse(newItemID),
                server_Itemdata = newItemData,
                client_Itemname = newItemName,
                IsCustom = true

            };

            switch (active_L2H_Item.item_Category)
            {
                case L2H_Item_Category.Weapon:
                    Client_Weapon newWeapon = ObjectExtensions.Copy(active_L2H_Item.client_Weapon);
                    newWeapon.id = newItemID;
                    client_Weapons.Add(newWeapon);
                    new_L2H_Item.client_Weapon = newWeapon;
                    new_L2H_Item.item_Category = L2H_Item_Category.Weapon;
                    break;
                case L2H_Item_Category.Armor:
                    Client_Armor newArmor = ObjectExtensions.Copy(active_L2H_Item.client_Armor);
                    newArmor.id = newItemID;
                    client_Armors.Add(newArmor);
                    new_L2H_Item.client_Armor = newArmor;
                    new_L2H_Item.item_Category = L2H_Item_Category.Armor;
                    break;
                case L2H_Item_Category.Etc:
                    Client_Etc newEtc = ObjectExtensions.Copy(active_L2H_Item.client_Etc);
                    newEtc.id = newItemID;
                    client_Etcs.Add(newEtc);
                    new_L2H_Item.client_Etc = newEtc;
                    new_L2H_Item.item_Category = L2H_Item_Category.Etc;
                    break;
                case L2H_Item_Category.Sets:
                    MessageBox.Show("Sets can't be cloned, I don't know how you managed to trigger this warning");
                    return;
                default:
                    break;
            }


            L2H_Items.Add(new_L2H_Item);

            L2H_Log.Instance.Log_Item_Clone(active_L2H_Item, new_L2H_Item);

            CollectionViewSource.GetDefaultView(Item_Name_Listview.ItemsSource).Refresh();


        }


        private void Delete_Item_Click(object sender, RoutedEventArgs e)
        {
            if (active_L2H_Item == null)
                MessageBox.Show("No item selected");



            if (!item_Template_Pointers.Exists(x => x.id == active_L2H_Item.ID.ToString()))
            {
                MessageBox.Show("You cannot delete original data");
                return;
            }

            L2H_Log.Instance.Log_Item_Delete(active_L2H_Item);


            //Remove all references of item
            switch (active_L2H_Item.item_Category)
            {
                case L2H_Item_Category.Weapon:
                    client_Weapons.Remove(active_L2H_Item.client_Weapon);
                    break;
                case L2H_Item_Category.Armor:
                    client_Armors.Remove(active_L2H_Item.client_Armor);
                    break;
                case L2H_Item_Category.Etc:
                    client_Etcs.Remove(active_L2H_Item.client_Etc);
                    break;
                case L2H_Item_Category.Sets:
                    break;
                default:
                    break;
            }

            itemdata.Remove(active_L2H_Item.server_Itemdata);
            client_Itemnames.Remove(active_L2H_Item.client_Itemname);
            item_Template_Pointers.Remove(item_Template_Pointers.Find(x => x.id == active_L2H_Item.ID.ToString()));

            L2H_Items.Remove(active_L2H_Item);
            active_L2H_Item = null;

            RefreshGridInterface(L2H_Item_Category.Weapon);
            CollectionViewSource.GetDefaultView(Item_Name_Listview.ItemsSource).Refresh();

        }

        private void Module_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Set_Create_Clicked(object sender, RoutedEventArgs e)
        {

            string template_P = "";
            string template_N = "";

            Server_Itemdata newServer_Itemdata = ObjectExtensions.Copy(L2H_Sets[0].server_Itemdata);
            

            newServer_Itemdata.set_id = L2H_Parser.GetUniqueSetID(itemdata);

            L2H_Set newSet = new L2H_Set();
            newSet.ID = int.Parse(newServer_Itemdata.set_id);
            newSet.template = template_P;
            newSet.name_ID = template_N;
            newSet.server_Itemdata = newServer_Itemdata;
            //newSet.client_Itemname = client_Itemnames.Find(x => x.id == newServer_Itemdata.slot_chest);

            itemdata.Add(newServer_Itemdata);
            L2H_Sets.Add(newSet);

            CollectionViewSource.GetDefaultView(Item_Set_Listview.ItemsSource).Refresh();
        }

        private void Item_Filter_Custom_Toggle_Clicked(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Item_Name_Listview.ItemsSource).Refresh();
        }

        private void Item_Force_Select_Waiting(object sender, RoutedEventArgs e)
        {
            if (forceWaiting)
            {
                switch (active_L2H_Item.item_Category)
                {
                    case L2H_Item_Category.Weapon:
                        RefreshGridInterface(L2H_Item_Category.Weapon, active_L2H_Item);
                        break;
                    case L2H_Item_Category.Armor:
                        RefreshGridInterface(L2H_Item_Category.Armor, active_L2H_Item);
                        break;
                    case L2H_Item_Category.Etc:
                        RefreshGridInterface(L2H_Item_Category.Etc, active_L2H_Item);
                        break;
                    default:
                        break;
                }
                forceWaiting = false;
            }
        }
    }

    //Loading Threads

    public class Thread_Load_Itemdata_Itemnames
    {
        public event EventHandler ThreadDone;

        List<Server_Itemdata> itemdata;
        List<Client_Itemname> client_Itemnames;

        public Thread_Load_Itemdata_Itemnames(List<Server_Itemdata> itemdata, List<Client_Itemname> client_Itemnames)
        {
            this.itemdata = itemdata;
            this.client_Itemnames = client_Itemnames;
        }

        public void ThreadProc()
        {
            // Load text from itemdata
            using (TextReader textReader = new StreamReader(L2H_Constants.server_Itemdata_Path, Encoding.GetEncoding(1200)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Server_Itemdata newItemdata = new Server_Itemdata(line);

                    itemdata.Add(newItemdata);
                }
            }


            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Itemnames_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Itemname newItemName = new Client_Itemname(line);
                    if (newItemName.id != "id")
                        client_Itemnames.Add(newItemName);
                }
            }

            if (ThreadDone != null)
                ThreadDone(this, EventArgs.Empty);
        }


    }

    public class Thread_LoadClientWeapons
    {

        public event EventHandler ThreadDone;
        public event EventHandler AddChild;

        List<Server_Itemdata> itemdata;
        List<Client_Itemname> client_Itemnames;
        List<Client_Weapon> client_Weapons;
        List<L2H_Template_Pointer> item_Template_Pointers;

        List<L2H_Item> L2H_Items;

        public Thread_LoadClientWeapons(List<Server_Itemdata> itemdata, List<Client_Itemname> itemnames, List<Client_Weapon> client_Weapons, List<L2H_Template_Pointer> item_Template_Pointers, List<L2H_Item> L2H_Items)
        {
            this.itemdata = itemdata;
            this.client_Itemnames = itemnames;
            this.client_Weapons = client_Weapons;
            this.item_Template_Pointers = item_Template_Pointers;
            this.L2H_Items = L2H_Items;
        }

        // The thread procedure performs the task
        public void ThreadProc()
        {
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Weapons_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;

                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Weapon newWeapon = new Client_Weapon(line);
                    if (newWeapon.id != "id")
                    {
                        client_Weapons.Add(newWeapon);

                        // Append
                        Server_Itemdata targetItemData = itemdata.Find(x => x.itemId == newWeapon.id);

                        if (targetItemData != null)
                        {
                            if (targetItemData.invalidItem)
                                continue;

                            string template_P = "";

                            L2H_Template_Pointer targetPointer = item_Template_Pointers.Find(x => x.id == newWeapon.id);

                            if (targetPointer != null)
                                template_P = targetPointer.templateId;

                            L2H_Item newItem = new L2H_Item();
                            newItem.ID = int.Parse(targetItemData.itemId);
                            newItem.server_Itemdata = targetItemData;
                            newItem.client_Weapon = newWeapon;
                            newItem.client_Itemname = client_Itemnames.Find(x => x.id == newWeapon.id);
                            newItem.item_Category = L2H_Item_Category.Weapon;

                            if (targetPointer != null)
                                newItem.IsCustom = true;
                            else
                                newItem.IsCustom = false;

                            if (newItem.client_Itemname != null)
                            {
                                if (newItem.client_Itemname.name != null)
                                    L2H_Items.Add(newItem);
                            }

                        }
                    }
                }


            }

            if (ThreadDone != null)
                ThreadDone(this, EventArgs.Empty);
        }


    }

    public class Thread_LoadClientArmors
    {

        public event EventHandler ThreadDone;
        public event EventHandler AddChild;

        List<Server_Itemdata> itemdata;
        List<Client_Itemname> client_Itemnames;
        List<Client_Armor> client_Armors;
        List<L2H_Template_Pointer> item_Template_Pointers;

        List<L2H_Item> L2H_Items;

        public Thread_LoadClientArmors(List<Server_Itemdata> itemdata, List<Client_Itemname> itemnames, List<Client_Armor> client_Armors, List<L2H_Template_Pointer> item_Template_Pointers, List<L2H_Item> L2H_Items)
        {
            this.itemdata = itemdata;
            this.client_Itemnames = itemnames;
            this.client_Armors = client_Armors;
            this.item_Template_Pointers = item_Template_Pointers;
            this.L2H_Items = L2H_Items;
        }

        // The thread procedure performs the task
        public void ThreadProc()
        {
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Armors_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;


                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Armor newArmor = new Client_Armor(line);
                    if (newArmor.id != "id")
                    {
                        client_Armors.Add(newArmor);

                        // Append
                        Server_Itemdata targetItemData = itemdata.Find(x => x.itemId == newArmor.id);

                        if (targetItemData != null)
                        {
                            if (targetItemData.invalidItem)
                                continue;

                            string template_P = "";

                            L2H_Template_Pointer targetPointer = item_Template_Pointers.Find(x => x.id == newArmor.id);

                            if (targetPointer != null)
                                template_P = targetPointer.templateId;


                            L2H_Item newItem = new L2H_Item();
                            newItem.ID = int.Parse(targetItemData.itemId);
                            newItem.server_Itemdata = targetItemData;
                            newItem.client_Armor = newArmor;
                            newItem.client_Itemname = client_Itemnames.Find(x => x.id == newArmor.id);
                            newItem.item_Category = L2H_Item_Category.Armor;

                            if (newItem.client_Itemname != null)
                            {
                                if (newItem.client_Itemname.name != null)
                                    L2H_Items.Add(newItem);
                            }
                            else
                            {
                                string dxxxspao = "Broken Armor Found!";
                            }



                        }
                    }
                }
            }

            if (ThreadDone != null)
                ThreadDone(this, EventArgs.Empty);
        }


    }

    public class Thread_LoadClientEtcs
    {

        public event EventHandler ThreadDone;

        List<Server_Itemdata> itemdata;
        List<Client_Itemname> client_Itemnames;
        List<Client_Etc> client_Etcs;
        List<L2H_Template_Pointer> item_Template_Pointers;

        List<L2H_Item> L2H_Items;

        public Thread_LoadClientEtcs(List<Server_Itemdata> itemdata, List<Client_Itemname> itemnames, List<Client_Etc> client_Etcs, List<L2H_Template_Pointer> item_Template_Pointers, List<L2H_Item> L2H_Items)
        {
            this.itemdata = itemdata;
            this.client_Itemnames = itemnames;
            this.client_Etcs = client_Etcs;
            this.item_Template_Pointers = item_Template_Pointers;
            this.L2H_Items = L2H_Items;
        }

        // The thread procedure performs the task
        public void ThreadProc()
        {
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Etcs_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;


                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Etc newEtc = new Client_Etc(line);
                    if (newEtc.id != "id")
                    {
                        client_Etcs.Add(newEtc);

                        // Append
                        Server_Itemdata targetItemData = itemdata.Find(x => x.itemId == newEtc.id);

                        if (targetItemData != null)
                        {
                            if (targetItemData.invalidItem)
                                continue;

                            string template_P = "";

                            L2H_Template_Pointer targetPointer = item_Template_Pointers.Find(x => x.id == newEtc.id);

                            if (targetPointer != null)
                                template_P = targetPointer.templateId;


                            L2H_Item newItem = new L2H_Item();

                            newItem.ID = int.Parse(targetItemData.itemId);
                            newItem.server_Itemdata = targetItemData;
                            newItem.client_Etc = newEtc;
                            newItem.client_Itemname = client_Itemnames.Find(x => x.id == newEtc.id);
                            newItem.item_Category = L2H_Item_Category.Etc;
                            newItem.capsuled_Items = new List<L2H_Capsuled_Item>();

                            string[] capsuledItems = L2H_Parser.GetCapsuledItems(newItem.server_Itemdata.capsuled_items);

                            if (capsuledItems != null)
                            {
                                int numberOfCapsuledItems = capsuledItems.Length / 4;
                                int numberOfCapsuledItemsProcessed = 0;

                                for (int i = 0; i < numberOfCapsuledItems; i++)
                                {
                                    newItem.capsuled_Items.Add(new L2H_Capsuled_Item()
                                    {
                                        Capsuled_Item = capsuledItems[4 * numberOfCapsuledItemsProcessed],
                                        Chance_Min = capsuledItems[4 * numberOfCapsuledItemsProcessed + 1],
                                        Chance_Max = capsuledItems[4 * numberOfCapsuledItemsProcessed + 2],
                                        Chance_Trigger = capsuledItems[4 * numberOfCapsuledItemsProcessed + 3]
                                    });

                                    numberOfCapsuledItemsProcessed++;
                                }

                            }

                            if (newItem.client_Itemname != null)
                                if (newItem.client_Itemname.name != null)
                                    L2H_Items.Add(newItem);

                        }
                    }
                }


                if (ThreadDone != null)
                    ThreadDone(this, EventArgs.Empty);


            }

        }


    }

    public class Thread_LoadSets
    {

        public event EventHandler ThreadDone;

        List<Server_Itemdata> itemdata;
        List<Client_Itemname> client_Itemnames;
        List<L2H_Template_Pointer> item_Template_Pointers;
        List<L2H_Template_Pointer> set_Template_Pointers;
        List<L2H_Item> L2H_Items;
        List<L2H_Set> L2H_Sets;

        public Thread_LoadSets(List<L2H_Item> L2H_Items, List<L2H_Set> L2H_Sets, List<Server_Itemdata> itemdata, List<Client_Itemname> itemnames, List<L2H_Template_Pointer> item_Template_Pointers, List<L2H_Template_Pointer> set_Template_Pointers)
        {
            this.itemdata = itemdata;
            this.client_Itemnames = itemnames;
            this.item_Template_Pointers = item_Template_Pointers;
            this.set_Template_Pointers = set_Template_Pointers;
            this.L2H_Items = L2H_Items;
            this.L2H_Sets = L2H_Sets;
        }

        // The thread procedure performs the task
        public void ThreadProc()
        {

            for (int i = 0; i < itemdata.Count; i++)
            {

                Server_Itemdata targetItemData = itemdata[i];
                Client_Itemname clientChest = client_Itemnames.Find(x => x.id == targetItemData.slot_chest);

                if (targetItemData == null)
                    continue;

                if (itemdata[i].beginning_text == "set_begin")
                {

                    string template_P = "";
                    string template_N = "";

                    L2H_Template_Pointer targetPointer = set_Template_Pointers.Find(x => x.id == itemdata[i].set_id);

                    if (targetPointer != null)
                    {
                        template_P = targetPointer.templateId;
                        if (targetPointer.extraName != null)
                            template_N = targetPointer.extraName;

                        if (targetPointer.templateId != null)
                            if (!set_Template_Pointers.Exists(x => x.id == targetPointer.id))
                                set_Template_Pointers.Add(targetPointer);
                    }

                    L2H_Set newSet = new L2H_Set();
                    newSet.ID = int.Parse(itemdata[i].set_id);
                    newSet.template = template_P;
                    newSet.name_ID = template_N;
                    newSet.server_Itemdata = targetItemData;
                    newSet.client_Itemname = clientChest;

                    if (!string.IsNullOrEmpty(targetItemData.slot_chest))
                    {

                        SpinWait.SpinUntil(() => L2H_Items.Exists(x => x.ID == int.Parse(targetItemData.slot_chest)), Timeout.Infinite);

                        newSet.slot_chest = L2H_Items.Find(x => x.ID == int.Parse(targetItemData.slot_chest));
                    }

                    if (!string.IsNullOrEmpty(targetItemData.slot_head))
                    {
                        if (targetItemData.slot_head.Contains(";"))
                        {
                            string[] splitString = targetItemData.slot_head.Split(';');
                            newSet.slot_head_A = L2H_Items.Find(x => x.ID == int.Parse(splitString[0]));
                            newSet.slot_head_B = L2H_Items.Find(x => x.ID == int.Parse(splitString[1]));
                        }
                        else
                        {
                            newSet.slot_head_A = L2H_Items.Find(x => x.ID == int.Parse(targetItemData.slot_head));
                        }
                    }

                    if (!string.IsNullOrEmpty(targetItemData.slot_gloves))
                    {
                        if (targetItemData.slot_gloves.Contains(";"))
                        {
                            string[] splitString = targetItemData.slot_gloves.Split(';');
                            newSet.slot_gloves_A = L2H_Items.Find(x => x.ID == int.Parse(splitString[0]));
                            newSet.slot_gloves_B = L2H_Items.Find(x => x.ID == int.Parse(splitString[1]));
                        }
                        else
                        {
                            newSet.slot_gloves_A = L2H_Items.Find(x => x.ID == int.Parse(targetItemData.slot_gloves));
                        }
                    }

                    if (!string.IsNullOrEmpty(targetItemData.slot_feet))
                    {
                        if (targetItemData.slot_feet.Contains(";"))
                        {
                            string[] splitString = targetItemData.slot_feet.Split(';');
                            newSet.slot_feet_A = L2H_Items.Find(x => x.ID == int.Parse(splitString[0]));
                            newSet.slot_feet_B = L2H_Items.Find(x => x.ID == int.Parse(splitString[1]));
                        }
                        else
                        {
                            newSet.slot_feet_A = L2H_Items.Find(x => x.ID == int.Parse(targetItemData.slot_feet));
                        }
                    }

                    if (!string.IsNullOrEmpty(targetItemData.slot_legs))
                    {
                        if (targetItemData.slot_legs.Contains(";"))
                        {
                            string[] splitString = targetItemData.slot_legs.Split(';');
                            newSet.slot_legs_A = L2H_Items.Find(x => x.ID == int.Parse(splitString[0]));
                            newSet.slot_legs_B = L2H_Items.Find(x => x.ID == int.Parse(splitString[1]));
                        }
                        else
                        {
                            newSet.slot_legs_A = L2H_Items.Find(x => x.ID == int.Parse(targetItemData.slot_legs));
                        }
                    }

                    if (!string.IsNullOrEmpty(targetItemData.slot_lhand))
                    {
                        if (targetItemData.slot_lhand.Contains(";"))
                        {
                            string[] splitString = targetItemData.slot_lhand.Split(';');
                            newSet.slot_additional_A = L2H_Items.Find(x => x.ID == int.Parse(splitString[0]));
                            newSet.slot_additional_B = L2H_Items.Find(x => x.ID == int.Parse(splitString[1]));
                        }
                        else
                        {
                            newSet.slot_additional_A = L2H_Items.Find(x => x.ID == int.Parse(targetItemData.slot_lhand));
                        }
                    }

                    L2H_Sets.Add(newSet);

                }

            }


            if (ThreadDone != null)
                ThreadDone(this, EventArgs.Empty);
        }


    }
}
