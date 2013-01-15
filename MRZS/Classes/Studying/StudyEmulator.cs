using System;
using System.Collections.Generic;
using MRZS.Classes.Errors;
using MRZS.Classes.InterTesting;
using MRZS.Views.Emulator;
namespace MRZS.Classes.Studying
{
    internal class StudyEmulator
    {
        List<string> tasks = new List<string>();
        int CurrentTask = -1;
        MrzsErrors mrzsErrors = new MrzsErrors();
        MTZ MtzCtrl = new MTZ();
        OutputsRele ReleCtrl = new OutputsRele();
        DVs DVCtrl = new DVs();
        SDI SdiCtrl = new SDI();
        APV ApvCtrl = new APV();

        internal StudyEmulator()
        {           
            string t1 = "1) Функция защиты МТЗ - это максимальная токовая защита от междуфазных коротких замыканий. Защита состоит из двух раздельных частей - двух ступейней - МТЗ1 и МТЗ2."+Environment.NewLine+
                " Каждая из ступеней имеет уставку по силе тока и временную выдержку. Если сила тока в сети возрастает выше значения уставки МТЗ1 или МТЗ2, то через время выдержки МТЗ"+
                " выключает выходные реле (встроеные в приборе, всего 5 реле). МТЗ следит за силой токов разных фаз - фазы А, В и С. Соответственно она следит за значениями силой токов"+
                " Ia, Ib и Ic."+Environment.NewLine+"Протестируем функцию защиты МТЗ."+Environment.NewLine+"1)Добавим МТЗ в меню прибора:"+Environment.NewLine+"Конфигурация-МТЗ-ЕСТЬ, пароль для разрешения операции-1111.";
            string t2 = "2) Включим первую ступень МТЗ:"+Environment.NewLine+
                "МТЗ->Управление->1 Ступень МТЗ->ВКЛ; пароль- 1111";
            string t3 = "3) Настроим уставку МТЗ1 на 5А:"+Environment.NewLine+
                "МТЗ->Уставка->Уставка МТЗ1-> 5A, пароль 1111";
            string t4 = "4) Настроим выдержку МТЗ1. Выдержка есть время через которое будет срабатывать МТЗ1."+Environment.NewLine+
                "Настроим выдержку на 0.5 секунды:"+Environment.NewLine+"МТЗ-> Выдержки-> Выдержка МТЗ1-> 0.5с, пароль 1111";
            string t5 = "5) Настроим первое выходное реле на срабатывание от МТЗ1. Если в электирческой сети, к которой присоединен МРЗС будет обнаруже ток, "+
                "превышающий уставку МТЗ1, то должно выключиться первое реле."+Environment.NewLine+
                "Для настройки реле на срабатывание от защиты МТЗ1 нужно активировать параметр 'Сраб МТЗ1' в меню прибора:"+Environment.NewLine+
                "Настройка-> Выходы ком.-> Р01-> Сраб МТЗ1-> ДА, пароль - 1111"+Environment.NewLine+Environment.NewLine+
                "По такому же принципу можна настроить выходное реле на срабатывание от МТЗ2 (вторая ступень МТЗ)."+Environment.NewLine+
                "МТЗ2 отличаэться от МТЗ1 лишь в использование БУ- блока ускорения. Более детально смотрите в инструкции по эксплуатации МРЗС";
            string t6 = "6) Установите на элемент управления Ia = 6A. Включите прибор. Через 0,5с должно отключиться первое выходное реле МРЗС.";
            string t7 = "7) Настроим первый светодиодный индикатор. Светоиндикаторы предназначены для уведомления о сработанных функциях прибора. На каждый "+
                "светодиод можно включить уведомление о сработанных нескольких функции."+Environment.NewLine+" Настроим первый светоиндикатор (СДИ1) на "+
                "срабатывание МТЗ1:"+Environment.NewLine+"Настройка-> Индикация-> СДИ1-> Сраб МТЗ1-> ДА; пароль 1111"+Environment.NewLine+
                "Отключите, а затем включите прибор. Через 0.5с должно отключиться реле и загореться первый светоиндикатор.";
            string t8 = "8) Рассмотрим дискреные входы. Дискретные входы (ДВ) предназначены для управлением логикой прибора. Так их можна настроить на запрет защитных функций МРЗС."+Environment.NewLine+
                "Настроим первый дискретный вход на отключение МТЗ1:" + Environment.NewLine + "Настройка-> Входы-> ДВ01-> Сраб МТЗ1-> ДА; пароль 1111."+Environment.NewLine+
                "Далее отключите прибор. Нажмите указателем мышы на лампочку первого дискретного входа(по умолчанию серого цвета), при этом дискреный вход активируется (зеленый цвет)."+
                Environment.NewLine+"Включите прибор. Теперь первое выходное реле не выключиться, а первый светоиндикатор не загориться, поскольку МТЗ1 не будет срабатывать.";
            string t9 = "";
            string t10 = "";
            string t11 = "";
            string t12 = ""; 

            tasks.Add(t1);
            tasks.Add(t2);
            tasks.Add(t3);
            tasks.Add(t4);
            tasks.Add(t5);
            tasks.Add(t6);
            tasks.Add(t7);
            tasks.Add(t8);
        }
        internal string checkMTZ1()
        {
            if (!MtzCtrl.IsTurnOn()) return mrzsErrors.mtzError1;
            else return mrzsErrors.SucfRez;
        }
        internal string checkMTZ2()
        {
            if (!MtzCtrl.IsMTZ1TurnOn()) return mrzsErrors.mtzError2;
            else return mrzsErrors.SucfRez;
        }
        internal string checkMTZ3()
        {
            if (MtzCtrl.getMTZ1Value() != 5) return mrzsErrors.mtzError3;
            else return mrzsErrors.SucfRez;
        }
        internal string checkMTZ4()
        {
            if (MtzCtrl.getTimerMtz1() != 0.5) return mrzsErrors.mtzError8;
            else return mrzsErrors.SucfRez;
        }
        internal string checkMTZ5()
        {
            if (!ReleCtrl.IsRele1ConfiguredOnMTZ1()) return mrzsErrors.mtzError4;
            else return mrzsErrors.SucfRez;
        }
        internal string checkMTZ6(double Ia, double Ib,double Ic)
        {
            if ((Ia < 6 || Ia > 6.9) && (Ic < 6 || Ic > 6.9) && (Ib < 6 || Ib > 6.9)) return mrzsErrors.NumUpDownError1;
            else return mrzsErrors.SucfRez;
        }
        internal string checkMTZ7()
        {
            if (!SdiCtrl.IsSD1ConfiguredOnMTZ1()) return mrzsErrors.SDIError1;
            else return mrzsErrors.SucfRez;
        }
        internal string checkMTZ8(InputDV dv1)
        {
            if (!DVCtrl.IsDV1ConfOnTurnOFFMtz1()) return mrzsErrors.DVError2;
            else if (!dv1.IsChecked.Value) return mrzsErrors.DVError3;
            else return mrzsErrors.SucfRez;
        }

        internal string getFirstTask()
        {
            CurrentTask = 0;
            return tasks[0];
        }
        internal string getNextTask()
        {
            if (CurrentTask < (tasks.Count - 1))
            {
                CurrentTask += 1;
                return tasks[CurrentTask];
            }
            else return null;
        }
        internal string getPrevTask()
        {
            if (CurrentTask > 0)
            {
                CurrentTask -= 1;
                return tasks[CurrentTask];
            }
            else return null;
        }
        internal int getCurrentTaskNumber()
        {
            return CurrentTask;
        }
    }
}
