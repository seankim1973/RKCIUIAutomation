using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Threading;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.GeneralNCR;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page.PageObjects.QARecordControl
{
    #region NCR Generic class

    public class GeneralNCR : GeneralNCR_Impl
    {
        public GeneralNCR()
        {
        }

        public GeneralNCR(IWebDriver driver) => this.Driver = driver;

        public override T SetClass<T>(IWebDriver driver)
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

            return (T)instance;
        }

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
            [StringValue("CQCM")] CQC_Manager,
            [StringValue("CQCMDate")] CQCManagerApprovedDate,
            [StringValue("CQCMSignature")] CQCManager_SignBtn,
            [StringValue("ContainmentActionSignature")] ContainmentActionManager_SignBtn,
            [StringValue("OMQM")] OMQ_Manager,
            [StringValue("OMQMDate")] OMQManagerApprovedDate,
            [StringValue("OMQMSignature")] OMQManager_SignBtn
        }

        public enum TableTab
        {
            [StringValue("All NCRs")] All_NCRs,
            [StringValue("ALL NCRs")] ALL_NCRs,
            [StringValue("Closed NCR")] Closed_NCR,
            [StringValue("CQM Review")] CQM_Review,
            [StringValue("QM Review")] QM_Review,
            [StringValue("Review")] Review,
            [StringValue("Create/Revise")] Create_Revise,
            [StringValue("Creating/Revise")] Creating_Revise,
            [StringValue("Developer Concurrence")] Developer_Concurrence,
            [StringValue("DOT Approval")] DOT_Approval,
            [StringValue("LAWA Concurrence")] LAWA_Concurrence,
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
            [StringValue("CQCMApproval_")] CQCMApproval_NA,
            [StringValue("CQCMApproval_True")] CQCMApproval_Yes,
            [StringValue("CQCMApproval_False")] CQCMApproval_No,
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
            EngineerOfRecord,
            Operations_Manager,
            CQC_Manager
        }

        [ThreadStatic]
        internal static string ncrDescription;

        internal static readonly By newBtn_ByLocator = By.XPath("//div[@id='NcrGrid_Revise']/div/a[contains(@class, 'k-button')]");

        internal static readonly By exportToExcel_ByLocator = By.XPath("//div[@class='k-content k-state-active']//button[text()='Export to Excel']");

        public override string ExpectedPageHeader { get; set; } = "List of NCR Reports";

        internal void ActionConfirmation(SubmitButtons submitButton, bool tenantHasAlert = true, bool acceptAlert = true)
        {
            try
            {
                PageAction.JsClickElement(PgHelper.GetSubmitButtonByLocator(submitButton));

                if (tenantHasAlert)
                {
                    PageAction.ConfirmActionDialog(acceptAlert);
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        internal void ClickBtn_Sign(InputFields signBtnType)
            => PageAction.JsClickElement(By.XPath($"//a[@signaturehidden='{signBtnType.GetString()}']"));

        internal By SignaturePanelBtnXPathLocator(string btnName)
            => By.XPath($"//div[@id='ncrSignaturePopup']//a[text()='{btnName}']");

        internal string EnterDesc(string desc, InputFields descField, bool tempDescription = false, bool replaceCurrentDesc = true)
        {
            By descLocator = PgHelper.GetTextAreaFieldByLocator(descField);

            if (replaceCurrentDesc)
            {
                CreateNcrDescription(tempDescription);
            }
            desc = desc.HasValue()
                ? desc
                : BaseUtil.GetVar(tempDescription
                    ? "NewNcrDescription"
                    : "NcrDescription");
            PageAction.EnterText(descLocator, desc);
            return desc;
        }

        internal void CreateNcrDescription(bool tempDescription = false)
        {
            string logMsg = string.Empty;
            string descValue = string.Empty;
            string descKey = tempDescription
                ? "NewNcrDescription"
                : "NcrDescription";

            if (tempDescription)
            {
                descValue = BaseUtil.GetVar(descKey);
                logMsg = "new temp ";
            }
            else
            {
                ncrDescription = BaseUtil.GetVar(descKey);
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
                string _ncrDesc = description.HasValue()
                    ? description
                    : GetNCRDocDescription();
                bool isDisplayed = VerifyNCRDocIsDisplayed(closedTab, _ncrDesc);

                if (isDisplayed)
                {
                    string docStatus = GridHelper.GetColumnValueForRow(_ncrDesc, "Workflow location");
                    ncrIsClosed = docStatus.Equals("Closed")
                        ? true
                        : false;
                    logMsg = $"Workflow Location displayed as: {docStatus}";
                }

                Report.Info($"NCR with description ({_ncrDesc}), {logMsg}", ncrIsClosed);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return ncrIsClosed;
        }

        internal void PopulateRequiredFieldsAndSave(bool SaveForward)
        {
            PopulateRequiredFields();
            PageAction.UploadFile("test.xlsx");

            if (SaveForward)
            {
                ClickBtn_SaveForward();
            }
            else
            {
                ClickBtn_SaveOnly();
            }

            PageAction.WaitForPageReady();
        }

        public override void FilterDescription(string description = "")
            => GridHelper.FilterTableColumnByValue(ColumnName.Description, description.HasValue()
                ? description
                : ncrDescription);

        public override void ClickBtn_Cancel()
            => PageAction.JsClickElement(PgHelper.GetSubmitButtonByLocator(SubmitButtons.Cancel));

        public override void ClickBtn_Revise()
            => PageAction.JsClickElement(PgHelper.GetSubmitButtonByLocator(SubmitButtons.Revise));

        public override void ClickBtn_Approve()
            => ActionConfirmation(SubmitButtons.Approve);

        public override void ClickBtn_DisapproveClose()
            => ActionConfirmation(SubmitButtons.DisapproveClose);

        public override void ClickBtn_KickBack()
            => PageAction.JsClickElement(PgHelper.GetSubmitButtonByLocator(SubmitButtons.KickBack));

        public override void ClickBtn_Close()
            => PageAction.JsClickElement(PgHelper.GetSubmitButtonByLocator(SubmitButtons.Close));

        public override void ClickBtn_SaveOnly()
            => PageAction.JsClickElement(PgHelper.GetSubmitButtonByLocator(SubmitButtons.SaveOnly));

        public override void ClickBtn_SaveForward()
            => PageAction.JsClickElement(PgHelper.GetSubmitButtonByLocator(SubmitButtons.SaveForward));

        public override void ClickBtn_New()
            => PageAction.JsClickElement(newBtn_ByLocator);

        public override void ClickBtn_ExportToExcel()
            => PageAction.JsClickElement(exportToExcel_ByLocator);

        public override void ClickBtn_Sign_RecordEngineer()
            => ClickBtn_Sign(InputFields.RecordEngineer_SignBtn);

        public override void ClickBtn_Sign_Owner()
            => ClickBtn_Sign(InputFields.Owner_SignBtn);

        public override void ClickBtn_Sign_IQFManager()
            => ClickBtn_Sign(InputFields.IQFManager_SignBtn);

        public override void ClickBtn_Sign_QCManager()
            => ClickBtn_Sign(InputFields.QCManager_SignBtn);

        public override void ClickBtn_SignaturePanel_OK()
            => PageAction.JsClickElement(SignaturePanelBtnXPathLocator("OK"));

        public override void ClickBtn_SignaturePanel_Clear()
            => PageAction.JsClickElement(SignaturePanelBtnXPathLocator("Clear"));

        public override void SignDateApproveNCR(Reviewer reviewer, bool Approve = true)
        {
            InputFields signBtn = InputFields.RecordEngineer_SignBtn;
            InputFields reviewerField = InputFields.Engineer_of_Record;
            RadioBtnsAndCheckboxes approvalField = Approve
                ? RadioBtnsAndCheckboxes.Engineer_Approval_Yes
                : RadioBtnsAndCheckboxes.Engineer_Approval_No;
            bool isApprovalRequired = false;

            switch (reviewer)
            {
                case Reviewer.EngineerOfRecord:
                    isApprovalRequired = true;
                    break;
                case Reviewer.Owner:
                    signBtn = InputFields.Owner_SignBtn;
                    reviewerField = InputFields.Owner_Review;
                    approvalField = Approve
                        ? RadioBtnsAndCheckboxes.Owner_Approval_Yes
                        : RadioBtnsAndCheckboxes.Owner_Approval_No;
                    isApprovalRequired = true;
                    break;
                case Reviewer.IQF_Manager:
                    signBtn = InputFields.IQFManager_SignBtn;
                    reviewerField = InputFields.IQF_Manager;
                    break;
                case Reviewer.QC_Manager:
                    signBtn = InputFields.QCManager_SignBtn;
                    reviewerField = InputFields.QC_Manager;
                    break;
                case Reviewer.CQC_Manager:
                    signBtn = InputFields.CQCManager_SignBtn;
                    reviewerField = InputFields.CQC_Manager;
                    approvalField = Approve
                        ? RadioBtnsAndCheckboxes.CQCMApproval_Yes
                        : RadioBtnsAndCheckboxes.CQCMApproval_No;
                    isApprovalRequired = true;
                    break;
                case Reviewer.Operations_Manager:
                    signBtn = InputFields.OMQManager_SignBtn;
                    reviewerField = InputFields.OMQ_Manager;
                    break;
            }

            Thread.Sleep(5000);
            PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(reviewerField), $"RKCIUIAutomation {reviewer.ToString()}");

            ClickBtn_Sign(signBtn);
            PageAction.EnterSignature();
            ClickBtn_SignaturePanel_OK();

            if (isApprovalRequired)
                PageAction.SelectRadioBtnOrChkbox(approvalField);
        }

        public override void ClickTab_All_NCRs()
            => GridHelper.ClickTab(TableTab.All_NCRs);

        public override void ClickTab_Closed_NCR()
            => GridHelper.ClickTab(TableTab.Closed_NCR);

        public override void ClickTab_CQM_Review()
            => GridHelper.ClickTab(TableTab.CQM_Review);

        public override void ClickTab_Review()
            => GridHelper.ClickTab(TableTab.Review);

        public override void ClickTab_Creating_Revise()
            => GridHelper.ClickTab(TableTab.Creating_Revise);

        public override void ClickTab_Developer_Concurrence()
            => GridHelper.ClickTab(TableTab.Developer_Concurrence);

        public override void ClickTab_DOT_Approval()
            => GridHelper.ClickTab(TableTab.DOT_Approval);

        public override void ClickTab_LAWA_Concurrence()
            => GridHelper.ClickTab(TableTab.LAWA_Concurrence);

        public override void ClickTab_Engineer_Concurrence()
            => GridHelper.ClickTab(TableTab.Engineer_Concurrence);

        public override void ClickTab_Originator_Concurrence()
            => GridHelper.ClickTab(TableTab.Originator_Concurrence);

        public override void ClickTab_Owner_Concurrence()
            => GridHelper.ClickTab(TableTab.Owner_Concurrence);

        public override void ClickTab_QC_Review()
            => GridHelper.ClickTab(TableTab.QC_Review);

        public override void ClickTab_Resolution_Disposition()
            => GridHelper.ClickTab(TableTab.Resolution_Disposition);

        public override void ClickTab_Review_Assign_NCR()
            => GridHelper.ClickTab(TableTab.Review_Assign_NCR);

        public override void ClickTab_Revise()
            => GridHelper.ClickTab(TableTab.Revise);

        public override void ClickTab_To_Be_Closed()
            => GridHelper.ClickTab(TableTab.To_Be_Closed);

        public override void ClickTab_Verification()
            => GridHelper.ClickTab(TableTab.Verification);

        public override void ClickTab_Verification_and_Closure()
            => GridHelper.ClickTab(TableTab.Verification_and_Closure);

        public override void SortTable_Descending()
            => GridHelper.SortColumnDescending(ColumnName.Action);

        public override void SortTable_Ascending()
            => GridHelper.SortColumnAscending(ColumnName.Action);

        public override void SortTable_ToDefault()
            => GridHelper.SortColumnToDefault(ColumnName.Action);

        public override void SelectRdoBtn_TypeOfNCR_Level1()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.TypeOfNCR_Level1);

        public override void SelectRdoBtn_TypeOfNCR_Level2()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.TypeOfNCR_Level2);

        public override void SelectRdoBtn_TypeOfNCR_Level3()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.TypeOfNCR_Level3);

        public override void SelectRdoBtn_EngOfRecordApproval_Yes()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Engineer_Approval_Yes);

        public override void SelectRdoBtn_EngOfRecordApproval_No()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Engineer_Approval_No);

        public override void SelectRdoBtn_EngOfRecordApproval_NA()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Engineer_Approval_NA);

        public override void SelectRdoBtn_OwnerApproval_Yes()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Owner_Approval_Yes);

        public override void SelectRdoBtn_OwnerApproval_No()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Owner_Approval_No);

        public override void SelectRdoBtn_OwnerApproval_NA()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Owner_Approval_NA);

        public override void SelectChkbox_AsBuiltRequired(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_RcmndDisposition_CorrectRework(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_Correct_Rework, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_RcmndDisposition_Replace(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_Replace, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_RcmndDisposition_AcceptAsIs(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_Accept_As_Is, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_RcmndDisposition_Repair(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_Repair, toggleChkboxIfAlreadySelected);

        public override void SelectDDL_Originator(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Originator, selectionIndex);

        public override void SelectDDL_ResponsibleManager(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.ResponsibleManager, selectionIndex);

        public override void SelectDDL_Foreman(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Foreman, selectionIndex);

        public override void SelectDDL_PreparedBy(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.PreparedBy, selectionIndex);

        public override void SelectDDL_Specification(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Specification, selectionIndex);

        public override void SelectDDL_Location(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Location, selectionIndex);

        public override void SelectDDL_Area(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Area, selectionIndex);

        public override void SelectDDL_Segment(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Segment, selectionIndex);

        public override void SelectDDL_TrackNo(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Roadway, selectionIndex);

        public override void SelectDDL_Roadway(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Roadway, selectionIndex);

        public override void SelectDDL_Feature(int selectionIndex = 2)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Feature, selectionIndex);

        public override void SelectDDL_SubFeature(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.SubFeature, selectionIndex);

        public override void PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ReturnToConformance()
        {
            PageAction.ExpandAndSelectFromDDList(InputFields.Concession_Request, 1);
            PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required, false);
            SelectChkbox_RcmndDisposition_CorrectRework();
            SelectChkbox_RcmndDisposition_Replace();
            EnterCorrectiveActionPlanToResolveNonconformance();
        }

        public override void PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ConcessionDeviation()
        {
            PageAction.ExpandAndSelectFromDDList(InputFields.Concession_Request, 2);
            PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.ChkBox_As_Built_Required, false);
            SelectChkbox_RcmndDisposition_AcceptAsIs();
            SelectChkbox_RcmndDisposition_Repair();
            EnterRepairPlan();
        }

        public override void EnterIssuedDate(string shortDate = "1/1/9999")
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.IssuedDate), GetShortDate());

        public override void EnterForemanNotificationDate(string dateTime = "1/1/9999 12:00 AM")
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.ForemanNotificationDate), dateTime);

        public override void EnterManagerNotificationDate(string dateTime = "1/1/9999 12:00 AM")
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.ManagerNotificationDate), dateTime);

        public override void EnterResponsibleManager(string mgrName)
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.ResponsibleManager), mgrName);

        public override void EnterEngineerOfRecord(string engOfRecordText = "")
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.Engineer_of_Record),
                engOfRecordText = (string.IsNullOrEmpty(engOfRecordText) ? "RKCIUIAutomation RecordEngineer" : engOfRecordText));

        public override void EnterRecordEngineerApprovedDate()
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.RecordEngineerApprovedDate), GetShortDate());

        public override void EnterOwnerReview(string ownerReviewText = "")
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.Owner_Review),
                ownerReviewText = (string.IsNullOrEmpty(ownerReviewText) ? "RKCIUIAutomation Owner" : ownerReviewText));

        public override void EnterOwnerApprovedDate()
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.OwnerDate), GetShortDate());

        public override void EnterIQFManager(string iqfMgrText = "")
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.IQF_Manager),
                iqfMgrText = (string.IsNullOrEmpty(iqfMgrText) ? "RKCIUIAutomation IQFMgr" : iqfMgrText));

        public override void EnterIQFManagerApprovedDate()
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.IQFManagerDate), GetShortDate());

        public override void EnterQCManager(string qcMgrText = "")
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.QC_Manager),
                qcMgrText = (string.IsNullOrEmpty(qcMgrText) ? "RKCIUIAutomation QCMgr" : qcMgrText));

        public override void EnterQCManagerApprovedDate()
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.QCManagerApprovedDate), GetShortDate());

        public override void EnterCQCM()
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.CQCM), "RKCIUIAutomation CQCM");

        public override void EnterCQCMDate()
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.CQCMDate), GetShortDate());

        public override void EnterCQAM()
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.CQAM), "RKCIUIAutomation CQAM");

        public override void EnterCQAMDate()
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.CQAMDate), GetShortDate());

        public override void EnterPreparedByDate()
            => PageAction.EnterText(PgHelper.GetTextInputFieldByLocator(InputFields.PreparedBy_Date), GetShortDate());

        public override string EnterDescription(string description = "", bool tempDescription = false)
            => EnterDesc(description, InputFields.Description_of_Nonconformance);

        public override string EnterDescriptionOfNCR(string description = "", bool tempDescription = false)
            => EnterDesc(description, InputFields.Description_of_NCR);

        public override string EnterRootCauseOfTheProblem(string description = "", bool tempDescription = false)
            => EnterDesc(description, InputFields.RootCause_of_the_Problem, false, false);

        public override void EnterCorrectiveActionPlanToResolveNonconformance(string actionPlanText = "")
            => PageAction.EnterText(PgHelper.GetTextAreaFieldByLocator(InputFields.CorrectiveAction), actionPlanText.HasValue()
                  ? actionPlanText
                  : "RKCIUIAutomation Corrective Action Plan To Resolve Nonconformance.");

        public override void EnterRepairPlan(string repairPlanText = "")
            => PageAction.EnterText(PgHelper.GetTextAreaFieldByLocator(InputFields.RepairPlan), repairPlanText.HasValue()
                  ? repairPlanText
                  : "RKCIUIAutomation Repair Plan To Repair Issue If Applicable.");

        //I15Tech, LAX
        public override IList<string> GetExpectedRequiredFieldIDs()
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

        public override string GetNCRDocDescription(bool tempDescription = false)
            => BaseUtil.GetVar(tempDescription
                ? "NewNcrDescription"
                : "NcrDescription");

        //public override string GetNewNCRDocDescription() => GetVar(ncrNewDescKey);

        public override bool VerifyReqFieldErrorLabelsForNewDoc()
        {
            int expectedCount = 0;
            int actualCount = 0;
            bool countsMatch = false;
            bool reqFieldsMatch = false;

            try
            {
                IList<IWebElement> ReqFieldErrorLabelElements = PageAction.GetElements(By.XPath("//span[contains(@class, 'ValidationErrorMessage')][contains(text(),'Required')]"));
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
                reqFieldTable[tblRowIndex] = new string[2] { "| Expected Required Field | ", " | Found Matching Field |" };

                for (int i = 0; i < expectedCount; i++)
                {
                    IWebElement actualElem = ActualReqFieldErrorLabelElements[i];
                    tblRowIndex++;
                    string actualId = actualElem.GetAttribute("data-valmsg-for");
                    reqFieldsMatch = ExpectedRequiredFieldIDs.Contains(actualId);
                    results.Add(reqFieldsMatch);

                    string tblRowNumber = tblRowIndex.ToString();
                    tblRowNumber = tblRowNumber.Length == 1
                        ? $"0{tblRowNumber}"
                        : tblRowNumber;
                    reqFieldTable[tblRowIndex] = new string[2] { $"{tblRowNumber}: {actualId}", reqFieldsMatch.ToString() };
                }

                if (!countsMatch)
                {
                    Report.Info($"Expected and Actual Required Field Counts DO NOT MATCH:" +
                        $"<br>Expected Count: {expectedCount}<br>Actual Count: {actualCount}", countsMatch);
                }

                reqFieldsMatch = results.Contains(false)
                    ? false
                    : true;
                reqFieldTable[tblRowIndex + 1] = new string[2] { "Total Required Fields:", results.Count.ToString() };
                Report.Info(reqFieldTable, reqFieldsMatch);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return reqFieldsMatch;
        }

        public override bool VerifyReqFieldErrorLabelForTypeOfNCR()
        {
            bool errorLabelIsDisplayed = false;

            try
            {

                IWebElement NcrTypeInputElem = PageAction.GetElement(By.Id(InputFields.Type_of_NCR.GetString()));
                errorLabelIsDisplayed = NcrTypeInputElem.FindElement(By.XPath("//preceding-sibling::span[@data-valmsg-for='NcrType']")).Displayed
                    ? true
                    : false;
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return errorLabelIsDisplayed;
        }

        public override bool VerifySignatureField(Reviewer reviewer, bool shouldFieldBeEmpty = false)
        {
            InputFields reviewerId = InputFields.Engineer_of_Record;
            string signatureValueAttrib = string.Empty;
            bool isResultExpected = false;

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

                    case Reviewer.Operations_Manager:
                        reviewerId = InputFields.OMQ_Manager;
                        break;
                }

                By locator = By.Id(reviewerId.GetString());
                PageAction.ScrollToElement(locator);

                bool isFieldEmpty = PageAction.GetAttribute(locator, "value").HasValue()
                    ? false
                    : true;
                isResultExpected = shouldFieldBeEmpty.Equals(isFieldEmpty)
                    ? true
                    : false;

                string logMsg = isResultExpected
                    ? "Result As Expected"
                    : "Unexpected Result";

                Report.Info($"Signature Field: {logMsg}", isResultExpected);

            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return isResultExpected;
        }

        public override bool VerifyReqFieldErrorLabelForConcessionRequest()
        {
            bool errorLabelIsDisplayed = false;
            try
            {
                IWebElement ConcessionRequestDDListElem = PageAction.GetElement(By.Id(InputFields.Concession_Request.GetString()));
                errorLabelIsDisplayed = ConcessionRequestDDListElem.FindElement(By.XPath("//preceding-sibling::span[@data-valmsg-for='ConcessionRequest']")).Displayed
                    ? true
                    : false;
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return errorLabelIsDisplayed;
        }

        //I15Tech, LAX
        public override void PopulateRequiredFields()
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

        public override void PopulateRequiredFieldsAndSaveForward()
            => PopulateRequiredFieldsAndSave(true);

        public override void PopulateRequiredFieldsAndSaveOnly()
            => PopulateRequiredFieldsAndSave(false);

        public override bool VerifyNCRDocIsDisplayed(TableTab tableTab, string description = "")
        {
            bool isDisplayed = false;

            try
            {
                PageAction.WaitForPageReady();

                GridHelper.ClickTab(tableTab);

                string _ncrDesc = description.HasValue()
                    ? description
                    : GetNCRDocDescription();

                isDisplayed = GridHelper.VerifyRecordIsDisplayed(ColumnName.Description, _ncrDesc);

                string logMsg = isDisplayed
                    ? "Found"
                    : "Unable to find";

                Report.Info($"{logMsg} record under '{tableTab.GetString()}' tab with description: {_ncrDesc}.", isDisplayed);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return isDisplayed;
        }

        public override bool VerifyNCRDocIsClosed(string description = "")
            => CheckNCRisClosed(description, TableTab.All_NCRs);

    }

    #endregion NCR Generic class


    #region workflow interface class

    public interface IGeneralNCR
    {
        string ExpectedPageHeader { get; set; }

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

        void ClickTab_Review();

        void ClickTab_Creating_Revise();

        void ClickTab_Developer_Concurrence();

        void ClickTab_DOT_Approval();

        void ClickTab_LAWA_Concurrence();

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

        void SelectDDL_TrackNo(int selectionIndex = 1);

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

        bool VerifyNCRDocIsClosed(string ncrDescription = "");

        bool VerifyReqFieldErrorLabelForTypeOfNCR();

        bool VerifySignatureField(Reviewer reviewer, bool shouldBeEmpty = false);

        bool VerifyReqFieldErrorLabelForConcessionRequest();

        string GetNCRDocDescription(bool tempDescription = false);

        IList<string> GetExpectedRequiredFieldIDs();
    }

    #endregion workflow interface class


    #region Common Workflow Implementation class

    public abstract class GeneralNCR_Impl : PageBase, IGeneralNCR
    {
        public abstract string ExpectedPageHeader { get; set; }
        public abstract void ClickBtn_Approve();
        public abstract void ClickBtn_Cancel();
        public abstract void ClickBtn_Close();
        public abstract void ClickBtn_DisapproveClose();
        public abstract void ClickBtn_ExportToExcel();
        public abstract void ClickBtn_KickBack();
        public abstract void ClickBtn_New();
        public abstract void ClickBtn_Revise();
        public abstract void ClickBtn_SaveForward();
        public abstract void ClickBtn_SaveOnly();
        public abstract void ClickBtn_SignaturePanel_Clear();
        public abstract void ClickBtn_SignaturePanel_OK();
        public abstract void ClickBtn_Sign_IQFManager();
        public abstract void ClickBtn_Sign_Owner();
        public abstract void ClickBtn_Sign_QCManager();
        public abstract void ClickBtn_Sign_RecordEngineer();
        public abstract void ClickTab_All_NCRs();
        public abstract void ClickTab_Closed_NCR();
        public abstract void ClickTab_CQM_Review();
        public abstract void ClickTab_Creating_Revise();
        public abstract void ClickTab_Developer_Concurrence();
        public abstract void ClickTab_DOT_Approval();
        public abstract void ClickTab_Engineer_Concurrence();
        public abstract void ClickTab_LAWA_Concurrence();
        public abstract void ClickTab_Originator_Concurrence();
        public abstract void ClickTab_Owner_Concurrence();
        public abstract void ClickTab_QC_Review();
        public abstract void ClickTab_Resolution_Disposition();
        public abstract void ClickTab_Review();
        public abstract void ClickTab_Review_Assign_NCR();
        public abstract void ClickTab_Revise();
        public abstract void ClickTab_To_Be_Closed();
        public abstract void ClickTab_Verification();
        public abstract void ClickTab_Verification_and_Closure();
        public abstract void EnterCorrectiveActionPlanToResolveNonconformance(string actionPlanText = "");
        public abstract void EnterCQAM();
        public abstract void EnterCQAMDate();
        public abstract void EnterCQCM();
        public abstract void EnterCQCMDate();
        public abstract string EnterDescription(string description = "", bool tempDescription = false);
        public abstract string EnterDescriptionOfNCR(string description = "", bool tempDescription = false);
        public abstract void EnterEngineerOfRecord(string engOfRecordText = "");
        public abstract void EnterForemanNotificationDate(string dateTime = "1/1/9999 12:00 AM");
        public abstract void EnterIQFManager(string iqfMgrText = "");
        public abstract void EnterIQFManagerApprovedDate();
        public abstract void EnterIssuedDate(string shortDate = "1/1/9999");
        public abstract void EnterManagerNotificationDate(string dateTime = "1/1/9999 12:00 AM");
        public abstract void EnterOwnerApprovedDate();
        public abstract void EnterOwnerReview(string ownerReviewText = "");
        public abstract void EnterPreparedByDate();
        public abstract void EnterQCManager(string qcMgrText = "");
        public abstract void EnterQCManagerApprovedDate();
        public abstract void EnterRecordEngineerApprovedDate();
        public abstract void EnterRepairPlan(string repairPlanText = "");
        public abstract void EnterResponsibleManager(string mgrName);
        public abstract string EnterRootCauseOfTheProblem(string description = "", bool tempDescription = false);
        public abstract void FilterDescription(string description = "");
        public abstract IList<string> GetExpectedRequiredFieldIDs();
        public abstract string GetNCRDocDescription(bool tempDescription = false);
        public abstract void PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ConcessionDeviation();
        public abstract void PopulateRelatedFields_And_SelectDDL_forConcessionRequest_ReturnToConformance();
        public abstract void PopulateRequiredFields();
        public abstract void PopulateRequiredFieldsAndSaveForward();
        public abstract void PopulateRequiredFieldsAndSaveOnly();
        public abstract void SelectChkbox_AsBuiltRequired(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectChkbox_RcmndDisposition_AcceptAsIs(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectChkbox_RcmndDisposition_CorrectRework(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectChkbox_RcmndDisposition_Repair(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectChkbox_RcmndDisposition_Replace(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectDDL_Area(int selectionIndex = 1);
        public abstract void SelectDDL_Feature(int selectionIndex = 2);
        public abstract void SelectDDL_Foreman(int selectionIndex = 1);
        public abstract void SelectDDL_Location(int selectionIndex = 1);
        public abstract void SelectDDL_Originator(int selectionIndexOrName = 1);
        public abstract void SelectDDL_PreparedBy(int selectionIndex = 1);
        public abstract void SelectDDL_ResponsibleManager(int selectionIndexOrName = 1);
        public abstract void SelectDDL_Roadway(int selectionIndex = 1);
        public abstract void SelectDDL_Segment(int selectionIndex = 1);
        public abstract void SelectDDL_Specification(int selectionIndex = 1);
        public abstract void SelectDDL_SubFeature(int selectionIndex = 1);
        public abstract void SelectDDL_TrackNo(int selectionIndex = 1);
        public abstract void SelectRdoBtn_EngOfRecordApproval_NA();
        public abstract void SelectRdoBtn_EngOfRecordApproval_No();
        public abstract void SelectRdoBtn_EngOfRecordApproval_Yes();
        public abstract void SelectRdoBtn_OwnerApproval_NA();
        public abstract void SelectRdoBtn_OwnerApproval_No();
        public abstract void SelectRdoBtn_OwnerApproval_Yes();
        public abstract void SelectRdoBtn_TypeOfNCR_Level1();
        public abstract void SelectRdoBtn_TypeOfNCR_Level2();
        public abstract void SelectRdoBtn_TypeOfNCR_Level3();
        public abstract void SignDateApproveNCR(Reviewer reviewer, bool Approve = true);
        public abstract void SortTable_Ascending();
        public abstract void SortTable_Descending();
        public abstract void SortTable_ToDefault();
        public abstract bool VerifyNCRDocIsClosed(string ncrDescription = "");
        public abstract bool VerifyNCRDocIsDisplayed(TableTab tableTab, string ncrDescription = "");
        public abstract bool VerifyReqFieldErrorLabelForConcessionRequest();
        public abstract bool VerifyReqFieldErrorLabelForTypeOfNCR();
        public abstract bool VerifyReqFieldErrorLabelsForNewDoc();
        public abstract bool VerifySignatureField(Reviewer reviewer, bool shouldBeEmpty = false);
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
            => CheckNCRisClosed(description, TableTab.Closed_NCR);
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
                InputFields.Roadway.GetString(),
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
            SelectDDL_TrackNo();
            SelectDDL_Originator();
            EnterDescription();
            EnterRootCauseOfTheProblem();
            SelectDDL_PreparedBy();
            EnterPreparedByDate();
        }

        public override void ClickTab_Creating_Revise()
            => GridHelper.ClickTab(TableTab.Create_Revise);

        public override void ClickBtn_Approve()
            => ActionConfirmation(SubmitButtons.Approve, false);

        public override void ClickBtn_DisapproveClose()
            => ActionConfirmation(SubmitButtons.DisapproveClose, false);
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
            => GridHelper.ClickTab(TableTab.ALL_NCRs);

        public override bool VerifyNCRDocIsClosed(string description = "")
            => CheckNCRisClosed(description, TableTab.ALL_NCRs);
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
            => GridHelper.ClickTab(TableTab.ALL_NCRs);

        public override bool VerifyNCRDocIsClosed(string description = "")
            => CheckNCRisClosed(description, TableTab.ALL_NCRs);
    }

    #endregion Implementation specific to SGWay - SimpleWF
}