using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace L2Homage
{
    public static class L2H_Parser
    {
        //Import functions
        public static string GetWeaponSubtypeStringFromWeaponTypeID(string id)
        {
            switch (id)
            {
                case "0":
                    {
                        return "Shield";
                    }
                case "1":
                    {
                        return "Sword";
                    }
                case "2":
                    {
                        return "Blunt";
                    }
                case "3":
                    {
                        return "Dagger";
                    }
                case "4":
                    {
                        return "Polearm";
                    }
                case "5":
                    {
                        return "Fist";
                    }
                case "6":
                    {
                        return "Bow";
                    }
                case "7":
                    {
                        return "Spellbook";
                    }
                case "8":
                    {
                        return "Duals";
                    }
                case "10":
                    {
                        return "Fishing Rod";
                    }
                case "11":
                    {
                        return "Rapier";
                    }
                case "12":
                    {
                        return "Crossbow";
                    }
                case "13":
                    {
                        return "Ancient Sword";
                    }
                case "15":
                    {
                        return "Dynasty";
                    }
                default:
                    return "unknown";
            }
        }
        public static string GetArmorSubtypeStringFromArmorTypeID(string id)
        {
            switch (id)
            {
                case "0":
                    {
                        return "None";
                    }
                case "1":
                    {
                        return "Light";
                    }
                case "2":
                    {
                        return "Heavy";
                    }
                case "3":
                    {
                        return "Magic";
                    }
                case "4":
                    {
                        return "Sigil";
                    }
                default:
                    return "unknown";
            }
        }
        public static string GetItemTypeStringFromTagID(string tag)
        {
            switch (tag)
            {
                case "0":
                    return "Weapon";
                case "1":
                    return "Armor";
                case "2":
                    return "Etc";
                default:
                    return "Unknown";
            }
        }
        public static string GetBodyPartStringFromBodyPartID(string id)
        {
            switch (id)
            {
                case "0":
                    return "None";
                case "1":
                    return "Earring";
                case "3":
                    return "Necklace";
                case "4":
                    return "Ring";
                case "6":
                    return "Head";
                case "7":
                    return "Both Hands";
                case "8":
                    return "Onepiece";
                case "9":
                    return "Full Set (Event)";
                case "10":
                    return "Hair Accessory (2 slots)";
                case "19":
                    return "Belt";
                case "20":
                    return "Gloves";
                case "21":
                    return "Chest";
                case "22":
                    return "Pants";
                case "23":
                    return "Boots";
                case "24":
                    return "Cloak";
                case "25":
                    return "Hair Accessory (top slot)";
                case "26":
                    return "Hair Accessory (bottom slot)";
                case "27":
                    return "Right Hand";
                case "28":
                    return "Left Hand";
                case "591":
                    return "Left Bracelet1";
                case "613":
                    return "Left Bracelet2";
                case "1043":
                    return "Left Bracelet3";
                case "1514":
                    return "Left Bracelet4";
                case "4294967295":
                    return "Left Bracelet5";
                default:
                    return "Unknown";
            }
        }


        public static string GetGradeStringFromCrystalTypeID(string id)
        {
            switch (id)
            {
                case "0":
                    return "none";
                case "1":
                    return "d";
                case "2":
                    return "c";
                case "3":
                    return "b";
                case "4":
                    return "a";
                case "5":
                    return "s";
                case "6":
                    return "s80";
                case "7":
                    return "s84";
                case "8":
                    return "event";
                default:
                    return "unknown";
            }
        }
        public static string GetGradeIndexFromGradeString(string text)
        {
            switch (text)
            {
                case "none":
                    return "0";
                case "d":
                    return "1";
                case "c":
                    return "2";
                case "b":
                    return "3";
                case "a":
                    return "4";
                case "s":
                    return "5";
                case "s80":
                    return "6";
                case "s84":
                    return "7";
                case "event":
                    return "8";
                default:
                    return "0";
            }
        }


        //Export functions
        public static string GetWeaponTypeIDFromSubTypeCellString(string id)
        {
            switch (id)
            {
                case "shield":
                    {
                        return "0";
                    }
                case "sword":
                    {
                        return "1";
                    }
                case "blunt":
                    {
                        return "2";
                    }
                case "dagger":
                    {
                        return "3";
                    }
                case "polearm":
                    {
                        return "4";
                    }
                case "fist":
                    {
                        return "5";
                    }
                case "bow":
                    {
                        return "6";
                    }
                case "spellbook":
                    {
                        return "7";
                    }
                case "duals":
                    {
                        return "8";
                    }
                case "vesper":
                    {
                        return "8";
                    }
                case "fishing Rod":
                    {
                        return "10";
                    }
                case "rapier":
                    {
                        return "1";
                    }
                case "crossbow":
                    {
                        return "12";
                    }
                case "great Sword":
                    {
                        return "12";
                    }
                case "ancient Sword":
                    {
                        return "13";
                    }
                case "dynasty":
                    {
                        return "15";
                    }
                default:
                    return "unknown";
            }
        }
        public static string GetArmorSubtypeIDFromCellString(string value)
        {
            switch (value)
            {
                case "None":
                    {
                        return "0";
                    }
                case "Light":
                    {
                        return "1";
                    }
                case "Heavy":
                    {
                        return "2";
                    }
                case "Magic":
                    {
                        return "3";
                    }
                case "Sigil":
                    {
                        return "4";
                    }
                default:
                    return "unknown";
            }
        }
        public static string GetBodyPartIDFromCellString(string text)
        {
            switch (text)
            {
                case "None":
                    return "0";
                case "Earring":
                    return "1";
                case "Necklace":
                    return "3";
                case "Ring":
                    return "4";
                case "Head":
                    return "6";
                case "Both Hands":
                    return "7";
                case "Onepiece":
                    return "8";
                case "Full Set (Event)":
                    return "9";
                case "Hair Accessory (2 slots)":
                    return "10";
                case "Belt":
                    return "19";
                case "Gloves":
                    return "20";
                case "Chest":
                    return "21";
                case "Pants":
                    return "22";
                case "Boots":
                    return "23";
                case "Cloak":
                    return "24";
                case "Hair Accessory (top slot)":
                    return "25";
                case "Hair Accessory (bottom slot)":
                    return "26";
                case "Right Hand":
                    return "27";
                case "Left Hand":
                    return "28";
                case "Left Bracelet1":
                    return "591";
                case "Left Bracelet2":
                    return "613";
                case "Left Bracelet3":
                    return "1043";
                case "Left Bracelet4":
                    return "1514";
                case "Left Bracelet5":
                    return "4294967295";
                default:
                    return "Unknown";
            }
        }
        public static string GetBodyPartItemDataStringFromCellString(string text, Server_Itemdata targetItemData = null) // Some body parts are unique to itemdata, so pass it here if required
        {
            switch (text)
            {
                case "None":
                    if (targetItemData != null)
                    {
                        return targetItemData.slot_bit_type;
                    }
                    return "none";
                case "Earring":
                    return "rear;lear";
                case "Necklace":
                    return "neck";
                case "Ring":
                    return "rfinger;lfinger";
                case "Head":
                    return "head";
                case "Both Hands":
                    return "lrhand";
                case "Onepiece":
                    return "onepiece";
                case "Full Set (Event)":
                    return "alldress";
                case "Hair Accessory (2 slots)":
                    return "hairall";
                case "Belt":
                    return "waist";
                case "Gloves":
                    return "gloves";
                case "Chest":
                    return "chest";
                case "Pants":
                    return "legs";
                case "Boots":
                    return "feet";
                case "Cloak":
                    return "back";
                case "Hair Accessory (top slot)":
                    return "hair";
                case "Hair Accessory (bottom slot)":
                    return "hair2";
                case "Right Hand":
                    return "rhand";
                case "Left Hand":
                    return "lhand";
                case "Left Bracelet1":
                    return "591";
                case "Left Bracelet2":
                    return "613";
                case "Left Bracelet3":
                    return "1043";
                case "Left Bracelet4":
                    return "1514";
                case "Left Bracelet5":
                    return "4294967295";
                default:
                    return "Unknown";
            }
        }




        //Debug functions
        public static void ListAllWeaponTypes(List<Client_Weapon> loadedWeapons)
        {
            List<string> typesAvailable = new List<string>();
            List<string> typeFoundOnLine = new List<string>();

            List<string> body_Parts_Available = new List<string>();
            List<string> body_Parts_FoundOnLine = new List<string>();

            List<string> handnessAvailable = new List<string>();
            List<string> handnessFoundOnLine = new List<string>();

            for (int i = 0; i < loadedWeapons.Count; i++)
            {
                if (!typesAvailable.Contains(loadedWeapons[i].weapon_type))
                {
                    typesAvailable.Add(loadedWeapons[i].weapon_type);
                    typeFoundOnLine.Add((i + 2).ToString());
                }
                if (!body_Parts_Available.Contains(loadedWeapons[i].body_part))
                {
                    body_Parts_Available.Add(loadedWeapons[i].body_part);
                    body_Parts_FoundOnLine.Add((i + 2).ToString());
                }
                if (!handnessAvailable.Contains(loadedWeapons[i].handness))
                {
                    handnessAvailable.Add(loadedWeapons[i].handness);
                    handnessFoundOnLine.Add((i + 2).ToString());
                }
            }

        }

        public static string GetItemNameIDFomItemID(List<Server_Itemdata> itemdata, string itemIDs)
        {
            if (!string.IsNullOrEmpty(itemIDs))
            {
                if (itemIDs.Contains(";"))
                {
                    string[] splitData = itemIDs.Split(';');

                    string finalString = "";

                    for (int j = 0; j < splitData.Length; j++)
                    {
                        finalString += GetItemDataFromItemID(itemdata, splitData[j]).itemName;

                        if (j != splitData.Length - 1)
                            finalString += ";";
                    }

                    return finalString;

                }
                else if (itemIDs.Contains(","))
                {
                    string[] splitData = itemIDs.Split(',');

                    string finalString = "";

                    for (int j = 0; j < splitData.Length; j++)
                    {
                        finalString += GetItemDataFromItemID(itemdata, splitData[j]).itemName;

                        if (j != splitData.Length - 1)
                            finalString += ",";
                    }

                    return finalString;
                }
                else
                {
                    return GetItemDataFromItemID(itemdata, itemIDs).itemName;
                }
            }

            return "";

        }

        public static Server_Itemdata GetItemDataFromItemID(List<Server_Itemdata> itemdata, string itemID)
        {
            return itemdata.Find(x => x.itemId == itemID);
        }

        public static string GetInGameNameFromItemID(List<Client_Itemname> itemnames, string itemID)
        {
            return itemnames.Find(x => x.id == itemID).name;
        }

        public static string GetInGameNameFromNameID(List<Client_Itemname> itemnames, List<Server_Itemdata> itemdata, string nameID)
        {
            return itemnames.Find(x => x.id == itemdata.Find(y => y.itemName == nameID).itemId).name;
            //return itemnames.Find(x => x.id == itemID).name;
        }

        public static string GetNPCExpSpRpOnKill(Server_Npcdata targetNPC, string type)
        {
            float returnValue = 0;

            float parsedLevel = 0;
            float parsedRate = 0;

            switch (type)
            {
                case "xp":

                    float.TryParse(targetNPC.level, out parsedLevel);
                    float.TryParse(targetNPC.acquire_exp_rate, out parsedRate);

                    returnValue = parsedLevel * parsedLevel * parsedRate;

                    return returnValue.ToString();
                case "sp":
                    float.TryParse(targetNPC.acquire_sp, out parsedRate);

                    returnValue = parsedRate;

                    return returnValue.ToString();
                case "rp":
                    float.TryParse(targetNPC.level, out parsedLevel);
                    float.TryParse(targetNPC.acquire_rp, out parsedRate);

                    returnValue = parsedRate;

                    return returnValue.ToString();

                default:
                    return "0";
            }

        }

        public static string GetReadableNPCType(NPCTypes nPCTypes)
        {
            switch (nPCTypes)
            {
                case NPCTypes.none:
                    return "none";
                case NPCTypes.warrior:
                    return "Monster";
                case NPCTypes.citizen:
                    return "Citizen";
                case NPCTypes.treasure:
                    return "Treasure";
                case NPCTypes.boss:
                    return "Boss";
                case NPCTypes.zzoldagu:
                    return "Zzoldagu";
                case NPCTypes.world_trap:
                    return "World\nTrap";
                case NPCTypes.collection:
                    return "Collection";
                case NPCTypes.merchant:
                    return "Merchant";
                case NPCTypes.warehouse_keeper:
                    return "Warehouse\nKeeper";
                case NPCTypes.teleporter:
                    return "Teleporter";
                case NPCTypes.guild_master:
                    return "Guild\nMaster";
                case NPCTypes.guild_coach:
                    return "Skill\nTrainer";
                case NPCTypes.guard:
                    return "Guard";
                case NPCTypes.blacksmith:
                    return "Blacksmith";
                case NPCTypes.mrkeeper:
                    return "Race\nManager";
                case NPCTypes.monrace:
                    return "Monster\nRace";
                case NPCTypes.package_keeper:
                    return "Premium\nManager";
                case NPCTypes.holything:
                    return "Artifact";
                case NPCTypes.siege_attacker:
                    return "Siege\nAttacker";
                case NPCTypes.ownthing:
                    return "Territory\nWard";
                case NPCTypes.summon:
                    return "Summon";
                case NPCTypes.pet:
                    return "Pet";
                case NPCTypes.xmastree:
                    return "Xmas\nTree";
                case NPCTypes.pc_trap:
                    return "Trap";
                case NPCTypes.doppelganger:
                    return "Decoy";
                default:
                    return "all";
            }
        }

        public static string GetFilteringNPCTypesString(string text)
        {
            switch (text)
            {
                case "none":
                    return "none";
                case "World\nTrap":
                    return "world_trap";
                case "Warehouse\nKeeper":
                    return "warehouse_keeper";
                case "Monster":
                    return "warrior";
                case "Guild\nMaster":
                    return "guild_master";
                case "Skill\nTrainer":
                    return "guild_coach";
                case "Race\nManager":
                    return "mrkeeper";
                case "Monster\nRace":
                    return "monrace";
                case "Premium\nManager":
                    return "package_keeper";
                case "Artifact":
                    return "holything";
                case "Siege\nAttacker":
                    return "siege_attacker";
                case "Territory\nWard":
                    return "ownthing";
                case "Xmas\nTree":
                    return "xmastree";
                case "Trap":
                    return "pc_trap";
                case "Decoy":
                    return "doppelganger";
                default:
                    return text;
            }
        }

        public static int GrabHighestIDFromNPCPointersList(List<Server_Npcdata> npcdata)
        {
            int returnValue = 0;

            for (int i = 0; i < npcdata.Count; i++)
            {
                int attempt = 0;
                int.TryParse(npcdata[i].npcId, out attempt);

                if (attempt > returnValue)
                    returnValue = attempt;
            }

            return returnValue;
        }

        public static string GetUniqueNPCNameID(List<Server_Npcdata> npcdata, string currentNameID, int newNumber = 0)
        {
            string returnString = "";

            if (currentNameID.Length > 25)
                currentNameID = "custom_npc";

            bool npcNameIDExists = npcdata.Exists(x => x.npcName == currentNameID);

            if (npcNameIDExists)
            {
                bool numberedCustomNpcNameIDExists = npcdata.Exists(x => x.npcName == currentNameID + "_" + newNumber);

                if (numberedCustomNpcNameIDExists)
                    returnString = GetUniqueNPCNameID(npcdata, currentNameID, newNumber + 1);
                else
                    returnString = currentNameID + "_" + newNumber;
            }
            else
                returnString = currentNameID;

            return returnString;
        }

        public static bool CheckIfTrue(string targetVariable)
        {
            if (targetVariable == "1")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns resistances as an array
        /// 0 = Fire
        /// 1 = Water
        /// 2 = Wind
        /// 3 = Earth
        /// 4 = Holy
        /// 5 = Unholy
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string[] GetResistancesArray(string source)
        {
            return source.Split(';');
        }

        /// <summary>
        /// Returns attack attribute and percentage of damage converted
        /// 0 = Attribute Type
        /// 1 = Percentage
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string[] GetAttributeAndPercentage(string source)
        {
            return source.Split(';');
        }

        public static string[] GetCapsuledItems(string source)
        {

            if (source.Contains(";"))
            {

                string stringToSplit = source.Replace("{", "");
                stringToSplit = stringToSplit.Replace("}", "");
                stringToSplit = stringToSplit.Replace("[", "");
                stringToSplit = stringToSplit.Replace("]", "");

                return stringToSplit.Split(';');

            }
            else
            {
                return null;
            }

        }

        public static int GetGradeDropdownIndexFromServerItemdataCrystalType(string source)
        {
            switch (source)
            {
                case "none":
                    return 0;
                case "crystal_free":
                    return 8;
                case "d":
                    return 1;
                case "c":
                    return 2;
                case "b":
                    return 3;
                case "a":
                    return 4;
                case "s":
                    return 5;
                case "s80":
                    return 6;
                case "s84":
                    return 7;
                default:
                    return 0;
            }
        }

        public static int GetAttributeDropdownIndexFromString(string source)
        {
            switch (source.ToLower())
            {
                case "attr_none":
                    return 0;
                case "attr_fire":
                    return 1;
                case "attr_water":
                    return 2;
                case "attr_wind":
                    return 3;
                case "attr_earth":
                    return 4;
                case "attr_holy":
                    return 5;
                case "attr_unholy":
                    return 6;
                default:
                    return 0;
            }
        }
        public static string GetAttributeDropdownStringFromIndex(int source)
        {
            switch (source)
            {
                case 0:
                    return "attr_none";
                case 1:
                    return "attr_fire";
                case 2:
                    return "attr_water";
                case 3:
                    return "attr_wind";
                case 4:
                    return "attr_earth";
                case 5:
                    return "attr_holy";
                case 6:
                    return "attr_unholy";
                default:
                    return "attr_none";
            }
        }

        public static string[] GetChanceAndAmountFromTrigger(string source)
        {
            if (string.IsNullOrEmpty(source))
                return null;
            else

                return source.Replace("{", "").Replace("}", "").Split(';');
        }

        public static BitmapImage GetItemImage(string bitmapName)
        {
            string iconString = bitmapName.Replace("icon.", "") + ".png";

            List<string> pathsToImages = new List<string>()
            {
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/accessary_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/action_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/boots_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/etc_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/glove_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/helmet_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/icon_panel/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/lowbody_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/MacroWnd/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/mticket/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/onepiece/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/shield_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/time_weapon/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/upbody_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/weapon_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/wedding_i/"
            };

            BitmapImage returnImage;

            for (int i = 0; i < pathsToImages.Count; i++)
            {
                if (System.IO.File.Exists(pathsToImages[i] + iconString))
                {
                    returnImage = new BitmapImage(new Uri(pathsToImages[i] + iconString, UriKind.Absolute));
                    if (returnImage != null)
                        return returnImage;
                }

            }

            return new BitmapImage(new Uri("/Images/ImageNotFound.png", UriKind.Relative));

        }

        public static string GetItemImagePath(string bitmapName)
        {
            string iconString = bitmapName.Replace("icon.", "") + ".png";

            List<string> pathsToImages = new List<string>()
            {
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/accessary_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/action_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/boots_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/etc_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/glove_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/helmet_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/icon_panel/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/lowbody_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/MacroWnd/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/mticket/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/onepiece/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/shield_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/time_weapon/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/upbody_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/weapon_i/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/wedding_i/"
            };

            string returnString;

            for (int i = 0; i < pathsToImages.Count; i++)
            {
                if (System.IO.File.Exists(pathsToImages[i] + iconString))
                {
                    returnString = pathsToImages[i] + iconString;
                    if (!string.IsNullOrEmpty(returnString))
                        return returnString;
                }

            }

            return "/Images/ImageNotFound.png";
        }

        public static BitmapImage GetClassImage(string bitmapName)
        {
            string iconString = bitmapName + "_icon.png";
            BitmapImage returnImage;
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"/Images/Classes/" + iconString))
            {
                returnImage = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"/Images/Classes/" + iconString, UriKind.Absolute));
                if (returnImage != null)
                    return returnImage;
            }

            return new BitmapImage(new Uri("/Images/ImageNotFound.png", UriKind.Relative));

        }

        public static BitmapImage GetWorldZoneImage(string zoneName, bool thumbnail = false)
        {


            if (!thumbnail)
            {
                string pathToWorldImages = AppDomain.CurrentDomain.BaseDirectory + @"/Images/Worldmap/";
                BitmapImage returnImage;
                if (System.IO.File.Exists(pathToWorldImages + zoneName + ".jpg"))
                {
                    returnImage = new BitmapImage(new Uri(pathToWorldImages + zoneName + ".jpg", UriKind.Absolute));
                    if (returnImage != null)
                        return returnImage;
                    else
                        return null;
                }
                else return null;
            }
            else
            {
                string pathToWorldImages = AppDomain.CurrentDomain.BaseDirectory + @"/Images/Worldmap_Thumbs/";
                BitmapImage returnImage;

                if (System.IO.File.Exists(pathToWorldImages + zoneName + ".jpg"))
                {
                    returnImage = new BitmapImage(new Uri(pathToWorldImages + zoneName + ".jpg", UriKind.Absolute));
                    if (returnImage != null)
                        return returnImage;
                    else
                    {
                        MessageBox.Show("Couldn't find worldmap thumbs :(\nThese are necessary for the zones system. They're mandatory.");
                        Environment.Exit(0);
                        return new BitmapImage(new Uri(pathToWorldImages + "blank.png", UriKind.Absolute));
                    }
                }
                else
                {
                    MessageBox.Show("Couldn't find worldmap thumbs :(\nThese are necessary for the zones system. They're mandatory.");
                    Environment.Exit(0);
                    return new BitmapImage(new Uri(pathToWorldImages + "blank.png", UriKind.Absolute));
                }
            }
        }

        public static List<BitmapImage> GetWorldZoneLayers(string zoneName)
        {
            string pathToWorldImages = AppDomain.CurrentDomain.BaseDirectory + @"/Images/Worldmap/";
            List<BitmapImage> returnList = new List<BitmapImage>();

            if (System.IO.File.Exists(pathToWorldImages + zoneName + ".jpg"))
            {
                returnList.Add(new BitmapImage(new Uri(pathToWorldImages + zoneName + ".jpg", UriKind.Absolute)));

            }

            for (int i = 0; i < 10; i++)
            {
                if (System.IO.File.Exists(pathToWorldImages + zoneName + "_" + i.ToString() + ".jpg"))
                {
                    returnList.Add(new BitmapImage(new Uri(pathToWorldImages + zoneName + "_" + i.ToString() + ".jpg", UriKind.Absolute)));
                }
            }

            return returnList;

        }

        public static BitmapImage GetNPCImage(string npc_id)
        {
            string pathToNPCImages = AppDomain.CurrentDomain.BaseDirectory + @"/Images/NPCs/";
            BitmapImage returnImage;

            if (System.IO.File.Exists(pathToNPCImages + npc_id + ".png"))
            {
                returnImage = new BitmapImage(new Uri(pathToNPCImages + npc_id + ".png", UriKind.Absolute));
                if (returnImage != null)
                    return returnImage;
            }


            return new BitmapImage(new Uri("/Images/ImageNotFound.png", UriKind.Relative));
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static string GetNPCExpSpRpOnKill(L2H_NPC targetNPC, string type)
        {
            float returnValue = 0;

            float parsedLevel = 0;
            float parsedRate = 0;

            switch (type)
            {
                case "xp":

                    float.TryParse(targetNPC.server_Npcdata.level, out parsedLevel);
                    float.TryParse(targetNPC.server_Npcdata.acquire_exp_rate, out parsedRate);

                    returnValue = parsedLevel * parsedLevel * parsedRate;

                    return returnValue.ToString("#");
                case "sp":
                    float.TryParse(targetNPC.server_Npcdata.acquire_sp, out parsedRate);

                    returnValue = parsedRate;

                    return returnValue.ToString("#");
                case "rp":
                    float.TryParse(targetNPC.server_Npcdata.level, out parsedLevel);
                    float.TryParse(targetNPC.server_Npcdata.acquire_rp, out parsedRate);

                    returnValue = parsedRate;

                    return returnValue.ToString("#");

                default:
                    return "0";
            }

        }

        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }

        public static string GetUniqueItemNameID(List<Server_Itemdata> itemdata, string currentItemNameID, int newNumber = 0)
        {
            string returnString = "";

            if (currentItemNameID.Length > 25)
                currentItemNameID = "custom_item";

            bool itemExists = itemdata.Exists(x => x.itemName == currentItemNameID);

            if (itemExists)
            {
                bool numberedItemExists = itemdata.Exists(x => x.itemName == currentItemNameID + "_" + newNumber);

                if (numberedItemExists)
                    returnString = GetUniqueItemNameID(itemdata, currentItemNameID, newNumber + 1);
                else
                    returnString = currentItemNameID + "_" + newNumber;
            }
            else
                returnString = currentItemNameID;

            return returnString;
        }

        public static string GetUniqueSetID(List<Server_Itemdata> itemdata, int newNumber = 300)
        {
            string returnString = "";

            if (itemdata.Exists(x => x.set_id == newNumber.ToString()))
            {
                returnString = GetUniqueSetID(itemdata, newNumber + 1);

            }
            else
                returnString = newNumber.ToString();

            return returnString;
        }

        public static string GetExpByLevel(string level)
        {
            if (!string.IsNullOrEmpty(level))
            {
                int parsedLevel = 0;
                if (parsedLevel < 1)
                    parsedLevel = 1;

                if (int.Parse(level) > L2H_Constants.OriginalExpTable.Count)
                {
                    return "0";
                }

                if (int.TryParse(level, out parsedLevel))
                    return L2H_Constants.OriginalExpTable[parsedLevel].ToString();
                else
                    return "0";

            }
            else
            {
                MessageBox.Show("No EXP entered. Level cannot be calculated.");
                return "0";
            }
        }

        public static string GetLevelByExp(string exp)
        {
            if (!string.IsNullOrEmpty(exp))
            {
                Int64 parsedExp = 0;

                if (Int64.TryParse(exp, out parsedExp))
                {
                    int levelIndex = 0;
                    for (int i = 0; i < L2H_Constants.OriginalExpTable.Count; i++)
                    {
                        if (parsedExp > L2H_Constants.OriginalExpTable[i])
                            levelIndex = i;
                        else
                            break;
                    }

                    return (levelIndex + 1).ToString();
                }
                else
                {
                    return "1";
                }
            }
            else
            {
                MessageBox.Show("No EXP entered. Level cannot be calculated.");
                return "1";
            }
        }

        public static string GetNPCExpSpRpRate(Server_Npcdata targetNPC, string newValueOnKill, string type)
        {

            float returnValue = 0;
            float parsedLevel = 0;
            float parsedNewExpOnKill = 0;
            float parsedNewSPOnKill = 0;
            float parsedNewRPOnKill = 0;

            switch (type)
            {
                case "level":
                    returnValue = 0;
                    float.TryParse(targetNPC.level, out parsedLevel);
                    float.TryParse(newValueOnKill, out parsedNewExpOnKill);

                    returnValue = parsedNewExpOnKill / (parsedLevel * parsedLevel);

                    return returnValue.ToString();

                case "xp":
                    returnValue = 0;
                    float.TryParse(targetNPC.level, out parsedLevel);
                    float.TryParse(newValueOnKill, out parsedNewExpOnKill);

                    returnValue = parsedNewExpOnKill / (parsedLevel * parsedLevel);

                    return returnValue.ToString();
                case "sp":
                    returnValue = 0;

                    float.TryParse(newValueOnKill, out parsedNewSPOnKill);

                    returnValue = parsedNewSPOnKill;

                    return returnValue.ToString();
                case "rp":
                    returnValue = 0;

                    float.TryParse(newValueOnKill, out parsedNewRPOnKill);

                    returnValue = parsedNewRPOnKill;

                    return returnValue.ToString();
                default:
                    return "0";
                    break;
            }
        }

        //Skills
        public static int Get_Server_Operate_Type_Index_From_Value(string value)
        {
            switch (value)
            {
                case "A1":
                    return 0;
                case "A2":
                    return 1;
                case "A3":
                    return 2;
                case "A4":
                    return 3;
                case "CA1":
                    return 4;
                case "CA5":
                    return 5;
                case "DA1":
                    return 6;
                case "DA2":
                    return 7;
                case "P":
                    return 8;
                case "T":
                    return 9;
                default:
                    return 0;
            }
        }
        public static string Get_Server_Operate_Type_Value_From_Index(int index)
        {
            switch (index)
            {
                case 0:
                    return "A1";
                case 1:
                    return "A2";
                case 2:
                    return "A3";
                case 3:
                    return "A4";
                case 4:
                    return "CA1";
                case 5:
                    return "CA5";
                case 6:
                    return "DA1";
                case 7:
                    return "DA2";
                case 8:
                    return "P";
                case 9:
                    return "T";
                default:
                    return "A1";
            }
        }
        public static int Get_Skill_Target_Type_Index_From_Value(string value)
        {
            switch (value)
            {
                case "enemy":
                    return 0;
                case "self":
                    return 1;
                case "enemy_only":
                    return 2;
                case "none":
                    return 3;
                case "holything":
                    return 4;
                case "target":
                    return 5;
                case "summon":
                    return 6;
                case "door_treasure":
                    return 7;
                case "pc_body":
                    return 8;
                case "npc_body":
                    return 9;
                case "others":
                    return 10;
                case "item":
                    return 11;
                case "ground":
                    return 12;
                case "advance_base":
                    return 13;
                case "fortress_flagpole":
                    return 14;
                case "wyvern_target":
                    return 15;
                case "artillery":
                    return 16;
                default:
                    return 0;
            }
        }

        public static string Get_Skill_Target_Type_Value_From_Index(int index)
        {
            switch (index)
            {
                case 0:
                    return "enemy";
                case 1:
                    return "self";
                case 2:
                    return "enemy_only";
                case 3:
                    return "none";
                case 4:
                    return "holything";
                case 5:
                    return "target";
                case 6:
                    return "summon";
                case 7:
                    return "door_treasure";
                case 8:
                    return "pc_body";
                case 9:
                    return "npc_body";
                case 10:
                    return "others";
                case 11:
                    return "item";
                case 12:
                    return "ground";
                case 13:
                    return "advance_base";
                case 14:
                    return "fortress_flagpole";
                case 15:
                    return "wyvern_target";
                case 16:
                    return "artillery";
                default:
                    return "enemy";

            }

        }

        public static int Get_Skill_Affect_Scope_Index_From_Value(string value)
        {
            switch (value)
            {
                case "single":
                    return 0;
                case "party":
                    return 1;
                case "range":
                    return 2;
                case "fan":
                    return 3;
                case "point_blank":
                    return 4;
                case "pledge":
                    return 5;
                case "dead_pledge":
                    return 6;
                case "square":
                    return 7;
                case "square_pb":
                    return 8;
                case "none":
                    return 9;
                default:
                    return 0;
            }
        }

        public static string Get_Skill_Affect_Scope_Value_From_Index(int index)
        {
            switch (index)
            {
                case 0:
                    return "single";
                case 1:
                    return "party";
                case 2:
                    return "range";
                case 3:
                    return "fan";
                case 4:
                    return "point_blank";
                case 5:
                    return "pledge";
                case 6:
                    return "dead_pledge";
                case 7:
                    return "square";
                case 8:
                    return "square_pb";
                case 9:
                    return "none";
                default:
                    return "single";
            }
        }

        public static int Get_Skill_Affect_Object_Index_From_Value(string value)
        {
            switch (value)
            {
                case "friend":
                    return 0;
                case "not_friend":
                    return 1;
                case "undead_real_enemy":
                    return 2;
                case "all":
                    return 3;
                case "clan":
                    return 4;
                default:
                    return 3;
            }
        }

        public static string Get_Skill_Affect_Object_Value_From_Index(int index)
        {
            switch (index)
            {
                case 0:
                    return "friend";
                case 1:
                    return "not_friend";
                case 2:
                    return "undead_real_enemy";
                case 3:
                    return "all";
                case 4:
                    return "clan";
                default:
                    return "all";
            }

        }
        public static BitmapImage GetSkillImage(string bitmapName)
        {
            string iconString = bitmapName.Replace("icon.", "") + ".png";

            List<string> pathsToImages = new List<string>()
            {
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/",
                AppDomain.CurrentDomain.BaseDirectory + @"/Images/L2Icons/skill_i/"
            };

            BitmapImage returnImage;

            for (int i = 0; i < pathsToImages.Count; i++)
            {
                if (System.IO.File.Exists(pathsToImages[i] + iconString))
                {
                    returnImage = new BitmapImage(new Uri(pathsToImages[i] + iconString, UriKind.Absolute));
                    if (returnImage != null)
                        return returnImage;
                }

            }

            return new BitmapImage(new Uri("/Images/ImageNotFound.png", UriKind.Relative));

        }

        public static string GetNextSkillLevel(List<Client_Skill> skills, string skillID, string currentLevel)
        {
            if (skills.Exists(x => x.id == skillID && x.level == currentLevel))
            {
                int newLevel = int.Parse(currentLevel);
                newLevel++;
                return GetNextSkillLevel(skills, skillID, newLevel.ToString());
            }
            else
            {
                return currentLevel;
            }
        }

        public static string GetUniqueSkillNameID(List<Server_Skilldata> skilldata, string currentSkillNameID, int newNumber = 0)
        {
            string returnString = "";

            bool skillExists = skilldata.Exists(x => x.skill_name == currentSkillNameID);

            if (skillExists)
            {
                bool numberedSkillExists = skilldata.Exists(x => x.skill_name == currentSkillNameID + "_" + newNumber);

                if (numberedSkillExists)
                    returnString = GetUniqueSkillNameID(skilldata, currentSkillNameID, newNumber + 1);
                else
                    returnString = currentSkillNameID + "_" + newNumber;
            }
            else
                returnString = currentSkillNameID;

            return returnString;
        }

        public static string GetUniqueSkillID(List<Server_Skilldata> skilldata, string currentSkillID)
        {
            string returnString = "";

            bool skillExists = skilldata.Exists(x => x.skill_id == currentSkillID);

            if (skillExists)
            {
                currentSkillID = (int.Parse(currentSkillID) + 1).ToString();
                returnString = GetUniqueSkillID(skilldata, currentSkillID);
            }
            else
                returnString = currentSkillID;

            return returnString;
        }

        //System Texts
        public static string GetUniqueSystemTextID(List<L2H_System_Text> existingTexts, int newNumber = 10000)
        {
            string returnValue = newNumber.ToString();
            if (existingTexts.Exists(x => x.ID == newNumber.ToString()))
            {
                int newerNumber = newNumber + 1;
                returnValue = GetUniqueSystemTextID(existingTexts, newerNumber);
            }

            return returnValue;

        }
    }
}