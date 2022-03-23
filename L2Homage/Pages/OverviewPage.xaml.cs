using System;
using System.Collections.Generic;
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

namespace L2Homage.Pages
{

    public partial class OverviewPage : Page
    {
        MainWindow mainWindow;
        public L2H_Settings L2H_Settings;
        bool loaded = false;


        public OverviewPage()
        {
            InitializeComponent();

            LoadL2HSettings();
        }

        private void StartInitializeProcess(object sender, RoutedEventArgs e)
        {
            if (loaded)
                return;
            if ((sender as Button).Tag.ToString() == "StartInitializeProcess")
            {
                UpdateLogSimple("Initialization process started..");
                loaded = true;
                mainWindow = ((MainWindow)Application.Current.MainWindow);

                ((MainWindow)Application.Current.MainWindow).Initialize();

                StartInitializationButton.Content = "Initialized";
                StartInitializationButton.IsEnabled = false;
            }
        }

        private void LoadL2HSettings()
        {
            List<string> settingsData = new List<string>();
            if (File.Exists(L2H_Constants.L2H_Settings_Path))
            {
                using (TextReader textReader = new StreamReader(L2H_Constants.L2H_Settings_Path, Encoding.GetEncoding(1200)))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        settingsData.Add(line);
                    }

                    L2H_Settings = new L2H_Settings(settingsData);


                    Overview_Server_Address_Textbox.Text = L2H_Settings.serverAddress;
                    Overview_Export_Settings_Custom_Only_Toggle.IsChecked = ((L2H_Settings.exportOnlyCustomSpawnAreas == "true") ? true : false);
                    Overview_Export_Settings_Use_Module_Toggle.IsChecked = (L2H_Settings.usingDiablomizedSkills == "true") ? true : false;
                    Overview_New_Item_Index_Start_Textbox.Text = L2H_Settings.NewItemIndexStart;
                    Overview_New_NPC_Index_Start_Textbox.Text = L2H_Settings.NewNPCIndexStart;
                    Overview_New_Skill_Index_Start_Textbox.Text = L2H_Settings.NewSkillIndexStart;


                }
            }
            else
            {
                File.Create(L2H_Constants.L2H_Settings_Path).Dispose();

                settingsData.Add("ServerAddress = " + "127.0.0.1");
                settingsData.Add("ExportOnlyCustomSpawnAreas = " + "false");
                settingsData.Add("UsingDiablomizedSkills = " + "false");
                settingsData.Add("NewItemIndexStart = " + "50000");
                settingsData.Add("NewNPCIndexStart = " + "37700");
                settingsData.Add("NewSkillIndexStart = " + "50000");

                File.WriteAllLines(L2H_Constants.L2H_Settings_Path, settingsData, Encoding.GetEncoding(1200));

                L2H_Settings = new L2H_Settings(settingsData);
                Overview_Server_Address_Textbox.Text = L2H_Settings.serverAddress;
                Overview_Export_Settings_Custom_Only_Toggle.IsChecked = (L2H_Settings.exportOnlyCustomSpawnAreas == "true") ? true : false;
                Overview_Export_Settings_Use_Module_Toggle.IsChecked = (L2H_Settings.usingDiablomizedSkills == "true") ? true : false;
                Overview_New_Item_Index_Start_Textbox.Text = L2H_Settings.NewItemIndexStart;
                Overview_New_NPC_Index_Start_Textbox.Text = L2H_Settings.NewNPCIndexStart;
                Overview_New_Skill_Index_Start_Textbox.Text = L2H_Settings.NewSkillIndexStart;
            }

