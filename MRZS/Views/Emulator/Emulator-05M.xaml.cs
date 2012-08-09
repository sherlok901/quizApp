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


namespace MRZS.Views.Emulator
{
    public partial class Emulator_05M : System.Windows.Controls.Page
    {
        //properties
        private BooleanVal1 boolContext;
        public BooleanVal1 pboolContext
        {
            get{ return boolContext;}
            set { boolContext = value; }
        }
        List<int?> parentIDlist;
        private NumericUpDown numUpDown1;
    
        public Emulator_05M()
        {
            InitializeComponent();
            display.TextWrapping = TextWrapping.Wrap;            
            display.FontFamily = new FontFamily("Arial");
            display.FontSize = 20.0;                        
            display.Padding = new Thickness(5.0);            
            //display.Text = "Часы"+Environment.NewLine;
            //display.Text += "Измерения" + Environment.NewLine;
            //display.Text += "Настройка" + Environment.NewLine;
            //display.Text += "Конфигурация" + Environment.NewLine;
            //display.Text += "Авария" + Environment.NewLine;
            //display.Text += "Диагностика" + Environment.NewLine;
            //display.Text += "МТЗ" + Environment.NewLine;
            //display.Text += "АПВ" + Environment.NewLine;            
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
            System.ServiceModel.DomainServices.Client.LoadOperation<mrzs05mMenu> b = sender as LoadOperation<mrzs05mMenu>;
            if (b != null)
            {
                //больше 0
                if(b.Entities.Count(n=>n!=null)>0)
                {
                    //get list of different elem in column parentID
                    parentIDlist = b.Entities.Select(n => n.parentID).Distinct().ToList();
                    //get 1 level of menu
                    List<string> s = b.Entities.Where(n => n.parentID == parentIDlist[0]).Select(n => n.menuElement).ToList();
                    DisplayMenu(s);
                }                
            }
        }
        /// <summary>
        /// Display menu
        /// </summary>        
        private void DisplayMenu(List<string> s)
        {
            foreach (string str in s)
            {
                display.Text += str + Environment.NewLine;
            }
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

        void webserv_GetMenuElementCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string s = e.ToString();
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

        private void NumericUpDown_MouseEnter_1(object sender, MouseEventArgs e)
        {
            ShowPopUpLittle(sender);;
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
        
        private void NumericUpDown_MouseEnter_2(object sender, MouseEventArgs e)
        {            
            ShowPopUpLittle(sender);
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

    }
}
