using OpenQA.Selenium;

using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Page.PageHelper;

namespace RKCIUIAutomation.Page
{
    public static class TableHelper
    {
        private static By GetTablePageNavButton(string pageDestination) => By.XPath($"//a[@aria-label='Go to the {pageDestination} page']");
        private static string GetTableRowXpath<ColumnName,RowValue>(ColumnName columnName, RowValue rowValue) => $""; //TODO - review different types of tables to come up with logic

        private static readonly string xpathExt_btnView = "//a[text()='View']";
        private static readonly string xpathExt_btnDelete = "//a[text()='Delete']";
        private static readonly string xpathExt_btnEdit = "//a[text()='Edit']";

        public static void GoToFirstPage() => ClickElement(GetTablePageNavButton("first"));
        public static void GoToPreviousPage() => ClickElement(GetTablePageNavButton("previous"));
        public static void GoToNextPage() => ClickElement(GetTablePageNavButton("next"));
        public static void GoToLastPage() => ClickElement(GetTablePageNavButton("last"));
        public static void GoToPageNumber(int pageNumber) => ClickElement(GetTableNavByLocator(pageNumber));

        public static void SelectTableTab<T>(T tabEnum) => ClickElement(GetTableTabByLocator(ConvertToEnumType(tabEnum)));

        //public static void ClickViewButtonForRow() => ClickElement();

    }
}
