using NUnit.Framework;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QADIRs;

namespace RKCIUIAutomation.Test.UnitTests
{
    [TestFixture]
    public class UnitTest_QaDIR_VerifyRevisionInDetailsPage : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("Verify Revision In Details Page")]
        public void QaDIR_RevisionInDetailsPage()
        {
            LogInfo("UnitTest_QaDIR_VerifyRevisionInDetailsPage");
            WF_QaRcrdCtrl_QaDIR.LoginToDirPage(UserType.DIRMgrQA);
            QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, "RKCHAD181017");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.VerifyDirRevisionInDetailsPage("B");
        }
    }
}
