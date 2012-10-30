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
        internal static List<mrzs05mMenu> getEntitiesByParentID(IEnumerable<mrzs05mMenu> mrzsTables,int? parentID)
        {
            return (from t in mrzsTables where t.parentID == parentID select t).ToList();
        }
        internal static mrzs05mMenu getEntityByID(IEnumerable<mrzs05mMenu> mrzsTables, int id)
        {
            return (from t in mrzsTables where t.id == id select t).Single();
        }        
    }
}
