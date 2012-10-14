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
    internal class NumValueChanging
    {
        List<int> numIndexesList=new List<int>(0);
        int currInputPosition=-1;
        string TempNumValue = null;
        
        //return 0002.0000 in string from "Уставка МТЗ1\r\n0002.0000 A"
        private string getNumIndexes(string StringForParse)
        {                                                
            return Regex.Match(StringForParse, @"\d\d\d\d\.\d\d\d\d").Value;            
        }
        internal void leftButtonClicked(TextBox t)
        {
            //working with selection by indexes
            int index = numIndexesList.IndexOf(currInputPosition);
            if ((index - 1) >= 0)
            {
                index--;
                currInputPosition = numIndexesList[index];
            }
            selectOneNumInTextBox(t,currInputPosition);
        }
        internal void enteredNumeric(TextBox t,int num)
        {
            string temp = t.Text;
            temp=temp.Remove(currInputPosition, 1);
            t.Text = temp.Insert(currInputPosition, num.ToString());            
            selectOneNumInTextBox(t,currInputPosition);
        }
        internal void rightButtonclicked(TextBox t)
        {
            int index = numIndexesList.IndexOf(currInputPosition);
            if ((index + 1) <= (numIndexesList.Count - 1))
            {
                index++;
                currInputPosition = numIndexesList[index];
            }
            selectOneNumInTextBox(t,currInputPosition);
        }
        internal void  parseNumeric(TextBox t,string TextWithNumerics)
        {            
            //list indexes of nums in TextBox
            numIndexesList = getIndexes(TextWithNumerics);
            currInputPosition = numIndexesList[0];
            selectOneNumInTextBox(t,currInputPosition);
        }
        //select one number in textbox for inputing process
        private void selectOneNumInTextBox(TextBox t,int SelectedPosition)
        {            
            t.Focus();
            t.SelectionStart = SelectedPosition;
            t.SelectionLength = 1;
        }

        //return indexes of 0002.0000 in string
        private List<int> getIndexes(string AllStr)
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

        internal void setValue(string SecondShowedMenuLine)
        {
           TempNumValue=Inputing.getNumsFromStr(SecondShowedMenuLine);
        }
        internal string getValue() { return TempNumValue; }
    }
}
