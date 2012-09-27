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
        LoadOperation<mrzsInOutOption> mrzsInOutOptionModel;
        LoadOperation<passwordCheckType> passwordCheckTypeModel;
        LoadOperation<kindSignalDC> kindSignalDCModel;
        LoadOperation<typeSignalDC> typeSignalDCModel;
        LoadOperation<typeFuncDC> typeFuncDCModel;
        LoadOperation<BooleanVal> BooleanValModel;
        LoadOperation<BooleanVal2> BooleanVal2Model;
        LoadOperation<BooleanVal3> BooleanVal3Model;
        LoadOperation<mtzVal> mtzValModel;
        LoadOperation<mrzsInOutOption> mrzsInOutOptModel;
        IEnumerable<mrzs05mMenu> mrzs05Entity;
        IEnumerable<passwordCheckType> listPass2 = null;
        IEnumerable<kindSignalDC> kindSignalDCList = null;
        IEnumerable<typeSignalDC> typeSignalDCList = null;
        IEnumerable<typeFuncDC> typeFuncDCList = null;
        IEnumerable<BooleanVal> BooleanValList = null;
        IEnumerable<BooleanVal2> BooleanVal2List = null;
        IEnumerable<BooleanVal3> BooleanVal3List = null;
        IEnumerable<mtzVal> mtzValList = null;
        IEnumerable<mrzsInOutOption> mrzsInOutOptionList = null;
        public IEnumerable<mrzs05mMenu> MrzsTable { get { return mrzs05Entity; } }
        public IEnumerable<passwordCheckType> passwordCheckTypeTable {get{return  listPass2; }}
        public IEnumerable<kindSignalDC> kindSignalDCTable { get { return kindSignalDCList; } }
        public IEnumerable<typeSignalDC> typeSignalDCTable { get { return typeSignalDCList; } }
        public IEnumerable<typeFuncDC> typeFuncDCTable {get{return  typeFuncDCList; }}
        public IEnumerable<BooleanVal> BooleanValTable {get{return  BooleanValList; }}
        public IEnumerable<BooleanVal2> BooleanVal2Table {get{return  BooleanVal2List; }}
        public IEnumerable<BooleanVal3> BooleanVal3Table {get{return  BooleanVal3List; }}
        public IEnumerable<mtzVal> mtzValTable {get{return  mtzValList; }}
        public IEnumerable<mrzsInOutOption> mrzsInOutOptionTable {get{return  mrzsInOutOptionList; }}
        public event EventHandler DataLoaded;

        internal LoadData()
        {
            mrzs05mMenuContext mrzs05mMContxt = new mrzs05mMenuContext();
            mrzs05mMModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetMrzs05mMenuQuery());
            mrzs05mMModel.Completed += mrzs05mMModel_Completed;
            passwordCheckTypeModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetPasswordCheckTypesQuery());
            passwordCheckTypeModel.Completed += passwordCheckTypeModel_Completed;
            kindSignalDCModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetKindSignalDCsQuery());
            kindSignalDCModel.Completed += kindSignalDCModel_Completed;
            typeSignalDCModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetTypeSignalDCsQuery());
            typeSignalDCModel.Completed += typeSignalDCModel_Completed;
            typeFuncDCModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetTypeFuncDCsQuery());
            typeFuncDCModel.Completed += typeFuncDCModel_Completed;
            BooleanValModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetBooleanValsQuery());
            BooleanValModel.Completed += BooleanValModel_Completed;
            BooleanVal2Model = mrzs05mMContxt.Load(mrzs05mMContxt.GetBooleanVal2Query());
            BooleanVal2Model.Completed += BooleanVal2Model_Completed;
            BooleanVal3Model = mrzs05mMContxt.Load(mrzs05mMContxt.GetBooleanVal3Query());
            BooleanVal3Model.Completed += BooleanVal3Model_Completed;
            mtzValModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetMtzValsQuery());
            mtzValModel.Completed += mtzValModel_Completed;
            mrzsInOutOptModel = mrzs05mMContxt.Load(mrzs05mMContxt.GetMrzsInOutOptionsQuery());
            mrzsInOutOptModel.Completed += mrzsInOutOptModel_Completed;
            
        }

        void mrzsInOutOptModel_Completed(object sender, EventArgs e)
        {
            mrzsInOutOptionList = mrzsInOutOptModel.Entities;
            checkNotNullTables();
        }
        void mtzValModel_Completed(object sender, EventArgs e)
        {
            mtzValList = mtzValModel.Entities;
            checkNotNullTables();
        }
        void BooleanValModel_Completed(object sender, EventArgs e)
        {
            BooleanValList = BooleanValModel.Entities;
            checkNotNullTables();
        }
        void BooleanVal3Model_Completed(object sender, EventArgs e)
        {
            BooleanVal3List = BooleanVal3Model.Entities;
            checkNotNullTables();
        }

        void BooleanVal2Model_Completed(object sender, EventArgs e)
        {
            BooleanVal2List = BooleanVal2Model.Entities;
            checkNotNullTables();
        }

        void typeFuncDCModel_Completed(object sender, EventArgs e)
        {
            typeFuncDCList = typeFuncDCModel.Entities;
            checkNotNullTables();
        }

        void typeSignalDCModel_Completed(object sender, EventArgs e)
        {
            typeSignalDCList = typeSignalDCModel.Entities;
            checkNotNullTables();
        }

        void kindSignalDCModel_Completed(object sender, EventArgs e)
        {
            kindSignalDCList = kindSignalDCModel.Entities;
            checkNotNullTables();
        }

        void passwordCheckTypeModel_Completed(object sender, EventArgs e)
        {
            listPass2 = passwordCheckTypeModel.Entities;
            checkNotNullTables();
        }

        void mrzs05mMModel_Completed(object sender, EventArgs e)
        {
            mrzs05Entity = mrzs05mMModel.Entities;
            checkNotNullTables();
        }

        //check that tables are loaded
        private void checkNotNullTables()
        {
            if (mrzs05Entity != null &&
                listPass2 != null &&
                kindSignalDCList != null &&
                typeSignalDCList != null &&
                typeFuncDCList != null &&
                BooleanValList != null &&
                BooleanVal2List != null &&
                BooleanVal3List != null &&
                mtzValList != null) //data loaded alert
                    if (DataLoaded != null) DataLoaded(this, EventArgs.Empty);            
        }
    }
}
