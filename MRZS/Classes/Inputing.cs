using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MRZS.Classes
{
    static public class Inputing
    {
        public static int CurrentNumPositionInputing = -1;
        //return 0002.0000 in string from "Уставка МТЗ1\r\n0002.0000 A"
        public static string getNumsFromStr(string StringForParse)
        {
            if (StringForParse == null || StringForParse == String.Empty) return null;
            else return Regex.Match(StringForParse, @"\d\d\d\d\.\d\d\d\d").Value;            
        }
        //return indexes of 0002.0000 in string
        public static List<int> getIndexes(string AllStr)
        {
            string NumStr = getNumsFromStr(AllStr);
            int StartIndex = AllStr.IndexOf(NumStr);

            List<int> IndexesList = new List<int>(0);
            for (int i = 0; i < NumStr.Length; i++)
            {
                char a = NumStr[i];
                if (Char.IsDigit(NumStr[i]))
                {
                    char b = AllStr[StartIndex];
                    char c = NumStr[i];
                    if (AllStr[StartIndex] == NumStr[i]) IndexesList.Add(StartIndex);                    
                }
                StartIndex++;
            }
            return IndexesList;
        }

        //check String to numeric 0000.0000
        internal static bool isNumericValue(string value)
        {
            bool IsDigits = true;
            string OnlyNumericStr = getNumsFromStr(value);
            if (OnlyNumericStr == null || OnlyNumericStr == String.Empty) return false;

            foreach (char a in OnlyNumericStr)
            {
                if (Char.IsDigit(a) || a == '.') continue;
                else IsDigits = false;
            }
            return IsDigits;
        }
    }
}
