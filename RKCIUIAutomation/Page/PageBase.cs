using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Page.PageObjects;

using static RKCIUIAutomation.Page.Action;

namespace RKCIUIAutomation.Page
{
    public class PageBase : BaseClass
    {
        private readonly By link_Login = By.XPath("//a[text()=' Login']");
        private readonly By link_Logout = By.XPath("//a[text()=' Log out']");

        public LoginPage ClickLoginLink()
        {
            ClickElement(link_Login);
            return new LoginPage(Driver);
        }

        public LandingPage ClickLogoutLink()
        {
            ClickElement(link_Logout);
            return new LandingPage();
        }



    }

}
