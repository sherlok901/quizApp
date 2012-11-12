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
using System.Windows.Media.Imaging;
using MRZS.Classes.Testing;
using MRZS.Classes;

namespace MRZS.Views.Testing
{
    public partial class Testing : Page
    {
        TestControler testControler = new TestControler();

        public Testing()
        {
            InitializeComponent();      
                              
            //Image im = new Image();
            //im.Source = new BitmapImage(new Uri("/MRZS;Component/Assets/1_active.png", UriKind.Relative));            
                                    
            busyIndicator.IsBusy = true;
            LoadData.DataLoaded += LoadData_DataLoaded;                                    
        }

        void LoadData_DataLoaded(object sender, EventArgs e)
        {
            busyIndicator.IsBusy = false;

            WrapPanel wp = testControler.getQuestionUIElem();
            //parse text there
            wp.Width = 500;
            wp.Orientation = Orientation.Horizontal;
            wp.Background = new SolidColorBrush(Colors.Cyan);            
            TestPanel.Children.Add(wp);
            //adding grid with answers
            Grid g = testControler.getAnswersUIElem();
            TestPanel.Children.Add(g);
            //adding buttons for test managment            
            
            Button CanselBtn = new Button();
            CanselBtn.FontFamily = new FontFamily("Arial");
            CanselBtn.FontSize = 14;
            CanselBtn.Width=70;
            CanselBtn.Height = 40;
            CanselBtn.Content = "Отмена";
            CanselBtn.Margin=new Thickness(10, 20, 10, 10);
            Button NextBtn = new Button();
            NextBtn.Name = "nextBtn";
            NextBtn.Width = 100;
            NextBtn.Height = 40;
            NextBtn.Margin=new Thickness(10, 20, 10, 10);
            NextBtn.FontFamily = new FontFamily("Arial");
            NextBtn.FontSize = 14;
            NextBtn.Content = "Следущий вопрос";
            Button PrevBtn = new Button();
            PrevBtn.Width = 100;
            PrevBtn.Height = 40;
            PrevBtn.Margin = new Thickness(10, 20, 10, 10);
            PrevBtn.Content = "Предыдущий вопрос";
            PrevBtn.FontFamily = new FontFamily("Arial");
            PrevBtn.FontSize = 14;

            StackPanel sp = new StackPanel();
            sp.Width = 500;
            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(CanselBtn);
            sp.Children.Add(PrevBtn);
            sp.Children.Add(NextBtn);
            TestPanel.Children.Add(sp);
        }

        // Выполняется, когда пользователь переходит на эту страницу.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
