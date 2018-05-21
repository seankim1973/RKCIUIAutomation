using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using RKCIUIAutomation.Config;
using System.Threading;

namespace RKCIUIAutomation.Test
{
    [TestFixture]
    public class Smoke : TestBase
    {
        //[Test, Property("TC#", "ELVS2345"), Property("Priority", "Priority 1")]
        [Category("RM Center")]
        [Description("Verify user can login successfully using project - admin account")]
        public void VerifyUserCanLogin_ProjAdmin()
        {
            loginPage.LoginUser(UserType.ProjAdmin);
        }


        //[Test, Property("TC#","ELVS1234"), Property("Priority", "Priority 2")]
        [Category("QA Test")]
        [Description("Verify user can login successfully using project - user account")]
        public void VerifyUserCanLogin_ProjUser()
        {
            loginPage.LoginUser(UserType.ProjAdmin);
        }

        public void MoveToElem(IWebElement webElement)
        {
            string javaScript = "var evObj = document.createEvent('MouseEvents');" +
            "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
            "arguments[0].dispatchEvent(evObj);";

            //IWebElement webElement = Driver.FindElement(elemByLocator);
            IJavaScriptExecutor executor = Driver as IJavaScriptExecutor;
            executor.ExecuteScript(javaScript, webElement);
        }

        [Test, Property("TC#", "ELVS3456"), Property("Priority", "Priority 1")]
        [Category("RM Center")]
        [Description("Verify user can login successfully using project - user account")]
        public void GenericTest()
        {
            loginPage.LoginUser(UserType.ProjAdmin);
            //projectMenu.NavigateToMyDetails();

            //Actions actions = new Actions(Driver);
            //ActionBuilder builder = new ActionBuilder();

            IWebElement projMenu = Driver.FindElement(By.XPath("//a[text()='Project']"));
            IWebElement myDetailsMenu = Driver.FindElement(By.XPath("//a[text()='My Details']"));

            MoveToElem(projMenu);
            Thread.Sleep(1000);
            MoveToElem(myDetailsMenu);
            Thread.Sleep(1000);
            myDetailsMenu.Click();
            
            //actions.MoveToElement(projMenu).Click(myDetailsMenu).Build().Perform();
            Thread.Sleep(3000);
        }

    }
}
