using System;
using System.Collections.Generic;

namespace L2Homage
{
    public class ServerSetting
    {
        public List<Server_InitialEquipment> initialEquipment;
        public List<Server_InitialEquipment> initialCustomEquipment;
        public List<InitialStartPoint> initialStartPoints;
        public List<string> restartPointData;
        public List<RaceStats> minimum_stat;
        public List<RaceStats> maximum_stat;
        public List<RaceStats> recommended_stat;
        public List<string> olympiad_arena;
        public List<string> olympiad_genaral_setting;
        public List<string> hero_general_setting;
        public List<string> pvpmatch_setting;
        public List<string> cleft_setting;
        public List<L2H_Item> L2H_Items;

        bool storingPoint;
        InitialStartPoint activeInitialStartPoint;

        public ServerSetting()
        {
            initialEquipment = new List<Server_InitialEquipment>();
            initialCustomEquipment = new List<Server_InitialEquipment>();
            initialStartPoints = new List<InitialStartPoint>();
            restartPointData = new List<string>();
            minimum_stat = new List<RaceStats>();
            maximum_stat = new List<RaceStats>();
            recommended_stat = new List<RaceStats>();
            olympiad_arena = new List<string>();
            olympiad_genaral_setting = new List<string>();
            hero_general_setting = new List<string>();
            pvpmatch_setting = new List<string>();
            cleft_setting = new List<string>();
        }

        public void AddData(string data, string dataType)
        {
            if (string.IsNullOrEmpty(data))
            {
                return;
            }
            switch (dataType)
            {
                case "initial_equipment_begin":
                    {
                        initialEquipment.Add(new Server_InitialEquipment(data, L2H_Items));
                    }
                    break;
                case "initial_custom_equipment_begin":
                    {
                        initialCustomEquipment.Add(new Server_InitialEquipment(data, L2H_Items));
                    }
                    break;
                case "initial_start_point_begin":
                    {
                        if (storingPoint)
                        {
                            if (data.Contains("point="))
                            {
                                string trimmedData = data.Replace("point={", "");
                                trimmedData = trimmedData.Replace("}", "");
                                activeInitialStartPoint.positions.Add(new PointPosition(trimmedData));
                            }
                            else if (data.Contains("class="))
                            {
                                string trimmedData = data.Replace("class={", "");
                                trimmedData = trimmedData.Replace("}", "");
                                activeInitialStartPoint.classes = trimmedData;
                            }
                        }
                        if (data.Contains("point_begin"))
                        {
                            InitialStartPoint newStartPoint = new InitialStartPoint();
                            activeInitialStartPoint = newStartPoint;
                            initialStartPoints.Add(newStartPoint);
                            storingPoint = true;
                        }
                        if (data.Contains("point_end"))
                        {
                            storingPoint = false;
                            activeInitialStartPoint = null;

                        }


                    }
                    break;
                case "restart_point_begin":
                    {
                        restartPointData.Add(data);
                        break;
                    }
                case "restart_point_end":
                    {
                        restartPointData.Add(data);
                        break;
                    }
                case "minimum_stat_begin":
                    {
                        minimum_stat.Add(new RaceStats(data));
                    }
                    break;
                case "maximum_stat_begin":
                    {
                        maximum_stat.Add(new RaceStats(data));
                    }
                    break;
                case "recommended_stat_begin":
                    {
                        recommended_stat.Add(new RaceStats(data));
                    }
                    break;
                case "olympiad_arena_begin":
                    {
                        olympiad_arena.Add(data);
                    }
                    break;
                case "olympiad_genaral_setting_begin":
                    {
                        olympiad_genaral_setting.Add(data);
                    }
                    break;
                case "hero_general_setting_start":
                    {
                        hero_general_setting.Add(data);
                    }
                    break;
                case "pvpmatch_setting_start":
                    {
                        pvpmatch_setting.Add(data);
                    }
                    break;
                case "cleft_setting_begin":
                    {
                        cleft_setting.Add(data);
                    }
                    break;
                default:
                    break;
            }
        }

        public void ReplaceInitialStartPoints(List<string> newPoints)
        {
            initialStartPoints.Clear();

            for (int i = 0; i < newPoints.Count; i++)
            {
                if (storingPoint)
                {
                    if (newPoints[i].Contains("point="))
                    {
                        string trimmedData = newPoints[i].Replace("point={", "");
                        trimmedData = trimmedData.Replace("}", "");
                        activeInitialStartPoint.positions.Add(new PointPosition(trimmedData));
                    }
                    else if (newPoints[i].Contains("{") && !newPoints.Contains("={"))
                    {
                        activeInitialStartPoint.classes = newPoints[i].Replace("{", "").Replace("}", "");
                    }
                }
                if (newPoints[i].Contains("point_begin"))
                {
                    InitialStartPoint newStartPoint = new InitialStartPoint();
                    activeInitialStartPoint = newStartPoint;
                    initialStartPoints.Add(newStartPoint);
                    storingPoint = true;
                }
                if (newPoints[i].Contains("point_end"))
                {
                    storingPoint = false;
                    activeInitialStartPoint = null;

                }
            }
        }

