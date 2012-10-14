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
using MRZS.Views.Emulator;

namespace MRZS.Classes
{
    internal class MenuController
    {       
        IEnumerable<mrzs05mMenu> MrzsTables;        
        DisplayViewModel dispControllr = new DisplayViewModel();
        LoadData ld = new LoadData();
        List<int> SelectedID = new List<int>(0);
        NumValueChanging NumValue = new NumValueChanging();
        List<int?> AllParentID;

        public event EventHandler DataLoad;        
        //current parentID of menu
        int? CurrentParentID = null;
        bool Showed = false;        

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
        internal void showNextMenuLine(EmulatorDisplay d)
        {
            dispControllr.moveToNextLine();
            impruveSelection(d);
        }
        //impruve selection
        private static void impruveSelection(EmulatorDisplay d)
        {
            //check width of selection
            if (d.IsSecondLineSelected)
            {
                d.SecondBorder.Width = d.SecondTextBlock.Text.Length * 16;
            }
        }
        internal void showPreviousMenuLine(EmulatorDisplay d)
        {
            dispControllr.moveToPreviousLine();
            impruveSelection(d);
        }

        internal void enterButtonClicked(EmulatorDisplay d)
        {            
            //get choosed Menu
            Menu ChoosedMenu = dispControllr.getChoosedMenuClass();
            if (ChoosedMenu != null)
            {
                //get deeper Menu level by ID choosed menu
                List<Menu> list = getMenu(ChoosedMenu.ID);
                //no deeper menu
                if (list.Count == 0)
                {
                    //if showed that password entered incorrect
                    if (PasswordController.IsPasswordCorrect() == false)
                    {
                        PasswordController.setPasswordAsk();
                        //show parent menu
                        returnToParentMenu();
                        return;
                    }

                    PasswordController.passwordProcess();
                        //first show text with selected second line
                    if (PasswordController.canShowValueWithSelection())
                    {   
                        //show menu
                        Menu m=dispControllr.getChoosedMenuClass();
                        dispControllr.showText(m.FirstLine, m.SecondLine);

                        //select first digit
                        if (Inputing.isNumericValue(m.SecondLine))
                        {
                            changingNumValue2(d, dispControllr.getChoosedMenuClass());                            
                        }
                        //select all value
                        else d.IsSecondLineSelected = true;                        
                        
                    }
                        //show next value
                    else if (PasswordController.canShowChangebleValue())
                    {                        
                        Menu m = dispControllr.getChoosedMenuClass();
                        if (Inputing.isNumericValue(m.SecondLine))
                        {                                                        
                            //canseled\saving dialog
                            changingNumValue2(d, dispControllr.getChoosedMenuClass());                            
                            d.IsSecondLineSelected = false;
                            dispControllr.showText("Вы уверены?", "Enter-ДА, Esc-НЕТ");
                            //set waiting state of confirming value by user
                            PasswordController.setWaintingState();
                            return;
                        }
                        else
                        {
                            //select all value
                            changingValue(d, dispControllr.getChoosedMenuClass());
                            d.IsSecondLineSelected = true;
                        }                        
                    }
                        //if user want save inputed\choosed value
                    else if (PasswordController.isWaitingState())
                    {
                        ld.savingAllChanges();
                        //set first state
                        PasswordController.setPasswordAsk();
                        //show parent last selected menu
                        returnToParentMenu();
                    }                                                                    
                }
                //still exist deeper menu
                else
                {
                    dispControllr.showMenu(list);
                    //add choosed Menu id to list
                    SelectedID.Add(ChoosedMenu.ID);
                }
            }
            
        }
        internal void escButtonClicked(EmulatorDisplay d)
        {
            
            //if user canseled after entering\choosing value
            if (PasswordController.canShowChangebleValue()||PasswordController.canShowValueWithSelection())
            {
                Menu m = dispControllr.getChoosedMenuClass();
                if (Inputing.isNumericValue(m.SecondLine))
                {
                    //saving numeric value
                    changingNumValue2(d, m);
                }

                //turn off selection
                d.IsSecondLineSelected = false;
                dispControllr.showText("Вы уверены?", "Enter-ДА, Esc-НЕТ");
                //set waiting state of confirming value by user
                PasswordController.setWaintingState();
                return;
            }
            //if user want reject entered\choosed value
            else if (PasswordController.isWaitingState())
            {
                ld.rejectAllChanges();
                //set first state
                PasswordController.setPasswordAsk();
            }
            //if showed that password entered incorrect or canseled entering password process
            else if (PasswordController.IsPasswordCorrect() == false||PasswordController.IsCheckPassword())
            {
                PasswordController.setPasswordAsk();                
            }            

            //if no selected mrzs05menu entity
            if (SelectedID.Count == 0) return;

            returnToParentMenu();
        }
        internal void numButtonClicked(TextBox t,int num)
        {
            if (PasswordController.canShowChangebleValue() || PasswordController.canShowValueWithSelection())
            {
                NumValue.enteredNumeric(t,num);                
            }
            else if (PasswordController.IsCheckPassword()) dispControllr.SecondMenuStr += num.ToString();
        }
        
