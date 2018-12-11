using NUnit.Framework;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QADIRs;

namespace RKCIUIAutomation.Test.DIR
{
    //[TestFixtureSource(typeof(UserGroups), "BothUserGroups")]

    //[TestFixture(UserGroup.DirQA)]
    //[TestFixture(UserGroup.DirQC)]

    [TestFixture]
    public class Verify_DIR_SimpleWF_End_To_End : TestBase
    {
        //private UserType technician;
        //private UserType manager;

        //public Verify_DIR_SimpleWF_End_To_End(UserGroup userGroup)
        //{
        //    ConfigTestUsers configUsers = new ConfigTestUsers();
        //    configUsers.AssignUsersByGroup(userGroup);
        //    technician = configUsers.technicianUser;
        //    manager = configUsers.managerUser;
        //}

        //For Tenants: GLX, I15SB, I15Tech, LAX
        [Test]
        [Category(Component.DIR)]
        //[Property(Component2, Component.DIR_WF_Simple_QA)]
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
            //WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inAuthorization(dirNumber);
            //AddAssertionToList(QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber), "VerifyDirIsClosedByTblFilter");
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
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete(TableTab.QC_Review, dirNumber, true), "Verify DIR is Displayed after accepting delete dialog");
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
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyWorkflowLocationAfterSimpleWF(dirNumber), "VerifyDirIsClosedByTblFilter");

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
            //QaRcrdCtrl_QaDIR.ClickBtn_Close_Selected(); //<--uncomment to Close DIR

            //Update WorkflowLocation to .Closed when Closing DIR by uncommenting step above
            AddAssertionToList(QaSearch_DIR.VerifyDirWorkflowLocationBySearch(dirNumber, WorkflowLocation.Closing), "QaSearch_DIR.VerifyDirWorkflowLocationBySearch");

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
            //Create and Close DIR in Simple WF
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRTechQA);
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR();
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inAuthorization(dirNumber);
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyWorkflowLocationAfterSimpleWF(dirNumber), "VerifyDirIsClosedByTblFilter");
            LogoutToLoginPage();

            //Create Revision of Closed DIR
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRTechQA);
            QaRcrdCtrl_QaDIR.ClickBtn_CreateNew();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirNumberExistsInDbError(), "VerifyDirNumberExistsInDbError");
            QaRcrdCtrl_QaDIR.ClickBtn_CreateRevision();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirRevisionInDetailsPage("B"), "VerifyDirRevisionInDetailsPage");
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_DirRevision_inTblRow_then_SaveForward_inCreateRevise(dirNumber, "B"), "Verify_DirRevision_inTblRow_then_SaveForward_inCreateRevise - Expected Revision (B)");
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA);
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_DirRevision_inTblRow_then_Approve_inQcReview(dirNumber, "B"), "Verify_DirRevision_inTblRow_then_Approve_inQcReview - Expected Revision (B)");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.Verify_DirRevision_inTblRow_then_Approve_inAuthorization(dirNumber, "B"), "Verify_DirRevision_inTblRow_then_Approve_inAuthorization - Expected Revision (B)");
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyWorkflowLocationAfterSimpleWF_forDirRevision(dirNumber, "B"), "VerifyWorkflowLocationAfterSimpleWF_forDirRevision - Expected Revision (B)");
            AssertAll();
        }
    }

    /*
    public class UserGroups
    {
        public static IEnumerable BothUserGroups
        {
            get
            {
                yield return new TestFixtureData(UserType.DIRTechQA, UserType.DIRMgrQA);
                yield return new TestFixtureData(UserType.DIRTechQC, UserType.DIRMgrQC);
            }
        }

        public static IEnumerable QaUserGroup
        {
            get
            {
                yield return new TestFixtureData(UserType.DIRTechQA, UserType.DIRMgrQA);
            }
        }
    }
    */
}