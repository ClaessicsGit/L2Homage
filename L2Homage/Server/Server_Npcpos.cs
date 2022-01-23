using System;
using System.Collections.Generic;

namespace L2Homage
{
    public enum Npc_Variable_Type { undefined, pos, total, respawn, respawn_rand, dbname, dbsaving, privates, ai, ai_parameters, nickname, is_chase_pc, boss_respawn_set, WayPoints }
    public enum Npc_Maker_Variable_Type { undefined, event_name, initial_spawn, spawn_time, maximum_npc, name, ai, ai_parameters, flying, banned_territory }
    public class Server_Npcpos
    {
        public List<Npc_Maker> npc_maker_list;
        public List<Npc_Maker_Ex> npc_maker_ex_list;
        public List<Npc_Begin> npc_begin_list;
        public List<Npc_Ex_Begin> npc_ex_begin_list;
        public List<SpawnTerritory> territories;
        public List<TerritoryDomain> domains;

        public Server_Npcpos()
        {
            npc_maker_list = new List<Npc_Maker>();
            npc_maker_ex_list = new List<Npc_Maker_Ex>();
            npc_begin_list = new List<Npc_Begin>();
            npc_ex_begin_list = new List<Npc_Ex_Begin>();
            territories = new List<SpawnTerritory>();
            domains = new List<TerritoryDomain>();
        }

        public Server_Npcpos(List<string> dataline, bool custom = false)
        {
            npc_maker_list = new List<Npc_Maker>();
            npc_maker_ex_list = new List<Npc_Maker_Ex>();
            npc_begin_list = new List<Npc_Begin>();
            npc_ex_begin_list = new List<Npc_Ex_Begin>();
            territories = new List<SpawnTerritory>();
            domains = new List<TerritoryDomain>();

            Npc_Maker active_Npc_Maker = new Npc_Maker();
            Npc_Maker_Ex active_Npc_Maker_Ex = new Npc_Maker_Ex();

            for (int i = 0; i < dataline.Count; i++)
            {
                string[] splitLine = dataline[i].Split('\t');


                switch (splitLine[0])
                {
                    case "domain_begin":
                        {
                            domains.Add(new TerritoryDomain(splitLine));
                            break;
                        }
                    case "npcmaker_begin":
                        {
                            Npc_Maker nm = new Npc_Maker(splitLine);
                            active_Npc_Maker = nm;
                            npc_maker_list.Add(nm);
                            break;
                        }
                    case "npc_begin":
                        {
                            npc_begin_list.Add(new Npc_Begin(splitLine, active_Npc_Maker));
                            break;
                        }
                    case "npcmaker_ex_begin":
                        {
                            Npc_Maker_Ex nme = new Npc_Maker_Ex(splitLine);
                            active_Npc_Maker_Ex = nme;
                            npc_maker_ex_list.Add(nme);
                            break;
                        }
                    case "npc_ex_begin":
                        {
                            npc_ex_begin_list.Add(new Npc_Ex_Begin(splitLine, active_Npc_Maker_Ex));
                            break;
                        }
                    case "npcmaker_end":
                        {
                            break;
                        }
                    case "npcmaker_ex_end":
                        {
                            break;
                        }

                    case "territory_begin":
                        {
                            territories.Add(new SpawnTerritory(splitLine));
                            break;
                        }
                    default:
                        break;
                }
            }


            AssignTerritoriesToNpcMakers();


        }

        public void AssignTerritoriesToNpcMakers()
        {
            for (int i = 0; i < npc_maker_list.Count; i++)
            {
                for (int j = 0; j < npc_maker_list[i].territoryIds.Count; j++)
                {
                    SpawnTerritory target = territories.Find(x => x.id == npc_maker_list[i].territoryIds[j]);
                    if (!target.npc_makers_in_terrain.Contains(npc_maker_list[i]))
                        target.npc_makers_in_terrain.Add(npc_maker_list[i]);
                }
            }

            for (int i = 0; i < npc_maker_ex_list.Count; i++)
            {
                for (int j = 0; j < npc_maker_ex_list[i].territoryIds.Count; j++)
                {
                    SpawnTerritory target = territories.Find(x => x.id == npc_maker_ex_list[i].territoryIds[j]);
                    if (!target.npc_ex_makers_in_terrain.Contains(npc_maker_ex_list[i]))
                        target.npc_ex_makers_in_terrain.Add(npc_maker_ex_list[i]);
                }
            }

        }

