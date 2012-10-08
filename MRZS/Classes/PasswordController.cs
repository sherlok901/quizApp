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
using MRZS.Web.Models;
using MRZS.Classes;
using MRZS.Classes.DisplayCode;

namespace MRZS.Classes
{
    static internal class PasswordController
    {
        static internal bool CheckPassOnce { get; set; }
        static DisplayViewModel DisplayControlr;
        enum states
        {            
            CheckPassword,
            passwordCorrect,
            passwordInCorrect,
            onlyView,
            allowedEnterOrChooseValue,
            canShowValue,
            confirmOrCanseledMemorize,
            MemoriseInputedVal,
            passwordAsk
        };
        static states St = states.passwordAsk;        

        static internal void setDisplayController(DisplayViewModel dispContr)
        {
            DisplayControlr = dispContr;
        }
        static void inputPasswordProcess()
        {
            DisplayControlr.showText("Введите пароль", string.Empty);
        }
        
        static internal void passwordProcess()
        {
            switch (St)
            {                
                case states.passwordAsk:
                    DisplayControlr.showText("Введите пароль", string.Empty);
                    //set next state
                    St = states.CheckPassword;
                    break;
                case states.CheckPassword:
                    if (DisplayControlr.SecondMenuStr == "1111")
                    {
                        DisplayControlr.showText("Пароль введен", "  верно");
                        //set next state
                        St = states.passwordCorrect;
                    }
                    else
                    {
                        DisplayControlr.showText("Пароль введен", "  неверно");
                        //set next state
                        St = states.passwordInCorrect;
                    }                    
                    break;                
                case states.passwordCorrect:
                    St = states.allowedEnterOrChooseValue;
                    break;
                case states.allowedEnterOrChooseValue:
                    St = states.canShowValue;
                    break;
            }
        }
        //set first state
        static internal void setPasswordAsk()
        {
            St = states.passwordAsk;
        }
        //set waiting state
        static internal void setWaintingState()
        {
            St = states.confirmOrCanseledMemorize;
        }
        static internal void setAllowedEnterOrChooseValue()
        {
            St = states.allowedEnterOrChooseValue;
        }
        static internal bool canShowValueWithSelection()
        {
            if (St == states.allowedEnterOrChooseValue) return true;
            else return false;
        }
        //can show changeble value\choosed other value
        static internal bool canShowChangebleValue()
        {
            if (St == states.canShowValue) return true;
            else return false;
        }
        static internal bool isWaitingState()
        {
            if (St == states.confirmOrCanseledMemorize) return true;
            else return false;
        }
        static internal bool CanChangeValue()
        {
            if (St == states.passwordInCorrect) return true;
            else return false;
        }
    }
}
