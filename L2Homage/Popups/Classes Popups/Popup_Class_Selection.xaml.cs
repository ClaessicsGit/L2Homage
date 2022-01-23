using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace L2Homage
{
    /// <summary>
    /// Interaction logic for Popup_Class_Selection.xaml
    /// </summary>
    public partial class Popup_Class_Selection : Window
    {

        L2H_Character_Class parent;
        L2H_Character_Class activeCharacterClass;
        List<L2H_Character_Class> classes;

        public Popup_Class_Selection(List<L2H_Character_Class> classes, L2H_Character_Class parent)
        {
            InitializeComponent();

            this.classes = classes;
            this.parent = parent;

            var newView = new CollectionViewSource() { Source = classes };
            var listViewCollection2 = (ListCollectionView)newView.View;
            listViewCollection2.Filter = Filter_Class_ID;
            Selections_Listview.ItemsSource = listViewCollection2;
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();

        }

        private void Class_Name_Clicked(object sender, RoutedEventArgs e)
        {
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Character_Class classClicked = vm as L2H_Character_Class;

            if (activeCharacterClass != null)
            {
                if (activeCharacterClass == classClicked)
                {
                    activeCharacterClass.IsSelectedTemp = false;
                    Selection_Preview_Grid.Visibility = Visibility.Hidden;
                }
                else
                {
                    activeCharacterClass.IsSelectedTemp = false;
                    activeCharacterClass = classClicked;
                    Selection_Preview_Grid.Visibility = Visibility.Visible;
                }
            }
            else
            {
                activeCharacterClass = classClicked;
                activeCharacterClass.IsSelectedTemp = true;
                Selection_Preview_Grid.Visibility = Visibility.Visible;
            }


            Preview_Name.Text = activeCharacterClass.classID;
            Preview_Icon.Source = activeCharacterClass.Get_Class_Image;


            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Selection(object sender, RoutedEventArgs e)
        {
            if (parent.L2H_Skill_Acquires.Count > 0)
            {
                for (int i = 0; i < parent.L2H_Skill_Acquires.Count; i++)
                {
                    parent.L2H_Skill_Acquires[i].server_Skillacquire.includeClass = "include_" + activeCharacterClass.classID.Replace("begin_", "");
                }
            }

            (((MainWindow)Application.Current.MainWindow).GetPageOfType(typeof(Pages.ClassesPage)) as Pages.ClassesPage).Refresh_Skill_Acquire_Data();

            this.Close();
        }

        private bool Filter_Class_ID(object item)
        {
            L2H_Character_Class filteredClass = item as L2H_Character_Class;

            if (filteredClass == null)
                return false;

            if (parent.L2H_Skill_Acquires[0].server_Skillacquire.class_begin == "include_" + filteredClass.classID.Replace("begin_", ""))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(Class_Filter_Name.Text))
                if (filteredClass.classID.Contains(Class_Filter_Name.Text))
                    return true;
                else
                    return false;


            return true;
        }

        private void Filter_Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void Selections_Listview_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
