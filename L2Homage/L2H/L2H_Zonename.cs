using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Zonename : INotifyPropertyChanged
    {
        public Client_Zonename client_Zonename { get; set; }
        public L2H_Zonename Instance { get { return this; } }
        public L2H_Zonename(Client_Zonename client_Zonename)
        {
            this.client_Zonename = client_Zonename;
        }
        public bool IsSelected { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override string ToString()
        {
            return client_Zonename.zone_name;
        }

        public string ID { get { return client_Zonename.nbr; } set { client_Zonename.nbr = value; } }
        public string Zone_Color_ID
        {
            get
            {
                return client_Zonename.zone_color_id;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "Zone Color ID", Zone_Color_ID, value);
                client_Zonename.zone_color_id = value;
            }
        }
        public string X_World_Grid
        {
            get
            {
                return client_Zonename.x_world_grid;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "X World Grid", X_World_Grid, value);
                client_Zonename.x_world_grid = value;
            }
        }
        public string Y_World_Grid
        {
            get
            {
                return client_Zonename.y_world_grid;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "Y World Grid", Y_World_Grid, value);
                client_Zonename.y_world_grid = value;
            }
        }
        public string Top_Z
        {
            get
            {
                return client_Zonename.top_z;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "Top Z", Top_Z, value);
                client_Zonename.top_z = value;
            }
        }
        public string Bottom_Z
        {
            get
            {
                return client_Zonename.bottom_z;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "Bottom Z", Bottom_Z, value);
                client_Zonename.bottom_z = value;
            }
        }
        public string Zone_Name
        {
            get
            {
                return client_Zonename.zone_name;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "Zone Name", Zone_Name, value);
                client_Zonename.zone_name = value;
            }
        }
        public string Coord_0
        {
            get
            {
                return client_Zonename.coord_0;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "+Button X Position", Coord_0, value);
                client_Zonename.coord_0 = value;
            }
        }
        public string Coord_1
        {
            get
            {
                return client_Zonename.coord_1;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "+Button Y Position", Coord_1, value);
                client_Zonename.coord_1 = value;
            }
        }
        public string Coord_2
        {
            get
            {
                return client_Zonename.coord_2;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "Center X Position", Coord_2, value);
                client_Zonename.coord_2 = value;
            }
        }
        public string Coord_3
        {
            get
            {
                return client_Zonename.coord_3;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "Center Y Position", Coord_3, value);
                client_Zonename.coord_3 = value;
            }
        }
        public string Coord_4
        {
            get
            {
                return client_Zonename.coord_4;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "Map Width", Coord_4, value);
                client_Zonename.coord_4 = value;
            }
        }
        public string Coord_5
        {
            get
            {
                return client_Zonename.coord_5;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "Map Height", Coord_5, value);
                client_Zonename.coord_5 = value;
            }
        }
        public string Map_Zoom
        {
            get
            {
                return client_Zonename.unk02;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "Map Zoom", Map_Zoom, value);
                client_Zonename.unk02 = value;
            }
        }
        public string Map_ID
        {
            get
            {
                return client_Zonename.map;
            }
            set
            {
                L2H_Log.Instance.Log_Zonename_Change(this, "Map ID", Map_ID, value);
                client_Zonename.map = value;
            }
        }
        public string Dupa
        {
            get
            {
                return client_Zonename.dupa;
            }
            set
            {
                client_Zonename.dupa = value;
            }
        }
    }
}
