using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using System;
using System.Threading;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.DesignDocument;
using RKCIUIAutomation.Test;
using System.Collections.Generic;
using System.Linq;
using static RKCIUIAutomation.Page.Workflows.DesignDocumentWF;
using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    #region DesignDocument Generic class

    public class DesignDocument : DesignDocument_Impl
    {
        public DesignDocument()
        {            
            createPgEntryFieldKeyValuePairs = GetDesignDocEntryFieldKeyValuePairs();           
            commentEntryFieldKeyValuePairs = GetCommentEntryFieldKeyValuePairs();
        }

        public DesignDocument(IWebDriver driver)
        {
            this.Driver = driver;
            commentEntryFields = GetCommentEntryFieldsList();
            designDocDetailsHeaders = GetDesignDocDetailsHeadersList();
            designDocCreatePgEntryFields = GetDesignDocCreatePgEntryFieldsList();
        }

        public override void ScrollToLastColumn()
            => PageAction.ScrollToElement(By.XPath("//tbody/tr/td[@style='vertical-align: top;'][last()]"));

        public override void ScrollToFirstColumn()
            => PageAction.ScrollToElement(By.XPath("//tbody/tr/td[@style='vertical-align: top;'][1]"));

        public enum CommentType
        {
            RegularComment,
            NoComment
        }

        public enum DesignDocEntryField
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

        public enum DesignDocHeader
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

        public enum CommentEntryField
        {
            [StringValue("Comment_ReviewTypeId_")] ReviewType,
            [StringValue("Comment_ResponseCodeId_")] ResponseCode,
            [StringValue("Comment_ResolutionStampId_")] ResolutionStamp,
            [StringValue("Comment_ClosingStampId_")] ClosingStamp,
            [StringValue("Comment_CommentTypeId_")] CommentType,
            [StringValue("Comment_CategoryId_")] Category,
            [StringValue("Comment_DisciplineId_")] Discipline,
            [StringValue("Comment_Text_")] CommentInput,
            [StringValue("Comment_ContractReference_")] ContractReferenceInput,
            [StringValue("Comment_DrawingPageNumber_")] DrawingPageNumberInput,
            [StringValue("Comment_Response_")] CommentResponseInput,
            [StringValue("Comment_ResolutionMeetingDecision_")] CommentResolutionInput,
            [StringValue("Comment_ClosingComment_")] CommentClosingInput
        }

        //data-field attribute values
        public enum CommentEntryField_InTable
        {
            [StringValue("CommentType")] DocType,
            [StringValue("CommentType")] CommentType,
            [StringValue("Category")] Category,
            [StringValue("Organization")] Org,
            //[StringValue("ReviewerName")] By,
            [StringValue("ReviewType")] ReviewType,
            [StringValue("Text")] CommentInput,
            //[StringValue("Text")] Comment,
            //[StringValue("ContractReference")] Contract_Doc_Reference,
            [StringValue("ContractReference")] ContractReferenceInput,
            [StringValue("DrawingPageNumber")] DrawingPageNumberInput,
            //[StringValue("DrawingPageNumber")] Page_Sht_Dwg,
            [StringValue("Discipline")] Discipline,
            [StringValue("Response")] CommentResponseInput,
            [StringValue("ResponseCode")] ResponseCode,
            [StringValue("ResolutionMeetingDecision")] CommentResolutionInput,
            [StringValue("ResolutionStamp")] ResolutionStamp,
            [StringValue("ClosingComment")] CommentClosingInput,
            [StringValue("ClosingStamp")] ClosingStamp,
            [StringValue("ReviewerName")] ReviewerName,
            [StringValue("Reviewer")] Reviewer,
            [StringValue("VerifierName")] VerifiedBy,
            [StringValue("VerificationCode")] VerificationCode,
            [StringValue("VerifiedDateOffset")] VerifiedDate,
            [StringValue("VerificationNotes")] VerificationNotes
        }

        //data-field attribute values
        //public enum CommentEntryField_InTable
        //{
        //    //[StringValue("RowId")] RowNumber,
        //    [StringValue("CommentType")] DocType,
        //    //[StringValue("DrawingPageNumber")] Page_Sht_Dwg,
        //    [StringValue("Discipline")] Discipline,
        //    //[StringValue("Text")] Comment,
        //    //[StringValue("ContractReference")] Contract_Doc_Reference,
        //    [StringValue("Organization")] Org,
        //    //[StringValue("ReviewerName")] By,
        //    [StringValue("ReviewType")] ReviewType,
        //    [StringValue("Reviewer")] Reviewer,
        //    [StringValue("CommentType")] CommentType,
        //    [StringValue("Category")] Category,
        //    [StringValue("ResolutionStamp")] ResolutionStamp,
        //    [StringValue("ResponseCode")] ResponseCode,
        //    [StringValue("ClosingStamp")] ClosingStamp,
        //    [StringValue("VerificationCode")] VerificationCode,
        //    [StringValue("VerifiedDateOffset")] VerifiedDate
        //}

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
        internal static IList<DesignDocEntryField> designDocCreatePgEntryFields;

        [ThreadStatic]
        internal static IList<DesignDocHeader> designDocDetailsHeaders;

        [ThreadStatic]
        internal static IList<KeyValuePair<DesignDocEntryField, string>> createPgEntryFieldKeyValuePairs;

        [ThreadStatic]
        internal static IList<Enum> commentEntryFields;

        [ThreadStatic]
        internal static IList<KeyValuePair<Enum, string>> commentEntryFieldKeyValuePairs;

        public override void CreateDocument()
        {
            IAction pgAction = PageAction;
            pgAction.WaitForPageReady();
            pgAction.ClickElement(UploadNewDesignDoc_ByLocator);

            PopulateAllCreatePgEntryFields();

            //EnterDesignDocTitleAndNumber();

            pgAction.UploadFile("test.xlsx");
            pgAction.ClickElement(SaveForwardBtnUploadPage_ByLocator);
            pgAction.WaitForPageReady();
        }

        private string GetTblColumnIndex(CommentEntryField_InTable tableHeader)
            => PageAction.GetAttribute(By.XPath($"//thead[@role='rowgroup']/tr/th[@data-field='{tableHeader.GetString()}']"), "data-index");

        private By GetTblBtnByLocator(CommentRowBtn rowButton, int rowID)
            => By.XPath($"//tbody/tr[{rowID}]/td[1]/a[text()='{rowButton.ToString()}']");

        private void Click_TblRowBtn(CommentRowBtn rowButton, bool clickBtnForLatest = true, int rowID = 1)
        {
            By locator = By.XPath("//div[@class='k-grid-content']//tbody/tr");

            try
            {
                rowID = clickBtnForLatest
                    ? PageAction.GetElementsCount(locator)
                    : rowID;

                PageAction.JsClickElement(GetTblBtnByLocator(rowButton, rowID));
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public override void ClickBtn_CommentsTblRow_Edit(bool clickBtnForLatest = true, int rowID = 1)
            => Click_TblRowBtn(CommentRowBtn.Edit, clickBtnForLatest, rowID);

        public override void Click_TblBtn_Delete(bool clickBtnForLatest = true, int rowID = 1)
            => Click_TblRowBtn(CommentRowBtn.Delete, clickBtnForLatest, rowID);

        public override void Click_TblBtn_Files(bool clickBtnForLatest = true, int rowID = 1)
            => Click_TblRowBtn(CommentRowBtn.Files, clickBtnForLatest, rowID);

        public override void Click_TblBtn_Details(bool clickBtnForLatest = true, int rowID = 1)
            => Click_TblRowBtn(CommentRowBtn.Details, clickBtnForLatest, rowID);

        private void StoreDesignDocTitleAndNumber()
        {
            designDocTitle = GetVar("DsgnDocTtl");
            designDocNumber = GetVar("DsgnDocNumb");
            Console.WriteLine($"#####Title: {designDocTitle}\nNumber: {designDocNumber}");
        }

        public override void EnterDesignDocTitleAndNumber()
        {
            StoreDesignDocTitleAndNumber();
            PageAction.EnterText(GetTextInputFieldByLocator(DesignDocEntryField.Title), designDocTitle);
            PageAction.EnterText(GetTextInputFieldByLocator(DesignDocEntryField.DocumentNumber), designDocNumber);
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

        private KeyValuePair<DesignDocEntryField, string> PopulateAndStoreEntryFieldValue<T>(DesignDocEntryField entryField, T indexOrText, bool useContains = false)
        {
            string fieldType = entryField.GetString(true);
            Type argType = indexOrText.GetType();
            object argValue = null;
            bool isValidArg = false;

            KeyValuePair<DesignDocEntryField, string> fieldValuePair;
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
                                if (entryField.Equals(DesignDocEntryField.Title))
                                {
                                    designDocTitle = GetVar(entryField);
                                    argValue = designDocTitle;
                                }
                                else if (entryField.Equals(DesignDocEntryField.DocumentNumber))
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
                        var maxDaysEnum = DesignDocEntryField.MaxReviewDays_QAF;

                        if (entryField.Equals(DesignDocEntryField.MaxReviewDays_DOT_Chkbox))
                        {
                            maxDaysEnum = DesignDocEntryField.MaxReviewDays_DOT;
                        }
                        else if (entryField.Equals(DesignDocEntryField.MaxReviewDays_Other_Chkbox))
                        {
                            PageAction.SelectRadioBtnOrChkbox(entryField);
                            maxDaysEnum = DesignDocEntryField.MaxReviewDays_Other;
                        }

                        fieldValue = PageAction.GetText(GetTextInputFieldByLocator(maxDaysEnum));
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            return fieldValuePair = new KeyValuePair<DesignDocEntryField, string>(entryField, fieldValue);
        }

        /// <summary>
        /// Populates entry fields accordingly to 'designDocCreatePgEntryFields' list ([ThreadStatic] field variable) and stores EntryField enum and value in 'createPgEntryFieldKeyValuePairs' ([ThreadStatic] field variable).
        /// </summary>
        public override void PopulateAllCreatePgEntryFields()
        {
            foreach (DesignDocEntryField entryField in designDocCreatePgEntryFields)
            {
                KeyValuePair<DesignDocEntryField, string> kvPair = new KeyValuePair<DesignDocEntryField, string>();
                kvPair = PopulateAndStoreEntryFieldValue(entryField, "");
                createPgEntryFieldKeyValuePairs.Add(kvPair);
            }

            //designDocTitle = (from kvp in createPgEntryFieldKeyValuePairs where kvp.Key == DesignDocEntryField.Title select kvp.Value).FirstOrDefault();
            //designDocNumber = (from kvp in createPgEntryFieldKeyValuePairs where kvp.Key == DesignDocEntryField.DocumentNumber select kvp.Value).FirstOrDefault();
        }

        private DesignDocEntryField GetExpectedValueForDesignDocHeader(DesignDocHeader docHeader)
        {
            DesignDocEntryField entryField = DesignDocEntryField.Title;

            if (docHeader.Equals(DesignDocHeader.Date) || docHeader.Equals(DesignDocHeader.Document_Date))
            {
                entryField = DesignDocEntryField.DocumentDate;
            }
            else if (docHeader.Equals(DesignDocHeader.Number) || docHeader.Equals(DesignDocHeader.Document_Number))
            {
                entryField = DesignDocEntryField.DocumentNumber;
            }
            else if (docHeader.Equals(DesignDocHeader.Transmittal_Date) || docHeader.Equals(DesignDocHeader.Trans_Date))
            {
                entryField = DesignDocEntryField.TransmittalDate;
            }
            else if (docHeader.Equals(DesignDocHeader.Transmittal_No) || docHeader.Equals(DesignDocHeader.Trans_No))
            {
                entryField = DesignDocEntryField.TransmittalNumber;
            }
            else if (docHeader.Equals(DesignDocHeader.Segment))
            {
                entryField = DesignDocEntryField.Segment;
            }
            else if (docHeader.Equals(DesignDocHeader.Title))
            {
                entryField = DesignDocEntryField.Title;
            }

            return entryField;
        }

        public override void VerifyDesignDocDetailsHeader()
        {
            IList<string> expectedValueInHeaderList = new List<string>();
            IList<string> actualValueInHeaderList = new List<string>();
            string entryValue = string.Empty;
            bool expectedHeadersDisplayed = true;

            try
            {
                foreach (DesignDocHeader header in designDocDetailsHeaders)
                {

                    if (
                        header.Equals(DesignDocHeader.Action) ||
                        header.Equals(DesignDocHeader.Status) ||
                        header.Equals(DesignDocHeader.Remaining_Days) ||
                        header.Equals(DesignDocHeader.Review_Deadline)
                        )
                    {
                        switch (header)
                        {
                            case DesignDocHeader.Action:
                                entryValue = "For Review/Comment";
                                break;
                            case DesignDocHeader.Status:
                                entryValue = documentStatus;
                                break;
                            case DesignDocHeader.Remaining_Days:
                                entryValue = ""; //TODO
                                break;
                            case DesignDocHeader.Review_Deadline:
                                entryValue = ""; //TODO
                                break;
                        }

                        //On SG, the 'Review Deadline' header field is not shown when Status is 'Requires Response'
                        expectedHeadersDisplayed = header.Equals(DesignDocHeader.Review_Deadline) && entryValue.Equals("Requires Response")
                            ? false
                            : true;
                    }
                    else
                    {
                        DesignDocEntryField entryField = GetExpectedValueForDesignDocHeader(header);
                        entryValue = (from kvp in createPgEntryFieldKeyValuePairs where kvp.Key == entryField select kvp.Value).FirstOrDefault();
                    }

                    if (expectedHeadersDisplayed)
                    {
                        actualValueInHeaderList.Add(GetHeaderValue(header));
                        expectedValueInHeaderList.Add($"{header}::{entryValue}");
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

        public override string GetHeaderValue(DesignDocHeader docHeader)
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
                documentStatus = Kendo().GetCurrentTableTabName();
            }
        }

        public override string GetDesignDocStatus()
            => documentStatus;

        public override IList<KeyValuePair<DesignDocEntryField, string>> GetDesignDocEntryFieldKeyValuePairs()
        {
            if (createPgEntryFieldKeyValuePairs == null)
            {
                createPgEntryFieldKeyValuePairs = new List<KeyValuePair<DesignDocEntryField, string>>();
            }

            return createPgEntryFieldKeyValuePairs;
        }

        public override IList<KeyValuePair<Enum, string>> GetCommentEntryFieldKeyValuePairs()
        {
            if (commentEntryFieldKeyValuePairs == null)
            {
                commentEntryFieldKeyValuePairs = new List<KeyValuePair<Enum, string>>();
            }

            return commentEntryFieldKeyValuePairs;
        }

        public override void VerifyItemStatusIsClosed()
        {
            ClickTab_Closed();
            //SelectTab(TableTab.Closed);
            TestUtility.AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(ColumnName.Number, designDocNumber), $"VerifyItemStatusIsClosed");
        }

    }

    #endregion DesignDocument Generic class

    #region Workflow Interface class

    public interface IDesignDocument
    {
        IList<DesignDocEntryField> GetDesignDocCreatePgEntryFieldsList();

        IList<DesignDocHeader> GetDesignDocDetailsHeadersList();

        IList<Enum> GetCommentEntryFieldsList();

        IList<KeyValuePair<DesignDocEntryField, string>> GetDesignDocEntryFieldKeyValuePairs();

        IList<KeyValuePair<Enum, string>> GetCommentEntryFieldKeyValuePairs();

        string GetHeaderValue(DesignDocHeader docHeader);

        void SetDesignDocStatus<T>(T tableTabOrWorkflow);

        string GetDesignDocStatus();

        void VerifyDesignDocDetailsHeader();

        void ScrollToLastColumn();

        void ScrollToFirstColumn();

        void EnterVerifiedDate(string shortDate = "01/01/2019");

        void ClickBtn_AddComment();

        void ClickBtn_Update();

        void ClickBtn_Cancel();

        void ClickBtn_CommentsTblRow_Edit(bool clickBtnForLatest = true, int rowID = 1);

        void Click_TblBtn_Delete(bool clickBtnForLatest = true, int rowID = 1);

        void Click_TblBtn_Files(bool clickBtnForLatest = true, int rowID = 1);

        void Click_TblBtn_Details(bool clickBtnForLatest = true, int rowID = 1);

        void SelectTab(TableTab tableTab);

        void SortTable_Descending();

        void SortTable_Ascending();

        void ClickTab_Comment();

        void ClickTab_Response();

        void ClickTab_Verification();

        void ClickTab_Creating();

        void ClickTab_Pending_Comment();

        void ClickTab_Requires_Comment();

        void ClickTab_Pending_Response();

        void ClickTab_Requires_Response();

        void ClickTab_Pending_Resolution();

        void ClickTab_Requires_Resolution();

        void ClickTab_Pending_Closing();

        void ClickTab_Requires_Closing();

        void ClickTab_Closed();

        void CreateDocument();

        void PopulateAllCreatePgEntryFields();

        void EnterDesignDocTitleAndNumber();

        void EnterRegularCommentAndDrawingPageNo();

        void EnterTextInCommentField(Enum commentType, int commentTabNumber = 1);

        void EnterNoComment();

        void EnterResponseCommentAndAgreeResponseCode();

        void EnterResponseCommentAndDisagreeResponseCode();

        void FilterDocNumber(string filterByValue = "");

        void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse();

        void Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse();

        void VerifyItemStatusIsClosed();

        void ClickBtn_BackToList();

        void ClickBtn_SaveOnly();

        void ClickBtn_SaveForward();

        void SelectRegularCommentReviewType(int commentTabNumber);

        void SelectNoCommentReviewType(int commentTabNumber);

        bool VerifyTitleFieldErrorMsgIsDisplayed();

        bool VerifyDocumentNumberFieldErrorMsgIsDisplayed();

        bool VerifyUploadFileErrorMsgIsDisplayed();

        //void EnterClosingCommentAndCode();

        void SelectDisagreeResolutionCode(int commentTabNumber = 1);

        void SelectCommentType(int commentTypeTabNumber = 1);

        void SelectOrganization(int disciplineNumber = 1);

        void SelectDiscipline(int disciplineNumber = 1);

        void SelectCategory(int categoryNumber = 1);

        void SelectAgreeResolutionCode(int commentTabNumber = 1);

        void SelectAgreeResponseCode(int commentTabNumber = 1);

        void SelectDisagreeResponseCode(int commentTabNumber = 1);

        void SelectDDL_ClosingStamp(int commentTabNumber = 1);

        void SelectDDL_VerificationCode(int selectionIndex = 1);

        void WaitForActiveCommentTab();

        void ClickCommentTabNumber(int commentTabNumber);

        void SelectDDL_ReviewType(int selectionIndex);

        void SelectDDL_Reviewer<T>(T selectionIndexOrReviewerName, bool useContainsFilter);

        void SelectDDL_CommentType(int selectionIndex);

        void SelectDDL_Category(int selectionIndex);

        void SelectDDL_Discipline(int selectionIndex);
    }

    #endregion Workflow Interface class

    #region Common Workflow Implementation class

    public abstract class DesignDocument_Impl : TestBase, IDesignDocument
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IDesignDocument SetPageClassBasedOnTenant(IWebDriver driver)
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
            return instance;
        }

        #region Page element By Locators
        public By UploadNewDesignDoc_ByLocator
            => By.XPath("//a[text()='Upload New Design Document']");

        public By CancelBtnUploadPage_ByLocator
            => By.Id("btnCancel");

        public By SaveOnlyBtnUploadPage_ByLocator
            => By.Id("btnSave");

        public By SaveForwardBtnUploadPage_ByLocator
            => By.Id("btnSaveForward");

        public By SaveOnlyBtn_ByLocator
            => By.XPath("//div[@class='k-content k-state-active']//button[contains(@id,'btnSave_')]");

        public By Table_ForwardBtn_ByLocator
            => By.XPath("//button[@id='btnSaveForward']");

        public By SaveForwardBtn_ByLocator
            => By.XPath("//div[@class='k-content k-state-active']//button[contains(@id,'btnSaveForward_')]");

        public By BackToListBtn_ByLocator
            => By.XPath("//button[text()='Back To List']");

        public By BackToListBtn_InTable_ByLocator
            => By.XPath("//a[text()=' Back to List']");

        public By Btn_Refresh_ByLocator
            => By.XPath("//a[@aria-label='Refresh']");

        public By Btn_Cancel_ByLocator
            => By.Id("btnCancelSave");

        public By Btn_Forward_ByLocator
            => By.XPath("//button[@id='btnSaveForward']");

        public By Btn_ShowFileList_ByLocator
            => By.XPath("//button[contains(@class,'showFileList')]");

        public By Btn_PDF_ByLocator
            => By.Id("PdfExportLink");

        public By Btn_XLS_ByLocator
            => By.Id("CsvExportLink");

        #endregion Page element By Locators

        public virtual IList<DesignDocEntryField> GetDesignDocCreatePgEntryFieldsList()
            => designDocCreatePgEntryFields;

        public virtual IList<DesignDocHeader> GetDesignDocDetailsHeadersList()
            => designDocDetailsHeaders;

        public virtual IList<Enum> GetCommentEntryFieldsList()
            => commentEntryFields;

        public virtual void SelectDDL_ReviewType(int selectionIndex)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ReviewType, selectionIndex);

        public virtual void SelectDDL_Reviewer<T>(T selectionIndexOrReviewerName, bool useContainsFilter)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.Reviewer, selectionIndexOrReviewerName, useContainsFilter);

        public virtual void SelectDDL_CommentType(int selectionIndex)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.CommentType, selectionIndex);

        public virtual void SelectDDL_Category(int selectionIndex)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.Category, selectionIndex);

        public virtual void SelectDDL_Discipline(int selectionIndex)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.Discipline, selectionIndex);

        public virtual void ClickBtn_BackToList()
        {
            PageAction.JsClickElement(BackToListBtn_ByLocator);
            PageAction.WaitForPageReady();
        }

        public virtual void ClickBtn_SaveOnly()
        {
            PageAction.ClickElement(SaveOnlyBtn_ByLocator);
            PageAction.WaitForPageReady();
        }

        public virtual void ClickBtn_SaveForward()
        {
            PageAction.JsClickElement(SaveForwardBtn_ByLocator);
            PageAction.WaitForPageReady();
        }

        public abstract void CreateDocument();
        //{
        //    WaitForPageReady();
        //    ClickElement(UploadNewDesignDoc_ByLocator);

        //    PopulateAllCreatePgEntryFields();

        //    //EnterDesignDocTitleAndNumber();

        //    UploadFile("test.xlsx");
        //    ClickElement(SaveForwardBtnUploadPage_ByLocator);
        //    WaitForPageReady();
        //}

        internal string SetCommentStamp(CommentEntryField inputFieldEnum, int commentTabIndex)
            => $"{inputFieldEnum.GetString()}{(commentTabIndex - 1).ToString()}_";

        public virtual void SelectRegularCommentReviewType(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp(CommentEntryField.ReviewType, commentTabNumber), 3);

        public virtual void SelectNoCommentReviewType(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp(CommentEntryField.ReviewType, commentTabNumber), 1);

        public virtual void SelectCommentType(int commentTypeTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp(CommentEntryField.CommentType, commentTypeTabNumber), 1);

        public virtual void SelectDiscipline(int disciplineNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp(CommentEntryField.Discipline, disciplineNumber), 1);

        public virtual void SelectCategory(int categoryNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp(CommentEntryField.Category, categoryNumber), 1);

        public virtual void SelectAgreeResolutionCode(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp(CommentEntryField.ResolutionStamp, commentTabNumber), 1); //check the index, UI not working so need to confirm later

        public virtual void SelectAgreeResponseCode(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp(CommentEntryField.ResponseCode, commentTabNumber), 2); //check the index, UI not working so need to confirm later

        public virtual void SelectDisagreeResponseCode(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp(CommentEntryField.ResponseCode, commentTabNumber), 3);//check the index, UI not working so need to confirm later

        public virtual void SelectDisagreeResolutionCode(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp(CommentEntryField.ResolutionStamp, commentTabNumber), 2);//check the index, UI not working so need to confirm later

        public virtual void SelectDDL_ClosingStamp(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp(CommentEntryField.ClosingStamp, commentTabNumber), 1);

        public virtual void SelectOrganization(int disciplineNumber = 1)
        {
        }

        public virtual void SelectDDL_VerificationCode(int selectionIndex = 1)
        {
        }

        public virtual void EnterVerifiedDate(string shortDate)
        {
        }


        /// <summary>
        /// Filters Number column using ThreadStatic value, designDocNumber, by default.
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="filterByValue"></param>
        public virtual void FilterDocNumber(string filterByValue = "")
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

        public virtual void EnterTextInCommentField(Enum commentField, int commentTabNumber = 1)
        {
            try
            {
                string text = "Comment 123";
                By entryFieldLocator = null;

                Type commentFieldEnum = commentField.GetType();

                if (commentFieldEnum == typeof(CommentEntryField))
                {
                    entryFieldLocator = By.Id($"{commentField.GetString()}{commentTabNumber - 1}_");
                }
                else if (commentFieldEnum == typeof(CommentEntryField_InTable))
                {
                    entryFieldLocator = By.Id($"{commentField.GetString()}");
                }
                //else if (commentFieldEnum == typeof(CommentEntryField_InTable))
                //{
                //    entryFieldLocator = GetTextInputFieldByLocator(commentField);
                //}

                PageAction.ScrollToElement(entryFieldLocator);

                if (commentField.Equals(CommentEntryField_InTable.VerifiedDate))
                {
                    text = GetShortDate();
                    PageAction.EnterText(GetTextInputFieldByLocator(commentField), text);
                }
                else
                {
                    if (commentField.Equals(CommentEntryField.ContractReferenceInput) || commentField.Equals(CommentEntryField_InTable.ContractReferenceInput))
                    {
                        text = "Contract Reference 123";
                    }
                    else if (commentField.Equals(CommentEntryField.DrawingPageNumberInput) || commentField.Equals(CommentEntryField_InTable.DrawingPageNumberInput))
                    {
                        text = "Drawing PageNumber 123";
                    }
                    else if (commentField.Equals(CommentEntryField.CommentResponseInput) || commentField.Equals(CommentEntryField_InTable.CommentResponseInput))
                    {
                        text = "Comment Response 123";
                    }
                    else if (commentField.Equals(CommentEntryField.CommentResolutionInput) || commentField.Equals(CommentEntryField_InTable.CommentResolutionInput))
                    {
                        text = "Comment Resolution 123";
                    }
                    else if (commentField.Equals(CommentEntryField.CommentClosingInput) || commentField.Equals(CommentEntryField_InTable.CommentClosingInput))
                    {
                        text = "Closing Comment 123";
                    }
                    else if (commentField.Equals(CommentEntryField_InTable.VerificationNotes))
                    {
                        text = "Verification Notes 123";
                    }
                    else if (commentField.Equals(CommentEntryField_InTable.ReviewerName) || commentField.Equals(CommentEntryField_InTable.VerifiedBy))
                    {
                        text = PageAction.GetCurrentUser();
                    }

                    PageAction.EnterText(entryFieldLocator, text);
                }

                Report.Step($"Entered '{text}' in {commentField.ToString()} field : {entryFieldLocator}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        public virtual void EnterRegularCommentAndDrawingPageNo()
        {
            //login as commenting user (SG- IQFuser, DoTuser | SH249-- IQFUser | Garenet and GLX-- DOTUser)
            SelectRegularCommentReviewType();
            EnterTextInCommentField(CommentEntryField.CommentInput);
            EnterTextInCommentField(CommentEntryField.DrawingPageNumberInput);
            ClickBtn_SaveOnly();
        }

        public virtual void EnterNoComment()
        {
            //login as commenting user (SG- IQFuser, DoTuser | SH249-- IQFUser | Garenet and GLX-- DOTUser)
            SelectNoCommentReviewType();
            ClickBtn_SaveOnly();
        }

        public virtual void EnterResponseCommentAndAgreeResponseCode()
        {
            // Login as user to make response comment (All tenants - DevUser)
            EnterTextInCommentField(CommentEntryField.CommentResponseInput);
            SelectAgreeResponseCode(); //Disagree then different workflow
            ClickBtn_SaveOnly();
        }

        public virtual void EnterResponseCommentAndDisagreeResponseCode()
        {
            // Login as user to make response comment (All tenants - DevUser)
            EnterTextInCommentField(CommentEntryField.CommentResponseInput);
            SelectDisagreeResponseCode(); //agree then different workflow
            ClickBtn_SaveOnly();
        }

        public virtual void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            Report.Step($"<<-- WORKFLOW ({tenantName}): EnterResolutionCommentAndResolutionCodeforDisagreeResponse -->>");
            ClickTab_Requires_Resolution();
            SortTable_Descending();
            GridHelper.ClickEnterBtnForRow();

            // Login as user to make resolution comment (All tenants - DevAdmin)
            EnterTextInCommentField(CommentEntryField.CommentResolutionInput);
            SelectDisagreeResolutionCode(); //
            ClickBtn_SaveOnly();
            ClickBtn_BackToList();
        }

        public virtual void Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            ClickTab_Requires_Resolution();
            SortTable_Descending();
            GridHelper.ClickEnterBtnForRow();
            ClickBtn_SaveForward();
        }

        private bool VerifyRequiredFieldErrorMsg(string errorMsg)
            => PageAction.ElementIsDisplayed(By.XPath($"//li[text()='{errorMsg}']"));

        public virtual bool VerifyTitleFieldErrorMsgIsDisplayed()
            => VerifyRequiredFieldErrorMsg("Submittal Title is required.");

        public virtual bool VerifyDocumentNumberFieldErrorMsgIsDisplayed()
            => VerifyRequiredFieldErrorMsg("Submittal Number is required.");

        public virtual bool VerifyUploadFileErrorMsgIsDisplayed()
            => VerifyRequiredFieldErrorMsg("At least one file must be added.");

        public abstract void VerifyItemStatusIsClosed();

        public virtual void SelectTab(TableTab tableTab)
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

        public virtual void ClickTab_Comment() => SelectTab(TableTab.Comment);

        public virtual void ClickTab_Response() => SelectTab(TableTab.Response);

        public virtual void ClickTab_Verification() => SelectTab(TableTab.Verification);

        public virtual void ClickTab_Creating() => SelectTab(TableTab.Creating);

        public virtual void ClickTab_Pending_Comment() => SelectTab(TableTab.Pending_Comment);

        public virtual void ClickTab_Requires_Comment() => SelectTab(TableTab.Requires_Comment);

        public virtual void ClickTab_Pending_Response() => SelectTab(TableTab.Pending_Response);

        public virtual void ClickTab_Requires_Response() => SelectTab(TableTab.Requires_Response);

        public virtual void ClickTab_Pending_Resolution() => SelectTab(TableTab.Pending_Resolution);

        public virtual void ClickTab_Requires_Resolution() => SelectTab(TableTab.Requires_Resolution);

        public virtual void ClickTab_Pending_Closing() => SelectTab(TableTab.Pending_Closing);

        public virtual void ClickTab_Requires_Closing() => SelectTab(TableTab.Requires_Closing);

        public virtual void ClickTab_Closed() => SelectTab(TableTab.Closed);

        public virtual void SortTable_Descending() => GridHelper.SortColumnDescending(ColumnName.Action);

        public virtual void SortTable_Ascending() => GridHelper.SortColumnAscending(ColumnName.Action);

        public virtual void WaitForActiveCommentTab()
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
                        activeTabNotDisplayed = PageAction.ElementIsDisplayed(By.XPath("//div[@class='k-content k-state-active']"));
                    }
                }
                while (activeTabNotDisplayed);
            }
        }

        public virtual void ClickCommentTabNumber(int commentTabNumber)
        {
            WaitForActiveCommentTab();
            Kendo().ClickCommentTab(commentTabNumber);
        }

        public abstract void EnterDesignDocTitleAndNumber();
        public abstract void ClickBtn_AddComment();
        public abstract void ClickBtn_Update();
        public abstract void ClickBtn_Cancel();
        public abstract void ClickBtn_CommentsTblRow_Edit(bool clickBtnForLatest = true, int rowID = 1);
        public abstract void Click_TblBtn_Delete(bool clickBtnForLatest = true, int rowID = 1);
        public abstract void Click_TblBtn_Files(bool clickBtnForLatest = true, int rowID = 1);
        public abstract void Click_TblBtn_Details(bool clickBtnForLatest = true, int rowID = 1);
        public abstract void ScrollToLastColumn();
        public abstract void ScrollToFirstColumn();
        public abstract void PopulateAllCreatePgEntryFields();
        public abstract void VerifyDesignDocDetailsHeader();
        public abstract void SetDesignDocStatus<T>(T tableTabOrWorkflow);
        public abstract string GetDesignDocStatus();
        public abstract IList<KeyValuePair<DesignDocEntryField, string>> GetDesignDocEntryFieldKeyValuePairs();
        public abstract IList<KeyValuePair<Enum, string>> GetCommentEntryFieldKeyValuePairs();
        public abstract string GetHeaderValue(DesignDocHeader docHeader);
    }

    #endregion Common Workflow Implementation class

    /// <summary>
    /// Tenant specific implementation of DesignDocument Comment Review
    /// </summary>

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
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp(CommentEntryField.ReviewType, commentTabNumber), 2);

        public override void SelectNoCommentReviewType(int commentTabNumber = 1)
            => PageAction.ExpandAndSelectFromDDList(SetCommentStamp(CommentEntryField.ReviewType, commentTabNumber), 3);
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
            EnterTextInCommentField(CommentEntryField.CommentResolutionInput);
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

        public override IList<DesignDocEntryField> GetDesignDocCreatePgEntryFieldsList()
        {
            if (designDocCreatePgEntryFields == null)
            {
                designDocCreatePgEntryFields = new List<DesignDocEntryField>
                {
                    DesignDocEntryField.Title,
                    DesignDocEntryField.DocumentNumber,
                    DesignDocEntryField.DocumentDate,
                    DesignDocEntryField.Segment,
                    DesignDocEntryField.TransmittalNumber,
                    DesignDocEntryField.TransmittalDate,
                    DesignDocEntryField.MaxReviewDays_QAF_Chkbox,
                    DesignDocEntryField.MaxReviewDays_DOT_Chkbox
                };
            }

            return designDocCreatePgEntryFields;
        }

        public override IList<DesignDocHeader> GetDesignDocDetailsHeadersList()
        {
            if (designDocDetailsHeaders == null)
            {
                designDocDetailsHeaders = new List<DesignDocHeader>
                {
                    DesignDocHeader.Document_Number,
                    DesignDocHeader.Status,
                    DesignDocHeader.Title,
                    DesignDocHeader.Segment,
                    DesignDocHeader.Document_Date,
                    DesignDocHeader.Review_Deadline,
                    DesignDocHeader.Transmittal_Date,
                    DesignDocHeader.Action,
                    DesignDocHeader.Remaining_Days,
                    DesignDocHeader.Transmittal_No
                };
            }

            return designDocDetailsHeaders;
        }

        //TODO
        public override IList<Enum> GetCommentEntryFieldsList()
        {
            if (commentEntryFields == null)
            {
                commentEntryFields = new List<Enum>
                {
                    CommentEntryField.CommentInput,
                };
            }

            return commentEntryFields;
        }

        public override string GetHeaderValue(DesignDocHeader docHeader)
            => PageAction.GetText(By.XPath($"//label[contains(text(),'{docHeader.GetString()}')]/parent::div/following-sibling::div[1]"));

        public override void EnterRegularCommentAndDrawingPageNo()
        {
            SelectRegularCommentReviewType();
            SelectCommentType();
            SelectCategory();
            SelectDiscipline();
            EnterTextInCommentField(CommentEntryField.CommentInput);
            EnterTextInCommentField(CommentEntryField.ContractReferenceInput);
            EnterTextInCommentField(CommentEntryField.DrawingPageNumberInput);
            ClickBtn_SaveOnly();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            EnterTextInCommentField(CommentEntryField.CommentResponseInput);
            SelectDisagreeResponseCode();
            ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterTextInCommentField(CommentEntryField.CommentResponseInput, commentTabNumber);
            SelectDisagreeResponseCode(commentTabNumber);
            ClickBtn_SaveOnly();
        }

        public override void EnterResponseCommentAndAgreeResponseCode()
        {
            EnterTextInCommentField(CommentEntryField.CommentResponseInput);
            SelectAgreeResponseCode();
            ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterTextInCommentField(CommentEntryField.CommentResponseInput, commentTabNumber);
            SelectAgreeResponseCode(commentTabNumber);
            ClickBtn_SaveOnly();
        }

        public override void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            Report.Step($"<<-- WORKFLOW ({tenantName}): EnterResolutionCommentAndResolutionCodeforDisagreeResponse -->>");
            EnterTextInCommentField(CommentEntryField.CommentResolutionInput);
            SelectDisagreeResolutionCode(); //
            ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterTextInCommentField(CommentEntryField.CommentResolutionInput, commentTabNumber);
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

        public override IList<DesignDocEntryField> GetDesignDocCreatePgEntryFieldsList()
        {
            if (designDocCreatePgEntryFields == null)
            {
                designDocCreatePgEntryFields = new List<DesignDocEntryField>
                {
                    DesignDocEntryField.Title,
                    DesignDocEntryField.DocumentNumber,
                    DesignDocEntryField.DocumentDate,
                    DesignDocEntryField.Segment,
                    DesignDocEntryField.TransmittalNumber,
                    DesignDocEntryField.TransmittalDate,
                    DesignDocEntryField.MaxReviewDays_QAF_Chkbox,
                    DesignDocEntryField.MaxReviewDays_DOT_Chkbox,
                    DesignDocEntryField.MaxReviewDays_Other_Chkbox,
                    DesignDocEntryField.MaxReviewDays_Other_Reviewer
                };
            }

            return designDocCreatePgEntryFields;
        }

        public override IList<DesignDocHeader> GetDesignDocDetailsHeadersList()
        {
            if (designDocDetailsHeaders == null)
            { 
                designDocDetailsHeaders = new List<DesignDocHeader>
                {
                    DesignDocHeader.Date,
                    DesignDocHeader.Number,
                    DesignDocHeader.Title,
                    DesignDocHeader.Segment,
                    DesignDocHeader.Action,
                    DesignDocHeader.Status,
                    DesignDocHeader.Trans_Date,
                    DesignDocHeader.Trans_No
                };
            }

            return designDocDetailsHeaders;
        }

        //TODO
        public override IList<Enum> GetCommentEntryFieldsList()
        {
            if (commentEntryFields == null)
            {
                commentEntryFields = new List<Enum>
                {
                    CommentEntryField_InTable.CommentInput,
                };
            }

            return commentEntryFields;
        }

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
            EnterTextInCommentField(CommentEntryField_InTable.DrawingPageNumberInput);
            EnterTextInCommentField(CommentEntryField_InTable.ContractReferenceInput);
            EnterTextInCommentField(CommentEntryField_InTable.CommentInput);
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
            EnterTextInCommentField(CommentEntryField.CommentResponseInput);
            EnterTextInCommentField(CommentEntryField.CommentResolutionInput);
            SelectDisagreeResolutionCode();
            ClickBtn_Update();
            ClickBtn_SaveForward();
        }

        public override void ClickBtn_BackToList()
        {
            PageAction.JsClickElement(BackToListBtn_InTable_ByLocator);
            PageAction.WaitForPageReady();
            //WaitForLoading();
        }

        public override void ClickBtn_SaveForward()
        {
            PageAction.ClickElement(Table_ForwardBtn_ByLocator);
            PageAction.WaitForPageReady();
            //WaitForLoading();
        }

        public override void SelectRegularCommentReviewType(int selectionIndex = 3)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ReviewType, selectionIndex);

        public override void SelectNoCommentReviewType(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ReviewType, selectionIndex);

        public override void SelectCommentType(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.CommentType, selectionIndex);

        public override void SelectDiscipline(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.Discipline, selectionIndex);

        public override void SelectCategory(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.Category, selectionIndex);

        public override void SelectAgreeResolutionCode(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ResolutionStamp, selectionIndex);

        public override void SelectAgreeResponseCode(int selectionIndex = 2)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ResponseCode, selectionIndex);

        public override void SelectDisagreeResponseCode(int selectionIndex = 3)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ResponseCode, selectionIndex);

        public override void SelectDisagreeResolutionCode(int selectionIndex = 2)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ResolutionStamp, selectionIndex);

        public override void SelectDDL_ClosingStamp(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ClosingStamp, selectionIndex);

    }

    #endregion Implementation specific to SH249

    #region Implementation specific to LAX

    public class DesignDocument_LAX : DesignDocument
    {
        public DesignDocument_LAX(IWebDriver driver) : base(driver)
        {
        }

        public override IList<DesignDocEntryField> GetDesignDocCreatePgEntryFieldsList()
        {
            if (designDocCreatePgEntryFields == null)
            {
                designDocCreatePgEntryFields = new List<DesignDocEntryField>
                {
                    DesignDocEntryField.Title,
                    DesignDocEntryField.DocumentNumber,
                    DesignDocEntryField.DocumentDate,
                    DesignDocEntryField.Segment,
                    DesignDocEntryField.TransmittalNumber,
                    DesignDocEntryField.TransmittalDate,
                    DesignDocEntryField.MaxReviewDays_DOT_Chkbox
                };
            }

            return designDocCreatePgEntryFields;
        }

        public override IList<DesignDocHeader> GetDesignDocDetailsHeadersList()
        {
            if (designDocDetailsHeaders == null)
            {
                designDocDetailsHeaders = new List<DesignDocHeader>
                {
                    DesignDocHeader.Date,
                    DesignDocHeader.Number,
                    DesignDocHeader.Title,
                    DesignDocHeader.Segment,
                    DesignDocHeader.Action,
                    DesignDocHeader.Status,
                    DesignDocHeader.Trans_Date,
                    DesignDocHeader.Trans_No
                };
            }

            return designDocDetailsHeaders;
        }

        //TODO
        public override IList<Enum> GetCommentEntryFieldsList()
        {
            if (commentEntryFields == null)
            {
                commentEntryFields = new List<Enum>
                {
                    CommentEntryField_InTable.CommentInput,
                };
            }

            return commentEntryFields;
        }

        public override void ClickBtn_BackToList()
        {
            PageAction.JsClickElement(BackToListBtn_InTable_ByLocator);
            PageAction.WaitForPageReady();
        }

        public override void ClickBtn_SaveForward()
        {
            PageAction.JsClickElement(Table_ForwardBtn_ByLocator);
            PageAction.WaitForPageReady();
        }

        public override void SelectRegularCommentReviewType(int selectionIndex = 3)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ReviewType, selectionIndex);

        public override void SelectNoCommentReviewType(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ReviewType, selectionIndex);

        public override void SelectCommentType(int selectionIndex = 1)
        {
            try
            {
                PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.DocType, selectionIndex);
            }
            catch (ElementNotInteractableException e)
            {
                log.Debug(e.Message);
            }
            catch (Exception)
            { }
        }

        public override void SelectOrganization(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.Org, selectionIndex);

        public override void SelectDiscipline(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.Discipline, selectionIndex);

        public override void SelectCategory(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.Category, selectionIndex);

        public override void SelectAgreeResolutionCode(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ResolutionStamp, selectionIndex);

        public override void SelectAgreeResponseCode(int selectionIndex = 2)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ResponseCode, selectionIndex);

        public override void SelectDisagreeResponseCode(int selectionIndex = 3)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ResponseCode, selectionIndex);

        public override void SelectDisagreeResolutionCode(int selectionIndex = 2)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ResolutionStamp, selectionIndex);

        public override void SelectDDL_ClosingStamp(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.ClosingStamp, selectionIndex);

        public override void SelectDDL_VerificationCode(int selectionIndex = 1)
            => PageAction.ExpandAndSelectFromDDList(CommentEntryField_InTable.VerificationCode, selectionIndex);

        public override void EnterVerifiedDate(string shortDate = "01/01/2019")
        {
            shortDate = shortDate.Equals("01/01/2019")
                ? GetShortDate()
                : shortDate;

            PageAction.EnterText(GetTextInputFieldByLocator(CommentEntryField_InTable.VerifiedDate), shortDate);
        }

        public override void EnterRegularCommentAndDrawingPageNo()
        {
            PageAction.WaitForPageReady();
            ClickBtn_AddComment();
            SelectCommentType();
            EnterTextInCommentField(CommentEntryField_InTable.ReviewerName);
            SelectOrganization();
            EnterTextInCommentField(CommentEntryField_InTable.ContractReferenceInput);
            EnterTextInCommentField(CommentEntryField_InTable.CommentInput);
            EnterTextInCommentField(CommentEntryField_InTable.DrawingPageNumberInput);
            SelectDiscipline();
            PageAction.WaitForPageReady();
        }

        public override void EnterResponseCommentAndAgreeResponseCode()
        {
            PageAction.WaitForPageReady();
            ClickBtn_CommentsTblRow_Edit();
            EnterTextInCommentField(CommentEntryField_InTable.CommentResponseInput);
            EnterTextInCommentField(CommentEntryField_InTable.CommentResolutionInput);
            SelectAgreeResolutionCode();
            ClickBtn_Update();
            ClickBtn_SaveForward();
        }
    }

    #endregion Implementation specific to LAX

}