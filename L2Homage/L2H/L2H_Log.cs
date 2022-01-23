using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace L2Homage
{
    public class L2H_Log
    {
        private static L2H_Log instance = null;

        SolidColorBrush Color_Category;
        SolidColorBrush Color_Add;
        SolidColorBrush Color_Parameter_Name;
        SolidColorBrush Color_Remove;
        SolidColorBrush Color_Modify;
        public static L2H_Log Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new L2H_Log();
                    instance.Color_Category = new SolidColorBrush((Color)ColorConverter.ConvertFromString(L2H_Constants.Color_Category));
                    instance.Color_Add = new SolidColorBrush((Color)ColorConverter.ConvertFromString(L2H_Constants.Color_Add));
                    instance.Color_Parameter_Name = new SolidColorBrush((Color)ColorConverter.ConvertFromString(L2H_Constants.Color_Parameter_Name));
                    instance.Color_Remove = new SolidColorBrush((Color)ColorConverter.ConvertFromString(L2H_Constants.Color_Remove));
                    instance.Color_Modify = new SolidColorBrush((Color)ColorConverter.ConvertFromString(L2H_Constants.Color_Modify));
                }
                return instance;
            }
        }

        void Add_Run_To_Paragraph(Paragraph paragraph, string text, SolidColorBrush color)
        {
            var run = new Run(text)
            {
                Foreground = color
            };
            paragraph.Inlines.Add(run);
        }
        #region Items
        public void Log_Item_Clone(L2H_Item item, L2H_Item newItem)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "CLONE ITEM\t ", Color_Add);
            Add_Run_To_Paragraph(paragraph, "ITEM: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, item.Item_Name + " (ID: " + item.ID + ")" + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "NEW ITEM: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, newItem.Item_Name + " (ID: " + newItem.ID + ")", Color_Parameter_Name);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "CLONE ITEM", item.Item_Name + "\n(ID: " + item.ID + ")", "NEW ITEM", newItem.Item_Name + "\n(ID: " + newItem.ID + ")");
        }
        public void Log_Item_Delete(L2H_Item item)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "DELETE ITEM\t ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, "ITEM: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, item.Item_Name + " (ID: " + item.ID + ")", Color_Parameter_Name);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Delete, "DELETE ITEM", item.Item_Name + "\n(ID: " + item.ID + ")");
        }
        public void Log_Item_Change(L2H_Item item, string propertyName, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {
                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY ITEM\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "ITEM: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, item.Item_Name + " (" + item.ID + ")" + "\t ", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, "PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, propertyName + "\t ", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, "OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue, Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY ITEM", item.Item_Name + " (" + item.ID + ")", propertyName, newValue);
            }
        }
        #endregion
        #region Sets
        public void Log_Set_Change(L2H_Set set, string property, string oldValue, string newValue)
        {

            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY SET\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "SET: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, set.Set_Name + " (" + set.ID + ")" + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, property + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY SET", set.ID.ToString(), property, newValue);


            }

        }
        public void Log_Set_Add(L2H_Set set)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "CREATE SET\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " SET: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, set.ID.ToString(), Color_Category);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "CREATE SET", set.ID.ToString());
        }
        public void Log_Set_Remove(L2H_Set set)
        {

        }
        #endregion

        #region Recipes
        public void Log_Recipe_Change(L2H_Recipe recipe, string property, string oldValue, string newValue)
        {

            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY RECIPE\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " RECIPE: ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, recipe.Recipe_Name + " (" + recipe.ID + ")" + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, property + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY RECIPE", recipe.Recipe_Name + "\n(" + recipe.ID + ")", property, newValue);

            }

        }
        public void Log_Recipe_Material_Add(L2H_Recipe recipe, L2H_Item material)
        {

            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "MODIFY RECIPE\t", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " RECIPE: ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, recipe.Recipe_Name + " (" + recipe.ID + ")" + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " ADD MATERIAL: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, material.client_Itemname.name + " (" + material.ID + ")" + "\t", Color_Parameter_Name);


            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY RECIPE", recipe.Recipe_Name + "\n(" + recipe.ID + ")", "ADD MATERIAL", material.client_Itemname.name + "\n(" + material.ID + ")");

        }
        public void Log_Recipe_Material_Remove(L2H_Recipe recipe, L2H_Recipe_Material material)
        {

            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "MODIFY RECIPE\t", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " RECIPE: ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, recipe.Recipe_Name + " (" + recipe.ID + ")" + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " REMOVE MATERIAL: ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, material.item.client_Itemname.name + " (" + material.item.ID + ")" + "\t", Color_Parameter_Name);


            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY RECIPE", recipe.Recipe_Name + "\n(" + recipe.ID + ")", "REMOVE MATERIAL", material.item.client_Itemname.name + "\n(" + material.item.ID + ")");

        }
        public void Log_Recipe_Result_Add(L2H_Recipe recipe, L2H_Item item)
        {

            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "MODIFY RECIPE\t", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " RECIPE: ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, recipe.Recipe_Name + " (" + recipe.ID + ")" + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " ADD RESULT: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, item.client_Itemname.name + " (" + item.ID + ")" + "\t", Color_Parameter_Name);


            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY RECIPE", recipe.Recipe_Name + "\n(" + recipe.ID + ")", "ADD RESULT", item.client_Itemname.name + "\n(" + item.ID + ")");

        }
        public void Log_Recipe_Result_Remove(L2H_Recipe recipe, L2H_Item item)
        {

            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "MODIFY RECIPE\t", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " RECIPE: ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, recipe.Recipe_Name + " (" + recipe.ID + ")" + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " REMOVE RESULT: ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, item.client_Itemname.name + " (" + item.ID + ")" + "\t", Color_Parameter_Name);


            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY RECIPE", recipe.Recipe_Name + "\n(" + recipe.ID + ")", "REMOVE RESULT", item.client_Itemname.name + "\n(" + item.ID + ")");

        }
        #endregion

        #region Droplists
        public void Log_Droplist_Change_Single_Property(L2H_Droplist target, string property, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY SINGLE DROPLIST\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " DROPLIST: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, target.ID + "\t ", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, property + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);

                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY SINGLE DROPLIST", target.ID, property, newValue);

            }
        }
        public void Log_Droplist_Add_Single_Drop(L2H_Droplist target, L2H_Item drop)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "MODIFY SINGLE DROPLIST\t", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " DROPLIST: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, target.ID + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " ADD ITEM DROP: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, drop.Item_Name + "\t", Color_Parameter_Name);

            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "ADD TO SINGLE DROPLIST", target.ID, "ITEM DROP", drop.Item_Name + "\n(" + drop.ID + ")");
        }
        public void Log_Droplist_Remove_Single_Drop(L2H_Droplist target, L2H_Item drop)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "MODIFY SINGLE DROPLIST\t", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " DROPLIST: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, target.ID + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " REMOVE ITEM DROP: ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, drop.Item_Name + "\t", Color_Parameter_Name);

            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Delete, "REMOVE FROM SINGLE DROPLIST", target.ID, "ITEM", drop.Item_Name + "\n(" + drop.ID + ")");
        }
        public void Log_Droplist_Add_Single_Droplist_To_Multi_Droplist(L2H_Droplist multiDroplist, L2H_Droplist singleDroplist)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "MODIFY MULTI DROPLIST\t", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " DROPLIST: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, multiDroplist.ID + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " ADD SINGLE DROPLIST: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, singleDroplist.ID + "\t", Color_Parameter_Name);

            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "ADD CONNECTION TO MULTI DROPLIST", multiDroplist.ID, "SINGLE DROPLIST", singleDroplist.ID);
        }


        public void Log_Droplist_Add_Droplist(L2H_Droplist target)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "CREATE DROPLIST\t ", Color_Add);
            Add_Run_To_Paragraph(paragraph, " DROPLIST: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, target.ID.ToString() + "\t ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "DROPLIST TYPE: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, target.Droplist_Type.ToString(), Color_Category);

            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "CREATE DROPLIST", target.ID.ToString());
        }

        #endregion

        #region spawns
        public void Log_Spawn_Area_Clone(string oldValue, string newValue)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "CLONE SPAWN AREA\t ", Color_Add);
            Add_Run_To_Paragraph(paragraph, "SPAWN AREA: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, oldValue + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "NEW AREA ID: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, newValue, Color_Parameter_Name);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "CLONE SPAWN AREA", oldValue, "NEW ID", newValue);
        }
        public void Log_Spawn_Area_Delete(string ID)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "DELETE SPAWN AREA\t ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, "SPAWN AREA: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, ID, Color_Parameter_Name);


            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Delete, "DELETE SPAWN AREA", ID);
        }

        public void Log_Spawn_NPC_Spawn_Changed(L2H_Spawn_NPC_Maker_NPC_Spawn npcSpawn, string property, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {
                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY NPC SPAWN\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "SPAWN AREA: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, ((System.Windows.Application.Current.MainWindow as MainWindow).GetPageOfType(typeof(Pages.SpawnsPage)) as Pages.SpawnsPage).Get_Active_Spawn_Area.Area_Name + "\t ", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, "NPC MAKER: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, npcSpawn.Npc_Maker.Maker_Name + "\t ", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, "NPC SPAWN: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, npcSpawn.L2H_Npc.client_Npcname.name + " (ID:" + npcSpawn.L2H_Npc.ID + " Lv:" + npcSpawn.L2H_Npc.server_Npcdata.level + ")" + "\t ", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, "PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, property + "\t ", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, "OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue, Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY NPC SPAWN", npcSpawn.L2H_Npc.client_Npcname.name + "\n(ID:" + npcSpawn.L2H_Npc.ID + " Lv:" + npcSpawn.L2H_Npc.server_Npcdata.level + ")", property, newValue);
            }
        }
        public void Log_Spawn_NPC_Spawn_Add(L2H_Spawn_NPC_Maker maker, L2H_NPC npc)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "ADD SPAWN\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, "SPAWN AREA: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, ((System.Windows.Application.Current.MainWindow as MainWindow).GetPageOfType(typeof(Pages.SpawnsPage)) as Pages.SpawnsPage).Get_Active_Spawn_Area.Area_Name + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "NPC MAKER: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, maker.Maker_Name + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "NPC SPAWN: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, npc.client_Npcname.name + " (ID:" + npc.ID + " Lv:" + npc.server_Npcdata.level + ")", Color_Parameter_Name);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "ADD NPC TO SPAWN AREA", ((System.Windows.Application.Current.MainWindow as MainWindow).GetPageOfType(typeof(Pages.SpawnsPage)) as Pages.SpawnsPage).Get_Active_Spawn_Area.Area_Name, "NPC", npc.client_Npcname.name + " (ID:" + npc.ID + " Lv:" + npc.server_Npcdata.level + ")");

        }
        public void Log_Spawn_Maker_Changed(L2H_Spawn_NPC_Maker maker, string property, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {
                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY SPAWN MAKER\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "SPAWN MAKER: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, maker.Maker_Name + "\t ", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, "PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, property + "\t ", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, "OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue, Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY NPC MAKER", maker.Maker_Name, property, newValue);
            }
        }
        public void Log_Spawn_NPC_Maker_Add(L2H_Spawn_Area area, L2H_Spawn_NPC_Maker maker)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "CREATE NPC MAKER\t ", Color_Add);
            Add_Run_To_Paragraph(paragraph, "SPAWN AREA: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, area.Area_Name + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "NPC MAKER: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, maker.Maker_Name, Color_Parameter_Name);


            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "ADD NPC MAKER TO AREA", area.Area_Name, "NPC MAKER", maker.Maker_Name);
        }
        public void Log_Spawn_NPC_Maker_Delete(L2H_Spawn_Area area, L2H_Spawn_NPC_Maker maker)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "DELETE NPC MAKER\t ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, "SPAWN AREA: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, area.Area_Name + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "NPC MAKER: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, maker.Maker_Name, Color_Parameter_Name);


            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Delete, "REMOVE NPC MAKER FROM AREA", area.Area_Name, "NPC MAKER", maker.Maker_Name);
        }
        #endregion

        #region NPCs
        public void Log_NPC_Clone(L2H_NPC npc, L2H_NPC newNpc)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "CLONE NPC\t ", Color_Add);
            Add_Run_To_Paragraph(paragraph, "NPC: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, npc.NPC_Name + " (ID: " + npc.NPC_ID + ")\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "NEW NPC: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, newNpc.NPC_Name + " (ID: " + newNpc.NPC_ID + ")\t", Color_Parameter_Name);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "CLONE NPC", npc.NPC_Name + "\n(ID: " + npc.NPC_ID + ")", "NEW NPC", newNpc.NPC_Name + "\n(ID: " + newNpc.NPC_ID + ")");
        }
        public void Log_NPC_Changed(L2H_NPC npc, string property, string oldValue, string newValue)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "MODIFY NPC\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, "NPC: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, npc.client_Npcname.name + " (ID:" + npc.ID + " Lv:" + npc.server_Npcdata.level + ")" + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "PROPERTY: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, property + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "OLD VALUE: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, oldValue + "\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, "NEW VALUE: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, newValue, Color_Modify);


            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY NPC", npc.client_Npcname.name + "\n(ID:" + npc.ID + " Lv:" + npc.server_Npcdata.level + ")", property, newValue);
        }
        public void Log_NPC_Delete(L2H_NPC npc)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "DELETE NPC\t ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, "NPC: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, npc.NPC_Name + " (ID: " + npc.NPC_ID + ")", Color_Parameter_Name);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Delete, "DELETE NPC", npc.NPC_Name + "\n(ID: " + npc.NPC_ID + ")");
        }
        #endregion

        #region Skills
        public void Log_Skill_Clone(L2H_Skill originalSkill, L2H_Skill newSkill)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "CLONE SKILL\t ", Color_Add);
            Add_Run_To_Paragraph(paragraph, "SKILL: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, originalSkill.Skill_Name + " (ID: " + originalSkill.ID + ")" + " (Lv: " + originalSkill.Skill_Level + ")\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "NEW SKILL: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, newSkill.Skill_Name + " (ID: " + newSkill.ID + ")" + " (Lv: " + newSkill.Skill_Level + ")\t ", Color_Parameter_Name);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "CLONE SKILL", originalSkill.Skill_Name + "\n(ID: " + originalSkill.ID + ") (Lv:" + originalSkill.Skill_Level + ")", "NEW SKILL", newSkill.Skill_Name + "\n(ID: " + newSkill.ID + ") (Lv:" + newSkill.Skill_Level + ")");
        }
        public void Log_Skill_Change(L2H_Skill skill, string property, string oldValue, string newValue)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "MODIFY SKILL\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, "SKILL: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, skill.Skill_Name + " (" + skill.Skill_ID + ")" + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " PROPERTY: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, property + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY SKILL", skill.Skill_ID, property, newValue);
        }
        public void Log_Skill_Delete(L2H_Skill skill)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "DELETE SKILL\t ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, "SKILL: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, skill.Skill_Name + " (ID: " + skill.Skill_ID + ")", Color_Parameter_Name);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Delete, "DELETE SKILL", skill.Skill_Name + "\n(ID: " + skill.Skill_ID + ")");
        }

        #endregion

        #region System Texts
        #region Add
        public void Log_System_Text_Add(L2H_System_Text obj, string systemTextName)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "ADD " + systemTextName + "\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, systemTextName + ": ", Color_Add);
            Add_Run_To_Paragraph(paragraph, obj.ID.ToString(), Color_Category);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "CREATE " + systemTextName, obj.ID.ToString());
        }
        #endregion
        #region Modify
        public void Log_System_Text_Change(L2H_System_Text obj, string systemTextName, string property, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {
                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY " + systemTextName + "\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, systemTextName + ": ", Color_Category);
                Add_Run_To_Paragraph(paragraph, obj.ID + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, property + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);

                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY " + systemTextName, obj.ID.ToString(), property, newValue);

            }
        }

        #endregion
        #region Delete
        public void Log_System_Text_Delete(L2H_System_Text obj, string systemTextName)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "DELETE " + systemTextName + "\t ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, systemTextName + ": ", Color_Category);
            Add_Run_To_Paragraph(paragraph, obj.Text + " (ID: " + obj.ID + ")", Color_Parameter_Name);


            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Delete, "DELETE " + systemTextName, "\n(ID: " + obj.ID + ")");
        }
        #endregion
        #endregion

        #region Experience Table
        public void Log_Exp_Change(Pages.Exp_Table_Value table_Value, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY EXP TABLE\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "LEVEL: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, table_Value.ID + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY EXP TABLE", table_Value.ID, "NEW VALUE:", newValue);


            }

        }
        #endregion

        #region Multisell
        #region Add
        public void Log_Multisell_Add(L2H_Multisell multisell)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "CREATE MULTISELL\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " MULTISELL: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, multisell.ID.ToString(), Color_Category);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "CREATE MULTISELL", multisell.ID.ToString());
        }
        public void Log_Multisell_Clone(L2H_Multisell multisell, L2H_Multisell newMultisell)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "CLONE MULTISELL\t ", Color_Add);
            Add_Run_To_Paragraph(paragraph, "MULTISELL: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, multisell.name_ID + " (ID: " + multisell.ID + ")" + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "NEW MULTISELL: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, newMultisell.name_ID + " (ID: " + newMultisell.ID + ")", Color_Parameter_Name);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "CLONE MULTISELL", multisell.name_ID + "\n(ID: " + multisell.ID + ")", "NEW MULTISELL", newMultisell.name_ID + "\n(ID: " + newMultisell.ID + ")");
        }
        public void Log_Multisell_Sale_Add(L2H_Multisell multisell, L2H_MultisellSale sale)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "ADD MULTISELL SALE\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " MULTISELL: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, multisell.ID.ToString() + "\t", Color_Category);
            Add_Run_To_Paragraph(paragraph, " SALE: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, sale.saleNames, Color_Category);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "ADD SALE", sale.ToString());
        }
        public void Log_Multisell_Sale_Bundle_Add(L2H_Multisell multisell, L2H_MultisellSale sale, L2H_Item item)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "ADD ITEM TO MULTISELL SALE BUNDLE\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " MULTISELL: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, multisell.ID.ToString() + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " SALE BUNDLE: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, sale.saleNames + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " ITEM: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, item.ToString(), Color_Parameter_Name);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "ADD ITEM TO BUNDLE", sale.saleNames);
        }
        public void Log_Multisell_Sale_Bundle_Remove(L2H_Multisell multisell, L2H_MultisellSale sale, L2H_Item item)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "REMOVE ITEM FROM MULTISELL SALE BUNDLE\t ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, " MULTISELL: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, multisell.ID.ToString() + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " SALE BUNDLE: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, sale.saleNames + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " ITEM: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, item.ToString(), Color_Parameter_Name);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Delete, "REMOVE ITEM FROM BUNDLE", sale.saleNames);
        }
        public void Log_Multisell_Cost_Add(L2H_Multisell multisell, L2H_MultisellSale sale, L2H_MultisellCost cost)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "ADD MULTISELL COST\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " MULTISELL: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, multisell.ID.ToString() + "\t", Color_Category);
            Add_Run_To_Paragraph(paragraph, " SALE: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, sale.saleNames + "\t", Color_Category);
            Add_Run_To_Paragraph(paragraph, " COST: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, cost.ToString(), Color_Category);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "ADD COST", cost.ToString());
        }
        public void Log_Multisell_NPC_Add(L2H_Multisell multisell, L2H_NPC npc)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "ADD MULTISELL REQUIRED NPC\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " MULTISELL: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, multisell.ID.ToString() + "\t", Color_Category);
            Add_Run_To_Paragraph(paragraph, " NPC: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, npc.NPC_Name_ID, Color_Category);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "ADD REQ. NPC", npc.NPC_Name_ID);
        }
        #endregion
        #region Modify
        public void Log_Multisell_Change(L2H_Multisell multisell, string property, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY MULTISELL\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "MULTISELL: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, multisell.name_ID + " (" + multisell.ID + ")" + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, property + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY MULTISELL", multisell.ID.ToString(), property, newValue);


            }

        }
        public void Log_Multisell_Sale_Change(L2H_Multisell multisell, L2H_MultisellSale sale, string property, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY MULTISELL\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "MULTISELL: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, multisell.name_ID + " (" + multisell.ID + ")" + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " SALE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, sale.ToString() + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, property + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY SALE", sale.ToString(), property, newValue);


            }
        }
        public void Log_Multisell_Cost_Change(L2H_Multisell multisell, L2H_MultisellSale sale, L2H_MultisellCost cost, string property, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY MULTISELL\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "MULTISELL: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, multisell.name_ID + " (" + multisell.ID + ")" + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " SALE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, sale.ToString() + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " COST: ", Color_Add);
                Add_Run_To_Paragraph(paragraph, cost.ToString() + "\t", Color_Category);
                Add_Run_To_Paragraph(paragraph, " PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, property + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY COST", cost.ToString(), property, newValue);


            }
        }
        #endregion
        #region Delete
        public void Log_Multisell_Delete(L2H_Multisell multisell)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "DELETE MULTISELL\t ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, "MULTISELL: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, multisell.name_ID + " (ID: " + multisell.ID + ")", Color_Parameter_Name);

            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Delete, "DELETE MULTISELL", multisell.name_ID + "\n(ID: " + multisell.ID + ")");
        }
        public void Log_Multisell_Sale_Delete(L2H_Multisell multisell, L2H_MultisellSale sale)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "DELETE SALE\t ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, "MULTISELL: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, multisell.name_ID + " (ID: " + multisell.ID + ")", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " SALE: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, sale.saleNames + "\t", Color_Parameter_Name);

            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Delete, "DELETE SALE", sale.saleNames);
        }
        public void Log_Multisell_Cost_Delete(L2H_Multisell multisell, L2H_MultisellSale sale, L2H_MultisellCost cost)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "DELETE COST\t ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, "MULTISELL: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, multisell.name_ID + " (ID: " + multisell.ID + ")", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " SALE: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, sale.saleNames + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " COST: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, cost.ToString() + "\t", Color_Category);

            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Delete, "DELETE COST", cost.ToString());
        }
        public void Log_Multisell_NPC_Delete(L2H_Multisell multisell, L2H_NPC npc)
        {
            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "REMOVE MULTISELL REQUIRED NPC\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, " MULTISELL: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, multisell.ID.ToString() + "\t", Color_Category);
            Add_Run_To_Paragraph(paragraph, " NPC: ", Color_Add);
            Add_Run_To_Paragraph(paragraph, npc.NPC_Name_ID, Color_Category);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "REMOVE REQ. NPC", npc.NPC_Name_ID);
        }
        #endregion
        #endregion

        #region Classes
        public void Log_Character_Table_Multiple_Line_Multivalue(Base_Parameter_Multiple_Line_Multivalue source, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY CLASS\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "CLASS: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, source.classID + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, source.parameterID + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY CLASS", source.classID, source.parameterID, newValue);


            }


        }
        public void Log_Character_Table_Single_Line_Multivalue(Base_Parameter_Single_Line_Multivalue source, string className, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY CLASS\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "CLASS: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, className + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, source.base_Parameter_ID + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY CLASS", className, source.base_Parameter_ID, newValue);


            }
        }

        public void Log_Character_Create_Item_Remove(string className, string slotName, L2H_Item item)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "REMOVE CHARACTER CREATION EQUIPMENT\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, "CLASS: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, className + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "SLOT: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, slotName + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, " ITEM: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, item.Item_Name + "\t", Color_Parameter_Name);

            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "REMOVE CREATION EQUIPMENT", className, slotName, item.Item_Name);
        }
        public void Log_Character_Create_Item_Change(string className, string slotName, L2H_Item item)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "MODIFY CHARACTER CREATION EQUIPMENT\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, "CLASS: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, className + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "SLOT: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, slotName + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "ITEM: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, item.Item_Name + "\t", Color_Parameter_Name);

            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY CREATION EQUIPMENT", className, slotName, item.Item_Name);
        }
        public void Log_Class_Karma_Constant_Multivalue(string propertyName, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY KARMA CONSTANT\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, propertyName + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY KARMA CONSTANT", propertyName, "", newValue);


            }


        }

        public void Log_Class_Movement_Speed(string className, string propertyName, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY CLASS MOVEMENT SPEED\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "CLASS: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, className + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, "PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, propertyName + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY MOVE SPEED", className, "", newValue);


            }
        }

        public void Log_Class_Base_Single_Value(string className, string propertyName, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {
                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY BASE CLASS PROPERTY\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "CLASS: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, className + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, "PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, propertyName + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY BASE CLASS PROPERTY", className, propertyName, newValue);

            }
        }

        public void Log_Character_Add_Initial_Equipment(Server_InitialEquipment initialEquipment, L2H_Item item)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "ADD INITIAL EQUIPMENT\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, "CLASS: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, initialEquipment.classID + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "ITEM: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, item.Item_Name + "\t", Color_Parameter_Name);


            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "ADD INITIAL EQUIPMENT", initialEquipment.classID, "", item.Item_Name);
        }
        public void Log_Character_Remove_Initial_Equipment(Server_InitialEquipment initialEquipment, L2H_Item item)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "REMOVE INITIAL EQUIPMENT\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, "CLASS: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, initialEquipment.classID + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "ITEM: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, item.Item_Name + "\t", Color_Parameter_Name);


            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "REMOVE INITIAL EQUIPMENT", initialEquipment.classID, "", item.Item_Name);
        }
        public void Log_Character_Modify_Initial_Equipment(Server_InitialEquipment initialEquipment, L2H_Item item, string amount)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "MODIFY INITIAL EQUIPMENT\t ", Color_Modify);
            Add_Run_To_Paragraph(paragraph, "CLASS: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, initialEquipment.classID + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "ITEM: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, item.Item_Name + "\t", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "AMOUNT: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, amount + "\t", Color_Parameter_Name);


            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY INITIAL EQUIPMENT", item.Item_Name, "AMOUNT", amount);
        }

        public void Log_Class_Base_Race_Stats(RaceStats raceStats, string propertyName, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {
                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY BASE CLASS PROPERTY\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "CLASS: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, raceStats.RaceClass + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, "PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, propertyName + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY BASE CLASS PROPERTY", raceStats.RaceClass, propertyName, newValue);

            }
        }
        #endregion

        #region Huntingzone
        public void Log_Huntingzone_Change(L2H_Huntingzone huntingzone, string propertyName, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY HUNTINGZONE\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "HUNTINGZONE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, huntingzone.Name + "(ID: " + huntingzone.ID + ")" + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, propertyName + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY HUNTINGZONE", huntingzone.Name, propertyName, newValue);

            }
        }

        public void Log_Huntingzone_Clone(L2H_Huntingzone huntingzone, L2H_Huntingzone newHuntingzone)
        {

            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "CLONE HUNTINGZONE\t ", Color_Add);
            Add_Run_To_Paragraph(paragraph, "HUNTINGZONE: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, huntingzone.Name + " (ID: " + huntingzone.ID + ")" + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "NEW HUNTINGZONE: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, newHuntingzone.Name + " (ID: " + newHuntingzone.ID + ")", Color_Parameter_Name);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "CLONE HUNTINGZONE", huntingzone.Name + "\n(ID: " + huntingzone.ID + ")", "NEW MULTISELL", newHuntingzone.Name + "\n(ID: " + newHuntingzone.ID + ")");
        }

        public void Log_Huntingzone_Delete(L2H_Huntingzone huntingzone)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "DELETE HUNTINGZONE\t ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, "HUNTINGZONE: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, huntingzone.Name + " (ID: " + huntingzone.ID + ")", Color_Parameter_Name);

            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Delete, "DELETE HUNTINGZONE", huntingzone.Name + "\n(ID: " + huntingzone.ID + ")");
        }
        public void Log_Raid_Clone(L2H_Raid raid, L2H_Raid newRaid)
        {

            var paragraph = new Paragraph();
            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "CLONE RAID\t ", Color_Add);
            Add_Run_To_Paragraph(paragraph, "RAID: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, raid.NPC_Name + " (ID: " + raid.ID + ")" + "\t ", Color_Parameter_Name);
            Add_Run_To_Paragraph(paragraph, "NEW RAID: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, newRaid.NPC_Name + " (ID: " + newRaid.ID + ")", Color_Parameter_Name);



            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Add, "CLONE RAID", raid.NPC_Name + "\n(ID: " + raid.ID + ")", "NEW RAID", newRaid.NPC_Name + "\n(ID: " + newRaid.ID + ")");
        }
        public void Log_Raid_Change(L2H_Raid raid, string propertyName, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY RAID\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "RAID: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, raid.NPC_Name + "(ID: " + raid.ID + ")" + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, propertyName + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY RAID", raid.NPC_Name, propertyName, newValue);

            }
        }
        public void Log_Raid_Delete(L2H_Raid raid)
        {
            var paragraph = new Paragraph();

            Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
            Add_Run_To_Paragraph(paragraph, "DELETE RAID\t ", Color_Remove);
            Add_Run_To_Paragraph(paragraph, "RAID: ", Color_Category);
            Add_Run_To_Paragraph(paragraph, raid.NPC_Name + " (ID: " + raid.ID + ")", Color_Parameter_Name);

            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
            (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Delete, "DELETE RAID", raid.NPC_Name + "\n(ID: " + raid.ID + ")");
        }
        public void Log_Zonename_Change(L2H_Zonename zonename, string propertyName, string oldValue, string newValue)
        {
            if (oldValue != newValue)
            {

                var paragraph = new Paragraph();

                Add_Run_To_Paragraph(paragraph, "[" + DateTime.Now.ToString("MMMM dd HH:mm:ss") + "] ", Color_Category);
                Add_Run_To_Paragraph(paragraph, "MODIFY ZONENAME\t ", Color_Modify);
                Add_Run_To_Paragraph(paragraph, "ZONENAME: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, zonename.Zone_Name + "(ID: " + zonename.ID + ")" + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " PROPERTY: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, propertyName + "\t", Color_Parameter_Name);
                Add_Run_To_Paragraph(paragraph, " OLD VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, oldValue + "\t", Color_Modify);
                Add_Run_To_Paragraph(paragraph, " NEW VALUE: ", Color_Category);
                Add_Run_To_Paragraph(paragraph, newValue + "\t", Color_Modify);


                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateLogAdvanced(paragraph);
                (System.Windows.Application.Current.MainWindow as MainWindow).UpdateMiniLog(LogAction.Modify, "MODIFY ZONENAME", zonename.Zone_Name, propertyName, newValue);

            }
        }

        #endregion
    }

}
