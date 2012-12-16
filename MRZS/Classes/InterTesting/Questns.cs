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
using MRZS.Classes.InterTesting;

namespace MRZS.Classes.InterTesting
{
    internal class Questns
    {
        List<string> qs = new List<string>(9);
        int CurrentQuest = 0;
        MTZ MtzCtrl = new MTZ();
        OutputsRele ReleCtrl = new OutputsRele();
        string er1 = "Вы не включили МТЗ";
        string er2 = "Вы не включили МТЗ1";
        string er3 = "Не верно настроеная Уставка МТЗ1";
        string er4 = "Не настроено выходное реле1 на МТЗ1";
        string er5 = "Вы не настроили значение Ia или Ib или Ic";
        string SucfRez = "Все настроено верно";

        public Questns()
        {
            string q1 = "1)	Настройте прибор МРЗС на срабатывание первой ступени максимальной токовой защиты (МТЗ1) на поступление тока Ia или Ib или Ic величиной 5А, при этом должно сработать первое реле прибора.";
            string q2 = "2)	Настройте первый дискретный вход так, чтобы ни одно реле не сработало на любые входные токи Ia или Ib или Ic, при этом должны быть соответственно включены и установлены МТЗ1 и МТЗ2 на 0,1 А, настроенное реле 01 на срабатывание МТЗ1 и МТЗ2.";
            string q3 = "3)	Настройте прибор так, чтобы выходное реле 01 и светодиод 01 срабатывали на один из токов Ia, Ib, Ic на 5 А и выше, при этом выходное реле срабатывало через 0,5 с.";
            string q4 = "4)	Настройте прибор так, чтобы срабатывало выходное реле 01 и 02 на МТЗ1 5А и через 3с после срабатывания МТЗ1 задействовалось автоматическое повторное включение (АПВ).";
            string q5 = "5)	Настройте выходные реле и светодиодные индикаторы прибора МРЗС на  срабатывание защиты МТЗ2 при значениях токов Ia, Ib, Ic от 5А и выше и через 3с включалось АПВ.";
            string q6 = "6)	Настройте АПВ так, чтобы оно срабатывало после срабатывания защиты МТЗ2, уставка которой равна 10А, выдержка 0,1с. АПВ должно срабатывать через 1с. 2-е выходное реле и 2-й светодиодный индикатор также настройте на защиту МТЗ2.";
            string q7 = "7)	Настройте индикацию прибора, чтобы срабатывали 1-е и 5-е реле на ток ЗI0=15 мА через 0,5с.";
            string q8 = "8)	Настройте индикацию прибора, чтобы засвечивались 1-й и 3-й индикатор на ток ЗфI=10 мА через 1с.";
            string q9 = "9)	Настройте первое выходное реле на срабатывание защиты от замыканий на землю (ЗЗ) на ток ЗI0 от 20мА и выше; установите уставку ЗЗ на 1с. После этого, включите и настройте первый дискретный вход 01 (ДВ01) так, чтобы выше упомянутое настроенное выходное реле 01 не срабатывало на ток ЗI0.";
            qs.Add(q1);
            qs.Add(q2);
            qs.Add(q3);
            qs.Add(q4);
            qs.Add(q5);
            qs.Add(q6);
            qs.Add(q7);
            qs.Add(q8);
            qs.Add(q9);
        }
        internal string getFirstTask()
        {
            CurrentQuest = 0;
            return qs[0];
        }
        internal string getNextTask()
        {
            if (CurrentQuest < (qs.Count - 1))
            {
                CurrentQuest += 1;
                return qs[CurrentQuest];
            }
            else return null;
        }
        internal string getPrevTask()
        {
            if (CurrentQuest > 0)
            {
                CurrentQuest -= 1;
                return qs[CurrentQuest];
            }
            else return null;
        }
        internal int getCurrentTaskNumber()
        {
            return CurrentQuest;
        }

        //check task 1
        internal string checkTask01(double Ia,double Ib,double Ic)
        {
            if (!MtzCtrl.IsTurnOn()) return er1;
            else if (!MtzCtrl.IsMTZ1TurnOn()) return er2;
            else if (MtzCtrl.getMTZ1Value() < 5) return er3;
            else if (!ReleCtrl.IsRele1ConfiguredOnMTZ1()) return er4;
            else if ((Ia < 5 || Ia > 5.9) && (Ic < 5 || Ic > 5.9) && (Ib < 5 || Ib > 5.9)) return er5;
            else return SucfRez;
        }
        internal string checkTask02()
        {

        }
    }
}
