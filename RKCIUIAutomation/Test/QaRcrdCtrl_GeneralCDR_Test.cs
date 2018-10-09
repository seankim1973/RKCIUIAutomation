using NUnit.Framework;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.GeneralCDR;

namespace RKCIUIAutomation.Test.CDR
{

    [TestFixture]
    public class Verify_Create_And_Save_CDR_Document : TestBase
    {
        [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187687)]
        [Property(Priority, "High")]
        [Description("To validate successful create and save of an CDR (Construction Deficiency Report) document.")]
        public void Create_And_Save_CDR_Document()
        {
            WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.Bhoomi);
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.QC_Review));
            LogoutToLoginPage();
        }
    }

}
