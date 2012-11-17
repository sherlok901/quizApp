using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MRZS.Classes;
using MRZS.Web.Models;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Media.Imaging;
using System.Windows.Browser;
using System.Windows.Threading;

namespace MRZS.Classes.Testing
{
    public class TestControler
    {
        
        IEnumerable<TestAnswer> TestAns;
        TestQuestion TestQues;
        TestResult TestRes;
        TestingImage TestImg;
        List<bool> CheckBoxValues=new List<bool>(1);
        

       
        int CurrentQuestionID = -1;

        internal TestControler()
        {
            LoadData.DataLoaded += ld_DataLoaded;
        }

        void ld_DataLoaded(object sender, EventArgs e)
        {
            TestAns = LoadData.TestAnswerTable; 
        }
        internal string getQuestion(int QuestindId)
        {            
            return LoadData.TestQuestionTable.Where(n => n.id == QuestindId).Single().question;
        }        
        internal int getCurrentQuestionID()
        {
            return CurrentQuestionID;
        }
        /// <summary>
        /// get question in wrappanel
        /// </summary>
        /// <returns></returns>
        internal WrapPanel getQuestionUIElem(int QuestID)
        {
            CurrentQuestionID = QuestID;
            string question = getQuestion(CurrentQuestionID);            
            string[] QuestionParts= question.Split(' ');
            WrapPanel wp = BuildWrapPanel(QuestionParts);
            return wp;
        }
        //building wrappanel from words in question
        //in: string[] mas -words from question
        //out: wrappanel with question text and/or images
        private WrapPanel BuildWrapPanel(string[] mas)
        {
            WrapPanel wp = new WrapPanel();
            //get question number
            List<int> allId= LoadData.TestQuestionTable.Select(n => n.id).ToList();
            int indexCurrQuesn = allId.IndexOf(CurrentQuestionID);
            indexCurrQuesn++;
            //show question number
            wp.Children.Add(createTextBlock("Вопрос № " + indexCurrQuesn + "/"+allId.Count+" :"));

            foreach (string s in mas)
            {
                //adding image
                if (s.Contains("imageID"))
                {
                    string Num = getImageIDfromStr(s);
                    string ImgPath = LoadData.TestingImageTable.Where(n => n.id == Convert.ToInt32(Num)).Single().imgPath;
                    Image im = new Image();                    
                    BitmapImage bIm = new BitmapImage(new Uri("/MRZS;Component/Assets/Testing/" + ImgPath, UriKind.Relative));
                    //bIm.DownloadProgress += bIm_DownloadProgress;                    
                    im.Source = bIm;
                    im.ImageOpened += im_ImageOpened;                    
                    im.Visibility = Visibility.Collapsed;
                    wp.Children.Add(im);
                }
                    //adding text
                else
                {
                    TextBlock tb = new TextBlock();
                    tb.FontFamily = new FontFamily("Arial");
                    tb.FontSize = 16;
                    tb.TextWrapping = TextWrapping.Wrap;                    
                    tb.FontStyle = FontStyles.Italic;
                    tb.FontWeight = FontWeights.Bold;
                    tb.Text = s;
                    Thickness th = new Thickness(5, 3, 5, 3);
                    tb.Margin = th;
                    wp.Children.Add(tb);
                }
            }
            return wp;
        }

        private static string getImageIDfromStr(string s)
        {
            int startIndex = s.IndexOf('=');
            startIndex++;
            char CurrChar = s[startIndex];
            string Num = null;
            while (CurrChar != '}' && CurrChar != ',' && CurrChar != ' ')
            {
                Num += CurrChar;
                startIndex++;
                CurrChar = s[startIndex];
            }
            return Num;
        }

        void im_ImageOpened(object sender, RoutedEventArgs e)
        {
            Image img = (Image)sender;            
            BitmapImage bi = (BitmapImage)img.Source;            
            double renderedWidth = bi.PixelWidth;
            double renderedHeith = bi.PixelHeight;
            //check if width over the 550 px
            //if true we reduce the sizes
            if (bi.PixelWidth > 500)
            {
                double koef = bi.PixelWidth / 500;
                renderedWidth = 500;
                renderedHeith = bi.PixelHeight / koef;
            }
            img.Width = renderedWidth;
            img.Height = renderedHeith;
            img.Visibility = Visibility.Visible;            
        }

