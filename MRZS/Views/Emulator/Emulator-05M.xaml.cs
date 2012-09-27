using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Windows.Controls.Primitives;
using MRZS.Helpers;
using MRZS.Web.Services;
using MRZS.Web.Models;
using MRZS.Web;
using System.Configuration;
using System.ServiceModel;
using System.Windows.Browser;
using System.ServiceModel.DomainServices.Client;
using MRZS.Classes;
using System.Windows.Threading;
using System.Threading;
using System.Globalization;
using System.ServiceModel.DomainServices;
using MRZS.Classes.DisplayCode;
using MRZS.Views.Emulator;
using System.Windows.Data;

namespace MRZS.Views.Emulator
{
    public partial class Emulator_05M : System.Windows.Controls.Page
    {                                
        //fields        
        private int AnimationCursorLineCurntPositionIndex=0;
        List<int?> parentIDlist;
        private bool displayAnimFlag;
        private NumericUpDown numUpDown1;
        //entity        
        IEnumerable<passwordCheckType> passwordCheckTypeList = null;
        IEnumerable<kindSignalDC> kindSignalDCList = null;
        IEnumerable<typeSignalDC> typeSignalDCList=null;
        IEnumerable<typeFuncDC> typeFuncDCList=null;
        IEnumerable<BooleanVal> BooleanValList = null;
        IEnumerable<BooleanVal2> BooleanVal2List=null;
        IEnumerable<BooleanVal3> BooleanVal3List=null;
        IEnumerable<mtzVal> mtzValList=null;
        IEnumerable<mrzsInOutOption> mrzsInOutOptionList = null;
        private IEnumerable<mrzs05mMenu> mrzs05Entity;       
       
        private MyList<mrzs05mMenu> SelectMenuElemHistory = new MyList<mrzs05mMenu>();
        private List<mrzs05mMenu> DisplayedEntities = new List<mrzs05mMenu>(0);        
        LoadOperation<mrzs05mMenu> mrzs05mMModel;
        //LoadOperation<mrzsInOutOption> mrzsInOutOptionModel;
        //LoadOperation<passwordCheckType> passwordCheckTypeModel;
        //LoadOperation<kindSignalDC> kindSignalDCModel;
        //LoadOperation<typeSignalDC> typeSignalDCModel;
        //LoadOperation<typeFuncDC> typeFuncDCModel;
        //LoadOperation<BooleanVal> BooleanValModel;
        //LoadOperation<BooleanVal2> BooleanVal2Model;
        //LoadOperation<BooleanVal3> BooleanVal3Model;
        //LoadOperation<mtzVal> mtzValModel;
        //LoadOperation<mrzsInOutOption> mrzsInOutOptModel;
        mrzs05mMenuContext mrzs05mMContxt;
        //timer
        DispatcherTimer Dtimer = new DispatcherTimer();
        //for jumping in display through 2 line
        mrzs05mMenu tempDisplayedEntity = null;
        List<mrzs05mMenu> tempDisplayedEntities = new List<mrzs05mMenu>(0);
        //inputing numbers
        List<int> numIndexesList = null;
        string inputedSymbol = null;
        string tempSymbol = null;
        private int currInputPosition = -1;
        private string tempDisplayedText = null;
        //password        
        private bool passwordCorrect = false;
        enum PasswordStates
        {
            inputingPasswordStart,            
            checkPassword,
            passwordCorrect,
            passwordInCorrect,
            onlyView,
            allowedEnterValue,
            MemoriseInputedVal,
            passwordAsk
        };
        PasswordStates PassStates=PasswordStates.passwordAsk;
        //choose another value by enter
        bool getAnother = false;
        bool getAnotherVal = false;
        //modes of changing values in menu by user
        enum InputingMode
        {
            InputingNum,
            ChoosingByEnter,
            ChoosingByEnterAndShowNextEntity,
            none
        };
        InputingMode InputingModeStates=InputingMode.InputingNum;
        //get info of inputs
        public enum Inputs
        {
            ДВ01, ДВ02, ДВ03, ДВ04, ДВ05, ДВ06,
            СДИ1, СДИ2, СДИ3, СДИ4, СДИ5, СДИ6,
            P01, P02, P03, P04, P05
        }
        DisplayViewModel dispViewModel;
        bool IsAnimCursorInserted = false;
        LoadData ld = new LoadData();

        public Emulator_05M()
        {
            InitializeComponent();
            display.TextWrapping = TextWrapping.Wrap;            
            display.FontFamily = new FontFamily("Arial");
            display.FontSize = 20.0;                        
            display.Padding = new Thickness(5.0);                          
            display.SelectionStart = display.Text.Length;

         
            //dispViewModel = new DisplayViewModel { FirstMenuStr = "sfs", SecondMenuStr = "sfsf" };
            //emju.DataContext = dispViewModel;
            //dispViewModel.FirstMenuStr = "dddddd";
          
            ld.DataLoaded += ld_DataLoaded;
            
            mrzs05mMContxt = new mrzs05mMenuContext();
            mrzs05mMModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetMrzs05mMenuQuery());
            mrzs05mMModel.Completed += mrzs05mMModel_Completed;

        }

        void ld_DataLoaded(object sender, EventArgs e)
        {
            passwordCheckTypeList = ld.passwordCheckTypeTable;
            kindSignalDCList = ld.kindSignalDCTable;
            typeSignalDCList = ld.typeSignalDCTable;
            typeFuncDCList = ld.typeFuncDCTable;
            BooleanValList = ld.BooleanValTable;
            BooleanVal2List = ld.BooleanVal2Table;
            BooleanVal3List = ld.BooleanVal3Table;
            mtzValList = ld.mtzValTable;
            mrzsInOutOptionList = ld.mrzsInOutOptionTable;
            if(mrzs05Entity==null) mrzs05Entity = ld.MrzsTable;
                        
            MenuController MenuControllr = new MenuController();
            emju.DataContext = MenuControllr.setDefaultMenu(mrzs05Entity);

            
        }        
        
        #region Entity complited loads ***
        /// <summary>
        /// igor: load parentID column
        /// </summary>        
        void mrzs05mMModel_Completed(object sender, EventArgs e)
        {

            if (mrzs05Entity == null) mrzs05Entity = mrzs05mMModel.Entities;
            //get list of different elem in column parentID            
            //mrzs05Entity = getEntities(sender);
            
            if (mrzs05Entity != null)
            {
                parentIDlist = mrzs05Entity.Select(n => n.parentID).Distinct().ToList();
                //get 1 level of menu 
                DisplayedEntities.AddRange(getEntitiesByParentID(parentIDlist[0]));
                DisplayMenu(DisplayedEntities.Select(n => n.menuElement).ToList());
                //set cursor animation
                display.Text = insertAnimatCursor(display.Text, AnimationCursorLineCurntPositionIndex, ">");
                //IsAnimCursorInserted = true;
                //Dtimer.Interval = new TimeSpan(0, 0, 0, 0, 300);
                //Dtimer.Tick += Dtimer_Tick;
                //Dtimer.Start();

            }
        }
                       
        #endregion        
               
        private string insertAnimatCursor(string TextForInserting,int InsertingPosition,string InsertedSymbol)
        {
            return TextForInserting.Insert(InsertingPosition, InsertedSymbol);       
        }

