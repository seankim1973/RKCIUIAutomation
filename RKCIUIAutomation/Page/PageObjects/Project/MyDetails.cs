using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.PageObjects.Project
{
    public class MyDetails : PageBase
    {
        public MyDetails(IWebDriver driver) => this.Driver = driver;

        public override T SetClass<T>(IWebDriver driver)
        {
            throw new System.NotImplementedException();
        }

    }
}