        public List<string> GetFullExport()
        {
            List<string> fullExport = new List<string>();

            //First we do domains
            string[] domainsToExport;

            domainsToExport = new string[domains.Count];

            //Grab all domains to export.
            for (int i = 0; i < domainsToExport.Length; i++)
            {
                domainsToExport[i] = domains[i].GetExportString();
            }

            //Then we grab all empty territories to export
            List<SpawnTerritory> emptyTerritories = new List<SpawnTerritory>();

            for (int i = 0; i < territories.Count; i++)
            {
                if (territories[i].npc_ex_makers_in_terrain.Count == 0 && territories[i].npc_makers_in_terrain.Count == 0)
                {
                    if (!emptyTerritories.Exists(x => x.id == territories[i].id))
                        emptyTerritories.Add(territories[i]);
                }
            }

            //Then we do npcmakers
            string[] npcMakerBegins = new string[npc_maker_list.Count];

            List<string> all_npc_maker_sections = new List<string>();
            List<SpawnTerritory> existingTerritories = new List<SpawnTerritory>();

            //First add all empty territories to make sure they're instantiated before NPC makers refer to them
            for (int i = 0; i < emptyTerritories.Count; i++)
            {
                all_npc_maker_sections.Add(emptyTerritories[i].GetExportString());
            }

            for (int i = 0; i < npcMakerBegins.Length; i++)
            {
                Npc_Maker targetMaker;

                targetMaker = npc_maker_list[i];

                List<string> npc_maker_section = new List<string>();

                for (int j = 0; j < targetMaker.territoryIds.Count; j++)
                {
                    SpawnTerritory targetTerritory = territories.Find(x => x.id == targetMaker.territoryIds[j]);

                    if (!existingTerritories.Contains(targetTerritory))
                    {
                        existingTerritories.Add(targetTerritory);
                        npc_maker_section.Add(targetTerritory.GetExportString());
                    }
                }

                npc_maker_section.Add(targetMaker.GetExportString());


                for (int j = 0; j < targetMaker.npcs.Count; j++)
                {
                    npc_maker_section.Add(targetMaker.npcs[j].GetExportString());
                }

                npc_maker_section.Add("npcmaker_end\t\t\t\t\t\t\t");

                //add to all_npc_maker_sections
                for (int j = 0; j < npc_maker_section.Count; j++)
                {
                    all_npc_maker_sections.Add(npc_maker_section[j]);
                }
            }

            for (int i = 0; i < npc_maker_ex_list.Count; i++)
            {
                Npc_Maker_Ex targetMakerEx;

                targetMakerEx = npc_maker_ex_list[i];

                List<string> npc_ex_maker_section = new List<string>();

                for (int j = 0; j < targetMakerEx.territoryIds.Count; j++)
                {
                    SpawnTerritory targetTerritory = territories.Find(x => x.id == targetMakerEx.territoryIds[j]);

                    if (!existingTerritories.Contains(targetTerritory))
                    {
                        existingTerritories.Add(targetTerritory);
                        npc_ex_maker_section.Add(targetTerritory.GetExportString());
                    }
                }

                npc_ex_maker_section.Add(targetMakerEx.GetExportString());

                for (int j = 0; j < targetMakerEx.npcs.Count; j++)
                {
                    npc_ex_maker_section.Add(targetMakerEx.npcs[j].GetExportString());
                }

                npc_ex_maker_section.Add("npcmaker_ex_end\t\t\t\t\t");

                //add to all_npc_maker_sections
                for (int j = 0; j < npc_ex_maker_section.Count; j++)
                {
                    all_npc_maker_sections.Add(npc_ex_maker_section[j]);
                }
            }

            for (int i = 0; i < domainsToExport.Length; i++)
            {
                fullExport.Add(domainsToExport[i]);
            }

            for (int i = 0; i < all_npc_maker_sections.Count; i++)
            {
                fullExport.Add(all_npc_maker_sections[i]);
            }

            return fullExport;
        }

    }

    /// <summary>
    /// Creates all the special NPCs
    /// </summary>
    public class Npc_Maker
    {
        public SpawnTerritory targetTerritory;

        string startString = "npcmaker_begin";
        string id_textstart = "[";
        public List<string> territoryIds;
        string id_textend = "]";
        public string initialSpawn;
        string initialSpawn_textstart = "initial_spawn=";
        public string spawnTime;
        string spawnTime_textstart = "spawn_time=";
        public string maximum_npc;
        string maximum_npc_textstart = "maximum_npc=";
        public string event_name;
        string event_name_textstart = "event_name=[";
        string event_name_textend = "]";

        int emptyVariables = 0;

        public List<Npc_Begin> npcs;

        public Npc_Maker()
        {
            //This one is empty and only for temp use
        }

        public Npc_Maker(string[] splitLine)
        {
            npcs = new List<Npc_Begin>();
            territoryIds = new List<string>();

            string ids = NpcPosUtility.StripExcessServerText(id_textstart, splitLine[1], id_textend);
            string[] splitIds = ids.Split(';');

            for (int i = 0; i < splitIds.Length; i++)
            {
                territoryIds.Add(splitIds[i]);
            }

            for (int i = 2; i < splitLine.Length; i++)
            {
                if (string.IsNullOrEmpty(splitLine[i]))
                {
                    emptyVariables++;
                    continue;
                }

                Npc_Maker_Variable_Type t;

                string value = NpcPosUtility.GetMakerVariableTypeAndValue(splitLine[i], out t);

                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                switch (t)
                {
                    case Npc_Maker_Variable_Type.undefined:
                        {

                        }
                        break;
                    case Npc_Maker_Variable_Type.initial_spawn:
                        {
                            initialSpawn = NpcPosUtility.StripExcessServerText(initialSpawn_textstart, splitLine[i]);
                        }
                        break;
                    case Npc_Maker_Variable_Type.spawn_time:
                        {
                            spawnTime = NpcPosUtility.StripExcessServerText(spawnTime_textstart, splitLine[i]);
                        }
                        break;
                    case Npc_Maker_Variable_Type.maximum_npc:
                        {
                            maximum_npc = NpcPosUtility.StripExcessServerText(maximum_npc_textstart, splitLine[i]);
                        }
                        break;
                    case Npc_Maker_Variable_Type.name:
                        {
                            string wat = "";
                        }
                        break;
                    case Npc_Maker_Variable_Type.ai:
                        {
                            string wat = "";
                        }
                        break;
                    case Npc_Maker_Variable_Type.ai_parameters:
                        {
                            string wat = "";
                        }
                        break;
                    case Npc_Maker_Variable_Type.event_name:
                        {
                            event_name = NpcPosUtility.StripExcessServerText(event_name_textstart, splitLine[i], event_name_textend);
                        }
                        break;
                    default:
                        {
                            string couldntfindthistype = splitLine[i];
                        }
                        break;
                }

            }

        }

