using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RKCIUIAutomation.Config.ProjectProperties;
using NUnit.Framework;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.LabFieldTests;
using RKCIUIAutomation.Base;
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
        void EnterDesignDocTitleAndNumber();
        void EnterRegularCommentAndDrawingPageNo();
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
            [StringValue("Submittal_TransmittalNumber")] TransmittalNumber,
            [StringValue("Comment_ReviewTypeId_0_")] ReviewType,
            //[StringValue("Comment_Text_0_")] RegularComment,
            //[StringValue("Comment_DrawingPageNumber")] DrawingPageNumber,
           // [StringValue("Response_Comment")] ResponseComment,
            [StringValue("SelectedResponseCode")] ResponseCode,
           // [StringValue("Resolution_Comment")] ResolutionComment,
            [StringValue("SelectedResolutionStamp")] ResolutionStamp,
           // [StringValue("Closing_Comment")] ClosingComment,
            [StringValue("SelectedClosingStamp")] ClosingStamp
        }

        public  void SelectRegularCommentReviewType() => ExpandAndSelectFromDDList(DesignDocDetails_InputFields.ReviewType, 3);
        public  void SelectNoCommentReviewType() => ExpandAndSelectFromDDList(DesignDocDetails_InputFields.ReviewType, 1);
      //  public  void EnterText_RegularComment(string text) => EnterText(GetTextInputFieldByLocator(DesignDocDetails_InputFields.RegularComment), text);
      //  public  void EnterText_DrawingPageNumber(string text) => EnterText(GetTextInputFieldByLocator(DesignDocDetails_InputFields.DrawingPageNumber), text);
      //  public  void EnterText_ResponseComment(string text) => EnterText(GetTextInputFieldByLocator(DesignDocDetails_InputFields.ResponseComment), text);
        public  void SelectDDL_ResponseCode<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(DesignDocDetails_InputFields.ResponseCode, itemIndexOrName);
      //  public  void EnterText_ResolutionComment(string text) => EnterText(GetTextInputFieldByLocator(DesignDocDetails_InputFields.ResolutionComment), text);
        public  void SelectDDL_ResolutionStamp<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(DesignDocDetails_InputFields.ResolutionStamp, itemIndexOrName);
       // public  void EnterText_ClosingComment(string text) => EnterText(GetTextInputFieldByLocator(DesignDocDetails_InputFields.ClosingComment), text);
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
            SetDesignDocTitleAndNumber();
            designDocTitle = GetVar(docTitleKey).ToString();
            EnterText(PageHelper.GetTextInputFieldByLocator(DesignDocDetails_InputFields.Title), designDocTitle);
            designDocNumber = GetVar(docNumberKey).ToString();
            EnterText(PageHelper.GetTextInputFieldByLocator(DesignDocDetails_InputFields.DocumentNumber), designDocNumber);
        }
        
        By commentInput = By.Id("Comment_Text_0_");
        //By commentResponseInput = By.Id("Comment_Text_0_");
       // By commentResolutionInput = By.Id("Comment_Text_0_");
        //By commentClosingInput = By.Id("Comment_Text_0_");
        public virtual void EnterRegularCommentAndDrawingPageNo()
        {
            SelectRegularCommentReviewType();
            EnterComment(commentInput);
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
