using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RKCIUIAutomation.Page
{
    public class TableHelper : Action
    {
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
        private string SetXPath_TableTabName(Enum tableTab) => $"//li[@queue='{tableTab.GetString()}']";
        private string SetXPath_NavToPageNumber(int pageNumber) => $"//div[@style='opacity: 1; display: block;']//ul/li/a[text()='{pageNumber.ToString()}']";


        private By GetTblRowBtn_ByLocator(string textInRowForAnyColumn, TableButton tblRowBtn) => By.XPath(SetXPath_TableRowBtnByTextInRow(textInRowForAnyColumn, tblRowBtn));
        private By GetGoToTblPageBtn_ByLocator(TableButton tblPageNavBtn) => By.XPath($"//a[contains(@aria-label,'{tblPageNavBtn.GetString()}')]");
        private By GetNavToPageNumber_ByLocator(int pageNumber) => By.XPath(SetXPath_NavToPageNumber(pageNumber));
        private By GetTableTabName_ByLocator(Enum tableTab) => By.XPath(SetXPath_TableTabName(tableTab));


        //TODO: Horizontal scroll in table (i.e. QA Search>ProctorCurveSummary)


        private string GetSelectedTblPgNumber() => GetText(By.XPath("//ul/li[@class='k-current-page']/span"));
        public void GoToTblPageNumber(int pageNumber) => ClickElement(GetNavToPageNumber_ByLocator(pageNumber));

        private string GetSelectedTblName() => GetText(By.XPath("//li[@aria-selected='true']/span[2]"));
        public void ClickTableTab<T>(T tblTabEnum)
        {
            Enum tabEnum = ConvertToEnumType(tblTabEnum);
            int tblSpanElemsCount = GetElementsCount(By.XPath($"{SetXPath_TableTabName(tabEnum)}/span"));
            By tblTabLocator = GetTableTabName_ByLocator(tabEnum);
            if (tblSpanElemsCount < 2)
            {
                ClickElement(tblTabLocator);
                LogInfo($"Clicked table tab - {tblTabLocator}");
            }
            else
            {
                LogDebug($"Did not click table tab, it is currently active");
            }

            WaitForTableToLoad();
        }


        public void GoToFirstPage() => ClickElement(GetGoToTblPageBtn_ByLocator(TableButton.First));
        public void GoToPreviousPage() => ClickElement(GetGoToTblPageBtn_ByLocator(TableButton.Previous));
        public void GoToNextPage() => ClickElement(GetGoToTblPageBtn_ByLocator(TableButton.Next));
        public void GoToLastPage() => ClickElement(GetGoToTblPageBtn_ByLocator(TableButton.Last));
        public void ToggleCheckBoxForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.CheckBox));
        public void ClickEditButtonForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Action_Edit));
        public void ClickReviseButtonForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Action_Revise));
        public void ClickEnterButtonForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Action_Enter));
        public void ClickQMSViewAttachmentsForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.QMS_Attachments_View));
        public void ClickViewAttachmentsForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Attachments_View));
        public void ClickViewWebFormForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.WebForm_View));
        public void ClickViewReportForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Report_View));


        private string SetXPath_ColumnHeaderName(Enum tblColumnName) => $"//div[contains(@style,'opacity: 1;')]//tr/th/a[2][text()='{tblColumnName.GetString()}']";
        private By GetColumnHeaderName_ByLocator(Enum tblColumnName) => By.XPath(SetXPath_ColumnHeaderName(tblColumnName));
        public void FilterTableColumn<T>(T tblColumnName, string filterValue)
        {
            Enum columnName = ConvertToEnumType(tblColumnName);
            By columnHeaderLocator = By.XPath(SetXPath_ColumnHeaderName(columnName));

            //TODO: map column filter menu and create methods
        }

        enum SortOrder
        {
            [StringValue("default")] Default,
            [StringValue("ascending")]Ascending,
            [StringValue("decending")]Decending
        }

        private SortOrder GetCurrentColumnSortOrder(Enum tblColumnName)
        {
            SortOrder sortOrder = SortOrder.Default;
            Enum columnName = ConvertToEnumType(tblColumnName);
            By sortIndicator = By.XPath($"{SetXPath_ColumnHeaderName(columnName)}/span");

            if (ElementIsDisplayed(sortIndicator))
            {
                string currentOrder = GetElementAttribute(By.XPath($"{SetXPath_ColumnHeaderName(columnName)}/parent::th"), "aria-sort");
                if (currentOrder == SortOrder.Ascending.GetString())
                {
                    sortOrder = SortOrder.Ascending;
                }
                else if (currentOrder == SortOrder.Decending.GetString())
                {
                    sortOrder = SortOrder.Decending;
                }
            }
            return sortOrder;
        }

        private void ToggleColumnSortOrder(Enum tblColumnName, SortOrder desiredOrder)
        {
            Enum columnName = ConvertToEnumType(tblColumnName);
            By columnHeaderLocator = By.XPath($"{SetXPath_ColumnHeaderName(columnName)}");

            SortOrder currentOrder = GetCurrentColumnSortOrder(tblColumnName);
            string current = string.Empty;
            string desired = desiredOrder.GetString();

            if (currentOrder == SortOrder.Default)
            {
                current = SortOrder.Default.GetString();

                if (desiredOrder == SortOrder.Ascending)
                {
                    ClickElement(columnHeaderLocator);
                }
                else if (desiredOrder == SortOrder.Decending)
                {
                    ClickElement(columnHeaderLocator);
                    Thread.Sleep(500);
                    ClickElement(columnHeaderLocator);
                }
            }
            else if (currentOrder == SortOrder.Ascending)
            {
                current = SortOrder.Ascending.GetString();

                if (desiredOrder == SortOrder.Decending)
                {
                    ClickElement(columnHeaderLocator);
                }
                else if (desiredOrder == SortOrder.Default)
                {
                    ClickElement(columnHeaderLocator);
                    Thread.Sleep(500);
                    ClickElement(columnHeaderLocator);
                }
            }
            else if (currentOrder == SortOrder.Decending)
            {
                current = SortOrder.Decending.GetString();

                if (desiredOrder == SortOrder.Default)
                {
                    ClickElement(columnHeaderLocator);
                }
                else if (desiredOrder == SortOrder.Ascending)
                {
                    ClickElement(columnHeaderLocator);
                    Thread.Sleep(500);
                    ClickElement(columnHeaderLocator);
                }
            }

            LogInfo($"Set {tblColumnName.GetString()} column sort order from {current} order to {desired} order");
        }
        public void SortColumnAscending(Enum tblColumnName)
        {
            ToggleColumnSortOrder(tblColumnName, SortOrder.Ascending);
        }

        public void SortColumnDecending(Enum tblColumnName)
        {
            ToggleColumnSortOrder(tblColumnName, SortOrder.Decending);
        }

        public void SortColumnToDefault(Enum tblColumnName)
        {
            ToggleColumnSortOrder(tblColumnName, SortOrder.Default);
        }

    }
}
