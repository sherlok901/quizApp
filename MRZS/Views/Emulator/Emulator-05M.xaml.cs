using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using MRZS.Classes;
using MRZS.Classes.DisplayCode;
using MRZS.Web.Models;

namespace MRZS.Views.Emulator
{
    public partial class Emulator_05M : System.Windows.Controls.Page
    {                                        
        private NumericUpDown numUpDown1;        
        //entity        
        IEnumerable<passwordCheckType> passwordCheckTypeList = null;
        IEnumerable<kindSignalDC> kindSignaLoadDataCList = null;
        IEnumerable<typeSignalDC> typeSignaLoadDataCList=null; 
        IEnumerable<typeFuncDC> typeFuncDCList=null;
        IEnumerable<BooleanVal> BooleanValList = null;
        IEnumerable<BooleanVal2> BooleanVal2List=null;
        IEnumerable<BooleanVal3> BooleanVal3List=null;
        IEnumerable<mtzVal> mtzValList=null;
        IEnumerable<mrzsInOutOption> mrzsInOutOptionList = null;
        private IEnumerable<mrzs05mMenu> mrzs05Entity;
        DispatcherTimer timer = new DispatcherTimer();

        //get info of inputs
        public enum Inputs
        {
            ДВ01, ДВ02, ДВ03, ДВ04, ДВ05, ДВ06,
            СДИ1, СДИ2, СДИ3, СДИ4, СДИ5, СДИ6,
            P01, P02, P03, P04, P05
        }        
                
        MenuController MenuControllr = new MenuController();

        public Emulator_05M()
        {
            InitializeComponent();
            busyIndicator.IsBusy = false;
            //subcribing for loaded data event
            if (LoadData.checkNotNullTables() == false)
            {
                busyIndicator.IsBusy = true;
                LoadData.DataLoaded += LoadData_DataLoaded;
                MenuControllr.DataLoad += MenuControllr_DataLoad;
            }
            //if data already loaded
            else
            {
                LoadData_DataLoaded(null, EventArgs.Empty);
                MenuControllr_DataLoad(null, EventArgs.Empty);
            }
        }        
                       
        void MenuControllr_DataLoad(object sender, EventArgs e)
        {
            DisplayViewModel DispControlr=MenuControllr.setDefaultMenu();
            emju.DataContext = DispControlr;
            //give DisplayViewModel instance
            PasswordController.setDisplayController(DispControlr);
        }

        void LoadData_DataLoaded(object sender, EventArgs e)
        {
            busyIndicator.IsBusy = false;
            passwordCheckTypeList = LoadData.passwordCheckTypeTable;
            kindSignaLoadDataCList = LoadData.kindSignalDCTable;
            typeSignaLoadDataCList = LoadData.typeSignalDCTable;
            typeFuncDCList = LoadData.typeFuncDCTable;
            BooleanValList = LoadData.BooleanValTable;
            BooleanVal2List = LoadData.BooleanVal2Table;
            BooleanVal3List = LoadData.BooleanVal3Table;
            mtzValList = LoadData.mtzValTable;
            mrzsInOutOptionList = LoadData.mrzsInOutOptionTable;
            if(mrzs05Entity==null) mrzs05Entity = LoadData.MrzsTable;            
            
        }                                                       

        #region cursor events ***
        private void downButton_Click(object sender, RoutedEventArgs e)
        {            
            
        }      
        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void leftButton_Click(object sender, RoutedEventArgs e)
        {
                        
        }
        private void rightButton_Click(object sender, RoutedEventArgs e)
        {
                       
        } 
        #endregion

        #region == other func buttons ==        

        private void enterButton_Click_2(object sender, RoutedEventArgs e)
        {
            MenuControllr.enterButtonClicked(emju);            
        }                                        
        
        private void escButton_Click(object sender, RoutedEventArgs e)
        {
            MenuControllr.escButtonClicked(emju);
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
            //выдержка мтз1
            double MTZ1TimeSpan = MenuControllr.getExcerptMTZ1();            
            Thread.Sleep(TimeSpan.FromSeconds(MTZ1TimeSpan));
            CheckDefence_MTZ1();

            //выдержка мтз2
            string uskorMTZ2 = MenuControllr.getUskorMTZ2();
            double temp = 0;
            if (uskorMTZ2.IndexOf("ВКЛ") != -1)
            {
                //Т Ускор МТЗ
                temp = MenuControllr.getTUskorMTZ();
            }//Выдержка МТЗ2
            else temp = MenuControllr.getExcerptMTZ2();
            //sleep
            Thread.Sleep(TimeSpan.FromSeconds(temp));
            CheckDefence_MTZ2();

            //выдержка ЗЗ
            double ZZtimespan = MenuControllr.getZZExcerpt();
            Thread.Sleep(TimeSpan.FromSeconds(ZZtimespan));
            CheckDefence_ZZ();
        }
        
