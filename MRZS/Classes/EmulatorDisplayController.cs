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
     public static class EmulatorDisplayController
    {
         //open methods
        public static void setCursorInNextLine(string displayText,int displaySelectedIndex) 
        { 
            
        }
         //private methods
        public static int IndexOfnextFirstSymbolFinding(string displayText, int displaySelectedIndex)
        {
            int index= displayText.IndexOf("\r\n", displaySelectedIndex);
            if (index == -1) return displaySelectedIndex;
            else return index+2;
        }
        public static int getPreviousIndexOfStartLineDisplay(string displayText, int displaySelectedIndex)
        {
            int temp = displaySelectedIndex;
            int flag = 0;
            while(temp!=0)
            {
                if (displayText[temp] == '\n') flag++;
                if (flag == 2)
                {
                    temp++;
                    break;
                }
                temp-=1;
            }
            return temp;
        }
    }
}
