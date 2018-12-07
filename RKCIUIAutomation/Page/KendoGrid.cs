using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKCIUIAutomation.Page
{
    public class KendoGrid : Action
    {
        public KendoGrid()
        {
        }

        public KendoGrid(IWebDriver driver) => this.Driver = driver;

        public void ClickCommentTab(int commentNumber)
        {
            WaitForPageReady();
            string jsToBeExecuted = GetTabStripReference();
            int commentTabIndex = commentNumber - 1;
            string tabSelect = $"tab.select('{commentTabIndex.ToString()}');";
            jsToBeExecuted = $"{jsToBeExecuted}{tabSelect}";
            ExecuteJsScript(jsToBeExecuted);
            LogInfo($"Clicked Comment {commentNumber} tab : {tabSelect}");
        }

        public string GetCurrentTableTabName()
            => GetText(By.XPath("//li[contains(@class, 'k-state-active')]/span[@class='k-link']"));

        public void ClickTableTab(string tblTabName)
        {
            try
            {
                string currentTabName = GetCurrentTableTabName();

                if (!tblTabName.Equals(currentTabName))
                {
                    string jsToBeExecuted = GetTabStripReference();

                    By locator = By.XPath("//ul[@class='k-reset k-tabstrip-items']/li/span[text()]");
                    int tabIndex = GetElementIndex(locator, tblTabName);
                    if (tabIndex >= 0)
                    {
                        string tabSelect = $"tab.select('{tabIndex.ToString()}');";
                        jsToBeExecuted = $"{jsToBeExecuted}{tabSelect}";
                        ExecuteJsScript(jsToBeExecuted);
                        LogInfo($"Clicked Table Tab - {tblTabName}");
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        private void ExecuteJsScript(string jsToBeExecuted)
        {
            IJavaScriptExecutor executor = Driver as IJavaScriptExecutor;
            executor.ExecuteScript(jsToBeExecuted);
        }

        private object ExecuteJsScriptGet(string jsToBeExecuted)
        {
            IJavaScriptExecutor executor = Driver as IJavaScriptExecutor;
            return executor.ExecuteScript(jsToBeExecuted);
        }

        private int GetElementIndex(By findElementsLocator, string matchValue)
        {
            int index = -1;
            try
            {
                IList<IWebElement> elements = new List<IWebElement>();
                elements = Driver.FindElements(findElementsLocator);

                for (int i = 0; i < elements.Count; i++)
                {
                    string spanText = elements[i].Text;
                    bool match = spanText.Equals(matchValue) ? true : false;

                    if (match)
                    {
                        index = i;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            if (index == -1)
            {
                log.Error($"Unable to get index for: {matchValue}");
            }

            return index;
        }

        public void RemoveFilters()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.filter([]);";
            ExecuteJsScript(jsToBeExecuted);
        }

        public int TotalNumberRows()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.total();";
            var jsResult = ExecuteJsScriptGet(jsToBeExecuted);
            return int.Parse(jsResult.ToString());
        }

        public void Reload()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.read();";
            ExecuteJsScript(jsToBeExecuted);
        }

        public int GetPageSize()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = $"{jsToBeExecuted} return grid.dataSource.pageSize();";
            var currentResponse = ExecuteJsScriptGet(jsToBeExecuted);
            int pageSize = int.Parse(currentResponse.ToString());
            return pageSize;
        }

        public void ChangePageSize(int newSize)
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.pageSize({newSize});";
            ExecuteJsScript(jsToBeExecuted);
        }

        public void GoToTablePage(int pageNumber)
        {
            ScrollToElement(By.XPath("//div[@data-role='pager']"));
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.page({pageNumber});";
            ExecuteJsScript(jsToBeExecuted);
            LogInfo($"Navigated to table page {pageNumber}");
        }

        public void Sort(string columnName, SortType sortType)
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.sort({{field: '{columnName}', dir: '{sortType.GetString()}'}});";
            ExecuteJsScript(jsToBeExecuted);
            LogInfo($"Sorted {columnName} column to {sortType.ToString()} order");
        }

        public List<T> GetItems<T>() where T : class
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = $"{jsToBeExecuted} return JSON.stringify(grid.dataSource.data());";
            var jsResults = ExecuteJsScriptGet(jsToBeExecuted);
            var items = JsonConvert.DeserializeObject<List<T>>(jsResults.ToString());
            return items;
        }

        public void FilterTableGrid(
            string columnName,
            string filterValue,
            FilterOperator filterOperator = FilterOperator.EqualTo,
            FilterLogic filterLogic = FilterLogic.And,
            string additionalFilterValue = null,
            FilterOperator additionalFilterOperator = FilterOperator.EqualTo
            )
            => Filter(
                new GridFilter(
                    columnName,
                    filterOperator,
                    filterValue,
                    filterLogic,
                    additionalFilterValue,
                    additionalFilterOperator
                    )
                );

        private void Filter(params GridFilter[] gridFilters)
        {
            string columnName = null;
            string filterValue = null;
            string filterOperator = null;
            string filterLogic = null;
            string addnlFilterOperator = null;
            string addnlFilterValue = null;

            string filterScript = null;

            foreach (var currentFilter in gridFilters)
            {
                filterLogic = currentFilter.FilterLogic.GetString();
                string jsFilterBase = $"grid.dataSource.filter({{ logic: '{filterLogic}', filters: [";

                DateTime filterDateTime;
                filterValue = currentFilter.FilterValue;
                bool isFilterDateTime = DateTime.TryParse(filterValue, out filterDateTime);
                string filterValueToBeApplied =
                    isFilterDateTime ? $"new Date({filterDateTime.Year}, {filterDateTime.Month - 1}, {filterDateTime.Day})" : $"{filterValue}";

                columnName = currentFilter.ColumnName;
                filterOperator = currentFilter.FilterOperator.GetString();
                filterScript = $"{jsFilterBase}{{ field: '{columnName}', operator: '{filterOperator}', value: '{filterValueToBeApplied}' }}";

                addnlFilterValue = currentFilter.AdditionalFilterValue;
                addnlFilterOperator = currentFilter.AdditionalFilterOperator.GetString();
                filterScript = (addnlFilterValue == null) ? filterScript :
                    $"{filterScript},{{ field: '{columnName}', operator: '{addnlFilterOperator}', value: '{addnlFilterValue}' }}";
            }

            StringBuilder sb = new StringBuilder();

            string gridRef = GetGridReference();

            sb.Append($"{gridRef}{filterScript}] }});");
            ExecuteJsScript(sb.ToString());

            string addnlFilter = (addnlFilterValue != null) ? $", Additional Filter - (Logic):{filterLogic}, (Operator):{addnlFilterOperator}, (Value):{addnlFilterValue}" : string.Empty;
            LogInfo($"Filtered: (Column):{columnName}, (Operator):{filterOperator}, (Value):{filterValue} {addnlFilter}");
        }

        public int GetCurrentPageNumber()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = $"{jsToBeExecuted} return grid.dataSource.page();";
            IJavaScriptExecutor executor = Driver as IJavaScriptExecutor;
            var result = executor.ExecuteScript(jsToBeExecuted);
            int pageNumber = int.Parse(result.ToString());
            return pageNumber;
        }

        private string GetTabStripReference()
        {
            string tabStripId = string.Empty;
            string logMsg = string.Empty;

            try
            {
                By tabStripLocator = By.XPath("//div[contains(@class,'k-tabstrip-top')]");
                tabStripId = GetElement(tabStripLocator).GetAttribute("id");
                logMsg = !string.IsNullOrEmpty(tabStripId) ? $"Found Kendo Grid TabStrip ID: {tabStripId}" : "NULL Kendo Grid TabStrip ID";
                log.Debug(logMsg);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            return $"var tab = $('#{tabStripId}').data('kendoTabStrip');";
        }

        private string GetGridReference()
        {
            string gridId = string.Empty;
            string logMsg = string.Empty;

            try
            {
                gridId = GetGridID();
                logMsg = !string.IsNullOrEmpty(gridId) ? $"Found Kendo Grid ID: {gridId}" : $"NULL Kendo Grid ID";
                log.Debug(logMsg);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }

            return $"var grid = $('#{gridId}').data('kendoGrid');";
        }

        public string GetGridID()
        {
            By singleGridDivLocator = By.XPath("//div[@class='k-widget k-grid k-display-block'][@data-role='grid']");
            By multiActiveGridDivLocator = By.XPath("//div[@class='k-content k-state-active']//div[@data-role='grid']");
            IWebElement gridElem = null;
            string gridId = string.Empty;

            try
            {
                gridElem = GetElement(multiActiveGridDivLocator) ?? GetElement(singleGridDivLocator);
                //gridElem = gridElem == null ? GetElement(singleGridDivLocator) : gridElem;
                gridId = gridElem.GetAttribute("id");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return gridId;
        }
    }

    public enum SortType
    {
        [StringValue("")] Default,
        [StringValue("asc")] Ascending,
        [StringValue("desc")] Descending
    }

    public enum FilterOperator
    {
        [StringValue("eq")] EqualTo,
        [StringValue("neq")] NotEqualTo,
        [StringValue("lt")] LessThan,
        [StringValue("lte")] LessThanOrEqualTo,
        [StringValue("gt")] GreaterThan,
        [StringValue("gte")] GreaterThanOrEqualTo,
        [StringValue("startswith")] StartsWith,
        [StringValue("endswith")] EndsWith,
        [StringValue("contains")] Contains,
        [StringValue("doesnotcontain")] NotContains,
        [StringValue("gt")] IsAfter,
        [StringValue("gte")] IsAfterOrEqualTo,
        [StringValue("lt")] IsBefore,
        [StringValue("lte")] IsBeforeOrEqualTo
    }

    public enum FilterLogic
    {
        [StringValue("and")] And,
        [StringValue("or")] Or
    }

    public class GridFilter
    {
        public GridFilter(string columnName, FilterOperator filterOperator, string filterValue, FilterLogic filterLogic = FilterLogic.And, string additionalFilterValue = null, FilterOperator additionalFilterOperator = FilterOperator.EqualTo)
        {
            ColumnName = columnName;
            FilterOperator = filterOperator;
            FilterValue = filterValue;
            FilterLogic = filterLogic;
            AdditionalFilterOperator = additionalFilterOperator;
            AdditionalFilterValue = additionalFilterValue;
        }

        public string ColumnName { get; internal set; }
        public FilterOperator FilterOperator { get; internal set; }
        public string FilterValue { get; internal set; }
        public FilterLogic FilterLogic { get; internal set; }
        public FilterOperator AdditionalFilterOperator { get; internal set; }
        public string AdditionalFilterValue { get; internal set; }
    }
}