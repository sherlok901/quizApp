﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MRZS.Classes.ReverseStudyTest
{
    internal class ReversStudyTest
    {        
        internal string Question;
        Dictionary<string, bool> AnswList = new Dictionary<string, bool>();
        internal bool ReduceApv = false;
        internal double ApvReducedValue;
        List<RadioButton> RadioBtnList = new List<RadioButton>();
         

        internal void add(string Answer,bool IsRight)
        {
            AnswList.Add(Answer, IsRight);
        }
        internal Grid getAnswers(List<bool> AnsweredList)
        {
            int RadioBtnCount = -1;

            Grid g = new Grid();
            g.Name = "AnswerGrid";
            g.Background = new SolidColorBrush(Colors.LightGray);           

            ColumnDefinition col1 = new ColumnDefinition();
            GridLength gl = new GridLength(50);
            col1.Width = gl;
            ColumnDefinition col2 = new ColumnDefinition();           
            g.ColumnDefinitions.Add(col1);
            g.ColumnDefinitions.Add(col2);

            for (int i = 0; i <AnswList.Count; i++)
            {
                RowDefinition r = new RowDefinition();
                g.RowDefinitions.Add(r);
            }
            int rowCount = 0;
            
            //building Grid
            foreach (KeyValuePair<string,bool> pairs in AnswList)
            {
                RadioBtnCount++;
                RadioButton rb = new RadioButton();
                if (AnsweredList == null) rb.IsChecked = false;                
                else rb.IsChecked = AnsweredList[RadioBtnCount];

                RadioBtnList.Add(rb);
                rb.Click += rb_Click;
                rb.HorizontalAlignment = HorizontalAlignment.Center;
                rb.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(rb, 0);
                Grid.SetRow(rb, rowCount);
                g.Children.Add(rb);                

                TextBlock tb = new TextBlock();
                tb.MaxWidth = 710;
                tb.FontFamily = new FontFamily("Arial");
                tb.FontSize = 14;
                tb.TextWrapping = TextWrapping.Wrap;
                tb.Text = pairs.Key;
                tb.Margin = new Thickness(5, 3, 5, 3);

                Grid.SetColumn(tb, 1);
                Grid.SetRow(tb, rowCount);
                g.Children.Add(tb);                
                rowCount++;
            }
            return g;
        }
        internal Dictionary<string, bool> GetAnswList() { return AnswList; }

        internal List<RadioButton> GetRadioBtnList()
        {
            return RadioBtnList;
        }
        void rb_Click(object sender, RoutedEventArgs e)
        {
            
        }
     
    }  
}
