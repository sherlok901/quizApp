using System;
using System.Net;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using MRZS.Web.Models;
using System.ServiceModel.DomainServices.Client;
using MRZS.Web.Services;

namespace MRZS.Models
{
    public class SectionViewModel : BaseViewModel
    {
        private ICommand deleteSectionCommand;
        private ICommand createSectionCommand;
        private Section selectedSection;
        private BookViewModel bookViewModel;
        private SectionContext sectionContext;

        public Book CurrentBook { get; set; }

        public ICommand CreateSectionCommand
        {
            get { return createSectionCommand; }
        }
        public ICommand DeleteSectionCommand
        {
            get { return deleteSectionCommand; }
        }
        public Section SelectedSection
        {
            get { return selectedSection; }
            set
            {
                if (sectionContext.HasChanges)
                    sectionContext.SubmitChanges();

                if (selectedSection != value)
                    bookViewModel.LoadQuizes(value);

                selectedSection = value;
            }
        }

        public EntitySet<Section> Sections
        {
            get
            {
                return sectionContext.Sections;
            }
        }

        public SectionViewModel(Book book, BookViewModel currentBookViewModel)
        {
            this.bookViewModel = currentBookViewModel;
            this.CurrentBook = book;
            sectionContext = new SectionContext();
            defineCommands();
        }

        public void LoadSectios()
        {
            sectionContext.Sections.Clear();
            var loadSectionResult = sectionContext.Load(sectionContext.GetSectionsForBookQuery(CurrentBook.BookId));
            loadSectionResult.Completed += new EventHandler(loadSectionResult_Completed);
        }

        void loadSectionResult_Completed(object sender, EventArgs e)
        {
            RaisePropertyChanged("Sections");
        }

        private void loadSection()
        {
            if (CurrentBook == null)
                return;
            sectionContext.Sections.Clear();
            sectionContext.Load(sectionContext.GetSectionsForBookQuery(CurrentBook.BookId));
            RaisePropertyChanged("Sections");
        }

        private void defineCommands()
        {
            createSectionCommand = new DelegateCommand<string>(OnCreateSection, OnCanCreateSection);
            deleteSectionCommand = new DelegateCommand<Section>(OmDeleteSection, OnCanDeleteSection);
        }

        public void OmDeleteSection(Section section)
        {
            section.IsDeleted = true;
            var sectionSubmitionResult = sectionContext.SubmitChanges();
            sectionSubmitionResult.Completed += new EventHandler(sectionSubmitionResult_Completed);
        }

        public bool OnCanDeleteSection(Section section)
        {
            return section != null;
        }

        public void OnCreateSection(string sectionName)
        {
            if (!string.IsNullOrWhiteSpace(sectionName))
            {
                sectionContext.Sections.Add(new Section() { Name = sectionName, BookId = CurrentBook.BookId });
                sectionContext.SubmitChanges();
            }
        }

        private bool OnCanCreateSection(string sectionName)
        {
            return !string.IsNullOrWhiteSpace(sectionName);
        }

        void sectionSubmitionResult_Completed(object sender, EventArgs e)
        {
            loadSection();
        }

    }
}
