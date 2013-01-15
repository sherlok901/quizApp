using MRZS.Web.Models;
using System.Linq;
using System;

namespace MRZS.Classes.InterTesting
{
    internal class ZZ
    {
        //добавлено ли зз к меню
        internal bool IsZZaddedToMenu()
        {
            return LoadData.MrzsTable.Where(n => n.menuElement != null && n.menuElement.StartsWith("ЗЗ \\{value}") && n.BooleanVal2ID != null).Single().BooleanVal2.val.StartsWith("ЕСТЬ");
            //return LoadData.MrzsTable.Where(n => n.menuElement != null).Where(n => n.menuElement.StartsWith("ЗЗ\\{value}")).Where(n => n.BooleanVal2ID != null).Single().BooleanVal2.val.StartsWith("ЕСТЬ");
        }

        //включено ли зз
        internal bool IsZzTurnOn()
        {
            return getZZMenuElement("Защита ЗЗ\\{value}").BooleanVal3.boolVal.StartsWith("ВКЛ");
        }
        //уставка ЗЗ
        internal double UstavkaZz()
        {
            return getMenuElementValue("Уставка ЗЗ ЗI0\\{value}");
        }
        //выдержка ЗЗ
        internal double VudergkaZz()
        {
            return getMenuElementValue("Выдержка ЗЗ\\{value}");
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
        mrzs05mMenu getZZMenuElement(string name)
        {
            return LoadData.MrzsTable.Where(n => n.menuElement != null).Where(n => n.menuElement.StartsWith(name)).Where(n => n.BooleanVal3ID != null).Single();
        }
    }
}
