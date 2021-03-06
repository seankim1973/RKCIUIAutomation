﻿using log4net;
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

        public static IBaseUtils BaseUtil => new BaseUtils();
        public static IBaseUtils SetReportPath(TenantNameType tenantName) => new BaseUtils(tenantName);
        public static IBaseUtils SetGridAddress(TestPlatformType testPlatform, string gridAddress) => new BaseUtils(testPlatform, gridAddress);

        public static IConfigUtils ConfigUtil => new ConfigUtils();

        public static ITenantProperties TenantProperty => new TenantProperties();

        public static IPageInteraction PageAction => new PageInteraction(driver);

        public static IPageHelper PgHelper => new PageHelper(driver);

        public static ITestUtils TestUtility => new TestUtils(driver);

        public static IReportLogger Report => new ReportLogger(driver);

        public static ITableHelper GridHelper => new TableHelper(driver);

        //PageObject Classes
        //public static void LoginAs(UserType user) => LoginPage.LoginUser(user);

        public static ITestDetails TestDetailsPg => new TestDetails().SetClass<ITestDetails>(driver);

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

        public static IQATestAll QATestMethod
            => new QATestAll_Common().SetClass<IQATestAll>(driver);

        public static IUploadDEVSubmittal UploadDEVSubmittal => new UploadDEVSubmittal().SetClass<IUploadDEVSubmittal>(driver);

        public static IUploadQASubmittal UploadQASubmittal => new UploadQASubmittal().SetClass<IUploadQASubmittal>(driver);

        public static IUploadOwnerSubmittal UploadOwnerSubmittal => new UploadOwnerSubmittal().SetClass<IUploadOwnerSubmittal>(driver);

        #region Workflow SetClass method calls

        public static ILinkCoverageWF WF_LinkCoverage => new LinkCoverageWF().SetClass<ILinkCoverageWF>(driver);

        public static IDesignDocumentWF WF_DesignDocCommentReview => new DesignDocumentWF().SetClass<IDesignDocumentWF>(driver);

        public static IQaRcrdCtrl_GeneralNCR_WF WF_QaRcrdCtrl_GeneralNCR => new QaRcrdCtrl_GeneralNCR_WF().SetClass<IQaRcrdCtrl_GeneralNCR_WF>(driver);

        public static IQaRcrdCtrl_GeneralCDR_WF WF_QaRcrdCtrl_GeneralCDR => new QaRcrdCtrl_GeneralCDR_WF().SetClass<IQaRcrdCtrl_GeneralCDR_WF>(driver);

        public static IQaRcrdCtrl_QaDIR_WF WF_QaRcrdCtrl_QaDIR => new QaRcrdCtrl_QaDIR_WF().SetClass<IQaRcrdCtrl_QaDIR_WF>(driver);

        #endregion Workflow SetClass method calls

    }
}