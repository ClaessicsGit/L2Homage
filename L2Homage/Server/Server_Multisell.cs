using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;

namespace L2Homage
{
    public class Server_Multisell
    {

        string startText = "MultiSell_begin";
        string id_textstart = "[";
        public string textid;
        string id_textend = "]";
        public string id;
        string endText = "MultiSell_end";

        public L2H_Multisell parent;


        public List<MultisellVariable> variables;

        public MultisellVariable taxed;
        public MultisellVariable showAll;
        public MultisellVariable keepEnchants;
        public MultisellVariable showVariants;
        public MultisellVariable requiredNPCs;
        public List<L2H_NPC> requiredL2H_NPCs;


        public List<L2H_MultisellSale> elements;


        public Server_Multisell(L2H_Multisell parentMultisell, string name)
        {
            parent = parentMultisell;
            elements = new List<L2H_MultisellSale>();
            variables = new List<MultisellVariable>();
            requiredL2H_NPCs = new List<L2H_NPC>();

            textid = name;

        }
        public Server_Multisell(L2H_Multisell parentMultisell, string[] sourceStrings)
        {
            parent = parentMultisell;
            elements = new List<L2H_MultisellSale>();
            variables = new List<MultisellVariable>();
            requiredL2H_NPCs = new List<L2H_NPC>();


            //First grabbing IDs, both number and text
            string strippedID = "";
            int strippedIDStartIndex = sourceStrings[0].IndexOf('[');
            strippedID = sourceStrings[0].Remove(0, strippedIDStartIndex + 1);
            int strippedIDEndIndex = strippedID.IndexOf(']');
            strippedID = strippedID.Remove(strippedIDEndIndex, strippedID.Length - strippedIDEndIndex);
            textid = strippedID;



            if (sourceStrings[0][sourceStrings[0].Length - 1] == '\t')
            {
                sourceStrings[0] = RemoveEmptyTabsAtEndOfLine(sourceStrings[0]);
            }

            if (sourceStrings[0].Contains('\t'))
            {
                string[] splitIDstring = sourceStrings[0].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                id = splitIDstring[splitIDstring.Length - 1];
            }
            else
            {
                string[] splitIDstring = sourceStrings[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                id = splitIDstring[splitIDstring.Length - 1];
            }


            //Then moving on to grab variables
            int numberOfEmptyLines = 0;

            for (int i = 1; i < sourceStrings.Length; i++)
            {
                if (sourceStrings[i].Contains("selllist={"))
                {
                    break;
                }
                else
                {
                    if (!string.IsNullOrEmpty(sourceStrings[i]))
                    {
                        MultisellVariable newVariable = new MultisellVariable(sourceStrings[i]);
                        if (newVariable != null)
                            if (!string.IsNullOrEmpty(newVariable.typeName))
                            {
                                variables.Add(newVariable);
                                switch (newVariable.typeName)
                                {
                                    case "is_dutyfree":
                                        taxed = newVariable;
                                        break;
                                    case "is_show_all":
                                        showVariants = newVariable;
                                        break;
                                    case "keep_enchanted":
                                        keepEnchants = newVariable;
                                        break;
                                    case "show_variation_item":
                                        showVariants = newVariable;
                                        break;
                                    case "required_npc":
                                        requiredNPCs = newVariable;
                                        break;
                                    default:
                                        break;
                                }
                            }
                    }
                    else
                    {
                        numberOfEmptyLines++;
                    }

                }

            }

            //Then grabbing all multisell elements
            for (int i = 1 + variables.Count + numberOfEmptyLines; i < sourceStrings.Length; i++)
            {

                if (!string.IsNullOrEmpty(sourceStrings[i].Replace("\t", "")))
                {
                    if (sourceStrings[i][0] == '}')
                        break;
                    L2H_MultisellSale newElement = new L2H_MultisellSale(parentMultisell, sourceStrings[i]);

                    if (newElement.sale_Items.Count > 0)
                        elements.Add(newElement);


                }

            }

        }

        string RemoveEmptyTabsAtEndOfLine(string source)
        {
            string returnString = source.Remove(source.Length - 1, 1);

            if (returnString[returnString.Length - 1] == '\t')
                returnString = RemoveEmptyTabsAtEndOfLine(returnString);

            return returnString;


        }

        public string GetExportString()
        {
            string returnString = @"//";
            returnString += '\n';

            returnString = startText + '\t' + id_textstart + textid + id_textend + '\t' + id + '\n';

            string NPC_IDs = "";
            if (requiredL2H_NPCs.Count > 0)
            {
                NPC_IDs += "required_npc = {[";
                for (int j = 0; j < requiredL2H_NPCs.Count; j++)
                {
                    NPC_IDs += requiredL2H_NPCs[j].NPC_Name_ID + "]";

                    if (j == requiredL2H_NPCs.Count - 1)
                    {
                        NPC_IDs += "}" + "\n";
                    }
                    else
                        NPC_IDs += ";[";
                }
            }

            returnString += NPC_IDs;
            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].typeName == "required_npc")
                {
                    continue;
                }
                else
                {
                    returnString += variables[i].typeName + " = ";

                    if (variables[i].state)
                        returnString += "1";
                    else
                        returnString += "0";
                    returnString += '\n';
                }

            }
            returnString += "selllist={\t\t\n";

