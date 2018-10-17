using System;
using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.GeneralNCR;

namespace RKCIUIAutomation.Page.Workflows
{
    public class QaRcrdCtrl_GeneralNCR_WF : QaRcrdCtrl_GeneralNCR_WF_Impl
    {
        public QaRcrdCtrl_GeneralNCR_WF()
        {
        }

        public QaRcrdCtrl_GeneralNCR_WF(IWebDriver driver) => this.Driver = driver;

    }

    public interface IQaRcrdCtrl_GeneralNCR_WF
    {
        /// <summary>
        /// Verifies Required field error labels in a new document then populates required fields and clicks Save & Forward button
        /// <para>Returns unique NCR document description string value</para>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string Create_and_SaveForward_NCR(UserType user);

        /// <summary>
        /// Verifies a document is shown in 'Revise' tab, after clicking Revise button for a document in the 'Review' tab.
        /// <para>Verifies a document is shown in 'To Be Closed' tab, after clicking Save & Fwd button for a document in the 'Review' tab.</para>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ncrDescription"></param>
        void Review_and_Approve_NCR(UserType user, string ncrDescription);

        void Review_and_Return_NCR_ForRevise(UserType user, string ncrDescription);

        void Return_ToRevise_FromVerificationClosure_ForReturnToConformance(string ncrDescription);

        void Return_ToResolutionDisposition_FromDeveloperConcurrence(string ncrDescription);

        void Return_ToDeveloperConcurrence_FromDOTApproval(string ncrDescription);

        void CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(string ncrDescription);

        void CloseNCR_ConcessionRequest_ConcessionDeviation(string ncrDescription);

        void CloseNCR_CQMReview_Disapprove(UserType user, string ncrDescription);

        void CloseNCR_ConcessionRequest_ReturnToConformance(string ncrDescription);

    }

    public abstract class QaRcrdCtrl_GeneralNCR_WF_Impl : TestBase, IQaRcrdCtrl_GeneralNCR_WF
    {
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IQaRcrdCtrl_GeneralNCR_WF SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IQaRcrdCtrl_GeneralNCR_WF instance = new QaRcrdCtrl_GeneralNCR_WF(driver);