        void bIm_DownloadProgress(object sender, DownloadProgressEventArgs e)
        {
            if (e.Progress == 100)
            {
                
            }
        }

        internal List<object> getNextQuestionAndAnsersUserElem(Dictionary<int, List<bool>> AllUserAnsw)
        {
            CurrentQuestionID += 1;
            //checking last question
            if (CurrentQuestionID > LoadData.TestQuestionTable.Last().id) return null;

#if DEBUG
            if (CurrentQuestionID==5) return null;
#endif
            WrapPanel wpQuestion = getQuestionUIElem(CurrentQuestionID);
            Grid grAnswers = null;
            if (AllUserAnsw.ContainsKey(CurrentQuestionID)) grAnswers = getAnswersUIElem(CurrentQuestionID, AllUserAnsw);
            else grAnswers = getAnswersUIElem(CurrentQuestionID);            
            List<object> QuestAnswListUElem=new List<object>(2);
            QuestAnswListUElem.Add(wpQuestion);
            QuestAnswListUElem.Add(grAnswers);
            return QuestAnswListUElem;
        }
        internal List<object> getPrevQuestionAndAnsersUserElem(Dictionary<int,List<bool>> AllUserAnsw)
        {
            CurrentQuestionID -= 1;
            WrapPanel wpQuestion = getQuestionUIElem(CurrentQuestionID);
            
            Grid grAnswers = getAnswersUIElem(CurrentQuestionID,AllUserAnsw);
            List<object> QuestAnswListUElem = new List<object>(2);
            QuestAnswListUElem.Add(wpQuestion);
            QuestAnswListUElem.Add(grAnswers);
            //set CheckBoxValues by values, which user selected early for this question
            CheckBoxValues = AllUserAnsw[CurrentQuestionID].ToList();
            return QuestAnswListUElem;
        }
        
        /// <summary>
        /// get answers in Grid
        /// </summary>
        /// <returns></returns>
        internal Grid getAnswersUIElem(int QuestID)
        {            
            Grid g = new Grid();
            g.ShowGridLines = true;
            g.Width = 550;

            ColumnDefinition col1 = new ColumnDefinition();
            GridLength gl = new GridLength(50);
            col1.Width = gl;
            ColumnDefinition col2 = new ColumnDefinition();
            GridLength gl2 = new GridLength(500);
            col2.Width = gl2;
            g.ColumnDefinitions.Add(col1);
            g.ColumnDefinitions.Add(col2);
            
            List<TestAnswer> answers = LoadData.TestAnswerTable.Where(n => n.questionID == QuestID).ToList();
            //creating rows for current answers
            for (int i = 0; i < answers.Count; i++)
            {
                RowDefinition r = new RowDefinition();
                g.RowDefinitions.Add(r);
            }            
            int rowCount = 0;
            
            //building Grid
            foreach (TestAnswer ans in answers)
            {
                CheckBox chBox = new CheckBox();
                chBox.Click += chBox_Click;
                chBox.HorizontalAlignment = HorizontalAlignment.Center;
                chBox.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(chBox, 0);
                Grid.SetRow(chBox, rowCount);
                g.Children.Add(chBox);

                StackPanel sp = ParsingAnswersForRow(g, rowCount, ans);
                Grid.SetColumn(sp, 1);
                Grid.SetRow(sp, rowCount);
                g.Children.Add(sp);
                rowCount++;
            }
            return g;
        }
        internal Grid getAnswersUIElem(int QuestID,Dictionary<int,List<bool>> AllUserAnsw)
        {
            Grid g = new Grid();
            g.ShowGridLines = true;
            g.Width = 550;

            ColumnDefinition col1 = new ColumnDefinition();
            GridLength gl = new GridLength(50);
            col1.Width = gl;
            ColumnDefinition col2 = new ColumnDefinition();
            GridLength gl2 = new GridLength(500);
            col2.Width = gl2;
            g.ColumnDefinitions.Add(col1);
            g.ColumnDefinitions.Add(col2);

            List<TestAnswer> answers = LoadData.TestAnswerTable.Where(n => n.questionID == QuestID).ToList();
            //creating rows for current answers
            for (int i = 0; i < answers.Count; i++)
            {
                RowDefinition r = new RowDefinition();
                g.RowDefinitions.Add(r);
            }
            int rowCount = 0;

            List<bool> UserAnwsForCurrQuest = AllUserAnsw[CurrentQuestionID];
            //building Grid
            for(int i=0; i<answers.Count; i++)
            {
                TestAnswer ans = answers[i];
                CheckBox chBox = new CheckBox();
                chBox.IsChecked = UserAnwsForCurrQuest[i];
                chBox.Click += chBox_Click;
                chBox.HorizontalAlignment = HorizontalAlignment.Center;
                chBox.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(chBox, 0);
                Grid.SetRow(chBox, rowCount);
                g.Children.Add(chBox);

                StackPanel sp = ParsingAnswersForRow(g, rowCount, ans);
                Grid.SetColumn(sp, 1);
                Grid.SetRow(sp, rowCount);
                g.Children.Add(sp);
                rowCount++;
            }
            return g;
        }

