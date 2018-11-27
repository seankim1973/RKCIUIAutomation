using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QADIRs;
using static RKCIUIAutomation.Page.TableHelper;

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
        void LoginToDirPage(UserType userType, bool useQaFieldMenu = false);

        void LoginToQaFieldDirPage(UserType userType);

        void LoginToRcrdCtrlDirPage(UserType userType);

        string Create_and_SaveForward_DIR();

        void KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab kickBackfromTableTab, string dirNumber);

        void KickBack_DIR_ForRevise_FromQcReview_then_Edit_SaveForward(string dirNumber);

        void KickBack_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber);

        void Modify_Cancel_Verify_inCreateRevise(string dirNumber);

        void Modify_Save_Verify_and_SaveForward_inCreateRevise(string dirNumber);

        void Verify_DIR_then_Approve_inReview(string dirNumber);

        void Verify_DIR_then_Approve_inAuthorization(string dirNumber);

        bool VerifyDirIsDisplayedInRevise(string dirNumber);

        bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber);

        void ApproveDIR();
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

        public virtual void LoginToDirPage(UserType userType, bool useQaFieldMenu = false)
        {
            bool QaFieldDIR = false;
            string expectedPageTitle = string.Empty;
            string logMsg = QaFieldDIR ? "LoginToQaFieldDirPage" : "LoginToRcrdCtrlDirPage";
            LogDebug($"---> {logMsg} <---");

            LoginAs(userType);

            if (!Driver.Title.Contains("DIR List"))
            {
                if (userType == UserType.DIRTechQA || userType == UserType.DIRMgrQA)
                {
                    string tenant = tenantName.ToString();
                    if (tenant.Equals("SGWay") || tenant.Equals("SH249") || tenant.Equals("Garnet"))
                    {
                        QaFieldDIR = userType == UserType.DIRTechQA ? true : useQaFieldMenu ? true : false;

                        if (QaFieldDIR)
                        {
                            expectedPageTitle = "IQF Field > List of Daily Inspection Reports";
                            NavigateToPage.QAField_QA_DIRs();
                        }
                        else
                        {
                            expectedPageTitle = "IQF Record Control > List of Daily Inspection Reports";
                            NavigateToPage.QARecordControl_QA_DIRs();
                        }
                    }
                    else
                    {
                        expectedPageTitle = "List of Inspector's Daily Report";
                        NavigateToPage.QARecordControl_QA_DIRs();
                    }
                }
                else if (userType == UserType.DIRTechQC || userType == UserType.DIRMgrQC)
                {
                    NavigateToPage.QCRecordControl_QC_DIRs();
                }

                Assert.True(VerifyPageTitle(expectedPageTitle));
            }
        }

        public virtual void LoginToQaFieldDirPage(UserType userType) => LoginToDirPage(userType, true);

        public virtual void LoginToRcrdCtrlDirPage(UserType userType) => LoginToDirPage(userType);

        public virtual bool VerifyDirIsDisplayedInRevise(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber);

        //GLX, 
        public virtual string Create_and_SaveForward_DIR()
        {
            LogDebug($"---> Create_and_SaveForward_DIR <---");

            QaRcrdCtrl_QaDIR.ClickBtn_CreateNew();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyReqFieldErrorsForNewDir(), "VerifyReqFieldErrorsForNewDir");
            QaRcrdCtrl_QaDIR.PopulateRequiredFields();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            return QaRcrdCtrl_QaDIR.GetDirNumber();
        }

        
        public virtual void KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab kickBackfromTableTab, string dirNumber)
        {
            LogDebug($"---> KickBack_DIR_ForRevise_From{kickBackfromTableTab.ToString()}Tab_then_Edit_inCreateReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(kickBackfromTableTab, dirNumber), "VerifyDirIsDisplayed");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_KickBack(); //TODO - Use 'Revise' button instead of 'Kick Back' for SG & SH249
            QaRcrdCtrl_QaDIR.SelectRdoBtn_SendEmailForRevise_No();
            QaRcrdCtrl_QaDIR.ClickBtn_SubmitRevise();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed");
            ClickEditBtnForRow();
        }

        public virtual void KickBack_DIR_ForRevise_FromQcReview_then_Edit_SaveForward(string dirNumber)
            => WF_QaRcrdCtrl_QaDIR.KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab.QC_Review, dirNumber);

        public virtual void KickBack_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber)
        {
            WF_QaRcrdCtrl_QaDIR.KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab.Authorization, dirNumber);
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
        }

        public virtual void Modify_Cancel_Verify_inCreateRevise(string dirNumber)
        {
            LogDebug($"---> Modify_DeficiencyDescription_Cancel_and_Verify <---");

            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_F();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_Deficiencies_Yes();
            QaRcrdCtrl_QaDIR.EnterText_DeficiencyDescription();
            QaRcrdCtrl_QaDIR.ClickBtn_Cancel();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Inspection_Result_P), "VerifyChkBoxRdoBtnSelection Inspection_Result_P");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Deficiencies_No), "VerifyChkBoxRdoBtnSelection AnyDeficiencies_No");
            AddAssertionToList(VerifyTextAreaField(InputFields.Deficiency_Description, true), "VerifyTextAreaField Deficiency_Description - Should Be Empty");
        }

        public virtual void Modify_Save_Verify_and_SaveForward_inCreateRevise(string dirNumber)
        {
            LogDebug($"---> Modify_DeficiencyDescription_Save_and_Verify <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDeficiencySelectionPopupMessages(), "VerifyDeficiencySelectionPopupMessages");
            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_F();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_Deficiencies_Yes();
            QaRcrdCtrl_QaDIR.EnterText_DeficiencyDescription();
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed(TableTab.Create_Revise)");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Inspection_Result_F), "VerifyChkBoxRdoBtnSelection(Inspection_Result_F)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Deficiencies_Yes), "VerifyChkBoxRdoBtnSelection(Deficiencies_Yes)");
            AddAssertionToList(VerifyTextAreaField(InputFields.Deficiency_Description), "VerifyTextAreaField(Deficiency_Description)");
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
        }

        public virtual void Verify_DIR_then_Approve_inReview(string dirNumber)
        {
            LogDebug($"---> Verify_DIR_then_Approve_inReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed(TableTab.QC_Review)");
            ClickEditBtnForRow();
            WF_QaRcrdCtrl_QaDIR.ApproveDIR();
        }

        public virtual void Verify_DIR_then_Approve_inAuthorization(string dirNumber)
        {
            LogDebug($"---> Verify_DIR_then_Approve_inAuthorization <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Authorization, dirNumber), "VerifyDirIsDisplayed(TableTab.Authorization)");
            ClickEditBtnForRow();
            WF_QaRcrdCtrl_QaDIR.ApproveDIR();
        }

        public virtual bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Closed);

        public virtual void ApproveDIR() => QaRcrdCtrl_QaDIR.ClickBtn_Approve();
    }

    internal class QaRcrdCtrl_QaDIR_WF_GLX : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override void KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab kickBackfromTableTab, string dirNumber)
        {
            LogDebug($"---> GLX - KickBack_DIR_ForRevise_FromTab_{kickBackfromTableTab.ToString()}_then_Edit_inCreateReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(kickBackfromTableTab, dirNumber), $"VerifyDirIsDisplayed({kickBackfromTableTab.ToString()})");
            ClickEditBtnForRow();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifySectionDescription(), "VerifySectionDescription");
            QaRcrdCtrl_QaDIR.ClickBtn_KickBack();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_SendEmailForRevise_No();
            QaRcrdCtrl_QaDIR.ClickBtn_SubmitRevise();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed(TableTab.Create_Revise)");
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

        public override bool VerifyDirIsDisplayedInRevise(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Revise, dirNumber);

        public override void KickBack_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber)
            => LogInfo("---> KickBack_DIR_ForRevise_FromAuthorization_then_Edit <---<br>Step skipped for Tenant SH249");

        public override void Verify_DIR_then_Approve_inAuthorization(string dirNumber)
            => LogInfo("---> Verify_DIR_then_Approve_inAuthorization <---<br>Step skipped for Tenant SH249");

        public override bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment);

        public override void ApproveDIR() => QaRcrdCtrl_QaDIR.ClickBtn_NoError();

    }
    internal class QaRcrdCtrl_QaDIR_WF_SGWay : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override bool VerifyDirIsDisplayedInRevise(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Revise, dirNumber);

        public override void KickBack_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber)
        {
            WF_QaRcrdCtrl_QaDIR.KickBack_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab.Authorization, dirNumber);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Authorization, dirNumber), "VerifyDirIsDisplayed");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_Back_To_QC_Review();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            Verify_DIR_then_Approve_inReview(dirNumber);
        }

        public override bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment);

        public override void ApproveDIR() => QaRcrdCtrl_QaDIR.ClickBtn_NoError();

    }
    internal class QaRcrdCtrl_QaDIR_WF_LAX : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}
