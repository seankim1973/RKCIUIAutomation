using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RKCIUIAutomation.Base;
using System;
using System.Reflection;

namespace RKCIUIAutomation.Page
{
    public class PageHelper : Action
    {
        public static string GetATagXpathString(Enum @enum) => $"//a[text()='{@enum.GetString()}']";
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

    public static class EnumHelper
    {
        public static string GetString(this Enum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            StringValueAttribute[] attrs = fi.GetCustomAttributes(false) as StringValueAttribute[];
            output = attrs[0].Value;
            return output;
        }
    }

    public class Action : BaseClass
    {
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

        private bool WaitForElement(By elementByLocator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
                wait.PollingInterval = TimeSpan.FromMilliseconds(250);
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
                IWebElement webElem = wait.Until(x => x.FindElement(elementByLocator));
                return webElem.Displayed;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            return false;
        }

        public void ClickElement(By elementByLocator)
        {
            if (WaitForElement(elementByLocator))
            {
                GetElement(elementByLocator).Click();
            }
            else
            {
                Console.Out.WriteLine("Click element not found");
            }
        }

        public void EnterText(By elementByLocator, string text)
        {
            if (WaitForElement(elementByLocator))
            {
                GetElement(elementByLocator).SendKeys(text);
            }
            else
            {
                Console.Out.WriteLine("TextField element not found");
            }
        }

        public string GetText(By elementByLocator)
        {
            string text = null;

            if (WaitForElement(elementByLocator))
            {
                text = GetElement(elementByLocator).Text;
            }
            else
            {
                Console.Out.WriteLine("TextField element not found");
            }
            return text;
        }

    }
}
