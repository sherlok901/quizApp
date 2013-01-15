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

namespace MRZS.Views.Testing
{
    public partial class InterTestControl : UserControl
    {
        internal event EventHandler PrevBtnClicked;
        internal event EventHandler NextBtnClicked;
        internal event EventHandler CheckBtnClicked;
        MRZS.Views.Testing.BookView book = null;
        MRZS.Views.Testing.BookView introBook = null;
        MRZS.Views.Testing.BookView mtz = null;
        MRZS.Views.Testing.BookView apv = null;
        MRZS.Views.Testing.BookView zz = null;

        public InterTestControl()
        {
            InitializeComponent();
            //загрузка книги
            book = new BookView("emulBook.pdf");
            introBook = new BookView("vstup.pdf");
            mtz = new BookView("мтз.pdf");
            apv = new BookView("апв.pdf");
            zz = new BookView("ЗЗ.pdf");
        }
        //task text
        internal string TaskText
        {
            set { TestTextTb.Text = value; }

        }
        //show status text as result of checking executed task
        internal string RezStatusText
        {
            set { ResultStateTb.Text = value; }
            get { return ResultStateTb.Text; }
        }
        internal bool IsCheckedResultGood
        {
            set
            {
                if (value)
                {
                    ResultStateTb.Foreground = new SolidColorBrush(Colors.Green);
                }
                else ResultStateTb.Foreground = new SolidColorBrush(Colors.Red);
            }
        }


        void PrevBtn_Click(object sender, RoutedEventArgs e)
        {            
            if (PrevBtnClicked != null) PrevBtnClicked(this, EventArgs.Empty);
        }       

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NextBtnClicked != null) NextBtnClicked(null, EventArgs.Empty);
        }

        private void CheckTestBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBtnClicked != null) CheckBtnClicked(null, EventArgs.Empty);
        }

        private void ViewBook_Click_1(object sender, RoutedEventArgs e)
        {
            
            book.Show();
        }

        internal void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            introBook.Show();
        }

        private void HyperlinkButton_Click_2(object sender, RoutedEventArgs e)
        {
            //book = null;
            //book = new BookView("мтз.pdf");
            //book.Show();
            mtz.Show();
        }

        private void HyperlinkButton_Click_3(object sender, RoutedEventArgs e)
        {
            apv.Show();
        }

        private void HyperlinkButton_Click_4(object sender, RoutedEventArgs e)
        {
            zz.Show();
        }
      
        //private void ResultStateTb_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    //if (ResultStateTb.Text == "Все настроено верно") ResultStateTb.Foreground = new SolidColorBrush(Colors.Green);
        //    //else ResultStateTb.Foreground = new SolidColorBrush(Colors.Red);
        //}
        
    }
}
