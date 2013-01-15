using System.Linq;
using System.Collections.Generic;
using MRZS.Web.Models;

namespace MRZS.Classes.InterTesting
{
    internal class OutputsRele
    {
        string Rele1Name = "P01";
        string Rele2Name = "P02";
        string Rele3Name = "P03";
        string Rele4Name = "P04";
        string Rele5Name = "P05";

        string MTZ1ReleParamName = "Сраб МТЗ 1";
        string MTZ2ReleParamName = "Сраб МТЗ 2";
        string boolValName = "ДА";

        string ZzParam = "Сраб ЗЗ";

        //настроено ли реле01 на МТЗ1
        internal bool IsRele1ConfiguredOnMTZ1()
        {
            return checkReleOnMtz(MTZ1ReleParamName, Rele1Name, boolValName);
        }
        //настроено ли реле02 на МТЗ1
        internal bool IsRele2ConfiguredOnMTZ1()
        {
            return checkReleOnMtz(MTZ1ReleParamName, Rele2Name, boolValName);
        }
        //настроено ли реле03 на МТЗ1
        internal bool IsRele3ConfiguredOnMTZ1()
        {
            return checkReleOnMtz(MTZ1ReleParamName, Rele3Name, boolValName);
        }
        //настроено ли реле04 на МТЗ1
        internal bool IsRele4ConfiguredOnMTZ1()
        {
            return checkReleOnMtz(MTZ1ReleParamName, Rele4Name, boolValName);
        }
        //настроено ли реле05 на МТЗ1
        internal bool IsRele5ConfiguredOnMTZ1()
        {
            return checkReleOnMtz(MTZ1ReleParamName, Rele5Name, boolValName);
        }

        //common func for checking rele on mtz1 param
        bool checkReleOnMtz(string MTZ1ReleParamName_, string ReleName,string boolValNam)
        {
            string boolval = IsParamTurnOnInCurrRele(MTZ1ReleParamName_, ReleName);
            if (boolval.Contains(boolValNam)) return true;
            else return false;
        }

        //настроено ли реле01 на МТЗ1
        internal bool IsRele1ConfiguredOnMTZ2()
        {
            string boolval = IsParamTurnOnInCurrRele(MTZ2ReleParamName, Rele1Name);
            if (boolval.Contains(boolValName)) return true;
            else return false;
        }
        //настроено ли реле02 на МТЗ1
        internal bool IsRele2ConfiguredOnMTZ2()
        {
            return checkReleOnMtz(MTZ2ReleParamName, Rele2Name, boolValName);
        }
        //настроено ли реле03 на МТЗ1
        internal bool IsRele3ConfiguredOnMTZ2()
        {
            return checkReleOnMtz(MTZ2ReleParamName, Rele3Name, boolValName);
        }
        //настроено ли реле04 на МТЗ1
        internal bool IsRele4ConfiguredOnMTZ2()
        {
            return checkReleOnMtz(MTZ2ReleParamName, Rele4Name, boolValName);
        }
        //настроено ли реле05 на МТЗ1
        internal bool IsRele5ConfiguredOnMTZ2()
        {
            return checkReleOnMtz(MTZ2ReleParamName, Rele5Name, boolValName);
        }

        internal bool IsRele1ConfiguredOnZZ()
        {
            string boolval = IsParamTurnOnInCurrRele(ZzParam, Rele1Name);
            if (boolval.Contains(boolValName)) return true;
            else return false;
        }
        internal bool IsRele5ConfiguredOnZZ()
        {
            string boolval = IsParamTurnOnInCurrRele(ZzParam, Rele5Name);
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
