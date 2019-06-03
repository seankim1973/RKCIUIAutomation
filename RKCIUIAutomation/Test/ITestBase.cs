using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects.LabFieldTests;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using RKCIUIAutomation.Page.PageObjects.QASearch;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Page.Workflows;

namespace RKCIUIAutomation.Test
{
    public interface ITestBase
    {
        IDesignDocument DesignDocCommentReview { get; set; }
        IPageNavigation NavigateToPage { get; set; }
        IProjectCorrespondenceLog ProjCorrespondenceLog { get; set; }
        IGeneralCDR QaRcrdCtrl_GeneralCDR { get; set; }
        IGeneralNCR QaRcrdCtrl_GeneralNCR { get; set; }
        IQADIRs QaRcrdCtrl_QaDIR { get; set; }
        IDailyInspectionReport QaSearch_DIR { get; set; }
        IInspectionDeficiencyLogReport QaSearch_InspctDefncyLogRprt { get; set; }
        ISearch RMCenterSearch { get; set; }
        ITableHelper TableHelper { get; set; }
        ITestDetails TestDetails { get; set; }
        IDesignDocumentWF WF_DesignDocCommentReview { get; set; }
        ILinkCoverageWF WF_LinkCoverage { get; set; }
        IQaRcrdCtrl_GeneralCDR_WF WF_QaRcrdCtrl_GeneralCDR { get; set; }
        IQaRcrdCtrl_GeneralNCR_WF WF_QaRcrdCtrl_GeneralNCR { get; set; }
        IQaRcrdCtrl_QaDIR_WF WF_QaRcrdCtrl_QaDIR { get; set; }

        void LoginAs(UserType user);
    }
}