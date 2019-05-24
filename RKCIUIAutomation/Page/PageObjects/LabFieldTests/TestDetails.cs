using OpenQA.Selenium;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page.PageObjects.LabFieldTests
{
    public class TestDetails : PageBase, ITestDetails
    {
        public TestDetails()
        {
        }

        public TestDetails(IWebDriver driver) => this.Driver = driver;

        public readonly By TestDetailsFormByLocator = By.Id("StartDiv");

        public bool VerifyTestDetailsFormIsDisplayed()
            => PageAction.ElementIsDisplayed(TestDetailsFormByLocator);
    }
}