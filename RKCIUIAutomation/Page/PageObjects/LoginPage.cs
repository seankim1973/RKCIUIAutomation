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

        public void LoginUser(UserType userType)
        {
            VerifyPageIsLoaded(false);

            string[] userAcct = Configs.GetUser(userType);
            IList<By> loginFields = new List<By>
            {
                field_Email,
                field_Password
            };

            foreach (By field in loginFields)
            {
                string credential = string.Empty;
                int userAcctIndex = 1;
                IWebElement webElem = null;

                if (field == field_Email)
                {
                    userAcctIndex = 0;
                    LogInfo($"Using account : {userAcct[0]}");
                }

                credential = userAcct[userAcctIndex];

                try
                {
                    LogInfo($"...waiting for element {field}");
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
                    {
                        PollingInterval = TimeSpan.FromMilliseconds(500)
                    };
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
                    webElem = wait.Until(x => x.FindElement(field));
                    webElem.SendKeys(credential);
                }
                catch (Exception e)
                {
                    LogInfo($"Exception occured while waiting for element - {field}", false, e);
                    throw;
                }
            }
            
            ClickElement(btn_Login);
        }

        public void ToggleRememberMeChkbox()
        {
            ClickElement(chkbx_RememberMe);
        }
    }
}
