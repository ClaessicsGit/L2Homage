using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.Windows.Shapes.Path;

namespace L2Homage.Pages
{
    /// <summary>
    /// Interaction logic for ExpPage.xaml
    /// </summary>
    public partial class ExpPage : Page
    {
        protected bool isDragging;
        private Point clickPosition;
        private TranslateTransform originTT;

        List<Exp_Table_Value> tableValues;
        List<long> longTableValues;

        double lowestValue = 0;
        double highestValue = 0;
        double differenceValue = 0;

        List<Ellipse> points;

        Path activePath;

        float pointDiameter = 5;

        MainWindow mainWindow;

        public ExpPage()
        {
            InitializeComponent();
            points = new List<Ellipse>();
            LoadExpTable();
            mainWindow = (MainWindow)Application.Current.MainWindow;
        }

        public void LoadExpTable()
        {
            tableValues = new List<Exp_Table_Value>();
            string expTablePath = L2H_Constants.server_expdata_Path;
            bool fileExists = false;

            if (File.Exists(L2H_Constants.L2H_Custom_Exp_Table_Path))
            {
                expTablePath = L2H_Constants.L2H_Custom_Exp_Table_Path;
                fileExists = true;
            }
            else
            {
                if (!File.Exists(L2H_Constants.server_expdata_Path))
                {
                    MessageBox.Show("expdata.txt not found in Data/server. That one is pretty important.");
                    Environment.Exit(0);
                }
            }
            // Load text
            using (TextReader textReader = new StreamReader(expTablePath, Encoding.GetEncoding(1200)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    if (!line.Contains("exp_table"))
                    {
                        tableValues.Add(new Exp_Table_Value(line));
                    }
                }
            }

            if (!fileExists)
            {
                SaveExpTable();
            }


        }

        public void SaveExpTable()
        {
            File.WriteAllLines(L2H_Constants.L2H_Custom_Exp_Table_Path, GetExportStrings().ToArray(), Encoding.GetEncoding(1200));
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Initialize_Pieces();
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

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var draggableControl = sender as Shape;
            originTT = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
            isDragging = true;
            clickPosition = e.GetPosition(this);
            draggableControl.CaptureMouse();

            Refresh_Lines();
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;

            var draggableControl = sender as Shape;

            if (draggableControl != null)
            {
                var transform = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();

                (draggableControl.DataContext as Exp_Table_Value).Value = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString("F0");

            }


            draggableControl.ReleaseMouseCapture();
            SaveExpTable();
            Initialize_Pieces();


        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var draggableControl = sender as Shape;
            if (isDragging && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(this);
                var transform = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
                transform.X = transform.X;
                transform.Y = originTT.Y + (currentPosition.Y - clickPosition.Y);

                if (transform.Y < 0)
                    transform.Y = 0;
                if (transform.Y > Canvas_Diagram.ActualHeight - pointDiameter)
                    transform.Y = (Canvas_Diagram.ActualHeight - pointDiameter);

                draggableControl.RenderTransform = new TranslateTransform(transform.X, transform.Y);

                Value_Textblock.Text = "Value: " + Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString("F0");


                Refresh_Lines();

            }
        }

        private void Canvas_Mouse_Leave(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
            }
        }

        private void Canvas_Mouse_Enter(object sender, MouseEventArgs e)
        {
        }

        private void Initialize_Pieces(object sender, RoutedEventArgs e)
        {
            Initialize_Pieces();
            Initialize_Level_Listview();
        }

