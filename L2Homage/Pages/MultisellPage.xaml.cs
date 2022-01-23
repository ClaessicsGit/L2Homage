using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace L2Homage.Pages
{
    /// <summary>
    /// Interaction logic for MultisellPage.xaml
    /// </summary>
    public partial class MultisellPage : Page
    {

        public List<L2H_Multisell> L2H_Multisells;

        L2H_Multisell active_L2H_Multisell;
        L2H_MultisellSale active_L2H_MultisellSale;

        MainWindow mainWindow;
        int numberOfThreadsToUseForHookingItemsToMultisells = 0;
        int numberOfThreadsCompleted = 0;

        public bool multisellsLoaded;
        public MultisellPage()
        {

            InitializeComponent();

            L2H_Multisells = new List<L2H_Multisell>();
            mainWindow = (MainWindow)Application.Current.MainWindow;

        }

        public void LoadMultisells()
        {
            if (!File.Exists(L2H_Constants.server_MultisellPath))
            {
                MessageBox.Show("Couldn't find multisell.txt in server files folder :(");
            }
            else
            {
                using (TextReader textReader = new StreamReader(L2H_Constants.server_MultisellPath, Encoding.GetEncoding(1200)))
                {
                    // Load the text line by line
                    string line = string.Empty;

                    List<string> multisellLines = new List<string>();

                    while ((line = textReader.ReadLine()) != null)
                    {
                        if (line.Contains("MultiSell_begin"))
                        {
                            multisellLines = new List<string>();
                            multisellLines.Add(line);
                        }
                        else if (line.Contains("MultiSell_end"))
                        {
                            string[] multisellLinesArray = multisellLines.ToArray();
                            L2H_Multisell newMultisell = new L2H_Multisell();
                            newMultisell.Server_Multisell = new Server_Multisell(newMultisell, multisellLinesArray);
                            L2H_Multisells.Add(newMultisell);
                            multisellLines.Clear();
                        }
                        else if (line.Contains("//") || line.Contains("selllist={"))
                        {
                            //Skip, we don't need comments nor selllist start
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                                multisellLines.Add(line);
                        }

                    }
                }
            }

            Dispatcher.Invoke(() =>
            {
                Multisell_IDs_Listview.ItemsSource = L2H_Multisells;
            });


            //Multithread here to link L2H_Items to multisell
            numberOfThreadsCompleted = 0;
            numberOfThreadsToUseForHookingItemsToMultisells = (L2H_Multisells.Count / L2H_Constants.Multisells_Per_Thread) + 1;
            for (int i = 0; i < numberOfThreadsToUseForHookingItemsToMultisells; i++)
            {
                int value = i;
                ThreadWorker_Hook_Items_To_Multisells threadWorker_Hook_Items_To_Multisells = new ThreadWorker_Hook_Items_To_Multisells();
                threadWorker_Hook_Items_To_Multisells.ThreadDone += (b, bb) => HandleThreadDone(b, bb, "Completed Thread: Hook Items to Multisells: " + (value * L2H_Constants.Multisells_Per_Thread).ToString() + " to " + (((value + 1) * L2H_Constants.Multisells_Per_Thread) - 1).ToString() + " of " + L2H_Multisells.Count);
                threadWorker_Hook_Items_To_Multisells.startIndex = value * L2H_Constants.Multisells_Per_Thread;
                threadWorker_Hook_Items_To_Multisells.endIndex = ((value + 1) * L2H_Constants.Multisells_Per_Thread);
                threadWorker_Hook_Items_To_Multisells.L2H_Items = (mainWindow.GetPageOfType(typeof(Pages.ItemsPage)) as ItemsPage).L2H_Items;
                threadWorker_Hook_Items_To_Multisells.L2H_NPCs = (mainWindow.GetPageOfType(typeof(Pages.NPCsPage)) as NPCsPage).L2H_Npcs;
                threadWorker_Hook_Items_To_Multisells.L2H_Multisells = L2H_Multisells;
                Thread t = new Thread(new ThreadStart(threadWorker_Hook_Items_To_Multisells.ThreadProc));
                t.Start();
            }

            Dispatcher.Invoke(() =>
            {
                (Application.Current.MainWindow as MainWindow).UpdateLog("Multisell lists hooked to items: " + L2H_Multisells.Count, L2H_Constants.Color_Add);
            });

            SpinWait.SpinUntil(() => numberOfThreadsToUseForHookingItemsToMultisells == numberOfThreadsCompleted, Timeout.Infinite);


            multisellsLoaded = true;

            mainWindow.UpdateLoadedNumber(LoadedTypes.Multisell);


        }

        void HandleThreadDone(object sender, EventArgs e, string message)
        {
            Interlocked.Increment(ref numberOfThreadsCompleted);

            if (numberOfThreadsToUseForHookingItemsToMultisells == numberOfThreadsCompleted)
            {
                Dispatcher.Invoke(() =>
                {
                    var newView = new CollectionViewSource() { Source = L2H_Multisells };
                    var listViewCollection2 = (ListCollectionView)newView.View;
                    listViewCollection2.Filter = Filter_TextChanged;
                    Multisell_IDs_Listview.ItemsSource = listViewCollection2;
                    CollectionViewSource.GetDefaultView(Multisell_IDs_Listview.ItemsSource).Refresh();


                });
            }


        }

        private void Refresh_Multisell(L2H_Multisell multisell)
        {
            Fee_Textbox.DataContext = null;

            if (active_L2H_Multisell != null)
            {
                active_L2H_Multisell.IsSelected = false;
            }

            if (active_L2H_Multisell == multisell)
            {
                active_L2H_Multisell.IsSelected = false;
                active_L2H_Multisell = null;
                Details_Grid.Visibility = Visibility.Hidden;
                Multisell_Costs_Listview.ItemsSource = null;
                Multisell_Sales_Listview.ItemsSource = null;
                return;
            }

            active_L2H_Multisell = multisell;
            active_L2H_Multisell.IsSelected = true;

            if (active_L2H_MultisellSale != null)
                active_L2H_MultisellSale.IsSelected = false;

            active_L2H_MultisellSale = null;

            Details_Grid.Visibility = Visibility.Visible;
            Additional_Fee_Grid.Visibility = Visibility.Hidden;
            Multisell_Costs_Listview.ItemsSource = null;

            Multisell_Sales_Listview.ItemsSource = active_L2H_Multisell.GetItemsForSale;
            ID_Name_Details_TextBlock.Text = "ID " + active_L2H_Multisell.ID + ": " + active_L2H_Multisell.name_ID;


            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Multisell_Variables_Grid))
            {
                tb.DataContext = active_L2H_Multisell;
            }
            foreach (ToggleButton tb in L2H_Parser.FindVisualChildren<ToggleButton>(Multisell_Variables_Grid))
            {
                tb.DataContext = active_L2H_Multisell;
            }

            Requires_NPCs_Listview.ItemsSource = active_L2H_Multisell.Server_Multisell.requiredL2H_NPCs;
            CollectionViewSource.GetDefaultView(Requires_NPCs_Listview.ItemsSource).Refresh();

            CollectionViewSource.GetDefaultView(Multisell_IDs_Listview.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(Multisell_Sales_Listview.ItemsSource).Refresh();
        }

        private void Multisell_ID_Click(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            L2H_Multisell multisell = (sender as ToggleButton).DataContext as L2H_Multisell;

            Refresh_Multisell(multisell);
        }

        private void Multisell_Filter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Multisell_IDs_Listview.ItemsSource).Refresh();
        }

        private bool Filter_TextChanged(object sender)
        {
            L2H_Multisell filteredItem = sender as L2H_Multisell;

            if (filteredItem == null)
                return false;

            bool IDFound = true;

            if (!string.IsNullOrEmpty(Filter_ID.Text))
            {
                IDFound = filteredItem.ID.IndexOf(Filter_ID.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            }

            if (!IDFound)
                return false;

            bool nameFound = true;

            if (!string.IsNullOrEmpty(Filter_Name.Text))
            {
                nameFound = filteredItem.name_ID.IndexOf(Filter_Name.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            }

            if (!nameFound)
                return false;

            bool itemFound = true;

            if (!string.IsNullOrEmpty(Filter_Contains_Item.Text))
            {
                itemFound = filteredItem.GetItemsForSale.Exists(x => x.sale_Items.Exists(y=>y.ToString().ToLower().Contains(Filter_Contains_Item.Text.ToLower())));
            }

            if (!itemFound)
                return false;


            else
                return true;
        }

        private void Multisell_Sale_Click(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            L2H_MultisellSale multisellElement = (sender as FrameworkElement).DataContext as L2H_MultisellSale;

            if (active_L2H_MultisellSale != null)
            {
                active_L2H_MultisellSale.IsSelected = false;

            }

            if (active_L2H_MultisellSale == multisellElement)
            {
                active_L2H_MultisellSale = null;
                Multisell_Costs_Listview.ItemsSource = null;
                Additional_Fee_Grid.Visibility = Visibility.Hidden;
                CollectionViewSource.GetDefaultView(Multisell_Sales_Listview.ItemsSource).Refresh();
                return;
            }

            active_L2H_MultisellSale = multisellElement;
            Additional_Fee_Grid.Visibility = Visibility.Visible;
            Fee_Textbox.DataContext = active_L2H_MultisellSale;

            Multisell_Costs_Listview.ItemsSource = active_L2H_MultisellSale.costs;
            CollectionViewSource.GetDefaultView(Multisell_Sales_Listview.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(Multisell_Costs_Listview.ItemsSource).Refresh();

        }

        private void Multisell_Sale_Remove_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            if (active_L2H_Multisell == null)
                return;

            L2H_MultisellSale targetSale = ((sender as FrameworkElement).DataContext as L2H_MultisellSale);
            if (targetSale == null)
                return;

            Popup_Confirmation newPopup = new Popup_Confirmation();
            newPopup.Confirmation_Action += (b, bb) => Multisell_Sale_Remove(targetSale);
            newPopup.Post_Confirmation_Action += (b, bb) => Refresh_Multisell_Sales_Listview();
            newPopup.InitializeConfirmation(targetSale.SaleItemNames, active_L2H_Multisell.name_ID + " sales?", targetSale.sale_Items[0].GetIconString);

        }

        void Multisell_Sale_Remove(L2H_MultisellSale targetSale)
        {
            if (active_L2H_MultisellSale != null)
            {
                if (active_L2H_MultisellSale == targetSale)
                {
                    Multisell_Costs_Listview.ItemsSource = null;
                    Additional_Fee_Grid.Visibility = Visibility.Hidden;
                }
            }
            L2H_Log.Instance.Log_Multisell_Sale_Delete(active_L2H_Multisell, targetSale);
            active_L2H_Multisell.Server_Multisell.elements.Remove(targetSale);
        }


        private void Multisell_Cost_Remove_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            if (active_L2H_Multisell == null)
                return;

            L2H_MultisellCost targetCost = ((sender as FrameworkElement).DataContext as L2H_MultisellCost);
            if (targetCost == null)
                return;

            Popup_Confirmation newPopup = new Popup_Confirmation();
            newPopup.Confirmation_Action += (b, bb) => Multisell_Cost_Remove(active_L2H_MultisellSale, targetCost);
            newPopup.Post_Confirmation_Action += (b, bb) => Refresh_Multisell_Costs_Listview();
            newPopup.InitializeConfirmation(targetCost.L2H_Item.Item_Name, active_L2H_MultisellSale.SaleItemNames + " costs?", targetCost.L2H_Item.GetIconString());

        }
        void Multisell_Cost_Remove(L2H_MultisellSale sale, L2H_MultisellCost targetCost)
        {            
            L2H_Log.Instance.Log_Multisell_Cost_Delete(active_L2H_Multisell, sale, targetCost);
            active_L2H_MultisellSale.costs.Remove(targetCost);
        }

        public void Refresh_Multisell_IDs_Listview()
        {
            CollectionViewSource.GetDefaultView(Multisell_IDs_Listview.ItemsSource).Refresh();
        }

        public void Refresh_Multisell_Sales_Listview()
        {
            CollectionViewSource.GetDefaultView(Multisell_Sales_Listview.ItemsSource).Refresh();
        }

        public void Refresh_Multisell_Costs_Listview()
        {
            CollectionViewSource.GetDefaultView(Multisell_Costs_Listview.ItemsSource).Refresh();
        }

        private void Validate_Integer_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Integer(e.Text);
        }


        private void Multisell_Required_NPC_Add_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            Popup_NPC_Selection dialog = new Popup_NPC_Selection((mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage).L2H_Npcs);
            dialog.Initialize_For_Multisell(active_L2H_Multisell);
        }

        private void Multisell_Required_NPC_Remove_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            L2H_Log.Instance.Log_Multisell_NPC_Delete(active_L2H_Multisell, (sender as FrameworkElement).DataContext as L2H_NPC);
            active_L2H_Multisell.Server_Multisell.requiredL2H_NPCs.Remove((sender as FrameworkElement).DataContext as L2H_NPC);
            CollectionViewSource.GetDefaultView(Requires_NPCs_Listview.ItemsSource).Refresh();
        }

        public void Refresh_Required_NPCs()
        {
            CollectionViewSource.GetDefaultView(Requires_NPCs_Listview.ItemsSource).Refresh();
        }

        private void Multisell_Sale_Add_Item_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            if (active_L2H_Multisell == null)
                return;

            Popup_Item_Selection dialog = new Popup_Item_Selection((mainWindow.GetPageOfType(typeof(ItemsPage)) as ItemsPage).L2H_Items);
            dialog.Initialize_For_Multisell_Sale_Add_Item(active_L2H_Multisell, Popup_Item_Pick_Type.multisell_add_sale);
        }

        private void Multisell_Add_Cost_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            if (active_L2H_MultisellSale == null)
                return;
            Popup_Item_Selection dialog = new Popup_Item_Selection((mainWindow.GetPageOfType(typeof(ItemsPage)) as ItemsPage).L2H_Items);
            dialog.Initialize_For_Multisell_Sale_Add_Cost(active_L2H_MultisellSale, Popup_Item_Pick_Type.multisell_add_cost);
        }

        private void Multisell_Add_Multisell_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            L2H_Multisell newMultisell = new L2H_Multisell();
            newMultisell.Server_Multisell = new Server_Multisell(newMultisell, Get_Unique_Multisell_ID());
            L2H_Multisells.Add(newMultisell);
            
            L2H_Log.Instance.Log_Multisell_Add(newMultisell);

            CollectionViewSource.GetDefaultView(Multisell_IDs_Listview.ItemsSource).Refresh();
            Refresh_Multisell(newMultisell);
            Multisell_IDs_Listview.ScrollIntoView(Multisell_IDs_Listview.Items[Multisell_IDs_Listview.Items.Count - 1]);


        }

        private void Multisell_Delete_Multisell_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            if (active_L2H_Multisell == null)
                return;
            else
            {
                Popup_Confirmation newPopup = new Popup_Confirmation();
                newPopup.Confirmation_Action += (b, bb) => Delete_Multisell(active_L2H_Multisell);
                newPopup.Post_Confirmation_Action += (b, bb) => Refresh_Multisell_IDs_Listview();
                newPopup.InitializeConfirmation(active_L2H_Multisell.name_ID, "Multisells?", "");
            }
        }

        private void Delete_Multisell(L2H_Multisell L2H_Multisell)
        {
            L2H_Log.Instance.Log_Multisell_Delete(active_L2H_Multisell);

            L2H_Multisells.Remove(active_L2H_Multisell);
            active_L2H_Multisell = null;
            Multisell_Costs_Listview.ItemsSource = null;
            Multisell_Sales_Listview.ItemsSource = null;

            CollectionViewSource.GetDefaultView(Multisell_IDs_Listview.ItemsSource).Refresh();
            Additional_Fee_Grid.Visibility = Visibility.Hidden;

        }

        private void Multisell_Clone_Multisell_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            if (active_L2H_Multisell == null)
                return;
            else
            {
                L2H_Multisell newMultisell = ObjectExtensions.Copy(active_L2H_Multisell);
                newMultisell.name_ID = Get_Unique_Multisell_ID();
                L2H_Multisells.Add(newMultisell);

                L2H_Log.Instance.Log_Multisell_Clone(active_L2H_Multisell, newMultisell);

                CollectionViewSource.GetDefaultView(Multisell_IDs_Listview.ItemsSource).Refresh();

                Refresh_Multisell(newMultisell);

                Multisell_IDs_Listview.ScrollIntoView(Multisell_IDs_Listview.Items[Multisell_IDs_Listview.Items.Count - 1]);
            }
        }

        private string Get_Unique_Multisell_ID(int multisellID = 0)
        {
            if (L2H_Multisells.Exists(x => x.name_ID == "custom_multisell_" + multisellID.ToString()))
            {
                int nextValue = multisellID + 1;
                return Get_Unique_Multisell_ID(nextValue);
            }
            else
                return "custom_multisell_" + multisellID.ToString();
        }

        public void Click_Multisell()
        {

        }

        void ClearFocus()
        {
            if (Keyboard.FocusedElement != null)
            {
                IInputElement focusedElement = Keyboard.FocusedElement;
                Keyboard.ClearFocus();
                focusedElement.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
            }
        }

        private void Multisell_Sale_Add_To_Bundle_Clicked(object sender, RoutedEventArgs e)
        {
            L2H_MultisellSale multisellElement = (sender as FrameworkElement).DataContext as L2H_MultisellSale;

            if (multisellElement == null)
                return;

            Popup_Item_Selection dialog = new Popup_Item_Selection((mainWindow.GetPageOfType(typeof(ItemsPage)) as ItemsPage).L2H_Items);
            dialog.Initialize_For_Multisell_Bundle_Add_Item(multisellElement, Popup_Item_Pick_Type.multisell_add_bundled_item);
        }

        private void Multisell_Sale_Remove_From_Bundle_Clicked(object sender, RoutedEventArgs e)
        {
            L2H_MultisellSale_Item multisellSale_Item = (sender as FrameworkElement).DataContext as L2H_MultisellSale_Item;

            if (multisellSale_Item == null)
                return;

            if (multisellSale_Item.parent.sale_Items.Count > 1)
            {
                Popup_Confirmation newPopup = new Popup_Confirmation();
                newPopup.Confirmation_Action += (b, bb) => multisellSale_Item.parent.sale_Items.Remove(multisellSale_Item);
                newPopup.Post_Confirmation_Action += (b, bb) => Refresh_Multisell_Sales_Listview();
                newPopup.Post_Confirmation_Log_Action += (b, bb) => L2H_Log.Instance.Log_Multisell_Sale_Bundle_Remove(multisellSale_Item.parent.parent, multisellSale_Item.parent, multisellSale_Item.L2H_Item);
                newPopup.InitializeConfirmation(multisellSale_Item.L2H_Item.Item_Name, "sale bundle?", multisellSale_Item.GetIconString);

                
               
            }
            else
            {
                Popup_Confirmation newPopup = new Popup_Confirmation();
                newPopup.Confirmation_Action += (b, bb) => Multisell_Sale_Remove(multisellSale_Item.parent);
                newPopup.Post_Confirmation_Action += (b, bb) => Refresh_Multisell_Sales_Listview();
                newPopup.InitializeConfirmation(multisellSale_Item.parent.SaleItemNames, multisellSale_Item.parent.parent.name_ID + " sales?", multisellSale_Item.parent.sale_Items[0].GetIconString);
            }


        }

        private void Multisell_Reorder_Sale(object sender, RoutedEventArgs e)
        {
            L2H_MultisellSale sale = (sender as FrameworkElement).DataContext as L2H_MultisellSale;

            string direction = (sender as FrameworkElement).Tag.ToString();
            int oldIndex = sale.parent.GetItemsForSale.FindIndex(x => x == sale);
            int newIndex = 0;

            switch (direction)
            {
                case "up":
                                        

                    if (oldIndex == 0)
                        return;

                    newIndex = oldIndex - 1;

                    sale.parent.GetItemsForSale.RemoveAt(oldIndex);

                    sale.parent.GetItemsForSale.Insert(newIndex, sale);

                    Refresh_Multisell_Sales_Listview();

                    break;
                case "down":

                    if (oldIndex == sale.parent.GetItemsForSale.Count - 1)
                        return;

                    newIndex = oldIndex + 1;

                    sale.parent.GetItemsForSale.RemoveAt(oldIndex);

                    sale.parent.GetItemsForSale.Insert(newIndex, sale);

                    Refresh_Multisell_Sales_Listview();

                    break;
                default:
                    break;
            }

        }
    }


    public class ThreadWorker_Hook_Items_To_Multisells
    {
        public event EventHandler ThreadDone;

        public int startIndex;
        public int endIndex;
        public List<L2H_Multisell> L2H_Multisells;
        public List<L2H_Item> L2H_Items;
        public List<L2H_NPC> L2H_NPCs;

        public ThreadWorker_Hook_Items_To_Multisells()
        {

        }

        public void ThreadProc()
        {

            if (endIndex > L2H_Multisells.Count - 1)
                endIndex = L2H_Multisells.Count;

            for (int i = startIndex; i < endIndex; i++)
            {
                L2H_Multisell targetMultisell = L2H_Multisells[i];

                for (int j = 0; j < targetMultisell.Server_Multisell.elements.Count; j++)
                {

                    for (int k = 0; k < targetMultisell.Server_Multisell.elements[j].Multisell_Sale_Items.Count; k++)
                    {
                        targetMultisell.Server_Multisell.elements[j].Multisell_Sale_Items[k].L2H_Item = L2H_Items.Find(x => x.Item_Name_ID == targetMultisell.Server_Multisell.elements[j].Multisell_Sale_Items[k].itemID);
                    }

                    for (int k = 0; k < targetMultisell.Server_Multisell.elements[j].costItemIDs.Count; k++)
                    {
                       
                        L2H_MultisellCost newMultisellCost = new L2H_MultisellCost(targetMultisell.Server_Multisell.elements[j]);
                        if (targetMultisell.Server_Multisell.elements[j].costItemIDs[k] == "pccafe_point" || targetMultisell.Server_Multisell.elements[j].costItemIDs[k] == "pvp_point")
                        {
                            newMultisellCost.non_item_name = targetMultisell.Server_Multisell.elements[j].costItemIDs[k];
                        }
                        else
                        {
                            newMultisellCost.L2H_Item = L2H_Items.Find(x => x.Item_Name_ID == targetMultisell.Server_Multisell.elements[j].costItemIDs[k]);
                        }
                        newMultisellCost.Amount = targetMultisell.Server_Multisell.elements[j].costItemAmounts[k];


                        targetMultisell.Server_Multisell.elements[j].costs.Add(newMultisellCost);
                    }
                }

                if (targetMultisell.Server_Multisell.requiredNPCs != null)
                {
                    string[] splitNPCs = targetMultisell.Server_Multisell.requiredNPCs.requiredNPCsString.Split(';');

                    for (int j = 0; j < splitNPCs.Length; j++)
                    {
                        targetMultisell.Server_Multisell.requiredL2H_NPCs.Add(L2H_NPCs.Find(x => x.NPC_Name_ID == splitNPCs[j]));
                    }

                }

            }

            if (ThreadDone != null)
            {
                ThreadDone(this, EventArgs.Empty);
            }
        }
    }
}
