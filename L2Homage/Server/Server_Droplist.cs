using System;
using System.Collections.Generic;

namespace L2Homage
{

    public class DroplistExtension
    {
        public L2H_Droplist L2H_Droplist;

        public virtual string GetCustomExportString()
        {
            return "";
        }

    }

    public class Server_Droplist : DroplistExtension
    {
        public string id;
        public bool isCustom;

        public List<ItemDrop> itemDrops;


        public Server_Droplist()
        {
        }

        /// <summary>
        /// Called when manually creating a server single droplist
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="isCustom"></param>
        public Server_Droplist(string ID, bool isCustom)
        {
            this.id = ID;
            this.isCustom = isCustom;
            itemDrops = new List<ItemDrop>();
        }

        public Server_Droplist(string droplist_ID, bool isCustom, string drops_string)
        {

            itemDrops = new List<ItemDrop>();

            id = droplist_ID;
            this.isCustom = isCustom;


            string[] splitList = drops_string.Split(';');

            int numberOfItems = splitList.Length / 4;

            int completedItems = 0;

            for (int i = 0; i < numberOfItems; i++)
            {

                string itemID = splitList[i + (completedItems * 3)];

                itemID = itemID.Remove(0, 2);

                itemID = itemID.Remove(itemID.Length - 1, 1);
                string itemMinAmount = splitList[i + (completedItems * 3) + 1];
                string itemMaxAmount = splitList[i + (completedItems * 3) + 2];
                string itemChance = splitList[i + (completedItems * 3) + 3];

                itemChance = itemChance.Replace("}", "");

                itemDrops.Add(new ItemDrop(itemID, itemMinAmount, itemMaxAmount, itemChance));

                completedItems++;
            }

        }


        public override string GetCustomExportString()
        {
            string returnString = "";

            returnString = returnString + id + '\t' + isCustom + '\t';

            for (int i = 0; i < itemDrops.Count; i++)
            {
                returnString = returnString + "{[" +
                    itemDrops[i].itemID + "];" +
                    itemDrops[i].minimumAmount + ";" +
                    itemDrops[i].maximumAmount + ";" +
                    itemDrops[i].probability + "}";

                if (i != itemDrops.Count - 1)
                {
                    returnString = returnString + ";";
                }
            }

            return returnString;
        }
    }



    public class Server_Multi_Droplist : DroplistExtension
    {
        public string id;
        public bool isCustom;
        public List<Server_Droplist> separateDroplists;
        public List<string> separateDroplistChances;
        public List<string> separateDroplistIDs;

        /// <summary>
        /// Called when creating a new multi droplist manually
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="isCustom"></param>
        public Server_Multi_Droplist(string ID, bool isCustom)
        {
            this.id = ID;
            this.isCustom = isCustom;
            separateDroplists = new List<Server_Droplist>();
            separateDroplistChances = new List<string>();
            separateDroplistIDs = new List<string>();
        }

        public Server_Multi_Droplist(L2H_Multi_Droplist_Pointer p, List<Server_Droplist> droplistData)
        {
            id = p.droplistID;
            separateDroplistIDs = p.multipartIDs;
            separateDroplistChances = p.multipartProbabilities;
            separateDroplists = droplistData;
        }

        /// <summary>
        /// Load server multi droplist from L2H_Multi_Droplist_Pointers
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="isCustom"></param>
        /// <param name="separateDroplistIDs"></param>
        /// <param name="separateDroplistChances"></param>
        public Server_Multi_Droplist(string ID, bool isCustom, List<string> separateDroplistIDs, List<string> separateDroplistChances)
        {
            id = ID;
            this.isCustom = isCustom;
            this.separateDroplistIDs = separateDroplistIDs;
            this.separateDroplistChances = separateDroplistChances;
            separateDroplists = new List<Server_Droplist>();


        }

