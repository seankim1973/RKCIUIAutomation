﻿using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.GeneralNCR;
using static RKCIUIAutomation.Base.Factory;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;

namespace RKCIUIAutomation.Page.Workflows
{
    public class QaRcrdCtrl_GeneralNCR_WF : QaRcrdCtrl_GeneralNCR_WF_Impl
    {
        public QaRcrdCtrl_GeneralNCR_WF()
        {
        }

        public QaRcrdCtrl_GeneralNCR_WF(IWebDriver driver) => this.Driver = driver;

        public T SetClass<T>(IWebDriver driver)
        {
            IQaRcrdCtrl_GeneralNCR_WF instance = new QaRcrdCtrl_GeneralNCR_WF(driver);

            if (tenantName == TenantNameType.SGWay)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_SGWay instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_SGWay(driver);
            }
            else if (tenantName == TenantNameType.SH249)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_SH249 instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_SH249(driver);
            }
            else if (tenantName == TenantNameType.Garnet)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_Garnet instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_Garnet(driver);
            }
            else if (tenantName == TenantNameType.GLX)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_GLX instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_GLX(driver);
            }
            else if (tenantName == TenantNameType.I15South)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_I15South instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_I15South(driver);
            }
            else if (tenantName == TenantNameType.I15Tech)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_I15Tech instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_I15Tech(driver);
            }
            else if (tenantName == TenantNameType.LAX)
            {
                log.Info($"###### using QaRcrdCtrl_GeneralNCR_WF_LAX instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_LAX(driver);
            }
            return (T)instance;
        }

        internal void NavigateToGeneralNcrPage()
        {
            string pgTitle = GetPageTitle();

            if (!pgTitle.Contains("NCR List"))
            {
                NavigateToPage.QARecordControl_General_NCR();
                QaRcrdCtrl_GeneralNCR.ClickTab_Creating_Revise();
                AddAssertion_VerifyNCRPageHeader("NavigateToGeneralNcrPage()");
            }
        }

        public override string Create_and_SaveForward_NCR(UserType user)
        {
            Report.Step("Workflow: Create_and_SaveForward_NCR_Document");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            QaRcrdCtrl_GeneralNCR.ClickBtn_New();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyReqFieldErrorLabelsForNewDoc(), "VerifyReqFieldErrorLabelsForNewDoc");
            QaRcrdCtrl_GeneralNCR.PopulateRequiredFieldsAndSaveForward();
            AddAssertionToList(VerifyPageHeader("List of NCR Reports"), "VerifyPageHeader('List of NCR Reports')");
            return QaRcrdCtrl_GeneralNCR.GetNCRDocDescription();
        }

        public override string Create_and_SaveOnly_NCR(UserType user)
        {
            Report.Step("Workflow: Create_and_SaveOnly_NCR_Document");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            QaRcrdCtrl_GeneralNCR.ClickBtn_New();
            QaRcrdCtrl_GeneralNCR.PopulateRequiredFieldsAndSaveOnly();
            AddAssertionToList(VerifyPageHeader("List of NCR Reports"), "VerifyPageHeader('List of NCR Reports')");
            return QaRcrdCtrl_GeneralNCR.GetNCRDocDescription();
        }

        public override void Review_and_Approve_NCR(UserType user, string ncrDescription, bool isResolution = false)
        {
            Report.Step("Workflow: Review_and_Approve_NCR_Document");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription, isResolution), "VerifyNCRDocIsDisplayedInReview");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Approve();
            //WaitForPageReady();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyReqFieldErrorLabelForTypeOfNCR(), "VerifyReqFieldErrorLabelForTypeOfNCR");
            QaRcrdCtrl_GeneralNCR.SelectRdoBtn_TypeOfNCR_Level1();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Approve();
        }

        public override void Review_and_Return_NCR_ForRevise(UserType user, string ncrDescription)
        {
            Report.Step("Workflow: Return_ToRevise_FromCQMReview");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayedInReview");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
        }

        public override void CloseNCR_CQMReview_Disapprove(UserType user, string ncrDescription)
        {
            Report.Step("Workflow: CloseNCR_CQMReview_Disapprove");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayedInReview");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_DisapproveClose();
        }

        public override void SaveForward_FromResolutionDisposition_ToVerificationClosure_ReturnToConformance(string ncrDescription)
        {
            Report.Step("Workflow: TraverseNCR_FromReview_ToVerificationClosure_ReturnToConformance");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            GridHelper.ClickEditBtnForRow();
            //todo: click Save&Fwd button and verify required field error label is shown for Concession Request DDList
            QaRcrdCtrl_GeneralNCR.PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ReturnToConformance();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }

        public override void CheckReviseKickback_FromVerificationClosure_ForReturnToConformance(string ncrDescription)
        {
            Report.Step("Workflow: Return_ToRevise_FromVerificationClosure");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_KickBack();
        }

        public override void SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(string ncrDescription, bool isResolution = false)
        {
            LogoutToLoginPage();

            if (!isResolution)
                Review_and_Approve_NCR(UserType.NCRMgr, ncrDescription, isResolution);
            else
            {
                LoginAs(UserType.NCRMgr);
                NavigateToGeneralNcrPage();
            }

            Report.Step("Workflow: SaveForward_FromResolutionDisposition_ToDeveloperConcurrence");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            GridHelper.ClickEditBtnForRow();

            //todo: click Save&Fwd button and verify required field error label is shown for Concession Request DDList
            QaRcrdCtrl_GeneralNCR.PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ConcessionDeviation();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }

        public override void SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(string ncrDescription, bool approveNCR = true)
        {
            Report.Step("Workflow: SaveForward_FromDeveloperConcurrence_ToDOTApproval");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");
            GridHelper.ClickEditBtnForRow();
            //todo: click Save&Fwd button and verify required field error label is shown (RecordEngineer_SignBtn, EngOfRecord, EngApprovalDate, ApprovalRadioBtn)

            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.EngineerOfRecord, approveNCR);
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }

        public override void SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(string ncrDescription, bool approveNCR = true)
        {
            Report.Step("Workflow: SaveForward_FromDOTApproval_ToVerificationClosure");
            AddAssertionToList(VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence(ncrDescription), "VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence");
            GridHelper.ClickEditBtnForRow();
            //todo: click Save&Fwd button and verify required field error label is shown(Owner_SignBtn, DOTReview, OwnerApprovalDate, OwnerApprovalRdoBtn)

            if (approveNCR)
            {
                QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.Owner);
            }
            else
            {
                QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.Owner, false);
            }

            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }

        public override void CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(string ncrDescription)
        {
            Report.Step("Workflow: Return_ToRevise_FromVerificationClosure_ForConcessionDiviation");

            SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_KickBack();
            SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(ncrDescription);
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
        }

        public override void CloseNCR_in_VerificationAndClosure(string ncrDescription)
        {
            Report.Step("Workflow: CloseNCR_in_VerificationAndClosure");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.IQF_Manager);
            Thread.Sleep(1000);
            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.QC_Manager);
            QaRcrdCtrl_GeneralNCR.ClickBtn_Close();
        }

        public override void SignDateApproveNCR(TableTab tabName)
        {
            if (tabName.Equals(TableTab.Developer_Concurrence))
            {
                QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.EngineerOfRecord);
            }
            else if (tabName.Equals(TableTab.Verification_and_Closure))
            {
                Report.Step("------  enter signature and name for IQF Mgr and QC Mgr then click SaveOnly  -------");
                QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.IQF_Manager);
                QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.QC_Manager);
                QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();
            }
        }

        public override void VerifySignatureNCR(TableTab tabName, string ncrDescription, bool shouldBeEmpty = false)
        {
            if (tabName.Equals(TableTab.Developer_Concurrence))
            {
                if (shouldBeEmpty)
                {
                    AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.EngineerOfRecord, true), "VerifySignatureField(Reviewer.EngineerOfRecord, true)");
                    AddAssertionToList(VerifyInputField(InputFields.Engineer_of_Record, true), "VerifyInputField(InputFields.Engineer_of_Record, true)");
                    AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Engineer_Approval_NA), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Engineer_Approval_NA)");
                }
                else
                {
                    AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.EngineerOfRecord), "VerifySignatureField(Reviewer.EngineerOfRecord)");
                    AddAssertionToList(VerifyInputField(InputFields.Engineer_of_Record), "VerifyInputField(InputFields.Engineer_of_Record)");
                    AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Engineer_Approval_Yes), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Engineer_Approval_Yes)");
                }
            }
            else if (tabName.Equals(TableTab.Verification_and_Closure))
            {
                AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.IQF_Manager), "VerifySignatureField(Reviewer.IQF_Manager)");
                AddAssertionToList(VerifyInputField(InputFields.IQF_Manager), "VerifyInputField(InputFields.IQF_Manager)");
                AddAssertionToList(VerifyInputField(InputFields.IQFManagerDate), "VerifyInputField(InputFields.IQFManagerDate)");
                AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.QC_Manager), "VerifySignatureField(Reviewer.QC_Manager)");
                AddAssertionToList(VerifyInputField(InputFields.QC_Manager), "VerifyInputField(InputFields.QC_Manager)");
                AddAssertionToList(VerifyInputField(InputFields.QCManagerApprovedDate), "VerifyInputField(InputFields.QCManagerApprovedDate)");
            }
        }

        public override bool VerifyNCRDocIsDisplayedInReview(string ncrDescription = "", bool isResolution = false)
            => QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Review, ncrDescription);

        public override bool VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence(string ncrDescription)
            => QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.DOT_Approval, ncrDescription);

        public override void VerifyNCRDocIsDisplayedInDevConcurrence(string ncrDescription)
            => AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");

        public override void VerifyNCRDocIsDisplayedInLAWAConcurrence(string ncrDescription)
            => AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence(ncrDescription), "VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence");

        public override void VerifyNCRDocIsDisplayedInVerificationAndClosure(string ncrDescription)
            => AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");

        public override void VerifyNCRDocIsDisplayedInResolutionAndDisposition(string ncrDescription)
            => AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");

        public override void AddAssertion_VerifyNCRPageHeader(string methodName)
            => AddAssertionToList_VerifyPageHeader(QaRcrdCtrl_GeneralNCR.ExpectedPageHeader, methodName);
    }

    public interface IQaRcrdCtrl_GeneralNCR_WF
    {
        void AddAssertion_VerifyNCRPageHeader(string methodName);
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
        void Review_and_Approve_NCR(UserType user, string ncrDescription, bool isResolution = false);
        void Review_and_Return_NCR_ForRevise(UserType user, string ncrDescription);
        void SaveForward_FromResolutionDisposition_ToVerificationClosure_ReturnToConformance(string ncrDescription);
        void CheckReviseKickback_FromVerificationClosure_ForReturnToConformance(string ncrDescription);
        void SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(string ncrDescription, bool isResolution = false);
        void SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(string ncrDescription, bool approveNCR = true);
        void SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(string ncrDescription, bool approveNCR = true);
        void CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(string ncrDescription);
        void CloseNCR_CQMReview_Disapprove(UserType user, string ncrDescription);
        void CloseNCR_in_VerificationAndClosure(string ncrDescription);
        bool VerifyNCRDocIsDisplayedInReview(string ncrDescription, bool isResolution = false);
        bool VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence(string ncrDescription);
        void VerifyNCRDocIsDisplayedInDevConcurrence(string ncrDescription);
        void SignDateApproveNCR(TableTab tabName);
        void VerifySignatureNCR(TableTab tabName, string ncrDescription, bool shouldBeEmpty = false);
        void VerifyNCRDocIsDisplayedInLAWAConcurrence(string ncrDescription);
        void VerifyNCRDocIsDisplayedInVerificationAndClosure(string ncrDescription);
        void VerifyNCRDocIsDisplayedInResolutionAndDisposition(string ncrDescription);
    }

    public abstract class QaRcrdCtrl_GeneralNCR_WF_Impl : TestBase, IQaRcrdCtrl_GeneralNCR_WF
    {
        public abstract void AddAssertion_VerifyNCRPageHeader(string methodName);
        public abstract void CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(string ncrDescription);
        public abstract void CheckReviseKickback_FromVerificationClosure_ForReturnToConformance(string ncrDescription);
        public abstract void CloseNCR_CQMReview_Disapprove(UserType user, string ncrDescription);
        public abstract void CloseNCR_in_VerificationAndClosure(string ncrDescription);
        public abstract string Create_and_SaveForward_NCR(UserType user);
        public abstract string Create_and_SaveOnly_NCR(UserType user);
        public abstract void Review_and_Approve_NCR(UserType user, string ncrDescription, bool isResolution = false);
        public abstract void Review_and_Return_NCR_ForRevise(UserType user, string ncrDescription);
        public abstract void SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(string ncrDescription, bool approveNCR = true);
        public abstract void SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(string ncrDescription, bool approveNCR = true);
        public abstract void SaveForward_FromResolutionDisposition_ToDeveloperConcurrence(string ncrDescription, bool isResolution = false);
        public abstract void SaveForward_FromResolutionDisposition_ToVerificationClosure_ReturnToConformance(string ncrDescription);
        public abstract void SignDateApproveNCR(TableTab tabName);
        public abstract void VerifyNCRDocIsDisplayedInDevConcurrence(string ncrDescription);
        public abstract bool VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence(string ncrDescription);
        public abstract void VerifyNCRDocIsDisplayedInLAWAConcurrence(string ncrDescription);
        public abstract void VerifyNCRDocIsDisplayedInResolutionAndDisposition(string ncrDescription);
        public abstract bool VerifyNCRDocIsDisplayedInReview(string ncrDescription, bool isResolution = false);
        public abstract void VerifyNCRDocIsDisplayedInVerificationAndClosure(string ncrDescription);
        public abstract void VerifySignatureNCR(TableTab tabName, string ncrDescription, bool shouldBeEmpty = false);
    }
    

    internal class QaRcrdCtrl_GeneralNCR_WF_GLX : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override bool VerifyNCRDocIsDisplayedInReview(string ncrDescription = "", bool isResolution = false)
            =>QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.QM_Review, ncrDescription);

        public override void Review_and_Approve_NCR(UserType user, string ncrDescription, bool isResolution = false)
        {
            Report.Step("Workflow: Review_and_Approve_NCR_Document");

            LoginAs(user);
            NavigateToGeneralNcrPage();
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDescription), "VerifyNCRDocIsDisplayedInReview");
            GridHelper.ClickEditBtnForRow();
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

        public override bool VerifyNCRDocIsDisplayedInReview(string ncrDescription = "", bool isResolution = false)
            => QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.QC_Review, ncrDescription);
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_SGWay : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override bool VerifyNCRDocIsDisplayedInReview(string ncrDescription = "", bool isResolution = false)
            => QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.QC_Review, ncrDescription);
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_LAX : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_LAX(IWebDriver driver) : base(driver)
        {
        }

        public override void SaveForward_FromDeveloperConcurrence_ToDOTApprovalOrLAWAConcurrence(string ncrDescription, bool approveNCR = true)
        {
            LogoutToLoginPage();

            Report.Step("Workflow: SaveForward_FromDeveloperConcurrence_ToDOTApproval");

            LoginAs(UserType.NCRDevConcur);
            NavigateToGeneralNcrPage();
            
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");
            GridHelper.ClickEditBtnForRow();

            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.EngineerOfRecord, approveNCR);
            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.CQC_Manager, approveNCR);

            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }

        public override void SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(string ncrDescription, bool approveNCR = true)
        {
            LogoutToLoginPage();
            //Review_and_Approve_NCR(UserType.NCRLawaConcur, ncrDescription);

            Report.Step("Workflow: SaveForward_FromDOTApproval_ToVerificationClosure");
            LoginAs(UserType.NCRLawaConcur);
            NavigateToGeneralNcrPage();

            AddAssertionToList(VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence(ncrDescription), "VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence");
            GridHelper.ClickEditBtnForRow();
            //todo: click Save&Fwd button and verify required field error label is shown(Owner_SignBtn, DOTReview, OwnerApprovalDate, OwnerApprovalRdoBtn)

            if (approveNCR)
            {
                QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.Owner);
            }
            else
            {
                QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.Owner, false);
            }

            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
        }

        public override bool VerifyNCRDocIsDisplayedInReview(string ncrDescription = "", bool isResolution = false)
            => QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(
                isResolution ? TableTab.Resolution_Disposition : TableTab.Review, 
                ncrDescription);

        public override void CloseNCR_in_VerificationAndClosure(string ncrDescription)
        {
            LogoutToLoginPage();
            Report.Step("Workflow: QPM CloseNCR_in_VerificationAndClosure");

            //Review_and_Approve_NCR(UserType.NCRQpm, ncrDescription);
            LoginAs(UserType.NCRQpm);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "NCR QPM - VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.IQF_Manager);
            Thread.Sleep(1000);
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            LogoutToLoginPage();
            Report.Step("Workflow: CQAM CloseNCR_in_VerificationAndClosure");

            LoginAs(UserType.NCRCqam);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "NCR CQAM - VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.QC_Manager);
            Thread.Sleep(1000);
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            LogoutToLoginPage();
            Report.Step("Workflow: OMQM CloseNCR_in_VerificationAndClosure");

            LoginAs(UserType.NCROmqm);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "NCR OMQM - VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.Operations_Manager);
            Thread.Sleep(1000);
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            Report.Step("Workflow: Final Closure CloseNCR_in_VerificationAndClosure");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "NCR Closure - VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Close();
        }

        public override void CheckReviseKickback_FromVerificationClosure_ForConcessionDiviation(string ncrDescription)
        {
            SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(ncrDescription);

            LogoutToLoginPage();
            Report.Step("Workflow: QPM CloseNCR_in_VerificationAndClosure");
            LoginAs(UserType.NCRQpm);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_KickBack();

            LogoutToLoginPage();
            Report.Step("Workflow: Lawa Concur CloseNCR_in_VerificationAndClosure");
            LoginAs(UserType.NCRLawaConcur);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.DOT_Approval, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            SaveForward_FromDOTApprovalOrLAWAConcurrence_ToVerificationClosure(ncrDescription);

            LogoutToLoginPage();
            Report.Step("Workflow: QPM Concur CloseNCR_in_VerificationAndClosure");
            LoginAs(UserType.NCRQpm);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.IQF_Manager);
            Thread.Sleep(1000);
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            LogoutToLoginPage();
            Report.Step("Workflow: CQAM Concur CloseNCR_in_VerificationAndClosure");
            LoginAs(UserType.NCRCqam);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "NCR CQAM - VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.QC_Manager, false);
            Thread.Sleep(1000);
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            LogoutToLoginPage();
            Report.Step("Workflow: QPM Concur CloseNCR_in_VerificationAndClosure");
            LoginAs(UserType.NCRQpm);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.IQF_Manager);
            Thread.Sleep(1000);
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            LogoutToLoginPage();
            Report.Step("Workflow: CQAM Concur CloseNCR_in_VerificationAndClosure");
            LoginAs(UserType.NCRCqam);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "NCR CQAM - VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.QC_Manager);
            Thread.Sleep(1000);
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            LogoutToLoginPage();
            Report.Step("Workflow: OMQM CloseNCR_in_VerificationAndClosure");
            LoginAs(UserType.NCROmqm);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "NCR OMQM - VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.Operations_Manager, false);
            Thread.Sleep(1000);
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            LogoutToLoginPage();
            Report.Step("Workflow: CQAM Concur CloseNCR_in_VerificationAndClosure");
            LoginAs(UserType.NCRCqam);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "NCR CQAM - VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.QC_Manager);
            Thread.Sleep(1000);
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            LogoutToLoginPage();
            Report.Step("Workflow: OMQM CloseNCR_in_VerificationAndClosure");
            LoginAs(UserType.NCROmqm);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "NCR OMQM - VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.Operations_Manager);
            Thread.Sleep(1000);
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

            Report.Step("Workflow: Final Closure CloseNCR_in_VerificationAndClosure");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "NCR Closure - VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();
        }

        public override void CheckReviseKickback_FromVerificationClosure_ForReturnToConformance(string ncrDescription)
        {
            Report.Step("Workflow: - QPM - Return_ToRevise_FromVerificationClosure");
            LogoutToLoginPage();
            LoginAs(UserType.NCRQpm);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "QPM - VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();

            LogoutToLoginPage();
            LoginAs(UserType.NCRMgr);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "QPM - VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();

            LogoutToLoginPage();
            LoginAs(UserType.NCRQpm);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "QPM - VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_KickBack();

            LogoutToLoginPage();
            LoginAs(UserType.NCRMgr);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "QPM - VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();

            Report.Step("Workflow: - CQAM - Return_ToRevise_FromVerificationClosure");

            LogoutToLoginPage();
            LoginAs(UserType.NCRCqam);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "CQAM - VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();

            LogoutToLoginPage();
            LoginAs(UserType.NCRMgr);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "CQAM - VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();

            LogoutToLoginPage();
            LoginAs(UserType.NCRCqam);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "CQAM - VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_KickBack();

            LogoutToLoginPage();
            LoginAs(UserType.NCRMgr);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "QPM - VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();

            Report.Step("Workflow: - OMQM - Return_ToRevise_FromVerificationClosure");

            LogoutToLoginPage();
            LoginAs(UserType.NCROmqm);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "OMQM - VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Revise();

            LogoutToLoginPage();
            LoginAs(UserType.NCRMgr);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "OMQM - VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();

            LogoutToLoginPage();
            LoginAs(UserType.NCROmqm);
            NavigateToGeneralNcrPage();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "OMQM - VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_KickBack();
        }

        public override void VerifyNCRDocIsDisplayedInDevConcurrence(string ncrDescription)
        {
            LogoutToLoginPage();
            LoginAs(UserType.NCRDevConcur);
            NavigateToPage.QARecordControl_General_NCR();
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence)");
        }

        public override void SignDateApproveNCR(TableTab tabName)
        {
            if (tabName.Equals(TableTab.Developer_Concurrence))
            {
                QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.EngineerOfRecord);
                QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.CQC_Manager);
            }
            else if (tabName.Equals(TableTab.Verification_and_Closure))
            {
                Report.Step("------  enter signature and name for IQF Mgr, QC Mgr & Operations Manager then click SaveOnly  -------");
                LogoutToLoginPage();
                LoginAs(UserType.NCRQpm);
                NavigateToPage.QARecordControl_General_NCR();
                QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription);
                GridHelper.ClickEditBtnForRow();
                QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.IQF_Manager);
                QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

                LogoutToLoginPage();
                LoginAs(UserType.NCRCqam);
                NavigateToPage.QARecordControl_General_NCR();
                QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription);
                GridHelper.ClickEditBtnForRow();
                QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.QC_Manager);
                QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();

                LogoutToLoginPage();
                LoginAs(UserType.NCROmqm);
                NavigateToPage.QARecordControl_General_NCR();
                QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription);
                GridHelper.ClickEditBtnForRow();
                QaRcrdCtrl_GeneralNCR.SignDateApproveNcrByReviewer(Reviewer.Operations_Manager);
                QaRcrdCtrl_GeneralNCR.ClickBtn_SaveOnly();
            }
        }

        public override void VerifySignatureNCR(TableTab tabName, string ncrDescription, bool shouldBeEmpty = false)
        {
            if (tabName.Equals(TableTab.Developer_Concurrence))
            {
                if (shouldBeEmpty)
                {
                    AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.EngineerOfRecord, true), "VerifySignatureField(Reviewer.EngineerOfRecord, true)");
                    AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.CQC_Manager, true), "VerifySignatureField(Reviewer.CQC_Manager, true)");
                    AddAssertionToList(VerifyInputField(InputFields.Engineer_of_Record, true), "VerifyInputField(InputFields.Engineer_of_Record, true)");
                    AddAssertionToList(VerifyInputField(InputFields.CQC_Manager, true), "VerifyInputField(InputFields.CQC_Manager, true)");
                    AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Engineer_Approval_NA), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Engineer_Approval_NA)");
                    AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.CQCMApproval_NA), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.CQCMApproval_NA)");
                }
                else
                {
                    AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.EngineerOfRecord), "VerifySignatureField(Reviewer.EngineerOfRecord)");
                    AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.CQC_Manager), "VerifySignatureField(Reviewer.CQC_Manager)");
                    AddAssertionToList(VerifyInputField(InputFields.Engineer_of_Record), "VerifyInputField(InputFields.Engineer_of_Record)");
                    AddAssertionToList(VerifyInputField(InputFields.CQC_Manager), "VerifyInputField(InputFields.CQC_Manager)");
                    AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Engineer_Approval_Yes), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Engineer_Approval_Yes)");
                    AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.CQCMApproval_Yes), "VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.CQCMApproval_Yes)");
                }
            }
            else if (tabName.Equals(TableTab.Verification_and_Closure))
            {
                AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.IQF_Manager), "VerifySignatureField(Reviewer.IQF_Manager)");
                AddAssertionToList(VerifyInputField(InputFields.IQF_Manager), "VerifyInputField(InputFields.IQF_Manager)");
                AddAssertionToList(VerifyInputField(InputFields.IQFManagerDate), "VerifyInputField(InputFields.IQFManagerDate)");
                AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.QC_Manager), "VerifySignatureField(Reviewer.QC_Manager)");
                AddAssertionToList(VerifyInputField(InputFields.QC_Manager), "VerifyInputField(InputFields.QC_Manager)");
                AddAssertionToList(VerifyInputField(InputFields.QCManagerApprovedDate), "VerifyInputField(InputFields.QCManagerApprovedDate)");
                AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifySignatureField(Reviewer.Operations_Manager), "VerifySignatureField(Reviewer.Operations_Manager)");
                AddAssertionToList(VerifyInputField(InputFields.OMQ_Manager), "VerifyInputField(InputFields.OMQ_Manager)");
                AddAssertionToList(VerifyInputField(InputFields.OMQManagerApprovedDate), "VerifyInputField(InputFields.OMQManagerApprovedDate)");
            }
        }

        public override void VerifyNCRDocIsDisplayedInLAWAConcurrence(string ncrDescription)
        {
            LogoutToLoginPage();
            LoginAs(UserType.NCRLawaConcur);
            NavigateToPage.QARecordControl_General_NCR();
            Report.Step("------  edit in DOT Approval tab and provide signature for DOT Review then click Cancel -------");
            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence(ncrDescription), "VerifyNCRDocIsDisplayedInDOTApprovalOrLAWAConcurrence");
        }

        public override void VerifyNCRDocIsDisplayedInVerificationAndClosure(string ncrDescription)
        {
            LogoutToLoginPage();
            LoginAs(UserType.NCRQpm);
            NavigateToPage.QARecordControl_General_NCR();
            Report.Step("------  edit in Verification and Closure tab then click Revise btn  -------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure)");
        }

        public override void VerifyNCRDocIsDisplayedInResolutionAndDisposition(string ncrDescription)
        {
            LogoutToLoginPage();
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
            Report.Step("------  edit in Resolution/Disposition tab and verify Concession Request DDL is set to 'Concession Deviation' from previous selection -------");
            AddAssertionToList(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription), "VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition)");

        }
    }
}