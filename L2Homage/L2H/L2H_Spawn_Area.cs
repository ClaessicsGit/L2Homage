using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace L2Homage
{
    public class L2H_Spawn_Area
    {
        public bool IsCustom { get; set; }

        public bool IsSelected { get; set; }

        public SpawnTerritory Territory { get; set; }
        public TerritoryDomain Domain { get; set; }

        private List<L2H_Spawn_NPC_Maker> _NPC_Makers;

        public string Area_Name { get { return Territory.id; } set { Territory.id = value; } } //This needs to be updated in NPC_Makers too I think


        public List<L2H_Spawn_NPC_Maker> NPC_Makers
        {
            get
            {
                if (_NPC_Makers == null)
                {
                    _NPC_Makers = new List<L2H_Spawn_NPC_Maker>();

                    for (int i = 0; i < Territory.npc_makers_in_terrain.Count; i++)
                    {
                        _NPC_Makers.Add(new L2H_Spawn_NPC_Maker() { Npc_Maker = Territory.npc_makers_in_terrain[i] });
                    }
                    for (int i = 0; i < Territory.npc_ex_makers_in_terrain.Count; i++)
                    {
                        _NPC_Makers.Add(new L2H_Spawn_NPC_Maker() { Npc_Maker_Ex = Territory.npc_ex_makers_in_terrain[i] });
                    }
                }

                return _NPC_Makers;
            }
        }
    }



    public class L2H_Spawn_NPC_Maker
    {
        public Npc_Maker Npc_Maker { get; set; }
        public Npc_Maker_Ex Npc_Maker_Ex { get; set; }
        public bool IsSelected { get; set; }

        //Shared Properties
        public string Maker_Type
        {
            get
            {
                if (Npc_Maker != null)
                    return "Basic";
                else
                    return "Extended";
            }
        }
        public string Maker_Name
        {
            get
            {
                if (Npc_Maker != null)
                {
                    return "Basic NPC Maker";
                }
                else
                {
                    return Npc_Maker_Ex.name;
                }
            }
            set
            {
                if (Npc_Maker != null)
                    return;
                else
                {
                    L2H_Log.Instance.Log_Spawn_Maker_Changed(this, "Maker Name", Npc_Maker_Ex.name, value);
                    Npc_Maker_Ex.name = value;
                }
            }
        }

        public string Maker_Max_Spawns
        {
            get
            {
                if (Npc_Maker != null)
                {
                    return Npc_Maker.maximum_npc;
                }
                else
                {
                    return Npc_Maker_Ex.maximum_npc;
                }
            }
            set
            {
                if (Npc_Maker != null)
                {
                    L2H_Log.Instance.Log_Spawn_Maker_Changed(this, "Maximum Spawns", Npc_Maker.maximum_npc, value);
                    Npc_Maker.maximum_npc = value;
                }
                else
                {
                    L2H_Log.Instance.Log_Spawn_Maker_Changed(this, "Maximum Spawns", Npc_Maker_Ex.maximum_npc, value);
                    Npc_Maker_Ex.maximum_npc = value;
                }
            }
        }

        //NPC_Maker properties
        public string Maker_Initial_Spawns
        {
            get
            {
                return Npc_Maker.initialSpawn;
            }
            set
            {
                L2H_Log.Instance.Log_Spawn_Maker_Changed(this, "Initial Spawns", Npc_Maker.initialSpawn, value);
                Npc_Maker.initialSpawn = value;
            }
        }
        public string Maker_Spawn_Time
        {
            get
            {
                return Npc_Maker.spawnTime;

            }
            set
            {
                L2H_Log.Instance.Log_Spawn_Maker_Changed(this, "Spawn Time", Npc_Maker.spawnTime, value);
                Npc_Maker.spawnTime = value;
            }
        }

        public string Maker_Event_Name
        {
            get
            {
                return Npc_Maker.event_name;

            }
            set
            {
                L2H_Log.Instance.Log_Spawn_Maker_Changed(this, "Event Name", Npc_Maker.event_name, value);
                Npc_Maker.event_name = value;
            }
        }

        //NPC_Maker_Ex properties
        public string Maker_AI
        {
            get
            {
                return Npc_Maker_Ex.ai;

            }
            set
            {
                L2H_Log.Instance.Log_Spawn_Maker_Changed(this, "Maker AI", Npc_Maker_Ex.ai, value);
                Npc_Maker_Ex.ai = value;
            }
        }
        public string Maker_AI_Parameters
        {
            get
            {
                return Npc_Maker_Ex.ai_parameters;

            }
            set
            {
                L2H_Log.Instance.Log_Spawn_Maker_Changed(this, "Maker AI Parameters", Npc_Maker_Ex.ai_parameters, value);
                Npc_Maker_Ex.ai_parameters = value;
            }
        }
        public bool Maker_Flying
        {
            get
            {
                if (Npc_Maker_Ex.flying == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Spawn_Maker_Changed(this, "Flying", "False", "True");
                    Npc_Maker_Ex.flying = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Spawn_Maker_Changed(this, "Flying", "True", "False");
                    Npc_Maker_Ex.flying = "0";
                }
            }
        }
        public string Maker_Banned_Territory
        {
            get
            {
                return Npc_Maker_Ex.banned_territory;

            }
            set
            {
                L2H_Log.Instance.Log_Spawn_Maker_Changed(this, "Banned Territory", Npc_Maker_Ex.banned_territory, value);
                Npc_Maker_Ex.banned_territory = value;
            }
        }
    }

    public class L2H_Spawn_NPC_Maker_NPC_Spawn
    {
        public L2H_Spawn_NPC_Maker Npc_Maker { get; set; }
        public Npc_Begin npc_Begin { get; set; }
        public Npc_Ex_Begin npc_Ex_Begin { get; set; }
        public L2H_NPC L2H_Npc { get; set; }
        public bool IsSelected { get; set; }

        private List<L2H_Spawn_NPC_Maker_NPC_Spawn_Fixed_Position> _fixed_Positions;
        private int _Fixed_Position_Index = 0;

        public BitmapImage GetNPCImage { get { return L2H_Npc.GetNPCImage(); } }
        public string NPC_Name
        {
            get
            {
                return L2H_Npc.client_Npcname.name + "\n(ID:" + L2H_Npc.ID + " Lv:" + L2H_Npc.server_Npcdata.level + ")";
            }
        }
        public string Max_Spawns
        {
            get
            {
                if (npc_Begin != null)
                    return npc_Begin.total;
                else
                    return npc_Ex_Begin.total;
            }
            set
            {

                if (npc_Begin != null)
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Max Spawns", npc_Begin.total, value);
                    npc_Begin.total = value;
                }
                else
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Max Spawns", npc_Ex_Begin.total, value);
                    npc_Ex_Begin.total = value;
                }
            }
        }
        public string Respawn_Time
        {
            get
            {
                if (npc_Begin != null)
                    return npc_Begin.respawn.Replace("sec", "").Replace("min", "").Replace("hour", "");
                else
                    return npc_Ex_Begin.respawn.Replace("sec", "").Replace("min", "").Replace("hour", "");
            }
            set
            {
                if (npc_Begin != null)
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Respawn Time", npc_Begin.respawn, value);
                    npc_Begin.respawn = value;
                }
                else
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Respawn Time", npc_Ex_Begin.respawn, value);
                    npc_Ex_Begin.respawn = value;
                }
            }
        }

        public int Respawn_Time_Type
        {
            get
            {
                if (npc_Begin != null)
                {

                    if (npc_Begin.respawn.Contains("sec"))
                        return 1;
                    else if (npc_Begin.respawn.Contains("min"))
                        return 2;
                    else if (npc_Begin.respawn.Contains("hour"))
                        return 3;
                    else
                        return 0;
                }
                else
                {
                    if (npc_Ex_Begin.respawn.Contains("sec"))
                        return 1;
                    else if (npc_Ex_Begin.respawn.Contains("min"))
                        return 2;
                    else if (npc_Ex_Begin.respawn.Contains("hour"))
                        return 3;
                    else
                        return 0;
                }
            }
            set
            {
                if (npc_Begin != null)
                {
                    string oldValue = "";
                    oldValue = npc_Begin.respawn;

                    if (value == 1)
                        npc_Begin.respawn = npc_Begin.respawn.Replace("min", "sec").Replace("hour", "sec");
                    else if (value == 2)
                        npc_Begin.respawn = npc_Begin.respawn.Replace("sec", "min").Replace("hour", "min");
                    else if (value == 3)
                        npc_Begin.respawn = npc_Begin.respawn.Replace("min", "hour").Replace("sec", "hour");
                    else
                        npc_Begin.respawn = "";

                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Respawn Time Type", oldValue, npc_Begin.respawn);
                }
                else
                {
                    string oldValue = "";
                    oldValue = npc_Ex_Begin.respawn;

                    if (value == 1)
                        npc_Ex_Begin.respawn = npc_Ex_Begin.respawn.Replace("min", "sec").Replace("hour", "sec");
                    else if (value == 2)
                        npc_Ex_Begin.respawn = npc_Ex_Begin.respawn.Replace("sec", "min").Replace("hour", "min");
                    else if (value == 3)
                        npc_Ex_Begin.respawn = npc_Ex_Begin.respawn.Replace("min", "hour").Replace("sec", "hour");
                    else
                        npc_Ex_Begin.respawn = "";

                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Respawn Time Type", oldValue, npc_Ex_Begin.respawn);
                }
            }
        }
        public string Respawn_Deviance
        {
            get
            {
                if (npc_Begin != null)
                {
                    return npc_Begin.respawn_rand.Replace("sec", "").Replace("min", "").Replace("hour", "");
                }
                else
                {
                    return npc_Ex_Begin.respawn_rand.Replace("sec", "").Replace("min", "").Replace("hour", "");
                }
            }
            set
            {
                if (npc_Begin != null)
                {

                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Respawn Random", npc_Begin.respawn_rand, value);
                    npc_Begin.respawn_rand = value;
                }
                else
                {

                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Respawn Random", npc_Ex_Begin.respawn_rand, value);
                    npc_Ex_Begin.respawn_rand = value;
                }
            }
        }
        public int Respawn_Deviance_Type
        {
            get
            {
                if (npc_Begin != null)
                {
                    if (npc_Begin.respawn_rand.Contains("sec"))
                        return 1;
                    else if (npc_Begin.respawn_rand.Contains("min"))
                        return 2;
                    else if (npc_Begin.respawn_rand.Contains("hour"))
                        return 3;
                    else
                        return 0;
                }
                else
                {
                    if (npc_Ex_Begin.respawn_rand.Contains("sec"))
                        return 1;
                    else if (npc_Ex_Begin.respawn_rand.Contains("min"))
                        return 2;
                    else if (npc_Ex_Begin.respawn_rand.Contains("hour"))
                        return 3;
                    else
                        return 0;
                }
            }
            set
            {
                if (npc_Begin != null)
                {
                    string oldValue = "";
                    oldValue = npc_Begin.respawn_rand;


                    if (value == 1)
                        npc_Begin.respawn_rand = npc_Begin.respawn_rand.Replace("min", "sec").Replace("hour", "sec");
                    else if (value == 2)
                        npc_Begin.respawn_rand = npc_Begin.respawn_rand.Replace("sec", "min").Replace("hour", "min");
                    else if (value == 3)
                        npc_Begin.respawn_rand = npc_Begin.respawn_rand.Replace("min", "hour").Replace("sec", "hour");
                    else
                        npc_Begin.respawn_rand = "";

                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Respawn Deviance Type", oldValue, npc_Begin.respawn_rand);
                }
                else
                {
                    string oldValue = "";
                    oldValue = npc_Ex_Begin.respawn_rand;
                    if (value == 1)
                        npc_Ex_Begin.respawn_rand = npc_Ex_Begin.respawn_rand.Replace("min", "sec").Replace("hour", "sec");
                    else if (value == 2)
                        npc_Ex_Begin.respawn_rand = npc_Ex_Begin.respawn_rand.Replace("sec", "min").Replace("hour", "min");
                    else if (value == 3)
                        npc_Ex_Begin.respawn_rand = npc_Ex_Begin.respawn_rand.Replace("min", "hour").Replace("sec", "hour");
                    else
                        npc_Ex_Begin.respawn_rand = "";
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Respawn Deviance Type", oldValue, npc_Ex_Begin.respawn_rand);
                }
            }
        }

        public bool Fixed_Position
        {
            get
            {
                if (npc_Begin != null)
                    return npc_Begin.fixedPos;
                else
                    return npc_Ex_Begin.fixedPos;
            }
            set
            {
                if (npc_Begin != null)
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Fixed Position", npc_Begin.fixedPos.ToString(), value.ToString());
                    npc_Begin.fixedPos = value;
                }
                else
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Fixed Position", npc_Ex_Begin.fixedPos.ToString(), value.ToString());
                    npc_Ex_Begin.fixedPos = value;
                }
            }
        }
        #region fixed position properties
        public List<L2H_Spawn_NPC_Maker_NPC_Spawn_Fixed_Position> Fixed_Positions
        {
            get
            {
                if (_fixed_Positions == null)
                {
                    _fixed_Positions = new List<L2H_Spawn_NPC_Maker_NPC_Spawn_Fixed_Position>();

                    if (npc_Begin != null)
                    {
                        for (int i = 0; i < npc_Begin.pos.Count; i++)
                        {
                            _fixed_Positions.Add(new L2H_Spawn_NPC_Maker_NPC_Spawn_Fixed_Position()
                            {
                                ID = i,
                                fixed_Position_X = npc_Begin.pos[i].xPos,
                                fixed_Position_Y = npc_Begin.pos[i].yPos,
                                fixed_Position_Z = npc_Begin.pos[i].zPos,
                                fixed_Position_Yaw = npc_Begin.pos[i].yaw,
                                fixed_Position_Chance = npc_Begin.pos[i].probability,
                                parent = this
                            });
                        }
                    }
                    else if (npc_Ex_Begin != null)
                    {
                        for (int i = 0; i < npc_Ex_Begin.pos.Count; i++)
                        {
                            _fixed_Positions.Add(new L2H_Spawn_NPC_Maker_NPC_Spawn_Fixed_Position()
                            {
                                ID = i,
                                fixed_Position_X = npc_Ex_Begin.pos[i].xPos,
                                fixed_Position_Y = npc_Ex_Begin.pos[i].yPos,
                                fixed_Position_Z = npc_Ex_Begin.pos[i].zPos,
                                fixed_Position_Yaw = npc_Ex_Begin.pos[i].yaw,
                                fixed_Position_Chance = npc_Ex_Begin.pos[i].probability,
                                parent = this
                            });
                        }
                    }
                }

                return _fixed_Positions;
            }
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    if (npc_Begin != null)
                    {
                        if (npc_Begin.pos.Count < i + 1)
                            npc_Begin.pos.Add(new Vector4(value[i].fixed_Position_X, value[i].fixed_Position_Y, value[i].fixed_Position_Z, value[i].fixed_Position_Yaw, value[i].fixed_Position_Chance));
                        else
                        {
                            npc_Begin.pos[i].xPos = value[i].fixed_Position_X;
                            npc_Begin.pos[i].yPos = value[i].fixed_Position_Y;
                            npc_Begin.pos[i].zPos = value[i].fixed_Position_Z;
                            npc_Begin.pos[i].yaw = value[i].fixed_Position_Yaw;
                            npc_Begin.pos[i].probability = value[i].fixed_Position_Chance;
                        }
                    }
                    else if (npc_Ex_Begin != null)
                    {
                        if (npc_Ex_Begin.pos.Count < i + 1)
                            npc_Ex_Begin.pos.Add(new Vector4(value[i].fixed_Position_X, value[i].fixed_Position_Y, value[i].fixed_Position_Z, value[i].fixed_Position_Yaw, value[i].fixed_Position_Chance));
                        else
                        {
                            npc_Ex_Begin.pos[i].xPos = value[i].fixed_Position_X;
                            npc_Ex_Begin.pos[i].yPos = value[i].fixed_Position_Y;
                            npc_Ex_Begin.pos[i].zPos = value[i].fixed_Position_Z;
                            npc_Ex_Begin.pos[i].yaw = value[i].fixed_Position_Yaw;
                            npc_Ex_Begin.pos[i].probability = value[i].fixed_Position_Chance;
                        }
                    }
                }

                _fixed_Positions = value;
            }

        }

        public int Fixed_Position_Index
        {
            get
            {
                return _Fixed_Position_Index;
            }
            set
            {
                _Fixed_Position_Index = value;
            }
        }


        #endregion

        public string DB_Name
        {
            get
            {
                if (npc_Begin != null)
                    return npc_Begin.dbname;
                else
                    return npc_Ex_Begin.dbname;
            }
            set
            {
                if (npc_Begin != null)
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Database Name", npc_Begin.dbname, value);

                    npc_Begin.dbname = value;
                }
                else
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Database Name", npc_Ex_Begin.dbname, value);

                    npc_Ex_Begin.dbname = value;
                }
            }
        }
        public string DB_Save
        {
            get
            {
                if (npc_Begin != null)
                    return npc_Begin.dbsaving;
                else
                    return npc_Ex_Begin.dbsaving;
            }
            set
            {
                if (npc_Begin != null)
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Database Saving", npc_Begin.dbsaving, value);
                    npc_Begin.dbsaving = value;
                }
                else
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Database Saving", npc_Ex_Begin.dbsaving, value);
                    npc_Ex_Begin.dbsaving = value;
                }
            }
        }
        public bool Boss_Respawn
        {
            get
            {
                if (npc_Begin != null)
                {
                    if (npc_Begin.boss_respawn_set == "yes")
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (npc_Ex_Begin.boss_respawn_set == "yes")
                        return true;
                    else
                        return false;
                }
            }
            set
            {
                if (npc_Begin != null)
                {
                    if (value == true)
                    {
                        L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Boss Respawn", "False", "True");
                        npc_Begin.boss_respawn_set = "yes";
                    }
                    else
                    {
                        L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Boss Respawn", "True", "False");

                        npc_Begin.boss_respawn_set = "";
                    }
                }
                else
                {
                    if (value == true)
                    {
                        L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Boss Respawn", "False", "True");
                        npc_Ex_Begin.boss_respawn_set = "yes";
                    }
                    else
                    {
                        L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Boss Respawn", "True", "False");

                        npc_Ex_Begin.boss_respawn_set = "";
                    }
                }
            }
        }
        public string Privates
        {
            get
            {
                if (npc_Begin != null)
                    return npc_Begin.privates;
                else
                    return npc_Ex_Begin.privates;
            }
            set
            {
                if (npc_Begin != null)
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Minions", npc_Begin.privates, value);

                    npc_Begin.privates = value;
                }
                else
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Minions", npc_Ex_Begin.privates, value);

                    npc_Ex_Begin.privates = value;
                }
            }
        }
        public string Nickname
        {
            get
            {
                if (npc_Begin != null)
                    return npc_Begin.nickname;
                else
                    return npc_Ex_Begin.nickname;
            }
            set
            {
                if (npc_Begin != null)
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Nickname", npc_Begin.nickname, value);

                    npc_Begin.nickname = value;
                }
                else
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Nickname", npc_Ex_Begin.nickname, value);

                    npc_Ex_Begin.nickname = value;
                }
            }
        }
        public string Maker_AI
        {
            get
            {
                if (npc_Begin != null)
                    return npc_Begin.ai;
                else
                    return npc_Ex_Begin.ai;
            }
            set
            {
                if (npc_Begin != null)
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Maker AI", npc_Begin.ai, value);
                    npc_Begin.ai = value;
                }
                else
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Maker AI", npc_Ex_Begin.ai, value);
                    npc_Ex_Begin.ai = value;
                }
            }
        }
        public string Maker_AI_Parameters
        {
            get
            {
                if (npc_Begin != null)
                    return npc_Begin.ai_parameters;
                else
                    return npc_Ex_Begin.ai_parameters;
            }
            set
            {
                if (npc_Begin != null)
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Maker AI Parameters", npc_Begin.ai_parameters, value);

                    npc_Begin.ai_parameters = value;
                }
                else
                {
                    L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Maker AI Parameters", npc_Ex_Begin.ai_parameters, value);

                    npc_Ex_Begin.ai_parameters = value;
                }
            }
        }
        public string Chase_Player
        {
            get
            {
                return npc_Ex_Begin.is_chase_pc;
            }
            set
            {
                L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(this, "Chase Player", npc_Ex_Begin.is_chase_pc, value);
                npc_Ex_Begin.is_chase_pc = value;
            }
        }

    }

    public class L2H_Spawn_NPC_Maker_NPC_Spawn_Fixed_Position
    {
        public L2H_Spawn_NPC_Maker_NPC_Spawn parent;
        public int ID { get; set; }
        public string fixed_Position_X;
        public string fixed_Position_Y;
        public string fixed_Position_Z;
        public string fixed_Position_Yaw;
        public string fixed_Position_Chance;
        public string Fixed_Position_X
        {
            get
            {
                return fixed_Position_X;
            }
            set
            {
                L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(parent, "Fixed Position X", fixed_Position_X, value);
                if (parent.npc_Begin != null)
                {
                    parent.npc_Begin.pos[ID].xPos = value;
                }
                else
                {
                    parent.npc_Ex_Begin.pos[ID].xPos = value;
                }

                fixed_Position_X = value;
            }
        }
        public string Fixed_Position_Y
        {
            get
            {
                return fixed_Position_Y;
            }
            set
            {
                L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(parent, "Fixed Position Y", fixed_Position_Y, value);
                if (parent.npc_Begin != null)
                {
                    parent.npc_Begin.pos[ID].yPos = value;
                }
                else
                {
                    parent.npc_Ex_Begin.pos[ID].yPos = value;
                }
                fixed_Position_Y = value;
            }
        }
        public string Fixed_Position_Z
        {
            get
            {
                return fixed_Position_Z;
            }
            set
            {
                L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(parent, "Fixed Position Z", fixed_Position_Z, value);
                if (parent.npc_Begin != null)
                {
                    parent.npc_Begin.pos[ID].zPos = value;
                }
                else
                {
                    parent.npc_Ex_Begin.pos[ID].zPos = value;
                }
                fixed_Position_Z = value;
            }
        }
        public string Fixed_Position_Yaw
        {
            get
            {
                return fixed_Position_Yaw;
            }
            set
            {
                L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(parent, "Fixed Position Yaw", fixed_Position_Yaw, value);
                if (parent.npc_Begin != null)
                {
                    parent.npc_Begin.pos[ID].yaw = value;
                }
                else
                {
                    parent.npc_Ex_Begin.pos[ID].yaw = value;
                }
                fixed_Position_Yaw = value;
            }
        }
        public string Fixed_Position_Chance
        {
            get
            {
                return fixed_Position_Chance;
            }
            set
            {
                L2H_Log.Instance.Log_Spawn_NPC_Spawn_Changed(parent, "Fixed Position Chance", fixed_Position_Chance, value);
                if (parent.npc_Begin != null)
                {
                    parent.npc_Begin.pos[ID].probability = value;
                }
                else
                {
                    parent.npc_Ex_Begin.pos[ID].probability = value;
                }
                fixed_Position_Chance = value;
            }
        }

        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
