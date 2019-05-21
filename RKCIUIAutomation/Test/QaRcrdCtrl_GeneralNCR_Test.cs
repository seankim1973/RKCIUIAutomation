using NUnit.Framework;
using RKCIUIAutomation.Config;
using System;
using System.Threading;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.GeneralNCR;

namespace RKCIUIAutomation.Test.NCR
{

    //[TestFixture] //grouped TCs - duplicates
    public class GeneralNCR_Verify_Complex_Workflow_Scenarios : TestBase
    {
        //[Test]
        [Category(Component.NCR)]
        [Property(Component2, Component.NCR_WF_Complex)]
        [Property(TestCaseNumber, 2187687)]
        [Property(Priority, "High")]
        [Description("To validate successful create and save of an NCR (Nonconformance Report) document.")]
        public void Create_And_SaveForward_Ncr_Document()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR(UserType.NCRTech);
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayedInReview");
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.CQM_Review, ncrDescription));
            AssertAll();
        }

        //[Test]
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
            AssertAll();
        }

        //[Test]
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
            AssertAll();
        }
    }

    //[TestFixture] //grouped TCs - duplicates
    public class GeneralNCR_Verify_ConcessionRequest_ReturnToConformance_Scenarios : TestBase
    {
        //[Test]
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
            AssertAll();
        }

        //[Test]
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

        //[Test]
        [Category(Component.NCR)]
        [Property(Component2, Component.NCR_WF_Complex)]
        [Property(TestCaseNumber, 2338474)]
        [Property(Priority, "High")]
        [Description("To verify Edit, Cancel, SaveOnly functions for NCR in the Concession Request: Return to Conformance workflow.")]
        public void Edit_Cancel_SaveOnly_Ncr_Document_ConcessionRequest_ReturnToConformance()
        {
            LogInfo("WIP");
            //string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR(UserType.NCRTech);
            //LogoutToLoginPage();
            //WF_QaRcrdCtrl_GeneralNCR.Review_and_Return_NCR_ForRevise(UserType.NCRMgr, ncrDescription);
            //AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Creating_Revise, ncrDescription));
            //ClickEditBtnForRow();
            ////step for Edit NCR ?
            //QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            //WF_QaRcrdCtrl_GeneralNCR.Review_and_Approve_NCR(UserType.NCRMgr, ncrDescription);
            //AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            //WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToVerificationClosure_ReturnToConformance(ncrDescription);
            //WF_QaRcrdCtrl_GeneralNCR.CheckReviseKickback_FromVerificationClosure_ForReturnToConformance(ncrDescription);
            //AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            //AssertAll();
        }
    }

    //[TestFixture] //grouped TCs - duplicates
    public class GeneralNCR_Verify_ConcessionRequest_ConcessionDeviation_Scenarios : TestBase
    {
        //[Test]
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
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.CloseNCR_in_VerificationAndClosure(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription));
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription));
            AssertAll();
        }

        //[Test]
        [Category(Component.NCR)]
        [Property(Component2, Component.NCR_WF_Complex)]
        [Property(TestCaseNumber, 2339453)]
        [Property(Priority, "High")]
        [Description("To successfully revising an NCR (Nonconformance Report) document in Concession Request: Concession Diviation workflow.")]
        public void Revise_Ncr_Document_ConcessionRequest_ConcessionDiviation()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR(UserType.NCRTech);
            
            //WF_QaRcrdCtrl_GeneralNCR.Return_ToResolutionDisposition_FromDeveloperConcurrence(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(ncrDescription, false);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription, true);

            //WF_QaRcrdCtrl_GeneralNCR.Return_ToDeveloperConcurrence_FromDOTApproval(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(ncrDescription, false);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(ncrDescription);

            WF_QaRcrdCtrl_GeneralNCR.CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            AssertAll();
        }

        //[Test]
        [Category(Component.NCR)]
        [Property(Component2, Component.NCR_WF_Complex)]
        [Property(TestCaseNumber, 2339453)]
        [Property(Priority, "High")]
        [Description("To verify Edit, Cancel, SaveOnly functions for NCR in Concession Request: Concession Diviation workflow.")]
        public void Edit_Cancel_SaveOnly_Ncr_Document_ConcessionRequest_ConcessionDiviation()
        {
            LogInfo("WIP");

            //string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR(UserType.NCRTech);
            //LogoutToLoginPage();
            //WF_QaRcrdCtrl_GeneralNCR.Review_and_Approve_NCR(UserType.NCRMgr, ncrDescription);
            ////WF_QaRcrdCtrl_GeneralNCR.Return_ToResolutionDisposition_FromDeveloperConcurrence(ncrDescription);
            //WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);
            //WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApproval(ncrDescription, false);
            //WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);

            ////WF_QaRcrdCtrl_GeneralNCR.Return_ToDeveloperConcurrence_FromDOTApproval(ncrDescription);
            //WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApproval(ncrDescription);
            //WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDOTApproval_ToVerificationClosure(ncrDescription, false);
            //WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApproval(ncrDescription);

            //WF_QaRcrdCtrl_GeneralNCR.CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(ncrDescription);
            //AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            ////Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            //AssertAll();
        }
    }


    [TestFixture]//complete, updated hiptest
    public class Verify_NCR_SimpleWF_End_To_End : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(Component2, Component.NCR_WF_Simple)]
        [Property(TestCaseNumber, 2187687)]
        [Property(Priority, "High")]
        [Description("To validate simple workflow for NCR module end-to-end.")]
        public void NCR_SimpleWF_End_To_End()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR(UserType.NCRTech);
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayedInReview");
            ClickEditBtnForRow();
            log.Debug("------------send to revise from Review------------");
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Revise, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Revise)");
            ClickEditBtnForRow();

            log.Debug("------------cancel, edit/saveonly in Revise------------");
            QaRcrdCtrl_GeneralNCR.EnterDescription("New NCR Description", true);
            QaRcrdCtrl_GeneralNCR.ClickBtn_Cancel();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Revise, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Revise)");
            ClickEditBtnForRow();
            ncrDescription = QaRcrdCtrl_GeneralNCR.EnterDescription();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Revise, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Revise)");
            ClickEditBtnForRow();

            log.Debug("------------save&fwd in Review------------");
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayedInReview");
            ClickEditBtnForRow();

            log.Debug("------------save&fwd in ToBeClosed------------");
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.To_Be_Closed, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.To_Be_Closed)");
            ClickEditBtnForRow();

            log.Debug("------------send to revise from ToBeClosed------------");
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Revise, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Revise)");
            ClickEditBtnForRow();

            log.Debug("------------save&fwd from Revise to Review------------");
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayedInReview");
            ClickEditBtnForRow();

            log.Debug("------------save&fwd in Review------------");
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.To_Be_Closed, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.To_Be_Closed)");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Close();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription), "VerifyNCRDocIsDisplayedInClosed");
            AssertAll();
        }
    }

    [TestFixture]//complete, updated hiptest
    public class Verify_Create_And_SaveForward_Ncr_Document : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(Component2, Component.NCR_WF_Complex)]
        [Property(TestCaseNumber, 2187687)]
        [Property(Priority, "High")]
        [Description("To validate successful create and SaveForward of an NCR (Nonconformance Report) document.")]
        public void Create_And_SaveForward_Ncr_Document()
        {
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveForward_NCR(UserType.NCRTech);
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayedInReview");
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
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(Resolution_Disposition)");
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
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription), "VerifyNCRDocIsClosed");
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
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription), "VerifyNCRDocIsClosed");
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription));
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

            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.CloseNCR_in_VerificationAndClosure(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription), "VerifyNCRDocIsClosed");
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
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Creating_Revise, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Creating_Revise)");
            ClickEditBtnForRow();
            //step for Edit NCR ?
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            WF_QaRcrdCtrl_GeneralNCR.Review_and_Approve_NCR(UserType.NCRMgr, ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToVerificationClosure_ReturnToConformance(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.CheckReviseKickback_FromVerificationClosure_ForReturnToConformance(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
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

            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(ncrDescription, false);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription, true);

            //WF_QaRcrdCtrl_GeneralNCR.Return_ToDeveloperConcurrence_FromDOTApproval(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(ncrDescription);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(ncrDescription, false);
            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(ncrDescription);

            WF_QaRcrdCtrl_GeneralNCR.SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(ncrDescription);

            WF_QaRcrdCtrl_GeneralNCR.CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            //Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            AssertAll();
        }
    }

    [TestFixture] //complete, updated hiptest
    public class Verify_Edit_Cancel_SaveOnly_Ncr_Document_ComplexWF : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(Component2, Component.NCR_WF_Complex)]
        [Property(TestCaseNumber, 2187691)]
        [Property(Priority, "High")]
        [Description("To verify Edit, Cancel, and SaveOnly functions for NCR Complex Workflow.")]
        public void Edit_Cancel_SaveOnly_Ncr_Document_ComplexWF()
        {
            LogInfo("------ Login as NCR Technician, create ncr and enter description, click saveOnly then logout -------");
            string ncrDescription = WF_QaRcrdCtrl_GeneralNCR.Create_and_SaveOnly_NCR(UserType.NCRTech);
            LogoutToLoginPage();

            LogInfo("------ Login as NCR Manager, edit NCR in revise tab, modify description then click cancel  -------");
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Creating_Revise, ncrDescription), "VerifyNCRDocIsDisplayed(Creating_Revise)");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.EnterDescription("Temp Description", true);
            QaRcrdCtrl_GeneralNCR.ClickBtn_Cancel();

            LogInfo("------  edit in revise tab then click saveFwd -------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Creating_Revise, ncrDescription), "VerifyNCRDocIsDisplayed(Creating_Revise)");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();

            LogInfo("------  edit in review tab and select Type of NCR (Level 3) then click cancel -------");
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayed(CQM_Review)");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SelectRdoBtn_TypeOfNCR_Level3();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Cancel();

            LogInfo("------  edit in review tab and select Type of NCR (Level 3) then click saveOnly -------");
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayed(CQM_Review)");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.TypeOfNCR_Level3/*, false*/), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.TypeOfNCR_Level3"); //<----- TODO: uncomment after bug is fixed: Retains selection when clicking Cancel btn after selecting Type Of NCR rdoBtn.
            QaRcrdCtrl_GeneralNCR.SelectRdoBtn_TypeOfNCR_Level3();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            LogInfo("------  edit in review tab and verify Type of NCR selection is intact then click Approve -------");
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayed(CQM_Review)");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.TypeOfNCR_Level3), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.TypeOfNCR_Level3)");
            QaRcrdCtrl_GeneralNCR.ClickBtn_Approve();

            LogInfo("------  edit in Resolution/Disposition tab and verify previously selected required field -------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.TypeOfNCR_Level3), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.TypeOfNCR_Level3)");

            LogInfo("------  selectDDL Concession Request - Concession Diviation and populate required fields then click Cancel  -------");
            QaRcrdCtrl_GeneralNCR.PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ConcessionDeviation();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Cancel();

            LogInfo("------  edit in Resolution/Disposition tab then verify Concession Request DDL is set to 'Please Select'  -------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyDDListSelectedValue(InputFields.Concession_Request, "Please Select"), "VerifyDDListSelectedValue(InputFields.Concession_Request, \"Please Select\")");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required, false), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required, false)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Accept_As_Is, false), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Accept_As_Is, false)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Repair, false), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Repair, false)");

            LogInfo("------  selectDDL Concession Request - Concession Diviation, populate required fields then click SaveOnly  -------");
            QaRcrdCtrl_GeneralNCR.PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ConcessionDeviation();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            LogInfo("------  edit in Resolution/Disposition tab and verify Concession Request DDL is set to 'Concession Diviation', checkboxes are selected then click SaveFwd  -------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyDDListSelectedValue(InputFields.Concession_Request, "Concession Deviation"), "VerifyDDListSelectedValue(InputFields.Concession_Request, \"Concession Deviation\")");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Accept_As_Is), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Accept_As_Is)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Repair), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Repair)");
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();

            LogInfo("------  edit in Developer Concurrence tab and verify preivously selected required fields -------");
            WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInDevConcurrence(ncrDescription);
            ClickEditBtnForRow();
            AddAssertionToList(VerifyDDListSelectedValue(InputFields.Concession_Request, "Concession Deviation"), "VerifyDDListSelectedValue(InputFields.Concession_Request, \"Concession Deviation\")");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Accept_As_Is), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Accept_As_Is)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Repair), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Repair)");

            LogInfo("------  provide signature for Engineer of Record then click Cancel  -------");
            WF_QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(TableTab.Developer_Concurrence);
            QaRcrdCtrl_GeneralNCR.ClickBtn_Cancel();

            LogInfo("------  edit in Developer Concurrence tab and verify signatature for Eng of Record is empty -------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");
            ClickEditBtnForRow();
            WF_QaRcrdCtrl_GeneralNCR.VerifySignatureNCR(TableTab.Developer_Concurrence, ncrDescription, true);
            

            LogInfo("------  provide signature, name and select Approval 'Yes' rdoBtn then click SaveOnly  -------");
            WF_QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(TableTab.Developer_Concurrence);
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            LogInfo("------  edit in Developer Concurrence tab and verify signature value attribute is not empty, name field is not empty, approval rdoBtn is selected then click saveFwd  -------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");
            ClickEditBtnForRow();
            WF_QaRcrdCtrl_GeneralNCR.VerifySignatureNCR(TableTab.Developer_Concurrence, ncrDescription, false);
            
            AddAssertionToList(VerifyInputField(InputFields.CQC_Manager), "VerifyInputField(InputFields.CQC_Manager)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.CQCMApproval_Yes), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.CQCMApproval_Yes)");
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();

            WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInLAWAConcurrence(ncrDescription);
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(Reviewer.Owner);
            QaRcrdCtrl_GeneralNCR.ClickBtn_Cancel();

            LogInfo("------  edit in DOT Approval tab and verify signatature for DOT Review is empty  -------");
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence(ncrDescription), "VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence");
            ClickEditBtnForRow();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.Owner, true), "VerifySignatureField(Reviewer.Owner, true)");
            AddAssertionToList(VerifyInputField(InputFields.Owner_Review, true), "VerifyInputField(InputFields.Owner_Review, true)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Owner_Approval_NA), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Owner_Approval_NA)");

            LogInfo("------  provide signature, name and select Approval 'Yes' rdoBtn then click SaveOnly  -------");
            QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(Reviewer.Owner);
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            LogInfo("------  edit in DOT Approval tab and verify signature value attribute is not empty, name field is not empty, approval rdoBtn is selected then click saveFwd  -------");
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence(ncrDescription), "VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence");
            ClickEditBtnForRow();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.Owner), "VerifySignatureField(Reviewer.Owner)");
            AddAssertionToList(VerifyInputField(InputFields.Owner_Review), "VerifyInputField(InputFields.Owner_Review)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Owner_Approval_Yes), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Owner_Approval_Yes)");
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();

            WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInVerificationAndClosure(ncrDescription);
            
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();

            WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInResolutionAndDisposition(ncrDescription);
            ClickEditBtnForRow();
            AddAssertionToList(VerifyDDListSelectedValue(InputFields.Concession_Request, "Concession Deviation"), "VerifyDDListSelectedValue(InputFields.Concession_Request)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Correct_Rework, false), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Correct_Rework, false)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Replace, false), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Replace, false)");

            LogInfo("------  selectDDL Concession Request - Return To Conformance and select checkboxes then click SaveOnly  -------");
            QaRcrdCtrl_GeneralNCR.PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ReturnToConformance();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            LogInfo("------  edit in Resolution/Disposition tab and verify Concession Request DDL is set to 'Return to Conformance' and checkboxes are selected then click SaveFwd  -------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyDDListSelectedValue(InputFields.Concession_Request, "Return to Conformance"), "VerifyDDListSelectedValue(InputFields.Concession_Request, \"Return to Conformance\")");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Correct_Rework), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Correct_Rework)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Replace), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Replace)");
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();

            WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInVerificationAndClosure(ncrDescription);
            ClickEditBtnForRow();
            AddAssertionToList(VerifyDDListSelectedValue(InputFields.Concession_Request, "Return to Conformance"), "VerifyDDListSelectedValue(InputFields.Concession_Request, \"Return to Conformance\")");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Correct_Rework), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Correct_Rework)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Replace), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.ChkBox_Replace)");

            LogInfo("------  provide signature for IQF Mgr then click cancel  -------");
            QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(Reviewer.IQF_Manager);
            QaRcrdCtrl_GeneralNCR.ClickBtn_Cancel();

            LogInfo("------  edit in Verification and Closure and verify signature is empty for IQF Mgr -------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription)");
            ClickEditBtnForRow();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.IQF_Manager, true), "VerifySignatureField(Reviewer.IQF_Manager, true)");

            WF_QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(TableTab.Verification_and_Closure);
            

            LogInfo("------  edit in Verification and Closure and verify signatures' element value and name field is not empty then click Close  -------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            ClickEditBtnForRow();
            WF_QaRcrdCtrl_GeneralNCR.VerifySignatureNCR(TableTab.Verification_and_Closure, ncrDescription);
            QaRcrdCtrl_GeneralNCR.ClickBtn_Close();

            LogInfo("------  verify ncr is closed  -------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsClosed(ncrDescription), $"VerifyNCRDocIsClosed({ncrDescription})");

            AssertAll();
        }
    }



    [TestFixture]
    public class Verify_Locked_Ncr_Document_Cannot_Be_Edited_By_Another_User : TestBase
    {
        //[Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2352692)]
        [Property(Priority, "High")]
        [Description("The objective of this test case is to verify that a Locked NCR cannot be edited by another user.")]
        public void Locked_Ncr_Document_Cannot_Be_Edited_By_Another_User()
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