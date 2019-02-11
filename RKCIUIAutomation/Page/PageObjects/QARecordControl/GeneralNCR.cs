using MiniGuids;
using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.GeneralNCR;

namespace RKCIUIAutomation.Page.PageObjects.QARecordControl
{
    #region NCR Generic class

    public class GeneralNCR : GeneralNCR_Impl
    {
        public GeneralNCR()
        {
        }

        public GeneralNCR(IWebDriver driver) => this.Driver = driver;

        public enum InputFields
        {
            [StringValue("IssuedDate")] IssuedDate,
            [StringValue("ForemanNotificationDate")] ForemanNotificationDate,
            [StringValue("ManagerNotificationDate")] ManagerNotificationDate,
            [StringValue("Originator")] Originator,
            [StringValue("CrewForeman")] Foreman,
            [StringValue("Specification")] Specification,
            [StringValue("StructureId")] Location,
            [StringValue("AreaId")] Area,
            [StringValue("SegmentId")] Segment,
            [StringValue("RoadwayId")] Roadway,
            [StringValue("ResponsibleManager")] ResponsibleManager,
            [StringValue("NonConformance")] Description_of_Nonconformance, //Description input field for Complex workflow
            [StringValue("RootCause")] RootCause_of_the_Problem, //GLX
            [StringValue("PreparedBy")] PreparedBy, //GLX DDList
            [StringValue("PreparedByDate")] PreparedBy_Date,
            [StringValue("NcrDescription")] Description_of_NCR, //Description input field for Simple workflow
            [StringValue("FeatureId")] Feature,
            [StringValue("SubFeatureId")] SubFeature,
            [StringValue("OtherLocation")] OtherLocation,
            [StringValue("ContainmentActions")] ContainmentActions,
            [StringValue("CorrectiveAction")] CorrectiveAction,
            [StringValue("RepairPlan")] RepairPlan,
            [StringValue("RecordEngineer")] Engineer_of_Record,
            [StringValue("RecordEngineerApprovedDate")] RecordEngineerApprovedDate,
            [StringValue("RecordEngineerSignature")] RecordEngineer_SignBtn,
            [StringValue("Owner")] Owner_Review,
            [StringValue("OwnerDate")] OwnerDate,
            [StringValue("OwnerSignature")] Owner_SignBtn,
            [StringValue("CompletionDate")] CompletionDate,
            [StringValue("CIQM")] CQCM, //for Simple workflow
            [StringValue("CIQMDate")] CQCMDate, //for Simple workflow
            [StringValue("QualityManager")] CQAM, //for Simple workflow
            [StringValue("QualityManagerApprovedDate")] CQAMDate, //for Simple workflow
            [StringValue("NcrTypeSubmit")] Type_of_NCR,
            [StringValue("ConcessionRequest")] Concession_Request,
            [StringValue("IQFManager")] IQF_Manager,
            [StringValue("IQFManagerDate")] IQFManagerDate,
            [StringValue("IQFManagerSignature")] IQFManager_SignBtn,
            [StringValue("QualityManager")] QC_Manager,
            [StringValue("QualityManagerApprovedDate")] QCManagerApprovedDate,
            [StringValue("QualityManagerSignature")] QCManager_SignBtn,
            [StringValue("ContainmentActionSignature")] ContainmentActionManager_SignBtn
        }

        public enum TableTab
        {
            [StringValue("All NCRs")] All_NCRs,
            [StringValue("ALL NCRs")] ALL_NCRs,
            [StringValue("Closed NCR")] Closed_NCR,
            [StringValue("CQM Review")] CQM_Review,
            [StringValue("QM Review")] QM_Review,
            [StringValue("Create/Revise")] Create_Revise,
            [StringValue("Creating/Revise")] Creating_Revise,
            [StringValue("Developer Concurrence")] Developer_Concurrence,
            [StringValue("DOT Approval")] DOT_Approval,
            [StringValue("Engineer Concurrence")] Engineer_Concurrence,
            [StringValue("Originator Concurrence")] Originator_Concurrence,
            [StringValue("Owner Concurrence")] Owner_Concurrence,
            [StringValue("QC Review")] QC_Review,
            [StringValue("Resolution/Disposition")] Resolution_Disposition,
            [StringValue("Review/Assign NCR")] Review_Assign_NCR,
            [StringValue("Revise")] Revise,
            [StringValue("To Be Closed")] To_Be_Closed,
            [StringValue("Verification")] Verification,
            [StringValue("Verification and Closure")] Verification_and_Closure,
            [StringValue("EOR Concurrence")] EOR_Concurrence,
            [StringValue("MBTA Concurrence")] MBTA_Concurrence,
            [StringValue("GLXC Acceptance")] GLXC_Acceptance,
            [StringValue("QA Verification")] QA_Verification,
            [StringValue("QM Closure")] QM_Closure
        }

        public enum ColumnName
        {
            [StringValue("NcrNo")] NcrNo,
            [StringValue("RevisedBy")] SentBy,
            [StringValue("RevisedDate")] SentDate,
            [StringValue("Description")] Description,
            [StringValue("StatusFlowItemName")] WorkflowLocation,
            [StringValue("LockedBy")] LockedBy,
            [StringValue("LockedDate")] LockedDate,
            [StringValue("Id")] Action
        }

        public enum SubmitButtons
        {
            [StringValue("Close")] Close,
            [StringValue("Cancel")] Cancel,
            [StringValue("Revise")] Revise,
            [StringValue("Approve")] Approve,
            [StringValue("Kick back")] KickBack,
            [StringValue("Save Only")] SaveOnly,
            [StringValue("Save & Forward")] SaveForward,
            [StringValue("Disapprove & Close")] DisapproveClose,
        }