            if (tenantName == TenantName.SGWay)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_SGWay instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_SH249 instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_Garnet instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_GLX instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_I15South instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_I15Tech instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_LAX instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_LAX(driver);
            }
            return instance;
        }

        private void NavigateToGeneralNcrPage()
        {
            if (!Driver.Title.Contains("NCR List"))
            {
                NavigateToPage.QARecordControl_General_NCR();
                Assert.True(VerifyPageTitle("List of NCR Reports"));
            }
        }

        public virtual string Create_and_SaveForward_NCR(UserType user)
        {
            LogDebug("------------WF Create_and_SaveForward_NCR_Document-------------");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            QaRcrdCtrl_GeneralNCR.ClickBtn_New();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyReqFieldErrorLabelsForNewDoc());
            QaRcrdCtrl_GeneralNCR.PopulateRequiredFieldsAndSaveForward();
            return QaRcrdCtrl_GeneralNCR.GetNCRDocDescription();
        }

        public virtual void Review_and_Approve_NCR(UserType user, string ncrDescription)
        {
            LogDebug("------------WF Review_and_Approve_NCR_Document-------------");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.CQM_Review, ncrDescription));
            ClickEditBtnForRow();

            //verify required field error label is shown
            QaRcrdCtrl_GeneralNCR.ClickBtn_Approve();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyReqFieldErrorLabelForTypeOfNCR());
            QaRcrdCtrl_GeneralNCR.SelectRdoBtn_TypeOfNCR_Level1();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Approve();
        }

        public virtual void Review_and_Return_NCR_ForRevise(UserType user, string ncrDescription)
        {
            LogDebug("------------WF Return_ToRevise_FromCQMReview-------------");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.CQM_Review, ncrDescription));
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
        }
        public virtual void CloseNCR_CQMReview_Disapprove(UserType user, string ncrDescription)
        {
            LogDebug("------------WF CloseNCR_CQMReview_Disapprove-------------");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            QaRcrdCtrl_GeneralNCR.ClickTab_CQM_Review();
            QaRcrdCtrl_GeneralNCR.FilterDescription(ncrDescription);
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_DisapproveClose();
        }

        private void TraverseNCR_FromResolutionDisposition_ToVerificationClosure_ReturnToConformance(string ncrDescription)
        {
            LogDebug("------------WF TraverseNCR_FromReview_ToVerificationClosure_ReturnToConformance-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            ClickEditBtnForRow();
            //todo: click Save&Fwd button and verify required field error label is shown for Concession Request DDList
            QaRcrdCtrl_GeneralNCR.SelectDDL_ConcessionRequest_ReturnToConformance();
            //todo: select checkboxes
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }

        public virtual void Return_ToRevise_FromVerificationClosure_ForReturnToConformance(string ncrDescription)
        {
            TraverseNCR_FromResolutionDisposition_ToVerificationClosure_ReturnToConformance(ncrDescription);

            LogDebug("------------WF Return_ToRevise_FromVerificationClosure-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription));
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
        }

        public virtual void CloseNCR_ConcessionRequest_ReturnToConformance(string ncrDescription)
        {
            LogDebug("------------WF CloseNCR_ConcessionRequest_ReturnToConformance-------------");

            TraverseNCR_FromResolutionDisposition_ToVerificationClosure_ReturnToConformance(ncrDescription);

            //todo: click Close button and verify required field error labels are shown(IQCMgr SignBtn, IQFMgr, IQFMgrApprovedDate, QCMgr_SignBtn, QCMgr, QCMgrApprovedDate)
            CloseNCR_in_VerificationAndClosure(ncrDescription);
        }

        private void TraverseNCR_FromResolutionDisposition_ToDeveloperConcurrence(string ncrDescription)
        {
            LogDebug("------------WF TraverseNCR_FromReview_ToDeveloperConcurrence-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            ClickEditBtnForRow();

            //>>>WORKFLOW for (Concession Request DDList) Concession Deviation
            //todo: click Save&Fwd button and verify required field error label is shown for Concession Request DDList
            QaRcrdCtrl_GeneralNCR.SelectDDL_ConcessionRequest_ConcessionDeviation();
            //todo: select checkboxes
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }

        private void TraverseNCR_FromDeveloperConcurrence_ToDOTApproval(string ncrDescription, bool approveNCR = true)
        {
            LogDebug("------------WF TraverseNCR_FromDeveloperConcurrence_ToDOTApproval-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence, ncrDescription));
            ClickEditBtnForRow();
            //todo: click Save&Fwd button and verify required field error label is shown (RecordEngineer_SignBtn, EngOfRecord, EngApprovalDate, ApprovalRadioBtn)

            if (approveNCR)
            {
                QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(Reviewer.EngineerOfRecord);
            }
            else
            {
                QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(Reviewer.EngineerOfRecord, false);
            }
            
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }

        private void TraverseNCR_FromDOTApproval_ToVerificationClosure(string ncrDescription, bool approveNCR = true)
        {
            LogDebug("------------WF TraverseNCR_FromDOTApproval_ToVerificationClosure-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.DOT_Approval, ncrDescription));
            ClickEditBtnForRow();
            //todo: click Save&Fwd button and verify required field error label is shown(Owner_SignBtn, DOTReview, OwnerApprovalDate, OwnerApprovalRdoBtn)

            if (approveNCR)
            {
                QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(Reviewer.Owner);
            }
            else
            {
                QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(Reviewer.Owner, false);
            }
            
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }
            
        //TODO: create new test case where No is selected for Approval and NCR returned to Resolution/Disposition
        public virtual void Return_ToResolutionDisposition_FromDeveloperConcurrence(string ncrDescription)
        {
            LogDebug("------------WF Return_ToResolutionDisposition_FromDeveloperConcurrence-------------");

            TraverseNCR_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);
            TraverseNCR_FromDeveloperConcurrence_ToDOTApproval(ncrDescription, false);
            TraverseNCR_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);
        }
            
        //TODO: create new test case where No is selected for Owner Approval and NCR is sent back to Developer Concurrence.
        public virtual void Return_ToDeveloperConcurrence_FromDOTApproval(string ncrDescription)
        {
            LogDebug("------------WF Return_ToDeveloperConcurrence_FromDOTApproval-------------");

            TraverseNCR_FromDeveloperConcurrence_ToDOTApproval(ncrDescription);
            TraverseNCR_FromDOTApproval_ToVerificationClosure(ncrDescription, false);
            TraverseNCR_FromDeveloperConcurrence_ToDOTApproval(ncrDescription);
        }

        public virtual void CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(string ncrDescription)
        {
            LogDebug("------------WF Return_ToRevise_FromVerificationClosure_ForConcessionDiviation-------------");

            TraverseNCR_FromDOTApproval_ToVerificationClosure(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription));
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_KickBack();
            TraverseNCR_FromDOTApproval_ToVerificationClosure(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription));
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
        }

        public virtual void CloseNCR_ConcessionRequest_ConcessionDeviation(string ncrDescription)
        {
            LogDebug("------------WF CloseNCR_ConcessionRequest_ConcessionDeviation-------------");
            TraverseNCR_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);
            TraverseNCR_FromDeveloperConcurrence_ToDOTApproval(ncrDescription);
            TraverseNCR_FromDOTApproval_ToVerificationClosure(ncrDescription);

            //todo: click Close button and verify required field error labels are shown(IQCMgr SignBtn, IQFMgr, IQFMgrApprovedDate, QCMgr_SignBtn, QCMgr, QCMgrApprovedDate)

            CloseNCR_in_VerificationAndClosure(ncrDescription);
        }

        public virtual void CloseNCR_in_VerificationAndClosure(string ncrDescription)
        {
            LogDebug("------------WF CloseNCR_in_VerificationAndClosure-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription));
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(Reviewer.IQF_Manager);
            QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(Reviewer.QC_Manager);
            QaRcrdCtrl_GeneralNCR.ClickBtn_Close();
            //todo: need verification step to check All NCRs tab and confirm 'workflow location' of NCR
        }        
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_GLX : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_Garnet : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_SH249 : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_SGWay : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_I15Tech : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_I15South : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_LAX : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}