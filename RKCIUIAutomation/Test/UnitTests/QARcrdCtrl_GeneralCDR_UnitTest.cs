using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System.Collections.Generic;
using System.Threading;

namespace RKCIUIAutomation.Test.UnitTests
{
    [TestFixture]
    class QARcrdCtrl_GeneralCDR_UnitTest_Garnet :TestBase
    {
        [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 654321)]
        [Property(Priority, "High")]
        [Description("CDR UnitTest for Garnet")]
        public void CDR_UnitTest_Garnet()
        {
            LogInfo("Unit test for Garnet");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QARecordControl_General_CDR();
            QaRcrdCtrl_GeneralNCR.ClickTab_Review_Assign_NCR();
            QaRcrdCtrl_GeneralNCR.ClickTab_Resolution_Disposition();
            QaRcrdCtrl_GeneralNCR.ClickTab_Engineer_Concurrence();
            QaRcrdCtrl_GeneralNCR.ClickTab_Owner_Concurrence();
            QaRcrdCtrl_GeneralNCR.ClickTab_Originator_Concurrence();
            QaRcrdCtrl_GeneralNCR.ClickTab_Verification();
            QaRcrdCtrl_GeneralNCR.ClickTab_Closed_NCR();
            QaRcrdCtrl_GeneralNCR.ClickTab_Creating_Revise();
            QaRcrdCtrl_GeneralNCR.ClickBtn_New();
        }
    }
}
