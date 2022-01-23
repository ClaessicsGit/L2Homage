using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public enum SettingType { ServerAddress, ExportOnlyCustomSpawnAreas, UsingDiablomizedSkills }
    public class L2H_Settings
    {
        public string serverAddress;
        public string serverAddress_TextStart = "ServerAddress = ";
        public string exportOnlyCustomSpawnAreas;
        public string exportOnlyCustomSpawnAreas_TextStart = "ExportOnlyCustomSpawnAreas = ";
        public string usingDiablomizedSkills;
        public string usingDiablomizedSkills_TextStart = "UsingDiablomizedSkills = ";

        public string ServerAddress
        {
            get
            {
                return serverAddress;
            }
            set
            {
                serverAddress = value;

                UpdateSettings();
            }
        }
        public bool ExportOnlyCustomSpawnAreas
        {
            get
            {
                if (exportOnlyCustomSpawnAreas == "true")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    exportOnlyCustomSpawnAreas = "true";
                else
                    exportOnlyCustomSpawnAreas = "false";

                UpdateSettings();

            }
        }
        public bool UsingDiablomizedSkills
        {
            get
            {
                if (usingDiablomizedSkills == "true")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    usingDiablomizedSkills = "true";
                else
                    usingDiablomizedSkills = "false";

                UpdateSettings();

            }
        }

        public L2H_Settings(List<string> settings)
        {
            for (int i = 0; i < settings.Count; i++)
            {
                string[] splitSetting = settings[i].Replace(" ","").Split('=');
                switch (splitSetting[0])
                {
                    case "ServerAddress":
                        serverAddress = splitSetting[1];
                        break;
                    case "ExportOnlyCustomSpawnAreas":
                        exportOnlyCustomSpawnAreas = splitSetting[1];
                        break;
                    case "UsingDiablomizedSkills":
                        usingDiablomizedSkills = splitSetting[1];
                        break;

                    default:
                        break;
                }
            }

        }

        public void UpdateSettings()
        {
            File.WriteAllLines(L2H_Constants.L2H_Settings_Path, GetExportStrings(), Encoding.GetEncoding(1200));
        }

        List<string> GetExportStrings()
        {
            List<string> exportString = new List<string>();
            exportString.Add(serverAddress_TextStart + serverAddress);
            exportString.Add(exportOnlyCustomSpawnAreas_TextStart + exportOnlyCustomSpawnAreas);
            exportString.Add(usingDiablomizedSkills_TextStart + usingDiablomizedSkills);

            return exportString;
        }
    }
}
