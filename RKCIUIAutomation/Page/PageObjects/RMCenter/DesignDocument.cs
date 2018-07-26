using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using MiniGuids;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.DesignDocument;

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
            [StringValue("Comment_ReviewTypeId_0_")] ReviewType,
            [StringValue("SelectedResponseCode")] ResponseCode,
            [StringValue("SelectedResolutionStamp")] ResolutionStamp,
            [StringValue("SelectedClosingStamp")] ClosingStamp
        }

        public enum TableTab
        {
            [StringValue("Creating")] Creating,
            [StringValue("Requires Comment")] Requires_Comment,
            [StringValue("Pending Response")] Pending_Response,
            [StringValue("Requires Resolution")] Requires_Resolution,
            [StringValue("Pending Resolution")] Pending_Resolution,
            [StringValue("Pending Closing")] Pending_Closing,
            [StringValue("Closed")] Closed,
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
        void CreateDocument();
        void EnterDesignDocTitleAndNumber();
        void EnterRegularCommentAndDrawingPageNo();
        void ForwardComment();
        void EnterResponseCommentAndAgreeResponseCode();
        void EnterResponseCommentAndDisagreeResponseCode();
        void ForwardResponseComment();
        void EnterResolutionCommentAndResolutionCodeforDisagreeResponse();
        void VerifyifClosed();
        void ClickBtn_BackToList();
        void ClickBtn_UploadNewDesignDoc();

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
        private By CancelBtn_ByLocator => By.Id("btnCancel");
        private By SaveOnlyBtn_ByLocator => By.Id("btnSave");
        private By SaveForwardBtn_ByLocator => By.Id("btnSaveForward");
        private By BackToListBtn_ByLocator => By.XPath("//button[text()='Back To List']");

        public virtual void ClickBtn_UploadNewDesignDoc() => ClickElement(UploadNewDesignDoc_ByLocator);
        public virtual void ClickBtn_BackToList() => ClickElement(BackToListBtn_ByLocator);

        public virtual void CreateDocument()
        {
            ClickElement(UploadNewDesignDoc_ByLocator);
            EnterDesignDocTitleAndNumber();
            UploadFile("test.xlsx");
            ClickElement(SaveForwardBtn_ByLocator);
            WaitForPageReady();
        }

        public  void SelectRegularCommentReviewType() => ExpandAndSelectFromDDList(DesignDocDetails_InputFields.ReviewType, 3);
        public  void SelectNoCommentReviewType() => ExpandAndSelectFromDDList(DesignDocDetails_InputFields.ReviewType, 1);
        public void SelectAgreeResponseCode() => ExpandAndSelectFromDDList(DesignDocDetails_InputFields.ResponseCode, 1); //check the index, UI not working so need to confirm later
        public void SelectDisagreeResponseCode() => ExpandAndSelectFromDDList(DesignDocDetails_InputFields.ResponseCode, 3);//check the index, UI not working so need to confirm later
        public void SelectAgreeResolutionCode() => ExpandAndSelectFromDDList(DesignDocDetails_InputFields.ResolutionStamp, 1); //check the index, UI not working so need to confirm later
        public void SelectDisagreeResolutionCode() => ExpandAndSelectFromDDList(DesignDocDetails_InputFields.ResolutionStamp, 2);//check the index, UI not working so need to confirm later
        public  void SelectDDL_ClosingStamp<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(DesignDocDetails_InputFields.ClosingStamp, itemIndexOrName);

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
            ClickSave();

        }

        public virtual void ForwardComment()
        {
            //login as user to forward the comment ( SG-- IQFadmin and DOTAdmin both | SH249 -- IQFAdmin/IQFRecordsMgr | Garnet and GLX-- DOTadmin)
            //find the record you want to edit 
            //wait for loading the comments and make any changes if required
            ClickSubmitForward();

        }

        public virtual void EnterResponseCommentAndAgreeResponseCode()
        {

            // Login as user to make response comment (All tenants - DevUser)
            EnterComment(commentResponseInput);
            SelectAgreeResponseCode(); //Disagree then different workflow
            ClickSave();
        }

        public virtual void EnterResponseCommentAndDisagreeResponseCode()
        {

            // Login as user to make response comment (All tenants - DevUser)
            EnterComment(commentResponseInput);
            SelectDisagreeResponseCode(); //agree then different workflow
            ClickSave();
        }

        public virtual void ForwardResponseComment()
        {
            //login as user to forward the comment ( All tenants - DevAdmin)
            //find the record you want to edit 
            //wait for loading the comments and make any changes if required
            ClickSubmitForward();

        }

        public virtual void EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {

            // Login as user to make resolution comment (All tenants - DevAdmin)
            EnterComment(commentResolutionInput);
            SelectAgreeResolutionCode(); //
            ClickSave();
            //wait for saveforward to load
            ClickSubmitForward();
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

        public virtual void VerifyifClosed()
        {
            //verify if records made it to Dev Closed tab.
        }


     
       


    }
    #endregion <--end of common implementation class

    /// <summary>
    /// Tenant specific implementation of DesignDocument Comment Review
    /// </summary>

    #region Implementation specific to Garnet
    public class DesignDocument_Garnet : DesignDocument
    {
        public DesignDocument_Garnet(IWebDriver driver) : base(driver) { }
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
