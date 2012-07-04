using System;
using MRZS.Web.Services;
using System.ServiceModel.DomainServices.Client;
using MRZS.Web.Models;
using System.Linq;

namespace MRZS.Models.Statistic
{
    public class UserStatisticViewModel : BaseViewModel
    {

        #region Fields
        private QuizLogDTO _selectedQuizResult;
        private readonly ResultQuizContext _resultQuizContext;
        private readonly UserContext _userContext;

        #endregion

        public UserStatisticViewModel(int userId)
        {
            _resultQuizContext = new ResultQuizContext();
            var quizResult = _resultQuizContext.Load(_resultQuizContext.GetAllQuizResultsForUserQuery(userId));
            quizResult.Completed += new EventHandler(quizResult_Completed);
            _userContext = new UserContext();
            var result = _userContext.Load(_userContext.GetUserByIdQuery(userId));
            result.Completed += new EventHandler(result_Completed);
        }

        #region HelperMethods

        void quizResult_Completed(object sender, EventArgs e)
        {
            QuizLoaded = true;
            RaisePropertyChanged("QuizLoaded");
        }

        public UserStatisticViewModel(int userId, bool onlyLastResult)
        {
            _resultQuizContext = new ResultQuizContext();
            var lastResultLoaded = _resultQuizContext.Load(_resultQuizContext.GetLastQuizResultsForUserQuery(userId));
            _userContext = new UserContext();
            var result = _userContext.Load(_userContext.GetUserByIdQuery(userId));
            result.Completed += new EventHandler(result_Completed);
            lastResultLoaded.Completed += new EventHandler(lastResultLoaded_Completed);
        }

        void lastResultLoaded_Completed(object sender, EventArgs e)
        {
            var quizResult = QuizResults.FirstOrDefault();
            if (quizResult == null)
            {
                QuestionLoaded = true;
                RaisePropertyChanged("QuestionLoaded");
                RaisePropertyChanged("QuestionNotLoaded");
                return;
            }
            SelectedQuizResult = quizResult;
        }

        void result_Completed(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentUser");
        }

        private void loadQuizLog(QuizLogDTO selectedQuizResult)
        {
            QuestionLoaded = false;
            RaisePropertyChanged("QuestionLoaded");
            RaisePropertyChanged("QuestionNotLoaded");
            if (selectedQuizResult == null)
            {
                QuestionLoaded = true;
                RaisePropertyChanged("QuestionLoaded");
                RaisePropertyChanged("QuestionNotLoaded");
                return;
            }
            _resultQuizContext.QuestionLogs.Clear();
            var questionResult = _resultQuizContext.Load(_resultQuizContext.GetStudentQuestionLogQuery(selectedQuizResult.EntityId));
            questionResult.Completed += new EventHandler(questionResult_Completed);
            RaisePropertyChanged("Questions");
        }

        void questionResult_Completed(object sender, EventArgs e)
        {
            QuestionLoaded = true;
            RaisePropertyChanged("QuestionLoaded");
            RaisePropertyChanged("QuestionNotLoaded");
        }

        #endregion

        #region Properties

        public EntitySet<QuizLogDTO> QuizResults
        {
            get
            {
                return _resultQuizContext.QuizLogDTOs;
            }
        }

        public User CurrentUser
        {
            get
            {
                return _userContext.Users.FirstOrDefault();
            }
        }

        public QuizLogDTO SelectedQuizResult
        {
            set
            {
                _selectedQuizResult = value;
                RaisePropertyChanged("SelectedQuizResult");
                loadQuizLog(_selectedQuizResult);
            }
            get
            {
                return _selectedQuizResult;
            }
        }

        public EntitySet<QuestionLog> Questions
        {
            get
            {
                return _resultQuizContext.QuestionLogs;
            }
        }

        public bool QuizLoaded { get; set; }

        public bool QuestionLoaded
        {
            get;
            set;
        }

        public bool QuestionNotLoaded
        {
            get
            {
                return !QuestionLoaded;
            }
        }

        #endregion
    }
}
