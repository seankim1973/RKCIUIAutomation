using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.PageObjects.LabFieldTests
{
    public class TestDetails : PageBase
    {
        public TestDetails(IWebDriver driver) => this.Driver = driver;

        public readonly By TestDetailsFormByLocator = By.Id("StartDiv");

        public bool VerifyTestDetailsFormIsDisplayed() => ElementIsDisplayed(TestDetailsFormByLocator);
    }
}