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
using MRZS.Models.Statistic;
using System.Threading;

namespace MRZS.Views.Coordinator
{
    public partial class Statistic : Page
    {
        private StatisticViewModel studentViewModel = new StatisticViewModel();
        public Statistic()
        {
            InitializeComponent();
            this.DataContext = studentViewModel;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void C1DataGrid_LoadedRowDetailsPresenter(object sender, C1.Silverlight.DataGrid.DataGridRowDetailsEventArgs e)
        {
            var child = VisualTreeHelper.GetChild(e.DetailsElement, 0) as ProgressBar;
            child.IsIndeterminate = true;

            var user = e.Row.DataItem as MRZS.Web.Models.User;

            e.DetailsElement.DataContext = new UserStatisticViewModel(user.UserId, true);
        }

        private void C1DataGridAnswers_LoadedRowDetailsPresenter(object sender, C1.Silverlight.DataGrid.DataGridRowDetailsEventArgs e)
        {
            e.DetailsElement.DataContext = new AnswersStatisticViewModel(
                (e.Row.DataItem as MRZS.Web.Models.QuestionLog).Id,
                ((sender as C1.Silverlight.DataGrid.C1DataGrid).DataContext as UserStatisticViewModel).SelectedQuizResult.EntityId);
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var answer = e.Row.DataContext as MRZS.Web.Models.Answer;

            var answersStatisticViewModel = (sender as DataGrid).DataContext as AnswersStatisticViewModel;
            if (answersStatisticViewModel.ResultAnswers.Any(ra => ra.AnswerId == answer.AnswerId && answer.IsCorrect))
            {
                e.Row.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if (answersStatisticViewModel.ResultAnswers.Any(ra => ra.AnswerId == answer.AnswerId && !answer.IsCorrect))
            {
                e.Row.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void C1DataGrid_LoadingRow(object sender, C1.Silverlight.DataGrid.DataGridRowEventArgs e)
        {
            
        }

        private void C1DataGrid_LoadedRowPresenter(object sender, C1.Silverlight.DataGrid.DataGridRowEventArgs e)
        {
            var questionLog = e.Row.DataItem as MRZS.Web.Models.QuestionLog;
            if (questionLog.IsCorrect)
            {
                e.Row.Presenter.Foreground = new SolidColorBrush(Colors.Green);
                //e.Row.RowStyle = new System.Windows.Style();
                //Setter setBackground = new Setter(DataGridRow.ForegroundProperty, new SolidColorBrush(Colors.Green));
                //e.Row.RowStyle.Setters.Add(setBackground);
            }
            else
            {
                e.Row.Presenter.Foreground = new SolidColorBrush(Colors.Red);
                //e.Row.RowStyle = new System.Windows.Style();
                //Setter setBackground = new Setter(DataGridRow.ForegroundProperty, new SolidColorBrush(Colors.Red));
                //e.Row.RowStyle.Setters.Add(setBackground);
            }
        }
    }
}
