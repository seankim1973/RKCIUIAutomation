using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.GeneralCDR;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page.Workflows
{
    public class QaRcrdCtrl_GeneralCDR_WF : QaRcrdCtrl_GeneralCDR_WF_Impl
    {
        public QaRcrdCtrl_GeneralCDR_WF()
        {
        }

        public QaRcrdCtrl_GeneralCDR_WF(IWebDriver driver) => this.Driver = driver;

        public override void AddAssertion_VerifyCDRPageHeader(string methodName)
            => AddAssertionToList_VerifyPageHeader("List of CDR Reports", methodName);

        /// <summary>
        /// Verifies Required field error labels in a new document then populates required fields and clicks Save & Forward button
        /// <para>Returns unique CDR document description string value</para>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override string CreateAndSaveForwardCDRDocument(UserType user)
        {
            LoginAs(user);
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertion_VerifyCDRPageHeader("CreateAndSaveForwardCDRDocument()");
            QaRcrdCtrl_GeneralCDR.ClickBtn_New();
            QaRcrdCtrl_GeneralCDR.PopulateRequiredFieldsAndSaveForward();
            return QaRcrdCtrl_GeneralCDR.GetCDRDocDescription();
        }

        public override void ReviewCDRDocument(UserType user, string cdrDescription)
        {
            LoginAs(user);
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertion_VerifyCDRPageHeader("ReviewCDRDocument()");
            QaRcrdCtrl_GeneralCDR.ClickTab_QC_Review();
            QaRcrdCtrl_GeneralCDR.FilterDescription(cdrDescription);
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralCDR.ClickBtn_SaveForward();
        }

        public override void DispositionCDRDocument(UserType user, string cdrDescription)
        {
            LoginAs(user);
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertion_VerifyCDRPageHeader("DispositionCDRDocument()");
            QaRcrdCtrl_GeneralCDR.ClickTab_Disposition();
            QaRcrdCtrl_GeneralCDR.FilterDescription(cdrDescription);
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralCDR.ClickBtn_SaveForward();
        }

        public override void KickBackToDispositionCDR(UserType user, string cdrDescription)
        {
            LoginAs(user);
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertion_VerifyCDRPageHeader("KickBackToDispositionCDR()");
            QaRcrdCtrl_GeneralCDR.ClickTab_To_Be_Closed();
            QaRcrdCtrl_GeneralCDR.FilterDescription(cdrDescription);
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralCDR.ClickBtn_Back_To_Disposition();
        }
        public override void KickBackToQCReviewCDR(UserType user, string cdrDescription)
        {
            LoginAs(user);
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertion_VerifyCDRPageHeader("KickBackToQCReviewCDR()");
            QaRcrdCtrl_GeneralCDR.ClickTab_Disposition();
            QaRcrdCtrl_GeneralCDR.FilterDescription(cdrDescription);
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralCDR.ClickBtn_Back_To_QC_Review();
        }

        public override void ReviewAndApproveCDRDocument(UserType user, string cdrDescription)
        {
            LoginAs(user);
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertion_VerifyCDRPageHeader("ReviewAndApproveCDRDocument()");
            QaRcrdCtrl_GeneralCDR.ClickTab_QC_Review();
            QaRcrdCtrl_GeneralCDR.FilterDescription(cdrDescription);
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralCDR.ClickBtn_SaveForward();
            QaRcrdCtrl_GeneralCDR.FilterDescription(cdrDescription);
            GridHelper.ClickEditBtnForRow();
        }

        public override void ReviewAndReviseCDRDocument(UserType user, string cdrDescription)
        {
            LoginAs(user);
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertion_VerifyCDRPageHeader("ReviewAndReviseCDRDocument()");
            QaRcrdCtrl_GeneralCDR.ClickTab_QC_Review();
            QaRcrdCtrl_GeneralCDR.FilterDescription(cdrDescription);
            GridHelper.ClickEditBtnForRow();

            QaRcrdCtrl_GeneralCDR.ClickBtn_Revise();
        }

        public override void CloseDocument(UserType user, string cdrDescription)
        {
            LoginAs(user);
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertion_VerifyCDRPageHeader("CloseDocument()");
            QaRcrdCtrl_GeneralCDR.ClickTab_To_Be_Closed();
            QaRcrdCtrl_GeneralCDR.FilterDescription(cdrDescription);
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralCDR.ClickBtn_CloseCDR();
        }

    }

    public interface IQaRcrdCtrl_GeneralCDR_WF
    {
        void AddAssertion_VerifyCDRPageHeader(string methodName);

        /// <summary>
        /// Verifies Required field error labels in a new document then populates required fields and clicks Save & Forward button
        /// <para>Returns unique CDR document description string value</para>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string CreateAndSaveForwardCDRDocument(UserType user);

        void ReviewCDRDocument(UserType user, string cdrDescription);
        void DispositionCDRDocument(UserType user, string cdrDescription);
        /// <summary>
        /// Verifies a document is shown in 'Revise' tab, after clicking Revise button for a document in the 'Review' tab.
        /// <para>Verifies a document is shown in 'To Be Closed' tab, after clicking Save & Fwd button for a document in the 'Review' tab.</para>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cdrDescription"></param>
        void ReviewAndApproveCDRDocument(UserType user, string cdrDescription);
        void KickBackToDispositionCDR(UserType user, string cdrDescription);
        void KickBackToQCReviewCDR(UserType user, string cdrDescription);
        void ReviewAndReviseCDRDocument(UserType user, string cdrDescription);
        void CloseDocument(UserType user, string cdrDescription);
    }

    public abstract class QaRcrdCtrl_GeneralCDR_WF_Impl : TestBase, IQaRcrdCtrl_GeneralCDR_WF
    {
        public T SetClass<T>(IWebDriver driver)
        {
            IQaRcrdCtrl_GeneralCDR_WF instance = new QaRcrdCtrl_GeneralCDR_WF(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralCDR_WF_SGWay instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralCDR_WF_SH249 instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralCDR_WF_Garnet instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralCDR_WF_GLX instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralCDR_WF_I15South instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralCDR_WF_I15Tech instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralCDR_WF_LAX instance ###### ");
                instance = new QaRcrdCtrl_GeneralCDR_WF_LAX(driver);
            }
            return (T)instance;
        }

        public abstract void AddAssertion_VerifyCDRPageHeader(string methodName);
        public abstract string CreateAndSaveForwardCDRDocument(UserType user);
        public abstract void ReviewCDRDocument(UserType user, string cdrDescription);
        public abstract void DispositionCDRDocument(UserType user, string cdrDescription);
        public abstract void ReviewAndApproveCDRDocument(UserType user, string cdrDescription);
        public abstract void KickBackToDispositionCDR(UserType user, string cdrDescription);
        public abstract void KickBackToQCReviewCDR(UserType user, string cdrDescription);
        public abstract void ReviewAndReviseCDRDocument(UserType user, string cdrDescription);
        public abstract void CloseDocument(UserType user, string cdrDescription);
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
