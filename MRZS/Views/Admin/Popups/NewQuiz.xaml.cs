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
    public partial class NewQuiz : ChildWindow
    {
        private QuizContext quizService = new QuizContext();
        private QuestionContext questionService = new QuestionContext();

        public int SectionId { get; set; }
        public Quiz NewTest { get; set; }
        public EntitySet<Answer> Answers
        {
            get
            {
                return answerService.Answers;
            }
        }
        public bool EditMode { get; set; }
        private int selectedQuestionId;
        private AnswerContext answerService = new AnswerContext();

        public NewQuiz()
        {
            InitializeComponent();
            if (!EditMode)
                NewTest = new Quiz();
            this.Loaded += new RoutedEventHandler(NewQuiz_Loaded);
        }

        void NewQuiz_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EditMode)
            {
                NewTest.SectionId = SectionId;
                quizService.Quizs.Add(NewTest);
                quizService.SubmitChanges();
            }
            else
            {
                NewTest = null;
                var quizLoad = quizService.Load(quizService.GetQuizsForSectionQuery(SectionId));
                quizLoad.Completed += new EventHandler(quizLoad_Completed);
            }
        }

        void quizLoad_Completed(object sender, EventArgs e)
        {
            NewTest = quizService.Quizs.First();
            if (!string.IsNullOrEmpty(NewTest.QuizName))
                textBoxQuizName.Text = NewTest.QuizName;
            if (NewTest.SimpleDifficulty.HasValue)
                numericUpDownDifficultySimple.Value = NewTest.SimpleDifficulty.Value;
            if (NewTest.MediumDifficulty.HasValue)
                numericUpDownDifficultyMedium.Value = NewTest.MediumDifficulty.Value;
            if (NewTest.HardDifficulty.HasValue)
                numericUpDownDifficultyHard.Value = NewTest.HardDifficulty.Value;
            dataGridQuestions.SelectionChanged += new SelectionChangedEventHandler(dataGridQuestions_SelectionChanged);
            dataGridAnswers.SelectionChanged += new SelectionChangedEventHandler(dataGridAnswers_SelectionChanged);
            RefreshQuestions();
        }

        void dataGridAnswers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                buttonDeleteAnswer.IsEnabled = false;
                return;
            }
            buttonDeleteAnswer.IsEnabled = true;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            NewTest.QuizName = textBoxQuizName.Text;
            NewTest.SimpleDifficulty = (int)numericUpDownDifficultySimple.Value;
            NewTest.MediumDifficulty = (int)numericUpDownDifficultyMedium.Value;
            NewTest.HardDifficulty = (int)numericUpDownDifficultyHard.Value;
            var updatingResult = quizService.SubmitChanges();
            updatingResult.Completed += new EventHandler(updatingResult_Completed);
            answerService.SubmitChanges();
            questionService.SubmitChanges();
        }

        void updatingResult_Completed(object sender, EventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var resultChangingQuiz = quizService.SubmitChanges();
            resultChangingQuiz.Completed += new EventHandler(resultChangingQuiz_Completed);
        }

        void resultChangingQuiz_Completed(object sender, EventArgs e)
        {
            if (!EditMode)
            {
                var quizLoadResult = quizService.Load(quizService.GetQuizsByIdQuery(NewTest.QuizId));
                quizLoadResult.Completed += new EventHandler(quizLoadResult_Completed);
            }
            else
            {
                this.DialogResult = false;
            }
        }

        void quizLoadResult_Completed(object sender, EventArgs e)
        {
            NewTest.IsDeleted = true;
            quizService.SubmitChanges();
            this.DialogResult = false;
        }

        #region Answers

        private void buttomAddNewAnswer_Click(object sender, RoutedEventArgs e)
        {
            NewAnswer newAnswerPopup = new NewAnswer();
            newAnswerPopup.Question = (dataGridQuestions.SelectedItem as Question);
            newAnswerPopup.Show();
            newAnswerPopup.Closed += new EventHandler(newAnswerPopup_Closed);
        }

        void newAnswerPopup_Closed(object sender, EventArgs e)
        {
            RefreshAnswerGrid(selectedQuestionId);
        }

        private void RefreshAnswerGrid(int questionId)
        {
            answerService.Answers.Clear();
            var answerLoadResult = answerService.Load(answerService.GetAnswersForQuestionQuery(questionId));
            answerLoadResult.Completed += new EventHandler(answerLoadResult_Completed);
        }

        void answerLoadResult_Completed(object sender, EventArgs e)
        {
            dataGridAnswers.ItemsSource = null;
            dataGridAnswers.ItemsSource = answerService.Answers.Where(a => a.QuestionId == selectedQuestionId);
        }

        #endregion

        #region Questions

        private void dataGridQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                ShowQuestionDependentButtons(false);
                return;
            }
            ShowQuestionDependentButtons(true);
            var selectedQuestion = (e.AddedItems[0] as Question);
            selectedQuestionId = selectedQuestion.QuestionId;
            RefreshAnswerGrid(selectedQuestionId);
        }

        public Question SelectedQuestion
        {
            get
            {
                return dataGridQuestions.SelectedItem as Question;
            }
        }

        public void ShowQuestionDependentButtons(bool show)
        {
            buttonDeleteQuestion.IsEnabled =
            buttomAddNewAnswer.IsEnabled = show;
        }

        private void RefreshQuestions()
        {
            var questionsLoadResult = questionService.Load(questionService.GetQuestionsForQuizQuery(NewTest.QuizId));
            questionsLoadResult.Completed += new EventHandler(questionsLoadResult_Completed);
        }

        void questionsLoadResult_Completed(object sender, EventArgs e)
        {
            var questionsLoadResult = sender as LoadOperation<Question>;
            dataGridQuestions.ItemsSource = null;
            dataGridQuestions.ItemsSource = questionsLoadResult.Entities;
        }

        private void buttomAddNewQuestion_Click(object sender, RoutedEventArgs e)
        {
            NewQuestion newQpopup = new NewQuestion();
            newQpopup.Test = NewTest;
            newQpopup.Show();

            newQpopup.Closing += new EventHandler<System.ComponentModel.CancelEventArgs>(newQpopup_Closing);
        }

        void newQpopup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefreshQuestions();
        }

        #endregion

        private void buttonDeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedQuestion == null)
                return;
            SelectedQuestion.IsDeleted = true;
            SaveQuestionChanges(RefreshQuestions);
        }

        public void SaveQuestionChanges(Action AfterSave)
        {
            var saveCompleted = questionService.SubmitChanges();
            saveCompleted.Completed += new EventHandler(saveCompleted_Completed);
        }

        void saveCompleted_Completed(object sender, EventArgs e)
        {
            RefreshQuestions();
        }

        private Answer selectedAnswer
        {
            get
            {
                return dataGridAnswers.SelectedItem as Answer;
            }
        }

        private void buttonDeleteAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAnswer == null)
                return;
            selectedAnswer.IsDeleted = true;
            var answerChangesSaving = answerService.SubmitChanges();
            answerChangesSaving.Completed += new EventHandler(answerChangesSaving_Completed);
        }

        void answerChangesSaving_Completed(object sender, EventArgs e)
        {
            RefreshAnswerGrid(selectedQuestionId);
        }
    }
}