        //search grid and checkboxes in it
        void chBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBoxValues.Clear();

            CheckBox cb = (CheckBox)sender;
            Grid gr = cb.Parent as Grid;
            
            UIElementCollection uiList= gr.Children;                   
            
            foreach (UIElement uiEl in uiList)
            {                
                //search answer's checkboxes
                if (uiEl is CheckBox)
                {
                    CheckBox c = uiEl as CheckBox;
                    //AllCheckBox.Add(c);                    
                    bool val = false;
                    if (c.IsChecked == null || c.IsChecked == false) val = false;
                    else val = true;
                    CheckBoxValues.Add(val);                    
                }
            }
        }

        /// <summary>
        /// get values from answer's checkboxes
        /// </summary>
        /// <returns></returns>
        internal List<bool> getCheckBoxValues()
        {            
            return CheckBoxValues.ToList();
        }
        internal void clearCheckBoxValues()
        {
            CheckBoxValues.Clear();
        }

        private StackPanel ParsingAnswersForRow(Grid g, int rowCount, TestAnswer ans)
        {
            string[] ansParts = ans.answer.Split(' ');
            StackPanel sp = new StackPanel();
            sp.Width = 500;
            sp.Orientation = Orientation.Vertical;
            WrapPanel wp = new WrapPanel();
            wp.Width = 500;
            wp.Orientation = Orientation.Horizontal;
            wp.Margin = new Thickness(0, 10, 0, 0);
            bool IsCreateNewWrapPanel = false;

            foreach (string s in ansParts)
            {
                if (IsCreateNewWrapPanel)
                {
                    wp = new WrapPanel();
                    wp.Width = 500;
                    wp.Orientation = Orientation.Horizontal;
                    IsCreateNewWrapPanel = false;
                }

                if (s.Contains("imageID"))
                {
                    int imgID = Convert.ToInt32(getImageIDfromStr(s));
                    bool NewLine = false;
                    
                    if (s.Contains("position"))
                    {
                        int posIndex = s.LastIndexOf('=');
                        posIndex++;
                        string position = s.Substring(posIndex, s.Length - 1 - posIndex);                        
                        if (position.Equals("NewLine")) NewLine = true;
                    }
                    string ImgPath = LoadData.TestingImageTable.Where(n => n.id == imgID).Single().imgPath;
                    Image im = new Image();
                    BitmapImage bIm = new BitmapImage(new Uri("/MRZS;Component/Assets/Testing/" + ImgPath, UriKind.Relative));                                         
                    im.Source = bIm;
                    im.ImageOpened += im_ImageOpened;
                    im.Visibility = Visibility.Collapsed;

                    //adding image in stackpanel
                    if (NewLine)
                    {
                        sp.Children.Add(wp);                        
                        sp.Children.Add(im);
                        IsCreateNewWrapPanel = true;
                    }
                    else //adding image to wrappanel
                    {                        
                        wp.Children.Add(im);                        
                    }

                }
                else
                {
                    TextBlock tb = createTextBlock(s);
                    wp.Children.Add(tb);                    
                }
            }
            if(sp.Children.Contains(wp)==false) sp.Children.Add(wp);
            return sp;
        }

        //creating textBlock
        internal TextBlock createTextBlock(string text)
        {
            TextBlock tb = new TextBlock();
            tb.FontFamily = new FontFamily("Arial");
            tb.FontSize = 16;
            tb.TextWrapping = TextWrapping.Wrap;
            tb.Text = text;
            Thickness th = new Thickness(5, 3, 5, 3);
            tb.Margin = th;
            return tb;
        }


    }    
}
