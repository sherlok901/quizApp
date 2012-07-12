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
            NumericUpDown f=new NumericUpDown();
            f.Background = new SolidColorBrush(Colors.Red);
            
        }

    }
}
