using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QADIRs;


namespace RKCIUIAutomation.Page.Workflows
{
    public class QaRcrdCtrl_QaDIR_WF : QaRcrdCtrl_QaDIR_WF_Impl
    {
        public QaRcrdCtrl_QaDIR_WF()
        {
        }

        public QaRcrdCtrl_QaDIR_WF(IWebDriver driver) => this.Driver = driver; 
    }

    public interface IQaRcrdCtrl_QaDIR_WF
    {
        void NavigateToDirPage();

        string Create_and_SaveForward_DIR(UserType userType);

        void Review_and_Return_DIR_ForRevise(UserType userType, string dirNumber);

        void Modify_Cancel_Verify_inCreateReview(string dirNumber);

        void Modify_Save_Verify_and_SaveForward_inCreateReview(string dirNumber);
    }

    public abstract class QaRcrdCtrl_QaDIR_WF_Impl : TestBase, IQaRcrdCtrl_QaDIR_WF
    {
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IQaRcrdCtrl_QaDIR_WF SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IQaRcrdCtrl_QaDIR_WF instance = new QaRcrdCtrl_QaDIR_WF(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_SGWay instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_SH249 instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_Garnet instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_GLX instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_I15South instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_I15Tech instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_LAX instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_LAX(driver);
            }
            return instance;
        }

        public virtual void NavigateToDirPage()
        {
            if (!Driver.Title.Contains("DIR List"))
            {
                NavigateToPage.QARecordControl_QA_DIRs();
                Assert.True(VerifyPageTitle($"List of Inspector's Daily Report"));
            }
        }

        public virtual string Create_and_SaveForward_DIR(UserType userType)
        {
            LogDebug("------------------ Create_and_SaveForward_DIR ------------------");

            LoginAs(userType);
            WF_QaRcrdCtrl_QaDIR.NavigateToDirPage();
            QaRcrdCtrl_QaDIR.ClickBtn_CreateNew();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyReqFieldErrorsForNewDir());
            QaRcrdCtrl_QaDIR.PopulateRequiredFields();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            return QaRcrdCtrl_QaDIR.GetDirNumber();
        }

        public virtual void Review_and_Return_DIR_ForRevise(UserType userType, string dirNumber)
        {
            LogDebug("------------------ Review_and_Return_DIR_ForRevise ------------------");

            LoginAs(userType);
            WF_QaRcrdCtrl_QaDIR.NavigateToDirPage();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber));
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_KickBack();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_SendEmailForRevise_No();
            QaRcrdCtrl_QaDIR.ClickBtn_SubmitRevise();
        }

        public virtual void Modify_Cancel_Verify_inCreateReview(string dirNumber)
        {
            LogDebug("------------------ Modify_DeficiencyDescription_Cancel_and_Verify ------------------");

            QaRcrdCtrl_QaDIR.SelectRdoBtn_AnyDeficiencies_Yes();
            QaRcrdCtrl_QaDIR.EnterText_DeficiencyDescription();
            QaRcrdCtrl_QaDIR.ClickBtn_Cancel();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber));
            ClickEditBtnForRow();
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.AnyDeficiencies_No));
            AddAssertionToList(VerifyTextAreaField(InputFields.Deficiency_Description, true));
        }

        public virtual void Modify_Save_Verify_and_SaveForward_inCreateReview(string dirNumber)
        {
            LogDebug("------------------ Modify_DeficiencyDescription_Save_and_Verify ------------------");

            QaRcrdCtrl_QaDIR.SelectRdoBtn_AnyDeficiencies_Yes();
            QaRcrdCtrl_QaDIR.EnterText_DeficiencyDescription();
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber));
            ClickEditBtnForRow();
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.AnyDeficiencies_Yes));
            AddAssertionToList(VerifyTextAreaField(InputFields.Deficiency_Description));
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
        }

    }

    internal class QaRcrdCtrl_QaDIR_WF_GLX : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override void NavigateToDirPage()
        {
            if (!Driver.Title.Contains("DIR List"))
            {
                NavigateToPage.QCRecordControl_QC_DIRs(); //? Verify correct menu selection
                Assert.True(VerifyPageTitle("List of Inspector's Daily Report"));
            }
        }
    }

    internal class QaRcrdCtrl_QaDIR_WF_Garnet : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_QaDIR_WF_I15Tech : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }
    internal class QaRcrdCtrl_QaDIR_WF_I15South : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_I15South(IWebDriver driver) : base(driver)
        {
        }
    }
    internal class QaRcrdCtrl_QaDIR_WF_SH249 : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_SH249(IWebDriver driver) : base(driver)
        {
        }
    }
    internal class QaRcrdCtrl_QaDIR_WF_SGWay : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }
    internal class QaRcrdCtrl_QaDIR_WF_LAX : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}
