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

namespace MRZS.Views.Admin.Popups
{
    public partial class MoveQuestion : Page
    {
        private int sectionId;
        private MoveQuestionViewModel moveQuestionViewModel;
        public MoveQuestion()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MoveQuestion_Loaded);
        }

        void MoveQuestion_Loaded(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(NavigationContext.QueryString["sectionId"], out sectionId))
                return;
            moveQuestionViewModel = new MoveQuestionViewModel(sectionId);
            this.DataContext = moveQuestionViewModel;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
