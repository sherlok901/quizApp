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
using MRZS.Web.Services;
using System.ServiceModel.DomainServices.Client;
using System.Threading;

namespace MRZS.Views.Coordinator.Popups
{
    public partial class DefineStudentSection : ChildWindow
    {
        private SectionContext sectionService = new SectionContext();
        private UserSectionContext userSectionService = new UserSectionContext();

        private IEnumerable<Web.Models.Section> sections;

        public MRZS.Web.Models.User CurrentUser { get; set; }

        public DefineStudentSection()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(DefineStudentSection_Loaded);
        }

        void DefineStudentSection_Loaded(object sender, RoutedEventArgs e)
        {
            var userSectionLoadResult = sectionService.Load(sectionService.GetCurrentStudentSectionQuery(CurrentUser.UserId));
            userSectionLoadResult.Completed += new EventHandler(userSectionLoadResult_Completed);
        }

        void userSectionLoadResult_Completed(object sender, EventArgs e)
        {
            var loadResult = sender as LoadOperation<Web.Models.Section>;
            if (loadResult == null)
                return;
            sections = loadResult.Entities;
            gridSection.ItemsSource = sections;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Web.Models.Section selectedSection = gridSection.SelectedItem as Web.Models.Section;
            if (selectedSection == null)
                return;

            Web.Models.UserSection newUserSection = new Web.Models.UserSection();
            newUserSection.SectionId = selectedSection.SectionId;
            newUserSection.UserId = CurrentUser.UserId;
            userSectionService.UserSections.Add(newUserSection);
            while (userSectionService.IsSubmitting)
            {
                Thread.Sleep(10);
            }
            var changes = userSectionService.SubmitChanges();
            changes.Completed += new EventHandler(changes_Completed);

        }

        void changes_Completed(object sender, EventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void sectionDomainDataSource_LoadedData(object sender, LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

    }
}

