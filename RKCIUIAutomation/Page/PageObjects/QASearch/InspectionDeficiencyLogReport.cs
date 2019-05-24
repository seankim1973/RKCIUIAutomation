using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using static RKCIUIAutomation.Page.PageObjects.QASearch.InspectionDeficiencyLogReport;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.TableHelper;

namespace RKCIUIAutomation.Page.PageObjects.QASearch
{
    public class InspectionDeficiencyLogReport : InspectionDeficiencyLogReport_Impl
    {
        public InspectionDeficiencyLogReport()
        {
        }

        public InspectionDeficiencyLogReport(IWebDriver driver) => this.Driver = driver;

        public enum InputFields
        {
            [StringValue("InspectBy")] Inspection_By, //text input
            [StringValue("StructureId")] Location, //ddList
            [StringValue("OpenClosed")] Closed, //ddList
            [StringValue("FeatureTypeId")] Feature //ddList
        }

        public enum ColumnName
        {
            [StringValue("DeficiencyNo")] Deficiency_No,
            [StringValue("DIR")] DIR_No,
            [StringValue("DirId")] ReportView_forDIR,
            [StringValue("Type")] Type,
            [StringValue("InspectionDate")] Inspection_Date,
            [StringValue("Crew")] Crew,
            [StringValue("InspectedBy")] Inspected_By,
            [StringValue("Description")] Description,
            [StringValue("Location")] Location,
            [StringValue("Status")] Status,
            [StringValue("ClosedDir")] Closed_Dir,
            [StringValue("ClosedBy")] Closed_By,
            [StringValue("ClosedDescription")] Closed_Description,
            [StringValue("ClosedDirId")] ReportView_forClosedDIR,
            [StringValue("ClosedDate")] Closed_Date
        }

        public enum OpenClosedStatus
        {
            Open,
            Closed
        }
    }

    public interface IInspectionDeficiencyLogReport
    {
        bool VerifyDirIsDisplayed(ColumnName columnName, string columnValue);

        void ClickBtn_Search();

        void ClickBtn_ExportToExcel();

        void ClickBtn_ViewHide();

        void EnterText_InspectionBy(string inspectedByName);

        void SelectDDList_Location<T>(T ddListSelection);

        void SelectDDList_OpenClosed(OpenClosedStatus OpenOrClosed);

        void SelectDDList_Feature<T>(T ddListSelection);
    }

    public class InspectionDeficiencyLogReport_Impl : TestBase, IInspectionDeficiencyLogReport
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Tenant'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        public IInspectionDeficiencyLogReport SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IInspectionDeficiencyLogReport instance = new InspectionDeficiencyLogReport(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_SGWay instance ###### ");
                instance = new InspectionDeficiencyLogReport_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_SH249 instance ###### ");
                instance = new InspectionDeficiencyLogReport_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_Garnet instance ###### ");
                instance = new InspectionDeficiencyLogReport_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_GLX instance ###### ");
                instance = new InspectionDeficiencyLogReport_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_I15South instance ###### ");
                instance = new InspectionDeficiencyLogReport_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_I15Tech instance ###### ");
                instance = new InspectionDeficiencyLogReport_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_LAX instance ###### ");
                instance = new InspectionDeficiencyLogReport_LAX(driver);
            }
            return instance;
        }

        public virtual bool VerifyDirIsDisplayed(ColumnName columnName, string columnValue)
        {
            bool isDisplayed = false;

            try
            {
                columnValue = (columnName == ColumnName.DIR_No) || (columnName == ColumnName.Closed_Dir)
                    ? $"{columnValue}-1"
                    : columnValue;

                isDisplayed = GridHelper.VerifyRecordIsDisplayed(columnName, columnValue, TableType.Single, false, FilterOperator.Contains);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            return isDisplayed;
        }

        public virtual void ClickBtn_ViewHide() => JsClickElement(By.LinkText("View / Hide"));

        public virtual void ClickBtn_ExportToExcel() => JsClickElement(By.XPath("//button[text()='Export to Excel']"));

        public virtual void ClickBtn_Search() => JsClickElement(By.XPath("//button[@id='SearchForTests']"));

        public virtual void EnterText_InspectionBy(string inspectedByName)
            => EnterText(GetTextInputFieldByLocator(InputFields.Inspection_By), inspectedByName);

        public virtual void SelectDDList_Location<T>(T ddListSelection)
            => ExpandAndSelectFromDDList(InputFields.Location, ddListSelection);

        public virtual void SelectDDList_OpenClosed(OpenClosedStatus OpenOrClosed)
            => ExpandAndSelectFromDDList(InputFields.Closed, (int)OpenOrClosed);

        public virtual void SelectDDList_Feature<T>(T ddListSelection)
            => ExpandAndSelectFromDDList(InputFields.Feature, ddListSelection);
    }

    public class InspectionDeficiencyLogReport_Garnet : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    public class InspectionDeficiencyLogReport_GLX : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    public class InspectionDeficiencyLogReport_SH249 : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    public class InspectionDeficiencyLogReport_SGWay : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    public class InspectionDeficiencyLogReport_I15South : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    public class InspectionDeficiencyLogReport_I15Tech : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    public class InspectionDeficiencyLogReport_LAX : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}