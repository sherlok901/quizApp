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

namespace MRZS.Classes.Testing
{
    public class TestControler
    {
        
        IEnumerable<TestAnswer> TestAns;
        TestQuestion TestQues;
        TestResult TestRes;
        TestingImage TestImg;
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
        internal string getFirstQuestion()
        {
            CurrentQuestionID = 1;
            return getQuestion(1);
        }
        internal int getCurrentQuestionID()
        {
            return CurrentQuestionID;
        }

        internal WrapPanel getQuestionUIElem()
        {            
            string question = getFirstQuestion();
            string[] mas= question.Split(' ');
            WrapPanel wp = new WrapPanel();
            foreach (string s in mas)
            {
                TextBlock tb = new TextBlock();
                tb.FontFamily = new FontFamily("Arial");
                tb.FontSize = 16;
                tb.TextWrapping = TextWrapping.Wrap;
                tb.Text = s;
                Thickness th = new Thickness(10, 10, 10, 10);
                tb.Margin = th;
                wp.Children.Add(tb);
            }                        
            return wp;
        }
        internal Grid getAnswersUIElem()
        {            
            Grid g = new Grid();
            g.ShowGridLines = true;
            g.Width = 500;

            ColumnDefinition col1 = new ColumnDefinition();
            GridLength gl = new GridLength(50);
            col1.Width = gl;
            ColumnDefinition col2 = new ColumnDefinition();
            GridLength gl2 = new GridLength(450);
            col2.Width = gl2;
            g.ColumnDefinitions.Add(col1);
            g.ColumnDefinitions.Add(col2);
            List<TestAnswer> answers = LoadData.TestAnswerTable.Where(n => n.questionID == 1).ToList();
            //creating rows for current answers
            for (int i = 0; i < answers.Count; i++)
            {
                RowDefinition r = new RowDefinition();
                g.RowDefinitions.Add(r);
            }
            
            int rowCount = 0;
            
            foreach (TestAnswer ans in answers)
            {
                CheckBox chBox = new CheckBox();
                chBox.HorizontalAlignment = HorizontalAlignment.Center;
                chBox.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(chBox, 0);
                Grid.SetRow(chBox, rowCount);
                g.Children.Add(chBox);

                TextBlock tb = createTextBlock(ans.answer);
                Grid.SetColumn(tb, 1);
                Grid.SetRow(tb, rowCount);
                g.Children.Add(tb);
                rowCount++;
            }
            return g;
        }

        //creating textBlock
        TextBlock createTextBlock(string text)
        {
            TextBlock tb = new TextBlock();
            tb.FontFamily = new FontFamily("Arial");
            tb.FontSize = 16;
            tb.TextWrapping = TextWrapping.Wrap;
            tb.Text = text;
            Thickness th = new Thickness(10, 10, 10, 10);
            tb.Margin = th;
            return tb;
        }

    }    
}
