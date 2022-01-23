using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace L2Homage
{
    /// <summary>
    /// Interaction logic for Popup_Class_Description.xaml
    /// </summary>
    public partial class Popup_Class_Description : Window
    {

        ToggleButton activeClassButton;

        List<Client_Classinfo> classinfos;

        Client_Classinfo activeClassinfo;


        public Popup_Class_Description(List<Client_Classinfo> classinfos)
        {
            InitializeComponent();

            this.classinfos = classinfos;

            activeClassButton = Human_Fighter_Class_ToggleButton;
            activeClassinfo = classinfos.Find(x => x.id == activeClassButton.Tag.ToString());
            Class_Description_TextBox.DataContext = this;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.FocusedElement != null)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;
                Keyboard.ClearFocus();
                focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
            }
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            if (Keyboard.FocusedElement != null)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;
                Keyboard.ClearFocus();
                focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
            }

            this.Close();
        }

        private void Start_Class_Clicked(object sender, RoutedEventArgs e)
        {
            ToggleButton t = sender as ToggleButton;

            if (t == activeClassButton)
            {
                activeClassButton.IsChecked = true;
                return;
            }

            if (Keyboard.FocusedElement != null)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;
                Keyboard.ClearFocus();
                focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
            }

            activeClassButton.IsChecked = false;

            activeClassButton = t;

            activeClassinfo = classinfos.Find(x => x.id == activeClassButton.Tag.ToString());
            Class_Description_TextBox.Text = Class_Description;

        }


        public string Class_Description
        {
            get
            {
                if (activeClassinfo != null)
                    return activeClassinfo.description;
                else
                    return "";
            }
            set
            {
                if (activeClassinfo != null)
                {
                    activeClassinfo.description = value;
                }
                else
                {

                }

            }
        }

        private void Validate_Any_Case_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Ingame_Name(e.Text);
        }
    }
}
