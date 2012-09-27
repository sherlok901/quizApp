using System;
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
        List<Menu> children=new List<Menu>();
        public List<Menu> Children
        {
            get { return children; }
        }
        string name;
        public string Name
        {
            get { return name; }
        }
        string value;
        public string Value
        {
            get { return value; }
        }
        internal void setChildren(List<mrzs05mMenu> MrzsTables)
        {
            foreach (mrzs05mMenu table in MrzsTables)
            {
                Menu m = new Menu();
                m.name = table.menuElement;
                children.Add(m);
            }
        }
    }
}