            for (int i = 0; i < elements.Count; i++)
            {
                string addToReturnString = "{" + elements[i].GetExportString();

                if (i != elements.Count - 1)
                {
                    addToReturnString += ";";
                }

                addToReturnString += '\n';
                if (!addToReturnString.Contains("selllist="))
                    returnString += addToReturnString;
            }

            returnString += "}\t\t\n";
            returnString += endText + "\t\t\n";

            return returnString;
        }
    }

    public class L2H_MultisellSale
    {
        public L2H_Multisell parent;

        public List<L2H_MultisellSale_Item> sale_Items;

        public List<string> costItemIDs;
        public List<string> costItemAmounts;

        public List<L2H_MultisellCost> costs;

        public bool hasTaxValue;
        public string taxValue;
        public string Separate_Adena_Cost
        {
            get
            {
                if (!string.IsNullOrEmpty(taxValue))
                    return taxValue;
                else
                    return "";
            }
            set
            {
                taxValue = value;
            }
        }

        public string saleNames
        {
            get
            {
                string returnString = "";

                for (int i = 0; i < sale_Items.Count; i++)
                {
                    returnString += sale_Items[i].L2H_Item.Item_Name;

                    if (i < sale_Items.Count - 1)
                        returnString += " - ";
                }

                return returnString;
            }

        }

        public bool IsSelected { get; set; }

        public L2H_MultisellSale Instance { get { return this; } }

        public List<L2H_MultisellSale_Item> Multisell_Sale_Items
        {
            get
            {
                return sale_Items;
            }
        }
        public string SaleItemNames
        {
            get
            {
                string names = "";

                for (int i = 0; i < sale_Items.Count; i++)
                {
                    names += sale_Items[i].ToString() + " ";
                }

                return names;
            }
        }
        

        public L2H_MultisellSale(L2H_Multisell parent, L2H_Item item)
        {
            costItemIDs = new List<string>();
            costItemAmounts = new List<string>();
            costs = new List<L2H_MultisellCost>();
            sale_Items = new List<L2H_MultisellSale_Item>();

            this.parent = parent;

            sale_Items.Add(new L2H_MultisellSale_Item(this, item, "1"));

            costItemIDs.Add("57");
            costItemAmounts.Add("1");

        }


        public L2H_MultisellSale(L2H_Multisell parent, string line)
        {
            this.parent = parent;
            costItemIDs = new List<string>();
            costItemAmounts = new List<string>();
            costs = new List<L2H_MultisellCost>();
            sale_Items = new List<L2H_MultisellSale_Item>();

            string[] splitElementLineIntoSalesAndCosts = Regex.Split(line, "}};{{");

            string splitSalesLine = splitElementLineIntoSalesAndCosts[0];
            string splitCostsLine = splitElementLineIntoSalesAndCosts[1];

            string[] splitSales = Regex.Split(splitSalesLine, "};{");
            string[] splitCosts = Regex.Split(splitCostsLine, "};{");

            for (int i = 0; i < splitSales.Length; i++)
            {
                splitSales[i] = splitSales[i].Replace("{", "");
                splitSales[i] = splitSales[i].Replace("}", "");
                splitSales[i] = splitSales[i].Replace("[", "");
                splitSales[i] = splitSales[i].Replace("]", "");
                splitSales[i] = splitSales[i].Replace("\t", "");
                if (splitSales[i].Contains(";"))
                {
                    string[] splitSale = splitSales[i].Split(';');

                    sale_Items.Add(new L2H_MultisellSale_Item(this, splitSale[0], splitSale[1]));
                }
            }

            for (int i = 0; i < splitCosts.Length; i++)
            {
                splitCosts[i] = splitCosts[i].Replace("{", "");
                splitCosts[i] = splitCosts[i].Replace("}", "");
                splitCosts[i] = splitCosts[i].Replace("[", "");
                splitCosts[i] = splitCosts[i].Replace("]", "");
                splitCosts[i] = splitCosts[i].Replace("\t", "");
                if (splitCosts[i].Contains(";")) 
                {
                    char[] splitSymbol = new char[1] { ';' };
                    string[] splitCost = splitCosts[i].Split(splitSymbol, StringSplitOptions.RemoveEmptyEntries);

                    if (splitCost.Length > 1)
                    {
                        costItemIDs.Add(splitCost[0]);
                        costItemAmounts.Add(splitCost[1]);
                    }
                    else
                    {
                        hasTaxValue = true;
                        taxValue = splitCost[0];
                    }
                }
                else
                {
                    hasTaxValue = true;
                    taxValue = splitCosts[i];
                }
            }


        }

        public string GetExportString()
        {
            string returnString = "";

            returnString += "{";

            for (int i = 0; i < sale_Items.Count; i++)
            {
                returnString += "{[" + sale_Items[i].itemID + "]" + ";" + sale_Items[i].GetAmount + "}";

                if (i != sale_Items.Count - 1)
                    returnString += ";";
                else
                    returnString += "}";
            }

            returnString += ";{";

            for (int i = 0; i < costs.Count; i++)
            {
                returnString += "{[" + costs[i].Get_Name_ID() + "];" + costs[i].Amount + "}";

                if (i != costs.Count - 1)
                    returnString += ";";
                else
                    returnString += "}";
            }

            if (hasTaxValue)
            {
                returnString += ";{" + taxValue + "}";
            }
            returnString = returnString.Replace("\t", "");

            returnString += "}";



            return returnString;
        }
    }

    public class L2H_MultisellCost
    {
        public L2H_MultisellSale parent;
        public string non_item_name { get; set; }
        public L2H_Item L2H_Item { get; set; }
        public string Amount { get; set; }

        public L2H_MultisellCost(L2H_MultisellSale parent)
        {
            this.parent = parent;
        }

        public L2H_MultisellCost Instance { get { return this; } }

        public override string ToString()
        {
            if (L2H_Item == null)
                return non_item_name;

            return L2H_Item.Item_Name;
        }
        public string Get_Name_ID()
        {
            if (L2H_Item == null)
                return non_item_name;

            return L2H_Item.Item_Name_ID;
        }

        public BitmapImage GetItemIcon
        {
            get
            {
                if (L2H_Item == null)
                    return L2H_Parser.GetItemImage(non_item_name);

                return L2H_Item.GetItemIcon;
            }
        }
        public string GetAmount
        {
            get
            {
                return Amount;
            }
            set
            {
                L2H_Log.Instance.Log_Multisell_Cost_Change(parent.parent, parent, this, "Amount", Amount, value);
                Amount = value;
            }
        }

    }

    public class MultisellVariable
    {
        public string typeName;
        public bool state;
        public string requiredNPCsString;

        public MultisellVariable()
        {

        }
        public MultisellVariable(string source)
        {
            string strippedSource = source.Replace("\t", "");
            bool requiredNPCs = false;

            if (strippedSource.Contains("required_npc"))
                requiredNPCs = true;


            string[] splitStrippedSource = strippedSource.Split('=');

            for (int i = 0; i < splitStrippedSource.Length; i++)
            {
                splitStrippedSource[i] = splitStrippedSource[i].Replace(" ", "");
            }

            if (splitStrippedSource.Length < 2)
                return;

            typeName = splitStrippedSource[0];

            if (requiredNPCs)
            {
                requiredNPCsString = splitStrippedSource[1].Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "");
            }
            else
            {
                if (splitStrippedSource[1] == "0")
                    state = false;
                else if (splitStrippedSource[1] == "1")
                    state = true;
            }

        }
    }

    public class L2H_MultisellSale_Item
    {
        public L2H_MultisellSale parent { get; set; }
        public L2H_Item L2H_Item { get; set; }
        public string itemID;
        string amount;

        public bool IsSelected { get; set; }

        public L2H_MultisellSale_Item Instance { get { return this; } }

        public override string ToString()
        {
            if (L2H_Item == null)
                return itemID;

            return L2H_Item.Item_Name;
        }
        public string GetAmount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
            }
        }
        public string GetIconString
        {
            get
            {
                if (L2H_Item != null)
                    return L2H_Item.GetIconString();
                else
                    return "";
            }
        }
        public BitmapImage GetItemIcon
        {
            get
            {
                if (L2H_Item == null)
                    return L2H_Parser.GetItemImage("");
                else
                    return L2H_Item.GetItemIcon;
            }
        }

        public L2H_MultisellSale_Item(L2H_MultisellSale parent, L2H_Item item, string amount)
        {
            this.parent = parent;
            this.L2H_Item = item;
            this.amount = amount;
        }
        public L2H_MultisellSale_Item(L2H_MultisellSale parent, string itemID, string amount)
        {
            this.parent = parent;
            this.itemID = itemID;
            this.amount = amount;
        }
    }
}
