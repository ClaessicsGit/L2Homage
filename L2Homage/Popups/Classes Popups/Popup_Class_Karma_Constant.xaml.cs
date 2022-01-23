using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace L2Homage
{
    public partial class Popup_Class_Karma_Constant : Window
    {
        public Base_Parameter_Multiple_Line_Multivalue karmaConstant;

        public Popup_Class_Karma_Constant(Base_Parameter_Multiple_Line_Multivalue karmaConstant)
        {

            InitializeComponent();

            this.karmaConstant = karmaConstant;

            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Popup_Class_Karma_Constant_Main_Grid))
            {
                tb.DataContext = this;
            }

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

        public string penalty_start_karma
        {
            get
            {
                return karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "penalty_start_karma")];
            }
            set
            {
                L2H_Log.Instance.Log_Class_Karma_Constant_Multivalue("penalty_start_karma", karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "penalty_start_karma")], value);
                karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "penalty_start_karma")] = value;
            }
        }
        public string penalty_duration_default
        {
            get
            {
                return karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "penalty_duration_default")];
            }
            set
            {
                L2H_Log.Instance.Log_Class_Karma_Constant_Multivalue("penalty_duration_default", karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "penalty_duration_default")], value);
                karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "penalty_duration_default")] = value;
            }
        }
        public string penalty_duration_increase
        {
            get
            {
                return karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "penalty_duration_increase")];
            }
            set
            {
                L2H_Log.Instance.Log_Class_Karma_Constant_Multivalue("penalty_duration_increase", karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "penalty_duration_increase")], value);
                karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "penalty_duration_increase")] = value;
            }
        }
        public string down_time_multiple
        {
            get
            {
                return karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "down_time_multiple")];
            }
            set
            {
                L2H_Log.Instance.Log_Class_Karma_Constant_Multivalue("down_time_multiple", karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "down_time_multiple")], value);
                karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "down_time_multiple")] = value;
            }
        }
        public string criminal_duration_multiple
        {
            get
            {
                return karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "criminal_duration_multiple")];
            }
            set
            {
                L2H_Log.Instance.Log_Class_Karma_Constant_Multivalue("criminal_duration_multiple", karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "criminal_duration_multiple")], value);
                karmaConstant.values[karmaConstant.levelIDs.FindIndex(x => x == "criminal_duration_multiple")] = value;
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }

}
