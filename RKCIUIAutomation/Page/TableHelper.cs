using OpenQA.Selenium;
using System;
using System.Threading;

namespace RKCIUIAutomation.Page
{
    public class TableHelper : Action
    {
        public TableHelper(){}
        public TableHelper(IWebDriver driver) => this.driver = driver;

        public void NavigateToTablePage(Enum tblTabEnum, int pageNumber)
        {
            string jsToBeExecuted = GetGridReference(tblTabEnum);
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.page(", pageNumber, ");");
            ExecuteJsScript(jsToBeExecuted);
        }

        public void ClickTableTab(Enum tblTabEnum)
        {
            Type tabType = tblTabEnum.GetType();
            object kendoTabStripEnum = Enum.Parse(tabType, "KendoTabStripId");
            Enum tabStripEnum = ConvertToEnumType(kendoTabStripEnum);

            string jsToBeExecuted = GetTabStripReference(tabStripEnum);
            int tabIndex = Array.IndexOf(Enum.GetValues(tabType), tblTabEnum);
            string tabSelect = string.Format("tab.select('{0}');", tabIndex.ToString());
            jsToBeExecuted = string.Concat(jsToBeExecuted, tabSelect);
            ExecuteJsScript(jsToBeExecuted);
        }

        private void ExecuteJsScript(string jsToBeExecuted)
        {
            Console.WriteLine($"#####CLICK TAB JS#####{jsToBeExecuted}");
            IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
            executor.ExecuteScript(jsToBeExecuted);
            Thread.Sleep(1000);
        }

        private string GetTabStripReference(Enum tblTabEnum)
        {
            string tabStripId = tblTabEnum.GetString();
            string initializeKendoTabStrip = string.Format("var tab = $('#{0}').data('kendoTabStrip');", tabStripId);
            return initializeKendoTabStrip;
        }
        private string GetGridReference(Enum tblTabEnum)
        {
            string gridId = tblTabEnum.GetString();
            string initializeKendoGrid = string.Format("var grid = $('#{0}').data('kendoGrid');", gridId);
            return initializeKendoGrid;
        }


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
        //public void ClickTableTab<T>(T tblTabEnum)
        //{
        //    Enum tabEnum = ConvertToEnumType(tblTabEnum);
        //    int tblSpanElemsCount = GetElementsCount(By.XPath($"{SetXPath_TableTabName(tabEnum)}/span"));
        //    By tblTabLocator = GetTableTabName_ByLocator(tabEnum);
        //    if (tblSpanElemsCount < 2)
        //    {
        //        ClickElement(tblTabLocator);
        //        LogInfo($"Clicked table tab - {tblTabLocator}");
        //    }
        //    else
        //    {
        //        LogDebug($"Did not click table tab, it is currently active");
        //    }

        //    WaitForTableToLoad(driver);
        //}

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


        #region Table Column Filter Methods
        //<<-- Table Column Filter Helpers -->>
        private class AttribType
        {
            internal const string Button = "Button";
            internal const string Input = "Input";
            internal const string DDList = "DDList";
        }
        private enum FilterForm
        {
            [StringValue("Operator", AttribType.DDList)] Operator,
            [StringValue("Value", AttribType.Input)] OperatorValue,
            [StringValue("Filters logic", AttribType.DDList)] FilterLogic,
            [StringValue("Additional operator", AttribType.DDList)] AddnlOperator,
            [StringValue("Additional value", AttribType.Input)] AddnlOperatorValue,
            [StringValue("submit", AttribType.Button)] Filter,
            [StringValue("reset", AttribType.Button)] Clear,
        }

        public enum Operator
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
        }

        public enum Logic
        {
            [StringValue("And")] And,
            [StringValue("Or")] Or
        }

        private string SetXPath_ColumnHeaderByName(Enum tblColumnName) => $"//div[contains(@style,'opacity: 1;')]//tr/th[@data-title='{tblColumnName.GetString()}']";
        private string SetXPath_ColumnHeaderFilterBtn(Enum tblColumnName) => $"{SetXPath_ColumnHeaderByName(tblColumnName)}/a[1]";
        private string GetXPath_ActiveFilterForm() => $"//form[@aria-hidden='false']";
        private string SetXPath_ActiveFilterFormElement(FilterForm formElement) => $"{GetXPath_ActiveFilterForm()}/{formElement.GetString()}";
        
