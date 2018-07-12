using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;

namespace RKCIUIAutomation.Page.PageObjects
{
    public class LoginPage : PageBase
    {
        public LoginPage(IWebDriver driver) => this.driver = driver;

        private readonly By field_Email = By.Name("Email");
        private readonly By field_Password = By.Name("Password");
        private readonly By chkbx_RememberMe = By.Name("RememberMe");
        private readonly By btn_Login = By.XPath("//input[@type='submit']");
        private int userAcctIndex = 0;
        private string credential = string.Empty;

        public void LoginUser(UserType userType)
        {
            VerifyPageIsLoaded(true, false);

            ConfigUtils Configs = new ConfigUtils();
            string[] userAcct = Configs.GetUser(userType);
            IList<By> loginFields = new List<By>
            {
                field_Email,
                field_Password
            };

            foreach (By field in loginFields)
            {              
                userAcctIndex = (field == field_Email) ? 0 : 1;
                IWebElement webElem = null;

                credential = userAcct[userAcctIndex];

                try
                {
                    LogInfo($"...waiting for element {field}");
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2))
                    {
                        PollingInterval = TimeSpan.FromMilliseconds(250)
                    };
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
                    webElem = wait.Until(x => x.FindElement(field));
                    webElem.SendKeys(credential);
                }
                catch (Exception e)
                {
                    LogError($"Exception occured while waiting for element - {field}", true, e);
                    throw;
                }
            }

            LogInfo($"Using account : {userAcct[0]}");
            ClickElement(btn_Login);
        }

        public void ToggleRememberMeChkbox()
        {
            ClickElement(chkbx_RememberMe);
        }
    }
}
