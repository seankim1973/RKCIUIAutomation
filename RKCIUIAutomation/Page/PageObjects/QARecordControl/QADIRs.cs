using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;

namespace RKCIUIAutomation.Page.PageObjects.QARecordControl
{
    #region DIR/IDR/DWR Generic Class
    public class QADIRs : QADIRs_Impl
    {
        public QADIRs()
        {
        }

        public QADIRs(IWebDriver driver) => this.Driver = driver;
        public enum InputFields
        {
        }
        public enum TableTab
        {
            [StringValue("Creating")] Creating,
            [StringValue("Create/Revise")] Create_Revise,
            [StringValue("Attachments")] Attachments,
            [StringValue("QC Review")] QC_Review,
            [StringValue("Authorization")] Authorization,
            [StringValue("Revise")] Revise,
            [StringValue("To Be Closed")] To_Be_Closed,
            [StringValue("Closed")] Closed,
            [StringValue("Create Packages")] Create_Packages,
            [StringValue("Packages")] Packages
        }
    }
    #endregion DIR Generic class
    public interface IQADIRs
    {
        bool IsLoaded();
    }

    //public class QADIRs : QADIRs_Impl
    //{
    //    public QADIRs(IWebDriver driver) => this.Driver = driver;
    //}

    public abstract class QADIRs_Impl : TestBase, IQADIRs
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public T SetClass<T>() => (T)SetPageClassBasedOnTenant();

        public IQADIRs SetPageClassBasedOnTenant()
        {
            IQADIRs instance = new QADIRs(Driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using QADIRs_SGWay instance ###### ");
                instance = new QADIRs_SGWay(Driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using QADIRs_SH249 instance ###### ");
                instance = new QADIRs_SH249(Driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using QADIRs_Garnet instance ###### ");
                instance = new QADIRs_Garnet(Driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using QADIRs_GLX instance ###### ");
                instance = new QADIRs_GLX(Driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using QADIRs_I15South instance ###### ");
                instance = new QADIRs_I15South(Driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using QADIRs_I15Tech instance ###### ");
                instance = new QADIRs_I15Tech(Driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using QADIRs_LAX instance ###### ");
                instance = new QADIRs_LAX(Driver);
            }
            return instance;
        }

        public virtual bool IsLoaded() => Driver.Title.Equals("DIR List - ELVIS PMC");
    }

    public class QADIRs_Garnet : QADIRs
    {
        public QADIRs_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QADIRs_GLX : QADIRs
    {
        public QADIRs_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QADIRs_SH249 : QADIRs
    {
        public QADIRs_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QADIRs_SGWay : QADIRs
    {
        public QADIRs_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QADIRs_I15South : QADIRs
    {
        public QADIRs_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QADIRs_I15Tech : QADIRs
    {
        public QADIRs_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QADIRs_LAX : QADIRs
    {
        public QADIRs_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}