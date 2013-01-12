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
using MRZS.Views.Emulator;
using MRZS.Classes.InterTesting;
using MRZS.Classes.Errors;

namespace MRZS.Classes.InterTesting
{
    internal class Questns
    {
        List<string> qs = new List<string>(9);
        int CurrentQuest = 0;
        MTZ MtzCtrl = new MTZ();
        OutputsRele ReleCtrl = new OutputsRele();
        DVs DVCtrl = new DVs();
        SDI SdiCtrl = new SDI();
        APV ApvCtrl = new APV();
        MrzsErrors mrzsErrors = new MrzsErrors();
        

        public Questns()
        {
            string q1 = "1)	Настройте прибор МРЗС на срабатывание первой ступени максимальной токовой защиты (МТЗ1) на поступление тока Ia или Ib или Ic величиной 5А, при этом должно сработать первое реле прибора.";
            string q2 = "2)	Настройте первый дискретный вход так, чтобы ни одно реле не сработало на любые входные токи Ia или Ib или Ic, при этом должны быть соответственно включены и установлены МТЗ1 и МТЗ2 на 0,1 А, настроенное реле 01 на срабатывание МТЗ1 и МТЗ2.";
            string q3 = "3)	Настройте прибор так, чтобы выходное реле 01 и светодиод 01 срабатывали на один из токов Ia, Ib, Ic на 5 А и выше, при этом выходное реле срабатывало через 0,5 с.";
            string q4 = "4)	Настройте прибор так, чтобы срабатывало выходное реле 01 и 02 на МТЗ1 5А и через 3с после срабатывания МТЗ1 задействовалось автоматическое повторное включение (АПВ).";
            string q5 = "5)	Настройте выходные реле и светодиодные индикаторы прибора МРЗС на  срабатывание защиты МТЗ2 при значениях токов Ia, Ib, Ic от 5А и выше и через 1с включалось АПВ.";
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
            if (!MtzCtrl.IsTurnOn()) return mrzsErrors.mtzError1;
            else if (!MtzCtrl.IsMTZ1TurnOn()) return mrzsErrors.mtzError2;
            else if (MtzCtrl.getMTZ1Value() < 5) return mrzsErrors.mtzError3;
            else if (!ReleCtrl.IsRele1ConfiguredOnMTZ1()) return mrzsErrors.mtzError4;
            else if ((Ia < 5 || Ia > 5.9) && (Ic < 5 || Ic > 5.9) && (Ib < 5 || Ib > 5.9)) return mrzsErrors.NumUpDownError1;
            else return mrzsErrors.SucfRez;
        }
        internal string checkTask02(InputDV dv1,InputDV dv2,InputDV dv3,InputDV dv4,InputDV dv5,InputDV dv6)
        {
            if (!MtzCtrl.IsTurnOn()) return mrzsErrors.mtzError1;
            else if (!MtzCtrl.IsMTZ1TurnOn()) return mrzsErrors.mtzError2;
            else if (!MtzCtrl.IsMTZ2TurnOn()) return mrzsErrors.mtzError5;
            else if (MtzCtrl.getMTZ1Value() != 0.1) return mrzsErrors.mtzError3;
            else if (MtzCtrl.getMTZ2Value() != 0.1) return mrzsErrors.mtzError6;
            else if (!ReleCtrl.IsRele1ConfiguredOnMTZ1()) return mrzsErrors.mtzError4;
            else if (!ReleCtrl.IsRele1ConfiguredOnMTZ2()) return mrzsErrors.mtzError7;
            else if (dv2.IsChecked.Value) return mrzsErrors.DVError1 + "2";
            else if (dv3.IsChecked.Value) return mrzsErrors.DVError1 + "3";
            else if (dv4.IsChecked.Value) return mrzsErrors.DVError1 + "4";
            else if (dv5.IsChecked.Value) return mrzsErrors.DVError1 + "5";
            else if (dv6.IsChecked.Value) return mrzsErrors.DVError1 + "6";
            else if (!DVCtrl.IsDV1ConfOnTurnOFFMtz1()) return mrzsErrors.DVError2;
            else if (!dv1.IsChecked.Value) return mrzsErrors.DVError3;
            else return mrzsErrors.SucfRez;
        }
        internal string checkTask03(double Ia, double Ib, double Ic)
        {
            if (!MtzCtrl.IsTurnOn()) return mrzsErrors.mtzError1;
            else if (!MtzCtrl.IsMTZ1TurnOn()) return mrzsErrors.mtzError2;
            else if (!MtzCtrl.IsMTZ2TurnOn()) return mrzsErrors.mtzError5;
            else if (MtzCtrl.getMTZ1Value() < 5) return mrzsErrors.mtzError3;
            else if (MtzCtrl.getMTZ2Value() < 5) return mrzsErrors.mtzError6;
            else if (MtzCtrl.getTimerMtz1() != 0.5) return mrzsErrors.mtzError8;
            else if (!ReleCtrl.IsRele1ConfiguredOnMTZ1()) return mrzsErrors.mtzError4;
            else if (!ReleCtrl.IsRele1ConfiguredOnMTZ2()) return mrzsErrors.mtzError7;
            else if (!SdiCtrl.IsSD1ConfiguredOnMTZ1()) return mrzsErrors.SDIError1;
            else if ((Ia < 5 || Ia > 5.9) && (Ic < 5 || Ic > 5.9) && (Ib < 5 || Ib > 5.9)) return mrzsErrors.NumUpDownError1;
            else return mrzsErrors.SucfRez;
        }
        internal string checkTask04(double Ia, double Ib, double Ic)
        {
            if (!MtzCtrl.IsTurnOn()) return mrzsErrors.mtzError1;
            else if (!MtzCtrl.IsMTZ1TurnOn()) return mrzsErrors.mtzError2;
            else if (MtzCtrl.getMTZ1Value() < 5) return mrzsErrors.mtzError3;
            else if (!ReleCtrl.IsRele1ConfiguredOnMTZ1()) return mrzsErrors.mtzError4;
            else if (!ReleCtrl.IsRele2ConfiguredOnMTZ1()) return ReleMtz1ErrorMessage(2);
            else if (!ApvCtrl.IsApvTurnOn()) return mrzsErrors.ApvError1;
            else if (ApvCtrl.getTimer1CycleApv() != 3) return mrzsErrors.ApvError2;
            else if ((Ia < MtzCtrl.getMTZ1Value()) && (Ic < MtzCtrl.getMTZ1Value()) && (Ib < MtzCtrl.getMTZ1Value())) return mrzsErrors.NumUpDownError2;
            else return mrzsErrors.SucfRez;
        }
        internal string checkTask05(double Ia, double Ib, double Ic)
        {
            if (!MtzCtrl.IsTurnOn()) return mrzsErrors.mtzError1;
            else if (!MtzCtrl.IsMTZ2TurnOn()) return mrzsErrors.mtzError5;
            else if (MtzCtrl.getMTZ2Value() != 5) return mrzsErrors.mtzError6;
            else if (!ReleCtrl.IsRele1ConfiguredOnMTZ2()) return ReleMtz2ErrorMessage(1);
            else if (!ReleCtrl.IsRele2ConfiguredOnMTZ2()) return ReleMtz2ErrorMessage(2);
            else if (!ReleCtrl.IsRele3ConfiguredOnMTZ2()) return ReleMtz2ErrorMessage(3);
            else if (!ReleCtrl.IsRele4ConfiguredOnMTZ2()) return ReleMtz2ErrorMessage(4);
            else if (!ReleCtrl.IsRele5ConfiguredOnMTZ2()) return ReleMtz2ErrorMessage(5);
            else if (!SdiCtrl.IsSD1ConfiguredOnMTZ2()) return SdiMtz2ErrorMess(1);
            else if (!SdiCtrl.IsSD2ConfiguredOnMTZ2()) return SdiMtz2ErrorMess(2);
            else if (!SdiCtrl.IsSD3ConfiguredOnMTZ2()) return SdiMtz2ErrorMess(3);
            else if (!SdiCtrl.IsSD4ConfiguredOnMTZ2()) return SdiMtz2ErrorMess(4);
            else if (!SdiCtrl.IsSD5ConfiguredOnMTZ2()) return SdiMtz2ErrorMess(5);
            else if (!SdiCtrl.IsSD6ConfiguredOnMTZ2()) return SdiMtz2ErrorMess(6);
            else if (!ApvCtrl.IsApvTurnOn()) return mrzsErrors.ApvError1;
            else if (ApvCtrl.getTimer1CycleApv() != 1) return mrzsErrors.ApvError2;
            else if ((Ia < 5 || Ia > 5.2) && (Ic < 5 || Ic > 5.2) && (Ib < 5 || Ib > 5.2)) return mrzsErrors.NumUpDownError1;
            else if ((Ia < MtzCtrl.getMTZ2Value()) && (Ic < MtzCtrl.getMTZ2Value()) && (Ib < MtzCtrl.getMTZ2Value())) return mrzsErrors.NumUpDownError3;
            else return mrzsErrors.SucfRez;
        }


        //установка все мтз в ноль
        internal void clearMTZs()
        {
            MtzCtrl.SetMTZsNull();
        }

        //сообщение об ненастроенном реле
        string ReleMtz1ErrorMessage(int ReleNumber)
        {
            return "Не настроено выходное реле"+ReleNumber.ToString()+" на МТЗ1";
        }
        string ReleMtz2ErrorMessage(int ReleNumber)
        {
            return "Не настроено выходное реле" + ReleNumber.ToString() + " на МТЗ2";
        }

        string SdiMtz1ErrorMess(int sdiNum)
        {
            return "Не настроеный "+sdiNum+" свето-индикатор на Мтз1";
        }
        string SdiMtz2ErrorMess(int sdiNum)
        {
            return "Не настроеный " + sdiNum + " свето-индикатор на Мтз2";
        }
    }
}
