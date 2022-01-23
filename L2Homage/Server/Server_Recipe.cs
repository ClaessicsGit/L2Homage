using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Server_Recipe
    {
        string recipe_begin;
        public string nameID; //[mk_wooden_arrow] recipe Name
        public string id;// 1	Recipe ID
        string level_textStart = "level=";
        public string level;// = 1
        string material_textStart = "material={";
        public string material;// = { {[stem];4};{[iron_ore];2}}	
        string material_textEnd = "}";

        public List<string> materialNames;
        public List<string> materialAmount;

        string catalyst_textStart = "catalyst={";
        public string catalyst;// = { }
        string catalyst_textEnd = "}";

        public List<string> productNames;
        public List<string> productAmount;
        public List<string> productProbability;

        string product_textStart = "product={";
        public string product;// = { {[wooden_arrow];500;100}}	
        string product_textEnd = "}";

        public List<string> npc_fee_Names;
        public List<string> npc_fee_Amount;

        string npc_fee_textStart = "npc_fee={";
        public string npc_fee;// = { {[rp_wooden_arrow];1};{[adena];200}}	
        string npc_fee_textEnd = "}";

        string mp_consume_textStart = "mp_consume=";
        public string mp_consume;// = 30

        string success_rate_textStart = "success_rate=";
        public string success_rate;// = 100

        string item_id_textStart = "item_id=";
        public string item_id;// = 1666

        string iscommonrecipe_textStart = "iscommonrecipe=";
        public string iscommonrecipe;// = 0

        string recipe_end;

        public Server_Recipe(string line)
        {
            materialNames = new List<string>();
            materialAmount = new List<string>();
            productNames = new List<string>();
            productAmount = new List<string>();
            productProbability = new List<string>();
            npc_fee_Names = new List<string>();
            npc_fee_Amount = new List<string>();

            string[] splitLine = line.Split('\t');

            recipe_begin = splitLine[0];
            nameID = splitLine[1];
            nameID = nameID.Replace("[", "");
            nameID = nameID.Replace("]", "");
            id = splitLine[2];
            level = splitLine[3].Replace(level_textStart, "");

            string trimmedMatString = splitLine[4].Replace(material_textStart, "");
            trimmedMatString = trimmedMatString.Replace("{", "");
            trimmedMatString = trimmedMatString.Replace("}", "");
            trimmedMatString = trimmedMatString.Replace("[", "");
            trimmedMatString = trimmedMatString.Replace("]", "");

            string[] splitMatString = trimmedMatString.Split(';');

            int numberOfMatsFound = 0;

            for (int i = 0; i < splitMatString.Length; i = i + 2)
            {
                materialNames.Add(splitMatString[i]);
                materialAmount.Add(splitMatString[i + 1]);
                numberOfMatsFound++;
            }

            for (int i = numberOfMatsFound; i < 11; i++)
            {
                materialNames.Add("");
                materialAmount.Add("");
            }

            catalyst = StripExcessServerText(catalyst_textStart, splitLine[5], catalyst_textEnd);

            string trimmedProductString = splitLine[6].Replace(product_textStart, "");
            trimmedProductString = trimmedProductString.Replace("{", "");
            trimmedProductString = trimmedProductString.Replace("}", "");
            trimmedProductString = trimmedProductString.Replace("[", "");
            trimmedProductString = trimmedProductString.Replace("]", "");

            string[] splitProductString = trimmedProductString.Split(';');

            for (int i = 0; i < splitProductString.Length; i = i + 3)
            {
                productNames.Add(splitProductString[i]);
                bool missingAmount = false;
                if (i + 2 < splitProductString.Length)
                    productProbability.Add(splitProductString[i + 2]);
                else
                {
                    productProbability.Add(splitProductString[i + 1]);
                    missingAmount = true;
                }

                if (missingAmount)
                    productAmount.Add("1");
                else
                    productAmount.Add(splitProductString[i + 1]);

            }


            string trimmedNpcFee = StripExcessServerText(npc_fee_textStart, splitLine[7], npc_fee_textEnd);

            if (!string.IsNullOrEmpty(trimmedNpcFee))
            {
                trimmedNpcFee = trimmedNpcFee.Replace("{", "");
                trimmedNpcFee = trimmedNpcFee.Replace("}", "");
                trimmedNpcFee = trimmedNpcFee.Replace("[", "");
                trimmedNpcFee = trimmedNpcFee.Replace("]", "");

                string[] splitTrimmedNpcFee = trimmedNpcFee.Split(';');

                for (int i = 0; i < splitTrimmedNpcFee.Length; i = i + 2)
                {
                    npc_fee_Names.Add(splitTrimmedNpcFee[i]);
                    npc_fee_Amount.Add(splitTrimmedNpcFee[i + 1]);
                }
            }
            else
            {
                npc_fee = trimmedNpcFee;
            }

            mp_consume = StripExcessServerText(mp_consume_textStart, splitLine[8], "");

            int extraTabsForNoReason = 0;
            if (string.IsNullOrEmpty(splitLine[9]))
                extraTabsForNoReason++;

            success_rate = StripExcessServerText(success_rate_textStart, splitLine[9 + extraTabsForNoReason], "");

            if (string.IsNullOrEmpty(splitLine[10 + extraTabsForNoReason]))
                extraTabsForNoReason++;

            item_id = StripExcessServerText(item_id_textStart, splitLine[10 + extraTabsForNoReason], "");

            if (string.IsNullOrEmpty(splitLine[11 + extraTabsForNoReason]))
                extraTabsForNoReason++;

            iscommonrecipe = StripExcessServerText(iscommonrecipe_textStart, splitLine[11 + extraTabsForNoReason], "");
            recipe_end = splitLine[12 + extraTabsForNoReason];

        }

        public string GetExportString()
        {
            string exportString = "";

            exportString += recipe_begin + '\t' + "[" + nameID + "]" + '\t' + id + '\t' + ConvertToServerText(level_textStart, level, "") + '\t';

            int validMaterialsFound = 0;

            for (int i = 0; i < materialNames.Count; i++)
            {
                if (!string.IsNullOrEmpty(materialNames[i]))
                    validMaterialsFound++;
            }

            string materialSequence = "";

            int validMaterialsProcessed = 0;
            for (int i = 0; i < materialNames.Count; i++)
            {
                if (!string.IsNullOrEmpty(materialNames[i]))
                {
                    materialSequence += "{[" + materialNames[i] + "];" + materialAmount[i] + "}";
                    validMaterialsProcessed++;

                    if (validMaterialsProcessed != validMaterialsFound)
                        materialSequence += ";";

                }
            }

            string materialString = ConvertToServerText(material_textStart, materialSequence, material_textEnd);

            exportString += materialString + "\t" + ConvertToServerText(catalyst_textStart, catalyst, catalyst_textEnd) + "\t";

            string productSequence = "";

            for (int i = 0; i < productNames.Count; i++)
            {
                if (!string.IsNullOrEmpty(productNames[i]))
                {
                    productSequence += "{[" + productNames[i] + "];" + productAmount[i];
                    if (productProbability.Count > 0)
                        productSequence += ";" + productProbability[i];
                    productSequence += "}";
                    if (productNames.Count == 2 && i == 0)
                        productSequence += ";";
                }
            }

            string productsString = ConvertToServerText(product_textStart, productSequence, product_textEnd);

            exportString += productsString + '\t';

            string npc_fee_sequence = "";

            for (int i = 0; i < npc_fee_Names.Count; i++)
            {
                if (!string.IsNullOrEmpty(npc_fee_Names[i]))
                {
                    npc_fee_sequence += "{[" + npc_fee_Names[i] + "];" + npc_fee_Amount[i] + "}";
                    if (npc_fee_Names.Count - 1 != i)
                        npc_fee_sequence += ";";
                }
            }

            exportString += ConvertToServerText(npc_fee_textStart, npc_fee_sequence, npc_fee_textEnd) + '\t';


            exportString +=
                ConvertToServerText(mp_consume_textStart, mp_consume, "") + '\t' +
                ConvertToServerText(success_rate_textStart, success_rate, "") + '\t' +
                ConvertToServerText(item_id_textStart, item_id, "") + '\t' +
                ConvertToServerText(iscommonrecipe_textStart, iscommonrecipe, "") + '\t' +
                recipe_end;

            return exportString;


        }

        private string StripExcessServerText(string startText, string variable, string endText)
        {

            string returnString = variable;
            returnString = returnString.Replace(startText, "");
            if (endText.Length > 0)
                returnString = returnString.Replace(endText, "");

            return returnString;
        }

        private string ConvertToServerText(string startText, string variable, string endText)
        {
            return startText + variable + endText;
        }
    }
}
