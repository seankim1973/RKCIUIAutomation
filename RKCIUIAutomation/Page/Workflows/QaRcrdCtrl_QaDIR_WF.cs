using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using RKCIUIAutomation.Test;
using System;
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
        void LoginToDirPage(UserType userType, bool useQaFieldMenu = false);

        void LoginToQaFieldDirPage(UserType userType);

        void LoginToRcrdCtrlDirPage(UserType userType);

        string Create_and_SaveForward_DIR();

        /// <summary>
        /// Returns string[] array: [0] dirNumber of newly created DIR, [1] dirNumber of Previous Failing Report
        /// </summary>
        string[] Create_and_SaveForward_DIR_with_Failed_Inspection_and_PreviousFailingReports();

        void Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab kickBackfromTableTab, string dirNumber);

        void Return_DIR_ForRevise_FromQcReview_then_Edit_SaveForward(string dirNumber);

        void Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber);

        void Modify_Cancel_Verify_inCreateRevise(string dirNumber);

        void Modify_Save_Verify_and_SaveForward_inCreateRevise(string dirNumber);

        void LogoutLoginAsQaTech_Edit_Result_SaveForward_then_LogoutLoginAsQaMgr(string dirNumber);

        void Enter_EngineerComments_and_Approve_inQcReview(string dirNumber);

        void Verify_DIR_then_Approve_inReview(string dirNumber);

        void Verify_DIR_then_Approve_inAuthorization(string dirNumber);

        bool VerifyDirIsDisplayedInCreate(string dirNumber);

        bool VerifyDirIsDisplayedInRevise(string dirNumber);

        bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber);

        bool VerifyWorkflowLocationAfterSimpleWF_forDirRevision(string dirNumber, string expectedRevision);

        bool Verify_DIR_Delete(TableTab tableTab, string dirNumber, bool acceptAlert = true);

        void ClickBtn_ApproveOrNoError();

        void ClickBtn_KickBackOrRevise();

        bool Verify_DirRevision_inTblRow_then_SaveForward_inCreateRevise(string dirNumber, string expectedDirRev);

        bool Verify_DirRevision_inTblRow_then_Approve_inQcReview(string dirNumber, string expectedDirRev);

        bool Verify_DirRevision_inTblRow_then_Approve_inAuthorization(string dirNumber, string expectedDirRev);
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

        //GLX, LAX, I15SB, I15Tech
        public virtual bool VerifyDirIsDisplayedInCreate(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber);

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

        public virtual string[] Create_and_SaveForward_DIR_with_Failed_Inspection_and_PreviousFailingReports()
        {
            LogDebug($"---> Create_and_SaveForward_Failed_Inspection_DIR_with_PreviousFailingReports <---");

            QaRcrdCtrl_QaDIR.ClickBtn_CreateNew();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyReqFieldErrorsForNewDir(), "VerifyReqFieldErrorsForNewDir");
            QaRcrdCtrl_QaDIR.PopulateRequiredFields();
            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_F();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_Deficiencies_Yes();
            QaRcrdCtrl_QaDIR.EnterText_DeficiencyDescription();
            string previousFailedDirNumber = QaRcrdCtrl_QaDIR.CreatePreviousFailingReport();
            string dirNumber = QaRcrdCtrl_QaDIR.GetDirNumber();
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInCreate(dirNumber), $"VerifyDirIsDisplayedInCreate DirNo. {dirNumber}");
            ClickEditBtnForRow();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyPreviousFailingDirEntry(previousFailedDirNumber), $"VerifyPreviousFailingDirEntry in Create: {previousFailedDirNumber}");
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            string[] dirNumbers = new string[2]
                {
                    dirNumber,
                    previousFailedDirNumber
                };
            return dirNumbers;
        }

        public virtual void Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab kickBackfromTableTab, string dirNumber)
        {
            LogDebug($"---> KickBack_DIR_ForRevise_From{kickBackfromTableTab.ToString()}Tab_then_Edit_inCreateReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(kickBackfromTableTab, dirNumber), "VerifyDirIsDisplayed");
            ClickEditBtnForRow();
            WF_QaRcrdCtrl_QaDIR.ClickBtn_KickBackOrRevise();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_SendEmailForRevise_No();
            QaRcrdCtrl_QaDIR.ClickBtn_SubmitRevise();
        }

        public virtual void Return_DIR_ForRevise_FromQcReview_then_Edit_SaveForward(string dirNumber)
            => WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab.QC_Review, dirNumber);

        public virtual void Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber)
        {
            WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab.Authorization, dirNumber);
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
        }

        private void Upload_Cancel_Verify_inAttachments(string dirNumber)
        {
            LogDebug($"---> Upload_Cancel_Verify_inAttachments <---");

            UploadFile();
        }

        public virtual void Modify_Cancel_Verify_inCreateRevise(string dirNumber)
        {
            LogDebug($"---> Modify_Cancel_Verify_inCreateRevise <---");

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
            LogDebug($"---> Modify_Save_Verify_and_SaveForward_inCreateRevise <---");

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

        public virtual void LogoutLoginAsQaTech_Edit_Result_SaveForward_then_LogoutLoginAsQaMgr(string dirNumber)
        {
            LogDebug($"---> LogoutLoginAsQaTech_Edit_Result_SaveForward_then_LogoutLoginAsQaMgr <---");

            LogoutToLoginPage();
            LoginAs(UserType.DIRTechQA);
            NavigateToPage.QAField_QA_DIRs();

            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed(TableTab.Create_Revise)");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_E(false); //Edit 'Results' checkbox in Revise
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();

            LogoutToLoginPage();
            LoginAs(UserType.DIRMgrQA);
            NavigateToPage.QAField_QA_DIRs();
        }

        //GLX, I15SB, I15Tech, LAX
        public virtual void Enter_EngineerComments_and_Approve_inQcReview(string dirNumber)
        {
            LogDebug($"---> Verify_EngineerComments_and_Approve_inQcReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review");
            ClickEditBtnForRow();
            ClearText(GetTextAreaFieldByLocator(InputFields.Engineer_Comments));
            //WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
            //AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyEngineerCommentsReqFieldErrors(), "VerifyEngineerCommentsReqFieldErrors");
            QaRcrdCtrl_QaDIR.EnterText_EngineerComments();
            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
        }

        private void Verify_DIR_then_Approve(TableTab tableTab, string dirNumber)
        {
            string tableTabName = tableTab.ToString();
            LogDebug($"---> Verify_DIR_then_Approve_in {tableTabName} <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber), $"VerifyDirIsDisplayed(TableTab.{tableTabName})");
            ClickEditBtnForRow();
            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
        }

        public virtual void Verify_DIR_then_Approve_inReview(string dirNumber)
            => Verify_DIR_then_Approve(TableTab.QC_Review, dirNumber);

        public virtual void Verify_DIR_then_Approve_inAuthorization(string dirNumber)
            => Verify_DIR_then_Approve(TableTab.Authorization, dirNumber);

        public virtual bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Closed);

        public virtual bool VerifyWorkflowLocationAfterSimpleWF_forDirRevision(string dirNumber, string expectedRevision)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Closed, true, expectedRevision);

        public virtual void ClickBtn_ApproveOrNoError()
            => QaRcrdCtrl_QaDIR.ClickBtn_Approve();

        public virtual void ClickBtn_KickBackOrRevise()
            => QaRcrdCtrl_QaDIR.ClickBtn_KickBack();

        public virtual bool Verify_DIR_Delete(TableTab tableTab, string dirNumber, bool acceptAlert = true)
        {
            LogDebug($"---> Verify_DIR_Delete in {tableTab.ToString()} - Accept Alert: {acceptAlert} <---");

            bool isDisplayed = false;
            string actionPerformed = string.Empty;
            string resultAfterAction = string.Empty;
            bool result = false;

            try
            {
                isDisplayed = QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber);
                AddAssertionToList(isDisplayed, $"VerifyDirIsDisplayed in {tableTab.ToString()}, before interacting with delete dialog");

                if (isDisplayed)
                {
                    ClickDeleteBtnForRow();
                    if (acceptAlert)
                    {
                        try
                        {
                            AcceptAlertMessage();
                            AcceptAlertMessage();
                        }
                        catch (Exception e)
                        {
                            log.Debug(e.Message);
                        }
                        finally
                        {
                            actionPerformed = "Accepted";
                            resultAfterAction = "Displayed After Accepting Delete Dialog: ";
                        }
                    }
                    else
                    {
                        try
                        {
                            DismissAlertMessage();
                            DismissAlertMessage();
                        }
                        catch (Exception e)
                        {
                            log.Debug(e.Message);
                        }
                        finally
                        {
                            actionPerformed = "Dismissed";
                            resultAfterAction = "Displayed After Dismissing Delete Dialog: ";
                        }
                    }

                    var verifyResult = QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber, acceptAlert);

                    result = acceptAlert ? !verifyResult : verifyResult;

                    AddAssertionToList(result, $"VerifyDirIsDisplayed in {tableTab.ToString()}, after {actionPerformed} delete dialog");
                    LogInfo($"Performed Action: {actionPerformed} delete dialog<br>{resultAfterAction}{isDisplayed}", result);
                }
                else
                {
                    LogError($"Unable to find DIR No. {dirNumber}");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return result;
        }

        private bool Verify_DirRevision_inTblRow_then_Approve(TableTab tableTab, string dirNumber, string expectedDirRev)
        {
            LogDebug($"---> Verify_DirRevision_inTblRow_then_Approve in {tableTab.ToString()} - Expected DIR Rev: {expectedDirRev} <---");

            bool dirRevMatches = false;
            string ifFalseLog = string.Empty;
            bool isDisplayed = false;

            try
            {
                if (string.IsNullOrEmpty(expectedDirRev))
                {
                    bool dirRevFound = VerifyRecordIsDisplayed(QADIRs.ColumnName.Revision, expectedDirRev);
                    AddAssertionToList(dirRevFound, $"VerifyRecordIsDisplayed - DIR Revision: {expectedDirRev}");
                    LogInfo($"Successfully filtered table by expected DIR Revision: {expectedDirRev}", dirRevFound);
                }

                isDisplayed = (tableTab == TableTab.Creating || tableTab == TableTab.Create_Revise)
                    ? WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber)
                    : QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber, false);

                string actualDirRev = isDisplayed ? GetColumnValueForRow(dirNumber, "Revision") : "DIR Not Found";
                dirRevMatches = actualDirRev.Equals(expectedDirRev);
                ifFalseLog = dirRevMatches ? "" : $"<br>Actual DIR Rev: {actualDirRev}";

                if (isDisplayed)
                {
                    ClickEditBtnForRow();

                    if (tableTab == TableTab.Creating || tableTab == TableTab.Create_Revise)
                    {
                        QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
                    }
                    else
                    {
                        WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }

            LogInfo($"Expected DIR Rev: {expectedDirRev}{ifFalseLog}<br>Actual Matches Expected Rev? : {dirRevMatches}", dirRevMatches);
            return dirRevMatches;
        }

        public virtual bool Verify_DirRevision_inTblRow_then_SaveForward_inCreateRevise(string dirNumber, string expectedDirRev)
            => Verify_DirRevision_inTblRow_then_Approve(TableTab.Create_Revise, dirNumber, expectedDirRev);

        public virtual bool Verify_DirRevision_inTblRow_then_Approve_inQcReview(string dirNumber, string expectedDirRev)
            => Verify_DirRevision_inTblRow_then_Approve(TableTab.QC_Review, dirNumber, expectedDirRev);

        public virtual bool Verify_DirRevision_inTblRow_then_Approve_inAuthorization(string dirNumber, string expectedDirRev)
            => Verify_DirRevision_inTblRow_then_Approve(TableTab.Authorization, dirNumber, expectedDirRev);
    }

    internal class QaRcrdCtrl_QaDIR_WF_GLX : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override void Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab kickBackfromTableTab, string dirNumber)
        {
            LogDebug($"---> GLX - KickBack_DIR_ForRevise_FromTab_{kickBackfromTableTab.ToString()}_then_Edit_inCreateReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(kickBackfromTableTab, dirNumber), $"VerifyDirIsDisplayed({kickBackfromTableTab.ToString()})");
            ClickEditBtnForRow();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifySectionDescription(), "VerifySectionDescription");
            WF_QaRcrdCtrl_QaDIR.ClickBtn_KickBackOrRevise();
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

        public override bool VerifyDirIsDisplayedInCreate(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Creating, dirNumber);

        public override void Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber)
            => LogInfo("---> KickBack_DIR_ForRevise_FromAuthorization_then_Edit <---<br>Step skipped for Tenant SH249");

        public override void Verify_DIR_then_Approve_inAuthorization(string dirNumber)
            => LogInfo("---> Verify_DIR_then_Approve_inAuthorization <---<br>Step skipped for Tenant SH249");

        public override bool Verify_DirRevision_inTblRow_then_Approve_inAuthorization(string dirNumber, string expectedDirRev)
        {
            LogInfo("---> Verify_DirRevision_inTblRow_then_Approve_inAuthorization <---<br>Step skipped for Tenant SH249");
            return true;
        }

        public override bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment);

        public override bool VerifyWorkflowLocationAfterSimpleWF_forDirRevision(string dirNumber, string expectedRevision)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment, true, expectedRevision);

        public override void ClickBtn_ApproveOrNoError() => QaRcrdCtrl_QaDIR.ClickBtn_NoError();

        public override void ClickBtn_KickBackOrRevise() => QaRcrdCtrl_QaDIR.ClickBtn_Revise();

        public override void Enter_EngineerComments_and_Approve_inQcReview(string dirNumber)
        {
            LogDebug($"---> Verify_EngineerComments_and_Approve_inQcReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review");
            ClickEditBtnForRow();
            ClearText(GetTextAreaFieldByLocator(InputFields.Engineer_Comments));
            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyEngineerCommentsReqFieldErrors(), "VerifyEngineerCommentsReqFieldErrors");
            QaRcrdCtrl_QaDIR.EnterText_EngineerComments();
            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
        }
    }

    internal class QaRcrdCtrl_QaDIR_WF_SGWay : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override bool VerifyDirIsDisplayedInRevise(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Revise, dirNumber);

        public override bool VerifyDirIsDisplayedInCreate(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Creating, dirNumber);

        public override void Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber)
        {
            WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab.Authorization, dirNumber);
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Authorization, dirNumber), "VerifyDirIsDisplayed");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_Back_To_QC_Review();
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
        }

        public override bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment);

        public override bool VerifyWorkflowLocationAfterSimpleWF_forDirRevision(string dirNumber, string expectedRevision)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment, true, expectedRevision);

        public override void ClickBtn_ApproveOrNoError() => QaRcrdCtrl_QaDIR.ClickBtn_NoError();

        public override void ClickBtn_KickBackOrRevise() => QaRcrdCtrl_QaDIR.ClickBtn_Revise();

        public override void Enter_EngineerComments_and_Approve_inQcReview(string dirNumber)
        {
            LogDebug($"---> Verify_EngineerComments_and_Approve_inQcReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review");
            ClickEditBtnForRow();
            ClearText(GetTextAreaFieldByLocator(InputFields.Engineer_Comments));
            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyEngineerCommentsReqFieldErrors(), "VerifyEngineerCommentsReqFieldErrors");
            QaRcrdCtrl_QaDIR.EnterText_EngineerComments();
            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
        }
    }

    internal class QaRcrdCtrl_QaDIR_WF_LAX : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}