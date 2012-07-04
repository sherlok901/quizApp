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
using System.ComponentModel;
using MRZS.Web.Services;
using System.ServiceModel.DomainServices.Client;
using MRZS.Web.Models;

namespace MRZS.Models.Statistic
{
    public class AnswersStatisticViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private AnswerContext answerContext;
        private ResultAnswerContext resultAnswerContext;
        private int questionId;
        private int quizResultId;

        public AnswersStatisticViewModel(int questionId, int quizResultId)
        {
            this.questionId = questionId;
            this.quizResultId = quizResultId;
            resultAnswerContext = new ResultAnswerContext();
            var resultAnswers = resultAnswerContext.Load(resultAnswerContext.GetResultAnswersForQuizResultQuery(quizResultId));
            answerContext = new AnswerContext();
            resultAnswers.Completed += new EventHandler(resultAnswers_Completed);
        }

        void resultAnswers_Completed(object sender, EventArgs e)
        {
            ResultAnswersLoaded = true;
            answerContext.Load(answerContext.GetAnswersForQuestionQuery(this.questionId));
            RaisePropertyChanged("ResultAnswersLoaded");
        }

        public EntitySet<Answer> Answers
        {
            get
            {
                return answerContext.Answers;
            }
        }

        public bool ResultAnswersLoaded { get; set; }

        public EntitySet<ResultAnswer> ResultAnswers
        {
            get
            {
                return resultAnswerContext.ResultAnswers;
            }
        }

        private void RaisePropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
