using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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
using Xceed.Wpf.Toolkit;

namespace L2Homage.Pages
{
    /// <summary>
    /// Interaction logic for SystemTextPage.xaml
    /// </summary>
    public partial class SystemTextPage : Page
    {
        MainWindow mainWindow;

        public List<Client_Obscene> client_Obscenes;
        public List<Client_System_String> client_System_Strings;
        public List<Client_System_Message> client_System_Messages;
        public List<Client_Gametip> client_Gametips;
        public List<Client_Npc_String> client_Npc_Strings;

        public Client_Eula client_Eula;
        public L2H_EULA L2H_EULA;

        public List<L2H_Chatfilter> L2H_Chatfilters;
        public List<L2H_System_String> L2H_System_Strings;
        public List<L2H_System_Message> L2H_System_Messages;
        public List<L2H_Gametip> L2H_Gametips;
        public List<L2H_NPC_String> L2H_NPC_Strings;

        public string obsceneStartLine;
        public string systemStringStartLine;
        public string systemMessageStartLine;
        public string gametipStartLine;
        public string eulaStartLine;
        public string npcStringStartLine;

        public ToggleButton activeSystemTextTypeToggleButton;
        public L2H_System_Message active_L2H_System_Message;

        public SystemTextPage()
        {
            InitializeComponent();
            mainWindow = (Application.Current.MainWindow as MainWindow);
            activeSystemTextTypeToggleButton = System_Text_Type_Chat_Button;
            Chat_Filter_Listview_Grid.Visibility = Visibility.Visible;
            
        }
        #region Loading
        public void LoadSystemTexts()
        {
            LoadChatFilter();
            LoadSystemStrings();
            LoadSystemMessages();
            LoadGametips();
            LoadEula();
            LoadNPCStrings();

            mainWindow.UpdateLoadedNumber(LoadedTypes.Chat_Filters);
            mainWindow.UpdateLoadedNumber(LoadedTypes.System_Strings);
            mainWindow.UpdateLoadedNumber(LoadedTypes.System_Messages);
            mainWindow.UpdateLoadedNumber(LoadedTypes.Gametips);
            mainWindow.UpdateLoadedNumber(LoadedTypes.NPC_Strings);
        }
        private void LoadChatFilter()
        {
            client_Obscenes = new List<Client_Obscene>();
            L2H_Chatfilters = new List<L2H_Chatfilter>();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Obscene_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Obscene newEntry = new Client_Obscene(line);
                    if (newEntry.ID != "id")
                    {
                        client_Obscenes.Add(newEntry);
                    }

                    else
                    {
                        obsceneStartLine = line;
                    }
                }
            }

            for (int i = 0; i < client_Obscenes.Count; i++)
            {
                L2H_Chatfilters.Add(new L2H_Chatfilter(client_Obscenes[i]));
            }

