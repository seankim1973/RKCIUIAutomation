using OpenQA.Selenium;
using RKCIUIAutomation.Config;

using static RKCIUIAutomation.Base.WebDriverFactory;
using static RKCIUIAutomation.Config.ConfigUtil;
using static RKCIUIAutomation.Page.Action;

namespace RKCIUIAutomation.Page.PageObjects
{
    public class LoginPage : PageBase
    {
        public LoginPage()
        { }
        public LoginPage(IWebDriver driver) => Driver = driver;

        private static readonly By field_Email = By.Name("Email");
        private static readonly By field_Password = By.Name("Password");
        private static readonly By chkbx_RememberMe = By.Name("RememberMe");
        private static readonly By btn_Login = By.XPath("//input[@type='submit']");

        public void LoginUser(UserType userType)
        {
            string[] userAcct = GetUser(userType);
            EnterText(field_Email, userAcct[0]);
            EnterText(field_Password, userAcct[1]);
            ClickElement(btn_Login);
            //return new LandingPage();
        }

        public void ToggleRememberMeChkbox()
        {
            ClickElement(chkbx_RememberMe);
        }

    }
}