        void CheckDefence_MTZ1()
        {            
            //get МТЗ->Уставки->Уставка МТЗ1
            double val = MenuControllr.getSetpointMTZ1();
            //get МТЗ-Управление-1 Ступень МТЗ-ВКЛ
            string stupen1MTZ = MenuControllr.getStupenMTZ();
            
            if ((Ia.Value > val || Ib.Value > val || Ic.Value > val)
                && (stupen1MTZ.IndexOf("ВКЛ")!=-1)
                && !IsInDVArrayValue("Блок МТЗ 1"))//ДВ не включены; если вкл, то на них нет ф-ции "Блок МТЗ 1"
            {
                //turn on/off rele
                //выкл (засветить) те реле, в которых включен параметр='Сраб МТЗ 1'
                CheckR("Сраб МТЗ 1", true);                
                CheckSDI("Сраб МТЗ 1", true);
                CheckDefence_APV(1);  
                
            }            
        }
        
        void CheckDefence_MTZ2()
        {            
            //get МТЗ->Уставки->Уставка МТЗ2
            double val = MenuControllr.getSetpointMTZ2();
            //get МТЗ-Управление-2 Ступень МТЗ-ВКЛ
            string stupen2MTZ = MenuControllr.getStupenMTZ2();

            //есть хотябы в одном ДВ устан. параметр "Блок. МТЗ 2"
            if ((Ia.Value > val || Ib.Value > val || Ic.Value > val)
                && (stupen2MTZ.IndexOf("ВКЛ") != -1)
                && !IsInDVArrayValue("Блок МТЗ 2"))//ДВ не включены; если вкл, то на них нет ф-ции "Блок МТЗ 1"
            {
                string uskorMTZ2 = MenuControllr.getUskorMTZ2();
                //Ускор МТЗ2=ВКЛ
                if (uskorMTZ2.IndexOf("ВКЛ") != -1)
                {
                    CheckR("Блок ускор. МТЗ", true);
                    CheckSDI("Блок ускор. МТЗ", true);
                }
                else
                {
                    CheckR("Сраб МТЗ 2", true);
                    CheckSDI("Сраб МТЗ 2", true);
                }
                CheckDefence_APV(2);
            }
        }
        private void CheckDefence_ZZ()
        {
            //уставка ЗЗ
            double ZZsetpoint=MenuControllr.ZZSetpoint();
            //защита ЗЗ
            string DefenceZZ=MenuControllr.getDefenceZZ();
            if ((ZIO.Value > ZZsetpoint || Izfa.Value > ZZsetpoint) && DefenceZZ.IndexOf("ВКЛ") != -1)//&& IsInDVArrayValue("")
            {
                CheckR("Сраб ЗЗ", true);
                CheckSDI("Сраб ЗЗ", true);
            }
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
                    //r1c_Click_1(null, null);
                }
                else if (listCheckbox.IndexOf(cb) == 1)
                {
                    functions = getTurnONFunctions(Inputs.P02, mrzs05Entity);
                    if (functions.Contains(functionName)) r2c.IsChecked = turnOff;
                    //r2c_Click_1(null, null);
                }
                else if (listCheckbox.IndexOf(cb) == 2)
                {
                    functions = getTurnONFunctions(Inputs.P03, mrzs05Entity);
                    if (functions.Contains(functionName)) r3c.IsChecked = turnOff;
                    //r3c_Click_1(null, null);
                }
                else if (listCheckbox.IndexOf(cb) == 3)
                {
                    functions = getTurnONFunctions(Inputs.P04, mrzs05Entity);
                    if (functions.Contains(functionName)) r4c.IsChecked = turnOff;
                    //r4c_Click_1(null, null);
                }
                else if (listCheckbox.IndexOf(cb) == 4)
                {
                    functions = getTurnONFunctions(Inputs.P05, mrzs05Entity);
                    if (functions.Contains(functionName)) r5c.IsChecked = turnOff;
                    //r5c_Click_1(null, null);
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
            double cycle1APV = MenuControllr.get1CycleAPV();
            Thread.Sleep(TimeSpan.FromSeconds(cycle1APV));
            //int? puskOtMTZ1= getMrzs05mMenuByMenuElement(mrzs05Entity,"Пуск от МТЗ1").Single().BooleanVal3ID;
            //int? puskOtMTZ2 = getMrzs05mMenuByMenuElement(mrzs05Entity, "Пуск от МТЗ2").Single().BooleanVal3ID;
            int? puskOtMTZ1 = MenuControllr.puskOtMtz1();
            int? puskOtMTZ2 = MenuControllr.puskOtMtz2();
            //get МТЗ->Уставки->Уставка МТЗ1                       
            double UstavkaMTZ1 = MenuControllr.getSetpointMTZ1();
            string stupen1MTZ = MenuControllr.getStupenMTZ();
            double UstavkaMTZ2 = MenuControllr.getSetpointMTZ2();
            string stupen2MTZ = MenuControllr.getStupenMTZ2();

            if (((puskOtMTZ1 == 1 && MTZnumber == 1) || (puskOtMTZ2 == 1 && MTZnumber == 2))
                && (!IsInDVArrayValue("АЧР/ЧАПВ")))//ДВ не вкл или если вкл, то ни на одном ДВ не установлен параметр "АЧР/ЧАПВ"
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
            //r1c_Click_1(null, null);
            r2c.IsChecked = false;
            //r2c_Click_1(null, null);
            r3c.IsChecked = false;
            //r3c_Click_1(null, null);
            r4c.IsChecked = false;
            //r4c_Click_1(null, null);
            r5c.IsChecked = false;
            //r5c_Click_1(null, null);
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
                                                  

        #region Numeric buttons events =============================================
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MenuControllr.numButtonClicked(emju.SecondTextBlock, 1);

            
            //System.Threading.Thread.Sleep(700);
            //(button1.Content as Image).Source = new BitmapImage(new Uri("/MRZS;component/Assets/1.png", UriKind.Relative));
        }
       
