using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MRZS.Web.Models;
using MRZS.Web.Services;

namespace MRZS.Classes
{
    internal class LoadData
    {
        LoadOperation<mrzs05mMenu> mrzs05mMModel;
        IEnumerable<mrzs05mMenu> mrzs05Entity;

        LoadData()
        {
            mrzs05mMenuContext mrzs05mMContxt = new mrzs05mMenuContext();
            mrzs05mMModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetMrzs05mMenuQuery());
            mrzs05mMModel.Completed += mrzs05mMModel_Completed;
        }

        void mrzs05mMModel_Completed(object sender, EventArgs e)
        {
            mrzs05Entity = mrzs05mMModel.Entities;
        }
    }
}
