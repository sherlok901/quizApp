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
    internal static class LoadData
    {        
        static LoadOperation<mrzs05mMenu> mrzs05mMModel;
        static LoadOperation<mrzsInOutOption> mrzsInOutOptionModel;
        static LoadOperation<passwordCheckType> passwordCheckTypeModel;
        static LoadOperation<kindSignalDC> kindSignalDCModel;
        static LoadOperation<typeSignalDC> typeSignalDCModel;
        static LoadOperation<typeFuncDC> typeFuncDCModel;
        static LoadOperation<BooleanVal> BooleanValModel;
        static LoadOperation<BooleanVal2> BooleanVal2Model;
        static LoadOperation<BooleanVal3> BooleanVal3Model;
        static LoadOperation<mtzVal> mtzValModel;
        static LoadOperation<mrzsInOutOption> mrzsInOutOptModel;
        static LoadOperation<TestAnswer> TestAnswerModel;
        static LoadOperation<TestQuestion> TestQuestionModel;
        static LoadOperation<TestResult> TestResultModel;
        static LoadOperation<TestingImage> TestingImageModel;
        static IEnumerable<mrzs05mMenu> mrzs05Entity;
        static IEnumerable<passwordCheckType> listPass2 = null;
        static IEnumerable<kindSignalDC> kindSignalDCList = null;
        static IEnumerable<typeSignalDC> typeSignalDCList = null;
        static IEnumerable<typeFuncDC> typeFuncDCList = null;
        static IEnumerable<BooleanVal> BooleanValList = null;
        static IEnumerable<BooleanVal2> BooleanVal2List = null;
        static IEnumerable<BooleanVal3> BooleanVal3List = null;
        static IEnumerable<mtzVal> mtzValList = null;
        static IEnumerable<mrzsInOutOption> mrzsInOutOptionList = null;
        static IEnumerable<TestAnswer> TestAnswerList = null;
        static IEnumerable<TestQuestion> TestQuestionList = null;
        static IEnumerable<TestResult> TestResultList = null;
        static IEnumerable<TestingImage> TestingImageList = null;

        public static IEnumerable<mrzs05mMenu> MrzsTable { get { return mrzs05Entity; } }
        public static IEnumerable<passwordCheckType> passwordCheckTypeTable { get { return listPass2; } }
        public static IEnumerable<kindSignalDC> kindSignalDCTable { get { return kindSignalDCList; } }
        public static IEnumerable<typeSignalDC> typeSignalDCTable { get { return typeSignalDCList; } }
        public static IEnumerable<typeFuncDC> typeFuncDCTable { get { return typeFuncDCList; } }
        public static IEnumerable<BooleanVal> BooleanValTable { get { return BooleanValList; } }
        public static IEnumerable<BooleanVal2> BooleanVal2Table { get { return BooleanVal2List; } }
        public static IEnumerable<BooleanVal3> BooleanVal3Table { get { return BooleanVal3List; } }
        public static IEnumerable<mtzVal> mtzValTable { get { return mtzValList; } }
        public static IEnumerable<mrzsInOutOption> mrzsInOutOptionTable { get { return mrzsInOutOptionList; } }
        internal static IEnumerable<TestAnswer> TestAnswerTable { get { return TestAnswerList; } }
        internal static IEnumerable<TestQuestion> TestQuestionTable { get { return TestQuestionList; } }
        internal static IEnumerable<TestResult> TestResultTable { get { return TestResultList; } }
        internal static IEnumerable<TestingImage> TestingImageTable { get { return TestingImageList; } }
        public static event EventHandler DataLoaded;
        static mrzs05mMenuContext mrzs05mMContxt = null;

        static LoadData()
        {
            //MessageBox.Show("Hello from MessageBox");
            try
            {
                mrzs05mMContxt = new mrzs05mMenuContext();
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

                TestAnswerContext TestAnswContxt = new TestAnswerContext();
                TestAnswerModel = TestAnswContxt.Load(TestAnswContxt.GetTestAnswersQuery());
                TestAnswerModel.Completed += TestAnswerModel_Completed;

                TestQuestionContext TestQuestContxt = new TestQuestionContext();
                TestQuestionModel = TestQuestContxt.Load(TestQuestContxt.GetTestQuestionsQuery());
                TestQuestionModel.Completed += TestQuestionModel_Completed;

                TestResultsContext TestResContxt = new TestResultsContext();
                TestResultModel = TestResContxt.Load(TestResContxt.GetTestResultsQuery());
                TestResultModel.Completed += TestResultModel_Completed;

                TestImagesContext TestImgContxt = new TestImagesContext();
                TestingImageModel = TestImgContxt.Load(TestImgContxt.GetTestingImagesQuery());
                TestingImageModel.Completed += TestingImageModel_Completed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //for test===
        static void TestingImageModel_Completed(object sender, EventArgs e)
        {
            TestingImageList = TestingImageModel.Entities;
            checkNotNullTables();
        }

        static void TestResultModel_Completed(object sender, EventArgs e)
        {
            TestResultList = TestResultModel.Entities;
            checkNotNullTables();
        }

        static void TestQuestionModel_Completed(object sender, EventArgs e)
        {
            TestQuestionList = TestQuestionModel.Entities;
            checkNotNullTables();
        }

        static void TestAnswerModel_Completed(object sender, EventArgs e)
        {
            TestAnswerList = TestAnswerModel.Entities;
            checkNotNullTables();
        }

        //for canseled saving
        internal static void rejectAllChanges()
        {
            mrzs05mMContxt.RejectChanges();
        }
        //for saving
        internal static void savingAllChanges()
        {
            mrzs05mMContxt.SubmitChanges();          
        }

        static void mrzsInOutOptModel_Completed(object sender, EventArgs e)
        {
            mrzsInOutOptionList = mrzsInOutOptModel.Entities;
            checkNotNullTables();
        }
        static void mtzValModel_Completed(object sender, EventArgs e)
        {
            mtzValList = mtzValModel.Entities;
            checkNotNullTables();
        }
        static void BooleanValModel_Completed(object sender, EventArgs e)
        {
            BooleanValList = BooleanValModel.Entities;
            checkNotNullTables();
        }
        static void BooleanVal3Model_Completed(object sender, EventArgs e)
        {
            BooleanVal3List = BooleanVal3Model.Entities;
            checkNotNullTables();
        }

        static void BooleanVal2Model_Completed(object sender, EventArgs e)
        {
            BooleanVal2List = BooleanVal2Model.Entities;
            checkNotNullTables();
        }

        static void typeFuncDCModel_Completed(object sender, EventArgs e)
        {
            typeFuncDCList = typeFuncDCModel.Entities;
            checkNotNullTables();
        }

        static void typeSignalDCModel_Completed(object sender, EventArgs e)
        {
            typeSignalDCList = typeSignalDCModel.Entities;
            checkNotNullTables();
        }

        static void kindSignalDCModel_Completed(object sender, EventArgs e)
        {
            kindSignalDCList = kindSignalDCModel.Entities;
            checkNotNullTables();
        }

        static void passwordCheckTypeModel_Completed(object sender, EventArgs e)
        {
            listPass2 = passwordCheckTypeModel.Entities;
            checkNotNullTables();
        }

        static void mrzs05mMModel_Completed(object sender, EventArgs e)
        {
            mrzs05Entity = mrzs05mMModel.Entities;
            checkNotNullTables();
        }

        //check that tables are loaded
        internal static bool checkNotNullTables()
        {
            if (mrzs05Entity != null &&
                listPass2 != null &&
                kindSignalDCList != null &&
                typeSignalDCList != null &&
                typeFuncDCList != null &&
                BooleanValList != null &&
                BooleanVal2List != null &&
                BooleanVal3List != null &&
                mtzValList != null &&
                TestAnswerList != null &&
                TestQuestionList != null &&
                TestResultList != null &&
                TestingImageList != null
                ) //data loaded alert
            {
                if (DataLoaded != null) DataLoaded(null, EventArgs.Empty);
                return true;
            }
            else return false;
        }
    }
}
