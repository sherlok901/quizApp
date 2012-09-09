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
    public partial class InputDV : UserControl
    {
        public string InputName
        {
            get { return textBlockInputName.Text; }
            set { textBlockInputName.Text = value; }
        }
        public bool? IsChecked
        {
            get { return dv.IsChecked; }
            set { dv.IsChecked = value; }
        }

        public InputDV()
        {
            InitializeComponent();
        }
    }
}
