using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RKCIUIAutomation.Page
{
    public class TableHelper : Action
    {        
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
        public void ToggleCheckBoxForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.CheckBox));
        public void ClickEditButtonForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Action_Edit));
        public void ClickReviseButtonForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Action_Revise));
        public void ClickEnterButtonForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Action_Enter));
        public void ClickQMSViewAttachmentsForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.QMS_Attachments_View));
        public void ClickViewAttachmentsForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Attachments_View));
        public void ClickViewWebFormForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.WebForm_View));
        public void ClickViewReportForRow(string textInRowForAnyColumn) => ClickElement(GetTblRowBtn_ByLocator(textInRowForAnyColumn, TableButton.Report_View));

        #endregion <-- end of Table Row Button Methods


        #region Table Tab Methods
        //<<-- Table Tab Helpers -->>
        private string GetSelectedTblName() => GetText(By.XPath("//li[@aria-selected='true']/span[2]"));
        private string SetXPath_TableTabName(Enum tableTab) => $"//li[@queue='{tableTab.GetString()}']";       
        private By GetTableTabName_ByLocator(Enum tableTab) => By.XPath(SetXPath_TableTabName(tableTab));
        
        //<<-- Table Tab Public Methods -->>
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

        #endregion <-- end of Table Tab Methods


        #region Table Page Navigation Methods
        //<<-- Table Page Navigation Helpers -->>
        private string GetSelectedTblPgNumber() => GetText(By.XPath("//ul/li[@class='k-current-page']/span"));
        private string SetXPath_NavToPageNumber(int pageNumber) => $"//div[@style='opacity: 1; display: block;']//ul/li/a[text()='{pageNumber.ToString()}']";
        private By GetGoToTblPageBtn_ByLocator(TableButton tblPageNavBtn) => By.XPath($"//a[contains(@aria-label,'{tblPageNavBtn.GetString()}')]");
        private By GetNavToPageNumber_ByLocator(int pageNumber) => By.XPath(SetXPath_NavToPageNumber(pageNumber));

        //<<-- Table Page Navigation Public Methods -->>
        public void GoToTblPageNumber(int pageNumber) => ClickElement(GetNavToPageNumber_ByLocator(pageNumber));
        public void GoToFirstPage() => ClickElement(GetGoToTblPageBtn_ByLocator(TableButton.First));
        public void GoToPreviousPage() => ClickElement(GetGoToTblPageBtn_ByLocator(TableButton.Previous));
        public void GoToNextPage() => ClickElement(GetGoToTblPageBtn_ByLocator(TableButton.Next));
        public void GoToLastPage() => ClickElement(GetGoToTblPageBtn_ByLocator(TableButton.Last));

        #endregion <-- end of Table Page Navigation Methods


        #region Table Column Sort Order Methods
        //<<-- Table Column Sort Order Helpers -->>
        private enum SortOrder
        {
            [StringValue("default")] Default,
            [StringValue("ascending")] Ascending,
            [StringValue("decending")] Decending
        }
        private SortOrder GetCurrentColumnSortOrder(Enum tblColumnName)
        {
            SortOrder sortOrder = SortOrder.Default;
            Enum columnName = ConvertToEnumType(tblColumnName);
            By sortIndicator = By.XPath($"{SetXPath_ColumnHeaderNameSortBtn(columnName)}/span");

            if (ElementIsDisplayed(sortIndicator))
            {
                string currentOrder = GetElementAttribute(By.XPath($"{SetXPath_GetColumnHeaderByName(columnName)}"), "aria-sort");
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
            By columnHeaderLocator = By.XPath($"{SetXPath_ColumnHeaderNameSortBtn(columnName)}");

            SortOrder currentOrder = GetCurrentColumnSortOrder(tblColumnName);
            string current = string.Empty;
            string desired = desiredOrder.GetString();

            switch (currentOrder)
            {
                case SortOrder.Ascending:
                    current = SortOrder.Ascending.GetString();

                    if (desiredOrder == SortOrder.Decending)
                    {
                        ClickElement(columnHeaderLocator);
                    }
                    else if (desiredOrder == SortOrder.Default)
                    {
                        ClickTwice(columnHeaderLocator);
                    }
                    break;
                case SortOrder.Decending:
                    current = SortOrder.Decending.GetString();

                    if (desiredOrder == SortOrder.Default)
                    {
                        ClickElement(columnHeaderLocator);
                    }
                    else if (desiredOrder == SortOrder.Ascending)
                    {
                        ClickTwice(columnHeaderLocator);
                    }
                    break;
                default:
                    current = SortOrder.Default.GetString();

                    if (desiredOrder == SortOrder.Ascending)
                    {
                        ClickElement(columnHeaderLocator);
                    }
                    else if (desiredOrder == SortOrder.Decending)
                    {
                        ClickTwice(columnHeaderLocator);
                    }
                    break;
            }

            LogInfo($"Set {tblColumnName.GetString()} column sort order from {current} order to {desired} order");
        }

        //<<-- Table Column Sort Order Public Methods -->>
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

        #endregion <-- end of Column Sort Order Methods


        #region Table Column Filter Methods
        //<<-- Table Column Filter Helpers -->>
        private class AttribType
        {
            internal const string Button = "//button[@type=";
            internal const string Input = "/input[@title=";
            internal const string DDLBtn = "//span[@class='k-select']";
            internal const string DDList = "DDList";
        }
        public enum FilterForm
        {
            [StringValue("Operator", AttribType.DDLBtn)] Operator,
            [StringValue("Value", AttribType.Input)] OperatorValue,
            [StringValue("Filters logic", AttribType.DDLBtn)] FilterLogic,
            [StringValue("Additional operator", AttribType.DDLBtn)] AddnlOperator,
            [StringValue("Additional value", AttribType.Input)] AddnlOperatorValue,
            [StringValue("submit", AttribType.Button)] Filter,
            [StringValue("reset", AttribType.Button)] Clear,
        }
        public enum OperatorOrLogic
        {
            [StringValue("Is equal to")] Is_equal_to,
            [StringValue("Is not equal to")] Is_not_equal_to,
            [StringValue("Starts with")] Starts_with,
            [StringValue("Contains")] Contains,
            [StringValue("Does not contain")] Does_not_contain,
            [StringValue("Ends with")] Ends_with,
            [StringValue("Is null")] Is_null,
            [StringValue("Is not null")] Is_not_null,
            [StringValue("Is empty")] Is_empty,
            [StringValue("Is not empty")] Is_not_empty,
            [StringValue("And")] And,
            [StringValue("Or")] Or
        }

        /// <summary>
        /// Table Colum Filter Form xPath string generator.
        /// Only FilterForm enum value is required to generate xpath for button or input elements.
        /// Both FilterForm and OperatorOrLogic enum values are required to generate xpath for filter Operator, Logic, or Additional Operator drop-down list elements.
        /// </summary>
        /// <param name="filterForm"></param>
        /// <param name="OperatorOrLogic"></param>
        /// <returns></returns>
        private string GenerateFilterFormElemXPath(FilterForm filterForm, Enum OperatorOrLogic = null)
        {
            string attrbType = filterForm.GetString(true);

            int divIndex = 0;
            string xPathExt = string.Empty;
            bool isDDListItem = (attrbType == AttribType.DDList) ? true : false;

            if (!isDDListItem)
            {
                divIndex = 1;
                    
                switch (attrbType)
                {
                    case AttribType.Button:
                        xPathExt = $"//button[@type='{filterForm.GetString()}']";
                        break;
                    case AttribType.Input:
                        xPathExt = $"/input[@title='{filterForm.GetString()}']";
                        break;
                    case AttribType.DDLBtn:
                        xPathExt = $"//span[@title='{filterForm.GetString()}']//span[@class='k-select']";
                        break;
                }
            }
            else
            {
                switch (filterForm)
                {
                    case FilterForm.Operator:
                        divIndex = 2;
                        break;
                    case FilterForm.FilterLogic:
                        divIndex = 3;
                        break;
                    case FilterForm.AddnlOperator:
                        divIndex = 4;
                        break;
                }

                xPathExt = $"//ul/li[text()='{OperatorOrLogic.GetString()}']";
            }

            return $"//form[@aria-hidden='false']/div[{divIndex.ToString()}]{xPathExt}";
        }
        private string SetXPath_GetColumnHeaderByName(Enum tblColumnName) => $"//div[contains(@style,'opacity: 1;')]//tr/th[@data-title='{tblColumnName.GetString()}']";
        private string SetXPath_ColumnHeaderFilterBtn(Enum tblColumnName) => $"{SetXPath_GetColumnHeaderByName(tblColumnName)}/a[1]";
        private string SetXPath_ColumnHeaderNameSortBtn(Enum tblColumnName) => $"{SetXPath_GetColumnHeaderByName(tblColumnName)}/a[2]";
        private string GetXPath_ActiveFilterForm() => $"//form[@aria-hidden='false']";
        private string SetXPath_ActiveFilterFormElement(FilterForm formElement) => $"{GetXPath_ActiveFilterForm()}/{formElement.GetString()}";


        //<<-- Table Column Filter Public Methods -->>
        public void FilterTableColumn<T>(T tblColumnName, string filterValue)
        {
            Enum columnName = ConvertToEnumType(tblColumnName);
            By columnHeaderFilterBtnLocator = By.XPath(SetXPath_ColumnHeaderFilterBtn(columnName));

            try
            {
                IWebElement activeFilterForm = driver.FindElement(By.XPath("//form[@aria-hidden='false']"));
                IWebElement filterBtn = activeFilterForm.FindElement(By.XPath(".//button[@type='submit']"));
                IWebElement clearBtn = activeFilterForm.FindElement(By.XPath(".//button[@type='reset']"));

                //TODO: create logic for table column filter methods using GenerateFilterFormElemXPath() method.
            }
            catch (Exception)
            {
                //TODO: Log for table column filter method
                throw;
            }
        }

        #endregion <-- end of Table Column Filter & Sort Order Methods



        //TODO: Horizontal scroll in table (i.e. QA Search>ProctorCurveSummary)
    }
}