        public string GetExportString()
        {

            string exportString = "";

            string territoriesString = "";

            for (int i = 0; i < territoryIds.Count; i++)
            {
                territoriesString += "[" + territoryIds[i] + "]";

                if (i != territoryIds.Count - 1)
                {
                    territoriesString += ";";
                }

            }

            exportString += startString + '\t' + territoriesString + '\t';

            if (!string.IsNullOrEmpty(event_name))
                exportString += event_name_textstart + event_name + event_name_textend + '\t';

            if (!string.IsNullOrEmpty(initialSpawn))
                exportString += initialSpawn_textstart + initialSpawn + '\t';

            if (!string.IsNullOrEmpty(spawnTime))
                exportString += spawnTime_textstart + spawnTime + '\t';

            if (!string.IsNullOrEmpty(maximum_npc))
                exportString += maximum_npc_textstart + maximum_npc + '\t';

            for (int i = 0; i < emptyVariables; i++)
            {
                exportString += '\t';
            }


            return exportString;
        }



    }

    /// <summary>
    /// Special NPC info
    /// </summary>
    public class Npc_Begin
    {
        Npc_Maker parent;
        string start = "npc_begin";
        public string id;
        string id_textstart = "[";
        string id_textend = "]";
        public List<Vector4> pos;
        string pos_textstart = "pos={";
        string pos_textend = "}";
        public string total;
        string total_textstart = "total=";
        public string respawn;
        string respawn_textstart = "respawn=";

        public string respawn_rand = "";
        string respawn_rand_textstart = "respawn_rand=";
        string respawn_rand_textend;

        public string privates = "";
        string privates_textstart = "Privates=[";
        string privates_textend = "]";

        public string nickname;
        string nickname_textstart = "nickname=[";
        string nickname_textend = "]";

        public string ai;
        string ai_textstart = "[";
        string ai_textend = "]";

        public string boss_respawn_set = "";
        string boss_respawn_set_textstart = "boss_respawn_set=";

        public string ai_parameters = "";
        string ai_parameters_textstart = "ai_parameters={";
        string ai_parameters_textend = "}";

        public string dbname = "";
        string dbname_textstart = "dbname=[";
        string dbname_textend = "]";
        public string dbsaving = "";
        string dbsaving_textstart = "dbsaving={";
        string dbsaving_textend = "}";
        string end = "npc_end";

        int emptyVariables = 0;
        public bool fixedPos = true;
        Vector4 fixedPosition;

        public Npc_Begin(Server_Npcdata npcData, Npc_Maker active_Npc_Maker)
        {
            parent = active_Npc_Maker;
            pos = new List<Vector4>();

            id = npcData.npcName;
            fixedPos = false;
            total = "1";
            respawn = "10sec";
        }

        public Npc_Begin(string[] splitLine, Npc_Maker active_Npc_Maker)
        {
            parent = active_Npc_Maker;
            parent.npcs.Add(this);
            pos = new List<Vector4>();
            id = NpcPosUtility.StripExcessServerText(id_textstart, splitLine[1], id_textend);

            for (int i = 2; i < splitLine.Length - 1; i++)
            {
                Npc_Variable_Type t;

                if (string.IsNullOrEmpty(splitLine[i]))
                {
                    emptyVariables++;
                    continue;
                }

                string value = NpcPosUtility.GetNPCVariableTypeAndValue(splitLine[i], out t);

                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                switch (t)
                {
                    case Npc_Variable_Type.undefined:
                        break;
                    case Npc_Variable_Type.pos:
                        {
                            if (value == "anywhere")
                                fixedPos = false;
                            else
                            {
                                string[] splitVectors = splitLine[i].Split(new string[] { "};{" }, StringSplitOptions.None);

                                for (int j = 0; j < splitVectors.Length; j++)
                                {
                                    splitVectors[j] = splitVectors[j].Replace("pos=", "");
                                    splitVectors[j] = splitVectors[j].Replace("{", "");
                                    splitVectors[j] = splitVectors[j].Replace("}", "");

                                    bool chances;

                                    pos.Add(new Vector4(splitVectors[j], out chances));
                                }
                            }
                        }
                        break;
                    case Npc_Variable_Type.total:
                        {
                            total = NpcPosUtility.StripExcessServerText(total_textstart, splitLine[i]);
                        }
                        break;
                    case Npc_Variable_Type.respawn:
                        {
                            respawn = NpcPosUtility.StripExcessServerText(respawn_textstart, splitLine[i]);
                        }
                        break;
                    case Npc_Variable_Type.respawn_rand:
                        {
                            respawn_rand = NpcPosUtility.StripExcessServerText(respawn_rand_textstart, splitLine[i]);
                        }
                        break;
                    case Npc_Variable_Type.dbname:
                        {
                            dbname = NpcPosUtility.StripExcessServerText(dbname_textstart, splitLine[i], dbname_textend);
                        }
                        break;
                    case Npc_Variable_Type.privates:
                        {
                            privates = NpcPosUtility.StripExcessServerText(privates_textstart, splitLine[i], privates_textend);
                        }
                        break;
                    case Npc_Variable_Type.ai_parameters:
                        {
                            ai_parameters = NpcPosUtility.StripExcessServerText(ai_parameters_textstart, splitLine[i], ai_parameters_textend);
                        }
                        break;
                    case Npc_Variable_Type.nickname:
                        {
                            nickname = NpcPosUtility.StripExcessServerText(nickname_textstart, splitLine[i], nickname_textend);
                        }
                        break;
                    case Npc_Variable_Type.dbsaving:
                        {
                            dbsaving = NpcPosUtility.StripExcessServerText(dbsaving_textstart, splitLine[i], dbsaving_textend);
                        }
                        break;
                    case Npc_Variable_Type.ai:
                        {
                            ai = NpcPosUtility.StripExcessServerText(ai_textstart, splitLine[i], ai_textend);
                        }
                        break;
                    case Npc_Variable_Type.boss_respawn_set:
                        {
                            boss_respawn_set = NpcPosUtility.StripExcessServerText(boss_respawn_set_textstart, splitLine[i]);
                        }
                        break;
                    default:
                        {
                            string couldntfindlocalvariableforvalue = value;
                        }
                        break;
                }
            }
        }

