using System;
using System.Collections.Generic;
using MRZS.Classes.Errors;
using MRZS.Classes.InterTesting;
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
            string t1 = "Функция защиты МТЗ - это максимальная токовая защита от междуфазных коротких замыканий. Защита состоит из двух раздельных частей - двух ступейней - МТЗ1 и МТЗ2."+Environment.NewLine+
                " Каждая из ступеней имеет уставку по силе тока и временную выдержку. Если сила тока в сети возрастает выше значения уставки МТЗ1 или МТЗ2, то через время выдержки МТЗ"+
                " выключает выходные реле (встроеные в приборе, всего 5 реле). МТЗ следит за силой токов разных фаз - фазы А, В и С. Соответственно она следит за значениями силой токов"+
                " Ia, Ib и Ic."+Environment.NewLine+"Протестируем функцию защиты МТЗ."+Environment.NewLine+"1)Добавим МТЗ в меню прибора:"+Environment.NewLine+"Конфигурация-МТЗ-ЕСТЬ, пароль для разрешения операции-1111.";
            string t2 = "Включим первую ступень МТЗ:"+Environment.NewLine+
                "МТЗ->Управление->1 Ступень МТЗ->ВКЛ; пароль- 1111";
            string t3 = "Настроим уставку МТЗ1 на 5А:"+Environment.NewLine+
                "МТЗ->Уставка->Уставка МТЗ1-> 5A, пароль 1111";
            string t4 = "Настроим выдержку МТЗ1. Выдержка есть время через которое будет срабатывать МТЗ1."+Environment.NewLine+
                "Настроим выдержку на 0.5 секунды:"+Environment.NewLine+"МТЗ-> Выдержки-> Выдержка МТЗ1-> 0.5с, пароль 1111";
            string t5 = "Настроим первое выходное реле на срабатывание от МТЗ1. Если в электирческой сети, к которой присоединен МРЗС будет обнаруже ток, "+
                "превышающий уставку МТЗ1, то должно выключиться первое реле."+Environment.NewLine+
                "Для настройки реле на срабатывание от защиты МТЗ1 нужно активировать параметр 'Сраб МТЗ1' в меню прибора:"+Environment.NewLine+
                "Настройка-> Выходы ком.-> Р01-> Сраб МТЗ1-> ДА, пароль - 1111"+Environment.NewLine+Environment.NewLine+
                "По такому же принципу можна настроить выходное реле на срабатывание от МТЗ2 (вторая ступень МТЗ)."+Environment.NewLine+
                "МТЗ2 отличаэться от МТЗ1 лишь в использование БУ- блока ускорения. Более детально смотрите в инструкции по эксплуатации МРЗС";
            string t6 = "";
            string t7 = "";
            string t8 = "";
            string t9 = "";
            string t10 = "";
            string t11 = "";
            string t12 = ""; 

            tasks.Add(t1);
            tasks.Add(t2);
            tasks.Add(t3);
            tasks.Add(t4);
            tasks.Add(t5);
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
