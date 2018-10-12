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
        [Property(TestCaseNumber, 2187609)]
        [Property(Priority, "High")]
        [Description("To validate successful create and save of an CDR (Construction Deficiency Report) document.")]
        public void Create_And_Save_CDR_Document()
        {
            WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.Bhoomi);
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.QC_Review));
            LogoutToLoginPage();
        }
    }

    [TestFixture]
    public class Verify_QC_Review_of_CDR_document : TestBase
    {
        [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187610)]
        [Property(Priority, "High")]
        [Description("To validate the QC review part of an CDR (Construction Deficiency Report).")]
        public void QC_Review_of_CDR_document()
        {
            //string cdrDescription = WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.CDRTech);
            //LogoutToLoginPage();

            string cdrDescription = "vGYIfxuESzquJMzzfLYKxDAwEf"; //chnage the name once you create new CDR
            WF_QaRcrdCtrl_GeneralCDR.ReviewAndApproveCDRDocument(UserType.CDRMgr, cdrDescription);
        }
    }

    [TestFixture]
    public class Verify_Disapprove_and_Close_of_CDR_document_by_CDR_Manager : TestBase
    {
        [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187784)]
        [Property(Priority, "High")]
        [Description("To validate the QC disapprove and close part of an CDR (Construction Deficiency Report).")]
        public void QC_Review_of_CDR_document_by_CDR_Manager()
        {
            string cdrDescription = WF_QaRcrdCtrl_GeneralCDR.CreateAndSaveForwardCDRDocument(UserType.CDRTech);
            LogoutToLoginPage();
            WF_QaRcrdCtrl_GeneralCDR.CloseDocument(UserType.CDRMgr, cdrDescription);
            Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(TableTab.Closed_DN, cdrDescription));
        }
    }

    [TestFixture]
    public class Verify_Revising_the_CDR_Document : TestBase
    {
        [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187609)]
        [Property(Priority, "High")]
        [Description("To successfully revising an CDR (Construction Deficiency Report) document.")]
        public void Revise_the_CDR_Document()
        {
            LoginAs(UserType.CDRMgr);
            NavigateToPage.QARecordControl_General_CDR();
        }
    }

    [TestFixture]
    public class Verify_Closing_the_CDR_Document : TestBase
    {
        [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187784)]
        [Property(Priority, "High")]
        [Description("To successfully close an CDR (Construction Deficiency Report) document.")]
        public void Close_the_CDR_Document()
        {
            LoginAs(UserType.CDRMgr);
            NavigateToPage.QARecordControl_General_CDR();
        }
    }

    [TestFixture]
    public class Verify_Editing_the_CDR_Document : TestBase
    {
        [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187610)]
        [Property(Priority, "High")]
        [Description("To successfully edit an CDR (Construction Deficiency Report) document.")]
        public void Edit_the_CDR_Document()
        {
            LoginAs(UserType.CDRMgr);
            NavigateToPage.QARecordControl_General_CDR();
        }
    }

    [TestFixture]
    public class Verify_Vewing_CDR_Document_Report : TestBase
    {
        [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 2187609)]
        [Property(Priority, "High")]
        [Description("To successfully view the report of an CDR (Construction Deficiency Report) document.")]
        public void View_CDR_Document_Report()
        {
            LoginAs(UserType.CDRMgr);
            NavigateToPage.QARecordControl_General_CDR();
        }
    }

}
