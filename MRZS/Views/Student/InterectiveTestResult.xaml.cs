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

namespace MRZS.Views.Student
{
    public partial class InterectiveTestResult : Page
    {
        private int sectionId;
        public InterectiveTestResult()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(InterectiveTestResult_Loaded);
        }

        protected void InterectiveTestResult_Loaded(object sender, RoutedEventArgs e)
        {
            sectionId = int.Parse(NavigationContext.QueryString["sectionId"]);
            this.DataContext = new InterectiveTestResultModel(sectionId);
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
