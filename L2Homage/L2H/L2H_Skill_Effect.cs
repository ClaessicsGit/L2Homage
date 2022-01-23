using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace L2Homage
{
    public enum L2H_Skill_Effect_Type { Effect, Start_Effect, End_Effect, Self_Effect, Tick_Effect, PVP_Effect, PVE_Effect }

    /// <summary>
    /// Contains all skill effects caused by parent skill
    /// </summary>
    public class L2H_Skill_Effects
    {
        public L2H_Skill_Effect_Type type;
        public L2H_Skill parent { get; set; }
        public List<L2H_Skill_Effect> skill_Effects { get; set; }

        public L2H_Skill_Effects(L2H_Skill parentSkill, string effectString, L2H_Skill_Effect_Type type)
        {
            this.type = type;
            skill_Effects = new List<L2H_Skill_Effect>();
            parent = parentSkill;
            FilterEffectString(effectString);
        }

        void FilterEffectString(string effectString)
        {
            //First check if multiple effects are present
            //Each effect is separated by };{
            string[] effectsArray;

            if (effectString.Contains("};{"))
            {
                string[] stringSeparators = new string[] { "};{" };
                effectsArray = effectString.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                effectsArray = new string[1];
                effectsArray[0] = effectString;
            }

            for (int i = 0; i < effectsArray.Length; i++)
            {
                skill_Effects.Add(new L2H_Skill_Effect(effectsArray[i], type));
            }
        }


        public void SaveSkillEffects()
        {
            string fullEffectString = "{";

            for (int i = 0; i < skill_Effects.Count; i++)
            {
                string partEffectString = "{";

                partEffectString += skill_Effects[i].skillEffectID;

                for (int j = 0; j < skill_Effects[i].values.Count; j++)
                {
                    partEffectString += ";" + skill_Effects[i].values[j].Value;
                }

                partEffectString += "}";

                fullEffectString += partEffectString;
            }

            fullEffectString += "}";

            switch (type)
            {
                case L2H_Skill_Effect_Type.Start_Effect:
                    parent.Skill_Start_Effect = fullEffectString;
                    break;
                case L2H_Skill_Effect_Type.Effect:
                    parent.Skill_Effect = fullEffectString;
                    break;
                case L2H_Skill_Effect_Type.Self_Effect:
                    parent.Skill_Self_Effect = fullEffectString;
                    break;
                case L2H_Skill_Effect_Type.Tick_Effect:
                    parent.Skill_Tick_Effect = fullEffectString;
                    break;
                case L2H_Skill_Effect_Type.End_Effect:
                    parent.Skill_End_Effect = fullEffectString;
                    break;
                case L2H_Skill_Effect_Type.PVP_Effect:
                    parent.Skill_PVP_Effect = fullEffectString;
                    break;
                case L2H_Skill_Effect_Type.PVE_Effect:
                    parent.Skill_PVE_Effect = fullEffectString;
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Contains single skill effect and values
    /// </summary>
    public class L2H_Skill_Effect
    {
        public L2H_Skill_Effect_Type type;
        public string skillEffectID { get; set; }
        public List<L2H_Skill_Effect_Value> values { get; set; }
        public bool isInteger { get; set; }

        public L2H_Skill_Effect(string effectString, L2H_Skill_Effect_Type type)
        {
            this.type = type;
            string filteredString = effectString.Replace("{{", "");
            filteredString = filteredString.Replace("}}", "");

            string[] splitString = filteredString.Split(';');

            values = new List<L2H_Skill_Effect_Value>();

            if (splitString.Length > 0)
            {
                skillEffectID = splitString[0];

                for (int i = 1; i < splitString.Length; i++)
                {
                    values.Add(new L2H_Skill_Effect_Value(this, splitString[i]));
                }
            }

        }

    }

    public class L2H_Skill_Effect_Value : INotifyPropertyChanged
    {
        public L2H_Skill_Effect parent { get; set; }
        string value;
        public string Value 
        { 
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                OnPropertyChanged();
            }
        }

        public L2H_Skill_Effect_Value(L2H_Skill_Effect parent, string value)
        {
            this.parent = parent;
            this.value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}


