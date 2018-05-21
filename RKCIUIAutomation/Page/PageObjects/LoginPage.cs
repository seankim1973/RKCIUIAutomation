using OpenQA.Selenium;
using RKCIUIAutomation.Config;

namespace RKCIUIAutomation.Page.PageObjects
{
    public class LoginPage : PageBase
    {
        public LoginPage()
        { }
        public LoginPage(IWebDriver driver) => Driver = driver;

        private static By field_Email = By.Name("Email");
        private static By field_Password = By.Name("Password");
        private static By chkbx_RememberMe = By.Name("RememberMe");
        private static By btn_Login = By.XPath("//input[@type='submit']");

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
