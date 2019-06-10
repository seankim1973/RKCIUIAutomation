﻿using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.DesignDocument;
using static RKCIUIAutomation.Page.Workflows.DesignDocumentWF;
using static RKCIUIAutomation.Base.Factory;
using System.Text.RegularExpressions;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public class DesignDocument : DesignDocument_Impl, IDesignDocument
    {
        public DesignDocument()
        {            
            createPgEntryFieldKeyValuePairs = GetDesignDocEntryFieldKeyValuePairs();           
            commentEntryFieldKeyValuePairs = GetCommentEntryFieldKeyValuePairs();
        }

        public DesignDocument(IWebDriver driver)
        {
            this.Driver = driver;
            //commentEntryFieldsList = GetCommentEntryFieldsList();
            designDocDetailsHeadersList = GetDesignDocDetailsHeadersList();
            designDocCreatePgEntryFieldsList = GetDesignDocCreatePgEntryFieldsList();
        }

        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Tenant'
        /// </summary>
        public override T SetClass<T>(IWebDriver driver)
        {
            IDesignDocument instance = new DesignDocument(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using DesignDocument_SGWay instance ###### ");
                instance = new DesignDocument_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using  DesignDocument_SH249 instance ###### ");
                instance = new DesignDocument_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using  DesignDocument_Garnet instance ###### ");
                instance = new DesignDocument_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using  DesignDocument_GLX instance ###### ");
                instance = new DesignDocument_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using  DesignDocument_I15South instance ###### ");
                instance = new DesignDocument_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using DesignDocument_I15Tech instance ###### ");
                instance = new DesignDocument_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using DesignDocument_LAX instance ###### ");
                instance = new DesignDocument_LAX(driver);
            }
            return (T)instance;
        }

        public override void ScrollToLastColumn()
            => PageAction.ScrollToElement(By.XPath("//tbody/tr/td[@style='vertical-align: top;'][last()]"));

        public override void ScrollToFirstColumn()
            => PageAction.ScrollToElement(By.XPath("//tbody/tr/td[@style='vertical-align: top;'][1]"));

        public enum ReviewType
        {
            RegularComment,
            NoComment
        }

        public enum DesignDocEntryFieldType
        {
            [StringValue("Submittal_Title", TEXT)] Title,
            [StringValue("Submittal_Document_Number", TEXT)] DocumentNumber,
            [StringValue("Submittal_Document_DocumentDate", DATE)] DocumentDate,
            [StringValue("Submittal_SegmentId", DDL)] Segment,
            [StringValue("Submittal_SecuritySensitiveInformationViewRightRequired", CHKBOX)] SSI,
            [StringValue("Submittal_TransmittalNumber", TEXT)] TransmittalNumber,
            [StringValue("Submittal_TransmittalDate", DATE)] TransmittalDate,
            [StringValue("chkIncludeDot", CHKBOX)] MaxReviewDays_DOT_Chkbox,
            [StringValue("MaxDOTDays", TEXT)] MaxReviewDays_DOT,
            [StringValue("chkIncludeIqf", CHKBOX)] MaxReviewDays_QAF_Chkbox,
            [StringValue("MaxIQFDays", TEXT)] MaxReviewDays_QAF,
            [StringValue("chkIncludeOther", CHKBOX)] MaxReviewDays_Other_Chkbox,
            [StringValue("MaxOtherDays", TEXT)] MaxReviewDays_Other,
            [StringValue("Submittal_OtherReviewerId", DDL)] MaxReviewDays_Other_Reviewer
        }

        public enum DesignDocHeaderType
        {
            [StringValue("Date")] Date,
            [StringValue("Document Date")] Document_Date,
            [StringValue("Number")] Number,
            [StringValue("Document Number")] Document_Number,
            [StringValue("Title")] Title,
            [StringValue("Segment")] Segment,
            [StringValue("Action")] Action,
            [StringValue("Status")] Status,
            [StringValue("Trans. Date")] Trans_Date,
            [StringValue("Transmittal Date")] Transmittal_Date,
            [StringValue("Trans. N°")] Trans_No,
            [StringValue("Transmittal N°")] Transmittal_No,
            [StringValue("Review Deadline")] Review_Deadline,
            [StringValue("Remaining Days")] Remaining_Days
            //[StringValue("")] File_Name
        }

        public enum TableTab
        {
            [StringValue("Creating")] Creating,
            [StringValue("Comment")] Comment,
            [StringValue("Pending Comment")] Pending_Comment,
            [StringValue("Requires Comment")] Requires_Comment,
            [StringValue("Response")] Response,
            [StringValue("Pending Response")] Pending_Response,
            [StringValue("Requires Response")] Requires_Response,
            [StringValue("Pending Resolution")] Pending_Resolution,
            [StringValue("Requires Resolution")] Requires_Resolution,
            [StringValue("Verification")] Verification,
            [StringValue("Pending Closing")] Pending_Closing,
            [StringValue("Requires Closing")] Requires_Closing,
            [StringValue("Closed")] Closed,
        }

        public enum ColumnName
        {
            [StringValue("Number")] Number,
            [StringValue("Title")] Title,
            [StringValue("DocumentId")] Action
        }

        /// <summary>
        /// DDL, TEXT, AUTOPOPULATED
        /// </summary>
        public enum CommentFieldType
        {
            //Legacy Comment Entry Fields in Comment Tab
            [StringValue("Comment_ReviewTypeId_", DDL)] ReviewType,
            [StringValue("Comment_ResponseCodeId_", DDL)] ResponseCode,
            [StringValue("Comment_ResolutionStampId_", DDL)] ResolutionStamp,
            [StringValue("Comment_ClosingStampId_", DDL)] ClosingStamp,
            [StringValue("Comment_CommentTypeId_", DDL)] CommentType,
            [StringValue("Comment_CategoryId_", DDL)] Category,
            [StringValue("Comment_DisciplineId_", DDL)] Discipline,
            [StringValue("Comment_Text_", TEXT)] CommentInput,
            [StringValue("Comment_ContractReference_", TEXT)] ContractReferenceInput,
            [StringValue("Comment_DrawingPageNumber_", TEXT)] DrawingPageNumberInput,
            [StringValue("Comment_Response_", TEXT)] CommentResponseInput,
            [StringValue("Comment_ResolutionMeetingDecision_", TEXT)] CommentResolutionInput,
            [StringValue("Comment_ClosingComment_", TEXT)] CommentClosingInput,
            [StringValue("Comment_ReviewerId_", AUTOPOPULATED)] Reviewer,
            [StringValue("Comment_ReviewedDate_", AUTOPOPULATED)] ReviewedDate,
            
            //Comment Entry Fields in Grid View
            [StringValue("CommentType", DDL)] DocType_InTable,
            [StringValue("CommentType", DDL)] CommentType_InTable,
            [StringValue("Category", DDL)] Category_InTable,
            [StringValue("Organization", DDL)] Org_InTable,
            [StringValue("ReviewType", DDL)] ReviewType_InTable,
            [StringValue("Text", TEXT)] CommentInput_InTable,
            [StringValue("ContractReference", TEXT)] ContractReferenceInput_InTable,
            [StringValue("DrawingPageNumber", TEXT)] DrawingPageNumberInput_InTable,
            [StringValue("Discipline", DDL)] Discipline_InTable,
            [StringValue("Response", TEXT)] CommentResponseInput_InTable,
            [StringValue("ResponseCode", DDL)] ResponseCode_InTable,
            [StringValue("ResolutionMeetingDecision", TEXT)] CommentResolutionInput_InTable,
            [StringValue("ResolutionStamp", DDL)] ResolutionStamp_InTable,
            [StringValue("ClosingComment", TEXT)] CommentClosingInput_InTable,
            [StringValue("ClosingStamp", DDL)] ClosingStamp_InTable,
            [StringValue("ReviewerName", TEXT)] ReviewerName_InTable,
            [StringValue("Reviewer", DDL)] Reviewer_InTable,
            [StringValue("VerifierName", TEXT)] VerifiedBy_InTable,
            [StringValue("VerificationCode", DDL)] VerificationCode_InTable,
            [StringValue("VerifiedDateOffset", TEXT)] VerifiedDate_InTable,
            [StringValue("VerificationNotes", TEXT)] VerificationNotes_InTable
        }

        public enum CommentRowBtn
        {
            Edit,
            Delete,
            Files,
            Details
        }

        [ThreadStatic]
        internal static string documentStatus;

        [ThreadStatic]
        internal static string designDocTitle;

        [ThreadStatic]
        internal static string designDocNumber;

        [ThreadStatic]
        internal static IList<DesignDocEntryFieldType> designDocCreatePgEntryFieldsList;

        [ThreadStatic]
        internal static IList<DesignDocHeaderType> designDocDetailsHeadersList;

        [ThreadStatic]
        internal static IList<KeyValuePair<DesignDocEntryFieldType, string>> createPgEntryFieldKeyValuePairs;

        [ThreadStatic]
        internal static IList<CommentFieldType> commentEntryFieldsList;

        [ThreadStatic]
        internal static IList<KeyValuePair<CommentFieldType, string>> commentEntryFieldKeyValuePairs;

        //Page element By Locators
        public override By UploadNewDesignDoc_ByLocator => By.XPath("//a[text()='Upload New Design Document']");
        public override By CancelBtnUploadPage_ByLocator => By.Id("btnCancel");
        public override By SaveOnlyBtnUploadPage_ByLocator => By.Id("btnSave");
        public override By SaveForwardBtnUploadPage_ByLocator => By.Id("btnSaveForward");
        public override By SaveOnlyBtn_ByLocator => By.XPath("//div[@class='k-content k-state-active']//button[contains(@id,'btnSave_')]");
        public override By Table_ForwardBtn_ByLocator => By.XPath("//button[@id='btnSaveForward']");
        public override By SaveForwardBtn_ByLocator => By.XPath("//div[@class='k-content k-state-active']//button[contains(@id,'btnSaveForward_')]");
        public override By BackToListBtn_ByLocator => By.XPath("//a[text()=' Back to List']"); //For tenants with Grid View for Comment Entries (LAX & SH249)
        public override By Btn_Refresh_ByLocator => By.XPath("//a[@aria-label='Refresh']");
        public override By Btn_Cancel_ByLocator => By.Id("btnCancelSave");
        public override By Btn_Forward_ByLocator => By.XPath("//button[@id='btnSaveForward']");
        public override By Btn_ShowFileList_ByLocator => By.XPath("//button[contains(@class,'showFileList')]");
        public override By Btn_PDF_ByLocator => By.Id("PdfExportLink");
        public override By Btn_XLS_ByLocator => By.Id("CsvExportLink");

        public override IList<CommentFieldType> RegularCommentFieldsList { get; set; }

        public override IList<CommentFieldType> NoCommentFieldsList { get; set; }

        public override void CreateDocument()
        {
            IPageInteraction pgAction = PageAction;
            pgAction.WaitForPageReady();
            pgAction.ClickElement(UploadNewDesignDoc_ByLocator);
            PopulateAllCreatePgEntryFields();
            pgAction.UploadFile("test.xlsx");
            pgAction.ClickElement(SaveForwardBtnUploadPage_ByLocator);
            pgAction.WaitForPageReady();
        }

        private string GetTblColumnIndex(CommentFieldType tableHeader)
            => PageAction.GetAttribute(By.XPath($"//thead[@role='rowgroup']/tr/th[@data-field='{tableHeader.GetString()}']"), "data-index");

        private By GetTblBtnByLocator(CommentRowBtn rowButton, int rowID)
            => By.XPath($"//tbody/tr[{rowID}]/td[1]/a[text()='{rowButton.ToString()}']");

        private void Click_TblRowBtn(CommentRowBtn rowButton, bool clickBtnForLatestRow = true, int rowID = 1)
        {
            By locator = By.XPath("//div[@class='k-grid-content']//tbody/tr");

            try
            {
                rowID = clickBtnForLatestRow
                    ? PageAction.GetElementsCount(locator)
                    : rowID;

                PageAction.JsClickElement(GetTblBtnByLocator(rowButton, rowID));
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public override void ClickBtn_CommentsTblRow_Edit(bool clickBtnForLatestRow = true, int rowID = 1)
            => Click_TblRowBtn(CommentRowBtn.Edit, clickBtnForLatestRow, rowID);

        public override void ClickBtn_CommentsTblRow_Delete(bool clickBtnForLatestRow = true, int rowID = 1)
            => Click_TblRowBtn(CommentRowBtn.Delete, clickBtnForLatestRow, rowID);

        public override void ClickBtn_CommentsTblRow_Files(bool clickBtnForLatestRow = true, int rowID = 1)
            => Click_TblRowBtn(CommentRowBtn.Files, clickBtnForLatestRow, rowID);

        public override void ClickBtn_CommentsTblRow_Details(bool clickBtnForLatestRow = true, int rowID = 1)
            => Click_TblRowBtn(CommentRowBtn.Details, clickBtnForLatestRow, rowID);

        private void StoreDesignDocTitleAndNumber()
        {
            designDocTitle = GetVar("DsgnDocTtl");
            designDocNumber = GetVar("DsgnDocNumb");
            log.Debug($"#####Title: {designDocTitle}\nNumber: {designDocNumber}");
        }

        public override void EnterDesignDocTitleAndNumber()
        {
            StoreDesignDocTitleAndNumber();
            PageAction.EnterText(GetTextInputFieldByLocator(DesignDocEntryFieldType.Title), designDocTitle);
            PageAction.EnterText(GetTextInputFieldByLocator(DesignDocEntryFieldType.DocumentNumber), designDocNumber);
        }

        private void Click_UniqueTblBtn(string btnClass)
        {
            PageAction.ClickElement(By.XPath($"//a[contains(@class, '{btnClass}')]"));
            PageAction.WaitForPageReady();
        }

        public override void ClickBtn_AddComment()
            => Click_UniqueTblBtn("k-grid-add");

        public override void ClickBtn_Update()
            => Click_UniqueTblBtn("k-grid-update");

        public override void ClickBtn_Cancel()
            => Click_UniqueTblBtn("k-grid-cancel");

        private KeyValuePair<DesignDocEntryFieldType, string> PopulateAndStoreDesignDocEntryFieldValue<T>(DesignDocEntryFieldType entryField, T indexOrText, bool useContains = false)
        {
            string fieldType = entryField.GetString(true);
            Type argType = indexOrText.GetType();
            object argValue = null;
            bool isValidArg = false;

            KeyValuePair<DesignDocEntryFieldType, string> fieldValuePair;
            string fieldValue = string.Empty;

            try
            {
                if (argType == typeof(string))
                {
                    isValidArg = true;
                    argValue = ConvertToType<string>(indexOrText);
                }
                else if (argType == typeof(int))
                {
                    isValidArg = true;
                    argValue = ConvertToType<int>(indexOrText);
                }

                if (isValidArg)
                {
                    if (fieldType.Equals(TEXT) || fieldType.Equals(DATE) || fieldType.Equals(FUTUREDATE))
                    {
                        if (!((string)argValue).HasValue())
                        {
                            if (fieldType.Equals(DATE) || fieldType.Equals(FUTUREDATE))
                            {
                                argValue = fieldType.Equals(DATE)
                                    ? GetShortDate()
                                    : GetFutureShortDate();
                            }
                            else
                            {
                                if (entryField.Equals(DesignDocEntryFieldType.Title))
                                {
                                    designDocTitle = GetVar(entryField);
                                    argValue = designDocTitle;
                                }
                                else if (entryField.Equals(DesignDocEntryFieldType.DocumentNumber))
                                {
                                    designDocNumber = GetVar(entryField);
                                    argValue = designDocNumber;
                                }
                                else
                                {
                                    argValue = GetVar(entryField, false);
                                }

                                int argValueLength = ((string)argValue).Length;

                                By inputLocator = GetInputFieldByLocator(entryField);
                                int elemMaxLength = 0;

                                try
                                {
                                    elemMaxLength = int.Parse(PageAction.GetAttribute(inputLocator, "maxlength"));
                                }
                                catch (Exception)
                                {
                                }

                                argValue = elemMaxLength > 0 && argValueLength > elemMaxLength
                                    ? ((string)argValue).Substring(0, elemMaxLength)
                                    : argValue;
                            }

                            fieldValue = (string)argValue;
                        }

                        PageAction.EnterText(By.Id(entryField.GetString()), fieldValue);
                    }
                    else if (fieldType.Equals(DDL) || fieldType.Equals(MULTIDDL))
                    {
                        argValue = ((argType == typeof(string) && !((string)argValue).HasValue()) || (int)argValue < 1)
                        ? 1
                        : argValue;

                        PageAction.ExpandAndSelectFromDDList(entryField, argValue, useContains, fieldType.Equals(MULTIDDL) ? true : false);

                        if (fieldType.Equals(DDL))
                        {
                            fieldValue = PageAction.GetTextFromDDL(entryField);
                        }
                        else
                        {
                            fieldValue = string.Join("::", PageAction.GetTextFromMultiSelectDDL(entryField).ToArray());
                        }                       
                    }
                    else if (fieldType.Equals(RDOBTN) || fieldType.Equals(CHKBOX))
                    {
                        if (entryField.Equals(DesignDocEntryFieldType.MaxReviewDays_QAF_Chkbox))
                        {
                            entryField = DesignDocEntryFieldType.MaxReviewDays_QAF;
                        }
                        else if (entryField.Equals(DesignDocEntryFieldType.MaxReviewDays_DOT_Chkbox))
                        {
                            entryField = DesignDocEntryFieldType.MaxReviewDays_DOT;
                        }
                        else if (entryField.Equals(DesignDocEntryFieldType.MaxReviewDays_Other_Chkbox))
                        {
                            PageAction.SelectRadioBtnOrChkbox(entryField);
                            entryField = DesignDocEntryFieldType.MaxReviewDays_Other;
                        }

                        fieldValue = PageAction.GetText(By.Id(entryField.GetString()));
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            return fieldValuePair = new KeyValuePair<DesignDocEntryFieldType, string>(entryField, fieldValue);
        }

        /// <summary>
        /// Populates entry fields accordingly to 'designDocCreatePgEntryFields' list ([ThreadStatic] field variable) and stores EntryField enum and value in 'createPgEntryFieldKeyValuePairs' ([ThreadStatic] field variable).
        /// </summary>
        public override void PopulateAllCreatePgEntryFields()
        {
            foreach (DesignDocEntryFieldType entryField in designDocCreatePgEntryFieldsList)
            {
                KeyValuePair<DesignDocEntryFieldType, string> kvPair = new KeyValuePair<DesignDocEntryFieldType, string>();
                kvPair = PopulateAndStoreDesignDocEntryFieldValue(entryField, "");
                createPgEntryFieldKeyValuePairs.Add(kvPair);
            }
        }

        private DesignDocEntryFieldType GetExpectedDesignDocEntryFieldTypeForDesignDocHeaderType(DesignDocHeaderType docHeader)
        {
            DesignDocEntryFieldType entryField = DesignDocEntryFieldType.Title;

            if (docHeader.Equals(DesignDocHeaderType.Date) || docHeader.Equals(DesignDocHeaderType.Document_Date))
            {
                entryField = DesignDocEntryFieldType.DocumentDate;
            }
            else if (docHeader.Equals(DesignDocHeaderType.Number) || docHeader.Equals(DesignDocHeaderType.Document_Number))
            {
                entryField = DesignDocEntryFieldType.DocumentNumber;
            }
            else if (docHeader.Equals(DesignDocHeaderType.Transmittal_Date) || docHeader.Equals(DesignDocHeaderType.Trans_Date))
            {
                entryField = DesignDocEntryFieldType.TransmittalDate;
            }
            else if (docHeader.Equals(DesignDocHeaderType.Transmittal_No) || docHeader.Equals(DesignDocHeaderType.Trans_No))
            {
                entryField = DesignDocEntryFieldType.TransmittalNumber;
            }
            else if (docHeader.Equals(DesignDocHeaderType.Segment))
            {
                entryField = DesignDocEntryFieldType.Segment;
            }
            else if (docHeader.Equals(DesignDocHeaderType.Title))
            {
                entryField = DesignDocEntryFieldType.Title;
            }

            return entryField;
        }

        public override void VerifyDesignDocDetailsHeader()
        {
            IList<string> expectedValueInHeaderList = new List<string>();
            IList<string> actualValueInHeaderList = new List<string>();
            string expectedHeaderValue = string.Empty;
            bool headerIsDisplayed = true;

            try
            {
                foreach (DesignDocHeaderType header in designDocDetailsHeadersList)
                {
                    if (header.Equals(DesignDocHeaderType.Action))
                    {
                        expectedHeaderValue = "For Review/Comment";
                    }
                    else if (header.Equals(DesignDocHeaderType.Status))
                    {
                        expectedHeaderValue = documentStatus;
                    }
                    else
                    {
                        DesignDocEntryFieldType entryFieldType = GetExpectedDesignDocEntryFieldTypeForDesignDocHeaderType(header);
                        expectedHeaderValue = (from kvp in createPgEntryFieldKeyValuePairs where kvp.Key == entryFieldType select kvp.Value).FirstOrDefault();

                        if (header.Equals(DesignDocHeaderType.Document_Date))
                        {
                            expectedHeaderValue = GetShortDate(expectedHeaderValue, true);
                        }
                        else if (header.Equals(DesignDocHeaderType.Remaining_Days) || header.Equals(DesignDocHeaderType.Review_Deadline))
                        {
                            //On SG, the 'Review Deadline' header field is not shown when Status is 'Requires Response'
                            if (documentStatus.Equals("Requires Response"))
                            {
                                headerIsDisplayed = false;
                            }
                            else
                            {
                                string reviewerType = GetCurrentReviewerType();

                                if (reviewerType.Equals("DOT"))
                                {
                                    entryFieldType = DesignDocEntryFieldType.MaxReviewDays_DOT;
                                }
                                else
                                {
                                    entryFieldType = DesignDocEntryFieldType.MaxReviewDays_QAF;
                                }

                                string maxReviewDays = (from kvp in createPgEntryFieldKeyValuePairs where kvp.Key == entryFieldType select kvp.Value).FirstOrDefault();

                                if (header.Equals(DesignDocHeaderType.Review_Deadline))
                                {
                                    string documentDate = (from kvp in createPgEntryFieldKeyValuePairs where kvp.Key == DesignDocEntryFieldType.DocumentDate select kvp.Value).FirstOrDefault();
                                    string deadlineDate = GetFutureShortDate(documentDate, double.Parse(maxReviewDays));
                                    expectedHeaderValue = $"{deadlineDate} ({reviewerType})";
                                }
                                else
                                {
                                    expectedHeaderValue = $"{maxReviewDays} ({reviewerType})";
                                }                               
                            }
                        }
                    }

                    if (headerIsDisplayed)
                    {
                        actualValueInHeaderList.Add(GetHeaderValue(header));
                        expectedValueInHeaderList.Add($"{header}::{expectedHeaderValue}");
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            string reportName = $"VerifyDesignDocDetailsHeader [GridTab ({documentStatus})]";
            TestUtility.AddAssertionToList(PageAction.VerifyExpectedList(actualValueInHeaderList, expectedValueInHeaderList, reportName), reportName);
        }

        public override string GetHeaderValue(DesignDocHeaderType docHeader)
            => PageAction.GetText(By.XPath($"//label[contains(text(),'{docHeader.GetString()}')]/following-sibling::div[1]"));

        public override void SetDesignDocStatus<T>(T tableTabOrWorkflow)
        {
            Type argType = tableTabOrWorkflow.GetType();

            if (argType == typeof(TableTab))
            {
                TableTab tableTab = TableTab.Creating;

                tableTab = ConvertToType<TableTab>(tableTabOrWorkflow);
                if (tableTab.Equals(TableTab.Comment) || tableTab.Equals(TableTab.Requires_Comment))
                {
                    documentStatus = "Requires Comment";
                }
                else if (tableTab.Equals(TableTab.Response) || tableTab.Equals(TableTab.Requires_Response))
                {
                    documentStatus = "Requires Response";
                }
                else if (tableTab.Equals(TableTab.Requires_Closing) || tableTab.Equals(TableTab.Verification))
                {
                    documentStatus = "Requires Closing";
                }
                else
                {
                    switch (tableTab)
                    {
                        case TableTab.Pending_Comment://SG
                            documentStatus = "Pending Comment";
                            break;
                        case TableTab.Pending_Resolution://SG
                            documentStatus = "Pending Resolution";
                            break;
                        case TableTab.Requires_Resolution://SH249, SG
                            documentStatus = "Requires Resolution";
                            break;
                        case TableTab.Pending_Response://SG
                            documentStatus = "Pending Response";
                            break;
                        case TableTab.Pending_Closing://SG
                            documentStatus = "Pending Closing";
                            break;
                        case TableTab.Closed://LAX, SG, SH249
                            documentStatus = "Review Completed";
                            break;
                    }
                }
            }
            else if (argType == typeof(CR_Workflow))
            {
                CR_Workflow workflow = CR_Workflow.CreateComment;
                workflow = ConvertToType<CR_Workflow>(tableTabOrWorkflow);
                documentStatus = GridHelper.GetCurrentTableTabName();
            }
        }

        public override string GetDesignDocStatus()
            => documentStatus;

        public override IList<KeyValuePair<DesignDocEntryFieldType, string>> GetDesignDocEntryFieldKeyValuePairs()
        {
            if (createPgEntryFieldKeyValuePairs == null)
            {
                createPgEntryFieldKeyValuePairs = new List<KeyValuePair<DesignDocEntryFieldType, string>>();
            }

            return createPgEntryFieldKeyValuePairs;
        }

        public override IList<KeyValuePair<CommentFieldType, string>> GetCommentEntryFieldKeyValuePairs()
        {
            if (commentEntryFieldKeyValuePairs == null)
            {
                commentEntryFieldKeyValuePairs = new List<KeyValuePair<CommentFieldType, string>>();
            }

            return commentEntryFieldKeyValuePairs;
        }

        public override void VerifyItemStatusIsClosed()
        {
            ClickTab_Closed();
            TestUtility.AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(ColumnName.Number, designDocNumber), $"VerifyItemStatusIsClosed");
        }

        public override IList<DesignDocEntryFieldType> GetDesignDocCreatePgEntryFieldsList()
            => designDocCreatePgEntryFieldsList;

        public override IList<DesignDocHeaderType> GetDesignDocDetailsHeadersList()
            => designDocDetailsHeadersList;

        //TODO 
        private KeyValuePair<CommentFieldType, string> PopulateAndStoreCommentFieldValue<T>(CommentFieldType commentFieldType, T indexOrText, bool useContains = false)
        {
            string fieldType = commentFieldType.GetString(true);
            Type argType = indexOrText.GetType();
            object argValue = null;
            bool isValidArg = false;

            KeyValuePair<CommentFieldType, string> fieldValuePair;
            string fieldValue = string.Empty;

            try
            {
                if (argType == typeof(string))
                {
                    isValidArg = true;
                    argValue = ConvertToType<string>(indexOrText);
                }
                else if (argType == typeof(int))
                {
                    isValidArg = true;
                    argValue = ConvertToType<int>(indexOrText);
                }

                if (isValidArg)
                {
                    if (fieldType.Equals(TEXT))
                    {
                        if (!((string)argValue).HasValue())
                        {
                            int argValueLength = ((string)argValue).Length;

                            By inputLocator = GetInputFieldByLocator(commentFieldType);
                            int elemMaxLength = 0;

                            try
                            {
                                elemMaxLength = int.Parse(PageAction.GetAttribute(inputLocator, "maxlength"));
                            }
                            catch (Exception)
                            {
                                log.Debug($"Element {inputLocator} does not have a maxlength attribute.");
                            }

                            argValue = elemMaxLength > 0 && argValueLength > elemMaxLength
                                ? ((string)argValue).Substring(0, elemMaxLength)
                                : argValue;

                            fieldValue = (string)argValue;
                        }

                        PageAction.EnterText(By.Id(commentFieldType.GetString()), fieldValue);
                    }
                    else if (fieldType.Equals(DDL))
                    {
                        if ((argType == typeof(string) && !((string)argValue).HasValue()) || (int)argValue < 1)
                        {
                            argValue = 1;
                        }

                        PageAction.ExpandAndSelectFromDDList(commentFieldType, argValue, useContains);
                    }
                    else if (fieldType.Equals(AUTOPOPULATED))
                    {
                        if (commentFieldType.Equals(CommentFieldType.Reviewer))
                        {
                            argValue = GetTextFromDDL(commentFieldType);
                        }
                        else
                        {
                            argValue = GetText(By.Id(commentFieldType.GetString()));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }

            return fieldValuePair = new KeyValuePair<CommentFieldType, string>(commentFieldType, fieldValue);

        }


        public override IList<CommentFieldType> GetCommentEntryFieldsList(ReviewType reviewType)
        {
            if (commentEntryFieldsList == null)
            {
                IList<CommentFieldType> fieldsList = null;

                commentEntryFieldsList = new List<CommentFieldType> { };

                if (reviewType == ReviewType.RegularComment)
                {
                    fieldsList = RegularCommentFieldsList;
                }
                else if (reviewType == ReviewType.NoComment)
                {
                    fieldsList = NoCommentFieldsList;
                }

                ((List<CommentFieldType>)commentEntryFieldsList).AddRange(fieldsList);
            }

            return commentEntryFieldsList;
        }

        public override void SelectDDL_ReviewType(int selectionIndex)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ReviewType_InTable, selectionIndex);

        public override void SelectDDL_Reviewer<T>(T selectionIndexOrReviewerName, bool useContainsFilter)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.Reviewer_InTable, selectionIndexOrReviewerName, useContainsFilter);

        public override void SelectDDL_CommentType(int selectionIndex)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.CommentType_InTable, selectionIndex);

        public override void SelectDDL_Category(int selectionIndex)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.Category_InTable, selectionIndex);

        public override void SelectDDL_Discipline(int selectionIndex)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.Discipline_InTable, selectionIndex);

        public override void ClickBtn_BackToList()
        {
            PageAction.JsClickElement(BackToListBtn_ByLocator);
            PageAction.WaitForPageReady();
        }

        public override void ClickBtn_SaveOnly()
        {
            PageAction.ClickElement(SaveOnlyBtn_ByLocator);
            PageAction.WaitForPageReady();
        }

        public override void ClickBtn_SaveForward()
        {
            PageAction.JsClickElement(SaveForwardBtn_ByLocator);
            PageAction.WaitForPageReady();
        }

        internal string SetCommentStamp_XPath(CommentFieldType inputFieldEnum, int commentTabIndex)
            => $"{inputFieldEnum.GetString()}{(commentTabIndex - 1).ToString()}_";

        public override void SelectRegularCommentReviewType(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp_XPath(CommentFieldType.ReviewType, commentTabNumber), 3);

        public override void SelectNoCommentReviewType(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp_XPath(CommentFieldType.ReviewType, commentTabNumber), 1);

        public override void SelectCommentType(int commentTypeTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp_XPath(CommentFieldType.CommentType, commentTypeTabNumber), 1);

        public override void SelectDiscipline(int disciplineNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp_XPath(CommentFieldType.Discipline, disciplineNumber), 1);

        public override void SelectCategory(int categoryNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp_XPath(CommentFieldType.Category, categoryNumber), 1);

        public override void SelectAgreeResolutionCode(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp_XPath(CommentFieldType.ResolutionStamp, commentTabNumber), 1); //check the index, UI not working so need to confirm later

        public override void SelectAgreeResponseCode(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp_XPath(CommentFieldType.ResponseCode, commentTabNumber), 2); //check the index, UI not working so need to confirm later

        public override void SelectDisagreeResponseCode(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp_XPath(CommentFieldType.ResponseCode, commentTabNumber), 3);//check the index, UI not working so need to confirm later

        public override void SelectDisagreeResolutionCode(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp_XPath(CommentFieldType.ResolutionStamp, commentTabNumber), 2);//check the index, UI not working so need to confirm later

        public override void SelectDDL_ClosingStamp(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp_XPath(CommentFieldType.ClosingStamp, commentTabNumber), 1);

        public override void SelectOrganization(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.Org_InTable, selectionIndex);

        public override void SelectDDL_VerificationCode(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.VerificationCode_InTable, selectionIndex);

        public override void EnterVerifiedDate(string shortDate = "01/01/2019")
        {
            shortDate = shortDate.Equals("01/01/2019")
                ? GetShortDate()
                : shortDate;

            PageAction.EnterText(GetTextInputFieldByLocator(CommentFieldType.VerifiedDate_InTable), shortDate);
        }


        /// <summary>
        /// Filters Number column using ThreadStatic value, designDocNumber, by default.
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="filterByValue"></param>
        public override void FilterDocNumber(string filterByValue = "")
        {
            try
            {
                designDocNumber = !filterByValue.HasValue()
                    ? designDocNumber
                    : filterByValue;
                GridHelper.FilterTableColumnByValue(ColumnName.Number, designDocNumber);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        public override string EnterTextInCommentField(CommentFieldType commentField, int commentTabNumber = 1)
        {
            string commentValue = "Comment 123";
            By entryFieldLocator = null;

            try
            {
                Type commentFieldEnum = commentField.GetType();

                if (commentFieldEnum == typeof(CommentFieldType))
                {
                    entryFieldLocator = By.Id($"{commentField.GetString()}{commentTabNumber - 1}_");
                }
                else if (commentFieldEnum == typeof(CommentFieldType))
                {
                    entryFieldLocator = By.Id($"{commentField.GetString()}");
                }

                PageAction.ScrollToElement(entryFieldLocator);

                if (commentField.Equals(CommentFieldType.VerifiedDate_InTable))
                {
                    commentValue = GetShortDate();
                    PageAction.EnterText(GetTextInputFieldByLocator(commentField), commentValue);
                }
                else
                {
                    if (commentField.Equals(CommentFieldType.ContractReferenceInput) || commentField.Equals(CommentFieldType.ContractReferenceInput_InTable))
                    {
                        commentValue = "Contract Reference 123";
                    }
                    else if (commentField.Equals(CommentFieldType.DrawingPageNumberInput) || commentField.Equals(CommentFieldType.DrawingPageNumberInput_InTable))
                    {
                        commentValue = "Drawing PageNumber 123";
                    }
                    else if (commentField.Equals(CommentFieldType.CommentResponseInput) || commentField.Equals(CommentFieldType.CommentResponseInput_InTable))
                    {
                        commentValue = "Comment Response 123";
                    }
                    else if (commentField.Equals(CommentFieldType.CommentResolutionInput) || commentField.Equals(CommentFieldType.CommentResolutionInput_InTable))
                    {
                        commentValue = "Comment Resolution 123";
                    }
                    else if (commentField.Equals(CommentFieldType.CommentClosingInput) || commentField.Equals(CommentFieldType.CommentClosingInput_InTable))
                    {
                        commentValue = "Closing Comment 123";
                    }
                    else if (commentField.Equals(CommentFieldType.VerificationNotes_InTable))
                    {
                        commentValue = "Verification Notes 123";
                    }
                    else if (commentField.Equals(CommentFieldType.ReviewerName_InTable) || commentField.Equals(CommentFieldType.VerifiedBy_InTable))
                    {
                        commentValue = PageAction.GetCurrentUser();
                    }

                    PageAction.EnterText(entryFieldLocator, commentValue);
                }

                Report.Step($"Entered '{commentValue}' in {commentField.ToString()} field : {entryFieldLocator}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            return commentValue;
        }

        public override void EnterRegularCommentAndDrawingPageNo()
        {
            //login as commenting user (SG- IQFuser, DoTuser | SH249-- IQFUser | Garenet and GLX-- DOTUser)

            SelectRegularCommentReviewType();
            EnterTextInCommentField(CommentFieldType.CommentInput);
            EnterTextInCommentField(CommentFieldType.DrawingPageNumberInput);
            ClickBtn_SaveOnly();
        }

        public override void EnterNoComment()
        {
            //login as commenting user (SG- IQFuser, DoTuser | SH249-- IQFUser | Garenet and GLX-- DOTUser)
            SelectNoCommentReviewType();
            ClickBtn_SaveOnly();
        }

        public override void EnterResponseCommentAndAgreeResponseCode()
        {
            // Login as user to make response comment (All tenants - DevUser)
            EnterTextInCommentField(CommentFieldType.CommentResponseInput);
            SelectAgreeResponseCode(); //Disagree then different workflow
            ClickBtn_SaveOnly();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            // Login as user to make response comment (All tenants - DevUser)
            EnterTextInCommentField(CommentFieldType.CommentResponseInput);
            SelectDisagreeResponseCode(); //agree then different workflow
            ClickBtn_SaveOnly();
        }

        public override void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            Report.Step($"<<-- WORKFLOW ({tenantName}): EnterResolutionCommentAndResolutionCodeforDisagreeResponse -->>");
            ClickTab_Requires_Resolution();
            SortTable_Descending();
            GridHelper.ClickEnterBtnForRow();

            // Login as user to make resolution comment (All tenants - DevAdmin)
            EnterTextInCommentField(CommentFieldType.CommentResolutionInput);
            SelectDisagreeResolutionCode(); //
            ClickBtn_SaveOnly();
            ClickBtn_BackToList();
        }

        public override void Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            ClickTab_Requires_Resolution();
            SortTable_Descending();
            GridHelper.ClickEnterBtnForRow();
            ClickBtn_SaveForward();
        }

        private bool VerifyRequiredFieldErrorMsg(string errorMsg)
            => PageAction.CheckIfElementIsDisplayed(By.XPath($"//li[text()='{errorMsg}']"));

        public override bool VerifyTitleFieldErrorMsgIsDisplayed()
            => VerifyRequiredFieldErrorMsg("Submittal Title is required.");

        public override bool VerifyDocumentNumberFieldErrorMsgIsDisplayed()
            => VerifyRequiredFieldErrorMsg("Submittal Number is required.");

        public override bool VerifyUploadFileErrorMsgIsDisplayed()
            => VerifyRequiredFieldErrorMsg("At least one file must be added.");

        public override void SelectTab(TableTab tableTab)
        {
            try
            {
                PageAction.WaitForPageReady();
                Thread.Sleep(2500);
                GridHelper.ClickTab(tableTab);
                Thread.Sleep(2500);
                PageAction.WaitForPageReady();
            }
            catch (Exception e)
            {
                log.Error($"Error occured in SelectedTab() : {e.StackTrace}");
                throw;
            }

            SetDesignDocStatus(tableTab);
        }

        public override void ClickTab_Comment() => SelectTab(TableTab.Comment);

        public override void ClickTab_Response() => SelectTab(TableTab.Response);

        public override void ClickTab_Verification() => SelectTab(TableTab.Verification);

        public override void ClickTab_Creating() => SelectTab(TableTab.Creating);

        public override void ClickTab_Pending_Comment() => SelectTab(TableTab.Pending_Comment);

        public override void ClickTab_Requires_Comment() => SelectTab(TableTab.Requires_Comment);

        public override void ClickTab_Pending_Response() => SelectTab(TableTab.Pending_Response);

        public override void ClickTab_Requires_Response() => SelectTab(TableTab.Requires_Response);

        public override void ClickTab_Pending_Resolution() => SelectTab(TableTab.Pending_Resolution);

        public override void ClickTab_Requires_Resolution() => SelectTab(TableTab.Requires_Resolution);

        public override void ClickTab_Pending_Closing() => SelectTab(TableTab.Pending_Closing);

        public override void ClickTab_Requires_Closing() => SelectTab(TableTab.Requires_Closing);

        public override void ClickTab_Closed() => SelectTab(TableTab.Closed);

        public override void SortTable_Descending() => GridHelper.SortColumnDescending(ColumnName.Action);

        public override void SortTable_Ascending() => GridHelper.SortColumnAscending(ColumnName.Action);

        public override void WaitForActiveCommentTab()
        {
            bool activeTabNotDisplayed = true;

            for (int i = 0; i > 30; i++)
            {
                do
                {
                    if (i == 30)
                    {
                        ElementNotVisibleException e = new ElementNotVisibleException();
                        log.Error($"Comment tab is not visible: {e.Message}");
                        throw e;
                    }
                    else
                    {
                        activeTabNotDisplayed = PageAction.CheckIfElementIsDisplayed(By.XPath("//div[@class='k-content k-state-active']"));
                    }
                }
                while (activeTabNotDisplayed);
            }
        }

        public override void ClickCommentTabNumber(int commentTabNumber)
        {
            WaitForActiveCommentTab();
            GridHelper.ClickCommentTab(commentTabNumber);
        }

        public override string GetCurrentReviewerType()
        {
            string currentUser = GetCurrentUser();
            string[] userName = null;

            if (currentUser.Contains("User"))
            {
                userName = Regex.Split(currentUser, " User");
            }
            else if (currentUser.Contains("Admin"))
            {
                userName = Regex.Split(currentUser, " Admin");
            }

            return userName[0];
        }

    }


    // DesignDocument Implementation class
    public abstract class DesignDocument_Impl : PageBase, IDesignDocument
    {
        public abstract IList<CommentFieldType> RegularCommentFieldsList { get; set; }
        public abstract IList<CommentFieldType> NoCommentFieldsList { get; set; }
        public abstract By BackToListBtn_ByLocator { get; }
        public abstract By Btn_Cancel_ByLocator { get; }
        public abstract By Btn_Forward_ByLocator { get; }
        public abstract By Btn_PDF_ByLocator { get; }
        public abstract By Btn_Refresh_ByLocator { get; }
        public abstract By Btn_ShowFileList_ByLocator { get; }
        public abstract By Btn_XLS_ByLocator { get; }
        public abstract By CancelBtnUploadPage_ByLocator { get; }
        public abstract By SaveForwardBtn_ByLocator { get; }
        public abstract By SaveForwardBtnUploadPage_ByLocator { get; }
        public abstract By SaveOnlyBtn_ByLocator { get; }
        public abstract By SaveOnlyBtnUploadPage_ByLocator { get; }
        public abstract By Table_ForwardBtn_ByLocator { get; }
        public abstract By UploadNewDesignDoc_ByLocator { get; }

        public abstract void ClickBtn_AddComment();
        public abstract void ClickBtn_BackToList();
        public abstract void ClickBtn_Cancel();
        public abstract void ClickBtn_CommentsTblRow_Delete(bool clickBtnForLatestRow = true, int rowID = 1);
        public abstract void ClickBtn_CommentsTblRow_Details(bool clickBtnForLatestRow = true, int rowID = 1);
        public abstract void ClickBtn_CommentsTblRow_Edit(bool clickBtnForLatestRow = true, int rowID = 1);
        public abstract void ClickBtn_CommentsTblRow_Files(bool clickBtnForLatestRow = true, int rowID = 1);
        public abstract void ClickBtn_SaveForward();
        public abstract void ClickBtn_SaveOnly();
        public abstract void ClickBtn_Update();
        public abstract void ClickCommentTabNumber(int commentTabNumber);
        public abstract void ClickTab_Closed();
        public abstract void ClickTab_Comment();
        public abstract void ClickTab_Creating();
        public abstract void ClickTab_Pending_Closing();
        public abstract void ClickTab_Pending_Comment();
        public abstract void ClickTab_Pending_Resolution();
        public abstract void ClickTab_Pending_Response();
        public abstract void ClickTab_Requires_Closing();
        public abstract void ClickTab_Requires_Comment();
        public abstract void ClickTab_Requires_Resolution();
        public abstract void ClickTab_Requires_Response();
        public abstract void ClickTab_Response();
        public abstract void ClickTab_Verification();
        public abstract void CreateDocument();
        public abstract void EnterDesignDocTitleAndNumber();
        public abstract void EnterNoComment();
        public abstract void EnterRegularCommentAndDrawingPageNo();
        public abstract void EnterResponseCommentAndAgreeResponseCode();
        public abstract void EnterResponseCommentAndDisagreeResponseCode();
        public abstract string EnterTextInCommentField(CommentFieldType commentType, int commentTabNumber = 1);
        public abstract void EnterVerifiedDate(string shortDate = "01/01/2019");
        public abstract void FilterDocNumber(string filterByValue = "");
        public abstract IList<KeyValuePair<CommentFieldType, string>> GetCommentEntryFieldKeyValuePairs();
        public abstract IList<CommentFieldType> GetCommentEntryFieldsList(ReviewType reviewType);
        public abstract string GetCurrentReviewerType();
        public abstract IList<DesignDocEntryFieldType> GetDesignDocCreatePgEntryFieldsList();
        public abstract IList<DesignDocHeaderType> GetDesignDocDetailsHeadersList();
        public abstract IList<KeyValuePair<DesignDocEntryFieldType, string>> GetDesignDocEntryFieldKeyValuePairs();
        public abstract string GetDesignDocStatus();
        public abstract string GetHeaderValue(DesignDocHeaderType docHeader);
        public abstract void PopulateAllCreatePgEntryFields();
        public abstract void ScrollToFirstColumn();
        public abstract void ScrollToLastColumn();
        public abstract void SelectAgreeResolutionCode(int commentTabNumber = 1);
        public abstract void SelectAgreeResponseCode(int commentTabNumber = 1);
        public abstract void SelectCategory(int categoryNumber = 1);
        public abstract void SelectCommentType(int commentTypeTabNumber = 1);
        public abstract void SelectDDL_Category(int selectionIndex);
        public abstract void SelectDDL_ClosingStamp(int commentTabNumber = 1);
        public abstract void SelectDDL_CommentType(int selectionIndex);
        public abstract void SelectDDL_Discipline(int selectionIndex);
        public abstract void SelectDDL_Reviewer<T>(T selectionIndexOrReviewerName, bool useContainsFilter);
        public abstract void SelectDDL_ReviewType(int selectionIndex);
        public abstract void SelectDDL_VerificationCode(int selectionIndex = 1);
        public abstract void SelectDisagreeResolutionCode(int commentTabNumber = 1);
        public abstract void SelectDisagreeResponseCode(int commentTabNumber = 1);
        public abstract void SelectDiscipline(int disciplineNumber = 1);
        public abstract void SelectNoCommentReviewType(int commentTabNumber);
        public abstract void SelectOrganization(int disciplineNumber = 1);
        public abstract void SelectRegularCommentReviewType(int commentTabNumber);
        public abstract void SelectTab(TableTab tableTab);
        public abstract void SetDesignDocStatus<T>(T tableTabOrWorkflow);
        public abstract void SortTable_Ascending();
        public abstract void SortTable_Descending();
        public abstract void VerifyDesignDocDetailsHeader();
        public abstract bool VerifyDocumentNumberFieldErrorMsgIsDisplayed();
        public abstract void VerifyItemStatusIsClosed();
        public abstract bool VerifyTitleFieldErrorMsgIsDisplayed();
        public abstract bool VerifyUploadFileErrorMsgIsDisplayed();
        public abstract void WaitForActiveCommentTab();
        public abstract void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse();
        public abstract void Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse();
    }


    // Tenant specific implementation of DesignDocument Comment Review
    #region Implementation specific to Garnet

    public class DesignDocument_Garnet : DesignDocument
    {
        public DesignDocument_Garnet(IWebDriver driver) : base(driver)
        {
        }

        public override void SelectTab(TableTab tableTab)
        {
            PageAction.WaitForPageReady();
            string currentUser = PageAction.GetCurrentUser();
            string tabName = string.Empty;
            string tabPrefix = "";

            if (currentUser.Contains("IQF"))
            {
                tabPrefix = "IQF ";
            }
            else if (currentUser.Contains("DEV"))
            {
                tabPrefix = "DEV ";
            }
            else if (currentUser.Contains("DOT"))
            {
                tabPrefix = "DOT ";
            }

            tabName = $"{tabPrefix}{tableTab.GetString()}";
            GridHelper.ClickTab(tabName);

            SetDesignDocStatus(tableTab);
        }

        public override void SelectRegularCommentReviewType(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp_XPath(CommentFieldType.ReviewType, commentTabNumber), 2);

        public override void SelectNoCommentReviewType(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp_XPath(CommentFieldType.ReviewType, commentTabNumber), 3);
    }

    #endregion Implementation specific to Garnet

    #region Implementation specific to GLX

    public class DesignDocument_GLX : DesignDocument
    {
        public DesignDocument_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            Report.Step($"<<-- WORKFLOW ({tenantName}): EnterResolutionCommentAndResolutionCodeforDisagreeResponse -->>");

            ClickTab_Pending_Resolution();
            SortTable_Descending();
            GridHelper.ClickEnterBtnForRow();
            //Thread.Sleep(2000);
            EnterTextInCommentField(CommentFieldType.CommentResolutionInput);
            SelectDisagreeResolutionCode(); //
            ClickBtn_SaveOnly();
            //Thread.Sleep(2000);
            ClickBtn_BackToList();
        }

        public override void Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            ClickTab_Pending_Resolution();
            SortTable_Descending();
            GridHelper.ClickEnterBtnForRow();
            ClickBtn_SaveForward();
        }
    }

    #endregion Implementation specific to GLX

    #region Implementation specific to I15South

    public class DesignDocument_I15South : DesignDocument
    {
        public DesignDocument_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15South

    #region Implementation specific to I15Tech

    public class DesignDocument_I15Tech : DesignDocument
    {
        public DesignDocument_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15Tech

    #region Implementation specific to SGWay

    public class DesignDocument_SGWay : DesignDocument
    {
        public DesignDocument_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override By BackToListBtn_ByLocator => By.XPath("//button[text()='Back To List']");

        public override IList<DesignDocEntryFieldType> GetDesignDocCreatePgEntryFieldsList()
        {
            if (designDocCreatePgEntryFieldsList == null)
            {
                designDocCreatePgEntryFieldsList = new List<DesignDocEntryFieldType>
                {
                    DesignDocEntryFieldType.Title,
                    DesignDocEntryFieldType.DocumentNumber,
                    DesignDocEntryFieldType.DocumentDate,
                    DesignDocEntryFieldType.Segment,
                    DesignDocEntryFieldType.TransmittalNumber,
                    DesignDocEntryFieldType.TransmittalDate,
                    DesignDocEntryFieldType.MaxReviewDays_QAF_Chkbox,
                    DesignDocEntryFieldType.MaxReviewDays_DOT_Chkbox
                };
            }

            return designDocCreatePgEntryFieldsList;
        }

        public override IList<DesignDocHeaderType> GetDesignDocDetailsHeadersList()
        {
            if (designDocDetailsHeadersList == null)
            {
                designDocDetailsHeadersList = new List<DesignDocHeaderType>
                {
                    DesignDocHeaderType.Document_Number,
                    DesignDocHeaderType.Status,//
                    DesignDocHeaderType.Title,
                    DesignDocHeaderType.Segment,
                    DesignDocHeaderType.Document_Date,
                    DesignDocHeaderType.Review_Deadline,
                    DesignDocHeaderType.Transmittal_Date,
                    DesignDocHeaderType.Action,
                    DesignDocHeaderType.Remaining_Days,
                    DesignDocHeaderType.Transmittal_No
                };
            }

            return designDocDetailsHeadersList;
        }

        public override IList<CommentFieldType> RegularCommentFieldsList => new List<CommentFieldType>
        {
            CommentFieldType.ReviewType,
            CommentFieldType.CommentType,
            CommentFieldType.Reviewer,
            CommentFieldType.ReviewedDate,
            CommentFieldType.Category,
            CommentFieldType.Discipline,
            CommentFieldType.CommentInput,
            CommentFieldType.ContractReferenceInput,
            CommentFieldType.DrawingPageNumberInput
        };

        public override IList<CommentFieldType> NoCommentFieldsList => new List<CommentFieldType>
        {
            CommentFieldType.ReviewType,
            CommentFieldType.Reviewer,
            CommentFieldType.ReviewedDate
        };

        public override string GetHeaderValue(DesignDocHeaderType docHeader)
            => PageAction.GetText(By.XPath($"//label[contains(text(),'{docHeader.GetString()}')]/parent::div/following-sibling::div[1]"));

        public override void EnterRegularCommentAndDrawingPageNo()
        {
            SelectRegularCommentReviewType();
            SelectCommentType();
            SelectCategory();
            SelectDiscipline();
            EnterTextInCommentField(CommentFieldType.CommentInput);
            EnterTextInCommentField(CommentFieldType.ContractReferenceInput);
            EnterTextInCommentField(CommentFieldType.DrawingPageNumberInput);

            ClickBtn_SaveOnly();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            EnterTextInCommentField(CommentFieldType.CommentResponseInput);
            SelectDisagreeResponseCode();
            ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterTextInCommentField(CommentFieldType.CommentResponseInput, commentTabNumber);
            SelectDisagreeResponseCode(commentTabNumber);
            ClickBtn_SaveOnly();
        }

        public override void EnterResponseCommentAndAgreeResponseCode()
        {
            EnterTextInCommentField(CommentFieldType.CommentResponseInput);
            SelectAgreeResponseCode();
            ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterTextInCommentField(CommentFieldType.CommentResponseInput, commentTabNumber);
            SelectAgreeResponseCode(commentTabNumber);
            ClickBtn_SaveOnly();
        }

        public override void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            Report.Step($"<<-- WORKFLOW ({tenantName}): EnterResolutionCommentAndResolutionCodeforDisagreeResponse -->>");
            EnterTextInCommentField(CommentFieldType.CommentResolutionInput);
            SelectDisagreeResolutionCode(); //
            ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterTextInCommentField(CommentFieldType.CommentResolutionInput, commentTabNumber);
            SelectDisagreeResolutionCode(commentTabNumber);
            ClickBtn_SaveOnly();
        }

    }

    #endregion Implementation specific to SGWay

    #region Implementation specific to SH249

    public class DesignDocument_SH249 : DesignDocument
    {
        public DesignDocument_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override IList<DesignDocEntryFieldType> GetDesignDocCreatePgEntryFieldsList()
        {
            if (designDocCreatePgEntryFieldsList == null)
            {
                designDocCreatePgEntryFieldsList = new List<DesignDocEntryFieldType>
                {
                    DesignDocEntryFieldType.Title,
                    DesignDocEntryFieldType.DocumentNumber,
                    DesignDocEntryFieldType.DocumentDate,
                    DesignDocEntryFieldType.Segment,
                    DesignDocEntryFieldType.TransmittalNumber,
                    DesignDocEntryFieldType.TransmittalDate,
                    DesignDocEntryFieldType.MaxReviewDays_QAF_Chkbox,
                    DesignDocEntryFieldType.MaxReviewDays_DOT_Chkbox,
                    DesignDocEntryFieldType.MaxReviewDays_Other_Chkbox,
                    DesignDocEntryFieldType.MaxReviewDays_Other_Reviewer
                };
            }

            return designDocCreatePgEntryFieldsList;
        }

        public override IList<DesignDocHeaderType> GetDesignDocDetailsHeadersList()
        {
            if (designDocDetailsHeadersList == null)
            { 
                designDocDetailsHeadersList = new List<DesignDocHeaderType>
                {
                    DesignDocHeaderType.Date,
                    DesignDocHeaderType.Number,
                    DesignDocHeaderType.Title,
                    DesignDocHeaderType.Segment,
                    DesignDocHeaderType.Action,
                    DesignDocHeaderType.Status,
                    DesignDocHeaderType.Trans_Date,
                    DesignDocHeaderType.Trans_No
                };
            }

            return designDocDetailsHeadersList;
        }

        public override IList<CommentFieldType> RegularCommentFieldsList => new List<CommentFieldType>
        {
            CommentFieldType.ReviewType_InTable,
            CommentFieldType.DrawingPageNumberInput_InTable,
            CommentFieldType.ContractReferenceInput_InTable,
            CommentFieldType.CommentInput_InTable,
            CommentFieldType.Discipline_InTable,
            CommentFieldType.Category_InTable,
            CommentFieldType.CommentType_InTable,
            CommentFieldType.Reviewer_InTable
        };

        public override IList<CommentFieldType> NoCommentFieldsList => new List<CommentFieldType>
        {
            CommentFieldType.ReviewType_InTable
        };


        public override void EnterNoComment()
        {
            PageAction.WaitForPageReady();
            ClickBtn_AddComment();
            SelectNoCommentReviewType();
            ClickBtn_Update();
        }

        public override void EnterRegularCommentAndDrawingPageNo()
        {
            //login as commenting user (SG- IQFuser, DoTuser | SH249-- IQFUser | Garenet and GLX-- DOTUser)
            PageAction.WaitForPageReady();
            ClickBtn_AddComment();

            SelectRegularCommentReviewType();
            EnterTextInCommentField(CommentFieldType.DrawingPageNumberInput_InTable);
            EnterTextInCommentField(CommentFieldType.ContractReferenceInput_InTable);
            EnterTextInCommentField(CommentFieldType.CommentInput_InTable);
            SelectDiscipline();
            SelectCategory();
            SelectCommentType();
            SelectDDL_Reviewer(PageAction.GetCurrentUser(), true);

            ClickBtn_Update();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            //This will add response and resolution both together for 249 tenant
            PageAction.WaitForPageReady();
            ClickBtn_CommentsTblRow_Edit();
            EnterTextInCommentField(CommentFieldType.CommentResponseInput);
            EnterTextInCommentField(CommentFieldType.CommentResolutionInput);
            SelectDisagreeResolutionCode();
            ClickBtn_Update();
            ClickBtn_SaveForward();
        }

        public override void ClickBtn_SaveForward()
        {
            PageAction.ClickElement(Table_ForwardBtn_ByLocator);
            PageAction.WaitForPageReady();
        }

        public override void SelectRegularCommentReviewType(int selectionIndex = 3)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ReviewType_InTable, selectionIndex);

        public override void SelectNoCommentReviewType(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ReviewType_InTable, selectionIndex);

        public override void SelectCommentType(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.CommentType_InTable, selectionIndex);

        public override void SelectDiscipline(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.Discipline_InTable, selectionIndex);

        public override void SelectCategory(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.Category_InTable, selectionIndex);

        public override void SelectAgreeResolutionCode(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ResolutionStamp_InTable, selectionIndex);

        public override void SelectAgreeResponseCode(int selectionIndex = 2)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ResponseCode_InTable, selectionIndex);

        public override void SelectDisagreeResponseCode(int selectionIndex = 3)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ResponseCode_InTable, selectionIndex);

        public override void SelectDisagreeResolutionCode(int selectionIndex = 2)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ResolutionStamp_InTable, selectionIndex);

        public override void SelectDDL_ClosingStamp(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ClosingStamp_InTable, selectionIndex);

    }

    #endregion Implementation specific to SH249

    #region Implementation specific to LAX

    public class DesignDocument_LAX : DesignDocument
    {
        public DesignDocument_LAX(IWebDriver driver) : base(driver)
        {
        }

        public override IList<DesignDocEntryFieldType> GetDesignDocCreatePgEntryFieldsList()
        {
            if (designDocCreatePgEntryFieldsList == null)
            {
                designDocCreatePgEntryFieldsList = new List<DesignDocEntryFieldType>
                {
                    DesignDocEntryFieldType.Title,
                    DesignDocEntryFieldType.DocumentNumber,
                    DesignDocEntryFieldType.DocumentDate,
                    DesignDocEntryFieldType.Segment,
                    DesignDocEntryFieldType.TransmittalNumber,
                    DesignDocEntryFieldType.TransmittalDate,
                    DesignDocEntryFieldType.MaxReviewDays_DOT_Chkbox
                };
            }

            return designDocCreatePgEntryFieldsList;
        }

        public override IList<DesignDocHeaderType> GetDesignDocDetailsHeadersList()
        {
            if (designDocDetailsHeadersList == null)
            {
                designDocDetailsHeadersList = new List<DesignDocHeaderType>
                {
                    DesignDocHeaderType.Date,
                    DesignDocHeaderType.Number,
                    DesignDocHeaderType.Title,
                    DesignDocHeaderType.Segment,
                    DesignDocHeaderType.Action,
                    DesignDocHeaderType.Status,
                    DesignDocHeaderType.Trans_Date,
                    DesignDocHeaderType.Trans_No
                };
            }

            return designDocDetailsHeadersList;
        }

        public override IList<CommentFieldType> RegularCommentFieldsList => new List<CommentFieldType>
        {
            CommentFieldType.CommentType_InTable,
            CommentFieldType.ReviewerName_InTable,
            CommentFieldType.Org_InTable,
            CommentFieldType.ContractReferenceInput_InTable,
            CommentFieldType.CommentInput_InTable,
            CommentFieldType.DrawingPageNumberInput_InTable,
            CommentFieldType.Discipline_InTable
        };

        public override void EnterRegularCommentAndDrawingPageNo()
        {
            PageAction.WaitForPageReady();
            ClickBtn_AddComment();

            SelectCommentType();
            EnterTextInCommentField(CommentFieldType.ReviewerName_InTable);
            SelectOrganization();
            EnterTextInCommentField(CommentFieldType.ContractReferenceInput_InTable);
            EnterTextInCommentField(CommentFieldType.CommentInput_InTable);
            EnterTextInCommentField(CommentFieldType.DrawingPageNumberInput_InTable);
            SelectDiscipline();

            PageAction.WaitForPageReady();
        }

        public override void EnterResponseCommentAndAgreeResponseCode()
        {
            PageAction.WaitForPageReady();
            ClickBtn_CommentsTblRow_Edit();
            EnterTextInCommentField(CommentFieldType.CommentResponseInput_InTable);
            EnterTextInCommentField(CommentFieldType.CommentResolutionInput_InTable);
            SelectAgreeResolutionCode();
            ClickBtn_Update();
            ClickBtn_SaveForward();
        }

        public override void ClickBtn_SaveForward()
        {
            PageAction.JsClickElement(Table_ForwardBtn_ByLocator);
            PageAction.WaitForPageReady();
        }

        public override void SelectRegularCommentReviewType(int selectionIndex = 3)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ReviewType_InTable, selectionIndex);

        public override void SelectCommentType(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.DocType_InTable, selectionIndex);

        public override void SelectDiscipline(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.Discipline_InTable, selectionIndex);

        public override void SelectCategory(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.Category_InTable, selectionIndex);

        public override void SelectAgreeResolutionCode(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ResolutionStamp_InTable, selectionIndex);

        public override void SelectAgreeResponseCode(int selectionIndex = 2)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ResponseCode_InTable, selectionIndex);

        public override void SelectDisagreeResponseCode(int selectionIndex = 3)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ResponseCode_InTable, selectionIndex);

        public override void SelectDisagreeResolutionCode(int selectionIndex = 2)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ResolutionStamp_InTable, selectionIndex);

        public override void SelectDDL_ClosingStamp(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentFieldType.ClosingStamp_InTable, selectionIndex);

    }

    #endregion Implementation specific to LAX
}