using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.DesignDocument;
using static RKCIUIAutomation.Page.Workflows.DesignDocumentWF;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public class DesignDocument : DesignDocument_Impl, IDesignDocument
    {
        [ThreadStatic]
        internal static IList<KeyValuePair<CommentFieldType, string>> commentEntryFieldKVPairsList;

        [ThreadStatic]
        internal static IList<CommentFieldType> commentEntryFieldsList;

        [ThreadStatic]
        internal static IList<KeyValuePair<string, string>> commentEntryFieldStringKVPairsList;

        [ThreadStatic]
        internal static IList<KeyValuePair<DesignDocEntryFieldType, string>> createPgEntryFieldKVPairsList;

        [ThreadStatic]
        internal static IList<DesignDocEntryFieldType> designDocCreatePgEntryFieldsList;

        [ThreadStatic]
        internal static IList<DesignDocHeaderType> designDocDetailsHeadersList;

        [ThreadStatic]
        internal static string designDocNumber;

        [ThreadStatic]
        internal static string designDocTitle;

        [ThreadStatic]
        internal static string documentStatus;

        public DesignDocument()
        {
            createPgEntryFieldKVPairsList = GetDesignDocEntryFieldKVPairsList();
            commentEntryFieldKVPairsList = GetCommentEntryFieldKVPairsList();
            commentEntryFieldStringKVPairsList = GetCommentEntryFieldStringKVPairsList();
        }

        public DesignDocument(IWebDriver driver)
        {
            this.Driver = driver;
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
            [StringValue("VerifiedDateOffset", DATE)] VerifiedDate_InTable,
            [StringValue("VerifiedDate", DATE)] VerifiedDateHeader_InTable,
            [StringValue("VerificationNotes", TEXT)] VerificationNotes_InTable
        }

        public enum CommentRowBtn
        {
            Edit,
            Delete,
            Files,
            Details
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
            [StringValue("Transmittal №")] Transmittal_No,
            [StringValue("Review Deadline")] Review_Deadline,
            [StringValue("Remaining Days")] Remaining_Days
            //[StringValue("")] File_Name
        }

        public enum ReviewType
        {
            RegularComment,
            NoComment
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

        public override By BackToListBtn_ByLocator => By.XPath("//a[text()=' Back to List']");

        public override By Btn_Cancel_ByLocator => By.Id("btnCancelSave");

        public override By Btn_Forward_ByLocator => By.XPath("//button[@id='btnSaveForward']");

        public override By Btn_PDF_ByLocator => By.Id("PdfExportLink");

        //For tenants with Grid View for Comment Entries (LAX & SH249)
        public override By Btn_Refresh_ByLocator => By.XPath("//a[@aria-label='Refresh']");

        public override By Btn_ShowFileList_ByLocator => By.XPath("//button[contains(@class,'showFileList')]");

        public override By Btn_XLS_ByLocator => By.Id("CsvExportLink");

        public override By CancelBtnUploadPage_ByLocator => By.Id("btnCancel");

        public override IList<CommentFieldType> NoCommentFieldsList { get; set; }

        public override IList<CommentFieldType> RegularCommentFieldsList { get; set; }

        public override By SaveForwardBtn_ByLocator => By.XPath("//div[@class='k-content k-state-active']//button[contains(@id,'btnSaveForward_')]");

        public override By SaveForwardBtnUploadPage_ByLocator => By.Id("btnSaveForward");

        public override By SaveOnlyBtn_ByLocator => By.XPath("//div[@class='k-content k-state-active']//button[contains(@id,'btnSave_')]");

        public override By SaveOnlyBtnUploadPage_ByLocator => By.Id("btnSave");

        public override By Table_ForwardBtn_ByLocator => By.XPath("//button[@id='btnSaveForward']");

        //Page element By Locators
        public override By UploadNewDesignDoc_ByLocator => By.XPath("//a[text()='Upload New Design Document']");

        private void Click_TblRowBtn(CommentRowBtn rowButton, bool clickBtnForLatestRow = true, int rowID = 1)
        {
            By locator = By.XPath("//div[@class='k-grid-content']//tbody/tr");

            try
            {
                if (clickBtnForLatestRow)
                {
                    rowID = PageAction.GetElementsCount(locator);
                }

                PageAction.JsClickElement(GetTblBtnByLocator(rowButton, rowID));
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        private void Click_UniqueTblBtn(string btnClass)
        {
            PageAction.ClickElement(By.XPath($"//a[contains(@class, '{btnClass}')]"));
            PageAction.WaitForPageReady();
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

        private By GetTblBtnByLocator(CommentRowBtn rowButton, int rowID)
                    => By.XPath($"//tbody/tr[{rowID}]/td[1]/a[text()='{rowButton.ToString()}']");

        private string GetTblColumnIndex(CommentFieldType tableHeader)
                    => PageAction.GetAttribute(By.XPath($"//thead[@role='rowgroup']/tr/th[@data-field='{tableHeader.GetString()}']"), "data-index");

        private KeyValuePair<DesignDocEntryFieldType, string> PopulateAndGetDesignDocEntryFieldKVPair<T>(DesignDocEntryFieldType entryField, T indexOrText, bool useContainsOperator = false)
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
                        bool argHasValue = ((string)argValue).HasValue();
                        By inputLocator = GetInputFieldByLocator(entryField);

                        if (!argHasValue)
                        {
                            if (fieldType.Equals(DATE) || fieldType.Equals(FUTUREDATE))
                            {
                                if (fieldType.Equals(DATE))
                                {
                                    argValue = GetShortDate();
                                }
                                else
                                {
                                    argValue = GetFutureShortDate();
                                }
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
                                int elemMaxLength = 0;

                                try
                                {
                                    elemMaxLength = int.Parse(PageAction.GetAttribute(inputLocator, "maxlength"));
                                }
                                catch (Exception)
                                {
                                    log.Debug($"Element {inputLocator} does not have a maxlength attribute.");
                                }
                                finally
                                {
                                    if (elemMaxLength > 0 && argValueLength > elemMaxLength)
                                    {
                                        argValue = ((string)argValue).Substring(0, elemMaxLength);
                                    }
                                }
                            }
                        }

                        PageAction.EnterText(inputLocator, (string)argValue);
                        fieldValue = GetText(inputLocator);
                    }
                    else if (fieldType.Equals(DDL) || fieldType.Equals(MULTIDDL))
                    {
                        if (argType == typeof(string) && !((string)argValue).HasValue() || argType == typeof(int) && (int)argValue < 1)
                        {
                            argValue = 1;
                        }
                        else if (argType == typeof(string) && ((string)argValue).HasValue())
                        {
                            useContainsOperator = true;
                        }

                        bool fieldIsMultiDDL = fieldType.Equals(MULTIDDL);

                        PageAction.ExpandAndSelectFromDDList(entryField, argValue, useContainsOperator, fieldIsMultiDDL);

                        if (fieldType.Equals(DDL))
                        {
                            fieldValue = PageAction.GetTextFromDDL(entryField);
                        }
                        else
                        {
                            fieldValue = string.Join("::", PageAction.GetTextFromMultiSelectDDL(entryField).ToArray());
                        }
                    }
                    else if (fieldType.Equals(CHKBOX))
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
                else
                {
                    string logMsg = $"Argument value type is not supported {indexOrText} : {argType}";
                    log.Error(logMsg);
                    throw new ArgumentException(logMsg);
                }
            }
            catch (ArgumentException ex)
            {
                log.Error(ex.InnerException.Message);
                throw ex.InnerException;
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }

            return fieldValuePair = new KeyValuePair<DesignDocEntryFieldType, string>(entryField, fieldValue);
        }

        private void StoreDesignDocTitleAndNumber()
        {
            designDocTitle = GetVar("DsgnDocTtl");
            designDocNumber = GetVar("DsgnDocNumb");
            log.Debug($"#####Title: {designDocTitle}\nNumber: {designDocNumber}");
        }

        private bool VerifyRequiredFieldErrorMsg(string errorMsg)
                    => PageAction.ElementIsDisplayed(By.XPath($"//li[text()='{errorMsg}']"));

        internal string GetGridCommentResponseColumnValue(CommentFieldType commentField)
                    => GetText(GetCommentFieldValueXPath_ByLocator(commentField));

        internal void OnlyUpdateCommentFieldKVPairList(CommentFieldType commentField)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(commentField, "", onlyUpdateKVPair: true);

        internal void PopulateFieldAndUpdateCommentFieldKVPairList<T>(CommentFieldType commentField, T inputTextORselectionIndex, int commentTabNumber = 1, bool useContainsOperator = false, bool updateKVPair = true, bool onlyUpdateKVPair = false)
        {
            object argValue = null;
            bool isValidArg = true;
            string fieldValue = string.Empty;
            string fieldType = commentField.GetString(true);
            Type argType = inputTextORselectionIndex.GetType();
            bool commentFieldTypeIsInGrid = commentField.ToString().Contains("_InTable");

            try
            {
                if (argType == typeof(string))
                {
                    argValue = ConvertToType<string>(inputTextORselectionIndex);
                }
                else if (argType == typeof(int))
                {
                    argValue = ConvertToType<int>(inputTextORselectionIndex);
                }
                else
                {
                    isValidArg = false;
                }

                if (isValidArg)
                {
                    if (fieldType.Equals(TEXT))
                    {
                        fieldValue = EnterCommentResponse(commentField, commentTabNumber, updateKVPair);
                    }
                    else
                    {
                        if (fieldType.Equals(DATE))
                        {
                            EnterText(GetTextInputFieldByLocator(commentField), (string)argValue);
                        }
                        else if (fieldType.Equals(DDL))
                        {
                            if (!onlyUpdateKVPair)
                            {
                                if (commentFieldTypeIsInGrid)
                                {
                                    ExpandAndSelectFromDDList(commentField, argValue, useContainsOperator);
                                }
                                else
                                {
                                    ExpandAndSelectFromDDList(SetCommentStamp_XPath(commentField, commentTabNumber), (int)argValue, useContainsOperator);
                                }
                            }

                            if (commentFieldTypeIsInGrid)
                            {
                                if (updateKVPair)
                                {
                                    fieldValue = GetTextFromDDL(commentField);
                                }
                            }
                            else
                            {
                                if (updateKVPair)
                                {
                                    fieldValue = GetTextFromDDListInActiveTab(commentField);
                                }
                            }
                        }
                        else if (fieldType.Equals(AUTOPOPULATED)) //SG only
                        {
                            if (updateKVPair)
                            {
                                if (commentField.Equals(CommentFieldType.Reviewer))
                                {
                                    fieldValue = GetTextFromDDListInActiveTab(commentField);
                                }
                                else if (commentField.Equals(CommentFieldType.ReviewedDate))
                                {
                                    By locator = By.XPath($"{ActiveContentXPath}//input[contains(@id,'{commentField.GetString()}')]");
                                    fieldValue = GetText(locator);
                                }
                            }
                        }

                        //TEXT entry field types update KVPair List within the EnterCommentResponse() method using UpdateCommentFieldKVPairList() method
                        if (updateKVPair)
                        {
                            UpdateCommentFieldKVPairList(commentField, fieldValue);
                        }
                    }
                }
                else
                {
                    string logMsg = $"Argument value type is not supported. {inputTextORselectionIndex} : {argType}";
                    log.Error(logMsg);
                    throw new ArgumentException(logMsg);
                }
            }
            catch (ArgumentException ex)
            {
                log.Error(ex.InnerException.Message);
                throw ex.InnerException;
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }
        }

        internal string SetCommentStamp_XPath(CommentFieldType inputFieldEnum, int commentTabIndex)
                    => $"{inputFieldEnum.GetString()}{(commentTabIndex - 1).ToString()}_";

        internal void UpdateCommentFieldKVPairList(CommentFieldType commentField, string inputValue)
        {
            bool commentFieldTypeIsInGrid = commentField.ToString().Contains("_InTable");

            if (commentFieldTypeIsInGrid)
            {
                var fieldValuePair = new KeyValuePair<CommentFieldType, string>(commentField, inputValue);
                commentEntryFieldKVPairsList.Add(fieldValuePair);
            }
            else
            {
                string groupName = GetActiveCommentTabGroupName();
                var fieldValuePair = new KeyValuePair<string, string>($"{commentField}_{groupName}", inputValue);
                commentEntryFieldStringKVPairsList.Add(fieldValuePair);
            }
        }

        public override void ClickBtn_AddComment()
                    => Click_UniqueTblBtn("k-grid-add");

        public override void ClickBtn_BackToList()
        {
            PageAction.JsClickElement(BackToListBtn_ByLocator);
            PageAction.WaitForPageReady();
        }

        public override void ClickBtn_Cancel()
                    => Click_UniqueTblBtn("k-grid-cancel");

        public override void ClickBtn_CommentsTblRow_Delete(bool clickBtnForLatestRow = true, int rowID = 1)
                    => Click_TblRowBtn(CommentRowBtn.Delete, clickBtnForLatestRow, rowID);

        public override void ClickBtn_CommentsTblRow_Details(bool clickBtnForLatestRow = true, int rowID = 1)
                    => Click_TblRowBtn(CommentRowBtn.Details, clickBtnForLatestRow, rowID);

        public override void ClickBtn_CommentsTblRow_Edit(bool clickBtnForLatestRow = true, int rowID = 1)
                    => Click_TblRowBtn(CommentRowBtn.Edit, clickBtnForLatestRow, rowID);

        public override void ClickBtn_CommentsTblRow_Files(bool clickBtnForLatestRow = true, int rowID = 1)
                    => Click_TblRowBtn(CommentRowBtn.Files, clickBtnForLatestRow, rowID);

        public override void ClickBtn_SaveForward()
        {
            PageAction.JsClickElement(SaveForwardBtn_ByLocator);
            PageAction.WaitForPageReady();
        }

        public override void ClickBtn_SaveOnly()
        {
            PageAction.ClickElement(SaveOnlyBtn_ByLocator);
            PageAction.WaitForPageReady();
        }

        public override void ClickBtn_Update()
                    => Click_UniqueTblBtn("k-grid-update");

        public override void ClickCommentTabNumber(int commentTabNumber)
        {
            PageAction.ScrollCommentTabInToView();
            GridHelper.ClickCommentTab(commentTabNumber);
            WaitForActiveCommentContentLoadToComplete();
        }

        public override void ClickTab_Closed() => SelectTab(TableTab.Closed);

        public override void ClickTab_Comment() => SelectTab(TableTab.Comment);

        public override void ClickTab_Creating() => SelectTab(TableTab.Creating);

        public override void ClickTab_Pending_Closing() => SelectTab(TableTab.Pending_Closing);

        public override void ClickTab_Pending_Comment() => SelectTab(TableTab.Pending_Comment);

        public override void ClickTab_Pending_Resolution() => SelectTab(TableTab.Pending_Resolution);

        public override void ClickTab_Pending_Response() => SelectTab(TableTab.Pending_Response);

        public override void ClickTab_Requires_Closing() => SelectTab(TableTab.Requires_Closing);

        public override void ClickTab_Requires_Comment() => SelectTab(TableTab.Requires_Comment);

        public override void ClickTab_Requires_Resolution() => SelectTab(TableTab.Requires_Resolution);

        public override void ClickTab_Requires_Response() => SelectTab(TableTab.Requires_Response);

        public override void ClickTab_Response() => SelectTab(TableTab.Response);

        public override void ClickTab_Verification() => SelectTab(TableTab.Verification);

        public override IList<KeyValuePair<CommentFieldType, string>> GetCommentEntryFieldKVPairsList()
        {
            if (commentEntryFieldKVPairsList == null)
            {
                commentEntryFieldKVPairsList = new List<KeyValuePair<CommentFieldType, string>>();
            }

            return commentEntryFieldKVPairsList;
        }

        public override IList<KeyValuePair<string, string>> GetCommentEntryFieldStringKVPairsList()
        {
            if (commentEntryFieldStringKVPairsList == null)
            {
                commentEntryFieldStringKVPairsList = new List<KeyValuePair<string, string>>();
            }

            return commentEntryFieldStringKVPairsList;
        }

        public override IList<KeyValuePair<DesignDocEntryFieldType, string>> GetDesignDocEntryFieldKVPairsList()
        {
            if (createPgEntryFieldKVPairsList == null)
            {
                createPgEntryFieldKVPairsList = new List<KeyValuePair<DesignDocEntryFieldType, string>>();
            }

            return createPgEntryFieldKVPairsList;
        }

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

        public override string EnterCommentResponse(CommentFieldType commentField, int commentTabNumber = 1, bool updateKVPair = true)
        {
            bool commentFieldTypeIsInGrid = commentField.ToString().Contains("_InTable");

            string commentValue = "Comment 123";
            By entryFieldLocator = null;

            string groupName = string.Empty;

            try
            {
                if (commentFieldTypeIsInGrid)
                {
                    entryFieldLocator = By.Id($"{commentField.GetString()}");
                }
                else
                {
                    entryFieldLocator = By.Id($"{commentField.GetString()}{commentTabNumber - 1}_");
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

                    if (!commentFieldTypeIsInGrid)
                    {
                        groupName = $"{GetActiveCommentTabGroupName()}_";
                    }

                    commentValue = $"{groupName}{commentValue}";
                    PageAction.EnterText(entryFieldLocator, commentValue);
                }

                if (updateKVPair)
                {
                    string commentFieldName = commentField.ToString();

                    if (!commentFieldTypeIsInGrid)
                    {
                        commentFieldName = $"{commentFieldName} under Tab Comment {commentTabNumber}";
                    }

                    UpdateCommentFieldKVPairList(commentField, commentValue);
                    Report.Step($"Entered '{commentValue}' in {commentFieldName} field : {entryFieldLocator}");
                }
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }

            return commentValue;
        }

        public override void EnterDesignDocTitleAndNumber()
        {
            StoreDesignDocTitleAndNumber();
            PageAction.EnterText(GetTextInputFieldByLocator(DesignDocEntryFieldType.Title), designDocTitle);
            PageAction.EnterText(GetTextInputFieldByLocator(DesignDocEntryFieldType.DocumentNumber), designDocNumber);
        }

        public override void EnterNoComment()
        {
            //login as commenting user (SG- IQFuser, DoTuser | SH249-- IQFUser | Garenet and GLX-- DOTUser)
            SelectNoCommentReviewType();
            OnlyUpdateCommentFieldKVPairList(CommentFieldType.Reviewer);
            OnlyUpdateCommentFieldKVPairList(CommentFieldType.ReviewedDate);
            ClickBtn_SaveOnly();
        }

        public override void EnterRegularCommentAndDrawingPageNo()
        {
            //login as commenting user (SG- IQFuser, DoTuser | SH249-- IQFUser | Garenet and GLX-- DOTUser)

            SelectRegularCommentReviewType();
            EnterCommentResponse(CommentFieldType.CommentInput);
            EnterCommentResponse(CommentFieldType.DrawingPageNumberInput);
            ClickBtn_SaveOnly();
        }

        public override void EnterResponseCommentAndAgreeResponseCode()
        {
            // Login as user to make response comment (All tenants - DevUser)
            EnterCommentResponse(CommentFieldType.CommentResponseInput);
            SelectAgreeResponseCode(); //Disagree then different workflow
            ClickBtn_SaveOnly();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            // Login as user to make response comment (All tenants - DevUser)
            EnterCommentResponse(CommentFieldType.CommentResponseInput);
            SelectDisagreeResponseCode(); //agree then different workflow
            ClickBtn_SaveOnly();
        }

        public override void EnterVerifiedDate(string shortDate = "01/01/2019")
        {
            shortDate = shortDate.Equals("01/01/2019")
                ? GetShortDate()
                : shortDate;

            PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.VerificationCode_InTable, shortDate);
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

        public override string GetActiveCommentTabGroupName()
        {
            string groupName = string.Empty;
            By activeCommentTabGroupName = By.XPath($"{ActiveContentXPath}//strong[text()='Group:']/parent::span");

            try
            {
                WaitForActiveCommentContentLoadToComplete();
                groupName = GetText(activeCommentTabGroupName);

                if (groupName.Contains("Iqf") || groupName.Contains("IQF"))
                {
                    groupName = "IQF";
                }
                else if (groupName.Contains("Dot") || groupName.Contains("DOT"))
                {
                    groupName = "DOT";
                }
            }
            catch (Exception)
            {
                throw;
            }

            return groupName;
        }

        /// <summary>
        /// Returns a list of enums, based on ReviewType parameter, in order to provide an array of element IDs of column headers in the 'Review Complete' workflow grid, which is then used to create a list of actual values shown in the grid to compare against a list of expected values generated in previous test steps.
        /// <para/> Element IDs for column headers are usually same as input field element IDs, but not always (i.e. VerifiedDate_InTable (VerifiedDateOffset) vs VerifiedDateHeader_InTable (VerifiedDate) - for LAX)
        /// </summary>
        /// <param name="reviewType"></param>
        /// <returns></returns>
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

        //LAX & SH249 - Grid Comment Entry role data
        public override By GetCommentFieldValueXPath_ByLocator(CommentFieldType commentField)
        {
            int columnIndex = int.Parse(GetAttribute(By.XPath($"//th[@data-field='{commentField.GetString()}']"), "data-index"));
            By locator = By.XPath($"//td[@style='vertical-align: top;'][{columnIndex - 2}]");
            return locator;
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

        public override IList<DesignDocEntryFieldType> GetDesignDocCreatePgEntryFieldsList()
                    => designDocCreatePgEntryFieldsList;

        public override IList<DesignDocHeaderType> GetDesignDocDetailsHeadersList()
                    => designDocDetailsHeadersList;

        public override string GetDesignDocStatus()
                    => documentStatus;

        public override string GetHeaderValue(DesignDocHeaderType docHeader)
                    => PageAction.GetText(By.XPath($"//label[contains(text(),'{docHeader.GetString()}')]/following-sibling::div[1]"));

        /// <summary>
        /// Populates entry fields accordingly to 'designDocCreatePgEntryFields' list ([ThreadStatic] field variable) and stores EntryField enum and value in 'createPgEntryFieldKeyValuePairs' ([ThreadStatic] field variable).
        /// </summary>
        public override void PopulateAllCreatePgEntryFields()
        {
            foreach (DesignDocEntryFieldType entryField in designDocCreatePgEntryFieldsList)
            {
                KeyValuePair<DesignDocEntryFieldType, string> kvPair = new KeyValuePair<DesignDocEntryFieldType, string>();
                kvPair = PopulateAndGetDesignDocEntryFieldKVPair(entryField, "");
                createPgEntryFieldKVPairsList.Add(kvPair);
            }
        }

        public override void ScrollToFirstColumn()
                    => PageAction.ScrollToElement(By.XPath("//tbody/tr/td[@style='vertical-align: top;'][1]"));

        public override void ScrollToLastColumn()
                    => PageAction.ScrollToElement(By.XPath("//tbody/tr/td[@style='vertical-align: top;'][last()]"));

        public override void SelectAgreeResolutionCode(int commentTabNumber = 1)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ResolutionStamp, 1, commentTabNumber);

        public override void SelectAgreeResponseCode(int commentTabNumber = 1)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ResponseCode, 2, commentTabNumber);

        public override void SelectCategory(int categoryNumber = 1)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.Category, categoryNumber);

        public override void SelectCommentType(int commentTabNumber = 1)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.CommentType, 1, commentTabNumber);

        public override void SelectDDL_Category(int selectionIndex)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.Category_InTable, selectionIndex);

        public override void SelectDDL_ClosingStamp(int commentTabNumber = 1)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ClosingStamp, 1, commentTabNumber);

        public override void SelectDDL_CommentType(int selectionIndex)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.CommentType_InTable, selectionIndex);

        public override void SelectDDL_Discipline(int selectionIndex)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.Discipline_InTable, selectionIndex);

        public override void SelectDDL_Reviewer<T>(T selectionIndexOrReviewerName, bool useContainsOperator)
            => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.Reviewer_InTable, selectionIndexOrReviewerName, useContainsOperator: useContainsOperator);

        public override void SelectDDL_ReviewType(int selectionIndex)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ReviewType_InTable, selectionIndex);

        public override void SelectDDL_VerificationCode(int selectionIndex = 1)
        {
            CommentFieldType verificationCodeType = CommentFieldType.VerificationCode_InTable;
            PopulateFieldAndUpdateCommentFieldKVPairList(verificationCodeType, selectionIndex, 1, false, false);
            string verificationCodeValue = GetGridCommentResponseColumnValue(verificationCodeType);
            UpdateCommentFieldKVPairList(verificationCodeType, verificationCodeValue);
        }

        public override void SelectDisagreeResolutionCode(int commentTabNumber = 1)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ResolutionStamp, 2, commentTabNumber);

        public override void SelectDisagreeResponseCode(int commentTabNumber = 1)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ResponseCode, 3, commentTabNumber);

        public override void SelectDiscipline(int disciplineNumber = 1)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.Discipline, disciplineNumber);

        public override void SelectNoCommentReviewType(int commentTabNumber = 1)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ReviewType, 1, commentTabNumber);

        public override void SelectOrganization(int selectionIndex = 1)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.Org_InTable, selectionIndex);

        public override void SelectRegularCommentReviewType(int commentTabNumber = 1)
                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ReviewType, 3, commentTabNumber);

        public override void SelectTab(TableTab tableTab)
        {
            PageAction.WaitForPageReady();
            GridHelper.ClickTab(tableTab);
            PageAction.WaitForPageReady();
            SetDesignDocStatus(tableTab);
        }

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

        public override void SortTable_Ascending() => GridHelper.SortColumnAscending(ColumnName.Action);

        public override void SortTable_Descending() => GridHelper.SortColumnDescending(ColumnName.Action);

        public override void VerifyCommentFieldValues(ReviewType reviewType)
        {
            string groupName = string.Empty;
            string logHeaderTitle = string.Empty;
            string expectedValue = string.Empty;
            IList<string> expectedValueList = null;
            IList<string> actualValueList = null;

            try
            {
                commentEntryFieldsList = GetCommentEntryFieldsList(reviewType);
                bool commentFieldIsInGrid = commentEntryFieldsList[0].ToString().Contains("_InTable");

                if (!commentFieldIsInGrid)
                {
                    groupName = GetActiveCommentTabGroupName();
                    logHeaderTitle = $" for Group: {groupName}";
                }

                expectedValueList = new List<string>();
                actualValueList = new List<string>();

                foreach (CommentFieldType commentField in commentEntryFieldsList)
                {
                    string actualValue = GetGridCommentResponseColumnValue(commentField);

                    if (actualValue.Contains(":"))
                    {
                        var splitActualValue = Regex.Split(actualValue, ":");
                        actualValue = splitActualValue[1].Trim();
                    }
                    actualValueList.Add(actualValue);

                    if (commentFieldIsInGrid)
                    {
                        expectedValue = (from kvp in commentEntryFieldKVPairsList where kvp.Key == commentField select kvp.Value).FirstOrDefault();
                    }
                    else
                    {
                        expectedValue = (from kvp in commentEntryFieldStringKVPairsList where kvp.Key == $"{commentField}_{groupName}" select kvp.Value).FirstOrDefault();                       
                    }

                    expectedValueList.Add($"{commentField}::{expectedValue}");
                }

                TestUtility.AddAssertionToList(PageAction.VerifyExpectedList(actualValueList, expectedValueList, $"Review Type : {reviewType}{logHeaderTitle}"), "VerifyCommentFieldValues");
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }
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
                        expectedHeaderValue = (from kvp in createPgEntryFieldKVPairsList where kvp.Key == entryFieldType select kvp.Value).FirstOrDefault();

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

                                string maxReviewDays = (from kvp in createPgEntryFieldKVPairsList where kvp.Key == entryFieldType select kvp.Value).FirstOrDefault();

                                if (header.Equals(DesignDocHeaderType.Review_Deadline))
                                {
                                    string documentDate = (from kvp in createPgEntryFieldKVPairsList where kvp.Key == DesignDocEntryFieldType.DocumentDate select kvp.Value).FirstOrDefault();
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

        public override bool VerifyDocumentNumberFieldErrorMsgIsDisplayed()
            => VerifyRequiredFieldErrorMsg("Submittal Number is required.");

        public override void VerifyItemStatusIsClosed(ReviewType reviewType)
        {
            ClickTab_Closed();
            TestUtility.AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(ColumnName.Number, designDocNumber), $"VerifyItemStatusIsClosed");
            GridHelper.ClickEnterBtnForRow();
            VerifyCommentFieldValues(reviewType);
        }

        public override bool VerifyTitleFieldErrorMsgIsDisplayed()
            => VerifyRequiredFieldErrorMsg("Submittal Title is required.");

        public override bool VerifyUploadFileErrorMsgIsDisplayed()
            => VerifyRequiredFieldErrorMsg("At least one file must be added.");

        public override void WaitForActiveCommentContentLoadToComplete()
        {
            string errMsg(int interval) => $"Comment tab did not load after {interval * 10} seconds.";
            By activeCommentTabContentLoadCompleteXPath = By.XPath("//ul[@class='k-reset k-tabstrip-items']/li[contains(@class,'k-state-active')]/span[contains(@class,'k-complete')]");
            bool activeContentNotDisplayed = true;

            for (int i = 0; i < 30; i++)
            {
                do
                {
                    if (i == 30)
                    {
                        Report.Error(errMsg(i));
                        throw new ElementNotVisibleException(errMsg(i));
                    }
                    else
                    {
                        try
                        {
                            if (PageAction.ElementIsDisplayed(activeCommentTabContentLoadCompleteXPath))
                            {
                                activeContentNotDisplayed = false;
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            if (i > 5)
                            {
                                log.Debug(errMsg(i));
                            }

                            continue;
                        }
                    }
                }
                while (activeContentNotDisplayed);
            }
        }

        public override void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            Report.Step($"<<-- WORKFLOW ({tenantName}): EnterResolutionCommentAndResolutionCodeforDisagreeResponse -->>");
            ClickTab_Requires_Resolution();
            SortTable_Descending();
            GridHelper.ClickEnterBtnForRow();

            // Login as user to make resolution comment (All tenants - DevAdmin)
            EnterCommentResponse(CommentFieldType.CommentResolutionInput);
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
    }

    // DesignDocument Implementation class
    public abstract class DesignDocument_Impl : PageBase, IDesignDocument
    {
        public abstract By BackToListBtn_ByLocator { get; }
        public abstract By Btn_Cancel_ByLocator { get; }
        public abstract By Btn_Forward_ByLocator { get; }
        public abstract By Btn_PDF_ByLocator { get; }
        public abstract By Btn_Refresh_ByLocator { get; }
        public abstract By Btn_ShowFileList_ByLocator { get; }
        public abstract By Btn_XLS_ByLocator { get; }
        public abstract By CancelBtnUploadPage_ByLocator { get; }
        public abstract IList<CommentFieldType> NoCommentFieldsList { get; set; }
        public abstract IList<CommentFieldType> RegularCommentFieldsList { get; set; }
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

        public abstract IList<KeyValuePair<CommentFieldType, string>> GetCommentEntryFieldKVPairsList();

        public abstract IList<KeyValuePair<string, string>> GetCommentEntryFieldStringKVPairsList();

        public abstract IList<KeyValuePair<DesignDocEntryFieldType, string>> GetDesignDocEntryFieldKVPairsList();

        public abstract void CreateDocument();

        public abstract string EnterCommentResponse(CommentFieldType commentType, int commentTabNumber = 1, bool updateKVPair = true);

        public abstract void EnterDesignDocTitleAndNumber();

        public abstract void EnterNoComment();

        public abstract void EnterRegularCommentAndDrawingPageNo();

        public abstract void EnterResponseCommentAndAgreeResponseCode();

        public abstract void EnterResponseCommentAndDisagreeResponseCode();
        public abstract void EnterVerifiedDate(string shortDate = "01/01/2019");

        public abstract void FilterDocNumber(string filterByValue = "");
        public abstract string GetActiveCommentTabGroupName();

        /// <summary>
        /// Returns a list of enums, based on ReviewType parameter, in order to provide an array of element IDs of column headers in the 'Review Complete' workflow grid, which is then used to create a list of actual values shown in the grid to compare against a list of expected values generated in previous test steps.
        /// <para/> Element IDs for column headers are usually same as input field element IDs, but not always (i.e. VerifiedDate_InTable (VerifiedDateOffset) vs VerifiedDateHeader_InTable (VerifiedDate) - for LAX)
        /// </summary>
        /// <param name="reviewType"></param>
        /// <returns></returns>
        public abstract IList<CommentFieldType> GetCommentEntryFieldsList(ReviewType reviewType);

        public abstract By GetCommentFieldValueXPath_ByLocator(CommentFieldType commentField);

        public abstract string GetCurrentReviewerType();
        public abstract IList<DesignDocEntryFieldType> GetDesignDocCreatePgEntryFieldsList();

        public abstract IList<DesignDocHeaderType> GetDesignDocDetailsHeadersList();
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

        public abstract void SelectDDL_Reviewer<T>(T selectionIndexOrReviewerName, bool useContainsOperator);

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

        public abstract void VerifyCommentFieldValues(ReviewType reviewType);

        public abstract void VerifyDesignDocDetailsHeader();
        public abstract bool VerifyDocumentNumberFieldErrorMsgIsDisplayed();

        public abstract void VerifyItemStatusIsClosed(ReviewType reviewType);

        public abstract bool VerifyTitleFieldErrorMsgIsDisplayed();

        public abstract bool VerifyUploadFileErrorMsgIsDisplayed();

        public abstract void WaitForActiveCommentContentLoadToComplete();

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

        public override void SelectNoCommentReviewType(int commentTabNumber = 1)
            => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ReviewType, 3, commentTabNumber);

        public override void SelectRegularCommentReviewType(int commentTabNumber = 1)
            => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ReviewType, 2, commentTabNumber);

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
            EnterCommentResponse(CommentFieldType.CommentResolutionInput);
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

        public override IList<CommentFieldType> NoCommentFieldsList => new List<CommentFieldType>
        {
            CommentFieldType.ReviewType,
            CommentFieldType.Reviewer,
            CommentFieldType.ReviewedDate,
            CommentFieldType.CommentResponseInput,
            CommentFieldType.ResponseCode,
            CommentFieldType.CommentClosingInput,
            CommentFieldType.ClosingStamp
        };

        public override IList<CommentFieldType> RegularCommentFieldsList => new List<CommentFieldType>
        {
            CommentFieldType.ReviewType,
            CommentFieldType.Reviewer,
            CommentFieldType.ReviewedDate,
            CommentFieldType.CommentType,
            CommentFieldType.Category,
            CommentFieldType.Discipline,
            CommentFieldType.CommentInput,
            CommentFieldType.ContractReferenceInput,
            CommentFieldType.DrawingPageNumberInput,
            CommentFieldType.CommentResponseInput,
            CommentFieldType.ResponseCode,
            CommentFieldType.CommentResolutionInput,
            CommentFieldType.ResolutionStamp,
            CommentFieldType.CommentClosingInput,
            CommentFieldType.ClosingStamp
        };

        public override void EnterRegularCommentAndDrawingPageNo()
        {
            SelectRegularCommentReviewType();
            OnlyUpdateCommentFieldKVPairList(CommentFieldType.Reviewer);
            OnlyUpdateCommentFieldKVPairList(CommentFieldType.ReviewedDate);
            SelectCommentType();
            SelectCategory();
            SelectDiscipline();
            EnterCommentResponse(CommentFieldType.CommentInput);
            EnterCommentResponse(CommentFieldType.ContractReferenceInput);
            EnterCommentResponse(CommentFieldType.DrawingPageNumberInput);

            ClickBtn_SaveOnly();
        }

        public override void EnterResponseCommentAndAgreeResponseCode()
        {
            EnterCommentResponse(CommentFieldType.CommentResponseInput);
            SelectAgreeResponseCode();
            ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterCommentResponse(CommentFieldType.CommentResponseInput, commentTabNumber);
            SelectAgreeResponseCode(commentTabNumber);
            ClickBtn_SaveOnly();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            EnterCommentResponse(CommentFieldType.CommentResponseInput);
            SelectDisagreeResponseCode();
            ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterCommentResponse(CommentFieldType.CommentResponseInput, commentTabNumber);
            SelectDisagreeResponseCode(commentTabNumber);
            ClickBtn_SaveOnly();
        }

        public override By GetCommentFieldValueXPath_ByLocator(CommentFieldType commentField)
            => By.XPath($"{ActiveContentXPath}//input[contains(@id,'{commentField.GetString()}')]/parent::div");

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

        public override string GetHeaderValue(DesignDocHeaderType docHeader)
            => PageAction.GetText(By.XPath($"//label[contains(text(),'{docHeader.GetString()}')]/parent::div/following-sibling::div[1]"));

        public override void VerifyItemStatusIsClosed(ReviewType reviewType)
        {
            ClickTab_Closed();
            TestUtility.AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(ColumnName.Number, designDocNumber), $"VerifyItemStatusIsClosed");
            GridHelper.ClickEnterBtnForRow();
            VerifyCommentFieldValues(reviewType);

            int totalCommentTabsCount = GetElementsCount(By.XPath("//ul[@class='k-reset k-tabstrip-items']/li"));

            if (totalCommentTabsCount > 1)
            {
                for (int i = 1; i < totalCommentTabsCount; i++)
                {
                    int tabNumber = i + 1;
                    ClickCommentTabNumber(tabNumber);
                    VerifyCommentFieldValues(reviewType);
                }
            }

            PageAction.ScrollCommentTabInToView();
        }

        public override void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            Report.Step($"<<-- WORKFLOW ({tenantName}): EnterResolutionCommentAndResolutionCodeforDisagreeResponse -->>");
            EnterCommentResponse(CommentFieldType.CommentResolutionInput);
            SelectDisagreeResolutionCode(); //
            ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterCommentResponse(CommentFieldType.CommentResolutionInput, commentTabNumber);
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

        public override IList<CommentFieldType> NoCommentFieldsList => new List<CommentFieldType>
        {
            CommentFieldType.ReviewType_InTable,
            CommentFieldType.Reviewer_InTable,
            CommentFieldType.CommentClosingInput_InTable,
            CommentFieldType.ClosingStamp_InTable
        };

        public override IList<CommentFieldType> RegularCommentFieldsList => new List<CommentFieldType>
        {
            CommentFieldType.ReviewType_InTable,
            CommentFieldType.Reviewer_InTable,
            CommentFieldType.CommentType_InTable,
            CommentFieldType.Category_InTable,
            CommentFieldType.Discipline_InTable,
            CommentFieldType.CommentInput_InTable,
            CommentFieldType.ContractReferenceInput_InTable,
            CommentFieldType.DrawingPageNumberInput_InTable,
            CommentFieldType.CommentResponseInput_InTable,
            CommentFieldType.CommentResolutionInput_InTable,
            CommentFieldType.ResolutionStamp_InTable,
            CommentFieldType.CommentClosingInput_InTable,
            CommentFieldType.ClosingStamp_InTable
        };

        public override void ClickBtn_SaveForward()
        {
            PageAction.ClickElement(Table_ForwardBtn_ByLocator);
            PageAction.WaitForPageReady();
        }

        public override void EnterNoComment()
        {
            PageAction.WaitForPageReady();
            ClickBtn_AddComment();
            SelectDDL_Reviewer(PageAction.GetCurrentUser(true), true);
            SelectNoCommentReviewType();
            ClickBtn_Update();
        }

        public override void EnterRegularCommentAndDrawingPageNo()
        {
            //login as commenting user (SG- IQFuser, DoTuser | SH249-- IQFUser | Garenet and GLX-- DOTUser)
            PageAction.WaitForPageReady();
            ClickBtn_AddComment();

            SelectRegularCommentReviewType();
            EnterCommentResponse(CommentFieldType.DrawingPageNumberInput_InTable);
            EnterCommentResponse(CommentFieldType.ContractReferenceInput_InTable);
            EnterCommentResponse(CommentFieldType.CommentInput_InTable);
            SelectDiscipline();
            SelectCategory();
            SelectCommentType();
            SelectDDL_Reviewer(PageAction.GetCurrentUser(true), true);

            ClickBtn_Update();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            //This will add response and resolution both together for 249 tenant
            PageAction.WaitForPageReady();
            ClickBtn_CommentsTblRow_Edit();
            EnterCommentResponse(CommentFieldType.CommentResponseInput);
            EnterCommentResponse(CommentFieldType.CommentResolutionInput);
            SelectDisagreeResolutionCode();
            ClickBtn_Update();
            ClickBtn_SaveForward();
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

        public override void SelectAgreeResolutionCode(int selectionIndex = 1)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ResolutionStamp_InTable, selectionIndex);

        public override void SelectAgreeResponseCode(int selectionIndex = 2)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ResponseCode_InTable, selectionIndex);

        public override void SelectCategory(int selectionIndex = 1)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.Category_InTable, selectionIndex);

        public override void SelectCommentType(int selectionIndex = 1)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.CommentType_InTable, selectionIndex);

        public override void SelectDDL_ClosingStamp(int selectionIndex = 1)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ClosingStamp_InTable, selectionIndex);

        public override void SelectDisagreeResolutionCode(int selectionIndex = 2)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ResolutionStamp_InTable, selectionIndex);

        public override void SelectDisagreeResponseCode(int selectionIndex = 3)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ResponseCode_InTable, selectionIndex);

        public override void SelectDiscipline(int selectionIndex = 1)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.Discipline_InTable, selectionIndex);

        public override void SelectNoCommentReviewType(int selectionIndex = 1)
            => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ReviewType_InTable, selectionIndex);

        public override void SelectRegularCommentReviewType(int selectionIndex = 3)
                                                                                    => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ReviewType_InTable, selectionIndex);
    }

    #endregion Implementation specific to SH249

    #region Implementation specific to LAX

    public class DesignDocument_LAX : DesignDocument
    {
        private readonly CommentFieldType responseCodeType = CommentFieldType.ResponseCode_InTable;

        public DesignDocument_LAX(IWebDriver driver) : base(driver)
        {
        }

        public override IList<CommentFieldType> RegularCommentFieldsList => new List<CommentFieldType>
        {
            CommentFieldType.DocType_InTable,
            CommentFieldType.DrawingPageNumberInput_InTable,
            CommentFieldType.Discipline_InTable,
            CommentFieldType.CommentInput_InTable,
            CommentFieldType.ContractReferenceInput_InTable,
            CommentFieldType.Org_InTable,
            CommentFieldType.ReviewerName_InTable,
            CommentFieldType.ResponseCode_InTable,
            CommentFieldType.CommentResponseInput_InTable,
            CommentFieldType.VerificationCode_InTable,
            CommentFieldType.VerifiedBy_InTable,
            CommentFieldType.VerifiedDateHeader_InTable,
            CommentFieldType.VerificationNotes_InTable
        };

        public override void ClickBtn_SaveForward()
            => PageAction.JsClickElement(Table_ForwardBtn_ByLocator);

        public override void EnterRegularCommentAndDrawingPageNo()
        {
            PageAction.WaitForPageReady();
            ClickBtn_AddComment();

            SelectCommentType();
            EnterCommentResponse(CommentFieldType.DrawingPageNumberInput_InTable);
            SelectDiscipline();
            EnterCommentResponse(CommentFieldType.CommentInput_InTable);
            EnterCommentResponse(CommentFieldType.ContractReferenceInput_InTable);
            SelectOrganization();
            EnterCommentResponse(CommentFieldType.ReviewerName_InTable);
            PageAction.ClickInMainBodyAwayFromField();
            PageAction.WaitForPageReady();
        }

        public override void EnterResponseCommentAndAgreeResponseCode()
        {
            PageAction.WaitForPageReady();
            ClickBtn_CommentsTblRow_Edit();
            EnterCommentResponse(CommentFieldType.CommentResponseInput_InTable);
            EnterCommentResponse(CommentFieldType.CommentResolutionInput_InTable);
            SelectAgreeResolutionCode();
            ClickBtn_Update();
            ClickBtn_SaveForward();
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
        public override void SelectAgreeResolutionCode(int selectionIndex = 1)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ResolutionStamp_InTable, selectionIndex);

        //Response Code dropdown list in LAX is the only required field in Response workflow,
        //selecting response code triggers auto-save of the grid and blocks from reading text value of the field, causing an NoSuchElementException.
        public override void SelectAgreeResponseCode(int selectionIndex = 2)
        {
            PopulateFieldAndUpdateCommentFieldKVPairList(responseCodeType, selectionIndex, 1, false, false);
            string responseCodeValue = GetGridCommentResponseColumnValue(responseCodeType);
            UpdateCommentFieldKVPairList(responseCodeType, responseCodeValue);
        }

        public override void SelectCategory(int selectionIndex = 1)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.Category_InTable, selectionIndex);

        public override void SelectCommentType(int selectionIndex = 1)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.DocType_InTable, selectionIndex);

        public override void SelectDDL_ClosingStamp(int selectionIndex = 1)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ClosingStamp_InTable, selectionIndex);

        public override void SelectDisagreeResolutionCode(int selectionIndex = 2)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ResolutionStamp_InTable, selectionIndex);

        //Response Code dropdown list in LAX is the only required field in Response workflow,
        //selecting response code triggers auto-save of the grid and blocks from reading text value of the field, causing an NoSuchElementException.
        public override void SelectDisagreeResponseCode(int selectionIndex = 3)
        {
            PopulateFieldAndUpdateCommentFieldKVPairList(responseCodeType, selectionIndex, 1, false, false);
            string responseCodeValue = GetGridCommentResponseColumnValue(responseCodeType);
            UpdateCommentFieldKVPairList(responseCodeType, responseCodeValue);
        }

        public override void SelectDiscipline(int selectionIndex = 1)
        => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.Discipline_InTable, selectionIndex);

        public override void SelectRegularCommentReviewType(int selectionIndex = 3)
                                                                            => PopulateFieldAndUpdateCommentFieldKVPairList(CommentFieldType.ClosingStamp_InTable, selectionIndex);
    }

    #endregion Implementation specific to LAX
}