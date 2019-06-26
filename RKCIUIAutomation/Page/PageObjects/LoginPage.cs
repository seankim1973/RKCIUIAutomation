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

        public override bool LogOutLinkIsDisplayed()
        {
            try
            {
                IWebElement elem = GetElement(By.XPath("//a[contains(text(), 'Log out')]"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void LoginUser(UserType userType)
        {
            try
            {
                PageAction.WaitForPageReady(60, 10000, false);
                pageTitle = PageAction.GetPageTitle();

                if (pageTitle.Contains("Log in"))
                {
                    PageAction.VerifyPageIsLoaded(true, false);

                    string credential = string.Empty;
                    string[] userAcct = ConfigUtil.GetUserCredentials(userType);

                    IList<By> loginFields = new List<By>
                        {
                            field_Email,
                            field_Password
                        };

                    foreach (By field in loginFields)
                    {
                        log.Info($"...waiting for element {field}");
                        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2))
                        {
                            PollingInterval = TimeSpan.FromMilliseconds(250)
                        };
                        wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                        wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
                        IWebElement webElem = wait.Until(x => x.FindElement(field));

                        if (field == field_Email)
                        {
                            credential = userAcct[0];
                            Report.Step($"Using account : {credential}");
                        }
                        else
                        {
                            credential = ConfigUtil.GetDecryptedPW(userAcct[1]);
                        }

                        webElem.SendKeys(credential);
                    }

                    PageAction.ClickElement(btn_Login);

                    pageTitle = PageAction.GetPageTitle();

                    if (pageTitle.Contains("Log in"))
                    {
                        string logMsg = string.Empty;
                        IWebElement invalidLoginError = null;
                        bool invalidLoginErrorIsDisplayed = true;

                        try
                        {
                            invalidLoginError = driver.FindElement(By.XPath("//div[@class='validation-summary-errors text-danger']/ul/li"));
                        }
                        catch (NoSuchElementException)
                        {
                            invalidLoginErrorIsDisplayed = false;
                        }

                        if (invalidLoginErrorIsDisplayed)
                        {
                            if ((invalidLoginError.Text).HasValue())
                            {
                                logMsg = invalidLoginError.Text;
                            }
                            else
                            {
                                logMsg = "Invalid Login Error is Displayed!!!";
                            }

                            Report.Error(logMsg, true);
                            throw new InvalidOperationException(logMsg);
                        }
                    }
                    else
                    {
                        ConfigUtil.SetCurrentUserEmail(userType);
                    }
                }
                else if (LogOutLinkIsDisplayed())
                {
                    Report.Step($"Already Logged in as {GetCurrentUser()}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void ToggleRememberMeChkbox()
            => PageAction.ClickElement(chkbx_RememberMe);

    }

    #endregion LoginPage Generic class

    #region LoginPage Interface class

    public interface ILoginPage
    {
        bool LogOutLinkIsDisplayed();

        void LoginUser(UserType userType);

        void ToggleRememberMeChkbox();
    }

    #endregion LoginPage Interface class

    #region LoginPage Common Implementation class

    public abstract class LoginPage_Impl : PageBase, ILoginPage
    {
        public abstract bool LogOutLinkIsDisplayed();
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