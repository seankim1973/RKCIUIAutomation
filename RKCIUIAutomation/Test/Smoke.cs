using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.Navigation;
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

        //public void MoveToElem(IWebElement webElement)
        //{
        //    string javaScript = "var evObj = document.createEvent('MouseEvents');" +
        //    "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
        //    "arguments[0].dispatchEvent(evObj);";

        //    IJavaScriptExecutor executor = Driver as IJavaScriptExecutor;
        //    executor.ExecuteScript(javaScript, webElement);
        //    Thread.Sleep(1000);
        //}

        [Test, Property("TC#", "ELVS3456"), Property("Priority", "Priority 1")]
        [Category("RM Center")]
        [Description("Verify user can login successfully using project - user account")]
        public void GenericTest()
        {
            loginPage.LoginUser(UserType.ProjAdmin);
            navigate.Menu(NavEnums.RMCenter_e.Upload_QA_Submittal);
            navigate.Menu(NavEnums.RMCenter_e.Review_Revise_Submittal);
            navigate.Menu(NavEnums.RMCenter_e.RFC_Management);
            navigate.Menu(NavEnums.RMCenter_e.DOT_Project_Correspondence_Log);
            navigate.Menu(NavEnums.RMCenter_e.Upload_DEV_Submittal);
            navigate.Menu(NavEnums.RMCenter_e.Upload_Owner_Submittal);
            navigate.Menu(NavEnums.RMCenter_e.Search);
            Thread.Sleep(3000);
        }

    }
}
