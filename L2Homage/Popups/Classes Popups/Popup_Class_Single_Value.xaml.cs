using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace L2Homage
{
    /// <summary>
    /// Interaction logic for Popup_Class_Single_Value.xaml
    /// </summary>
    public partial class Popup_Class_Single_Value : Window
    {
        Single_Value single_Value;

        public Popup_Class_Single_Value(Base_Parameter_Single_Value source)
        {

            InitializeComponent();

            single_Value = new Single_Value() { parent = source };
            single_Value.propertyName = source.base_Parameter_ID;

            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Popup_Class_Single_Value_Main_Grid))
            {
                tb.DataContext = single_Value;
            }
            foreach (ToggleButton tb in L2H_Parser.FindVisualChildren<ToggleButton>(Popup_Class_Single_Value_Main_Grid))
            {
                tb.DataContext = single_Value;
            }
            foreach (ComboBox cb in L2H_Parser.FindVisualChildren<ComboBox>(Popup_Class_Single_Value_Main_Grid))
            {
                cb.DataContext = single_Value;
            }

            Class_Property.Text = source.base_Parameter_ID;
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
    }

    public class Single_Value
    {
        public string propertyName { get; set; }
        public Base_Parameter_Single_Value parent { get; set; }

        public Single_Value()
        {

        }

        public string FFighter_Value
        {
            get
            {
                return parent.FFighter_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Female Fighter", propertyName, parent.FFighter_Value, value);
                parent.FFighter_Value = value;
            }
        }
        public string MFighter_Value
        {
            get
            {
                return parent.MFighter_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Male Fighter", propertyName, parent.MFighter_Value, value);
                parent.MFighter_Value = value;
            }
        }
        public string FMagic_Value
        {
            get
            {
                return parent.FMagic_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Female Magic", propertyName, parent.FMagic_Value, value);
                parent.FMagic_Value = value;
            }
        }
        public string MMagic_Value
        {
            get
            {
                return parent.MMagic_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Male Magic", propertyName, parent.MMagic_Value, value);
                parent.MMagic_Value = value;
            }
        }
        public string FElfFighter_Value
        {
            get
            {
                return parent.FElfFighter_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Female Elf Fighter", propertyName, parent.FElfFighter_Value, value);
                parent.FElfFighter_Value = value;
            }
        }
        public string MElfFighter_Value
        {
            get
            {
                return parent.MElfFighter_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Male Elf Fighter", propertyName, parent.MElfFighter_Value, value);
                parent.MElfFighter_Value = value;
            }
        }
        public string FElfMagic_Value
        {
            get
            {
                return parent.FElfMagic_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Female Elf Magic", propertyName, parent.FElfMagic_Value, value);
                parent.FElfMagic_Value = value;
            }
        }
        public string MElfMagic_Value
        {
            get
            {
                return parent.MElfMagic_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Male Elf Magic", propertyName, parent.MElfMagic_Value, value);
                parent.MElfMagic_Value = value;
            }
        }
        public string FDarkelfFighter_Value
        {
            get
            {
                return parent.FDarkelfFighter_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Female Darkelf Fighter", propertyName, parent.FDarkelfFighter_Value, value);
                parent.FDarkelfFighter_Value = value;
            }
        }
        public string MDarkelfFighter_Value
        {
            get
            {
                return parent.MDarkelfFighter_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Male Darkelf Fighter", propertyName, parent.MDarkelfFighter_Value, value);
                parent.MDarkelfFighter_Value = value;
            }
        }
        public string FDarkelfMagic_Value
        {
            get
            {
                return parent.FDarkelfMagic_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Female Darkelf Magic", propertyName, parent.FDarkelfMagic_Value, value);
                parent.FDarkelfMagic_Value = value;
            }
        }
        public string MDarkelfMagic_Value
        {
            get
            {
                return parent.MDarkelfMagic_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Male Darkelf Magic", propertyName, parent.MDarkelfMagic_Value, value);
                parent.MDarkelfMagic_Value = value;
            }
        }
        public string FOrcFighter_Value
        {
            get
            {
                return parent.FOrcFighter_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Female Orc Fighter", propertyName, parent.FOrcFighter_Value, value);
                parent.FOrcFighter_Value = value;
            }
        }
        public string MOrcFighter_Value
        {
            get
            {
                return parent.MOrcFighter_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Male Orc Fighter", propertyName, parent.MOrcFighter_Value, value);
                parent.MOrcFighter_Value = value;
            }
        }
        public string FShaman_Value
        {
            get
            {
                return parent.FShaman_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Female Orc Shaman", propertyName, parent.FShaman_Value, value);
                parent.FShaman_Value = value;
            }
        }
        public string MShaman_Value
        {
            get
            {
                return parent.MShaman_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Male Orc Shaman", propertyName, parent.MShaman_Value, value);
                parent.MShaman_Value = value;
            }
        }
        public string FDwarfFighter_Value
        {
            get
            {
                return parent.FDwarfFighter_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Female Dwarf Fighter", propertyName, parent.FDwarfFighter_Value, value);
                parent.FDwarfFighter_Value = value;
            }
        }
        public string MDwarfFighter_Value
        {
            get
            {
                return parent.MDwarfFighter_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Male Dwarf Fighter", propertyName, parent.MDwarfFighter_Value, value);
                parent.MDwarfFighter_Value = value;
            }
        }
        public string FKamaelSoldier_Value
        {
            get
            {
                return parent.FKamaelSoldier_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Female Kamael Soldier", propertyName, parent.FKamaelSoldier_Value, value);
                parent.FKamaelSoldier_Value = value;
            }
        }
        public string MKamaelSoldier_Value
        {
            get
            {
                return parent.MKamaelSoldier_Value;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Single_Value("Male Kamael Soldier", propertyName, parent.MKamaelSoldier_Value, value);
                parent.MKamaelSoldier_Value = value;
            }
        }

    }
}