        void Dtimer_Tick(object sender, EventArgs e)
        {
            display.Focus();
            display.SelectionStart = AnimationCursorLineCurntPositionIndex;
            //switch cursor text
            if (displayAnimFlag)
            {
                //display.Text = "_" + display.Text.Substring(1);
                display.Text = display.Text.Remove(AnimationCursorLineCurntPositionIndex, 1);
                display.Text = display.Text.Insert(AnimationCursorLineCurntPositionIndex, "_");
                displayAnimFlag = false;
            }
            else
            {
                //display.Text = ">" + display.Text.Substring(1);
                display.Text = display.Text.Remove(AnimationCursorLineCurntPositionIndex, 1);
                display.Text = display.Text.Insert(AnimationCursorLineCurntPositionIndex, ">");
                displayAnimFlag = true;
            }
            //run animation of cursor again
        }        

        #region cursor events ***
        private void downButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputingModeStates == InputingMode.ChoosingByEnter) return;
            else if (InputingModeStates == InputingMode.ChoosingByEnterAndShowNextEntity)
            {
                //show next mrzs05Menu entity where mrzsInOutOption and BooleanValId not null
                int index = tempDisplayedEntities.IndexOf(tempDisplayedEntity);
                if ((index + 1) < tempDisplayedEntities.Count && index != -1)
                {
                    index += 1;
                    tempDisplayedEntity = tempDisplayedEntities[index];
                    showMrzsInOutOptions();                    
                }
                return;
            }

            //display entity with {value} in menuElement column
            if (tempDisplayedEntity != null && DisplayedEntities.Count != 0 &&tempDisplayedEntity.menuElement!=null)
            {
                int index = tempDisplayedEntities.IndexOf(tempDisplayedEntity);
                if ((index + 1 )< tempDisplayedEntities.Count && index!= -1)
                {
                    index += 1;
                    tempDisplayedEntity = tempDisplayedEntities[index];
                    display.Text = DisplayEntity(tempDisplayedEntity);
                }
            }
            else if(AnimationCursorLineCurntPositionIndex != -1)
            {
                display.Focus();
                if (IsAnimCursorInserted)
                {
                    display.Text = display.Text.Remove(AnimationCursorLineCurntPositionIndex, 1);
                }
                AnimationCursorLineCurntPositionIndex = EmulatorDisplayController.IndexOfnextFirstSymbolFinding(display.Text, AnimationCursorLineCurntPositionIndex);
                if (AnimationCursorLineCurntPositionIndex != -1)
                {
                    if (IsAnimCursorInserted) display.Text = display.Text.Insert(AnimationCursorLineCurntPositionIndex, ">");
                    display.SelectionStart = AnimationCursorLineCurntPositionIndex;
                }
            }

        }      
        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputingModeStates == InputingMode.ChoosingByEnter) return;
            else if (InputingModeStates == InputingMode.ChoosingByEnterAndShowNextEntity)
            {
                //show next mrzs05Menu entity where mrzsInOutOption and BooleanValId not null
                int index = tempDisplayedEntities.IndexOf(tempDisplayedEntity);
                if (index > 0)
                {
                    index -= 1;
                    tempDisplayedEntity = tempDisplayedEntities[index];
                    showMrzsInOutOptions();                    
                }
                return;                              
            }
            //display entity with {value} in menuElement column
            if (tempDisplayedEntity != null && tempDisplayedEntities.Count != 0 &&tempDisplayedEntity.menuElement!=null)
            {
                int index = tempDisplayedEntities.IndexOf(tempDisplayedEntity);                
                if (index > 0)
                {
                    index -= 1;
                    tempDisplayedEntity = tempDisplayedEntities[index];
                    if (tempDisplayedEntity.menuElement.IndexOf("{value}") != -1)
                        display.Text = DisplayEntity(tempDisplayedEntity);
                }
            }
            else if (AnimationCursorLineCurntPositionIndex != -1)
            {
                display.Focus();
                if (IsAnimCursorInserted) display.Text = display.Text.Remove(AnimationCursorLineCurntPositionIndex, 1);
                AnimationCursorLineCurntPositionIndex = EmulatorDisplayController.getPreviousIndexOfStartLineDisplay(display.Text, AnimationCursorLineCurntPositionIndex);
                if (AnimationCursorLineCurntPositionIndex != -1)
                {
                    if (IsAnimCursorInserted) display.Text = display.Text.Insert(AnimationCursorLineCurntPositionIndex, ">");
                    display.SelectionStart = AnimationCursorLineCurntPositionIndex;
                }
            }
        }
        private void leftButton_Click(object sender, RoutedEventArgs e)
        {
            //if current mode if inputing nums
            if (InputingModeStates == InputingMode.InputingNum)
            {
                int index = numIndexesList.IndexOf(currInputPosition);
                if ((index - 1) >= 0)
                {
                    index--;
                    currInputPosition = numIndexesList[index];
                }
                display.Focus();
                display.SelectionStart = currInputPosition;
                display.SelectionLength = 1;
            }
        }
        private void rightButton_Click(object sender, RoutedEventArgs e)
        {
            //if current mode if inputing nums
            if (InputingModeStates == InputingMode.InputingNum)
            {
                int index = numIndexesList.IndexOf(currInputPosition);
                if ((index + 1) <= (numIndexesList.Count - 1))
                {
                    index++;
                    currInputPosition = numIndexesList[index];
                }
                display.Focus();
                display.SelectionStart = currInputPosition;
                display.SelectionLength = 1;
            }
        } 
        #endregion

        #region == other func buttons ==        

        private void enterButton_Click_2(object sender, RoutedEventArgs e)
        {
            mrzs05mMenu selectedEntity = null;
            if (tempDisplayedEntity != null) selectedEntity=tempDisplayedEntity;
            else
            {
                if (DisplayedEntities.Last().mrzsInOutOptionsID != null)
                {
                    selectedEntity = tempDisplayedEntities[0];
                    tempDisplayedEntity = tempDisplayedEntities[0];
                }
                else
                {                                        
                    string selectedWordMenu = GetSelectedWordMenu();
                    //get id selected word by menuElement
                    if (selectedWordMenu != null) selectedEntity = DisplayedEntities.Last(n => n.menuElement == selectedWordMenu);                    
                }
            }
                                
            //get next menu list by parentID of current selected word                
            List<mrzs05mMenu> newMenuLevel = getEntitiesByParentID(selectedEntity.id);
            //check the mrzsInOutOptionsID column
            if (newMenuLevel.Count > 0)
            {
                //add current selected word and id to history list                                               
                SelectMenuElemHistory.Add(selectedEntity);
                //display mrzsInOutOptionsID column
                if (newMenuLevel[0].menuElement == null && newMenuLevel[0].mrzsInOutOptionsID != null)
                {
                    DisplayMenu_MrzsInOutOpt(newMenuLevel);
                    tempDisplayedEntities.AddRange(newMenuLevel);
                }
                else
                {
                    //display menuElement 
                    if (newMenuLevel[0].menuElement.IndexOf("{value}") == -1)
                    {
                        List<string> s = newMenuLevel.Select(n => n.menuElement).ToList();
                        DisplayMenu(s);
                    }
                    //display menuElement with {value}
                    else
                    {
                        clearTextBox(display);
                        //replace {value} on data from value column or from another column if it not null
                        display.Text = replaceValueInColumn(newMenuLevel[0]);
                        //add entities to temp displayed list
                        tempDisplayedEntities.AddRange(newMenuLevel);
                        tempDisplayedEntity = newMenuLevel[0];
                        PassStates = PasswordStates.passwordAsk;
                    }
                }

                DisplayedEntities.AddRange(newMenuLevel);

                //set animation cursor
                AnimationCursorLineCurntPositionIndex = 0;
                //if it is not last menu level
                if (!isLastElem(newMenuLevel[0].id))
                {
                    display.Text = insertAnimatCursor(display.Text, AnimationCursorLineCurntPositionIndex, ">");
                    IsAnimCursorInserted = true;
                }
                else IsAnimCursorInserted = false;
            }
                //no new deep menu level ================================
                //inputing numbers
            else
            {
                //if (!passwordInputing) inputing(display);
                //else
                {
                    switch (PassStates)
                    {
                        case PasswordStates.onlyView:
                            display.Text = replaceValueInColumn(tempDisplayedEntity);
                            PassStates = PasswordStates.passwordAsk;
                            break;
                        case PasswordStates.passwordAsk:
                            inputing(display);
                            break;
                        case PasswordStates.inputingPasswordStart:
                            passwordCheck();
                            if (passwordCorrect) passwordAnswerDisplay(display);
                            else
                            {
                                display.Text = "Пароль введен" + Environment.NewLine + "  " + "неверно";
                                PassStates = PasswordStates.onlyView;
                            }
                            break;
                        case PasswordStates.passwordCorrect:
                            //user can input or choose some value in menu                            
                            passwordCorrect = false;
                            //show MrzsInOutOptions
                            if (tempDisplayedEntity.menuElement == null)
                            {
                                InputingModeStates = InputingMode.ChoosingByEnterAndShowNextEntity;
                                showMrzsInOutOptions(ref getAnotherVal);
                                
                            }
                            else if(tempDisplayedEntity.menuElement.IndexOf("{value}")!=-1)
                            {
                                //if menuElement have {value}: Тип МТЗ//{value}
                                //where {value} choosed by Enter
                                forValueInMenuElement();
                                //for turn off down\up button
                                InputingModeStates = InputingMode.InputingNum;
                            }                                                        
                            break;
                        case PasswordStates.allowedEnterValue:
                            //temporary memorise textbox text
                            tempDisplayedText = display.Text;
                            //ask to memorise or not the inputed value
                            display.Text = allowedInputedValue();
                            PassStates = PasswordStates.MemoriseInputedVal;
                            break;
                        case PasswordStates.MemoriseInputedVal:
                            //show temporary memorised text
                            display.Text = tempDisplayedText;
                            if (InputingModeStates == InputingMode.ChoosingByEnter)
                            {
                                //memorising choosed by Enter value                                
                                if (tempDisplayedEntity.BooleanValID != null)
                                {
                                    foreach (BooleanVal val in BooleanValList)
                                    {
                                        if (display.Text.Contains(val.val)) tempDisplayedEntity.BooleanValID = val.id;                                                                                                                          
                                    }
                                }
                                else if (tempDisplayedEntity.mtzValID != null)
                                {
                                    foreach (mtzVal val in mtzValList)
                                    {
                                        if (display.Text.Contains(val.mtzVals)) tempDisplayedEntity.mtzValID = val.id;
                                    }
                                }
                                //and all tables                                
                                mrzs05mMContxt.SubmitChanges();
                                //showing memorised
                                display.Text = replaceValueInColumn(tempDisplayedEntity);
                            }
                            else if (InputingModeStates == InputingMode.ChoosingByEnterAndShowNextEntity)
                            {
                                //memorising choosed by Enter value                                
                                mrzs05mMContxt.SubmitChanges();
                                getAnotherVal = false;
                                DisplayMenu_MrzsInOutOpt(tempDisplayedEntities);
                                InputingModeStates = InputingMode.none;
                            }
                            else
                            {
                                //memorise inputed value (temporary memorised text)
                                tempDisplayedEntity.value = Inputing.getNumIndexes(display.Text);
                                mrzs05mMContxt.SubmitChanges();
                            }

                            //if (!mrzs05mMContxt.IsSubmitting)
                            //{
                            //    if (mrzs05mMContxt.HasChanges) mrzs05mMContxt.SubmitChanges();
                            //}
                            PassStates = PasswordStates.passwordAsk;
                            break;                        
                    }                    
                }
            }           
            
            
        }
        //show entity where mrzsInOutOption and BooleanValID columns not null
        private void showMrzsInOutOptions()
        {
            string boolVal = null;            
            boolVal = BooleanValList.Where(n => n.id == tempDisplayedEntity.BooleanValID).Single().val;                        
            //show one tempentity
            display.Text = "";
            display.Text += tempDisplayedEntity.mrzsInOutOption.optionsName + Environment.NewLine;
            display.Text += boolVal;
            //select show value                                
            display.Focus();
            display.SelectionStart = display.Text.IndexOf(boolVal);
            display.SelectionLength = boolVal.Length;
        }
        private void showMrzsInOutOptions(ref bool getAnother)
        {
            string boolVal = null;
            if (getAnother == false)
            {
                boolVal = BooleanValList.Where(n => n.id == tempDisplayedEntity.BooleanValID).Single().val;
                getAnother = true;
            }
            else
            {
                boolVal = BooleanValList.Where(n => n.id != tempDisplayedEntity.BooleanValID).Single().val;
                tempDisplayedEntity.BooleanValID = BooleanValList.Where(n => n.id != tempDisplayedEntity.BooleanValID).Single().id;                
            }
            //show one tempentity
            display.Text = "";
            display.Text += tempDisplayedEntity.mrzsInOutOption.optionsName + Environment.NewLine;
            display.Text += boolVal;

            //select show value                                
            display.Focus();
            display.SelectionStart = display.Text.IndexOf(boolVal);
            display.SelectionLength = boolVal.Length;
        }
        private void forValueInMenuElement()
        {
            display.Text = replaceValueInColumn(tempDisplayedEntity);
            //if displayed entity not have "0002.0000" num
            if (Inputing.getNumIndexes(display.Text) == "")
            {
                //for turn off down\up button
                InputingModeStates = InputingMode.ChoosingByEnter;
                //for first displaying
                if (getAnother == false)
                {
                    getAnother = true;
                    string tempVal = getValueFromNotNullColumn(tempDisplayedEntity);
                    display.Focus();
                    display.SelectionStart = display.Text.IndexOf(tempVal);
                    display.SelectionLength = tempVal.Length;
                }
                else
                {
                    //display another value, because enter clicked
                    getAnother = false;
                    string rez = null;
                    if (tempDisplayedEntity.BooleanValID != null)
                        rez = BooleanValList.Where(n => n.id != tempDisplayedEntity.BooleanVal.id).Single().val;
                    else if (tempDisplayedEntity.mtzValID != null) rez = mtzValList.Where(n => n.id != tempDisplayedEntity.mtzVal.id).Single().mtzVals;
                    else if (tempDisplayedEntity.kindSignalDCid != null) rez = kindSignalDCList.Where(n => n.id != tempDisplayedEntity.kindSignalDCid).Single().kindSignal;
                    //else if (tempDisplayedEntity.typeSignalDCid != null) rez = typ
                    //else if (entity.typeFuncDCid != null) rez = entity.typeFuncDC.typeFunction;
                    //else if (entity.BooleanVal2ID != null) rez = entity.BooleanVal2.val;
                    //else if (entity.BooleanVal3ID != null) rez = entity.BooleanVal3.boolVal;
                    //else if (entity.mtzValID != null) rez = entity.mtzVal.mtzVals;
                    display.Text = replaceVelueInColumnOnStr(tempDisplayedEntity.menuElement, rez);
                    display.Focus();
                    display.SelectionStart = display.Text.IndexOf(rez);
                    display.SelectionLength = rez.Length;
                }
            }
            else
            {
                //in next case show EscEnter dialog if would be pressed Enter
                PassStates = PasswordStates.allowedEnterValue;
                numIndexesList = Inputing.getIndexes(display.Text);
                currInputPosition = numIndexesList[0];
                selectOneNumInTextBox(display, currInputPosition);
            }
        }
        //select one number in textbox for inputing process
        private void selectOneNumInTextBox(TextBox tb,int position)
        {
            tb.Focus();
            tb.SelectionStart = position;
            tb.SelectionLength = 1;
        }

        void Dtimer_Tick2(object sender, EventArgs e)
        {
            display.Focus();
            if(tempSymbol==null)tempSymbol = display.Text[currInputPosition].ToString();
            //switch cursor text
            if (!displayAnimFlag)
            {
                
                display.Text = display.Text.Remove(currInputPosition, 1);
                display.Text = display.Text.Insert(currInputPosition, "_");
                displayAnimFlag = true;
            }
            else
            {
                display.Text = display.Text.Remove(currInputPosition, 1);
                display.Text = display.Text.Insert(currInputPosition, tempSymbol);
                tempSymbol = null;
                displayAnimFlag = false;
            }
            //run animation of cursor again
        }
        private void insertSymbol(TextBox tb,ref string symbol,ref int position,ref bool animationFlag)
        {
            tb.Text = tb.Text.Remove(position, 1);
            display.Text = display.Text.Insert(position, symbol);
            symbol = null;
            animationFlag = false;
        }
        
        private void escButton_Click(object sender, RoutedEventArgs e)
        {
            //canseled inputing password
            if (PassStates == PasswordStates.inputingPasswordStart)
            {
                display.Text = "Пароль введен" + Environment.NewLine + "  " + "неверно";
                PassStates = PasswordStates.onlyView;                
                return;
            }//second canseled for back to view menu
            else if ((PassStates == PasswordStates.MemoriseInputedVal || PassStates == PasswordStates.onlyView)
                && tempDisplayedEntity != null)
            {
                if (InputingModeStates == InputingMode.ChoosingByEnterAndShowNextEntity)
                {
                    mrzs05mMContxt.RejectChanges();
                    InputingModeStates = InputingMode.none;
                    getAnotherVal = false;
                    //display MrzsInOutOptions
                    DisplayMenu_MrzsInOutOpt(tempDisplayedEntities);
                }
                else display.Text = replaceValueInColumn(tempDisplayedEntity);                     
                PassStates = PasswordStates.passwordAsk;
                return;
            }
            if (PassStates == PasswordStates.passwordCorrect)
            {
                //when user choosing values in menu by clicked Enter and then press Esc
                PassStates = PasswordStates.MemoriseInputedVal;
                //temporary memorise textbox text
                tempDisplayedText = display.Text;
                display.Text = allowedInputedValue();
                return;
            }
            //clear temp displayed entities
            if(tempDisplayedEntities.Count>0) tempDisplayedEntities.RemoveRange(0, tempDisplayedEntities.Count);
            tempDisplayedEntity = null;
            //get parentID of last selected elem in menu
            if (SelectMenuElemHistory.Count == 0) return; //!возможна DisplayedEntities будет содержать елем
            int? parentId = SelectMenuElemHistory.Last().parentID;
            //delete displayed entities                        
            while (true)
            {
                if (DisplayedEntities.Last().parentID == SelectMenuElemHistory.Last().id)
                    DisplayedEntities.Remove(DisplayedEntities.Last());
                else break;
            }
            List<string> list = getEntitiesByParentID(parentId).Select(n => n.menuElement).ToList();
            //set display first line on last selected word and set animation cursor
            DisplayMenu(list, SelectMenuElemHistory.Last().menuElement);
            //delete selected entity
            SelectMenuElemHistory.Remove(SelectMenuElemHistory.Last());
            IsAnimCursorInserted = true;            
        }

        void SelectMenuElemHistory_OnDelete(object sender, EventArgs e)
        {
            
        }
        //Adding new item
        void SelectMenuElemHistory_OnAdd(object sender, EventArgs e)
        {
            

        }
        private void DeviceON_button_Click(object sender, RoutedEventArgs e)
        {
            //set to DeviceOff_button default style
            DeviceOff_button.Background = new SolidColorBrush(Colors.Gray);
            DeviceOff_button.BorderBrush = new SolidColorBrush(Colors.Gray);
            DeviceOff_button.BorderThickness = new Thickness(1);
            //set another style for DeviceON_button
            DeviceON_button.Background = new SolidColorBrush(Colors.Green);            
            DeviceON_button.BorderBrush = new SolidColorBrush(Colors.Green);            
            DeviceON_button.BorderThickness = new Thickness(3);
            //defences
            CheckDefence_MTZ1();
            CheckDefence_MTZ2();
        }
        private void CheckDefence_MTZ1()
        {
            //get МТЗ->Уставки->Уставка МТЗ1
            string rez= mrzs05Entity.Where(n=>n.menuElement=="Уставка МТЗ1\\{value}").Single().value;            
            double val = Double.Parse(rez, CultureInfo.InvariantCulture);
            //get МТЗ-Управление-1 Ступень МТЗ-ВКЛ
            string stupen1MTZ=mrzs05Entity.Where(n=>n.menuElement=="1 Ступень МТЗ\\{value}").Single().BooleanVal3.boolVal;

            bool flag = IsInDVArrayValue("Блок МТЗ 1");
            if ((Ia.Value > val || Ib.Value > val || Ic.Value > val)
                && (stupen1MTZ.IndexOf("ВКЛ")!=-1)
                && !IsInDVArrayValue("Блок МТЗ 1"))//ДВ не включены; если вкл, то на них нет ф-ции "Блок МТЗ 1"
            {
                //turn on/off rele
                CheckR("Сраб МТЗ 1", true);
                //
                CheckSDI("Сраб МТЗ 1", true);
                CheckDefence_APV(1);
                //засветить реле 1,4,5
                //r1.turnOn = false;
                //r4.turnOn = false;
                //r5.turnOn = false;
                //засветить лампочки sdi 1,2,4
                //sdi1.IsChecked = true;
                //sdi2.IsChecked = true;
                //sdi4.IsChecked = true;
            }
        }
        private void CheckDefence_MTZ2()
        {
            //если 1й DVI не вкл, то: если вкл реле 1,4,5 - то выкл их
        }
        private void CheckDefence_ZZ()
        {

        }
        //check all DV on true or false
        private bool IsInDVArrayValue(string functionName)
        {
            List<InputDV> list = new List<InputDV>(6);
            list.Add(dv1);
            list.Add(dv2);
            list.Add(dv3);
            list.Add(dv4);
            list.Add(dv5);
            list.Add(dv6);
            List<string> functions = null;

            foreach (InputDV dv in list)
            {
                //if (dv.IsChecked == false) continue;
                if(dv.IsChecked==true)
                {
                    //check what "MrzsInOutOption" functions are turn on for current DV
                    //functions= getTurnONFunctions(Inputs.ДВ01, mrzs05Entity);
                    //if (functions.Contains(functionName)) return true;

                    if (list.IndexOf(dv) == 0)
                    {
                        functions = getTurnONFunctions(Inputs.ДВ01, mrzs05Entity);
                        if (functions.Contains(functionName)) return true;
                    }
                    else if (list.IndexOf(dv) == 1)
                    {
                        functions = getTurnONFunctions(Inputs.ДВ02, mrzs05Entity);
                        if (functions.Contains(functionName)) return true;
                    }
                    else if (list.IndexOf(dv) == 2)
                    {
                        functions = getTurnONFunctions(Inputs.ДВ03, mrzs05Entity);
                        if (functions.Contains(functionName)) return true;
                    }
                    else if (list.IndexOf(dv) == 3)
                    {
                        functions = getTurnONFunctions(Inputs.ДВ04, mrzs05Entity);
                        if (functions.Contains(functionName)) return true;
                    }
                    else if (list.IndexOf(dv) == 4)
                    {
                        functions = getTurnONFunctions(Inputs.ДВ05, mrzs05Entity);
                        if (functions.Contains(functionName)) return true;
                    }
                    else if (list.IndexOf(dv) == 5)
                    {
                        functions = getTurnONFunctions(Inputs.ДВ06, mrzs05Entity);
                        if (functions.Contains(functionName)) return true;
                    }
                }                
            }
            return false;
            //if (dv1.IsChecked == true
            //    || dv2.IsChecked == true
            //    || dv3.IsChecked == true
            //    || dv4.IsChecked == true
            //    || dv5.IsChecked == true
            //    || dv6.IsChecked == true) return true;
            //else return false;
        }
        private void CheckR(string functionName,bool turnOff)
        {
            List<CheckBox> listCheckbox = new List<CheckBox>(5);
            listCheckbox.Add(r1c);
            listCheckbox.Add(r2c);
            listCheckbox.Add(r3c);
            listCheckbox.Add(r4c);
            listCheckbox.Add(r5c);
            List<string> functions=null;

            foreach (CheckBox cb in listCheckbox)
            {
                if (listCheckbox.IndexOf(cb) == 0)
                {
                    functions = getTurnONFunctions(Inputs.P01, mrzs05Entity);
                    if (functions.Contains(functionName)) r1c.IsChecked = turnOff;
                    r1c_Click_1(null, null);
                }
                else if (listCheckbox.IndexOf(cb) == 1)
                {
                    functions = getTurnONFunctions(Inputs.P02, mrzs05Entity);
                    if (functions.Contains(functionName)) r2c.IsChecked = turnOff;
                    r2c_Click_1(null, null);
                }
                else if (listCheckbox.IndexOf(cb) == 2)
                {
                    functions = getTurnONFunctions(Inputs.P03, mrzs05Entity);
                    if (functions.Contains(functionName)) r3c.IsChecked = turnOff;
                    r3c_Click_1(null, null);
                }
                else if (listCheckbox.IndexOf(cb) == 3)
                {
                    functions = getTurnONFunctions(Inputs.P04, mrzs05Entity);
                    if (functions.Contains(functionName)) r4c.IsChecked = turnOff;
                    r4c_Click_1(null, null);
                }
                else if (listCheckbox.IndexOf(cb) == 4)
                {
                    functions = getTurnONFunctions(Inputs.P05, mrzs05Entity);
                    if (functions.Contains(functionName)) r5c.IsChecked = turnOff;
                    r5c_Click_1(null, null);
                }
            }
        }
        private void CheckSDI(string functionName, bool turnOn)
        {
            List<CheckBox> listCheckBox = new List<CheckBox>(6);
            listCheckBox.Add(sdi1);
            listCheckBox.Add(sdi2);
            listCheckBox.Add(sdi3);
            listCheckBox.Add(sdi4);
            listCheckBox.Add(sdi5);
            listCheckBox.Add(sdi6);
            List<string> functions = null;
            foreach(CheckBox cb in listCheckBox)
            {
                if((cb.IsChecked==true&&turnOn) || (cb.IsChecked==false&& !turnOn)) continue;

                if (listCheckBox.IndexOf(cb) == 0)
                {
                    functions = getTurnONFunctions(Inputs.СДИ1, mrzs05Entity);
                    if (functions.Contains(functionName)) sdi1.IsChecked = turnOn;
                }
                else if (listCheckBox.IndexOf(cb) == 1)
                {
                    functions = getTurnONFunctions(Inputs.СДИ2, mrzs05Entity);
                    if (functions.Contains(functionName)) sdi2.IsChecked = turnOn;
                }
                else if (listCheckBox.IndexOf(cb) == 2)
                {
                    functions = getTurnONFunctions(Inputs.СДИ3, mrzs05Entity);
                    if (functions.Contains(functionName)) sdi3.IsChecked = turnOn;
                }
                else if (listCheckBox.IndexOf(cb) == 3)
                {
                    functions = getTurnONFunctions(Inputs.СДИ4, mrzs05Entity);
                    if (functions.Contains(functionName)) sdi4.IsChecked = turnOn;
                }
                else if (listCheckBox.IndexOf(cb) == 4)
                {
                    functions = getTurnONFunctions(Inputs.СДИ5, mrzs05Entity);
                    if (functions.Contains(functionName)) sdi5.IsChecked = turnOn;
                }
                else if (listCheckBox.IndexOf(cb) == 5)
                {
                    functions = getTurnONFunctions(Inputs.СДИ6, mrzs05Entity);
                    if (functions.Contains(functionName)) sdi6.IsChecked = turnOn;
                }
            }
        }
        private void CheckDefence_APV(int MTZnumber)
        {
            int? puskOtMTZ1= getMrzs05mMenuByMenuElement(mrzs05Entity,"Пуск от МТЗ1").Single().BooleanVal3ID;
            int? puskOtMTZ2 = getMrzs05mMenuByMenuElement(mrzs05Entity, "Пуск от МТЗ2").Single().BooleanVal3ID;
            //get МТЗ->Уставки->Уставка МТЗ1
            string rez= mrzs05Entity.Where(n=>n.menuElement=="Уставка МТЗ1\\{value}").Single().value;            
            double UstavkaMTZ1 = Double.Parse(rez, CultureInfo.InvariantCulture);
            string stupen1MTZ=mrzs05Entity.Where(n=>n.menuElement=="1 Ступень МТЗ\\{value}").Single().BooleanVal3.boolVal;
            string rez2 = mrzs05Entity.Where(n => n.menuElement == "Уставка МТЗ2\\{value}").Single().value;
            double UstavkaMTZ2 = Double.Parse(rez2, CultureInfo.InvariantCulture);
            string stupen2MTZ = mrzs05Entity.Where(n => n.menuElement == "2 Ступень МТЗ\\{value}").Single().BooleanVal3.boolVal;

            if (((puskOtMTZ1 == 1 && MTZnumber == 1) || (puskOtMTZ2 == 1 && MTZnumber == 2))
                && (!IsInDVArrayValue("АЧР/ЧАПВ")))
            {
                if ((Ia.Value > UstavkaMTZ1 || Ib.Value > UstavkaMTZ1 || Ic.Value > UstavkaMTZ1) == false && (stupen2MTZ.IndexOf("ВКЛ") != -1))
                {
                    CheckR("Сраб МТЗ 1", false);
                }

                if ((Ia.Value > UstavkaMTZ2 || Ib.Value > UstavkaMTZ2 || Ic.Value > UstavkaMTZ2) == false && (stupen1MTZ.IndexOf("ВКЛ") != -1))
                {
                    CheckR("Сраб МТЗ 2", false);
                }
                CheckSDI("Сраб АПВ", true);
            }               
            
        }
        private void DeviceOff_button_Click(object sender, RoutedEventArgs e)
        {
            //set to DeviceON_button default style
            DeviceON_button.Background = new SolidColorBrush(Colors.Gray);
            DeviceON_button.BorderBrush = new SolidColorBrush(Colors.Gray);
            DeviceON_button.BorderThickness = new Thickness(1);
            //set another style for DeviceOff_button         
            DeviceOff_button.Background = new SolidColorBrush(Colors.Red);
            DeviceOff_button.BorderBrush = new SolidColorBrush(Colors.Red);
            DeviceOff_button.BorderThickness = new Thickness(3);

            //turn off all indicators
            dv1.IsChecked = false;
            dv2.IsChecked = false;
            dv3.IsChecked = false;
            dv4.IsChecked = false;
            dv5.IsChecked = false;
            dv6.IsChecked = false;

            sdi1.IsChecked = false;
            sdi2.IsChecked = false;
            sdi3.IsChecked = false;
            sdi4.IsChecked = false;
            sdi5.IsChecked = false;
            sdi6.IsChecked = false;

            r1c.IsChecked = false;
            r1c_Click_1(null, null);
            r2c.IsChecked = false;
            r2c_Click_1(null, null);
            r3c.IsChecked = false;
            r3c_Click_1(null, null);
            r4c.IsChecked = false;
            r4c_Click_1(null, null);
            r5c.IsChecked = false;
            r5c_Click_1(null, null);
        }
        #endregion==

        #region ====Addition Functions=======================================

        private bool isLastElem(int? ElemId)
        {
            List<mrzs05mMenu> newMenuLevel = getEntitiesByParentID(ElemId);
            if (newMenuLevel.Count > 0) return false;
            else return true;
        }

        private void clearTextBox(TextBox text)
        {
            if (text.Text != String.Empty) text.ClearValue(TextBox.TextProperty);
        }        
        #endregion=======================
        //get menuElement list for displeing in menu
        private List<string> getMenuListByParentID(int? parenID)
        {
            return mrzs05Entity.Where(n => n.parentID == parenID).Select(n => n.menuElement).ToList();            
        }
        private List<mrzs05mMenu> getEntitiesByParentID(int? parentID)
        {
            return (from t in mrzs05Entity where t.parentID == parentID select t).ToList();
        }       
        
        #region DisplayMenu functions===================================
        private void DisplayMenu(List<string> s)
        {
            if(display.Text!= String.Empty) display.ClearValue(TextBox.TextProperty);            
            foreach (string str in s)
            {
                if (str == null) return;//заглушка
                if (s.Last().ToString() == str) display.Text += str;
                else display.Text += str + Environment.NewLine;
            }
        }
        private void DisplayMenu(List<string> s, string firstDisplayedWord)
        {
            DisplayMenu(s);
            AnimationCursorLineCurntPositionIndex = display.Text.IndexOf(firstDisplayedWord);
            display.Text = insertAnimatCursor(display.Text, AnimationCursorLineCurntPositionIndex, ">");
            display.SelectionStart = AnimationCursorLineCurntPositionIndex;
        }
        private string DisplayEntity(mrzs05mMenu entity)
        {
            //clear textbox
            if (display.Text != String.Empty) display.ClearValue(TextBox.TextProperty);
            //display menuElement column with replaced {value}
            return replaceValueInColumn(entity);
        }
        
        //replace {value} in menuElement column
        private string replaceValueInColumn(mrzs05mMenu entity)
        {
            string strToDisplay = null;
            //search in menuElement column text with {value}
            if (entity.menuElement.IndexOf("{value}") >= 0)
            {
                //replace {value} on value of "value" and "unitValue" columns
                string[] temp = entity.menuElement.Split(new string[1] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                if (temp.Count() >= 2)
                {
                    //get value from one of column mrzs05menu if it not null
                    temp[1] = getValueFromNotNullColumn(entity);
                    strToDisplay = temp[0] + Environment.NewLine + temp[1];
                }                
            }
            return strToDisplay;
        }
        //replace {value} on insertedValue
        private string replaceVelueInColumnOnStr(string strWithValueForReplace, string insertedValue)
        {
            string strToDisplay = null;
            //search in menuElement column text with {value}
            if (strWithValueForReplace.IndexOf("{value}") >= 0)
            {
                //replace {value} on value of "value" and "unitValue" columns
                string[] temp = strWithValueForReplace.Split(new string[1] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                if (temp.Count() >= 2)
                {
                    //get value from one of column mrzs05menu if it not null
                    temp[1] = insertedValue;
                    strToDisplay = temp[0] + Environment.NewLine + temp[1];
                }
            }
            return strToDisplay;
        }
        //return value from one of column mrzs05menu if it not null
        private string getValueFromNotNullColumn(mrzs05mMenu entity)
        {
            string rez = null;
            if (entity.value != null && entity.unitValue != null) rez = entity.value + " " + entity.unitValue;
            else if (entity.BooleanValID != null) rez = entity.BooleanVal.val;
            else if (entity.kindSignalDCid != null) rez = entity.kindSignalDC.kindSignal;
            else if (entity.typeSignalDCid != null) rez = entity.typeSignalDC.typeSignal;
            else if (entity.typeFuncDCid != null) rez = entity.typeFuncDC.typeFunction;
            else if (entity.BooleanVal2ID != null) rez = entity.BooleanVal2.val;
            else if (entity.BooleanVal3ID != null) rez = entity.BooleanVal3.boolVal;
            else if (entity.mtzValID != null) rez = entity.mtzVal.mtzVals;
            return rez;
        }
        private int? getIdFromNotNullColumn(mrzs05mMenu entity)
        {
            int? rez = null;
            if (entity.BooleanValID != null) rez = entity.BooleanVal.id;
            else if (entity.kindSignalDCid != null) rez = entity.kindSignalDC.id;
            else if (entity.typeSignalDCid != null) rez = entity.typeSignalDC.id;
            else if (entity.typeFuncDCid != null) rez = entity.typeFuncDC.@int;
            else if (entity.BooleanVal2ID != null) rez = entity.BooleanVal2.id;
            else if (entity.BooleanVal3ID != null) rez = entity.BooleanVal3.id;
            else if (entity.mtzValID != null) rez = entity.mtzVal.id;
            return rez;
        }
        private void DisplayMenu_MrzsInOutOpt(List<mrzs05mMenu> entityList)
        {            
            if (display.Text != String.Empty) display.ClearValue(TextBox.TextProperty);
            foreach (mrzs05mMenu entity in entityList)
            {
                if (entity == entityList.Last()) display.Text += entity.mrzsInOutOption.optionsName;
                else display.Text += entity.mrzsInOutOption.optionsName + Environment.NewLine;

                

                //if (entity.BooleanVal.val != String.Empty) displayStr(entity.BooleanVal.val);
                //else if (entity.kindSignalDC.kindSignal != String.Empty) displayStr(entity.kindSignalDC.kindSignal);
                //else if (entity.typeSignalDC.typeSignal != String.Empty) displayStr(entity.typeSignalDC.typeSignal);
                //else if (entity.typeFuncDC.typeFunction != String.Empty) displayStr(entity.typeFuncDC.typeFunction);
                //else if (entity.BooleanVal2.val != String.Empty) displayStr(entity.BooleanVal2.val);
                //else if (entity.BooleanVal3.boolVal != String.Empty) displayStr(entity.BooleanVal3.boolVal);
                //else if (entity.mtzVal.mtzVals != String.Empty) displayStr(entity.mtzVal.mtzVals);
            }
        }
        private void displayStr(string str)
        {
            display.Text += str+Environment.NewLine;
        }
        #endregion ========================================================       

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }        

        private void slider1_MouseLeave(object sender, MouseEventArgs e)
        {
            popUp.IsOpen = false;
        }

        private void f_MouseEnter(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }
        
        /// <summary>
        /// Show popup under current numericupdown
        /// </summary>
        /// <param name="sender"></param>
        private void ShowPopUp(object sender)
        {            
            if (numUpDown1 != null)
            {                      
                System.Windows.Data.Binding bind = new System.Windows.Data.Binding("Value") ;
                bind.Mode = System.Windows.Data.BindingMode.TwoWay;
                bind.Source = numUpDown1;
                slider1.Maximum = numUpDown1.Maximum;
                slider1.SetBinding(Slider.ValueProperty, bind);

                System.Windows.Data.Binding bind2 = new System.Windows.Data.Binding("Value");
                bind2.Mode = System.Windows.Data.BindingMode.TwoWay;
                bind2.Source = stepNumericUpDown;
                numUpDown1.SetBinding(NumericUpDown.IncrementProperty, bind2);

                System.Windows.Data.Binding bind3 = new System.Windows.Data.Binding("Value");
                bind3.Mode = System.Windows.Data.BindingMode.TwoWay;
                bind3.Source = stepNumericUpDown;
                slider1.SetBinding(Slider.SmallChangeProperty, bind3);                

                int row = (int)numUpDown1.GetValue(Grid.RowProperty);
                int col = (int)numUpDown1.GetValue(Grid.ColumnProperty);
                popUp.SetValue(Grid.RowProperty, row + 1);
                popUp.SetValue(Grid.ColumnProperty, col);
                popUp.IsOpen = true;
            }
        }                       

        private void ShowPopUpLittle(object sender)
        {
            if (!popUp.IsOpen)
            {
                numUpDown1 = sender as NumericUpDown;
                if (numUpDown1 != null)
                {
                    int row = (int)numUpDown1.GetValue(Grid.RowProperty);
                    int col = (int)numUpDown1.GetValue(Grid.ColumnProperty);
                    popUpLittle.SetValue(Grid.RowProperty, row);
                    popUpLittle.SetValue(Grid.ColumnProperty, col + 1);
                    popUpLittle.IsOpen = true;
                    
                }
            }
        }

        #region PopUp events
        private void popUp_MouseLeave(object sender, MouseEventArgs e)
        {
            popUp.IsOpen = false;
        }

        private void closePopUp_MouseEnter(object sender, MouseEventArgs e)
        {
            popUp.IsOpen = false;
        }

        private void closePopUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            popUp.IsOpen = false;
        }

        private void f_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void closePopUp_Click(object sender, RoutedEventArgs e)
        {
            popUp.IsOpen = false;
        }

        

        private void popUpLittle_MouseEnter(object sender, MouseEventArgs e)
        {
            //popUpLittle.IsOpen = true;
        }

        private void popUpButton_Click(object sender, RoutedEventArgs e)
        {
            popUpLittle.IsOpen = false;
            ShowPopUp(sender);
        }

        private void Border_MouseLeave_1(object sender, MouseEventArgs e)
        {
            popUpLittle.IsOpen = false;
        }

        private void Border_MouseLeave_2(object sender, MouseEventArgs e)
        {
            popUpLittle.IsOpen = false;
        }
        #endregion                

        

        /// <summary>
        /// get selected menu word
        /// </summary>
        /// <returns></returns>
        private string GetSelectedWordMenu()
        {
            int index = display.SelectionStart;
            //int lengt = display.Text.Length;//for debug
            if (index > display.Text.Length - 1) index = display.Text.Length - 1;
            //char a = display.Text[index];//for debug
            //Мен|ю\r\n                                                            
            int startWordIndex = index;
            //search "\n" in left direction
            while (display.Text[startWordIndex] != '\n' && startWordIndex != 0)
            {
                startWordIndex -= 1;
            }
            if (display.Text[startWordIndex] == '\n') startWordIndex += 1;
            //search "\r" in right direction
            int endWordIndex = index;
            //a = display.Text[index];//for debug
            while (display.Text[endWordIndex] != '\r' && (endWordIndex != display.Text.Length - 1))
            {
                endWordIndex += 1;
                //a = display.Text[endWordIndex];//for debug
            }
            int leng = endWordIndex - startWordIndex;
            if (endWordIndex == display.Text.Length - 1) leng += 1;
            //check word to animation cursor
            string temp=display.Text.Substring(startWordIndex, leng);
            string word=null;
            for(int i=0;i<temp.Length; i++)
            {
                if(temp[i]!='>' && temp[i]!='_') word+=temp[i].ToString();
            }
            //return selected word
            return word;
        }

        //events

        #region NumericUpDown events
        private void NumericUpDown_MouseEnter_1(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }
        private void NumericUpDown_MouseEnter_2(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }
        private void NumericUpDown_MouseEnter_15(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void NumericUpDown_MouseEnter_16(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void NumericUpDown_MouseEnter_17(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }
        private void NumericUpDown_MouseEnter_3(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void NumericUpDown_MouseEnter_4(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void NumericUpDown_MouseEnter_5(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void NumericUpDown_MouseEnter_6(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void NumericUpDown_MouseEnter_7(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void NumericUpDown_MouseEnter_8(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void NumericUpDown_MouseEnter_9(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void NumericUpDown_MouseEnter_10(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void NumericUpDown_MouseEnter_11(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void NumericUpDown_MouseEnter_12(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void NumericUpDown_MouseEnter_13(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }

        private void NumericUpDown_MouseEnter_14(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);
        }
        #endregion
                                          
        #region Inputing symbols=======================
        private void inputing(TextBox tb)
        {
            inputedSymbol = null;
            string tempText = tb.Text;
            clearTextBox(tb);
            passwordAskDisplay(tb);
        }
        private void passwordAskDisplay(TextBox tb)
        {
            tb.Text = "Введите пароль" + Environment.NewLine;            
            PassStates = PasswordStates.inputingPasswordStart;
        }
        private void passwordAnswerDisplay(TextBox tb)
        {
            tb.Text = "Пароль введен" + Environment.NewLine;
            tb.Text += "  верно";
        }
        private string allowedInputedValue()
        {
            return " Вы уверены?"+Environment.NewLine + "Enter-ДА, Esc-НЕТ";
        }
        private void passwordInputsDisplay(TextBox tb,string inputSymbol)
        {
            tb.Text += inputSymbol;
        }

        private void passwordCheck()
        {
            if (inputedSymbol == "1111")
            {
                passwordCorrect = true;
                PassStates = PasswordStates.passwordCorrect;
                inputedSymbol = null;
            }
            else
            {
                inputedSymbol = null;
                passwordCorrect = false;
                PassStates = PasswordStates.passwordInCorrect;
            }
        }
        #endregion====================================

        #region Numeric buttons events =============================================
        private void button1_Click(object sender, RoutedEventArgs e)
        {            
            //for inputing password
            if (PassStates == PasswordStates.inputingPasswordStart)
            {
                inputedSymbol += "1";
                passwordInputsDisplay(display, "1");
            }
            else
            {//for inputing num                
                display.Text = replaceSymbol(display.Text, currInputPosition, "1");
                selectOneNumInTextBox(display, currInputPosition);
            }
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (PassStates != PasswordStates.inputingPasswordStart && currInputPosition != -1)
            {
                display.Text = replaceSymbol(display.Text, currInputPosition, "2");
                selectOneNumInTextBox(display, currInputPosition);
            }
        }
        //replace symbol in textbox on new inputed symbol
        private string replaceSymbol(string textBoxStr, int position, string symbol)
        {
            textBoxStr = textBoxStr.Remove(position, 1);
            return textBoxStr.Insert(position, symbol);             
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (PassStates != PasswordStates.inputingPasswordStart && currInputPosition != -1)
            {
                display.Text = replaceSymbol(display.Text, currInputPosition, "3");
                selectOneNumInTextBox(display, currInputPosition);
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (PassStates != PasswordStates.inputingPasswordStart && currInputPosition != -1)
            {
                display.Text = replaceSymbol(display.Text, currInputPosition, "4");
                selectOneNumInTextBox(display, currInputPosition);
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (PassStates != PasswordStates.inputingPasswordStart && currInputPosition != -1)
            {
                display.Text = replaceSymbol(display.Text, currInputPosition, "5");
                selectOneNumInTextBox(display, currInputPosition);
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (PassStates != PasswordStates.inputingPasswordStart && currInputPosition != -1)
            {
                display.Text = replaceSymbol(display.Text, currInputPosition, "6");
                selectOneNumInTextBox(display, currInputPosition);
            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            if (PassStates != PasswordStates.inputingPasswordStart && currInputPosition != -1)
            {
                display.Text = replaceSymbol(display.Text, currInputPosition, "7");
                selectOneNumInTextBox(display, currInputPosition);
            }
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            if (PassStates != PasswordStates.inputingPasswordStart && currInputPosition != -1)
            {
                display.Text = replaceSymbol(display.Text, currInputPosition, "8");
                selectOneNumInTextBox(display, currInputPosition);
            }
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            if (PassStates != PasswordStates.inputingPasswordStart && currInputPosition != -1)
            {
                display.Text = replaceSymbol(display.Text, currInputPosition, "9");
                selectOneNumInTextBox(display, currInputPosition);
            }
        }

        private void button0_Click(object sender, RoutedEventArgs e)
        {
            if (PassStates != PasswordStates.inputingPasswordStart && currInputPosition != -1)
            {
                display.Text = replaceSymbol(display.Text, currInputPosition, "0");
                selectOneNumInTextBox(display, currInputPosition);
            }
        }
        #endregion   ================================================================                                        

        //return inputsTurnONFunctionIDs
        private List<int?> getmrzsInOutOptionsID(Inputs input, IEnumerable<mrzs05mMenu> mrzs05Entitys)
        {
            List<mrzs05mMenu> list= mrzs05Entitys.Where(n => n.menuElement != null).Where(n => n.menuElement.Contains(input.ToString()) && n.passwordCheckType == null).ToList();
            int inputIDInMenu = mrzs05Entitys.Where(n => n.menuElement != null).Where(n => n.menuElement.Contains(input.ToString()) && n.passwordCheckType == null).Single().id;
            //int inputIDInMenu = mrzs05Entitys.Where(n => n.menuElement.Contains(input.ToString()) && n.passwordCheckType == null).Single().id;
            //return inputsTurnONFunctionIDs
            return getEntitiesByParentID(inputIDInMenu).Where(n => n.BooleanValID == 1).Select(n => n.mrzsInOutOptionsID).ToList();            
        }
        private List<string> getInputsTurnONFunctionsByID(List<int?> ids)
        {
            return mrzsInOutOptionList.Where(n => ids.Contains(n.id)).Select(n => n.optionsName).ToList();
        }
        private List<string> getTurnONFunctions(Inputs input, IEnumerable<mrzs05mMenu> mrzs05Entities)
        {
            List<int?> list= getmrzsInOutOptionsID(input, mrzs05Entities);
            return getInputsTurnONFunctionsByID(list);
        }
        private List<mrzs05mMenu> getMrzs05mMenuByMenuElement(IEnumerable<mrzs05mMenu> mrzs05Entities, string menuElement)
        {
            return mrzs05Entities.Where(n => n.menuElement != null).Where(n => n.menuElement.Contains(menuElement)).ToList();
        }


        private void r1c_Click_1(object sender, RoutedEventArgs e)
        {
            if (r1c.IsChecked == true) r1.turnOn = true;
            else if (r1c.IsChecked == false) r1.turnOn = false;
        }

        private void r2c_Click_1(object sender, RoutedEventArgs e)
        {
            if (r2c.IsChecked == true) r2.turnOn = true;
            else if (r1c.IsChecked == false) r2.turnOn = false;
        }

        private void r3c_Click_1(object sender, RoutedEventArgs e)
        {
            if (r3c.IsChecked == true) r3.turnOn = true;
            else if (r3c.IsChecked == false) r3.turnOn = false;
        }

        private void r4c_Click_1(object sender, RoutedEventArgs e)
        {
            if (r4c.IsChecked == true) r4.turnOn = true;
            else if (r4c.IsChecked == false) r4.turnOn = false;
        }

        private void r5c_Click_1(object sender, RoutedEventArgs e)
        {
            if (r5c.IsChecked == true) r5.turnOn = true;
            else if (r5c.IsChecked == false) r5.turnOn = false;
        }        
    }

    class MyList<T> : List<T>
    {

        public event EventHandler OnAdd;
        public event EventHandler OnDelete;

        public void Add(T item)
        {
            if (null != OnAdd) OnAdd(this, null);            
            base.Add(item);
        }
        public bool Remove(T item)
        {
            if (OnDelete != null) OnDelete(this, null);
            return base.Remove(item);
        }
    }    
}