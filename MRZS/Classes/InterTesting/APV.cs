using System;
using System.Collections.Generic;
using System.Linq;
using MRZS.Web.Models;

namespace MRZS.Classes.InterTesting
{
    internal class APV
    {
        //добавлено ли апв к меню
        internal bool IsApvAddedToMenu()
        {
            return LoadData.MrzsTable.Where(n => n.menuElement != null && n.menuElement.StartsWith("АПВ\\{value}") && n.BooleanVal2ID != null).Single().BooleanVal2.val.StartsWith("ЕСТЬ");
            //mrzs05mMenu m= LoadData.MrzsTable.Where(n => n.id == 411).Single(); 
                //Where(n=> n.BooleanVal2ID != null).Single().BooleanVal2.val.StartsWith("ЕСТЬ");
            //return true;
        }
        //включено ли апв
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
        //вкл ли запуск апв от мтз1
        internal bool IsApvStartsFromMtz1()
        {
            if(getApvMenuElement("Пуск от МТЗ1\\{value}").BooleanVal3.boolVal.Contains("ВКЛ")) return true;
            else return false;
        }
        //вкл ли запуск апв от мтз2
        internal bool IsApvStartsFromMtz2()
        {
            if (getApvMenuElement("Пуск от МТЗ2\\{value}").BooleanVal3.boolVal.Contains("ВКЛ")) return true;
            else return false;
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
