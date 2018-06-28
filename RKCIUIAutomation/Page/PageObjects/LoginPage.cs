using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using static RKCIUIAutomation.Base.WebDriverFactory;
using static RKCIUIAutomation.Config.ConfigUtil;
using static RKCIUIAutomation.Base.BaseUtils;
using static RKCIUIAutomation.Page.Action;

namespace RKCIUIAutomation.Page.PageObjects
{
    public class LoginPage : PageBase
    {
        public LoginPage(IWebDriver driver) => this.driver = driver;

        private By field_Email = By.Name("Email");
        private By field_Password = By.Name("Password");
        private By chkbx_RememberMe = By.Name("RememberMe");
        private By btn_Login = By.XPath("//input[@type='submit']");

        public void LoginUser(UserType userType)
        {
            string[] userAcct = configUtil.GetUser(userType);
            IList<By> loginFields = new List<By>
            {
                field_Email,
                field_Password
            };

            foreach (By field in loginFields)
            {
                string credential = String.Empty;
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
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20))
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
                    LogInfo($"Exception occured while waiting for element - {field}", e);
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
