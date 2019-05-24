using Newtonsoft.Json;
using OpenQA.Selenium;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using static RKCIUIAutomation.Page.TableHelper;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page
{
    extern alias newtJson;

    public class KendoGrid : Action, IKendoGrid
    {
        KendoGrid _Kendo { get; set; }
        public KendoGrid GetInstance()
        {
            if (_Kendo == null)
            {
                _Kendo = new KendoGrid();
            }

            return _Kendo;
        }
        public KendoGrid()
        {
            _Kendo = new KendoGrid();
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
            Report.Step($"Clicked Comment {commentNumber} tab : {tabSelect}");
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
                    By locator = By.XPath("//ul[@class='k-reset k-tabstrip-items']/li/span[text()]");
                    int tabIndex = GetElementIndex(locator, tblTabName);

                    if (tabIndex >= 0)
                    {
                        string jsToBeExecuted = GetTabStripReference();
                        string tabSelect = $"tab.select('{tabIndex.ToString()}');";
                        jsToBeExecuted = $"{jsToBeExecuted}{tabSelect}";
                        ExecuteJsScript(jsToBeExecuted);
                        Report.Step($"Clicked Table Tab - {tblTabName}");
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
            int index = -1;
            IList<IWebElement> elements;

            try
            {
                elements = new List<IWebElement>();
                elements = GetElements(findElementsLocator);

                for (int i = 0; i < elements.Count; i++)
                {
                    string spanText = elements[i].Text;
                    bool match = spanText.Equals(matchValue)
                        ? true
                        : false;

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

        public void RemoveFilters(TableType tableType = TableType.Unknown)
        {
            string jsToBeExecuted = this.GetGridReference(tableType);
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.filter([]);";
            ExecuteJsScript(jsToBeExecuted);
            Report.Info("Cleared all table filter(s)");
        }

        public int TotalNumberRows(TableType tableType = TableType.Unknown)
        {
            string jsToBeExecuted = this.GetGridReference(tableType);
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.total();";
            var jsResult = ExecuteJsScriptGet(jsToBeExecuted);
            return int.Parse(jsResult.ToString());
        }

        public void Reload(TableType tableType = TableType.Unknown)
        {
            string jsToBeExecuted = this.GetGridReference(tableType);
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.read();";
            ExecuteJsScript(jsToBeExecuted);
        }

        public int GetPageSize(TableType tableType = TableType.Unknown)
        {
            string jsToBeExecuted = this.GetGridReference(tableType);
            jsToBeExecuted = $"{jsToBeExecuted} return grid.dataSource.pageSize();";
            var currentResponse = ExecuteJsScriptGet(jsToBeExecuted);
            int pageSize = int.Parse(currentResponse.ToString());
            return pageSize;
        }

        public void ChangePageSize(int newSize, TableType tableType = TableType.Unknown)
        {
            string jsToBeExecuted = this.GetGridReference(tableType);
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.pageSize({newSize});";
            ExecuteJsScript(jsToBeExecuted);
        }

        public void GoToTablePage(int pageNumber, TableType tableType = TableType.Unknown)
        {
            ScrollToElement(By.XPath("//div[@data-role='pager']"));
            string jsToBeExecuted = this.GetGridReference(tableType);
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.page({pageNumber});";
            ExecuteJsScript(jsToBeExecuted);
            Report.Info($"Navigated to table page {pageNumber}");
        }

        public void Sort(string columnName, SortType sortType, TableType tableType = TableType.Unknown)
        {
            string jsToBeExecuted = this.GetGridReference(tableType);
            jsToBeExecuted = $"{jsToBeExecuted} grid.dataSource.sort({{field: '{columnName}', dir: '{sortType.GetString()}'}});";
            ExecuteJsScript(jsToBeExecuted);
            Report.Info($"Sorted {columnName} column to {sortType.ToString()} order");
        }

        public List<T> GetItems<T>(TableType tableType = TableType.Unknown) where T : class
        {
            string jsToBeExecuted = this.GetGridReference(tableType);
            jsToBeExecuted = $"{jsToBeExecuted} return JSON.stringify(grid.dataSource.data());";
            var jsResults = ExecuteJsScriptGet(jsToBeExecuted);
            var items = newtJson.Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(jsResults.ToString());
            return items;
        }

        public void FilterTableGrid(
            string columnName,
            string filterValue,
            FilterOperator filterOperator = FilterOperator.EqualTo,
            FilterLogic filterLogic = FilterLogic.And,
            string additionalFilterValue = null,
            FilterOperator additionalFilterOperator = FilterOperator.EqualTo,
            TableType tableType = TableType.Unknown
            ) => Filter( tableType,
                new GridFilter(
                    columnName,
                    filterOperator,
                    filterValue,
                    filterLogic,
                    additionalFilterValue,
                    additionalFilterOperator
                    )
                );

        private void Filter(TableType tableType, params GridFilter[] gridFilters)
        {
            string columnName = null;
            string filterValue = null;
            string filterOperator = null;
            string filterLogic = null;
            string addnlFilterOperator = null;
            string addnlFilterValue = null;
            string filterScript = null;

            try
            {
                foreach (var currentFilter in gridFilters)
                {
                    filterLogic = currentFilter.FilterLogic.GetString();
                    string jsFilterBase = $"grid.dataSource.filter({{ logic: '{filterLogic}', filters: [";

                    filterValue = currentFilter.FilterValue;
                    columnName = currentFilter.ColumnName;
                    filterOperator = currentFilter.FilterOperator.GetString();
                    filterScript = $"{jsFilterBase}{{ field: '{columnName}', operator: '{filterOperator}', value: '{filterValue}' }}";

                    addnlFilterValue = currentFilter.AdditionalFilterValue;
                    addnlFilterOperator = currentFilter.AdditionalFilterOperator.GetString();

                    filterScript = addnlFilterValue == null
                        ? filterScript 
                        : $"{filterScript},{{ field: '{columnName}', operator: '{addnlFilterOperator}', value: '{addnlFilterValue}' }}";
                }

                StringBuilder sb = new StringBuilder();

                string gridRef = GetGridReference(tableType);

                sb.Append($"{gridRef}{filterScript}] }});");
                ExecuteJsScript(sb.ToString());

                string addnlFilterLogMsg = addnlFilterValue == null
                    ? string.Empty
                    : $", Additional Filter - (Logic):{filterLogic}, (Operator):{addnlFilterOperator}, (Value):{addnlFilterValue}";

                Report.Step($"Filtered: (Column):{columnName}, (Operator):{filterOperator}, (Value):{filterValue} {addnlFilterLogMsg}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        public int GetCurrentPageNumber(TableType tableType = TableType.Unknown)
        {
            int pageNumber = 1;

            try
            {
                string jsToBeExecuted = GetGridReference(tableType);
                jsToBeExecuted = $"{jsToBeExecuted} return grid.dataSource.page();";
                IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
                var result = executor.ExecuteScript(jsToBeExecuted);
                pageNumber = int.Parse(result.ToString());
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            return pageNumber;
        }

        private string GetTabStripReference()
        {
            string tabStripRef = string.Empty;

            try
            {
                By tabStripLocator = By.XPath("//div[contains(@class,'k-tabstrip-top')]");
                string tabStripId = GetAttribute(tabStripLocator, "id");
                tabStripRef = $"var tab = $('#{tabStripId}').data('kendoTabStrip');";

                string logMsg = tabStripId.HasValue()
                    ? $"...Found Kendo Grid TabStrip ID: {tabStripId}"
                    : "!!!NULL Kendo Grid TabStrip ID";
                log.Debug(logMsg);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            return tabStripRef;
        }

        private string GetGridReference(TableType tableType)
        {
            string gridRef = string.Empty;

            try
            {
                string gridId = GetGridID(tableType);
                gridRef = $"var grid = $('#{gridId}').data('kendoGrid');";

                string logMsg = gridId.HasValue()
                    ? $"...Found Kendo Grid ID: {gridId}"
                    : "!!!NULL Kendo Grid ID";
                log.Debug(logMsg);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }

            return gridRef;
        }

        public string GetGridID(TableType tableType)
        {
            By singleGridDivLocator = By.XPath("//div[@class='k-widget k-grid k-display-block'][@data-role='grid']");
            By multiActiveGridDivLocator = By.XPath("//div[@class='k-content k-state-active']//div[@data-role='grid']");
            IWebElement gridElem = null;
            string gridId = string.Empty;

            try
            {
                switch (tableType)
                {
                    case TableType.Single:
                        gridElem = GetElement(singleGridDivLocator);
                        break;
                    case TableType.MultiTab:
                        gridElem = GetElement(multiActiveGridDivLocator);
                        break;
                    case TableType.Unknown:
                        gridElem = GetElement(multiActiveGridDivLocator) ?? GetElement(singleGridDivLocator);
                        break;
                }

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