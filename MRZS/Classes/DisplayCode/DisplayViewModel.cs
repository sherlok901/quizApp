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

        private ObservableCollection<mrzs05mMenu> currentMenuMembers;
        private mrzs05mMenu child;
        private IEnumerable<mrzs05mMenu> mrzs05Entity;
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
        public bool IsCursorInFirstStr 
        { 
            get; 
            set; 
        }
        public bool IsCursorInSecondStr { get; set; }
        //data load vars
        LoadData ld = new LoadData();

        public DisplayViewModel()
        {
        }
        
        public void getNext()
        {
        }
        public void getPrevious()
        {
        }
        public void getNextTwo()
        {
        }
        internal void setMrzsTables(IEnumerable<mrzs05mMenu>list)
        {
            mrzs05Entity = list;
        }
        public DisplayViewModel showMenu(Menu m)
        {
            FirstMenuStr = m.Children[0].Name;
            SecondMenuStr = m.Children[1].Name;
            return this;
        }
    }
}