        /// <summary>
        /// Table Colum Filter Form xPath string generator is based on the active Filter Form and requires a prior click of the column filter button.
        /// Only FilterForm enum value is required to generate xpath for input fields and buttons(including buttons to expand a DDList) elements.
        /// Both FilterForm and OperatorOrLogic enum values are required to generate xpath for filter Operator, Logic, or Additional Operator drop-down list item elements.
        /// </summary>
        /// <param name="filterForm"></param>
        /// <param name="OperatorOrLogic"></param>
        /// <returns></returns>
        private string DetermineFilterFormElemXPath(FilterForm filterForm, Enum OperatorOrLogic = null)
        {
            string attrbType = filterForm.GetString(true);

            int divIndex = 0;
            string xPathExt = string.Empty;
            bool isDDListItem = (attrbType == AttribType.DDList) ? true : false;

            if (OperatorOrLogic == null)
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
                    case AttribType.DDList:
                        xPathExt = $"//span[@title='{filterForm.GetString()}']//span[@class='k-select']";
                        break;
                }
            }
            else if (OperatorOrLogic != null)
            {
                try
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
                catch (Exception)
                {
                    LogError("When OperatorOrLogic value is specified, FilterForm value must be Operator, FilterLogic, or AddnlOperator");
                    throw;
                }
            }

            return $"//form[@aria-hidden='false']/div[{divIndex.ToString()}]{xPathExt}";
        }
        private By GetFilterForm_ByLocator(FilterForm filterForm, Enum OperatorOrLogic = null) => By.XPath(DetermineFilterFormElemXPath(filterForm, OperatorOrLogic));
        private void ExpandFilterDDL(FilterForm filterForm)
        {
            By locator = GetFilterForm_ByLocator(filterForm);
            try
            {
                ClickElement(locator);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                LogError($"Unable to expand filter drop down list - {locator}", true, e);
            }
        }
        private void ExpandandAndSelectFilterDDL(FilterForm filterForm, Enum operatorLogic)
        {
            ExpandFilterDDL(filterForm);
            By ddListItemLocator = GetFilterForm_ByLocator(filterForm, operatorLogic);
            ClickElement(ddListItemLocator);
        }

        //<<-- Table Column Filter Public Methods -->>
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tblColumnName"></param>
        /// <param name="operatorFilterValue"></param>
        /// <param name="additionalOperatorFilterValue"></param>
        /// <param name="Operator"></param>
        /// <param name="Logic"></param>
        /// <param name="AddnlOperator"></param>
        public void FilterTableColumn<T>(T tblColumnName, string operatorFilterValue, string additionalOperatorFilterValue = null, Operator Operator = Operator.Is_equal_to, Logic Logic = Logic.And, Operator AddnlOperator = Operator.Is_equal_to)
        {
            Enum columnName = ConvertToEnumType(tblColumnName);
            By filterColumnBtnLocator = By.XPath(SetXPath_ColumnHeaderFilterBtn(columnName));
            ClickElement(filterColumnBtnLocator);

            try
            {
                if (Operator != Operator.Is_equal_to)
                {
                    ExpandandAndSelectFilterDDL(FilterForm.Operator, Operator);
                }

                By operatorInputLocator = GetFilterForm_ByLocator(FilterForm.OperatorValue);
                EnterText(operatorInputLocator, operatorFilterValue);

                if (Logic != Logic.And)
                {
                    ExpandandAndSelectFilterDDL(FilterForm.FilterLogic, Logic);
                }

                if (additionalOperatorFilterValue != null)
                {
                    By addnlOperatorInputLocator = GetFilterForm_ByLocator(FilterForm.AddnlOperatorValue);

                    if (AddnlOperator != Operator.Is_equal_to)
                    {
                        ExpandandAndSelectFilterDDL(FilterForm.AddnlOperator, AddnlOperator);
                    }

                    EnterText(addnlOperatorInputLocator, additionalOperatorFilterValue);
                }

                By filterBtnLocator = GetFilterForm_ByLocator(FilterForm.Filter);
                ClickElement(filterBtnLocator);
            }
            catch (Exception e)
            {
                LogError(e.Message);
                throw;
            }
        }
        public void ClearTableColumnFilter<T>(T tblColumnName)
        {
            try
            {
                Enum columnName = ConvertToEnumType(tblColumnName);
                By filterColumnBtnLocator = By.XPath(SetXPath_ColumnHeaderFilterBtn(columnName));
                ClickElement(filterColumnBtnLocator);

                By clearFilterBtnLocator = GetFilterForm_ByLocator(FilterForm.Clear);
                ClickElement(clearFilterBtnLocator);
            }
            catch (Exception e)
            {
                LogError(e.Message);
                throw;
            }
        }
        #endregion <-- end of Table Column Filter & Sort Order Methods


        #region Table Column Sort Order Methods
        //<<-- Table Column Sort Order Helpers -->>
        private string SetXPath_ColumnHeaderNameSortBtn(Enum tblColumnName) => $"{SetXPath_ColumnHeaderByName(tblColumnName)}/a[2]";
        private enum SortOrder
        {
            [StringValue("default")] Default,
            [StringValue("ascending")] Ascending,
            [StringValue("decending")] Decending
        }

        private SortOrder GetCurrentColumnSortOrder(Enum tblColumnName)
        {
            WaitForTableToLoad();

            SortOrder sortOrder = SortOrder.Default;
            Enum columnName = ConvertToEnumType(tblColumnName);
            By columnHeaderLocator = By.XPath(SetXPath_ColumnHeaderByName(columnName));
            string attribute = "aria-sort";
            bool isSorted = JsHasAttribute(columnHeaderLocator, attribute);

            if (!isSorted)
            {
                sortOrder = SortOrder.Default;
            }
            else
            {
                string sortedAttrib = JsGetAttribute(columnHeaderLocator, attribute);
                if (sortedAttrib == SortOrder.Ascending.GetString())
                {
                    sortOrder = SortOrder.Ascending;
                }
                else if (sortedAttrib == SortOrder.Decending.GetString())
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


        //TODO: Horizontal scroll in table (i.e. QA Search>ProctorCurveSummary)


        //TODO: KendoUI jsScripts - 
        static string tabIndex = "1";
        string jsTabStrip = string.Format("var tabStrip = $('#DesignDocumentList').data('kendoTabStrip');");
        string jsClickTab = string.Format("tabStrip.element['0'].childNodes['0'].childNodes['{0}'].firstChild.click();", tabIndex);
        string jsFilterColumn = "grid.dataSource.filter({logic: 'and', filters: [{field: 'SubmittalNumber', operator: 'eq', value: 'Bhoomi416'}]});";
    }
}
