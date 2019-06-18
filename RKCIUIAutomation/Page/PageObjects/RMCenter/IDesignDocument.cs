using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.DesignDocument;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public interface IDesignDocument
    {
        By BackToListBtn_ByLocator { get; }
        By Btn_Cancel_ByLocator { get; }
        By Btn_Forward_ByLocator { get; }
        By Btn_PDF_ByLocator { get; }
        By Btn_Refresh_ByLocator { get; }
        By Btn_ShowFileList_ByLocator { get; }
        By Btn_XLS_ByLocator { get; }
        By CancelBtnUploadPage_ByLocator { get; }
        IList<CommentFieldType> NoCommentFieldsList { get; set; }
        IList<CommentFieldType> RegularCommentFieldsList { get; set; }
        By SaveForwardBtn_ByLocator { get; }
        By SaveForwardBtnUploadPage_ByLocator { get; }
        By SaveOnlyBtn_ByLocator { get; }
        By SaveOnlyBtnUploadPage_ByLocator { get; }
        By Table_ForwardBtn_ByLocator { get; }
        By UploadNewDesignDoc_ByLocator { get; }

        void ClickBtn_AddComment();
        void ClickBtn_BackToList();
        void ClickBtn_Cancel();
        void ClickBtn_CommentsTblRow_Delete(bool clickBtnForLatestRow = true, int rowID = 1);
        void ClickBtn_CommentsTblRow_Details(bool clickBtnForLatestRow = true, int rowID = 1);
        void ClickBtn_CommentsTblRow_Edit(bool clickBtnForLatestRow = true, int rowID = 1);
        void ClickBtn_CommentsTblRow_Files(bool clickBtnForLatestRow = true, int rowID = 1);
        void ClickBtn_SaveForward();
        void ClickBtn_SaveOnly();
        void ClickBtn_Update();
        void ClickCommentTabNumber(int commentTabNumber);
        void ClickTab_Closed();
        void ClickTab_Comment();
        void ClickTab_Creating();
        void ClickTab_Pending_Closing();
        void ClickTab_Pending_Comment();
        void ClickTab_Pending_Resolution();
        void ClickTab_Pending_Response();
        void ClickTab_Requires_Closing();
        void ClickTab_Requires_Comment();
        void ClickTab_Requires_Resolution();
        void ClickTab_Requires_Response();
        void ClickTab_Response();
        void ClickTab_Verification();
        void CreateDocument();
        void EnterDesignDocTitleAndNumber();
        void EnterNoComment();
        void EnterRegularCommentAndDrawingPageNo();
        void EnterResponseCommentAndAgreeResponseCode();
        void EnterResponseCommentAndDisagreeResponseCode();
        string EnterCommentResponse(CommentFieldType commentField, int commentTabNumber = 1, bool updateKVPair = true);
        void EnterVerifiedDate(string shortDate = "01/01/2019");
        void FilterDocNumber(string filterByValue = "");
        IList<KeyValuePair<CommentFieldType, string>> CreateCommentEntryFieldKVPairsList();
        IList<CommentFieldType> GetCommentEntryFieldsList(ReviewType reviewType);
        By GetCommentFieldValueXPath_ByLocator(CommentFieldType commentField);
        string GetCurrentReviewerType();
        string GetActiveCommentTabGroupName();
        IList<DesignDocEntryFieldType> GetDesignDocCreatePgEntryFieldsList();
        IList<DesignDocHeaderType> GetDesignDocDetailsHeadersList();
        IList<KeyValuePair<DesignDocEntryFieldType, string>> CreateDesignDocEntryFieldKVPairsList();
        string GetDesignDocStatus();
        string GetHeaderValue(DesignDocHeaderType docHeader);
        void PopulateAllCreatePgEntryFields();
        void ScrollToFirstColumn();
        void ScrollToLastColumn();
        void SelectAgreeResolutionCode(int commentTabNumber = 1);
        void SelectAgreeResponseCode(int commentTabNumber = 1);
        void SelectCategory(int categoryNumber = 1);
        void SelectCommentType(int commentTypeTabNumber = 1);
        void SelectDDL_Category(int selectionIndex);
        void SelectDDL_ClosingStamp(int commentTabNumber = 1);
        void SelectDDL_CommentType(int selectionIndex);
        void SelectDDL_Discipline(int selectionIndex);
        void SelectDDL_Reviewer<T>(T selectionIndexOrReviewerName, bool useContainsOperator);
        void SelectDDL_ReviewType(int selectionIndex);
        void SelectDDL_VerificationCode(int selectionIndex = 1);
        void SelectDisagreeResolutionCode(int commentTabNumber = 1);
        void SelectDisagreeResponseCode(int commentTabNumber = 1);
        void SelectDiscipline(int disciplineNumber = 1);
        void SelectNoCommentReviewType(int commentTabNumber = 1);
        void SelectOrganization(int selectionIndex = 1);
        void SelectRegularCommentReviewType(int commentTabNumber = 1);
        void SelectTab(TableTab tableTab);
        T SetClass<T>(IWebDriver driver);
        void SetDesignDocStatus<T>(T tableTabOrWorkflow);
        void SortTable_Ascending();
        void SortTable_Descending();
        void VerifyDesignDocDetailsHeader();
        void VerifyCommentFieldValues(ReviewType reviewType);
        bool VerifyDocumentNumberFieldErrorMsgIsDisplayed();
        void VerifyItemStatusIsClosed(ReviewType reviewType);
        bool VerifyTitleFieldErrorMsgIsDisplayed();
        bool VerifyUploadFileErrorMsgIsDisplayed();
        void WaitForActiveCommentTab();
        void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse();
        void Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse();
    }
}