        public string GetExportString()
        {
            string exportString = "";

            exportString += start + '\t' + id_textstart + id + id_textend + '\t';


            if (!string.IsNullOrEmpty(nickname)) //Strange how nickname comes before pos= in npc_begin, but after respawn_rand on npc_ex_begin
                exportString += nickname_textstart + nickname + nickname_textend + '\t';


            if (!fixedPos)
                exportString += "pos=anywhere" + '\t';
            else
            {
                if (pos.Count == 1)
                {
                    exportString += pos_textstart + pos[0].xPos + ";" + pos[0].yPos + ";" + pos[0].zPos + ";" + pos[0].yaw;

                    if (!string.IsNullOrEmpty(pos[0].probability))
                    {
                        exportString += ";" + pos[0].probability;
                    }
                    exportString += pos_textend + '\t';
                }
                else
                {
                    exportString += pos_textstart + NpcPosUtility.GetVectorsExportString(pos, false) + '\t';
                }
            }
            exportString += total_textstart + total + '\t' + respawn_textstart + respawn + '\t';

            if (!string.IsNullOrEmpty(respawn_rand))
                exportString += respawn_rand_textstart + respawn_rand + respawn_rand_textend + '\t';

            if (!string.IsNullOrEmpty(dbname))
                exportString += dbname_textstart + dbname + dbname_textend + '\t';

            if (!string.IsNullOrEmpty(dbsaving))
                exportString += dbsaving_textstart + dbsaving + dbsaving_textend + '\t';

            if (!string.IsNullOrEmpty(ai_parameters))
                exportString += ai_parameters_textstart + ai_parameters + ai_parameters_textend + '\t';

            if (!string.IsNullOrEmpty(privates))
                exportString += privates_textstart + privates + privates_textend + '\t';

            if (!string.IsNullOrEmpty(boss_respawn_set))
                exportString += boss_respawn_set_textstart + boss_respawn_set + '\t';

            for (int i = 0; i < emptyVariables; i++)
            {
                exportString += '\t';
            }

            exportString += end;

            return exportString;

        }
    }

    public class Npc_Maker_Ex
    {
        string start = "npcmaker_ex_begin";
        public List<string> territoryIds;
        string id_textstart = "[";
        string id_textend = "]";
        public string name;
        string name_textstart = "name=[";
        string name_textend = "]";
        public string ai;
        string ai_textstart = "ai=[";
        string ai_textend = "]";
        public string ai_parameters;
        string ai_parameters_textstart = "ai_parameters={";
        string ai_parameters_textend = "}";
        public string maximum_npc;
        string maximum_npc_textstart = "maximum_npc=";
        public string flying;
        string flying_textstart = "flying=";
        public string banned_territory;
        string banned_territory_textstart = "banned_territory={";
        string banned_territory_textend = "}";

        public List<Npc_Ex_Begin> npcs;

        int emptyVariables = 0;

        public Npc_Maker_Ex()
        {
            //This one is empty and only for initializing a local variable
        }

        public Npc_Maker_Ex(string territoryID)
        {
            territoryIds = new List<string>();
            territoryIds.Add(territoryID);
            npcs = new List<Npc_Ex_Begin>();
            maximum_npc = "10";

            name = territoryID + "_maker";
            ai = "default_maker";

        }

