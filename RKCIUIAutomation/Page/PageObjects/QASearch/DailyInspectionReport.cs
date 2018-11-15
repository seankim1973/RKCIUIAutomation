using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using static RKCIUIAutomation.Page.PageObjects.QASearch.DailyInspectionReport;

namespace RKCIUIAutomation.Page.PageObjects.QASearch
{
    public class DailyInspectionReport : DailyInspectionReport_Impl
    {
        public DailyInspectionReport()
        {
        }

        public DailyInspectionReport(IWebDriver driver) => this.Driver = driver;

        public enum ColumnName
        {
            [StringValue ("DIRNO")] DIR_No,            
            [StringValue ("Revision")] Revision,
            [StringValue ("InspectDate")] Inspection_Date,
            [StringValue ("InspectBy")] Inspection_By,
            [StringValue ("Comments")] Workflow_Location,
            [StringValue ("LabField")] Belong_To,
            [StringValue ("Area")] Area,
            [StringValue ("SpecSection")] Spec_Section,
            [StringValue ("Reviewby")] Reviewed_By,
            [StringValue ("ApproveBy")] Approved_By,
            [StringValue ("ApproveDate")] Approve_Date
        }
    }

    public interface IDailyInspectionReport
    {
        bool VerifyDirIsDisplayed(string dirNumber);

        bool VerifyDirIsClosed(string dirNumber);
    }

    public abstract class DailyInspectionReport_Impl : TestBase, IDailyInspectionReport
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Tenant'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        public IDailyInspectionReport SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IDailyInspectionReport instance = new DailyInspectionReport(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using DailyInspectionReport_SGWay instance ###### ");
                instance = new DailyInspectionReport_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using DailyInspectionReport_SH249 instance ###### ");
                instance = new DailyInspectionReport_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using DailyInspectionReport_Garnet instance ###### ");
                instance = new DailyInspectionReport_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using DailyInspectionReport_GLX instance ###### ");
                instance = new DailyInspectionReport_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using DailyInspectionReport_I15South instance ###### ");
                instance = new DailyInspectionReport_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using DailyInspectionReport_I15Tech instance ###### ");
                instance = new DailyInspectionReport_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using DailyInspectionReport_LAX instance ###### ");
                instance = new DailyInspectionReport_LAX(driver);
            }
            return instance;
        }

        public virtual bool VerifyDirIsDisplayed(string dirNumber)
        {
            bool isDisplayed = false;
            ColumnName dirNumberCol = ColumnName.DIR_No;
            try
            {
                FilterColumn(dirNumberCol, dirNumber);
                isDisplayed = VerifyRecordIsDisplayed(dirNumberCol, dirNumber);
                string logMsg = isDisplayed ? "Found" : "Unable to find";
                LogInfo($"{logMsg} DIR record number {dirNumber}.", isDisplayed);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return isDisplayed;
        }

        public virtual bool VerifyDirIsClosed(string dirNumber)
        {
            bool dirIsClosed = false;
            string logMsg = "not found.";

            try
            {
                string _dirNumber = dirNumber.Equals("") ? QaRcrdCtrl_QaDIR.GetDirNumber() : dirNumber;
                NavigateToPage.QASearch_Daily_Inspection_Report();
                bool isDisplayed = QaSearch_DIR.VerifyDirIsDisplayed(_dirNumber);

                if (isDisplayed)
                {
                    string docStatus = GetColumnValueForRow(_dirNumber, "Workflow Location");
                    dirIsClosed = docStatus.Equals("Closed") ? true : false;
                    logMsg = $"Workflow Location displayed as: {docStatus}";
                }

                LogInfo($"DIR Number {_dirNumber}, {logMsg}", dirIsClosed);

            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return dirIsClosed;
        }
    }

    public class DailyInspectionReport_Garnet : DailyInspectionReport
    {
        public DailyInspectionReport_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    public class DailyInspectionReport_GLX : DailyInspectionReport
    {
        public DailyInspectionReport_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    public class DailyInspectionReport_SH249 : DailyInspectionReport
    {
        public DailyInspectionReport_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    public class DailyInspectionReport_SGWay : DailyInspectionReport
    {
        public DailyInspectionReport_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    public class DailyInspectionReport_I15South : DailyInspectionReport
    {
        public DailyInspectionReport_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    public class DailyInspectionReport_I15Tech : DailyInspectionReport
    {
        public DailyInspectionReport_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    public class DailyInspectionReport_LAX : DailyInspectionReport
    {
        public DailyInspectionReport_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}