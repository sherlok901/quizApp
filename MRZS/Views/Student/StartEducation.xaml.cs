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
using C1.Silverlight.MediaPlayer;
using System.ServiceModel.DomainServices.Client;
using System.Windows.Browser;

namespace MRZS.Views.Student
{
    public partial class StartEducation : Page
    {
        private PageContext pageService = new PageContext();
        private SectionContext sectionService = new SectionContext();
        private InterectiveTestContext interectiveTestService = new InterectiveTestContext();
        private MediaContentContext mediaService = new MediaContentContext();
        private bool isInterective = false;
        private IEnumerable<MRZS.Web.Models.Page> loadedPage;
        public StartEducation()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(StartEducation_Loaded);
        }
        public bool IsEncodeNeeded { get { return false; } }
        void StartEducation_Loaded(object sender, RoutedEventArgs e)
        {

            var pageLoadResult = pageService.Load(pageService.GetNextPageForStudentQuery(-1, WebContext.Current.User.Name));
            loadedPage = pageLoadResult.Entities;
            pageLoadResult.Completed += new EventHandler(pageLoadResult_Completed);

        }

        void pageLoadResult_Completed(object sender, EventArgs e)
        {
            bool pageExist = loadedPage.Any();
            if (pageExist)
            {
                var page = loadedPage.First();


                try
                {
                    //Uri Uri = new Uri(string.Format("{0}://{1}:{2}/{3}", App.Current.Host.Source.Scheme, App.Current.Host.Source.Host, App.Current.Host.Source.Port,
                    //    HttpUtility.UrlEncode(page.PagePath.Substring(page.PagePath.IndexOf("MRZS\\") + 7).Replace("\\", "/")), UriKind.Absolute));

                    Uri pageUri = buildUriToPage(page);
                    if (checkBoxOrigination.IsChecked.HasValue && checkBoxOrigination.IsChecked.Value)
                    {
                        text.Text = pageUri.OriginalString;
                    }
                    else
                        text.Text = pageUri.AbsoluteUri;

                    htmlHost.Navigate(new UriBuilder(pageUri).Uri);

                }
                catch
                {
                    htmlHost.NavigateToString("Текущий файл недоступен. Обратитесь за помощью к администратору.");
                }

                //htmlHost.Navigate(new Uri(page.PagePath, UriKind.Relative));
                buttonPreviousPage.IsEnabled = page.PageOrder > 1;
                buttonNextPage.IsEnabled = !(page.IsLastPage.HasValue && page.IsLastPage.Value);
                buttonStartTest.IsEnabled = page.IsLastPage.HasValue && page.IsLastPage.Value;
                LoadPageMediaContent(page.PageId);

                var interectiveTestLoadResult = interectiveTestService.Load(interectiveTestService.GetInterectiveTestForSectionRandomQuery(page.SectionId));
                interectiveTestLoadResult.Completed += new EventHandler(interectiveTestLoadResult_Completed);
            }
            else
            {
                buttonNextPage.IsEnabled = false;
            }
        }

        private Uri buildUriToPage(MRZS.Web.Models.Page page)
        {
            string reletivePath = string.Empty;
            if (IsEncodeNeeded)
            {
                reletivePath = HttpUtility.UrlEncode(page.PagePath.Substring(page.PagePath.IndexOf("MRZS\\") + 7).Replace("\\", "/"));
            }
            else
            {
                reletivePath = page.PagePath.Substring(page.PagePath.IndexOf("MRZS\\") + 7).Replace("\\", "/");
            }

            string absolutePath = string.Format("{0}://{1}:{2}", App.Current.Host.Source.Scheme, App.Current.Host.Source.Host, App.Current.Host.Source.Port);

            Uri Uri = new Uri(string.Format("{0}/{1}", absolutePath, reletivePath), UriKind.Absolute);

            return Uri;


        }

        void interectiveTestLoadResult_Completed(object sender, EventArgs e)
        {
            var loadResult = sender as LoadOperation<Web.Models.InterectiveTest>;
            isInterective = loadResult.Entities.Any();

        }

        private void LoadPageMediaContent(int pageId)
        {

            var mediaLoadResult = mediaService.Load(mediaService.GetMultimediaContentsForPageQuery(pageId));
            mediaLoadResult.Completed += new EventHandler(mediaLoadResult_Completed);

        }

        void mediaLoadResult_Completed(object sender, EventArgs e)
        {
            var result = sender as LoadOperation<Web.Models.MultimediaContent>;
            mediaPlayer.Items.Clear();
            foreach (var item in result.Entities)
            {
                try
                {
                    C1MediaItem C1MediaItem = new C1MediaItem();
                    Uri Uri = new Uri(string.Format("{0}://{1}:{2}/{3}", App.Current.Host.Source.Scheme, App.Current.Host.Source.Host, App.Current.Host.Source.Port, item.Path));
                    C1MediaItem.MediaSource = Uri;
                    C1MediaItem.Title = item.ContentName;
                    mediaPlayer.Items.Add(C1MediaItem);
                    mediaPlayer.Play();
                }
                catch
                {
                    throw new Exception("Неправельный путь к медиа контенту! Обратитесь за помощью к администратору.");
                }
            }
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void buttonNextPage_Click(object sender, RoutedEventArgs e)
        {
            var currentPage = loadedPage.FirstOrDefault();
            if (currentPage == null)
                return;

            var pageLoadResult = pageService.Load(pageService.GetNextPageForStudentQuery(currentPage.PageId, WebContext.Current.User.Name));
            loadedPage = pageLoadResult.Entities;
            pageLoadResult.Completed += new EventHandler(pageLoadResult_Completed);
        }

        private void buttonPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            var currentPage = loadedPage.FirstOrDefault();
            if (currentPage == null)
                return;

            var pageLoadResult = pageService.Load(pageService.GetPreviousPageForStudentQuery(currentPage.PageId, WebContext.Current.User.Name));
            loadedPage = pageLoadResult.Entities;
            pageLoadResult.Completed += new EventHandler(pageLoadResult_Completed);
        }

        private void buttonStartTest_Click(object sender, RoutedEventArgs e)
        {
            if (isInterective)
            {
                if (loadedPage.First().SectionId == 33)
                {
                    NavigationService.Navigate(new Uri("/InteractiveTests/Tests/Kurs-93-m/InterectiveTestKurs93M", UriKind.Relative));
                }
                else
                {
                    NavigationService.Navigate(new Uri(string.Format("/InteractiveTests/InteractiveTest?sectionId={0}&index=0", loadedPage.First().SectionId), UriKind.Relative));
                }
                return;
            }
            NavigationService.Navigate(new Uri(string.Format("/Student/Test?SectionId={0}", loadedPage.First().SectionId), UriKind.Relative));
        }

        private void bb1_Click(object sender, RoutedEventArgs e)
        {
            htmlHost.Navigate(new Uri(text.Text));
        }

    }
}
