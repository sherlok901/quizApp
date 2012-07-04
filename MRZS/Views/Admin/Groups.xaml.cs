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
using MRZS.Views.Admin.Popups;
using MRZS.Web.Services;
using System.ServiceModel.DomainServices.Client;

namespace MRZS.Views.Admin
{
    public partial class Groups : Page
    {
        private GroupContext groupService = new GroupContext();

        public Groups()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Groups_Loaded);
        }

        void Groups_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGroups();
        }

        private void LoadGroups()
        {
            var loadGroupsResult = groupService.Load(groupService.GetGroupsQuery());
            loadGroupsResult.Completed += new EventHandler(loadGroupsResult_Completed);
        }

        void loadGroupsResult_Completed(object sender, EventArgs e)
        {
            var loadGroupsResult = sender as LoadOperation<Web.Models.Group>;
            groupDataGrid.ItemsSource = loadGroupsResult.Entities;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void buttonNewGroup_Click(object sender, RoutedEventArgs e)
        {
            CreateNewGroup newGroupPopup = new CreateNewGroup();
            newGroupPopup.Show();
            newGroupPopup.Closed += new EventHandler(newGroupPopup_Closed);
        }

        void newGroupPopup_Closed(object sender, EventArgs e)
        {
            LoadGroups();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedGroup = groupDataGrid.SelectedItem as Web.Models.Group;
            if (selectedGroup == null)
                return;
            selectedGroup.IsDeleted = true;
            var submitingChages = groupService.SubmitChanges();
            submitingChages.Completed += new EventHandler(submitingChages_Completed);
        }

        void submitingChages_Completed(object sender, EventArgs e)
        {
            LoadGroups();
        }

        private void groupDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                buttonDeleteGroup.IsEnabled = false;
                return;
            }
            buttonDeleteGroup.IsEnabled = true;
        }

    }
}
