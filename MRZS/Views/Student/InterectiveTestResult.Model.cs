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
using MRZS.Models;
using MRZS.Web.Services;
using System.ServiceModel.DomainServices.Client;
using MRZS.Web.Models;

namespace MRZS.Views.Student
{
    public class InterectiveTestResultModel : BaseViewModel
    {
        private InterectiveTestLogContext interectiveTestLogContext;
        public EntitySet<InterectiveTestLog> interectiveTestResults
        { get { return interectiveTestLogContext.InterectiveTestLogs; } }


        public InterectiveTestResultModel(int sectionId)
        {
            interectiveTestLogContext = new InterectiveTestLogContext();

            interectiveTestLogContext.Load(interectiveTestLogContext.GetInterectiveTestLogsForSectionQuery(sectionId));
        }
    }
}
