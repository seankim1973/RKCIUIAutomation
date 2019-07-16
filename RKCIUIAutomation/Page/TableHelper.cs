using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page
{
    public class TableHelper : TableHelper_Impl
    {
        public TableHelper()
        {
        }

        public TableHelper(IWebDriver driver) => this.Driver = driver;

        private static IKendoGrid Kendo => new KendoGrid(driver);

        #region Kendo Grid public override Methods

        //public override void ClickCommentTabNumber(int commentNumber) => Kendo.ClickCommentTab(commentNumber);

        public override void ClickTab(Enum tblTabEnum) => Kendo.ClickTableTab(tblTabEnum.GetString());

        public override void ClickTab(string tblTabName) => Kendo.ClickTableTab(tblTabName);

        public override void RefreshTable() => Kendo.Reload();

        public override int GetTableRowCount(bool isMultiTabGrid = true)
            => PageAction.GetElementsCount(By.XPath($"{GetGridTypeXPath(isMultiTabGrid)}//tbody/tr"));

        //<<-- Table Page Navigation Helpers -->>
        public override By GetGoToTblPgBtn_ByLocator(TableButton tblPageNavBtn)
            => By.XPath($"//a[contains(@aria-label,'{tblPageNavBtn.GetString()}')]");

        public override void GoToFirstPage() => PageAction.JsClickElement(GetGoToTblPgBtn_ByLocator(TableButton.First));

        public override void GoToPreviousPage() => PageAction.JsClickElement(GetGoToTblPgBtn_ByLocator(TableButton.Previous));

        public override void GoToNextPage() => PageAction.JsClickElement(GetGoToTblPgBtn_ByLocator(TableButton.Next));

        public override void GoToLastPage() => PageAction.JsClickElement(GetGoToTblPgBtn_ByLocator(TableButton.Last));

        public override int GetCurrentPageNumber() => Kendo.GetCurrentPageNumber();

        public override void GoToPageNumber(int pageNumber) => Kendo.GoToTablePage(pageNumber);

        public override int GetCurrentViewItemsPerPageSize() => Kendo.GetPageSize();

        public override void SetViewItemsPerPageSize(int newSize) => Kendo.ChangePageSize(newSize);

        /// <summary>
        /// ColumnName enumerator and FilterValue is required.
        /// <para>Default Values: FilterOperator is 'Equal To', FilerLogic is 'AND' and Additional FilterOperator is 'Equal To'.</para>
        /// <para>When filtering the column using an additional filter value, the FilterLogic must be specified ('AND' or 'OR'), but the Additional FilterOperator is optional.</para>
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="filterValue"></param>
        /// <param name="filterOperator"></param>
        /// <param name="filterLogic"></param>
        /// <param name="additionalFilterValue"></param>
        /// <param name="additionalFilterOperator"></param>
        public override void FilterColumn(Enum columnName, string filterValue, TableType tableType = TableType.Unknown, FilterOperator filterOperator = FilterOperator.EqualTo, FilterLogic filterLogic = FilterLogic.And, string additionalFilterValue = null, FilterOperator additionalFilterOperator = FilterOperator.EqualTo)
            => Kendo.FilterTableGrid(columnName.GetString(), filterValue, filterOperator, filterLogic, additionalFilterValue, additionalFilterOperator, tableType);

        public override void ClearTableFilters(TableType tableType = TableType.Unknown)
            => Kendo.RemoveFilters(tableType);

        public override void SortColumnAscending(Enum columnName, TableType tableType = TableType.Unknown)
            => Kendo.Sort(columnName.GetString(), SortType.Ascending, tableType);

        public override void SortColumnDescending(Enum columnName, TableType tableType = TableType.Unknown)
            => Kendo.Sort(columnName.GetString(), SortType.Descending, tableType);

        public override void SortColumnToDefault(Enum columnName, TableType tableType = TableType.Unknown)
            => Kendo.Sort(columnName.GetString(), SortType.Default, tableType);

        #endregion Kendo Grid public override Methods

        #region Table Row Button Methods

        //<<-- Table Row Button Helpers -->>
        public class BtnCategory
        {
            internal const string LastOrOnlyInRow = "LastOrOnlyInRow";
            internal const string MultiDupsInRow = "MultiDupsInRow";
            internal const string ActionColumnBtn = "ActionColumnBtn";
            internal const string RowEndsWithChkbx = "RowEndsWithChkbx";
            internal const string Packages = "Package";
            internal const string Download = "Download";
        }

        public enum TableButton
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
            [StringValue("Create", BtnCategory.Packages)] Create_Package,
            [StringValue("Recreate", BtnCategory.Packages)] Recreate_Package,
            [StringValue("/a[contains(@onclick, 'download')]", BtnCategory.Download)] Download,
            [StringValue("/a", BtnCategory.LastOrOnlyInRow)] View,
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

        public enum TableType
        {
            Unknown,
            MultiTab,
            Single
        }

        public override string DetermineTblRowBtnXPathExt(TableButton tblBtn, bool rowEndsWithChkbx = false)
        {
            string xPathExtRowType = string.Empty;
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
                    xPathExtValue = rowEndsWithChkbx 
                        ? (int.Parse(xPathExtValue) - 1).ToString()
                        : xPathExtValue;
                    xPathExt = $"{xPathLast(xPathExtValue)}/a";
                    break;

                case BtnCategory.ActionColumnBtn:
                    xPathExtRowType = rowEndsWithChkbx
                        ? ""
                        : $"{xPathLast()}";
                    xPathExt = $"{xPathExtRowType}/a[contains(text(),'{xPathExtValue}')]";
                    break;

                case BtnCategory.Packages:
                    xPathExtRowType = rowEndsWithChkbx
                        ? ""
                        : $"{xPathLast()}";
                    xPathExt = $"{xPathExtRowType}/a[text()='{xPathExtValue}']";
                    break;

                default:
                    xPathExt = xPathExtValue;
                    break;
            }
            return xPathExt;
        }

        public override string GetGridTypeXPath(bool isMultiTabGrid)
            => isMultiTabGrid 
                ? "//div[@class='k-content k-state-active']"
                : "//div[@data-role='grid']";

        public override string GetXPathForTblRowBasedOnTextInRowOrRowIndex<T>(T textInRowForAnyColumnOrRowIndex, bool useContainsOperator = false)
        {
            object argObj = null;
            string xpath = string.Empty;
            Type argType = textInRowForAnyColumnOrRowIndex.GetType();

            if (argType == typeof(int))
            {
                argObj = BaseUtil.ConvertToType<int>(textInRowForAnyColumnOrRowIndex);
                xpath = $"//tr[{argObj.ToString()}]/td//parent::tr/td";
            }
            else if (argType == typeof(string))
            {
                argObj = BaseUtil.ConvertToType<string>(textInRowForAnyColumnOrRowIndex);
                xpath = ((string)argObj).HasValue()
                    ? useContainsOperator
                        ? $"//td[text()='{argObj}']/parent::tr/td"
                        : $"//td[contains(text(),'{argObj}')]/parent::tr/td"
                    : "//tr[1]/td";
            }

            return xpath;
        }

        public override By GetTblRowBtn_ByLocator<T>(TableButton tblRowBtn, T textInRowForAnyColumnOrRowIndex, bool isMultiTabGrid = true, bool rowEndsWithChkbx = false)
            => By.XPath($"{GetGridTypeXPath(isMultiTabGrid)}{GetXPathForTblRowBasedOnTextInRowOrRowIndex(textInRowForAnyColumnOrRowIndex)}{DetermineTblRowBtnXPathExt(tblRowBtn, rowEndsWithChkbx)}");

        public override By GetTblRowElementByLocator(string textInRowForAnyColumn, bool isMultiTabGrid, bool useContainsOperator = false)
            => By.XPath($"{GetGridTypeXPath(isMultiTabGrid)}{GetXPathForTblRowBasedOnTextInRowOrRowIndex(textInRowForAnyColumn)}");

        public override string GetColumnValueForRow<T, C>(T textInRowForAnyColumnOrRowIndex, C getValueFromColumnName, bool isMultiTabGrid = true)
        {
            object cArgObj = null;
            string colValue = string.Empty;
            string rowXPath = string.Empty;
            string columnDataTypeXPath = string.Empty;

            Type cArgType = getValueFromColumnName.GetType();

            try
            {
                string gridTypeXPath = $"{GetGridTypeXPath(isMultiTabGrid)}";

                if (getValueFromColumnName is string)
                {
                    cArgObj = BaseUtil.ConvertToType<string>(getValueFromColumnName);
                    columnDataTypeXPath = $"//th[@data-title='{(string)cArgObj}']";
                }
                else if (getValueFromColumnName is Enum)
                {
                    cArgObj = BaseUtil.ConvertToType<Enum>(getValueFromColumnName);
                    string columnId = ((Enum)cArgObj).GetString();
                    columnDataTypeXPath = $"//th[@data-field='{columnId}']";
                }
          
                By headerLocator = By.XPath($"{gridTypeXPath}{columnDataTypeXPath}");
                string dataIndex = PageAction.GetAttribute(headerLocator, "data-index");
                int xPathIndex = int.Parse(dataIndex) + 1;

                rowXPath = $"{gridTypeXPath}{GetXPathForTblRowBasedOnTextInRowOrRowIndex(textInRowForAnyColumnOrRowIndex)}[{xPathIndex.ToString()}]";
                colValue = PageAction.GetText(By.XPath(rowXPath));
            }
            catch (Exception)
            {
                throw;
            }

            return colValue;
        }

        public override void ClickButtonForRow<T>(TableButton tableButton, T textInRowForAnyColumnOrRowIndex, bool isMultiTabGrid = true, bool rowEndsWithChkbox = false)
        {
            string[] logMsgBasedOnBtnType = null;

            try
            {
                if (tableButton.Equals(TableButton.CheckBox))
                {
                    logMsgBasedOnBtnType = new string[] { "Toggled", "checkbox"};
                }
                else
                {
                    logMsgBasedOnBtnType = new string[] { "Clicked", "button" };
                }
                
                By locator = GetTblRowBtn_ByLocator(tableButton, textInRowForAnyColumnOrRowIndex, isMultiTabGrid, rowEndsWithChkbox);
                PageAction.JsClickElement(locator);
                Report.Step($"{logMsgBasedOnBtnType[0]} {tableButton} {logMsgBasedOnBtnType[1]} for row {textInRowForAnyColumnOrRowIndex}");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                PageAction.WaitForPageReady();
            }
        }

        public override By GetTableBtnLocator<T>(TableButton tableButton, T textInRowForAnyColumnOrRowIndex, bool isMultiTabGrid = true, bool rowEndsWithChkbox = false)
            => GetTblRowBtn_ByLocator(tableButton, textInRowForAnyColumnOrRowIndex, isMultiTabGrid, rowEndsWithChkbox);

        //<<-- Table Row Button public override Methods -->>
        /// <summary>
        /// If no argument is provided, the checkbox for the first row will be selected.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public override void ToggleCheckBoxForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.CheckBox, textInRowForAnyColumn, isMultiTabGrid);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public override void ClickDeleteBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.Action_Delete, textInRowForAnyColumn, isMultiTabGrid);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public override void ClickEditBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true, bool rowEndsWithChkbox = false)
            => ClickButtonForRow(TableButton.Action_Edit, textInRowForAnyColumn, isMultiTabGrid, rowEndsWithChkbox);

        public override void ClickViewBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true, bool rowEndsWithChkbox = false)
            => ClickButtonForRow(TableButton.View, textInRowForAnyColumn, isMultiTabGrid, rowEndsWithChkbox);

        public override void ClickCreateBtnForRow(int textInRowForAnyColumn = 1, bool isMultiTabGrid = true)
        {
            try
            {
                ClickButtonForRow(TableButton.Create_Package, textInRowForAnyColumn, isMultiTabGrid, true);
                PageAction.AcceptAlertMessage();
                PageAction.AcceptAlertMessage();
            }
            catch (UnhandledAlertException ae)
            {
                log.Debug(ae.Message);
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }

        }

        public override void ClickRecreateBtnForRow(int textInRowForAnyColumn = 1, bool isMultiTabGrid = true)
        {
            try
            {
                ClickButtonForRow(TableButton.Recreate_Package, textInRowForAnyColumn, isMultiTabGrid, false);
                PageAction.AcceptAlertMessage();
                PageAction.AcceptAlertMessage();
            }
            catch (UnhandledAlertException ae)
            {
                log.Debug(ae.Message);
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }
        }

        public override void ClickDownloadBtnForRow(int rowIndex = 1, bool isMultiTabGrid = true, bool rowEndsWithChkbox = false)
            => ClickButtonForRow(TableButton.Download, rowIndex, isMultiTabGrid, rowEndsWithChkbox);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        public override void ClickCloseDirBtnForRow(string dirNumber = "", bool isMultiTabGrid = true, bool rowEndsWithChkbox = true)
        {
            try
            {
                ClickButtonForRow(TableButton.Action_Close_DIR, dirNumber, isMultiTabGrid, rowEndsWithChkbox);
                PageAction.AcceptAlertMessage();
                PageAction.AcceptAlertMessage();
            }
            catch (UnhandledAlertException ae)
            {
                log.Debug(ae.Message);
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }
        }

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public override void ClickReviseBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.Action_Revise, textInRowForAnyColumn, isMultiTabGrid);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public override void ClickEnterBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
        {
            ClickButtonForRow(TableButton.Action_Enter, textInRowForAnyColumn, isMultiTabGrid);
            PageAction.WaitForPageReady();
        }

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public override void ClickQMSViewAttachmentsForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.QMS_Attachments_View, textInRowForAnyColumn, isMultiTabGrid);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public override void ClickViewAttachmentsForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.Attachments_View, textInRowForAnyColumn, isMultiTabGrid);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public override void ClickViewWebFormForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.WebForm_View, textInRowForAnyColumn, isMultiTabGrid);

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked and return URL for PDF
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public override string ClickViewReportBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true, bool rowEndsWithChkbx = false)
        {
            string hrefPDF = GetPdfHref(textInRowForAnyColumn, isMultiTabGrid, rowEndsWithChkbx);
            ClickButtonForRow(TableButton.Report_View, textInRowForAnyColumn, isMultiTabGrid);
            return hrefPDF;
        }

        public override string GetPdfHref<T>(T textInColumnForRowOrRowIndex, bool isMultiTabGrid = true, bool rowEndsWithChkbx = false)
            => PageAction.GetAttribute(By.XPath($"{GetGridTypeXPath(isMultiTabGrid)}{GetXPathForTblRowBasedOnTextInRowOrRowIndex(textInColumnForRowOrRowIndex)}{DetermineTblRowBtnXPathExt(TableButton.Report_View, rowEndsWithChkbx)}"), "href");

        public override void SelectCheckboxForRow<T>(T textInRowForAnyColumnOrRowIndex, bool isMultiTabGrid = true)
            => ClickButtonForRow(TableButton.CheckBox, textInRowForAnyColumnOrRowIndex, isMultiTabGrid);
        
        #endregion Table Row Button Methods

        public override void FilterTableColumnByValue(Enum columnName, string recordNameOrNumber, TableType tableType = TableType.Unknown, FilterOperator filterOperator = FilterOperator.EqualTo)
        {
            try
            {
                PageAction.WaitForPageReady();
                FilterColumn(columnName, recordNameOrNumber, tableType, filterOperator);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override bool VerifyRecordIsDisplayed(Enum columnName, string recordNameOrNumber, TableType tableType = TableType.Unknown, bool noRecordsExpected = false, FilterOperator filterOperator = FilterOperator.EqualTo)
        {
            IList<IWebElement> tblRowElems = null;
            bool useContainsFilterOperator = false;
            bool isDisplayedAsExpected = false;
            //bool noRecordsMsgDisplayed = false;
            bool isMultiTabGrid = false;
            string currentTabName = string.Empty;
            string logMsg = string.Empty;
            string activeTblTab = "";
            By recordRowLocator = null;

            By noRecordsMsgLocator = By.XPath("//div[@class='k-grid-norecords']");
            By tableRowsLocator = By.XPath("//tbody[@role='rowgroup']/tr");

            try
            {
                FilterTableColumnByValue(columnName, recordNameOrNumber, tableType, filterOperator);
                PageAction.WaitForPageReady();

                if (tableType == TableType.Unknown)
                {
                    string gridId = Kendo.GetGridID(tableType);
                    By gridParentDivLocator = By.XPath($"//div[@id='{gridId}']/parent::div/parent::div/parent::div");
                    string gridType = PageAction.GetAttribute(gridParentDivLocator, "class");

                    if(gridType.Contains("active"))
                    {
                        isMultiTabGrid = true;
                    }
                }
                else
                {
                    switch (tableType)
                    {
                        case TableType.Single:
                            isMultiTabGrid = false;
                            break;
                        case TableType.MultiTab:
                            isMultiTabGrid = true;
                            //currentTabName = GetCurrentTableTabName();//GetText(By.XPath("//li[contains(@class, 'k-state-active')]/span[@class='k-link']"));
                            activeTblTab = "//div[@class='k-content k-state-active']";
                            //noRecordsMsgLocator = By.XPath($"{activeTblTab}//div[@class='k-grid-norecords']");
                            tableRowsLocator = By.XPath($"{activeTblTab}//tbody[@role='rowgroup']/tr");
                            break;
                    }
                }

                if (filterOperator == FilterOperator.Contains)
                {
                    useContainsFilterOperator = true;
                }

                string noRecordLocatedMsg = $"'No Records Located' message";

                if (noRecordsExpected)
                {
                    try
                    {
                        Report.Step($"{noRecordLocatedMsg} is expected");
                        VerifyNoRecordMessageIsDisplayed(tableType);
                        //noRecordsMsgDisplayed = PageAction.ElementIsDisplayed(noRecordsMsgLocator);
                        isDisplayedAsExpected = true;
                        logMsg = $"{noRecordLocatedMsg} is displayed in the grid as expected";
                    }
                    catch (NoSuchElementException)
                    {
                        logMsg = $"{noRecordLocatedMsg} is not displayed in the grid as expected";

                        try
                        {
                            recordRowLocator = GetTblRowElementByLocator(recordNameOrNumber, isMultiTabGrid, useContainsFilterOperator);
                            tblRowElems = PageAction.GetElements(tableRowsLocator, false);

                            if (tblRowElems.Any())
                            {
                                logMsg = $"{noRecordLocatedMsg} is expected, but grid contains data";
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            logMsg = $"Grid failed to load : {logMsg} and no data is found in the grid";
                        }

                        Report.Error(logMsg);
                    }
                }
                else
                {
                    try
                    {
                        Report.Step($"Searching for record: {recordNameOrNumber}");
                        recordRowLocator = GetTblRowElementByLocator(recordNameOrNumber, isMultiTabGrid, useContainsFilterOperator);
                        tblRowElems = PageAction.GetElements(tableRowsLocator, false);

                        if (tblRowElems.Any())
                        {
                            isDisplayedAsExpected = true;
                            logMsg = $"Found record after filtering grid by specified value: [{recordNameOrNumber}] in [{columnName}] column";
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        logMsg = $"Unable to find record after filtering grid by specified value: [{recordNameOrNumber}] in [{columnName}] column";

                        try
                        {
                            //noRecordsMsgDisplayed = PageAction.ElementIsDisplayed(noRecordsMsgLocator);
                            VerifyNoRecordMessageIsDisplayed(tableType);
                            logMsg = $"Expected record, but {noRecordLocatedMsg} is displayed in the grid";
                        }
                        catch (NoSuchElementException)
                        {
                            logMsg = $"Grid failed to load : {logMsg} and {noRecordLocatedMsg} is not displayed in the grid";
                        }

                        Report.Error(logMsg);
                    }
                }
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }

            Report.Info(logMsg, isDisplayedAsExpected);
            return isDisplayedAsExpected;
        }

        public override bool VerifyNoRecordMessageIsDisplayed(TableHelper.TableType tableType = TableHelper.TableType.Unknown)
        {
            IWebElement elem = null;
            By noRecordsMsgLocator = null;
            string noRecordsXPathString = "//div[@class='k-grid-norecords']";

            try
            {
                if (tableType == TableType.Unknown)
                {
                    string gridId = Kendo.GetGridID(tableType);
                    By gridParentDivLocator = By.XPath($"//div[@id='{gridId}']/parent::div/parent::div/parent::div");
                    string gridType = PageAction.GetAttribute(gridParentDivLocator, "class");

                    if (gridType.Contains("active"))
                    {
                        tableType = TableType.MultiTab;
                    }
                    else
                    {
                        tableType = TableType.Single;
                    }
                }

                switch (tableType)
                {
                    case TableType.Single:
                        noRecordsMsgLocator = By.XPath(noRecordsXPathString);
                        break;
                    case TableType.MultiTab:
                        string activeTblTabXPathString = "//div[@class='k-content k-state-active']";
                        noRecordsMsgLocator = By.XPath($"{activeTblTabXPathString}{noRecordsXPathString}");
                        break;
                }

                elem = PageAction.GetElement(noRecordsMsgLocator);
                return true;
            }
            catch(NoSuchElementException)
            {
                throw;
            }
            catch (Exception e)
            {
                Report.Error(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Verifies the URL of the PDF Report is valid.  If 'isMultiViewPDF' parameter is true, then 'expectedReportUrl' must be provided.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        /// <param name="isMultiViewPDF"></param>
        /// <param name="selectNoneForMultiView"></param>
        /// <param name="expectedReportUrl"></param>
        /// <returns></returns>
        public override bool VerifyViewPdfReport(string textInRowForAnyColumn = "", bool isMultiViewPDF = false, bool selectNoneForMultiView = false, string expectedReportUrl = "")
        {
            bool errorSearchResult = true;
            bool pdfTabUrlExpected = false;
            bool newTabDisplayed = false;
            string logMsg = string.Empty;
            string reportUrlLogMsg = string.Empty;
            string mainWindowHandle = string.Empty;
            string viewPdfWindowHandle = string.Empty;
            //string expectedReportUrl = string.Empty;
            string pageErrorHeadingText = string.Empty;
            string actualReportUrl = string.Empty;
            string pdfWindowTitle = string.Empty;

            try
            {
                expectedReportUrl = isMultiViewPDF
                    ? expectedReportUrl.HasValue()
                        ? expectedReportUrl
                        : "ERROR: Expected MultiView Report URL not provided"
                    : ClickViewReportBtnForRow(textInRowForAnyColumn, true, false);

                mainWindowHandle = driver.WindowHandles[0];
                viewPdfWindowHandle = driver.WindowHandles[1];
                newTabDisplayed = driver.WindowHandles.Count > 1 ? true : false;

                if (newTabDisplayed)
                {
                    int retryCount = 0;

                    do
                    {
                        Thread.Sleep(1000);

                        if (retryCount < 5)
                        {
                            driver.SwitchTo().Window(viewPdfWindowHandle);
                            actualReportUrl = driver.Url;
                            pdfWindowTitle = driver.Title;
                            retryCount++;
                        }
                    }
                    while (string.IsNullOrEmpty(actualReportUrl) || actualReportUrl.Contains("blank"));
                }

                if (!expectedReportUrl.Contains("ERROR:"))
                {
                    pdfTabUrlExpected = expectedReportUrl.Equals(actualReportUrl);

                    if (!pdfTabUrlExpected || pdfWindowTitle.Contains("Error") || pdfWindowTitle.Contains("Object reference"))
                    {
                        try
                        {
                            IWebElement headingElem = driver.FindElement(By.XPath("//h1")) ?? driver.FindElement(By.XPath("//h2"));

                            if (headingElem != null)
                            {
                                pageErrorHeadingText = headingElem?.Text;
                                logMsg = $"an error was found on the ViewDirPDF page: {pageErrorHeadingText}";
                                errorSearchResult = false;
                            }
                        }
                        catch (Exception)
                        {
                            logMsg = "Searched for Error page headings, but did not find any";
                        }

                        TestUtility.AddAssertionToList(errorSearchResult, "Verify ViewDirPDF page loaded successfully");
                    }
                }

                string logURLsMatch = $"Expected and Actual PDF Report URLs match<br>{actualReportUrl}";

                reportUrlLogMsg = pdfTabUrlExpected
                    ? errorSearchResult
                        ? logURLsMatch
                        : $"{logURLsMatch}<br>However {logMsg}"
                    : selectNoneForMultiView
                        ? newTabDisplayed
                            ? "No DIR Rows were selected for MultiView, but a new browser tab opened"
                            : "Change this message when bug is fixed - should show/check for a pop-up msg (Select a DIR Row)"
                        : $"Unexpected Actual PDF Report URL<br>Expected URL: {expectedReportUrl}<br>Actual URL: {actualReportUrl}";

                TestUtility.AddAssertionToList(pdfTabUrlExpected, "ViewDirPDF URL is as expected");
                bool[] results = new bool[]{pdfTabUrlExpected, errorSearchResult};
                Report.Info($"{reportUrlLogMsg}", results);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                if (newTabDisplayed)
                {
                    driver.SwitchTo().Window(viewPdfWindowHandle).Close();
                    driver.SwitchTo().Window(mainWindowHandle);
                }
            }

            return pdfTabUrlExpected;
        }

        public override string GetCurrentTableTabName()
            => PageAction.GetText(By.XPath("//li[contains(@class, 'k-state-active')]/span[@class='k-link']"));

        public override void ClickCommentTab(int commentNumber)
            => Kendo.ClickCommentTab(commentNumber);
        //TODO: Horizontal scroll in table (i.e. QA Search>ProctorCurveSummary)
    }

    public abstract class TableHelper_Impl : BaseUtils, ITableHelper
    {
        public abstract void ClearTableFilters(TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        public abstract void ClickButtonForRow<T>(TableHelper.TableButton tableButton, T textInRowForAnyColumnOrRowIndex, bool isMultiTabGrid = true, bool rowEndsWithChkbox = false);
        public abstract void ClickCloseDirBtnForRow(string dirNumber = "", bool isMultiTabGrid = true, bool rowEndsWithChkbox = true);
        public abstract void ClickCommentTab(int commentNumber);
        public abstract void ClickCreateBtnForRow(int textInRowForAnyColumn = 1, bool isMultiTabGrid = true);
        public abstract void ClickDeleteBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        public abstract void ClickDownloadBtnForRow(int rowIndex = 1, bool isMultiTabGrid = true, bool rowEndsWithChkbox = false);
        public abstract void ClickEditBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true, bool rowEndsWithChkbox = false);
        public abstract void ClickEnterBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        public abstract void ClickQMSViewAttachmentsForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        public abstract void ClickRecreateBtnForRow(int textInRowForAnyColumn = 1, bool isMultiTabGrid = true);
        public abstract void ClickReviseBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        public abstract void ClickTab(Enum tblTabEnum);
        public abstract void ClickTab(string tblTabName);
        public abstract void ClickViewAttachmentsForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        public abstract void ClickViewBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true, bool rowEndsWithChkbox = false);
        public abstract string ClickViewReportBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true, bool rowEndsWithChkbx = false);
        public abstract void ClickViewWebFormForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        public abstract string DetermineTblRowBtnXPathExt(TableHelper.TableButton tblBtn, bool rowEndsWithChkbx = false);
        public abstract void FilterColumn(Enum columnName, string filterValue, TableHelper.TableType tableType = TableHelper.TableType.Unknown, FilterOperator filterOperator = FilterOperator.EqualTo, FilterLogic filterLogic = FilterLogic.And, string additionalFilterValue = null, FilterOperator additionalFilterOperator = FilterOperator.EqualTo);
        public abstract void FilterTableColumnByValue(Enum columnName, string recordNameOrNumber, TableHelper.TableType tableType = TableHelper.TableType.Unknown, FilterOperator filterOperator = FilterOperator.EqualTo);
        public abstract string GetColumnValueForRow<T, C>(T textInRowForAnyColumnOrRowIndex, C getValueFromColumnName, bool isMultiTabGrid = true);
        public abstract int GetCurrentPageNumber();
        public abstract string GetCurrentTableTabName();
        public abstract int GetCurrentViewItemsPerPageSize();
        public abstract By GetGoToTblPgBtn_ByLocator(TableHelper.TableButton tblPageNavBtn);
        public abstract string GetGridTypeXPath(bool isMultiTabGrid);
        public abstract string GetPdfHref<T>(T textInColumnForRowOrRowIndex, bool isMultiTabGrid = true, bool rowEndsWithChkbx = false);
        public abstract By GetTableBtnLocator<T>(TableHelper.TableButton tableButton, T textInRowForAnyColumnOrRowIndex, bool isMultiTabGrid = true, bool rowEndsWithChkbox = false);
        public abstract int GetTableRowCount(bool isMultiTabGrid = true);
        public abstract By GetTblRowBtn_ByLocator<T>(TableHelper.TableButton tblRowBtn, T textInRowForAnyColumnOrRowIndex, bool isMultiTabGrid = true, bool rowEndsWithChkbx = false);
        public abstract By GetTblRowElementByLocator(string textInRowForAnyColumn, bool isMultiTabGrid, bool useContainsOperator = false);
        public abstract string GetXPathForTblRowBasedOnTextInRowOrRowIndex<T>(T textInRowForAnyColumnOrRowIndex, bool useContainsOperator = false);
        public abstract void GoToFirstPage();
        public abstract void GoToLastPage();
        public abstract void GoToNextPage();
        public abstract void GoToPageNumber(int pageNumber);
        public abstract void GoToPreviousPage();
        public abstract void RefreshTable();
        public abstract void SelectCheckboxForRow<T>(T textInRowForAnyColumnOrRowIndex, bool isMultiTabGrid = true);
        public abstract void SetViewItemsPerPageSize(int newSize);
        public abstract void SortColumnAscending(Enum columnName, TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        public abstract void SortColumnDescending(Enum columnName, TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        public abstract void SortColumnToDefault(Enum columnName, TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        public abstract void ToggleCheckBoxForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        public abstract bool VerifyRecordIsDisplayed(Enum columnName, string recordNameOrNumber, TableHelper.TableType tableType = TableHelper.TableType.Unknown, bool noRecordsExpected = false, FilterOperator filterOperator = FilterOperator.EqualTo);
        public abstract bool VerifyNoRecordMessageIsDisplayed(TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        public abstract bool VerifyViewPdfReport(string textInRowForAnyColumn = "", bool isMultiViewPDF = false, bool selectNoneForMultiView = false, string expectedReportUrl = "");
    }

}