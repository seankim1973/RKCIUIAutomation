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
        void LoggedInUserUploadsDesignDocument();
        void LoggedInUserCommentsAndSave();
        void LoggedInUserForwardsComment();
        void LoggedInUserResponseCommentAndSave();
        void LoggedInUserForwardsResponseComment();
        void LoggedInUserResolutionCommentAndSave();
        void LoggedInUserForwardsResolutionComment();
        void LoggedInUserCloseCommentAndSave();
        void LoggedInUserForwardsCloseComment();

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
                
        private By UploadNewDesignDoc_ByLocator => By.XPath("//a[text()='Upload New Design Document']");
        private By CancelBtn_ByLocator => By.Id("btnCancel");
        private By SaveOnlyBtn_ByLocator => By.Id("btnSave");
        private By SaveForwardBtn_ByLocator => By.Id("btnSaveForward");


        public void ClickBtn_UploadNewDesignDoc() => ClickElement(UploadNewDesignDoc_ByLocator);


        public void CreateDocument()
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
            [StringValue("Submittal_TransmittalNumber")] TransmittalNumber
        }

        private void SetDesignDocTitleAndNumber()
        {
            guid = MiniGuid.NewGuid();

            string docKey = $"{tenantName}{GetTestName()}";
            docTitleKey = $"{docKey}_DsgnDocTtl";
            docNumberKey = $"{docKey}_DsgnDocNumb";
            CreateVar(docTitleKey, docTitleKey);
            CreateVar(docNumberKey, guid);
        }

        public void EnterDesignDocTitleAndNumber()
        {
            SetDesignDocTitleAndNumber();
            designDocTitle = GetVar(docTitleKey).ToString();
            EnterText(PageHelper.GetTextInputFieldByLocator(DesignDocDetails_InputFields.Title), designDocTitle);
            designDocNumber = GetVar(docNumberKey).ToString();
            EnterText(PageHelper.GetTextInputFieldByLocator(DesignDocDetails_InputFields.DocumentNumber), designDocNumber);
        }





        /// <summary>
        /// TODO - implement common workflow
        /// </summary>
        public virtual void LoggedInUserUploadsDesignDocument()
        {
           
        }

        public virtual void LoggedInUserCommentsAndSave() { }
        public virtual void LoggedInUserForwardsComment() { }
        public virtual void LoggedInUserResponseCommentAndSave() { }
        public virtual void LoggedInUserForwardsResponseComment() { }
        public virtual void LoggedInUserResolutionCommentAndSave() { }
        public virtual void LoggedInUserForwardsResolutionComment() { }
        public virtual void LoggedInUserCloseCommentAndSave() { }
        public virtual void LoggedInUserForwardsCloseComment() { }

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
