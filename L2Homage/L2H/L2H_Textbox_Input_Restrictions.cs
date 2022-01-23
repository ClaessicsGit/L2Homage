using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace L2Homage
{
    public static class L2H_Textbox_Input_Restrictions
    {
        /// <summary>
        /// Spaces allowed by default. Space does not trigger previewimputtext
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool Is_Valid_Integer(string text)
        {
            Regex _regex = new Regex("^[0-9\\-]");
            return !_regex.IsMatch(text);
        }

        public static bool Is_Valid_Float(string text)
        {
            Regex _regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            return !_regex.IsMatch(text);
        }

        public static void Check_If_Dot_Exists_In_Float_TextBox(object sender, KeyEventArgs e)
        {
            var vm = sender as TextBox;
            if (vm.Tag == null)
            {
                return;
            }

            string tag = vm.Tag.ToString();

            if (tag == ("float"))
            {
                if (vm.Text.Contains("."))
                    if (e.Key == Key.OemPeriod || e.Key == Key.Decimal)
                    {
                        e.Handled = true;
                    }
            }
        }

        /// <summary>
        /// Fuinctional, allows white space by default
        /// Only lower case letters, numbers and underscores
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool Is_Valid_Name_ID(string text)
        {
            Regex _regex = new Regex(@"^[a-z0-9_\\-]+");
            return !_regex.IsMatch(text);
        }
        public static bool Is_Valid_Lower_Case_And_Symbols(string text)
        {
            Regex _regex = new Regex(@"^[a-z0-9_\-\\{\\}\;\[\]]+");
            return !_regex.IsMatch(text);
        }


        /// <summary>
        /// Functional, allows white space by default
        /// Any letters, numbers, apostrophes, asterixes, dashes and plusses
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool Is_Valid_Ingame_Name(string text)
        {
            Regex _regex = new Regex("^[a-zA-Z0-9'*\\-\\+]*$");
            return !_regex.IsMatch(text);
        }
        public static bool Is_Valid_Clientfile_Entry(string text)
        {
            Regex _regex = new Regex("^[a-zA-Z0-9'_*\\-\\+\\.]*$");
            return !_regex.IsMatch(text);
        }
    }

}
