using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace L2Homage
{
    public class L2H_Drop_Details_Single
    {
        public L2H_Item L2H_Item { get; set; }

        public ItemDrop itemDrop { get; set; }

        public string Name { get { return L2H_Item.client_Itemname.name; } set { Name = value; } }
        public string Min
        {
            get { return itemDrop.minimumAmount; }
            set { itemDrop.minimumAmount = value; }
        }
        public string Max
        {
            get { return itemDrop.maximumAmount; }
            set { itemDrop.maximumAmount = value; }
        }
        public string Chance
        {
            get { return itemDrop.probability; }
            set { itemDrop.probability = value; }
        }
        public string ImagePath { get { return L2H_Parser.GetItemImagePath(L2H_Item.GetIconString()); } }

        public override string ToString()
        {
            return L2H_Item.client_Itemname.name;
        }

        public string DropAmountString
        {
            get
            {
                string amount = "";

                amount = itemDrop.minimumAmount;
                if (Min != Max)
                    amount += "-" + itemDrop.maximumAmount;

                return amount;
            }
        }
    }
}
