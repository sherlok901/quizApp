using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using MRZS.Web.Services;
using MRZS.Web.Models;
using System.ServiceModel.DomainServices.Client;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace MRZS.Models
{
    public class BookViewModel : BaseViewModel
    {
        #region Fields

        private PageContext pageContext;
        private QuizContext quizContext;
        private BookContext bookContext;
        private SectionViewModel selectedBook;
        private Section selectedSection;
        private Page selectedPage;

        private ICommand deleteBookCommand;
        private ICommand createBookCommand;
        private ICommand editBookCommand;
        private ICommand pageUpCommand;
        private ICommand pageDownCommand;
        private ICommand savePagesChangesCommand;
        private ICommand deletePageCommand;
        #endregion

        #region Properties

        public Page SelectedPage
        {
            get { return selectedPage; }
            set { 
                
                selectedPage = value;
                RaisePropertyChanged("SelectedPage");
            }
        }
        public ICommand DeletePageCommand
        {
            get { return deletePageCommand; }
            set { deletePageCommand = value; }
        }
        public ICommand SavePagesChangesCommand
        {
            get { return savePagesChangesCommand; }
            set { savePagesChangesCommand = value; }
        }
        public ICommand PageUpCommand
        {
            get { return pageUpCommand; }
            set { pageUpCommand = value; }
        }
        public ICommand PageDownCommand
        {
            get { return pageDownCommand; }
            set { pageDownCommand = value; }
        }
        public ICommand EditBookCommand
        {
            get { return editBookCommand; }
        }
        public ICommand CreateBookCommand
        {
            get { return createBookCommand; }
        }
        public ICommand DeleteBookCommand
        {
            get { return deleteBookCommand; }
        }

        public bool IsBookLoaded { get; set; }

        public ObservableCollection<SectionViewModel> Books
        {
            get
            {
                return new ObservableCollection<SectionViewModel>(from book in bookContext.Books
                                                                  select new SectionViewModel(book, this));
            }
        }

        public SectionViewModel SelectedBook
        {
            get
            {
                return selectedBook;
            }
            set
            {
                selectedBook = value;
                if (bookContext.HasChanges)
                    bookContext.SubmitChanges();
            }
        }

        public Section SelectedSection
        {
            get
            {
                return selectedSection;
            }
            set
            {
                selectedSection = value;
                loadPages();
                loadQuizes();
            }
        }

        public List<Page> Pages
        {
            get
            {
                //var pagesEntitySet = new EntitySet<Page>();
                return pageContext.Pages.OrderBy(p => p.PageOrder).ToList();
            }
        }

        public EntitySet<Quiz> Quizes
        {
            get
            {
                return quizContext.Quizs;
            }
        }
        #endregion

        #region HelperMethods

        private void loadQuizes()
        {
            if (SelectedSection == null)
                return;
            quizContext.Quizs.Clear();
            quizContext.Load(quizContext.GetQuizsForSectionQuery(SelectedSection.SectionId));
            RaisePropertyChanged("Quizes");
        }

        private void loadPages()
        {
            IsBookLoaded = false;
            if (SelectedSection == null)
                return;
            pageContext.Pages.Clear();
            var pageLoadResult = pageContext.Load(pageContext.GetPagesForSectionQuery(SelectedSection.SectionId));
            pageLoadResult.Completed += new EventHandler(pageLoadResult_Completed);
        }

        void pageLoadResult_Completed(object sender, EventArgs e)
        {
            IsBookLoaded = true;
            RaisePropertyChanged("Pages");
        }

        private void loadBooks()
        {
            bookContext.Books.Clear();
            var loadBookResult = bookContext.Load(bookContext.GetBooksQuery());
            loadBookResult.Completed += new EventHandler(loadBookResult_Completed);
        }

        void deleteOperationResult_Completed(object sender, EventArgs e)
        {
            loadBooks();
        }

        void loadBookResult_Completed(object sender, EventArgs e)
        {
            IsBookLoaded = true;
            RaisePropertyChanged("Books");
        }
        #endregion

        public BookViewModel()
        {
            bookContext = new BookContext();
            var loadBookResult = bookContext.Load(bookContext.GetBooksQuery());
            loadBookResult.Completed += new EventHandler(loadBookResult_Completed);

            pageContext = new PageContext();
            quizContext = new QuizContext();
            DefineCommands();
        }

        private void DefineCommands()
        {
            deleteBookCommand = new DelegateCommand<SectionViewModel>(OnDeleteCommand, CanDeleteCommand);
            createBookCommand = new DelegateCommand<string>(OnCreateBook, OnCanCreateBool);
            editBookCommand = new DelegateCommand<SectionViewModel>(OnEditBook);
            savePagesChangesCommand = new DelegateCommand<Page>(OnSavePagesChanges);
            pageUpCommand = new DelegateCommand<Page>(OnPageUp, OnCanPageUp);
            pageDownCommand = new DelegateCommand<Page>(OnPageDown, OnCanPageDown);
            deletePageCommand = new DelegateCommand<Page>(OnDeletePage, OnCanDeletePage);
        }

       

        public void RefreshQuizes()
        {
            loadQuizes();
        }
       
        public void RefreshBooks()
        {
            loadPages();
        }

        private void OnSavePagesChanges(object parameter)
        {
            if (!pageContext.HasChanges)
                return;
            if (Pages.Count > 0)
            {
                int i = 0;
                for (i = 0; i < Pages.Count - 1; i++)
                {
                    Pages[i].IsLastPage = false;
                }
                Pages[i].IsLastPage = true;
            }
            var submitPageSaving = pageContext.SubmitChanges();
            submitPageSaving.Completed += new EventHandler(submitPageSaving_Completed);
        }

        void submitPageSaving_Completed(object sender, EventArgs e)
        {
            RaisePropertyChanged("Pages");
        }
        
        void submitPageSavingReload_Completed(object sender, EventArgs e)
        {
            loadPages();
        }

        private bool OnCanDeletePage(Page page)
        {
            return page != null;
        }

        private void OnDeletePage(Page page)
        {
            page.IsDeleted = true;
            foreach (var currentPage in Pages.Where(p => p.PageOrder > page.PageOrder))
            {
                currentPage.PageOrder--;
            }

            var submitPageSaving = pageContext.SubmitChanges();
            submitPageSaving.Completed += new EventHandler(submitPageSavingReload_Completed);
        }

        private void OnEditBook(SectionViewModel book)
        {
            var deleteOperationResult = bookContext.SubmitChanges();
            deleteOperationResult.Completed += new EventHandler(deleteOperationResult_Completed);
        }

        private void OnCreateBook(string bookName)
        {
            bookContext.Books.Add(new Book() { Name = bookName });
            var loadBookResult = bookContext.SubmitChanges();
            loadBookResult.Completed += new EventHandler(loadBookResult_Completed);
        }

        private bool OnCanCreateBool(string bookName)
        {
            return !string.IsNullOrWhiteSpace(bookName);
        }

        private bool OnCanPageDown(Page page)
        {
            return page != null && page.PageOrder > 1;
        }

        private void OnPageDown(Page page)
        {
            int currentOrder = page.PageOrder.Value;
            var nextPage = Pages.FirstOrDefault(p => p.PageOrder < currentOrder);

            if (nextPage == null)
                throw new Exception("Произошла непредвиденная ошибка. Обратитесь за помощью к администратору.");

            page.PageOrder--;
            nextPage.PageOrder++;
            RaisePropertyChanged("Pages");
        }

        private bool OnCanPageUp(Page page)
        {
            return page != null && Pages.Count != page.PageOrder;
        }

        private void OnPageUp(Page page)
        {
            int currentOrder = page.PageOrder.Value;
            var nextPage = Pages.SingleOrDefault(p => p.PageOrder > currentOrder);

            if (nextPage == null)
                throw new Exception("Произошла непредвиденная ошибка. Обратитесь за помощью к администратору.");

            page.PageOrder++;
            nextPage.PageOrder--;
            RaisePropertyChanged("Pages");
        }

        private bool CanDeleteCommand(SectionViewModel book)
        {
            return book != null;
        }

        private void OnDeleteCommand(SectionViewModel book)
        {
            book.CurrentBook.IsDeleted = true;
            var deleteOperationResult = bookContext.SubmitChanges();
            deleteOperationResult.Completed += new EventHandler(deleteOperationResult_Completed);
        }

        public void LoadQuizes(Section selectedSection)
        {
            SelectedSection = selectedSection;
        }
    }
}
