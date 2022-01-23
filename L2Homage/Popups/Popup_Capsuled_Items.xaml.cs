using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace L2Homage
{
    /// <summary>
    /// Interaction logic for Popup_Capsuled_Items.xaml
    /// </summary>
    public partial class Popup_Capsuled_Items : Window
    {

        public L2H_Item sourceItem;

        public Popup_Capsuled_Items(L2H_Item sourceItem)
        {
            InitializeComponent();
            this.sourceItem = sourceItem;
            
            ResizeMode = ResizeMode.CanResize;
            
        }

        private void Capsuled_Items_Listview_Loaded(object sender, RoutedEventArgs e)
        {
            Capsuled_Items_Listview.ItemsSource = sourceItem.capsuled_Items;
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Capsuled_Item(object sender, RoutedEventArgs e)
        {
            sourceItem.capsuled_Items.Add(new L2H_Capsuled_Item()
            {
                Capsuled_Item = "item_name_id",
                Chance_Min = "min",
                Chance_Max = "max",
                Chance_Trigger = "%"
            });

            CollectionViewSource.GetDefaultView(Capsuled_Items_Listview.ItemsSource).Refresh();
        }

        private void Delete_Capsuled_Item_Clicked(object sender, RoutedEventArgs e)
        {
            sourceItem.capsuled_Items.Remove((sender as Button).DataContext as L2H_Capsuled_Item);

            CollectionViewSource.GetDefaultView(Capsuled_Items_Listview.ItemsSource).Refresh();
        }
    }
}
