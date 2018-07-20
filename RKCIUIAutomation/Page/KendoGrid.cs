using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading;

namespace RKCIUIAutomation.Page
{
    public class KendoGrid : Action
    {
        public KendoGrid(){ }
        public KendoGrid(IWebDriver driver) => this.driver = driver;


        public void ClickTableTab(string tblTabName)
        {
            string jsToBeExecuted = GetTabStripReference();

            By locator = By.XPath("//ul[@class='k-reset k-tabstrip-items']/li");
            var tabIndex = GetElementIndex(locator, tblTabName);
            string tabSelect = $"tab.select('{tabIndex.ToString()}');";
            jsToBeExecuted = $"{jsToBeExecuted}{tabSelect}";
            ExecuteJsScript(jsToBeExecuted);
            Thread.Sleep(1000);
        }

        private void ExecuteJsScript(string jsToBeExecuted)
        {
            Console.WriteLine($"#####CLICK TAB JS#####{jsToBeExecuted}");
            IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
            executor.ExecuteScript(jsToBeExecuted);
        }

        private object ExecuteJsScriptGet(string jsToBeExecuted)
        {
            IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
            return executor.ExecuteScript(jsToBeExecuted);
        }

        private int GetElementIndex(By findElementsLocator, string matchValue)
        {
            IList<IWebElement> elements = new List<IWebElement>();
            elements = driver.FindElements(findElementsLocator);

            int index = 0;
            for (int i = 0; i < elements.Count; i++)
            {
                string queueValue = elements[i].GetAttribute("queue");
                bool match = (queueValue == matchValue) ? true : false;

                if (match == true)
                {
                    index = i;
                    break;
                }
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

        public void NavigateToTablePage(int pageNumber)
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.page({pageNumber});";
            ExecuteJsScript(jsToBeExecuted);
        }

        public void Sort(string columnName, SortType sortType)
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.sort({{field: '{columnName}', dir: '{sortType.GetString()}'}});";
            ExecuteJsScript(jsToBeExecuted);
        }

        public List<T> GetItems<T>() where T : class
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = $"{jsToBeExecuted} return JSON.stringify(grid.dataSource.data());";
            var jsResults = ExecuteJsScriptGet(jsToBeExecuted);
            var items = JsonConvert.DeserializeObject<List<T>>(jsResults.ToString());
            return items;
        }

        public void Filter(string columnName, FilterOperator filterOperator, string filterValue, FilterLogic filterLogic = FilterLogic.And, string additionalFilterValue = null, FilterOperator additionalFilterOperator = FilterOperator.EqualTo)
        {
            this.Filter(new GridFilter(columnName, filterOperator, filterValue, filterLogic, additionalFilterValue, additionalFilterOperator));
        }

        private void Filter(params GridFilter[] gridFilters)
        {
            string filterScript = string.Empty;

            foreach (var currentFilter in gridFilters)
            {
                string jsFilterBase = $"grid.dataSource.filter({{ logic: '{currentFilter.FilterLogic.GetString()}', filters: [";

                DateTime filterDateTime;
                bool isFilterDateTime = DateTime.TryParse(currentFilter.FilterValue, out filterDateTime);
                string filterValueToBeApplied =
                    isFilterDateTime ? $"new Date({filterDateTime.Year}, {filterDateTime.Month - 1}, {filterDateTime.Day})" : $"{currentFilter.FilterValue}";

                string columnName = currentFilter.ColumnName;

                filterScript = $"{jsFilterBase}{{ field: '{columnName}', operator: '{currentFilter.FilterOperator.GetString()}', value: '{filterValueToBeApplied}' }}";

                filterScript = (currentFilter.AdditionalFilterValue == null) ? filterScript :
                    $"{filterScript},{{ field: '{columnName}', operator: '{currentFilter.AdditionalFilterOperator.GetString()}', value: '{currentFilter.AdditionalFilterValue}' }}";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append($"{GetGridReference()}{filterScript}] }});");
            Console.WriteLine(sb.ToString());
            ExecuteJsScript(sb.ToString());
        }

        public int GetCurrentPageNumber()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = $"{jsToBeExecuted} return grid.dataSource.page();";
            IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
            var result = executor.ExecuteScript(jsToBeExecuted);
            int pageNumber = int.Parse(result.ToString());
            return pageNumber;
        }


        private string GetTabStripReference()
        {
            string tabStripId = string.Empty;
            WaitForPageReady();

            try
            {
                By tabStripLocator = By.XPath("//div[contains(@class,'k-tabstrip-top')]");
                tabStripId = GetElement(tabStripLocator).GetAttribute("id");

                if (!string.IsNullOrEmpty(tabStripId))
                {
                    LogInfo($"Found Kendo Grid TabStrip ID: {tabStripId}");
                }
            }
            catch (Exception e)
            {
                LogError(e.Message);
                throw;
            }

            return $"var tab = $('#{tabStripId}').data('kendoTabStrip');";
        }
        private string GetGridReference()
        {
            string gridId = string.Empty;
            WaitForPageReady();

            try
            {
                By tabStripLocator = By.XPath("//div[@data-role='tabstrip']");
                By singleGridDivLocator = By.XPath("//div[@data-role='grid']");
                By multiActiveGridDivLocator = By.XPath("//div[@class='k-content k-state-active']/div");

                IWebElement tabStripElem = GetElement(tabStripLocator);
                By gridLocator = (tabStripElem?.Displayed == true) ? multiActiveGridDivLocator : singleGridDivLocator;
                gridId = GetElement(gridLocator).GetAttribute("id");

                if (!string.IsNullOrEmpty(gridId))
                {
                    string gridType = (gridLocator == singleGridDivLocator) ? "Single" : "Multi";
                    LogInfo($"Found Kendo {gridType}-Table type Grid ID: {gridId}");
                }
            }
            catch (Exception e)
            {
                LogDebug(e.Message);
            }

            return $"var grid = $('#{gridId}').data('kendoGrid');";
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
        public GridFilter (string columnName, FilterOperator filterOperator, string filterValue, FilterLogic filterLogic = FilterLogic.And, string additionalFilterValue = null, FilterOperator additionalFilterOperator = FilterOperator.EqualTo)
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
