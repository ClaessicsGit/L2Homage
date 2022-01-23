using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace L2Homage
{
    public partial class Popup_Class_Movement_Speed : Window
    {

        ToggleButton activeClassButton;
        Base_Parameter_Single_Line_Multivalue source;
        Single_Line_Multivalue activeClassValue;


        public Popup_Class_Movement_Speed(Base_Parameter_Single_Line_Multivalue source)
        {
            InitializeComponent();

            this.source = source;

            activeClassButton = Top_Class_ToggleButton;
            activeClassValue = source.MFighter_Value;

            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Movement_Speed_Properties_Grid))
            {
                tb.DataContext = this;
            }

        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        private void Start_Class_Clicked(object sender, RoutedEventArgs e)
        {
            ToggleButton t = sender as ToggleButton;

            if (t == activeClassButton)
            {
                activeClassButton.IsChecked = true;
                return;
            }

            activeClassButton.IsChecked = false;

            activeClassButton = t;

            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Movement_Speed_Properties_Grid))
            {
                tb.DataContext = null;
            }

            switch (activeClassButton.Tag.ToString())
            {
                case "FFighter":
                    {
                        activeClassValue = source.FFighter_Value;
                    }
                    break;
                case "MFighter":
                    {
                        activeClassValue = source.MFighter_Value;
                    }
                    break;
                case "FMagic":
                    {
                        activeClassValue = source.FMagic_Value;
                    }
                    break;
                case "MMagic":
                    {
                        activeClassValue = source.MMagic_Value;
                    }
                    break;
                case "FElfFighter":
                    {
                        activeClassValue = source.FElfFighter_Value;
                    }
                    break;
                case "MElfFighter":
                    {
                        activeClassValue = source.MElfFighter_Value;
                    }
                    break;
                case "FElfMagic":
                    {
                        activeClassValue = source.FElfMagic_Value;
                    }
                    break;
                case "MElfMagic":
                    {
                        activeClassValue = source.MElfMagic_Value;
                    }
                    break;
                case "FDarkelfFighter":
                    {
                        activeClassValue = source.FDarkelfFighter_Value;
                    }
                    break;
                case "MDarkelfFighter":
                    {
                        activeClassValue = source.MDarkelfFighter_Value;
                    }
                    break;
                case "FDarkelfMagic":
                    {
                        activeClassValue = source.FDarkelfMagic_Value;
                    }
                    break;
                case "MDarkelfMagic":
                    {
                        activeClassValue = source.MDarkelfMagic_Value;
                    }
                    break;
                case "FOrcFighter":
                    {
                        activeClassValue = source.FOrcFighter_Value;
                    }
                    break;
                case "MOrcFighter":
                    {
                        activeClassValue = source.MOrcFighter_Value;
                    }
                    break;
                case "FShaman":
                    {
                        activeClassValue = source.FShaman_Value;
                    }
                    break;
                case "MShaman":
                    {
                        activeClassValue = source.MShaman_Value;
                    }
                    break;
                case "FDwarfFighter":
                    {
                        activeClassValue = source.FDwarfFighter_Value;
                    }
                    break;
                case "MDwarfFighter":
                    {
                        activeClassValue = source.MDwarfFighter_Value;
                    }
                    break;
                case "FKamaelSoldier":
                    {
                        activeClassValue = source.FKamaelSoldier_Value;
                    }
                    break;
                case "MKamaelSoldier":
                    {
                        activeClassValue = source.MKamaelSoldier_Value;
                    }
                    break;

                default:
                    break;
            }

            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Movement_Speed_Properties_Grid))
            {
                tb.DataContext = this;
            }

        }

        public string Ground_Low_Speed
        {
            get
            {
                return activeClassValue.values[0];
            }
            set
            {
                L2H_Log.Instance.Log_Class_Movement_Speed(activeClassValue.classID, "Ground Low Speed", activeClassValue.values[0], value);
                activeClassValue.values[0] = value;
            }
        }
        public string Ground_High_Speed
        {
            get
            {
                return activeClassValue.values[1];
            }
            set
            {
                L2H_Log.Instance.Log_Class_Movement_Speed(activeClassValue.classID, "Ground High Speed", activeClassValue.values[1], value);
                activeClassValue.values[1] = value;
            }
        }
        public string Underwater_Low_Speed
        {
            get
            {
                return activeClassValue.values[2];
            }
            set
            {
                L2H_Log.Instance.Log_Class_Movement_Speed(activeClassValue.classID, "Underwater Low Speed", activeClassValue.values[2], value);
                activeClassValue.values[2] = value;
            }
        }
        public string Underwater_High_Speed
        {
            get
            {
                return activeClassValue.values[3];
            }
            set
            {
                L2H_Log.Instance.Log_Class_Movement_Speed(activeClassValue.classID, "Underwater High Speed", activeClassValue.values[3], value);
                activeClassValue.values[3] = value;
            }
        }
        public string Flying_Low_Speed
        {
            get
            {
                return activeClassValue.values[4];
            }
            set
            {
                L2H_Log.Instance.Log_Class_Movement_Speed(activeClassValue.classID, "Flying Low Speed", activeClassValue.values[4], value);
                activeClassValue.values[4] = value;
            }
        }
        public string Flying_High_Speed
        {
            get
            {
                return activeClassValue.values[5];
            }
            set
            {
                L2H_Log.Instance.Log_Class_Movement_Speed(activeClassValue.classID, "Flying High Speed", activeClassValue.values[5], value);
                activeClassValue.values[5] = value;
            }
        }
        public string Floating_Low_Speed
        {
            get
            {
                return activeClassValue.values[6];
            }
            set
            {
                L2H_Log.Instance.Log_Class_Movement_Speed(activeClassValue.classID, "Floating Low Speed", activeClassValue.values[6], value);
                activeClassValue.values[6] = value;
            }
        }
        public string Floating_High_Speed
        {
            get
            {
                return activeClassValue.values[7];
            }
            set
            {
                L2H_Log.Instance.Log_Class_Movement_Speed(activeClassValue.classID, "Floating High Speed", activeClassValue.values[7], value);
                activeClassValue.values[7] = value;
            }
        }


    }
}