        private void button2_Click(object sender, RoutedEventArgs e)
        {            
            MenuControllr.numButtonClicked(emju.SecondTextBlock, 2);
        }
        //replace symbol in textbox on new inputed symbol
        private string replaceSymbol(string textBoxStr, int position, string symbol)
        {
            textBoxStr = textBoxStr.Remove(position, 1);
            return textBoxStr.Insert(position, symbol);             
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            MenuControllr.numButtonClicked(emju.SecondTextBlock, 3);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            MenuControllr.numButtonClicked(emju.SecondTextBlock, 4);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            MenuControllr.numButtonClicked(emju.SecondTextBlock, 5);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            MenuControllr.numButtonClicked(emju.SecondTextBlock, 6);
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            MenuControllr.numButtonClicked(emju.SecondTextBlock, 7);
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            MenuControllr.numButtonClicked(emju.SecondTextBlock, 8);
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            MenuControllr.numButtonClicked(emju.SecondTextBlock, 9);
        }

        private void button0_Click(object sender, RoutedEventArgs e)
        {
            MenuControllr.numButtonClicked(emju.SecondTextBlock, 0);
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
       

        private void button1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void button1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            (button1.Content as Image).Source = new BitmapImage(new Uri("/MRZS;component/Assets/1_active.png", UriKind.Relative));
        }

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }
        
        //BMK:
        private void leftButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //set other clicked image
            leftButton.Source = new BitmapImage(new Uri("/MRZS;component/Assets/left_active.png", UriKind.Relative));
            //functionality
            MenuControllr.leftButtonClicked(emju.SecondTextBlock);
        }
        //set other unclicked image
        private void leftButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            leftButton.Source = new BitmapImage(new Uri("/MRZS;component/Assets/left.png", UriKind.Relative));
        }

        private void upButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            upButton.Source = new BitmapImage(new Uri("/MRZS;component/Assets/up_active.png", UriKind.Relative));
            MenuControllr.showPreviousMenuLine(emju);
        }

        private void upButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            upButton.Source = new BitmapImage(new Uri("/MRZS;component/Assets/up.png", UriKind.Relative));
        }

        private void rightButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            rightButton.Source= new BitmapImage(new Uri("/MRZS;component/Assets/right_active.png", UriKind.Relative));
            MenuControllr.rightButtonClicked(emju.SecondTextBlock);
        }

        private void rightButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rightButton.Source = new BitmapImage(new Uri("/MRZS;component/Assets/right.png", UriKind.Relative));
        }

        private void bottomButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bottomButton.Source = new BitmapImage(new Uri("/MRZS;component/Assets/bottom_active.png", UriKind.Relative));
            MenuControllr.showNextMenuLine(emju);
        }

        private void bottomButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bottomButton.Source = new BitmapImage(new Uri("/MRZS;component/Assets/bottom.png", UriKind.Relative));
        }

        private void emju_Loaded(object sender, RoutedEventArgs e)
        {

        }
       
    }
    
}