        private void Initialize_Pieces()
        {
            Canvas_Diagram.Children.Clear();
            Levels_Text_Grid.Children.Clear();
            Levels_Text_Grid.ColumnDefinitions.Clear();
            points.Clear();

            double distance = Canvas_Diagram.ActualWidth / tableValues.Count;
            List<double> pointValues = new List<double>();

            for (int i = 0; i < tableValues.Count; i++)
            {
                pointValues.Add(double.Parse(tableValues[i].Value));
            }

            lowestValue = pointValues.Min();
            highestValue = pointValues.Max();
            differenceValue = highestValue - lowestValue;

            for (int i = 0; i < tableValues.Count; i++)
            {
                Ellipse newEllipse = Create_New_Point();
                newEllipse.DataContext = tableValues[i];

                double percentageValue = ((double.Parse(tableValues[i].Value) - lowestValue) / (highestValue - lowestValue));
                Position_Point(newEllipse, i, distance, percentageValue);
                Add_New_Level_Text(i + 1);

            }


            Refresh_Lines();
        }

        private Ellipse Create_New_Point()
        {
            Ellipse newEllipse = new Ellipse();
            newEllipse.Width = pointDiameter;
            newEllipse.Height = pointDiameter;
            newEllipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f2f2f2"));
            newEllipse.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;
            newEllipse.MouseLeftButtonDown += Canvas_MouseLeftButtonDown;
            newEllipse.MouseMove += Canvas_MouseMove;

            Canvas_Diagram.Children.Add(newEllipse);

            return newEllipse;
        }

        private void Position_Point(Ellipse point, int index, double distance, double percentageValue)
        {
            var transform = point.RenderTransform as TranslateTransform ?? new TranslateTransform();
            transform.X = (distance * index) + (distance / 2.00);

            double positionValue = (Canvas_Diagram.ActualHeight - pointDiameter - ((Canvas_Diagram.ActualHeight - pointDiameter) * percentageValue));

            transform.Y = positionValue;

            point.RenderTransform = transform;

            points.Add(point);
        }

        private void Add_New_Level_Text(int value)
        {
            TextBlock newLevelText = new TextBlock();
            newLevelText.Text = value.ToString();
            newLevelText.TextAlignment = TextAlignment.Center;
            newLevelText.Style = this.FindResource("HomageLoadedText") as Style;
            newLevelText.MinWidth = 2;
            newLevelText.Margin = new Thickness(0);
            newLevelText.FontSize = 8;

            Levels_Text_Grid.ColumnDefinitions.Add(new ColumnDefinition());
            Levels_Text_Grid.Children.Add(newLevelText);
            newLevelText.SetValue(Grid.ColumnProperty, value - 1);
        }

        private Grid Create_Skill_List_Entry()
        {
            Grid newGrid = new Grid();
            newGrid.Margin = new Thickness(4, 0, 8, 0);
            newGrid.Height = 26;
            newGrid.Width = 188;
            newGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            newGrid.Focusable = false;
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { MaxWidth = 60 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition());

            return newGrid;
        }

        private void Initialize_Level_Listview()
        {

            Levels_Listview.Items.Clear();

            if (points.Count == 0)
            {
                return;
            }

            for (int i = 0; i < tableValues.Count; i++)
            {
                Grid newGrid = Create_Skill_List_Entry();
                TextBlock levelTextBlock = new TextBlock();
                levelTextBlock.Margin = new Thickness(0);
                levelTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                levelTextBlock.TextAlignment = TextAlignment.Center;
                levelTextBlock.MinWidth = 10;
                levelTextBlock.Style = Application.Current.FindResource("HomagePropertyTitleText") as Style;

                Binding bindingTextBlock = new Binding("ID");
                bindingTextBlock.Source = tableValues[i];
                bindingTextBlock.Mode = BindingMode.OneWay;
                levelTextBlock.SetBinding(TextBlock.TextProperty, bindingTextBlock);

                TextBox levelTextBox = new TextBox();
                levelTextBox.TextAlignment = TextAlignment.Left;
                levelTextBox.Style = Application.Current.FindResource("HomagePropertyTextBox") as Style;
                levelTextBox.Tag = "float";
                levelTextBox.PreviewTextInput += Validate_Integer_Input;
                levelTextBox.LostFocus += LevelTextBox_LostFocus;
                levelTextBox.FontFamily = new FontFamily("lucida_console");

                Binding bindingTextBox = new Binding();
                bindingTextBox.Path = new PropertyPath("Value");

                bindingTextBox.Source = tableValues[i];
                bindingTextBox.Mode = BindingMode.TwoWay;
                bindingTextBox.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                levelTextBox.SetBinding(TextBox.TextProperty, bindingTextBox);

                newGrid.Children.Add(levelTextBlock);
                Grid.SetColumn(levelTextBlock, 0);

                newGrid.Children.Add(levelTextBox);
                Grid.SetColumn(levelTextBox, 1);
                Levels_Listview.Items.Add(newGrid);

            }

        }

