﻿using NUnit.Framework;
using RKCIUIAutomation.Config;
using System;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.GeneralNCR;

namespace RKCIUIAutomation.Test.NCR
{
    [TestFixture]//complete, updated hiptest
    public class Verify_Create_And_Save_Ncr_Document : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2187687)]
        [Property(Priority, "High")]
        [Description("To validate successful create and save of an NCR (Nonconformance Report) document.")]
        public void Create_And_Save_Ncr_Document()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR(UserType.NCRTech);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.CQM_Review, ncrDescription));
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.CQM_Review, ncrDescription));
            ClickLogoutLink();
            AssertAll();
        }
    }

    [TestFixture]//complete, updated hiptest
    public class Verify_Review_and_Approval_of_NCR_Document_by_NCRManager : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(Component2, Component.NCR_WF_Complex)]
        [Property(TestCaseNumber, 2187688)]
        [Property(Priority, "High")]
        [Description("To validate the QC review part of an Ncr (Nonconformance Report).")]
        public void Review_and_Approval_of_Ncr_Document_by_NCRManager()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR(UserType.NCRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralNCR.Review_and_Approve_NCR(UserType.NCRMgr, ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            ClickLogoutLink();
            AssertAll();
        }
    }

    [TestFixture]//complete, updated hiptest
    public class Verify_Disapprove_and_Close_of_Ncr_Document_by_NCRManager : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(Component2, Component.NCR_WF_Complex)]
        [Property(TestCaseNumber, 2299482)]
        [Property(Priority, "High")]
        [Description("To validate the QC disapprove and close part of an NCR (Nonconformance Report).")]
        public void Disapprove_and_Close_of_Ncr_Document_by_NCRManager()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR(UserType.NCRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralNCR.CloseNCR_CQMReview_Disapprove(UserType.NCRMgr, ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription));
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription));
            ClickLogoutLink();
            AssertAll();
        }
    }

    [TestFixture]//complete, updated hiptest
    public class Verify_Close_Ncr_Document_ConcessionRequest_ReturnToConformance : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(Component2, Component.NCR_WF_Complex)]
        [Property(TestCaseNumber, 2313396)]
        [Property(Priority, "High")]
        [Description("To validate workflow for closing NCR (Nonconformance Report), using Concession Request: Return To Conformance")]
        public void Close_Ncr_Document_ConcessionRequest_ReturnToConformance()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR(UserType.NCRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralNCR.Review_and_Approve_NCR(UserType.NCRMgr, ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToVerificationClosure_ReturnToConformance(ncrDescription);
            //WF_QaRcrdCtrl_GeneralNCR.CloseNCR_ConcessionRequest_ReturnToConformance(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.CloseNCR_in_VerificationAndClosure(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription));
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription));
            ClickLogoutLink();
            AssertAll();
        }
    }

    [TestFixture]//complete, updated hiptest
    public class Verify_Close_Ncr_Document_ConcessionRequest_ConcessionDeviation : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(Component2, Component.NCR_WF_Complex)]
        [Property(TestCaseNumber, 2338393)]
        [Property(Priority, "High")]
        [Description("To validate workflow for closing NCR (Nonconformance Report), using Concession Request: Concession Deviation")]
        public void Close_Ncr_Document_ConcessionRequest_ConcessionDeviation()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR(UserType.NCRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralNCR.Review_and_Approve_NCR(UserType.NCRMgr, ncrDescription);
            //WF_QaRcrdCtrl_GeneralNCR.CloseNCR_ConcessionRequest_ConcessionDeviation(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApproval(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDOTApproval_ToVerificationClosure(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.CloseNCR_in_VerificationAndClosure(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription));
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription));
            ClickLogoutLink();
            AssertAll();
        }
    }

    [TestFixture]//complete, updated hiptest
    public class Verify_Revise_Ncr_Document_ConcessionRequest_ReturnToConformance : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(Component2, Component.NCR_WF_Complex)]
        [Property(TestCaseNumber, 2338474)]
        [Property(Priority, "High")]
        [Description("To verify setting a NCR (Nonconformance Report) to revise workflow state in the Concession Request: Return to Conformance workflow.")]
        public void Revise_Ncr_Document_ConcessionRequest_ReturnToConformance()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR(UserType.NCRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralNCR.Review_and_Return_NCR_ForRevise(UserType.NCRMgr, ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Creating_Revise, ncrDescription));
            ClickEditBtnForRow();
            //step for Edit NCR ?
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            WF_QaRcrdCtrl_GeneralNCR.Review_and_Approve_NCR(UserType.NCRMgr, ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToVerificationClosure_ReturnToConformance(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.CheckReviseKickback_FromVerificationClosure_ForReturnToConformance(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            AssertAll();
        }
    }

    [TestFixture]//complete, updated hiptest
    public class Verify_Revise_Ncr_Document_ConcessionRequest_ConcessionDiviation : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(Component2, Component.NCR_WF_Complex)]
        [Property(TestCaseNumber, 2339453)]
        [Property(Priority, "High")]
        [Description("To successfully revising an NCR (Nonconformance Report) document in Concession Request: Concession Diviation workflow.")]
        public void Revise_Ncr_Document_ConcessionRequest_ConcessionDiviation()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR(UserType.NCRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralNCR.Review_and_Approve_NCR(UserType.NCRMgr, ncrDescription);
            //WF_QaRcrdCtrl_GeneralNCR.Return_ToResolutionDisposition_FromDeveloperConcurrence(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApproval(ncrDescription, false);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);

            //WF_QaRcrdCtrl_GeneralNCR.Return_ToDeveloperConcurrence_FromDOTApproval(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApproval(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDOTApproval_ToVerificationClosure(ncrDescription, false);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApproval(ncrDescription);


            WF_QaRcrdCtrl_GeneralNCR.CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            AssertAll();
        }
    }

    [TestFixture]
    public class Verify_Closing_the_Ncr_Document : TestBase
    {
        //[Test]
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
        //[Test]
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
        //[Test]
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