        public Npc_Maker_Ex(string[] splitLine)
        {
            territoryIds = new List<string>();
            npcs = new List<Npc_Ex_Begin>();

            string[] splitTerrainIds = splitLine[1].Split(';');

            for (int i = 0; i < splitTerrainIds.Length; i++)
            {
                if (splitTerrainIds[0] == "[godard32_2515_10]")
                {
                    string wat = "";
                }
                territoryIds.Add(NpcPosUtility.StripExcessServerText(id_textstart, splitTerrainIds[i], id_textend));
            }
            for (int i = 2; i < splitLine.Length; i++)
            {
                if (string.IsNullOrEmpty(splitLine[i]))
                {
                    emptyVariables++;
                    continue;
                }

                Npc_Maker_Variable_Type t;

                string value = NpcPosUtility.GetMakerVariableTypeAndValue(splitLine[i], out t);

                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                switch (t)
                {
                    case Npc_Maker_Variable_Type.undefined:
                        {
                            string wat = "";
                        }
                        break;
                    case Npc_Maker_Variable_Type.event_name:
                        {
                            string wat = "";
                        }
                        break;
                    case Npc_Maker_Variable_Type.initial_spawn:
                        {
                            string wat = "";
                        }
                        break;
                    case Npc_Maker_Variable_Type.spawn_time:
                        {
                            string wat = "";
                        }
                        break;
                    case Npc_Maker_Variable_Type.maximum_npc:
                        maximum_npc = NpcPosUtility.StripExcessServerText(maximum_npc_textstart, splitLine[i]);
                        break;
                    case Npc_Maker_Variable_Type.name:
                        name = NpcPosUtility.StripExcessServerText(name_textstart, splitLine[i], name_textend);
                        break;
                    case Npc_Maker_Variable_Type.ai:
                        ai = NpcPosUtility.StripExcessServerText(ai_textstart, splitLine[i], ai_textend);
                        break;
                    case Npc_Maker_Variable_Type.ai_parameters:
                        ai_parameters = NpcPosUtility.StripExcessServerText(ai_parameters_textstart, splitLine[i], ai_parameters_textend);
                        break;
                    case Npc_Maker_Variable_Type.flying:
                        flying = NpcPosUtility.StripExcessServerText(flying_textstart, splitLine[i]);
                        break;
                    case Npc_Maker_Variable_Type.banned_territory:
                        {
                            banned_territory = NpcPosUtility.StripExcessServerText(banned_territory_textstart, splitLine[i], banned_territory_textend);
                        }
                        break;
                    default:
                        {
                            string wat = "";
                        }
                        break;
                }
            }


        }

        public string GetExportString()
        {
            string exportString = "";

            string territoriesString = "";

            for (int i = 0; i < territoryIds.Count; i++)
            {
                territoriesString += "[" + territoryIds[i] + "]";

                if (i != territoryIds.Count - 1)
                {
                    territoriesString += ";";
                }

            }

            exportString += start + '\t' + territoriesString + '\t';

            if (!string.IsNullOrEmpty(banned_territory))
                exportString += banned_territory_textstart + banned_territory + banned_territory_textend + '\t';

            if (!string.IsNullOrEmpty(name))
                exportString += name_textstart + name + name_textend + '\t';

            if (!string.IsNullOrEmpty(ai))
                exportString += ai_textstart + ai + ai_textend + '\t';

            if (!string.IsNullOrEmpty(ai_parameters))
                exportString += ai_parameters_textstart + ai_parameters + ai_parameters_textend + '\t';

            if (!string.IsNullOrEmpty(maximum_npc))
                exportString += maximum_npc_textstart + maximum_npc + '\t';

            if (!string.IsNullOrEmpty(flying))
                exportString += flying_textstart + flying + '\t';

            for (int i = 1; i < emptyVariables; i++)
            {
                exportString += '\t';
            }

            return exportString;

        }
    }

    public class Npc_Ex_Begin
    {
        Npc_Maker_Ex parent;
        string start = "npc_ex_begin";

        public string id;
        string id_textstart = "[";
        string id_textend = "]";

        public bool fixedPos = true;
        public List<Vector4> pos;
        List<string> posChances;
        string pos_textstart = "pos={";
        string pos_textend = "}";
        public string total;
        string total_textstart = "total=";
        public string respawn = "";
        string respawn_textstart = "respawn=";
        string respawn_textend;

        public string respawn_rand = "";
        string respawn_rand_textstart = "respawn_rand=";
        string respawn_rand_textend;

        public string dbname;
        string dbname_textstart = "dbname=[";
        string dbname_textend = "]";

        public string dbsaving;
        string dbsaving_textstart = "dbsaving={";
        string dbsaving_textend = "}";

        public string boss_respawn_set;
        string boss_respawn_set_textstart = "boss_respawn_set=";

        public string privates;
        string privates_textstart = "Privates=[";
        string privates_textend = "]";

        public string nickname;
        string nickname_textstart = "nickname=[";
        string nickname_textend = "]";

        public string ai;
        string ai_textstart = "[";
        string ai_textend = "]";

        public string ai_parameters;
        string ai_parameters_textstart = "ai_parameters={";
        string ai_parameters_textend = "}";

        public string is_chase_pc;
        string is_chase_pc_textstart = "is_chase_pc=";

        public string waypoints;
        string waypoints_textstart = "WayPoints={";
        string waypoints_textend = "}";

        string end = "npc_ex_end";

        int emptyVariables = 0;

