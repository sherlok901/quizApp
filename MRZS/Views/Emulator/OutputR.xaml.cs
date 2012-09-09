using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MRZS
{
	public partial class OutputR : UserControl
	{
	    public string OutputName
	    {
            get { return OutName.Text; }
            set { OutName.Text = value; }
	    }      

	    public OutputR()
		{
			// Required to initialize variables
			InitializeComponent();
		}
	}
}