        /// <summary>
        /// Create original server multi droplist
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="isCustom"></param>
        /// <param name="droplists_string"></param>
        public Server_Multi_Droplist(string ID, bool isCustom, string droplists_string)
        {
            separateDroplistIDs = new List<string>();
            string[] splitSource = droplists_string.Split(new string[] { "{{{" }, StringSplitOptions.RemoveEmptyEntries);


            id = ID;
            this.isCustom = isCustom;

            separateDroplists = new List<Server_Droplist>();
            separateDroplistChances = new List<string>();
            separateDroplistIDs = new List<string>();

            int numberOfDroplistsCompleted = 0;

            for (int h = 0; h < splitSource.Length; h++)
            {

                Server_Droplist singleDroplist = new Server_Droplist();
                singleDroplist.itemDrops = new List<ItemDrop>();

                singleDroplist.id = id + "_" + h;


                string[] splitList = splitSource[h].Split(';');

                int numberOfItems = splitList.Length / 4;

                int completedItems = 0;

                for (int i = 0; i < numberOfItems; i++)
                {

                    string itemID = splitList[i + (completedItems * 3)];

                    if (completedItems > 0)
                    {
                        itemID = itemID.Remove(0, 2);
                    }
                    else
                    {
                        itemID = itemID.Remove(0, 1);
                    }

                    itemID = itemID.Remove(itemID.Length - 1, 1);
                    string itemMinAmount = splitList[i + (completedItems * 3) + 1];
                    string itemMaxAmount = splitList[i + (completedItems * 3) + 2];
                    string itemChance = splitList[i + (completedItems * 3) + 3];

                    itemChance = itemChance.Replace("{", ""); itemChance = itemChance.Replace("}", ""); itemChance = itemChance.Replace("[", ""); itemChance = itemChance.Replace("]", "");


                    singleDroplist.itemDrops.Add(new ItemDrop(itemID, itemMinAmount, itemMaxAmount, itemChance));

                    completedItems++;
                }

                separateDroplistIDs.Add(singleDroplist.id);

                separateDroplists.Add(singleDroplist);

                string separateDropChance = splitList[splitList.Length - 1];
                if (string.IsNullOrEmpty(separateDropChance))
                    separateDropChance = splitList[splitList.Length - 2];

                separateDropChance = separateDropChance.Replace("}", "");

                separateDroplistChances.Add(separateDropChance);

                numberOfDroplistsCompleted++;
            }
        }

        public void Hook_Single_Droplists_To_Multi_Droplist(List<Server_Droplist> single_Droplist_Data)
        {
            for (int i = 0; i < separateDroplistIDs.Count; i++)
            {
                separateDroplists.Add(single_Droplist_Data.Find(x => x.id == separateDroplistIDs[i]));
            }
        }

        public override string GetCustomExportString()
        {
            string returnString = "";


            returnString = returnString + id + '\t' + isCustom + '\t';

            for (int i = 0; i < separateDroplists.Count; i++)
            {
                if (separateDroplists.Find(x=>x.id == separateDroplistIDs[i]).itemDrops.Count > 0)
                {
                    returnString = returnString + separateDroplistIDs[i] + "\t" + separateDroplistChances[i] + "\t";
                }
            }


            return returnString;
        }

        public void UpdateDroplistReferences(L2H_Multi_Droplist_Pointer newReferences, DroplistType type)
        {
            for (int i = 0; i < newReferences.multipartIDs.Count; i++)
            {
                if (i < separateDroplistIDs.Count)
                {
                    separateDroplistIDs[i] = newReferences.multipartIDs[i];
                    separateDroplistChances[i] = newReferences.multipartProbabilities[i];

                }
                else
                {
                    if (!separateDroplistIDs.Contains(newReferences.multipartIDs[i]))
                    {
                        separateDroplistIDs.Add(newReferences.multipartIDs[i]);
                        separateDroplistChances.Add(newReferences.multipartProbabilities[i]);
                    }
                }
            }
        }
    }

    public class ItemDrop
    {
        public string itemID;
        public string minimumAmount;
        public string maximumAmount;
        public string probability;

        public L2H_Item l2h_Item;

        public ItemDrop(string itemID, string minimumAmount, string maximumAmount, string chancePercentage)
        {
            this.itemID = itemID;
            this.minimumAmount = minimumAmount;
            this.maximumAmount = maximumAmount;
            this.probability = chancePercentage;
        }

        public ItemDrop(L2H_Item l2H_Item)
        {
            this.l2h_Item = l2H_Item;
            minimumAmount = "1";
            maximumAmount = "1";
            probability = "100";
        }
    }
}
