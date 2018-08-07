﻿using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using MiniGuids;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.DesignDocument;
using RKCIUIAutomation.Test;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    #region DesignDocument Generic class
    public class DesignDocument : DesignDocument_Impl
    {
        public DesignDocument(){}
        public DesignDocument(IWebDriver driver) => this.driver = driver;

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
            [StringValue("IQF Creating")] IQF_Creating,   
            
            [StringValue("DEV Pending Comment")] DEV_Pending_Comment,
            [StringValue("DOT Requires Comment")] DOT_Requires_Comment,
            [StringValue("IQF Pending Comment")] IQF_Pending_Comment,
            [StringValue("Pending Comment")] Pending_Comment,
            [StringValue("Requires Comment")] Requires_Comment,

            [StringValue("DEV Requires Response")] DEV_Requires_Response, //Garnet
            [StringValue("DOT Pending Response")] DOT_Pending_Response,
            [StringValue("IQF Pending Response")] IQF_Pending_Response,
            [StringValue("Pending Response")] Pending_Response,
            [StringValue("Requires Response")] Requires_Response,
                        
            [StringValue("DEV Requires Resolution")] DEV_Requires_Resolution, //Garnet
            [StringValue("DOT Pending Resolution")] DOT_Pending_Resolution,
            [StringValue("IQF Pending Resolution")] IQF_Pending_Resolution,
            [StringValue("Pending Resolution")] Pending_Resolution,
            [StringValue("Requires Resolution")] Requires_Resolution,

            [StringValue("Pending Closing")] Pending_Closing,
            [StringValue("Requires Closing")] Requires_Closing,

            [StringValue("Closed")] Closed,
            [StringValue("DOT Closed")] DOT_Closed,
            [StringValue("DEV Closed")] DEV_Closed,
            [StringValue("IQF Closed")] IQF_Closed
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
        void SelectTab_Creating();
        void SelectTab_IQF_Creating();
        void SelectTab_DEV_Pending_Comment();
        void SelectTab_DOT_Requires_Comment();
        void SelectTab_IQF_Pending_Comment();
        void SelectTab_Pending_Comment();        
        void SelectTab_Requires_Comment();       
        void SelectTab_DEV_Requires_Response();
        void SelectTab_DOT_Pending_Response();
        void SelectTab_IQF_Pending_Response();
        void SelectTab_Pending_Response();
        void SelectTab_Requires_Response();
        void SelectTab_DEV_Requires_Resolution();
        void SelectTab_DOT_Pending_Resolution();
        void SelectTab_IQF_Pending_Resolution();
        void SelectTab_Requires_Resolution();       
        void SelectTab_Pending_Resolution();
        void SelectTab_Pending_Closing();
        void SelectTab_Requires_Closing();
        void SelectTab_Closed();
        void SelectTab_DOT_Closed();
        void SelectTab_DEV_Closed();
        void SelectTab_IQF_Closed();


        void CreateDocument();
        void EnterDesignDocTitleAndNumber();
        void EnterRegularCommentAndDrawingPageNo();
        void ForwardComment();
        void EnterResponseCommentAndAgreeResponseCode();
        void EnterResponseCommentAndDisagreeResponseCode();
        void ForwardResponseComment();
        void EnterResolutionCommentAndResolutionCodeforDisagreeResponse();
        bool VerifyifClosed(string recordTitleOrNumber);
        void ClickBtn_BackToList();
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


        private string designDocTitle;
        private string designDocNumber;
        private string docTitleKey;
        private string docNumberKey;
        private MiniGuid guid;

       
        private By UploadNewDesignDoc_ByLocator => By.XPath("//a[text()='Upload New Design Document']");
        private By CancelBtnUploadPage_ByLocator => By.Id("btnCancel");
        private By SaveOnlyBtnUploadPage_ByLocator => By.Id("btnSave");
        private By SaveForwardBtnUploadPage_ByLocator => By.Id("btnSaveForward");
        private By SaveOnlyBtn_ByLocator => By.XPath("//div[@class='k-content k-state-active']//button[contains(text(),'Save Only')]");
        private By SaveForwardBtn_ByLocator => By.XPath("//div[@class='k-content k-state-active']//button[contains(text(),'Save & Forward')]");
        private By BackToListBtn_ByLocator => By.XPath("//button[text()='Back To List']");
        //private By SaveOnlyBtnComment1_ByLocator => By.Id("btnSave_0_");
        //private By SaveForwardBtnComment1_ByLocator => By.Id("btnSaveForward_0_");


        public virtual void ClickBtn_UploadNewDesignDoc() => ClickElement(UploadNewDesignDoc_ByLocator);
        public virtual void ClickBtn_BackToList() => ClickElement(BackToListBtn_ByLocator);

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
        
        By commentInput = By.Id("Comment_Text_0_");
        By commentResponseInput = By.Id("Comment_Response_0_");
        By commentResolutionInput = By.Id("Comment_ResolutionMeetingDecision_0_");
        By commentClosingInput = By.Id("Comment_ClosingComment_0_");

        public virtual void EnterRegularCommentAndDrawingPageNo()
        {
            //login as commenting user (SG- IQFuser, DoTuser | SH249-- IQFUser | Garenet and GLX-- DOTUser)
            SelectRegularCommentReviewType();
            SelectRegularCommentReviewType();
            EnterComment(commentInput);
            EnterText(By.Id("Comment_DrawingPageNumber_0_"), "Draw123");
            ClickElement(SaveOnlyBtn_ByLocator);

        }

        public virtual void ForwardComment()
        {
            //login as user to forward the comment ( SG-- IQFadmin and DOTAdmin both | SH249 -- IQFAdmin/IQFRecordsMgr | Garnet and GLX-- DOTadmin)
            //find the record you want to edit 
            //wait for loading the comments and make any changes if required
            ClickElement(SaveForwardBtn_ByLocator);

        }

        public virtual void EnterResponseCommentAndAgreeResponseCode()
        {

            // Login as user to make response comment (All tenants - DevUser)
            EnterComment(commentResponseInput);
            SelectAgreeResponseCode(); //Disagree then different workflow
            ClickElement(SaveOnlyBtn_ByLocator);
        }

        public virtual void EnterResponseCommentAndDisagreeResponseCode()
        {

            // Login as user to make response comment (All tenants - DevUser)
            EnterComment(commentResponseInput);
            SelectDisagreeResponseCode(); //agree then different workflow
            ClickElement(SaveOnlyBtn_ByLocator);
        }

        public virtual void ForwardResponseComment()
        {
            //login as user to forward the comment ( All tenants - DevAdmin)
            //find the record you want to edit 
            //wait for loading the comments and make any changes if required
            JsClickElement(SaveForwardBtn_ByLocator);

        }

        public virtual void EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {

            // Login as user to make resolution comment (All tenants - DevAdmin)
            EnterComment(commentResolutionInput);
            SelectAgreeResolutionCode(); //
            ClickElement(SaveOnlyBtn_ByLocator);
            //wait for saveforward to load
            ClickElement(SaveForwardBtn_ByLocator);
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

        private bool VerifyRecordIsShownInTab(TableTab tableTab, string recordTitleOrNumber)
        {
            SelectTab(tableTab);
            return ElementIsDisplayed(GetTableRowLocator(recordTitleOrNumber));
        }
        public virtual bool VerifyifClosed(string recordTitleOrNumber) => VerifyRecordIsShownInTab(TableTab.Closed, recordTitleOrNumber);

        //TODO - use CurrentUser variable to determine tab name (i.e. DOT, DEV, IQF, etc)
        private string CurrentUser => GetCurrentUser();

        private void SelectTab(TableTab tableTab) => ClickTab(tableTab);
        public void SelectTab_Creating() => SelectTab(TableTab.Creating);
        public void SelectTab_IQF_Creating() => SelectTab(TableTab.IQF_Creating);
        public void SelectTab_DEV_Pending_Comment() => SelectTab(TableTab.DEV_Pending_Comment);
        public void SelectTab_DOT_Requires_Comment() => SelectTab(TableTab.DOT_Requires_Comment);
        public void SelectTab_IQF_Pending_Comment() => SelectTab(TableTab.IQF_Pending_Comment);
        public void SelectTab_Pending_Comment() => SelectTab(TableTab.Pending_Comment);
        public void SelectTab_Requires_Comment() => SelectTab(TableTab.Requires_Comment);
        public void SelectTab_DEV_Requires_Response() => SelectTab(TableTab.DEV_Requires_Response);
        public void SelectTab_DOT_Pending_Response() => SelectTab(TableTab.DOT_Pending_Response);
        public void SelectTab_IQF_Pending_Response() => SelectTab(TableTab.IQF_Pending_Response);
        public void SelectTab_Pending_Response() => SelectTab(TableTab.Pending_Response);
        public void SelectTab_Requires_Response() => SelectTab(TableTab.Requires_Response);
        public void SelectTab_DEV_Requires_Resolution() => SelectTab(TableTab.DEV_Requires_Resolution);
        public void SelectTab_DOT_Pending_Resolution() => SelectTab(TableTab.DOT_Pending_Resolution);
        public void SelectTab_IQF_Pending_Resolution() => SelectTab(TableTab.IQF_Pending_Resolution);
        public void SelectTab_Requires_Resolution() => SelectTab(TableTab.Requires_Resolution);
        public void SelectTab_Pending_Resolution() => SelectTab(TableTab.Pending_Resolution);
        public void SelectTab_Pending_Closing() => SelectTab(TableTab.Pending_Closing);
        public void SelectTab_Requires_Closing() => SelectTab(TableTab.Requires_Closing);
        public void SelectTab_Closed() => SelectTab(TableTab.Closed);
        public void SelectTab_DOT_Closed() => SelectTab(TableTab.DOT_Closed);
        public void SelectTab_DEV_Closed() => SelectTab(TableTab.DEV_Closed);
        public void SelectTab_IQF_Closed() => SelectTab(TableTab.IQF_Closed);
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
    }
    #endregion specific to GLX


    #region Implementation specific to SH249
    public class DesignDocument_SH249 : DesignDocument
    {
        public DesignDocument_SH249(IWebDriver driver) : base(driver) { }

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
