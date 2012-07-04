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
using System.Windows.Threading;

namespace MRZS.Views.Coordinator
{
    public partial class UserStatistic : Page
    {
        private UserStatisticViewModel userStatisticViewModel;
        public UserStatistic()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(UserStatistic_Loaded);
        }

        void UserStatistic_Loaded(object sender, RoutedEventArgs e)
        {
            busyIndicator.IsBusy = true;

            int userId = int.Parse(NavigationContext.QueryString["userId"]);
            userStatisticViewModel = new UserStatisticViewModel(userId);
            this.DataContext = userStatisticViewModel;
            ThreadPool.QueueUserWorkItem((state) =>
            {
                while (!userStatisticViewModel.QuizLoaded)
                    Thread.Sleep(100);
                Dispatcher.BeginInvoke(() => busyIndicator.IsBusy = false);
            });
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void C1DataGrid_LoadedRowDetailsPresenter(object sender, C1.Silverlight.DataGrid.DataGridRowDetailsEventArgs e)
        {
            if (userStatisticViewModel.SelectedQuizResult == null)
                return;
            e.DetailsElement.DataContext = new AnswersStatisticViewModel(
                (e.Row.DataItem as MRZS.Web.Models.QuestionLog).Id,
                userStatisticViewModel.SelectedQuizResult.EntityId);
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

        private void C1DataGrid_LoadedRowPresenter(object sender, C1.Silverlight.DataGrid.DataGridRowEventArgs e)
        {
            var questionLog = e.Row.DataItem as MRZS.Web.Models.QuestionLog;
            if (questionLog.IsCorrect)
            {
                e.Row.Presenter.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                e.Row.Presenter.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
