using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.Navigation;

namespace RKCIUIAutomation.Page
{
    public class LoginPage : PageBase
    {
        private static By field_Email = By.Name("Email");
        private static By field_Password = By.Name("Password");

        public PageNav LoginUser(UserType userType)
        {
            string[] userAcct = GetUser(userType);
            EnterText(field_Email, userAcct[0]);
            EnterText(field_Password, userAcct[1]);
            return new PageNav();
        }

    }
}
