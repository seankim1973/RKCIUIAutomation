using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RKCIUIAutomation.Page
{
    public class TableHelper : Action
    {        
        public TableHelper(){}
        public TableHelper(IWebDriver driver) => this.driver = driver;

        private KendoGrid kendo;
        private KendoGrid Kendo => kendo = new KendoGrid(driver);


        #region Kendo Grid Public Methods

        public void ClickTab(Enum tblTabEnum) => Kendo.ClickTableTab(tblTabEnum.GetString());
        public void RefreshTable() => Kendo.Reload();
        public int GetTableRowCount() => Kendo.TotalNumberRows();

        //<<-- Table Page Navigation Helpers -->>
        private By GetGoToTblPgBtn_ByLocator(TableButton tblPageNavBtn) => By.XPath($"//a[contains(@aria-label,'{tblPageNavBtn.GetString()}')]");
        public void GoToFirstPage() => JsClickElement(GetGoToTblPgBtn_ByLocator(TableButton.First));
        public void GoToPreviousPage() => JsClickElement(GetGoToTblPgBtn_ByLocator(TableButton.Previous));
        public void GoToNextPage() => JsClickElement(GetGoToTblPgBtn_ByLocator(TableButton.Next));
        public void GoToLastPage() => JsClickElement(GetGoToTblPgBtn_ByLocator(TableButton.Last));
        public int GetCurrentPageNumber() => Kendo.GetCurrentPageNumber();
        public void GoToPageNumber(int pageNumber) => Kendo.NavigateToTablePage(pageNumber);
        public int GetCurrentViewItemsPerPageSize() => Kendo.GetPageSize();
        public void SetViewItemsPerPageSize(int newSize) => Kendo.ChangePageSize(newSize);

        public void ClearFilters() => Kendo.RemoveFilters();
        public void FilterColumn(
            Enum columnName,
            FilterOperator filterOperator,
            string filterValue,
            FilterLogic filterLogic = FilterLogic.And,
            string additionalFilterValue = null,
            FilterOperator additionalFilterOperator = FilterOperator.EqualTo
            ) => Kendo.Filter(columnName.GetString(), filterOperator, filterValue, filterLogic, additionalFilterValue, additionalFilterOperator);

        public void SortColumnAscending(Enum columnName) => Kendo.Sort(columnName.GetString(), SortType.Ascending);
        public void SortColumnDescending(Enum columnName) => Kendo.Sort(columnName.GetString(), SortType.Descending);
        public void SortColumnToDefault(Enum columnName) => Kendo.Sort(columnName.GetString(), SortType.Default);
        
        #endregion



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
            [StringValue("-1", BtnCategory.MultiDupsInRow)]Report_View,
            [StringValue("-2", BtnCategory.MultiDupsInRow)] WebForm_View,
            [StringValue("-3", BtnCategory.MultiDupsInRow)] Attachments_View,
            [StringValue("1", BtnCategory.DblInCell)] Action_Revise,
            [StringValue("2", BtnCategory.DblInCell)] Action_Enter,
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
                    xPathExt = $"{xPathLast()}/a[{xPathExtValue}]";
                    break;
                default:
                    xPathExt = xPathExtValue;
                    break;
            }
            return xPathExt;
        }
        private string SetXPath_TableRowBaseByTextInRow(string textInRowForAnyColumn) => $"//td[text()='{textInRowForAnyColumn}']/parent::tr/td";
        private string SetXPath_TableRowBtnByTextInRow(string textInRowForAnyColumn, TableButton tblRowBtn) => $"{SetXPath_TableRowBaseByTextInRow(textInRowForAnyColumn)}{DetermineTblRowBtnXPathExt(tblRowBtn)}";
        private By GetTblRowBtn_ByLocator(string textInRowForAnyColumn, TableButton tblRowBtn) => By.XPath(SetXPath_TableRowBtnByTextInRow(textInRowForAnyColumn, tblRowBtn));

        //<<-- Table Row Button Public Methods -->>
        public void ToggleCheckBoxForRow(string textInRowForAnyColumn) => JsClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.CheckBox));
        public void ClickEditBtnForRow(string textInRowForAnyColumn) => JsClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Action_Edit));
        public void ClickReviseBtnForRow(string textInRowForAnyColumn) => JsClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Action_Revise));
        public void ClickEnterBtnForRow(string textInRowForAnyColumn) => JsClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Action_Enter));
        public void ClickQMSViewAttachmentsForRow(string textInRowForAnyColumn) => JsClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.QMS_Attachments_View));
        public void ClickViewAttachmentsForRow(string textInRowForAnyColumn) => JsClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Attachments_View));
        public void ClickViewWebFormForRow(string textInRowForAnyColumn) => JsClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.WebForm_View));
        public void ClickViewReportForRow(string textInRowForAnyColumn) => JsClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Report_View));

        #endregion <-- end of Table Row Button Methods



        //TODO: Horizontal scroll in table (i.e. QA Search>ProctorCurveSummary)
    }
}
