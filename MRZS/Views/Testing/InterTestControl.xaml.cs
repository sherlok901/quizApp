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

namespace MRZS.Views.Testing
{
    public partial class InterTestControl : UserControl
    {
        internal event EventHandler PrevBtnClicked;
        internal event EventHandler NextBtnClicked;
        internal event EventHandler CheckBtnClicked;

        public InterTestControl()
        {
            InitializeComponent();          
  
        }
        //task text
        internal string TaskText
        {
            set { TestTextTb.Text = value; }

        }
        //show status text as result of checking executed task
        internal string RezStatusText
        {
            set { ResultStateTb.Text = value; }
        }

        void PrevBtn_Click(object sender, RoutedEventArgs e)
        {            
            if (PrevBtnClicked != null) PrevBtnClicked(this, EventArgs.Empty);
        }       

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NextBtnClicked != null) NextBtnClicked(null, EventArgs.Empty);
        }

        private void CheckTestBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBtnClicked != null) CheckBtnClicked(null, EventArgs.Empty);
        }
      
        private void ResultStateTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ResultStateTb.Text == "Все настроено верно") ResultStateTb.Foreground = new SolidColorBrush(Colors.Green);
            else ResultStateTb.Foreground = new SolidColorBrush(Colors.Red);
        }
        
    }
}
