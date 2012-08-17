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

        List<int?> parentIDlist;
        private bool displayAnimFlag;
        private NumericUpDown numUpDown1;
        private IEnumerable<mrzs05mMenu> mrzs05Entity;
        //private List<Display> SelectedMenuElementHistory = new List<Display>();
        private List<mrzs05mMenu> SelectedMenuElemHistory = new List<mrzs05mMenu>(0);
        private List<mrzs05mMenu> DisplayedEntities = new List<mrzs05mMenu>(0);

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

            mrzs05mMenuContext mrzs05mMContxt = new mrzs05mMenuContext();
            LoadOperation<mrzs05mMenu> mrzs05mMModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetMrzs05mMenuQuery());
            mrzs05mMModel.Completed += mrzs05mMModel_Completed;
            
        }
        /// <summary>
        /// igor: load parentID column
        /// </summary>        
        void mrzs05mMModel_Completed(object sender, EventArgs e)
        {            
            //get list of different elem in column parentID
            mrzs05Entity = getEntities(sender);
            if (mrzs05Entity != null)
            {
                parentIDlist = mrzs05Entity.Select(n => n.parentID).Distinct().ToList();                
                //get 1 level of menu 
                DisplayedEntities.AddRange(getEntitiesByParentID(parentIDlist[0]));
                DisplayMenu(DisplayedEntities);
                display.Text = ">" + display.Text;
                timer2.Completed += timer2_Completed;
                timer2.Begin();
                //timer.Completed += timer_Completed;                
                //timer.Begin();
            }
        }

        void timer2_Completed(object sender, EventArgs e)
        {
            if (displayAnimFlag)
            {
                display.Text = "_" + display.Text.Substring(1);
                displayAnimFlag = false;
            }
            else
            {
                display.Text = ">" + display.Text.Substring(1);
                displayAnimFlag = true;
            }
            timer2.Begin();            
        }
        
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
        /// <summary>
        /// Display menu
        /// </summary>        
        private void DisplayMenu(List<string> s)
        {
            display.ClearValue(TextBox.TextProperty);            
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
            display.SelectionStart = display.Text.IndexOf(firstDisplayedWord);
            //List<string> list= display.Text.Split(new char[2] { '\r', '\n' }).ToList();
            //int index = list.IndexOf(firstDisplayedWord);
        }

        private void DisplayMenu(List<mrzs05mMenu> list)
        {
            List<string> s = list.Select(n => n.menuElement).ToList();
            DisplayMenu(s);
        }
        void boolEntity_Completed(object sender, EventArgs e)
        {                        
            System.ServiceModel.DomainServices.Client.LoadOperation<BooleanVal> b = sender as LoadOperation<BooleanVal>;
            if (b!=null)
            {
                IEnumerable<BooleanVal> list = b.Entities;
                foreach (BooleanVal bv in list)
                {
                    int id = bv.id;
                    string val = bv.val;
                }
            }
        }        

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int start = display.SelectionStart;
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
        /// Enter button clicked,
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string selectedWordMenu= GetSelectedWordMenu();            
            if (selectedWordMenu != null)
            {
                //get id selected word by menuElement
                mrzs05mMenu selectedEntity = DisplayedEntities.Last(n => n.menuElement == selectedWordMenu);
                if (getEntitiesByParentID(selectedEntity.id).Count == 0) return;
                //add current selected word and id to history list                                
                SelectedMenuElemHistory.Add(selectedEntity);
                //get next menu list by parentID of current selected word                
                List<mrzs05mMenu> newMenuLevel = getEntitiesByParentID(selectedEntity.id);
                DisplayMenu(newMenuLevel);
                DisplayedEntities.AddRange(newMenuLevel);                
            }            
        }

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
            //return selected word
            return display.Text.Substring(startWordIndex, leng);
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

        private void escButton_Click(object sender, RoutedEventArgs e)
        {
            //get parentID of last selected elem in menu
            if (SelectedMenuElemHistory.Count == 0) return; //!возможна DisplayedEntities будет содержать елем
            int? parentId = SelectedMenuElemHistory.Last().parentID;                                    
            //delete displayed entities                        
            while(true)
            {
                if(DisplayedEntities.Last().parentID==SelectedMenuElemHistory.Last().id) 
                    DisplayedEntities.Remove(DisplayedEntities.Last());
                else break;
            }          
            List<string> list =getEntitiesByParentID(parentId).Select(n => n.menuElement).ToList();
            //set display first line on last selected word
            DisplayMenu(list, SelectedMenuElemHistory.Last().menuElement);
            //delete selected entity
            SelectedMenuElemHistory.Remove(SelectedMenuElemHistory.Last());            
        }

        #region cursor events
        
        private void downButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}