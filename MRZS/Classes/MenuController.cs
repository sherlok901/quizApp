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
using MRZS.Classes.DisplayCode;
using MRZS.Web.Models;

namespace MRZS.Classes
{
    internal class MenuController
    {
        IEnumerable<mrzs05mMenu> MrzsTables;
        internal DisplayViewModel setDefaultMenu(IEnumerable<mrzs05mMenu> MrzsTable2)
        {
            MrzsTables = MrzsTable2;
            List<int?> DiffrentMrzsParentId = AddFunctions.selectDistinctMrzsId(MrzsTables);
            List<mrzs05mMenu>mrzsList= AddFunctions.getEntitiesByParentID(MrzsTable2, DiffrentMrzsParentId[0]);
            Menu m = new Menu();
            m.setChildren(mrzsList);
            DisplayViewModel dispControllr = new DisplayViewModel();
            return dispControllr.showMenu(m);
        }
    }
}
