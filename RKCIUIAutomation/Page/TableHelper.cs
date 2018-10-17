using OpenQA.Selenium;
using System;

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
        public void FilterColumn(
            Enum columnName,
            string filterValue,
            FilterOperator filterOperator = FilterOperator.EqualTo,
            FilterLogic filterLogic = FilterLogic.And,
            string additionalFilterValue = null,
            FilterOperator additionalFilterOperator = FilterOperator.EqualTo
            ) => Kendo.Filter(columnName.GetString(), filterValue, filterOperator, filterLogic, additionalFilterValue, additionalFilterOperator);

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
            internal const string DblInCell = "DblInCell";
        }

        private enum TableButton
        {
            [StringValue("/input", BtnCategory.LastOrOnlyInRow)] CheckBox,
            [StringValue("/a", BtnCategory.LastOrOnlyInRow)] QMS_Attachments_View,
            [StringValue("/a", BtnCategory.LastOrOnlyInRow)] Action_Edit,
            [StringValue("-1", BtnCategory.MultiDupsInRow)] Report_View,
            [StringValue("-2", BtnCategory.MultiDupsInRow)] WebForm_View,
            [StringValue("-3", BtnCategory.MultiDupsInRow)] Attachments_View,
            [StringValue("Revise", BtnCategory.DblInCell)] Action_Revise,
            [StringValue("Enter", BtnCategory.DblInCell)] Action_Enter,
            [StringValue("first")] First,
            [StringValue("previous")] Previous,
            [StringValue("next")] Next,
            [StringValue("last")] Last
        }

        private string DetermineTblRowBtnXPathExt(TableButton tblBtn)
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

                case BtnCategory.DblInCell:
                    xPathExt = $"{xPathLast()}/a[text()='{xPathExtValue}']";
                    break;

                default:
                    xPathExt = xPathExtValue;
                    break;
            }
            return xPathExt;
        }

        private readonly string ActiveTableDiv = "//div[@class='k-content k-state-active']";

        private string TableByTextInRow(string textInRowForAnyColumn) => $"//td[text()='{textInRowForAnyColumn}']/parent::tr/td";

        private string TableColumnIndex(string columnName) => $"//th[@data-title={columnName}']";
        private string SetXPath_TableRowBaseByTextInRow(string textInRowForAnyColumn = null) => (string.IsNullOrEmpty(textInRowForAnyColumn)) ? "//tr[1]/td" : $"{TableByTextInRow(textInRowForAnyColumn)}";

        private By GetTblRowBtn_ByLocator(TableButton tblRowBtn, string textInRowForAnyColumn = null) => By.XPath($"{ActiveTableDiv}{SetXPath_TableRowBaseByTextInRow(textInRowForAnyColumn)}{DetermineTblRowBtnXPathExt(tblRowBtn)}");

        public By GetTableRowLocator(string textInRowForAnyColumn) => By.XPath($"{ActiveTableDiv}{TableByTextInRow(textInRowForAnyColumn)}");

        public string GetColumnValueForRow(string textInRowForAnyColumn, string columnName)
        {
            By headerLocator = By.XPath($"{ActiveTableDiv}{TableColumnIndex(columnName)}");
            string columnIndex = GetElement(headerLocator).GetAttribute("data-index");

            string rowXPath = $"{TableByTextInRow(textInRowForAnyColumn)}[{columnIndex}]";
            return GetText(By.XPath(rowXPath));
        }

        //<<-- Table Row Button Public Methods -->>
        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ToggleCheckBoxForRow(string textInRowForAnyColumn = null)
        {
            JsClickElement(GetTblRowBtn_ByLocator(TableButton.CheckBox, textInRowForAnyColumn));
            LogInfo($"Toggled Checkbox for row {textInRowForAnyColumn}");
        }

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickEditBtnForRow(string textInRowForAnyColumn = null)
        {
            JsClickElement(GetTblRowBtn_ByLocator(TableButton.Action_Edit, textInRowForAnyColumn));
            LogInfo($"Clicked Edit button for row {textInRowForAnyColumn}");
        }

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickReviseBtnForRow(string textInRowForAnyColumn = null)
        {
            JsClickElement(GetTblRowBtn_ByLocator(TableButton.Action_Revise, textInRowForAnyColumn));
            LogInfo($"Clicked Revise button for row {textInRowForAnyColumn}");
        }

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickEnterBtnForRow(string textInRowForAnyColumn = null)
        {
            JsClickElement(GetTblRowBtn_ByLocator(TableButton.Action_Enter, textInRowForAnyColumn));
            LogInfo($"Clicked Enter button for row {textInRowForAnyColumn}");
        }

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickQMSViewAttachmentsForRow(string textInRowForAnyColumn = null)
        {
            JsClickElement(GetTblRowBtn_ByLocator(TableButton.QMS_Attachments_View, textInRowForAnyColumn));
            LogInfo($"Clicked QMS View Attachments button for row {textInRowForAnyColumn}");
        }

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickViewAttachmentsForRow(string textInRowForAnyColumn = null)
        {
            JsClickElement(GetTblRowBtn_ByLocator(TableButton.Attachments_View, textInRowForAnyColumn));
            LogInfo($"Clicked View Attachments button for row {textInRowForAnyColumn}");
        }

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickViewWebFormForRow(string textInRowForAnyColumn = null)
        {
            JsClickElement(GetTblRowBtn_ByLocator(TableButton.WebForm_View, textInRowForAnyColumn));
            LogInfo($"Clicked View WebForm button for row {textInRowForAnyColumn}");
        }

        /// <summary>
        /// If no argument is provided, the button on the first row will be clicked.
        /// </summary>
        /// <param name="textInRowForAnyColumn"></param>
        public void ClickViewReportForRow(string textInRowForAnyColumn = null)
        {
            JsClickElement(GetTblRowBtn_ByLocator(TableButton.Report_View, textInRowForAnyColumn));
            LogInfo($"Clicked View Report button for row {textInRowForAnyColumn}");
        }

        #endregion Table Row Button Methods

        public void FilterTableColumnByValue(Enum columnName, string recordNameOrNumber)
        {
            WaitForPageReady();
            FilterColumn(columnName, recordNameOrNumber);
        }

        public bool VerifyRecordIsDisplayed(Enum columnName, string recordNameOrNumber)
        {
            bool isDisplayed = false;

            try
            {
                FilterTableColumnByValue(columnName, recordNameOrNumber);
                LogDebug($"Searching for record: {recordNameOrNumber}");

                isDisplayed = ElementIsDisplayed(GetTableRowLocator(recordNameOrNumber)) ? true : isDisplayed;

                if (isDisplayed)
                {
                    LogInfo($"Found record: {recordNameOrNumber}.");
                }
                else
                {
                    LogDebug($"Unable to find record {recordNameOrNumber}.");
                }
            }
            catch (Exception e)
            {
                LogError("Error occured in VerifyRecordIsShownInTab method", true, e);
            }

            return isDisplayed;
        }


        //TODO: Horizontal scroll in table (i.e. QA Search>ProctorCurveSummary)
    }
}