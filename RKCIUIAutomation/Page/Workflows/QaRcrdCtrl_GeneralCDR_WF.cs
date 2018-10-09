using RKCIUIAutomation.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.Workflows
{
    public class QaRcrdCtrl_GeneralCDR_WF : QaRcrdCtrl_GeneralCDR_WF_Impl
    {
        public QaRcrdCtrl_GeneralCDR_WF()
        {
        }

        public QaRcrdCtrl_GeneralCDR_WF(IWebDriver driver) => this.Driver = driver;
    }

    public interface IQaRcrdCtrl_GeneralCDR_WF
    {
        string CreateAndSaveForwardCDRDocument(UserType user);

        void ReviewCDRDocument(UserType user, string cdrNo);
    }

    public abstract class QaRcrdCtrl_GeneralCDR_WF_Impl : TestBase, IQaRcrdCtrl_GeneralCDR_WF
    {
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IQaRcrdCtrl_GeneralCDR_WF SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IQaRcrdCtrl_GeneralCDR_WF instance = new QaRcrdCtrl_GeneralCDR_WF(driver);

            if (tenantName == TenantName.SGWay)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralCDR_WF_SGWay instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralCDR_WF_SH249 instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralCDR_WF_Garnet instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralCDR_WF_GLX instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralCDR_WF_I15South instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralCDR_WF_I15Tech instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralCDR_WF_LAX instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_LAX(driver);
            }
            return instance;
        }

        /// <summary>
        /// Verifies Required field error labels in a new document then populates required fields and clicks Save & Forward button
        /// <para>Returns unique CDR document description string value</para>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual string CreateAndSaveForwardCDRDocument(UserType user)
        {
            LoginAs(user);
            NavigateToPage.QARecordControl_General_CDR();
            Assert.True(VerifyPageTitle("List of CDR Reports"));
            QaRcrdCtrl_GeneralCDR.ClickBtn_New();
            QaRcrdCtrl_GeneralCDR.PopulateRequiredFieldsAndSaveForward();
            return QaRcrdCtrl_GeneralCDR.GetCDRDocDescription();
        }

        public virtual void ReviewCDRDocument(UserType user, string cdrDescription)
        {
            LoginAs(user);
            NavigateToPage.QARecordControl_General_CDR();
            Assert.True(VerifyPageTitle("List of CDR Reports"));
            QaRcrdCtrl_GeneralCDR.ClickTab_QC_Review();
            QaRcrdCtrl_GeneralCDR.FilterDescription(cdrDescription);
            TableHelper.ClickEditBtnForRow();
            //Assert.True(QaRcrdCtrl_GeneralCDR.VerifyCDRDocIsDisplayed(GeneralCDR.TableTab.CQM_Review, ncrDescription));

        }
    }

    internal class QaRcrdCtrl_GeneralCDR_WF_GLX : QaRcrdCtrl_GeneralCDR_WF
    {
        public QaRcrdCtrl_GeneralCDR_WF_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralCDR_WF_Garnet : QaRcrdCtrl_GeneralCDR_WF
    {
        public QaRcrdCtrl_GeneralCDR_WF_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralCDR_WF_SH249 : QaRcrdCtrl_GeneralCDR_WF
    {
        public QaRcrdCtrl_GeneralCDR_WF_SH249(IWebDriver driver) : base(driver)
        {
        }
    }
    internal class QaRcrdCtrl_GeneralCDR_WF_SGWay : QaRcrdCtrl_GeneralCDR_WF
    {
        public QaRcrdCtrl_GeneralCDR_WF_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }
    internal class QaRcrdCtrl_GeneralCDR_WF_I15Tech : QaRcrdCtrl_GeneralCDR_WF
    {
        public QaRcrdCtrl_GeneralCDR_WF_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }
    internal class QaRcrdCtrl_GeneralCDR_WF_I15South : QaRcrdCtrl_GeneralCDR_WF
    {
        public QaRcrdCtrl_GeneralCDR_WF_I15South(IWebDriver driver) : base(driver)
        {
        }
    }
    internal class QaRcrdCtrl_GeneralCDR_WF_LAX : QaRcrdCtrl_GeneralCDR_WF
    {
        public QaRcrdCtrl_GeneralCDR_WF_LAX(IWebDriver driver) : base(driver)
        {
        }
    }

}
