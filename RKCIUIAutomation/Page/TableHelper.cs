using OpenQA.Selenium;
using RKCIUIAutomation.Test;
using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Page.PageHelper;

namespace RKCIUIAutomation.Page
{
    public class TableHelper : TestBase
    {
        private By GetTablePageNavButton(string pageDestination) => By.XPath($"//a[@aria-label='Go to the {pageDestination} page']");
        private string GetTableRowXpath<ColumnName,RowValue>(ColumnName columnName, RowValue rowValue) => $""; //TODO - review different types of tables to come up with logic

        private readonly string xpathExt_btnView = "//a[text()='View']";
        private readonly string xpathExt_btnDelete = "//a[text()='Delete']";
        private readonly string xpathExt_btnEdit = "//a[text()='Edit']";

        public void GoToFirstPage() => ClickElement(GetTablePageNavButton("first"));
        public void GoToPreviousPage() => ClickElement(GetTablePageNavButton("previous"));
        public void GoToNextPage() => ClickElement(GetTablePageNavButton("next"));
        public void GoToLastPage() => ClickElement(GetTablePageNavButton("last"));
        public void GoToPageNumber(int pageNumber) => ClickElement(GetTableNavByLocator(pageNumber));

        public void SelectTableTab<T>(T tabEnum) => ClickElement(GetTableTabByLocator(ConvertToEnumType(tabEnum)));

        //public static void ClickViewButtonForRow() => ClickElement();

    }
}
