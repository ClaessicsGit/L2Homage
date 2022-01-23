using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Initial_Equipment
    {
        public Server_InitialEquipment server_InitialEquipment {get;set;}
        public L2H_Item L2H_Item { get; set; }
        private string amount;
        public string Amount { 
            get
            {
                return amount;
            }
            set
            {
                L2H_Log.Instance.Log_Character_Modify_Initial_Equipment(server_InitialEquipment, L2H_Item, amount);
                amount = value;
            }
        }
        public L2H_Initial_Equipment(Server_InitialEquipment server_InitialEquipment, L2H_Item item, string amount)
        {
            this.server_InitialEquipment = server_InitialEquipment;
            this.L2H_Item = item;
            this.amount = amount;
        }

        public System.Windows.Media.Imaging.BitmapImage GetItemIcon
        {
            get
            {
                if (L2H_Item == null)
                    return L2H_Parser.GetItemImage("");
                else
                    return L2H_Item.GetItemIcon;
            }
        }

        public string Item_Name
        {
            get
            {
                if (L2H_Item == null)
                    return "ITEM NAME NOT FOUND";
                else
                    return L2H_Item.Item_Name;
            }
        }
    }
}
