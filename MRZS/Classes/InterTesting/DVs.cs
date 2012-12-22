using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Linq;
using MRZS.Web.Models;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MRZS.Classes.InterTesting
{
    public class DVs
    {
        internal bool IsDV1ConfOnTurnOFFMtz1()
        {
            IEnumerable<int?> dv1mrzsInOutOptionsID = from t in LoadData.MrzsTable
                               where t.parentID ==
                            (LoadData.MrzsTable.Where(n => n.menuElement != null).Where(n => n.menuElement.Contains("ДВ01") && n.passwordCheckType == null).Single().id)
                            && t.BooleanValID==1
                            select t.mrzsInOutOptionsID;
            List<string> TurnOnFuncs = LoadData.mrzsInOutOptionTable.Where(n => dv1mrzsInOutOptionsID.Contains(n.id)).Select(n => n.optionsName).ToList();
            if (TurnOnFuncs.Contains("Блок МТЗ 1")) return true;
            else return false;
        }
    }
}
