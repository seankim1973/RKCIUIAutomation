using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page.PageObjects
{
    #region LoginPage Generic class

    public class LoginPage : LoginPage_Impl
    {
        public LoginPage()
        {
        }

        public LoginPage(IWebDriver driver) => this.Driver = driver;

        public override T SetClass<T>(IWebDriver driver)
        {
            ILoginPage instance = new LoginPage(driver);

            if (tenantName == TenantName.SGWay)
            {
                Report.Info($"###### using LoginPage_SGWay instance ###### ");
                instance = new LoginPage_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                Report.Info($"###### using LoginPage_SH249 instance ###### ");
                instance = new LoginPage_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                Report.Info($"###### using LoginPage_Garnet instance ###### ");
                instance = new LoginPage_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                Report.Info($"###### using LoginPage_GLX instance ###### ");
                instance = new LoginPage_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                Report.Info($"###### using LoginPage_I15South instance ###### ");
                instance = new LoginPage_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                Report.Info($"###### using LoginPage_I15Tech instance ###### ");
                instance = new LoginPage_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                Report.Info($"###### using LoginPage_LAX instance ###### ");
                instance = new LoginPage_LAX(driver);
            }
            return (T)instance;
        }

        internal readonly By field_Email = By.Name("Email");
        internal readonly By field_Password = By.Name("Password");
        internal readonly By chkbx_RememberMe = By.Name("RememberMe");
        internal readonly By btn_Login = By.XPath("//input[@type='submit']");
        internal int userAcctIndex = 0;
        internal string credential = string.Empty;

        public override bool AlreadyLoggedIn()
        {
            bool result = false;
            IWebElement elem = null;

            try
            {
                elem = driver.FindElement(By.XPath("//a[contains(text(), 'Login')]"));
                if (elem.Displayed)
                {
                    result = false;
                }
            }
            catch (Exception)
            {
                try
                {
                    elem = driver.FindElement(By.XPath("//a[contains(text(), 'Log out')]"));
                    if (elem.Displayed)
                    {
                        result = true;
                    }
                }
                catch (Exception)
                {
                }
            }

            return result;
        }

        public override void LoginUser(UserType userType)
        {
            PageAction.WaitForPageReady();
            bool alreadyLoggedIn = AlreadyLoggedIn();

            try
            {
                if (!alreadyLoggedIn)
                {
                    pageTitle = PageAction.GetPageTitle();

                    if (pageTitle.Contains("Log in"))
                    {
                        PageAction.VerifyPageIsLoaded(true, false);

                        string[] userAcct = ConfigUtil.GetUser(userType);
                        IList<By> loginFields = new List<By>
                        {
                            field_Email,
                            field_Password
                        };

                        foreach (By field in loginFields)
                        {
                            userAcctIndex = field == field_Email
                                ? 0
                                : 1;

                            credential = userAcct[userAcctIndex];

                            try
                            {
                                log.Info($"...waiting for element {field}");
                                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2))
                                {
                                    PollingInterval = TimeSpan.FromMilliseconds(250)
                                };
                                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                                wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
                                IWebElement webElem = wait.Until(x => x.FindElement(field));

                                if (userAcctIndex == 1)
                                {
                                    webElem.SendKeys(ConfigUtil.GetDecryptedPW(credential));
                                }
                                else
                                {
                                    webElem.SendKeys(credential);
                                }

                            }
                            catch (Exception e)
                            {
                                Report.Error($"Exception occured while waiting for element - {field}", true, e);
                                throw;
                            }
                        }

                        Report.Step($"Using account : {userAcct[0]}");
                        PageAction.ClickElement(btn_Login);
                    }
                }
                else
                {
                    Report.Step("Already Logged In");
                }
            }
            catch (Exception e)
            {
                log.Error($"Error occured in LogingUser() : {e.Message}");
            }
            finally
            {
                try
                {
                    pageTitle = PageAction.GetPageTitle();

                    if (pageTitle.Contains("Log in"))
                    {
                        bool invalidLoginErrorDisplayed = false;

                        IWebElement invalidLoginError = PageAction.GetElement(By.XPath("//div[@class='validation-summary-errors text-danger']/ul/li"));
                        invalidLoginErrorDisplayed = invalidLoginError.Displayed;

                        if (invalidLoginError != null && invalidLoginErrorDisplayed)
                        {
                            string logMsg = invalidLoginError.Text;

                            var ex = new Exception(logMsg.HasValue() ? logMsg : "Invalid Login Error is Displayed!!!");
                            Report.Error(logMsg, true);
                            throw ex;
                        }
                    }
                }
                catch (Exception e)
                {
                    log.Error($"ERROR in LoginAs method : {e.StackTrace}");
                }
            }
        }

        public override void ToggleRememberMeChkbox()
            => PageAction.ClickElement(chkbx_RememberMe);

    }

    #endregion LoginPage Generic class

    #region LoginPage Interface class

    public interface ILoginPage
    {
        bool AlreadyLoggedIn();

        void LoginUser(UserType userType);

        void ToggleRememberMeChkbox();
    }

    #endregion LoginPage Interface class

    #region LoginPage Common Implementation class

    public abstract class LoginPage_Impl : PageBase, ILoginPage
    {
        public abstract bool AlreadyLoggedIn();
        public abstract void LoginUser(UserType userType);
        public abstract void ToggleRememberMeChkbox();
    }

    #endregion LoginPage Common Implementation class

    #region Implementation specific to SGWay

    public class LoginPage_SGWay : LoginPage
    {
        public LoginPage_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to SGWay

    #region Implementation specific to SH249

    public class LoginPage_SH249 : LoginPage
    {
        public LoginPage_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to SH249

    #region Implementation specific to Garnet

    public class LoginPage_Garnet : LoginPage
    {
        public LoginPage_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to Garnet

    #region Implementation specific to GLX

    public class LoginPage_GLX : LoginPage
    {
        public LoginPage_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to GLX

    #region Implementation specific to I15South

    public class LoginPage_I15South : LoginPage
    {
        public LoginPage_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15South

    #region Implementation specific to I15Tech

    public class LoginPage_I15Tech : LoginPage
    {
        public LoginPage_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15Tech

    #region Implementation specific to LAX

    public class LoginPage_LAX : LoginPage
    {
        public LoginPage_LAX(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to LAX
}