        public string[] GetExportStrings()
        {
            List<string> exportStrings = new List<string>();

            exportStrings.Add("initial_equipment_begin");

            for (int i = 0; i < initialEquipment.Count; i++)
            {
                string stringToAdd = "\t" + initialEquipment[i].classID + "={";

                for (int j = 0; j < initialEquipment[i].L2H_Initial_Equipment.Count; j++)
                {
                    stringToAdd += "{" + "[" + initialEquipment[i].L2H_Initial_Equipment[j].L2H_Item.Item_Name_ID + "]" + ";" + initialEquipment[i].L2H_Initial_Equipment[j].Amount + "}";

                    if (j < initialEquipment[i].L2H_Initial_Equipment.Count - 1)
                        stringToAdd += ";";
                }

                stringToAdd += "}";
                exportStrings.Add(stringToAdd);
            }
            exportStrings.Add("initial_equipment_end");
            exportStrings.Add(Environment.NewLine);

            exportStrings.Add("initial_custom_equipment_begin");

            for (int i = 0; i < initialCustomEquipment.Count; i++)
            {
                string stringToAdd = "\t" + initialCustomEquipment[i].classID + "={";

                for (int j = 0; j < initialCustomEquipment[i].L2H_Initial_Equipment.Count; j++)
                {
                    stringToAdd += "{" + "[" + initialCustomEquipment[i].L2H_Initial_Equipment[j].L2H_Item.Item_Name_ID + "]" + ";" + initialCustomEquipment[i].L2H_Initial_Equipment[j].Amount + "}";

                    if (j < initialCustomEquipment[i].L2H_Initial_Equipment.Count - 1)
                        stringToAdd += ";";
                }

                stringToAdd += "}";
                exportStrings.Add(stringToAdd);
            }
            exportStrings.Add("initial_custom_equipment_end");
            exportStrings.Add(Environment.NewLine);

            //Initial StartPoints // WORKING
            exportStrings.Add("initial_start_point_begin");
            for (int i = 0; i < initialStartPoints.Count; i++)
            {
                string stringToAdd = "\tpoint_begin";
                exportStrings.Add(stringToAdd);

                for (int j = 0; j < initialStartPoints[i].positions.Count; j++)
                {
                    stringToAdd = "\t\tpoint={" + initialStartPoints[i].positions[j].xPos + ";" + initialStartPoints[i].positions[j].yPos + ";" + initialStartPoints[i].positions[j].zPos + "}";
                    exportStrings.Add(stringToAdd);
                }

                exportStrings.Add("\t\tclass={" + initialStartPoints[i].classes + "}");
                
                exportStrings.Add("\tpoint_end");
            }

            exportStrings.Add("initial_start_point_end");

            exportStrings.Add(Environment.NewLine);

            //restartPointData //Needs Test
            exportStrings.Add("restart_point_begin");
            for (int i = 0; i < restartPointData.Count; i++)
            {
                if (restartPointData[i] == "area_begin" || restartPointData[i] == "area_end")
                {
                    exportStrings.Add("\t" + restartPointData[i]);
                }
                else
                    exportStrings.Add(restartPointData[i]);
            }
            exportStrings.Add("restart_point_end");

            exportStrings.Add(Environment.NewLine);
           
            //Racestats WORKING
            exportStrings.Add("minimum_stat_begin");
            for (int i = 0; i < minimum_stat.Count; i++)
            {
                string stringToAdd = "";
                // INT / STR / CON / MEN / DEX/ WIT
                stringToAdd = "\t" + minimum_stat[i].RaceClass + "={" + minimum_stat[i].INT + ";" +
                                                                  minimum_stat[i].STR + ";" +
                                                                  minimum_stat[i].CON + ";" +
                                                                  minimum_stat[i].MEN + ";" +
                                                                  minimum_stat[i].DEX + ";" +
                                                                  minimum_stat[i].WIT + "}";

                exportStrings.Add(stringToAdd);
            }
            exportStrings.Add("minimum_stat_end");
            exportStrings.Add(Environment.NewLine);

            exportStrings.Add("maximum_stat_begin");
            for (int i = 0; i < maximum_stat.Count; i++)
            {
                string stringToAdd = "";
                // INT / STR / CON / MEN / DEX/ WIT
                stringToAdd = "\t" + maximum_stat[i].RaceClass + "={" + maximum_stat[i].INT + ";" +
                                                                        maximum_stat[i].STR + ";" +
                                                                        maximum_stat[i].CON + ";" +
                                                                        maximum_stat[i].MEN + ";" +
                                                                        maximum_stat[i].DEX + ";" +
                                                                        maximum_stat[i].WIT + "}";

                exportStrings.Add(stringToAdd);
            }

            exportStrings.Add("maximum_stat_end");
            exportStrings.Add(Environment.NewLine);

            exportStrings.Add("recommended_stat_begin");
            for (int i = 0; i < recommended_stat.Count; i++)
            {
                string stringToAdd = "";
                // INT / STR / CON / MEN / DEX/ WIT
                stringToAdd = "\t" + recommended_stat[i].RaceClass + "={" + recommended_stat[i].INT + ";" +
                                                                            recommended_stat[i].STR + ";" +
                                                                            recommended_stat[i].CON + ";" +
                                                                            recommended_stat[i].MEN + ";" +
                                                                            recommended_stat[i].DEX + ";" +
                                                                            recommended_stat[i].WIT + "}";

                exportStrings.Add(stringToAdd);
            }

            exportStrings.Add("recommended_stat_end");
            exportStrings.Add(Environment.NewLine);

            exportStrings.Add("olympiad_arena_begin");
            for (int i = 0; i < olympiad_arena.Count; i++)
            {
                exportStrings.Add(olympiad_arena[i]);
            }
            exportStrings.Add("olympiad_arena_end");
            exportStrings.Add(Environment.NewLine);


            exportStrings.Add("olympiad_genaral_setting_begin");
            for (int i = 0; i < olympiad_genaral_setting.Count; i++)
            {
                exportStrings.Add(olympiad_genaral_setting[i]);
            }
            exportStrings.Add("olympiad_general_setting_end");
            exportStrings.Add(Environment.NewLine);


            exportStrings.Add("hero_general_setting_start");
            for (int i = 0; i < hero_general_setting.Count; i++)
            {
                exportStrings.Add(hero_general_setting[i]);
            }
            exportStrings.Add("hero_general_setting_end");
            exportStrings.Add(Environment.NewLine);


            exportStrings.Add("pvpmatch_setting_start");
            for (int i = 0; i < pvpmatch_setting.Count; i++)
            {
                exportStrings.Add(pvpmatch_setting[i]);
            }
            exportStrings.Add("pvpmatch_setting_end");
            exportStrings.Add(Environment.NewLine);


            exportStrings.Add("cleft_setting_begin");
            for (int i = 0; i < cleft_setting.Count; i++)
            {
                exportStrings.Add(cleft_setting[i]);
            }
            exportStrings.Add("cleft_setting_end");
            exportStrings.Add(Environment.NewLine);

            return exportStrings.ToArray();
        }
    }

