using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using System;
using System.Threading;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.DesignDocument;
using RKCIUIAutomation.Test;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    #region DesignDocument Generic class

    public class DesignDocument : DesignDocument_Impl
    {
        public DesignDocument()
        {
        }

        public DesignDocument(IWebDriver driver) => this.Driver = driver;

        public override void ScrollToLastColumn()
            => ScrollToElement(By.XPath("//tbody/tr/td[@style='vertical-align: top;'][last()]"));

        public override void ScrollToFirstColumn()
            => ScrollToElement(By.XPath("//tbody/tr/td[@style='vertical-align: top;'][1]"));

        public enum DesignDocDetails_InputFields
        {
            [StringValue("Submittal_Title")] Title,
            [StringValue("Submittal_Document_Number")] DocumentNumber,
            [StringValue("Submittal_Document_DocumentDate")] DocumentDate,
            [StringValue("Submittal_TransmittalDate")] TransmittalDate,
            [StringValue("Submittal_TransmittalNumber")] TransmittalNumber,
            [StringValue("Comment_ReviewTypeId_")] ReviewType,
            [StringValue("Comment_ResponseCodeId_")] ResponseCode,
            [StringValue("Comment_ResolutionStampId_")] ResolutionStamp,
            [StringValue("Comment_ClosingStampId_")] ClosingStamp,
            [StringValue("Comment_CommentTypeId_")] CommentType,
            [StringValue("Comment_CategoryId_")] Category,
            [StringValue("Comment_DisciplineId_")] Discipline
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

        public enum CommentType
        {
            [StringValue("Comment_Text_")] CommentInput,
            [StringValue("Comment_ContractReference_")] ContractReferenceInput,
            [StringValue("Comment_DrawingPageNumber_")] DrawingPageNumberInput,
            [StringValue("Comment_Response_")] CommentResponseInput,
            [StringValue("Comment_ResolutionMeetingDecision_")] CommentResolutionInput,
            [StringValue("Comment_ClosingComment_")] CommentClosingInput
        }

        public enum CommentType_InTable
        {
            [StringValue("Text")] CommentInput,
            [StringValue("ContractReference")] ContractReferenceInput,
            [StringValue("DrawingPageNumber")] DrawingPageNumberInput,
            [StringValue("Response")] CommentResponseInput,
            [StringValue("ResolutionMeetingDecision")] CommentResolutionInput,
            [StringValue("ClosingComment")] CommentClosingInput,
            [StringValue("ReviewerName")] ReviewerName,
            [StringValue("VerifierName")] VerifiedBy,
            [StringValue("VerificationNotes")] VerificationNotes,
        }

        //data-field attribute values
        public enum PkgComments_TblHeader
        {
            [StringValue("RowId")] RowNumber,
            [StringValue("CommentType")] DocType,
            [StringValue("DrawingPageNumber")] Page_Sht_Dwg,
            [StringValue("Discipline")] Discipline,
            [StringValue("Text")] Comment,
            [StringValue("ContractReference")] Contract_Doc_Reference,
            [StringValue("Organization")] Org,
            [StringValue("ReviewerName")] By,
            [StringValue("ReviewType")] ReviewType,
            [StringValue("Reviewer")] Reviewer,
            [StringValue("CommentType")] CommentType,
            [StringValue("Category")] Category,
            [StringValue("ResolutionStamp")] ResolutionStamp,
            [StringValue("ResponseCode")] ResponseCode,
            [StringValue("ClosingStamp")] ClosingStamp,
            [StringValue("VerificationCode")] VerificationCode,
            [StringValue("VerifiedDateOffset")] VerifiedDate,
        }

        public enum PkgComments_Button
        {
            Edit,
            Delete,
            Files,
            Details
        }

        [ThreadStatic]
        internal static string designDocTitle;

        [ThreadStatic]
        internal static string designDocNumber;

        internal By UploadNewDesignDoc_ByLocator => By.XPath("//a[text()='Upload New Design Document']");

        internal By CancelBtnUploadPage_ByLocator => By.Id("btnCancel");

        internal By SaveOnlyBtnUploadPage_ByLocator => By.Id("btnSave");

        internal By SaveForwardBtnUploadPage_ByLocator => By.Id("btnSaveForward");

        internal By SaveOnlyBtn_ByLocator => By.XPath("//div[@class='k-content k-state-active']//button[contains(@id,'btnSave_')]");

        internal By Table_ForwardBtn_ByLocator => By.XPath("//button[@id='btnSaveForward']");

        internal By SaveForwardBtn_ByLocator => By.XPath("//div[@class='k-content k-state-active']//button[contains(@id,'btnSaveForward_')]");

        internal By BackToListBtn_ByLocator => By.XPath("//button[text()='Back To List']");

        internal By BackToListBtn_InTable_ByLocator => By.XPath("//a[text()=' Back to List']");

        internal By Btn_Refresh_ByLocator => By.XPath("//a[@aria-label='Refresh']");

        internal By Btn_Cancel_ByLocator => By.Id("btnCancelSave");

        internal By Btn_Forward_ByLocator => By.XPath("//button[@id='btnSaveForward']");

        internal By Btn_ShowFileList_ByLocator => By.XPath("//button[contains(@class,'showFileList')]");

        internal By Btn_PDF_ByLocator => By.Id("PdfExportLink");

        internal By Btn_XLS_ByLocator => By.Id("CsvExportLink");

        private string GetTblColumnIndex(PkgComments_TblHeader tableHeader)
            => GetAttribute(By.XPath($"//thead[@role='rowgroup']/tr/th[@data-field='{tableHeader.GetString()}']"), "data-index");

        private By GetTblBtnByLocator(PkgComments_Button rowButton, int rowID)
            => By.XPath($"//tbody/tr[{rowID}]/td[1]/a[text()='{rowButton.ToString()}']");

        internal void Click_TblRowBtn(PkgComments_Button rowButton, bool clickBtnForLatest = true, int rowID = 1)
        {
            By locator = By.XPath("//div[@class='k-grid-content']//tbody/tr");

            try
            {
                rowID = clickBtnForLatest
                    ? GetElementsCount(locator)
                    : rowID;

                JsClickElement(GetTblBtnByLocator(rowButton, rowID));
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        internal void Click_UniqueTblBtn(string btnClass)
        {
            ClickElement(By.XPath($"//a[contains(@class, '{btnClass}')]"));
            WaitForPageReady();
        }

        internal void StoreDesignDocTitleAndNumber()
        {
            //MiniGuid guid = GenerateRandomGuid();

            //string docKey = $"{tenantName}{GetTestName()}";
            //docTitleKey = $"{docKey}_DsgnDocTtl";
            //docNumberKey = $"{docKey}_DsgnDocNumb";
            //docTitleKey = $"DsgnDocTtl";
            //docNumberKey = $"DsgnDocNumb";

            //CreateVar(docTitleKey, docTitleKey);
            //CreateVar(docNumberKey, guid);
            designDocTitle = GetVar("DsgnDocTtl");
            designDocNumber = GetVar("DsgnDocNumb");
            Console.WriteLine($"#####Title: {designDocTitle}\nNumber: {designDocNumber}");
        }

    }

    #endregion DesignDocument Generic class

    #region Workflow Interface class

    public interface IDesignDocument
    {
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

        void EnterDesignDocTitleAndNumber();

        void EnterRegularCommentAndDrawingPageNo();

        void EnterTextInCommentField(Enum commentType, int commentTabNumber = 1);

        void EnterNoComment();

        void EnterResponseCommentAndAgreeResponseCode();

        void EnterResponseCommentAndDisagreeResponseCode();

        void FilterDocNumber(string filterByValue = "");

        void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse();

        void Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse();

        bool VerifyItemStatusIsClosed();

        void ClickBtn_BackToList();

        void ClickBtn_SaveOnly();

        void ClickBtn_SaveForward();

        void ClickBtn_UploadNewDesignDoc();

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

        internal DesignDocument DesignDoc_Base => new DesignDocument();

        public virtual void SelectDDL_ReviewType(int selectionIndex)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ReviewType, selectionIndex);

        public virtual void SelectDDL_Reviewer<T>(T selectionIndexOrReviewerName, bool useContainsFilter)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.Reviewer, selectionIndexOrReviewerName, useContainsFilter);

        public virtual void SelectDDL_CommentType(int selectionIndex)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.CommentType, selectionIndex);

        public virtual void SelectDDL_Category(int selectionIndex)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.Category, selectionIndex);

        public virtual void SelectDDL_Discipline(int selectionIndex)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.Discipline, selectionIndex);

        public virtual void ClickBtn_AddComment()
            => DesignDoc_Base.Click_UniqueTblBtn("k-grid-add");

        public virtual void ClickBtn_Update()
            => DesignDoc_Base.Click_UniqueTblBtn("k-grid-update");

        public virtual void ClickBtn_Cancel()
            => DesignDoc_Base.Click_UniqueTblBtn("k-grid-cancel");

        public virtual void ClickBtn_CommentsTblRow_Edit(bool clickBtnForLatest = true, int rowID = 1)
            => DesignDoc_Base.Click_TblRowBtn(PkgComments_Button.Edit, clickBtnForLatest, rowID);

        public virtual void Click_TblBtn_Delete(bool clickBtnForLatest = true, int rowID = 1)
            => DesignDoc_Base.Click_TblRowBtn(PkgComments_Button.Delete, clickBtnForLatest, rowID);
        
        public virtual void Click_TblBtn_Files(bool clickBtnForLatest = true, int rowID = 1)
            => DesignDoc_Base.Click_TblRowBtn(PkgComments_Button.Files, clickBtnForLatest, rowID);

        public virtual void Click_TblBtn_Details(bool clickBtnForLatest = true, int rowID = 1)
            => DesignDoc_Base.Click_TblRowBtn(PkgComments_Button.Details, clickBtnForLatest, rowID);

        public virtual void ClickBtn_UploadNewDesignDoc() => ClickElement(DesignDoc_Base.UploadNewDesignDoc_ByLocator);

        public virtual void ClickBtn_BackToList()
        {
            JsClickElement(DesignDoc_Base.BackToListBtn_ByLocator);
            WaitForPageReady();
        }

        public virtual void ClickBtn_SaveOnly()
        {
            ClickElement(DesignDoc_Base.SaveOnlyBtn_ByLocator);
            WaitForPageReady();
        }

        public virtual void ClickBtn_SaveForward()
        {
            JsClickElement(DesignDoc_Base.SaveForwardBtn_ByLocator);
            WaitForPageReady();
        }

        public virtual void CreateDocument()
        {
            WaitForPageReady();
            ClickElement(DesignDoc_Base.UploadNewDesignDoc_ByLocator);
            EnterDesignDocTitleAndNumber();
            UploadFile("test.xlsx");
            ClickElement(DesignDoc_Base.SaveForwardBtnUploadPage_ByLocator);
            WaitForPageReady();
        }

        internal string SetCommentStamp(DesignDocDetails_InputFields inputFieldEnum, int commentTabIndex)
            => $"{inputFieldEnum.GetString()}{(commentTabIndex - 1).ToString()}_";

        public virtual void SelectRegularCommentReviewType(int commentTabNumber = 1)
            => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ReviewType, commentTabNumber), 3);

        public virtual void SelectNoCommentReviewType(int commentTabNumber = 1)
            => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ReviewType, commentTabNumber), 1);

        public virtual void SelectCommentType(int commentTypeTabNumber = 1)
            => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.CommentType, commentTypeTabNumber), 1);

        public virtual void SelectOrganization(int disciplineNumber = 1)
        {
        }

        public virtual void SelectDiscipline(int disciplineNumber = 1)
            => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.Discipline, disciplineNumber), 1);

        public virtual void SelectCategory(int categoryNumber = 1)
            => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.Category, categoryNumber), 1);

        public virtual void SelectAgreeResolutionCode(int commentTabNumber = 1)
            => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ResolutionStamp, commentTabNumber), 1); //check the index, UI not working so need to confirm later

        public virtual void SelectAgreeResponseCode(int commentTabNumber = 1)
            => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ResponseCode, commentTabNumber), 2); //check the index, UI not working so need to confirm later

        public virtual void SelectDisagreeResponseCode(int commentTabNumber = 1)
            => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ResponseCode, commentTabNumber), 3);//check the index, UI not working so need to confirm later

        public virtual void SelectDisagreeResolutionCode(int commentTabNumber = 1)
            => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ResolutionStamp, commentTabNumber), 2);//check the index, UI not working so need to confirm later

        public virtual void SelectDDL_ClosingStamp(int commentTabNumber = 1)
            => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ClosingStamp, commentTabNumber), 1);

        public virtual void SelectDDL_VerificationCode(int selectionIndex = 1)
        {
        }

        public virtual void EnterVerifiedDate(string shortDate)
        {
        }

        private void SetDesignDocTitleAndNumber()
            => DesignDoc_Base.StoreDesignDocTitleAndNumber();

        public void EnterDesignDocTitleAndNumber()
        {
            //login as uploading user IQFRecordsmgr (for SG and SH249) and IQFuser(GLX and Garnet)
            SetDesignDocTitleAndNumber();
            EnterText(PageHelper.GetTextInputFieldByLocator(DesignDocDetails_InputFields.Title), designDocTitle);
            EnterText(PageHelper.GetTextInputFieldByLocator(DesignDocDetails_InputFields.DocumentNumber), designDocNumber);
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
                FilterTableColumnByValue(ColumnName.Number, designDocNumber);
            }
            catch (Exception e)
            {
                LogError(e.StackTrace);
                throw e;
            }

        }

        public virtual void EnterTextInCommentField(Enum commentType, int commentTabNumber = 1)
        {
            try
            {
                string text = "Comment 123";
                By commentTypeLocator = null;

                Type commentTypeEnum = commentType.GetType();

                if (commentTypeEnum == typeof(CommentType))
                {
                    commentTypeLocator = By.Id($"{commentType.GetString()}{commentTabNumber - 1}_");
                }
                else if (commentTypeEnum == typeof(CommentType_InTable))
                {
                    commentTypeLocator = By.Id($"{commentType.GetString()}");
                }
                else if (commentTypeEnum == typeof(PkgComments_TblHeader))
                {
                    commentTypeLocator = GetTextInputFieldByLocator(commentType);
                }

                ScrollToElement(commentTypeLocator);

                if (commentType.Equals(PkgComments_TblHeader.VerifiedDate))
                {
                    text = GetShortDate();
                    EnterText(GetTextInputFieldByLocator(commentType), text);
                }
                else
                {
                    if (commentType.Equals(CommentType.ContractReferenceInput) || commentType.Equals(CommentType_InTable.ContractReferenceInput))
                    {
                        text = "Contract Reference 123";
                    }
                    else if (commentType.Equals(CommentType.DrawingPageNumberInput) || commentType.Equals(CommentType_InTable.DrawingPageNumberInput))
                    {
                        text = "Drawing PageNumber 123";
                    }
                    else if (commentType.Equals(CommentType.CommentResponseInput) || commentType.Equals(CommentType_InTable.CommentResponseInput))
                    {
                        text = "Comment Response 123";
                    }
                    else if (commentType.Equals(CommentType.CommentResolutionInput) || commentType.Equals(CommentType_InTable.CommentResolutionInput))
                    {
                        text = "Comment Resolution 123";
                    }
                    else if (commentType.Equals(CommentType.CommentClosingInput) || commentType.Equals(CommentType_InTable.CommentClosingInput))
                    {
                        text = "Closing Comment 123";
                    }
                    else if (commentType.Equals(CommentType_InTable.VerificationNotes))
                    {
                        text = "Verification Notes 123";
                    }
                    else if (commentType.Equals(CommentType_InTable.ReviewerName) || commentType.Equals(CommentType_InTable.VerifiedBy))
                    {
                        text = GetCurrentUser();
                    }

                    EnterText(commentTypeLocator, text);
                }

                LogStep($"Entered '{text}' in {commentType.ToString()} field : {commentTypeLocator}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
        }

        public virtual void EnterRegularCommentAndDrawingPageNo()
        {
            //login as commenting user (SG- IQFuser, DoTuser | SH249-- IQFUser | Garenet and GLX-- DOTUser)
            SelectRegularCommentReviewType();
            EnterTextInCommentField(CommentType.CommentInput);
            EnterTextInCommentField(CommentType.DrawingPageNumberInput);
            //EnterText(By.Id("Comment_DrawingPageNumber_0_"), "Draw123");
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
            EnterTextInCommentField(CommentType.CommentResponseInput);
            SelectAgreeResponseCode(); //Disagree then different workflow
            ClickBtn_SaveOnly();
        }

        public virtual void EnterResponseCommentAndDisagreeResponseCode()
        {
            // Login as user to make response comment (All tenants - DevUser)
            EnterTextInCommentField(CommentType.CommentResponseInput);
            SelectDisagreeResponseCode(); //agree then different workflow
            ClickBtn_SaveOnly();
        }

        public virtual void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            LogInfo($"<<-- WORKFLOW ({tenantName}): EnterResolutionCommentAndResolutionCodeforDisagreeResponse -->>");
            ClickTab_Requires_Resolution();
            SortTable_Descending();
            ClickEnterBtnForRow();
            //Thread.Sleep(2000);

            // Login as user to make resolution comment (All tenants - DevAdmin)
            EnterTextInCommentField(CommentType.CommentResolutionInput);
            SelectDisagreeResolutionCode(); //
            ClickBtn_SaveOnly();
            //Thread.Sleep(2000);
            ClickBtn_BackToList();
        }

        public virtual void Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            ClickTab_Requires_Resolution();
            SortTable_Descending();
            ClickEnterBtnForRow();
            ClickBtn_SaveForward();
        }

        //public virtual void EnterClosingCommentAndCode()
        //{
        //    WaitForPageReady();
        //    EnterTextInCommentField(CommentType.CommentClosingInput);
        //    SelectDDL_ClosingStamp();
        //    ClickBtn_SaveOnly();
        //    ClickBtn_SaveForward();
        //}

        public virtual void _LoggedInUserUploadsDesignDocument()
        {
            // Login as user to make resolution comment (All tenants - DevAdmin)
            EnterTextInCommentField(CommentType.CommentResolutionInput);
            SelectAgreeResolutionCode(); //
            ClickSave();
            //wait for saveforward to load
            ClickSubmitForward();
        }

        private bool VerifyRequiredFieldErrorMsg(string errorMsg)
            => ElementIsDisplayed(By.XPath($"//li[text()='{errorMsg}']"));

        public virtual bool VerifyTitleFieldErrorMsgIsDisplayed()
            => VerifyRequiredFieldErrorMsg("Submittal Title is required.");

        public virtual bool VerifyDocumentNumberFieldErrorMsgIsDisplayed()
            => VerifyRequiredFieldErrorMsg("Submittal Number is required.");

        public virtual bool VerifyUploadFileErrorMsgIsDisplayed()
            => VerifyRequiredFieldErrorMsg("At least one file must be added.");

        public virtual bool VerifyItemStatusIsClosed()
        {
            SelectTab(TableTab.Closed);
            return VerifyRecordIsDisplayed(ColumnName.Number, designDocNumber);
        }

        public virtual void SelectTab(TableTab tableTab)
            => ClickTab(tableTab);

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

        public virtual void SortTable_Descending() => SortColumnDescending(ColumnName.Action);

        public virtual void SortTable_Ascending() => SortColumnAscending(ColumnName.Action);

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
                        activeTabNotDisplayed = ElementIsDisplayed(By.XPath("//div[@class='k-content k-state-active']"));
                    }
                }
                while (activeTabNotDisplayed);
            }
        }

        public virtual void ClickCommentTabNumber(int commentTabNumber)
        {
            WaitForActiveCommentTab();
            Kendo.ClickCommentTab(commentTabNumber);
        }

        public abstract void ScrollToLastColumn();

        public abstract void ScrollToFirstColumn();
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
            WaitForPageReady();
            string currentUser = GetCurrentUser();
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
            ClickTab(tabName);
        }

        public override void SelectRegularCommentReviewType(int commentTabNumber = 1)
            => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ReviewType, commentTabNumber), 2);

        public override void SelectNoCommentReviewType(int commentTabNumber = 1)
            => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ReviewType, commentTabNumber), 3);
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
            LogInfo($"<<-- WORKFLOW ({tenantName}): EnterResolutionCommentAndResolutionCodeforDisagreeResponse -->>");

            ClickTab_Pending_Resolution();
            SortTable_Descending();
            ClickEnterBtnForRow();
            //Thread.Sleep(2000);
            EnterTextInCommentField(CommentType.CommentResolutionInput);
            SelectDisagreeResolutionCode(); //
            ClickBtn_SaveOnly();
            //Thread.Sleep(2000);
            ClickBtn_BackToList();
        }

        public override void Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            ClickTab_Pending_Resolution();
            SortTable_Descending();
            ClickEnterBtnForRow();
            ClickBtn_SaveForward();
        }
    }

    #endregion Implementation specific to GLX

    #region Implementation specific to SH249

    public class DesignDocument_SH249 : DesignDocument
    {
        public DesignDocument_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override void EnterNoComment()
        {
            WaitForPageReady();
            ClickBtn_AddComment();
            SelectNoCommentReviewType();
            ClickBtn_Update();
        }

        public override void EnterRegularCommentAndDrawingPageNo()
        {
            //login as commenting user (SG- IQFuser, DoTuser | SH249-- IQFUser | Garenet and GLX-- DOTUser)
            WaitForPageReady();
            ClickBtn_AddComment();
            SelectRegularCommentReviewType();
            EnterTextInCommentField(CommentType_InTable.DrawingPageNumberInput);
            EnterTextInCommentField(CommentType_InTable.ContractReferenceInput);
            EnterTextInCommentField(CommentType_InTable.CommentInput);
            SelectDiscipline();
            SelectCategory();
            SelectCommentType();
            SelectDDL_Reviewer(GetCurrentUser(), true);
            ClickBtn_Update();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            //This will add response and resolution both together for 249 tenant
            WaitForPageReady();
            ClickBtn_CommentsTblRow_Edit();
            EnterTextInCommentField(CommentType.CommentResponseInput);
            EnterTextInCommentField(CommentType.CommentResolutionInput);
            SelectDisagreeResolutionCode();
            ClickBtn_Update();
            ClickBtn_SaveForward();
        }


        public override void ClickBtn_BackToList()
        {
            JsClickElement(DesignDoc_Base.BackToListBtn_InTable_ByLocator);
            WaitForPageReady();
        }

        public override void ClickBtn_SaveForward()
        {
            ClickElement(DesignDoc_Base.Table_ForwardBtn_ByLocator);
            WaitForPageReady();
        }

        public override void SelectRegularCommentReviewType(int selectionIndex = 3)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ReviewType, selectionIndex);

        public override void SelectNoCommentReviewType(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ReviewType, selectionIndex);

        public override void SelectCommentType(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.CommentType, selectionIndex);

        public override void SelectDiscipline(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.Discipline, selectionIndex);

        public override void SelectCategory(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.Category, selectionIndex);

        public override void SelectAgreeResolutionCode(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ResolutionStamp, selectionIndex);

        public override void SelectAgreeResponseCode(int selectionIndex = 2)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ResponseCode, selectionIndex);

        public override void SelectDisagreeResponseCode(int selectionIndex = 3)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ResponseCode, selectionIndex);

        public override void SelectDisagreeResolutionCode(int selectionIndex = 2)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ResolutionStamp, selectionIndex);

        public override void SelectDDL_ClosingStamp(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ClosingStamp, selectionIndex);

    }

    #endregion Implementation specific to SH249

    #region Implementation specific to SGWay

    public class DesignDocument_SGWay : DesignDocument
    {
        public DesignDocument_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override void EnterRegularCommentAndDrawingPageNo()
        {
            SelectRegularCommentReviewType();
            SelectCommentType();
            SelectCategory();
            SelectDiscipline();
            EnterTextInCommentField(CommentType.CommentInput);
            EnterTextInCommentField(CommentType.ContractReferenceInput);
            EnterTextInCommentField(CommentType.DrawingPageNumberInput);
            ClickBtn_SaveOnly();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            EnterTextInCommentField(CommentType.CommentResponseInput);
            SelectDisagreeResponseCode();
            ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterTextInCommentField(CommentType.CommentResponseInput, commentTabNumber);
            SelectDisagreeResponseCode(commentTabNumber);
            ClickBtn_SaveOnly();
        }

        public override void EnterResponseCommentAndAgreeResponseCode()
        {
            EnterTextInCommentField(CommentType.CommentResponseInput);
            SelectAgreeResponseCode();
            ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterTextInCommentField(CommentType.CommentResponseInput, commentTabNumber);
            SelectAgreeResponseCode(commentTabNumber);
            ClickBtn_SaveOnly();
        }

        public override void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            LogInfo($"<<-- WORKFLOW ({tenantName}): EnterResolutionCommentAndResolutionCodeforDisagreeResponse -->>");
            EnterTextInCommentField(CommentType.CommentResolutionInput);
            SelectDisagreeResolutionCode(); //
            ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterTextInCommentField(CommentType.CommentResolutionInput, commentTabNumber);
            SelectDisagreeResolutionCode(commentTabNumber);
            ClickBtn_SaveOnly();
        }

    }

    #endregion Implementation specific to SGWay

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

    #region Implementation specific to LAX

    public class DesignDocument_LAX : DesignDocument
    {
        public DesignDocument_LAX(IWebDriver driver) : base(driver)
        {
        }

        public override void ClickBtn_BackToList()
        {
            JsClickElement(DesignDoc_Base.BackToListBtn_InTable_ByLocator);
            WaitForPageReady();
        }

        public override void ClickBtn_SaveForward()
        {
            JsClickElement(DesignDoc_Base.Table_ForwardBtn_ByLocator);
            WaitForPageReady();
        }

        public override void SelectRegularCommentReviewType(int selectionIndex = 3)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ReviewType, selectionIndex);

        public override void SelectNoCommentReviewType(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ReviewType, selectionIndex);

        public override void SelectCommentType(int selectionIndex = 1)
        {
            try
            {
                ExpandAndSelectFromDDList(PkgComments_TblHeader.DocType, selectionIndex);
            }
            catch (ElementNotInteractableException e)
            {
                log.Debug(e.Message);
            }
            catch (Exception)
            { }
        }

        public override void SelectOrganization(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.Org, selectionIndex);

        public override void SelectDiscipline(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.Discipline, selectionIndex);

        public override void SelectCategory(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.Category, selectionIndex);

        public override void SelectAgreeResolutionCode(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ResolutionStamp, selectionIndex);

        public override void SelectAgreeResponseCode(int selectionIndex = 2)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ResponseCode, selectionIndex);

        public override void SelectDisagreeResponseCode(int selectionIndex = 3)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ResponseCode, selectionIndex);

        public override void SelectDisagreeResolutionCode(int selectionIndex = 2)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ResolutionStamp, selectionIndex);

        public override void SelectDDL_ClosingStamp(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.ClosingStamp, selectionIndex);

        public override void SelectDDL_VerificationCode(int selectionIndex = 1)
            => ExpandAndSelectFromDDList(PkgComments_TblHeader.VerificationCode, selectionIndex);

        public override void EnterVerifiedDate(string shortDate = "01/01/2019")
        {
            shortDate = shortDate.Equals("01/01/2019")
                ? GetShortDate()
                : shortDate;

            EnterText(GetTextInputFieldByLocator(PkgComments_TblHeader.VerifiedDate), shortDate);
        }

        public override void EnterRegularCommentAndDrawingPageNo()
        {
            WaitForPageReady();
            ClickBtn_AddComment();
            SelectCommentType();
            EnterTextInCommentField(CommentType_InTable.ReviewerName);
            SelectOrganization();
            EnterTextInCommentField(CommentType_InTable.ContractReferenceInput);
            EnterTextInCommentField(CommentType_InTable.CommentInput);
            EnterTextInCommentField(CommentType_InTable.DrawingPageNumberInput);
            SelectDiscipline();
            WaitForPageReady();
        }

        public override void EnterResponseCommentAndAgreeResponseCode()
        {
            WaitForPageReady();
            ClickBtn_CommentsTblRow_Edit();
            EnterTextInCommentField(CommentType_InTable.CommentResponseInput);
            EnterTextInCommentField(CommentType_InTable.CommentResolutionInput);
            SelectAgreeResolutionCode();
            ClickBtn_Update();
            ClickBtn_SaveForward();
        }
    }

    #endregion Implementation specific to LAX
}