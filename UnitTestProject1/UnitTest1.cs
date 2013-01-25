using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;
using MRZS.Classes;

namespace UnitTestProject1
{
    [TestClass]  
    public class UnitTest1
    {
        string s = "Уставка МТЗ1\r\n0002.0000 A";
        
        [TestMethod]
        public void TestREGEX()
        {            
            string rez = Inputing.getNumsFromStr(s);
            Assert.AreEqual("0002.0000", rez);
        }
        [TestMethod]
        public void TestIndexOf()
        {
            string rez = Inputing.getNumsFromStr(s);
            int index = s.IndexOf(rez);
            Assert.AreEqual(14, index);
        }
        [TestMethod]
        public void Test_getIndexes()
        {
            List<int> list = new List<int>(0);
            list.Add(14);
            list.Add(15);
            list.Add(16);
            list.Add(17);

            list.Add(19);
            list.Add(20);
            list.Add(21);
            list.Add(22);
            List<int> rezList = Inputing.getIndexes(s);
            CollectionAssert.AreEqual(list, rezList);                   
        }
    }

}
