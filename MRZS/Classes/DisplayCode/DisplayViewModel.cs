using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MRZS.Views.Emulator;
using MRZS.Web.Models;


namespace MRZS.Classes.DisplayCode
{
    public class DisplayViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //check event and in not null notify about it
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) PropertyChanged(this, e);
        }
        
        
        private List<Menu> MenuList;
        private Menu ShowedOne;
        private Menu ShowedTwo;
        Menu CurrentMenu;

        private string firstMenuStr;
        public string FirstMenuStr 
        { 
            get{return firstMenuStr;}
            set
            {
                firstMenuStr=value;
                OnPropertyChanged(new PropertyChangedEventArgs("FirstMenuStr"));
            }
        }
        private string secondMenuStr;
        public string SecondMenuStr 
        {
            get { return secondMenuStr; }
            set
            {
                secondMenuStr = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SecondMenuStr"));
            }
        }
        public bool IsCursorEnabled { get; set; }
        private bool isCursorInFirstStr;
        public bool IsCursorInFirstStr 
        {
            get { return isCursorInFirstStr; }
            set
            {
                isCursorInFirstStr = value;
                if (isCursorInFirstStr)
                {
                    IsCursorInSecondStr = false;
                    //adding cursor
                    int CursorIndex = FirstMenuStr.IndexOf(">");
                    if (CursorIndex == -1)
                    {
                        FirstMenuStr = FirstMenuStr.Insert(0, ">");
                    }
                }
            }
        }
        private bool isCursorInSecondStr;
        public bool IsCursorInSecondStr 
        {
            get { return isCursorInSecondStr; }
            set
            {
                isCursorInSecondStr=value;
                if (IsCursorInSecondStr) IsCursorInFirstStr = false;
            }
        }
        //data load vars
        
        //===== Methods ======                

        //show or move to next line with cursor
        public void moveToNextLine()
        {
            if (IsCursorInSecondStr)
            {
                //show next menuline
                if (getNextMenuClass(ShowedTwo) != null)
                {
                    ShowedOne = ShowedTwo;
                    ShowedTwo = getNextMenuClass(ShowedOne);
                    setDisplayingText(ShowedOne, ShowedTwo);
                    SecondMenuStr = SecondMenuStr.Insert(0, ">");
                }                
            }
            else
            {
                //move cursor to second menuline
                FirstMenuStr= FirstMenuStr.Remove(FirstMenuStr.IndexOf(">"), 1);
                SecondMenuStr = SecondMenuStr.Insert(0, ">");                
                IsCursorInSecondStr = true;
            }
        }
        public void moveToPreviousLine()
        {
            //cursor in second line
            if (IsCursorInSecondStr)
            {
                SecondMenuStr=SecondMenuStr.Remove(secondMenuStr.IndexOf(">"),1);
                FirstMenuStr = FirstMenuStr.Insert(0, ">");               
                IsCursorInFirstStr=true;
            }
            else
            {
                if (getPreviousMenuClass(ShowedOne) != null)
                {
                    ShowedTwo = ShowedOne;
                    ShowedOne = getPreviousMenuClass(ShowedTwo);
                    setDisplayingText(ShowedOne, ShowedTwo);
                    IsCursorInFirstStr = true;                    
                }
            }
        }
        //get next Menu class for displaying
        Menu getNextMenuClass(Menu CurrentMenu)
        {
            int index = MenuList.IndexOf(CurrentMenu);
            if (index > -1 && 
                ((index + 1) <= (MenuList.Count - 1))) return MenuList[(index + 1)];
            else return null;
        }
        Menu getPreviousMenuClass(Menu CurrentMenu)
        {
            int index = MenuList.IndexOf(CurrentMenu);
            if (index > 0) return MenuList[(index - 1)];
            else return null;
        }
        void setDisplayingText(Menu one, Menu two)
        {
            FirstMenuStr = one.Name;
            SecondMenuStr = two.Name;
        }
                
        public void showMenu(List<Menu> list)
        {
            ShowedOne = list[0];
            ShowedTwo = list[1];
            setDisplayingText(ShowedOne, ShowedTwo);
            IsCursorInFirstStr = true;
            MenuList = list;            
        }
        internal DisplayViewModel getThisInstance()
        {
            return this;
        }

        //get choosed Menu class by user clicking on Enter button
        public Menu getChoosedMenuClass()
        {
            if (IsCursorInFirstStr) return ShowedOne;
            else if (IsCursorInSecondStr) return ShowedTwo;
            else return null;
        }
    }
}
