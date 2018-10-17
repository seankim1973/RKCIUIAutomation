using NUnit.Framework;
using RKCIUIAutomation.Config;
using System;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.GeneralNCR;

namespace RKCIUIAutomation.Test.NCR
{
    [TestFixture]//complete
    public class Verify_Create_And_Save_Ncr_Document : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2187687)]
        [Property(Priority, "High")]
        [Description("To validate successful create and save of an NCR (Nonconformance Report) document.")]
        public void Create_And_Save_Ncr_Document()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR_Document(UserType.NCRTech);
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.CQM_Review, ncrDescription));
            LogoutToLoginPage();
        }
    }

    [TestFixture]//complete
    public class Verify_QC_Review_of_NCR_document_by_NCR_Manager : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2187688)]
        [Property(Priority, "High")]
        [Description("To validate the QC review part of an Ncr (Nonconformance Report).")]
        public void QC_Review_of_Ncr_document_by_NcrManager()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR_Document(UserType.NCRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralNCR.Review_and_Approve_NCR_Document(UserType.NCRMgr, ncrDescription);
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
        }
    }

    [TestFixture]//complete
    public class Verify_Disapprove_and_Close_of_Ncr_document_by_NCR_Manager : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2299482)]
        [Property(Priority, "High")]
        [Description("To validate the QC disapprove and close part of an NCR (Nonconformance Report).")]
        public void QC_Review_of_Ncr_document_by_NCR_Manager()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR_Document(UserType.NCRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralNCR.CloseNCR_CQMReview_Disapprove(UserType.NCRMgr, ncrDescription);
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription));
        }
    }

    [TestFixture]//complete - TODO: accepting confirmation for Approve button click shows error log in report
    public class Verify_Close_Ncr_document_ConcessionRequest_ReturnToConformance : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 12345)]
        [Property(Priority, "High")]
        [Description("To validate workflow for closing NCR (Nonconformance Report), using Concession Request: Return To Conformance")]
        public void Close_Ncr_document_ConcessionRequest_ReturnToConformance()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR_Document(UserType.NCRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralNCR.CloseNCR_ConcessionRequest_ReturnToConformance(UserType.NCRMgr, ncrDescription);
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription));
        }
    }

    [TestFixture]
    public class Verify_Close_Ncr_document_ConcessionRequest_ConcessionDeviation : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 12345)]
        [Property(Priority, "High")]
        [Description("To validate workflow for closing NCR (Nonconformance Report), using Concession Request: Concession Deviation")]
        public void Close_Ncr_document_ConcessionRequest_ConcessionDeviation()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR_Document(UserType.NCRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralNCR.CloseNCR_ConcessionRequest_ConcessionDeviation(UserType.NCRMgr, ncrDescription);
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription));
        }
    }

    [TestFixture]
    public class Verify_Revising_the_Ncr_Document_ConcessionRequest_ReturnToConformance : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2187689)]
        [Property(Priority, "High")]
        [Description("To successfully revising an NCR (Nonconformance Report) document in Concession Request: Return to Conformance workflow.")]
        public void Revise_Ncr_Document_ConcessionRequest_ReturnToConformance()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR_Document(UserType.NCRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralNCR.Return_For_Revise_From_CQMReview(UserType.NCRMgr, ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Creating_Revise, ncrDescription));
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Creating_Revise, ncrDescription));
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            WF_QaRcrdCtrl_GeneralNCR.Review_and_Approve_NCR_Document(UserType.NCRMgr, ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            WF_QaRcrdCtrl_GeneralNCR.Return_For_Revise_From_VerificationClosure(UserType.NCRMgr, ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            QaRcrdCtrl_GeneralNCR.SelectDDL_ConcessionRequest_ReturnToConformance();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription));
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription));
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_KickBack();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            AssertAll();
        }
    }

    [TestFixture]
    public class Verify_Revising_the_Ncr_Document_ConcessionRequest_ConcessionDiviation : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2187689)]
        [Property(Priority, "High")]
        [Description("To successfully revising an NCR (Nonconformance Report) document in Concession Request: Concession Diviation workflow.")]
        public void Revise_Ncr_Document_ConcessionRequest_ReturnToConformance()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR_Document(UserType.NCRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralNCR.Return_For_Revise_From_CQMReview(UserType.NCRMgr, ncrDescription);
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Creating_Revise, ncrDescription));
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            WF_QaRcrdCtrl_GeneralNCR.Review_and_Approve_NCR_Document(UserType.NCRMgr, ncrDescription);
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            WF_QaRcrdCtrl_GeneralNCR.Return_For_Revise_From_VerificationClosure(UserType.NCRMgr, ncrDescription);
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
        }
    }

    [TestFixture]
    public class Verify_Closing_the_Ncr_Document : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2187690)]
        [Property(Priority, "High")]
        [Description("To successfully close an NCR (Nonconformance Report) document.")]
        public void Close_the_Ncr_Document()
        {
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
        }
    }

    [TestFixture]
    public class Verify_Editing_the_Ncr_Document : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2187691)]
        [Property(Priority, "High")]
        [Description("To successfully edit an NCR (Nonconformance Report) document.")]
        public void Edit_the_Ncr_Document()
        {
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
        }
    }

    [TestFixture]
    public class Verify_Vewing_Ncr_Document_Report : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2188063)]
        [Property(Priority, "High")]
        [Description("To successfully view the report of an NCR (Nonconformance Report) document.")]
        public void View_Ncr_Document_Report()
        {
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
        }
    }
}