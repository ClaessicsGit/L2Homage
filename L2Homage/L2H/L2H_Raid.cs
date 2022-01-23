using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Raid : INotifyPropertyChanged
    {
        public Client_Raid client_Raid { get; set; }
        public L2H_NPC L2H_NPC { get; set; }
        public L2H_Raid Instance { get { return this; } }
        public L2H_Raid(Client_Raid client_Raid)
        {
            this.client_Raid = client_Raid;
        }
        public bool IsSelected { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override string ToString()
        {
            return client_Raid.npc_id;
        }

        public void Set_New_L2H_NPC(L2H_NPC newNpc)
        {
            L2H_NPC = newNpc;
            client_Raid.npc_id = newNpc.NPC_ID;
            client_Raid.npc_level = newNpc.NPC_Level;
            OnPropertyChanged("NPC_Name");
            OnPropertyChanged("NPC_ID"); 
            OnPropertyChanged("NPC_Level");
        }

        public string ID
        {
            get { return client_Raid.ID; }
            set
            { ID = value; }
        }
        public string NPC_Name
        {
            get
            {
                if (L2H_NPC != null)
                    return L2H_NPC.NPC_Name;
                else
                    return "NPC NOT FOUND";
            }
        }
        public string NPC_ID
        {
            get { return L2H_NPC.NPC_ID; }
        }
        public string NPC_Level
        {
            get { return L2H_NPC.NPC_Level; }
        }
        public string Affiliated_Area_ID
        {
            get
            {
                return client_Raid.affiliated_area_id;
            }
            set
            {
                L2H_Log.Instance.Log_Raid_Change(this, "Affiliated Area ID", Affiliated_Area_ID, value);
                client_Raid.affiliated_area_id = value;
            }
        }
        public int World_Block_X
        {
            get
            {
                float x = 0;
                float.TryParse(Loc_X, out x);

                return (int)((327680 + (x)) / 32768) + 10;
            }
        }
        public int World_Block_Y
        {
            get
            {
                float y = 0;
                float.TryParse(Loc_Y, out y);

                return (int)((262144 + (y)) / 32768) + 10;
            }
        }
        public string Loc_X
        {
            get
            {
                return client_Raid.loc_x;
            }
            set
            {
                L2H_Log.Instance.Log_Raid_Change(this, "Location X", Loc_X, value);
                client_Raid.loc_x = value;
            }
        }
        public string Loc_Y
        {
            get
            {
                return client_Raid.loc_y;
            }
            set
            {
                L2H_Log.Instance.Log_Raid_Change(this, "Location Y", Loc_Y, value);
                client_Raid.loc_y = value;
            }
        }
        public string Loc_Z
        {
            get
            {
                return client_Raid.loc_z;
            }
            set
            {
                L2H_Log.Instance.Log_Raid_Change(this, "Location Z", Loc_Z, value);
                client_Raid.loc_z = value;
            }
        }
        public string Description
        {
            get
            {
                return client_Raid.raid_desc;
            }
            set
            {
                L2H_Log.Instance.Log_Raid_Change(this, "Description", Description, value);
                client_Raid.raid_desc = value;
            }
        }
        public List<Vector2> GrabPolygon(float imageWidth, float imageHeight)
        {
            List<Vector2> vectors = new List<Vector2>();

            float localPosX;
            float localPosY;

            float xPosValue = 0;
            float.TryParse(Loc_X, out xPosValue);
            float yPosValue = 0;
            float.TryParse(Loc_Y, out yPosValue);

            localPosX = (xPosValue + 327680) - (32768 * (World_Block_X - 10));
            localPosY = (yPosValue + 262144) - (32768 * (World_Block_Y - 10));

            vectors.Add(new Vector2(localPosX - 5f, localPosY - 5f));
            vectors.Add(new Vector2(localPosX - 5f, localPosY + 5f));
            vectors.Add(new Vector2(localPosX + 5f, localPosY + 5f));
            vectors.Add(new Vector2(localPosX + 5f, localPosY - 5f));

            float unitsPer = 32768f;
            float imageScaleX = imageWidth / unitsPer;
            float imageScaleY = imageHeight / unitsPer;

            for (int i = 0; i < vectors.Count; i++)
            {
                vectors[i].xPos *= imageScaleX;
                vectors[i].yPos *= imageScaleY;
            }

            vectors[0].xPos -= 5f;
            vectors[0].yPos -= 5f;
            vectors[1].xPos -= 5f;
            vectors[1].yPos += 5f;
            vectors[2].xPos += 5f;
            vectors[2].yPos += 5f;
            vectors[3].xPos += 5f;
            vectors[3].yPos -= 5f;

            return vectors;
        }
    }
}
