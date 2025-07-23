using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Dossier_Entreprise
{
    class Function
    {
        //Function.emtyFields(RIGHTTFORM,new String[] { })
        //Function.emtyFields(RIGHTTFORM)
        public static String emtyFields(Panel container, String[] exception=null)
        {
            String message = "";
            foreach (object child in container.Children)
            {
                String childname = "";
                String childContent = "";
                if (child is FrameworkElement && child is TextBox)
                {
                    childname = (child as FrameworkElement).Name;
                    childContent = (child as TextBox).Text;
                    if (!(exception != null && exception.Contains(childname)) && childContent == "")
                        message += "- " + childname + " vide \n";
                }
                else if (child is FrameworkElement && child is Panel)
                    message = emtyFields((Panel)child);

            }
            return message;
        }

        /*****************/
        public static void CleanField(Panel container)
        {
            foreach (object child in container.Children)
                if (child is FrameworkElement && child is TextBox)
                    (child as TextBox).Text = "";
        }


        /*****************/
        public static bool IsEmpty(Panel container)
        {
            foreach (object child in container.Children)
                if (child is FrameworkElement && child is TextBox && ((TextBox)child).Text == "")
                    return true;

            return false;
        }

        public static bool isLong(String number)
        {
            long i;

            return long.TryParse(number, out i);
        }
        public static bool isInt(String number)
        {
            int i;

            return Int32.TryParse(number, out i);
        }
        public static bool isDouble(String number)
        {
            Double i;

            return Double.TryParse(number, out i);
        }
        public static bool isNumber(String number)
        {
            double i;

            return double.TryParse(number, out i);
        }

        public static bool isDate(int year, int month, int day)
        {
            try
            {
                new DateTime(year, month, day);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool isDateTime(String date)
        {
            DateTime dt;
            return DateTime.TryParse(date, out dt);

        }
        public static bool isAlphaNumeric(String str)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");

            if (r.IsMatch(str))
                return true;

            return false;
        }
        public static bool isAlpha(String str)
        {
            Regex r = new Regex("^[a-zA-Z]*$");
            if (r.IsMatch(str))
                return true;

            return false;
        }
        public static long? ConvertInt64(string val)
        {
            if (!isLong(val)) return null;

            return long.Parse(val);
        }

        public static long? ConvertInt(string val)
        {
            if (!isInt(val)) return null;

            return Int32.Parse(val);
        }
        public static string ConvertString(object val) // database to class
        {
            return (val == null) ? null : val.ToString();
        }
        public static string ConvertString(string val) // form to database
        {
            return (val == "") ? null : val;
        }
        public static Double? ConvertDouble(string val)
        {
            if (!isDouble(val)) return null;

            return Double.Parse(val);
        }
        public static DateTime? ConvertDateTime(string val)
        {
            if (!isDateTime(val)) return null;
            return DateTime.Parse(val);
        }


        /*****************/
        //public static void fillStructure(ComboBox list, IList<Structure> structures, Int64 selectedIndex=-1)
        //{
        //    foreach (var element in structures)
        //    {
        //        ComboBoxItem item = new ComboBoxItem() { Content = element.libelle, Tag = element.id };
        //        list.Items.Add(item);
        //        if (element.id == selectedIndex) list.SelectedItem = item;
        //    }
        //}

        //public static void fill_Natures_Traveaux(ComboBox list, IList<Nature_Traveaux> natures_traveaux, Int64 selectedIndex=-1)
        //{
        //    foreach (var element in natures_traveaux)
        //    {
        //        ComboBoxItem item = new ComboBoxItem() { Content = element.libelle, Tag = element.id };
        //        list.Items.Add(item);
        //        if (element.id == selectedIndex) list.SelectedItem = item;
        //    }
        //}

        //public static void fill_Position(ComboBox list, IList<Position> position, Int64 selectedIndex = -1)
        //{
        //    foreach (var element in position)
        //    {
        //        ComboBoxItem item = new ComboBoxItem() { Content = element.libelle, Tag = element.id };
        //        list.Items.Add(item);
        //        if (element.id == selectedIndex) list.SelectedItem = item;
        //    }
        //}
    }
}

