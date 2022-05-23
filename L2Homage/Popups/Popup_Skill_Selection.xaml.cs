using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace L2Homage
{
    /// <summary>
    /// Interaction logic for Popup_Skill_Selection.xaml
    /// </summary>
    public partial class Popup_Skill_Selection : Window
    {
        List<L2H_Skill> L2H_Skills;
        L2H_Character_Class active_L2H_Character_Class;
        L2H_Skill active_Skill;

        public Popup_Skill_Selection(List<L2H_Skill> skills)
        {
            InitializeComponent();

            L2H_Skills = skills;

            var newView = new CollectionViewSource() { Source = L2H_Skills };
            var listViewCollection2 = (ListCollectionView)newView.View;
            listViewCollection2.Filter = Filter_Skill;
            Selections_Listview.ItemsSource = listViewCollection2;
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();

        }

        public void Initialize_For_Skillacquire(L2H_Character_Class active_L2H_Character_Class)
        {
            this.active_L2H_Character_Class = active_L2H_Character_Class;
        }

        private void Filter_Changed(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();
        }

        private bool Filter_Skill(object item)
        {
            L2H_Skill filteredSkill = item as L2H_Skill;

            if (filteredSkill == null)
                return false;


            if (filteredSkill.client_Skill == null)
                return false;

            bool returnType = true;

            if (!string.IsNullOrEmpty(Filter_TextBox_Level.Text))
            {
                returnType = (filteredSkill.Skill_Level.IndexOf(Filter_TextBox_Level.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!returnType)
                return returnType;

            if (!string.IsNullOrEmpty(Filter_TextBox_Name.Text))
            {
                returnType = (filteredSkill.client_Skillname.name.IndexOf(Filter_TextBox_Name.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!returnType)
                return returnType;

            if (!string.IsNullOrEmpty(Filter_TextBox_ID.Text))
            {
                returnType = (filteredSkill.ID.IndexOf(Filter_TextBox_ID.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!returnType)
                return returnType;


            return returnType;

        }

        private void Selection_Name_Clicked(object sender, RoutedEventArgs e)
        {
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Skill objClicked = vm as L2H_Skill;

            if (active_Skill != null)
            {
                if (active_Skill == objClicked)
                {
                    active_Skill.IsSelectedTemp = false;
                    Selection_Preview_Grid.Visibility = Visibility.Hidden;
                }
                else
                {
                    active_Skill.IsSelectedTemp = false;
                    active_Skill = objClicked;
                    Selection_Preview_Grid.Visibility = Visibility.Visible;
                }
            }
            else
            {
                
                active_Skill = objClicked;
                active_Skill.IsSelectedTemp = true;
                Selection_Preview_Grid.Visibility = Visibility.Visible;
            }

            Preview_Name.Text = active_Skill.Skill_Name;
            Preview_ID.Text = active_Skill.ID;
            Preview_Icon.Source = active_Skill.GetSkillIcon;
            Preview_Type.Text = "Lvl " + active_Skill.Skill_Level;

            CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();
        }

        private void Confirm_Selection(object sender, RoutedEventArgs e)
        {
            if (active_L2H_Character_Class != null && active_Skill != null)
            {
                L2H_Skill_Acquire newSkillAcquire = new L2H_Skill_Acquire();
                newSkillAcquire.server_Skillacquire = new Server_Skillacquire();
                newSkillAcquire.server_Skillacquire.autoget = "false";
                newSkillAcquire.server_Skillacquire.class_begin = active_L2H_Character_Class.classID + "_begin";
                newSkillAcquire.server_Skillacquire.get_lv = "1";
                newSkillAcquire.server_Skillacquire.skill_name = active_Skill.Skill_Name_ID;
                
                if (active_L2H_Character_Class.L2H_Skill_Acquires.Count > 0)
                    newSkillAcquire.server_Skillacquire.includeClass = active_L2H_Character_Class.L2H_Skill_Acquires[0].server_Skillacquire.includeClass;
                else
                    newSkillAcquire.server_Skillacquire.includeClass = "";

                newSkillAcquire.server_Skillacquire.lv_up_sp = "0";
                newSkillAcquire.server_Skillacquire.item_needed = "{}";
                
                newSkillAcquire.L2H_Skill = active_Skill;

                active_L2H_Character_Class.L2H_Skill_Acquires.Add(newSkillAcquire);

                #region Sioner skill acquire export fix
                //missing addition
                List<Server_Skillacquire> server_Skillacquires = (((MainWindow)Application.Current.MainWindow).GetPageOfType(typeof(Pages.ClassesPage)) as Pages.ClassesPage).server_Skillacquires;
                Boolean matched = false;
                Boolean added = false;
                for (int i = 0; i < server_Skillacquires.Count; i++)
                {
                    Server_Skillacquire sa = server_Skillacquires[i];
                    if (!matched && sa.class_begin.Equals(newSkillAcquire.server_Skillacquire.class_begin))
                    {
                        matched = true;
                    }
                    else if (matched && !sa.class_begin.Equals(newSkillAcquire.server_Skillacquire.class_begin))
                    {
                        server_Skillacquires.Insert(i, newSkillAcquire.server_Skillacquire);
                        added = true;
                        break;
                    }
                }
                if (!added)
                {
                    server_Skillacquires.Add(newSkillAcquire.server_Skillacquire);
                }
                #endregion

                (((MainWindow)Application.Current.MainWindow).GetPageOfType(typeof(Pages.ClassesPage)) as Pages.ClassesPage).Refresh_Skill_Acquire_Data();
            }


            this.Close();
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
