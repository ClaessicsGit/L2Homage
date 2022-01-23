using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace L2Homage
{
    public enum Character_Table_Type { Multiple_Line_Multivalue, Single_Line_Multivalue }
    /// <summary>
    /// Interaction logic for Popup_Character_Table.xaml
    /// </summary>
    public partial class Popup_Character_Table : Window
    {
        public Character_Table_Type table_Type;
        protected bool isDragging;
        private Point clickPosition;
        private TranslateTransform originTT;

        int baseClassIndex = 0;

        public Base_Parameter_Multiple_Line_Multivalue multiple_Line_Multivalue_Table;
        public Base_Parameter_Single_Line_Multivalue single_Line_Multivalue_Table;

        List<Table_Value> table_Values;

        double lowestValue = 0;
        double highestValue = 0;
        double differenceValue = 0;

        List<Ellipse> points;
        Path activePath;

        float pointDiameter = 5;

        bool usingFloat = false;

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        public Popup_Character_Table(Base_Parameter_Multiple_Line_Multivalue table)
        {
            InitializeComponent();
            Class_Base_Properties_Selection_Grid.Visibility = Visibility.Hidden;


            bool foundFloat = false;
            for (int i = 0; i < table.values.Count; i++)
            {
                if (table.values[i].Contains("."))
                    foundFloat = true;
            }

            usingFloat = foundFloat;


            table_Type = Character_Table_Type.Multiple_Line_Multivalue;
            points = new List<Ellipse>();
            this.multiple_Line_Multivalue_Table = table;
            Initialize_Levels();

            Class_Name.Text = table.classID;
            Class_Icon.Source = L2H_Parser.GetClassImage(table.classID);
            Class_Table_ID.Text = table.parameterID.Replace("_begin", "");



        }

        public Popup_Character_Table(Base_Parameter_Single_Line_Multivalue table, int baseClassIndex)
        {
            InitializeComponent();
            Class_Base_Properties_Selection_Grid.Visibility = Visibility.Visible;

            table_Type = Character_Table_Type.Single_Line_Multivalue;
            points = new List<Ellipse>();
            this.single_Line_Multivalue_Table = table;
            Initialize_Levels();

            Class_Name.Text = (Base_Class_Selection_Combobox.SelectedItem as ComboBoxItem).Content.ToString();
            Class_Icon.Source = L2H_Parser.GetClassImage(Class_Name.Text);

            Class_Table_ID.Text = table.base_Parameter_ID.Replace("_begin", "");



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

        public void Initialize_Levels()
        {
            table_Values = new List<Table_Value>();

            switch (table_Type)
            {
                case Character_Table_Type.Multiple_Line_Multivalue:
                    if (multiple_Line_Multivalue_Table != null)
                        for (int i = 0; i < multiple_Line_Multivalue_Table.levelIDs.Count; i++)
                        {
                            table_Values.Add(new Table_Value(multiple_Line_Multivalue_Table, i, multiple_Line_Multivalue_Table.levelIDs[i], multiple_Line_Multivalue_Table.values[i]));
                        }
                    break;
                case Character_Table_Type.Single_Line_Multivalue:
                    if (single_Line_Multivalue_Table != null)
                        for (int i = 0; i < single_Line_Multivalue_Table.FromIndex(Base_Class_Selection_Combobox.SelectedIndex).values.Count; i++)
                        {

                            bool foundFloat = false;
                            for (int j = 0; j < single_Line_Multivalue_Table.FromIndex(Base_Class_Selection_Combobox.SelectedIndex).values.Count; j++)
                            {
                                if (single_Line_Multivalue_Table.FromIndex(Base_Class_Selection_Combobox.SelectedIndex).values[j].Contains("."))
                                    foundFloat = true;
                            }

                            usingFloat = foundFloat;

                            table_Values.Add(new Table_Value(single_Line_Multivalue_Table, Base_Class_Selection_Combobox.SelectedIndex, i, i.ToString(), single_Line_Multivalue_Table.FromIndex(Base_Class_Selection_Combobox.SelectedIndex).values[i]));
                        }
                    break;
                default:
                    break;
            }

            if (table_Values.Exists(x => x.ID.Contains("attribute")))
            {
                Level_Value_TextBlock_Top.Text = "Attribute Bonus";
                Level_Type_TextBlock_Top.Text = "Value";
                Level_Type_TextBlock_Bot.Text = "Attribute Value";
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

            string numberOfDecimals = "F0";
            if (usingFloat)
                numberOfDecimals = "F2";

            var draggableControl = sender as Shape;

            if (draggableControl != null)
            {
                var transform = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();

                (draggableControl.DataContext as Table_Value).Value = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);

            }


            draggableControl.ReleaseMouseCapture();

            Initialize_Pieces();


        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            string numberOfDecimals = "F0";
            if (usingFloat)
                numberOfDecimals = "F2";

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

                Value_Textblock.Text = "Value: " + Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);


                Refresh_Lines();

            }
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
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

            double distance = Canvas_Diagram.ActualWidth / table_Values.Count;
            List<double> pointValues = new List<double>();

            for (int i = 0; i < table_Values.Count; i++)
            {
                pointValues.Add(double.Parse(table_Values[i].Value));
            }

            lowestValue = pointValues.Min();
            highestValue = pointValues.Max();
            differenceValue = highestValue - lowestValue;

            for (int i = 0; i < table_Values.Count; i++)
            {
                Ellipse newEllipse = Create_New_Point();
                newEllipse.DataContext = table_Values[i];

                double percentageValue = ((double.Parse(table_Values[i].Value) - lowestValue) / (highestValue - lowestValue));
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
            newGrid.Margin = new Thickness(4, 4, 8, 4);
            newGrid.Height = 40;
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

            for (int i = 0; i < table_Values.Count; i++)
            {
                Grid newGrid = Create_Skill_List_Entry();
                TextBlock levelTextBlock = new TextBlock();
                levelTextBlock.Margin = new Thickness(0);
                levelTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                levelTextBlock.TextAlignment = TextAlignment.Center;
                levelTextBlock.MinWidth = 10;
                levelTextBlock.Style = Application.Current.FindResource("HomagePropertyTitleText") as Style;

                Binding bindingTextBlock = new Binding("ShortID");
                bindingTextBlock.Source = table_Values[i];
                bindingTextBlock.Mode = BindingMode.OneWay;
                levelTextBlock.SetBinding(TextBlock.TextProperty, bindingTextBlock);

                TextBox levelTextBox = new TextBox();
                levelTextBox.TextAlignment = TextAlignment.Left;
                levelTextBox.Style = Application.Current.FindResource("HomagePropertyTextBox") as Style;
                levelTextBox.Tag = "float";
                levelTextBox.PreviewKeyDown += TextBox_Float_No_Spaces_Allowed;
                levelTextBox.LostFocus += LevelTextBox_LostFocus;
                levelTextBox.FontFamily = new FontFamily("lucida_console");

                Binding bindingTextBox = new Binding();
                bindingTextBox.Path = new PropertyPath("Value");

                bindingTextBox.Source = table_Values[i];
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

        private void Base_Class_Selection_Combobox_Index_Changed(object sender, SelectionChangedEventArgs e)
        {
            baseClassIndex = (sender as ComboBox).SelectedIndex;
            if (Levels_Listview != null)
            {
                Initialize_Levels();
                Initialize_Pieces();
            }
        }
    }

    class Table_Value : INotifyPropertyChanged
    {
        Character_Table_Type table_Type;

        public Base_Parameter_Multiple_Line_Multivalue parent_Multiple_Line_Multivalue { get; set; }
        public Base_Parameter_Single_Line_Multivalue parent_Single_Line_Multivalue { get; set; }
        public int parent_Single_Line_Multivalue_Class_Index { get; set; }
        public int valueIndex { get; set; }
        public string ID { get; set; }
        public string ShortID
        {
            get
            {
                return ID.Replace("lvl_", "").Replace("attribute_", "");
            }
        }
        string value;
        public string Value
        {
            get { return value; }
            set
            {
                this.value = value;
                switch (table_Type)
                {
                    case Character_Table_Type.Multiple_Line_Multivalue:
                        {
                            L2H_Log.Instance.Log_Character_Table_Multiple_Line_Multivalue(parent_Multiple_Line_Multivalue, parent_Multiple_Line_Multivalue.values[valueIndex], value);
                            parent_Multiple_Line_Multivalue.values[valueIndex] = value;
                        }
                        break;
                    case Character_Table_Type.Single_Line_Multivalue:
                        {
                            L2H_Log.Instance.Log_Character_Table_Single_Line_Multivalue(parent_Single_Line_Multivalue, parent_Single_Line_Multivalue.FromIndex(parent_Single_Line_Multivalue_Class_Index).classID, parent_Single_Line_Multivalue.FromIndex(parent_Single_Line_Multivalue_Class_Index).values[valueIndex], value);
                            parent_Single_Line_Multivalue.FromIndex(parent_Single_Line_Multivalue_Class_Index).values[valueIndex] = value;
                        }
                        break;
                    default:
                        break;
                }
                OnPropertyChanged();
            }
        }

        public Table_Value(Base_Parameter_Multiple_Line_Multivalue parent, int index, string ID, string value)
        {
            table_Type = Character_Table_Type.Multiple_Line_Multivalue;
            this.parent_Multiple_Line_Multivalue = parent;
            this.valueIndex = index;
            this.ID = ID;
            this.value = value;

        }
        public Table_Value(Base_Parameter_Single_Line_Multivalue parent, int parent_Single_Line_Multivalue_Class_Index, int index, string ID, string value)
        {
            table_Type = Character_Table_Type.Single_Line_Multivalue;
            this.parent_Single_Line_Multivalue = parent;
            this.valueIndex = index;
            this.ID = ID;
            this.value = value;
            this.parent_Single_Line_Multivalue_Class_Index = parent_Single_Line_Multivalue_Class_Index;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string GetExportString()
        {
            switch (table_Type)
            {
                case Character_Table_Type.Multiple_Line_Multivalue:
                    return "\t" + ID + " = " + Value;
                case Character_Table_Type.Single_Line_Multivalue:
                    return "\t" + ID + " = " + Value;
                default:
                    break;
            }

            return "\t" + ID + " = " + Value;
        }
    }
}

