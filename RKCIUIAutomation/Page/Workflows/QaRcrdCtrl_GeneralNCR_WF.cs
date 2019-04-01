using System;
using System.Threading;
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

        void SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(string ncrDescription, bool approveNCR = true);

        void SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(string ncrDescription, bool approveNCR = true);

        void CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(string ncrDescription);

        void CloseNCR_CQMReview_Disapprove(UserType user, string ncrDescription);

        void CloseNCR_in_VerificationAndClosure(string ncrDescription);

        bool VerifyNCRDocIsDisplayedInReview(string ncrDescription);

        bool VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence(string ncrDescription);
    }

    public abstract class QaRcrdCtrl_GeneralNCR_WF_Impl : TestBase, IQaRcrdCtrl_GeneralNCR_WF
    {
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IQaRcrdCtrl_GeneralNCR_WF SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IQaRcrdCtrl_GeneralNCR_WF instance = new QaRcrdCtrl_GeneralNCR_WF(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_SGWay instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_SH249 instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_Garnet instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_GLX instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_I15South instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_I15Tech instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_LAX instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_LAX(driver);
            }
            return instance;
        }

        internal void NavigateToGeneralNcrPage()
        {
            driver = Driver;

            if (!driver.Title.Contains("NCR List"))
            {
                NavigateToPage.QARecordControl_General_NCR();
                QaRcrdCtrl_GeneralNCR.ClickTab_Creating_Revise();
                Assert.True(VerifyPageHeader("List of NCR Reports"));
            }
        }

        public virtual string Create_and_SaveForward_NCR(UserType user)
        {
            LogDebug("------------WF Create_and_SaveForward_NCR_Document-------------");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            QaRcrdCtrl_GeneralNCR.ClickBtn_New();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyReqFieldErrorLabelsForNewDoc(), "VerifyReqFieldErrorLabelsForNewDoc");
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
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayedInReview");
            ClickEditBtnForRow();

            //verify required field error label is shown
            QaRcrdCtrl_GeneralNCR.ClickBtn_Approve();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyReqFieldErrorLabelForTypeOfNCR(), "VerifyReqFieldErrorLabelForTypeOfNCR");
            QaRcrdCtrl_GeneralNCR.SelectRdoBtn_TypeOfNCR_Level1();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Approve();
        }

        public virtual void Review_and_Return_NCR_ForRevise(UserType user, string ncrDescription)
        {
            LogDebug("------------WF Return_ToRevise_FromCQMReview-------------");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayedInReview");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
        }
        public virtual void CloseNCR_CQMReview_Disapprove(UserType user, string ncrDescription)
        {
            LogDebug("------------WF CloseNCR_CQMReview_Disapprove-------------");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayedInReview");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_DisapproveClose();
        }

        public virtual void SaveForward_FromResolutionDisposition_ToVerificationClosure_ReturnToConformance(string ncrDescription)
        {
            LogDebug("------------WF TraverseNCR_FromReview_ToVerificationClosure_ReturnToConformance-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            ClickEditBtnForRow();
            //todo: click Save&Fwd button and verify required field error label is shown for Concession Request DDList
            QaRcrdCtrl_GeneralNCR.PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ReturnToConformance();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }

        public virtual void CheckReviseKickback_FromVerificationClosure_ForReturnToConformance(string ncrDescription)
        {
            LogDebug("------------WF Return_ToRevise_FromVerificationClosure-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_KickBack();
        }

        public virtual void SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(string ncrDescription)
        {
            LogDebug("------------WF SaveForward_FromResolutionDisposition_ToDeveloperConcurrence-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            ClickEditBtnForRow();

            //todo: click Save&Fwd button and verify required field error label is shown for Concession Request DDList
            QaRcrdCtrl_GeneralNCR.PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ConcessionDeviation();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }

        public virtual void SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(string ncrDescription, bool approveNCR = true)
        {
            LogDebug("------------WF SaveForward_FromDeveloperConcurrence_ToDOTApproval-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");
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

        public virtual void SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(string ncrDescription, bool approveNCR = true)
        {
            LogDebug("------------WF SaveForward_FromDOTApproval_ToVerificationClosure-------------");
            AddAssertionToList(VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence(ncrDescription), "VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence");
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
            
        public virtual void CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(string ncrDescription)
        {
            LogDebug("------------WF Return_ToRevise_FromVerificationClosure_ForConcessionDiviation-------------");

            SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_KickBack();
            SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
        }

        public virtual void CloseNCR_in_VerificationAndClosure(string ncrDescription)
        {
            LogDebug("------------WF CloseNCR_in_VerificationAndClosure-------------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(Reviewer.IQF_Manager);
            Thread.Sleep(1000);
            QaRcrdCtrl_GeneralNCR.SignDateApproveNCR(Reviewer.QC_Manager);
            QaRcrdCtrl_GeneralNCR.ClickBtn_Close();
        }

        public virtual bool VerifyNCRDocIsDisplayedInReview(string ncrDescription = "")
            => QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Review, ncrDescription);

        public virtual bool VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence(string ncrDescription)
            => QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.DOT_Approval, ncrDescription);

    }


    internal class QaRcrdCtrl_GeneralNCR_WF_GLX : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override bool VerifyNCRDocIsDisplayedInReview(string ncrDescription = "")
            =>QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.QM_Review, ncrDescription);

        public override void Review_and_Approve_NCR(UserType user, string ncrDescription)
        {
            LogDebug("------------WF Review_and_Approve_NCR_Document-------------");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayedInReview");
            ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Approve();
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

        public override bool VerifyNCRDocIsDisplayedInReview(string ncrDescription = "")
            => QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.QC_Review, ncrDescription);
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_SGWay : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override bool VerifyNCRDocIsDisplayedInReview(string ncrDescription = "")
            => QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.QC_Review, ncrDescription);
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_LAX : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_LAX(IWebDriver driver) : base(driver)
        {
        }

        //public override bool VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence(string ncrDescription)
        //    => QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.LAWA_Concurrence, ncrDescription);
    }
}