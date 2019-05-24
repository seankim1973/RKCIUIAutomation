using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects;
using RKCIUIAutomation.Page.PageObjects.LabFieldTests;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using RKCIUIAutomation.Page.PageObjects.QASearch;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Page.Workflows;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Test
{
    public class TestBase : TestUtils
    {
        internal const string Component2 = "Component2";
        internal const string TestCaseNumber = "TC#";
        internal const string Priority = "Priority";

        public ITableHelper TableHelper => new TableHelper(driver);

        public ITestDetails TestDetails => new TestDetails(driver);

        public void LoginAs(UserType user) => LoginPage.LoginUser(user);

        public ILoginPage LoginPage => new LoginPage().SetClass<ILoginPage>(driver);

        public IPageNavigation NavigateToPage => new PageNavigation().SetClass<IPageNavigation>(driver);

        public ISearch RMCenterSearch => new Search().SetClass<ISearch>(driver);

        public IDesignDocument DesignDocCommentReview => new DesignDocument().SetClass<IDesignDocument>(driver);

        public IGeneralNCR QaRcrdCtrl_GeneralNCR => new GeneralNCR().SetClass<IGeneralNCR>(driver);

        public IGeneralCDR QaRcrdCtrl_GeneralCDR => new GeneralCDR().SetClass<IGeneralCDR>(driver);

        public IQADIRs QaRcrdCtrl_QaDIR => new QADIRs().SetClass<IQADIRs>(driver);

        public IDailyInspectionReport QaSearch_DIR => new DailyInspectionReport().SetClass<IDailyInspectionReport>(driver);

        public IInspectionDeficiencyLogReport QaSearch_InspctDefncyLogRprt => new InspectionDeficiencyLogReport().SetClass<IInspectionDeficiencyLogReport>(driver);

        public IProjectCorrespondenceLog ProjCorrespondenceLog => new ProjectCorrespondenceLog().SetClass<IProjectCorrespondenceLog>(driver);

        #region Workflow SetClass method calls

        public ILinkCoverageWF WF_LinkCoverage => new LinkCoverageWF().SetClass<ILinkCoverageWF>(driver);

        public IDesignDocumentWF WF_DesignDocCommentReview => new DesignDocumentWF().SetClass<IDesignDocumentWF>(driver);

        public IQaRcrdCtrl_GeneralNCR_WF WF_QaRcrdCtrl_GeneralNCR => new QaRcrdCtrl_GeneralNCR_WF().SetClass<IQaRcrdCtrl_GeneralNCR_WF>(driver);

        public IQaRcrdCtrl_GeneralCDR_WF WF_QaRcrdCtrl_GeneralCDR => new QaRcrdCtrl_GeneralCDR_WF().SetClass<IQaRcrdCtrl_GeneralCDR_WF>(driver);

        public IQaRcrdCtrl_QaDIR_WF WF_QaRcrdCtrl_QaDIR => new QaRcrdCtrl_QaDIR_WF().SetClass<IQaRcrdCtrl_QaDIR_WF>(driver);

        #endregion Workflow SetClass method calls
    }
}