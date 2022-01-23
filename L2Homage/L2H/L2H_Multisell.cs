using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Multisell
    {
        public Server_Multisell Server_Multisell { get; set; }
        public string name_ID { get { return Server_Multisell.textid; } set { Server_Multisell.textid = value; } }
        public string ID { get { return Server_Multisell.id; } set { Server_Multisell.id = value; } }
        public bool IsSelected { get; set; }
        public L2H_Multisell Instance { get { return this; } }

        public override string ToString()
        {
            return "ID: " + ID + "\n" + name_ID;
        }

        public List<L2H_MultisellSale> GetItemsForSale
        {
            get
            {
                return Server_Multisell.elements;
            }

        }

        public bool Taxed
        {
            get
            {
                if (Server_Multisell.taxed == null)
                    return false;

                return Server_Multisell.taxed.state;
            }
            set
            {
                string oldValue = "";

                if (Server_Multisell.taxed == null)
                {
                    Server_Multisell.taxed = new MultisellVariable() { typeName = "is_dutyfree", state = value };

                }
                else
                {
                    oldValue = Server_Multisell.taxed.state.ToString();
                    Server_Multisell.taxed.state = value;
                }

                L2H_Log.Instance.Log_Multisell_Change(this, "Taxed", oldValue, value.ToString());
            }
        }

        public bool Show_All
        {
            get
            {
                if (Server_Multisell.showAll == null)
                    return false;

                return Server_Multisell.showAll.state;
            }
            set
            {
                string oldValue = "";


                if (Server_Multisell.showAll == null)
                    Server_Multisell.showAll = new MultisellVariable() { typeName = "is_show_all", state = value };
                else
                {
                    oldValue = Server_Multisell.showAll.state.ToString();
                    Server_Multisell.showAll.state = value;
                }

                L2H_Log.Instance.Log_Multisell_Change(this, "Show All", oldValue, value.ToString());

            }
        }

        public bool Keep_Enchants
        {
            get
            {
                if (Server_Multisell.keepEnchants == null)
                    return false;

                return Server_Multisell.keepEnchants.state;
            }
            set
            {
                string oldValue = "";

                if (Server_Multisell.keepEnchants == null)
                    Server_Multisell.keepEnchants = new MultisellVariable() { typeName = "keep_enchanted", state = value };
                else
                {
                    oldValue = Server_Multisell.keepEnchants.state.ToString();
                    Server_Multisell.keepEnchants.state = value;
                }
                L2H_Log.Instance.Log_Multisell_Change(this, "Keep Enchants", oldValue, value.ToString());
            }
        }

        public bool Show_Variations
        {
            get
            {
                if (Server_Multisell.showVariants == null)
                    return false;

                return Server_Multisell.showVariants.state;
            }
            set
            {
                string oldValue = "";

                if (Server_Multisell.showVariants == null)
                    Server_Multisell.showVariants = new MultisellVariable() { typeName = "show_variation_item", state = value };
                else
                {
                    oldValue = Server_Multisell.showVariants.state.ToString();
                    Server_Multisell.showVariants.state = value;
                }

                L2H_Log.Instance.Log_Multisell_Change(this, "Show Variations", oldValue, value.ToString());
            }
        }


    }
}
