using L2Homage.Viewmodels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace L2Homage.Pages
{
    /// <summary>
    /// Interaction logic for RecipesPage.xaml
    /// </summary>
    public partial class RecipesPage : Page
    {
        public List<L2H_Recipe> L2H_Recipes;
        public List<Client_Recipe> client_Recipes;
        public List<Server_Recipe> server_Recipes;
        public bool recipesLoaded = false;

        MainWindow mainWindow;

        L2H_Recipe active_L2H_Recipe;

        public string client_Recipes_Startline;
        List<TextBlock> material_names;
        List<TextBox> material_amount;
        List<Button> material_buttons;
        List<StackPanel> material_amount_panels;
        List<Image> material_images;

        public RecipesPage()
        {
            InitializeComponent();

            L2H_Recipes = new List<L2H_Recipe>();
            client_Recipes = new List<Client_Recipe>();
            server_Recipes = new List<Server_Recipe>();
            material_names = new List<TextBlock>();
            material_amount = new List<TextBox>();
            material_buttons = new List<Button>();
            material_amount_panels = new List<StackPanel>();
            material_images = new List<Image>();

            #region shame on me, please don't tell anyone, I don't want to end up on /r/badcode
            material_names.Add(Preview_Recipe_Material_0_Name);
            material_names.Add(Preview_Recipe_Material_1_Name);
            material_names.Add(Preview_Recipe_Material_2_Name);
            material_names.Add(Preview_Recipe_Material_3_Name);
            material_names.Add(Preview_Recipe_Material_4_Name);
            material_names.Add(Preview_Recipe_Material_5_Name);
            material_names.Add(Preview_Recipe_Material_6_Name);
            material_names.Add(Preview_Recipe_Material_7_Name);
            material_names.Add(Preview_Recipe_Material_8_Name);
            material_names.Add(Preview_Recipe_Material_9_Name);
            material_names.Add(Preview_Recipe_Material_10_Name);

            material_buttons.Add(Preview_Recipe_Material_0_Button);
            material_buttons.Add(Preview_Recipe_Material_1_Button);
            material_buttons.Add(Preview_Recipe_Material_2_Button);
            material_buttons.Add(Preview_Recipe_Material_3_Button);
            material_buttons.Add(Preview_Recipe_Material_4_Button);
            material_buttons.Add(Preview_Recipe_Material_5_Button);
            material_buttons.Add(Preview_Recipe_Material_6_Button);
            material_buttons.Add(Preview_Recipe_Material_7_Button);
            material_buttons.Add(Preview_Recipe_Material_8_Button);
            material_buttons.Add(Preview_Recipe_Material_9_Button);
            material_buttons.Add(Preview_Recipe_Material_10_Button);

            material_amount.Add(Preview_Recipe_Material_0_Amount);
            material_amount.Add(Preview_Recipe_Material_1_Amount);
            material_amount.Add(Preview_Recipe_Material_2_Amount);
            material_amount.Add(Preview_Recipe_Material_3_Amount);
            material_amount.Add(Preview_Recipe_Material_4_Amount);
            material_amount.Add(Preview_Recipe_Material_5_Amount);
            material_amount.Add(Preview_Recipe_Material_6_Amount);
            material_amount.Add(Preview_Recipe_Material_7_Amount);
            material_amount.Add(Preview_Recipe_Material_8_Amount);
            material_amount.Add(Preview_Recipe_Material_9_Amount);
            material_amount.Add(Preview_Recipe_Material_10_Amount);

            material_images.Add(Preview_Recipe_Material_0_Icon);
            material_images.Add(Preview_Recipe_Material_1_Icon);
            material_images.Add(Preview_Recipe_Material_2_Icon);
            material_images.Add(Preview_Recipe_Material_3_Icon);
            material_images.Add(Preview_Recipe_Material_4_Icon);
            material_images.Add(Preview_Recipe_Material_5_Icon);
            material_images.Add(Preview_Recipe_Material_6_Icon);
            material_images.Add(Preview_Recipe_Material_7_Icon);
            material_images.Add(Preview_Recipe_Material_8_Icon);
            material_images.Add(Preview_Recipe_Material_9_Icon);
            material_images.Add(Preview_Recipe_Material_10_Icon);

            material_amount_panels.Add(Preview_Recipe_Amount_Panel_0);
            material_amount_panels.Add(Preview_Recipe_Amount_Panel_1);
            material_amount_panels.Add(Preview_Recipe_Amount_Panel_2);
            material_amount_panels.Add(Preview_Recipe_Amount_Panel_3);
            material_amount_panels.Add(Preview_Recipe_Amount_Panel_4);
            material_amount_panels.Add(Preview_Recipe_Amount_Panel_5);
            material_amount_panels.Add(Preview_Recipe_Amount_Panel_6);
            material_amount_panels.Add(Preview_Recipe_Amount_Panel_7);
            material_amount_panels.Add(Preview_Recipe_Amount_Panel_8);
            material_amount_panels.Add(Preview_Recipe_Amount_Panel_9);
            material_amount_panels.Add(Preview_Recipe_Amount_Panel_10);


            #endregion

            mainWindow = (Application.Current.MainWindow as MainWindow);
        }

        public void LoadRecipes()
        {
            Dispatcher.Invoke(() => { mainWindow.UpdateLog("Initializing Thread: Load Recipes..", L2H_Constants.Color_Log_Thread_Begin); });

            ItemsPage targetPage = (mainWindow.GetPageOfType(typeof(Pages.ItemsPage)) as ItemsPage);

            client_Recipes_Startline = File.ReadLines(L2H_Constants.client_Recipes_Path).First();

            ThreadWorker_Recipes threadWorker_LoadRecipes = new ThreadWorker_Recipes(L2H_Recipes, targetPage.L2H_Items, server_Recipes, client_Recipes, targetPage.itemdata, targetPage.client_Itemnames);
            threadWorker_LoadRecipes.ThreadDone += (sender, e) => HandleThreadDone(sender, e, "Loaded Recipes: " + server_Recipes.Count);
            Thread thread_LoadRecipes = new Thread(new ThreadStart(threadWorker_LoadRecipes.ThreadProc));
            thread_LoadRecipes.Start();
        }

        public void BindData()
        {
            var newView = new CollectionViewSource() { Source = L2H_Recipes };
            var listViewCollection2 = (ListCollectionView)newView.View;
            listViewCollection2.Filter = Filter_Recipe;
            Recipe_Name_Listview.ItemsSource = listViewCollection2;
            CollectionViewSource.GetDefaultView(Recipe_Name_Listview.ItemsSource).Refresh();

        }

        private void HandleThreadDone(object sender, EventArgs e, string logMessage)
        {
            Dispatcher.Invoke(() =>
            {

                (Application.Current.MainWindow as MainWindow).UpdateLog(logMessage, L2H_Constants.Color_Add);

                BindData();

                (Application.Current.MainWindow as MainWindow).UpdateLoadedNumber(LoadedTypes.Recipes);

                recipesLoaded = true;

            });
        }

        public void RefreshGridInterface()
        {

            Preview_Recipe_Name.DataContext = active_L2H_Recipe;
            Preview_Recipe_Image.DataContext = active_L2H_Recipe;
            Recipe_Details_Owner_Item_ID.DataContext = active_L2H_Recipe;
            Recipe_Details_Property_ID.DataContext = active_L2H_Recipe;
            Recipe_Details_Property_Name_ID.DataContext = active_L2H_Recipe;
            Recipe_Details_Property_Craft_Level.DataContext = active_L2H_Recipe;
            Recipe_Details_Property_MP_Consume.DataContext = active_L2H_Recipe;
            Recipe_Details_Property_Success_Rate.DataContext = active_L2H_Recipe;

            Recipe_Details_Property_Common_Recipe_Toggle.DataContext = active_L2H_Recipe;

            Recipe_Details_Property_Require_Extra_Recipe_Toggle.DataContext = active_L2H_Recipe;

            Preview_Recipe_Result_Name_A.DataContext = active_L2H_Recipe;
            Preview_Recipe_Result_Chance_A.DataContext = active_L2H_Recipe;
            Preview_Recipe_Result_Amount_A.DataContext = active_L2H_Recipe;
            Preview_Recipe_Result_Image_A_Icon.DataContext = active_L2H_Recipe;
            Preview_Recipe_Description.DataContext = active_L2H_Recipe;

            Preview_Recipe_Result_Name_B.DataContext = active_L2H_Recipe;
            Preview_Recipe_Result_Chance_B.DataContext = active_L2H_Recipe;
            Preview_Recipe_Result_Amount_B.DataContext = active_L2H_Recipe;
            Preview_Recipe_Result_Image_B_Icon.DataContext = active_L2H_Recipe;
                        

            //Materials
            for (int i = 0; i < 11; i++)
            {
                if (i < active_L2H_Recipe.recipe_Materials.Count)
                {
                    material_names[i].Text = active_L2H_Recipe.recipe_Materials[i].item.client_Itemname.name;
                    material_amount[i].Text = active_L2H_Recipe.recipe_Materials[i].amount.ToString();
                    material_images[i].Source = L2H_Parser.GetItemImage(active_L2H_Recipe.recipe_Materials[i].item.GetIconString());

                    material_buttons[i].Visibility = Visibility.Visible;
                    material_amount_panels[i].Visibility = Visibility.Visible;
                    material_names[i].Visibility = Visibility.Visible;
                    material_images[i].Visibility = Visibility.Visible;
                }
                else
                {
                    material_images[i].Visibility = Visibility.Hidden;
                    material_amount_panels[i].Visibility = Visibility.Hidden;
                    material_names[i].Visibility = Visibility.Hidden;
                    material_images[i].Visibility = Visibility.Hidden;
                    if (i == active_L2H_Recipe.recipe_Materials.Count)
                        material_buttons[i].Visibility = Visibility.Visible;
                    else
                        material_buttons[i].Visibility = Visibility.Hidden;
                }
            }


            CollectionViewSource.GetDefaultView(Recipe_Name_Listview.ItemsSource).Refresh();
        }
        private void Recipe_Name_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            var vm = (sender as FrameworkElement).DataContext;
            L2H_Recipe recipeClicked = vm as L2H_Recipe;

            if (active_L2H_Recipe != null)
            {
                if (active_L2H_Recipe == recipeClicked)
                {
                    active_L2H_Recipe.IsSelected = false;
                    active_L2H_Recipe = null;
                    Recipes_Details_Grid.Visibility = Visibility.Hidden;
                    return;
                }
                else
                {
                    active_L2H_Recipe.IsSelected = false;
                    active_L2H_Recipe = recipeClicked;
                    active_L2H_Recipe.IsSelected = true;
                    Recipes_Details_Grid.Visibility = Visibility.Visible;
                }
            }
            else
            {
                active_L2H_Recipe = recipeClicked;
                Recipes_Details_Grid.Visibility = Visibility.Visible;
            }

            RefreshGridInterface();
        }

        private bool Filter_Recipe(object item)
        {
            L2H_Recipe filteredObject = item as L2H_Recipe;

            if (filteredObject == null)
                return false;

            if (!string.IsNullOrEmpty(Recipe_Filter_Name.Text))
                if (filteredObject.Recipe_Name.IndexOf(Recipe_Filter_Name.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                {

                }
                else
                    return false;

            if (Recipe_Filter_Craft_Level.SelectedIndex != 10)
                if (filteredObject.Recipe_Craft_Level_Index != Recipe_Filter_Craft_Level.SelectedIndex)
                    return false;

            if (Item_Filter_Custom_Toggle.IsChecked == true)
                if (!filteredObject.owner_L2H_Item.IsCustom)
                    return false;


                return true;

        }


        private void Filter_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Recipe_Name_Listview.ItemsSource).Refresh();
        }
        private void Filter_Craft_Level_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Recipe_Name_Listview != null)
            CollectionViewSource.GetDefaultView(Recipe_Name_Listview.ItemsSource).Refresh();
        }
        private void Filter_Custom_Clicked(object sender, RoutedEventArgs e)
        {
            if (Recipe_Name_Listview != null)
                CollectionViewSource.GetDefaultView(Recipe_Name_Listview.ItemsSource).Refresh();
        }

        private void Material_Slot_Clicked(object sender, RoutedEventArgs e)
        {
            Popup_Item_Selection set_Item_Selection_Popup = new Popup_Item_Selection((mainWindow.GetPageOfType(typeof(Pages.ItemsPage)) as Pages.ItemsPage).L2H_Items);

            if (active_L2H_Recipe.recipe_Materials.Count < int.Parse((sender as Button).Tag.ToString()) + 1)
                set_Item_Selection_Popup.Initialize_For_Recipes(sender as Button, Popup_Item_Pick_Type.recipes_add_material, active_L2H_Recipe, null, null);
            else
                set_Item_Selection_Popup.Initialize_For_Recipes(sender as Button, Popup_Item_Pick_Type.recipes_replace_material, active_L2H_Recipe, null, active_L2H_Recipe.recipe_Materials[int.Parse((sender as Button).Tag.ToString())]);
        }

        private void Material_Slot_RightClicked(object sender, MouseButtonEventArgs e)
        {
            if (active_L2H_Recipe.recipe_Materials.Count == 1)
            {
                MessageBox.Show("Recipe cannot have 0 materials");
                return;
            }

            if (active_L2H_Recipe.recipe_Materials.Count <= int.Parse((sender as Button).Tag.ToString()))
                return;

            Popup_Confirmation newPopup = new Popup_Confirmation();
            newPopup.Confirmation_Action += (b, bb) => active_L2H_Recipe.RemoveMaterial(int.Parse((sender as Button).Tag.ToString()));
            newPopup.Post_Confirmation_Action += (b, bb) => RefreshGridInterface();
            L2H_Item target_Item = active_L2H_Recipe.recipe_Materials[int.Parse((sender as Button).Tag.ToString())].item;
            newPopup.InitializeConfirmation(target_Item.client_Itemname.name, "Recipe Materials?", target_Item.GetIconString(), target_Item);


        }
        private void Result_Slot_Clicked(object sender, RoutedEventArgs e)
        {
            Popup_Item_Selection set_Item_Selection_Popup = new Popup_Item_Selection((mainWindow.GetPageOfType(typeof(Pages.ItemsPage)) as Pages.ItemsPage).L2H_Items);

            if (active_L2H_Recipe.recipe_Results.Count < int.Parse((sender as Button).Tag.ToString()) + 1)
                set_Item_Selection_Popup.Initialize_For_Recipes(sender as Button, Popup_Item_Pick_Type.recipes_add_result, active_L2H_Recipe, null, null);
            else
                set_Item_Selection_Popup.Initialize_For_Recipes(sender as Button, Popup_Item_Pick_Type.recipes_replace_result, active_L2H_Recipe, active_L2H_Recipe.recipe_Results[int.Parse((sender as Button).Tag.ToString())]);
        }

        private void Result_Slot_RightClicked(object sender, MouseButtonEventArgs e)
        {
            if (active_L2H_Recipe.recipe_Results.Count == 1)
            {
                MessageBox.Show("Recipe cannot have 0 results");
                return;
            }

            if (active_L2H_Recipe.recipe_Results.Count <= int.Parse((sender as Button).Tag.ToString()))
                return;

            Popup_Confirmation newPopup = new Popup_Confirmation();
            newPopup.Confirmation_Action += (b, bb) => active_L2H_Recipe.RemoveResult(int.Parse((sender as Button).Tag.ToString()));
            newPopup.Post_Confirmation_Action += (b, bb) => RefreshGridInterface();
            L2H_Item target_Item = active_L2H_Recipe.recipe_Results[int.Parse((sender as Button).Tag.ToString())].item;
            newPopup.InitializeConfirmation(target_Item.client_Itemname.name, "Recipe Results?", target_Item.GetIconString(), target_Item);

        }

        private void Validate_Lower_Case_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Name_ID(e.Text);
        }
        private void Validate_Integer_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Integer(e.Text);
        }
        private void Validate_Any_Case_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Ingame_Name(e.Text);
        }
        private void TextBox_No_Spaces_Allowed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;

            L2H_Textbox_Input_Restrictions.Check_If_Dot_Exists_In_Float_TextBox(sender, e);
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

    }

    public class ThreadWorker_RecipePreview
    {
        public event EventHandler ThreadDone;
        ItemsPage itemsPage;
        public string recipeName;
        public string recipeResult;

        public string[] materialIDs;
        public string[] materialCounts;



        public ThreadWorker_RecipePreview(ItemsPage itemsPage, string recipeName, string recipeResult, string[] materialIDs, string[] materialCounts)
        {
            this.itemsPage = itemsPage;
            this.recipeName = recipeName;
            this.recipeResult = recipeResult;
            this.materialIDs = materialIDs;
            this.materialCounts = materialCounts;
        }

        public void ThreadProc()
        {
            recipeName = L2H_Parser.GetInGameNameFromItemID(itemsPage.client_Itemnames, recipeName);
            recipeResult = L2H_Parser.GetInGameNameFromNameID(itemsPage.client_Itemnames, itemsPage.itemdata, recipeResult);

            if (ThreadDone != null)
                ThreadDone.Invoke(this, EventArgs.Empty);
        }
    }

    public class ThreadWorker_Recipes
    {
        public event EventHandler ThreadDone;

        List<Server_Recipe> server_Recipes;
        List<Client_Recipe> client_Recipes;
        List<Server_Itemdata> server_Itemdata;
        List<Client_Itemname> client_Itemnames;
        List<L2H_Recipe> L2H_Recipes;
        List<L2H_Item> L2H_Items;

        public ThreadWorker_Recipes(List<L2H_Recipe> L2H_Recipes, List<L2H_Item> L2H_Items, List<Server_Recipe> server_Recipes, List<Client_Recipe> client_Recipes, List<Server_Itemdata> itemdata, List<Client_Itemname> itemnames)
        {
            this.server_Recipes = server_Recipes;
            this.client_Recipes = client_Recipes;
            this.server_Itemdata = itemdata;
            this.client_Itemnames = itemnames;
            this.L2H_Recipes = L2H_Recipes;
            this.L2H_Items = L2H_Items;
        }

        public void ThreadProc()
        {
            if (!File.Exists(L2H_Constants.client_Recipes_Path))
            {
                MessageBox.Show("Couldn't find Recipe-c.txt in decrypted client files folder :(");
            }
            else
            {
                int lineNumber = 0;
                using (TextReader textReader = new StreamReader(L2H_Constants.client_Recipes_Path, Encoding.GetEncoding(65001)))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            if (lineNumber > 0)
                                client_Recipes.Add(new Client_Recipe(line));
                        }
                        lineNumber++;
                    }

                }
            }

            if (!File.Exists(L2H_Constants.server_Recipes_Path))
            {
                MessageBox.Show("Couldn't find recipe.txt in server files folder :(");
            }
            else
            {
                using (TextReader textReader = new StreamReader(L2H_Constants.server_Recipes_Path, Encoding.GetEncoding(1200)))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                            server_Recipes.Add(new Server_Recipe(line));
                    }
                }
            }

            for (int i = 0; i < client_Recipes.Count; i++)
            {

                Server_Itemdata targetItemData = server_Itemdata.Find(x => x.itemId == client_Recipes[i].id_recipe);
                Client_Itemname targetitemName = client_Itemnames.Find(x => x.id == targetItemData.itemId);
                Client_Recipe targetClientData = client_Recipes[i];
                Server_Recipe targetServerData = server_Recipes.Find(x => x.id == targetClientData.id_mk);
                L2H_Item owner_L2H_Item = L2H_Items.Find(x => x.ID.ToString() == targetItemData.itemId);

                Client_Etc targetClientEtc;

                if (owner_L2H_Item != null)
                    targetClientEtc = owner_L2H_Item.client_Etc;
                else
                    targetClientEtc = null;


                if (targetItemData == null || targetitemName == null || targetClientData == null || targetServerData == null || targetClientEtc == null)
                    continue;

                L2H_Recipe newRecipe = new L2H_Recipe();
                newRecipe.owner_L2H_Item = owner_L2H_Item;
                newRecipe.owner_Itemdata = targetItemData;
                newRecipe.owner_ItemName = targetitemName;
                newRecipe.server_Recipe = targetServerData;
                newRecipe.client_Recipe = targetClientData;
                newRecipe.client_Etc = targetClientEtc;
                newRecipe.recipe_Results = new List<L2H_Recipe_Result>();
                newRecipe.recipe_Materials = new List<L2H_Recipe_Material>();

                int amount_A = 1;
                if (!string.IsNullOrEmpty(newRecipe.server_Recipe.productProbability[0]))
                    amount_A = int.Parse(newRecipe.server_Recipe.productProbability[0]);

                newRecipe.AddResult(L2H_Items.Find(x => x.server_Itemdata.itemName == newRecipe.server_Recipe.productNames[0]), int.Parse(newRecipe.server_Recipe.productProbability[0]), int.Parse(newRecipe.server_Recipe.productAmount[0]));

                if (newRecipe.server_Recipe.productNames.Count > 1)
                    newRecipe.AddResult(L2H_Items.Find(x => x.server_Itemdata.itemName == newRecipe.server_Recipe.productNames[1]), int.Parse(newRecipe.server_Recipe.productProbability[1]), int.Parse(newRecipe.server_Recipe.productAmount[1]));

                newRecipe.ID = int.Parse(newRecipe.client_Recipe.id_mk);

                if (!string.IsNullOrEmpty(newRecipe.client_Recipe.materials_m_0_id))
                {
                    newRecipe.AddMaterial(L2H_Items.Find(x => x.ID.ToString() == newRecipe.client_Recipe.materials_m_0_id), int.Parse(newRecipe.client_Recipe.materials_m_0_cnt));
                }
                if (!string.IsNullOrEmpty(newRecipe.client_Recipe.materials_m_1_id))
                {
                    newRecipe.AddMaterial(L2H_Items.Find(x => x.ID.ToString() == newRecipe.client_Recipe.materials_m_1_id), int.Parse(newRecipe.client_Recipe.materials_m_1_cnt));
                }
                if (!string.IsNullOrEmpty(newRecipe.client_Recipe.materials_m_2_id))
                {
                    newRecipe.AddMaterial(L2H_Items.Find(x => x.ID.ToString() == newRecipe.client_Recipe.materials_m_2_id), int.Parse(newRecipe.client_Recipe.materials_m_2_cnt));
                }
                if (!string.IsNullOrEmpty(newRecipe.client_Recipe.materials_m_3_id))
                {
                    newRecipe.AddMaterial(L2H_Items.Find(x => x.ID.ToString() == newRecipe.client_Recipe.materials_m_3_id), int.Parse(newRecipe.client_Recipe.materials_m_3_cnt));
                }
                if (!string.IsNullOrEmpty(newRecipe.client_Recipe.materials_m_4_id))
                {
                    newRecipe.AddMaterial(L2H_Items.Find(x => x.ID.ToString() == newRecipe.client_Recipe.materials_m_4_id), int.Parse(newRecipe.client_Recipe.materials_m_4_cnt));
                }
                if (!string.IsNullOrEmpty(newRecipe.client_Recipe.materials_m_5_id))
                {
                    newRecipe.AddMaterial(L2H_Items.Find(x => x.ID.ToString() == newRecipe.client_Recipe.materials_m_5_id), int.Parse(newRecipe.client_Recipe.materials_m_5_cnt));
                }
                if (!string.IsNullOrEmpty(newRecipe.client_Recipe.materials_m_6_id))
                {
                    newRecipe.AddMaterial(L2H_Items.Find(x => x.ID.ToString() == newRecipe.client_Recipe.materials_m_6_id), int.Parse(newRecipe.client_Recipe.materials_m_6_cnt));
                }
                if (!string.IsNullOrEmpty(newRecipe.client_Recipe.materials_m_7_id))
                {
                    newRecipe.AddMaterial(L2H_Items.Find(x => x.ID.ToString() == newRecipe.client_Recipe.materials_m_7_id), int.Parse(newRecipe.client_Recipe.materials_m_7_cnt));
                }
                if (!string.IsNullOrEmpty(newRecipe.client_Recipe.materials_m_8_id))
                {
                    newRecipe.AddMaterial(L2H_Items.Find(x => x.ID.ToString() == newRecipe.client_Recipe.materials_m_8_id), int.Parse(newRecipe.client_Recipe.materials_m_8_cnt));
                }
                if (!string.IsNullOrEmpty(newRecipe.client_Recipe.materials_m_9_id))
                {
                    newRecipe.AddMaterial(L2H_Items.Find(x => x.ID.ToString() == newRecipe.client_Recipe.materials_m_9_id), int.Parse(newRecipe.client_Recipe.materials_m_9_cnt));
                }
                if (!string.IsNullOrEmpty(newRecipe.client_Recipe.materials_m_10_id))
                {
                    newRecipe.AddMaterial(L2H_Items.Find(x => x.ID.ToString() == newRecipe.client_Recipe.materials_m_10_id), int.Parse(newRecipe.client_Recipe.materials_m_10_cnt));
                }

                L2H_Recipes.Add(newRecipe);

            }

            if (ThreadDone != null)
            {
                ThreadDone(this, EventArgs.Empty);
            }
        }


    }
}
