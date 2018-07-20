using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using MiniGuids;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    #region DesignDocumentCommentReview Generic class
    public class DesignDocumentCommentReview : DesignDocumentCommentReview_Impl
    {
        public DesignDocumentCommentReview(){}
        public DesignDocumentCommentReview(IWebDriver driver) => this.driver = driver;
        
    }
    #endregion  <-- end of DesignDocumentCommentReview Generic Class

    #region Workflow Interface class
    public interface IDesignDocumentCommentReview
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
       // void LoggedInUserForwardsComment();
       //void LoggedInUserResponseCommentAndSave();
       // void LoggedInUserForwardsResponseComment();
       // void LoggedInUserResolutionCommentAndSave();
       // void LoggedInUserForwardsResolutionComment();
       // void LoggedInUserCloseCommentAndSave();
       // void LoggedInUserForwardsCloseComment();

    }
    #endregion <-- end of Workflow Interface class

    #region Common Workflow Implementation class
    public abstract class DesignDocumentCommentReview_Impl : PageBase, IDesignDocumentCommentReview
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);
        private IDesignDocumentCommentReview SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IDesignDocumentCommentReview instance = new DesignDocumentCommentReview(driver);

            if (tenantName == TenantName.SGWay)
            {
                LogInfo($"###### using DesignDocumentCommentReview_SGWay instance ###### ");
                instance = new DesignDocumentCommentReview_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                LogInfo($"###### using  DesignDocumentCommentReview_SH249 instance ###### ");
                instance = new DesignDocumentCommentReview_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                LogInfo($"###### using  DesignDocumentCommentReview_Garnet instance ###### ");
                instance = new DesignDocumentCommentReview_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                LogInfo($"###### using  DesignDocumentCommentReview_GLX instance ###### ");
                instance = new DesignDocumentCommentReview_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                LogInfo($"###### using  DesignDocumentCommentReview_I15South instance ###### ");
                instance = new DesignDocumentCommentReview_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                LogInfo($"###### using DesigntDocumentCommentReview_I15Tech instance ###### ");
                instance = new DesignDocumentCommentReview_I15Tech(driver);
            }

            return instance;
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
            [StringValue("IqfDeadlineDate")] Deadline
        }
        
        private string designDocTitle;
        private string designDocNumber;
        private string docTitleKey;
        private string docNumberKey;
        private MiniGuid guid;

        public enum TableTab
        {
            [StringValue("Creating")] Creating,
            [StringValue("Comment")] Comment,
            [StringValue("Resolution")] Resolution,
            [StringValue("Response")] Response,
            [StringValue("Closing")] Closing,
            [StringValue("Closed")] Closed
        }

        private By UploadNewDesignDoc_ByLocator => By.XPath("//a[text()='Upload New Design Document']");
        private By CancelBtn_ByLocator => By.Id("btnCancel");
        private By SaveOnlyBtn_ByLocator => By.Id("btnSave");
        private By SaveForwardBtn_ByLocator => By.Id("btnSaveForward");


        public void ClickBtn_UploadNewDesignDoc() => ClickElement(UploadNewDesignDoc_ByLocator);
        public void ClickTab_Creating() => ClickTableTab(TableTab.Creating);
        public void ClickTab_Comment() => ClickTableTab(TableTab.Comment);
        public void ClickTab_Resolution() => ClickTableTab(TableTab.Resolution);
        public void ClickTab_Response() => ClickTableTab(TableTab.Response);
        public void ClickTab_Closing() => ClickTableTab(TableTab.Closing);
        public void ClickTab_Closed() => ClickTableTab(TableTab.Closed);


        public virtual void CreateDocument()
        {
            ClickElement(UploadNewDesignDoc_ByLocator);
            EnterDesignDocTitleAndNumber();
            UploadFile("test.xlsx");
            ClickElement(SaveForwardBtn_ByLocator);
        }

        enum DesignDocDetails_InputFields
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
        }

        public virtual void EnterDesignDocTitleAndNumber()
        {
            //login as uploading user IQFRecordsmgr (for SG and SH249) and IQFuser(GLX and Garnet)
            SetDesignDocTitleAndNumber();
            designDocTitle = GetVar(docTitleKey).ToString();
            EnterText(PageHelper.GetTextInputFieldByLocator(DesignDocDetails_InputFields.Title), designDocTitle);
            designDocNumber = GetVar(docNumberKey).ToString();
            EnterText(PageHelper.GetTextInputFieldByLocator(DesignDocDetails_InputFields.DocumentNumber), designDocNumber);
            ClickSubmitForward();
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

        /// <summary>
        /// TODO - implement common workflow
        /// </summary>
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
    public class DesignDocumentCommentReview_Garnet : DesignDocumentCommentReview
    {
        public DesignDocumentCommentReview_Garnet(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to Garnet


    #region Implementation specific to GLX
    public class DesignDocumentCommentReview_GLX : DesignDocumentCommentReview
    {
        public DesignDocumentCommentReview_GLX(IWebDriver driver) : base(driver) { }
    }
    #endregion specific to GLX


    #region Implementation specific to SH249
    public class DesignDocumentCommentReview_SH249 : DesignDocumentCommentReview
    {
        public DesignDocumentCommentReview_SH249(IWebDriver driver) : base(driver) { }

    }
    #endregion <--specific toSGway


    #region Implementation specific to SGWay
    public class DesignDocumentCommentReview_SGWay : DesignDocumentCommentReview
    {
        public DesignDocumentCommentReview_SGWay(IWebDriver driver) : base(driver) { }

    }
    #endregion <--specific toSGway


    #region Implementation specific to I15South
    public class DesignDocumentCommentReview_I15South : DesignDocumentCommentReview
    {
        public DesignDocumentCommentReview_I15South(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to I15south


    #region Implementation specific to I15Tech
    public class DesignDocumentCommentReview_I15Tech : DesignDocumentCommentReview
    {
        public DesignDocumentCommentReview_I15Tech(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to I15tech
}
