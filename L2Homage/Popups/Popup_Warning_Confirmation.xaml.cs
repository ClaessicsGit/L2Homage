using System;
using System.Windows;

namespace L2Homage
{
    /// <summary>
    /// Interaction logic for Popup_Warning_Confirmation.xaml
    /// </summary>
    public partial class Popup_Warning_Confirmation : Window
    {

        public event EventHandler Confirmation_Action;
        public event EventHandler Post_Confirmation_Action;

        public Popup_Warning_Confirmation()
        {
            InitializeComponent();
        }

        public void InitializeWarningConfirmation(string warningText)
        {
            Confirmation_Description_Details.Text = warningText;
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
