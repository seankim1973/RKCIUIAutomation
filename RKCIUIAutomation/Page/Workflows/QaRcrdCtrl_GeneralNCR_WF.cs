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

        string Create_and_SaveOnly_NCR(UserType user);

        /// <summary>
        /// Verifies a document is shown in 'Revise' tab, after clicking Revise button for a document in the 'Review' tab.
        /// <para>Verifies a document is shown in 'To Be Closed' tab, after clicking Save & Fwd button for a document in the 'Review' tab.</para>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ncrDescription"></param>
        void Review_and_Approve_NCR(UserType user, string ncrDescription);

        void Review_and_Return_NCR_ForRevise(UserType user, string ncrDescription);

        void SaveForward_FromResolutionDisposition_ToVerificationClosure_ReturnToConformance(string ncrDescription);

        void CheckReviseKickback_FromVerificationClosure_ForReturnToConformance(string ncrDescription);

        void SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(string ncrDescription);

        void SaveForward_FromDeveloperConcurrence_ToDOTApproval(string ncrDescription, bool approveNCR = true);

        void SaveForward_FromDOTApproval_ToVerificationClosure(string ncrDescription, bool approveNCR = true);

        //void Return_ToResolutionDisposition_FromDeveloperConcurrence(string ncrDescription);

        //void Return_ToDeveloperConcurrence_FromDOTApproval(string ncrDescription);

        void CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(string ncrDescription);

        //void CloseNCR_ConcessionRequest_ConcessionDeviation(string ncrDescription);

        void CloseNCR_CQMReview_Disapprove(UserType user, string ncrDescription);

        //void CloseNCR_ConcessionRequest_ReturnToConformance(string ncrDescription);

        void CloseNCR_in_VerificationAndClosure(string ncrDescription);
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

        internal void NavigateToGeneralNcrPage()
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

        public virtual string Create_and_SaveOnly_NCR(UserType user)
        {
            LogDebug("------------WF Create_and_SaveOnly_NCR_Document-------------");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            QaRcrdCtrl_GeneralNCR.ClickBtn_New();
            QaRcrdCtrl_GeneralNCR.PopulateRequiredFieldsAndSaveOnly();
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

        public virtual void SaveForward_FromResolutionDisposition_ToVerificationClosure_ReturnToConformance(string ncrDescription)
        {
            LogDebug("------------WF TraverseNCR_FromReview_ToVerificationClosure_ReturnToConformance-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            ClickEditBtnForRow();
            //todo: click Save&Fwd button and verify required field error label is shown for Concession Request DDList
            QaRcrdCtrl_GeneralNCR.SelectDDL_PopulateRelatedFields_forConcessionRequest_ReturnToConformance();
            //todo: select checkboxes
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }

        public virtual void CheckReviseKickback_FromVerificationClosure_ForReturnToConformance(string ncrDescription)
        {
            LogDebug("------------WF Return_ToRevise_FromVerificationClosure-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription));
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            ClickEditBtnForRow();
            //QaRcrdCtrl_GeneralNCR.SelectDDL_ConcessionRequest_ReturnToConformance();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription));
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_KickBack();
        }

        //public virtual void CloseNCR_ConcessionRequest_ReturnToConformance(string ncrDescription)
        //{
        //    LogDebug("------------WF CloseNCR_ConcessionRequest_ReturnToConformance-------------");

        //    //todo: click Close button and verify required field error labels are shown(IQCMgr SignBtn, IQFMgr, IQFMgrApprovedDate, QCMgr_SignBtn, QCMgr, QCMgrApprovedDate)
        //    //CloseNCR_in_VerificationAndClosure(ncrDescription);
        //}

        //SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(string ncrDescription);
        public void SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(string ncrDescription)
        {
            LogDebug("------------WF SaveForward_FromResolutionDisposition_ToDeveloperConcurrence-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            ClickEditBtnForRow();

            //>>>WORKFLOW for (Concession Request DDList) Concession Deviation
            //todo: click Save&Fwd button and verify required field error label is shown for Concession Request DDList
            QaRcrdCtrl_GeneralNCR.SelectDDL_PopulateRelatedFields_forConcessionRequest_ConcessionDeviation();
            //todo: select checkboxes
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }

        //SaveForward_FromDeveloperConcurrence_ToDOTApproval(string ncrDescription, bool approveNCR = true);
        public void SaveForward_FromDeveloperConcurrence_ToDOTApproval(string ncrDescription, bool approveNCR = true)
        {
            LogDebug("------------WF SaveForward_FromDeveloperConcurrence_ToDOTApproval-------------");
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

        //SaveForward_FromDOTApproval_ToVerificationClosure(string ncrDescription, bool approveNCR = true);
        public void SaveForward_FromDOTApproval_ToVerificationClosure(string ncrDescription, bool approveNCR = true)
        {
            LogDebug("------------WF SaveForward_FromDOTApproval_ToVerificationClosure-------------");
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
            
        ////TODO: create new test case where No is selected for Approval and NCR returned to Resolution/Disposition
        //public virtual void Return_ToResolutionDisposition_FromDeveloperConcurrence(string ncrDescription)
        //{
        //    LogDebug("------------WF Return_ToResolutionDisposition_FromDeveloperConcurrence-------------");

        //    //SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);
        //    //SaveForward_FromDeveloperConcurrence_ToDOTApproval(ncrDescription, false);
        //    //SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);
        //}
            
        //public virtual void Return_ToDeveloperConcurrence_FromDOTApproval(string ncrDescription)
        //{
        //    LogDebug("------------WF Return_ToDeveloperConcurrence_FromDOTApproval-------------");

        //    //SaveForward_FromDeveloperConcurrence_ToDOTApproval(ncrDescription);
        //    //SaveForward_FromDOTApproval_ToVerificationClosure(ncrDescription, false);
        //    //SaveForward_FromDeveloperConcurrence_ToDOTApproval(ncrDescription);
        //}

        public virtual void CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(string ncrDescription)
        {
            LogDebug("------------WF Return_ToRevise_FromVerificationClosure_ForConcessionDiviation-------------");

            SaveForward_FromDOTApproval_ToVerificationClosure(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription));
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_KickBack();
            SaveForward_FromDOTApproval_ToVerificationClosure(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription));
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
        }

        //public virtual void CloseNCR_ConcessionRequest_ConcessionDeviation(string ncrDescription)
        //{
        //    LogDebug("------------WF CloseNCR_ConcessionRequest_ConcessionDeviation-------------");
        //    //SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(ncrDescription);
        //    //SaveForward_FromDeveloperConcurrence_ToDOTApproval(ncrDescription);
        //    //SaveForward_FromDOTApproval_ToVerificationClosure(ncrDescription);

        //    //todo: click Close button and verify required field error labels are shown(IQCMgr SignBtn, IQFMgr, IQFMgrApprovedDate, QCMgr_SignBtn, QCMgr, QCMgrApprovedDate)

        //    //CloseNCR_in_VerificationAndClosure(ncrDescription);
        //}

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


        /*
         * NCR SimpleWF internal methods
         */

        //internal string SimpleWF_Create_and_SaveForward_NCR(UserType user, bool isComplexWF = false) => Create_and_SaveForward_NCR(user, isComplexWF);

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

    internal class QaRcrdCtrl_GeneralNCR_WF_SH249 : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_SH249(IWebDriver driver) : base(driver)
        {
        }

        //public override string Create_and_SaveForward_NCR(UserType user, bool isComplexWF = false)
        //    => SimpleWF_Create_and_SaveForward_NCR(user);
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_SGWay : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_SGWay(IWebDriver driver) : base(driver)
        {
        }

        //public override string Create_and_SaveForward_NCR(UserType user, bool isComplexWF = false)
        //    => SimpleWF_Create_and_SaveForward_NCR(user);
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_LAX : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_LAX(IWebDriver driver) : base(driver)
        {
        }

        //public override string Create_and_SaveForward_NCR(UserType user, bool isComplexWF = false)
        //    => SimpleWF_Create_and_SaveForward_NCR(user);
    }
}