using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.PageObjects.QARecordControl
{
    #region NCR Generic class
    public class GeneralNCR : GeneralNCR_Impl
    {
        public GeneralNCR() { }
        public GeneralNCR(IWebDriver driver) => this.Driver = driver;

        public enum GeneralNCR_InputFields
        {
            [StringValue("IssuedDate")] NCRIssueDate,
            [StringValue("NcrDescription")] NcrDescription,
            [StringValue("OtherLocation")] OtherLocation,
            [StringValue("ContainmentActions")] ContainmentActions,
            [StringValue("CorrectiveAction")] CorrectiveAction,
            [StringValue("RepairPlan")] RepairPlan,
            [StringValue("RecordEngineer")] RecordEngineer,
            [StringValue("RecordEngineerApprovedDate")] RecordEngineerApprovedDate,
            [StringValue("Owner")] Owner,
            [StringValue("OwnerDate")] OwnerDate,
            [StringValue("CompletionDate")] CompletionDate,
            [StringValue("CIQM")] CIQM,
            [StringValue("CIQMDate")] CIQMDate,
            [StringValue("QualityManager")] QualityManager,
            [StringValue("QualityManagerApprovedDate")] QualityManagerApprovedDate
        }
        public enum TableTab
        {
            [StringValue("Creating/Revise")] Creating,
            [StringValue("CQM Review")] CQMReview,
            [StringValue("Resolution/Disposition")] ResolutionDisposition,
            [StringValue("Developer Concurrence")] DeveloperConcurrence,
            [StringValue("DOT Approval")] DOTApproval,
            [StringValue("Verification and Closure")] VerificationAndClosure,
            [StringValue("All NCRs")] AllNCRs
        }

        public enum ColumnName
        {
            [StringValue("NcrNo")] NcrNo,
            [StringValue("RevisedBy")] SentBy,
            [StringValue("RevisedDate")] SentDate,
            [StringValue("Description")] Description,
            [StringValue("StatusFlowItemName")] WorkflowLocation,
            [StringValue("LockedBy")] LockedBy,
            [StringValue("LockedDate")] LockedDate
        }
    }
    #endregion  <-- end of NCR Generic Class 

    #region workflow interface class
    public interface IGeneralNCR
    {
    }
    #endregion workflow interface class

    #region Common Workflow Implementation class
    public abstract class GeneralNCR_Impl : PageBase, IGeneralNCR
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        /// 
    }
    #endregion Common Workflow Implementation class

    /// <summary>
    /// Tenant specific implementation of DesignDocument Comment Review
    /// </summary>
    /// 

    #region Implementation specific to Garnet
    public class GeneralNCR_Garnet : GeneralNCR
    {
        public GeneralNCR_Garnet(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to Garnet

    #region Implementation specific to GLX
    public class GeneralNCR_GLX : GeneralNCR
    {
        public GeneralNCR_GLX(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to GLX

    #region Implementation specific to SH249    
    public class GeneralNCR_SH249 : GeneralNCR
    {
        public GeneralNCR_SH249(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to SH249


    #region Implementation specific to SGWay
    public class GeneralNCR_SGWay : GeneralNCR
    {
        public GeneralNCR_SGWay(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to SGWay

    #region Implementation specific to I15South
    public class GeneralNCR_I15South : GeneralNCR
    {
        public GeneralNCR_I15South(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to I15South

    #region Implementation specific to I15Tech
    public class GeneralNCR_I15Tech : GeneralNCR
    {
        public GeneralNCR_I15Tech(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to I15Tech

}
