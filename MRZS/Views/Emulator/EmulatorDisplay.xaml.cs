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

namespace MRZS.Views.Emulator
{
    public partial class EmulatorDisplay : UserControl
    {
        public EmulatorDisplay()
        {
            InitializeComponent();
        }
        public string FirstMenuString 
        {
            get { return FirstTextBlock.Text; }
            set 
            { 
                FirstTextBlock.Text = value;                
            }
        }
        public string SecondMenuString
        {
            get { return SecondTextBlock.Text; }
            set 
            { 
                SecondTextBlock.Text = value;
                //reselect text
                SecondBorder.Width = 16 * SecondTextBlock.Text.Length;
                if (IsSecondLineSelected) SecondBorder.Background = new SolidColorBrush(Colors.Black);
            }
        }
        private bool SecondLineSelected=false;
        public bool IsSecondLineSelected 
        {
            get
            {                
                return SecondLineSelected;
            }
            set
            {
                SecondLineSelected = value;
                if (SecondLineSelected)
                {
                    SecondBorder.Width = 16 * SecondTextBlock.Text.Length;                    
                    SecondBorder.Background = new SolidColorBrush(Colors.Black);
                    SecondTextBlock.Foreground = new SolidColorBrush(Colors.White);                                      
                }
                else
                {
                    Color a = new Color();
                    a.A = Convert.ToByte(100);
                    a.B = Convert.ToByte(47);
                    a.G = Convert.ToByte(255);
                    a.R = Convert.ToByte(173);
                    SecondBorder.Background = new SolidColorBrush(a);
                    SecondTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                    SecondBorder.Width = 229;
                }
            }
        }
    }
}
