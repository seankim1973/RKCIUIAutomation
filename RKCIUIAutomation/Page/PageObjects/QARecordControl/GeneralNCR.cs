using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.GeneralNCR;

namespace RKCIUIAutomation.Page.PageObjects.QARecordControl
{
    #region NCR Generic class
    public class GeneralNCR : GeneralNCR_Impl
    {
        public GeneralNCR() { }
        public GeneralNCR(IWebDriver driver) => this.Driver = driver;

        public enum InputFields
        {
            [StringValue("IssuedDate")] IssuedDate,
            [StringValue("NonConformance")] Description,
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
            [StringValue("All NCRs")] All_NCRs,
            [StringValue("Closed NCR")] Closed_NCR,
            [StringValue("CQM Review")] CQM_Review,
            [StringValue("Creating/Revise")] Creating_Revise,
            [StringValue("Developer Concurrence")] Developer_Concurrence,
            [StringValue("DOT Approval")] DOT_Approval,
            [StringValue("Engineer Concurrence")] Engineer_Concurrence,
            [StringValue("Originator Concurrence")] Originator_Concurrence,
            [StringValue("Owner Concurrence")] Owner_Concurrence,
            [StringValue("QC Review")] QC_Review,
            [StringValue("Resolution/Disposition")] Resolution_Disposition,
            [StringValue("Review/Assign NCR")] Review_Assign_NCR,
            [StringValue("Revise")] Revise,
            [StringValue("To Be Closed")] To_Be_Closed,
            [StringValue("Verification")] Verification,
            [StringValue("Verification and Closure")] Verification_and_Closure
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

        public enum SubmitButtons
        {
            [StringValue("Cancel")] Cancel,
            [StringValue("Revise")] Revise,
            [StringValue("Approve")] Approve,
            [StringValue("Disapprove & Close")] DisapproveClose,
            [StringValue("Save Only")] SaveOnly,
            [StringValue("Save & Forward")] SaveForward
        }
    }
    #endregion  <-- end of NCR Generic Class 

    #region workflow interface class
    public interface IGeneralNCR
    {
        void ClickBtn_New();

        void ClickBtn_Cancel();

        void ClickBtn_SaveOnly();

        void ClickBtn_SaveForward();
        
        void ClickTab_Creating();

        void ClickTab_Revise();

        void PopulateRequiredFieldsAndSave();

        bool VerifyNCRDocInReviseTab();
    }
    #endregion workflow interface class

    #region Common Workflow Implementation class
    public abstract class GeneralNCR_Impl : PageBase, IGeneralNCR
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);
        private IGeneralNCR SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IGeneralNCR instance = new GeneralNCR(driver);

            if (tenantName == TenantName.SGWay)
            {
                LogInfo($"###### using GeneralNCR_SGWay instance ###### ");
                instance = new GeneralNCR_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                LogInfo($"###### using  GeneralNCR_SH249 instance ###### ");
                instance = new GeneralNCR_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                LogInfo($"###### using  GeneralNCR_Garnet instance ###### ");
                instance = new GeneralNCR_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                LogInfo($"###### using  GeneralNCR_GLX instance ###### ");
                instance = new GeneralNCR_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                LogInfo($"###### using  GeneralNCR_I15South instance ###### ");
                instance = new GeneralNCR_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                LogInfo($"###### using GeneralNCR_I15Tech instance ###### ");
                instance = new GeneralNCR_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                LogInfo($"###### using GeneralNCR_LAX instance ###### ");
                instance = new GeneralNCR_LAX(driver);
            }
            return instance;
        }

        private readonly By newBtn_ByLocator = By.XPath("//div[@id='NcrGrid_Revise']/div/a[contains(@class, 'k-button')]");
        private By GetSubmitBtnLocator(SubmitButtons buttonName)
        {
            string buttonValue = buttonName.GetString();
            By locator = By.XPath($"//input[@value='{buttonValue}']");
            return locator;
        }


        public virtual void ClickBtn_Cancel() => JsClickElement(GetSubmitBtnLocator(SubmitButtons.Cancel));

        public virtual void ClickBtn_SaveOnly() => JsClickElement(GetSubmitBtnLocator(SubmitButtons.SaveOnly));

        public virtual void ClickBtn_SaveForward() => JsClickElement(GetSubmitBtnLocator(SubmitButtons.SaveForward));

        public virtual void ClickBtn_New() => JsClickElement(newBtn_ByLocator);

        public virtual void ClickTab_Creating() => ClickTab(TableTab.Creating_Revise);

        public virtual void ClickTab_Revise() => ClickTab(TableTab.Revise);

        public void PopulateRequiredFieldsAndSave()
        {
            throw new NotImplementedException(); //TODO
        }

        public bool VerifyNCRDocInReviseTab()
        {
            return true; //TODO - need a way to get value unique to newly created NCR
        }
        

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

    #region Implementation specific to LAX
    public class GeneralNCR_LAX : GeneralNCR
    {
        public GeneralNCR_LAX(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to LAX
}
