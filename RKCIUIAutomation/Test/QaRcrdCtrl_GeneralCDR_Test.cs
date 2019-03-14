using NUnit.Framework;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.GeneralCDR;

namespace RKCIUIAutomation.Test.CDR
{


    [TestFixture]//complete, updated hiptest
    public class Verify_CDR_SimpleWF_End_To_End : TestBase
    {
        [Test]
        [Category(Component.CDR)]
        [Property(Component2,Component.CDR_WF_Simple)]
        [Property(TestCaseNumber, 2584703)]
        [Property(Priority, "High")]
        [Description("To validate simple workflow for CDR module end-to-end.")]
        public void CDR_SimpleWF_End_To_End()
        {
            string cdrDescription = WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.CDRTech);
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.QC_Review, cdrDescription));
            ClickEditBtnForRow();
            LogInfo("------------send to revise from Review------------");
            QaRcrdCtrl_GeneralCDR.ClickBtn_Revise();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Revise, cdrDescription));
            ClickEditBtnForRow();

            LogInfo("------------cancel, edit/saveonly in Revise------------");
            QaRcrdCtrl_GeneralCDR.EnterDescription("New CDR Description", true);
            QaRcrdCtrl_GeneralCDR.ClickBtn_Cancel();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Revise, cdrDescription));
            ClickEditBtnForRow();
            cdrDescription = QaRcrdCtrl_GeneralCDR.EnterDescription();
            QaRcrdCtrl_GeneralCDR.ClickBtn_SaveOnly();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Revise, cdrDescription));
            ClickEditBtnForRow();

            LogInfo("------------save&fwd in Review------------");
            QaRcrdCtrl_GeneralCDR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.QC_Review, cdrDescription));
            ClickEditBtnForRow();

            LogInfo("------------save&fwd in ToBeClosed------------");
            QaRcrdCtrl_GeneralCDR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.To_Be_Closed, cdrDescription));
            ClickEditBtnForRow();

            LogInfo("------------send Back to QC Review from ToBeClosed------------");
            QaRcrdCtrl_GeneralCDR.ClickBtn_Back_To_QC_Review();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.QC_Review, cdrDescription));
            ClickEditBtnForRow();

            LogInfo("------------save&fwd from QC Review to ToBeClosed------------");
            QaRcrdCtrl_GeneralCDR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.To_Be_Closed, cdrDescription));
            ClickEditBtnForRow();

            LogInfo("------------verify closed in Closed tab------------");
            //QaRcrdCtrl_GeneralCDR.ClickBtn_SaveForward();
            //AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.To_Be_Closed, cdrDescription));
            //ClickEditBtnForRow();
            QaRcrdCtrl_GeneralCDR.ClickBtn_CloseCDR();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Closed_DN, cdrDescription));
            AssertAll();
        }
    }

    [TestFixture]//complete, updated hiptest
    public class Verify_CDR_ComplexWF_End_To_End : TestBase
    {
        [Test]
        [Category(Component.CDR)]
        [Property(Component2, Component.CDR_WF_Complex)]
        [Property(TestCaseNumber, 2584942)]
        [Property(Priority, "High")]
        [Description("To validate complex workflow for CDR module end-to-end.")]
        public void CDR_ComplexWF_End_To_End()
        {
            string cdrDescription = WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.CDRTech);
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.QC_Review, cdrDescription), "VerifyCDRDocIsDisplayed(TableTab.QC_Review)");
            ClickEditBtnForRow();
            LogInfo("------------send to revise from Review------------");
            QaRcrdCtrl_GeneralCDR.ClickBtn_Revise();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Revise, cdrDescription), "VerifyCDRDocIsDisplayed(TableTab.Revise)");
            ClickEditBtnForRow();

            LogInfo("------------cancel, edit/saveonly in Revise------------");
            QaRcrdCtrl_GeneralCDR.EnterDescription("New CDR Description", true);
            QaRcrdCtrl_GeneralCDR.ClickBtn_Cancel();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Revise, cdrDescription), "VerifyCDRDocIsDisplayed(TableTab.Revise)");
            ClickEditBtnForRow();
            cdrDescription = QaRcrdCtrl_GeneralCDR.EnterDescription();
            QaRcrdCtrl_GeneralCDR.ClickBtn_SaveOnly();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Revise, cdrDescription), "VerifyCDRDocIsDisplayed(TableTab.Revise)");
            ClickEditBtnForRow();

            LogInfo("------------save&fwd in Review------------");
            QaRcrdCtrl_GeneralCDR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.QC_Review, cdrDescription), "VerifyCDRDocIsDisplayed(TableTab.QC_Review)");
            ClickEditBtnForRow();

            LogInfo("------------save&fwd in Disposition------------");
            QaRcrdCtrl_GeneralCDR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Disposition, cdrDescription), "VerifyCDRDocIsDisplayed(TableTab.Disposition)");
            ClickEditBtnForRow();


            LogInfo("------------save&fwd in ToBeClosed------------");
            QaRcrdCtrl_GeneralCDR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.To_Be_Closed, cdrDescription), "VerifyCDRDocIsDisplayed(TableTab.To_Be_Closed)");
            ClickEditBtnForRow();

            LogInfo("------------send Back to Disposition from ToBeClosed------------");
            QaRcrdCtrl_GeneralCDR.ClickBtn_Back_To_Disposition();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Disposition, cdrDescription), "VerifyCDRDocIsDisplayed(TableTab.Disposition)");
            ClickEditBtnForRow();

            LogInfo("------------send back to QC review from Disposition------------");
            QaRcrdCtrl_GeneralCDR.ClickBtn_Back_To_QC_Review();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.QC_Review, cdrDescription), "VerifyCDRDocIsDisplayed(TableTab.QC_Review)");
            ClickEditBtnForRow();

            LogInfo("------------save&fwd in Disposition------------");
            QaRcrdCtrl_GeneralCDR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Disposition, cdrDescription), "VerifyCDRDocIsDisplayed(TableTab.Disposition)");
            ClickEditBtnForRow();

            LogInfo("------------verify closed in Closed tab------------");
            QaRcrdCtrl_GeneralCDR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.To_Be_Closed, cdrDescription), "VerifyCDRDocIsDisplayed(TableTab.To_Be_Closed)");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralCDR.ClickBtn_CloseCDR();
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Closed_DN, cdrDescription), "VerifyCDRDocIsDisplayed(TableTab.Closed_DN)");
            AssertAll();
        }
    }

    [TestFixture]
    public class Verify_Create_And_Save_CDR_Document : TestBase
    {
       // [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187609)]
        [Property(Priority, "High")]
        [Description("To validate successful create and save of an CDR (Construction Deficiency Report) document.")]
        public void Create_And_Save_CDR_Document()
        {
            string cdrDescription= WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.CDRTech);
            AddAssertionToList(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.QC_Review), "VerifyCDRDocIsDisplayedinQCReviewTab");
            AssertAll();
        }
    }

    [TestFixture]
    public class Verify_QC_Review_of_CDR_document : TestBase
    {
       // [Test]
        [Category(Component.CDR)]
        [Property(Component2, Component.CDR_WF_Complex)]
        [Property(TestCaseNumber, 2187610)]
        [Property(Priority, "High")]
        [Description("To validate the QC review part of an CDR (Construction Deficiency Report).GLX and I15s only")]
        public void QC_Review_of_CDR_document()
        {
            //string cdrDescription = "UASpeuycVMIunVSWlLAYEEypBS"; //chnage the name once you create new CDR
            //WF_QaRcrdCtrl_GeneralCDR.ReviewAndApproveCDRDocument(UserType.CDRTech, cdrDescription);
            //string cdrDescription = WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.CDRTech);
            //LogoutToLoginPage();
            string cdrDescription = WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.CDRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralCDR.ReviewCDRDocument(UserType.CDRTech, cdrDescription);
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Disposition, cdrDescription));
           
        }
    }

    [TestFixture]
    public class Verify_QC_Review_of_CDR_document_by_CDR_Manager : TestBase
    {
       // [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187784)]
        [Property(Priority, "High")]
        [Description("To validate the QC disapprove and close part of an CDR (Construction Deficiency Report).")]
        public void QC_Review_of_CDR_document_by_CDR_Manager()
        {
            string cdrDescription = WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.CDRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralCDR.ReviewCDRDocument(UserType.CDRTech, cdrDescription);
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.To_Be_Closed, cdrDescription));
        }
    }

    [TestFixture]
    public class Verify_Revising_the_CDR_Document : TestBase
    {
       // [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187609)]
        [Property(Priority, "High")]
        [Description("To successfully revising an CDR (Construction Deficiency Report) document.")]
        public void Revise_the_CDR_Document()
        {
            string cdrDescription = WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.CDRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralCDR.ReviewAndReviseCDRDocument(UserType.CDRTech, cdrDescription);
        
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Revise, cdrDescription));
        }
    }


    [TestFixture]
    public class Verify_Disposition_Of_the_CDR_Document : TestBase
    {
       // [Test]
        [Category(Component.CDR)]
        [Property(Component2, Component.CDR_WF_Complex)]
        [Property(TestCaseNumber, 2187784)]
        [Property(Priority, "High")]
        [Description("To successfully Disposition an CDR (Construction Deficiency Report) document. GLX only")]
        public void Disposition_Of_the_CDR_Document()
        {
            string cdrDescription = WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.CDRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralCDR.ReviewCDRDocument(UserType.CDRTech, cdrDescription);
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Disposition, cdrDescription));
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralCDR.DispositionCDRDocument(UserType.CDRTech, cdrDescription);
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.To_Be_Closed, cdrDescription));
        }
    }

    [TestFixture]
    public class Verify_Closing_the_CDR_Document : TestBase
    {
        //[Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187784)]
        [Property(Priority, "High")]
        [Description("To successfully close an CDR (Construction Deficiency Report) document.")]
        public void Close_the_CDR_Document()
        {
            string cdrDescription = "DvsNBomRSWtexnmeoPKheNWtmJ";
            WF_QaRcrdCtrl_GeneralCDR.CloseDocument(UserType.CDRTech, cdrDescription);
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Closed_DN, cdrDescription));
        }
    }

    [TestFixture]
    public class Verify_KickBack_To_Disposition_the_CDR_Document : TestBase
    {
       // [Test]
        [Category(Component.CDR)]
        [Property(Component2, Component.CDR_WF_Complex)]
        [Property(TestCaseNumber, 2187784)]
        [Property(Priority, "High")]
        [Description("To successfully close an CDR (Construction Deficiency Report) document.")]
        public void KickBack_To_Disposition_the_CDR_Document()
        {
            // string cdrDescription = "UASpeuycVMIunVSWlLAYEEypBS";
            string cdrDescription = WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.CDRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralCDR.ReviewCDRDocument(UserType.CDRTech, cdrDescription);
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Disposition, cdrDescription));
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralCDR.DispositionCDRDocument(UserType.CDRTech, cdrDescription);
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.To_Be_Closed, cdrDescription));
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralCDR.KickBackToDispositionCDR(UserType.CDRTech, cdrDescription);
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Disposition, cdrDescription));
        }
    }

    [TestFixture]
    public class Verify_KickBack_To_QC_Review_the_CDR_Document : TestBase
    {
       // [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187784)]
        [Property(Priority, "High")]
        [Description("To successfully close an CDR (Construction Deficiency Report) document.")]
        public void KickBack_To_QC_Review_the_CDR_Document()
        {
            // string cdrDescription = "UASpeuycVMIunVSWlLAYEEypBS";
            string cdrDescription = WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.CDRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralCDR.ReviewCDRDocument(UserType.CDRTech, cdrDescription);
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Disposition, cdrDescription));
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralCDR.KickBackToQCReviewCDR(UserType.CDRTech, cdrDescription);
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.QC_Review, cdrDescription));
        }
    }

    [TestFixture]
    public class Verify_Editing_the_CDR_Document : TestBase
    {
       // [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187610)]
        [Property(Priority, "High")]
        [Description("To successfully edit an CDR (Construction Deficiency Report) document.")]
        public void Edit_the_CDR_Document()
        {
            LoginAs(UserType.CDRMgr);
            NavigateToPage.QARecordControl_General_CDR();
        }
    }

    [TestFixture]
    public class Verify_Vewing_CDR_Document_Report : TestBase
    {
       // [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187609)]
        [Property(Priority, "High")]
        [Description("To successfully view the report of an CDR (Construction Deficiency Report) document.")]
        public void View_CDR_Document_Report()
        {
            LoginAs(UserType.CDRMgr);
            NavigateToPage.QARecordControl_General_CDR();
        }
    }

}
