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
using System.Linq;

namespace MRZS.Classes
{
    internal static class AddFunctions
    {
        internal static List<int?> selectDistinctMrzsId(IEnumerable<mrzs05mMenu> mrzsList)
        {
            return mrzsList.Select(n => n.parentID).Distinct().ToList();
        }
    }
}
