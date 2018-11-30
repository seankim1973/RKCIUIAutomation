using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QADIRs;

namespace RKCIUIAutomation.Test.DIR
{
    //[TestFixtureSource(typeof(UserGroups), "BothUserGroups")]

    //[TestFixture(UserGroup.DirQA)]
    //[TestFixture(UserGroup.DirQC)]

    //For Tenants: GLX, I15SB, I15Tech, LAX
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

        //Garnet, SG, SH249
        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Complex)]
        [Property(TestCaseNumber, 2488411)]
        [Property(Priority, "High")]
        [Description("To validate creating and saving a DIR (Daily Inspection Report) document in Complex Workflow.")]
        public void DIR_ComplexWF_End_to_End()
        {/*
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
            */

            string dirNumber = "3333181130";
            LoginAs(UserType.DIRMgrQA);
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
            AddAssertionToList(VerifyUploadedFileNames(fileName, true), "VerifyUploadedFileNames Before 'Edit DIR, Save' - (Expected) File 'test.xlsx' should be seen: ");
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Attachments, dirNumber), "VerifyDirIsDisplayed in Attachments Tab");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyUploadedFileNames(fileName), "VerifyUploadedFileNames After 'Edit DIR, Save' - (Expected) File 'test.xlsx' should be seen: ");

            //Return_DIR_ForRevise_FromAttachments_then_          
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
            ClickEditBtnForRow();

            WF_QaRcrdCtrl_QaDIR.ClickBtn_KickBackOrRevise();//7
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Revise, dirNumber), "VerifyDirIsDisplayed in (QaRecordControl) Revise Tab after (clicked 'Revise' from To Be Closed)");
            ClickEditBtnForRow();

            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();//8
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review Tab after (clicked 'Save Forward' from Revise - 2ndRound)");
            ClickEditBtnForRow();

            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();//9
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.To_Be_Closed, dirNumber), "VerifyDirIsDisplayed in To Be Closed Tab after (clicked 'No Error' from QC Review - 2ndRound)");
            ToggleCheckBoxForRow(dirNumber);

            //QaRcrdCtrl_QaDIR.ClickBtn_Close_Selected();

            AddAssertionToList(QaSearch_DIR.VerifyDirWorkflowLocationBySearch(dirNumber, WorkflowLocation.Closing));

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