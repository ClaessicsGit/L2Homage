using System;
using System.Windows;

namespace L2Homage
{
    /// <summary>
    /// Interaction logic for Popup_Confirmation.xaml
    /// </summary>
    public partial class Popup_Confirmation : Window
    {
        public event EventHandler Confirmation_Action;
        public event EventHandler Post_Confirmation_Action;
        public event EventHandler Post_Confirmation_Log_Action;
        L2H_Item active_L2H_Item;

        public Popup_Confirmation()
        {
            InitializeComponent();
        }

        public void InitializeConfirmation(string target, string location, string iconPath, L2H_Item active_L2H_Item = null, string LogMessage = "")
        {
            Confirmation_Description_Target.Text = target;
            Confirmation_Description_Location.Text = location;
            Confimation_Description_Icon.Source = L2H_Parser.GetItemImage(iconPath);
            if (active_L2H_Item != null)
                this.active_L2H_Item = active_L2H_Item;
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

            if (Post_Confirmation_Log_Action != null)
                Post_Confirmation_Log_Action.Invoke(this, EventArgs.Empty);

            if (active_L2H_Item != null)
                active_L2H_Item.IsSelected = false;

            Close();
        }

    }
}
