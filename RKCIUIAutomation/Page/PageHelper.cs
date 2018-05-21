using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RKCIUIAutomation.Base;
using System;
using System.Reflection;
using System.Threading;

namespace RKCIUIAutomation.Page
{
    public class PageHelper : Action
    {
        private static string GetATagXpathString(Enum @enum) => $"//a[text()='{@enum.GetString()}']";
        public static By SetLocatorXpath(Enum @enum) => By.XPath(GetATagXpathString(@enum));
    }

    public class StringValueAttribute : Attribute
    {
        public StringValueAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }

    public static class BaseHelper
    {
        public static string GetString(this Enum value)
        {
            string output = null;

            try
            {
                Type type = value.GetType();
                FieldInfo fi = type.GetField(value.ToString());
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute),false) as StringValueAttribute[];
                output = attrs[0].Value;
            }
            catch (Exception)
            {
                throw;
            }
            return output;
        }
    }

    public class Action : BaseClass
    {
        public void HoverAndClick(By elemByToHover, By elemByToClick)
        {
            Hover(elemByToHover);
            ClickElement(elemByToClick);
        }

        public void Hover(By elementByLocator)
        {
            string javaScript = "var evObj = document.createEvent('MouseEvents');" +
            "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
            "arguments[0].dispatchEvent(evObj);";

            IJavaScriptExecutor executor = Driver as IJavaScriptExecutor;
            executor.ExecuteScript(javaScript, GetElement(elementByLocator));
            Thread.Sleep(1000);
        }

        public IWebElement GetElement(By elementByLocator)
        {
            IWebElement elem = null;
            try
            {
                elem = Driver.FindElement(elementByLocator);
                LogInfo($"Found element - {elementByLocator}");
            }
            catch (Exception e)
            {
                LogInfo($"Unable to locate element - {elementByLocator}", e);
            }
            return elem;
        }

        private bool WaitForElement(By elementByLocator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
                wait.PollingInterval = TimeSpan.FromMilliseconds(500);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
                IWebElement webElem = wait.Until(x => x.FindElement(elementByLocator));
                return webElem.Displayed;
            }
            catch (Exception e)
            {
                LogInfo($"WaitForElement timeout occured for element - {elementByLocator}", e);
            }
            return false;
        }

        public void ClickElement(By elementByLocator)
        {
            if (WaitForElement(elementByLocator))
            {
                try
                {
                    GetElement(elementByLocator).Click();
                    LogInfo($"Clicked element - {elementByLocator}");
                }
                catch (Exception e)
                {
                    LogInfo($"Unable to click element - {elementByLocator}", e);
                }
            }    
        }

        public void EnterText(By elementByLocator, string text)
        {
            if (WaitForElement(elementByLocator))
            {
                try
                {
                    GetElement(elementByLocator).SendKeys(text);
                    LogInfo($"Entered {text} in field - {elementByLocator}");
                }
                catch (Exception e)
                {
                    LogInfo($"Unable to enter text in field - {elementByLocator}", e);
                }
            }
        }

        public string GetText(By elementByLocator)
        {
            string text = null;

            if (WaitForElement(elementByLocator))
            {
                try
                {
                    text = GetElement(elementByLocator).Text;
                    LogInfo($"Retrieved text {text} from element - {elementByLocator}");
                }
                catch (Exception e)
                {
                    LogInfo($"Unable to retrieve text from element - {elementByLocator}", e);
                }
            }
            
            return text;
        }
    }
}
