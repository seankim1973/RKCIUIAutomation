using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.QASearch;
using System;
using System.Threading;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QADIRs;

namespace RKCIUIAutomation.Test.DIR
{
    [TestFixture]
    public class Verify_DIR_SimpleWF_End_To_End : TestBase
    {
        //For Tenants: GLX, I15SB, I15Tech, LAX
        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Simple_QA)]
        [Property(TestCaseNumber, 2187591)]
        [Property(Priority, "High")]
        [Description("To validate creating and saving a DIR (Daily Inspection Report) document in Simple Workflow.")]
        public void DIR_SimpleWF_End_To_End_UserGroup_QA()
        {
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRTechQA);
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR();
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA, true);
            WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromQcReview_then_Edit_SaveForward(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Modify_Cancel_Verify_inCreateRevise(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Modify_Save_Verify_and_SaveForward_inCreateRevise(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inAuthorization(dirNumber);
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyWorkflowLocationAfterSimpleWF(dirNumber), "VerifyDirIsClosedByTblFilter");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDbCleanupForDIR(dirNumber), $"VerifyDbCleanupForDIR : {dirNumber}");
            AssertAll();
        }
    }

    [TestFixture]
    public class Verify_DIR_SimpleWF_End_To_End_UserGroup_QC : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        //[Property(Component2, Component.DIR_WF_Simple_QC)]
        [Property(TestCaseNumber, 2187591)]
        [Property(Priority, "High")]
        [Description("To validate creating and saving a DIR (Daily Inspection Report) document in Simple Workflow.")]
        public void DIR_SimpleWF_End_To_End_UserGroup_QC()
        {
            WF_QaRcrdCtrl_QaDIR.LoginToRcrdCtrlDirPage(UserType.DIRTechQC);
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR();
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginToRcrdCtrlDirPage(UserType.DIRMgrQC);
            WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromQcReview_then_Edit_SaveForward(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Modify_Cancel_Verify_inCreateRevise(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Modify_Save_Verify_and_SaveForward_inCreateRevise(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inAuthorization(dirNumber);
            AddAssertionToList(QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber), "VerifyDirIsClosedByTblFilter");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDbCleanupForDIR(dirNumber), $"VerifyDbCleanupForDIR : {dirNumber}");
            AssertAll();
        }
    }

    [TestFixture]
    public class Verify_QC_Review_by_Project_Manager : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        [Property(TestCaseNumber, 2187592)]
        [Property(Priority, "High")]
        [Description("To validate QC Review by Project Manager.")]
        public void QC_Review_by_Project_Manager()
        {
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRTechQA);
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR();
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA, true);
            WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromQcReview_then_Edit_SaveForward(dirNumber);
            WF_QaRcrdCtrl_QaDIR.LogoutLoginAsQaTech_Edit_Result_SaveForward_then_LogoutLoginAsQaMgr(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Enter_EngineerComments_and_Approve_inQcReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inAuthorization(dirNumber);
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyWorkflowLocationAfterSimpleWF(dirNumber), "VerifyDirIsClosedByTblFilter");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDbCleanupForDIR(dirNumber), $"VerifyDbCleanupForDIR : {dirNumber}");
            AssertAll();
        }
    }

    [TestFixture]
    public class Verify_Delete_a_DIR : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        [Property(TestCaseNumber, 2491626)]
        [Property(Priority, "High")]
        [Description("To validate Deletion of a DIR.")]
        public void Delete_a_DIR()
        {
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRTechQA);
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR();
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA, true);
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete(TableTab.QC_Review, dirNumber, false), "Verify DIR is Displayed after dismissing delete dialog");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete(TableTab.QC_Review, dirNumber, true), "Verify DIR is not Displayed after accepting delete dialog");
            AssertAll();
        }
    }

    //SG & SH249 - DIR ComplexWF Tenants
    [TestFixture]
    public class Verify_Filter_of_DIR_Table_ComplexWF : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        [Property(TestCaseNumber, 2584485)]
        [Property(Component2, Component.DIR_WF_Complex)]
        [Property(Priority, "High")]
        [Description("To validate Filtering of a DIR table for ComplexWF tenants.")]
        public void Filter_of_DIR_Table_ComplexWF()
        {            
            string dirRev = "A";
            var currentUser = UserType.DIRTechQA;
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(currentUser);
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveOnly_DIR();
            WF_QaRcrdCtrl_QaDIR.Edit_DIR_inCreate_and_Verify_AutoSaveTimerRefresh_then_Save(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_Create(currentUser, dirNumber, dirRev, false, true);
            LogoutToLoginPage();
            currentUser = UserType.DIRMgrQA;
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(currentUser, true);
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_ForTabs_In_QaFieldMenu(currentUser, dirNumber, dirRev);

            //2nd half of ComplexWF
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_Attachments(currentUser, dirNumber, dirRev, true);
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_Revise(currentUser, dirNumber, dirRev);
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_QcReview(currentUser, dirNumber, dirRev);
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_ToBeClosed(currentUser, dirNumber, dirRev, true);
            
            AddAssertionToList(QaSearch_DIR.VerifyDirWorkflowLocationBySearch(dirNumber, WorkflowLocation.Closed), "QaSearch_DIR.VerifyDirWorkflowLocationBySearch");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDbCleanupForDIR(dirNumber), $"VerifyDbCleanupForDIR : {dirNumber}");
            AssertAll();
        }
    }

    [TestFixture]
    public class Verify_Filter_of_DIR_Table_SimpleWF : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Simple_QA)]
        [Property(TestCaseNumber, 2584474)]
        [Property(Priority, "High")]
        [Description("To validate Filtering of a DIR table for SimpleWF tenants.")]
        public void Filter_of_DIR_Table_SimpleWF()
        {
            string dirRev = "A";
            var currentUser = UserType.DIRTechQA;
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(currentUser);
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveOnly_DIR();
            WF_QaRcrdCtrl_QaDIR.Edit_DIR_inCreate_and_Verify_AutoSaveTimerRefresh_then_Save(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_Create(currentUser, dirNumber, dirRev);
            LogoutToLoginPage();
            currentUser = UserType.DIRMgrQA;
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(currentUser);
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_QcReview(currentUser, dirNumber, dirRev);
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_Authorization(currentUser, dirNumber, dirRev);
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_Revise(currentUser, dirNumber, dirRev, true, false);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete_or_ApproveNoError_inQcReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete_or_ApproveNoError_inAuthorization(dirNumber, false, true);
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyWorkflowLocationAfterSimpleWF(dirNumber), "VerifyWorkflowLocationAfterSimpleWF");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDbCleanupForDIR(dirNumber), $"VerifyDbCleanupForDIR : {dirNumber}");
            AssertAll();
        }
    }

    [TestFixture]
    public class Verify_DIR_ComplexWF_End_To_End : TestBase
    {
        //Garnet, SG, SH249
        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Complex)]
        [Property(TestCaseNumber, 2488411)]
        [Property(Priority, "High")]
        [Description("To validate creating and saving a DIR (Daily Inspection Report) document in Complex Workflow.")]
        public void DIR_ComplexWF_End_To_End()
        {
            //SimpleWF portion of ComplexWF (QaField menu)
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRTechQA);
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR();
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA, true);
            WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromQcReview_then_Edit_SaveForward(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Modify_Cancel_Verify_inCreateRevise(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Modify_Save_Verify_and_SaveForward_inCreateRevise(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inAuthorization(dirNumber);
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyWorkflowLocationAfterSimpleWF(dirNumber), "VerifyWorkflowLocationAfterSimpleWF");

            //ComplexWF
            NavigateToPage.QARecordControl_QA_DIRs();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Attachments, dirNumber), "VerifyDirIsDisplayed in Attachments Tab");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_Back_To_Field();
            NavigateToPage.QAField_QA_DIRs();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed in >>QA Field>>QA DIRs in Revise Tab after (clicked 'Back To Field')");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inAuthorization(dirNumber);

            //Modify_Upload_and_VerifyCancel_VerifySave_inAttachments
            NavigateToPage.QARecordControl_QA_DIRs();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Attachments, dirNumber), "VerifyDirIsDisplayed in Attachments Tab");
            ClickEditBtnForRow();
            string fileName = UploadFile();
            AddAssertionToList(VerifyUploadedFileNames(fileName, true), "VerifyUploadedFileNames Before 'Edit DIR, Cancel' - (Expected) File 'test.xlsx' should be seen: ");
            QaRcrdCtrl_QaDIR.ClickBtn_Cancel();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Attachments, dirNumber), "VerifyDirIsDisplayed in Attachments Tab");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyUploadedFileNames(""), "VerifyUploadedFileNames After 'Edit DIR, Cancel' - (Expected) No files are seen: ");
            UploadFile();
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Attachments, dirNumber), "VerifyDirIsDisplayed in Attachments Tab");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyUploadedFileNames(fileName), "VerifyUploadedFileNames After 'Edit DIR, Save' - (Expected) File 'test.xlsx' should be seen: ");

            WF_QaRcrdCtrl_QaDIR.ClickBtn_KickBackOrRevise();//1
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Revise, dirNumber), "VerifyDirIsDisplayed in (QaRecordControl) Revise Tab after (clicked 'Revise' from Attachments)");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyUploadedFileNames(fileName), "VerifyUploadedFileNames After 'Return DIR for Revise' - (Expected) File 'test.xlsx' should be seen: ");

            QaRcrdCtrl_QaDIR.ClickBtn_Send_To_Attachment();//2
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Attachments, dirNumber), "VerifyDirIsDisplayed in Attachments Tab after (clicked 'Send To Attachments' from Revise)");
            ClickEditBtnForRow();

            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();//3
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review Tab after (clicked 'Save Forward' from Attachments)");
            ClickEditBtnForRow();

            WF_QaRcrdCtrl_QaDIR.ClickBtn_KickBackOrRevise();//4
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Revise, dirNumber), "VerifyDirIsDisplayed in (QaRecordControl) Revise Tab after (clicked 'Revise' from QC Review)");
            ClickEditBtnForRow();

            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();//5
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review Tab after (clicked 'Save Forward' from Revise)");
            ClickEditBtnForRow();

            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();//6
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.To_Be_Closed, dirNumber), "VerifyDirIsDisplayed in To Be Closed Tab after (clicked 'No Error' from QC Review)");
            ClickEditBtnForRow(dirNumber, true, true);

            WF_QaRcrdCtrl_QaDIR.ClickBtn_KickBackOrRevise();//7
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Revise, dirNumber), "VerifyDirIsDisplayed in (QaRecordControl) Revise Tab after (clicked 'Revise' from To Be Closed)");
            ClickEditBtnForRow();

            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();//8
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review Tab after (clicked 'Save Forward' from Revise - 2ndRound)");
            ClickEditBtnForRow();

            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();//9
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.To_Be_Closed, dirNumber), "VerifyDirIsDisplayed in To Be Closed Tab after (clicked 'No Error' from QC Review - 2ndRound)");
            ToggleCheckBoxForRow(dirNumber);

            QaRcrdCtrl_QaDIR.ClickBtn_Close_Selected();
            AddAssertionToList(QaSearch_DIR.VerifyDirWorkflowLocationBySearch(dirNumber, WorkflowLocation.Closed), "QaSearch_DIR.VerifyDirWorkflowLocationBySearch");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDbCleanupForDIR(dirNumber), $"VerifyDbCleanupForDIR : {dirNumber}");

            AssertAll();
        }
    }

    //GLX, I15SB, I15Tech, LAX
    [TestFixture]
    public class Verify_Creating_A_Revision_Of_A_Closed_DIR : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Simple_QA)]
        [Property(TestCaseNumber, 2187594)]
        [Property(Priority, "High")]
        [Description("To validate creating a revision of a closed DIR (Daily Inspection Report) document in Simple Workflow.")]
        public void Create_A_Revision_Of_A_Closed_DIR()
        {
            string expectedRevision = "B";

            //Create and Close DIR in Simple WF
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRTechQA);
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR();
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inAuthorization(dirNumber);
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyWorkflowLocationAfterSimpleWF(dirNumber), "VerifyWorkflowLocationAfterSimpleWF");
            LogoutToLoginPage();

            //Create Revision of Closed DIR
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRTechQA);
            QaRcrdCtrl_QaDIR.ClickBtn_CreateNew();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirNumberExistsInDbError(), "VerifyDirNumberExistsInDbError");
            QaRcrdCtrl_QaDIR.ClickBtn_CreateRevision();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirRevisionInDetailsPage(expectedRevision), $"VerifyDirRevisionInDetailsPage - Expected Revision {expectedRevision}");
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_DirRevision_inTblRow_then_SaveForward_inCreateRevise(dirNumber, expectedRevision), $"Verify_DirRevision_inTblRow_then_SaveForward_inCreateRevise - Expected Revision {expectedRevision}");
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA);
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_DirRevision_inTblRow_then_Approve_inQcReview(dirNumber, expectedRevision), $"Verify_DirRevision_inTblRow_then_Approve_inQcReview - Expected Revision {expectedRevision}");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_DirRevision_inTblRow_then_Approve_inAuthorization(dirNumber, expectedRevision), $"Verify_DirRevision_inTblRow_then_Approve_inAuthorization - Expected Revision {expectedRevision}");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyWorkflowLocationAfterSimpleWF_forDirRevision(dirNumber, expectedRevision), $"VerifyWorkflowLocationAfterSimpleWF_forDirRevision - Expected Revision {expectedRevision}");

            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDbCleanupForDIR(dirNumber), $"VerifyDbCleanupForDIR : {dirNumber}");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDbCleanupForDIR(dirNumber, expectedRevision), $"VerifyDbCleanupForDIR : {dirNumber} Rev. {expectedRevision}");
            AssertAll();
        }
    }

    //GLX, LAX, I15SB, I15Tech - DIR SimpleWF Tenants
    [TestFixture]
    public class Verify_IDLReport_for_QaDIR_With_Deficiencies_SimpleWF : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Simple_QA)]
        [Property(TestCaseNumber, 2187596)]
        [Property(Priority, "High")]
        [Description("To validate lookup of a DIR with Deficiency in an Inspection Deficiency Log Report in Simple Workflow.")]
        public void Inspection_Deficiency_Log_Report_for_QaDIR_With_Deficiencies()
        {
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRTechQA);
            string[] dirNumbers = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR_with_Failed_Inspection_and_PreviousFailingReports();
            string dirNumber = dirNumbers[0];
            string failedDirNumber = dirNumbers[1];
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inAuthorization(dirNumber);

            NavigateToPage.QASearch_Inspection_Deficiency_Log_Report();
            AddAssertionToList(QaSearch_InspctDefncyLogRprt.VerifyDirIsDisplayed(InspectionDeficiencyLogReport.ColumnName.DIR_No, dirNumber), $"InspctDefncyLogRprt.VerifyDirIsDisplayed DIR No: {dirNumber}");
            ClearTableFilters();
            AddAssertionToList(QaSearch_InspctDefncyLogRprt.VerifyDirIsDisplayed(InspectionDeficiencyLogReport.ColumnName.Closed_Dir, failedDirNumber), $"InspctDefncyLogRprt.VerifyDirIsDisplayed Previously Failed DIR No: {failedDirNumber}");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDbCleanupForDIR(dirNumber), $"VerifyDbCleanupForDIR : {dirNumber}");
            AssertAll();
        }
    }

    //SG & SH249 - DIR ComplexWF Tenants
    [TestFixture]
    public class Verify_IDLReport_for_QaDIR_With_Deficiencies_ComplexWF : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Complex)]
        [Property(TestCaseNumber, 2571204)]
        [Property(Priority, "High")]
        [Description("To validate lookup of a DIR with Deficiency in an Inspection Deficiency Log Report in Complex Workflow.")]
        public void Inspection_Deficiency_Log_Report_for_QaDIR_With_Deficiencies()
        {
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRTechQA, true);
            string[] dirNumbers = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR_with_Failed_Inspection_and_PreviousFailingReports();
            string dirNumber = dirNumbers[0];
            string previousFailedDirNumber = dirNumbers[1];
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA, true);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inAuthorization(dirNumber);

            NavigateToPage.QARecordControl_QA_DIRs();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Attachments, dirNumber), "VerifyDirIsDisplayed in Attachments Tab");
            ClickEditBtnForRow();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyPreviousFailingDirEntry(previousFailedDirNumber), $"VerifyPreviousFailingDirEntry in Attachments: {previousFailedDirNumber}");
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review Tab after (clicked 'Save Forward' from Attachments)");
            ClickEditBtnForRow();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyPreviousFailingDirEntry(previousFailedDirNumber), $"VerifyPreviousFailingDirEntry in QaRcrdCtrl_QcReview: {previousFailedDirNumber}");
            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.To_Be_Closed, dirNumber), "VerifyDirIsDisplayed in To Be Closed Tab after (clicked 'No Error' from QC Review)");
            ClickEditBtnForRow(dirNumber, true, true);
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyPreviousFailingDirEntry(previousFailedDirNumber), $"VerifyPreviousFailingDirEntry in ToBeClosed: {previousFailedDirNumber}");
            QaRcrdCtrl_QaDIR.ClickBtn_Cancel();

            NavigateToPage.QASearch_Inspection_Deficiency_Log_Report();
            AddAssertionToList(QaSearch_InspctDefncyLogRprt.VerifyDirIsDisplayed(InspectionDeficiencyLogReport.ColumnName.DIR_No, dirNumber), $"InspctDefncyLogRprt.VerifyDirIsDisplayed DIR No: {dirNumber}");
            ClearTableFilters();
            AddAssertionToList(QaSearch_InspctDefncyLogRprt.VerifyDirIsDisplayed(InspectionDeficiencyLogReport.ColumnName.Closed_Dir, previousFailedDirNumber), $"InspctDefncyLogRprt.VerifyDirIsDisplayed Previously Failed DIR No: {previousFailedDirNumber}");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDbCleanupForDIR(dirNumber), $"VerifyDbCleanupForDIR : {dirNumber}");
            AssertAll();
        }
    }

    //SG & SH249 - DIR Complex Tenants
    [TestFixture]
    public class Verify_Packages_Table_Columns_Filter : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Complex)]
        [Property(TestCaseNumber, 2556979)]
        [Property(Priority, "High")]
        [Description("To validate filtering of DIR table columns under Packages Tab.")]
        public void Packages_Table_Columns_Filter()
        {
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA);

            QaRcrdCtrl_QaDIR.ClickTab_Create_Packages();
            QaRcrdCtrl_QaDIR.FilterTable_CreatePackagesTab();                        
            QaRcrdCtrl_QaDIR.ClickTab_Packages();
            QaRcrdCtrl_QaDIR.FilterTable_PackagesTab();
            AssertAll();
        }
    }

    //SG & SH249 - DIR Complex Tenants
    [TestFixture]
    public class Verify_DIR_Packages_Download : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Complex)]
        [Property(TestCaseNumber, 2556982)]
        [Property(Priority, "High")]
        [Description("To validate creation of DIR packages in Complex Workflow.")]
        public void DIR_Packages_Download()
        {
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA);
            QaRcrdCtrl_QaDIR.ClickTab_Packages();
            AddAssertionToList(QaRcrdCtrl_QaDIR.Verify_Package_Download(), "Verify DIR Package Downloaded");
            AssertAll();
        }
    }

    //SG & SH249 - DIR Complex Tenants
    [TestFixture]
    public class Verify_Create_Packages : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Complex)]
        [Property(TestCaseNumber, 2518643)]
        [Property(Priority, "High")]
        [Description("To validate creation of DIR packages in Complex Workflow.")]
        public void Create_Packages()
        {
            int rowIndex = 1;
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA);
            QaRcrdCtrl_QaDIR.ClickTab_Create_Packages();
            string weekStart = QaRcrdCtrl_QaDIR.GetDirPackagesDataForRow<string>(PackagesColumnName.Week_Start, rowIndex);
            string[] newDIRs = QaRcrdCtrl_QaDIR.GetDirPackagesDataForRow<string[]>(PackagesColumnName.New_DIRs, rowIndex);

            bool pkgIsCreated = QaRcrdCtrl_QaDIR.Verify_Package_Created(rowIndex, weekStart, newDIRs);
            AddAssertionToList(pkgIsCreated, "Verify DIR Package Created Successfully");

            if (pkgIsCreated)
            {
                WF_QaRcrdCtrl_QaDIR.VerifyDbCleanupForCreatePackages(weekStart, newDIRs);
            }

            AssertAll();
        }
    }

    //SG & SH249 - DIR Complex Tenants
    [TestFixture]
    public class Verify_DIR_Packages_Recreate : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Complex)]
        [Property(TestCaseNumber, 2690311)]
        [Property(Priority, "High")]
        [Description("To validate re-creation of DIR packages in Complex Workflow.")]
        public void DIR_Packages_Recreate()
        {
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA);
            QaRcrdCtrl_QaDIR.ClickTab_Packages();

            AssertAll();
        }
    }

    [TestFixture]
    public class Verify_PDF_Report_View : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        [Property(TestCaseNumber, 2478991)]
        [Property(Priority, "High")]
        [Description("To validate function of Report View feature for DIR.")]
        public void PDF_Report_View()
        {
            //Simple WF
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRTechQA);
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveOnly_DIR();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_ViewReport_forDIR_inCreate(dirNumber), "Verify View Report for DIR in Create tab");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();            
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA);
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_ViewReport_forDIR_inQcReview(dirNumber), "Verify View Report for DIR in QC Review tab");
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_ViewReport_forDIR_inAuthorization(dirNumber), "Verify View Report for DIR in QC Authorization tab");

            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inAuthorization(dirNumber);
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyWorkflowLocationAfterSimpleWF(dirNumber), "VerifyWorkflowLocationAfterSimpleWF");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDbCleanupForDIR(dirNumber), $"VerifyDbCleanupForDIR : {dirNumber}");
            AssertAll();
        }
    }

    [TestFixture]
    public class Verify_PDF_Report_MultiView : TestBase
    {
        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Complex)]
        [Property(TestCaseNumber, 2491562)]
        [Property(Priority, "High")]
        [Description("To validate function of Report MultiView feature for DIR.")]
        public void PDF_Report_MultiView()
        {
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA);
            QaRcrdCtrl_QaDIR.ClickTab_To_Be_Closed();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_ViewMultiDirPDF(true), "Verify_ViewMultiDirPDF - No DIR Rows Selected");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_ViewMultiDirPDF(), "Verify_ViewMultiDirPDF - Top 3 DIR Rows Selected");
            AssertAll();
        }
    }
}