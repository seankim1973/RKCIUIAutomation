using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page.PageObjects.LabFieldTests
{
    public class TestDetails : TestDetails_Impl
    {
        public TestDetails()
        {
        }

        public TestDetails(IWebDriver driver) => this.Driver = driver;

        public override T SetClass<T>(IWebDriver driver)
        { 
            ITestDetails instance = new TestDetails(driver);

            if (tenantName == TenantNameType.SGWay)
            {
                log.Info($"###### using Navigation_SGWay instance ###### ");
                instance = new TestDetails_SGWay(driver);
            }
            else if (tenantName == TenantNameType.SH249)
            {
                log.Info($"###### using Navigation_SH249 instance ###### ");
                instance = new TestDetails_SH249(driver);
            }
            else if (tenantName == TenantNameType.Garnet)
            {
                log.Info($"###### using Navigation_Garnet instance ###### ");
                instance = new TestDetails_Garnet(driver);
            }
            else if (tenantName == TenantNameType.GLX)
            {
                log.Info($"###### using Navigation_GLX instance ###### ");
                instance = new TestDetails_GLX(driver);
            }
            else if (tenantName == TenantNameType.I15South)
            {
                log.Info($"###### using Navigation_I15South instance ###### ");
                instance = new TestDetails_I15South(driver);
            }
            else if (tenantName == TenantNameType.I15Tech)
            {
                log.Info($"###### using Navigation_I15Tech instance ###### ");
                instance = new TestDetails_I15Tech(driver);
            }
            else if (tenantName == TenantNameType.LAX)
            {
                log.Info($"###### using Navigation_LAX instance ###### ");
                instance = new TestDetails_LAX(driver);
            }

            return (T)instance;
        }


        readonly By TestDetailsFormByLocator = By.Id("StartDiv");

        public override bool VerifyTestDetailsFormIsDisplayed()
            => PageAction.ElementIsDisplayed(TestDetailsFormByLocator);
    }

    public interface ITestDetails
    {
        bool VerifyTestDetailsFormIsDisplayed();
    }

    public abstract class TestDetails_Impl : PageBase, ITestDetails
    {
        public abstract bool VerifyTestDetailsFormIsDisplayed();
    }

    internal class TestDetails_LAX : TestDetails
    {
        public TestDetails_LAX(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class TestDetails_I15Tech : TestDetails
    {
        public TestDetails_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class TestDetails_I15South : TestDetails
    {
        public TestDetails_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class TestDetails_GLX : TestDetails
    {
        public TestDetails_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class TestDetails_Garnet : TestDetails
    {
        public TestDetails_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class TestDetails_SH249 : TestDetails
    {
        public TestDetails_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class TestDetails_SGWay : TestDetails
    {
        public TestDetails_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }
}