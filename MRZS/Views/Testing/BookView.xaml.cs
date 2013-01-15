using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FirstFloor.Documents.Pdf;
using FirstFloor.Documents.Search;


namespace MRZS.Views.Testing
{
    public partial class BookView : ChildWindow
    {
        public BookView() { }
        public BookView(string bookName)
        {
            InitializeComponent();

            //string Width = HtmlPage.Window.Eval("screen.width").ToString();
            //string Height = HtmlPage.Window.Eval("screen.height").ToString();

            this.DataSource.PackageReader = new PdfDocumentReader(new Uri(bookName, UriKind.Relative));
           
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void DataSource_LoadError_1(object sender, FirstFloor.Documents.Controls.ErrorEventArgs e)
        {
            MessageBox.Show(e.Error.Message);

        }

        private void BookView_Loaded_1(object sender, RoutedEventArgs e)
        {            
            
        }

        private void DataSource_SearchCompleted_1(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
            }
        }

        private void SearchResults_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var result = this.SearchResults.SelectedItem as SearchResult;
            if (result != null)
            {
                // select
                this.Viewer.Selection.Select(result.Range);

                // bring into view
                this.Viewer.BringIntoView(result.Range.Start);
            }

        }

        private void Apv1Btn_Click_1(object sender, RoutedEventArgs e)
        {
            Viewer.PageNumber = 10;
            
        }
    }
}

