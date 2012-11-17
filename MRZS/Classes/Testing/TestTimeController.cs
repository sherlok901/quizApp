using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MRZS.Classes.Testing
{
    internal class TestTimeController
    {
        DateTime StartTest;
        DateTime EndTest;
        int QuestionCount;


        internal void Start()
        {
            //how mamy question
            QuestionCount = LoadData.TestQuestionTable.ToList().Count;
            //init date
            StartTest = DateTime.Now;            
            //one minute per 1 question
            TimeSpan MinutePerQuestion=new TimeSpan(0,QuestionCount,0);
            //get end time of test
            EndTest=StartTest+MinutePerQuestion;
                        
        }
        //how time remains to end of test
        internal TimeSpan howReimains()
        {
            return EndTest - DateTime.Now;
        }

    }

}
