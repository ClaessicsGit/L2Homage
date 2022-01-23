using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace L2Homage
{

    public enum Popup_Item_Pick_Type { all, sets, recipes_add_result, recipes_replace_result, recipes_add_material, recipes_replace_material, initial_equipment, character_creation_equipment, multisell_add_sale, multisell_add_cost, multisell_add_bundled_item }

    public partial class Popup_Item_Selection : Window
    {
        MainWindow mainWindow;
        L2H_Item activeItem;
        Popup_Item_Pick_Type pick_Type;
        List<L2H_Item> L2H_Items;

        //Recipe stuff
        L2H_Recipe active_L2H_Recipe;
        L2H_Recipe_Result replace_Result;
        L2H_Recipe_Material replace_Material;

        //Set stuff
        L2H_Set active_L2H_Set;
        Set_Slot_Type set_slot;

        //Droplist stuff
        L2H_Droplist active_L2H_Droplist;

        //Initial Equipment stuff
        Server_InitialEquipment active_Initial_Equipment;
        Popup_Class_Start_Equipment active_Class_Equipment_Popup;
        Popup_Class_Create_Equipment active_Class_Create_Equipment_Popup;

        //Character Creation Stuff
        string slot_bit_type;

        //Multisell stuff
        L2H_Multisell active_L2H_Multisell;
        L2H_MultisellSale active_L2H_MultisellSale;


        bool firstLoad = true;

        public Popup_Item_Selection(List<L2H_Item> L2H_Items)
        {
            InitializeComponent();

            this.L2H_Items = L2H_Items;
            ResizeMode = ResizeMode.CanResize;
            mainWindow = ((MainWindow)Application.Current.MainWindow);

        }

        public void Initialize_For_Sets(Button sender, L2H_Set active_L2H_Set, Set_Slot_Type set_slot)
        {
            pick_Type = Popup_Item_Pick_Type.sets;
            this.active_L2H_Set = active_L2H_Set;
            this.set_slot = set_slot;

            Initialize_ItemView();
            ShowDialog();
        }

        public void Initialize_For_Recipes(Button sender, Popup_Item_Pick_Type pick_Type, L2H_Recipe active_L2H_Recipe, L2H_Recipe_Result replace_Result = null, L2H_Recipe_Material replace_Material = null)
        {
            this.pick_Type = pick_Type;

            if (replace_Result != null)
                this.replace_Result = replace_Result;

            if (replace_Material != null)
                this.replace_Material = replace_Material;

            this.active_L2H_Recipe = active_L2H_Recipe;

            if (!firstLoad)
                CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();

            Initialize_ItemView();
            firstLoad = false;

            ShowDialog();
        }

        public void Initialize_For_Droplists(L2H_Droplist sender)
        {
            this.active_L2H_Droplist = sender;
            pick_Type = Popup_Item_Pick_Type.all;
            Initialize_ItemView();
            ShowDialog();

        }

        public void Initialize_For_Initial_Equipment(Popup_Class_Start_Equipment active_Class_Equipment_Popup, Server_InitialEquipment initialEquipment)
        {
            this.active_Class_Equipment_Popup = active_Class_Equipment_Popup;
            this.active_Initial_Equipment = initialEquipment;
            pick_Type = Popup_Item_Pick_Type.initial_equipment;
            Initialize_ItemView();
            ShowDialog();

        }

        public void Initialize_For_Character_Creation_Equipment(Popup_Class_Create_Equipment active_Class_Create_Equipment_Popup, string slot_bit_type)
        {
            pick_Type = Popup_Item_Pick_Type.character_creation_equipment;
            this.active_Class_Create_Equipment_Popup = active_Class_Create_Equipment_Popup;

            this.slot_bit_type = slot_bit_type;
            Initialize_ItemView();
            ShowDialog();
        }

        public void Initialize_For_Multisell_Sale_Add_Item(L2H_Multisell active_Multisell, Popup_Item_Pick_Type pick_Type)
        {
            this.pick_Type = pick_Type;
            this.active_L2H_Multisell = active_Multisell;
            Initialize_ItemView();
            ShowDialog();
        }
        public void Initialize_For_Multisell_Bundle_Add_Item(L2H_MultisellSale active_MultisellSale, Popup_Item_Pick_Type pick_Type)
        {
            this.pick_Type = pick_Type;
            this.active_L2H_MultisellSale = active_MultisellSale;
            this.active_L2H_Multisell = active_L2H_MultisellSale.parent;
            Initialize_ItemView();
            ShowDialog();
        }

        public void Initialize_For_Multisell_Sale_Add_Cost(L2H_MultisellSale active_MultisellSale, Popup_Item_Pick_Type pick_Type)
        {
            this.pick_Type = pick_Type;
            this.active_L2H_MultisellSale = active_MultisellSale;
            this.active_L2H_Multisell = active_MultisellSale.parent;
            Initialize_ItemView();
            ShowDialog();
        }

        void Initialize_ItemView()
        {
            var newView = new CollectionViewSource() { Source = L2H_Items };
            var listViewCollection2 = (ListCollectionView)newView.View;
            listViewCollection2.Filter = Filter_Item_Name;
            Selections_Listview.ItemsSource = listViewCollection2;
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            if (activeItem != null)
                activeItem.IsSelectedTemp = false;

            Close();
        }

        private void Selections_Listview_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Item_Name_Clicked(object sender, RoutedEventArgs e)
        {
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Item itemClicked = vm as L2H_Item;

            if (activeItem != null)
            {
                if (activeItem == itemClicked)
                {
                    activeItem.IsSelectedTemp = false;
                    Item_Selection_Preview_Grid.Visibility = Visibility.Hidden;
                }
                else
                {
                    activeItem.IsSelectedTemp = false;
                    activeItem = itemClicked;
                    Item_Selection_Preview_Grid.Visibility = Visibility.Visible;
                }
            }
            else
            {
                activeItem = itemClicked;
                activeItem.IsSelectedTemp = true;
                Item_Selection_Preview_Grid.Visibility = Visibility.Visible;
            }

            if (activeItem.client_Weapon != null)
            {
                Preview_Icon.Source = L2H_Parser.GetItemImage(activeItem.client_Weapon.icon[0]);
                Preview_Type.Text = "Weapon";
            }
            else if (activeItem.client_Armor != null)
            {
                Preview_Icon.Source = L2H_Parser.GetItemImage(activeItem.client_Armor.icon[0]);
                Preview_Type.Text = "Armor";
            }
            else if (activeItem.client_Etc != null)
            {
                Preview_Icon.Source = L2H_Parser.GetItemImage(activeItem.client_Etc.icon[0]);
                Preview_Type.Text = "Etc";
            }

            Preview_Name.Text = activeItem.client_Itemname.name;
            Preview_ID.Text = activeItem.ID.ToString();


            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();

        }

        private void Confirm_Selection(object sender, RoutedEventArgs e)
        {
            if (pick_Type == Popup_Item_Pick_Type.all)
            {
                    active_L2H_Droplist.server_Droplist.itemDrops.Add(new ItemDrop(activeItem));
                    L2H_Log.Instance.Log_Droplist_Add_Single_Drop(active_L2H_Droplist, activeItem);
                    (mainWindow.GetPageOfType(typeof(Pages.DroplistsPage)) as Pages.DroplistsPage).Refresh_Drop_Details(active_L2H_Droplist);
            }
            else if (pick_Type == Popup_Item_Pick_Type.sets)
            {
                switch (set_slot)
                {
                    case Set_Slot_Type.head_A:
                        active_L2H_Set.Slot_head_A = activeItem;
                        break;
                    case Set_Slot_Type.head_B:
                        active_L2H_Set.Slot_head_B = activeItem;
                        break;
                    case Set_Slot_Type.chest:
                        active_L2H_Set.Slot_chest = activeItem;
                        break;
                    case Set_Slot_Type.legs_A:
                        active_L2H_Set.Slot_legs_A = activeItem;
                        break;
                    case Set_Slot_Type.legs_B:
                        active_L2H_Set.Slot_legs_B = activeItem;
                        break;
                    case Set_Slot_Type.gloves_A:
                        active_L2H_Set.Slot_gloves_A = activeItem;
                        break;
                    case Set_Slot_Type.gloves_B:
                        active_L2H_Set.Slot_gloves_B = activeItem;
                        break;
                    case Set_Slot_Type.feet_A:
                        active_L2H_Set.Slot_feet_A = activeItem;
                        break;
                    case Set_Slot_Type.feet_B:
                        active_L2H_Set.Slot_feet_B = activeItem;
                        break;
                    case Set_Slot_Type.additional_A:
                        active_L2H_Set.Slot_additional_A = activeItem;
                        break;
                    case Set_Slot_Type.additional_B:
                        active_L2H_Set.Slot_additional_B = activeItem;
                        break;
                    default:
                        break;
                }

            (mainWindow.GetPageOfType(typeof(Pages.ItemsPage)) as Pages.ItemsPage).RefreshGridInterface(L2H_Item_Category.Sets);
            }
            else if (pick_Type == Popup_Item_Pick_Type.recipes_add_material)
            {
                if (active_L2H_Recipe.AddMaterial(activeItem, 1, true))
                    Close();
                else
                    return;
            }
            else if (pick_Type == Popup_Item_Pick_Type.recipes_replace_material)
            {
                int index = active_L2H_Recipe.recipe_Materials.FindIndex(x => x == replace_Material);
                active_L2H_Recipe.recipe_Materials[index] = new L2H_Recipe_Material() { item = activeItem, amount = active_L2H_Recipe.recipe_Materials[index].amount };
            }
            else if (pick_Type == Popup_Item_Pick_Type.recipes_add_result)
            {
                int chance = 100;
                if (active_L2H_Recipe.recipe_Results.Count > 1)
                    chance = 100 - active_L2H_Recipe.recipe_Results[0].chance;

                active_L2H_Recipe.AddResult(activeItem, chance, 1, true);
            }
            else if (pick_Type == Popup_Item_Pick_Type.recipes_replace_result)
            {
                int index = active_L2H_Recipe.recipe_Results.FindIndex(x => x == replace_Result);
                active_L2H_Recipe.recipe_Results[index] = new L2H_Recipe_Result() { item = activeItem, amount = active_L2H_Recipe.recipe_Results[index].amount, chance = active_L2H_Recipe.recipe_Results[index].chance };
            }
            else if (pick_Type == Popup_Item_Pick_Type.initial_equipment)
            {
                active_Initial_Equipment.L2H_Initial_Equipment.Add(new L2H_Initial_Equipment(active_Initial_Equipment, activeItem, "1"));
                L2H_Log.Instance.Log_Character_Add_Initial_Equipment(active_Initial_Equipment, activeItem);
                active_Class_Equipment_Popup.Refresh_Equipment_Lists();
            }
            else if (pick_Type == Popup_Item_Pick_Type.character_creation_equipment)
            {
                active_Class_Create_Equipment_Popup.Update_Equipment_Slot(activeItem, slot_bit_type);

            }
            else if (pick_Type == Popup_Item_Pick_Type.multisell_add_cost)
            {
                L2H_MultisellCost newCost = new L2H_MultisellCost(active_L2H_MultisellSale) { L2H_Item = activeItem, Amount = "1", non_item_name = "" };
                active_L2H_MultisellSale.costs.Add(newCost);
                L2H_Log.Instance.Log_Multisell_Cost_Add(active_L2H_Multisell, active_L2H_MultisellSale, newCost);
                (mainWindow.GetPageOfType(typeof(Pages.MultisellPage)) as Pages.MultisellPage).Refresh_Multisell_Costs_Listview();
            }
            else if (pick_Type == Popup_Item_Pick_Type.multisell_add_sale)
            {
                L2H_MultisellSale newSale = new L2H_MultisellSale(active_L2H_Multisell, activeItem);
                active_L2H_Multisell.Server_Multisell.elements.Add(newSale);
                L2H_Log.Instance.Log_Multisell_Sale_Add(active_L2H_Multisell, newSale);
                (mainWindow.GetPageOfType(typeof(Pages.MultisellPage)) as Pages.MultisellPage).Refresh_Multisell_Sales_Listview();
            }
            else if (pick_Type == Popup_Item_Pick_Type.multisell_add_bundled_item)
            {
                active_L2H_MultisellSale.Multisell_Sale_Items.Add(new L2H_MultisellSale_Item(active_L2H_MultisellSale, activeItem, "1"));
                L2H_Log.Instance.Log_Multisell_Sale_Bundle_Add(active_L2H_Multisell, active_L2H_MultisellSale, activeItem);
                (mainWindow.GetPageOfType(typeof(Pages.MultisellPage)) as Pages.MultisellPage).Refresh_Multisell_Sales_Listview();
            }

            if (pick_Type != Popup_Item_Pick_Type.sets && 
                pick_Type != Popup_Item_Pick_Type.all && 
                pick_Type != Popup_Item_Pick_Type.initial_equipment && 
                pick_Type != Popup_Item_Pick_Type.character_creation_equipment &&
                pick_Type != Popup_Item_Pick_Type.multisell_add_cost &&
                pick_Type != Popup_Item_Pick_Type.multisell_add_sale &&
                pick_Type != Popup_Item_Pick_Type.multisell_add_bundled_item
                )
                (mainWindow.GetPageOfType(typeof(Pages.RecipesPage)) as Pages.RecipesPage).RefreshGridInterface();

            activeItem.IsSelectedTemp = false;
            Close();
        }
        private bool Filter_Item_Name(object item)
        {
            L2H_Item filteredItem = item as L2H_Item;

            if (filteredItem == null)
                return false;


            //should also filter grade and custom only

            if (pick_Type == Popup_Item_Pick_Type.sets)
            {
                if (set_slot == Set_Slot_Type.additional_A || set_slot == Set_Slot_Type.additional_B)
                {
                    if (filteredItem.server_Itemdata.item_type != "armor" && filteredItem.server_Itemdata.item_type != "accessary" && filteredItem.server_Itemdata.item_type != "weapon")
                        return false;

                    //Not sure if other slots can be used here, but for now limiting it to lhand
                    if (filteredItem.server_Itemdata.slot_bit_type != "lhand")
                        return false;

                }
                else
                {
                    if (filteredItem.server_Itemdata.item_type != "armor" && filteredItem.server_Itemdata.item_type != "accessary")
                        return false;

                    switch (set_slot)
                    {
                        case Set_Slot_Type.head_A:
                            if (filteredItem.server_Itemdata.slot_bit_type != "head")
                                return false;
                            break;
                        case Set_Slot_Type.head_B:
                            if (filteredItem.server_Itemdata.slot_bit_type != "head")
                                return false;
                            break;
                        case Set_Slot_Type.chest:
                            if (filteredItem.server_Itemdata.slot_bit_type != "chest")
                                return false;
                            break;
                        case Set_Slot_Type.legs_A:
                            if (filteredItem.server_Itemdata.slot_bit_type != "legs")
                                return false;
                            break;
                        case Set_Slot_Type.legs_B:
                            if (filteredItem.server_Itemdata.slot_bit_type != "legs")
                                return false;
                            break;
                        case Set_Slot_Type.gloves_A:
                            if (filteredItem.server_Itemdata.slot_bit_type != "gloves")
                                return false;
                            break;
                        case Set_Slot_Type.gloves_B:
                            if (filteredItem.server_Itemdata.slot_bit_type != "gloves")
                                return false;
                            break;
                        case Set_Slot_Type.feet_A:
                            if (filteredItem.server_Itemdata.slot_bit_type != "feet")
                                return false;
                            break;
                        case Set_Slot_Type.feet_B:
                            if (filteredItem.server_Itemdata.slot_bit_type != "feet")
                                return false;
                            break;
                        default:
                            break;
                    }

                }

            }

            if (pick_Type == Popup_Item_Pick_Type.character_creation_equipment)
            {
                if (slot_bit_type == "weapon")
                {
                    if (filteredItem.server_Itemdata.slot_bit_type != "rhand" && filteredItem.server_Itemdata.slot_bit_type != "lrhand")
                        return false;
                }
                else if (filteredItem.server_Itemdata.slot_bit_type != slot_bit_type)
                    return false;
            }

            if (!string.IsNullOrEmpty(Item_Filter_Name.Text))
            {
                return (filteredItem.client_Itemname.name.IndexOf(Item_Filter_Name.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return true;
        }



        private void Item_Filter_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();
        }
    }
}