        public enum RadioBtnsAndCheckboxes
        {
            [StringValue("NcrType_1")] TypeOfNCR_Level1,
            [StringValue("NcrType_2")] TypeOfNCR_Level2,
            [StringValue("NcrType_3")] TypeOfNCR_Level3,
            [StringValue("RecordEngineerApproval_")] Engineer_Approval_NA,
            [StringValue("RecordEngineerApproval_True")] Engineer_Approval_Yes,
            [StringValue("RecordEngineerApproval_False")] Engineer_Approval_No,
            [StringValue("OwnerApproval_")] Owner_Approval_NA,
            [StringValue("OwnerApproval_True")] Owner_Approval_Yes,
            [StringValue("OwnerApproval_False")] Owner_Approval_No,
            [StringValue("AsBuiltRequired")] ChkBox_As_Built_Required,
            [StringValue("ActionCorrect")] ChkBox_Correct_Rework,
            [StringValue("ActionReplace")] ChkBox_Replace,
            [StringValue("ActionAccept")] ChkBox_Accept_As_Is,
            [StringValue("ActionRepair")] ChkBox_Repair,
            [StringValue("Critical")] ChkBox_Critical
        }

        public enum Reviewer
        {
            Owner,
            IQF_Manager,
            QC_Manager,
            EngineerOfRecord
        }

        [ThreadStatic]
        internal static string ncrDescription;

        [ThreadStatic]
        internal static string ncrNewDescription;

        [ThreadStatic]
        internal static string ncrDescKey;

        [ThreadStatic]
        internal static string ncrNewDescKey;

        internal MiniGuid guid;

        internal static readonly By newBtn_ByLocator = By.XPath("//div[@id='NcrGrid_Revise']/div/a[contains(@class, 'k-button')]");

        internal static readonly By exportToExcel_ByLocator = By.XPath("//div[@class='k-content k-state-active']//button[text()='Export to Excel']");

        internal void ActionConfirmation(SubmitButtons submitButton, bool acceptAlert = true)
        {
            try
            {
                JsClickElement(GetSubmitButtonByLocator(submitButton));
            }
            catch (UnhandledAlertException e)
            {
                log.Debug(e.AlertText);
            }

            ConfirmActionDialog();
        }

        internal void ClickBtn_Sign(InputFields signBtnType)
            => JsClickElement(By.XPath($"//a[@signaturehidden='{signBtnType.GetString()}']"));

        internal By SignaturePanelBtnXPathLocator(string btnName)
            => By.XPath($"//div[@id='ncrSignaturePopup']//a[text()='{btnName}']");

        internal string EnterDesc(string desc, InputFields descField, bool tempDescription = false, bool replaceCurrentDesc = true)
        {
            By descLocator = GetTextAreaFieldByLocator(descField);

            if (replaceCurrentDesc)
            {
                CreateNcrDescription(tempDescription);
            }
            desc = desc.Equals("") || string.IsNullOrEmpty(desc)
                ? GetVar(tempDescription
                    ? ncrNewDescKey
                    : ncrDescKey)
                : desc;
            EnterText(descLocator, desc);
            return desc;
        }

        internal void CreateNcrDescription(bool tempDescription = false)
        {
            guid = MiniGuid.NewGuid();
            string descKey = $"{tenantName}{GetTestName()}";
            string logMsg = string.Empty;
            string descValue = string.Empty;

            if (tempDescription)
            {
                ncrNewDescKey = $"{descKey}_NcrNewDescription";
                CreateVar(ncrNewDescKey, guid);
                ncrNewDescription = GetVar(ncrNewDescKey);
                descKey = ncrNewDescKey;
                descValue = ncrNewDescription;
                logMsg = "new temp ";
            }
            else
            {
                ncrDescKey = $"{descKey}_NcrDescription";
                CreateVar(ncrDescKey, guid);
                ncrDescription = GetVar(ncrDescKey);
                descKey = ncrDescKey;
                descValue = ncrDescription;
                logMsg = "";
            }

            log.Debug($"#####Created a {logMsg}NCR Description: KEY: {descKey} VALUE: {descValue}");
        }

        internal bool CheckNCRisClosed(string description, TableTab closedTab)
        {
            bool ncrIsClosed = false;
            string logMsg = "not found.";

            try
            {
                string _ncrDesc = description.Equals("") ? GetNCRDocDescription() : description;
                bool isDisplayed = VerifyNCRDocIsDisplayed(closedTab, _ncrDesc);

                if (isDisplayed)
                {
                    string docStatus = GetColumnValueForRow(_ncrDesc, "Workflow location");
                    ncrIsClosed = docStatus.Equals("Closed") ? true : false;
                    logMsg = $"Workflow Location displayed as: {docStatus}";
                }

                LogInfo($"NCR with description ({_ncrDesc}), {logMsg}", ncrIsClosed);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return ncrIsClosed;
        }
    }

    #endregion NCR Generic class

    #region workflow interface class

    public interface IGeneralNCR
    {
        void ClickBtn_Sign_RecordEngineer();

        void ClickBtn_Sign_Owner();

        void ClickBtn_Sign_IQFManager();

        void ClickBtn_Sign_QCManager();

        void ClickBtn_SignaturePanel_OK();

        void ClickBtn_SignaturePanel_Clear();

        /// <summary>
        /// Clicks Sign button, clicks OK button on signature panel, enters review field and date.
        /// <para>Selects Approval radio button (defaults to 'Yes'), when EngineerOfRecord or Owner is provided as Reviewer agrument.</para>
        /// </summary>
        /// <param name="reviewer"></param>
        /// <param name="Approve"></param>
        void SignDateApproveNCR(Reviewer reviewer, bool Approve = true);

        void ClickBtn_New();

        void ClickBtn_ExportToExcel();

        void ClickBtn_Cancel();

        void ClickBtn_Revise();

        void ClickBtn_Approve();

        void ClickBtn_DisapproveClose();

