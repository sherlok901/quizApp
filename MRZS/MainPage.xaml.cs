namespace MRZS
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using MRZS.LoginUI;
    using System;

    /// <summary>
    /// <see cref="UserControl"/> class providing the main UI for the application.
    /// </summary>
    public partial class MainPage : UserControl
    {
        /// <summary>
        /// Creates a new <see cref="MainPage"/> instance.
        /// </summary>
        public MainPage()
        {
            WebContext.Current.Authentication.LoggedIn += new System.EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedIn);
            WebContext.Current.Authentication.LoggedOut += new System.EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedOut);
            InitializeComponent();
            this.loginContainer.Child = new LoginStatus();

        }

        void Authentication_LoggedOut(object sender, System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs e)
        {
            ShowRoleSpecificContent();
            ContentFrame.Navigate(new Uri("/Home", UriKind.Relative));
        }

        void Authentication_LoggedIn(object sender, System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs e)
        {
            ShowRoleSpecificContent();
        }

        public void ShowRoleSpecificContent()
        {
            
            LinkEducation.Visibility =
            LinkCoordination.Visibility =
            DividerStatistics.Visibility =
            LinkCoordinatorStatistics.Visibility =
            AdminBooks.Visibility =
            AdminGroups.Visibility =
            AdminUsers.Visibility =
            AdminDividerUsers.Visibility =
            DividerGroups.Visibility = System.Windows.Visibility.Collapsed;
            //ShowAdministratorContent();
            ShowCoordinatorContent();
            ShowStudentContent();
        }

        private void ShowAdministratorContent()
        {

            //if (WebContext.Current.User.IsInRole("Admin"))
            //{
                AdminBooks.Visibility = System.Windows.Visibility.Visible;
                AdminGroups.Visibility = System.Windows.Visibility.Visible;
                AdminUsers.Visibility = System.Windows.Visibility.Visible;
                AdminDividerUsers.Visibility = System.Windows.Visibility.Visible;
                DividerGroups.Visibility = System.Windows.Visibility.Visible;
            //}
        }
        private void ShowCoordinatorContent()
        {

            if (WebContext.Current.User.IsInRole("Coordinator"))
            {
                LinkCoordinatorStatistics.Visibility = System.Windows.Visibility.Visible;
                DividerStatistics.Visibility = System.Windows.Visibility.Visible;
                AdminDividerCoordinator.Visibility = System.Windows.Visibility.Visible;
                LinkCoordination.Visibility = System.Windows.Visibility.Visible;
                ShowAdministratorContent();
            }
        }

        private void ShowStudentContent()
        {
            if (WebContext.Current.User.IsInRole("Student"))
            {
                LinkEducation.Visibility = System.Windows.Visibility.Visible;
            }
        }

        /// <summary>
        /// After the Frame navigates, ensure the <see cref="HyperlinkButton"/> representing the current page is selected
        /// </summary>
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            foreach (UIElement child in LinksStackPanel.Children)
            {
                HyperlinkButton hb = child as HyperlinkButton;
                if (hb != null && hb.NavigateUri != null)
                {
                    if (hb.NavigateUri.ToString().Equals(e.Uri.ToString()))
                    {
                        VisualStateManager.GoToState(hb, "ActiveLink", true);
                    }
                    else
                    {
                        VisualStateManager.GoToState(hb, "InactiveLink", true);
                    }
                }
            }
        }

        /// <summary>
        /// If an error occurs during navigation, show an error window
        /// </summary>
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ErrorWindow.CreateNew(e.Exception);
        }
    }
}