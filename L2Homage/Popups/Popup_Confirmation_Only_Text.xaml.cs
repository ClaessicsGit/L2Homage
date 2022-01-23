using System;
using System.Windows;

namespace L2Homage
{
    /// <summary>
    /// Interaction logic for Popup_Confirmation_Only_Text.xaml
    /// </summary>
    public partial class Popup_Confirmation_Only_Text : Window
    {
        public event EventHandler Confirmation_Action;
        public event EventHandler Post_Confirmation_Action;

        public Popup_Confirmation_Only_Text()
        {
            InitializeComponent();
        }

        public void InitializeConfirmation(string text)
        {
            Confirmation_Description.Text = text;
            ShowDialog();
        }

        private void Deny_Decision(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Decision(object sender, RoutedEventArgs e)
        {
            Confirmation_Action.Invoke(this, EventArgs.Empty);
            if (Post_Confirmation_Action != null)
                Post_Confirmation_Action.Invoke(this, EventArgs.Empty);

            Close();
        }


    }
}
