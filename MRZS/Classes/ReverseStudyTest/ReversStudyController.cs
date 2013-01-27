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

            ReversStudyTest rv2 = new ReversStudyTest();
            rv2.add("Сработала защита МТЗ1; Уставка МТЗ1=3А, Выдержка МТЗ1=2с; затем включился таймер АПВ; 1 Цикл АПВ=3с; при срабатывании АПВ реле 1 и 3 включились", false);
            rv2.add("Сработала защита МТЗ1; Уставка МТЗ1=4А, Выдержка МТЗ1=3с; затем включился таймер АПВ; 1 Цикл АПВ=4с; при срабатывании АПВ реле 1 и 3 включились", true);
            rv2.add("Сработала защита МТЗ1; Уставка МТЗ1=4А, Выдержка МТЗ1=2с; затем включился таймер АПВ; 1 Цикл АПВ=4с; при срабатывании АПВ реле 1 и 3 включились", false);
            rv2.Question = rv.Question;
            rv2.ReduceApv = false;
            rv2.ApvReducedValue = 3;
            TaskList.Add(rv2);
        }       

        internal string GetNextTask()
        {
            CurrentTask++;
            if (CurrentTask == TaskList.Count) { CurrentTask = TaskList.Count - 1; return null; }

            return TaskList[CurrentTask].Question;
        }
        internal string GetPrevTask()
        {            
            CurrentTask--;
            if (CurrentTask < 0) { CurrentTask = 0; return null; }

            return TaskList[CurrentTask].Question;
        }
        internal Grid GetAnswers(List<bool> AnsweredList)
        {
            if (CurrentTask == TaskList.Count) return null;
            Grid gr=TaskList[CurrentTask].getAnswers(AnsweredList);            
            return gr;
        }
        internal int GetTaskCount()
        {
            return TaskList.Count;
        }
        internal int GetCurrentTaskNumber()
        {
            return CurrentTask;
        }
        internal void SetCurrentTaskNumberToZero() { CurrentTask = -1; }
        internal List<ReversStudyTest> GetAnswersList() { return TaskList; }
    }
}
