using System;
using System.Net;
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
     static class EmulatorDisplayController
    {
         //open methods
        public static void setCursorInNextLine(string displayText,int displaySelectedIndex) 
        { 
            
        }
         //private methods
        public static int IndexOfnextFirstSymbolFinding(string displayText, int displaySelectedIndex)
        {
            int index= displayText.IndexOf("\r\n", displaySelectedIndex);
            if (index != -1) return (index + 2);
            else return index;
        }
        public static int getPreviousIndexOfStartLineDisplay(string displayText, int displaySelectedIndex)
        {
            int temp = displaySelectedIndex;
            while(temp!=0 && displayText[temp]!='\r')
            {
                temp-=1;
            }
            if ((temp - 1) >= 0) return (temp - 1);
            else return -1;
        }
    }
}
