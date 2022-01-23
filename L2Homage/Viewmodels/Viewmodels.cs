using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage.Viewmodels
{
    public class ViewModel
    {
        public ViewModel()
        {
            WeaponsDataTableCollection = GetWeaponsDataTable();
            ArmorsDataTableCollection = GetArmorsDataTable();
            EtcsDataTableCollection = GetEtcsDataTable();
            SetsDataTableCollection = GetSetsDataTable();
            RecipesDataTableCollection = GetRecipesDataTable();
            NPCsDataTableCollection = GetNPCsDataTable();
            Skills_Normal_DataTableCollection = GetSkillsDataTable(SkillType.normal);
            Skills_Enchanted_DataTableCollection = GetSkillsDataTable(SkillType.enchanted);
            Skills_Sound_DataTableCollection = GetSkillsDataTable(SkillType.sound);
            Skills_Mobskillanim_DataTableCollection = GetSkillsDataTable(SkillType.mobskillanim);
            GametipsDataTableCollection = GetGametipsDataTable();
            HuntingzonesDataTableCollection = GetHuntingzonesDataTable();
            InstantzonesDataTableCollection = GetInstantzonesDataTable();
            ChatfilterDataTableCollection = GetChatfilterDataTable();
            QuestnamesDataTableCollection = GetQuestnameDataTable();
            RaidsDataTableCollection = GetRaidsDataTable();
            SystemstringsDataTableCollection = GetSystemstringsDataTable();
            SystemMessagesDataTableCollection = GetSystemMessagesDataTable();
            ZonenamesDataTableCollection = GetZonenamesDataTable();
        }
        public DataTable WeaponsDataTableCollection { get; set; }
        public DataTable ArmorsDataTableCollection { get; set; }
        public DataTable EtcsDataTableCollection { get; set; }
        public DataTable SetsDataTableCollection { get; set; }
        public DataTable RecipesDataTableCollection { get; set; }
        public DataTable NPCsDataTableCollection { get; set; }
        public DataTable Skills_Normal_DataTableCollection { get; set; }
        public DataTable Skills_Enchanted_DataTableCollection { get; set; }
        public DataTable Skills_Sound_DataTableCollection { get; set; }
        public DataTable Skills_Mobskillanim_DataTableCollection { get; set; }
        public DataTable GametipsDataTableCollection { get; set; }
        public DataTable HuntingzonesDataTableCollection { get; set; }
        public DataTable InstantzonesDataTableCollection { get; set; }
        public DataTable ChatfilterDataTableCollection { get; set; }
        public DataTable QuestnamesDataTableCollection { get; set; }
        public DataTable RaidsDataTableCollection { get; set; }
        public DataTable SystemstringsDataTableCollection { get; set; }
        public DataTable SystemMessagesDataTableCollection { get; set; }
        public DataTable ZonenamesDataTableCollection { get; set; }

        private DataTable GetWeaponsDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn() { ColumnName = "ID", DataType = typeof(int), ReadOnly = true });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Name ID", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Template", ReadOnly = true });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "In-Game Name", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Additional Name", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Type", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "SubType", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Price", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Durability", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Period", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Weight", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Crystallizable", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Crystal Type (Grade)", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Crystal Count", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "P Atk", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "M Atk", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Crit Rate", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Hit Rate", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Evasion", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Shield P Def", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Shield Rate", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Speed", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Hero" });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "MP Consume Per Attack", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Soulshot Consume", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Spiritshot Consume", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Attack Range", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Damage Range" });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Base Attribute Attack" });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Base Attribute Defense" });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Item Skill" });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Attack Skill" });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Magic Skill" });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Magic Skill Percentage" });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Enchanted to +4 Skill" });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Special Skills" });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Droppable", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Tradable", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Private Store-able", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Destructible", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Description", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Weapon Mesh 1", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Weapon Mesh 2", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Weapon Texture 1", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Weapon Texture 2", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Weapon Texture 3", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Weapon Texture 4", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Weapon Sound 1", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Weapon Sound 2", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Weapon Sound 3", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Weapon Sound 4", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Drop Sound", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Equip Sound", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Effect" });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Icon", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Icon 2", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Icon 3", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Icon 4", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Icon 5", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Drop Mesh 1", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Drop Mesh 2", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Drop Mesh 3", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Drop Texture 1", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Drop Texture 2", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Drop Texture 3", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Drop Texture Extra", DataType = typeof(string) });

            return dataTable;
        }

        private DataTable GetArmorsDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name ID");
            dataTable.Columns.Add("Template");
            dataTable.Columns.Add("In-Game Name");
            dataTable.Columns.Add("Additional Name");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("Price");
            dataTable.Columns.Add("Body Part");
            dataTable.Columns.Add("Armor Type");
            dataTable.Columns.Add("Durability");
            dataTable.Columns.Add("Period");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("Crystallizable");
            dataTable.Columns.Add("Crystal Type (Grade)");
            dataTable.Columns.Add("Crystal Count");
            dataTable.Columns.Add("Evasion");
            dataTable.Columns.Add("P Def");
            dataTable.Columns.Add("M Def");
            dataTable.Columns.Add("MP Bonus");
            dataTable.Columns.Add("Base Attribute Attack");
            dataTable.Columns.Add("Base Attribute Defense");
            dataTable.Columns.Add("Item Skill");
            dataTable.Columns.Add("Attack Skill");
            dataTable.Columns.Add("Magic Skill");
            dataTable.Columns.Add("Enchanted to +4 Skill");
            dataTable.Columns.Add("Special Skills");
            dataTable.Columns.Add("Droppable");
            dataTable.Columns.Add("Tradable");
            dataTable.Columns.Add("Private Store-able");
            dataTable.Columns.Add("Destructible");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("Armor Sound 1");
            dataTable.Columns.Add("Armor Sound 2");
            dataTable.Columns.Add("Armor Sound 3");
            dataTable.Columns.Add("Armor Sound 4");
            dataTable.Columns.Add("Drop Sound");
            dataTable.Columns.Add("Equip Sound");
            dataTable.Columns.Add("Effect");
            dataTable.Columns.Add("Icon");
            dataTable.Columns.Add("Icon 2");
            dataTable.Columns.Add("Icon 3");
            dataTable.Columns.Add("Icon 4");
            dataTable.Columns.Add("Icon 5");
            dataTable.Columns.Add("Drop Mesh 1");
            dataTable.Columns.Add("Drop Mesh 2");
            dataTable.Columns.Add("Drop Mesh 3");
            dataTable.Columns.Add("Drop Texture 1");
            dataTable.Columns.Add("Drop Texture 2");
            dataTable.Columns.Add("Drop Texture 3");
            dataTable.Columns.Add("Drop Texture Extra");
            return dataTable;
        }

        private DataTable GetEtcsDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name ID");
            dataTable.Columns.Add("Template");
            dataTable.Columns.Add("In-Game Name");
            dataTable.Columns.Add("Additional Name");
            dataTable.Columns.Add("Item Type");
            dataTable.Columns.Add("Type");
            dataTable.Columns.Add("SubType");
            dataTable.Columns.Add("Etc Type");
            dataTable.Columns.Add("Default Action");
            dataTable.Columns.Add("Consume Type");
            dataTable.Columns.Add("Item Skill");
            dataTable.Columns.Add("Initial Count");
            dataTable.Columns.Add("Maximum Count");
            dataTable.Columns.Add("Price");
            dataTable.Columns.Add("Durability");
            dataTable.Columns.Add("Period");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("Family");
            dataTable.Columns.Add("Stackable");
            dataTable.Columns.Add("Crystallizable");
            dataTable.Columns.Add("Crystal Type (Grade)");
            dataTable.Columns.Add("Crystal Count");
            dataTable.Columns.Add("Droppable");
            dataTable.Columns.Add("Tradable");
            dataTable.Columns.Add("Private Store-able");
            dataTable.Columns.Add("Destructible");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("Icon");
            dataTable.Columns.Add("Icon 2");
            dataTable.Columns.Add("Icon 3");
            dataTable.Columns.Add("Icon 4");
            dataTable.Columns.Add("Icon 5");
            dataTable.Columns.Add("Drop Mesh 1");
            dataTable.Columns.Add("Drop Mesh 2");
            dataTable.Columns.Add("Drop Mesh 3");
            dataTable.Columns.Add("Drop Texture 1");
            dataTable.Columns.Add("Drop Texture 2");
            dataTable.Columns.Add("Drop Texture 3");
            dataTable.Columns.Add("Drop Texture Extra");
            dataTable.Columns.Add("Item Sound");
            dataTable.Columns.Add("Equip Sound");
            return dataTable;
        }

        private DataTable GetSetsDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name ID");
            dataTable.Columns.Add("Template");
            dataTable.Columns.Add("Chest ID");
            dataTable.Columns.Add("Legs ID");
            dataTable.Columns.Add("Head ID");
            dataTable.Columns.Add("Gloves ID");
            dataTable.Columns.Add("Feet ID");
            dataTable.Columns.Add("Left Hand ID");
            dataTable.Columns.Add("Additional Slot");
            dataTable.Columns.Add("Set Skill ID");
            dataTable.Columns.Add("Extra Pieces IDs");
            dataTable.Columns.Add("Additional Effect Skill");
            dataTable.Columns.Add("Str Bonus");
            dataTable.Columns.Add("Con Bonus");
            dataTable.Columns.Add("Dex Bonus");
            dataTable.Columns.Add("Int Bonus");
            dataTable.Columns.Add("Men Bonus");
            dataTable.Columns.Add("Wit Bonus");
            dataTable.Columns.Add("Set Bonus Description");
            dataTable.Columns.Add("Set Bonus Extra Description");
            dataTable.Columns.Add("+Enchants For Enchant Effect");
            dataTable.Columns.Add("Enchant Effect Skill ID");
            dataTable.Columns.Add("Enchant Effect Description");

            return dataTable;
        }

        private DataTable GetRecipesDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Recipe Name ID");
            dataTable.Columns.Add("Recipe Item ID");
            dataTable.Columns.Add("Recipe Item Name ID");
            dataTable.Columns.Add("Level");
            dataTable.Columns.Add("Catalyst");
            dataTable.Columns.Add("MP Cost");
            dataTable.Columns.Add("Success Rate");
            dataTable.Columns.Add("Is Common");
            dataTable.Columns.Add("Product 1 ID");
            dataTable.Columns.Add("Product 1 Amount");
            dataTable.Columns.Add("Product 1 Probability");
            dataTable.Columns.Add("Product 2 ID");
            dataTable.Columns.Add("Product 2 Amount");
            dataTable.Columns.Add("Product 2 Probability");
            dataTable.Columns.Add("Material 1 ID");
            dataTable.Columns.Add("Material 1 Amount");
            dataTable.Columns.Add("Material 2 ID");
            dataTable.Columns.Add("Material 2 Amount");
            dataTable.Columns.Add("Material 3 ID");
            dataTable.Columns.Add("Material 3 Amount");
            dataTable.Columns.Add("Material 4 ID");
            dataTable.Columns.Add("Material 4 Amount");
            dataTable.Columns.Add("Material 5 ID");
            dataTable.Columns.Add("Material 5 Amount");
            dataTable.Columns.Add("Material 6 ID");
            dataTable.Columns.Add("Material 6 Amount");
            dataTable.Columns.Add("Material 7 ID");
            dataTable.Columns.Add("Material 7 Amount");
            dataTable.Columns.Add("Material 8 ID");
            dataTable.Columns.Add("Material 8 Amount");
            dataTable.Columns.Add("Material 9 ID");
            dataTable.Columns.Add("Material 9 Amount");
            dataTable.Columns.Add("Material 10 ID");
            dataTable.Columns.Add("Material 10 Amount");
            dataTable.Columns.Add("Material 11 ID");
            dataTable.Columns.Add("Material 11 Amount");
            dataTable.Columns.Add("HTML");
            dataTable.Columns.Add("Extra Material");


            return dataTable;
        }

        private DataTable GetNPCsDataTable()
        {
            DataTable dataTable = new DataTable();

            //DataColumn d = new DataColumn();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Template");
            dataTable.Columns.Add("Name ID");
            dataTable.Columns.Add("In Game Name");
            dataTable.Columns.Add("Category");
            dataTable.Columns.Add("Level");
            dataTable.Columns.Add("Exp");
            dataTable.Columns.Add("Crit Effect");
            dataTable.Columns.Add("Unique?");
            dataTable.Columns.Add("Race");
            dataTable.Columns.Add("Sex");
            dataTable.Columns.Add("Skill List");
            dataTable.Columns.Add("Chest Slot");
            dataTable.Columns.Add("Hand (R) Slot");
            dataTable.Columns.Add("Hand (L) Slot");
            dataTable.Columns.Add("Collision Radius");
            dataTable.Columns.Add("Collision Height");
            dataTable.Columns.Add("Hit time factor");
            dataTable.Columns.Add("Hit time factor skill");
            dataTable.Columns.Add("strength");
            dataTable.Columns.Add("intelligence");
            dataTable.Columns.Add("dexterity");
            dataTable.Columns.Add("wit");
            dataTable.Columns.Add("con");
            dataTable.Columns.Add("men");
            dataTable.Columns.Add("HP");
            dataTable.Columns.Add("HP Regen");
            dataTable.Columns.Add("MP");
            dataTable.Columns.Add("MP Regen");
            dataTable.Columns.Add("Attack Type");
            dataTable.Columns.Add("Attack Range");
            dataTable.Columns.Add("Damage Range");
            dataTable.Columns.Add("Random Damage");
            dataTable.Columns.Add("Phys Damage");
            dataTable.Columns.Add("Critical");
            dataTable.Columns.Add("Phys Hit Modifier");
            dataTable.Columns.Add("Attack Speed");
            dataTable.Columns.Add("Reuse Delay");
            dataTable.Columns.Add("Magic Attack");
            dataTable.Columns.Add("Defense");
            dataTable.Columns.Add("Magic Defense");
            dataTable.Columns.Add("Base Attribute Attack");
            dataTable.Columns.Add("Base Attribute Defend");
            dataTable.Columns.Add("Physical Avoid Modifier");
            dataTable.Columns.Add("Shield Defense Rate");
            dataTable.Columns.Add("Shield Defense");
            dataTable.Columns.Add("Safe Height");
            dataTable.Columns.Add("Soulshots");
            dataTable.Columns.Add("Spiritshots");
            dataTable.Columns.Add("Clan");
            dataTable.Columns.Add("Ignore Clan");
            dataTable.Columns.Add("Clan Help Range");
            dataTable.Columns.Add("Undying");
            dataTable.Columns.Add("Can Be Attacked");
            dataTable.Columns.Add("Corpse Duration");
            dataTable.Columns.Add("No Sleep Mode");
            dataTable.Columns.Add("Agro Range");
            dataTable.Columns.Add("Passable Door");
            dataTable.Columns.Add("Can Move");
            dataTable.Columns.Add("Flying");
            dataTable.Columns.Add("Has Summoner");
            dataTable.Columns.Add("Targetable");
            dataTable.Columns.Add("Show Name Tag");
            dataTable.Columns.Add("AI ID");
            dataTable.Columns.Add("AI Minions");
            dataTable.Columns.Add("Event Flag");
            dataTable.Columns.Add("Unsowing");
            dataTable.Columns.Add("EXP On Kill");
            dataTable.Columns.Add("SP On Kill");
            dataTable.Columns.Add("RP On Kill");
            dataTable.Columns.Add("Spoil Droplist ID");
            dataTable.Columns.Add("Single Droplist ID");
            dataTable.Columns.Add("Multi Droplist ID");
            dataTable.Columns.Add("Extra Multi Droplist ID");
            dataTable.Columns.Add("Description");
            return dataTable;
        }
        //private DataTable GetDroplistsDataTable(DroplistType type)
        //{
        //    DataTable dataTable = new DataTable();

        //    switch (type)
        //    {
        //        case DroplistType.normal:
        //            {
        //                int normalDroplistItemLimit = 20;

        //                dataTable.Columns.Add("ID");
        //                dataTable.Columns.Add("Template");

        //                int numberOfItemsCompleted = 0;

        //                for (int i = 2; i < normalDroplistItemLimit + 2; i++)
        //                {
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Name");
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Min Amount");
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Max Amount");
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Probability");

        //                    numberOfItemsCompleted++;
        //                }

        //                return dataTable;
        //            }
        //        case DroplistType.spoil:
        //            {
        //                int spoilDroplistItemLimit = 20;

        //                dataTable.Columns.Add("ID");
        //                dataTable.Columns.Add("Template");

        //                int numberOfItemsCompleted = 0;

        //                for (int i = 2; i < spoilDroplistItemLimit + 2; i++)
        //                {
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Name");
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Min Amount");
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Max Amount");
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Probability");

        //                    numberOfItemsCompleted++;
        //                }

        //                return dataTable;
        //            }

        //        case DroplistType.multi:
        //            {
        //                int multiDroplistsLimit = 50;


        //                dataTable.Columns.Add("ID");
        //                dataTable.Columns.Add("Template");

        //                int numberOfItemsCompleted = 0;

        //                for (int i = 2; i < multiDroplistsLimit + 2; i++)
        //                {
        //                    dataTable.Columns.Add("MultiPart " + (numberOfItemsCompleted + 1) + " ID");
        //                    dataTable.Columns.Add("MultiPart " + (numberOfItemsCompleted + 1) + " Probability");

        //                    numberOfItemsCompleted++;
        //                }

        //                return dataTable;
        //            }
        //        case DroplistType.multipart:
        //            {
        //                int multipartDroplistItemLimit = 40;

        //                dataTable.Columns.Add("ID");
        //                dataTable.Columns.Add("Template");
        //                dataTable.Columns.Add("Source ID");

        //                int numberOfItemsCompleted = 0;

        //                for (int i = 3; i < multipartDroplistItemLimit + 3; i++)
        //                {
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Name");
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Min Amount");
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Max Amount");
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Probability");

        //                    numberOfItemsCompleted++;
        //                }

        //                return dataTable;
        //            }
        //        case DroplistType.extra:
        //            {
        //                int extraDroplistsLimit = 50;

        //                dataTable.Columns.Add("ID");
        //                dataTable.Columns.Add("Template");

        //                int numberOfItemsCompleted = 0;

        //                for (int i = 2; i < extraDroplistsLimit + 2; i++)
        //                {
        //                    dataTable.Columns.Add("ExtraPart " + (numberOfItemsCompleted + 1) + " ID");
        //                    dataTable.Columns.Add("ExtraPart " + (numberOfItemsCompleted + 1) + " Probability");

        //                    numberOfItemsCompleted++;
        //                }

        //                return dataTable;
        //            }
        //        case DroplistType.extrapart:
        //            {
        //                int extrapartDroplistItemLimit = 40;


        //                dataTable.Columns.Add("ID");
        //                dataTable.Columns.Add("Template");
        //                dataTable.Columns.Add("Source ID");

        //                int numberOfItemsCompleted = 0;

        //                for (int i = 3; i < extrapartDroplistItemLimit + 3; i++)
        //                {
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Name");
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Min Amount");
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Max Amount");
        //                    dataTable.Columns.Add("Drop " + (numberOfItemsCompleted + 1) + " Probability");

        //                    numberOfItemsCompleted++;
        //                }

        //                return dataTable;
        //            }
        //        default:
        //            {
        //                return dataTable;
        //            }

        //    }

        //}
        private DataTable GetSkillsDataTable(SkillType type)
        {
            DataTable dataTable = new DataTable();


            switch (type)
            {
                case SkillType.normal:

                    dataTable.Columns.Add("ID");
                    dataTable.Columns.Add("Template");
                    dataTable.Columns.Add("Name ID");
                    dataTable.Columns.Add("In-Game Name");
                    dataTable.Columns.Add("Description");
                    dataTable.Columns.Add("Desc?");
                    dataTable.Columns.Add("Add. Description");
                    dataTable.Columns.Add("Add. Description 2");
                    dataTable.Columns.Add("Cast Style");
                    dataTable.Columns.Add("Animation");
                    dataTable.Columns.Add("Icon Name");
                    dataTable.Columns.Add("Icon Name 2");
                    dataTable.Columns.Add("Is Enchanted");
                    dataTable.Columns.Add("Enchanted Skill ID");
                    dataTable.Columns.Add("Level");
                    dataTable.Columns.Add("Client Operate Type");
                    dataTable.Columns.Add("Server Operate Type");
                    dataTable.Columns.Add("Magic Level");				
                    dataTable.Columns.Add("start_effect");				
                    dataTable.Columns.Add("self_effect");				
                    dataTable.Columns.Add("effect");					
                    dataTable.Columns.Add("pvp_effect");				
                    dataTable.Columns.Add("pve_effect");				
                    dataTable.Columns.Add("tick_effect");				
                    dataTable.Columns.Add("fail_effect");				
                    dataTable.Columns.Add("end_effect");				
                    dataTable.Columns.Add("tick_interval");				
                    dataTable.Columns.Add("attached_skill");			
                    dataTable.Columns.Add("operate_cond");				
                    dataTable.Columns.Add("target_operate_cond");		
                    dataTable.Columns.Add("is_magic");					
                    dataTable.Columns.Add("mp_consume1");				
                    dataTable.Columns.Add("mp_consume2");				
                    dataTable.Columns.Add("mp_consume_tick");			
                    dataTable.Columns.Add("hp_consume");				
                    dataTable.Columns.Add("item_consume");				
                    dataTable.Columns.Add("consume_etc");				
                    dataTable.Columns.Add("cast_range");				
                    dataTable.Columns.Add("effective_range");			
                    dataTable.Columns.Add("skill_hit_time");			
                    dataTable.Columns.Add("skill_cool_time");			
                    dataTable.Columns.Add("skill_hit_cancel_time");		
                    dataTable.Columns.Add("reuse_delay");				
                    dataTable.Columns.Add("reuse_delay_lock");			
                    dataTable.Columns.Add("reuse_delay_type");			
                    dataTable.Columns.Add("activate_rate");				
                    dataTable.Columns.Add("lv_bonus_rate");				
                    dataTable.Columns.Add("basic_property");			
                    dataTable.Columns.Add("abnormal_time");				
                    dataTable.Columns.Add("abnormal_lv");				
                    dataTable.Columns.Add("abnormal_type");				
                    dataTable.Columns.Add("abnormal_instant");			
                    dataTable.Columns.Add("irreplaceable_buff");		
                    dataTable.Columns.Add("buff_protect_level");		
                    dataTable.Columns.Add("abnormal_delete_leaveworld");
                    dataTable.Columns.Add("attribute");					
                    dataTable.Columns.Add("trait");						
                    dataTable.Columns.Add("effect_point");				
                    dataTable.Columns.Add("target_type");				
                    dataTable.Columns.Add("affect_scope");				
                    dataTable.Columns.Add("affect_range");				
                    dataTable.Columns.Add("affect_object");				
                    dataTable.Columns.Add("fan_range");					
                    dataTable.Columns.Add("affect_limit");				
                    dataTable.Columns.Add("next_action");				
                    dataTable.Columns.Add("transform_type");			
                    dataTable.Columns.Add("abnormal_visual_effect");	
                    dataTable.Columns.Add("debuff");					
                    dataTable.Columns.Add("ride_state");				
                    dataTable.Columns.Add("passive_conditions");		
                    dataTable.Columns.Add("affect_scope_height");		
                    dataTable.Columns.Add("multi_class");
                    dataTable.Columns.Add("olympiad_use");

                    return dataTable;

                case SkillType.enchanted:

                    dataTable.Columns.Add("enchant_id");
                    dataTable.Columns.Add("original_skill");
                    dataTable.Columns.Add("route_id");
                    dataTable.Columns.Add("skill_level");
                    dataTable.Columns.Add("importance");
                    dataTable.Columns.Add("required_item");
                    

                    int levelsDone = 0;

                    for (int i = 0; i < 30; i++)
                    {
                        dataTable.Columns.Add("lvl " + (i + 1) + " adena cost");
                        dataTable.Columns.Add("lvl " + (i + 1) + " SP cost");

                        levelsDone++;

                    }

                    return dataTable;

                case SkillType.sound:

                    dataTable.Columns.Add("ID");
                    dataTable.Columns.Add("Name ID");
                    dataTable.Columns.Add("level");
                    dataTable.Columns.Add("spelleffect_sound_1");
                    dataTable.Columns.Add("spelleffect_sound_2");
                    dataTable.Columns.Add("spelleffect_sound_3");
                    dataTable.Columns.Add("spelleffect_sound_vol_1");
                    dataTable.Columns.Add("spelleffect_sound_rad_1");
                    dataTable.Columns.Add("spelleffect_sound_vol_2");
                    dataTable.Columns.Add("spelleffect_sound_rad_2");
                    dataTable.Columns.Add("spelleffect_sound_vol_3");
                    dataTable.Columns.Add("spelleffect_sound_rad_3");
                    dataTable.Columns.Add("shoteffect_sound_1");
                    dataTable.Columns.Add("shoteffect_sound_2");
                    dataTable.Columns.Add("shoteffect_sound_3");
                    dataTable.Columns.Add("shoteffect_sound_vol_1");
                    dataTable.Columns.Add("shoteffect_sound_rad_1");
                    dataTable.Columns.Add("shoteffect_sound_vol_2");
                    dataTable.Columns.Add("shoteffect_sound_rad_2");
                    dataTable.Columns.Add("shoteffect_sound_vol_3");
                    dataTable.Columns.Add("shoteffect_sound_rad_3");
                    dataTable.Columns.Add("expeffect_sound_1");
                    dataTable.Columns.Add("expeffect_sound_2");
                    dataTable.Columns.Add("expeffect_sound_3");
                    dataTable.Columns.Add("expeffect_sound_vol_1");
                    dataTable.Columns.Add("expeffect_sound_rad_1");
                    dataTable.Columns.Add("expeffect_sound_vol_2");
                    dataTable.Columns.Add("expeffect_sound_rad_2");
                    dataTable.Columns.Add("expeffect_sound_vol_3");
                    dataTable.Columns.Add("expeffect_sound_rad_3");
                    dataTable.Columns.Add("mfighter_sub");
                    dataTable.Columns.Add("ffighter_sub");
                    dataTable.Columns.Add("mdarkelf_sub");
                    dataTable.Columns.Add("fdarkelf_sub");
                    dataTable.Columns.Add("mdwarf_sub");
                    dataTable.Columns.Add("fdwarf_sub");
                    dataTable.Columns.Add("melf_sub");
                    dataTable.Columns.Add("felf_sub");
                    dataTable.Columns.Add("mmagic_sub");
                    dataTable.Columns.Add("fmagic_sub");
                    dataTable.Columns.Add("morc_sub");
                    dataTable.Columns.Add("forc_sub");
                    dataTable.Columns.Add("mshaman_sub");
                    dataTable.Columns.Add("fshaman_sub");
                    dataTable.Columns.Add("mkamael_sub");
                    dataTable.Columns.Add("fkamael_sub");
                    dataTable.Columns.Add("mfighter_throw");
                    dataTable.Columns.Add("ffighter_throw");
                    dataTable.Columns.Add("mdarkelf_throw");
                    dataTable.Columns.Add("fdarkelf_throw");
                    dataTable.Columns.Add("mdwarf_throw");
                    dataTable.Columns.Add("fdwarf_throw");
                    dataTable.Columns.Add("melf_throw");
                    dataTable.Columns.Add("felf_throw");
                    dataTable.Columns.Add("mmagic_throw");
                    dataTable.Columns.Add("fmagic_throw");
                    dataTable.Columns.Add("morc_throw");
                    dataTable.Columns.Add("forc_throw");
                    dataTable.Columns.Add("mshaman_throw");
                    dataTable.Columns.Add("fshaman_throw");
                    dataTable.Columns.Add("mkamael_throw");
                    dataTable.Columns.Add("fkamael_throw");
                    dataTable.Columns.Add("mextra_throw");
                    dataTable.Columns.Add("fextra_throw");
                    dataTable.Columns.Add("sound_vol");
                    dataTable.Columns.Add("sound_rad");


                    return dataTable;

                case SkillType.mobskillanim:
                    {

                        dataTable.Columns.Add("Npc ID");
                        dataTable.Columns.Add("Template");
                        dataTable.Columns.Add("Npc Name");
                        dataTable.Columns.Add("Skill ID");
                        dataTable.Columns.Add("Skill Name");
                        dataTable.Columns.Add("Seq Name");
                        dataTable.Columns.Add("Npc Class");



                        break;
                    }
            }

            return dataTable;


        }
        private DataTable GetGametipsDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Int1");
            dataTable.Columns.Add("Int2");
            dataTable.Columns.Add("Enabled");
            dataTable.Columns.Add("Tip");

            return dataTable;
        }
        private DataTable GetHuntingzonesDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Hunting Type");
            dataTable.Columns.Add("Level");
            dataTable.Columns.Add("unk_1");
            dataTable.Columns.Add("loc_x");
            dataTable.Columns.Add("loc_y");
            dataTable.Columns.Add("loc_z");
            dataTable.Columns.Add("extra");
            dataTable.Columns.Add("affiliated_area_id");

            return dataTable;
        }
        private DataTable GetInstantzonesDataTable()
        {
            DataTable dataTable = new DataTable();


            return dataTable;
        }
        private DataTable GetChatfilterDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Text");

            return dataTable;
        }
        private DataTable GetQuestnameDataTable()
        {
            DataTable dataTable = new DataTable();


            return dataTable;
        }
        private DataTable GetRaidsDataTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("npc_id");
            dataTable.Columns.Add("npc_level");
            dataTable.Columns.Add("affiliated_area_id");
            dataTable.Columns.Add("loc_x");
            dataTable.Columns.Add("loc_y");
            dataTable.Columns.Add("loc_z");
            dataTable.Columns.Add("raid_desc");
            return dataTable;
        }
        private DataTable GetSystemstringsDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Text");

            return dataTable;
        }
        private DataTable GetSystemMessagesDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("message");
            dataTable.Columns.Add("sub_msg");
            dataTable.Columns.Add("type");
            dataTable.Columns.Add("UNK_0");
            dataTable.Columns.Add("group");
            dataTable.Columns.Add("rgba_0");
            dataTable.Columns.Add("rgba_1");
            dataTable.Columns.Add("rgba_2");
            dataTable.Columns.Add("rgba_3");
            dataTable.Columns.Add("item_sound");
            dataTable.Columns.Add("sys_msg_ref");
            dataTable.Columns.Add("UNK_1_0");
            dataTable.Columns.Add("UNK_1_1");
            dataTable.Columns.Add("UNK_1_2");
            dataTable.Columns.Add("UNK_1_3");
            dataTable.Columns.Add("UNK_1_4");

            return dataTable;
        }
        private DataTable GetZonenamesDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("nbr");
            dataTable.Columns.Add("zone_name");
            dataTable.Columns.Add("zone_color_id");
            dataTable.Columns.Add("x_world_grid");
            dataTable.Columns.Add("y_world_grid");
            dataTable.Columns.Add("top_z");
            dataTable.Columns.Add("bottom_z");
            dataTable.Columns.Add("coords_0");
            dataTable.Columns.Add("coords_1");
            dataTable.Columns.Add("coords_2");
            dataTable.Columns.Add("coords_3");
            dataTable.Columns.Add("coords_4");
            dataTable.Columns.Add("coords_5");
            dataTable.Columns.Add("unk02");
            dataTable.Columns.Add("map");
            dataTable.Columns.Add("dupa");

            return dataTable;
        }


    }
}
