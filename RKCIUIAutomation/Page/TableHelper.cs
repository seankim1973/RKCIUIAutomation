using OpenQA.Selenium;
using RKCIUIAutomation.Base;

namespace RKCIUIAutomation.Page
{
    public class TableHelper : PageHelper
    {
        public TableHelper(){}
        public TableHelper(IWebDriver driver) => Driver = driver;

        private static By Btn_GoToFirstPg { get; } = By.XPath("//a[@aria-label='Go to the first page']");
        private static By Btn_GoToPrevPg { get; } = By.XPath("//a[@aria-label='Go to the previous page']");
        private static By Btn_GoToNextPg { get; } = By.XPath("//a[@aria-label='Go to the next page']");
        private static By Btn_GoToLastPg { get; } = By.XPath("//a[@aria-label='Go to the last page']");

        private static readonly string xpathExt_btnView = "//a[text()='View']";
        private static readonly string xpathExt_btnDelete = "//a[text()='Delete']";
        private static readonly string xpathExt_btnEdit = "//a[text()='Edit']";

        public void GoToFirstPage() => ClickElement(Btn_GoToFirstPg);
        public void GoToPreviousPage() => ClickElement(Btn_GoToPrevPg);
        public void GoToNextPage() => ClickElement(Btn_GoToNextPg);
        public void GoToLastPage() => ClickElement(Btn_GoToLastPg);
        public void GoToPageNumber(int pageNumber) => ClickElement(GetTableNavByLocator(pageNumber));
        public void SelectTableTab<T>(T tabEnum) => ClickElement(GetTableTabByLocator(ConvertToEnumType(tabEnum)));

    }
}
