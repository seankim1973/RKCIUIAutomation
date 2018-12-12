using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;

namespace RKCIUIAutomation.Page.PageObjects.QASearch
{
    public class InspectionDeficiencyLogReport : InspectionDeficiencyLogReport_Impl
    {
        public InspectionDeficiencyLogReport()
        {
        }

        public InspectionDeficiencyLogReport(IWebDriver driver) => this.Driver = driver;
    }

    public interface IInspectionDeficiencyLogReport
    {
    }

    public class InspectionDeficiencyLogReport_Impl : TestBase, IInspectionDeficiencyLogReport
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Tenant'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        public IInspectionDeficiencyLogReport SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IInspectionDeficiencyLogReport instance = new InspectionDeficiencyLogReport(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_SGWay instance ###### ");
                instance = new InspectionDeficiencyLogReport_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_SH249 instance ###### ");
                instance = new InspectionDeficiencyLogReport_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_Garnet instance ###### ");
                instance = new InspectionDeficiencyLogReport_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_GLX instance ###### ");
                instance = new InspectionDeficiencyLogReport_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_I15South instance ###### ");
                instance = new InspectionDeficiencyLogReport_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_I15Tech instance ###### ");
                instance = new InspectionDeficiencyLogReport_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using InspectionDeficiencyLogReport_LAX instance ###### ");
                instance = new InspectionDeficiencyLogReport_LAX(driver);
            }
            return instance;
        }
    }

    public class InspectionDeficiencyLogReport_Garnet : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    public class InspectionDeficiencyLogReport_GLX : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    public class InspectionDeficiencyLogReport_SH249 : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    public class InspectionDeficiencyLogReport_SGWay : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    public class InspectionDeficiencyLogReport_I15South : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    public class InspectionDeficiencyLogReport_I15Tech : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    public class InspectionDeficiencyLogReport_LAX : InspectionDeficiencyLogReport
    {
        public InspectionDeficiencyLogReport_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}