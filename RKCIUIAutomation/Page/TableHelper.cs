using OpenQA.Selenium;

namespace RKCIUIAutomation.Page
{
    public class TableHelper : Action
    {
        enum RowButtonType
        {
            Report_View,
            WebForm_View,
            Attachments_View,
            Action_Edit
        }

        private string GetTableRowXpath<ColumnName,RowValue>(ColumnName columnName, RowValue rowValue) => $""; //TODO - review different types of tables to come up with logic

        //TODO - need to figure out logic for scenarios where View button in the attachment column is present/not present
        private string SetTableRowViewButtonXpath(string uniqueRowValue, RowButtonType rowButtonType)
        {
            string xpath = string.Empty;

            //if report_view or action_edit, use text()=""
            //if attachments, webform or report
            xpath = $"//td[text()='{uniqueRowValue}']/following-sibling::td/a[text()='View']";

            return xpath;
        }
        private string SetTableRowEditButtonXpath(string uniqueRowValue) => $"//td[text()='{uniqueRowValue}']/following-sibling::td/a[text()=' Edit']";
        private By GetTableRowButtonByLocator(string uniqueRowValue, RowButtonType rowButtonType) => By.XPath(SetTableRowViewButtonXpath(uniqueRowValue, rowButtonType));
        private By GetTablePageNavButton(string pageDestination) => By.XPath($"//a[@aria-label='Go to the {pageDestination} page']");

        public void GoToFirstPage() => ClickElement(GetTablePageNavButton("first"));
        public void GoToPreviousPage() => ClickElement(GetTablePageNavButton("previous"));
        public void GoToNextPage() => ClickElement(GetTablePageNavButton("next"));
        public void GoToLastPage() => ClickElement(GetTablePageNavButton("last"));
        public void GoToPageNumber(int pageNumber) => ClickElement(GetTableNavByLocator(pageNumber));

        public void ClickTableTab<T>(T tabEnum) => ClickElement(GetTableTabByLocator(ConvertToEnumType(tabEnum)));
        

        public void ClickViewButtonForRow(string uniqueRowValue) => ClickElement(GetTableRowButtonByLocator(uniqueRowValue, RowButtonType.Report_View));
        public void ClickEditButtonForRow(string uniqueRowValue) => ClickElement(GetTableRowButtonByLocator(uniqueRowValue, RowButtonType.Action_Edit));

    }
}
