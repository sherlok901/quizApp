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


namespace MRZS.Views.Emulator
{
    public partial class Emulator_05M : System.Windows.Controls.Page
    {
        //properties
        private BooleanVal1 boolContext;//for experiment
        public BooleanVal1 pboolContext
        {
            get{ return boolContext;}
            set { boolContext = value; }
        }//for experiment
  
        private int AnimationCursorLineCurntPositionIndex=0;
        List<int?> parentIDlist;
        private bool displayAnimFlag;
        private NumericUpDown numUpDown1;
        private IEnumerable<mrzs05mMenu> mrzs05Entity;        
        private List<mrzs05mMenu> SelectedMenuElemHistory = new List<mrzs05mMenu>(0);
        private List<mrzs05mMenu> DisplayedEntities = new List<mrzs05mMenu>(0);
        LoadOperation<mrzsInOutOption> mrzsInOutOptionModel;
        LoadOperation<passwordCheckType> passwordCheckTypeModel;
        LoadOperation<kindSignalDC> kindSignalDCModel;
        LoadOperation<typeSignalDC> typeSignalDCModel;
        LoadOperation<typeFuncDC> typeFuncDCModel;
        LoadOperation<BooleanVal2> BooleanVal2Model;
        LoadOperation<BooleanVal3> BooleanVal3Model;
        LoadOperation<mtzVal> mtzValModel;        
        DispatcherTimer Dtimer = new DispatcherTimer();

        public Emulator_05M()
        {
            InitializeComponent();
            display.TextWrapping = TextWrapping.Wrap;            
            display.FontFamily = new FontFamily("Arial");
            display.FontSize = 20.0;                        
            display.Padding = new Thickness(5.0);                          
            display.SelectionStart = display.Text.Length;                        

            //generated class by ria service (for client side)
            boolContext = new Web.Services.BooleanVal1();
            //using wcf service (DomainService) with my method to load entities
            LoadOperation<MRZS.Web.Models.BooleanVal> boolEntity = boolContext.Load(boolContext.GetBooleanValByIDQuery(1));                        
            
            myDataGrid.ItemsSource = boolEntity.Entities;
            IEnumerable<BooleanVal> list = boolEntity.Entities;            
            boolEntity.Completed += boolEntity_Completed;

            mrzsInOutOptionsContext mrzsInOutOptConxt = new mrzsInOutOptionsContext();
            LoadOperation<mrzsInOutOption> mrzsInOutOptModel = mrzsInOutOptConxt.Load(mrzsInOutOptConxt.GetMrzsInOutOptionsQuery());
            mrzsInOutOptModel.Completed += mrzsInOutOptModel_Completed;

            mrzs05mMenuContext mrzs05mMContxt = new mrzs05mMenuContext();
            LoadOperation<mrzs05mMenu> mrzs05mMModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetMrzs05mMenuQuery());            
            mrzs05mMModel.Completed += mrzs05mMModel_Completed;

            mrzsInOutOptionModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetMrzsInOutOptionsQuery());            

            passwordCheckTypeModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetPasswordCheckTypesQuery());
            kindSignalDCModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetKindSignalDCsQuery());
            typeSignalDCModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetTypeSignalDCsQuery());
            typeFuncDCModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetTypeFuncDCsQuery());
            BooleanVal2Model = mrzs05mMContxt.Load(mrzs05mMContxt.GetBooleanVal2Query());
            BooleanVal3Model = mrzs05mMContxt.Load(mrzs05mMContxt.GetBooleanVal3Query());
            mtzValModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetMtzValsQuery());
        }
        #region Entity complited loads ***
        /// <summary>
        /// igor: load parentID column
        /// </summary>        
        void mrzs05mMModel_Completed(object sender, EventArgs e)
        {
            //get list of different elem in column parentID            
            mrzs05Entity = getEntities(sender);
            if (mrzsInOutOptionModel.IsComplete)
            {
                List<mrzsInOutOption> m = mrzsInOutOptionModel.Entities.ToList();
            }
            if (mrzs05Entity != null)
            {
                parentIDlist = mrzs05Entity.Select(n => n.parentID).Distinct().ToList();
                //get 1 level of menu 
                DisplayedEntities.AddRange(getEntitiesByParentID(parentIDlist[0]));
                DisplayMenu(DisplayedEntities);
                //set cursor animation
                display.Text = insertAnimatCursor(display.Text, AnimationCursorLineCurntPositionIndex, ">");
                Dtimer.Interval = new TimeSpan(0, 0, 0, 0, 300);
                Dtimer.Tick += Dtimer_Tick;
                //Dtimer.Start();
            }
        }

        void mrzsInOutOptModel_Completed(object sender, EventArgs e)
        {

        }
        void boolEntity_Completed(object sender, EventArgs e)
        {
            System.ServiceModel.DomainServices.Client.LoadOperation<BooleanVal> b = sender as LoadOperation<BooleanVal>;
            if (b != null)
            {
                IEnumerable<BooleanVal> list = b.Entities;
                foreach (BooleanVal bv in list)
                {
                    int id = bv.id;
                    string val = bv.val;
                }
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
            if (AnimationCursorLineCurntPositionIndex != -1)
            {
                display.Focus();
                display.Text = display.Text.Remove(AnimationCursorLineCurntPositionIndex, 1);
                AnimationCursorLineCurntPositionIndex = EmulatorDisplayController.IndexOfnextFirstSymbolFinding(display.Text, AnimationCursorLineCurntPositionIndex);
                if (AnimationCursorLineCurntPositionIndex != -1)
                {
                    display.Text = display.Text.Insert(AnimationCursorLineCurntPositionIndex, ">");
                    display.SelectionStart = AnimationCursorLineCurntPositionIndex;
                }
            }
        }      
        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnimationCursorLineCurntPositionIndex != -1)
            {
                display.Focus();            
                display.Text = display.Text.Remove(AnimationCursorLineCurntPositionIndex, 1);
                AnimationCursorLineCurntPositionIndex = EmulatorDisplayController.getPreviousIndexOfStartLineDisplay(display.Text, AnimationCursorLineCurntPositionIndex);
                if (AnimationCursorLineCurntPositionIndex != -1)
                {
                    display.Text = display.Text.Insert(AnimationCursorLineCurntPositionIndex, ">");
                    display.SelectionStart = AnimationCursorLineCurntPositionIndex;
                }
            }
        }
        private void leftButton_Click(object sender, RoutedEventArgs e)
        {
            //debug:
            //display.Focus();            
            if (display.SelectionStart != 0)
            {
                char a = display.Text[display.SelectionStart];
                char b = display.Text[display.SelectionStart + 1];
                if (display.Text[display.SelectionStart - 1] == '\r') display.SelectionStart -= 2;
                else display.SelectionStart -= 1;
            }
        }
        private void rightButton_Click(object sender, RoutedEventArgs e)
        {
           
            //debug:
            //display.Focus();            
            if (display.SelectionStart != display.Text.Length - 1)
            {
                char a = display.Text[display.SelectionStart];
                char b = display.Text[display.SelectionStart + 1];
                if (display.Text[display.SelectionStart] == '\r') display.SelectionStart += 2;
                else display.SelectionStart += 1;
            }
        } 
        #endregion

        #region == other func buttons ==

        private void enterButton_Click_2(object sender, RoutedEventArgs e)
        {
            string selectedWordMenu = GetSelectedWordMenu();

            if (selectedWordMenu != null)
            {
                //get id selected word by menuElement
                mrzs05mMenu selectedEntity = DisplayedEntities.Last(n => n.menuElement == selectedWordMenu);
                if (getEntitiesByParentID(selectedEntity.id).Count == 0) return;
                //add current selected word and id to history list                                
                SelectedMenuElemHistory.Add(selectedEntity);
                //get next menu list by parentID of current selected word                
                List<mrzs05mMenu> newMenuLevel = getEntitiesByParentID(selectedEntity.id);
                //check the mrzsInOutOptionsID column
                if (newMenuLevel.Count > 0)
                {
                    //if mrzsInOutOptionsID column not null
                    if (newMenuLevel[0].menuElement == null && newMenuLevel[0].mrzsInOutOptionsID != null)
                    {
                        DisplayMenu_MrzsInOutOpt(newMenuLevel);
                    }
                    else
                    {
                        DisplayMenu(newMenuLevel);
                    }
                }


                DisplayedEntities.AddRange(newMenuLevel);
                //set animation cursor
                AnimationCursorLineCurntPositionIndex = 0;
                display.Text = insertAnimatCursor(display.Text, AnimationCursorLineCurntPositionIndex, ">");
            }
        }
        private void escButton_Click(object sender, RoutedEventArgs e)
        {
            //get parentID of last selected elem in menu
            if (SelectedMenuElemHistory.Count == 0) return; //!возможна DisplayedEntities будет содержать елем
            int? parentId = SelectedMenuElemHistory.Last().parentID;
            //delete displayed entities                        
            while (true)
            {
                if (DisplayedEntities.Last().parentID == SelectedMenuElemHistory.Last().id)
                    DisplayedEntities.Remove(DisplayedEntities.Last());
                else break;
            }
            List<string> list = getEntitiesByParentID(parentId).Select(n => n.menuElement).ToList();
            //set display first line on last selected word and set animation cursor
            DisplayMenu(list, SelectedMenuElemHistory.Last().menuElement);
            //delete selected entity
            SelectedMenuElemHistory.Remove(SelectedMenuElemHistory.Last());


        }

        #endregion==

        //get menuElement list for displeing in menu
        private List<string> getMenuListByParentID(int? parenID)
        {
            return mrzs05Entity.Where(n => n.parentID == parenID).Select(n => n.menuElement).ToList();            
        }
        private List<mrzs05mMenu> getEntitiesByParentID(int? parentID)
        {
            return (from t in mrzs05Entity where t.parentID == parentID select t).ToList();
        }
        /// <summary>
        /// Get mrzs05m entities by object sender
        /// </summary>
        /// <param name="eventSender"></param>
        /// <returns></returns>
        private IEnumerable<mrzs05mMenu> getEntities(object eventSender)
        {
            System.ServiceModel.DomainServices.Client.LoadOperation<mrzs05mMenu> b = eventSender as LoadOperation<mrzs05mMenu>;            
            if (b != null)
            {
                //больше 0
                if (b.Entities.Count(n => n != null) > 0)
                {
                    return b.Entities;
                }
            }
            return null;
        }
        
        #region DisplayMenu functions
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
        private void DisplayMenu(List<mrzs05mMenu> list)
        {
            //string strToDisplay = null;
            List<string> s = list.Select(n => n.menuElement).ToList();
            DisplayMenu(s);
            //foreach (mrzs05mMenu entity in list)            
            //{
            //    strToDisplay = null;
            //    //search in menuElement column text with {value}
            //    if (entity.menuElement.IndexOf("{value}") >= 0)
            //    {
            //        //replace {value} on value of "value" and "unitValue" columns
            //        string[] temp = entity.menuElement.Split(new string[1] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            //        if (temp.Count() >= 2)
            //        {
            //            temp[1] = entity.value + " " + entity.unitValue;
            //            strToDisplay += temp[0] + Environment.NewLine + temp[1];                                                                        
            //        }
            //    }
            //    else strToDisplay += entity.menuElement;
                
            //    display.Text += strToDisplay;
            //    //if it is not last entity
            //    if (entity != list.Last()) display.Text += Environment.NewLine;
            //}            
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
        #endregion        

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
                             
        
    }
}