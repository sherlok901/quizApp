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
        TestTimeController TimerCtrl = new TestTimeController();        
        Dictionary<int, List<bool>> AllUsrAnswers = new Dictionary<int, List<bool>>();
        Grid gdAnsw;
                
        //Thread newThread;
        DispatcherTimer timer;

        public Testing()
        {
            InitializeComponent();
            
            if (!LoadData.checkNotNullTables())
            {
                busyIndicator.IsBusy = true;
                LoadData.DataLoaded += LoadData_DataLoaded;
            }
            else busyIndicator.IsBusy = false;
            TestTimeTblock.Visibility= CanselBtn.Visibility = PrevBtn.Visibility = NextBtn.Visibility = Visibility.Collapsed;

            //newThread = new Thread(new ThreadStart(myTimer));
            //newThread.IsBackground = true;
            //newThread.Start();
         
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
        }                
         
        void tmr_Tick(object sender, EventArgs e)
        {            
            TestTimeTblock.Text ="До конца теста: "+ TimerCtrl.howReimains().Hours.ToString() + ":" + TimerCtrl.howReimains().Minutes.ToString() + ":" + TimerCtrl.howReimains().Seconds.ToString();
            //if time is left
            if (TimerCtrl.howReimains().Hours ==0 && TimerCtrl.howReimains().Minutes == 0 && TimerCtrl.howReimains().Seconds == 0)
            {
                timer.Stop();
                TestTimeTblock.Visibility = CanselBtn.Visibility = PrevBtn.Visibility = NextBtn.Visibility = Visibility.Collapsed;
                TestPanel.Children.Clear();
                TextBlock tb= testControler.createTextBlock("Время вышло!");
                TestPanel.Children.Add(tb);
                ShowButtonExit();
            }
        }       

       

        void CanselBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Student/Education", UriKind.Relative));
        }

        void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            //check what checkbox user select/deselect in question that was showed before user clicked on PrevButton
            recordUserAnwer(); 
            
            //select/deselect checkboxes which user selected in previous question

            //get previous question            
            List<object> QuestAnswList = testControler.getPrevQuestionAndAnsersUserElem(AllUsrAnswers);
            if (QuestAnswList == null) return;//if no previous question, do nothing

            WrapPanel wpQuest = QuestAnswList[0] as WrapPanel;
            Grid gdAnsw = QuestAnswList[1] as Grid;
            TestPanel.Children.Clear();
            TestPanel.Children.Add(wpQuest);
            TestPanel.Children.Add(gdAnsw);
            
        }

        void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            recordUserAnwer();            
            
            
            //get next question
            List<object> QuestAnswList = testControler.getNextQuestionAndAnsersUserElem(AllUsrAnswers);
            if (QuestAnswList == null)
            {
                //no questions, showing result
                
                TestTimeTblock.Visibility = CanselBtn.Visibility = PrevBtn.Visibility = NextBtn.Visibility = Visibility.Collapsed;
                //calculating result
                double AllQuestNum = LoadData.TestQuestionTable.ToList().Count();
                double markForEachQuestion = AllQuestNum / 100;
                double MarkSum = 0;

                foreach (KeyValuePair<int,List<bool>> k in AllUsrAnswers)
                {
                    //get answers from database
                    List<bool?> dbAnswList= LoadData.TestAnswerTable.Where(n => n.questionID == k.Key).Select(n => n.IsTrue).ToList();
                    //get user answers
                    List<bool> userAns = AllUsrAnswers[k.Key];
                    //calculate correct answers in current question
                    int CorrectAnsCount = 0;
                    for (int i = 0; i < dbAnswList.Count; i++)
                    {
                        if (userAns[i] == true && (dbAnswList[i] == true)) CorrectAnsCount++;
                    }

                    int CountTrueAnsInDB=dbAnswList.Where(n=>n==true).ToList().Count;
                    double markForEachAnswer = markForEachQuestion / CountTrueAnsInDB;
                    MarkSum += markForEachAnswer * CorrectAnsCount;
                }
                //showing result
                TextBlock tb = testControler.createTextBlock("Результат: " + MarkSum.ToString() + "/" + 100);                                
                TestPanel.Children.Clear();
                TestPanel.Children.Add(tb);
                ShowButtonExit();
            }
            else //show next question and answers
            {
                WrapPanel wpQuest = QuestAnswList[0] as WrapPanel;
                gdAnsw = QuestAnswList[1] as Grid;
                gdAnsw.Name = "gridAnswers";
                TestPanel.Children.Clear();
                TestPanel.Children.Add(wpQuest);
                TestPanel.Children.Add(gdAnsw);
            }                        
        }       

        /// <summary>
        /// record user answers
        /// </summary>
        private void recordUserAnwer()
        {
            //record user answers
                //update record
            int id = testControler.getCurrentQuestionID();
            List<bool> boolVals=testControler.getCheckBoxValues();

            int answCount = LoadData.TestAnswerTable.Where(n => n.questionID == testControler.getCurrentQuestionID()).Select(n => n.IsTrue).ToList().Count();

            if (AllUsrAnswers.Keys.Contains(id))
            {
                if (testControler.getCheckBoxValues() == null || testControler.getCheckBoxValues().Count == 0)
                {
                    //if user update current answers = unselect all checkboxes
                    //his answers wi                    
                    //AllUsrAnswers[id] = getNewBoolList(answCount);
                }
                else AllUsrAnswers[testControler.getCurrentQuestionID()] = boolVals;
            }
                //insert new record of user answers = checkboxes values
            else
            {
                if (testControler.getCheckBoxValues() == null || testControler.getCheckBoxValues().Count == 0)
                {             
                    AllUsrAnswers.Add(id, getNewBoolList(answCount));
                }
                else AllUsrAnswers.Add(id, boolVals);
            }
            foreach (KeyValuePair<int, List<bool>> k in AllUsrAnswers)
            {

            }
            testControler.clearCheckBoxValues();//
            foreach (KeyValuePair<int, List<bool>> k in AllUsrAnswers)
            {

            }
        }

        /// <summary>
        /// creating false's List when user unclick on each checkboxes
        /// </summary>
        /// <param name="answCount"></param>
        /// <returns></returns>
        internal List<bool> getNewBoolList(int answCount)
        {
            List<bool> tempList = new List<bool>(1);
            for (int i = 1; i <= answCount; i++)
            {
                tempList.Add(false);
            }
            return tempList;
        }
        // Выполняется, когда пользователь переходит на эту страницу.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
        /// <summary>
        /// start test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartTestButton_Click_1(object sender, RoutedEventArgs e)
        {
            //starting the test
            //hide start test button
            StartTestButton.Visibility = Visibility.Collapsed;
            //show timer and test magment buttons 
            TestTimeTblock.Visibility = CanselBtn.Visibility = PrevBtn.Visibility = NextBtn.Visibility = Visibility.Visible;

            //get first question            
            int questionId = LoadData.TestQuestionTable.First().id;
#if DEBUG
            questionId = 1;
#endif
            WrapPanel wp = testControler.getQuestionUIElem(questionId);
            //parse text there
            wp.Width = 500;
            wp.Orientation = Orientation.Horizontal;
            TestPanel.Children.Add(wp);

            //adding grid with first answers
            Grid g = testControler.getAnswersUIElem(questionId);
            g.Margin = new Thickness(0, 15, 0, 0);
            TestPanel.Children.Add(g);

            //init timer
            double interval = 1.0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(interval);
            timer.Tick += new EventHandler(tmr_Tick);
            TimerCtrl.Start();
            timer.Start();            
        }

        internal void ShowButtonExit()
        {
            CanselBtn.Content = "Выход";
            CanselBtn.Visibility = Visibility.Visible;
            CanselBtn.Width = 150;
            CanselBtn.HorizontalAlignment = HorizontalAlignment.Center;
        }
    }    
}
