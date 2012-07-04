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

namespace MRZS.Views.Admin.Popups
{
    public partial class NewQuestion : ChildWindow
    {
        private QuestionContext questionService = new QuestionContext();
        private Question question { get; set; }
        public Quiz Test { get; set; }
        
        public NewQuestion()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxQuestionText.Text))
                return;
            question = new Question();
            question.Text = textBoxQuestionText.Text;
            question.QuizId = Test.QuizId;
            questionService.Questions.Add(question);
            var savingResult = questionService.SubmitChanges();
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

