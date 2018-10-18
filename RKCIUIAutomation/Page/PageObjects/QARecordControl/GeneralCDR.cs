using MiniGuids;
using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.GeneralCDR;

namespace RKCIUIAutomation.Page.PageObjects.QARecordControl
{
    #region CDR/DN Generic Class
    public class GeneralCDR : GeneralCDR_Impl
    {

        public GeneralCDR()
        {
        }

        public GeneralCDR(IWebDriver driver) => this.Driver = driver;

        public enum InputFields
        {
            [StringValue("PLC")] PLC,
            [StringValue("Subcontractor")] Subcontractor,
            [StringValue("Other")] Other,
            [StringValue("DwgNo")] DwgNo,
            [StringValue("SpecificationDescription")] SpecificationDescription,
            [StringValue("OtherLocation")] OtherLocation,
            [StringValue("ItemDescription")] ItemDescription,
            [StringValue("DeficiencyDescription")] DeficiencyDescription,
            [StringValue("IssuedDate")] IssuedDate,
            [StringValue("Correction")] Correction,
            [StringValue("Initiator")] Initiator,
            [StringValue("InitiatorDate")] InitiatorDate,
            [StringValue("ActionVerified")] ActionVerified,
            [StringValue("ActionVerifiedDate")] ActionVerifiedDate,
            [StringValue("CQCM")] CQCM,
            [StringValue("CQCMDate")] CQCMDate,
            [StringValue("CQManager")] CQManager,
            [StringValue("CQApprovedDate")] CQApprovedDate,
            [StringValue("Originator")] Originator,
           
        }

        public enum TableTab
        {
            [StringValue("Revise")] Revise,
            [StringValue("QC Review")] QC_Review,
            [StringValue("To Be Closed")] To_Be_Closed,
            [StringValue("Closed DN")] Closed_DN,
            [StringValue("Disposition")] Disposition,
            [StringValue("All")] All
            
        }

        public enum ColumnName
        {
            [StringValue("CdrNo")] CdrNo,
            [StringValue("RevisedBy")] SentBy,
            [StringValue("RevisedDate")] SentDate,
            [StringValue("Description")] Description,
            [StringValue("LockedBy")] LockBy,
            [StringValue("LockedDate")] LockDate,
            [StringValue("Action")] Action
        }

        public enum SubmitButtons
        {
            [StringValue("Cancel")] Cancel,
            [StringValue("Revise")] Revise,
            [StringValue("Close CDR")] Close_CDR,
            [StringValue("Close DN")] Close_DN,
            [StringValue("Save Only")] SaveOnly,
            [StringValue("Save & Forward")] SaveForward,
            [StringValue("Back To QC Review")] Back_To_QC_Review,
            [StringValue ("Back To Disposition")] Back_To_Disposition
        }

       
    }

    #endregion CDR Generic class

    #region workflow interface class

    public interface IGeneralCDR
    {
         void ClickBtn_New();

        void ClickBtn_Cancel();

        void ClickBtn_SaveOnly();

        void ClickBtn_SaveForward();

        void ClickBtn_Revise();

        void ClickBtn_CloseCDR();

        void ClickBtn_Back_To_QC_Review();

        void ClickBtn_Back_To_Disposition();

        void ClickTab_All();

        void ClickTab_Closed_DN();

        void ClickTab_QC_Review();

        void ClickTab_Disposition();

        void ClickTab_Revise();

        void ClickTab_To_Be_Closed();

        void FilterDescription(string description = "");

        void SortTable_Descending();

        void SortTable_Ascending();

        void SortTable_ToDefault();

        void SelectDDL_Originator(int selectionIndexOrName = 1);

        void EnterIssuedDate(string shortDate = "1/1/9999");

        void PopulateRequiredFieldsAndSaveForward();

        bool VerifyCDRDocIsDisplayed(TableTab tableTab, string CDRDescription = "");

        string GetCDRDocDescription();

        IList<string> GetRequiredFieldIDs();
    }

    #endregion workflow interface class

    #region Common Workflow Implementation class

