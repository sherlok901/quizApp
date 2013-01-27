using System;
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
    internal class SelectedAnswConrllr
    {
        Dictionary<int, List<bool>> UserAnswers = new Dictionary<int, List<bool>>();

        internal void CheckAnsw(List<RadioButton> listRb, int TaskNumber)
        {            

            if(UserAnswers.ContainsKey(TaskNumber))
            {
                //пользователь изменяет ответ на ранее просматриваемое задание
                List<bool> RadioBtnValues = new List<bool>();
                foreach (RadioButton rb in listRb)
                {
                    if (rb.IsChecked == null || rb.IsChecked == false) RadioBtnValues.Add(false);
                    else RadioBtnValues.Add(true);
                }
                UserAnswers[TaskNumber] = RadioBtnValues;
            }
            else
            {
                //пользователь впервые отвечате на даное задание
                List<bool> RadioBtnValues = new List<bool>();
                foreach (RadioButton rb in listRb)
                {
                    if (rb.IsChecked == null || rb.IsChecked == false) RadioBtnValues.Add(false);
                    else RadioBtnValues.Add(true);
                }
                UserAnswers.Add(TaskNumber, RadioBtnValues);
            }
        }

        internal bool IsThisTaskWasAnswered(int TaskNumber)
        {
            if (UserAnswers.ContainsKey(TaskNumber)) return true;
            else return false;
        }

        internal List<bool> GetAnswredValue(int TaskNumber)
        {
            return UserAnswers[TaskNumber];
        }
        internal int TestRusult(List<ReversStudyTest> TaskList)
        {
            int RightUserAnsCount = 0;
            foreach (ReversStudyTest Task in TaskList)
            {
                int RightAnswIndex = -1;
                int userAnsIndex = -1;
                //find right answer index in asnwers                             
                Dictionary<string,bool> Answers = Task.GetAnswList();                
                foreach (KeyValuePair<string, bool> pair in Answers)
                {
                    RightAnswIndex++;
                    if (pair.Value) break;
                }
                //find user's answer
                int taskNumber = TaskList.IndexOf(Task);
                if (UserAnswers.ContainsKey(taskNumber))
                {
                    //user was answered on this task
                    List<bool> list = UserAnswers[taskNumber];                    
                    foreach (bool ans in list)
                    {
                        userAnsIndex++;
                        if (ans) break;
                    }

                }

                if (RightAnswIndex == userAnsIndex && RightAnswIndex != -1) RightUserAnsCount++;
            }
            return RightUserAnsCount;
        }

        internal double TestResultWithPenalty(List<ReversStudyTest> TaskList)
        {
            int RightAnswCount = TestRusult(TaskList);
            double rez = RightAnswCount - Convert.ToDouble(RightAnswCount) * 0.2;
            return rez;
        }

        internal void ClearAnswers() { UserAnswers.Clear(); }
    }
}