        void ClickBtn_SaveOnly();

        void ClickBtn_SaveForward();

        void ClickBtn_KickBack();

        void ClickBtn_Close();

        void ClickTab_All_NCRs();

        void ClickTab_Closed_NCR();

        void ClickTab_CQM_Review();

        void ClickTab_Creating_Revise();

        void ClickTab_Developer_Concurrence();

        void ClickTab_DOT_Approval();

        void ClickTab_Engineer_Concurrence();

        void ClickTab_Originator_Concurrence();

        void ClickTab_Owner_Concurrence();

        void ClickTab_QC_Review();

        void ClickTab_Resolution_Disposition();

        void ClickTab_Review_Assign_NCR();

        void ClickTab_Revise();

        void ClickTab_To_Be_Closed();

        void ClickTab_Verification();

        void ClickTab_Verification_and_Closure();

        void FilterDescription(string description = "");

        void SortTable_Descending();

        void SortTable_Ascending();

        void SortTable_ToDefault();

        void SelectRdoBtn_TypeOfNCR_Level1();

        void SelectRdoBtn_TypeOfNCR_Level2();

        void SelectRdoBtn_TypeOfNCR_Level3();

        void SelectRdoBtn_OwnerApproval_Yes();

        void SelectRdoBtn_OwnerApproval_No();

        void SelectRdoBtn_OwnerApproval_NA();

        void SelectRdoBtn_EngOfRecordApproval_Yes();

        void SelectRdoBtn_EngOfRecordApproval_No();

        void SelectRdoBtn_EngOfRecordApproval_NA();

        void SelectChkbox_AsBuiltRequired(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_RcmndDisposition_CorrectRework(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_RcmndDisposition_Replace(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_RcmndDisposition_AcceptAsIs(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_RcmndDisposition_Repair(bool toggleChkboxIfAlreadySelected = true);

        void SelectDDL_Originator(int selectionIndexOrName = 1);

        void SelectDDL_ResponsibleManager(int selectionIndexOrName = 1);

        void SelectDDL_Foreman(int selectionIndex = 1);

        void SelectDDL_Specification(int selectionIndex = 1);

        void SelectDDL_Location(int selectionIndex = 1);

        void SelectDDL_Area(int selectionIndex = 1);

        void SelectDDL_Segment(int selectionIndex = 1);

        void SelectDDL_Roadway(int selectionIndex = 1);

        void SelectDDL_Feature(int selectionIndex = 2);

        void SelectDDL_SubFeature(int selectionIndex = 1);

        void SelectDDL_PreparedBy(int selectionIndex = 1);

        void PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ReturnToConformance();

        void PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ConcessionDeviation();

        void EnterIssuedDate(string shortDate = "1/1/9999");

        void EnterForemanNotificationDate(string dateTime = "1/1/9999 12:00 AM");

        void EnterManagerNotificationDate(string dateTime = "1/1/9999 12:00 AM");

        void EnterResponsibleManager(string mgrName);

        string EnterDescription(string description = "", bool tempDescription = false);

        string EnterDescriptionOfNCR(string description = "", bool tempDescription = false);

        string EnterRootCauseOfTheProblem(string description = "", bool tempDescription = false);

        void EnterCorrectiveActionPlanToResolveNonconformance(string actionPlanText = "");

        void EnterRepairPlan(string repairPlanText = "");

        void EnterEngineerOfRecord(string engOfRecordText = "");

        void EnterRecordEngineerApprovedDate();

        void EnterOwnerReview(string ownerReviewText = "");

        void EnterOwnerApprovedDate();

        void EnterIQFManager(string iqfMgrText = "");

        void EnterIQFManagerApprovedDate();

        void EnterQCManager(string qcMgrText = "");

        void EnterQCManagerApprovedDate();

        void EnterCQCM();

        void EnterCQCMDate();

        void EnterCQAM();

        void EnterCQAMDate();

        void EnterPreparedByDate();

        void PopulateRequiredFields();

        void PopulateRequiredFieldsAndSaveForward();

        void PopulateRequiredFieldsAndSaveOnly();

        bool VerifyReqFieldErrorLabelsForNewDoc();

        /// <summary>
        /// Clicks specified table tab, filters table by NCR Description column, then verifies NCR document is shown.
        /// <para>Method will get unique NCR Description value, based on [ThreadStatic] NcrDescKey if, NCRDescription string is not supplied.</para>
        /// </summary>
        /// <param name="tableTab"></param>
        /// <param name="ncrDescription"></param>
        /// <returns>Return true if NCR document is shown in the tab specified</returns>
        bool VerifyNCRDocIsDisplayed(TableTab tableTab, string ncrDescription = "");

        bool VerifyNCRDocIsDisplayedInReview(string ncrDescription = "");

        bool VerifyNCRDocIsClosed(string ncrDescription = "");

        bool VerifyReqFieldErrorLabelForTypeOfNCR();

        bool VerifySignatureField(Reviewer reviewer, bool shouldBeEmpty = false);

        string GetNCRDocDescription(bool tempDescription = false);

        IList<string> GetExpectedRequiredFieldIDs();
    }

    #endregion workflow interface class

    #region Common Workflow Implementation class

    public abstract class GeneralNCR_Impl : PageBase, IGeneralNCR
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IGeneralNCR SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IGeneralNCR instance = new GeneralNCR(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using GeneralNCR_SGWay instance ###### ");
                instance = new GeneralNCR_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using  GeneralNCR_SH249 instance ###### ");
                instance = new GeneralNCR_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using  GeneralNCR_Garnet instance ###### ");
                instance = new GeneralNCR_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using  GeneralNCR_GLX instance ###### ");
                instance = new GeneralNCR_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using  GeneralNCR_I15South instance ###### ");
                instance = new GeneralNCR_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using GeneralNCR_I15Tech instance ###### ");
                instance = new GeneralNCR_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using GeneralNCR_LAX instance ###### ");
                instance = new GeneralNCR_LAX(driver);
            }

            return instance;
        }

        internal GeneralNCR GeneralNCR_Base => new GeneralNCR(Driver);

        public virtual void FilterDescription(string description = "")
        {
            ncrDescription = string.IsNullOrEmpty(description) 
                ? ncrDescription 
                : description;
            FilterTableColumnByValue(ColumnName.Description, ncrDescription);
        }

        public virtual void ClickBtn_Cancel() 
            => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Cancel));

