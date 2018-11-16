using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using System;
using System.Collections;
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
        [Property(Component2, Component.DIR_WF_Simple_QA)]
        [Property(TestCaseNumber, 2187591)]
        [Property(Priority, "High")]
        [Description("To validate create and save a DIR (Daily Inspection Report) document.")]
        public void DIR_SimpleWF_End_To_End_UserGroup_QA()
        {
            WF_QaRcrdCtrl_QaDIR.LoginAndNavigateToDirPage(UserType.DIRTechQA);
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR();
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginAndNavigateToDirPage(UserType.DIRMgrQA);
            WF_QaRcrdCtrl_QaDIR.KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab.QC_Review, dirNumber);
            WF_QaRcrdCtrl_QaDIR.Modify_Cancel_Verify_inCreateRevise(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Modify_Save_Verify_and_SaveForward_inCreateRevise(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab.Authorization, dirNumber);
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);         
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inAuthorization(dirNumber);
        }

        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Simple_QC)]
        [Property(TestCaseNumber, 2187591)]
        [Property(Priority, "High")]
        [Description("To validate create and save a DIR (Daily Inspection Report) document.")]
        public void DIR_SimpleWF_End_To_End_UserGroup_QC()
        {
            WF_QaRcrdCtrl_QaDIR.LoginAndNavigateToDirPage(UserType.DIRTechQC);
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR();
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.LoginAndNavigateToDirPage(UserType.DIRMgrQC);
            WF_QaRcrdCtrl_QaDIR.KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab.QC_Review, dirNumber);
            WF_QaRcrdCtrl_QaDIR.Modify_Cancel_Verify_inCreateRevise(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Modify_Save_Verify_and_SaveForward_inCreateRevise(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab.Authorization, dirNumber);
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inAuthorization(dirNumber);
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