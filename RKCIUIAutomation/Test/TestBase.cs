using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects;
using RKCIUIAutomation.Page.PageObjects.LabFieldTests;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using RKCIUIAutomation.Page.PageObjects.QASearch;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Page.Workflows;
using System;

namespace RKCIUIAutomation.Test
{
    public class TestBase : TestUtils
    {
        [ThreadStatic]
        private IWebDriver driver;

        internal const string Component2 = "Component2";
        internal const string TestCaseNumber = "TC#";
        internal const string Priority = "Priority";

        public TestDetails TestDetails
            => new TestDetails(driver = Driver);

        private ILoginPage LoginPage
            => new LoginPage().SetClass<ILoginPage>(driver = Driver);

        public void LoginAs(UserType user)
            => LoginPage.LoginUser(user);

        public TableHelper TableHelper
            => new TableHelper(driver = Driver);

        public IPageNavigation NavigateToPage
            => new PageNavigation().SetClass<IPageNavigation>(driver = Driver);

        public ISearch RMCenter_SearchPage
            => new Search().SetClass<ISearch>(driver = Driver);

        public IDesignDocument DesignDocCommentReview
            => new DesignDocument().SetClass<IDesignDocument>(driver = Driver);

        public IGeneralNCR QaRcrdCtrl_GeneralNCR
            => new GeneralNCR().SetClass<IGeneralNCR>(driver = Driver);

        public IGeneralCDR QaRcrdCtrl_GeneralCDR
            => new GeneralCDR().SetClass<IGeneralCDR>(driver = Driver);

        public IQADIRs QaRcrdCtrl_QaDIR
            => new QADIRs().SetClass<IQADIRs>(driver = Driver);

        public IDailyInspectionReport QaSearch_DIR
            => new DailyInspectionReport().SetClass<IDailyInspectionReport>(driver = Driver);

        public IInspectionDeficiencyLogReport QaSearch_InspctDefncyLogRprt
            => new InspectionDeficiencyLogReport().SetClass<IInspectionDeficiencyLogReport>(driver = Driver);

        #region Workflow SetClass method calls

        public ILinkCoverageWF WF_LinkCoverage => new LinkCoverageWF().SetClass<ILinkCoverageWF>(driver = Driver);

        public IDesignDocumentWF WF_DesignDocCommentReview => new DesignDocumentWF().SetClass<IDesignDocumentWF>(driver = Driver);

        public IQaRcrdCtrl_GeneralNCR_WF WF_QaRcrdCtrl_GeneralNCR => new QaRcrdCtrl_GeneralNCR_WF().SetClass<IQaRcrdCtrl_GeneralNCR_WF>(driver = Driver);

        public IQaRcrdCtrl_GeneralCDR_WF WF_QaRcrdCtrl_GeneralCDR => new QaRcrdCtrl_GeneralCDR_WF().SetClass<IQaRcrdCtrl_GeneralCDR_WF>(driver = Driver);

        public IQaRcrdCtrl_QaDIR_WF WF_QaRcrdCtrl_QaDIR => new QaRcrdCtrl_QaDIR_WF().SetClass<IQaRcrdCtrl_QaDIR_WF>(driver = Driver);

        #endregion Workflow SetClass method calls
    }
}