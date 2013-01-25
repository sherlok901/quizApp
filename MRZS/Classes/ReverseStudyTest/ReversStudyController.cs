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
using System.Collections.Generic;
using MRZS.Classes.ReverseStudyTest;

namespace MRZS.Classes.ReverseStudyTest
{
    internal class ReversStudyController
    {
        int CurrentTask = -1;
        List<ReversStudyTest> TaskList = new List<ReversStudyTest>();

        internal ReversStudyController()
        {            
            ReversStudyTest rv = new ReversStudyTest();
            rv.add("сработала защита МТЗ, первая ступень, Уставка МТЗ1=5А, Выдержка МТЗ1=2с", true);
            rv.add("сработала защита МТЗ, первая ступень, Уставка МТЗ1=7А, Выдержка МТЗ1=2с", false);
            rv.add("сработала защита МТЗ, первая ступень, Уставка МТЗ1=5А, Выдержка МТЗ1=3с", false);
            rv.Question = "Какие параметры установил оператор?";

            TaskList.Add(rv);            
        }

        internal string GetNextTask()
        {
            CurrentTask++;
            if (CurrentTask == TaskList.Count) return null;

            ReversStudyTest rv = TaskList[CurrentTask];
            return rv.Question;
        }
        internal Grid GetAnswers()
        {
            return TaskList[CurrentTask].getAnswers();
        }
    }
}
