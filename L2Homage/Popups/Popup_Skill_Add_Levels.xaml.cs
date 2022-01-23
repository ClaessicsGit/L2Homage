using System.Windows;

namespace L2Homage
{
    public partial class Popup_Skill_Add_Levels : Window
    {
        Pages.SkillsPage skillsPage;

        public Popup_Skill_Add_Levels()
        {
            InitializeComponent();
        }

        public void Initialize(Pages.SkillsPage skillsPage)
        {
            this.skillsPage = skillsPage;
            ShowDialog();
        }

        private void Deny_Decision(object sender, RoutedEventArgs e)
        {
            Close();

        }

        private void Confirm_Decision(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(Levels_Input_Box.Text))
            {
                MessageBox.Show("Incorrect value");
                return;
            }


            try
            {
                int levels = int.Parse(Levels_Input_Box.Text);

                if (levels > 99 || levels < 1)
                {
                    MessageBox.Show("Incorrect Value.\nYou can add between 1 and 99 levels.");
                    return;
                }

                for (int i = 0; i < levels; i++)
                {
                    skillsPage.CloneSkill(sender, e, true);
                }
            }
            catch
            {
                MessageBox.Show("Incorrect value");
                return;
            }

            skillsPage.RefreshViews();

            Close();
        }

    }
}
