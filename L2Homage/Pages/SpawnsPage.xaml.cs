//This used to be only for spawns, hence the name "SpawnsPage". I decided to integrate the two other features (zonename and huntingzone) that use coordinates.
//This was done to only load the world map images once, preventing redundancy. It also fits relatively well into the design.
//The naming convention may be a little confusing because of this change. If someone wants to change this, go ahead. I'm going to focus on the next features for now.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace L2Homage.Pages
{
    /// <summary>
    /// Interaction logic for SpawnsPage.xaml
    /// </summary>
    public partial class SpawnsPage : Page
    {
        MainWindow mainWindow;
        NPCsPage npcsPage;

        #region Spawn Zones
        //Data
        public Server_Npcpos server_Npcpos_Original;
        public Server_Npcpos server_Npcpos_Custom;

        public List<L2H_Spawn_Area> L2H_Spawn_Areas;
        public List<L2H_Spawn_NPC_Maker_NPC_Spawn> L2H_Spawn_NPC_Maker_NPCs;
        public List<L2H_Spawn_NPC_Maker> L2H_Spawn_NPC_Makers;

        public ToggleButton activeZoneTypeToggle;

        //Interface
        List<L2H_Spawn_Shortcut> shortcuts;
        L2H_Spawn_Selection_Block activeSelectionBlock;
        L2H_Spawn_Selection_Block previewSelectionBlock;
        List<L2H_Spawn_Selection_Block> selectionBlocks;

        PointCollection Spawn_Area_Preview_Polygon_Points { get; set; }
        PointCollection Spawn_Area_Preview_Polygon_Arrow_Points { get; set; }
        PointCollection Spawn_Area_Selected_Polygon_Points { get; set; }
        PointCollection Spawn_Area_Selected_Polygon_Arrow_Points { get; set; }

        SpawnTerritory previewTerritory;
        L2H_Spawn_Area active_Spawn_Area;
        L2H_Spawn_NPC_Maker active_Spawn_NPC_Maker;
        L2H_Spawn_NPC_Maker_NPC_Spawn active_Spawn_NPC_Maker_NPC_Spawn;

        #endregion

        #region Hunting Zones
        public List<Client_Huntingzone> client_Huntingzones;
        public List<L2H_Huntingzone> L2H_Huntingzones;
        public string huntingzoneStartLine;
        ToggleButton active_Hunting_Zone_ToggleButton;
        L2H_Huntingzone preview_L2H_Huntingzone;
        L2H_Huntingzone active_L2H_Huntingzone;

        PointCollection Huntingzone_Preview_Polygon_Points { get; set; }
        PointCollection Huntingzone_Preview_Polygon_Arrow_Points { get; set; }
        PointCollection Huntingzone_Selected_Polygon_Points { get; set; }
        PointCollection Huntingzone_Selected_Polygon_Arrow_Points { get; set; }

        #endregion

        #region Zone Names
        public List<Client_Zonename> client_Zonenames;
        public List<L2H_Zonename> L2H_Zonenames;
        public string zonenameStartLine;
        ToggleButton active_Zonename_ToggleButton;
        L2H_Zonename active_L2H_Zonename;
        L2H_Zonename preview_L2H_Zonename;

        PointCollection Zonename_Preview_Polygon_Points { get; set; }
        PointCollection Zonename_Preview_Polygon_Arrow_Points { get; set; }
        PointCollection Zonename_Selected_Polygon_Points { get; set; }
        PointCollection Zonename_Selected_Polygon_Arrow_Points { get; set; }
        #endregion

        #region Raids
        public List<Client_Raid> client_Raids;
        public List<L2H_Raid> L2H_Raids;
        public string raidsStartLine;
        ToggleButton active_Raid_ToggleButton;
        L2H_Raid preview_L2H_Raid;
        L2H_Raid active_L2H_Raid;

        PointCollection Raid_Preview_Polygon_Points { get; set; }
        PointCollection Raid_Preview_Polygon_Arrow_Points { get; set; }
        PointCollection Raid_Selected_Polygon_Points { get; set; }
        PointCollection Raid_Selected_Polygon_Arrow_Points { get; set; }

        #endregion

        public L2H_Spawn_Area Get_Active_Spawn_Area
        {
            get
            {
                return active_Spawn_Area;
            }
        }

        public int Get_Total_Makers_Number
        {
            get
            {
                int x = 0;
                for (int i = 0; i < L2H_Spawn_Areas.Count; i++)
                {
                    if (L2H_Spawn_Areas[i].NPC_Makers.Count > 0)
                        x += L2H_Spawn_Areas[i].NPC_Makers.Count;
                }

                return x;
            }
        }

        public int Get_Total_Spawns_Number
        {
            get
            {
                int x = 0;
                for (int i = 0; i < L2H_Spawn_Areas.Count; i++)
                {
                    if (L2H_Spawn_Areas[i].NPC_Makers.Count > 0)
                        for (int j = 0; j < L2H_Spawn_Areas[i].NPC_Makers.Count; j++)
                        {
                            if (L2H_Spawn_Areas[i].NPC_Makers[j].Npc_Maker != null)
                            {
                                if (L2H_Spawn_Areas[i].NPC_Makers[j].Npc_Maker.npcs != null)
                                    x += L2H_Spawn_Areas[i].NPC_Makers[j].Npc_Maker.npcs.Count;
                            }
                            if (L2H_Spawn_Areas[i].NPC_Makers[j].Npc_Maker_Ex != null)
                            {
                                if (L2H_Spawn_Areas[i].NPC_Makers[j].Npc_Maker_Ex.npcs != null)
                                    x += L2H_Spawn_Areas[i].NPC_Makers[j].Npc_Maker_Ex.npcs.Count;
                            }
                        }

                }

                return x;
            }
        }

        public int Get_Total_Domains_Number
        {
            get
            {
                return server_Npcpos_Original.domains.Count + server_Npcpos_Custom.domains.Count;
            }
        }


        public SpawnsPage()
        {
            InitializeComponent();

            InitializeWorldMapZonePicker();
            InitializeShortcuts();

            Spawns_Zone_Details_Grid.Visibility = Visibility.Hidden;
            Spawns_Zone_Selection_Overview_Grid.Visibility = Visibility.Visible;

            mainWindow = (Application.Current.MainWindow as MainWindow);
            L2H_Spawn_Areas = new List<L2H_Spawn_Area>();
            L2H_Spawn_NPC_Makers = new List<L2H_Spawn_NPC_Maker>();
            L2H_Spawn_NPC_Maker_NPCs = new List<L2H_Spawn_NPC_Maker_NPC_Spawn>();

            npcsPage = (mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage);
            activeZoneTypeToggle = Zone_Type_Toggle_Spawn_Areas;
            Refresh_Zone_Type_Selection();


        }


        void InitializeWorldMapZonePicker()
        {
            int startZoneWidthID = 10;
            int startZoneHeightID = 10;

            selectionBlocks = new List<L2H_Spawn_Selection_Block>();

            List<StackPanel> gridRows = new List<StackPanel>();

            gridRows.Add(Image_Row_0);
            gridRows.Add(Image_Row_1);
            gridRows.Add(Image_Row_2);
            gridRows.Add(Image_Row_3);
            gridRows.Add(Image_Row_4);
            gridRows.Add(Image_Row_5);
            gridRows.Add(Image_Row_6);
            gridRows.Add(Image_Row_7);
            gridRows.Add(Image_Row_8);
            gridRows.Add(Image_Row_9);
            gridRows.Add(Image_Row_10);
            gridRows.Add(Image_Row_11);
            gridRows.Add(Image_Row_12);
            gridRows.Add(Image_Row_13);
            gridRows.Add(Image_Row_14);
            gridRows.Add(Image_Row_15);

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    L2H_Spawn_Selection_Block block = new L2H_Spawn_Selection_Block();

                    Border newBorder = new Border();
                    newBorder.BorderBrush = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(L2H_Constants.Color_Worldmap_Edges));
                   
                    newBorder.BorderThickness = new Thickness(1);
                    block.border = newBorder;

                    Button worldmapZoneButton = new Button();
                    worldmapZoneButton.MaxWidth = 64;
                    worldmapZoneButton.MaxHeight = 64;
                    worldmapZoneButton.Style = Application.Current.FindResource("HomageButtonStyle") as Style;
                    worldmapZoneButton.Tag = (startZoneWidthID + j).ToString() + "_" + (startZoneHeightID + i).ToString();
                    worldmapZoneButton.Click += Spawn_Selection_Block_Clicked;



                    worldmapZoneButton.DataContext = block;
                    block.button = worldmapZoneButton;
                    block.ZoneID = worldmapZoneButton.Tag.ToString();

                    System.Windows.Controls.Image newImage = new System.Windows.Controls.Image { Source = L2H_Parser.GetWorldZoneImage((startZoneWidthID + j).ToString() + "_" + (startZoneHeightID + i).ToString(), true) };
                    System.Windows.Controls.Image opacityImage = new System.Windows.Controls.Image { Source = new BitmapImage(new Uri("/Images/white.png", UriKind.Relative)), Opacity = 0 };
                    Grid newGrid = new Grid();
                    newGrid.Children.Add(newImage);
                    newGrid.Children.Add(opacityImage);
                    worldmapZoneButton.Content = newGrid;
                    VerticalAlignment = VerticalAlignment.Center;
                    block.image = newImage;
                    block.opacityImage = opacityImage;

                    newBorder.Child = worldmapZoneButton;
                    worldmapZoneButton.MouseEnter += Spawn_Selection_Mouse_Over;

                    selectionBlocks.Add(block);
                    gridRows[i].Children.Add(newBorder);
                }
            }
        }

        void InitializeShortcuts()
        {
            shortcuts = new List<L2H_Spawn_Shortcut>();

            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Abandoned Coal Mines", "25_12", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Ant Nest", "19_23", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Archaic Laboratory", "22_14", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Cave of Trials", "20_14", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Cruma Tower", "20_21", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Elven Fortress", "20_20", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Elven Ruins", "21_25", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Monastery of Silence", "23_15", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Nornil's Cave", "17_19", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Nornil's Garden", "16_20", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Pagan Temple", "19_16", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "School of Dark Arts", "18_19", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Seed of Destruction", "12_24", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Tower of Insolence", "23_18", 1));


            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Catacomb of Dark Omens", "19_18", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Catacomb of the Apostate", "22_20", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Catacomb of the Branded", "21_23", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Catacomb of the Forbidden Path", "23_20", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Catacomb of the Heretic", "21_22", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Catacomb of the Witch", "24_20", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Necropolis of the Disciple", "25_17", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Necropolis of the Martyr", "23_22", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Necropolis of Devotion", "18_20", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Necropolis of Sacrifice", "18_24", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Necropolis of the Patriot", "19_20", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Necropolis of the Pilgrim", "21_21", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Necropolis of the Saint", "22_24", 1));
            shortcuts.Add(new L2H_Spawn_Shortcut(selectionBlocks, "Necropolis of Worship", "23_23", 1));


            Spawns_Zone_Shortcuts_Listview.ItemsSource = shortcuts;


        }

        #region Loading
        public void LoadSpawns()
        {
            LoadNpcPos();

            LoadCustomNpcPos();
            LoadSpawnAreas();
            mainWindow.UpdateLoadedNumber(LoadedTypes.Spawn_Areas);
            mainWindow.UpdateLoadedNumber(LoadedTypes.NPC_Makers);
            mainWindow.UpdateLoadedNumber(LoadedTypes.NPC_Begins);
            LoadHuntingzones();
            mainWindow.UpdateLoadedNumber(LoadedTypes.Huntingzones);
            LoadZonenames();
            mainWindow.UpdateLoadedNumber(LoadedTypes.Zonenames);
            LoadRaiddata();
            mainWindow.UpdateLoadedNumber(LoadedTypes.Raids);
            mainWindow.UpdateLoadedNumber(LoadedTypes.Domains);

        }

        public void LoadNpcPos()
        {
            Dispatcher.Invoke(() => { mainWindow.UpdateLog("Initializing Thread: Load Spawns..", L2H_Constants.Color_Log_Thread_Begin); });

            List<string> npcposData = new List<string>();

            bool originalNpcPosLoaded = false;

            if (!File.Exists(L2H_Constants.original_npcpos_path))
            {
                File.Create(L2H_Constants.original_npcpos_path).Close();


                using (TextReader textReader = new StreamReader(L2H_Constants.server_NpcposPath, Encoding.GetEncoding(1200)))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        npcposData.Add(line);
                    }

                }

                server_Npcpos_Original = new Server_Npcpos(npcposData, false);

            }
            else
            {
                // Load text
                using (TextReader textReader = new StreamReader(L2H_Constants.original_npcpos_path, Encoding.GetEncoding(1200)))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        npcposData.Add(line);
                    }

                }

                server_Npcpos_Original = new Server_Npcpos(npcposData, false);
                originalNpcPosLoaded = true;

            }

            if (!originalNpcPosLoaded)
            {
                ExportOriginalNpcPos(true);
            }

        }


        private void LoadCustomNpcPos()
        {

            List<string> npcposData = new List<string>();

            if (!File.Exists(L2H_Constants.custom_npcpos_path))
            {
                File.Create(L2H_Constants.custom_npcpos_path).Close();

                server_Npcpos_Custom = new Server_Npcpos();
            }
            else
            {
                // Load text
                using (TextReader textReader = new StreamReader(L2H_Constants.custom_npcpos_path, Encoding.GetEncoding(1200)))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        npcposData.Add(line);
                    }

                }

                server_Npcpos_Custom = new Server_Npcpos(npcposData, true);

            }
        }

        private void LoadSpawnAreas()
        {
            for (int i = 0; i < server_Npcpos_Original.territories.Count; i++)
            {
                L2H_Spawn_Areas.Add(new L2H_Spawn_Area { Territory = server_Npcpos_Original.territories[i] });
            }
            for (int i = 0; i < server_Npcpos_Custom.territories.Count; i++)
            {
                L2H_Spawn_Areas.Add(new L2H_Spawn_Area { Territory = server_Npcpos_Original.territories[i], IsCustom = true });
            }

            Dispatcher.Invoke(() =>
            {
                var newView = new CollectionViewSource() { Source = L2H_Spawn_Areas };
                var listViewCollection2 = (ListCollectionView)newView.View;
                listViewCollection2.Filter = Filter_Spawn_Area;
                Spawns_Zone_Details_Grid_Spawn_Areas_Listview.ItemsSource = listViewCollection2;
                CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_Spawn_Areas_Listview.ItemsSource).Refresh();
                mainWindow.UpdateLog("Spawn Areas Loaded: " + L2H_Spawn_Areas.Count, L2H_Constants.Color_Add);
                mainWindow.UpdateLoadedNumber(LoadedTypes.Spawn_Areas);

            });
        }
        public void LoadHuntingzones()
        {
            client_Huntingzones = new List<Client_Huntingzone>();
            L2H_Huntingzones = new List<L2H_Huntingzone>();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Huntingzone_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Huntingzone newEntry = new Client_Huntingzone(line);
                    if (newEntry.ID != "id")
                    {
                        client_Huntingzones.Add(newEntry);
                    }

                    else
                    {
                        huntingzoneStartLine = line;
                    }
                }
            }

            for (int i = 0; i < client_Huntingzones.Count; i++)
            {
                L2H_Huntingzones.Add(new L2H_Huntingzone(client_Huntingzones[i]));
            }

            Dispatcher.Invoke(() =>
            {
                var newView = new CollectionViewSource() { Source = L2H_Huntingzones };
                var listViewCollection2 = (ListCollectionView)newView.View;
                listViewCollection2.Filter = Filter_Huntingzone;
                Huntingzones_Listview.ItemsSource = listViewCollection2;
                CollectionViewSource.GetDefaultView(Huntingzones_Listview.ItemsSource).Refresh();
                mainWindow.UpdateLog("Hunting Zones Loaded: " + L2H_Huntingzones.Count, L2H_Constants.Color_Add);

            });

        }
        public void LoadZonenames()
        {
            client_Zonenames = new List<Client_Zonename>();
            L2H_Zonenames = new List<L2H_Zonename>();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Zonename_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Zonename newEntry = new Client_Zonename(line);
                    if (newEntry.nbr != "nbr")
                    {
                        client_Zonenames.Add(newEntry);
                    }

                    else
                    {
                        zonenameStartLine = line;
                    }
                }
            }

            for (int i = 0; i < client_Zonenames.Count; i++)
            {
                L2H_Zonenames.Add(new L2H_Zonename(client_Zonenames[i]));
            }

            Dispatcher.Invoke(() =>
            {
                var newView = new CollectionViewSource() { Source = L2H_Zonenames };
                var listViewCollection3 = (ListCollectionView)newView.View;
                listViewCollection3.Filter = Filter_Zonename;
                Zonenames_Listview.ItemsSource = listViewCollection3;
                CollectionViewSource.GetDefaultView(Zonenames_Listview.ItemsSource).Refresh();
                mainWindow.UpdateLog("Zone names Loaded: " + L2H_Zonenames.Count, L2H_Constants.Color_Add);

            });

        }
        public void LoadRaiddata()
        {
            client_Raids = new List<Client_Raid>();
            L2H_Raids = new List<L2H_Raid>();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Raiddata_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Raid newEntry = new Client_Raid(line);
                    if (newEntry.ID != "id")
                    {
                        client_Raids.Add(newEntry);
                    }

                    else
                    {
                        raidsStartLine = line;
                    }
                }
            }

            for (int i = 0; i < client_Raids.Count; i++)
            {
                L2H_Raids.Add(new L2H_Raid(client_Raids[i]) { L2H_NPC = npcsPage.L2H_Npcs.Find(x => x.NPC_ID == client_Raids[i].npc_id) });

            }

            Dispatcher.Invoke(() =>
            {
                var newView = new CollectionViewSource() { Source = L2H_Raids };
                var listViewCollection4 = (ListCollectionView)newView.View;
                listViewCollection4.Filter = Filter_Raid;
                Raids_Listview.ItemsSource = listViewCollection4;
                CollectionViewSource.GetDefaultView(Raids_Listview.ItemsSource).Refresh();
                mainWindow.UpdateLog("Raids Loaded: " + L2H_Raids.Count, L2H_Constants.Color_Add);

            });

        }
        #endregion

        void Update_Preview(string zoneID)
        {
            if (previewSelectionBlock != null)
            {
                if (previewSelectionBlock.ZoneID == zoneID)
                    return;
                else
                {
                    previewSelectionBlock.SetOpacity(0);

                }
            }

            previewSelectionBlock = selectionBlocks.Find(x => x.ZoneID == zoneID);
            previewSelectionBlock.SetOpacity(0.5f);
            Spawns_Zone_Preview_Image.Source = L2H_Parser.GetWorldZoneImage(zoneID);
            Spawns_Zone_Preview_Name.Text = zoneID;

            Spawns_Zone_Preview_Layers_Panel.Children.Clear();
            Spawns_Zone_Preview_Layers_Text.Visibility = Visibility.Hidden;

            for (int i = 0; i < 10; i++)
            {
                BitmapImage newImage = L2H_Parser.GetWorldZoneImage(zoneID + "_" + (i.ToString()), false);
                if (newImage != null)
                {
                    Spawns_Zone_Preview_Layers_Text.Visibility = Visibility.Visible;
                    System.Windows.Controls.Image image = new System.Windows.Controls.Image { Source = newImage };
                    image.MaxHeight = 128;
                    image.MaxWidth = 128;
                    image.Margin = new Thickness(4, 4, 4, 4);

                    Spawns_Zone_Preview_Layers_Panel.Children.Add(image);
                }
            }
        }

        void Update_Spawn_Area_Details_View()
        {
            if (activeSelectionBlock != null)
            {
                Spawns_Zone_Selection_Overview_Grid.Visibility = Visibility.Hidden;
                Spawns_Zone_Details_Grid.Visibility = Visibility.Visible;



                CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_Spawn_Areas_Listview.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_Layers_ListView.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(Huntingzones_Listview.ItemsSource).Refresh();
                CollectionViewSource.GetDefaultView(Zonenames_Listview.ItemsSource).Refresh();


            }
            else
            {
                Spawns_Zone_Selection_Overview_Grid.Visibility = Visibility.Visible;
                Spawns_Zone_Details_Grid.Visibility = Visibility.Hidden;
            }

            if (active_Spawn_NPC_Maker != null)
            {
                if (active_Spawn_NPC_Maker.Npc_Maker != null)
                {
                    Spawns_Zone_Details_Grid_NPC_Maker_Parameters_Panel_Overview.Visibility = Visibility.Visible;
                    Spawns_Zone_Details_Grid_NPC_Maker_Parameters_Panel.Visibility = Visibility.Visible;
                    Spawns_Zone_Details_Grid_NPC_Maker_Ex_Parameters_Panel.Visibility = Visibility.Hidden;
                }
                else
                {
                    Spawns_Zone_Details_Grid_NPC_Maker_Parameters_Panel_Overview.Visibility = Visibility.Visible;
                    Spawns_Zone_Details_Grid_NPC_Maker_Parameters_Panel.Visibility = Visibility.Hidden;
                    Spawns_Zone_Details_Grid_NPC_Maker_Ex_Parameters_Panel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                Spawns_Zone_Details_Grid_NPC_Maker_Parameters_Panel.Visibility = Visibility.Hidden;
                Spawns_Zone_Details_Grid_NPC_Maker_Ex_Parameters_Panel.Visibility = Visibility.Hidden;
            }

            if (active_Spawn_NPC_Maker_NPC_Spawn != null)
            {
                CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_NPC_Maker_NPC_Spawns_Listview.ItemsSource).Refresh();
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Panel.Visibility = Visibility.Visible;
            }
            else
            {
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Panel.Visibility = Visibility.Hidden;
            }

            return;

        }

        void Update_Huntingzone_Details_View()
        {
            if (activeSelectionBlock != null)
            {

                foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Huntingzone_Details_Grid))
                {
                    tb.DataContext = active_L2H_Huntingzone;
                }
                foreach (ToggleButton tb in L2H_Parser.FindVisualChildren<ToggleButton>(Huntingzone_Details_Grid))
                {
                    tb.DataContext = active_L2H_Huntingzone;
                }
                foreach (ComboBox cb in L2H_Parser.FindVisualChildren<ComboBox>(Huntingzone_Details_Grid))
                {
                    cb.DataContext = active_L2H_Huntingzone;
                }


                CollectionViewSource.GetDefaultView(Huntingzones_Listview.ItemsSource).Refresh();

            }
        }

        void Update_Zonename_Details_View()
        {
            if (activeSelectionBlock != null)
            {

                foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Zonename_Details_Grid))
                {
                    tb.DataContext = active_L2H_Zonename;
                }
                foreach (ToggleButton tb in L2H_Parser.FindVisualChildren<ToggleButton>(Zonename_Details_Grid))
                {
                    tb.DataContext = active_L2H_Zonename;
                }
                foreach (ComboBox cb in L2H_Parser.FindVisualChildren<ComboBox>(Zonename_Details_Grid))
                {
                    cb.DataContext = active_L2H_Zonename;
                }


                CollectionViewSource.GetDefaultView(Zonenames_Listview.ItemsSource).Refresh();

            }
        }

        void Update_Raid_Details_View()
        {
            if (activeSelectionBlock != null)
            {
                foreach (Button b in L2H_Parser.FindVisualChildren<Button>(Raid_Details_Grid))
                {
                    b.DataContext = active_L2H_Raid;
                }
                foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(Raid_Details_Grid))
                {
                    tb.DataContext = active_L2H_Raid;
                }
                foreach (ToggleButton tb in L2H_Parser.FindVisualChildren<ToggleButton>(Raid_Details_Grid))
                {
                    tb.DataContext = active_L2H_Raid;
                }
                foreach (ComboBox cb in L2H_Parser.FindVisualChildren<ComboBox>(Raid_Details_Grid))
                {
                    cb.DataContext = active_L2H_Raid;
                }


                CollectionViewSource.GetDefaultView(Raids_Listview.ItemsSource).Refresh();

            }
        }


        public void Update_NPC_Maker_NPC_Spawns_Panel()
        {
            L2H_Spawn_NPC_Maker_NPCs.Clear();

            if (active_Spawn_NPC_Maker == null)
            {
                CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_NPC_Maker_NPC_Spawns_Listview.ItemsSource).Refresh();
                return;
            }


            if (active_Spawn_NPC_Maker.Npc_Maker != null)
            {
                for (int i = 0; i < active_Spawn_NPC_Maker.Npc_Maker.npcs.Count; i++)
                {
                    Npc_Begin target = active_Spawn_NPC_Maker.Npc_Maker.npcs[i];
                    L2H_Spawn_NPC_Maker_NPCs.Add(new L2H_Spawn_NPC_Maker_NPC_Spawn()
                    {
                        L2H_Npc = npcsPage.L2H_Npcs.Find(x => x.server_Npcdata.npcName == target.id),
                        npc_Begin = target,
                        Npc_Maker = active_Spawn_NPC_Maker
                    });
                }
            }
            else
            {
                for (int i = 0; i < active_Spawn_NPC_Maker.Npc_Maker_Ex.npcs.Count; i++)
                {
                    Npc_Ex_Begin target = active_Spawn_NPC_Maker.Npc_Maker_Ex.npcs[i];
                    L2H_Spawn_NPC_Maker_NPCs.Add(new L2H_Spawn_NPC_Maker_NPC_Spawn()
                    {
                        L2H_Npc = npcsPage.L2H_Npcs.Find(x => x.server_Npcdata.npcName == target.id),
                        npc_Ex_Begin = target,
                        Npc_Maker = active_Spawn_NPC_Maker
                    });
                }
            }
            Spawns_Zone_Details_Grid_NPC_Maker_Parameters_Panel_Overview.Visibility = Visibility.Visible;

            if (active_Spawn_NPC_Maker.Npc_Maker != null)
            {
                Spawns_Zone_Details_Grid_NPC_Maker_Parameters_Panel.Visibility = Visibility.Visible;
                Spawns_Zone_Details_Grid_NPC_Maker_Ex_Parameters_Panel.Visibility = Visibility.Hidden;
            }
            else
            {
                Spawns_Zone_Details_Grid_NPC_Maker_Parameters_Panel.Visibility = Visibility.Hidden;
                Spawns_Zone_Details_Grid_NPC_Maker_Ex_Parameters_Panel.Visibility = Visibility.Visible;
            }


            CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_Spawn_NPC_Makers_Listview.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_NPC_Maker_NPC_Spawns_Listview.ItemsSource).Refresh();
        }

        //Filters
        private bool Filter_Spawn_Area(object item)
        {
            L2H_Spawn_Area filteredItem = item as L2H_Spawn_Area;

            if (filteredItem == null)
                return false;

            if (activeSelectionBlock == null)
                return false;


            bool inside_Active_Spawn_Area = false;

            if (filteredItem.Territory.blockCoordinateX.Exists(x => x == activeSelectionBlock.xCoordinate) && filteredItem.Territory.blockCoordinateY.Exists(x => x == activeSelectionBlock.yCoordinate))
                inside_Active_Spawn_Area = true;


            if (inside_Active_Spawn_Area)
                return true;
            else
                return false;

        }
        private bool Filter_Huntingzone(object item)
        {
            L2H_Huntingzone filteredItem = item as L2H_Huntingzone;

            if (filteredItem == null)
                return false;

            if (activeSelectionBlock == null)
                return false;


            bool inside_Active_Spawn_Area = false;



            if (filteredItem.World_Block_X == activeSelectionBlock.xCoordinate && filteredItem.World_Block_Y == activeSelectionBlock.yCoordinate)
                inside_Active_Spawn_Area = true;


            if (inside_Active_Spawn_Area)
                return true;
            else
                return false;

        }
        private bool Filter_Zonename(object item)
        {
            L2H_Zonename filteredItem = item as L2H_Zonename;

            if (filteredItem == null)
                return false;

            if (activeSelectionBlock == null)
                return false;


            bool inside_Active_Spawn_Area = false;

            if (filteredItem.X_World_Grid == activeSelectionBlock.xCoordinate.ToString() && filteredItem.Y_World_Grid == activeSelectionBlock.yCoordinate.ToString())
                inside_Active_Spawn_Area = true;


            if (inside_Active_Spawn_Area)
                return true;
            else
                return false;

        }

        private bool Filter_Raid(object item)
        {
            L2H_Raid filteredItem = item as L2H_Raid;

            if (filteredItem == null)
                return false;

            if (activeSelectionBlock == null)
                return false;


            bool inside_Active_Spawn_Area = false;

            if (filteredItem.World_Block_X == activeSelectionBlock.xCoordinate && filteredItem.World_Block_Y == activeSelectionBlock.yCoordinate)
                inside_Active_Spawn_Area = true;


            if (inside_Active_Spawn_Area)
                return true;
            else
                return false;
        }

        //Triggers
        //Mouse Over
        void Spawn_Selection_Mouse_Over(object sender, MouseEventArgs e)
        {
            Update_Preview((sender as Button).Tag.ToString());

        }
        private void Spawn_Area_Mouse_Over(object sender, MouseEventArgs e)
        {
            L2H_Spawn_Area spawnArea = (sender as FrameworkElement).DataContext as L2H_Spawn_Area;


            if (spawnArea == null)
            {
                Draw_Polygon_On_Map();
            }
            else
            {
                previewTerritory = spawnArea.Territory;
                Draw_Polygon_On_Map(previewTerritory);
            }

        }
        private void Spawn_Area_Mouse_Leave(object sender, MouseEventArgs e)
        {
            Spawn_Area_Preview_Polygon_Points = null;
            Spawn_Area_Preview_Polygon_Arrow_Points = null;
            Spawns_Zone_Details_Grid_Image_Polygon_Preview.Points = new PointCollection();
            Spawns_Zone_Details_Grid_Image_Polygon_Preview_Arrow.Points = new PointCollection();
        }


        private void Spawn_NPC_Mouse_Over(object sender, MouseEventArgs e)
        {

        }

        private void Spawns_Zone_Shortcut_Mouse_Over(object sender, MouseEventArgs e)
        {
            L2H_Spawn_Shortcut obj = (sender as FrameworkElement).DataContext as L2H_Spawn_Shortcut;

            if (obj != null)
            {
                Update_Preview(obj.Zone);
            }

        }
        //Clicks
        private void Spawn_Area_Clicked(object sender, RoutedEventArgs e)
        {
            if (active_Spawn_Area != (sender as FrameworkElement).DataContext as L2H_Spawn_Area)
            {
                if (active_Spawn_Area != null)
                    active_Spawn_Area.IsSelected = false;

                active_Spawn_Area = (sender as FrameworkElement).DataContext as L2H_Spawn_Area;
                active_Spawn_Area.IsSelected = true;

                Spawn_Area_Selected_Polygon_Points = Spawn_Area_Preview_Polygon_Points;
                Spawn_Area_Selected_Polygon_Arrow_Points = Spawn_Area_Preview_Polygon_Arrow_Points;
                Spawns_Zone_Details_Grid_Image_Polygon_Selected.Points = Spawn_Area_Selected_Polygon_Points;
                Spawns_Zone_Details_Grid_Image_Polygon_Selected_Arrow.Points = Spawn_Area_Selected_Polygon_Arrow_Points;
                Spawn_Area_Preview_Polygon_Points = null;
                Spawn_Area_Preview_Polygon_Arrow_Points = null;
                Spawns_Zone_Details_Grid_Image_Polygon_Preview.Points = new PointCollection();

                L2H_Spawn_NPC_Makers = active_Spawn_Area.NPC_Makers;

                Spawns_Zone_Details_Grid_Spawn_NPC_Makers_Listview.ItemsSource = L2H_Spawn_NPC_Makers;
                Spawns_Zone_Details_Grid_NPC_Maker_NPC_Spawns_Listview.ItemsSource = L2H_Spawn_NPC_Maker_NPCs;

                active_Spawn_NPC_Maker_NPC_Spawn = null;

                if (active_Spawn_NPC_Maker != null)
                {
                    active_Spawn_NPC_Maker.IsSelected = false;
                    active_Spawn_NPC_Maker = null;
                }

                if (active_Spawn_Area.NPC_Makers.Count > 0)
                {
                    active_Spawn_NPC_Maker = L2H_Spawn_NPC_Makers[0];
                    active_Spawn_NPC_Maker.IsSelected = true;
                    NPC_Maker_Parameters_Bind();
                }


                Update_NPC_Maker_NPC_Spawns_Panel();
                Update_Spawn_Area_Details_View();


                CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_Spawn_NPC_Makers_Listview.ItemsSource).Refresh();

            }
        }


        private void Spawns_Zone_Shortcut_Clicked(object sender, RoutedEventArgs e)
        {
            activeSelectionBlock = ((sender as FrameworkElement).DataContext as L2H_Spawn_Shortcut).L2H_Spawn_Selection_Block;
            Spawns_Zone_Details_Grid_Image.Source = L2H_Parser.GetWorldZoneImage(activeSelectionBlock.ZoneID);
            Spawns_Zone_Details_Grid_Layers_ListView.ItemsSource = activeSelectionBlock.Layers;
            Update_Spawn_Area_Details_View();

        }
        private void Spawn_Selection_Block_Clicked(object sender, RoutedEventArgs e)
        {
            activeSelectionBlock = (sender as FrameworkElement).DataContext as L2H_Spawn_Selection_Block;
            Spawns_Zone_Details_Grid_Image.Source = L2H_Parser.GetWorldZoneImage(activeSelectionBlock.ZoneID);
            Spawns_Zone_Details_Grid_Layers_ListView.ItemsSource = activeSelectionBlock.Layers;
            Update_Spawn_Area_Details_View();

        }
        private void Spawns_Zone_Details_Grid_Back_Clicked(object sender, RoutedEventArgs e)
        {
            activeSelectionBlock = null;

            if (active_Spawn_NPC_Maker_NPC_Spawn != null)
            {
                active_Spawn_NPC_Maker_NPC_Spawn.IsSelected = false;
                active_Spawn_NPC_Maker_NPC_Spawn = null;
            }
            if (active_Spawn_NPC_Maker != null)
            {
                active_Spawn_NPC_Maker.IsSelected = false;
                active_Spawn_NPC_Maker = null;
            }
            if (active_Spawn_Area != null)
            {
                active_Spawn_Area.IsSelected = false;
                active_Spawn_Area = null;
            }

            Spawns_Zone_Details_Grid_Spawn_NPC_Makers_Listview.ItemsSource = null;
            Spawns_Zone_Details_Grid_NPC_Maker_NPC_Spawns_Listview.ItemsSource = null;

            Spawn_Area_Preview_Polygon_Points = null;
            Spawn_Area_Preview_Polygon_Arrow_Points = null;
            Spawns_Zone_Details_Grid_Image_Polygon_Preview.Points = new PointCollection();
            Spawn_Area_Selected_Polygon_Points = null;
            Spawns_Zone_Details_Grid_Image_Polygon_Selected.Points = new PointCollection();
            Spawn_Area_Selected_Polygon_Arrow_Points = null;
            Spawns_Zone_Details_Grid_Image_Polygon_Selected_Arrow.Points = new PointCollection();


            Update_Spawn_Area_Details_View();
        }
        private void Spawn_Area_Layer_Clicked(object sender, RoutedEventArgs e)
        {
            Spawns_Zone_Details_Grid_Image.Source = ((sender as FrameworkElement).DataContext as L2H_Spawn_Selection_Block_Layer).Image;
        }

        private void Spawn_NPC_Maker_Clicked(object sender, RoutedEventArgs e)
        {
            if (active_Spawn_NPC_Maker != null)
            {
                if (active_Spawn_NPC_Maker == (sender as FrameworkElement).DataContext as L2H_Spawn_NPC_Maker)
                {
                    L2H_Spawn_NPC_Makers.Find(x => x == active_Spawn_NPC_Maker).IsSelected = true;
                    (sender as System.Windows.Controls.Primitives.ToggleButton).IsChecked = true;
                    active_Spawn_NPC_Maker.IsSelected = true;
                    return;
                }
                else
                {
                    active_Spawn_NPC_Maker.IsSelected = false;
                    active_Spawn_NPC_Maker = (sender as FrameworkElement).DataContext as L2H_Spawn_NPC_Maker;
                    active_Spawn_NPC_Maker.IsSelected = true;
                }
            }
            else
            {
                active_Spawn_NPC_Maker = (sender as FrameworkElement).DataContext as L2H_Spawn_NPC_Maker;
                active_Spawn_NPC_Maker.IsSelected = true;
            }

            active_Spawn_NPC_Maker_NPC_Spawn = null;

            NPC_Maker_Parameters_Bind();
            Update_NPC_Maker_NPC_Spawns_Panel();

            Update_Spawn_Area_Details_View();

            CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_Spawn_NPC_Makers_Listview.ItemsSource).Refresh();
        }
        private void Spawns_Zone_Details_Grid_NPC_Maker_NPC_Spawn_Clicked(object sender, RoutedEventArgs e)
        {
            L2H_Spawn_NPC_Maker_NPC_Spawn obj = ((sender as FrameworkElement).DataContext) as L2H_Spawn_NPC_Maker_NPC_Spawn;
            if (obj != null)
            {
                if (active_Spawn_NPC_Maker_NPC_Spawn != null)
                {
                    if (active_Spawn_NPC_Maker_NPC_Spawn == obj)
                        return;
                    else
                    {
                        active_Spawn_NPC_Maker_NPC_Spawn.IsSelected = false;
                        active_Spawn_NPC_Maker_NPC_Spawn = obj;
                        active_Spawn_NPC_Maker_NPC_Spawn.IsSelected = true;
                        NPC_Spawn_Parameters_Bind();
                    }
                }
                else
                {
                    active_Spawn_NPC_Maker_NPC_Spawn = obj;
                    active_Spawn_NPC_Maker_NPC_Spawn.IsSelected = true;
                    NPC_Spawn_Parameters_Bind();
                }
            }
            else
                return;

            Update_Spawn_Area_Details_View();
        }

        private void Spawn_NPC_Maker_NPC_Spawn_Add_Clicked(object sender, RoutedEventArgs e)
        {
            if (active_Spawn_NPC_Maker == null)
            {
                MessageBox.Show("Select an NPC Maker first");
                return;
            }

            Popup_NPC_Selection NPC_Selection_Popup = new Popup_NPC_Selection((mainWindow.GetPageOfType(typeof(Pages.NPCsPage)) as Pages.NPCsPage).L2H_Npcs);

            NPC_Selection_Popup.Initialize_For_Spawns(active_Spawn_NPC_Maker);

        }

        private void Spawn_NPC_Maker_NPC_Spawn_Remove_Clicked(object sender, RoutedEventArgs e)
        {
            if (active_Spawn_NPC_Maker == null)
            {
                MessageBox.Show("Select an NPC Maker first");
                return;
            }

            if (active_Spawn_NPC_Maker_NPC_Spawn == null)
            {
                MessageBox.Show("Select an NPC first");
                return;
            }



            if (active_Spawn_NPC_Maker.Npc_Maker != null)
            {
                active_Spawn_NPC_Maker.Npc_Maker.npcs.Remove(active_Spawn_NPC_Maker_NPC_Spawn.npc_Begin);
            }
            else
            {
                active_Spawn_NPC_Maker.Npc_Maker_Ex.npcs.Remove(active_Spawn_NPC_Maker_NPC_Spawn.npc_Ex_Begin);
            }

            L2H_Spawn_NPC_Maker_NPCs.Remove(active_Spawn_NPC_Maker_NPC_Spawn);

            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Panel.Visibility = Visibility.Hidden;

            CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_NPC_Maker_NPC_Spawns_Listview.ItemsSource).Refresh();

        }


        private void Spawn_NPC_Maker_Add_Basic_Clicked(object sender, RoutedEventArgs e)
        {
            if (active_Spawn_Area == null)
            {
                MessageBox.Show("Select Spawn Area First");
                return;
            }
            if (active_Spawn_NPC_Maker != null)
                if (active_Spawn_NPC_Maker.Npc_Maker_Ex != null)
                {
                    MessageBox.Show("Target Spawn Area has one or more Extended NPC Makers assigned to it.\n\nA Spawn Area cannot have both Basic and Extended NPC Makers assigned.\n\nRemove the existing Extended NPC Maker(s) or clone a Spawn Area to assign a Basic NPC Maker to it.");
                    return;
                }

            L2H_Spawn_NPC_Maker newMaker = new L2H_Spawn_NPC_Maker()
            {
                Npc_Maker = new Npc_Maker()
                {
                    initialSpawn = "all",
                    maximum_npc = "10",
                    targetTerritory = active_Spawn_Area.Territory,
                    npcs = new List<Npc_Begin>(),
                    territoryIds = new List<string>()
                }
            };

            newMaker.Npc_Maker.territoryIds.Add(active_Spawn_Area.Territory.id);

            active_Spawn_Area.NPC_Makers.Add(newMaker);

            L2H_Log.Instance.Log_Spawn_NPC_Maker_Add(active_Spawn_Area, newMaker);

            CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_Spawn_NPC_Makers_Listview.ItemsSource).Refresh();
            Update_Spawn_Area_Details_View();



        }
        private void Spawn_NPC_Maker_Add_Extended_Clicked(object sender, RoutedEventArgs e)
        {
            if (active_Spawn_Area == null)
            {
                MessageBox.Show("Select Spawn Area First");
                return;
            }
            if (active_Spawn_NPC_Maker != null)
                if (active_Spawn_NPC_Maker.Npc_Maker != null)
                {
                    MessageBox.Show("Target Spawn Area has one or more Basic NPC Makers assigned to it.\n\nA Spawn Area cannot have both Basic and Extended NPC Makers assigned.\n\nRemove the existing Basic NPC Maker(s) or clone a Spawn Area to assign an Extended NPC Maker to it.");
                    return;
                }


            L2H_Spawn_NPC_Maker newMaker = new L2H_Spawn_NPC_Maker()
            {
                Npc_Maker_Ex = new Npc_Maker_Ex()
                {
                    name = Get_Unique_NPC_Maker_Name(active_Spawn_Area, 0),
                    ai = "default_maker",
                    maximum_npc = "10",
                    territoryIds = new List<string>(),
                    npcs = new List<Npc_Ex_Begin>()

                }
            };

            newMaker.Npc_Maker_Ex.territoryIds.Add(active_Spawn_Area.Territory.id);

            active_Spawn_Area.NPC_Makers.Add(newMaker);

            L2H_Log.Instance.Log_Spawn_NPC_Maker_Add(active_Spawn_Area, newMaker);


            CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_Spawn_NPC_Makers_Listview.ItemsSource).Refresh();
        }

        private void Spawn_NPC_Maker_Remove_Clicked(object sender, RoutedEventArgs e)
        {
            if (active_Spawn_NPC_Maker == null)
            {
                MessageBox.Show("No NPC Maker selected");
                return;
            }

            L2H_Log.Instance.Log_Spawn_NPC_Maker_Delete(active_Spawn_Area, active_Spawn_NPC_Maker);

            active_Spawn_NPC_Maker.IsSelected = false;
            active_Spawn_Area.NPC_Makers.Remove(active_Spawn_NPC_Maker);
            active_Spawn_NPC_Maker = null;
            L2H_Spawn_NPC_Maker_NPCs.Clear();
            Update_Spawn_Area_Details_View();
            Spawns_Zone_Details_Grid_NPC_Maker_Parameters_Panel_Overview.Visibility = Visibility.Hidden;
            CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_Spawn_NPC_Makers_Listview.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_NPC_Maker_NPC_Spawns_Listview.ItemsSource).Refresh();
        }

        private void Spawn_Area_Clone_Clicked(object sender, RoutedEventArgs e)
        {
            if (active_Spawn_Area == null)
            {
                MessageBox.Show("Select a Spawn Area first");
                return;
            }

            SpawnTerritory newSpawnTerritory = ObjectExtensions.Copy(active_Spawn_Area.Territory);
            newSpawnTerritory.id = Get_Unique_Custom_Spawn_Area_ID("custom_zone");
            newSpawnTerritory.npc_ex_makers_in_terrain.Clear();
            newSpawnTerritory.npc_makers_in_terrain.Clear();
            server_Npcpos_Custom.territories.Add(newSpawnTerritory);

            L2H_Log.Instance.Log_Spawn_Area_Clone(active_Spawn_Area.Area_Name, newSpawnTerritory.id);


            L2H_Spawn_Areas.Add(new L2H_Spawn_Area() { Territory = newSpawnTerritory, IsCustom = true });
            CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_Spawn_Areas_Listview.ItemsSource).Refresh();
        }

        private void Spawn_Area_Delete_Clicked(object sender, RoutedEventArgs e)
        {
            if (active_Spawn_Area == null)
            {
                MessageBox.Show("Select a Spawn Area first");
                return;
            }

            if (active_Spawn_Area.IsCustom)
            {
                L2H_Log.Instance.Log_Spawn_Area_Delete(active_Spawn_Area.Area_Name);
                L2H_Spawn_Areas.Remove(active_Spawn_Area);
                CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_Spawn_Areas_Listview.ItemsSource).Refresh();
            }
            else
            {
                MessageBox.Show("Original spawn areas cannot be removed.\n\nThey can be edited freely, so you can remove NPC spawns if you need to.\n\nIf you want to disable all original spawns, use the 'Custom Areas Only' setting in the Export options.");
                return;
            }
        }

        private void NPC_Parameter_Fixed_Positions_Add_Clicked(object sender, RoutedEventArgs e)
        {
            List<L2H_Spawn_NPC_Maker_NPC_Spawn_Fixed_Position> Fixed_Positions = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions;

            Fixed_Positions.Add(new L2H_Spawn_NPC_Maker_NPC_Spawn_Fixed_Position()
            {
                ID = Fixed_Positions.Count,
                fixed_Position_Chance = "100",
                fixed_Position_X = active_Spawn_Area.Territory.vertices[0].xPos,
                fixed_Position_Y = active_Spawn_Area.Territory.vertices[0].yPos,
                fixed_Position_Z = active_Spawn_Area.Territory.vertices[0].zPos,
                fixed_Position_Yaw = "0",
                parent = active_Spawn_NPC_Maker_NPC_Spawn
            });

            active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions = Fixed_Positions;

            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Positions_Dropdown.SelectedIndex = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions.Count - 1;

            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_X.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions.Count - 1];
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Y.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions.Count - 1];
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Z.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions.Count - 1];
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Rotation.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions.Count - 1];
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Probability.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions.Count - 1];

            L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(active_Spawn_NPC_Maker_NPC_Spawn, "Fixed Positions", (active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions.Count - 1).ToString(), active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions.Count.ToString());

            CollectionViewSource.GetDefaultView(Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Positions_Dropdown.ItemsSource).Refresh();
        }


        private void Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Positions_Dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (active_Spawn_NPC_Maker_NPC_Spawn == null)
                return;
            if (active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions.Count == 0)
                return;


            if (Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Positions_Dropdown.SelectedIndex == -1)
                active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Position_Index = 0;
            else
                active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Position_Index = Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Positions_Dropdown.SelectedIndex;
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_X.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Position_Index];
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Y.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Position_Index];
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Z.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Position_Index];
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Rotation.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Position_Index];
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Probability.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Position_Index];


        }

        //Input
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
        private void Validate_Clientfile_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Clientfile_Entry(e.Text);
        }
        private void Validate_Lower_Case_And_Symbols_Input(object sender, TextCompositionEventArgs e)
        {
            e.Handled = L2H_Textbox_Input_Restrictions.Is_Valid_Lower_Case_And_Symbols(e.Text);
        }

        private void TextBox_No_Spaces_Allowed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;

            L2H_Textbox_Input_Restrictions.Check_If_Dot_Exists_In_Float_TextBox(sender, e);
        }


        //Misc functions
        void NPC_Spawn_Parameters_Bind()
        {
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_NPC_Name.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Max_Spawns.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Respawn_Time.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Respawn_Deviance.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;           
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Positions_Dropdown.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;
            Respawn_Time_Type_Dropdown.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;
            Respawn_Deviance_Type_Dropdown.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Toggle.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;


            if (active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions.Count > 0)
            {
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Positions_Dropdown.SelectedIndex = 0;

                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_X.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Positions_Dropdown.SelectedIndex];
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Y.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Positions_Dropdown.SelectedIndex];
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Z.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Positions_Dropdown.SelectedIndex];
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Rotation.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Positions_Dropdown.SelectedIndex];
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Probability.DataContext = active_Spawn_NPC_Maker_NPC_Spawn.Fixed_Positions[Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Positions_Dropdown.SelectedIndex];
            }
            else
            {
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_X.DataContext = null;
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Y.DataContext = null;
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Z.DataContext = null;
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Rotation.DataContext = null;
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Fixed_Position_Probability.DataContext = null;
            }

            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_DB_Name.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_DB_Save.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Boss_Respawn.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Privates.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Nickname.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Maker_AI.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;
            Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Maker_AI_Parameters.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;

            if (active_Spawn_NPC_Maker_NPC_Spawn.npc_Ex_Begin != null)
            {
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Chase_Player_Label.Visibility = Visibility.Visible;
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Chase_Player.Visibility = Visibility.Visible;
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Chase_Player.DataContext = active_Spawn_NPC_Maker_NPC_Spawn;
            }
            else
            {
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Chase_Player_Label.Visibility = Visibility.Hidden;
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Chase_Player.Visibility = Visibility.Hidden;
                Spawns_Zone_Details_Grid_Spawn_NPC_Parameter_Chase_Player.DataContext = null;
            }
        }

        void NPC_Maker_Parameters_Bind()
        {
            if (active_Spawn_NPC_Maker.Npc_Maker != null)
            {
                Spawns_Zone_Details_Grid_NPC_Maker_Initial_Spawn.DataContext = active_Spawn_NPC_Maker;
                Spawns_Zone_Details_Grid_NPC_Maker_Max_Spawns.DataContext = active_Spawn_NPC_Maker;
                Spawns_Zone_Details_Grid_NPC_Maker_Spawn_Time.DataContext = active_Spawn_NPC_Maker;
                Spawns_Zone_Details_Grid_NPC_Maker_Type.DataContext = active_Spawn_NPC_Maker;
                Spawns_Zone_Details_Grid_NPC_Maker_Event_Name.DataContext = active_Spawn_NPC_Maker;
            }
            else
            {
                Spawns_Zone_Details_Grid_Spawn_Area_Parameter_Name.DataContext = active_Spawn_NPC_Maker;
                Spawns_Zone_Details_Grid_Spawn_Area_Parameter_Max_Spawns.DataContext = active_Spawn_NPC_Maker;
                Spawns_Zone_Details_Grid_Spawn_Area_Parameter_AI.DataContext = active_Spawn_NPC_Maker;
                Spawns_Zone_Details_Grid_Spawn_Area_Parameter_AI_Parameters.DataContext = active_Spawn_NPC_Maker;
                Spawns_Zone_Details_Grid_Spawn_Area_Parameter_NPC_Maker_Parameter_Flying.DataContext = active_Spawn_NPC_Maker;
                Spawns_Zone_Details_Grid_Spawn_Area_Parameter_Banned_Territory.DataContext = active_Spawn_NPC_Maker;
                Spawns_Zone_Details_Grid_NPC_Maker_Type.DataContext = active_Spawn_NPC_Maker;
            }
        }

        //Exports
        private void ExportOriginalNpcPos(bool firstRun)
        {

            if (firstRun)
            {
                if (!File.Exists(L2H_Constants.original_npcpos_path))
                {
                    File.Create(L2H_Constants.original_npcpos_path).Dispose();
                }
                else
                {
                    File.WriteAllText(L2H_Constants.original_npcpos_path, String.Empty, Encoding.GetEncoding(1200));
                }

                File.WriteAllLines(L2H_Constants.original_npcpos_path, server_Npcpos_Original.GetFullExport(), Encoding.GetEncoding(1200));

            }
            else
            {
                List<string> originalStrings = server_Npcpos_Original.GetFullExport();
                List<string> customStrings = server_Npcpos_Custom.GetFullExport();
                List<string> fullStrings = (originalStrings.Concat(customStrings).ToList());

                File.WriteAllText(L2H_Constants.server_NpcposPath, String.Empty);
                File.WriteAllText(L2H_Constants.original_npcpos_path, String.Empty);
                File.WriteAllText(L2H_Constants.custom_npcpos_path, String.Empty);

                File.WriteAllLines(L2H_Constants.server_NpcposPath, fullStrings, Encoding.GetEncoding(1200));
                File.WriteAllLines(L2H_Constants.original_npcpos_path, originalStrings, Encoding.GetEncoding(1200));
                File.WriteAllLines(L2H_Constants.custom_npcpos_path, customStrings, Encoding.GetEncoding(1200));
            }

        }

        void Draw_Polygon_On_Map(SpawnTerritory targetTerritory = null, bool refresh_selected = false)
        {

            int mapHeight = (int)Spawns_Zone_Details_Grid_Image.ActualHeight;//SpawnMap.Bounds.Height;
            int mapWidth = (int)Spawns_Zone_Details_Grid_Image.ActualWidth;//SpawnMap.Bounds.Width;
            float offsetX = 0;
            float offsetY = 0;

            if (mapWidth > mapHeight)
            {
                offsetX = (mapWidth - mapHeight) / 2f;
            }
            else if (mapWidth < mapHeight)
            {
                offsetY = (mapHeight - mapWidth) / 2f;
            }

            List<Vector2> polygon = new List<Vector2>();

            if (activeZoneTypeToggle == Zone_Type_Toggle_Spawn_Areas)
            {
                polygon = previewTerritory.GrabPolygon(mapWidth - (offsetX * 2), mapHeight - (offsetY * 2)); //This gives 0,0
                                                                                                             //else if (activeZoneTypeToggle == Zone_Type_Toggle_Huntingzones)
                                                                                                             //make a point instead of a polygon
                                                                                                             //    else
                                                                                                             //make a polygon based off the coordinates in zone name
            }
            else if (activeZoneTypeToggle == Zone_Type_Toggle_Huntingzones)
            {
                polygon = preview_L2H_Huntingzone.GrabPolygon(mapWidth - (offsetX * 2), mapHeight - (offsetY * 2));
            }
            else if (activeZoneTypeToggle == Zone_Type_Toggle_Raids)
            {
                polygon = preview_L2H_Raid.GrabPolygon(mapWidth - (offsetX * 2), mapHeight - (offsetY * 2));
            }
            else
            {
                //Last option is zonename, that one cannot draw a polygon because it's based on the colormap in the l2zonename.utx file. Why? Because of reasons.
            }

            PointCollection points = new PointCollection(polygon.Count);

            float minXValue = 0;
            float maxXValue = 0;
            float minYValue = 0;
            float maxYValue = 0;

            for (int i = 0; i < polygon.Count; i++)
            {
                points.Add(new Point(polygon[i].xPos + offsetX, polygon[i].yPos + offsetY));
                if (minXValue == 0)
                {
                    minXValue = polygon[i].xPos;
                    maxXValue = polygon[i].xPos;
                }
                if (minYValue == 0)
                {
                    minYValue = polygon[i].yPos;
                    maxYValue = polygon[i].yPos;
                }

                if (polygon[i].xPos < minXValue)
                    minXValue = polygon[i].xPos;
                if (polygon[i].xPos > maxXValue)
                    maxXValue = polygon[i].xPos;

                if (polygon[i].yPos < minYValue)
                    minYValue = polygon[i].yPos;
                if (polygon[i].yPos > maxYValue)
                    maxYValue = polygon[i].yPos;
            }

            if (refresh_selected)
            {
                List<Vector2> vector2s = targetTerritory.GrabPolygon(mapWidth, mapHeight);
                PointCollection redrawPoints = new PointCollection();

                for (int i = 0; i < vector2s.Count; i++)
                {
                    redrawPoints.Add(new Point(vector2s[i].xPos, vector2s[i].yPos));
                }

                if (activeZoneTypeToggle == Zone_Type_Toggle_Spawn_Areas)
                {
                    Spawn_Area_Selected_Polygon_Points = redrawPoints;
                    Spawns_Zone_Details_Grid_Image_Polygon_Selected.Points = Spawn_Area_Selected_Polygon_Points;
                }
                else if (activeZoneTypeToggle == Zone_Type_Toggle_Huntingzones)
                {
                    Huntingzone_Selected_Polygon_Points = redrawPoints;
                    Huntingzone_Details_Grid_Image_Polygon_Selected.Points = Huntingzone_Selected_Polygon_Points;
                }
                else if (activeZoneTypeToggle == Zone_Type_Toggle_Zonenames)
                {
                    Zonename_Selected_Polygon_Points = redrawPoints;
                    Zonename_Details_Grid_Image_Polygon_Selected.Points = Zonename_Selected_Polygon_Points;
                }
                else
                {
                    Raid_Selected_Polygon_Points = redrawPoints;
                    Raid_Details_Grid_Image_Polygon_Selected.Points = Raid_Selected_Polygon_Points;
                }
                
                Draw_Arrows(redrawPoints);

            }
            else
            {
                Draw_Arrows(points, true);
                
            }
        }

        void Draw_Arrows(PointCollection points, bool preview = false)
        {
            PointCollection polygon_Arrow_Points;

            if (activeZoneTypeToggle == Zone_Type_Toggle_Spawn_Areas)
            {
                Spawn_Area_Preview_Polygon_Points = points;
            }
            else if (activeZoneTypeToggle == Zone_Type_Toggle_Huntingzones)
            {
                Huntingzone_Preview_Polygon_Points = points;
            }
            else if (activeZoneTypeToggle == Zone_Type_Toggle_Zonenames)
            {
                Zonename_Preview_Polygon_Points = points;
            }
            else
            {
                Raid_Preview_Polygon_Points = points;
            }

            double minX = 2000;
            double minY = 2000;
            double offset = 22;

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X < minX)
                {
                    minX = points[i].X;
                    minY = points[i].Y;
                }
            }


            polygon_Arrow_Points = new PointCollection();
            polygon_Arrow_Points.Add(new Point(minX - offset + 0, minY - (offset / 2) + 5));
            polygon_Arrow_Points.Add(new Point(minX - offset + 10, minY - (offset / 2) + 5));
            polygon_Arrow_Points.Add(new Point(minX - offset + 10, minY - (offset / 2) + 0));
            polygon_Arrow_Points.Add(new Point(minX - offset + 12, minY - (offset / 2) + 0));
            polygon_Arrow_Points.Add(new Point(minX - offset + 20, minY - (offset / 2) + 9));
            polygon_Arrow_Points.Add(new Point(minX - offset + 20, minY - (offset / 2) + 11));
            polygon_Arrow_Points.Add(new Point(minX - offset + 12, minY - (offset / 2) + 20));
            polygon_Arrow_Points.Add(new Point(minX - offset + 10, minY - (offset / 2) + 20));
            polygon_Arrow_Points.Add(new Point(minX - offset + 10, minY - (offset / 2) + 15));
            polygon_Arrow_Points.Add(new Point(minX - offset + 0, minY - (offset / 2) + 15));

            if (preview)
            {
                if (activeZoneTypeToggle == Zone_Type_Toggle_Spawn_Areas)
                {
                    Spawn_Area_Preview_Polygon_Arrow_Points = polygon_Arrow_Points;
                    Spawns_Zone_Details_Grid_Image_Polygon_Preview_Arrow.Points = polygon_Arrow_Points;
                    Spawns_Zone_Details_Grid_Image_Polygon_Preview.Points = Spawn_Area_Preview_Polygon_Points;
                }
                else if (activeZoneTypeToggle == Zone_Type_Toggle_Huntingzones)
                {
                    Huntingzone_Preview_Polygon_Arrow_Points = polygon_Arrow_Points;
                    Huntingzone_Details_Grid_Image_Polygon_Preview_Arrow.Points = polygon_Arrow_Points;
                    Huntingzone_Details_Grid_Image_Polygon_Preview.Points = Huntingzone_Preview_Polygon_Points;
                }
                else if (activeZoneTypeToggle == Zone_Type_Toggle_Zonenames)
                {
                    Zonename_Preview_Polygon_Arrow_Points = polygon_Arrow_Points;
                    Zonename_Details_Grid_Image_Polygon_Preview_Arrow.Points = polygon_Arrow_Points;
                    Zonename_Details_Grid_Image_Polygon_Preview.Points = Zonename_Preview_Polygon_Points;
                }
                else
                {
                    Raid_Preview_Polygon_Arrow_Points = polygon_Arrow_Points;
                    Raid_Details_Grid_Image_Polygon_Preview_Arrow.Points = polygon_Arrow_Points;
                    Raid_Details_Grid_Image_Polygon_Preview.Points = Raid_Preview_Polygon_Points;
                }
            }
            else
            {
                if (activeZoneTypeToggle == Zone_Type_Toggle_Spawn_Areas)
                {
                    Spawn_Area_Selected_Polygon_Arrow_Points = polygon_Arrow_Points;
                    Spawns_Zone_Details_Grid_Image_Polygon_Selected_Arrow.Points = polygon_Arrow_Points;
                    Spawns_Zone_Details_Grid_Image_Polygon_Selected.Points = Spawn_Area_Selected_Polygon_Points;
                }
                else if (activeZoneTypeToggle == Zone_Type_Toggle_Huntingzones)
                {
                    Huntingzone_Selected_Polygon_Arrow_Points = polygon_Arrow_Points;
                    Huntingzone_Details_Grid_Image_Selected_Arrow.Points = polygon_Arrow_Points;
                    Huntingzone_Details_Grid_Image_Polygon_Selected.Points = Huntingzone_Selected_Polygon_Points;
                }
                else if (activeZoneTypeToggle == Zone_Type_Toggle_Zonenames)
                {
                    Zonename_Selected_Polygon_Arrow_Points = polygon_Arrow_Points;
                    Zonename_Details_Grid_Image_Selected_Arrow.Points = polygon_Arrow_Points;
                    Zonename_Details_Grid_Image_Polygon_Selected.Points = Zonename_Selected_Polygon_Points;
                }
                else
                {
                    Raid_Selected_Polygon_Arrow_Points = polygon_Arrow_Points;
                    Raid_Details_Grid_Image_Selected_Arrow.Points = polygon_Arrow_Points;
                    Raid_Details_Grid_Image_Polygon_Selected.Points = Raid_Selected_Polygon_Points;
                }
            }
        }



        private void Spawns_Zone_Details_Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (active_Spawn_Area != null)
            {
                Draw_Polygon_On_Map(active_Spawn_Area.Territory, true);
            }
        }

        private string Get_Unique_NPC_Maker_Name(L2H_Spawn_Area spawn_Area, int number)
        {

            string returnString = spawn_Area.Territory.id + "_custom_" + number.ToString();
            if (!spawn_Area.NPC_Makers.Exists(x => x.Npc_Maker_Ex.name == returnString))
                return returnString;
            else
                return Get_Unique_NPC_Maker_Name(spawn_Area, number + 1);

        }
        private string Get_Unique_Custom_Spawn_Area_ID(string currentID, int newNumber = 0)
        {
            string returnString = "";

            bool npcPosExists = server_Npcpos_Custom.territories.Exists(x => x.id == currentID);

            if (npcPosExists)
            {
                bool numberedCustomNpcPosExists = server_Npcpos_Custom.territories.Exists(x => x.id == currentID + "_" + newNumber);

                if (numberedCustomNpcPosExists)
                    returnString = Get_Unique_Custom_Spawn_Area_ID(currentID, newNumber + 1);
                else
                    returnString = currentID + "_" + newNumber;
            }
            else
                returnString = currentID;

            return returnString;
        }

        private void Zone_Type_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            ToggleButton tb = sender as ToggleButton;

            if (activeZoneTypeToggle != tb)
                activeZoneTypeToggle.IsChecked = false;

            activeZoneTypeToggle = tb;
            activeZoneTypeToggle.IsChecked = true;

            Refresh_Zone_Type_Selection();

        }
        public void Refresh_Zone_Type_Selection()
        {
            switch (activeZoneTypeToggle.Tag)
            {
                case "spawn":
                    Top_Right_Title.Text = "NPC Maker Spawns";
                    Spawns_Zone_Canvas.Visibility = Visibility.Visible;
                    Spawn_Maker_Spawns_Grid.Visibility = Visibility.Visible;
                    Spawn_Makers_Grid.Visibility = Visibility.Visible;
                    Spawn_Parameters_Grid.Visibility = Visibility.Visible;
                    Spawn_Areas_Grid.Visibility = Visibility.Visible;

                    Huntingzone_Canvas.Visibility = Visibility.Hidden;
                    Huntingzone_Grid.Visibility = Visibility.Hidden;
                    Huntingzone_Parameters_Grid.Visibility = Visibility.Hidden;

                    Zonename_Canvas.Visibility = Visibility.Hidden;
                    Zonename_Grid.Visibility = Visibility.Hidden;
                    Zonename_Parameters_Grid.Visibility = Visibility.Hidden;

                    Raid_Parameters_Grid.Visibility = Visibility.Hidden;
                    Raid_Canvas.Visibility = Visibility.Hidden;
                    Raid_Grid.Visibility = Visibility.Hidden;

                    Update_Spawn_Area_Details_View();
                    break;
                case "hunting":
                    Top_Right_Title.Text = "Hunting Grounds Details";
                    Spawns_Zone_Canvas.Visibility = Visibility.Hidden;
                    Spawn_Maker_Spawns_Grid.Visibility = Visibility.Hidden;
                    Spawn_Makers_Grid.Visibility = Visibility.Hidden;
                    Spawn_Parameters_Grid.Visibility = Visibility.Hidden;
                    Spawn_Areas_Grid.Visibility = Visibility.Hidden;

                    Huntingzone_Canvas.Visibility = Visibility.Visible;
                    Huntingzone_Grid.Visibility = Visibility.Visible;
                    if (active_L2H_Huntingzone != null)
                        Huntingzone_Parameters_Grid.Visibility = Visibility.Visible;
                    else
                        Huntingzone_Parameters_Grid.Visibility = Visibility.Hidden;

                    Zonename_Canvas.Visibility = Visibility.Hidden;
                    Zonename_Grid.Visibility = Visibility.Hidden;
                    Zonename_Parameters_Grid.Visibility = Visibility.Hidden;

                    Raid_Parameters_Grid.Visibility = Visibility.Hidden;
                    Raid_Canvas.Visibility = Visibility.Hidden;
                    Raid_Grid.Visibility = Visibility.Hidden;

                    Update_Huntingzone_Details_View();
                    break;
                case "names":
                    Top_Right_Title.Text = "Zone Name Details";
                    Spawns_Zone_Canvas.Visibility = Visibility.Hidden;
                    Spawn_Maker_Spawns_Grid.Visibility = Visibility.Hidden;
                    Spawn_Makers_Grid.Visibility = Visibility.Hidden;
                    Spawn_Parameters_Grid.Visibility = Visibility.Hidden;
                    Spawn_Areas_Grid.Visibility = Visibility.Hidden;

                    Huntingzone_Canvas.Visibility = Visibility.Hidden;
                    Huntingzone_Grid.Visibility = Visibility.Hidden;
                    Huntingzone_Parameters_Grid.Visibility = Visibility.Hidden;

                    Zonename_Canvas.Visibility = Visibility.Visible;
                    Zonename_Grid.Visibility = Visibility.Visible;
                    if (active_L2H_Zonename != null)
                        Zonename_Parameters_Grid.Visibility = Visibility.Visible;
                    else
                        Zonename_Parameters_Grid.Visibility = Visibility.Hidden;

                    Raid_Parameters_Grid.Visibility = Visibility.Hidden;
                    Raid_Canvas.Visibility = Visibility.Hidden;
                    Raid_Grid.Visibility = Visibility.Hidden;

                    Update_Zonename_Details_View();
                    break;
                case "raids":
                    Top_Right_Title.Text = "Raid Details";
                    Spawns_Zone_Canvas.Visibility = Visibility.Hidden;
                    Spawn_Maker_Spawns_Grid.Visibility = Visibility.Hidden;
                    Spawn_Makers_Grid.Visibility = Visibility.Hidden;
                    Spawn_Parameters_Grid.Visibility = Visibility.Hidden;
                    Spawn_Areas_Grid.Visibility = Visibility.Hidden;

                    Huntingzone_Canvas.Visibility = Visibility.Hidden;
                    Huntingzone_Grid.Visibility = Visibility.Hidden;
                    Huntingzone_Parameters_Grid.Visibility = Visibility.Hidden;

                    Zonename_Canvas.Visibility = Visibility.Hidden;
                    Zonename_Grid.Visibility = Visibility.Hidden;
                    Zonename_Parameters_Grid.Visibility = Visibility.Hidden;

                    Raid_Canvas.Visibility = Visibility.Visible;
                    Raid_Grid.Visibility = Visibility.Visible;
                    if (active_L2H_Raid != null)
                        Raid_Parameters_Grid.Visibility = Visibility.Visible;
                    else
                        Raid_Parameters_Grid.Visibility = Visibility.Hidden;

                    Update_Raid_Details_View();
                    break;
                default:
                    break;
            }

        }

        private void Huntingzone_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            L2H_Huntingzone huntingzone = (sender as FrameworkElement).DataContext as L2H_Huntingzone;


            if (active_L2H_Huntingzone != huntingzone)
            {
                if (active_L2H_Huntingzone != null)
                {
                    active_L2H_Huntingzone.IsSelected = false;
                }
                active_L2H_Huntingzone = huntingzone;
                active_L2H_Huntingzone.IsSelected = true;

                Huntingzone_Selected_Polygon_Points = Huntingzone_Preview_Polygon_Points;
                Huntingzone_Selected_Polygon_Arrow_Points = Huntingzone_Preview_Polygon_Arrow_Points;
                Huntingzone_Details_Grid_Image_Polygon_Selected.Points = Huntingzone_Selected_Polygon_Points;
                Huntingzone_Details_Grid_Image_Selected_Arrow.Points = Huntingzone_Selected_Polygon_Arrow_Points;
                Huntingzone_Preview_Polygon_Points = null;
                Huntingzone_Preview_Polygon_Arrow_Points = null;
                Huntingzone_Details_Grid_Image_Polygon_Preview.Points = new PointCollection();


                Update_Huntingzone_Details_View();
                Huntingzone_Details_Grid.Visibility = Visibility.Visible;
                Huntingzone_Parameters_Grid.Visibility = Visibility.Visible;

            }

        }

        private void Zone_Name_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            L2H_Zonename zonename = (sender as FrameworkElement).DataContext as L2H_Zonename;


            if (active_L2H_Zonename != zonename)
            {
                if (active_L2H_Zonename != null)
                {
                    active_L2H_Zonename.IsSelected = false;
                }
                active_L2H_Zonename = zonename;
                active_L2H_Zonename.IsSelected = true;

                Zonename_Selected_Polygon_Points = Zonename_Preview_Polygon_Points;
                Zonename_Selected_Polygon_Arrow_Points = Zonename_Preview_Polygon_Arrow_Points;
                Zonename_Details_Grid_Image_Polygon_Selected.Points = Zonename_Selected_Polygon_Points;
                Zonename_Details_Grid_Image_Selected_Arrow.Points = Zonename_Selected_Polygon_Arrow_Points;

                Zonename_Preview_Polygon_Points = null;
                Zonename_Preview_Polygon_Arrow_Points = null;
                Zonename_Details_Grid_Image_Polygon_Preview.Points = new PointCollection();


                Update_Zonename_Details_View();
                Zonename_Details_Grid.Visibility = Visibility.Visible;
                Zonename_Parameters_Grid.Visibility = Visibility.Visible;

            }
        }

        private void Huntingzone_Mouse_Over(object sender, MouseEventArgs e)
        {
            L2H_Huntingzone area = (sender as FrameworkElement).DataContext as L2H_Huntingzone;

            if (area != null)
            {
                preview_L2H_Huntingzone = area;
                Draw_Polygon_On_Map();
            }
        }
        private void Huntingzone_Mouse_Leave(object sender, MouseEventArgs e)
        {
            Huntingzone_Preview_Polygon_Points = null;
            Huntingzone_Preview_Polygon_Arrow_Points = null;
            Huntingzone_Details_Grid_Image_Polygon_Preview.Points = new PointCollection();
            Huntingzone_Details_Grid_Image_Polygon_Preview_Arrow.Points = new PointCollection();
        }
        private void Zone_Name_Mouse_Over(object sender, MouseEventArgs e)
        {
            L2H_Zonename area = (sender as FrameworkElement).DataContext as L2H_Zonename;

            if (area != null)
            {
                preview_L2H_Zonename = area;
                Draw_Polygon_On_Map();
            }
        }
        private void Zone_Name_Mouse_Leave(object sender, MouseEventArgs e)
        {
            Zonename_Preview_Polygon_Points = null;
            Zonename_Preview_Polygon_Arrow_Points = null;
            Zonename_Details_Grid_Image_Polygon_Preview.Points = new PointCollection();
            Zonename_Details_Grid_Image_Polygon_Preview_Arrow.Points = new PointCollection();
        }


        private void Huntingzone_Clone_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            if (active_L2H_Huntingzone == null)
                return;
            else
            {
                L2H_Huntingzone newHuntingzone = ObjectExtensions.Copy(active_L2H_Huntingzone);
                newHuntingzone.ID = Get_Unique_Huntingzone_ID();
                L2H_Huntingzones.Add(newHuntingzone);

                L2H_Log.Instance.Log_Huntingzone_Clone(active_L2H_Huntingzone, newHuntingzone);

                CollectionViewSource.GetDefaultView(Huntingzones_Listview.ItemsSource).Refresh();

                Refresh_Zone_Type_Selection();

                Huntingzones_Listview.ScrollIntoView(Huntingzones_Listview.Items[Huntingzones_Listview.Items.Count - 1]);
            }
        }

        private void Huntingzone_Delete_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            if (active_L2H_Huntingzone == null)
                return;

            L2H_Log.Instance.Log_Huntingzone_Delete(active_L2H_Huntingzone);

            L2H_Huntingzones.Remove(active_L2H_Huntingzone);
            active_L2H_Huntingzone = null;

            Update_Huntingzone_Details_View();
            Huntingzone_Details_Grid.Visibility = Visibility.Hidden;

        }

        private void Raid_Clone_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            if (active_L2H_Huntingzone == null)
                return;
            else
            {
                L2H_Raid newRaid = ObjectExtensions.Copy(active_L2H_Raid);
                newRaid.ID = Get_Unique_Raid_ID();
                L2H_Raids.Add(newRaid);

                L2H_Log.Instance.Log_Raid_Clone(active_L2H_Raid, newRaid);

                CollectionViewSource.GetDefaultView(Raids_Listview.ItemsSource).Refresh();

                Refresh_Zone_Type_Selection();

                Raids_Listview.ScrollIntoView(Raids_Listview.Items[Raids_Listview.Items.Count - 1]);
            }
        }

        private void Raid_Delete_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            if (active_L2H_Raid == null)
                return;

            L2H_Log.Instance.Log_Raid_Delete(active_L2H_Raid);

            L2H_Raids.Remove(active_L2H_Raid);
            active_L2H_Raid = null;

            Update_Raid_Details_View();
            Raid_Details_Grid.Visibility = Visibility.Hidden;
        }

        private void Raid_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFocus();
            L2H_Raid raid = (sender as FrameworkElement).DataContext as L2H_Raid;


            if (active_L2H_Raid != raid)
            {
                if (active_L2H_Raid != null)
                {
                    active_L2H_Raid.IsSelected = false;
                }
                active_L2H_Raid = raid;
                active_L2H_Raid.IsSelected = true;

                Raid_Selected_Polygon_Points = Raid_Preview_Polygon_Points;
                Raid_Selected_Polygon_Arrow_Points = Raid_Preview_Polygon_Arrow_Points;
                Raid_Details_Grid_Image_Polygon_Selected.Points = Raid_Selected_Polygon_Points;
                Raid_Details_Grid_Image_Selected_Arrow.Points = Raid_Selected_Polygon_Arrow_Points;

                Raid_Preview_Polygon_Points = null;
                Raid_Preview_Polygon_Arrow_Points = null;
                Raid_Details_Grid_Image_Polygon_Preview.Points = new PointCollection();


                Raid_Details_Grid.Visibility = Visibility.Visible;
                Raid_Parameters_Grid.Visibility = Visibility.Visible;
                Update_Raid_Details_View();

            }

        }

        private void Raid_Mouse_Leave(object sender, MouseEventArgs e)
        {
            Raid_Preview_Polygon_Points = null;
            Raid_Preview_Polygon_Arrow_Points = null;
            Raid_Details_Grid_Image_Polygon_Preview.Points = new PointCollection();
            Raid_Details_Grid_Image_Polygon_Preview_Arrow.Points = new PointCollection();
        }

        private void Raid_Mouse_Over(object sender, MouseEventArgs e)
        {
            L2H_Raid raid = (sender as FrameworkElement).DataContext as L2H_Raid;

            if (raid != null)
            {
                preview_L2H_Raid = raid;
                Draw_Polygon_On_Map();
            }
        }

        private void Raid_NPC_Clicked(object sender, RoutedEventArgs e)
        {
            Popup_NPC_Selection dialog = new Popup_NPC_Selection(npcsPage.L2H_Npcs);
            dialog.Initialize_For_Raid(active_L2H_Raid);

        }

        private void Raid_GoTo_Clicked(object sender, RoutedEventArgs e)
        {
            if (active_L2H_Raid != null)
                if (active_L2H_Raid.L2H_NPC != null)
                {
                    (mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage).forceWaiting = true;
                    (mainWindow.GetPageOfType(typeof(NPCsPage)) as NPCsPage).Force_NPC_Selection(active_L2H_Raid.L2H_NPC);

                    mainWindow.Category_Force(MenuCategories.NPCs);
                }
        }

        private string Get_Unique_Huntingzone_ID(int huntingzoneID = 1)
        {
            if (L2H_Huntingzones.Exists(x => x.ID == huntingzoneID.ToString()))
            {
                int nextValue = huntingzoneID + 1;
                return Get_Unique_Huntingzone_ID(nextValue);
            }
            else
                return huntingzoneID.ToString();
        }
        private string Get_Unique_Raid_ID(int ID = 1)
        {
            if (L2H_Raids.Exists(x => x.ID == ID.ToString()))
            {
                int nextValue = ID + 1;
                return Get_Unique_Raid_ID(nextValue);
            }
            else
                return ID.ToString();
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
}