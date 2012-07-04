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
using System.Threading;
using System.Windows.Threading;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Data;

namespace MRZS.Views.Student
{
    public partial class Test : Page
    {
        #region Services

        private QuizContext quizService = new QuizContext();
        private QuestionContext questionService = new QuestionContext();
        private AnswerContext answerService = new AnswerContext();
        private ResultQuizContext resultQuizService = new ResultQuizContext();
        private UserContext userService = new UserContext();
        private ResultAnswerContext resultAnswerService = new ResultAnswerContext();

        #endregion

        private IEnumerable<MRZS.Web.Models.User> userSet;
        private bool alreadyEned;
        private MRZS.Web.Models.User currentUser;
        private MRZS.Web.Models.QuizResult quizResult;
        private IEnumerable<MRZS.Web.Models.Question> questions;
        private MRZS.Web.Models.Question currentQuestion;
        private IEnumerable<MRZS.Web.Models.Answer> answers;
        private MRZS.Web.Models.Quiz currentQuiz;
        private IEnumerable<MRZS.Web.Models.QuizResult> previousResults;

        public int CurrentSectionId { get; set; }
        private List<MRZS.Web.Models.Question> answeredQuestions = new List<Web.Models.Question>();


        private bool IsTestEnded
        {
            get
            {
                return answeredQuestions.Count == questions.Count();
            }
        }

        public Test()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(Test_Loaded);
        }

        void Test_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentSectionId = int.Parse(NavigationContext.QueryString["SectionId"]);

            var quizLoadResult = quizService.Load(quizService.GetQuizsForSectionQuery(CurrentSectionId));
            quizLoadResult.Completed += new EventHandler(quizLoadResult_Completed);

        }


        void quizLoadResult_Completed(object sender, EventArgs e)
        {
            var loadQuizResult = (LoadOperation)sender;
            if (loadQuizResult.Entities.Count() == 0)
                return;
            currentQuiz = (MRZS.Web.Models.Quiz)loadQuizResult.Entities.First();
            var questionLoadResult = questionService.Load(questionService.GetQuestionsForLearningQuery(currentQuiz.QuizId));
            questions = questionLoadResult.Entities;//Load all question set
            questionLoadResult.Completed += new EventHandler(questionLoadResult_Completed);

            var loadresultQuiz = resultQuizService.Load(resultQuizService.GetQuizResultByQuizIdQuery(currentQuiz.QuizId));
            previousResults = loadresultQuiz.Entities;
            TestDispatcherTimer();
        }

        private void TestDispatcherTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            int counter = 0;

            labelTestTime.MouseLeftButtonDown +=
                delegate(object s, MouseButtonEventArgs args)
                {
                    if (timer.IsEnabled) timer.Stop(); else timer.Start();
                };

            timer.Tick +=
                delegate(object s, EventArgs args)
                {
                    labelTestTime.Content = string.Format("Время: {0} с", counter++.ToString());
                };

            timer.Interval = new TimeSpan(0, 0, 1); // one second
            timer.Start();
        }

        void questionLoadResult_Completed(object sender, EventArgs e)
        {
            if (questions.Any())
            {
                currentQuestion = GetNextQuestion();
                if (quizResult == null)
                {
                    var loadUserResult = userService.Load(userService.GetUserByAspUserNameQuery(WebContext.Current.User.DisplayName));
                    loadUserResult.Completed += new EventHandler(loadUserResult_Completed);
                    userSet = loadUserResult.Entities;
                }
            }
            else
            {
                //TODO: Redirect away
            }
        }

        void loadUserResult_Completed(object sender, EventArgs e)
        {
            currentUser = userSet.First();
            quizResult = new Web.Models.QuizResult();
            quizResult.StartTime = DateTime.Now;
            quizResult.QuizId = currentQuestion.QuizId;
            quizResult.UserId = currentUser.UserId;
            resultQuizService.QuizResults.Add(quizResult);
            resultQuizService.SubmitChanges();
        }

        void answersLoadResult_Completed(object sender, EventArgs e)
        {
            if (!answers.Any(a => currentQuestion.QuestionId == a.QuestionId))
            {
                currentQuestion = GetNextQuestion();
                return;
            }

            labelQuestionText.Text = currentQuestion.Text;
            dataGrdiAnswers.ItemsSource = answers;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void buttonNextQuestion_Click(object sender, RoutedEventArgs e)
        {
            LogAnswer(false);
        }

        private void LogAnswer(bool isLastQuestion)
        {
            var userAnswer = (dataGrdiAnswers.SelectedItem as MRZS.Web.Models.Answer);
            if (userAnswer != null)
            {
                MRZS.Web.Models.ResultAnswer logAnswer = new Web.Models.ResultAnswer();
                logAnswer.AnswerId = userAnswer.AnswerId;
                logAnswer.AnswerTime = DateTime.Now;
                logAnswer.QuizResultId = quizResult.QuizResultId;
                logAnswer.QuestionId = currentQuestion.QuestionId;
                resultAnswerService.ResultAnswers.Add(logAnswer);
            }
            var log = resultAnswerService.SubmitChanges();
            if (!isLastQuestion)
                log.Completed += new EventHandler(log_Completed);
        }

        void log_Completed(object sender, EventArgs e)
        {
            currentQuestion = GetNextQuestion();
        }

        private int GetQuestionOrder(MRZS.Web.Models.Question question)
        {
            previousResults.OrderByDescending(r => r.EndTime);
            var lastResult = previousResults.FirstOrDefault();
            //lastResult.
            //currentQuiz.SimpleDifficulty
            return 0;
        }

        private MRZS.Web.Models.Question GetNextQuestion()
        {
            buttonNextQuestion.IsEnabled = false;
            //TODO: implement GetNExtQuestion()
            if (questions == null)
                return null;
            var resultQuestion = questions.FirstOrDefault(q => !answeredQuestions.Contains(q));
            answeredQuestions.Add(resultQuestion);
            var answersLoadResult = answerService.Load(answerService.GetAnswersForQuestionQuery(resultQuestion.QuestionId));
            answersLoadResult.Completed += new EventHandler(answersLoadResult_Completed);
            answers = answersLoadResult.Entities;
            buttonNextQuestion.IsEnabled = !IsTestEnded;
            return resultQuestion;
        }

        private void buttonEndTest_Click(object sender, RoutedEventArgs e)
        {
            LogAnswer(true);
            EndTest();
            alreadyEned = true;
        }

        private void EndTest()
        {
            if (quizResult != null)
            {
                quizResult.EndTime = DateTime.Now;
                var endTest = resultQuizService.SubmitChanges();
                endTest.Completed += new EventHandler(endTest_Completed);
            }
        }

        void endTest_Completed(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/Student/TestResult?quizResultId={0}", quizResult.QuizResultId), UriKind.Relative));
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (!alreadyEned)
            {
                EndTest();
            }
        }

    }
}