        public Npc_Ex_Begin(Server_Npcdata npcData, Npc_Maker_Ex active_Npc_Maker_Ex)
        {
            parent = active_Npc_Maker_Ex;
            posChances = new List<string>();
            pos = new List<Vector4>();

            id = npcData.npcName;
            fixedPos = false;
            total = "1";
            respawn = "10sec";

        }
        public Npc_Ex_Begin(string[] splitLine, Npc_Maker_Ex active_Npc_Maker_Ex)
        {
            parent = active_Npc_Maker_Ex;
            parent.npcs.Add(this);
            posChances = new List<string>();
            pos = new List<Vector4>();
            id = NpcPosUtility.StripExcessServerText(id_textstart, splitLine[1], id_textend);

            for (int i = 2; i < splitLine.Length - 1; i++)
            {
                Npc_Variable_Type t;

                if (string.IsNullOrEmpty(splitLine[i]))
                {
                    emptyVariables++;
                    continue;
                }

                string value = NpcPosUtility.GetNPCVariableTypeAndValue(splitLine[i], out t);

                if (splitLine[i].Contains(waypoints_textstart))
                {
                    value = splitLine[i];
                }


                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                switch (t)
                {
                    case Npc_Variable_Type.undefined:
                        break;
                    case Npc_Variable_Type.pos:
                        {
                            if (value == "anywhere")
                                fixedPos = false;
                            else
                            {
                                string[] splitVectors = splitLine[i].Split(new string[] { "};{" }, StringSplitOptions.None);

                                for (int j = 0; j < splitVectors.Length; j++)
                                {
                                    splitVectors[j] = splitVectors[j].Replace("pos=", "");
                                    splitVectors[j] = splitVectors[j].Replace("{", "");
                                    splitVectors[j] = splitVectors[j].Replace("}", "");

                                    bool chances;

                                    pos.Add(new Vector4(splitVectors[j], out chances));
                                }

                            }
                        }
                        break;
                    case Npc_Variable_Type.total:
                        {
                            total = NpcPosUtility.StripExcessServerText(total_textstart, splitLine[i]);
                        }
                        break;
                    case Npc_Variable_Type.respawn:
                        {
                            respawn = NpcPosUtility.StripExcessServerText(respawn_textstart, splitLine[i]);
                        }
                        break;
                    case Npc_Variable_Type.respawn_rand:
                        {
                            respawn_rand = NpcPosUtility.StripExcessServerText(respawn_rand_textstart, splitLine[i]);
                        }
                        break;
                    case Npc_Variable_Type.dbname:
                        {
                            dbname = NpcPosUtility.StripExcessServerText(dbname_textstart, splitLine[i], dbname_textend);
                        }
                        break;
                    case Npc_Variable_Type.dbsaving:
                        {
                            dbsaving = NpcPosUtility.StripExcessServerText(dbsaving_textstart, splitLine[i], dbsaving_textend);
                        }
                        break;
                    case Npc_Variable_Type.privates:
                        {
                            privates = NpcPosUtility.StripExcessServerText(privates_textstart, splitLine[i], privates_textend);
                        }
                        break;
                    case Npc_Variable_Type.ai_parameters:
                        {
                            ai_parameters = NpcPosUtility.StripExcessServerText(ai_parameters_textstart, splitLine[i], ai_parameters_textend);
                        }
                        break;
                    case Npc_Variable_Type.nickname:
                        {
                            nickname = NpcPosUtility.StripExcessServerText(nickname_textstart, splitLine[i], nickname_textend);
                        }
                        break;
                    case Npc_Variable_Type.ai:
                        {
                            ai = NpcPosUtility.StripExcessServerText(ai_textstart, splitLine[i], ai_textend);
                        }
                        break;
                    case Npc_Variable_Type.boss_respawn_set:
                        {
                            boss_respawn_set = NpcPosUtility.StripExcessServerText(boss_respawn_set_textstart, splitLine[i]);
                        }
                        break;
                    case Npc_Variable_Type.is_chase_pc:
                        {
                            is_chase_pc = NpcPosUtility.StripExcessServerText(is_chase_pc_textstart, splitLine[i]);
                        }
                        break;
                    case Npc_Variable_Type.WayPoints:
                        {
                            waypoints = value;
                        }
                        break;
                    default:
                        {
                            string couldntfindlocalvariableforvalue = value;
                        }
                        break;
                }
            }
        }

        public string GetExportString()
        {
            string exportString = "";

            exportString += start + '\t' + id_textstart + id + id_textend + '\t';




            if (!fixedPos)
                exportString += "pos=anywhere" + '\t';
            else
            {
                if (pos.Count == 1)
                {
                    exportString += pos_textstart + pos[0].xPos + ";" + pos[0].yPos + ";" + pos[0].zPos + ";" + pos[0].yaw;

                    if (!string.IsNullOrEmpty(pos[0].probability))
                    {
                        exportString += ";" + pos[0].probability;
                    }
                    exportString += pos_textend + '\t';
                }
                else
                {
                    exportString += pos_textstart + NpcPosUtility.GetVectorsExportString(pos, false) + '\t';
                }
            }
            exportString += total_textstart + total + '\t' + respawn_textstart + respawn + '\t';

            if (!string.IsNullOrEmpty(respawn_rand))
                exportString += respawn_rand_textstart + respawn_rand + respawn_rand_textend + '\t';

            if (!string.IsNullOrEmpty(nickname)) //Strange how nickname comes before pos= in npc_begin, but after respawn_rand on npc_ex_begin
                exportString += nickname_textstart + nickname + nickname_textend + '\t';

            if (!string.IsNullOrEmpty(dbname))
                exportString += dbname_textstart + dbname + dbname_textend + '\t';

            if (!string.IsNullOrEmpty(dbsaving))
                exportString += dbsaving_textstart + dbsaving + dbsaving_textend + '\t';

            if (!string.IsNullOrEmpty(waypoints))
                exportString += waypoints + '\t';

            if (!string.IsNullOrEmpty(ai_parameters))
                exportString += ai_parameters_textstart + ai_parameters + ai_parameters_textend + '\t';

            if (!string.IsNullOrEmpty(privates))
                exportString += privates_textstart + privates + privates_textend + '\t';

            if (!string.IsNullOrEmpty(is_chase_pc))
                exportString += is_chase_pc_textstart + is_chase_pc + '\t';

            if (!string.IsNullOrEmpty(boss_respawn_set))
                exportString += boss_respawn_set_textstart + boss_respawn_set + '\t';

            exportString += end;

            for (int i = 0; i < emptyVariables; i++)
            {
                exportString += '\t';
            }

            return exportString;

        }
    }


    public class SpawnTerritory
    {
        string starttext = "territory_begin";
        public string id;
        string id_textstart = "[";
        string id_textend = "]";

        public List<Npc_Maker> npc_makers_in_terrain;
        public List<Npc_Maker_Ex> npc_ex_makers_in_terrain;

