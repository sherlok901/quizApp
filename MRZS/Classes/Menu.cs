﻿using System;
using System.Collections.Generic;
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

namespace MRZS.Classes
{
    public class Menu
    {        
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public bool HasChildren = true;
        List<Menu> children=new List<Menu>();
        public List<Menu> Children
        {
            get { return children; }
        }


        public string Name
        {
            get;
            set;
        }
        public string Value
        {
            get;
            set;
        }
        public string Unit { get; set; }
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public double Value2 { get; set; }
    }
}
