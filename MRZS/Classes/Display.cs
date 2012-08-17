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
    public class Display
    {
        public string menuElement { get; set; }
        //public int? CurrentParentID{get;set;}
        //public int selectedOnDisplayIndex{get;set;}
        public int id { get; set; }    
        public Display() { }
        public Display(string menuElem,int _id)
        {
            menuElement = menuElem;
            id = _id;
        }
    }
}
