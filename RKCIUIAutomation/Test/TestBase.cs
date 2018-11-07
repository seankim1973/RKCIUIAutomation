using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects;
using RKCIUIAutomation.Page.PageObjects.LabFieldTests;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Page.Workflows;

namespace RKCIUIAutomation.Test
{
    public class TestBase : TestUtils
    {
        internal const string Component2 = "Component2";
        internal const string TestCaseNumber = "TC#";
        internal const string Priority = "Priority";

        public TestDetails TestDetails => new TestDetails(Driver);

        private ILoginPage LoginPage => new LoginPage().SetClass<ILoginPage>(Driver);

        public void LoginAs(UserType user) => LoginPage.LoginUser(user);

        public TableHelper TableHelper => new TableHelper(Driver);

        public IPageNavigation NavigateToPage => new PageNavigation().SetClass<IPageNavigation>(Driver);

        public ISearch RMCenter_SearchPage => new Search().SetClass<ISearch>(Driver);

        public IDesignDocument DesignDocCommentReview => new DesignDocument().SetClass<IDesignDocument>(Driver);

        public IGeneralNCR QaRcrdCtrl_GeneralNCR => new GeneralNCR().SetClass<IGeneralNCR>(Driver);

        public IGeneralCDR QaRcrdCtrl_GeneralCDR => new GeneralCDR().SetClass<IGeneralCDR>(Driver);

        public IQADIRs QaRcrdCtrl_QaDIR => new QADIRs().SetClass<IQADIRs>(Driver);


        #region Workflow SetClass method calls

        public ILinkCoverageWF WF_LinkCoverage => new LinkCoverageWF().SetClass<ILinkCoverageWF>(Driver);

        public IDesignDocumentWF WF_DesignDocCommentReview => new DesignDocumentWF().SetClass<IDesignDocumentWF>(Driver);

        public IQaRcrdCtrl_GeneralNCR_WF WF_QaRcrdCtrl_GeneralNCR => new QaRcrdCtrl_GeneralNCR_WF().SetClass<IQaRcrdCtrl_GeneralNCR_WF>(Driver);

        public IQaRcrdCtrl_GeneralCDR_WF WF_QaRcrdCtrl_GeneralCDR => new QaRcrdCtrl_GeneralCDR_WF().SetClass<IQaRcrdCtrl_GeneralCDR_WF>(Driver);

        public IQaRcrdCtrl_QaDIR_WF WF_QaRcrdCtrl_QaDIR => new QaRcrdCtrl_QaDIR_WF().SetClass<IQaRcrdCtrl_QaDIR_WF>(Driver);

        #endregion End of Workflow SetClass method calls
    }
}