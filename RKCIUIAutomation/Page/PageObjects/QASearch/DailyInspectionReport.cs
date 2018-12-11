using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using static RKCIUIAutomation.Page.PageObjects.QASearch.DailyInspectionReport;
using static RKCIUIAutomation.Page.TableHelper;

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
            [StringValue("DIRNO")] DIR_No,
            [StringValue("Revision")] Revision,
            [StringValue("InspectDate")] Inspection_Date,
            [StringValue("InspectBy")] Inspection_By,
            [StringValue("Comments")] Workflow_Location,
            [StringValue("LabField")] Belong_To,
            [StringValue("Area")] Area,
            [StringValue("SpecSection")] Spec_Section,
            [StringValue("Reviewby")] Reviewed_By,
            [StringValue("ApproveBy")] Approved_By,
            [StringValue("ApproveDate")] Approve_Date
        }

        public enum SearchCriteria
        {
            //text fields
            [StringValue("FromDate")] SamplePeriod_FromDate,

            [StringValue("ToDate")] SamplePeriod_ToDate,
            [StringValue("InspectBy")] Inspection_By,
            [StringValue("LIN")] TINs,
            [StringValue("DirNo")] DIR_No,
            [StringValue("ConversationLog")] Conversation_Log,
            [StringValue("AssociateReports")] List_of_previous_FDC_CDR_NCR,

            //DDList
            [StringValue("FID")] FID,

            [StringValue("SegmentId")] Segment,
            [StringValue("DivisionId")] Division,
            [StringValue("BidCodeItemId")] Bid_Item_Code, //dependant on Division DDList selection
            [StringValue("RoadWayId")] Roadway,
            [StringValue("StructureId")] Location,
            [StringValue("FeatureTypeId")] Feature,
            [StringValue("TypeId")] Type,
            [StringValue("ResultId")] Result,
            [StringValue("PreviousIssuesResolved")] Resolved,
            [StringValue("Deficiency")] Any_Deficiencies,
            [StringValue("ReviewById")] Review_By,
            [StringValue("StatusFlowId")] Workflow_Location,
            [StringValue("Source")] Belong_To,

            //Add Query portion
            [StringValue("Name")] AddQuery_Name, //input[@name='Name']

            [StringValue("Description")] AddQuery_Description, //input[@name='Description']
            [StringValue("AllowAccessToAllUsers")] AddQuery_AccessLevel
        }

        public enum Buttons
        {
            [StringValue("ViewSelectedDIR_")] View_Selected_DIR,
            [StringValue("SearchForTests")] Search,
            [StringValue("ClearButton")] Clear,
            [StringValue("")] Add_Query, //TODO - anchor tag - create method
            [StringValue("")] My_Queries, //TODO - button @data-target='#testing-queries-modal'

            //Add Query portion
            [StringValue("AddQueryButton")] AddQuery_Save,

            [StringValue("CancelButton")] AddQuery_Cancel,
        }
    }

    public interface IDailyInspectionReport
    {
        bool VerifyDirIsDisplayed(string dirNumber);

        bool VerifyDirWorkflowLocationBySearch(string dirNumber, WorkflowLocation expectedWorkflow = WorkflowLocation.Closed, bool dirRevision = false, string expectedRevision = "B");

        bool VerifyDirWorkflowLocationByTblFilter(string dirNumber, WorkflowLocation expectedWorkflow = WorkflowLocation.Closed, bool dirRevision = false, string expectedRevision = "B");

        void ClickBtn_Search();

        void ClickBtn_Clear();

        void EnterText_DIR_Number(string dirNumber);
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

        public virtual void ClickBtn_Search() => JsClickElement(By.Id(Buttons.Search.GetString()));

        public virtual void ClickBtn_Clear() => JsClickElement(By.Id(Buttons.Clear.GetString()));

        public virtual void EnterText_DIR_Number(string dirNumber) => EnterText(By.Id(SearchCriteria.DIR_No.GetString()), dirNumber);

        public virtual bool VerifyDirIsDisplayed(string dirNumber)
        {
            bool isDisplayed = false;

            try
            {
                isDisplayed = VerifyRecordIsDisplayed(ColumnName.DIR_No, dirNumber, TableType.Single);
                string logMsg = isDisplayed ? "Found" : "Unable to find";
                LogInfo($"{logMsg} DIR record number {dirNumber}.", isDisplayed);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return isDisplayed;
        }

        private bool CheckIfWorkflowLocationsMatch(string dirNumber, WorkflowLocation expectedWorkflow, bool usingSearch = false, bool dirRevision = false, string expectedRevision = "B")
        {
            NavigateToPage.QASearch_Daily_Inspection_Report();
            string actualWorkflow = string.Empty;
            bool wfLocationsMatch = false;

            try
            {
                dirNumber = dirNumber.Equals("") ? QaRcrdCtrl_QaDIR.GetDirNumber() : dirNumber;

                if (usingSearch)
                {
                    QaSearch_DIR.EnterText_DIR_Number(dirNumber);
                    QaSearch_DIR.ClickBtn_Search();
                }

                bool isDisplayed = QaSearch_DIR.VerifyDirIsDisplayed(dirNumber);

                if (isDisplayed && dirRevision)
                {
                    isDisplayed = QaSearch_DIR.VerifyDirIsDisplayed(expectedRevision);
                }

                if (isDisplayed)
                {
                    actualWorkflow = GetColumnValueForRow(dirNumber, "Workflow Location", false);
                    string logMsg = string.IsNullOrEmpty(actualWorkflow) ? "not found." : $"Workflow Location displayed as: {actualWorkflow}";
                    wfLocationsMatch = actualWorkflow.Equals(expectedWorkflow.GetString()) ? true : false;
                    LogInfo($"DIR Number {dirNumber}, {logMsg}", wfLocationsMatch);
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return wfLocationsMatch;
        }

        public virtual bool VerifyDirWorkflowLocationBySearch(string dirNumber, WorkflowLocation expectedWorkflow = WorkflowLocation.Closed, bool dirRevision = false, string expectedRevision = "B")
            => CheckIfWorkflowLocationsMatch(dirNumber, expectedWorkflow, true, dirRevision, expectedRevision);

        public virtual bool VerifyDirWorkflowLocationByTblFilter(string dirNumber, WorkflowLocation expectedWorkflow = WorkflowLocation.Closed, bool dirRevision = false, string expectedRevision = "B")
            => CheckIfWorkflowLocationsMatch(dirNumber, expectedWorkflow, dirRevision, false, expectedRevision);
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