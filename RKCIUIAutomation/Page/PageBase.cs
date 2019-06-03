using OpenQA.Selenium;

namespace RKCIUIAutomation.Page
{
    public interface IPageBase
    {
        T SetClass<T>(IWebDriver driver);
    }

    public abstract class PageBase : PageHelper, IPageBase
    {
        public PageBase()
        {
        }

        public PageBase(IWebDriver driver) => this.Driver = driver;

        public abstract T SetClass<T>(IWebDriver driver);


    }
}