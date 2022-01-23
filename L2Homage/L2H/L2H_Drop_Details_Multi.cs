using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace L2Homage
{
    public class L2H_Drop_Details_Multi
    {
        public L2H_Droplist L2H_Droplist { get; set; }
        public List<L2H_Drop_Details_Single> drops_details { get; set; }
        public List<Server_Droplist> separateDroplists { get; set; }

        public string ID { get; set; }
        public string Chance { get; set; }

        public override string ToString()
        {
            return ID;
        }
    }
}