        internal void leftButtonClicked(TextBox t)
        {
            Menu m = dispControllr.getChoosedMenuClass();
            if (Inputing.isNumericValue(m.SecondLine) && PasswordController.canShowValueWithSelection()) NumValue.leftButtonClicked(t);
        }
        internal void rightButtonClicked(TextBox t)
        {
            Menu m = dispControllr.getChoosedMenuClass();
            if (Inputing.isNumericValue(m.SecondLine) && PasswordController.canShowValueWithSelection()) NumValue.rightButtonclicked(t);
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
        
        //give user opportunity input\choose value\values
        //this method get next choosed value and set it to mrzs entity
        void changingValue(EmulatorDisplay d,Menu CurrMenu)
        {
            //get current entity
            mrzs05mMenu ent = AddFunctions.getEntityByID(ld.MrzsTable, CurrMenu.ID);
            
            //if value can be choosed by Enter
            if (ent.value == null)
            {
                string val = null;
                if (ent.kindSignalDCid != null)
                {
                    //get entity by another id that not equale id of Menu
                    kindSignalDC KindEnt = ld.kindSignalDCTable.Where(n => n.id != ent.kindSignalDCid).Single();
                    //get val from entity
                    val = KindEnt.kindSignal;

                    ent.kindSignalDCid = Convert.ToInt32(KindEnt.id);
                }
                else if (ent.typeSignalDCid != null)
                {
                    typeSignalDC TypeEnt = ld.typeSignalDCTable.Where(n => n.id != ent.typeSignalDCid).Single();
                    val = TypeEnt.typeSignal;
                    ent.typeSignalDCid = Convert.ToInt32(TypeEnt.id);
                }
                else if (ent.typeFuncDCid != null)
                {
                    typeFuncDC TypeFunc = ld.typeFuncDCTable.Where(n => n.@int != ent.typeFuncDCid).Single();
                    val = TypeFunc.typeFunction;
                    ent.typeFuncDCid = Convert.ToInt32(TypeFunc.@int);
                }
                else if (ent.BooleanVal2ID != null)
                {
                    BooleanVal2 Bool2Ent = ld.BooleanVal2Table.Where(n => n.id != ent.BooleanVal2ID).Single();
                    val = Bool2Ent.val;
                    //set new value to current mrzs05Menu entity
                    ent.BooleanVal2ID = Convert.ToInt32(Bool2Ent.id);
                }
                else if (ent.BooleanVal3ID != null)
                {
                    BooleanVal3 Bool3 = ld.BooleanVal3Table.Where(n => n.id != ent.BooleanVal3ID).Single();
                    val = Bool3.boolVal;
                    ent.BooleanVal3ID = Convert.ToInt32(Bool3.id);
                }
                else if (ent.mtzValID != null)
                {
                    mtzVal MtzEnt = ld.mtzValTable.Where(n => n.id != ent.mtzValID).Single();
                    val = MtzEnt.mtzVals;
                    ent.mtzValID = Convert.ToInt32(MtzEnt.id);
                }

                CurrMenu.SecondLine = val;
                dispControllr.SecondMenuStr = val;

            }
            //if changed\entered numerical value
            //else
            //{
            //    //changing\enter numerical value
            //    NumValue.parseNumeric(d.SecondTextBlock, d.SecondTextBlock.Text);
            //}
        }
        
        //select inputed numeric value and temporary saving
        void changingNumValue2(EmulatorDisplay d, Menu CurrMenu)
        {
            //get current entity
            mrzs05mMenu ent = AddFunctions.getEntityByID(ld.MrzsTable, CurrMenu.ID);
            NumValue.parseNumeric(d.SecondTextBlock, d.SecondTextBlock.Text);
            //temporary saved value
            NumValue.setValue(dispControllr.SecondMenuStr);
            CurrMenu.SecondLine = NumValue.getValue() + CurrMenu.Unit;
            if (ent.value.Equals(NumValue.getValue()) == false) ent.value = NumValue.getValue();
        }

        //return to parent selected menu
        void returnToParentMenu()
        {
            //get selected mrzs05menu entity
            mrzs05mMenu temp = ld.MrzsTable.Where(n => n.id == SelectedID.Last()).Single();
            if (temp == null) return;

            //transform entity to Menu class
            Menu LastChoosedMenu = getTransformedMenuClass(temp);
            dispControllr.showMenu(getMenu(LastChoosedMenu.ParentID), LastChoosedMenu);
            //delete last selected Menu class
            SelectedID.RemoveAt(SelectedID.IndexOf(SelectedID.Last()));
        }

        
    }
}
