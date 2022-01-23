using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Huntingzone : INotifyPropertyChanged
    {
        public Client_Huntingzone client_Huntingzone; 
        public L2H_Huntingzone Instance { get { return this; } }
        public string ID { get { return client_Huntingzone.ID; } set { client_Huntingzone.ID = value; } }
        public bool IsSelected { get; set; }

        public L2H_Huntingzone(Client_Huntingzone client_Huntingzone)
        {
            this.client_Huntingzone = client_Huntingzone;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override string ToString()
        {
            return client_Huntingzone.name;
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

            float unitsPer = 32768f; //- wtf
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

        public string Name
        {
            get
            {
                return client_Huntingzone.name;
            }
            set
            {
                L2H_Log.Instance.Log_Huntingzone_Change(this, "Name", client_Huntingzone.name, value);
                client_Huntingzone.name = value;
            }
        }
        public string Hunting_Type
        {
            get
            {
                return client_Huntingzone.hunting_type;
            }
            set
            {
                L2H_Log.Instance.Log_Huntingzone_Change(this, "Hunting Type", client_Huntingzone.hunting_type, value);
                client_Huntingzone.hunting_type = value;
            }
        }
        public string Level
        {
            get
            {
                return client_Huntingzone.level;
            }
            set
            {
                L2H_Log.Instance.Log_Huntingzone_Change(this, "Level", client_Huntingzone.level, value);
                client_Huntingzone.level = value;
            }
        }
        public string unk_1
        {
            get
            {
                return client_Huntingzone.unk_1;
            }
            set
            {
                L2H_Log.Instance.Log_Huntingzone_Change(this, "unk1", client_Huntingzone.unk_1, value);
                client_Huntingzone.unk_1 = value;
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
                return client_Huntingzone.loc_x;
            }
            set
            {
                L2H_Log.Instance.Log_Huntingzone_Change(this, "Location X", client_Huntingzone.loc_x, value);
                client_Huntingzone.loc_x = value;
            }
        }
        public string Loc_Y
        {
            get
            {
                return client_Huntingzone.loc_y;
            }
            set
            {
                L2H_Log.Instance.Log_Huntingzone_Change(this, "Location Y", client_Huntingzone.loc_y, value);
                client_Huntingzone.loc_y = value;
            }
        }
        public string Loc_Z
        {
            get
            {
                return client_Huntingzone.loc_z;
            }
            set
            {
                L2H_Log.Instance.Log_Huntingzone_Change(this, "Location Z", client_Huntingzone.loc_z, value);
                client_Huntingzone.loc_z = value;
            }
        }
        public string Extra
        {
            get
            {
                return client_Huntingzone.extra;
            }
            set
            {
                L2H_Log.Instance.Log_Huntingzone_Change(this, "Extra", client_Huntingzone.extra, value);
                client_Huntingzone.extra = value;
            }
        }
        public string Affiliated_Area_ID
        {
            get
            {
                return client_Huntingzone.affiliated_area_id;
            }
            set
            {
                L2H_Log.Instance.Log_Huntingzone_Change(this, "Affiliated Area ID", client_Huntingzone.affiliated_area_id, value);
                client_Huntingzone.affiliated_area_id = value;
            }
        }

    }
}