        public List<int> blockCoordinateX;// = 0;
        public List<int> blockCoordinateY;// = 0;

        public List<Vector4> vertices;

        int emptyVariables = 0;

        string end = "territory_end";

        public SpawnTerritory(string[] splitString)
        {
            vertices = new List<Vector4>();
            blockCoordinateX = new List<int>();
            blockCoordinateY = new List<int>();
            npc_makers_in_terrain = new List<Npc_Maker>();
            npc_ex_makers_in_terrain = new List<Npc_Maker_Ex>();

            id = NpcPosUtility.StripExcessServerText(id_textstart, splitString[1], id_textend);

            string[] splitZones = splitString[2].Split(new string[] { "};{" }, StringSplitOptions.None);

            for (int i = 0; i < splitZones.Length; i++)
            {
                splitZones[i] = splitZones[i].Replace("{", "");
                splitZones[i] = splitZones[i].Replace("}", "");

                bool chances;

                vertices.Add(new Vector4(splitZones[i], out chances));
            }
            for (int i = 2; i < splitString.Length; i++)
            {
                if (string.IsNullOrEmpty(splitString[i]))
                {
                    emptyVariables++;
                    continue;
                }
            }

            for (int i = 0; i < vertices.Count; i++)
            {
                if (!blockCoordinateX.Exists(x => x == vertices[i].worldBlockX))
                {
                    blockCoordinateX.Add(vertices[i].worldBlockX);
                }
                if (!blockCoordinateY.Exists(x => x == vertices[i].worldBlockY))
                {
                    blockCoordinateY.Add(vertices[i].worldBlockY);
                }
            }

        }

        public List<Vector2> GrabPolygon(float imageWidth, float imageHeight)
        {
            List<Vector2> vectors = new List<Vector2>();

            for (int i = 0; i < vertices.Count; i++)
            {
                vectors.Add(vertices[i].GrabPictureVector2());
            }

            float unitsPer = 32768f; //- wtf
            float imageScaleX = imageWidth / unitsPer;
            float imageScaleY = imageHeight / unitsPer;

            for (int i = 0; i < vectors.Count; i++)
            {
                vectors[i].xPos *= imageScaleX;
                vectors[i].yPos *= imageScaleY;
            }

            return vectors;
        }

        public string GetExportString()
        {
            string returnString = "";

            string emptyTabs = "";

            for (int i = 0; i < emptyVariables; i++)
            {
                emptyTabs += '\t';
            }

            returnString += starttext + '\t' + id_textstart + id + id_textend + '\t' + NpcPosUtility.GetVectorsExportString(vertices) + '\t' + end + emptyTabs;

            return returnString;

        }

    }

    public class TerritoryDomain
    {
        string starttext = "domain_begin";
        string id;
        string id_textstart = "[";
        string id_textend = "]";

        string domain_id;
        string domain_id_textstart = "domain_id=";

        List<Vector4> vertices;

        string end = "domain_end";

        int emptyVariables = 0;

        public TerritoryDomain(string[] splitString)
        {
            vertices = new List<Vector4>();
            id = id = NpcPosUtility.StripExcessServerText(id_textstart, splitString[1], id_textend);
            domain_id = NpcPosUtility.StripExcessServerText(domain_id_textstart, splitString[2]);

            string[] splitZones = splitString[3].Split(new string[] { "};{" }, StringSplitOptions.None);

            for (int i = 0; i < splitZones.Length; i++)
            {
                splitZones[i] = splitZones[i].Replace("{", "");
                splitZones[i] = splitZones[i].Replace("}", "");

                bool chances;
                vertices.Add(new Vector4(splitZones[i], out chances));
            }

            for (int i = 4; i < splitString.Length; i++)
            {
                if (string.IsNullOrEmpty(splitString[i]))
                    emptyVariables++;
            }

        }

        public string GetExportString()
        {
            string returnString = "";
            returnString += starttext + '\t' + id_textstart + id + id_textend + '\t' + domain_id_textstart + domain_id + '\t' + "{";

            for (int i = 0; i < vertices.Count; i++)
            {
                returnString += "{" + vertices[i].xPos + ";" + vertices[i].yPos + ";" + vertices[i].zPos + ";" + vertices[i].yaw;// + "}";
                if (!string.IsNullOrEmpty(vertices[i].probability))
                {
                    returnString += vertices[i].probability;
                }
                returnString += "}";

                if (i != vertices.Count - 1)
                    returnString += ";";
            }

            returnString += "}" + '\t' + end;

            for (int i = 0; i < emptyVariables; i++)
            {
                returnString += '\t';
            }

            return returnString;


        }
    }

    public class Vector2
    {
        public float xPos;
        public float yPos;

        public Vector2()
        {

        }

        public Vector2(float xPos, float yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
        }
    }

    public class Vector4
    {
        public string xPos;
        public string yPos;
        public string zPos;
        public string yaw;
        public string probability;

        public int worldBlockX = 0;
        public int worldBlockY = 0;

        public Vector4(string xPos, string yPos, string zPos, string orientation, string probability)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.zPos = zPos;
            this.yaw = orientation;
            this.probability = probability;


            int xPosValue = 0;//
            int.TryParse(xPos, out xPosValue);
            int yPosValue = 0;//
            int.TryParse(yPos, out yPosValue);

            worldBlockX = ((327680 + (xPosValue)) / 32768) + 10;
            worldBlockY = ((262144 + (yPosValue)) / 32768) + 10;

        }

