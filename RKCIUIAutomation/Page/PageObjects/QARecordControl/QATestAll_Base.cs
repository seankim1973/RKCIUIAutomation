using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page.PageObjects.QARecordControl
{
    public class QATestAllBase_Common : QATestAllBase
    {
        public QATestAllBase_Common()
        {
        }

        public QATestAllBase_Common(IWebDriver driver) => this.Driver = driver;

        public override T SetClass<T>(IWebDriver driver)
        {
            IQATestAllBase instance = new QATestAllBase_Common(driver);

            if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using QATestAllBase_LAX instance ###### ");
                instance = new LAX(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using QATestAllBase_SH249 instance ###### ");
                instance = new SH249(driver);
            }
            else if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using QATestAllBase_SGWay instance ###### ");
                instance = new SGWay(driver);
            }
            else if (tenantName == TenantName.I15North)
            {
                log.Info($"###### using QATestAllBase_GLX instance ###### ");
                instance = new I15North(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using QATestAllBase_I15South instance ###### ");
                instance = new I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using QATestAllBase_I15Tech instance ###### ");
                instance = new I15Tech(driver);
            }

            return (T)instance;
        }

        public enum WFType_I15SB
        {
            A1,
            A2,
            E1,
            E2,
            E3,
            F1,
            F2,
            F3
        }
    }

    public interface IQATestAllBase
    {
    }

    public abstract class QATestAllBase : PageBase, IQATestAllBase
    {
    }

    public class LAX : QATestAllBase_Common
    {
        public LAX(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class SH249 : QATestAllBase_Common
    {
        internal SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class SGWay : QATestAllBase_Common
    {
        internal SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class I15North : QATestAllBase_Common
    {
        internal I15North(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class I15South : QATestAllBase_Common
    {
        internal I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class I15Tech : QATestAllBase_Common
    {
        internal I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

}
