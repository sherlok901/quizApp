﻿using System;
using System.Collections.Generic;
using System.Linq;
using MRZS.Web.Models;

namespace MRZS.Classes.InterTesting
{
    internal class MTZ
    {
        //включено ли мтз вообще
        internal bool IsTurnOn()
        {
            if (getMenuElement("МТЗ \\{value}").BooleanVal2.val.Contains("ЕСТЬ")) return true;
            else return false;
        }
        internal void SetTurnOn()
        {
            int id = (int)getMenuElement("МТЗ \\{value}").BooleanVal2ID;
            int TurnOnId = LoadData.BooleanVal2Table.Where(n => n.val.Contains("ЕСТЬ")).Single().id;
            if (id != TurnOnId)
            {
                getMenuElement("МТЗ \\{value}").BooleanVal2ID = LoadData.BooleanVal2Table.Where(n => n.val.Contains("ЕСТЬ")).Single().id;               
            }
        }
        //включено ли мтз1
        internal bool IsMTZ1TurnOn()
        {
            if (getMenuElement("1 Ступень МТЗ\\{value}").BooleanVal3.boolVal.Contains("ВКЛ")) return true;
            else return false;
        }
        internal void SetMtz1TurnOn()
        {
            int id = (int)getMenuElement("1 Ступень МТЗ\\{value}").BooleanVal3ID;
            int TurnOnId= LoadData.BooleanVal3Table.Where(n => n.boolVal.Contains("ВКЛ")).Single().id;
            if (TurnOnId != id)
            {
                getMenuElement("1 Ступень МТЗ\\{value}").BooleanVal3ID = LoadData.BooleanVal3Table.Where(n => n.boolVal.Contains("ВКЛ")).Single().id;               
            }
        }
        //включено ли мтз2
        internal bool IsMTZ2TurnOn()
        {
            if (getMenuElement("2 Ступень МТЗ\\{value}").BooleanVal3.boolVal.Contains("ВКЛ")) return true;
            else return false;
        }
        //значение уставкм МТЗ1
        internal double getMTZ1Value()
        {
            return getMenuElementValue("Уставка МТЗ1\\{value}");
        }
        internal void SetMtz1Value(string value)
        {
            mrzs05mMenu ent = getMenuElement("Уставка МТЗ1\\{value}");
            if (ent.value != value)
            {
                ent.value = value;                
            }
        }
        //уставка мтз2
        internal double getMTZ2Value()
        {
            return getMenuElementValue("Уставка МТЗ2\\{value}");
        }
        //выдержка мтз1
        internal double getTimerMtz1()
        {
            return getMenuElementValue("Выдержка МТЗ1\\{value}");
        }
        internal void SetTimerMtz1(string value)
        {
            getMenuElement("Выдержка МТЗ1\\{value}").value=value;

        }
        //выдержка мтз2
        internal double getTimerMtz2()
        {
            return getMenuElementValue("Выдержка МТЗ2\\{value}");
        }
        
        mrzs05mMenu getMenuElement(string name)
        {
            mrzs05mMenu q = (from t2 in
                                 (from t in LoadData.MrzsTable where t.menuElement != null select t)
                             where t2.menuElement.StartsWith(name)
                             select t2).Single();            
            return q;
        }
        double getMenuElementValue(string name)
        {
            mrzs05mMenu t = getMenuElement(name);
            //string val= LoadData.MrzsTable.Where(n => n.menuElement.StartsWith(name)).Single().value;
            string val = t.value;
            return Double.Parse(replaceDotOnComma(val));
        }
        //заменить в строке точку на комму
        string replaceDotOnComma(string InStr)
        {
            return InStr.Replace('.', ',');
        }
        
        internal void SetMTZsNull()
        {
            mrzs05mMenu m = getMenuElement("Уставка МТЗ1\\{value}");
            if (!m.value.StartsWith("0000.0000"))
            {
                m.value = "0000.0000";
                LoadData.savingAllChanges();
            }
        }
    }
}
