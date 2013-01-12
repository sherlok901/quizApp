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
//using System.Windows.Threading;

namespace MRZS.Classes.myTimer
{

    internal class MyTimer
    {
        
        internal MyTimer()
        {            
                       

        }
        private static Timer ticker;

        public static void TimerMethod(object state)
        {
            ticker.Change(9, 0);
        }

        public static void Main()
        {
            ticker = new Timer(TimerMethod, null, 1000, 1000);
          
        }
        


    }
}
