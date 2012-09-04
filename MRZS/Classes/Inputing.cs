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
        //get first index of 0002.0000 in string
        public static string getNumIndexes(string StringForParse)
        {                                                
            return Regex.Match(StringForParse, @"\d\d\d\d\.\d\d\d\d").Value;            
        }
        //return indexes of 0002.0000 in string
        public static List<int> getIndexes(string AllStr)
        {
            string NumStr = getNumIndexes(AllStr);
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
    }
}
