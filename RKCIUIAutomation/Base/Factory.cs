using log4net;
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
using RKCIUIAutomation.Test;
using static RKCIUIAutomation.Base.WebDriverFactory;

namespace RKCIUIAutomation.Base
{
    public static class Factory
    {
        static Factory()
        {
        }

        private static ILog _logger(string loggerName = "") => LogManager.GetLogger(loggerName);
        public static readonly ILog log = _logger();

        private static KendoGrid _Kendo() => new KendoGrid(driver);
        public static IKendoGrid Kendo() => _Kendo();

        //private static Action _Action() => new Action();
        public static IAction PageAction => new Action(driver);

        //private static BaseUtils _BaseUtils => new BaseUtils();
        public static IBaseUtils BaseUtil => new BaseUtils(driver);
        public static IBaseUtils SetReportPath(TenantName tenantName) => new BaseUtils(tenantName);
        public static IBaseUtils SetGridAddress(TestPlatform testPlatform, string gridAddress) => new BaseUtils(testPlatform, gridAddress);

        //private static ConfigUtils _ConfigUtils() => 
        public static IConfigUtils ConfigUtil => new ConfigUtils(driver);

        //private static TableHelper _TableHelper() => new TableHelper();
        public static ITableHelper GridHelper => new TableHelper(driver);

        //private static TestUtils _TestUtils() => 
        public static ITestUtils TestUtility => new TestUtils(driver);

        //private static PageHelper _PageHelper() => new PageHelper();
        public static IPageHelper PgHelper => new PageHelper(driver);

        //private static ProjectProperties _ProjectProperties() => new ProjectProperties();
        public static IProjectProperties Props => new ProjectProperties(driver);
        public static IProjectProperties SetTenantComponents(TenantName tenantName) => new ProjectProperties(tenantName);
       
        public static IReportLogger Report => new ReportLogger(driver);

        //PageObject Classes

        //public static ITableHelper TableHelper => new TableHelper(driver);

        public static ITestDetails TestDetails => new TestDetails(driver);

        public static void LoginAs(UserType user) => LoginPage.LoginUser(user);

        public static ILoginPage LoginPage => new LoginPage().SetClass<ILoginPage>(driver);

        public static IPageNavigation NavigateToPage => new PageNavigation().SetClass<IPageNavigation>(driver);

        public static ISearch RMCenterSearch => new Search().SetClass<ISearch>(driver);

        public static IDesignDocument DesignDocCommentReview => new DesignDocument().SetClass<IDesignDocument>(driver);

        public static IGeneralNCR QaRcrdCtrl_GeneralNCR => new GeneralNCR().SetClass<IGeneralNCR>(driver);

        public static IGeneralCDR QaRcrdCtrl_GeneralCDR => new GeneralCDR().SetClass<IGeneralCDR>(driver);

        public static IQADIRs QaRcrdCtrl_QaDIR => new QADIRs().SetClass<IQADIRs>(driver);

        public static IDailyInspectionReport QaSearch_DIR => new DailyInspectionReport().SetClass<IDailyInspectionReport>(driver);

        public static IInspectionDeficiencyLogReport QaSearch_InspctDefncyLogRprt => new InspectionDeficiencyLogReport().SetClass<IInspectionDeficiencyLogReport>(driver);

        public static IProjectCorrespondenceLog ProjCorrespondenceLog => new ProjectCorrespondenceLog().SetClass<IProjectCorrespondenceLog>(driver);

        #region Workflow SetClass method calls

        public static ILinkCoverageWF WF_LinkCoverage => new LinkCoverageWF().SetClass<ILinkCoverageWF>(driver);

        public static IDesignDocumentWF WF_DesignDocCommentReview => new DesignDocumentWF().SetClass<IDesignDocumentWF>(driver);

        public static IQaRcrdCtrl_GeneralNCR_WF WF_QaRcrdCtrl_GeneralNCR => new QaRcrdCtrl_GeneralNCR_WF().SetClass<IQaRcrdCtrl_GeneralNCR_WF>(driver);

        public static IQaRcrdCtrl_GeneralCDR_WF WF_QaRcrdCtrl_GeneralCDR => new QaRcrdCtrl_GeneralCDR_WF().SetClass<IQaRcrdCtrl_GeneralCDR_WF>(driver);

        public static IQaRcrdCtrl_QaDIR_WF WF_QaRcrdCtrl_QaDIR => new QaRcrdCtrl_QaDIR_WF().SetClass<IQaRcrdCtrl_QaDIR_WF>(driver);

        #endregion Workflow SetClass method calls

    }
}