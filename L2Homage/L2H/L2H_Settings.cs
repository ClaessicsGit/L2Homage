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
        public string newItemIndexStart_TextStart = "NewItemIndexStart = ";
        public string newItemIndexStart;
        public string newNPCIndexStart_TextStart = "NewNPCIndexStart = ";
        public string newNPCIndexStart;
        public string newSkillIndexStart_TextStart = "NewSkillIndexStart = ";
        public string newSkillIndexStart;

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
        public string NewItemIndexStart
        {
            get
            {
                return newItemIndexStart;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    newItemIndexStart = "50000";
                else
                    newItemIndexStart = value;

                UpdateSettings();
            }
        }

        public string NewNPCIndexStart
        {
            get
            {
                return newNPCIndexStart;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    newNPCIndexStart = "37700";
                else
                newNPCIndexStart = value;

                UpdateSettings();
            }
        }

        public string NewSkillIndexStart
        {
            get
            {
                return newSkillIndexStart;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    newSkillIndexStart = "50000";
                else
                    newSkillIndexStart = value;

                UpdateSettings();
            }
        }

        public L2H_Settings(List<string> settings)
        {
            for (int i = 0; i < settings.Count; i++)
            {
                string[] splitSetting = settings[i].Replace(" ", "").Split('=');
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
                    case "NewItemIndexStart":
                        newItemIndexStart = splitSetting[1];
                        break;
                    case "NewNPCIndexStart":
                        newNPCIndexStart = splitSetting[1];
                        break;
                    case "NewSkillIndexStart":
                        newSkillIndexStart = splitSetting[1];
                        break;
                    default:
                        break;
                }
            }

            if (string.IsNullOrEmpty(newItemIndexStart))
                newItemIndexStart = "50000";
            if (string.IsNullOrEmpty(newNPCIndexStart))
                newNPCIndexStart = "37700"; 
            if (string.IsNullOrEmpty(newSkillIndexStart))
                newSkillIndexStart = "50000";

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
            exportString.Add(newItemIndexStart_TextStart + newItemIndexStart);
            exportString.Add(newNPCIndexStart_TextStart + newNPCIndexStart);
            exportString.Add(newSkillIndexStart_TextStart + newSkillIndexStart);

            return exportString;
        }
    }
}
