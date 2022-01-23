using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace L2Homage
{
    /// <summary>
    /// Interaction logic for Popup_Capsuled_Items.xaml
    /// </summary>
    public partial class Popup_Multiple_Selections_Multiple_Choice : Window
    {

        public L2H_Item sourceItem;
        List<string> selections;
        Button sender;

        public Popup_Multiple_Selections_Multiple_Choice(Button sender, L2H_Item sourceItem)
        {
            InitializeComponent();
            this.sender = sender;
            this.sourceItem = sourceItem;
            this.selections = L2H_Constants.GetSelectionsList((Popup_Choice_Selection)Enum.Parse(typeof(Popup_Choice_Selection), sender.Tag.ToString()));
            Popup_Title.Text = L2H_Constants.GetSelectionsTitle((Popup_Choice_Selection)Enum.Parse(typeof(Popup_Choice_Selection), sender.Tag.ToString()));
            
            ResizeMode = ResizeMode.CanResize;
            
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Selections_Listview_Loaded(object sender, RoutedEventArgs e)
        {
            Selections_Listview.ItemsSource = selections;
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();
        }

        private void Update_Item_Property(string newValue)
        {
            
            switch ((Popup_Choice_Selection)Enum.Parse(typeof(Popup_Choice_Selection), sender.Tag.ToString()))
            {
                case Popup_Choice_Selection.condition_equip:
                    sourceItem.server_Itemdata.consume_type = newValue;
                    break;
                case Popup_Choice_Selection.condition_situation:
                    sourceItem.server_Itemdata.default_action = newValue;
                    break;
                case Popup_Choice_Selection.condition_use:
                    sourceItem.server_Itemdata.etcitem_type = newValue;
                    break;
                default:
                    break;
            }
        }

        private void Selection_Clicked(object sender, RoutedEventArgs e)
        {
            var vm = sender as Button;
            Update_Item_Property(vm.Content.ToString());
            this.sender.Content = vm.Content;
            this.Close();
            
        }

        private void Selection_Toggled(object sender, RoutedEventArgs e)
        {

        }
    }
}
