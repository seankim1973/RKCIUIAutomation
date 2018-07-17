using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;

namespace RKCIUIAutomation.Page.PageObjects
{
    #region LoginPage Generic class
    public class LoginPage : LoginPage_Impl
    {
        public LoginPage() { }
        public LoginPage(IWebDriver driver) => this.driver = driver;
    }
    #endregion end of LoginPage Generic class


    #region LoginPage Interface class
    public interface ILoginPage
    {
        void LoginUser(UserType userType);
        void ToggleRememberMeChkbox();

        void LoginAsSysAdmin();
        void LoginAsProjAdmin();
        void LoginAsProjUser();
        void LoginAsIQFRecordsMgr();
        void LoginAsIQFAdmin();
        void LoginAsIQFUser();
        void LoginAsIQFDOTAdmin();
        void LoginAsIQFDOTUser();
        void LoginAsIQFDevAdmin();
        void LoginAsIQFDevUser();
    }
    #endregion end of LoginPage Interface class


    #region LoginPage Common Implementation class
    public abstract class LoginPage_Impl : PageBase, ILoginPage
    {
        internal readonly By field_Email = By.Name("Email");
        internal readonly By field_Password = By.Name("Password");
        internal readonly By chkbx_RememberMe = By.Name("RememberMe");
        internal readonly By btn_Login = By.XPath("//input[@type='submit']");
        internal int userAcctIndex = 0;
        internal string credential = string.Empty;

        public virtual void LoginUser(UserType userType)
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

        public virtual void ToggleRememberMeChkbox()
        {
            ClickElement(chkbx_RememberMe);
        }


        public virtual void LoginAsSysAdmin() => LoginUser(UserType.SysAdmin);
        public virtual void LoginAsProjAdmin() => LoginUser(UserType.ProjAdmin);
        public virtual void LoginAsProjUser() => LoginUser(UserType.ProjUser);

        public virtual void LoginAsIQFRecordsMgr() => LoginUser(UserType.IqfRecordsMgr);
        public virtual void LoginAsIQFAdmin() => LoginUser(UserType.IqfAdmin);
        public virtual void LoginAsIQFUser() => LoginUser(UserType.IqfUser);
        public virtual void LoginAsIQFDOTAdmin() => LoginUser(UserType.IqfDOTAdmin);
        public virtual void LoginAsIQFDOTUser() => LoginUser(UserType.IqfDOTUser);
        public virtual void LoginAsIQFDevAdmin() => LoginUser(UserType.IqfDevAdmin);
        public virtual void LoginAsIQFDevUser() => LoginUser(UserType.IqfDevUser);


        //Only SH249 and SG use IQF Records Mgr accts
        // --> SH249 use only IQFRecordsMgr, IQFAdmin, and IQFUser accts
        // --> SG uses all 7 IQF accts
        // --> Garnet and GLX do not use IQFRecordsMgr and IQFAdmin accts
        //I15Tech and I15South do not use IQF Accts


        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);
        private ILoginPage SetPageClassBasedOnTenant(IWebDriver driver)
        {
            ILoginPage instance = new LoginPage(driver);

            if (tenantName == TenantName.SGWay)
            {
                LogInfo($"###### using LoginPage_SGWay instance ###### ");
                instance = new LoginPage_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                LogInfo($"###### using LoginPage_SH249 instance ###### ");
                instance = new LoginPage_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                LogInfo($"###### using LoginPage_Garnet instance ###### ");
                instance = new LoginPage_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                LogInfo($"###### using LoginPage_GLX instance ###### ");
                instance = new LoginPage_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                LogInfo($"###### using LoginPage_I15South instance ###### ");
                instance = new LoginPage_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                LogInfo($"###### using LoginPage_I15Tech instance ###### ");
                instance = new LoginPage_I15Tech(driver);
            }
            return instance;
        }

    }
    #endregion end of LoginPage Common Implementation class



    #region Implementation specific to SGWay
    public class LoginPage_SGWay : LoginPage
    {
        public LoginPage_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }
    #endregion


    #region Implementation specific to SH249
    public class LoginPage_SH249 : LoginPage
    {
        public LoginPage_SH249(IWebDriver driver) : base(driver)
        {
        }
    }
    #endregion


    #region Implementation specific to Garnet
    public class LoginPage_Garnet : LoginPage
    {
        public LoginPage_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }
    #endregion


    #region Implementation specific to GLX
    public class LoginPage_GLX : LoginPage
    {
        public LoginPage_GLX(IWebDriver driver) : base(driver)
        {
        }
    }
    #endregion


    #region Implementation specific to I15South
    public class LoginPage_I15South : LoginPage
    {
        public LoginPage_I15South(IWebDriver driver) : base(driver)
        {
        }
    }
    #endregion


    #region Implementation specific to I15Tech
    public class LoginPage_I15Tech : LoginPage
    {
        public LoginPage_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }
    #endregion
    
}
