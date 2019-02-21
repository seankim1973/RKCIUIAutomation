using MiniGuids;
using NUnit.Framework;
using OpenQA.Selenium;
using RestSharp.Extensions;
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
            [StringValue("Back To Disposition")] Back_To_Disposition,
            [StringValue("QC Disagree")] QC_Disagree,
            [StringValue("QC Agree")] QC_Agree
        }

        [ThreadStatic]
        internal static string cdrDescription;

        [ThreadStatic]
        internal static string cdrNewDescription;

        [ThreadStatic]
        internal static string cdrDescKey;

        [ThreadStatic]
        internal static string cdrNewDescKey;

        internal readonly By newBtn_ByLocator = By.XPath("//div[@id='CdrGrid_Revise']/div/a[contains(@class, 'k-button')]");

        internal By GetSubmitBtnLocator(SubmitButtons buttonName)
        {
            string buttonValue = buttonName.GetString();
            By locator = By.XPath($"//input[@value='{buttonValue}']");
            return locator;
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

        string EnterDescription(string description = "", bool tempDescription = false);

        bool VerifyCDRDocIsClosed(string description = "");

        void SortTable_Descending();

        void SortTable_Ascending();

        void SortTable_ToDefault();

        void SelectDDL_Originator(int selectionIndexOrName = 1);

        void EnterIssuedDate(string shortDate = "1/1/9999");

        void PopulateRequiredFieldsAndSaveForward();

        bool VerifyCDRDocIsDisplayed(TableTab tableTab, string CDRDescription = "");

        string GetCDRDocDescription(bool tempDescription = false);
        
      

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
                log.Info($"###### using GeneralCDR_SGWay instance ###### ");
                instance = new GeneralCDR_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using  GeneralCDR_SH249 instance ###### ");
                instance = new GeneralCDR_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using  GeneralCDR_Garnet instance ###### ");
                instance = new GeneralCDR_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using  GeneralCDR_GLX instance ###### ");
                instance = new GeneralCDR_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using  GeneralCDR_I15South instance ###### ");
                instance = new GeneralCDR_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using GeneralCDR_I15Tech instance ###### ");
                instance = new GeneralCDR_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using GeneralCDR_LAX instance ###### ");
                instance = new GeneralCDR_LAX(driver);
            }
            return instance;
        }

        internal GeneralCDR GeneralCDR_Base => new GeneralCDR(Driver);

        public virtual void ClickBtn_Cancel() => JsClickElement(GeneralCDR_Base.GetSubmitBtnLocator(SubmitButtons.Cancel));

        public virtual void ClickBtn_SaveOnly() => JsClickElement(GeneralCDR_Base.GetSubmitBtnLocator(SubmitButtons.SaveOnly));

        public virtual void ClickBtn_SaveForward() => JsClickElement(GeneralCDR_Base.GetSubmitBtnLocator(SubmitButtons.SaveForward));

        public virtual void ClickBtn_CloseCDR() => JsClickElement(GeneralCDR_Base.GetSubmitBtnLocator(SubmitButtons.Close_CDR));

        public virtual void ClickBtn_Revise() => JsClickElement(GeneralCDR_Base.GetSubmitBtnLocator(SubmitButtons.Revise));

        public virtual void ClickBtn_Back_To_Disposition() => JsClickElement(GeneralCDR_Base.GetSubmitBtnLocator(SubmitButtons.Back_To_Disposition));

        public virtual void ClickBtn_Back_To_QC_Review() => JsClickElement(GeneralCDR_Base.GetSubmitBtnLocator(SubmitButtons.Back_To_QC_Review));


        public virtual void ClickBtn_New() => JsClickElement(GeneralCDR_Base.newBtn_ByLocator);

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

        private void CreateCdrDescription(bool tempDescription = false)
        {
            MiniGuid guid = MiniGuid.NewGuid();
            string descKey = $"{tenantName}{GetTestName()}";
            string logMsg = string.Empty;
            string descValue = string.Empty;

            if (tempDescription)
            {
                cdrNewDescKey = $"{descKey}_CdrNewDescription";
                CreateVar(cdrNewDescKey, guid);
                cdrNewDescription = GetVar(cdrNewDescKey);
                descKey = cdrNewDescKey;
                descValue = cdrNewDescription;
                logMsg = "new temp ";
            }
            else
            {
                cdrDescKey = $"{descKey}_CdrDescription";
                CreateVar(cdrDescKey, guid);
                cdrDescription = GetVar(cdrDescKey);
                descKey = cdrDescKey;
                descValue = cdrDescription;
                logMsg = "";
            }

            log.Debug($"#####Created a {logMsg}CDR Description: KEY: {descKey} VALUE: {descValue}");


            //guid = MiniGuid.NewGuid();

            //string descKey = $"{tenantName}{GetTestName()}";
            //cdrDescKey = $"{descKey}_CdrDescription";
            //CreateVar(cdrDescKey, guid);
            //cdrDescription = GetVar(cdrDescKey);
            //Console.WriteLine($"#####NCR Description: {cdrDescription}");
        }

        public virtual string GetCDRDocDescription(bool tempDescription = false)
                   => GetVar(tempDescription ? cdrNewDescKey : cdrDescKey);

        public virtual string EnterDescription(string description = "", bool tempDescription = false)
            => EnterDesc(description, InputFields.DeficiencyDescription);

        internal string EnterDesc(string desc, InputFields descField, bool tempDescription = false)
        {
            By descLocator = GetTextAreaFieldByLocator(descField);
            CreateCdrDescription(tempDescription);
            desc = desc.Equals("") || string.IsNullOrEmpty(desc) ? GetCDRDocDescription(tempDescription) : desc;
            EnterText(descLocator, desc);
            return desc;
        }

        public virtual bool VerifyCDRDocIsClosed(string description = "")
           => CheckCDRisClosed(description, TableTab.Closed_DN);

        internal bool CheckCDRisClosed(string description, TableTab closedTab)
        {
            bool cdrIsClosed = false;
            string logMsg = "not found.";

            try
            {
                string _cdrDesc = description.Equals("") ? GetCDRDocDescription() : description;
                bool isDisplayed = VerifyCDRDocIsDisplayed(closedTab, _cdrDesc);

                if (isDisplayed)
                {
                    string docStatus = GetColumnValueForRow(_cdrDesc, "Workflow location");
                    cdrIsClosed = docStatus.Equals("Closed") ? true : false;
                    logMsg = $"Workflow Location displayed as: {docStatus}";
                }

                LogInfo($"CDR with description ({_cdrDesc}), {logMsg}", cdrIsClosed);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return cdrIsClosed;
        }


        //public virtual void EnterDescription(string description = "")
        //{
        //    CreateCdrDescription();
        //    description = cdrDescription;
        //    ScrollToElement(By.Id($"{InputFields.DeficiencyDescription.GetString()}"));
        //    EnterText(GetTextAreaFieldByLocator(InputFields.DeficiencyDescription), description);
        //}

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
            cdrDescription = CDRDescription.HasValue() ? CDRDescription : cdrDescription;
            return VerifyRecordIsDisplayed(ColumnName.Description, cdrDescription);
        }

        public virtual void FilterDescription(string description = "")
        {
            cdrDescription = description.HasValue() ? description : cdrDescription;
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

        public override void ClickBtn_Back_To_Disposition()
            => JsClickElement(GetSubmitBtnLocator(SubmitButtons.QC_Disagree));

        public override void ClickBtn_CloseCDR()
            => JsClickElement(GeneralCDR_Base.GetSubmitBtnLocator(SubmitButtons.QC_Agree));
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