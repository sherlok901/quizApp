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
using MRZS.Web;
using MRZS.Web.Services;
using System.ServiceModel.DomainServices.Client;
//using MRZS.Views.Admin.Popups;

namespace MRZS.Views.Admin
{
    public partial class Users : Page
    {
        private UserContext userService = new UserContext();

        public Users()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Users_Loaded);
        }

        void Users_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUserGrid();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewUser newUserPopup = new NewUser();
            newUserPopup.Show();
            newUserPopup.Closed += new EventHandler(newUserPopup_Closed);
            //CreateNewUser newUserPopup = new CreateNewUser();
            //newUserPopup.Show();
            //newUserPopup.Closed += new EventHandler(newUserPopup_Closed);
        }

        void newUserPopup_Closed(object sender, EventArgs e)
        {
            RefreshUserGrid();
        }

        private void RefreshUserGrid()
        {
            userService = new UserContext();
            var loadUsersResult = userService.Load(userService.GetUsersQuery());
            loadUsersResult.Completed += new EventHandler(loadUsersResult_Completed);
        }

        void loadUsersResult_Completed(object sender, EventArgs e)
        {
            var loadUsersResult = sender as LoadOperation<Web.Models.User>;
            gridUsers.ItemsSource = null;
            gridUsers.ItemsSource = loadUsersResult.AllEntities;
        }

        private void gridUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                buttonEditUser.IsEnabled =
                buttonDeleteUser.IsEnabled = false;
                return;
            }
            buttonEditUser.IsEnabled =
            buttonDeleteUser.IsEnabled = true;
        }

        private void ButtonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (userService.IsSubmitting)
                return;
            var selectedUser = gridUsers.SelectedItem as Web.Models.User;
            selectedUser.IsDeleted = true;
            var submitingResult = userService.SubmitChanges();
            submitingResult.Completed += new EventHandler(submitingResult_Completed);
        }

        void submitingResult_Completed(object sender, EventArgs e)
        {
            RefreshUserGrid();
        }

        private void buttonEditUser_Click(object sender, RoutedEventArgs e)
        {
            NewUser newUserPopup = new NewUser();
            newUserPopup.EditMode = true;
            var selectedUser = gridUsers.SelectedItem as Web.Models.User;

            newUserPopup.CurrentUser = selectedUser;
            newUserPopup.Show();
            newUserPopup.Closed += new EventHandler(newUserPopup_Closed);
        }

    }
}
