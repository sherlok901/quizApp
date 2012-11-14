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
using System.Threading;
using System.Windows.Threading;

namespace MRZS.Views.Testing
{
    public partial class Testing : Page
    {
        TestControler testControler = new TestControler();          
        
        int myTick=0;
        //TimeSpan ts; 
        //Thread newThread;
        

        public Testing()
        {
            InitializeComponent();
                                          
            //Image im = new Image();
            //im.Source = new BitmapImage(new Uri("/MRZS;Component/Assets/1_active.png", UriKind.Relative));            
                                    
            busyIndicator.IsBusy = true;
            LoadData.DataLoaded += LoadData_DataLoaded;

            //newThread = new Thread(new ThreadStart(myTimer));
            //newThread.IsBackground = true;
            //newThread.Start();
         
        }

        void timer_Completed(object sender, EventArgs e)
        {
            
            TestTimeTblock.Text = myTick.ToString();
        
        }

        void myUpdate(object state)
        {
            //this.Dispatcher.BeginInvoke(delegate()
            //{
            //    //TestTimeTblock.Text = DateTime.Now.ToLongTimeString();
            //    myTick += 1;
            //    TestTimeTblock.Text = myTick.ToString();

            //});
        }

        void LoadData_DataLoaded(object sender, EventArgs e)
        {
            busyIndicator.IsBusy = false;
            //get first question
            int questionId = 47;
            WrapPanel wp = testControler.getQuestionUIElem(questionId);
            //parse text there
            wp.Width = 500;
            wp.Orientation = Orientation.Horizontal;                       
            TestPanel.Children.Add(wp);
            
            //adding grid with first answers
            Grid g = testControler.getAnswersUIElem(questionId);
            g.Margin = new Thickness(0, 15, 0, 0);
            TestPanel.Children.Add(g);                        
            

            //if (TestPanel.Children.Contains(TimerTextBlock) == false) TestPanel.Children.Add(TimerTextBlock);
        }
        
        private void myTimer()
        {
            //DispatcherTimer tmr = new DispatcherTimer();            
            //tmr.Interval = TimeSpan.FromSeconds(5);
            //tmr.Tick += tmr_Tick;
            //tmr.Start();

            //Timer t = new Timer(myUpdate, null, 1000, 1000);
                  
        }
         
        void tmr_Tick(object sender, EventArgs e)
        {
            
            
        }       

       

        void CanselBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Student/Education", UriKind.Relative));
        }

        void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            List<object> QuestAnswList = testControler.getPrevQuestionAndAnsersUserElem();
            WrapPanel wpQuest = QuestAnswList[0] as WrapPanel;
            Grid gdAnsw = QuestAnswList[1] as Grid;
            TestPanel.Children.Clear();
            TestPanel.Children.Add(wpQuest);
            TestPanel.Children.Add(gdAnsw);
            
        }

        void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            List<object> QuestAnswList = testControler.getNextQuestionAndAnsersUserElem();
            WrapPanel wpQuest = QuestAnswList[0] as WrapPanel;
            Grid gdAnsw = QuestAnswList[1] as Grid;
            TestPanel.Children.Clear();
            TestPanel.Children.Add(wpQuest);
            TestPanel.Children.Add(gdAnsw);
            
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
