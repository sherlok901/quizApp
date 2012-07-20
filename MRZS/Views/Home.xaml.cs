namespace MRZS
{
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using System;
    using MRZS.Web.Resources;

    /// <summary>
    /// Home page for the application.
    /// </summary>
    public partial class Home : Page
    {
        /// <summary>
        /// Creates a new <see cref="Home"/> instance.
        /// </summary>
        public Home()
        {
            InitializeComponent();

            this.Title = ApplicationStrings.HomePageTitle;

            webBrowserHomePage.NavigateToString(RegistrationDataResources.HomePage);
            this.Loaded += new System.Windows.RoutedEventHandler(Home_Loaded);
        }

        void Home_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //WebContext.Current.Authentication.LoggedIn += new EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedIn);           
            NavigationService.Navigate(new Uri("/Emulator/Emulator-05M", UriKind.Relative));
        }

        void Authentication_LoggedIn(object sender, System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs e)
        {
            if (WebContext.Current.User.IsInRole("Student"))
            {
                textBlokHelp.Text = "Для начала обучение нажмите кнопку 'Обучение' в правом верхнем углу экрана.";
            }
        }

        /// <summary>
        /// Executes when the user navigates to this page.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}