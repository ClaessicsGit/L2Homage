using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public enum L2H_Droplist_Type { single, multi }

    public class L2H_Droplist
    {
        public string ID { get; set; }
        public Server_Droplist server_Droplist { get; set; }
        public Server_Multi_Droplist server_Multi_Droplist { get; set; }
        public L2H_Droplist_Type Droplist_Type { get; set; }
        public bool IsCustom { get; set; }
        public bool IsSelected { get; set; }
        public List<L2H_NPC> ConnectedNpcs { get; set; }
        public List<L2H_Droplist> ConnectedDroplists { get; set; }
        public L2H_Droplist Instance { get { return this; } }
        public override string ToString()
        {
            switch (Droplist_Type)
            {
                case L2H_Droplist_Type.single:
                    return "(ID: " + server_Droplist.id + ")";
                case L2H_Droplist_Type.multi:
                    return "(ID: " + server_Multi_Droplist.id + ")";
                default:
                    break;
            }

            return base.ToString();
        }

        public void Add_Single_Droplist_To_Multi_Droplist(L2H_Droplist droplist)
        {
            L2H_Log.Instance.Log_Droplist_Add_Single_Droplist_To_Multi_Droplist(this, droplist);

            ConnectedDroplists.Add(droplist);
            droplist.ConnectedDroplists.Add(this);
            server_Multi_Droplist.separateDroplistChances.Add("5");
            server_Multi_Droplist.separateDroplistIDs.Add(droplist.ID);
        }

        public System.Windows.Media.SolidColorBrush GetDroplistColor
        {
            get
            {
                switch (Droplist_Type)
                {
                    case L2H_Droplist_Type.single:
                        return new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(L2H_Constants.Color_Single_Droplist));
                    case L2H_Droplist_Type.multi:
                        return new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(L2H_Constants.Color_Multi_Droplist));
                    default:
                        return new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#f4f4f4"));
                }
            }
        }
    }
}
