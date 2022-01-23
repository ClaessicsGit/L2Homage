using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace L2Homage
{
    public partial class Popup_Class_Weight_Parameters : Window
    {
        public Base_Parameter_Multiple_Line_Multivalue parameters;

        public Popup_Class_Weight_Parameters(Base_Parameter_Multiple_Line_Multivalue parameters)
        {

            InitializeComponent();

            this.parameters = parameters;

            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Popup_Class_Weight_Parameters_Main_Grid))
            {
                tb.DataContext = this;
            }

            switch (parameters.parameterID)
            {
                case "org_hp_regen_weight_begin":
                    {
                        Weights_Title_Textblock.Text = "HP Regen Weight";
                    }
                    break;
                case "org_mp_regen_weight_begin":
                    {
                        Weights_Title_Textblock.Text = "MP Regen Weight";
                    }
                    break;
                case "org_cp_regen_weight_begin":
                    {
                        Weights_Title_Textblock.Text = "CP Regen Weight";
                    }
                    break;
                default:
                    break;
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


        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public string sit
        {
            get
            {
                return parameters.values[parameters.levelIDs.FindIndex(x => x == "sit")];
            }
            set
            {
                L2H_Log.Instance.Log_Character_Table_Multiple_Line_Multivalue(parameters, sit, value);
                parameters.values[parameters.levelIDs.FindIndex(x => x == "sit")] = value;
            }
        }

        public string stand
        {
            get
            {
                return parameters.values[parameters.levelIDs.FindIndex(x => x == "stand")];
            }
            set
            {
                L2H_Log.Instance.Log_Character_Table_Multiple_Line_Multivalue(parameters, stand, value);
                parameters.values[parameters.levelIDs.FindIndex(x => x == "stand")] = value;
            }
        }

        public string low
        {
            get
            {
                return parameters.values[parameters.levelIDs.FindIndex(x => x == "low")];
            }
            set
            {
                L2H_Log.Instance.Log_Character_Table_Multiple_Line_Multivalue(parameters, low, value);
                parameters.values[parameters.levelIDs.FindIndex(x => x == "low")] = value;
            }
        }

        public string high
        {
            get
            {
                return parameters.values[parameters.levelIDs.FindIndex(x => x == "high")];
            }
            set
            {
                L2H_Log.Instance.Log_Character_Table_Multiple_Line_Multivalue(parameters, high, value);
                parameters.values[parameters.levelIDs.FindIndex(x => x == "high")] = value;
            }
        }

    }

}
