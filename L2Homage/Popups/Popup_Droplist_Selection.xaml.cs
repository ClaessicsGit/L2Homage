using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace L2Homage
{

    public enum Droplist_Selection_Type { all, multi, single }
    

    public partial class Popup_Droplist_Selection : Window
    {
        MainWindow mainWindow;
        L2H_Droplist origin_Droplist;
        Droplist_Selection_Type selection_Type;
        List<L2H_Droplist> L2H_Droplists;

        L2H_NPC origin_NPC;
        DroplistSlot origin_NPC_Droplist_Slot;


        //Droplist stuff
        L2H_Droplist active_L2H_Droplist;
        ToggleButton active_Filter_Droplist_Type;


        public Popup_Droplist_Selection(List<L2H_Droplist> L2H_Droplists)
        {
            InitializeComponent();

            this.L2H_Droplists = L2H_Droplists;
            ResizeMode = ResizeMode.CanResize;
            mainWindow = ((MainWindow)Application.Current.MainWindow);

        }

        public void Initialize_For_Picking_Single_Droplist(L2H_Droplist sender)
        {
            origin_Droplist = sender;

            selection_Type = Droplist_Selection_Type.single;

            Droplist_Filter_Droplist_Type_All.IsChecked = false;
            Droplist_Filter_Droplist_Type_Multi.IsChecked = false;
            Droplist_Filter_Droplist_Type_Single.IsChecked = true;

            Droplist_Filter_Droplist_Type_All.Visibility = Visibility.Hidden;
            Droplist_Filter_Droplist_Type_Multi.Visibility = Visibility.Hidden;
            Droplist_Filter_Droplist_Type_Single.Visibility = Visibility.Hidden;


            var newView = new CollectionViewSource() { Source = L2H_Droplists };
            var listViewCollection2 = (ListCollectionView)newView.View;
            listViewCollection2.Filter = Filter_Droplist;
            Selections_Listview.ItemsSource = listViewCollection2;
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();


            ShowDialog();

        }
        public void Initialize_For_Picking_Single_Droplist(L2H_NPC sender, DroplistSlot slot)
        {
            origin_NPC = sender;
            origin_NPC_Droplist_Slot = slot;


            selection_Type = Droplist_Selection_Type.single;


            Droplist_Filter_Droplist_Type_All.IsChecked = false;
            Droplist_Filter_Droplist_Type_Multi.IsChecked = false;
            Droplist_Filter_Droplist_Type_Single.IsChecked = true;

            Droplist_Filter_Droplist_Type_All.Visibility = Visibility.Hidden;
            Droplist_Filter_Droplist_Type_Multi.Visibility = Visibility.Hidden;
            Droplist_Filter_Droplist_Type_Single.Visibility = Visibility.Hidden;


            var newView = new CollectionViewSource() { Source = L2H_Droplists };
            var listViewCollection2 = (ListCollectionView)newView.View;
            listViewCollection2.Filter = Filter_Droplist;
            Selections_Listview.ItemsSource = listViewCollection2;
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();


            ShowDialog();

        }
        public void Initialize_For_Picking_Multi_Droplist(L2H_Droplist sender)
        {
            this.active_L2H_Droplist = sender;

            selection_Type = Droplist_Selection_Type.multi;

            Droplist_Filter_Droplist_Type_All.IsChecked = false;
            Droplist_Filter_Droplist_Type_Multi.IsChecked = true;
            Droplist_Filter_Droplist_Type_Single.IsChecked = false;

            Droplist_Filter_Droplist_Type_All.Visibility = Visibility.Hidden;
            Droplist_Filter_Droplist_Type_Multi.Visibility = Visibility.Hidden;
            Droplist_Filter_Droplist_Type_Single.Visibility = Visibility.Hidden;

            var newView = new CollectionViewSource() { Source = L2H_Droplists };
            var listViewCollection2 = (ListCollectionView)newView.View;
            listViewCollection2.Filter = Filter_Droplist;
            Selections_Listview.ItemsSource = listViewCollection2;
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();


            ShowDialog();

        }
        public void Initialize_For_Picking_Multi_Droplist(L2H_NPC sender, DroplistSlot slot)
        {
            this.origin_NPC = sender;
            origin_NPC_Droplist_Slot = slot;

            selection_Type = Droplist_Selection_Type.multi;

            Droplist_Filter_Droplist_Type_All.IsChecked = false;
            Droplist_Filter_Droplist_Type_Multi.IsChecked = true;
            Droplist_Filter_Droplist_Type_Single.IsChecked = false;

            Droplist_Filter_Droplist_Type_All.Visibility = Visibility.Hidden;
            Droplist_Filter_Droplist_Type_Multi.Visibility = Visibility.Hidden;
            Droplist_Filter_Droplist_Type_Single.Visibility = Visibility.Hidden;

            var newView = new CollectionViewSource() { Source = L2H_Droplists };
            var listViewCollection2 = (ListCollectionView)newView.View;
            listViewCollection2.Filter = Filter_Droplist;
            Selections_Listview.ItemsSource = listViewCollection2;
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();


            ShowDialog();

        }
        public void Initialize_For_Picking_Any_Droplist(L2H_Droplist sender)
        {
            this.active_L2H_Droplist = sender;

            selection_Type = Droplist_Selection_Type.all;

            Droplist_Filter_Droplist_Type_All.IsChecked = true;
            Droplist_Filter_Droplist_Type_Multi.IsChecked = false;
            Droplist_Filter_Droplist_Type_Single.IsChecked = false;

            Droplist_Filter_Droplist_Type_All.Visibility = Visibility.Visible;
            Droplist_Filter_Droplist_Type_Multi.Visibility = Visibility.Visible;
            Droplist_Filter_Droplist_Type_Single.Visibility = Visibility.Visible;

            var newView = new CollectionViewSource() { Source = L2H_Droplists };
            var listViewCollection2 = (ListCollectionView)newView.View;
            listViewCollection2.Filter = Filter_Droplist;
            Selections_Listview.ItemsSource = listViewCollection2;
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();

            ShowDialog();

        }


        private void Close_Window(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Selections_Listview_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void Droplist_Name_Clicked(object sender, RoutedEventArgs e)
        {
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Droplist droplistClicked = vm as L2H_Droplist;


            if (active_L2H_Droplist != null)
            {
                if (active_L2H_Droplist == droplistClicked)
                {
                    if (!active_L2H_Droplist.IsSelected)
                    {
                        Selection_Single_Droplist_Preview_Grid.Visibility = Visibility.Hidden;
                        Selection_Multi_Droplist_Preview_Grid.Visibility = Visibility.Hidden;
                        active_L2H_Droplist.IsSelected = false;
                        active_L2H_Droplist = null;
                        return;
                    }
                    else
                    {
                        Toggle_Preview(droplistClicked);
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

            if (active_L2H_Droplist.Droplist_Type == L2H_Droplist_Type.single)
            {
                List<L2H_Drop_Details_Single> drops = new List<L2H_Drop_Details_Single>();

                for (int i = 0; i < active_L2H_Droplist.server_Droplist.itemDrops.Count; i++)
                {
                    drops.Add(new L2H_Drop_Details_Single()
                    {
                        L2H_Item = active_L2H_Droplist.server_Droplist.itemDrops[i].l2h_Item,
                        itemDrop = active_L2H_Droplist.server_Droplist.itemDrops[i]
                    });
                }
                Droplists_Drops_Listview.ItemsSource = drops;
            }
            else
            {

                List<L2H_Drop_Details_Multi> drop_Details_Multis = new List<L2H_Drop_Details_Multi>();

                for (int i = 0; i < active_L2H_Droplist.ConnectedDroplists.Count; i++)
                {
                    if (active_L2H_Droplist.ConnectedDroplists[i].Droplist_Type == L2H_Droplist_Type.single)
                    {
                        L2H_Drop_Details_Multi new_Details = new L2H_Drop_Details_Multi()
                        {
                            L2H_Droplist = active_L2H_Droplist.ConnectedDroplists[i],
                            Chance = active_L2H_Droplist.server_Multi_Droplist.separateDroplistChances[i],
                            ID = active_L2H_Droplist.server_Multi_Droplist.separateDroplistIDs[i]
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
            }

            Toggle_Preview(droplistClicked);


            RefreshListview();

        }

        void Toggle_Preview(L2H_Droplist previewTarget)
        {

            switch (previewTarget.Droplist_Type)
            {
                case L2H_Droplist_Type.single:
                    Selection_Single_Droplist_Preview_Grid_Droplist_ID.Text = previewTarget.ID;
                    Selection_Multi_Droplist_Preview_Grid.Visibility = Visibility.Hidden;
                    Selection_Single_Droplist_Preview_Grid.Visibility = Visibility.Visible;
                    break;
                case L2H_Droplist_Type.multi:
                    Selection_Multi_Droplist_Preview_Grid_Droplist_ID.Text = previewTarget.ID;
                    Selection_Multi_Droplist_Preview_Grid.Visibility = Visibility.Visible;
                    Selection_Single_Droplist_Preview_Grid.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }
        }

        private void Confirm_Selection(object sender, RoutedEventArgs e)
        {
            if (origin_Droplist != null)
            {
                switch (selection_Type)
                {
                    case Droplist_Selection_Type.all:
                        break;
                    case Droplist_Selection_Type.multi:
                        break;
                    case Droplist_Selection_Type.single:
                            origin_Droplist.Add_Single_Droplist_To_Multi_Droplist(active_L2H_Droplist);
                        break;
                    default:
                        break;
                }

            (mainWindow.GetPageOfType(typeof(Pages.DroplistsPage)) as Pages.DroplistsPage).Refresh_Drop_Details(origin_Droplist);
            }
            else if (origin_NPC != null)
            {
                switch (origin_NPC_Droplist_Slot)
                {
                    case DroplistSlot.normal:
                        origin_NPC.NPC_Droplist_Normal_ID = active_L2H_Droplist.ID;
                        break;
                    case DroplistSlot.spoil:
                        origin_NPC.NPC_Droplist_Spoil_ID = active_L2H_Droplist.ID;
                        break;
                    case DroplistSlot.multi:
                        origin_NPC.NPC_Droplist_Multi_ID = active_L2H_Droplist.ID;
                        break;
                    case DroplistSlot.extra:
                        origin_NPC.NPC_Droplist_Extra_ID = active_L2H_Droplist.ID;
                        break;
                    default:
                        break;
                }
            }
            active_L2H_Droplist.IsSelected = false;
            Close();
        }
        private void Droplist_Filter_Droplist_Type_Clicked(object sender, RoutedEventArgs e)
        {
            ToggleButton tb = sender as ToggleButton;

            if (active_Filter_Droplist_Type != null)
            {
                active_Filter_Droplist_Type.IsChecked = false;
                active_Filter_Droplist_Type = tb;
                tb.IsChecked = true;
            }
            else
                active_Filter_Droplist_Type = tb;

            RefreshListview();
        }

        private bool Filter_Droplist(object item)
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



            return true;
        }

        private void Droplist_Filter_Changed(object sender, TextChangedEventArgs e)
        {
            RefreshListview();
        }


        private void Droplist_Filter_Changed(object sender, RoutedEventArgs e)
        {
            RefreshListview();
        }



        private void Item_Filter_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshListview();
        }

        private void RefreshListview()
        {
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();
        }
    }
}