        public virtual void ClickBtn_Revise()
            => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Revise));

        public virtual void ClickBtn_Approve()
            => GeneralNCR_Base.ActionConfirmation(SubmitButtons.Approve);

        public virtual void ClickBtn_DisapproveClose()
            => GeneralNCR_Base.ActionConfirmation(SubmitButtons.DisapproveClose);

        public virtual void ClickBtn_KickBack()
            => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.KickBack));

        public virtual void ClickBtn_Close()
            => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Close));

        public virtual void ClickBtn_SaveOnly()
            => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.SaveOnly));

        public virtual void ClickBtn_SaveForward()
            => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.SaveForward));

        public virtual void ClickBtn_New()
            => JsClickElement(newBtn_ByLocator);

        public virtual void ClickBtn_ExportToExcel()
            => JsClickElement(exportToExcel_ByLocator);

        public virtual void ClickBtn_Sign_RecordEngineer()
            => GeneralNCR_Base.ClickBtn_Sign(InputFields.RecordEngineer_SignBtn);

        public virtual void ClickBtn_Sign_Owner()
            => GeneralNCR_Base.ClickBtn_Sign(InputFields.Owner_SignBtn);

        public virtual void ClickBtn_Sign_IQFManager()
            => GeneralNCR_Base.ClickBtn_Sign(InputFields.IQFManager_SignBtn);

        public virtual void ClickBtn_Sign_QCManager()
            => GeneralNCR_Base.ClickBtn_Sign(InputFields.QCManager_SignBtn);

        public virtual void ClickBtn_SignaturePanel_OK()
            => JsClickElement(GeneralNCR_Base.SignaturePanelBtnXPathLocator("OK"));

        public virtual void ClickBtn_SignaturePanel_Clear()
            => JsClickElement(GeneralNCR_Base.SignaturePanelBtnXPathLocator("Clear"));

        public virtual void SignDateApproveNCR(Reviewer reviewer, bool Approve = true)
        {
            InputFields signBtn = InputFields.RecordEngineer_SignBtn;
            InputFields reviewerField = InputFields.Engineer_of_Record;
            RadioBtnsAndCheckboxes approvalField = Approve ? RadioBtnsAndCheckboxes.Engineer_Approval_Yes : RadioBtnsAndCheckboxes.Engineer_Approval_No;

            switch (reviewer)
            {
                case Reviewer.EngineerOfRecord:
                    break;

                case Reviewer.Owner:
                    signBtn = InputFields.Owner_SignBtn;
                    reviewerField = InputFields.Owner_Review;
                    approvalField = Approve ? RadioBtnsAndCheckboxes.Owner_Approval_Yes : RadioBtnsAndCheckboxes.Owner_Approval_No;
                    break;

                case Reviewer.IQF_Manager:
                    signBtn = InputFields.IQFManager_SignBtn;
                    reviewerField = InputFields.IQF_Manager;
                    break;

                case Reviewer.QC_Manager:
                    signBtn = InputFields.QCManager_SignBtn;
                    reviewerField = InputFields.QC_Manager;
                    break;
            }

            EnterText(GetTextInputFieldByLocator(reviewerField), $"RKCIUIAutomation {reviewer}");

            GeneralNCR_Base.ClickBtn_Sign(signBtn);
            ClickBtn_SignaturePanel_OK();

            if (reviewer == Reviewer.EngineerOfRecord || reviewer == Reviewer.Owner)
            {
                SelectRadioBtnOrChkbox(approvalField);
            }
        }

        public virtual void ClickTab_All_NCRs()
            => ClickTab(TableTab.All_NCRs);

        public virtual void ClickTab_Closed_NCR()
            => ClickTab(TableTab.Closed_NCR);

        public virtual void ClickTab_CQM_Review()
            => ClickTab(TableTab.CQM_Review);

        public virtual void ClickTab_Creating_Revise()
            => ClickTab(TableTab.Creating_Revise);

        public virtual void ClickTab_Developer_Concurrence()
            => ClickTab(TableTab.Developer_Concurrence);

        public virtual void ClickTab_DOT_Approval()
            => ClickTab(TableTab.DOT_Approval);

        public virtual void ClickTab_Engineer_Concurrence()
            => ClickTab(TableTab.Engineer_Concurrence);

        public virtual void ClickTab_Originator_Concurrence()
            => ClickTab(TableTab.Originator_Concurrence);

        public virtual void ClickTab_Owner_Concurrence()
            => ClickTab(TableTab.Owner_Concurrence);

        public virtual void ClickTab_QC_Review()
            => ClickTab(TableTab.QC_Review);

        public virtual void ClickTab_Resolution_Disposition()
            => ClickTab(TableTab.Resolution_Disposition);

        public virtual void ClickTab_Review_Assign_NCR()
            => ClickTab(TableTab.Review_Assign_NCR);

        public virtual void ClickTab_Revise()
            => ClickTab(TableTab.Revise);

        public virtual void ClickTab_To_Be_Closed()
            => ClickTab(TableTab.To_Be_Closed);

        public virtual void ClickTab_Verification()
            => ClickTab(TableTab.Verification);

        public virtual void ClickTab_Verification_and_Closure()
            => ClickTab(TableTab.Verification_and_Closure);

        public virtual void SortTable_Descending()
            => SortColumnDescending(ColumnName.Action);

        public virtual void SortTable_Ascending()
            => SortColumnAscending(ColumnName.Action);

        public virtual void SortTable_ToDefault()
            => SortColumnToDefault(ColumnName.Action);

        public virtual void SelectRdoBtn_TypeOfNCR_Level1()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.TypeOfNCR_Level1);

        public virtual void SelectRdoBtn_TypeOfNCR_Level2()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.TypeOfNCR_Level2);

        public virtual void SelectRdoBtn_TypeOfNCR_Level3()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.TypeOfNCR_Level3);

        public virtual void SelectRdoBtn_EngOfRecordApproval_Yes()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Engineer_Approval_Yes);

        public virtual void SelectRdoBtn_EngOfRecordApproval_No()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Engineer_Approval_No);

        public virtual void SelectRdoBtn_EngOfRecordApproval_NA()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Engineer_Approval_NA);

        public virtual void SelectRdoBtn_OwnerApproval_Yes()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Owner_Approval_Yes);

        public virtual void SelectRdoBtn_OwnerApproval_No()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Owner_Approval_No);

        public virtual void SelectRdoBtn_OwnerApproval_NA()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Owner_Approval_NA);

        public virtual void SelectChkbox_AsBuiltRequired(bool toggleChkboxIfAlreadySelected = true)
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_RcmndDisposition_CorrectRework(bool toggleChkboxIfAlreadySelected = true)
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_Correct_Rework, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_RcmndDisposition_Replace(bool toggleChkboxIfAlreadySelected = true)
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_Replace, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_RcmndDisposition_AcceptAsIs(bool toggleChkboxIfAlreadySelected = true)
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_Accept_As_Is, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_RcmndDisposition_Repair(bool toggleChkboxIfAlreadySelected = true)
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_Repair, toggleChkboxIfAlreadySelected);

        public virtual void SelectDDL_Originator(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(InputFields.Originator, selectionIndex);

        public virtual void SelectDDL_ResponsibleManager(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(InputFields.ResponsibleManager, selectionIndex);
        
        public virtual void SelectDDL_Foreman(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(InputFields.Foreman, selectionIndex);

        public virtual void SelectDDL_PreparedBy(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(InputFields.PreparedBy, selectionIndex);

        public virtual void SelectDDL_Specification(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(InputFields.Specification, selectionIndex);

        public virtual void SelectDDL_Location(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(InputFields.Location, selectionIndex);

        public virtual void SelectDDL_Area(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(InputFields.Area, selectionIndex);

        public virtual void SelectDDL_Segment(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(InputFields.Segment, selectionIndex);

        public virtual void SelectDDL_Roadway(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(InputFields.Roadway, selectionIndex);

        public virtual void SelectDDL_Feature(int selectionIndex = 2)
            => ExpandAndSelectFromDDList(InputFields.Feature, selectionIndex);

        public virtual void SelectDDL_SubFeature(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(InputFields.SubFeature, selectionIndex);

        public virtual void PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ReturnToConformance()
        {
            ExpandAndSelectFromDDList(InputFields.Concession_Request, 1);
            SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required, false);
            SelectChkbox_RcmndDisposition_CorrectRework();
            SelectChkbox_RcmndDisposition_Replace();
            EnterCorrectiveActionPlanToResolveNonconformance();
        }

        public virtual void PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ConcessionDeviation()
        {
            ExpandAndSelectFromDDList(InputFields.Concession_Request, 2);
            SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required, false);
            SelectChkbox_RcmndDisposition_AcceptAsIs();
            SelectChkbox_RcmndDisposition_Repair();
            EnterRepairPlan();
        }

        public virtual void EnterIssuedDate(string shortDate = "1/1/9999")
            => EnterText(GetTextInputFieldByLocator(InputFields.IssuedDate), GetShortDate());

        public virtual void EnterForemanNotificationDate(string dateTime = "1/1/9999 12:00 AM")
            => EnterText(GetTextInputFieldByLocator(InputFields.ForemanNotificationDate), dateTime);

        public virtual void EnterManagerNotificationDate(string dateTime = "1/1/9999 12:00 AM")
            => EnterText(GetTextInputFieldByLocator(InputFields.ManagerNotificationDate), dateTime);

        public virtual void EnterResponsibleManager(string mgrName)
            => EnterText(GetTextInputFieldByLocator(InputFields.ResponsibleManager), mgrName);

        public virtual void EnterEngineerOfRecord(string engOfRecordText = "")
            => EnterText(GetTextInputFieldByLocator(InputFields.Engineer_of_Record),
                engOfRecordText = (string.IsNullOrEmpty(engOfRecordText) ? "RKCIUIAutomation RecordEngineer" : engOfRecordText));

        public virtual void EnterRecordEngineerApprovedDate()
            => EnterText(GetTextInputFieldByLocator(InputFields.RecordEngineerApprovedDate), GetShortDate());

        public virtual void EnterOwnerReview(string ownerReviewText = "")
            => EnterText(GetTextInputFieldByLocator(InputFields.Owner_Review),
                ownerReviewText = (string.IsNullOrEmpty(ownerReviewText) ? "RKCIUIAutomation Owner" : ownerReviewText));

        public virtual void EnterOwnerApprovedDate()
            => EnterText(GetTextInputFieldByLocator(InputFields.OwnerDate), GetShortDate());

        public virtual void EnterIQFManager(string iqfMgrText = "")
            => EnterText(GetTextInputFieldByLocator(InputFields.IQF_Manager),
                iqfMgrText = (string.IsNullOrEmpty(iqfMgrText) ? "RKCIUIAutomation IQFMgr" : iqfMgrText));

        public virtual void EnterIQFManagerApprovedDate()
            => EnterText(GetTextInputFieldByLocator(InputFields.IQFManagerDate), GetShortDate());

        public virtual void EnterQCManager(string qcMgrText = "")
            => EnterText(GetTextInputFieldByLocator(InputFields.QC_Manager),
                qcMgrText = (string.IsNullOrEmpty(qcMgrText) ? "RKCIUIAutomation QCMgr" : qcMgrText));

        public virtual void EnterQCManagerApprovedDate()
            => EnterText(GetTextInputFieldByLocator(InputFields.QCManagerApprovedDate), GetShortDate());

        public virtual void EnterCQCM()
            => EnterText(GetTextInputFieldByLocator(InputFields.CQCM), "RKCIUIAutomation CQCM");

        public virtual void EnterCQCMDate()
            => EnterText(GetTextInputFieldByLocator(InputFields.CQCMDate), GetShortDate());

        public virtual void EnterCQAM()
            => EnterText(GetTextInputFieldByLocator(InputFields.CQAM), "RKCIUIAutomation CQAM");

        public virtual void EnterCQAMDate()
            => EnterText(GetTextInputFieldByLocator(InputFields.CQAMDate), GetShortDate());

        public virtual void EnterPreparedByDate()
            => EnterText(GetTextInputFieldByLocator(InputFields.PreparedBy_Date), GetShortDate());

        public virtual string EnterDescription(string description = "", bool tempDescription = false)
            => GeneralNCR_Base.EnterDesc(description, InputFields.Description_of_Nonconformance);

        public virtual string EnterDescriptionOfNCR(string description = "", bool tempDescription = false)
            => GeneralNCR_Base.EnterDesc(description, InputFields.Description_of_NCR);

        public virtual string EnterRootCauseOfTheProblem(string description = "", bool tempDescription = false)
            => GeneralNCR_Base.EnterDesc(description, InputFields.RootCause_of_the_Problem, false, false);

        public virtual void EnterCorrectiveActionPlanToResolveNonconformance(string actionPlanText = "")
        {
            By textAreaLocator = GetTextAreaFieldByLocator(InputFields.CorrectiveAction);
            ScrollToElement(textAreaLocator);
            EnterText(textAreaLocator, actionPlanText = (string.IsNullOrEmpty(actionPlanText)
                  ? "RKCIUIAutomation Corrective Action Plan To Resolve Nonconformance." : actionPlanText));
        }

        public virtual void EnterRepairPlan(string repairPlanText = "")
        {
            By textAreaLocator = GetTextAreaFieldByLocator(InputFields.RepairPlan);
            ScrollToElement(textAreaLocator);
            EnterText(textAreaLocator, repairPlanText = (string.IsNullOrEmpty(repairPlanText)
                  ? "RKCIUIAutomation Repair Plan To Repair Issue If Applicable." : repairPlanText));
        }

        //I15Tech, LAX
        public virtual IList<string> GetExpectedRequiredFieldIDs()
        {
            IList<string> RequiredFieldIDs = new List<string>
            {
                InputFields.IssuedDate.GetString(),
                InputFields.Originator.GetString(),
                InputFields.Foreman.GetString(),
                InputFields.ForemanNotificationDate.GetString(),
                InputFields.ResponsibleManager.GetString(),
                InputFields.ManagerNotificationDate.GetString(),
                $"{InputFields.Specification.GetString()}Id",
                InputFields.Area.GetString(),
                InputFields.Roadway.GetString(),
                InputFields.Description_of_Nonconformance.GetString()
            };

            return RequiredFieldIDs;
        }

        public virtual string GetNCRDocDescription(bool tempDescription = false) 
            => GetVar(tempDescription ? ncrNewDescKey : ncrDescKey);

        public virtual string GetNewNCRDocDescription() => GetVar(ncrNewDescKey);

        public virtual bool VerifyReqFieldErrorLabelsForNewDoc()
        {
            int expectedCount = 0;
            int actualCount = 0;
            bool countsMatch = false;
            bool reqFieldsMatch = false;

            try
            {
                IList<IWebElement> ReqFieldErrorLabelElements = GetElements(By.XPath("//span[contains(@class, 'ValidationErrorMessage')][contains(text(),'Required')]"));
                IList<IWebElement> ActualReqFieldErrorLabelElements = new List<IWebElement>();

                foreach (IWebElement elem in ReqFieldErrorLabelElements)
                {
                    if (elem.Displayed && elem.Enabled)
                    {
                        ActualReqFieldErrorLabelElements.Add(elem);
                    }
                }

                IList<string> ExpectedRequiredFieldIDs = GetExpectedRequiredFieldIDs();
                IList<bool> results = new List<bool>();

                actualCount = ActualReqFieldErrorLabelElements.Count;
                expectedCount = ExpectedRequiredFieldIDs.Count;
                countsMatch = expectedCount.Equals(actualCount);

                int tblRowIndex = 0;
                string[][] reqFieldTable = new string[expectedCount + 2][];
                reqFieldTable[tblRowIndex] = new string[2] {"| Expected Required Field | ", " | Found Matching Field |"};

                for (int i = 0; i < expectedCount; i++)
                {
                    IWebElement actualElem = ActualReqFieldErrorLabelElements[i];
                    tblRowIndex++;
                    string actualId = actualElem.GetAttribute("data-valmsg-for");
                    reqFieldsMatch = ExpectedRequiredFieldIDs.Contains(actualId);
                    results.Add(reqFieldsMatch);

                    string tblRowNumber = tblRowIndex.ToString();
                    tblRowNumber = (tblRowNumber.Length == 1) ? $"0{tblRowNumber}" : tblRowNumber;
                    reqFieldTable[tblRowIndex] = new string[2] { $"{tblRowNumber}: {actualId}", reqFieldsMatch.ToString() };
                }

                if (!countsMatch)
                {
                    LogInfo($"Expected and Actual Required Field Counts DO NOT MATCH:" +
                        $"<br>Expected Count: {expectedCount}<br>Actual Count: {actualCount}", countsMatch);
                }

                reqFieldsMatch = results.Contains(false) ? false : true;
                reqFieldTable[tblRowIndex + 1] = new string[2] {"Total Required Fields:", results.Count.ToString()};
                LogInfo(reqFieldTable, reqFieldsMatch);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }

            return reqFieldsMatch;
        }

        public virtual bool VerifyReqFieldErrorLabelForTypeOfNCR()
        {
            try
            {
                bool errorLabelIsDisplayed = false;
                IWebElement NcrTypeInputElem = GetElement(By.Id(InputFields.Type_of_NCR.GetString()));
                errorLabelIsDisplayed = NcrTypeInputElem.FindElement(By.XPath("//preceding-sibling::span[@data-valmsg-for='NcrType']")).Displayed ? true : false;

                return errorLabelIsDisplayed;
            }
            catch (Exception e)
            {
                LogError(e.Message);
                return false;
            }
        }

        public virtual bool VerifySignatureField(Reviewer reviewer, bool shouldFieldBeEmpty = false)
        {
            InputFields reviewerId = InputFields.Engineer_of_Record;
            string signatureValueAttrib = string.Empty;

            try
            {
                switch (reviewer)
                {
                    case Reviewer.EngineerOfRecord:
                        break;

                    case Reviewer.Owner:
                        reviewerId = InputFields.Owner_Review;
                        break;

                    case Reviewer.IQF_Manager:
                        reviewerId = InputFields.IQF_Manager;
                        break;

                    case Reviewer.QC_Manager:
                        reviewerId = InputFields.QC_Manager;
                        break;
                }

                By locator = By.Id(reviewerId.GetString());
                ScrollToElement(locator);
                IWebElement signatureFieldElement = GetElement(locator);
                signatureValueAttrib = signatureFieldElement.GetAttribute("value");

                bool isFieldEmpty = string.IsNullOrEmpty(signatureValueAttrib) ? true : false;
                bool isResultExpected = shouldFieldBeEmpty.Equals(isFieldEmpty) ? true : false;

                string logMsg = isResultExpected ? "Result As Expected" : "Unexpected Result";

                LogInfo($"Signature Field: {logMsg}", isResultExpected);
                return isResultExpected;
            }
            catch (Exception e)
            {
                LogError(e.StackTrace);
                return false;
            }
        }
    
        public virtual bool VerifyReqFieldErrorLabelForConcessionRequest()
        {
            try
            {
                bool errorLabelIsDisplayed = false;
                IWebElement ConcessionRequestDDListElem = GetElement(By.Id(InputFields.Concession_Request.GetString()));
                errorLabelIsDisplayed = ConcessionRequestDDListElem.FindElement(By.XPath("//preceding-sibling::span[@data-valmsg-for='ConcessionRequest']")).Displayed ? true : false;

                return errorLabelIsDisplayed;
            }
            catch (Exception e)
            {
                LogError(e.StackTrace);
                return false;
            }
        }

        //I15Tech, LAX
        public virtual void PopulateRequiredFields()
        {
            EnterIssuedDate();
            SelectDDL_Originator();
            SelectDDL_Foreman();
            EnterForemanNotificationDate();
            EnterResponsibleManager("Bhoomi Purohit");
            EnterManagerNotificationDate();
            SelectDDL_Specification();
            SelectDDL_Area();
            SelectDDL_Roadway();
            EnterDescription();
        }

        public virtual void PopulateRequiredFieldsAndSaveForward()
            => PopulateRequiredFieldsAndSave(true);

        public virtual void PopulateRequiredFieldsAndSaveOnly()
            => PopulateRequiredFieldsAndSave(false);

        public virtual bool VerifyNCRDocIsDisplayed(TableTab tableTab, string description = "")
        {
            bool isDisplayed = false;

            try
            {
                ClickTab(tableTab);
                string _ncrDesc = description.Equals("") ? GetNCRDocDescription() : description;
                isDisplayed = VerifyRecordIsDisplayed(ColumnName.Description, _ncrDesc);
                string logMsg = isDisplayed ? "Found" : "Unable to find";
                LogInfo($"{logMsg} record under {tableTab.GetString()} tab with description: {_ncrDesc}.", isDisplayed);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return isDisplayed;
        }

        public virtual bool VerifyNCRDocIsDisplayedInReview(string ncrDescription = "")
            => VerifyNCRDocIsDisplayed(TableTab.CQM_Review, ncrDescription);

        public virtual bool VerifyNCRDocIsClosed(string description = "")
            => GeneralNCR_Base.CheckNCRisClosed(description, TableTab.All_NCRs);

        private void PopulateRequiredFieldsAndSave(bool SaveForward)
        {
            PopulateRequiredFields();
            UploadFile("test.xlsx");

            if (SaveForward)
            {
                ClickBtn_SaveForward();
            }
            else
            {
                ClickBtn_SaveOnly();
            }
        }

        public void SendBackToRevise(UserType user, string ncrDescription)
        {
            FilterDescription(ncrDescription);
            ClickEditBtnForRow();
            ClickBtn_Revise();

            Assert.True(VerifyNCRDocIsDisplayed(TableTab.Creating_Revise, ncrDescription));
        }
    }

    #endregion Common Workflow Implementation class

    /// <summary>
    /// Tenant specific implementation of DesignDocument Comment Review
    /// </summary>

    #region Implementation specific to Garnet

    public class GeneralNCR_Garnet : GeneralNCR
    {
        public GeneralNCR_Garnet(IWebDriver driver) : base(driver)
        {
        }

        public override bool VerifyNCRDocIsClosed(string description = "")
            => GeneralNCR_Base.CheckNCRisClosed(description, TableTab.Closed_NCR);
    }

    #endregion Implementation specific to Garnet

    #region Implementation specific to GLX

    public class GeneralNCR_GLX : GeneralNCR
    {
        public GeneralNCR_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override IList<string> GetExpectedRequiredFieldIDs()
        {
            IList<string> RequiredFieldIDs = new List<string>
            {
                InputFields.IssuedDate.GetString(),
                InputFields.Area.GetString(),
                InputFields.Originator.GetString(),
                InputFields.Description_of_Nonconformance.GetString(),
                InputFields.RootCause_of_the_Problem.GetString(),
                InputFields.PreparedBy.GetString(),
                InputFields.PreparedBy_Date.GetString(),
            };

            return RequiredFieldIDs;
        }

        public override void PopulateRequiredFields()
        {
            EnterIssuedDate();
            SelectDDL_Area();
            SelectDDL_Originator();
            EnterDescription();
            EnterRootCauseOfTheProblem();
            SelectDDL_PreparedBy();
            EnterPreparedByDate();
        }

        public override bool VerifyNCRDocIsDisplayedInReview(string ncrDescription = "")
            => VerifyNCRDocIsDisplayed(TableTab.QM_Review, ncrDescription);

        public override void ClickTab_Creating_Revise()
            => ClickTab(TableTab.Create_Revise);
    }

    #endregion Implementation specific to GLX

    #region Implementation specific to I15South

    public class GeneralNCR_I15South : GeneralNCR
    {
        public GeneralNCR_I15South(IWebDriver driver) : base(driver)
        {
        }

        public override IList<string> GetExpectedRequiredFieldIDs()
        {
            List<string> RequiredFieldIDs = new List<string>
            {
                InputFields.IssuedDate.GetString(),
                InputFields.Originator.GetString(),
                InputFields.Foreman.GetString(),
                InputFields.ForemanNotificationDate.GetString(),
                InputFields.ResponsibleManager.GetString(),
                InputFields.ManagerNotificationDate.GetString(),
                $"{InputFields.Specification.GetString()}Id",
                InputFields.Segment.GetString(),
                InputFields.Roadway.GetString(),
                InputFields.Description_of_Nonconformance.GetString(),
            };

            return RequiredFieldIDs;
        }

        public override void PopulateRequiredFields()
        {
            EnterIssuedDate();
            SelectDDL_Originator();
            SelectDDL_Foreman();
            EnterForemanNotificationDate();
            EnterResponsibleManager("Bhoomi Purohit");
            EnterManagerNotificationDate();
            SelectDDL_Specification();
            SelectDDL_Segment();
            SelectDDL_Roadway();
            EnterDescription();
        }
    }

    #endregion Implementation specific to I15South

    #region Implementation specific to I15Tech

    public class GeneralNCR_I15Tech : GeneralNCR
    {
        public GeneralNCR_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15Tech

    #region Implementation specific to LAX

    public class GeneralNCR_LAX : GeneralNCR
    {
        public GeneralNCR_LAX(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to LAX

    #region Implementation specific to SH249 - SimpleWF

    public class GeneralNCR_SH249 : GeneralNCR
    {
        public GeneralNCR_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override IList<string> GetExpectedRequiredFieldIDs()
        {
            IList<string> RequiredFieldIDs = new List<string>
            {
                InputFields.IssuedDate.GetString(),
                InputFields.Originator.GetString(),
                InputFields.ResponsibleManager.GetString(),
                InputFields.CQCM.GetString(),
                InputFields.CQCMDate.GetString(),
                InputFields.CQAM.GetString(),
                InputFields.CQAMDate.GetString(),
            };

            return RequiredFieldIDs;
        }

        public override void PopulateRequiredFields()
        {
            EnterIssuedDate();
            SelectDDL_Originator();
            SelectDDL_ResponsibleManager();
            EnterDescriptionOfNCR();
            EnterCQCM();
            EnterCQCMDate();
            EnterCQAM();
            EnterCQAMDate();
        }

        public override string EnterDescription(string description = "", bool tempDescription = false)
            => EnterDesc(description, InputFields.Description_of_NCR);

        public override void ClickTab_All_NCRs()
            => ClickTab(TableTab.ALL_NCRs);

        public override bool VerifyNCRDocIsClosed(string description = "")
            => GeneralNCR_Base.CheckNCRisClosed(description, TableTab.ALL_NCRs);
    }

    #endregion Implementation specific to SH249 - SimpleWF

    #region Implementation specific to SGWay - SimpleWF

    public class GeneralNCR_SGWay : GeneralNCR
    {
        public GeneralNCR_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override IList<string> GetExpectedRequiredFieldIDs()
        {
            IList<string> RequiredFieldIDs = new List<string>
            {
                InputFields.IssuedDate.GetString(),
                InputFields.Originator.GetString(),
                InputFields.CQCM.GetString(),
                InputFields.CQCMDate.GetString(),
                InputFields.CQAM.GetString(),
                InputFields.CQAMDate.GetString(),
            };

            return RequiredFieldIDs;
        }

        public override void PopulateRequiredFields()
        {
            EnterIssuedDate();
            SelectDDL_Originator();
            EnterDescriptionOfNCR();
            EnterCQCM();
            EnterCQCMDate();
            EnterCQAM();
            EnterCQAMDate();
        }

        public override string EnterDescription(string description = "", bool tempDescription = false)
            => EnterDesc(description, InputFields.Description_of_NCR);

        public override void ClickTab_All_NCRs()
            => ClickTab(TableTab.ALL_NCRs);

        public override bool VerifyNCRDocIsClosed(string description = "")
            => GeneralNCR_Base.CheckNCRisClosed(description, TableTab.ALL_NCRs);
    }

    #endregion Implementation specific to SGWay - SimpleWF
}