using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime.InteropServices.Automation;

namespace MRZS.Models
{
    public class EmulatorStarter
    {
        //private string _appName = "KievPribor.exe";
        //private string _dirPath = @"D:\Projects\MRZS\Sources\QuizApplication\Emulator";
        public bool Start()
        {
            //dynamic shell = AutomationFactory.CreateObject("Shell.Application");
            //shell.ShellExecute(_appName, "", _dirPath);
            ////TODO: add error handling

            return true;
        }

    }
}
