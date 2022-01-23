using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace L2Homage
{
    /// <summary>
    /// Interaction logic for Popup_Class_Create_Equipment.xaml
    /// </summary>
    public partial class Popup_Class_Create_Equipment : Window
    {
        List<Client_Charcreate> source;
        List<L2H_Item> L2H_Items;
        ToggleButton activeClassButton;
        Button activeEquipmentButton;

        Client_Charcreate activeCharCreate;



        public Popup_Class_Create_Equipment(List<Client_Charcreate> source, List<L2H_Item> L2H_Items)
        {
            InitializeComponent();
            activeClassButton = Top_Class_ToggleButton;
            this.L2H_Items = L2H_Items;
            this.source = source;

            Set_Active_Char_Create();
            

        }
        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void Start_Class_Clicked(object sender, RoutedEventArgs e)
        {
            ToggleButton t = sender as ToggleButton;

            if (t == activeClassButton)
            {
                activeClassButton.IsChecked = true;
                return;
            }

            activeClassButton.IsChecked = false;

            activeClassButton = t;

            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Equipment_Grid))
            {
                tb.DataContext = null;
            }

            Set_Active_Char_Create();

        }

        private void Set_Active_Char_Create()
        {
            switch (activeClassButton.Tag.ToString())
            {
                case "FFighter":
                    {
                        activeCharCreate = source[1];
                    }
                    break;
                case "MFighter":
                    {
                        activeCharCreate = source[0];
                    }
                    break;
                case "FMagic":
                    {
                        activeCharCreate = source[3];
                    }
                    break;
                case "MMagic":
                    {
                        activeCharCreate = source[2];
                    }
                    break;
                case "FElfFighter":
                    {
                        activeCharCreate = source[5];
                    }
                    break;
                case "MElfFighter":
                    {
                        activeCharCreate = source[4];
                    }
                    break;
                case "FElfMagic":
                    {
                        activeCharCreate = source[7];
                    }
                    break;
                case "MElfMagic":
                    {
                        activeCharCreate = source[6];
                    }
                    break;
                case "FDarkelfFighter":
                    {
                        activeCharCreate = source[9];
                    }
                    break;
                case "MDarkelfFighter":
                    {
                        activeCharCreate = source[8];
                    }
                    break;
                case "FDarkelfMagic":
                    {
                        activeCharCreate = source[11];
                    }
                    break;
                case "MDarkelfMagic":
                    {
                        activeCharCreate = source[10];
                    }
                    break;
                case "FOrcFighter":
                    {
                        activeCharCreate = source[13];
                    }
                    break;
                case "MOrcFighter":
                    {
                        activeCharCreate = source[12];
                    }
                    break;
                case "FShaman":
                    {
                        activeCharCreate = source[15];
                    }
                    break;
                case "MShaman":
                    {
                        activeCharCreate = source[14];
                    }
                    break;
                case "FDwarfFighter":
                    {
                        activeCharCreate = source[17];
                    }
                    break;
                case "MDwarfFighter":
                    {
                        activeCharCreate = source[16];
                    }
                    break;
                case "FKamaelSoldier":
                    {
                        activeCharCreate = source[19];
                    }
                    break;
                case "MKamaelSoldier":
                    {
                        activeCharCreate = source[18];
                    }
                    break;

                default:
                    break;
            }

            Refresh_Items();
        }


        private void Equipment_Slot_Clicked(object sender, RoutedEventArgs e)
        {
            activeEquipmentButton = sender as Button;

            Popup_Item_Selection dialog = new Popup_Item_Selection(L2H_Items);
            dialog.Initialize_For_Character_Creation_Equipment(this, (sender as Button).Tag.ToString());

        }

        private void Equipment_Slot_RightClicked(object sender, MouseButtonEventArgs e)
        {
            Popup_Confirmation newPopup = new Popup_Confirmation();

            L2H_Item target_Item;

            switch ((sender as Button).Tag.ToString())
            {
                case "weapon":
                    target_Item = L2H_Items.Find(x => x.Item_ID == activeCharCreate.ints_4);
                    newPopup.Confirmation_Action += (b, bb) => activeCharCreate.ints_4 = "0";
                    break;
                case "lhand":
                    target_Item = L2H_Items.Find(x => x.Item_ID == activeCharCreate.ints_5);
                    newPopup.Confirmation_Action += (b, bb) => activeCharCreate.ints_5 = "0";
                    break;
                case "chest":
                    target_Item = L2H_Items.Find(x => x.Item_ID == activeCharCreate.ints_0);
                    newPopup.Confirmation_Action += (b, bb) => activeCharCreate.ints_0 = "0";
                    break;
                case "legs":
                    target_Item = L2H_Items.Find(x => x.Item_ID == activeCharCreate.ints_1);
                    newPopup.Confirmation_Action += (b, bb) => activeCharCreate.ints_1 = "0";
                    break;
                case "gloves":
                    target_Item = L2H_Items.Find(x => x.Item_ID == activeCharCreate.ints_2);
                    newPopup.Confirmation_Action += (b, bb) => activeCharCreate.ints_2 = "0";
                    break;
                case "feet":
                    target_Item = L2H_Items.Find(x => x.Item_ID == activeCharCreate.ints_3);
                    newPopup.Confirmation_Action += (b, bb) => activeCharCreate.ints_3 = "0";
                    break;
                default:
                    target_Item = null;
                    break;
            }


            if (target_Item == null)
            {
                newPopup.Close();
                return;
            }

            newPopup.Post_Confirmation_Action += (b, bb) => Refresh_Items();
            newPopup.Post_Confirmation_Log_Action += (b, bb) => L2H_Log.Instance.Log_Character_Create_Item_Remove(activeClassButton.Tag.ToString(), (sender as Button).Tag.ToString(), target_Item);
            newPopup.InitializeConfirmation(target_Item.client_Itemname.name, "Character Creation Equipment: " + activeClassButton.Tag.ToString() + "?", target_Item.GetIconString(), target_Item);

        }

        public void Update_Equipment_Slot(L2H_Item item, string slot_bit_type)
        {
            switch (slot_bit_type)
            {
                case "weapon":
                    {
                        activeCharCreate.ints_4 = item.Item_ID;
                        Equipment_Details_Preview_Weapon_Icon.Source = item.GetItemIcon;
                        Equipment_Details_Preview_Weapon_Name.Text = item.Item_Name;

                    }
                    break;
                case "lhand":
                    {
                        activeCharCreate.ints_5 = item.Item_ID;
                        Equipment_Details_Preview_Offhand_Icon.Source = item.GetItemIcon;
                        Equipment_Details_Preview_Offhand_Name.Text = item.Item_Name;
                    }
                    break;
                case "chest":
                    {
                        activeCharCreate.ints_0 = item.Item_ID;
                        Equipment_Details_Preview_Chest_Icon.Source = item.GetItemIcon;
                        Equipment_Details_Preview_Chest_Name.Text = item.Item_Name;
                    }
                    break;
                case "legs":
                    {
                        activeCharCreate.ints_1 = item.Item_ID;
                        Equipment_Details_Preview_Pants_Icon.Source = item.GetItemIcon;
                        Equipment_Details_Preview_Pants_Name.Text = item.Item_Name;
                    }
                    break;
                case "gloves":
                    {
                        activeCharCreate.ints_2 = item.Item_ID;
                        Equipment_Details_Preview_Gloves_Icon.Source = item.GetItemIcon;
                        Equipment_Details_Preview_Gloves_Name.Text = item.Item_Name;
                    }
                    break;
                case "feet":
                    {
                        activeCharCreate.ints_3 = item.Item_ID;
                        Equipment_Details_Preview_Boots_Icon.Source = item.GetItemIcon;
                        Equipment_Details_Preview_Boots_Name.Text = item.Item_Name;
                    }
                    break;
                default:
                    break;
            }

            L2H_Log.Instance.Log_Character_Create_Item_Change(activeClassButton.Tag.ToString(), slot_bit_type, item);

            Check_For_Incompatible_Item_Combination(item);
        }

        private void Refresh_Items()
        {
            Update_Equipment_Slot_Preview(Equipment_Details_Preview_Chest_Icon, Equipment_Details_Preview_Chest_Name, activeCharCreate.ints_0);
            Update_Equipment_Slot_Preview(Equipment_Details_Preview_Pants_Icon, Equipment_Details_Preview_Pants_Name, activeCharCreate.ints_1);
            Update_Equipment_Slot_Preview(Equipment_Details_Preview_Gloves_Icon, Equipment_Details_Preview_Gloves_Name, activeCharCreate.ints_2);
            Update_Equipment_Slot_Preview(Equipment_Details_Preview_Boots_Icon, Equipment_Details_Preview_Boots_Name, activeCharCreate.ints_3);
            Update_Equipment_Slot_Preview(Equipment_Details_Preview_Weapon_Icon, Equipment_Details_Preview_Weapon_Name, activeCharCreate.ints_4);
            Update_Equipment_Slot_Preview(Equipment_Details_Preview_Offhand_Icon, Equipment_Details_Preview_Offhand_Name, activeCharCreate.ints_5);

        }

        private void Update_Equipment_Slot_Preview(Image image, TextBlock text, string itemID)
        {
            if (itemID == "0")
            {
                image.Source = L2H_Parser.GetItemImage("");
                text.Text = "No Item";
                return;
            }

            L2H_Item item = L2H_Items.Find(x => x.Item_ID == itemID);
            image.Source = item.GetItemIcon;
            text.Text = item.Item_Name;

            Check_For_Incompatible_Item_Combination(item);
        }

        private void Check_For_Incompatible_Item_Combination(L2H_Item item)
        {
            if (item.server_Itemdata.slot_bit_type == "lrhand")
            {
                if (activeCharCreate.ints_5 != "0")
                {
                    activeCharCreate.ints_5 = "0";
                    Equipment_Details_Preview_Offhand_Icon.Source = L2H_Parser.GetItemImage("");
                    Equipment_Details_Preview_Offhand_Name.Text = "";
                }
            }
            else if (item.server_Itemdata.slot_bit_type == "lhand")
                if (activeCharCreate.ints_4 != "0")
                {
                    L2H_Item item2 = L2H_Items.Find(x => x.Item_ID == activeCharCreate.ints_4);
                    if (item2.server_Itemdata.slot_bit_type != "rhand")
                    {
                        activeCharCreate.ints_4 = "0";
                        Equipment_Details_Preview_Weapon_Icon.Source = L2H_Parser.GetItemImage("");
                        Equipment_Details_Preview_Weapon_Name.Text = "";
                    }
                }
        }
    }

    class Equipment_Button
    {
        public L2H_Item item { get; set; }
        public BitmapImage GetImage { get { return item.GetItemIcon; } }
        public string GetName { get { return item.Item_Name; } }
    }
}
