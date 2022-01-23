using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class Client_L2ini
    {
        string[] totalLines;
        int serverAddressLineIndex = 0;

        public Client_L2ini(string[] allLines)
        {
            totalLines = allLines;

            for (int i = 0; i < totalLines.Length; i++)
            {
                if (totalLines[i].Contains("ServerAddr="))
                    serverAddressLineIndex = i;
            }
        }

        public string[] GetExportStrings(string serverIP)
        {
            totalLines[serverAddressLineIndex] = "ServerAddr=" + serverIP;

            return totalLines;
        }
    }
}
