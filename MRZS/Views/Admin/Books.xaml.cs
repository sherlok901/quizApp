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
using MRZS.Web.Models;
using MRZS.Views.Admin.Popups;
using System.ServiceModel.DomainServices.Client;
using MRZS.Models;
using Microsoft.Windows.Controls;

namespace MRZS.Views.Admin
{
    public partial class Books : System.Windows.Controls.Page
    {
        private SectionContext sectionService = new SectionContext();
        private PageContext pageService = new PageContext();
        private QuizContext quizService = new QuizContext();
        private BookContext bookService = new BookContext();

        private BookViewModel bookViewModel;

        public Books()
        {
            this.Loaded += new RoutedEventHandler(Books_Loaded);
            InitializeComponent();
        }

        void Books_Loaded(object sender, RoutedEventArgs e)
        {
            bookViewModel = new BookViewModel();
            this.DataContext = bookViewModel;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        #region Quizes



        private void buttonDeleteTest_Click(object sender, RoutedEventArgs e)
        {
            //if (SelectedQuiz == null)
            //    return;
            //SelectedQuiz.IsDeleted = true;
            //var submited = quizService.SubmitChanges();
            //submited.Completed += new EventHandler(submited_Completed);

        }


        private void buttonNewTest_Click(object sender, RoutedEventArgs e)
        {
            //NewQuiz newQuizPopup = new NewQuiz();
            //MRZS.Web.Models.Section currentSection = (listBoxSections.SelectedValue as MRZS.Web.Models.Section);
            //if (currentSection == null)
            //    return;
            //newQuizPopup.SectionId = currentSection.SectionId;
            //newQuizPopup.Show();
            //newQuizPopup.Closed += new EventHandler(newQuizPopup_Closed);
        }


        private void buttonEditTest_Click(object sender, RoutedEventArgs e)
        {
            NewQuiz newQuizPopup = new NewQuiz();
            newQuizPopup.SectionId = bookViewModel.SelectedSection.SectionId;
            bool newTest = bookViewModel.Quizes.Count == 0;
            if (!newTest)
            {
                newQuizPopup.NewTest = bookViewModel.Quizes.First();
                newQuizPopup.EditMode = true;
            }

            newQuizPopup.Show();
            newQuizPopup.Closed += new EventHandler(newQuizPopup_Closed);
        }
        #endregion


        void newQuizPopup_Closed(object sender, EventArgs e)
        {
            bookViewModel.RefreshQuizes();
        }

        void ShowNewPagePopup(bool editMode)
        {
            NewPage newPagePopup = new NewPage();
            if (bookViewModel.SelectedPage == null && editMode)
                return;
            newPagePopup.SelectedSection = bookViewModel.SelectedSection;
            if (editMode)
            {
                newPagePopup.CurrentPageId = bookViewModel.SelectedPage.PageId;
                newPagePopup.EditMode = true;
            }
            else
            {
                newPagePopup.PageOrder = bookViewModel.Pages.Count + 1;
            }
            newPagePopup.Closed += new EventHandler(newPagePopup_Closed);
            newPagePopup.Show();
        }

        void newPagePopup_Closed(object sender, EventArgs e)
        {
            bookViewModel.RefreshBooks();
        }

        private void buttonAddPage_Click(object sender, RoutedEventArgs e)
        {
            ShowNewPagePopup(false);
        }


        private void buttonEditPage_Click(object sender, RoutedEventArgs e)
        {
            ShowNewPagePopup(true);
        }

        private void dataGridBooks_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {
            bookViewModel.EditBookCommand.Execute(bookViewModel.SelectedBook);
        }

        private void dataGridBooks_LoadedRowDetailsPresenter(object sender, C1.Silverlight.DataGrid.DataGridRowDetailsEventArgs e)
        {
            var sectionViewModel = (e.DetailsElement.DataContext as SectionViewModel);
            if (sectionViewModel == null)
                return;
            sectionViewModel.LoadSectios();
        }

        private void textBoxBookName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                buttonCreateNewBook.Command.Execute(textBoxBookName.Text);
        }

        private void textBoxSectionName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;
            var textBoxSectionName = sender as WatermarkedTextBox;
            var sectionViewModel = textBoxSectionName.DataContext as SectionViewModel;
            if (sectionViewModel == null)
                return;
            sectionViewModel.OnCreateSection(textBoxSectionName.Text);
        }

        private void ButtonMoveQuestion_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Admin/Popups/MoveQuestion?sectionId=" + bookViewModel.SelectedSection.SectionId, UriKind.Relative));
        }

    }
}
