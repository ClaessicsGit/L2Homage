using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace L2Homage
{
    /// <summary>
    /// Interaction logic for Popup_Class_Start_Equipment.xaml
    /// </summary>
    public partial class Popup_Class_Start_Equipment : Window
    {

        ToggleButton activeClassButton;

        List<Server_InitialEquipment> initialEquipment;
        List<Server_InitialEquipment> initialCustomEquipment;
        List<L2H_Item> L2H_Items;

        Server_InitialEquipment activeEquipment;
        Server_InitialEquipment activeCustomEquipment;



        public Popup_Class_Start_Equipment(List<Server_InitialEquipment> initialEquipment, List<Server_InitialEquipment> initialCustomEquipment, List<L2H_Item> items)
        {
            InitializeComponent();

            this.initialEquipment = initialEquipment;
            this.initialCustomEquipment = initialCustomEquipment;
            L2H_Items = items;

            activeClassButton = Human_Fighter_Class_ToggleButton;

            Initialize_Equipment_Lists();
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Class_Start_Equipment_Remove_Clicked(object sender, RoutedEventArgs e)
        {
            L2H_Initial_Equipment item = (sender as FrameworkElement).DataContext as L2H_Initial_Equipment;
            if (item != null)
            {
                L2H_Log.Instance.Log_Character_Remove_Initial_Equipment(activeEquipment, item.L2H_Item);
                activeEquipment.L2H_Initial_Equipment.Remove(item);
                Refresh_Equipment_Lists();
            }
        }

        private void Class_Start_Custom_Equipment_Remove_Clicked(object sender, RoutedEventArgs e)
        {
            L2H_Initial_Equipment item = (sender as FrameworkElement).DataContext as L2H_Initial_Equipment;
            if (item != null)
            {
                L2H_Log.Instance.Log_Character_Remove_Initial_Equipment(activeEquipment, item.L2H_Item);
                activeCustomEquipment.L2H_Initial_Equipment.Remove(item);
                Refresh_Equipment_Lists();
            }
        }

        private void Class_Start_Equipment_Add_Clicked(object sender, RoutedEventArgs e)
        {
            Popup_Item_Selection popup_Item_Selection = new Popup_Item_Selection(L2H_Items);
            popup_Item_Selection.Initialize_For_Initial_Equipment(this, activeEquipment);
        }

        private void Class_Start_Custom_Equipment_Add_Clicked(object sender, RoutedEventArgs e)
        {
            Popup_Item_Selection popup_Item_Selection = new Popup_Item_Selection(L2H_Items);
            popup_Item_Selection.Initialize_For_Initial_Equipment(this, activeCustomEquipment);
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

            Initialize_Equipment_Lists();

        }

        private void Initialize_Equipment_Lists()
        {
            activeEquipment = initialEquipment.Find(x => x.classID == activeClassButton.Tag.ToString());
            activeCustomEquipment = initialCustomEquipment.Find(x => x.classID == activeClassButton.Tag.ToString());


            for (int i = 0; i < activeEquipment.L2H_Initial_Equipment.Count; i++)
            {
                if (activeEquipment.L2H_Initial_Equipment[i].L2H_Item == null)
                    activeEquipment.L2H_Initial_Equipment[i].L2H_Item = L2H_Items.Find(x => x.server_Itemdata.itemName == activeEquipment.L2H_Initial_Equipment[i].L2H_Item.Item_Name_ID);
            }

            for (int i = 0; i < activeCustomEquipment.L2H_Initial_Equipment.Count; i++)
            {
                if (activeCustomEquipment.L2H_Initial_Equipment[i].L2H_Item == null)
                    activeCustomEquipment.L2H_Initial_Equipment[i].L2H_Item = L2H_Items.Find(x => x.server_Itemdata.itemName == activeCustomEquipment.L2H_Initial_Equipment[i].L2H_Item.Item_Name_ID);
            }

            Class_Initial_Equipment_Listview.ItemsSource = activeEquipment.L2H_Initial_Equipment;
            Class_Initial_Custom_Equipment_Listview.ItemsSource = activeCustomEquipment.L2H_Initial_Equipment;

            Refresh_Equipment_Lists();

        }

        public void Refresh_Equipment_Lists()
        {
            CollectionViewSource.GetDefaultView(Class_Initial_Equipment_Listview.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(Class_Initial_Custom_Equipment_Listview.ItemsSource).Refresh();
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
    }
}
