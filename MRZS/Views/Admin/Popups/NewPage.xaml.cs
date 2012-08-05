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
using System.Threading;
using System.ServiceModel.DomainServices.Client;

namespace MRZS.Views.Admin.Popups
{
    public partial class NewPage : ChildWindow
    {
        public MRZS.Web.Models.Section SelectedSection { get; set; }
        public int PageOrder { get; set; }

        private MRZS.Web.Models.Page page=null;
        private List<Web.Models.MultimediaContent> medias = new List<Web.Models.MultimediaContent>();

        private PageContext pageService = new PageContext();
        private MediaContentContext mediaService = new MediaContentContext();


        public bool EditMode { get; set; }
        public int CurrentPageId { get; set; }

        private MRZS.Web.Models.Page CurrentPage { get; set; }

        public NewPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(NewPage_Loaded);
        }

        void NewPage_Loaded(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
            if (EditMode)
            {
                var pageLoadResult = pageService.Load(pageService.GetPageByIdQuery(CurrentPageId));
                pageLoadResult.Completed += new EventHandler(pageLoadResult_Completed);
            }
            else
            {
                pageOrder.Value = PageOrder;
            }
        }

        void pageLoadResult_Completed(object sender, EventArgs e)
        {
            var pageLoadResult = sender as LoadOperation<Web.Models.Page>;
            CurrentPage = pageLoadResult.Entities.LastOrDefault();
            if (CurrentPage == null)
                return;
            textBoxPagePath.Text = CurrentPage.PagePath;
            //richTextBoxHtmlContent.Text = CurrentPage.PageContent;
            checkBoxLastPage.IsChecked = CurrentPage.IsLastPage;
            pageOrder.Value = CurrentPage.PageOrder.HasValue ? CurrentPage.PageOrder.Value : 1;
            var mediaLoadResult = mediaService.Load(mediaService.GetMultimediaContentsForPageQuery(CurrentPage.PageId));
            mediaLoadResult.Completed += new EventHandler(mediaLoadResult_Completed);
        }

        void mediaLoadResult_Completed(object sender, EventArgs e)
        {
            var loadResult = sender as LoadOperation<Web.Models.MultimediaContent>;

            medias = loadResult.Entities.ToList();

            RefreshContentGrid();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            SavePage();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void buttonAddContent_Click(object sender, RoutedEventArgs e)
        {
            var newMedia = new Web.Models.MultimediaContent();
            newMedia.Path = textBoxFilePath.Text;
            newMedia.ContentName = textBoxContentName.Text;
            medias.Add(newMedia);
            textBoxFilePath.Text = textBoxContentName.Text = "";
            RefreshContentGrid();
        }

        private void RefreshContentGrid()
        {
            gridContents.ItemsSource = null;
            gridContents.ItemsSource = medias;
        }

        private void SavePage()
        {
            //if (!EditMode)
            //{
            //    page = new Web.Models.Page();
            //    page.PagePath = textBoxPagePath.Text;
            //    page.SectionId = SelectedSection.SectionId;
            //    page.PageContent = richTextBoxHtmlContent.Save(Liquid.Format.Text, Liquid.RichTextSaveOptions.None);
            //    page.IsLastPage = checkBoxLastPage.IsChecked;
            //    page.PageOrder = (int)pageOrder.Value;
            //    pageService.Pages.Add(page);
            //}
            //else
            //{
            //    CurrentPage.PagePath = textBoxPagePath.Text;
            //    CurrentPage.PageContent = richTextBoxHtmlContent.Save(Liquid.Format.Text, Liquid.RichTextSaveOptions.None);
            //    CurrentPage.IsLastPage = checkBoxLastPage.IsChecked;
            //    CurrentPage.PageOrder = (int)pageOrder.Value;
            //}
            var submitingResult = pageService.SubmitChanges();
            submitingResult.Completed += new EventHandler(submitingResult_Completed);
        }

        private void SaveContent(Web.Models.Page page)
        {
            foreach (var item in medias)
            {
                if (!EditMode)
                {
                    item.PageId = page.PageId;
                    mediaService.MultimediaContents.Add(item);
                }
                else if (!mediaService.MultimediaContents.Contains(item))
                {
                    item.PageId = CurrentPageId;
                    mediaService.MultimediaContents.Add(item);
                }
            }
            var mediaSaveResult = mediaService.SubmitChanges();
            mediaSaveResult.Completed += new EventHandler(mediaSaveResult_Completed);
        }

        void mediaSaveResult_Completed(object sender, EventArgs e)
        {
            this.DialogResult = true;
        }

        void submitingResult_Completed(object sender, EventArgs e)
        {
            SaveContent(page);
        }

        private void gridContents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBoxContentName.Text =
            textBoxFilePath.Text = "";

            if (e.AddedItems.Count == 0)
            {
                buttonDeleteContent.IsEnabled = false;
                return;
            }
            buttonDeleteContent.IsEnabled = true;
            Web.Models.MultimediaContent selectedMedia = e.AddedItems[0] as Web.Models.MultimediaContent;
            if (selectedMedia == null)
                return;
            if (selectedMedia.ContentName != null)
                textBoxContentName.Text = selectedMedia.ContentName;
            textBoxFilePath.Text = selectedMedia.Path;
        }

        private void buttonDeleteContent_Click(object sender, RoutedEventArgs e)
        {
            Web.Models.MultimediaContent selectedContent = gridContents.SelectedItem as Web.Models.MultimediaContent;
            if (selectedContent == null)
                return;
            medias.Remove(selectedContent);
            selectedContent.IsDeleted = true;
            mediaService.SubmitChanges();
            RefreshContentGrid();
        }
    }
}

