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


namespace MRZS.Views.Emulator
{
    public partial class Emulator_05M : Page
    {
        public Emulator_05M()
        {
            InitializeComponent();
            
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void NumericUpDown_GotFocus_1(object sender, RoutedEventArgs e)
        {
            
        }
        //f numericUpDown
        private void NumericUpDown_f_GotFocus(object sender, RoutedEventArgs e)
        {
            //NumericUpDown f=new NumericUpDown();
            //f.Background = new SolidColorBrush(Colors.Red);   
            
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void slider1_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void slider1_MouseLeave(object sender, MouseEventArgs e)
        {
            popUp.IsOpen = false;
        }

        private void f_MouseEnter(object sender, MouseEventArgs e)
        {
            ShowPopUp(sender);
        }

        private void NumericUpDown_MouseEnter_1(object sender, MouseEventArgs e)
        {
            ShowPopUp(sender);            
        }
        /// <summary>
        /// Show popup under current numericupdown
        /// </summary>
        /// <param name="sender"></param>
        private void ShowPopUp(object sender)
        {
            NumericUpDown numUpDown1 = sender as NumericUpDown;
            if (numUpDown1 != null)
            {
                //numUpDown1.ClearValue(NumericUpDown.ValueProperty);
                //slider1.ClearValue(Slider.ValueProperty);
                //f.ClearValue(NumericUpDown.ValueProperty);
                System.Windows.Data.Binding bind = new System.Windows.Data.Binding("Value");
                bind.Mode = System.Windows.Data.BindingMode.OneTime;
                bind.Source = slider1;
                numUpDown1.SetBinding(NumericUpDown.ValueProperty, bind);
                int row = (int)numUpDown1.GetValue(Grid.RowProperty);
                int col = (int)numUpDown1.GetValue(Grid.ColumnProperty);
                popUp.SetValue(Grid.RowProperty, row + 1);
                popUp.SetValue(Grid.ColumnProperty, col);
                popUp.IsOpen = true;
            }
        }
        
        private void NumericUpDown_MouseEnter_2(object sender, MouseEventArgs e)
        {
            ShowPopUp(sender);
        }

    }
}
