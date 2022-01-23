using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace L2Homage
{
    public partial class Popup_NPC_Selection : Window
    {
        MainWindow mainWindow;
        L2H_NPC activeNPC;
        List<L2H_NPC> L2H_NPCs;

        L2H_Spawn_NPC_Maker active_L2H_Spawn_NPC_Maker;
        L2H_Multisell active_L2H_Multisell;
        L2H_Raid active_L2H_Raid;

        bool firstLoad = true;

        public Popup_NPC_Selection(List<L2H_NPC> L2H_NPCs)
        {
            InitializeComponent();

            this.L2H_NPCs = L2H_NPCs;
            ResizeMode = ResizeMode.CanResize;
            mainWindow = ((MainWindow)Application.Current.MainWindow);

        }

        public void Initialize_For_Spawns(L2H_Spawn_NPC_Maker active_L2H_Spawn_NPC_Maker)
        {
            this.active_L2H_Spawn_NPC_Maker = active_L2H_Spawn_NPC_Maker;

            var newView = new CollectionViewSource() { Source = L2H_NPCs };
            var listViewCollection2 = (ListCollectionView)newView.View;
            listViewCollection2.Filter = Filter_NPC_Name;
            Selections_Listview.ItemsSource = listViewCollection2;
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();


            ShowDialog();
        }

        public void Initialize_For_Multisell(L2H_Multisell active_L2H_Multisell)
        {
            this.active_L2H_Multisell = active_L2H_Multisell;

            var newView = new CollectionViewSource() { Source = L2H_NPCs };
            var listViewCollection2 = (ListCollectionView)newView.View;
            listViewCollection2.Filter = Filter_NPC_Name;
            Selections_Listview.ItemsSource = listViewCollection2;
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();


            ShowDialog();
        }

        public void Initialize_For_Raid(L2H_Raid active_L2H_Raid)
        {
            this.active_L2H_Raid = active_L2H_Raid;

            var newView = new CollectionViewSource() { Source = L2H_NPCs };
            var listViewCollection2 = (ListCollectionView)newView.View;
            listViewCollection2.Filter = Filter_NPC_Name;
            Selections_Listview.ItemsSource = listViewCollection2;
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();


            ShowDialog();
        }


        private void Close_Window(object sender, RoutedEventArgs e)
        {
            if (activeNPC != null)
                activeNPC.IsSelectedTemp = false;

            Close();
        }

        private void Selections_Listview_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void Item_Name_Clicked(object sender, RoutedEventArgs e)
        {
            var vm = (sender as FrameworkElement).DataContext;
            L2H_NPC itemClicked = vm as L2H_NPC;

            if (activeNPC != null)
            {
                if (activeNPC == itemClicked)
                {
                    activeNPC.IsSelectedTemp = false;
                    Selection_Preview_Grid.Visibility = Visibility.Hidden;
                }
                else
                {
                    activeNPC.IsSelectedTemp = false;
                    activeNPC = itemClicked;
                    Selection_Preview_Grid.Visibility = Visibility.Visible;
                }
            }
            else
            {
                activeNPC = itemClicked;
                activeNPC.IsSelectedTemp = true;
                Selection_Preview_Grid.Visibility = Visibility.Visible;
            }


            Preview_Icon.Source = activeNPC.GetNPCImage();

            Preview_Name.Text = activeNPC.client_Npcname.name;
            Preview_ID.Text = activeNPC.ID.ToString();


            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();

        }

        private void Confirm_Selection(object sender, RoutedEventArgs e)
        {


            if (active_L2H_Spawn_NPC_Maker != null)
            {
                bool succesful = false;

                if (active_L2H_Spawn_NPC_Maker.Npc_Maker != null)
                {
                    if (!active_L2H_Spawn_NPC_Maker.Npc_Maker.npcs.Exists(x => x.id == activeNPC.server_Npcdata.npcName))
                    {
                        active_L2H_Spawn_NPC_Maker.Npc_Maker.npcs.Add(new Npc_Begin(activeNPC.server_Npcdata, active_L2H_Spawn_NPC_Maker.Npc_Maker));
                        succesful = true;
                    }
                    else
                    {
                        MessageBox.Show("Selected NPC Maker already has spawn: " + activeNPC.client_Npcname.name);
                    }
                }
                else
                {
                    if (!active_L2H_Spawn_NPC_Maker.Npc_Maker_Ex.npcs.Exists(x => x.id == activeNPC.server_Npcdata.npcName))
                    {
                        active_L2H_Spawn_NPC_Maker.Npc_Maker_Ex.npcs.Add(new Npc_Ex_Begin(activeNPC.server_Npcdata, active_L2H_Spawn_NPC_Maker.Npc_Maker_Ex));
                        succesful = true;
                    }
                    else
                    {
                        MessageBox.Show("Selected NPC Maker already has spawn: " + activeNPC.client_Npcname.name);
                    }
                }

                if (succesful)
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Add(active_L2H_Spawn_NPC_Maker, activeNPC);
                    activeNPC.IsSelectedTemp = false;
                    (mainWindow.GetPageOfType(typeof(Pages.SpawnsPage)) as Pages.SpawnsPage).Update_NPC_Maker_NPC_Spawns_Panel();
                    Close();
                }
            }
            else if (active_L2H_Multisell != null)
            {
                L2H_Log.Instance.Log_Multisell_NPC_Add(active_L2H_Multisell, activeNPC);
                active_L2H_Multisell.Server_Multisell.requiredL2H_NPCs.Add(activeNPC);
                activeNPC.IsSelectedTemp = false;
                (mainWindow.GetPageOfType(typeof(Pages.MultisellPage)) as Pages.MultisellPage).Refresh_Required_NPCs();
                Close();
            }
            else if (active_L2H_Raid != null)
            {
                L2H_Log.Instance.Log_Raid_Change(active_L2H_Raid, "NPC", active_L2H_Raid.L2H_NPC.NPC_Name, activeNPC.NPC_Name);
                active_L2H_Raid.Set_New_L2H_NPC(activeNPC);
                activeNPC.IsSelectedTemp = false;
                
                (mainWindow.GetPageOfType(typeof(Pages.SpawnsPage)) as Pages.SpawnsPage).Refresh_Zone_Type_Selection();
                Close();
            }

        }
        private bool Filter_NPC_Name(object item)
        {
            L2H_NPC filteredItem = item as L2H_NPC;

            if (filteredItem == null)
                return false;

            if (Item_Filter_Custom_Toggle.IsChecked == true)
            {
                if (!filteredItem.IsCustom)
                    return false;
            }

            if (!string.IsNullOrEmpty(Item_Filter_Name.Text))
            {
                return (filteredItem.client_Npcname.name.IndexOf(Item_Filter_Name.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return true;
        }



        private void Item_Filter_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();
        }

        private void Item_Filter_Name_TextChanged(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();
        }
    }
}
