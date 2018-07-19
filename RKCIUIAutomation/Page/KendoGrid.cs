using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace RKCIUIAutomation.Page
{
    public class KendoGrid : Action
    {
        private readonly string gridId;
        private readonly string tabStripId;
        private new readonly IJavaScriptExecutor driver;

        public KendoGrid(string gridDiv)
        {
            this.gridId = gridDiv;
        }

        public KendoGrid(IWebDriver driver, IWebElement gridDiv)
        {
            this.gridId = gridDiv.GetAttribute("id");
            this.driver = (IJavaScriptExecutor)driver;
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
            this.driver.ExecuteScript(jsToBeExecuted);
        }

        //Get single table gridId - GetElement(By.XPath("//div[contains(@class, 'k-widget k-grid')]")).GetAttribute("id");
        //Get multi table gridId - GetElement(By.XPath("//div[@aria-expanded='true']/div[contains(@class, 'k-widget k-grid')]")).GetAttribute("id");
        //Get tabStripId - GetElement(By.XPath("//div[contains(@class,'k-tabstrip-top')]")).GetAttribute("id");
        //FilterColumn - 


        public void RemoveFilters()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.filter([]);");
            this.driver.ExecuteScript(jsToBeExecuted);
        }

        public int TotalNumberRows()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.total();");
            var jsResult = this.driver.ExecuteScript(jsToBeExecuted);
            return int.Parse(jsResult.ToString());
        }

        public void Reload()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.read();");
            this.driver.ExecuteScript(jsToBeExecuted);
        }

        public int GetPageSize()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return grid.dataSource.pageSize();");
            var currentResponse = this.driver.ExecuteScript(jsToBeExecuted);
            int pageSize = int.Parse(currentResponse.ToString());
            return pageSize;
        }

        public void ChangePageSize(int newSize)
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.pageSize(", newSize, ");");
            this.driver.ExecuteScript(jsToBeExecuted);
        }

        public void NavigateToPage(int pageNumber)
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.page(", pageNumber, ");");
            this.driver.ExecuteScript(jsToBeExecuted);
        }

        public void Sort(string columnName, SortType sortType)
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.sort({field: '", columnName, "', dir: '", sortType.GetString(), "'});");
            //jsToBeExecuted = string.Concat(jsToBeExecuted, "grid.dataSource.sort({field: '", columnName, "', dir: '", sortType.ToString().ToLower(), "'});");
            this.driver.ExecuteScript(jsToBeExecuted);
        }

        public List<T> GetItems<T>() where T : class
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return JSON.stringify(grid.dataSource.data());");
            var jsResults = this.driver.ExecuteScript(jsToBeExecuted);
            var items = JsonConvert.DeserializeObject<List<T>>(jsResults.ToString());
            return items;
        }

        public void Filter(string columnName, FilterOperator filterOperator, string filterValue, FilterLogic filterLogic = FilterLogic.And, string additionalFilterValue = null, FilterOperator additionalFilterOperator = FilterOperator.EqualTo)
        {
            this.Filter(new GridFilter(columnName, filterOperator, filterValue, filterLogic, additionalFilterValue, additionalFilterOperator));
        }

        public void Filter(params GridFilter[] gridFilters)
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
            this.driver.ExecuteScript(sb.ToString());
        }

        public int GetCurrentPageNumber()
        {
            string jsToBeExecuted = this.GetGridReference();
            jsToBeExecuted = string.Concat(jsToBeExecuted, "return grid.dataSource.page();");
            var result = this.driver.ExecuteScript(jsToBeExecuted);
            int pageNumber = int.Parse(result.ToString());
            return pageNumber;
        }


        private string GetTabStripReference(Enum tblTabEnum)
        {
            string tabStripId = GetElement(By.XPath("//div[contains(@class,'k-tabstrip-top')]")).GetAttribute("id"); ;
            string initializeKendoTabStrip = $"var tab = $('#{tabStripId}').data('kendoTabStrip');";
            return initializeKendoTabStrip;
        }

        private string GetGridReference()
        {
            string initializeKendoGrid = $"var grid = $('#{gridId}').data('kendoGrid');";
            return initializeKendoGrid;
        }

        //private string ConvertFilterOperatorToKendoOperator(FilterOperator filterOperator)
        //{
        //    string kendoFilterOperator = string.Empty;
        //    switch (filterOperator)
        //    {
        //        case FilterOperator.EqualTo:
        //            kendoFilterOperator = "eq";
        //            break;
        //        case FilterOperator.NotEqualTo:
        //            kendoFilterOperator = "neq";
        //            break;
        //        case FilterOperator.LessThan:
        //            kendoFilterOperator = "lt";
        //            break;
        //        case FilterOperator.LessThanOrEqualTo:
        //            kendoFilterOperator = "lte";
        //            break;
        //        case FilterOperator.GreaterThan:
        //            kendoFilterOperator = "gt";
        //            break;
        //        case FilterOperator.GreaterThanOrEqualTo:
        //            kendoFilterOperator = "gte";
        //            break;
        //        case FilterOperator.StartsWith:
        //            kendoFilterOperator = "startswith";
        //            break;
        //        case FilterOperator.EndsWith:
        //            kendoFilterOperator = "endswith";
        //            break;
        //        case FilterOperator.Contains:
        //            kendoFilterOperator = "contains";
        //            break;
        //        case FilterOperator.NotContains:
        //            kendoFilterOperator = "doesnotcontain";
        //            break;
        //        case FilterOperator.IsAfter:
        //            kendoFilterOperator = "gt";
        //            break;
        //        case FilterOperator.IsAfterOrEqualTo:
        //            kendoFilterOperator = "gte";
        //            break;
        //        case FilterOperator.IsBefore:
        //            kendoFilterOperator = "lt";
        //            break;
        //        case FilterOperator.IsBeforeOrEqualTo:
        //            kendoFilterOperator = "lte";
        //            break;
        //        default:
        //            throw new ArgumentException("The specified filter operator is not supported.");
        //    }

        //    return kendoFilterOperator;
        //}

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
