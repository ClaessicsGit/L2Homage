using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public class L2H_Character_Class
    {
        public string classID { get; set; }
        public bool IsSelected { get; set; }
        public bool IsSelectedTemp { get; set; }
        public L2H_Character_Class Instance { get { return this; } }

        List<L2H_Skill_Acquire> l2H_Skill_Acquires;
        public List<L2H_Skill_Acquire> L2H_Skill_Acquires
        {
            get
            {
                if (l2H_Skill_Acquires == null)
                    l2H_Skill_Acquires = new List<L2H_Skill_Acquire>();

                return l2H_Skill_Acquires;
            }
            set
            {
                l2H_Skill_Acquires = value;
            }
        }
        public override string ToString()
        {
            return classID;
        }

        public System.Windows.Media.Imaging.BitmapImage Get_Class_Image
        {
            get
            {
                if (classID != null)
                    return L2H_Parser.GetClassImage(classID);
                else
                    return new System.Windows.Media.Imaging.BitmapImage(new Uri("/Images/ImageNotFound.png", UriKind.Relative));
            }
        }
    }
}