        public Vector4(string importString, out bool percentage)
        {
            string[] splitString = importString.Split(';');
            xPos = splitString[0];
            yPos = splitString[1];
            zPos = splitString[2];
            yaw = splitString[3];

            if (splitString.Length > 4)
            {
                this.probability = splitString[4];
                percentage = true;
            }
            else
                percentage = false;

            int xPosValue = 0;//
            int.TryParse(xPos, out xPosValue);
            int yPosValue = 0;//
            int.TryParse(yPos, out yPosValue);

            worldBlockX = ((327680 + (xPosValue)) / 32768) + 10;
            worldBlockY = ((262144 + (yPosValue)) / 32768) + 10;
        }

        public Vector2 GrabPictureVector2()
        {
            float localPosX;
            float localPosY;


            float xPosValue = 0;
            float.TryParse(xPos, out xPosValue);
            float yPosValue = 0;
            float.TryParse(yPos, out yPosValue);

            localPosX = (xPosValue + 327680) - (32768 * (worldBlockX - 10));
            localPosY = (yPosValue + 262144) - (32768 * (worldBlockY - 10));

            return new Vector2(localPosX, localPosY);
        }
    }

    public static class NpcPosUtility
    {
        public static string GetVectorsExportString(List<Vector4> vs, bool addBracket = true)
        {
            string returnString = ""; if (addBracket) returnString += "{";

            for (int i = 0; i < vs.Count; i++)
            {
                returnString += "{" + vs[i].xPos + ";" + vs[i].yPos + ";" + vs[i].zPos + ";" + vs[i].yaw;
                if (!string.IsNullOrEmpty(vs[i].probability))
                {
                    returnString += ";" + vs[i].probability;
                }
                returnString += "}";

                if (i != vs.Count - 1)
                    returnString += ";";
            }

            return returnString + "}";
        }
        public static string StripExcessServerText(string startText, string variable, string endText = "")
        {

            string returnString = variable;
            returnString = returnString.Replace(startText, "");
            if (endText.Length > 0)
                returnString = returnString.Replace(endText, "");

            return returnString;
        }

        public static string ConvertToServerText(string startText, string variable, string endText)
        {
            return startText + variable + endText;
        }

        public static string GetMakerVariableTypeAndValue(string fullString, out Npc_Maker_Variable_Type type)
        {
            string[] splitString = fullString.Split('=');
            if (splitString.Length == 1)
            {
                type = Npc_Maker_Variable_Type.undefined;
                return "";
            }
            else
            {
                type = GetMakerVariableType(splitString[0]);
                return splitString[1];
            }
        }

        public static string GetNPCVariableTypeAndValue(string fullString, out Npc_Variable_Type type)
        {
            string[] splitString = fullString.Split('=');
            if (splitString.Length == 1)
            {
                type = Npc_Variable_Type.undefined;
                return "";
            }
            else
            {
                type = GetNPCVariableType(splitString[0]);
                return splitString[1];
            }
        }

        private static Npc_Maker_Variable_Type GetMakerVariableType(string variableName)
        {
            switch (variableName)
            {
                case "initial_spawn":
                    {
                        return Npc_Maker_Variable_Type.initial_spawn;
                    }
                case "maximum_npc":
                    {
                        return Npc_Maker_Variable_Type.maximum_npc;
                    }
                case "name":
                    {
                        return Npc_Maker_Variable_Type.name;
                    }
                case "ai":
                    {
                        return Npc_Maker_Variable_Type.ai;
                    }
                case "ai_parameters":
                    {
                        return Npc_Maker_Variable_Type.ai_parameters;
                    }
                case "spawn_time":
                    {
                        return Npc_Maker_Variable_Type.spawn_time;
                    }
                case "event_name":
                    {
                        return Npc_Maker_Variable_Type.event_name;
                    }
                case "banned_territory":
                    {
                        return Npc_Maker_Variable_Type.banned_territory;
                    }
                case "flying":
                    {
                        return Npc_Maker_Variable_Type.flying;
                    }
                default:
                    return Npc_Maker_Variable_Type.undefined;
            }
        }

        private static Npc_Variable_Type GetNPCVariableType(string variableName)
        {
            switch (variableName)
            {
                case "total":
                    {
                        return Npc_Variable_Type.total;
                    }
                case "ai_parameters":
                    {
                        return Npc_Variable_Type.ai_parameters;
                    }
                case "ai":
                    {
                        return Npc_Variable_Type.ai;
                    }
                case "dbname":
                    {
                        return Npc_Variable_Type.dbname;
                    }
                case "nickname":
                    {
                        return Npc_Variable_Type.nickname;
                    }
                case "pos":
                    {
                        return Npc_Variable_Type.pos;
                    }
                case "Privates":
                    {
                        return Npc_Variable_Type.privates;
                    }
                case "respawn":
                    {
                        return Npc_Variable_Type.respawn;
                    }
                case "respawn_rand":
                    {
                        return Npc_Variable_Type.respawn_rand;
                    }
                case "dbsaving":
                    {
                        return Npc_Variable_Type.dbsaving;
                    }
                case "is_chase_pc":
                    {
                        return Npc_Variable_Type.is_chase_pc;
                    }
                case "boss_respawn_set":
                    {
                        return Npc_Variable_Type.boss_respawn_set;
                    }
                case "WayPoints":
                    {
                        return Npc_Variable_Type.WayPoints;
                    }
                default:
                    {

                        string couldntfindanythingforthistype = variableName;
                        return Npc_Variable_Type.undefined;
                    }
            }
        }
    }
}