            Dispatcher.Invoke(() =>
            {
                var newView = new CollectionViewSource() { Source = L2H_Chatfilters };
                var listViewCollection2 = (ListCollectionView)newView.View;
                listViewCollection2.Filter = Filter_Chatfilter;
                Chat_Filter_Listview.ItemsSource = listViewCollection2;
                CollectionViewSource.GetDefaultView(Chat_Filter_Listview.ItemsSource).Refresh();
                mainWindow.UpdateLog("Chat Filters Loaded: " + L2H_Chatfilters.Count, L2H_Constants.Color_Add);

            });
        }
        private void LoadSystemStrings()
        {
            client_System_Strings = new List<Client_System_String>();
            L2H_System_Strings = new List<L2H_System_String>();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Sysstring_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_System_String newEntry = new Client_System_String(line);
                    if (newEntry.ID != "id")
                    {
                        client_System_Strings.Add(newEntry);
                    }

                    else
                    {
                        systemStringStartLine = line;
                    }
                }
            }

            for (int i = 0; i < client_System_Strings.Count; i++)
            {
                L2H_System_Strings.Add(new L2H_System_String(client_System_Strings[i]));
            }

            Dispatcher.Invoke(() =>
            {
                var newView = new CollectionViewSource() { Source = L2H_System_Strings };
                var listViewCollection2 = (ListCollectionView)newView.View;
                listViewCollection2.Filter = Filter_System_String;
                System_Strings_Listview.ItemsSource = listViewCollection2;
                CollectionViewSource.GetDefaultView(System_Strings_Listview.ItemsSource).Refresh();
                mainWindow.UpdateLog("System strings Loaded: " + L2H_System_Strings.Count, L2H_Constants.Color_Add);

            });

        }
        private void LoadSystemMessages()
        {
            client_System_Messages = new List<Client_System_Message>();
            L2H_System_Messages = new List<L2H_System_Message>();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Systemmsg_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_System_Message newEntry = new Client_System_Message(line);
                    if (newEntry.ID != "id")
                    {
                        client_System_Messages.Add(newEntry);
                    }

                    else
                    {
                        systemMessageStartLine = line;
                    }
                }
            }

            for (int i = 0; i < client_System_Messages.Count; i++)
            {
                L2H_System_Messages.Add(new L2H_System_Message(client_System_Messages[i]));
            }

            Dispatcher.Invoke(() =>
            {
                var newView = new CollectionViewSource() { Source = L2H_System_Messages };
                var listViewCollection2 = (ListCollectionView)newView.View;
                listViewCollection2.Filter = Filter_System_Message;
                System_Messages_Listview.ItemsSource = listViewCollection2;

                CollectionViewSource.GetDefaultView(System_Messages_Listview.ItemsSource).Refresh();
                mainWindow.UpdateLog("System messages Loaded: " + L2H_System_Messages.Count, L2H_Constants.Color_Add);

            });
        }
        private void LoadGametips()
        {
            client_Gametips = new List<Client_Gametip>();
            L2H_Gametips = new List<L2H_Gametip>();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Gametip_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Gametip newEntry = new Client_Gametip(line);
                    if (newEntry.ID != "id")
                    {
                        client_Gametips.Add(newEntry);
                    }

                    else
                    {
                        gametipStartLine = line;
                    }
                }
            }

            for (int i = 0; i < client_Gametips.Count; i++)
            {
                L2H_Gametips.Add(new L2H_Gametip(client_Gametips[i]));
            }

            Dispatcher.Invoke(() =>
            {
                var newView = new CollectionViewSource() { Source = L2H_Gametips };
                var listViewCollection2 = (ListCollectionView)newView.View;
                listViewCollection2.Filter = Filter_Gametip;
                Gametips_Listview.ItemsSource = listViewCollection2;
                CollectionViewSource.GetDefaultView(Gametips_Listview.ItemsSource).Refresh();
                mainWindow.UpdateLog("Gametips Loaded: " + L2H_Gametips.Count, L2H_Constants.Color_Add);

            });

        }
        private void LoadNPCStrings()
        {
            client_Npc_Strings = new List<Client_Npc_String>();
            L2H_NPC_Strings = new List<L2H_NPC_String>();

            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_NPCstrings_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Npc_String newEntry = new Client_Npc_String(line);
                    if (newEntry.ID != "id")
                    {
                        client_Npc_Strings.Add(newEntry);
                    }

                    else
                    {
                        npcStringStartLine = line;
                    }
                }
            }

            for (int i = 0; i < client_Npc_Strings.Count; i++)
            {
                L2H_NPC_Strings.Add(new L2H_NPC_String(client_Npc_Strings[i]));
            }

            Dispatcher.Invoke(() =>
            {
                var newView = new CollectionViewSource() { Source = L2H_NPC_Strings };
                var listViewCollection2 = (ListCollectionView)newView.View;
                listViewCollection2.Filter = Filter_NPC_String;
                NPC_Strings_Listview.ItemsSource = listViewCollection2;
                CollectionViewSource.GetDefaultView(NPC_Strings_Listview.ItemsSource).Refresh();
                mainWindow.UpdateLog("NPC strings Loaded: " + L2H_NPC_Strings.Count, L2H_Constants.Color_Add);

            });

        }

        private void LoadEula()
        {
            // Load text
            using (TextReader textReader = new StreamReader(L2H_Constants.client_Eula_Path, Encoding.GetEncoding(65001)))
            {
                // Load the text line by line
                string line = string.Empty;
                while ((line = textReader.ReadLine()) != null)
                {
                    Client_Eula newEula = new Client_Eula(line);
                    if (newEula.eula != "eula")
                    {
                        client_Eula = newEula;
                        this.L2H_EULA = new L2H_EULA(client_Eula);
                    }

                    else
                    {
                        eulaStartLine = line;
                    }
                }
            }

            Dispatcher.Invoke(() =>
            {
                Eula_Text.DataContext = L2H_EULA;
            });

        }
        #endregion

        //Filters
        #region Filters
        private bool Filter_Chatfilter(object item)
        {
            L2H_Chatfilter filteredItem = item as L2H_Chatfilter;

            if (filteredItem == null)
                return false;

            if (!string.IsNullOrEmpty(Text_Filter_ID.Text))
            {
                if (!filteredItem.ID.Contains(Text_Filter_ID.Text))
                    return false;

            }

            if (!string.IsNullOrEmpty(Text_Filter_Contains.Text))
            {
                if (!filteredItem.Text.Contains(Text_Filter_Contains.Text))
                    return false;
            }

            return true;
        }
        private bool Filter_Gametip(object item)
        {
            L2H_Gametip filteredItem = item as L2H_Gametip;

            if (filteredItem == null)
                return false;


            if (!string.IsNullOrEmpty(Text_Filter_ID.Text))
            {
                if (!filteredItem.ID.Contains(Text_Filter_ID.Text))
                    return false;

            }

            if (!string.IsNullOrEmpty(Text_Filter_Contains.Text))
            {
                if (!filteredItem.Text.Contains(Text_Filter_Contains.Text))
                    return false;
            }

            return true;
        }
        private bool Filter_System_Message(object item)
        {
            L2H_System_Message filteredItem = item as L2H_System_Message;

            if (filteredItem == null)
                return false;


            if (!string.IsNullOrEmpty(Text_Filter_ID.Text))
            {
                if (!filteredItem.ID.Contains(Text_Filter_ID.Text))
                    return false;

            }

            if (!string.IsNullOrEmpty(Text_Filter_Contains.Text))
            {
                if (!filteredItem.Text.Contains(Text_Filter_Contains.Text))
                    return false;
            }

            return true;
        }
        private bool Filter_System_String(object item)
        {
            L2H_System_String filteredItem = item as L2H_System_String;

            if (filteredItem == null)
                return false;


            if (!string.IsNullOrEmpty(Text_Filter_ID.Text))
            {
                if (!filteredItem.ID.Contains(Text_Filter_ID.Text))
                    return false;

            }

            if (!string.IsNullOrEmpty(Text_Filter_Contains.Text))
            {
                if (!filteredItem.Text.Contains(Text_Filter_Contains.Text))
                    return false;
            }

            return true;
        }
        private bool Filter_NPC_String(object item)
        {
            L2H_NPC_String filteredItem = item as L2H_NPC_String;

            if (filteredItem == null)
                return false;


            if (!string.IsNullOrEmpty(Text_Filter_ID.Text))
            {
                if (!filteredItem.ID.Contains(Text_Filter_ID.Text))
                    return false;

            }

            if (!string.IsNullOrEmpty(Text_Filter_Contains.Text))
            {
                if (!filteredItem.Text.Contains(Text_Filter_Contains.Text))
                    return false;
            }

            return true;
        }


        private void Filter_ID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (activeSystemTextTypeToggleButton == System_Text_Type_Chat_Button)
            {
                CollectionViewSource.GetDefaultView(Chat_Filter_Listview.ItemsSource).Refresh();
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_EULA_Button)
            {
                //No reason to filter eula
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_Gametips_Button)
            {
                CollectionViewSource.GetDefaultView(Gametips_Listview.ItemsSource).Refresh();
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_NPC_Button)
            {
                CollectionViewSource.GetDefaultView(NPC_Strings_Listview.ItemsSource).Refresh();
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_System_Messages_Button)
            {
                CollectionViewSource.GetDefaultView(System_Messages_Listview.ItemsSource).Refresh();
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_System_Strings_Button)
            {
                CollectionViewSource.GetDefaultView(System_Strings_Listview.ItemsSource).Refresh();
            }
        }

        private void Filter_Contains_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (activeSystemTextTypeToggleButton == System_Text_Type_Chat_Button)
            {
                CollectionViewSource.GetDefaultView(Chat_Filter_Listview.ItemsSource).Refresh();
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_EULA_Button)
            {
                //No reason to filter eula
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_Gametips_Button)
            {
                CollectionViewSource.GetDefaultView(Gametips_Listview.ItemsSource).Refresh();
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_NPC_Button)
            {
                CollectionViewSource.GetDefaultView(NPC_Strings_Listview.ItemsSource).Refresh();
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_System_Messages_Button)
            {
                CollectionViewSource.GetDefaultView(System_Messages_Listview.ItemsSource).Refresh();
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_System_Strings_Button)
            {
                CollectionViewSource.GetDefaultView(System_Strings_Listview.ItemsSource).Refresh();
            }
        }
        #endregion

        //UI Visibility
        private void RefreshVisibility()
        {
            if (activeSystemTextTypeToggleButton == System_Text_Type_Chat_Button)
            {
                Chat_Filter_Listview_Grid.Visibility = Visibility.Visible;
                Eula_Grid.Visibility = Visibility.Hidden;
                Gametips_Listview_Grid.Visibility = Visibility.Hidden;
                NPC_Strings_Listview_Grid.Visibility = Visibility.Hidden;
                System_Messages_Listview_Grid.Visibility = Visibility.Hidden;
                System_Strings_Listview_Grid.Visibility = Visibility.Hidden;

                CollectionViewSource.GetDefaultView(Chat_Filter_Listview.ItemsSource).Refresh();
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_EULA_Button)
            {
                Chat_Filter_Listview_Grid.Visibility = Visibility.Hidden;
                Eula_Grid.Visibility = Visibility.Visible;
                Gametips_Listview_Grid.Visibility = Visibility.Hidden;
                System_Messages_Listview_Grid.Visibility = Visibility.Hidden;
                NPC_Strings_Listview_Grid.Visibility = Visibility.Hidden;
                System_Strings_Listview_Grid.Visibility = Visibility.Hidden;
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_Gametips_Button)
            {
                Chat_Filter_Listview_Grid.Visibility = Visibility.Hidden;
                Eula_Grid.Visibility = Visibility.Hidden;
                Gametips_Listview_Grid.Visibility = Visibility.Visible;
                System_Messages_Listview_Grid.Visibility = Visibility.Hidden;
                NPC_Strings_Listview_Grid.Visibility = Visibility.Hidden;
                System_Strings_Listview_Grid.Visibility = Visibility.Hidden;

                CollectionViewSource.GetDefaultView(Gametips_Listview.ItemsSource).Refresh();
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_NPC_Button)
            {
                Chat_Filter_Listview_Grid.Visibility = Visibility.Hidden;
                Eula_Grid.Visibility = Visibility.Hidden;
                Gametips_Listview_Grid.Visibility = Visibility.Hidden;
                System_Messages_Listview_Grid.Visibility = Visibility.Hidden;
                NPC_Strings_Listview_Grid.Visibility = Visibility.Visible;
                System_Strings_Listview_Grid.Visibility = Visibility.Hidden;

                CollectionViewSource.GetDefaultView(NPC_Strings_Listview.ItemsSource).Refresh();
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_System_Messages_Button)
            {
                Chat_Filter_Listview_Grid.Visibility = Visibility.Hidden;
                Eula_Grid.Visibility = Visibility.Hidden;
                Gametips_Listview_Grid.Visibility = Visibility.Hidden;
                System_Messages_Listview_Grid.Visibility = Visibility.Visible;
                NPC_Strings_Listview_Grid.Visibility = Visibility.Hidden;
                System_Strings_Listview_Grid.Visibility = Visibility.Hidden;

                CollectionViewSource.GetDefaultView(System_Messages_Listview.ItemsSource).Refresh();
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_System_Strings_Button)
            {
                Chat_Filter_Listview_Grid.Visibility = Visibility.Hidden;
                Eula_Grid.Visibility = Visibility.Hidden;
                Gametips_Listview_Grid.Visibility = Visibility.Hidden;
                System_Messages_Listview_Grid.Visibility = Visibility.Hidden;
                NPC_Strings_Listview_Grid.Visibility = Visibility.Hidden;
                System_Strings_Listview_Grid.Visibility = Visibility.Visible;

                CollectionViewSource.GetDefaultView(System_Strings_Listview.ItemsSource).Refresh();
            }

        }

        //Clicks
        #region Clicks
        private void System_Text_Type_Clicked(object sender, RoutedEventArgs e)
        {
            ToggleButton tb = (sender as FrameworkElement) as ToggleButton;

            if (tb != null)
            {
                if (activeSystemTextTypeToggleButton != tb)
                {
                    activeSystemTextTypeToggleButton.IsChecked = false;
                }

                activeSystemTextTypeToggleButton = tb;
                activeSystemTextTypeToggleButton.IsChecked = true;

                RefreshVisibility();
            }
        }

        private void System_Message_Click(object sender, RoutedEventArgs e)
        {
            L2H_System_Message sm = (sender as FrameworkElement).DataContext as L2H_System_Message;

            if (sm == null)
                return;

            if (active_L2H_System_Message != null)
            {
                if (active_L2H_System_Message == sm)
                {
                    if (!active_L2H_System_Message.IsSelected)
                    {
                        System_Message_Details_Grid.Visibility = Visibility.Hidden;
                        active_L2H_System_Message.IsSelected = false;
                        active_L2H_System_Message = null;
                        return;
                    }
                    else
                    {
                        System_Message_Details_Grid.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    active_L2H_System_Message.IsSelected = false;
                    active_L2H_System_Message = sm;
                    active_L2H_System_Message.IsSelected = true;
                }
            }
            else
            {
                active_L2H_System_Message = sm;
                active_L2H_System_Message.IsSelected = true;
                System_Message_Details_Grid.Visibility = Visibility.Visible;
            }


            foreach (TextBox tb in L2H_Parser.FindVisualChildren<TextBox>(System_Message_Details_Grid))
            {
                tb.DataContext = active_L2H_System_Message;
            }
            foreach (ToggleButton tb in L2H_Parser.FindVisualChildren<ToggleButton>(System_Message_Details_Grid))
            {
                tb.DataContext = active_L2H_System_Message;
            }
            foreach (ComboBox cb in L2H_Parser.FindVisualChildren<ComboBox>(System_Message_Details_Grid))
            {
                cb.DataContext = active_L2H_System_Message;
            }

            System_Message_ColorPicker.DataContext = active_L2H_System_Message;


            CollectionViewSource.GetDefaultView(System_Messages_Listview.ItemsSource).Refresh();

        }

        #region Adding
        private void Add_Chatfilter_Clicked(object sender, RoutedEventArgs e)
        {
            List<L2H_System_Text> tempList = new List<L2H_System_Text>();
            tempList.AddRange(L2H_Chatfilters);

            Client_Obscene clientFile = new Client_Obscene(L2H_Parser.GetUniqueSystemTextID(tempList), "New");
            client_Obscenes.Add(clientFile);
            L2H_Chatfilter newChatFilter = new L2H_Chatfilter(clientFile);
            L2H_Chatfilters.Add(newChatFilter);
            CollectionViewSource.GetDefaultView(Chat_Filter_Listview.ItemsSource).Refresh();
            Chat_Filter_Listview.ScrollIntoView(Chat_Filter_Listview.Items[Chat_Filter_Listview.Items.Count - 1]);
            L2H_Log.Instance.Log_System_Text_Add(newChatFilter, "CHAT FILTER");
        }
        private void Add_Gametip_Clicked(object sender, RoutedEventArgs e)
        {
            List<L2H_System_Text> tempList = new List<L2H_System_Text>();
            tempList.AddRange(L2H_Gametips);

            Client_Gametip clientFile = new Client_Gametip(L2H_Parser.GetUniqueSystemTextID(tempList), "New");
            client_Gametips.Add(clientFile);
            L2H_Gametip newGametip = new L2H_Gametip(clientFile);
            L2H_Gametips.Add(newGametip);
            ListView targetListView = Gametips_Listview;
            CollectionViewSource.GetDefaultView(targetListView.ItemsSource).Refresh();
            targetListView.ScrollIntoView(targetListView.Items[targetListView.Items.Count - 1]);
            L2H_Log.Instance.Log_System_Text_Add(newGametip, "GAMETIP");
        }
        private void Add_System_String_Clicked(object sender, RoutedEventArgs e)
        {
            List<L2H_System_Text> tempList = new List<L2H_System_Text>();
            tempList.AddRange(L2H_System_Strings);

            Client_System_String clientFile = new Client_System_String(L2H_Parser.GetUniqueSystemTextID(tempList), "New");
            client_System_Strings.Add(clientFile);

            L2H_System_String neww = new L2H_System_String(clientFile);
            L2H_System_Strings.Add(neww);

            ListView targetListView = System_Strings_Listview;
            CollectionViewSource.GetDefaultView(targetListView.ItemsSource).Refresh();
            targetListView.ScrollIntoView(targetListView.Items[targetListView.Items.Count - 1]);
            L2H_Log.Instance.Log_System_Text_Add(neww, "SYSTEM STRING");
        }

        private void Add_NPC_String_Clicked(object sender, RoutedEventArgs e)
        {
            List<L2H_System_Text> tempList = new List<L2H_System_Text>();
            tempList.AddRange(L2H_NPC_Strings);

            Client_Npc_String clientFile = new Client_Npc_String(L2H_Parser.GetUniqueSystemTextID(tempList, 2000000), "New");
            client_Npc_Strings.Add(clientFile);

            L2H_NPC_String neww = new L2H_NPC_String(clientFile);
            L2H_NPC_Strings.Add(neww);

            ListView targetListView = NPC_Strings_Listview;
            CollectionViewSource.GetDefaultView(targetListView.ItemsSource).Refresh();
            targetListView.ScrollIntoView(targetListView.Items[targetListView.Items.Count - 1]);
            L2H_Log.Instance.Log_System_Text_Add(neww, "NPC STRING");
        }
        private void Add_System_Message_Clicked(object sender, RoutedEventArgs e)
        {
            List<L2H_System_Text> tempList = new List<L2H_System_Text>();
            tempList.AddRange(L2H_System_Messages);

            Client_System_Message clientFile = new Client_System_Message(L2H_Parser.GetUniqueSystemTextID(tempList), "New");
            client_System_Messages.Add(clientFile);

            L2H_System_Message newMessage = new L2H_System_Message(clientFile);
            L2H_System_Messages.Add(newMessage);

            ListView targetListView = System_Messages_Listview;
            CollectionViewSource.GetDefaultView(targetListView.ItemsSource).Refresh();
            targetListView.ScrollIntoView(targetListView.Items[targetListView.Items.Count - 1]);
            L2H_Log.Instance.Log_System_Text_Add(newMessage, "SYSTEM MESSAGE");

        }
        #endregion
        #endregion

        #region Deleting
        private void Delete_Chatfilter(L2H_Chatfilter item)
        {

            if (item == null)
                return;

            L2H_Log.Instance.Log_System_Text_Delete(item, "CHAT FILTER");
            L2H_Chatfilters.Remove(item);
            CollectionViewSource.GetDefaultView(Chat_Filter_Listview.ItemsSource).Refresh();
        }
        private void Delete_NPC_String(L2H_NPC_String item)
        {
            if (item == null)
                return;

            L2H_Log.Instance.Log_System_Text_Delete(item, "NPC STRING");
            L2H_NPC_Strings.Remove(item);
            CollectionViewSource.GetDefaultView(NPC_Strings_Listview.ItemsSource).Refresh();
        }

        

        private void Delete_System_String(L2H_System_String item)
        {
            if (item == null)
                return;

            L2H_Log.Instance.Log_System_Text_Delete(item, "SYSTEM STRING");
            L2H_System_Strings.Remove(item);
            CollectionViewSource.GetDefaultView(System_Strings_Listview.ItemsSource).Refresh();
        }
       
        private void Delete_Gametip(L2H_Gametip item)
        {

            if (item == null)
                return;

            L2H_Log.Instance.Log_System_Text_Delete(item, "GAMETIP");
            L2H_Gametips.Remove(item);
            CollectionViewSource.GetDefaultView(Gametips_Listview.ItemsSource).Refresh();
        }
        private void Delete_System_Message(L2H_System_Message item)
        {
            if (item == null)
                return;

            L2H_Log.Instance.Log_System_Text_Delete(item, "SYSTEM MESSAGE");
            L2H_System_Messages.Remove(item);
            CollectionViewSource.GetDefaultView(System_Messages_Listview.ItemsSource).Refresh();
        }

        private void Delete_Object(object sender, RoutedEventArgs e)
        {
            if (activeSystemTextTypeToggleButton == System_Text_Type_Chat_Button)
            {
                L2H_Chatfilter item = (sender as FrameworkElement).DataContext as L2H_Chatfilter;
                Popup_Confirmation_Only_Text dialog = new Popup_Confirmation_Only_Text();
                dialog.Confirmation_Action += (b, bb) => Delete_Chatfilter(item);
                dialog.InitializeConfirmation("Delete Chatfilter entry ID: " + item.ID + "?\nText: " + item.Text);
                
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_Gametips_Button)
            {
                L2H_Gametip item = (sender as FrameworkElement).DataContext as L2H_Gametip;
                Popup_Confirmation_Only_Text dialog = new Popup_Confirmation_Only_Text();
                dialog.Confirmation_Action += (b, bb) => Delete_Gametip(item);
                dialog.InitializeConfirmation("Delete Gametip entry ID: " + item.ID + "?\nText: " + item.Text);
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_NPC_Button)
            {
                L2H_NPC_String item = (sender as FrameworkElement).DataContext as L2H_NPC_String;
                Popup_Confirmation_Only_Text dialog = new Popup_Confirmation_Only_Text();
                dialog.Confirmation_Action += (b, bb) => Delete_NPC_String(item);
                dialog.InitializeConfirmation("Delete NPC String entry ID: " + item.ID + "?\nText: " + item.Text);
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_System_Messages_Button)
            {
                L2H_System_Message item = (sender as FrameworkElement).DataContext as L2H_System_Message;
                Popup_Confirmation_Only_Text dialog = new Popup_Confirmation_Only_Text();
                dialog.Confirmation_Action += (b, bb) => Delete_System_Message(item);
                dialog.InitializeConfirmation("Delete System Message entry ID: " + item.ID + "?\nText: " + item.Text);
            }
            else if (activeSystemTextTypeToggleButton == System_Text_Type_System_Strings_Button)
            {
                L2H_System_String item = (sender as FrameworkElement).DataContext as L2H_System_String;
                Popup_Confirmation_Only_Text dialog = new Popup_Confirmation_Only_Text();
                dialog.Confirmation_Action += (b, bb) => Delete_System_String(item);
                dialog.InitializeConfirmation("Delete System String entry ID: " + item.ID + "?\nText: " + item.Text);
            }

            
        }


        #endregion



    }
}
