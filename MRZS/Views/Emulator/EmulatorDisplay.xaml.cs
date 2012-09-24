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
            set { FirstTextBlock.Text = value; }
        }
        public string SecondMenuString
        {
            get { return SecondTextBlock.Text; }
            set { SecondTextBlock.Text = value; }
        }
    }
}
