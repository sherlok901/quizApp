using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class DisplayListViewModel
    {
        private ObservableCollection<mrzs05mMenu> currentMenuMembers;
        IEnumerable<mrzs05mMenu> MrzsTable;

        public void displayFirstMenuList(IEnumerable<mrzs05mMenu> MrzsTable2)
        {
            MrzsTable = MrzsTable2;
            List<int?> DiffrentMrzsParentId = AddFunctions.selectDistinctMrzsId(MrzsTable);

        }
    }
}
