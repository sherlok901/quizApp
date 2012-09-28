using System;
using System.Linq;
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
        DisplayViewModel dispControllr = new DisplayViewModel();
        LoadData ld = new LoadData();
        List<int?> SelectedID = new List<int?>(0);
        List<int?> AllParentID;

        public event EventHandler DataLoad;        
        //current parentID of menu
        int? CurrentParentID = null;
        

        internal MenuController()
        {
            //subcribe to data load event
            ld.DataLoaded += ld_DataLoaded;
        }

        void ld_DataLoaded(object sender, EventArgs e)
        {
            if (DataLoad != null) DataLoad(this, EventArgs.Empty);
        }

        //getting first menu level
        internal List<Menu> getFirstMenu()
        {
            AllParentID = AddFunctions.selectDistinctMrzsId(ld.MrzsTable);
            CurrentParentID = AllParentID[0];
            return getMenu(CurrentParentID);            
        }
        
        //get menu by parentID
        List<Menu> getMenu(int? parentID)
        {
            List<mrzs05mMenu> mrzsTables= AddFunctions.getEntitiesByParentID(ld.MrzsTable, parentID);
            return getTransformedMenu(mrzsTables);
        }

        //transform entity to Menu class
        List<Menu> getTransformedMenu(List<mrzs05mMenu> MrzsTables)
        {
            List<Menu> list = new List<Menu>(0);
            Menu m;
            foreach (mrzs05mMenu entity in MrzsTables)
            {
                m = new Menu();
                //set ids
                m.ID = entity.id;
                m.ParentID = entity.parentID;
                if (entity.unitValue != null) m.Unit = entity.unitValue.Replace(" ",string.Empty);
                if (entity.value != null) m.Value = entity.value.Replace(" ", string.Empty);
                //check column
                if (entity.menuElement.IndexOf("{value}") != -1)
                {
                    string temp = null;
                    //if column "value" is null
                    if (entity.value == null)
                    {
                    }
                    else
                    {
                        temp=entity.menuElement.Replace("{value}", entity.value);
                        m.Value = entity.value;
                        temp = replaceUnit(entity, temp);                        
                    }
                    m.Name = temp;
                }
                else if (entity.menuElement.IndexOf("{0000.0000}") != -1)
                {
                    replaceNulls(entity, m);                    
                }
                else if (entity.menuElement.IndexOf("{dd:mm:year\\hh:mm:ss}") != -1)
                {
                }
                else if(entity.menuElement==null)
                {

                }
                else
                {
                    m.Name = entity.menuElement;
                }
                list.Add(m);
            }
            return list;
        }
        
        internal DisplayViewModel setDefaultMenu()
        {
            //show first level menu
            dispControllr.showMenu(getFirstMenu());
            return dispControllr.getThisInstance();
        }
        internal void showNextMenuLine()
        {
            dispControllr.moveToNextLine();
        }
        internal void showPreviousMenuLine()
        {
            dispControllr.moveToPreviousLine();
        }
        internal void enterButtonClicked()
        {
            Menu ChoosedMenu = dispControllr.getChoosedMenuClass();
            if (ChoosedMenu != null)
            {
                //get deeper Menu level by ID choosed menu
                dispControllr.showMenu(getMenu(ChoosedMenu.ID));
            }
        }

        //methods for replace strings
        string replaceUnit(mrzs05mMenu entity1,string strWithReplacedValue)
        {
            //if menuElement column has {unit}
            if (strWithReplacedValue.IndexOf("{unit}") != -1)
            {
                strWithReplacedValue = strWithReplacedValue.Replace("{unit}", " " + entity1.unitValue);
            }
            //if unit column is not null
            else if (entity1.unitValue != null)
            {
                strWithReplacedValue = strWithReplacedValue + entity1.unitValue;
            }
            return strWithReplacedValue;
        }
        //replace {0000.0000} in menuElement
        void replaceNulls(mrzs05mMenu ent,Menu menuForSaveRezults)
        {
            string strForReplace = ent.menuElement;            
            strForReplace = strForReplace.Replace("{0000.0000}", ent.value);
            if (ent.unitValue != null) strForReplace = strForReplace + " " + ent.unitValue;            
            if (strForReplace.IndexOf("\\") != -1)
            {
                strForReplace = strForReplace.Replace("\\", Environment.NewLine);
            }
            //replace spaces
            strForReplace = strForReplace.Replace(" ", string.Empty);
            menuForSaveRezults.Name = strForReplace;            
        }
    }
}
