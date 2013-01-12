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

namespace MRZS.Classes.Errors
{
    internal class MrzsErrors
    {
        internal string mtzError1 = "Вы не включили МТЗ";//mtzError1
        internal string mtzError2 = "Вы не включили МТЗ1";//er2
        internal string mtzError3 = "Не верно настроеная Уставка МТЗ1";//er3
        internal string mtzError4 = "Не настроено выходное реле1 на МТЗ1";//er4
        internal string mtzError5 = "Вы не включили МТЗ2";//er6
        internal string mtzError6 = "Не верно настроеная Уставка МТЗ2";//er7
        internal string mtzError7 = "Не настроено выходное реле1 на МТЗ2";//er8
        internal string mtzError8 = "Не верное значение выдержки МТЗ1";//er13

        internal string NumUpDownError1= "Вы не настроили значение Ia или Ib или Ic";//er5
        internal string NumUpDownError2 = "Одно из значений Ia или Ib или Ic должно быть больше мтз1";//er16
        internal string NumUpDownError3 = "Одно из значений Ia или Ib или Ic должно быть больше мтз2";//er17
        
        internal string SDIError1 = "Индикатор 1 не не настроен на МТЗ1";//er12

        internal string DVError1 = "Выключите ДВ";//er9
        internal string DVError2 = "ДВ1 не настроен на отключение МТЗ1";//er10
        internal string DVError3 = "ДВ1 не включен";//er11
                
        internal string ApvError1 = "Вы не включили АПВ";//er14
        internal string ApvError2 = "Не верное значение выдержки АПВ (1 цикла)";//er15
        
        internal string SucfRez = "Все настроено верно";
  
    }
}
