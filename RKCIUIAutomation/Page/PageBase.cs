using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RKCIUIAutomation.Base;
using System;

namespace RKCIUIAutomation.Page
{
    public class PageBase : BaseClass
    {
        private static By link_Login = By.LinkText("Login");

        public LoginPage ClickLoginLink()
        {
            ClickElement(link_Login);
            return new LoginPage();
        }


        public void HoverOverElement(By elementByLocator)
        {
            WaitForElement(elementByLocator);
            Actions action = new Actions(Driver);
            action.MoveToElement(GetElement(elementByLocator)).Perform();
        }

        public IWebElement GetElement(By elementByLocator)
        {
            IWebElement elem = Driver.FindElement(elementByLocator);
            return elem;
        }
        
        private void WaitForElement(By elementByLocator)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(Driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(30);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
            IWebElement webElem = fluentWait.Until(x => x.FindElement(elementByLocator));
        }

        public void ClickElement(By elementByLocator)
        {
            WaitForElement(elementByLocator);
            GetElement(elementByLocator).Click();
        }

        public void EnterText(By elementByLocator, string text)
        {
            WaitForElement(elementByLocator);
            GetElement(elementByLocator).SendKeys(text);
        }

        public string GetText(By elementByLocator)
        {
            WaitForElement(elementByLocator);
            string text = GetElement(elementByLocator).Text;
            return text;
        }

    }
}
