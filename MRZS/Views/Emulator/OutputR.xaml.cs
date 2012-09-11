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
        
        public bool turnOn
        {            
            set {
                Color a = new Color();
                if (!value)
                {
                    line2.Y2 = 7;                    
                    a.R = Convert.ToByte(235);
                    a.G = Convert.ToByte(128);
                    a.B = Convert.ToByte(128);
                    a.A = Convert.ToByte(100);                   
                }
                else
                {
                    a.R = Convert.ToByte(144);
                    a.G = Convert.ToByte(238);
                    a.B = Convert.ToByte(144);
                    a.A = Convert.ToByte(100);
                    line2.Y2 = 2;
                }
                LayRoot.Background = new SolidColorBrush(a); 
            }
            get { return turnOn; }
        }        

	    public OutputR()
		{
			// Required to initialize variables
			InitializeComponent();
		}
	}
}