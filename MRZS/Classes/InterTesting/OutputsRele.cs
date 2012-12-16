using System.Linq;
using System.Collections.Generic;
using MRZS.Web.Models;

namespace MRZS.Classes.InterTesting
{
    internal class OutputsRele
    {
        string Rele1Name = "P01";
        string MTZ1ReleParamName = "Сраб МТЗ 1";
        string MTZ2ReleParamName = "Сраб МТЗ 2";
        string boolValName = "ДА";


        internal bool IsRele1ConfiguredOnMTZ1()
        {
            string boolval = IsParamTurnOnInCurrRele(MTZ1ReleParamName, Rele1Name);
            if (boolval.Contains(boolValName)) return true;
            else return false;
        }
        internal bool IsRele1ConfiguredOnMTZ2()
        {
            string boolval = IsParamTurnOnInCurrRele(MTZ2ReleParamName, Rele1Name);
            if (boolval.Contains(boolValName)) return true;
            else return false;
        }

        //get boolval by MrzsInOutOption paramName and rele name
         string IsParamTurnOnInCurrRele(string ParamName,string ReleName)
        {
            int id= (from t2 in
                 (from t in LoadData.MrzsTable where t.menuElement != null select t)
             where t2.menuElement.StartsWith(ReleName)
             select t2).Single().id;            
            int? paramID = LoadData.mrzsInOutOptionTable.Where(n => n.optionsName.Contains(ParamName)).Select(n => n.id).Single();
            //select all rows of current rele in mrzs table
            List<mrzs05mMenu> CurrRele = (from m in LoadData.MrzsTable where m.parentID == id select m).ToList();
            int? boolValID = (from table in CurrRele where table.mrzsInOutOptionsID == paramID select table.BooleanValID).Single();
            string boolVal = (from b in LoadData.BooleanValTable where b.id == boolValID select b.val).Single();

            return boolVal;
        }
    }
}
