using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MRZS.Views.Emulator;
using System.Windows.Controls;
using System.Windows;

namespace MRZStests
{
    
    public class Class1
    {
        [Test]
        public void Test1()
        {                  
            MRZS.Views.Emulator.Emulator_05M emul=new Emulator_05M();

            //object obj = RunInstanceMethod(
            //    typeof(MRZS.Classes.EmulatorDisplayController),
            //    "IndexOfnextFirstSymbolFinding",
            //    MRZS.Classes.EmulatorDisplayController, new object[2] { 2, 3 });

            //int rez = Convert.ToInt32(obj);

            //Assert.AreEqual(5, rez);
        }
        public static object RunInstanceMethod(System.Type t, string strMethod, object objInstance, object[] aobjParams)
        {
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return RunMethod(t, strMethod, objInstance, aobjParams, eFlags);
        }
        private static object RunMethod(System.Type t, string strMethod, object objInstance, object[] aobjParams, BindingFlags eFlags)
        {
            MethodInfo m;
            try
            {
                m = t.GetMethod(strMethod, eFlags);
                if (m == null)
                {
                    throw new ArgumentException("There is no method '" +
                     strMethod + "' for type '" + t.ToString() + "'.");
                }

                object objRet = m.Invoke(objInstance, aobjParams);
                return objRet;
            }
            catch
            {
                throw;
            }
        }
    }
}