    public abstract class GeneralCDR_Impl : PageBase, IGeneralCDR
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IGeneralCDR SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IGeneralCDR instance = new GeneralCDR(driver);

            if (tenantName == TenantName.SGWay)
            {
                LogInfo($"###### using GeneralCDR_SGWay instance ###### ");
                instance = new GeneralCDR_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                LogInfo($"###### using  GeneralCDR_SH249 instance ###### ");
                instance = new GeneralCDR_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                LogInfo($"###### using  GeneralCDR_Garnet instance ###### ");
                instance = new GeneralCDR_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                LogInfo($"###### using  GeneralCDR_GLX instance ###### ");
                instance = new GeneralCDR_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                LogInfo($"###### using  GeneralCDR_I15South instance ###### ");
                instance = new GeneralCDR_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                LogInfo($"###### using GeneralCDR_I15Tech instance ###### ");
                instance = new GeneralCDR_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                LogInfo($"###### using GeneralCDR_LAX instance ###### ");
                instance = new GeneralCDR_LAX(driver);
            }
            return instance;
        }


        [ThreadStatic]
        private static string cdrDescription;

        [ThreadStatic]
        private static string cdrDescKey;

        private MiniGuid guid;

        private readonly By newBtn_ByLocator = By.XPath("//div[@id='CdrGrid_Revise']/div/a[contains(@class, 'k-button')]");
       
        private By GetSubmitBtnLocator(SubmitButtons buttonName)
        {
            string buttonValue = buttonName.GetString();
            By locator = By.XPath($"//input[@value='{buttonValue}']");
            return locator;
        }

        public virtual void ClickBtn_Cancel() => JsClickElement(GetSubmitBtnLocator(SubmitButtons.Cancel));

        public virtual void ClickBtn_SaveOnly() => JsClickElement(GetSubmitBtnLocator(SubmitButtons.SaveOnly));

        public virtual void ClickBtn_SaveForward() => JsClickElement(GetSubmitBtnLocator(SubmitButtons.SaveForward));

        public virtual void ClickBtn_CloseCDR() => JsClickElement(GetSubmitBtnLocator(SubmitButtons.Close_CDR));

        public virtual void ClickBtn_Revise() => JsClickElement(GetSubmitBtnLocator(SubmitButtons.Revise));

        public virtual void ClickBtn_Back_To_Disposition() => JsClickElement(GetSubmitBtnLocator(SubmitButtons.Back_To_Disposition));

        public virtual void ClickBtn_Back_To_QC_Review() => JsClickElement(GetSubmitBtnLocator(SubmitButtons.Back_To_QC_Review));


        public virtual void ClickBtn_New() => JsClickElement(newBtn_ByLocator);

        public virtual void ClickTab_All() => ClickTab(TableTab.All);
        public virtual void ClickTab_Closed_DN() => ClickTab(TableTab.Closed_DN);

       
        public virtual void ClickTab_QC_Review() => ClickTab(TableTab.QC_Review);

        public virtual void ClickTab_Disposition() => ClickTab(TableTab.Disposition);

       

        public virtual void ClickTab_Revise() => ClickTab(TableTab.Revise);

        public virtual void ClickTab_To_Be_Closed() => ClickTab(TableTab.To_Be_Closed);

        public virtual void SortTable_Descending() => SortColumnDescending(ColumnName.Action);

        public virtual void SortTable_Ascending() => SortColumnAscending(ColumnName.Action);

        public virtual void SortTable_ToDefault() => SortColumnToDefault(ColumnName.Action);

        public virtual void SelectDDL_Originator(int selectionIndex = 1) => ExpandAndSelectFromDDList(InputFields.Originator, selectionIndex);

        public virtual void EnterIssuedDate(string shortDate = "1/1/9999")
           => EnterText(GetTextInputFieldByLocator(InputFields.IssuedDate), GetMaxShortDate());

        private void CreateCdrDescription()
        {
            guid = MiniGuid.NewGuid();

            string descKey = $"{tenantName}{GetTestName()}";
            cdrDescKey = $"{descKey}_CdrDescription";
            CreateVar(cdrDescKey, guid);
            cdrDescription = GetVar(cdrDescKey);
            Console.WriteLine($"#####NCR Description: {cdrDescription}");
        }

        public virtual string GetCDRDocDescription() => GetVar(cdrDescKey);

        public virtual void EnterDescription(string description = "")
        {
            CreateCdrDescription();
            description = cdrDescription;
            ScrollToElement(By.Id($"{InputFields.DeficiencyDescription.GetString()}"));
            EnterText(GetTextAreaFieldByLocator(InputFields.DeficiencyDescription), description);
        }

        private bool VerifyReqFieldsErrorLabelsForNewDoc()
        {
            try
            {
                bool errorLabelsDisplayed = false;

                IList<IWebElement> ReqFieldErrorLabelElements = Driver.FindElements(By.XPath("//span[contains(@class, 'ValidationErrorMessage')]"));

                IList<string> RequiredFieldIDs = GetRequiredFieldIDs();

                IList<bool> results = new List<bool>();

                foreach (IWebElement elem in ReqFieldErrorLabelElements)
                {
                    if (elem.Displayed && elem.Enabled)
                    {
                        var id = elem.GetAttribute("data-valmsg-for");
                        Console.WriteLine(id);
                        results.Add(RequiredFieldIDs.Contains(id));
                    }
                }

                Console.WriteLine($"REQUIRED FIELD COUNT: {results.Count}");
                errorLabelsDisplayed = results.Contains(false) ? false : true;

                return errorLabelsDisplayed;
            }
            catch (Exception e)
            {
                LogError(e.Message);
                return false;
            }
        }


        public void PopulateRequiredFieldsAndSaveForward()
        {
            ClickBtn_SaveForward();
            Assert.True(VerifyReqFieldsErrorLabelsForNewDoc());
            EnterIssuedDate();
            SelectDDL_Originator();
            EnterDescription();
            
            ClickBtn_SaveForward();
            WaitForPageReady();
        }
        public virtual IList<string> GetRequiredFieldIDs()
        {
            List<string> RequiredFieldIDs = new List<string>
            {
                InputFields.IssuedDate.GetString(),
                InputFields.Originator.GetString(),
              
            };

            return RequiredFieldIDs;
        }

        public virtual bool VerifyCDRDocIsDisplayed(TableTab tableTab, string CDRDescription = "")
        {
            ClickTab(tableTab);
            cdrDescription = string.IsNullOrWhiteSpace(CDRDescription) ? cdrDescription : CDRDescription;
            return VerifyRecordIsDisplayed(ColumnName.Description, cdrDescription);
        }

        public virtual void FilterDescription(string description = "")
        {
            cdrDescription = !string.IsNullOrWhiteSpace(description) ? description : cdrDescription;
            FilterTableColumnByValue(ColumnName.Description, cdrDescription);
        }



    }

    #endregion Common Workflow Implementation class

    /// <summary>
    /// Tenant specific implementation of CDR/DN Comment Review
    /// </summary>
    ///

    #region Implementation specific to Garnet

    public class GeneralCDR_Garnet : GeneralCDR
    {
        public GeneralCDR_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to Garnet

    #region Implementation specific to GLX

    public class GeneralCDR_GLX : GeneralCDR
    {
        public GeneralCDR_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to GLX

    #region Implementation specific to SH249

    public class GeneralCDR_SH249 : GeneralCDR
    {
        public GeneralCDR_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to SH249

    #region Implementation specific to SGWay

    public class GeneralCDR_SGWay : GeneralCDR
    {
        public GeneralCDR_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to SGWay

    #region Implementation specific to I15South

    public class GeneralCDR_I15South : GeneralCDR
    {
        public GeneralCDR_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15South

    #region Implementation specific to I15Tech

    public class GeneralCDR_I15Tech : GeneralCDR
    {
        public GeneralCDR_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15Tech

    #region Implementation specific to LAX

    public class GeneralCDR_LAX : GeneralCDR
    {
        public GeneralCDR_LAX(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to LAX
}