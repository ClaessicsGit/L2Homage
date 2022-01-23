using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Data;

namespace L2Homage
{
    /// <summary>
    /// Interaction logic for Popup_NPC_AI_Parameters.xaml
    /// </summary>
    public partial class Popup_NPC_AI_Parameters : Window
    {        
        L2H_NPC data;
        List<L2H_NPC_AI_Parameter> L2H_NPC_AI_Parameters;

        bool loadingParameters;

        public Popup_NPC_AI_Parameters(L2H_NPC targetData)
        {
            InitializeComponent();
            loadingParameters = true;
            data = targetData;
            
            List<string> targetAIPaths = new List<string>();


            if (!File.Exists(L2H_Constants.server_ai_classesPath))
            {
                MessageBox.Show("Couldn't find classes.txt at path: " + L2H_Constants.server_ai_classesPath + "\n");
                return;
            }
            else
            {
                int numberOfClassesFound = 0;
                using (TextReader textReader = new StreamReader(L2H_Constants.server_ai_classesPath))
                {
                    // Load the text line by line
                    string line = string.Empty;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        if (line.Contains(@"\" + data.server_Npcdata.npc_ai_id + ".nasc"))
                        {
                            //split the string with '/' character. if string is not "ai", it's the sum of the existing strings plus the new one

                            string[] splitString = line.Split('\\');

                            string parentString = @"Data\AI\";

                            for (int i = 0; i < splitString.Length; i++)
                            {
                                targetAIPaths.Add(parentString + splitString[i].Replace(".nasc", "") + @".nasc");
                                parentString = parentString + splitString[i] + @"\";
                                numberOfClassesFound++;
                            }


                        }
                    }

                    textReader.Dispose();
                }

                if (numberOfClassesFound == 0)
                {
                    MessageBox.Show("No .nasc file found with the name: " + data.server_Npcdata.npc_ai_id);
                    return;
                }

                L2H_NPC_AI_Parameters = new List<L2H_NPC_AI_Parameter>();


                for (int i = 0; i < targetAIPaths.Count; i++)
                {
                    using (TextReader textReader = new StreamReader(targetAIPaths[i]))
                    {
                        bool loadingParameters = false;
                        // Load the text line by line
                        string line = string.Empty;
                        while ((line = textReader.ReadLine()) != null)
                        {
                            if (line == @"parameter:")
                            {
                                loadingParameters = true;
                                continue;
                            }

                            if (line == @"handler:")
                            {
                                loadingParameters = false;
                            }

                            if (loadingParameters)
                            {
                                string[] splitLine = line.Split('=');
                                if (splitLine.Length > 1)
                                {
                                    string trimmedName = splitLine[0].Replace("\t", " ");
                                    trimmedName = trimmedName.Replace(" int ", "");
                                    trimmedName = trimmedName.Replace(" float ", "");
                                    trimmedName = trimmedName.Replace(" string ", "");
                                    trimmedName = trimmedName.Replace(" ", "");
                                    string trimmedValue = splitLine[1].Replace(" ", "");
                                    trimmedValue = trimmedValue.Replace(";", "");
                                    L2H_NPC_AI_Parameters.Add(new L2H_NPC_AI_Parameter(targetData, trimmedName, trimmedValue));
                                }
                            }
                        }
                    }
                }

                Selections_Listview.ItemsSource = L2H_NPC_AI_Parameters;
                CollectionViewSource.GetDefaultView(Selections_Listview.ItemsSource).Refresh();

                List<string> variableStringsToRemove = new List<string>();

                for (int i = 0; i < targetData.server_Npcdata.npc_ai_variables.Count; i++)
                {
                    if(L2H_NPC_AI_Parameters.Exists(x=> x.Name == targetData.server_Npcdata.npc_ai_variables[i]))
                    {
                        L2H_NPC_AI_Parameters.Find(x => x.Name == targetData.server_Npcdata.npc_ai_variables[i]).isEnabled = true;
                    }
                    else
                    {
                        //add a red text item with a checked box
                        //Remove any irrelevant properties maybe?
                        variableStringsToRemove.Add(targetData.server_Npcdata.npc_ai_variables[i]);
                    }
                }

                for (int i = 0; i < variableStringsToRemove.Count; i++)
                {
                    int targetIndex = targetData.server_Npcdata.npc_ai_variables.FindIndex(x => x == variableStringsToRemove[i]);
                    targetData.server_Npcdata.npc_ai_variables.Remove(variableStringsToRemove[i]);
                    targetData.server_Npcdata.npc_ai_values.RemoveAt(targetIndex);
                }

                variableStringsToRemove.Clear();

            }

            loadingParameters = false;
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
    }
}
