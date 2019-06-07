using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public interface IDesignDocument
    {
        By BackToListBtn_ByLocator { get; }
        //By BackToListBtn_InTable_ByLocator { get; }
        By Btn_Cancel_ByLocator { get; }
        By Btn_Forward_ByLocator { get; }
        By Btn_PDF_ByLocator { get; }
        By Btn_Refresh_ByLocator { get; }
        By Btn_ShowFileList_ByLocator { get; }
        By Btn_XLS_ByLocator { get; }
        By CancelBtnUploadPage_ByLocator { get; }
        IList<Enum> NoCommentFieldsList { get; set; }
        IList<Enum> RegularCommentFieldsList { get; set; }
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
        string EnterTextInCommentField(Enum commentField, int commentTabNumber = 1);
        void EnterVerifiedDate(string shortDate = "01/01/2019");
        void FilterDocNumber(string filterByValue = "");
        IList<KeyValuePair<Enum, string>> GetCommentEntryFieldKeyValuePairs();
        IList<Enum> GetCommentEntryFieldsList(DesignDocument.ReviewType reviewType);
        string GetCurrentReviewerType();
        IList<DesignDocument.DesignDocEntryFieldType> GetDesignDocCreatePgEntryFieldsList();
        IList<DesignDocument.DesignDocHeaderType> GetDesignDocDetailsHeadersList();
        IList<KeyValuePair<DesignDocument.DesignDocEntryFieldType, string>> GetDesignDocEntryFieldKeyValuePairs();
        string GetDesignDocStatus();
        string GetHeaderValue(DesignDocument.DesignDocHeaderType docHeader);
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
        void SelectDDL_Reviewer<T>(T selectionIndexOrReviewerName, bool useContainsFilter);
        void SelectDDL_ReviewType(int selectionIndex);
        void SelectDDL_VerificationCode(int selectionIndex = 1);
        void SelectDisagreeResolutionCode(int commentTabNumber = 1);
        void SelectDisagreeResponseCode(int commentTabNumber = 1);
        void SelectDiscipline(int disciplineNumber = 1);
        void SelectNoCommentReviewType(int commentTabNumber = 1);
        void SelectOrganization(int selectionIndex = 1);
        void SelectRegularCommentReviewType(int commentTabNumber = 1);
        void SelectTab(DesignDocument.TableTab tableTab);
        T SetClass<T>(IWebDriver driver);
        void SetDesignDocStatus<T>(T tableTabOrWorkflow);
        void SortTable_Ascending();
        void SortTable_Descending();
        void VerifyDesignDocDetailsHeader();
        bool VerifyDocumentNumberFieldErrorMsgIsDisplayed();
        void VerifyItemStatusIsClosed();
        bool VerifyTitleFieldErrorMsgIsDisplayed();
        bool VerifyUploadFileErrorMsgIsDisplayed();
        void WaitForActiveCommentTab();
        void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse();
        void Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse();
    }
}