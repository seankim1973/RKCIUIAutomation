﻿using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using MiniGuids;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.DesignDocument;
using RKCIUIAutomation.Test;
using System;
using System.Threading;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    #region DesignDocument Generic class
    public class DesignDocument : DesignDocument_Impl
    {
        public DesignDocument(){}
        public DesignDocument(IWebDriver driver) => this.Driver = driver;

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
            [StringValue("Comment_ClosingStampId_")] ClosingStamp
        }

        public enum TableTab
        {
            [StringValue("Creating")] Creating,
            //[StringValue("IQF Creating")] IQF_Creating,   
            
            //[StringValue("DEV Pending Comment")] DEV_Pending_Comment,
            //[StringValue("DOT Requires Comment")] DOT_Requires_Comment,
            //[StringValue("IQF Pending Comment")] IQF_Pending_Comment,
            [StringValue("Pending Comment")] Pending_Comment,
            [StringValue("Requires Comment")] Requires_Comment,

            //[StringValue("DEV Requires Response")] DEV_Requires_Response, //Garnet
            //[StringValue("DOT Pending Response")] DOT_Pending_Response,
            //[StringValue("IQF Pending Response")] IQF_Pending_Response,
            [StringValue("Pending Response")] Pending_Response,
            [StringValue("Requires Response")] Requires_Response,
                        
            //[StringValue("DEV Requires Resolution")] DEV_Requires_Resolution, //Garnet
            //[StringValue("DOT Pending Resolution")] DOT_Pending_Resolution,
            //[StringValue("IQF Pending Resolution")] IQF_Pending_Resolution,
            [StringValue("Pending Resolution")] Pending_Resolution,
            [StringValue("Requires Resolution")] Requires_Resolution,

            [StringValue("Pending Closing")] Pending_Closing,
            [StringValue("Requires Closing")] Requires_Closing,

            [StringValue("Closed")] Closed,
            //[StringValue("DOT Closed")] DOT_Closed,
            //[StringValue("DEV Closed")] DEV_Closed,
            //[StringValue("IQF Closed")] IQF_Closed
        }

        public enum ColumnName
        {
            [StringValue("SubmittalNumber")] Number,
            [StringValue("SubmittalTitle")] Title,
            [StringValue("IqfDeadlineDate")] Deadline,
            [StringValue("DocumentId")] Action
        }

    }
    #endregion  <-- end of DesignDocument Generic Class

    #region Workflow Interface class
    public interface IDesignDocument
    {
        void SelectTab(TableTab tableTab);
        void SortTable_Descending();
        void SortTable_Ascending();
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
        void ForwardComment();
        void EnterResponseCommentAndAgreeResponseCode();
        void EnterResponseCommentAndDisagreeResponseCode();
        void ForwardResponseComment();
        void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse();
        void Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse();
        bool VerifyItemStatusIsClosed();
        void ClickBtn_BackToList();
        void ClickBtn_SaveOnly();
        void ClickBtn_SaveForward();
        void ClickBtn_UploadNewDesignDoc();
        void SelectRegularCommentReviewType(int commentTabNumber);
        bool VerifyTitleFieldErrorMsgIsDisplayed();
        bool VerifyDocumentNumberFieldErrorMsgIsDisplayed();
        bool VerifyUploadFileErrorMsgIsDisplayed();

    }
    #endregion <-- end of Workflow Interface class

    #region Common Workflow Implementation class
    public abstract class DesignDocument_Impl : PageBase, IDesignDocument
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
                LogInfo($"###### using DesignDocument_SGWay instance ###### ");
                instance = new DesignDocument_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                LogInfo($"###### using  DesignDocument_SH249 instance ###### ");
                instance = new DesignDocument_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                LogInfo($"###### using  DesignDocument_Garnet instance ###### ");
                instance = new DesignDocument_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                LogInfo($"###### using  DesignDocument_GLX instance ###### ");
                instance = new DesignDocument_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                LogInfo($"###### using  DesignDocument_I15South instance ###### ");
                instance = new DesignDocument_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                LogInfo($"###### using DesignDocument_I15Tech instance ###### ");
                instance = new DesignDocument_I15Tech(driver);
            }

            return instance;
        }

        [ThreadStatic]
        private static string designDocTitle;

        [ThreadStatic]
        private static string designDocNumber;

        [ThreadStatic]
        private static string docTitleKey;

        [ThreadStatic]
        private static string docNumberKey;

        private MiniGuid guid;

        private By UploadNewDesignDoc_ByLocator => By.XPath("//a[text()='Upload New Design Document']");
        private By CancelBtnUploadPage_ByLocator => By.Id("btnCancel");
        private By SaveOnlyBtnUploadPage_ByLocator => By.Id("btnSave");
        private By SaveForwardBtnUploadPage_ByLocator => By.Id("btnSaveForward");
        private By SaveOnlyBtn_ByLocator => By.XPath("//div[@class='k-content k-state-active']//button[contains(@id,'btnSave_')]");
        private By SaveForwardBtn_ByLocator => By.XPath("//div[@class='k-content k-state-active']//button[contains(@id,'btnSaveForward_')]");
        private By BackToListBtn_ByLocator => By.XPath("//button[text()='Back To List']");
        //private By SaveOnlyBtnComment1_ByLocator => By.Id("btnSave_0_");
        //private By SaveForwardBtnComment1_ByLocator => By.Id("btnSaveForward_0_");


        public virtual void ClickBtn_UploadNewDesignDoc() => ClickElement(UploadNewDesignDoc_ByLocator);
        public virtual void ClickBtn_BackToList()
        {
            IWebElement elem = GetElement(BackToListBtn_ByLocator);

            if (elem != null)
            {
                elem.Click();
                LogInfo($"Clicked element - {BackToListBtn_ByLocator}");
            }
        }
        public virtual void ClickBtn_SaveOnly() => ClickElement(SaveOnlyBtn_ByLocator);
        public virtual void ClickBtn_SaveForward() => ClickElement(SaveForwardBtn_ByLocator);

        public virtual void CreateDocument()
        {
            ClickElement(UploadNewDesignDoc_ByLocator);
            EnterDesignDocTitleAndNumber();
            UploadFile("test.xlsx");
            ClickElement(SaveForwardBtnUploadPage_ByLocator);
            WaitForPageReady();
        }

        internal string SetCommentStamp(DesignDocDetails_InputFields inputFieldEnum, int commentTabIndex) => $"{inputFieldEnum.GetString()}{(commentTabIndex - 1).ToString()}_";
        public virtual void SelectRegularCommentReviewType(int commentTabNumber = 1) => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ReviewType, commentTabNumber), 3);
        public void SelectNoCommentReviewType(int commentTabNumber = 1) => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ReviewType, commentTabNumber), 1);
        public void SelectAgreeResponseCode(int commentTabNumber = 1) => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ResponseCode, commentTabNumber), 2); //check the index, UI not working so need to confirm later
        public void SelectDisagreeResponseCode(int commentTabNumber = 1) => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ResponseCode, commentTabNumber), 3);//check the index, UI not working so need to confirm later
        public void SelectAgreeResolutionCode(int commentTabNumber = 1) => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ResolutionStamp, commentTabNumber), 1); //check the index, UI not working so need to confirm later
        public void SelectDisagreeResolutionCode(int commentTabNumber = 1) => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ResolutionStamp, commentTabNumber), 2);//check the index, UI not working so need to confirm later
        public void SelectDDL_ClosingStamp(int commentTabNumber = 1) => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ClosingStamp, commentTabNumber), 1);

        private void SetDesignDocTitleAndNumber()
        {
            guid = MiniGuid.NewGuid();

            string docKey = $"{tenantName}{GetTestName()}";
            docTitleKey = $"{docKey}_DsgnDocTtl";
            docNumberKey = $"{docKey}_DsgnDocNumb";
            CreateVar(docTitleKey, docTitleKey);
            CreateVar(docNumberKey, guid);
            SetDesignDocTitle();
            SetDesignDocNumber();
        }

        private void SetDesignDocTitle() => designDocTitle = GetVar(docTitleKey).ToString();
        private void SetDesignDocNumber() => designDocNumber = GetVar(docNumberKey).ToString();


        public void EnterDesignDocTitleAndNumber()
        {
            //login as uploading user IQFRecordsmgr (for SG and SH249) and IQFuser(GLX and Garnet)
            SetDesignDocTitleAndNumber();
            EnterText(PageHelper.GetTextInputFieldByLocator(DesignDocDetails_InputFields.Title), designDocTitle);
            EnterText(PageHelper.GetTextInputFieldByLocator(DesignDocDetails_InputFields.DocumentNumber), designDocNumber);            
        }
        
        internal By commentInput = By.Id("Comment_Text_0_");
        internal By commentResponseInput = By.Id("Comment_Response_0_");
        internal By commentResolutionInput = By.Id("Comment_ResolutionMeetingDecision_0_");
        internal By commentClosingInput = By.Id("Comment_ClosingComment_0_");

        public virtual void EnterRegularCommentAndDrawingPageNo()
        {
            //login as commenting user (SG- IQFuser, DoTuser | SH249-- IQFUser | Garenet and GLX-- DOTUser)
            SelectRegularCommentReviewType();
            //SelectRegularCommentReviewType();
            EnterComment(commentInput);
            EnterText(By.Id("Comment_DrawingPageNumber_0_"), "Draw123");
            ClickBtn_SaveOnly();

        }

        public virtual void ForwardComment()
        {
            //login as user to forward the comment ( SG-- IQFadmin and DOTAdmin both | SH249 -- IQFAdmin/IQFRecordsMgr | Garnet and GLX-- DOTadmin)
            //find the record you want to edit 
            //wait for loading the comments and make any changes if required
            ClickBtn_SaveForward();

        }

        public virtual void EnterResponseCommentAndAgreeResponseCode()
        {

            // Login as user to make response comment (All tenants - DevUser)
            EnterComment(commentResponseInput);
            SelectAgreeResponseCode(); //Disagree then different workflow
            ClickBtn_SaveOnly();
        }

        public virtual void EnterResponseCommentAndDisagreeResponseCode()
        {

            // Login as user to make response comment (All tenants - DevUser)
            EnterComment(commentResponseInput);
            SelectDisagreeResponseCode(); //agree then different workflow
            ClickBtn_SaveOnly();
        }

        public virtual void ForwardResponseComment()
        {
            //login as user to forward the comment ( All tenants - DevAdmin)
            //find the record you want to edit 
            //wait for loading the comments and make any changes if required
            JsClickElement(SaveForwardBtn_ByLocator);
            WaitForPageReady();
        }

        public virtual void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            LogInfo($"<<-- WORKFLOW ({tenantName}): EnterResolutionCommentAndResolutionCodeforDisagreeResponse -->>");
            ClickTab_Requires_Resolution();
            SortTable_Descending();
            ClickEnterBtnForRow();
            WaitForPageReady();


            // Login as user to make resolution comment (All tenants - DevAdmin)
            EnterComment(commentResolutionInput);
            SelectDisagreeResolutionCode(); //
            ClickBtn_SaveOnly();
            //wait for saveforward to load
            //WaitForPageReady();
            //ClickElement(SaveForwardBtn_ByLocator);


            Thread.Sleep(2000);
            ClickBtn_BackToList();
            WaitForPageReady();
        }

        public virtual void Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            ClickTab_Requires_Resolution();
            SortTable_Descending();
            ClickEnterBtnForRow();
            WaitForPageReady();
            ClickBtn_SaveForward();
        }


        public virtual void _LoggedInUserUploadsDesignDocument()
        {

            // Login as user to make resolution comment (All tenants - DevAdmin)
            EnterComment(commentResolutionInput);
            SelectAgreeResolutionCode(); //
            ClickSave();
            //wait for saveforward to load
            ClickSubmitForward();
        }

        

        private bool VerifyRequiredFieldErrorMsg(string errorMsg) => ElementIsDisplayed(By.XPath($"//li[text()='{errorMsg}']"));
        public virtual bool VerifyTitleFieldErrorMsgIsDisplayed() => VerifyRequiredFieldErrorMsg("Submittal Title is required.");
        public virtual bool VerifyDocumentNumberFieldErrorMsgIsDisplayed() => VerifyRequiredFieldErrorMsg("Submittal Number is required.");
        public virtual bool VerifyUploadFileErrorMsgIsDisplayed() => VerifyRequiredFieldErrorMsg("At least one file must be added.");

        internal bool VerifyRecordIsShownInTab(TableTab tableTab)
        {
            SelectTab(tableTab);
            SortColumnDescending(ColumnName.Action);
            return ElementIsDisplayed(GetTableRowLocator(designDocNumber));
        }

        //internal bool VerifyRecordIsShownInTab_StdUser(TableTab tableTab)
        //{
        //    SelectTab(tableTab);
        //    SortColumnDescending(ColumnName.Action);
        //    return ElementIsDisplayed(GetTableRowLocator(designDocNumber));
        //}

        public virtual bool VerifyItemStatusIsClosed() => VerifyRecordIsShownInTab(TableTab.Closed);

        private string CurrentUser => GetCurrentUser();

        public virtual void SelectTab(TableTab tableTab)
        {
            string tabName = string.Empty;
            string tabPrefix = "";

            if (CurrentUser.Contains("IQF"))
            {
                tabPrefix = "IQF ";
            }
            else if (CurrentUser.Contains("DEV"))
            {
                tabPrefix = "DEV ";
            }
            else if (CurrentUser.Contains("DOT"))
            {
                tabPrefix = "DOT ";
            }

            tabName = $"{tabPrefix}{tableTab.GetString()}";
            ClickTab(tabName);
        }

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

        public void SortTable_Descending() => SortColumnDescending(ColumnName.Action);
        public void SortTable_Ascending() => SortColumnAscending(ColumnName.Action);
    }
    #endregion <--end of common implementation class

    /// <summary>
    /// Tenant specific implementation of DesignDocument Comment Review
    /// </summary>

    #region Implementation specific to Garnet
    public class DesignDocument_Garnet : DesignDocument
    {
        public DesignDocument_Garnet(IWebDriver driver) : base(driver) { }
        public override void SelectRegularCommentReviewType(int commentTabNumber = 1) => ExpandAndSelectFromDDList(SetCommentStamp(DesignDocDetails_InputFields.ReviewType, commentTabNumber), 2);
    }
    #endregion <--specific to Garnet


    #region Implementation specific to GLX
    public class DesignDocument_GLX : DesignDocument
    {
        public DesignDocument_GLX(IWebDriver driver) : base(driver) { }

        public override void SelectTab(TableTab tableTab) => ClickTab(tableTab);

        public override void Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            LogInfo($"<<-- WORKFLOW ({tenantName}): EnterResolutionCommentAndResolutionCodeforDisagreeResponse -->>");

            ClickTab_Pending_Resolution();
            SortTable_Descending();
            ClickEnterBtnForRow();
            WaitForPageReady();


            EnterComment(commentResolutionInput);
            SelectDisagreeResolutionCode(); //
            ClickBtn_SaveOnly();


            Thread.Sleep(2000);
            ClickBtn_BackToList();
            WaitForPageReady();
        }
        
        public override void Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            ClickTab_Pending_Resolution();
            SortTable_Descending();
            ClickEnterBtnForRow();
            WaitForPageReady();
            ClickBtn_SaveForward();
        }
    }
    #endregion specific to GLX


    #region Implementation specific to SH249
    public class DesignDocument_SH249 : DesignDocument
    {
        public DesignDocument_SH249(IWebDriver driver) : base(driver) { }

        //public override bool VerifyifClosed() => VerifyRecordIsShownInTab_StdUser(TableTab.Closed);

    }
    #endregion <--specific toSGway


    #region Implementation specific to SGWay
    public class DesignDocument_SGWay : DesignDocument
    {
        public DesignDocument_SGWay(IWebDriver driver) : base(driver) { }

    }
    #endregion <--specific toSGway


    #region Implementation specific to I15South
    public class DesignDocument_I15South : DesignDocument
    {
        public DesignDocument_I15South(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to I15south


    #region Implementation specific to I15Tech
    public class DesignDocument_I15Tech : DesignDocument
    {
        public DesignDocument_I15Tech(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to I15tech
}
