using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace L2Homage
{
    public partial class Popup_Class_Stats : Window
    {

        ToggleButton activeClassButton;
        RaceStats activeRaceStats;

        List<RaceStats> raceStats;


        public Popup_Class_Stats(List<RaceStats> raceStats, string title)
        {
            InitializeComponent();

            this.raceStats = raceStats;

            Stats_Title_Textblock.Text = title;
            activeClassButton = Human_Fighter_Class_ToggleButton;
            activeRaceStats = raceStats.Find(x => x.RaceClass == activeClassButton.Tag.ToString());
            Refresh_Stats();
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
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

            activeClassButton.IsChecked = false;

            activeClassButton = t;

            Refresh_Stats();

        }

        void Refresh_Stats()
        {
            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Class_Stats_Properties_Grid))
            {
                tb.DataContext = null;
            }

            activeRaceStats = raceStats.Find(x => x.RaceClass == activeClassButton.Tag.ToString());
            

            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Class_Stats_Properties_Grid))
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

        public string INT
        {
            get
            {
                return activeRaceStats.INT;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Race_Stats(activeRaceStats, "INT", activeRaceStats.INT, value);
                activeRaceStats.INT = value;
            }
        }
        public string STR
        {
            get
            {
                return activeRaceStats.STR;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Race_Stats(activeRaceStats, "STR", activeRaceStats.STR, value);
                activeRaceStats.STR = value;
            }
        }
        public string CON
        {
            get
            {
                return activeRaceStats.CON;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Race_Stats(activeRaceStats, "CON", activeRaceStats.CON, value);
                activeRaceStats.CON = value;
            }
        }
        public string DEX
        {
            get
            {
                return activeRaceStats.DEX;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Race_Stats(activeRaceStats, "DEX", activeRaceStats.DEX, value);
                activeRaceStats.DEX = value;
            }
        }
        public string MEN
        {
            get
            {
                return activeRaceStats.MEN;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Race_Stats(activeRaceStats, "MEN", activeRaceStats.MEN, value);
                activeRaceStats.MEN = value;
            }
        }
        public string WIT
        {
            get
            {
                return activeRaceStats.WIT;
            }
            set
            {
                L2H_Log.Instance.Log_Class_Base_Race_Stats(activeRaceStats, "WIT", activeRaceStats.WIT, value);
                activeRaceStats.WIT = value;
            }
        }
    }
}