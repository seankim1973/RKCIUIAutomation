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
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR(UserType.DIRTechQA);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.Review_and_Return_DIR_ForRevise(UserType.DIRMgrQA, dirNumber);
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber));
            ClickEditBtnForRow();
            WF_QaRcrdCtrl_QaDIR.Modify_Cancel_Verify_inCreateReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Modify_Save_Verify_and_SaveForward_inCreateReview(dirNumber);
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review));
        }

        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Simple_QC)]
        [Property(TestCaseNumber, 2187591)]
        [Property(Priority, "High")]
        [Description("To validate create and save a DIR (Daily Inspection Report) document.")]
        public void DIR_SimpleWF_End_To_End_UserGroup_QC()
        {
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR(UserType.DIRTechQC);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.Review_and_Return_DIR_ForRevise(UserType.DIRMgrQC, dirNumber);
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber));
            ClickEditBtnForRow();
            WF_QaRcrdCtrl_QaDIR.Modify_Cancel_Verify_inCreateReview(dirNumber);
            WF_QaRcrdCtrl_QaDIR.Modify_Save_Verify_and_SaveForward_inCreateReview(dirNumber);
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review));
        }
    }

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
}