    public class Server_InitialEquipment
    {
        public List<L2H_Initial_Equipment> L2H_Initial_Equipment;
        public string classID = "";

        public Server_InitialEquipment(string dataString, List<L2H_Item> L2H_Items)
        {
            L2H_Initial_Equipment = new List<L2H_Initial_Equipment>();

            string trimmedDataString = dataString.Replace("{", "");
            trimmedDataString = trimmedDataString.Replace("}", "");
            trimmedDataString = trimmedDataString.Replace("[", "");
            trimmedDataString = trimmedDataString.Replace("]", "");

            string[] splitClassFromData = trimmedDataString.Split('=');
            classID = splitClassFromData[0].Replace("\t", "");
            string[] splitDataString = splitClassFromData[1].Split(';');

            int entriesCompleted = 0;

            for (int i = 0; i < splitDataString.Length / 2; i++)
            {
                L2H_Item item = L2H_Items.Find(x => x.Item_Name_ID == splitDataString[i + entriesCompleted]);
                this.L2H_Initial_Equipment.Add(new L2H_Initial_Equipment(this, item, splitDataString[i + 1 + entriesCompleted]));

                entriesCompleted++;
            }


        }
    }

    public class InitialStartPoint
    {
        public string classes = "";
        public List<PointPosition> positions;

        public InitialStartPoint()
        {
            positions = new List<PointPosition>();
        }

        public List<string> GetExportStrings()
        {
            List<string> exportStrings = new List<string>();

            exportStrings.Add("point_begin" + "\n");
            for (int i = 0; i < positions.Count; i++)
            {
                exportStrings.Add("point={" + positions[i].xPos + ";" + positions[i].yPos + ";" + positions[i].zPos + "}");
            }

            exportStrings.Add("{" + classes + "}");
            exportStrings.Add("point_end");
            exportStrings.Add("\n");

            return exportStrings;
        }
    }

    public class PointPosition
    {
        public string xPos;
        public string yPos;
        public string zPos;

        public PointPosition(string data)
        {
            string[] splitData = data.Split(';');
            xPos = splitData[0];
            yPos = splitData[1];
            zPos = splitData[2];
        }
    }

    public class RaceStats
    {
        public string RaceClass = "";

        public string INT;
        public string STR;
        public string CON;
        public string MEN;
        public string DEX;
        public string WIT;

        public RaceStats(string dataLine)
        {
            string strippedDataLine = dataLine.Replace("{", "");
            strippedDataLine = strippedDataLine.Replace("}", "");

            string[] splitDataLine = strippedDataLine.Split('=');
            string[] splitDataLineStats = strippedDataLine.Split(';');

            RaceClass = splitDataLine[0];
            INT = splitDataLineStats[0].Replace(RaceClass + "=", "");
            STR = splitDataLineStats[1];
            CON = splitDataLineStats[2];
            MEN = splitDataLineStats[3];
            DEX = splitDataLineStats[4];
            WIT = splitDataLineStats[5];

        }
    }




}

