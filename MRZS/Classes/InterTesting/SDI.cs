using System.Collections.Generic;
using System.Linq;
using MRZS.Web.Models;

namespace MRZS.Classes.InterTesting
{
    internal class SDI
    {
        string SD1Name = "СДИ1";
        string SD2Name = "СДИ2";
        string SD3Name = "СДИ3";
        string SD4Name = "СДИ4";
        string SD5Name = "СДИ5";
        string SD6Name = "СДИ6";

        string MTZ1ReleParamName = "Сраб МТЗ 1";
        string MTZ2ReleParamName = "Сраб МТЗ 2";
        string boolValName = "ДА";

        //настроено ли CД01 на МТЗ1
        internal bool IsSD1ConfiguredOnMTZ1()
        {
            return checkSdiOnMtz(MTZ1ReleParamName, SD1Name, boolValName);
        }
        //настроено ли CД02 на МТЗ2
        internal bool IsSD2ConfiguredOnMTZ1()
        {
            return checkSdiOnMtz(MTZ1ReleParamName, SD2Name, boolValName);
        }
        //настроено ли CД03 на МТЗ2
        internal bool IsSD3ConfiguredOnMTZ1()
        {
            return checkSdiOnMtz(MTZ1ReleParamName, SD3Name, boolValName);
        }
        //настроено ли CД04 на МТЗ2
        internal bool IsSD4ConfiguredOnMTZ1()
        {
            return checkSdiOnMtz(MTZ1ReleParamName, SD4Name, boolValName);
        }
        //настроено ли CД05 на МТЗ2
        internal bool IsSD5ConfiguredOnMTZ1()
        {
            return checkSdiOnMtz(MTZ1ReleParamName, SD5Name, boolValName);
        }
        //настроено ли CД06 на МТЗ2
        internal bool IsSD6ConfiguredOnMTZ1()
        {
            return checkSdiOnMtz(MTZ1ReleParamName, SD6Name, boolValName);
        }


        //настроено ли CД01 на МТЗ2
        internal bool IsSD1ConfiguredOnMTZ2()
        {
            return checkSdiOnMtz(MTZ2ReleParamName, SD1Name, boolValName);
        }
        //настроено ли CД02 на МТЗ2
        internal bool IsSD2ConfiguredOnMTZ2()
        {
            return checkSdiOnMtz(MTZ2ReleParamName, SD2Name, boolValName);
        }
        //настроено ли CД03 на МТЗ2
        internal bool IsSD3ConfiguredOnMTZ2()
        {
            return checkSdiOnMtz(MTZ2ReleParamName, SD3Name, boolValName);
        }
        //настроено ли CД04 на МТЗ2
        internal bool IsSD4ConfiguredOnMTZ2()
        {
            return checkSdiOnMtz(MTZ2ReleParamName, SD4Name, boolValName);
        }
        //настроено ли CД05 на МТЗ2
        internal bool IsSD5ConfiguredOnMTZ2()
        {
            return checkSdiOnMtz(MTZ2ReleParamName, SD5Name, boolValName);
        }
        //настроено ли CД06 на МТЗ2
        internal bool IsSD6ConfiguredOnMTZ2()
        {
            return checkSdiOnMtz(MTZ2ReleParamName, SD6Name, boolValName);
        }
        
        internal bool IsSD1ConfiguredOnZZ()
        {
            return checkSdiOnMtz("Сраб ЗЗ", SD1Name, boolValName);
        }
        internal bool IsSD3ConfiguredOnZZ()
        {
            return checkSdiOnMtz("Сраб ЗЗ", SD3Name, boolValName);
        }

        //проверка СДИ настроеные ли на мтз1 или мтз2
        bool checkSdiOnMtz(string MTZ1ReleParamName_, string SdiName, string boolValNam)
        {
            string boolval = IsParamTurnOnInCurrRele(MTZ1ReleParamName_, SdiName);
            if (boolval.Contains(boolValNam)) return true;
            else return false;
        }
        //get boolval by MrzsInOutOption paramName and rele name
        string IsParamTurnOnInCurrRele(string ParamName, string SDIName)
        {           
            int id = LoadData.MrzsTable.Where(n => n.menuElement != null).Where(n => n.menuElement.StartsWith(SDIName)).Single().id;
            int? paramID = LoadData.mrzsInOutOptionTable.Where(n => n.optionsName.Contains(ParamName)).Select(n => n.id).Single();
            //select all rows of current sdi in mrzs table
            List<mrzs05mMenu> CurrSdi = (from m in LoadData.MrzsTable where m.parentID == id select m).ToList();
            int? boolValID = (from table in CurrSdi where table.mrzsInOutOptionsID == paramID select table.BooleanValID).Single();
            string boolVal = (from b in LoadData.BooleanValTable where b.id == boolValID select b.val).Single();

            return boolVal;
        }
    }
}
