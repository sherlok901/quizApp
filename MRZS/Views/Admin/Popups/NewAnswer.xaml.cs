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
using MRZS.Web.Models;
using MRZS.Web.Services;
using System.ServiceModel.DomainServices.Client;

namespace MRZS.Views.Admin.Popups
{
    public partial class NewAnswer : ChildWindow
    {
        public Question Question { get; set; }
        private AnswerContext answerService = new AnswerContext();

        public NewAnswer()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Answer answer = new Answer();
            answer.AnswerText = textBoxAnswer.Text;
            answer.IsCorrect = checkBoxIsCorrect.IsChecked.Value;
            answer.QuestionId = Question.QuestionId;
            answerService.Answers.Add(answer);
            var savingResult = answerService.SubmitChanges();
            savingResult.Completed += new EventHandler(savingResult_Completed);
        }

        void savingResult_Completed(object sender, EventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

