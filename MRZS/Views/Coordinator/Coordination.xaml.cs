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
using MRZS.Views.Coordinator.Popups;
using System.Windows.Browser;
using System.Runtime.InteropServices.Automation;

namespace MRZS.Views.Coordinator
{
    public partial class Coordination : Page
    {
        public Coordination()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void userDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            var user = (gridStudents.SelectedItem as Web.Models.User);
            if (user == null)
                return;
            DefineStudentSection popup = new DefineStudentSection();
            popup.Closed += new EventHandler(popup_Closed);
            popup.CurrentUser = user;
            popup.Show();
        }

        void popup_Closed(object sender, EventArgs e)
        {
            this.NavigationService.Refresh();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            dynamic shell = AutomationFactory.CreateObject("Shell.Application");
            shell.ShellExecute(@"KievPribor.exe", "", @"D:\Projects\MRZS\Sources\WebAst\Emulator\");
            //if (AutomationFactory.IsAvailable)
            //{
            //    using (dynamic shell = AutomationFactory.GetObject("Wscript.Shell"))
            //    {
            //        shell.Run(@"""D:\Projects\MRZS\Sources\WebAst\Emulator\KievPribor.exe""");
            //    }
            //}
        }

    }
}
