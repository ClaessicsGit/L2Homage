using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace L2Homage
{
    /// <summary>
    /// Interaction logic for Popup_Capsuled_Items.xaml
    /// I am not proud of this one, it was my first "work" after 2 months of illness. It works, but it's.... not pretty.
    /// Update: Fuck it, I am proud. I'm surprised I even got this done, I can't remember doing this at all.
    /// </summary>
    public partial class Popup_Skill_Batch : Window
    {
        protected bool isDragging;
        private Point clickPosition;
        private TranslateTransform originTT;
        List<L2H_Skill> skills;
        List<L2H_Skill_Effects> skill_Effects;
        List<L2H_Skill_Effect> skill_Effect_Types;

        public int selectedEffectTypeIndex = 0;
        public int selectedEffectIndex = 0;
        public int selectedValueIndex = 0;

        double lowestValue = 0;
        double highestValue = 0;
        double differenceValue = 0;

        List<Ellipse> points;
        Path activePath;

        float pointDiameter = 5;

        bool usingFloat = false;

        public Popup_Skill_Batch(List<L2H_Skill> skills)
        {
            InitializeComponent();
            points = new List<Ellipse>();
            this.skills = skills;
            Initialize_Skill_Levels();

            Skill_Name.Text = skills[0].Skill_Name;
            Skill_Icon.Source = L2H_Parser.GetSkillImage(skills[0].client_Skill.icon_name);
            Skill_Property_Level_List_Title.Text = (Skill_Batch_Property_Target_Combobox.SelectedItem as ComboBoxItem).Content.ToString();

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

        public void Initialize_Skill_Levels()
        {

            skill_Effects = new List<L2H_Skill_Effects>();
            skill_Effect_Types = new List<L2H_Skill_Effect>();

            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i].Skill_Effect != null && skills[i].Skill_Effect != "{}")
                {
                    L2H_Skill_Effects newEffect = new L2H_Skill_Effects(skills[i], skills[i].Skill_Effect, L2H_Skill_Effect_Type.Effect);
                    skill_Effects.Add(newEffect);
                }
                if (skills[i].Skill_Start_Effect != null && skills[i].Skill_Start_Effect != "{}")
                {
                    L2H_Skill_Effects newStartEffect = new L2H_Skill_Effects(skills[i], skills[i].Skill_Start_Effect, L2H_Skill_Effect_Type.Start_Effect);
                    skill_Effects.Add(newStartEffect);
                }
                if (skills[i].Skill_End_Effect != null && skills[i].Skill_End_Effect != "{}")
                {
                    L2H_Skill_Effects newEndEffect = new L2H_Skill_Effects(skills[i], skills[i].Skill_End_Effect, L2H_Skill_Effect_Type.End_Effect);
                    skill_Effects.Add(newEndEffect);
                }
                if (skills[i].Skill_Self_Effect != null && skills[i].Skill_Self_Effect != "{}")
                {
                    L2H_Skill_Effects newSelfEffect = new L2H_Skill_Effects(skills[i], skills[i].Skill_Self_Effect, L2H_Skill_Effect_Type.Self_Effect);
                    skill_Effects.Add(newSelfEffect);
                }
                if (skills[i].Skill_Tick_Effect != null && skills[i].Skill_Tick_Effect != "{}")
                {
                    L2H_Skill_Effects newTickEffect = new L2H_Skill_Effects(skills[i], skills[i].Skill_Tick_Effect, L2H_Skill_Effect_Type.Tick_Effect);
                    skill_Effects.Add(newTickEffect);
                }
                if (skills[i].Skill_PVP_Effect != null && skills[i].Skill_PVP_Effect != "{}")
                {
                    L2H_Skill_Effects newPVPEffect = new L2H_Skill_Effects(skills[i], skills[i].Skill_PVP_Effect, L2H_Skill_Effect_Type.PVP_Effect);
                    skill_Effects.Add(newPVPEffect);
                }
                if (skills[i].Skill_PVE_Effect != null && skills[i].Skill_PVE_Effect != "{}")
                {
                    L2H_Skill_Effects newPVEEffect = new L2H_Skill_Effects(skills[i], skills[i].Skill_PVE_Effect, L2H_Skill_Effect_Type.PVE_Effect);
                    skill_Effects.Add(newPVEEffect);
                }


                for (int j = 0; j < skill_Effects.Count; j++)
                {
                    for (int k = 0; k < skill_Effects[j].skill_Effects.Count; k++)
                    {
                        if (!skill_Effect_Types.Exists(x => x.skillEffectID == skill_Effects[j].skill_Effects[k].skillEffectID))
                            skill_Effect_Types.Add(skill_Effects[j].skill_Effects[k]);
                    }
                }


            }

            Skill_Batch_Effect_Target_Combobox.SelectedItem = null;
            Skill_Batch_Effect_Target_Combobox.Items.Clear();
            Skill_Batch_Effect_Target_Combobox.DataContext = skill_Effect_Types;



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

                switch ((Skill_Batch_Property_Target_Combobox.SelectedItem as ComboBoxItem).Content)
                {
                    case "MP Cost (Initial)":
                        {
                            (draggableControl.DataContext as L2H_Skill).Skill_MP_Cost1 = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);
                            break;
                        }
                    case "MP Cost (Finish)":
                        {
                            (draggableControl.DataContext as L2H_Skill).Skill_MP_Cost2 = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);
                            break;
                        }
                    case "MP Cost (Per Tick)":
                        {
                            (draggableControl.DataContext as L2H_Skill).Skill_MP_Cost_Tick = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);
                            break;
                        }
                    case "HP Cost":
                        {
                            (draggableControl.DataContext as L2H_Skill).Skill_HP_Cost = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);
                            break;
                        }
                    case "Cast Time":
                        {
                            (draggableControl.DataContext as L2H_Skill).Skill_Cast_Time = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);
                            break;
                        }
                    case "Cast Range":
                        {
                            (draggableControl.DataContext as L2H_Skill).Skill_Cast_Range = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);
                            break;
                        }
                    case "Aggro Generation":
                        {
                            (draggableControl.DataContext as L2H_Skill).Skill_Effect_Point = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);
                            break;
                        }
                    case "Reuse Delay":
                        {
                            (draggableControl.DataContext as L2H_Skill).Skill_Reuse_Delay = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);
                            break;
                        }
                    case "Duration":
                        {
                            (draggableControl.DataContext as L2H_Skill).Skill_Abnormal_Time = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);
                            break;
                        }
                    case "Affect Range":
                        {
                            (draggableControl.DataContext as L2H_Skill).Skill_Affect_Range = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);
                            break;
                        }
                    case "Tick Interval":
                        {
                            (draggableControl.DataContext as L2H_Skill).Skill_Tick_Interval = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);
                            break;
                        }
                    case "Effect(s)":
                        {
                            (draggableControl.DataContext as L2H_Skill_Effects).skill_Effects[selectedEffectIndex].values[selectedValueIndex].Value = Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString(numberOfDecimals);
                            (draggableControl.DataContext as L2H_Skill_Effects).SaveSkillEffects();
                            break;
                        }
                    default:
                        break;
                }


            }


            draggableControl.ReleaseMouseCapture();

            Initialize_Pieces();

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var draggableControl = sender as Shape;
            if (isDragging && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(this);
                var transform = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
                transform.X = transform.X;// originTT.X + (currentPosition.X - clickPosition.X);
                transform.Y = originTT.Y + (currentPosition.Y - clickPosition.Y);

                if (transform.Y < 0)
                    transform.Y = 0;
                if (transform.Y > Canvas_Diagram.ActualHeight - pointDiameter)
                    transform.Y = (Canvas_Diagram.ActualHeight - pointDiameter);

                draggableControl.RenderTransform = new TranslateTransform(transform.X, transform.Y);

                if (usingFloat)
                    Value_Textblock.Text = "Value: " + Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString("F2");
                else
                    Value_Textblock.Text = "Value: " + Math.Abs((((Canvas_Diagram.ActualHeight - transform.Y - pointDiameter) / (Canvas_Diagram.ActualHeight - pointDiameter)) * differenceValue) + lowestValue).ToString("F0");


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
            Skill_Property_Level_List_Title.Text = (Skill_Batch_Property_Target_Combobox.SelectedItem as ComboBoxItem).Content.ToString();
            points.Clear();

            double distance = Canvas_Diagram.ActualWidth / skills.Count;
            List<double> skillPointValues = new List<double>();

            switch ((Skill_Batch_Property_Target_Combobox.SelectedItem as ComboBoxItem).Content)
            {
                case "MP Cost (Initial)":
                    {
                        if (double.TryParse(skills[0].Skill_MP_Cost1, out var parsedNumber))
                        {
                            for (int i = 0; i < skills.Count; i++)
                            {
                                skillPointValues.Add(double.Parse(skills[i].Skill_MP_Cost1));
                            }

                            lowestValue = skillPointValues.Min();
                            highestValue = skillPointValues.Max();
                            differenceValue = highestValue - lowestValue;
                        }
                        else
                        {
                            return;
                        }

                        for (int i = 0; i < skills.Count; i++)
                        {
                            Ellipse newEllipse = Create_New_Point();
                            newEllipse.DataContext = skills[i];

                            double percentageValue = ((double.Parse(skills[i].Skill_MP_Cost1) - lowestValue) / (highestValue - lowestValue));
                            Position_Point(newEllipse, i, distance, percentageValue);
                            Add_New_Level_Text(i);

                        }

                        break;
                    }
                case "MP Cost (Finish)":
                    {
                        if (double.TryParse(skills[0].Skill_MP_Cost2, out var parsedNumber))
                        {
                            for (int i = 0; i < skills.Count; i++)
                            {
                                skillPointValues.Add(double.Parse(skills[i].Skill_MP_Cost2));
                            }

                            lowestValue = skillPointValues.Min();
                            highestValue = skillPointValues.Max();
                            differenceValue = highestValue - lowestValue;
                        }
                        else
                        {
                            return;
                        }

                        for (int i = 0; i < skills.Count; i++)
                        {
                            Ellipse newEllipse = Create_New_Point();
                            newEllipse.DataContext = skills[i];

                            double percentageValue = ((double.Parse(skills[i].Skill_MP_Cost2) - lowestValue) / (highestValue - lowestValue));
                            Position_Point(newEllipse, i, distance, percentageValue);
                            Add_New_Level_Text(i);

                        }
                        break;
                    }
                case "MP Cost (Per Tick)":
                    {
                        if (double.TryParse(skills[0].Skill_MP_Cost_Tick, out var parsedNumber))
                        {
                            for (int i = 0; i < skills.Count; i++)
                            {
                                skillPointValues.Add(double.Parse(skills[i].Skill_MP_Cost_Tick));
                            }

                            lowestValue = skillPointValues.Min();
                            highestValue = skillPointValues.Max();
                            differenceValue = highestValue - lowestValue;
                        }
                        else
                        {
                            return;
                        }

                        for (int i = 0; i < skills.Count; i++)
                        {
                            Ellipse newEllipse = Create_New_Point();
                            newEllipse.DataContext = skills[i];

                            double percentageValue = ((double.Parse(skills[i].Skill_MP_Cost_Tick) - lowestValue) / (highestValue - lowestValue));
                            Position_Point(newEllipse, i, distance, percentageValue);
                            Add_New_Level_Text(i);

                        }
                        break;
                    }
                case "HP Cost":
                    {
                        if (double.TryParse(skills[0].Skill_HP_Cost, out var parsedNumber))
                        {
                            for (int i = 0; i < skills.Count; i++)
                            {
                                skillPointValues.Add(double.Parse(skills[i].Skill_HP_Cost));
                            }

                            lowestValue = skillPointValues.Min();
                            highestValue = skillPointValues.Max();
                            differenceValue = highestValue - lowestValue;
                        }
                        else
                        {
                            return;
                        }

                        for (int i = 0; i < skills.Count; i++)
                        {
                            Ellipse newEllipse = Create_New_Point();
                            newEllipse.DataContext = skills[i];

                            double percentageValue = ((double.Parse(skills[i].Skill_HP_Cost) - lowestValue) / (highestValue - lowestValue));
                            Position_Point(newEllipse, i, distance, percentageValue);
                            Add_New_Level_Text(i);

                        }
                        break;
                    }
                case "Cast Time":
                    {
                        if (double.TryParse(skills[0].Skill_Cast_Time, out var parsedNumber))
                        {
                            for (int i = 0; i < skills.Count; i++)
                            {
                                skillPointValues.Add(double.Parse(skills[i].Skill_Cast_Time));
                            }

                            lowestValue = skillPointValues.Min();
                            highestValue = skillPointValues.Max();
                            differenceValue = highestValue - lowestValue;
                        }
                        else
                        {
                            return;
                        }

                        for (int i = 0; i < skills.Count; i++)
                        {
                            Ellipse newEllipse = Create_New_Point();
                            newEllipse.DataContext = skills[i];

                            double percentageValue = ((double.Parse(skills[i].Skill_Cast_Time) - lowestValue) / (highestValue - lowestValue));
                            Position_Point(newEllipse, i, distance, percentageValue);
                            Add_New_Level_Text(i);

                        }
                        break;
                    }
                case "Cast Range":
                    {
                        if (double.TryParse(skills[0].Skill_Cast_Range, out var parsedNumber))
                        {
                            for (int i = 0; i < skills.Count; i++)
                            {
                                skillPointValues.Add(double.Parse(skills[i].Skill_Cast_Range));
                            }

                            lowestValue = skillPointValues.Min();
                            highestValue = skillPointValues.Max();
                            differenceValue = highestValue - lowestValue;
                        }
                        else
                        {
                            return;
                        }

                        for (int i = 0; i < skills.Count; i++)
                        {
                            Ellipse newEllipse = Create_New_Point();
                            newEllipse.DataContext = skills[i];

                            double percentageValue = ((double.Parse(skills[i].Skill_Cast_Range) - lowestValue) / (highestValue - lowestValue));
                            Position_Point(newEllipse, i, distance, percentageValue);
                            Add_New_Level_Text(i);

                        }
                        break;
                    }
                case "Aggro Generation":
                    {
                        if (double.TryParse(skills[0].Skill_Effect_Point, out var parsedNumber))
                        {
                            for (int i = 0; i < skills.Count; i++)
                            {
                                skillPointValues.Add(double.Parse(skills[i].Skill_Effect_Point));
                            }

                            lowestValue = skillPointValues.Min();
                            highestValue = skillPointValues.Max();
                            differenceValue = highestValue - lowestValue;
                        }
                        else
                        {
                            return;
                        }

                        for (int i = 0; i < skills.Count; i++)
                        {
                            Ellipse newEllipse = Create_New_Point();
                            newEllipse.DataContext = skills[i];

                            double percentageValue = ((double.Parse(skills[i].Skill_Effect_Point) - lowestValue) / (highestValue - lowestValue));
                            Position_Point(newEllipse, i, distance, percentageValue);
                            Add_New_Level_Text(i);

                        }
                        break;
                    }
                case "Reuse Delay":
                    {
                        if (double.TryParse(skills[0].Skill_Reuse_Delay, out var parsedNumber))
                        {
                            for (int i = 0; i < skills.Count; i++)
                            {
                                skillPointValues.Add(double.Parse(skills[i].Skill_Reuse_Delay));
                            }

                            lowestValue = skillPointValues.Min();
                            highestValue = skillPointValues.Max();
                            differenceValue = highestValue - lowestValue;
                        }
                        else
                        {
                            return;
                        }

                        for (int i = 0; i < skills.Count; i++)
                        {
                            Ellipse newEllipse = Create_New_Point();
                            newEllipse.DataContext = skills[i];

                            double percentageValue = ((double.Parse(skills[i].Skill_Reuse_Delay) - lowestValue) / (highestValue - lowestValue));
                            Position_Point(newEllipse, i, distance, percentageValue);
                            Add_New_Level_Text(i);

                        }
                        break;
                    }
                case "Duration":
                    {
                        if (double.TryParse(skills[0].Skill_Abnormal_Time, out var parsedNumber))
                        {
                            for (int i = 0; i < skills.Count; i++)
                            {
                                skillPointValues.Add(double.Parse(skills[i].Skill_Abnormal_Time));
                            }

                            lowestValue = skillPointValues.Min();
                            highestValue = skillPointValues.Max();
                            differenceValue = highestValue - lowestValue;
                        }
                        else
                        {
                            return;
                        }

                        for (int i = 0; i < skills.Count; i++)
                        {
                            Ellipse newEllipse = Create_New_Point();
                            newEllipse.DataContext = skills[i];

                            double percentageValue = ((double.Parse(skills[i].Skill_Abnormal_Time) - lowestValue) / (highestValue - lowestValue));
                            Position_Point(newEllipse, i, distance, percentageValue);
                            Add_New_Level_Text(i);

                        }
                        break;
                    }
                case "Affect Range":
                    {
                        if (double.TryParse(skills[0].Skill_Affect_Range, out var parsedNumber))
                        {
                            for (int i = 0; i < skills.Count; i++)
                            {
                                skillPointValues.Add(double.Parse(skills[i].Skill_Affect_Range));
                            }

                            lowestValue = skillPointValues.Min();
                            highestValue = skillPointValues.Max();
                            differenceValue = highestValue - lowestValue;
                        }
                        else
                        {
                            return;
                        }

                        for (int i = 0; i < skills.Count; i++)
                        {
                            Ellipse newEllipse = Create_New_Point();
                            newEllipse.DataContext = skills[i];

                            double percentageValue = ((double.Parse(skills[i].Skill_Affect_Range) - lowestValue) / (highestValue - lowestValue));
                            Position_Point(newEllipse, i, distance, percentageValue);
                            Add_New_Level_Text(i);

                        }
                        break;
                    }
                case "Tick Interval":
                    {
                        if (double.TryParse(skills[0].Skill_Tick_Interval, out var parsedNumber))
                        {
                            for (int i = 0; i < skills.Count; i++)
                            {
                                skillPointValues.Add(double.Parse(skills[i].Skill_Tick_Interval));
                            }

                            lowestValue = skillPointValues.Min();
                            highestValue = skillPointValues.Max();
                            differenceValue = highestValue - lowestValue;
                        }
                        else
                        {
                            return;
                        }

                        for (int i = 0; i < skills.Count; i++)
                        {
                            Ellipse newEllipse = Create_New_Point();
                            newEllipse.DataContext = skills[i];

                            double percentageValue = ((double.Parse(skills[i].Skill_Tick_Interval) - lowestValue) / (highestValue - lowestValue));
                            Position_Point(newEllipse, i, distance, percentageValue);
                            Add_New_Level_Text(i);

                        }
                        break;
                    }
                case "Effect(s)":
                    {
                        if (selectedValueIndex < 0)
                            selectedValueIndex = 0;

                        List<L2H_Skill_Effects> selectedSkillEffects = skill_Effects.FindAll(x => (int)x.type == selectedEffectTypeIndex);
                        if (selectedSkillEffects.Count == 0)
                            return;

                        for (int i = 0; i < selectedSkillEffects.Count; i++)
                        {
                            if (selectedSkillEffects[i].skill_Effects[selectedEffectIndex].values.Count > 0)
                            {
                                if (double.TryParse(selectedSkillEffects[i].skill_Effects[selectedEffectIndex].values[selectedValueIndex].Value, out var parsedNumber))
                                {
                                    skillPointValues.Add(double.Parse(selectedSkillEffects[i].skill_Effects[selectedEffectIndex].values[selectedValueIndex].Value));

                                    lowestValue = skillPointValues.Min();
                                    highestValue = skillPointValues.Max();
                                    differenceValue = highestValue - lowestValue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }


                        //Calculating the points based on effects
                        distance = Canvas_Diagram.ActualWidth / selectedSkillEffects.Count;

                        for (int i = 0; i < selectedSkillEffects.Count; i++)
                        {
                            if (selectedSkillEffects[i].skill_Effects[selectedEffectIndex].values.Count == 0)
                                continue;

                            if (double.TryParse(selectedSkillEffects[i].skill_Effects[selectedEffectIndex].values[selectedValueIndex].Value, out var parsedNumber))
                            {
                                Ellipse newEllipse = Create_New_Point();
                                newEllipse.DataContext = selectedSkillEffects[i];

                                double percentageValue = ((double.Parse(selectedSkillEffects[i].skill_Effects[selectedEffectIndex].values[selectedValueIndex].Value) - lowestValue) / (highestValue - lowestValue));
                                Position_Point(newEllipse, i, distance, percentageValue);
                                Add_New_Level_Text(i);
                            }
                        }
                    }
                    break;
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
            newLevelText.SetValue(Grid.ColumnProperty, value);
        }

        private Grid Create_Skill_List_Entry()
        {
            Grid newGrid = new Grid();
            newGrid.Margin = new Thickness(4, 4, 4, 4);
            newGrid.Height = 40;
            newGrid.Width = 188;
            newGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            newGrid.Focusable = false;
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { MaxWidth = 40 });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition());

            return newGrid;
        }

        private void Initialize_Level_Listview()
        {

            Skill_Levels_Listview.Items.Clear();

            if (points.Count == 0)
            {
                Property_Not_Found_On_Skill.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                Property_Not_Found_On_Skill.Visibility = Visibility.Hidden;
            }


            if (((Skill_Batch_Property_Target_Combobox.SelectedItem as ComboBoxItem).Content).ToString() != "Effect(s)")
            {
                for (int i = 0; i < skills.Count; i++)
                {
                    Grid newGrid = Create_Skill_List_Entry();
                    TextBlock levelTextBlock = new TextBlock();
                    levelTextBlock.Margin = new Thickness(0);
                    levelTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    levelTextBlock.TextAlignment = TextAlignment.Center;
                    levelTextBlock.MinWidth = 10;
                    levelTextBlock.Style = Application.Current.FindResource("HomagePropertyTitleText") as Style;

                    Binding bindingTextBlock = new Binding("Skill_Level");
                    bindingTextBlock.Source = skills[i];
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
                    switch ((Skill_Batch_Property_Target_Combobox.SelectedItem as ComboBoxItem).Content)
                    {
                        case "MP Cost (Initial)":
                            {
                                bindingTextBox.Path = new PropertyPath("Skill_MP_Cost1");
                                break;
                            }
                        case "MP Cost (Finish)":
                            {
                                bindingTextBox.Path = new PropertyPath("Skill_MP_Cost2");
                                break;
                            }
                        case "MP Cost (Per Tick)":
                            {
                                bindingTextBox.Path = new PropertyPath("Skill_MP_Cost_Tick");
                                break;
                            }
                        case "HP Cost":
                            {
                                bindingTextBox.Path = new PropertyPath("Skill_HP_Cost");
                                break;
                            }
                        case "Cast Time":
                            {
                                bindingTextBox.Path = new PropertyPath("Skill_Cast_Time");
                                break;
                            }
                        case "Cast Range":
                            {
                                bindingTextBox.Path = new PropertyPath("Skill_Cast_Range");
                                break;
                            }
                        case "Aggro Generation":
                            {
                                bindingTextBox.Path = new PropertyPath("Skill_Effect_Point");
                                break;
                            }
                        case "Reuse Delay":
                            {
                                bindingTextBox.Path = new PropertyPath("Skill_Reuse_Delay");
                                break;
                            }
                        case "Duration":
                            {
                                bindingTextBox.Path = new PropertyPath("Skill_Abnormal_Time");
                                break;
                            }
                        case "Affect Range":
                            {
                                bindingTextBox.Path = new PropertyPath("Skill_Affect_Range");
                                break;
                            }
                        case "Tick Interval":
                            {
                                bindingTextBox.Path = new PropertyPath("Skill_Tick_Interval");
                                break;
                            }
                        case "Effect(s)":
                            {
                                break;
                            }
                        default:
                            break;
                    }

                    bindingTextBox.Source = skills[i];
                    bindingTextBox.Mode = BindingMode.TwoWay;
                    bindingTextBox.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    levelTextBox.SetBinding(TextBox.TextProperty, bindingTextBox);

                    newGrid.Children.Add(levelTextBlock);
                    Grid.SetColumn(levelTextBlock, 0);

                    newGrid.Children.Add(levelTextBox);
                    Grid.SetColumn(levelTextBox, 1);
                    Skill_Levels_Listview.Items.Add(newGrid);
                }
            }
            else
            {
                for (int i = 0; i < skill_Effects.Count; i++)
                {
                    if ((int)skill_Effects[i].type == selectedEffectTypeIndex)
                    {
                        Grid newGrid = Create_Skill_List_Entry();
                        TextBlock levelTextBlock = new TextBlock();
                        levelTextBlock.Margin = new Thickness(0);
                        levelTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        levelTextBlock.TextAlignment = TextAlignment.Center;
                        levelTextBlock.MinWidth = 10;
                        levelTextBlock.Style = Application.Current.FindResource("HomagePropertyTitleText") as Style;

                        Binding bindingTextBlock = new Binding("Skill_Level");
                        bindingTextBlock.Source = skill_Effects[i].parent;
                        bindingTextBlock.Mode = BindingMode.OneWay;
                        levelTextBlock.SetBinding(TextBlock.TextProperty, bindingTextBlock);

                        TextBox levelTextBox = new TextBox();
                        levelTextBox.TextAlignment = TextAlignment.Left;
                        levelTextBox.Style = Application.Current.FindResource("HomagePropertyTextBox") as Style;
                        levelTextBox.Tag = "float";
                        levelTextBox.PreviewKeyDown += TextBox_Float_No_Spaces_Allowed;
                        levelTextBox.LostFocus += LevelTextBox_LostFocus;

                        Binding bindingTextBox = new Binding();

                        bindingTextBox.Path = new PropertyPath("Value"); //This needs to point to specific skill_effect_value

                        bindingTextBox.Source = skill_Effects[i].skill_Effects[selectedEffectIndex].values[selectedValueIndex];
                        bindingTextBox.Mode = BindingMode.TwoWay;
                        bindingTextBox.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                        levelTextBox.SetBinding(TextBox.TextProperty, bindingTextBox);

                        newGrid.Children.Add(levelTextBlock);
                        Grid.SetColumn(levelTextBlock, 0);

                        newGrid.Children.Add(levelTextBox);
                        Grid.SetColumn(levelTextBox, 1);
                        Skill_Levels_Listview.Items.Add(newGrid);
                    }
                }
            }

            bool foundFloat = false;
            for (int i = 0; i < Skill_Levels_Listview.Items.Count; i++)
            {

                foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Skill_Levels_Listview))
                {
                    if (tb.Text.Contains("."))
                        foundFloat = true;
                }


            }

            usingFloat = foundFloat;

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

        private void Skill_Batch_Property_Target_Combobox_Selection_Index_Changed(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (Skill_Effect_Properties_Grid == null)
                return;

            if ((comboBox.SelectedItem as ComboBoxItem).Content.ToString() == "Effect(s)")
            {
                Filter_Skill_Effect_IDs_And_Values();
                Initialize_Pieces();
            }
            else
            {
                Skill_Effect_Properties_Grid.Visibility = Visibility.Hidden;
                Initialize_Pieces();
            }

            Initialize_Level_Listview();

        }

        private void Filter_Skill_Effect_IDs_And_Values()
        {
            Skill_Batch_Effect_Target_Combobox.Items.Clear();

            for (int i = 0; i < skill_Effect_Types.Count; i++)
            {
                if ((int)skill_Effect_Types[i].type == selectedEffectTypeIndex)
                    Skill_Batch_Effect_Target_Combobox.Items.Add(new ComboBoxItem() { Content = "[" + skill_Effect_Types[i].type.ToString() + "] " + skill_Effect_Types[i].skillEffectID, Style = Application.Current.FindResource("HomageComboBoxItemStyle") as Style });// < ComboBoxItem Style = "{StaticResource HomageComboBoxItemStyle}" Content = "MP Cost (Finish)" />
            }


            Skill_Batch_Effect_Target_Combobox.SelectedIndex = 0;
            Skill_Effect_Properties_Grid.Visibility = Visibility.Visible;

            int numberOfSkillEffectValues = 0;

            for (int i = 0; i < skill_Effect_Types.Count; i++)
            {
                if ((int)skill_Effect_Types[i].type == selectedEffectTypeIndex)
                    if (skill_Effect_Types[i].values.Count > numberOfSkillEffectValues)
                        numberOfSkillEffectValues = skill_Effect_Types[i].values.Count;
            }

            Skill_Batch_Effect_Target_Value_Combobox.Items.Clear();

            for (int i = 0; i < numberOfSkillEffectValues; i++)
            {
                Skill_Batch_Effect_Target_Value_Combobox.Items.Add(new ComboBoxItem() { Content = "Value " + i, Style = Application.Current.FindResource("HomageComboBoxItemStyle") as Style });
            }

            if (Skill_Batch_Effect_Target_Value_Combobox.Items.Count > 0)
                Skill_Batch_Effect_Target_Value_Combobox.SelectedIndex = 0;
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

        private void Skill_Batch_Effect_Value_Index_Changed(object sender, SelectionChangedEventArgs e)
        {
            selectedValueIndex = (sender as ComboBox).SelectedIndex;
            Initialize_Pieces();
            Initialize_Level_Listview();
        }

        private void Skill_Batch_Effect_ID_Index_Changed(object sender, SelectionChangedEventArgs e)
        {
            selectedEffectIndex = (sender as ComboBox).SelectedIndex;
            if (selectedEffectIndex < 0)
                selectedEffectIndex = 0;

            if (selectedValueIndex < 0)
                selectedValueIndex = 0;


            Initialize_Pieces();
            Initialize_Level_Listview();
        }

        private void Skill_Batch_Effect_Type_Index_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (Skill_Effect_Properties_Grid.Visibility == Visibility.Hidden)
                return;

            selectedEffectTypeIndex = (sender as ComboBox).SelectedIndex;
            if (selectedEffectTypeIndex < 0)
                selectedEffectTypeIndex = 0;

            Initialize_Pieces();
            Initialize_Level_Listview();
            Filter_Skill_Effect_IDs_And_Values();
        }
    }
}
