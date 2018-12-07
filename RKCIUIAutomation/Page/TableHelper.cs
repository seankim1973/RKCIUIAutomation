using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace RKCIUIAutomation.Page
{
    public class TableHelper : Action
    {
        public TableHelper()
        {
        }

        public TableHelper(IWebDriver driver) => this.Driver = driver;

        private KendoGrid kendo;
        private KendoGrid Kendo => kendo = new KendoGrid(Driver);

        #region Kendo Grid Public Methods

        public void ClickCommentTabNumber(int commentNumber) => Kendo.ClickCommentTab(commentNumber);

        public void ClickTab(Enum tblTabEnum) => Kendo.ClickTableTab(tblTabEnum.GetString());

        public void ClickTab(string tblTabName) => Kendo.ClickTableTab(tblTabName);

        public void RefreshTable() => Kendo.Reload();

        public int GetTableRowCount() => Kendo.TotalNumberRows();

        //<<-- Table Page Navigation Helpers -->>
        private By GetGoToTblPgBtn_ByLocator(TableButton tblPageNavBtn) => By.XPath($"//a[contains(@aria-label,'{tblPageNavBtn.GetString()}')]");

        public void GoToFirstPage() => JsClickElement(GetGoToTblPgBtn_ByLocator(TableButton.First));

        public void GoToPreviousPage() => JsClickElement(GetGoToTblPgBtn_ByLocator(TableButton.Previous));

        public void GoToNextPage() => JsClickElement(GetGoToTblPgBtn_ByLocator(TableButton.Next));

        public void GoToLastPage() => JsClickElement(GetGoToTblPgBtn_ByLocator(TableButton.Last));

        public int GetCurrentPageNumber() => Kendo.GetCurrentPageNumber();

        public void GoToPageNumber(int pageNumber) => Kendo.GoToTablePage(pageNumber);

        public int GetCurrentViewItemsPerPageSize() => Kendo.GetPageSize();

        public void SetViewItemsPerPageSize(int newSize) => Kendo.ChangePageSize(newSize);

        /// <summary>
        /// ColumnName enumerator and FilterValue is required. FilterOperator has a default value of 'Equal To'.
        /// The default value for FilerLogic is 'AND' and Additional FilterOperator 'Equal To'.
        /// When filtering the column using an additional filter value, the FilterLogic must be specified ('AND' or 'OR'), but the Additional FilterOperator is optional.
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="filterValue"></param>
        /// <param name="filterOperator"></param>
        /// <param name="filterLogic"></param>
        /// <param name="additionalFilterValue"></param>
        /// <param name="additionalFilterOperator"></param>
        public void FilterColumn(Enum columnName, string filterValue, FilterOperator filterOperator = FilterOperator.EqualTo, FilterLogic filterLogic = FilterLogic.And, string additionalFilterValue = null, FilterOperator additionalFilterOperator = FilterOperator.EqualTo)
        {
            try
            {
                Kendo.FilterTableGrid(columnName.GetString(), filterValue, filterOperator, filterLogic, additionalFilterValue, additionalFilterOperator);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public void ClearTableFilters() => Kendo.RemoveFilters();

        public void SortColumnAscending(Enum columnName) => Kendo.Sort(columnName.GetString(), SortType.Ascending);

        public void SortColumnDescending(Enum columnName) => Kendo.Sort(columnName.GetString(), SortType.Descending);

        public void SortColumnToDefault(Enum columnName) => Kendo.Sort(columnName.GetString(), SortType.Default);

        #endregion Kendo Grid Public Methods

        #region Table Row Button Methods

        //<<-- Table Row Button Helpers -->>
        private class BtnCategory
        {
            internal const string LastOrOnlyInRow = "LastOrOnlyInRow";
            internal const string MultiDupsInRow = "MultiDupsInRow";
            internal const string ActionColumnBtn = "ActionColumnBtn";
            internal const string RowEndsWithChkbx = "RowEndsWithChkbx";
        }

        private enum TableButton
        {
            [StringValue("/input", BtnCategory.LastOrOnlyInRow)] CheckBox,
            [StringValue("/a", BtnCategory.LastOrOnlyInRow)] QMS_Attachments_View,
            [StringValue("-1", BtnCategory.MultiDupsInRow)] Report_View,
            [StringValue("-2", BtnCategory.MultiDupsInRow)] WebForm_View,
            [StringValue("-3", BtnCategory.MultiDupsInRow)] Attachments_View,
            [StringValue("Revise", BtnCategory.ActionColumnBtn)] Action_Revise,
            [StringValue("Enter", BtnCategory.ActionColumnBtn)] Action_Enter,
            [StringValue("Delete", BtnCategory.ActionColumnBtn)] Action_Delete,
            [StringValue("Edit", BtnCategory.ActionColumnBtn)] Action_Edit,
            [StringValue("Close DIR", BtnCategory.ActionColumnBtn)] Action_Close_DIR,
            [StringValue("first")] First,
            [StringValue("previous")] Previous,
            [StringValue("next")] Next,
            [StringValue("last")] Last
        }

        public enum TimeBlock
        {
            AM_12_00, AM_12_30, AM_01_00, AM_01_30, AM_02_00, AM_02_30,
            AM_03_00, AM_03_30, AM_04_00, AM_04_30, AM_05_00, AM_05_30,
            AM_06_00, AM_06_30, AM_07_00, AM_07_30, AM_08_00, AM_08_30,
            AM_09_00, AM_09_30, AM_10_00, AM_10_30, AM_11_00, AM_11_30,
            PM_12_00, PM_12_30, PM_01_00, PM_01_30, PM_02_00, PM_02_30,
            PM_03_00, PM_03_30, PM_04_00, PM_04_30, PM_05_00, PM_05_30,
            PM_06_00, PM_06_30, PM_07_00, PM_07_30, PM_08_00, PM_08_30,
            PM_09_00, PM_09_30, PM_10_00, PM_10_30, PM_11_00, PM_11_30
        }

        public enum WorkflowLocation
        {
            [StringValue("Attachment")] Attachment,
            [StringValue("Closing")] Closing,
            [StringValue("Closed")] Closed,
            [StringValue("Creating")] Creating,
            [StringValue("Packaged")] Packaged,
            [StringValue("QcReview")] QcReview,
            [StringValue("Revise")] Revise
        }

        private string DetermineTblRowBtnXPathExt(TableButton tblBtn, bool rowEndsWithChkbx = false)
        {
            string xPathExt = string.Empty;
            string xPathLast(string value = "") => $"[last(){value}]";

            string xPathExtValue = tblBtn.GetString();
            string btnCategory = tblBtn.GetString(true);

            switch (btnCategory)
            {
                case BtnCategory.LastOrOnlyInRow:
                    xPathExt = $"{xPathLast()}{xPathExtValue}";
                    break;

                case BtnCategory.MultiDupsInRow:
                    xPathExt = $"{xPathLast(xPathExtValue)}/a";
                    break;

                case BtnCategory.ActionColumnBtn:
                    string xPathExtRowType = rowEndsWithChkbx ? "" : $"{xPathLast()}";
                    xPathExt = $"{xPathExtRowType}/a[contains(text(),'{xPathExtValue}')]";
                    break;

                default:
                    xPathExt = xPathExtValue;
                    break;
            }
            return xPathExt;
        }

        private string GetGridTypeXPath(bool isMultiTabGrid)
            => isMultiTabGrid ? "//div[@class='k-content k-state-active']" : "//div[@data-role='grid']";

        //private readonly string ActiveTableDiv = "//div[@class='k-content k-state-active']";

        private string TableByTextInRow(string textInRowForAnyColumn)
            => $"//td[text()='{textInRowForAnyColumn}']/parent::tr/td";

        private string TableColumnIndex(string columnName)
            => $"//th[@data-title='{columnName}']";

        private string SetXPath_TableRowBaseByTextInRow(string textInRowForAnyColumn)
            => textInRowForAnyColumn.Equals("") ? "//tr[1]/td" : $"{TableByTextInRow(textInRowForAnyColumn)}";

        private By GetTblRowBtn_ByLocator(TableButton tblRowBtn, string textInRowForAnyColumn, bool isMultiTabGrid = true, bool rowEndsWithChkbx = false)
            => By.XPath($"{GetGridTypeXPath(isMultiTabGrid)}{SetXPath_TableRowBaseByTextInRow(textInRowForAnyColumn)}{DetermineTblRowBtnXPathExt(tblRowBtn, rowEndsWithChkbx)}");

        public By GetTableRowLocator(string textInRowForAnyColumn, bool isMultiTabGrid)
            => By.XPath($"{GetGridTypeXPath(isMultiTabGrid)}{TableByTextInRow(textInRowForAnyColumn)}");

        public string GetColumnValueForRow(string textInRowForAnyColumn, string columnName, bool isMultiTabGrid = true)
        {
            string rowXPath = string.Empty;

            try
            {
                By headerLocator = By.XPath($"{GetGridTypeXPath(isMultiTabGrid)}{TableColumnIndex(columnName)}");
                string dataIndexAttribute = GetElement(headerLocator).GetAttribute("data-index");
                int xPathIndex = int.Parse(dataIndexAttribute) + 1;
                rowXPath = $"{TableByTextInRow(textInRowForAnyColumn)}[{xPathIndex.ToString()}]";
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            string text = GetText(By.XPath(rowXPath));
            return text;
        }

        private void ClickButtonForRow(TableButton tableButton, string textInRowForAnyColumn = "", bool isMultiTabGrid = true, bool rowEndsWithChkbox = false)
        {
            try
            {
                string[] logBtnType = tableButton.Equals(TableButton.CheckBox)
                    ? new string[] { "Toggled", "checkbox" }
                    : new string[] { "Clicked", "button" };
                JsClickElement(GetTblRowBtn_ByLocator(tableButton, textInRowForAnyColumn, isMultiTabGrid, rowEndsWithChkbox));
                log.Debug($"{logBtnType[0]} {tableButton.ToString()} {logBtnType[1]} for row {textInRowForAnyColumn}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        //<<-- Table Row Button Public Methods -->>
        /// <summary>
        /// If no argument is provided, the checkbox for the first row will be selected.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ToggleCheckBoxForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.CheckBox, textInRowForAnyColumn, isMultiTabGrid);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickDeleteBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.Action_Delete, textInRowForAnyColumn, isMultiTabGrid);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickEditBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true, bool rowEndsWithChkbox = false)
            => ClickButtonForRow(TableButton.Action_Edit, textInRowForAnyColumn, isMultiTabGrid, rowEndsWithChkbox);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        public void ClickCloseDirBtnForRow(string dirNumber = "", bool isMultiTabGrid = true, bool rowEndsWithChkbox = true)
            => ClickButtonForRow(TableButton.Action_Close_DIR, dirNumber, isMultiTabGrid, rowEndsWithChkbox);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickReviseBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.Action_Revise, textInRowForAnyColumn, isMultiTabGrid);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickEnterBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.Action_Enter, textInRowForAnyColumn, isMultiTabGrid);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickQMSViewAttachmentsForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.QMS_Attachments_View, textInRowForAnyColumn, isMultiTabGrid);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickViewAttachmentsForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.Attachments_View, textInRowForAnyColumn, isMultiTabGrid);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickViewWebFormForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.WebForm_View, textInRowForAnyColumn, isMultiTabGrid);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickViewReportForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.Report_View, textInRowForAnyColumn, isMultiTabGrid);

        #endregion Table Row Button Methods

        public void FilterTableColumnByValue(Enum columnName, string recordNameOrNumber)
        {
            try
            {
                WaitForPageReady();
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
            finally
            {
                FilterColumn(columnName, recordNameOrNumber);
            }
        }

        public bool VerifyRecordIsDisplayed(Enum columnName, string recordNameOrNumber, bool noRecordsExpected = false)
        {
            IList<IWebElement> tblRowElems = new List<IWebElement>();
            bool isMultiTabGrid = false;
            bool isDisplayedAsExpected = false;
            bool noRecordsMsgDisplayed = false;
            string currentTabName = string.Empty;
            string logMsg = string.Empty;
            string activeTblTab = "";
            int tblRowCount = 0;
            
            try
            {
                FilterTableColumnByValue(columnName, recordNameOrNumber);

                string gridId = kendo.GetGridID();
                By gridParentDivLocator = By.XPath($"//div[@id='{gridId}']/parent::div/parent::div/parent::div");
                string gridType = GetAttribute(gridParentDivLocator, "class");
                isMultiTabGrid = gridType.Contains("active") ? true : false;
                By recordRowLocator = GetTableRowLocator(recordNameOrNumber, isMultiTabGrid);
                

                if (isMultiTabGrid)
                {
                    currentTabName = GetText(By.XPath("//li[contains(@class, 'k-state-active')]/span[@class='k-link']"));
                    activeTblTab = "//div[@class='k-content k-state-active']";
                }

                By noRecordsMsgLocator = By.XPath($"{activeTblTab}//div[@class='k-grid-norecords']");

                if (noRecordsExpected)
                {
                    noRecordsMsgDisplayed = ElementIsDisplayed(noRecordsMsgLocator);

                    if (noRecordsMsgDisplayed)
                    {
                        isDisplayedAsExpected = false;
                        logMsg = "'No Records Located' message is displayed as expected";
                    }
                    else
                    {
                        tblRowElems = GetElements(By.XPath($"{activeTblTab}//tbody[@role='rowgroup']/tr"));
                        tblRowCount = (int)tblRowElems?.Count;

                        if (tblRowCount > 0)
                        {
                            var recordRowDisplayed = ElementIsDisplayed(recordRowLocator);
                            if (recordRowDisplayed)
                            {
                                logMsg = $"No Records are Expected, but found {tblRowCount} record";
                            }
                        }
                        else
                        {
                            logMsg = "No Records are Expected, No Row are found in the table, and 'No Records Located' message is not displayed";
                        }
                    }
                }
                else
                {
                    tblRowElems = GetElements(By.XPath($"{activeTblTab}//tbody[@role='rowgroup']/tr"));
                    tblRowCount = (int)tblRowElems?.Count;

                    if (tblRowCount > 0)
                    {
                        LogDebug($"Searching for record: {recordNameOrNumber}");
                        isDisplayedAsExpected = ElementIsDisplayed(recordRowLocator);

                        if (isDisplayedAsExpected)
                        {
                            logMsg = $"Found Record {recordNameOrNumber} as Expected";
                        }
                        else
                        {
                            logMsg = $"Expected Record, but Unable to find Record {recordNameOrNumber}";
                        }
                    }
                    else
                    {
                        noRecordsMsgDisplayed = ElementIsDisplayed(noRecordsMsgLocator);
                        if (noRecordsMsgDisplayed)
                        {
                            logMsg = "Expected Record, but 'No Record Located' message is displayed";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            LogInfo(logMsg, noRecordsExpected?!isDisplayedAsExpected:isDisplayedAsExpected);
            return isDisplayedAsExpected;
        }

        //TODO: Horizontal scroll in table (i.e. QA Search>ProctorCurveSummary)
    }
}