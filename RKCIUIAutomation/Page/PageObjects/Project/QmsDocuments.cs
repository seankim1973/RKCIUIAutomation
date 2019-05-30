using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.PageObjects.Project
{
    public class QmsDocuments : PageBase
    {
        public QmsDocuments(IWebDriver driver) => this.Driver = driver;

        public override T SetClass<T>(IWebDriver driver)
        {
            throw new System.NotImplementedException();
        }
    }
}