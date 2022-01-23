using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Recipe
    {
        public int ID { get; set; }
        public bool IsSelected { get; set; }
        public Client_Recipe client_Recipe { get; set; }
        public Server_Recipe server_Recipe { get; set; }
        public L2H_Item owner_L2H_Item { get; set; }
        public Server_Itemdata owner_Itemdata { get; set; }
        public Client_Itemname owner_ItemName { get; set; }
        public Client_Etc client_Etc { get; set; }
        public L2H_Recipe Instance { get { return this; } }
        public bool IsCustom { get; set; }

        public List<L2H_Recipe_Material> recipe_Materials { get; set; }

        public List<L2H_Recipe_Result> recipe_Results { get; set; }

        public override string ToString()
        {
            return owner_ItemName.name.Replace("Recipe: ", "") + " (ID: " + ID + ")";
        }

        public void AddResult(L2H_Item item, int chance, int amount, bool manually = false)
        {
            if (recipe_Results.Count > 1)
            {
                System.Windows.MessageBox.Show("Recipe already has maximum results");
            }
            else
            {
                if (manually)
                    L2H_Log.Instance.Log_Recipe_Result_Add(this, item);
                recipe_Results.Add(new L2H_Recipe_Result() { item = item, amount = amount, chance = chance });
            }
        }

        public void RemoveResult(L2H_Item item)
        {
            if (recipe_Results.Exists(x => x.item == item))
            {
                recipe_Results.Remove(recipe_Results.Find(x => x.item == item));
            }
        }

        public void RemoveResult(int index)
        {
            if (recipe_Results.Count > 1)
            {
                L2H_Log.Instance.Log_Recipe_Result_Remove(this, recipe_Results[index].item);
                recipe_Results.RemoveAt(index);
            }
            else
            {
                System.Windows.MessageBox.Show("Recipe results cannot be empty");
            }
        }

        public bool AddMaterial(L2H_Item item, int amount, bool manually = false)
        {
            if (!recipe_Materials.Exists(x => x.item == item))
            {
                if (manually)
                    L2H_Log.Instance.Log_Recipe_Material_Add(this, item);
                recipe_Materials.Add(new L2H_Recipe_Material() { item = item, amount = amount });
                return true;
            }
            else
            {
                System.Windows.MessageBox.Show("Recipe already contains material: " + item.client_Itemname.name);
                return false;
            }
        }


        public void RemoveMaterial(int index)
        {
            if (recipe_Materials.Count > 1)
            {
                L2H_Log.Instance.Log_Recipe_Material_Remove(this, recipe_Materials[index]);
                recipe_Materials.RemoveAt(index);
            }
            else
            {
                System.Windows.MessageBox.Show("Recipe material cost cannot be empty");
            }
        }

        #region Properties

        public string Recipe_Name
        {
            get
            {
                return owner_ItemName.name;
            }
        }
        public string Recipe_Owner_Item_ID
        {
            get
            {
                return owner_Itemdata.itemId;
            }
        }
        public string Recipe_Name_ID
        {
            get
            {
                return server_Recipe.nameID;
            }
            set
            {

                L2H_Log.Instance.Log_Recipe_Change(this, "Recipe Name", server_Recipe.nameID, value);
                server_Recipe.nameID = value;
            }
        }
        public System.Windows.Media.Imaging.BitmapImage Recipe_Icon
        {
            get
            {
                return L2H_Parser.GetItemImage(recipe_Results[0].item.GetIconString());
            }
        }
        public int Recipe_Craft_Level
        {
            get
            {
                return int.Parse(server_Recipe.level);
            }
            set
            {
                server_Recipe.level = value.ToString();
            }
        }
        public int Recipe_Craft_Level_Index
        {
            get
            {
                return int.Parse(server_Recipe.level) - 1;
            }
            set
            {
                L2H_Log.Instance.Log_Recipe_Change(this, "Recipe Craft Level", server_Recipe.level, (value + 1).ToString());
                server_Recipe.level = (value + 1).ToString();
            }
        }
        public string Recipe_MP_Cost
        {
            get
            {
                return server_Recipe.mp_consume;
            }
            set
            {
                L2H_Log.Instance.Log_Recipe_Change(this, "Recipe MP Cost", server_Recipe.mp_consume, value);
                server_Recipe.mp_consume = value;
            }
        }
        public string Recipe_Success_Rate
        {
            get
            {
                return server_Recipe.success_rate;
            }
            set
            {
                L2H_Log.Instance.Log_Recipe_Change(this, "Recipe Success Rate", server_Recipe.success_rate, value);
                server_Recipe.success_rate = value;
            }
        }
        public bool Recipe_Common
        {
            get
            {
                if (server_Recipe.iscommonrecipe == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Recipe_Change(this, "Recipe Is Common", "False", "True");
                    server_Recipe.iscommonrecipe = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Recipe_Change(this, "Recipe Is Common", "True", "False");
                    server_Recipe.iscommonrecipe = "0";
                }
            }
        }
        public bool Recipe_Requires_Extra_Recipe
        {
            get
            {
                if (client_Recipe.materials_extra == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    L2H_Log.Instance.Log_Recipe_Change(this, "Recipe Extra Required", "False", "True");
                    client_Recipe.materials_extra = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_Recipe_Change(this, "Recipe Extra Required", "True", "False");
                    client_Recipe.materials_extra = "0";
                }
            }
        }

        public string Recipe_Result_Name_A
        {
            get
            {
                return recipe_Results[0].item.client_Itemname.name;
            }
        }
        public string Recipe_Result_Chance_A
        {
            get
            {
                return recipe_Results[0].chance.ToString();
            }
            set
            {
                L2H_Log.Instance.Log_Recipe_Change(this, "Recipe Result A Chance", recipe_Results[0].chance.ToString(), value);
                recipe_Results[0].chance = int.Parse(value);
            }
        }
        public string Recipe_Result_Amount_A
        {
            get
            {
                return recipe_Results[0].amount.ToString();
            }
            set
            {
                L2H_Log.Instance.Log_Recipe_Change(this, "Recipe Result A Amount", recipe_Results[0].amount.ToString(), value);
                recipe_Results[0].amount = int.Parse(value);
            }
        }
        public string Recipe_Result_Name_B
        {
            get
            {
                if (recipe_Results.Count > 1)
                    return recipe_Results[1].item.client_Itemname.name;
                else
                    return "No Other Outcome";
            }
        }
        public string Recipe_Result_Chance_B
        {
            get
            {
                if (recipe_Results.Count > 1)
                    return recipe_Results[1].chance.ToString();
                else
                    return "";
            }
            set
            {
                if (recipe_Results.Count > 1)
                {
                    L2H_Log.Instance.Log_Recipe_Change(this, "Recipe Result B Chance", recipe_Results[1].chance.ToString(), value);
                    recipe_Results[1].chance = int.Parse(value);
                }
            }
        }
        public string Recipe_Result_Amount_B
        {
            get
            {
                if (recipe_Results.Count > 1)
                    return recipe_Results[1].amount.ToString();
                else
                    return "";
            }
            set
            {
                if (recipe_Results.Count > 1)
                {
                    L2H_Log.Instance.Log_Recipe_Change(this, "Recipe Result B Amount", recipe_Results[1].amount.ToString(), value);
                    recipe_Results[1].amount = int.Parse(value);
                }
            }
        }
        public System.Windows.Media.Imaging.BitmapImage Recipe_Result_Image_A_Icon
        {
            get
            {
                return L2H_Parser.GetItemImage(recipe_Results[0].item.GetIconString());
            }
        }
        public System.Windows.Media.Imaging.BitmapImage Recipe_Result_Image_B_Icon
        {
            get
            {
                if (recipe_Results.Count > 1)
                    return L2H_Parser.GetItemImage(recipe_Results[1].item.GetIconString());
                else
                    return L2H_Parser.GetItemImage("");
            }
        }
        public string Recipe_Description
        {
            get
            {
                return owner_ItemName.description;
            }
            set
            {
                L2H_Log.Instance.Log_Recipe_Change(this, "Recipe Result A Chance", owner_ItemName.description, value);
                owner_ItemName.description = value;
            }
        }


        #endregion
    }

    public class L2H_Recipe_Material
    {
        public L2H_Item item { get; set; }
        public int amount { get; set; }
    }

    public class L2H_Recipe_Result
    {
        public L2H_Item item { get; set; }
        public int chance { get; set; }
        public int amount { get; set; }
    }
}