            Overview_Server_Address_Textbox.DataContext = L2H_Settings;
            Overview_Export_Settings_Custom_Only_Toggle.DataContext = L2H_Settings;
            Overview_Export_Settings_Use_Module_Toggle.DataContext = L2H_Settings;
            Overview_New_Item_Index_Start_Textbox.DataContext = L2H_Settings;
            Overview_New_NPC_Index_Start_Textbox.DataContext = L2H_Settings;
            Overview_New_Skill_Index_Start_Textbox.DataContext = L2H_Settings;
        }

        public void UpdateLoadedNumber(LoadedTypes loadType)
        {
            Dispatcher.Invoke(() =>
            {
                switch (loadType)
                {
                    case LoadedTypes.Weapons:
                        UI_LoadedWeaponsCount.Text = (mainWindow.GetPageOfType(typeof(Pages.ItemsPage)) as ItemsPage).client_Weapons.Count.ToString();
                        break;
                    case LoadedTypes.Armors:
                        UI_LoadedArmorsCount.Text = (mainWindow.GetPageOfType(typeof(Pages.ItemsPage)) as ItemsPage).client_Armors.Count.ToString();
                        break;
                    case LoadedTypes.Etcs:
                        UI_LoadedEtcsCount.Text = (mainWindow.GetPageOfType(typeof(Pages.ItemsPage)) as ItemsPage).client_Etcs.Count.ToString();
                        break;
                    case LoadedTypes.Sets:
                        UI_LoadedSetsCount.Text = (mainWindow.GetPageOfType(typeof(Pages.ItemsPage)) as ItemsPage).L2H_Sets.Count.ToString();
                        break;
                    case LoadedTypes.Recipes:
                        UI_LoadedRecipesCount.Text = (mainWindow.GetPageOfType(typeof(Pages.RecipesPage)) as RecipesPage).server_Recipes.Count.ToString();
                        break;
                    case LoadedTypes.NPCs:
                        UI_LoadedNPCCount.Text = (mainWindow.GetPageOfType(typeof(Pages.NPCsPage)) as NPCsPage).server_Npcdata.Count.ToString();
                        break;
                    case LoadedTypes.Droplists:
                        int numberOfSingleDroplists = 0;
                        int numberOfMultiDroplists = 0;
                        int numberOfCustomSingleDroplists = 0;
                        int numberOfCustomMultiDroplists = 0;

                        List<L2H_Droplist> droplists = (mainWindow.GetPageOfType(typeof(DroplistsPage)) as DroplistsPage).L2H_Droplists;

                        for (int i = 0; i < droplists.Count; i++)
                        {
                            if (droplists[i].Droplist_Type == L2H_Droplist_Type.single)
                            {
                                if (droplists[i].IsCustom == true)
                                    numberOfCustomSingleDroplists++;
                                else
                                    numberOfSingleDroplists++;
                            }
                            else
                            {
                                if (droplists[i].IsCustom == true)
                                    numberOfCustomMultiDroplists++;
                                else
                                    numberOfMultiDroplists++;
                            }
                        }

                        UI_LoadedDroplists_SingleCount.Text = numberOfSingleDroplists.ToString();
                        UI_LoadedDroplists_MultiCount.Text = numberOfMultiDroplists.ToString();
                        UI_LoadedDroplists_Custom_SingleCount.Text = numberOfCustomSingleDroplists.ToString();
                        UI_LoadedDroplists_Custom_MultiCount.Text = numberOfCustomMultiDroplists.ToString();
                        break;
                    case LoadedTypes.Domains:
                        UI_LoadedDomainsCount.Text = (mainWindow.GetPageOfType(typeof(Pages.SpawnsPage)) as SpawnsPage).Get_Total_Domains_Number.ToString();
                        break;
                    case LoadedTypes.Spawn_Areas:
                        UI_LoadedSpawnAreasCount.Text = (mainWindow.GetPageOfType(typeof(Pages.SpawnsPage)) as SpawnsPage).L2H_Spawn_Areas.Count().ToString();
                        break;
                    case LoadedTypes.NPC_Makers:
                        UI_LoadedNPCMakersCount.Text = (mainWindow.GetPageOfType(typeof(Pages.SpawnsPage)) as SpawnsPage).Get_Total_Makers_Number.ToString();
                        break;
                    case LoadedTypes.NPC_Ex_Makers:
                        break;
                    case LoadedTypes.NPC_Begins:
                        UI_LoadedNPCSpawns.Text = (mainWindow.GetPageOfType(typeof(Pages.SpawnsPage)) as SpawnsPage).Get_Total_Spawns_Number.ToString();
                        break;
                    case LoadedTypes.NPC_Ex_Begins:
                        break;
                    case LoadedTypes.Domains_Custom:
                        break;
                    case LoadedTypes.Spawn_Areas_Custom:
                        break;
                    case LoadedTypes.NPC_Makers_Custom:
                        break;
                    case LoadedTypes.NPC_Ex_Makers_Custom:
                        break;
                    case LoadedTypes.NPC_Begins_Custom:
                        break;
                    case LoadedTypes.NPC_Ex_Begins_Custom:
                        break;
                    case LoadedTypes.Skills:
                        UI_LoadedSkillsCount.Text = (mainWindow.GetPageOfType(typeof(Pages.SkillsPage)) as SkillsPage).Get_L2H_Skills.Count.ToString();
                        break;
                    case LoadedTypes.Empty:
                        break;
                    case LoadedTypes.Chat_Filters:
                        UI_ChatFilterCount.Text = (mainWindow.GetPageOfType(typeof(Pages.SystemTextPage)) as SystemTextPage).L2H_Chatfilters.Count.ToString();
                        break;
                    case LoadedTypes.Gametips:
                        UI_GametipsCount.Text = (mainWindow.GetPageOfType(typeof(Pages.SystemTextPage)) as SystemTextPage).L2H_Gametips.Count.ToString();
                        break;
                    case LoadedTypes.NPC_Strings:
                        UI_NPCStringsCount.Text = (mainWindow.GetPageOfType(typeof(Pages.SystemTextPage)) as SystemTextPage).L2H_NPC_Strings.Count.ToString();
                        break;
                    case LoadedTypes.System_Messages:
                        UI_SystemMessagesCount.Text = (mainWindow.GetPageOfType(typeof(Pages.SystemTextPage)) as SystemTextPage).L2H_System_Messages.Count.ToString();
                        break;
                    case LoadedTypes.System_Strings:
                        UI_SystemStringsCount.Text = (mainWindow.GetPageOfType(typeof(Pages.SystemTextPage)) as SystemTextPage).L2H_System_Strings.Count.ToString();
                        break;
                    case LoadedTypes.Huntingzones:
                        UI_HuntingzonesCount.Text = (mainWindow.GetPageOfType(typeof(Pages.SpawnsPage)) as SpawnsPage).L2H_Huntingzones.Count.ToString();
                        break;
                    case LoadedTypes.Zonenames:
                        UI_ZonenamesCount.Text = (mainWindow.GetPageOfType(typeof(Pages.SpawnsPage)) as SpawnsPage).L2H_Zonenames.Count.ToString();
                        break;
                    case LoadedTypes.Raids:
                        UI_RaidsCount.Text = (mainWindow.GetPageOfType(typeof(Pages.SpawnsPage)) as SpawnsPage).L2H_Raids.Count.ToString();
                        break;
                    case LoadedTypes.Multisell:
                        UI_LoadedMultisellCount.Text = (mainWindow.GetPageOfType(typeof(Pages.MultisellPage)) as MultisellPage).L2H_Multisells.Count.ToString();
                        break;
                    case LoadedTypes.Enchanted_Skills:
                        UI_LoadedSkillsEnchantedCount.Text = (mainWindow.GetPageOfType(typeof(Pages.SkillsPage)) as SkillsPage).server_Skillenchantdata.Count.ToString();
                        break;
                    default:
                        break;
                }
            });
        }

        public void UpdateLogSimple(string content, string color = null)
        {
            if (color == null)
            {
                LogPanel.AppendText(content + "\n");
            }
            else
            {
                AppendText(LogPanel, content + "\n", color);
            }

            LogPanel.ScrollToEnd();
        }

        public void UpdateLogAdvanced(Paragraph paragraph)
        {

            LogPanel.Document.Blocks.Add(paragraph);
            DirectoryInfo di = Directory.CreateDirectory(L2H_Constants.logs_path);
            string textToAppend = "";
            for (int i = 0; i < paragraph.Inlines.Count; i++)
            {
                textToAppend += paragraph.Inlines.ElementAt(i).ContentStart.GetTextInRun(LogicalDirection.Forward);
            }

            File.AppendAllText(L2H_Constants.logs_path + "\\" + DateTime.Now.ToString("dd MMMM yyyy") + ".txt",
                textToAppend + Environment.NewLine);

            LogPanel.ScrollToEnd();
        }

        public void AppendText(RichTextBox box, string text, string color)
        {
            BrushConverter bc = new BrushConverter();
            TextRange tr = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
            tr.Text = text;
            try
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty,
                    bc.ConvertFromString(color));
            }
            catch (FormatException) { }
        }

        private void LogPanel_Loaded(object sender, RoutedEventArgs e)
        {
            LogPanel.ScrollToEnd();
        }


        private void Start_Export(object sender, RoutedEventArgs e)
        {
            L2H_Exporter.Instance.Save_All(true);
        }

        private void Save_Clicked(object sender, RoutedEventArgs e)
        {
            L2H_Exporter.Instance.Save_All(false);
        }
    }
}
