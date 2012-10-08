using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Linq;
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
        
        private List<Menu> CurrentMenuList;
        Menu ShowedFirst;
        Menu ShowedSecond;        
        int CurrentMenuWithCursorIndex = -1;

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
        
        //===== Methods ======                

        //show or move to next line with cursor
        public void moveToNextLine()
        {
            //for two Menu classes
            if (ShowedFirst.HasChildren)
            {
                //cursor in first line
                if (FirstMenuStr.IndexOf(">") != -1)
                {
                    FirstMenuStr = FirstMenuStr.Remove(FirstMenuStr.IndexOf(">"), 1);
                    SecondMenuStr = SecondMenuStr.Insert(0, ">");                                        
                }
                else if (SecondMenuStr.IndexOf(">") != -1)
                {
                    if (getNextMenuClass(ShowedSecond) != null)
                    {                        
                        ShowedFirst = ShowedSecond;
                        ShowedSecond = getNextMenuClass(ShowedSecond);
                        FirstMenuStr = ShowedFirst.FirstLine;
                        SecondMenuStr = ShowedSecond.FirstLine.Insert(0,">");
                    }
                }
            }
            //for one Menu class
            else
            {
                if (getNextMenuClass(ShowedFirst) != null)
                {
                    ShowedFirst = getNextMenuClass(ShowedFirst);
                    ShowedSecond = null;
                    FirstMenuStr = ShowedFirst.FirstLine;
                    SecondMenuStr = ShowedFirst.SecondLine;
                }
            } 
        }
        public void moveToPreviousLine()
        {
            //for two Menu classes
            if (ShowedFirst.HasChildren)
            {    
                //cursor in first line
                if (FirstMenuStr.IndexOf(">") != -1)
                {
                    if(getPreviousMenuClass(ShowedFirst)!=null)
                    {
                        ShowedSecond=ShowedFirst;
                        ShowedFirst=getPreviousMenuClass(ShowedFirst);
                        FirstMenuStr = ShowedFirst.FirstLine.Insert(0, ">");
                        SecondMenuStr = ShowedSecond.FirstLine;
                    }                   
                }
                else if (SecondMenuStr.IndexOf(">") != -1)
                {
                    SecondMenuStr = SecondMenuStr.Remove(secondMenuStr.IndexOf(">"), 1);
                    FirstMenuStr = FirstMenuStr.Insert(0, ">");
                }
            }
            //for one Menu class
            else
            {
                if (getPreviousMenuClass(ShowedFirst) != null)
                {                    
                    ShowedFirst = getPreviousMenuClass(ShowedFirst);
                    ShowedSecond = null;
                    FirstMenuStr = ShowedFirst.FirstLine;
                    SecondMenuStr = ShowedFirst.SecondLine;
                }
            }                        
        }
        //get next Menu class for displaying
        Menu getNextMenuClass(Menu CurrentMenu)
        {
            Menu m = CurrentMenuList.Where(n => n.ID == CurrentMenu.ID).Single();
            int index = CurrentMenuList.IndexOf(m);
            if (index > -1 && 
                ((index + 1) <= (CurrentMenuList.Count - 1))) return CurrentMenuList[(index + 1)];
            else return null;
        }
        Menu getPreviousMenuClass(Menu CurrentMenu)
        {
            Menu m = CurrentMenuList.Where(n => n.ID == CurrentMenu.ID).Single();
            int index = CurrentMenuList.IndexOf(m);
            if (index > 0) return CurrentMenuList[(index - 1)];
            else return null;
        }
        void setDisplayingText(Menu one, Menu two)
        {
            
            FirstMenuStr = one.Name;
            SecondMenuStr = two.Name;
        }
           
        //=== PUBLIC METHODS ===
        public void showMenu(List<Menu> list)
        {
            //for displaying two lines of text
            if (list.Count > 1) ShowedLogic(list[0], list[1], true);
            //for one text line
            else ShowedLogic(list[0], null,true); 
            CurrentMenuList = list;            
        }
        //show previous parent Menu class what was choosed by user
        internal void showMenu(List<Menu> list, Menu ParentChoosedMenu)
        {
            Menu MenuCurrent = list.Where(n => n.ID == (ParentChoosedMenu.ID)).Single();                        
            int index=list.IndexOf(MenuCurrent);
            Menu next = null;
            //if it is last Menu in list
            if (index == (list.Count - 1))
            {
                next = list[(index - 1)];
                ShowedLogic(ParentChoosedMenu, next,false);
            }
            else if ((index > -1) && ((index + 1) < list.Count))
            {
                next = list[(index + 1)];
                ShowedLogic(ParentChoosedMenu, next,true);
            }
            
            CurrentMenuList = list;
        }
        internal DisplayViewModel getThisInstance()
        {
            return this;
        }

        //get choosed Menu class by user clicking on Enter button
        public Menu getChoosedMenuClass()
        {
            if (ShowedFirst == null) return null;

            //for two Menu classes
            if (ShowedFirst.HasChildren)
            {                
                //for two Menu classes
                if (FirstMenuStr.IndexOf(">") != -1) 
                {
                    if (FirstMenuStr.IndexOf(ShowedFirst.FirstLine) != -1) return ShowedFirst;
                    else if (FirstMenuStr.IndexOf(ShowedSecond.FirstLine) != -1) return ShowedSecond;
                    else return null;
                }
                else if (SecondMenuStr.IndexOf(">") != -1)
                {
                    if (SecondMenuStr.IndexOf(ShowedFirst.FirstLine) != -1) return ShowedFirst;
                    else if (SecondMenuStr.IndexOf(ShowedSecond.FirstLine) != -1) return ShowedSecond;
                    else return null;
                }
                else return null;
            }
            //for one Menu classes
            else return ShowedFirst;            
        }

        //logic for displaying one Menu or two Menu classes
        void ShowedLogic(Menu one,Menu two,bool IsCursorInFirst)
        {
            if (one != null && one.HasChildren && two!=null)
            {
                if (IsCursorInFirst)
                {
                    FirstMenuStr = one.FirstLine.Insert(0, ">");
                    SecondMenuStr = two.FirstLine;
                }
                else
                {
                    FirstMenuStr = two.FirstLine;
                    SecondMenuStr = one.FirstLine.Insert(0, ">");                    
                }
                ShowedFirst = one;
                ShowedSecond = two;
            }            
            else if (one != null && (!one.HasChildren))
            {
                FirstMenuStr = one.FirstLine;
                SecondMenuStr = one.SecondLine;
                ShowedFirst = one;
                ShowedSecond = null;
            }
        }

        //show text
        internal void showText(string FirstLine,string SecondLine)
        {
            //ShowedFirst = null;
            //ShowedSecond = null;
            FirstMenuStr = FirstLine;
            SecondMenuStr = SecondLine;
        }
        
    }
}
