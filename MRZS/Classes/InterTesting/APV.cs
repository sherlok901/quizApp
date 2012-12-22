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
using System.Collections.Generic;
using System.Linq;
using MRZS.Web.Models;

namespace MRZS.Classes.InterTesting
{
    internal class APV
    {
        //включено ли апв вообще
        internal bool IsApvTurnOn()
        {
            if (getApvMenuElement("АПВ\\{value}").BooleanVal3.boolVal.Contains("ВКЛ")) return true;
            else return false;
        }
        //выдержка 1 цикл апв
        internal double getTimer1CycleApv()
        {
            return getMenuElementValue("1 Цикл АПВ\\{value}");
        }

        double getMenuElementValue(string name)
        {
            mrzs05mMenu t = getMenuElement(name);            
            string val = t.value;
            return Double.Parse(replaceDotOnComma(val));
        }

        mrzs05mMenu getMenuElement(string name)
        {
            return LoadData.MrzsTable.Where(n => n.menuElement != null).Where(n => n.menuElement.StartsWith(name)).Single();
        }
        
        //заменить в строке точку на комму
        string replaceDotOnComma(string InStr)
        {
            return InStr.Replace('.', ',');
        }

        //
        mrzs05mMenu getApvMenuElement(string name)
        {
            return LoadData.MrzsTable.Where(n => n.menuElement != null).Where(n => n.menuElement.StartsWith(name)).Where(n => n.BooleanVal3ID != null).Single();                        
        }
    }
}
