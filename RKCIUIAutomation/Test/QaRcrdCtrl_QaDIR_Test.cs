using NUnit.Framework;
using RKCIUIAutomation.Config;
using System.Collections;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QADIRs;

namespace RKCIUIAutomation.Test.DIR
{
    [TestFixture(UserType.DIRTechQA, UserType.DIRMgrQA)]
    //[TestFixtureSource(typeof(UserGroups), "BothUserGroups")]
    [TestFixture(UserType.DIRTechQC, UserType.DIRMgrQC)]
    //For Tenants: GLX, I15SB, I15Tech, LAX
    public class Verify_DIR_SimpleWF_End_To_End : TestBase
    {
        private UserType technician;
        private UserType manager;

        public Verify_DIR_SimpleWF_End_To_End(UserType tech, UserType mgr)
        {
            technician = tech;
            manager = mgr;
        }

        [Test]
        [Category(Component.DIR)]
        [Property(Component2, Component.DIR_WF_Simple)]
        [Property(TestCaseNumber, 2187591)]
        [Property(Priority, "High")]
        [Description("To validate create and save a DIR (Daily Inspection Report) document.")]
        public void DIR_SimpleWF_End_To_End()
        {
            string dirNumber = WF_QaRcrdCtrl_QaDIR.Create_and_SaveForward_DIR(technician);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_QaDIR.Review_and_Return_DIR_ForRevise(manager, dirNumber);
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber));
            ClickEditBtnForRow();
            WF_QaRcrdCtrl_QaDIR.Modify_Cancel_Verify_inCreateReview(dirNumber);
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber));
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