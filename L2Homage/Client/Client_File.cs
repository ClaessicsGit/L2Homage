using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_File
    {
        public string GetArrayString(string[] stringArray, string identifier = "")
        {
            string returnString = "";

            if (stringArray.Length == 1)
            {
                returnString = stringArray[0];
            }
            else
            {
                for (int i = 0; i < stringArray.Length; i++)
                {
                    if (i > 0)
                        returnString = returnString + '\t' + stringArray[i];
                    else
                    {
                        returnString = stringArray[0];
                    }
                }
            }

            return returnString;
        }

    }
}
