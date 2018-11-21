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
        void LoginAndNavigateToDirPage(UserType userType);

        string Create_and_SaveForward_DIR();

        void KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab kickBackfromTableTab, string dirNumber);

        void Modify_Cancel_Verify_inCreateRevise(string dirNumber);

        void Modify_Save_Verify_and_SaveForward_inCreateRevise(string dirNumber);

        void Verify_DIR_then_Approve_inReview(string dirNumber);

        void Verify_DIR_then_Approve_inAuthorization(string dirNumber);
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

        public virtual void LoginAndNavigateToDirPage(UserType userType)
        {
            LogDebug($">>> LoginAndNavigateToDirPage <<<");

            LoginAs(userType);

            if (!Driver.Title.Contains("DIR List"))
            {
                if (userType == UserType.DIRTechQA || userType == UserType.DIRMgrQA)
                {
                    NavigateToPage.QARecordControl_QA_DIRs();
                }
                else
                {
                    NavigateToPage.QCRecordControl_QC_DIRs();
                }
                
                Assert.True(VerifyPageTitle($"List of Inspector's Daily Report"));
            }
        }

        //GLX, 
        public virtual string Create_and_SaveForward_DIR()
        {
            LogDebug($">>> Create_and_SaveForward_DIR <<<");

            QaRcrdCtrl_QaDIR.ClickBtn_CreateNew();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyReqFieldErrorsForNewDir(), "VerifyReqFieldErrorsForNewDir");
            QaRcrdCtrl_QaDIR.PopulateRequiredFields();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            return QaRcrdCtrl_QaDIR.GetDirNumber();
        }

        public virtual void KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab kickBackfromTableTab, string dirNumber)
        {
            LogDebug($">>> KickBack_DIR_ForRevise_From{kickBackfromTableTab.ToString()}Tab_then_Edit_inCreateReview <<<");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(kickBackfromTableTab, dirNumber), "VerifyDirIsDisplayed");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_KickBack();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_SendEmailForRevise_No();
            QaRcrdCtrl_QaDIR.ClickBtn_SubmitRevise();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber), "VerifyDirIsDisplayed");
            ClickEditBtnForRow();
        }

        public virtual void Modify_Cancel_Verify_inCreateRevise(string dirNumber)
        {
            LogDebug($">>> Modify_DeficiencyDescription_Cancel_and_Verify <<<");

            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_F();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_Deficiencies_Yes();
            QaRcrdCtrl_QaDIR.EnterText_DeficiencyDescription();
            QaRcrdCtrl_QaDIR.ClickBtn_Cancel();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber), "VerifyDirIsDisplayed");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Inspection_Result_P), "VerifyChkBoxRdoBtnSelection Inspection_Result_P");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Deficiencies_No), "VerifyChkBoxRdoBtnSelection AnyDeficiencies_No");
            AddAssertionToList(VerifyTextAreaField(InputFields.Deficiency_Description, true), "VerifyTextAreaField Deficiency_Description - Should Be Empty");
        }

        public virtual void Modify_Save_Verify_and_SaveForward_inCreateRevise(string dirNumber)
        {
            LogDebug($">>> Modify_DeficiencyDescription_Save_and_Verify <<<");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDeficiencySelectionPopupMessages(), "VerifyDeficiencySelectionPopupMessages");
            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_F();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_Deficiencies_Yes();
            QaRcrdCtrl_QaDIR.EnterText_DeficiencyDescription();
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber), "VerifyDirIsDisplayed(TableTab.Create_Revise)");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Inspection_Result_F), "VerifyChkBoxRdoBtnSelection(Inspection_Result_F)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Deficiencies_Yes), "VerifyChkBoxRdoBtnSelection(Deficiencies_Yes)");
            AddAssertionToList(VerifyTextAreaField(InputFields.Deficiency_Description), "VerifyTextAreaField(Deficiency_Description)");
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
        }

        public virtual void Verify_DIR_then_Approve_inReview(string dirNumber)
        {
            LogDebug($">>> Verify_DIR_then_Approve_inReview <<<");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed(TableTab.QC_Review)");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_Approve();
        }

        public virtual void Verify_DIR_then_Approve_inAuthorization(string dirNumber)
        {
            LogDebug($">>> Verify_DIR_then_Approve_inAuthorization <<<");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Authorization, dirNumber), "VerifyDirIsDisplayed(TableTab.Authorization)");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_Approve();
            AddAssertionToList(QaSearch_DIR.VerifyDirIsClosedByTblFilter(dirNumber), "VerifyDirIsClosedByTblFilter");
        }

    }

    internal class QaRcrdCtrl_QaDIR_WF_GLX : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override void KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab kickBackfromTableTab, string dirNumber)
        {
            LogDebug($">>> KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateReview <<<");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(kickBackfromTableTab, dirNumber), "VerifyDirIsDisplayed(kickBackfromTableTab)");
            ClickEditBtnForRow();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifySectionDescription(), "VerifySectionDescription");
            QaRcrdCtrl_QaDIR.ClickBtn_KickBack();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_SendEmailForRevise_No();
            QaRcrdCtrl_QaDIR.ClickBtn_SubmitRevise();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber), "VerifyDirIsDisplayed(TableTab.Create_Revise)");
            ClickEditBtnForRow();
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
