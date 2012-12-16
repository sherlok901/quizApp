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
using MRZS.Web.Services;
using System.ServiceModel.DomainServices.Client;
using System.Threading;
using MRZS.Models;

namespace MRZS.Views.Student
{
    public partial class Education : Page
    {
        private SectionContext sectionService = new SectionContext();
        private MRZS.Web.Models.Section currentSection;
        public Education()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Education_Loaded);
        }

        void Education_Loaded(object sender, RoutedEventArgs e)
        {
            var sectionLoadComplete = sectionService.Load(sectionService.GetCurrentStudentSectionByUserNameQuery(WebContext.Current.User.DisplayName));
            sectionLoadComplete.Completed += new EventHandler(sectionLoadComplete_Completed);
            busyIndicator.IsBusy = true;
        }

        void sectionLoadComplete_Completed(object sender, EventArgs e)
        {
            var loadResult = sender as LoadOperation<MRZS.Web.Models.Section>;
            currentSection = loadResult.Entities.FirstOrDefault(s => s.IsCurrent);
            gridCurrentEducation.DataContext = currentSection;
            //if (currentSection == null)
            //    buttonStartEducation.IsEnabled = false;
            busyIndicator.IsBusy = false;

            //NavigationService.Navigate(new Uri("/Testing/Testing", UriKind.Relative));
            //NavigationService.Navigate(new Uri("/Emulator/Emulator-05M", UriKind.Relative));
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        //interactive test
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //EmulatorStarter starter = new EmulatorStarter();
            //starter.Start();

            string uriText = String.Format("/Emulator/Emulator-05M?t={0}", "t");
            NavigationService.Navigate(new Uri(uriText, UriKind.Relative));
            //NavigationService.Navigate(new Uri("/Student/StartEducation", UriKind.Relative));
        }

        /// <summary>
        /// запуск емулятора
        /// </summary>       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Emulator/Emulator-05M", UriKind.Relative));
        }

        //тестирование
        private void Testing_Click_1(object sender, RoutedEventArgs e)
        {            
            NavigationService.Navigate(new Uri("/Testing/Testing", UriKind.Relative));            
        }
        

    }
}