        private void LevelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LevelTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Initialize_Pieces();
            SaveExpTable();
        }

        private void Refresh_Lines()
        {

            List<Point> tempPointsList = new List<Point>();

            for (int i = 0; i < points.Count; i++)
            {
                Point newPoint = new Point();

                var transform = points[i].RenderTransform as TranslateTransform ?? new TranslateTransform();
                newPoint.X = transform.X + (pointDiameter / 2f);
                newPoint.Y = transform.Y + (pointDiameter / 2f);

                tempPointsList.Add(newPoint);
            }

            if (activePath != null)
            {
                Canvas_Diagram.Children.Remove(activePath);
                activePath = null;
            }

            if (tempPointsList.Count == 0)
                return;

            PolyLineSegment pls = new PolyLineSegment(tempPointsList.ToArray(), true);
            PathFigure pf = new PathFigure(pls.Points[0], new[] { pls }, false);
            PathFigureCollection pfc = new PathFigureCollection();
            pfc.Add(pf);
            var pge = new PathGeometry();
            pge.Figures = pfc;
            Path p = new Path();
            activePath = p;
            p.IsHitTestVisible = false;
            p.Data = pge;
            p.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8f8f8f"));

            Canvas_Diagram.Children.Add(p);


        }

        private void Initialize_LongTableValues()
        {
            longTableValues = new List<long>();

            for (int i = 0; i < tableValues.Count; i++)
            {
                longTableValues.Add(Int64.Parse(tableValues[i].Value));
            }
            if (longTableValues == null)
            {
                MessageBox.Show("Could not find level exp values, something went wrong");
            }
        }


        private void Validate_Integer_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Integer(e.Text);
        }
        private void Validate_Float_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Float(e.Text);

        }
        private void Validate_Lower_Case_And_Symbols_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Lower_Case_And_Symbols(e.Text);
        }

        private void TextBox_Float_No_Spaces_Allowed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;

            if (L2H_Textbox_Input_Restrictions.Is_Valid_Float(e.Key.ToString()))
                L2H_Textbox_Input_Restrictions.Check_If_Dot_Exists_In_Float_TextBox(sender, e);
        }

        public List<string> GetExportStrings()
        {
            List<string> exportList = new List<string>();

            exportList.Add("exp_table_begin");
            for (int i = 0; i < tableValues.Count; i++)
            {
                exportList.Add(tableValues[i].GetExportString());
            }
            exportList.Add("exp_table_end");

            return exportList;

        }

        private void Recalculate_NPCs_Keep_Exp_Clicked(object sender, RoutedEventArgs e)
        {
            Popup_Warning_Confirmation dialog = new Popup_Warning_Confirmation();
            dialog.Confirmation_Action += (b, bb) => Recalculate_NPCs_Keep_Exp();
            dialog.InitializeWarningConfirmation("This action will iterate through all NPCs, adjusting their level based on their EXP value. That means the EXP value for every NPC will remain the same, but levels may change, depending on the values in the Experience Table.");
            

        }
        void Recalculate_NPCs_Keep_Exp()
        {
            List<L2H_NPC> npcs = (mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage).L2H_Npcs;

            if (npcs.Count == 0)
            {
                MessageBox.Show("No NPCs loaded.");
                return;
            }

            Initialize_LongTableValues();

            for (int i = 0; i < npcs.Count; i++)
            {
                npcs[i].server_Npcdata.level = GetLevelByExp(npcs[i].server_Npcdata.exp);
                npcs[i].server_Npcdata.acquire_exp_rate = L2H_Parser.GetNPCExpSpRpRate(npcs[i].server_Npcdata, npcs[i].NPC_Kill_Experience, "xp");
            }

            MessageBox.Show("All NPC level values adjusted.\n\nEXP Values remain the same.");
        }

        private void Recalculate_NPCs_Keep_Levels_Clicked(object sender, RoutedEventArgs e)
        {
            Popup_Warning_Confirmation dialog = new Popup_Warning_Confirmation();
            dialog.Confirmation_Action += (b, bb) => Recalculate_NPCs_Keep_Levels();
            dialog.InitializeWarningConfirmation("This action will iterate through all NPCs, adjusting their EXP values based on their LEVEL. That means the LEVEL for every NPC will remain the same, but EXP values may change, depending on the values in the Experience Table.");
            
        }
        void Recalculate_NPCs_Keep_Levels()
        {
            List<L2H_NPC> npcs = (mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage).L2H_Npcs;

            if (npcs.Count == 0)
            {
                MessageBox.Show("No NPCs loaded.");
                return;
            }
            Initialize_LongTableValues();

            for (int i = 0; i < npcs.Count; i++)
            {
                npcs[i].server_Npcdata.acquire_exp_rate = L2H_Parser.GetNPCExpSpRpRate(npcs[i].server_Npcdata, npcs[i].NPC_Kill_Experience, "xp");

                Int64 thisLevelEXP = Int64.Parse(GetExpByLevel(npcs[i].server_Npcdata.level));
                Int64 existingEXP = Int64.Parse(npcs[i].server_Npcdata.exp);
                Int64 nextLevelEXP = Int64.Parse(GetExpByLevel(npcs[i].server_Npcdata.level + 1));

                if (existingEXP < thisLevelEXP || existingEXP > nextLevelEXP)
                {
                    npcs[i].server_Npcdata.exp = GetExpByLevel(npcs[i].server_Npcdata.level);
                }

            }
            MessageBox.Show("All NPC EXP values adjusted.\n\nLevels remain the same.");
        }
        private void Recalculate_NPCs_Exp_On_Kill_Clicked(object sender, RoutedEventArgs e)
        {
            Popup_Warning_Confirmation dialog = new Popup_Warning_Confirmation();
            dialog.Confirmation_Action += (b, bb) => Recalculate_NPCs_Exp_On_Kill();
            dialog.InitializeWarningConfirmation("This action is entirely OPTIONAL. If you continue, this will iterate through all NPCs, adjusting the EXP a player gets when killing them. This is not 100% accurate, but it is a decent approximation. The calculation is based on the difference between the NPC's original level's EXP value and its current level's EXP value. Example: If the original NPC's level was 20, that would mean it's EXP value would be roughly 835862. If you've changed the Experience Table, so the EXP value for level 20 is now 420069, the EXP value will be multiplied by (420069 divided by 835862 = 0.5025). If the original NPC gives 800exp on kill, it now gives (800 * 0.5025 = 402). This calculation assumes you're using an EXP curve similar to the original game, and it's only intended to give EXP on kill values that are closer to your intended values. This action should only be performed ONE SINGLE TIME per project, as the recalculation will adjust the amount every time this button is pressed. Use wisely.");
            
        }
        void Recalculate_NPCs_Exp_On_Kill()
        {
            List<L2H_NPC> npcs = (mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage).L2H_Npcs;

            if (npcs.Count == 0)
            {
                MessageBox.Show("No NPCs loaded.");
                return;
            }
            Initialize_LongTableValues();

            //Show warning prompt, then calculate the new exp per kill rewards.
            //Warning should let users know only to do this once

            for (int i = 0; i < npcs.Count; i++)
            {
                if (!string.IsNullOrEmpty(npcs[i].NPC_Kill_Experience))
                {
                    int currentLevel = int.Parse(npcs[i].server_Npcdata.level);
                    double differenceInPercentage = (double)longTableValues[currentLevel - 1] / (double)L2H_Constants.OriginalExpTable[currentLevel - 1];
                    double currentKillValue = double.Parse(npcs[i].NPC_Kill_Experience);
                    string newKillValue = (currentKillValue * differenceInPercentage).ToString("F0");

                    npcs[i].server_Npcdata.acquire_exp_rate = L2H_Parser.GetNPCExpSpRpRate(npcs[i].server_Npcdata, newKillValue, "xp");
                }
                else
                    continue;
            }

            MessageBox.Show("All NPC Experience on kill values adjusted.");

        }

        private void Recalculate_NPCs_SP_On_Kill_Clicked(object sender, RoutedEventArgs e)
        {
            Popup_Warning_Confirmation dialog = new Popup_Warning_Confirmation();
            dialog.Confirmation_Action += (b, bb) => Recalculate_NPCs_SP_On_Kill();
            dialog.InitializeWarningConfirmation("This action is entirely OPTIONAL. If you continue, this will iterate through all NPCs, adjusting the SKILL POINTS a player gets when killing them. This is not 100% accurate, but it is a decent approximation. The calculation is based on the difference between the NPC's original level's EXP value and its current level's EXP value. Example: If the original NPC's level was 20, that would mean it's EXP value would be roughly 835862. If you've changed the Experience Table, so the EXP value for level 20 is now 420069, the EXP value will be multiplied by (420069 divided by 835862 = 0.5025). If the original NPC gives 800 SKILL POINTS on kill, it now gives (800 * 0.5025 = 402). This calculation assumes you're using an EXP curve similar to the original game, and it's only intended to give SKILL POINTS on kill values that are closer to your intended values. This action should only be performed ONE SINGLE TIME per project, as the recalculation will adjust the amount every time this button is pressed. Use wisely.");

        }

        void Recalculate_NPCs_SP_On_Kill()
        {
            List<L2H_NPC> npcs = (mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage).L2H_Npcs;

            if (npcs.Count == 0)
            {
                MessageBox.Show("No NPCs loaded.");
                return;
            }
            Initialize_LongTableValues();

            for (int i = 0; i < npcs.Count; i++)
            {
                if (!string.IsNullOrEmpty(npcs[i].NPC_Kill_Skill_Points))
                {
                    int currentLevel = int.Parse(npcs[i].server_Npcdata.level);
                    double differenceInPercentage = (double)longTableValues[currentLevel - 1] / (double)L2H_Constants.OriginalExpTable[currentLevel - 1];
                    double currentKillValue = double.Parse(npcs[i].NPC_Kill_Skill_Points);
                    string newKillValue = (currentKillValue * differenceInPercentage).ToString("F0");

                    npcs[i].server_Npcdata.acquire_exp_rate = L2H_Parser.GetNPCExpSpRpRate(npcs[i].server_Npcdata, newKillValue, "sp");
                }
                else
                    continue;
            }
            MessageBox.Show("All NPC Skill Points on kill values adjusted.");
        }

        private void Recalculate_NPCs_RP_On_Kill_Clicked(object sender, RoutedEventArgs e)
        {
            Popup_Warning_Confirmation dialog = new Popup_Warning_Confirmation();
            dialog.Confirmation_Action += (b, bb) => Recalculate_NPCs_RP_On_Kill();
            dialog.InitializeWarningConfirmation("This action is entirely OPTIONAL. If you continue, this will iterate through all NPCs, adjusting the REPUTATION POINTS a player gets when killing them. This is not 100% accurate, but it is a decent approximation. The calculation is based on the difference between the NPC's original level's EXP value and its current level's EXP value. Example: If the original NPC's level was 20, that would mean it's EXP value would be roughly 835862. If you've changed the Experience Table, so the EXP value for level 20 is now 420069, the EXP value will be multiplied by (420069 divided by 835862 = 0.5025). If the original NPC gives 800 REPUTATION POINTS on kill, it now gives (800 * 0.5025 = 402). This calculation assumes you're using an EXP curve similar to the original game, and it's only intended to give REPUTATION POINTS on kill values that are closer to your intended values. This action should only be performed ONE SINGLE TIME per project, as the recalculation will adjust the amount every time this button is pressed. Use wisely.");

        }
        void Recalculate_NPCs_RP_On_Kill()
        {
            List<L2H_NPC> npcs = (mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage).L2H_Npcs;

            if (npcs.Count == 0)
            {
                MessageBox.Show("No NPCs loaded.");
                return;
            }
            Initialize_LongTableValues();

            for (int i = 0; i < npcs.Count; i++)
            {
                if (!string.IsNullOrEmpty(npcs[i].NPC_Kill_Reputation_Points))
                {
                    int currentLevel = int.Parse(npcs[i].server_Npcdata.level);
                    double differenceInPercentage = (double)longTableValues[currentLevel - 1] / (double)L2H_Constants.OriginalExpTable[currentLevel - 1];
                    double currentKillValue = double.Parse(npcs[i].NPC_Kill_Reputation_Points);
                    string newKillValue = (currentKillValue * differenceInPercentage).ToString("F0");

                    npcs[i].server_Npcdata.acquire_exp_rate = L2H_Parser.GetNPCExpSpRpRate(npcs[i].server_Npcdata, newKillValue, "rp");
                }
                else
                    continue;
            }

            MessageBox.Show("All NPC Reputation Points on kill values adjusted.");
        }

        public string GetExpByLevel(string level)
        {
            if (!string.IsNullOrEmpty(level))
            {
                int parsedLevel = 0;
                if (parsedLevel < 1)
                    parsedLevel = 1;

                Initialize_LongTableValues();

                if (int.Parse(level) > longTableValues.Count)
                {
                    return "0";
                }

                if (int.TryParse(level, out parsedLevel))
                    return longTableValues[parsedLevel].ToString();
                else
                    return "0";

            }
            else
            {
                MessageBox.Show("No Level entered. EXP cannot be calculated.");
                return "0";
            }
        }
        public string GetLevelByExp(string exp)
        {
            if (!string.IsNullOrEmpty(exp))
            {
                Int64 parsedExp = 0;

                if (Int64.TryParse(exp, out parsedExp))
                {
                    int levelIndex = 0;
                    Initialize_LongTableValues();
                    for (int i = 0; i < longTableValues.Count; i++)
                    {
                        if (parsedExp > longTableValues[i])
                            levelIndex = i;
                        else
                            break;
                    }

                    return (levelIndex + 1).ToString();
                }
                else
                {
                    return "1";
                }
            }
            else
            {
                MessageBox.Show("No EXP entered. Level cannot be calculated.");
                return "1";
            }
        }

        private void Help_Clicked(object sender, RoutedEventArgs e)
        {
            //Add custom info popup
        }

        private void Set_Exp_Table_To_Original_Values_Clicked(object sender, RoutedEventArgs e)
        {
            mainWindow.UpdateLog("\nExp table reset to original values", L2H_Constants.Color_Modify);

            for (int i = 0; i < tableValues.Count; i++)
            {
                tableValues[i].value = L2H_Constants.OriginalExpTable[i].ToString();
            }

            SaveExpTable();
            Initialize_Pieces();
        }

    }


    public class Exp_Table_Value : INotifyPropertyChanged
    {
        public string value;
        public string ID { get; set; }
        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                L2H_Log.Instance.Log_Exp_Change(this, this.value, value);
                this.value = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Exp_Table_Value(string source)
        {
            string filteredString = source.Replace(" ", "");
            string[] splitString = filteredString.Split(new string[] { "exp=" }, StringSplitOptions.None);

            ID = splitString[0].Replace("level=", "");
            value = splitString[1];
        }
        public string GetExportString()
        {
            return " level=" + ID + " exp=" + value + " ";
        }

    }
}
