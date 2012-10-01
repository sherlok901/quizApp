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
        List<int> SelectedID = new List<int>(0);
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
                m = getTransformedMenuClass(entity);
                list.Add(m);
            }
            return list;
        }

        //transform mrzsmenu05 entity to Menu class
        private Menu getTransformedMenuClass(mrzs05mMenu entity)
        {
            Menu m = new Menu();
            //set ids
            m.ID = entity.id;
            m.ParentID = entity.parentID;
            if (entity.unitValue != null) m.Unit = entity.unitValue.Replace(" ", string.Empty);
            if (entity.value != null) m.Value = entity.value.Replace(" ", string.Empty);
            m.HasChildren = checkMenuOnShowOneOrTwo(m);
            //check column MenuElement
            if (entity.menuElement == null)
            {
                m.FirstLine = entity.mrzsInOutOption.optionsName;
                m.SecondLine = entity.BooleanVal.val;
            }
            else if (entity.menuElement.IndexOf("{value}") != -1)
            {
                string temp = null;
                //if column "value" is null
                if (entity.value == null)
                {
                    string val = checkColumnsForNotNULL(entity);
                    temp = entity.menuElement.Replace("{value}", val);
                    string[] s = temp.Split(new char[1] { '\\' });
                    m.FirstLine = s[0];
                    m.SecondLine = s[1];
                }
                //when "value" column is not null
                else
                {
                    string val = entity.value.Replace(" ", string.Empty);
                    temp = entity.menuElement.Replace("{value}", val);
                    //m.Value = entity.value;
                    temp = replaceUnit(entity, temp);
                    if (temp.IndexOf("\\") != -1)
                    {
                        string[] s = temp.Split(new char[1] { '\\' });
                        m.FirstLine = s[0];
                        m.SecondLine = s[1];
                    }
                    else m.FirstLine = temp;
                }
                m.Name = temp;
            }
            else if (entity.menuElement.IndexOf("{0000.0000}") != -1)
            {
                replaceNulls(entity, m);
            }
            else if (entity.menuElement.IndexOf("{dd:mm:year\\hh:mm:ss}") != -1)
            {
                //m.FirstLine = DateTime.Now.ToShortDateString();
                string s = "dd.MM.yyyy";
                m.FirstLine = DateTime.Now.ToString(s);
                string s2 = "hh:mm:ss";
                m.SecondLine = DateTime.Now.ToString(s2);
            }            
            else
            {
                m.FirstLine = entity.menuElement;
            }
            return m;
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
            //get choosed Menu
            Menu ChoosedMenu = dispControllr.getChoosedMenuClass();
            if (ChoosedMenu != null)
            {
                //get deeper Menu level by ID choosed menu
                List<Menu> list=getMenu(ChoosedMenu.ID);
                if (list.Count == 0) return;

                dispControllr.showMenu(list);
                //add choosed Menu id to list
                SelectedID.Add(ChoosedMenu.ID);
            }
        }
        internal void escButtonClicked()
        {            
            //get selected mrzs05menu entity
            if (SelectedID.Count == 0) return;
            mrzs05mMenu temp=ld.MrzsTable.Where(n=>n.id==SelectedID.Last()).Single();
            if (temp == null) return;

            //transform entity to Menu class
            Menu LastChoosedMenu= getTransformedMenuClass(temp);
            dispControllr.showMenu(getMenu(LastChoosedMenu.ParentID), LastChoosedMenu);
            //delete last selected Menu class
            SelectedID.RemoveAt(SelectedID.IndexOf(SelectedID.Last()));
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
                string[] s= strForReplace.Split(new char[1] { '\\' });
                menuForSaveRezults.FirstLine = s[0];
                menuForSaveRezults.SecondLine = s[1];
            }            
        }
        //has Menu children or not
        bool checkMenuOnShowOneOrTwo(Menu m)
        {
            List<mrzs05mMenu> mrzsTables = AddFunctions.getEntitiesByParentID(ld.MrzsTable, m.ID);
            if (mrzsTables == null) return false;
            else if (mrzsTables.Count == 0) return false;
            else return true;            
        }
        //check some columns in mrzs05menu for not null
        string checkColumnsForNotNULL(mrzs05mMenu ent)
        {
            if (ent.kindSignalDCid != null) return ent.kindSignalDC.kindSignal;
            else if (ent.typeSignalDCid != null) return ent.typeSignalDC.typeSignal;
            else if (ent.typeFuncDCid != null) return ent.typeFuncDC.typeFunction;
            else if (ent.BooleanVal2ID != null) return ent.BooleanVal2.val;
            else if (ent.BooleanVal3ID != null) return ent.BooleanVal3.boolVal;
            else if (ent.mtzValID != null) return ent.mtzVal.mtzVals;
            else return null;
        }
    }
}
