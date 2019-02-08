using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System.Threading;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.DesignDocument;

namespace RKCIUIAutomation.Page.Workflows
{
    public class DesignDocumentWF : DesignDocumentWF_Impl
    {
        public DesignDocumentWF()
        {
        }

        public DesignDocumentWF(IWebDriver driver) => this.Driver = driver;

        //internal void WaitForActiveCommentTab()
        //{
        //    bool activeTabNotDisplayed = true;

        //    for (int i = 0; i > 30; i++)
        //    {
        //        do
        //        {
        //            if (i == 30)
        //            {
        //                ElementNotVisibleException e = new ElementNotVisibleException();
        //                log.Error($"Comment tab is not visible: {e.Message}");
        //                throw e;
        //            }
        //            else
        //            {
        //                activeTabNotDisplayed = ElementIsDisplayed(By.XPath("//div[@class='k-content k-state-active']"));
        //            }
        //        }
        //        while (activeTabNotDisplayed);
        //    }
        //}
    }

    public interface IDesignDocumentWF
    {
        void TCWF_CommentReviewRegularComment();

        void TCWF_CommentReviewNoComment();

        void CreateDesignDocCommentReviewDocument(UserType user);

        void EnterRegularComment(UserType user);

        void EnterNoComment(UserType user);

        void ForwardComment(UserType user);

        void ForwardResponseComment(UserType user);

        void EnterResponseAndDisagreeResponseCode(UserType user);

        void EnterResponseAndAgreeResponseCode(UserType user);

        void EnterResolutionCommentAndResolutionCodeforDisagreeResponse();

        void EnterClosingCommentAndCode();

        void EnterResolutionCommentAndResolutionCodeforDisagreeResponse(UserType user);

        void ForwardResolutionCommentAndCodeForDisagreeResponse();

        void EnterAndForwardClosingComment();

        void EnterAndForwardClosingComment(UserType user);

        void FilterTableAndEditDoc(string docNumber = "");
    }

    public abstract class DesignDocumentWF_Impl : TestBase, IDesignDocumentWF
    {
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IDesignDocumentWF SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IDesignDocumentWF instance = new DesignDocumentWF(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using DesignDocumentWF_SGWay instance ###### ");
                instance = new DesignDocumentWF_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using DesignDocumentWF_SH249 instance ###### ");
                instance = new DesignDocumentWF_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using DesignDocumentWF_Garnet instance ###### ");
                instance = new DesignDocumentWF_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using DesignDocumentWF_GLX instance ###### ");
                instance = new DesignDocumentWF_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using DesignDocumentWF_I15South instance ###### ");
                instance = new DesignDocumentWF_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using DesignDocumentWF_I15Tech instance ###### ");
                instance = new DesignDocumentWF_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using DesignDocumentWF_LAX instance ###### ");
                instance = new DesignDocumentWF_LAX(driver);
            }
            return instance;
        }

        internal DesignDocumentWF DesignDocWF => new DesignDocumentWF();

        public virtual void FilterTableAndEditDoc(string docNumber = "")
        {
            DesignDocCommentReview.FilterDocNumber(docNumber);
            ClickEnterBtnForRow();
            DesignDocCommentReview.WaitForActiveCommentTab();
        }

        //All Tenants
        public virtual void CreateDesignDocCommentReviewDocument(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            Assert.True(VerifyPageTitle("Design Document"));
            DesignDocCommentReview.CreateDocument();
        }

        //Garnet and GLX
        public virtual void EnterRegularComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        //Garnet and GLX
        public virtual void EnterNoComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterNoComment();
        }

        public virtual void ForwardComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
            DesignDocCommentReview.WaitForActiveCommentTab();
            WaitForPageReady();
        }

        public virtual void ForwardResponseComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.ClickBtnJs_SaveForward();
            //DesignDocCommentReview.ClickBtn_BackToList();
        }

        public virtual void EnterResponseAndDisagreeResponseCode(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            EnterResponseCommentAndDisagreeResponseCode();
        }

        //Garnet
        public virtual void ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public virtual void EnterResolutionCommentAndResolutionCodeforDisagreeResponse(UserType user)
        {
        }

        //Garnet
        public virtual void EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //WaitForPageReady();
            //ClickEnterBtnForRow();
            //WaitForPageReady();
            //Thread.Sleep(2000);
            EnterComment(CommentType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            Thread.Sleep(2000);
            DesignDocCommentReview.ClickBtn_BackToList();
            WaitForPageReady();
        }

        public virtual void EnterResponseCommentAndDisagreeResponseCode()
        {
            // Login as user to make response comment (All tenants - DevUser)
            EnterComment(CommentType.CommentResponseInput);
            DesignDocCommentReview.SelectDisagreeResponseCode(); //agree then different workflow
            DesignDocCommentReview.ClickBtn_SaveOnly();
        }

        public virtual void EnterResponseAndAgreeResponseCode(UserType user)
        {
        }

        public virtual void EnterAndForwardClosingComment()
        {
        }

        public virtual void EnterClosingCommentAndCode()
        {
        }

        public virtual void EnterAndForwardClosingComment(UserType user)
        {
        }

        /// <summary>
        /// Comment Review Regular Comment Test Case Workflow for Garnet and GLX
        /// </summary>
        public virtual void TCWF_CommentReviewRegularComment()
        {
            LogInfo("--------------------------1.Login As IQF User and Create Document----------------------");
            CreateDesignDocCommentReviewDocument(UserType.IQFUser);
            LogoutToLoginPage();

            LogInfo("--------------------------2.Login As DOT User and Enter Regular Comment----------------------");
            EnterRegularComment(UserType.DOTUser);
            LogoutToLoginPage();

            LogInfo("--------------------------3. Login As DOT Admin and Forward Comment----------------------");
            ForwardComment(UserType.DOTAdmin);
            LogoutToLoginPage();

            LogInfo("--------------------------4.Login As DEV User, Enter Response and Disagree Response Code----------------------");
            EnterResponseAndDisagreeResponseCode(UserType.DEVUser);
            LogoutToLoginPage();

            LogInfo("--------------------------5. Login As DEV Admin and Forward Response Comment----------------------");
            ForwardResponseComment(UserType.DEVAdmin);

            LogInfo("--------------------------6. DEV Admin enters Resolution='Disagree workflow'----------------------");
            EnterResolutionCommentAndResolutionCodeforDisagreeResponse();

            LogInfo("--------------------------7. DEV Admin forwards Resolution='Disagree workflow'----------------------");
            ForwardResolutionCommentAndCodeForDisagreeResponse();

            LogInfo("--------------------------8. DEV Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }

        /// <summary>
        /// Comment Review No Comment Test Case Workflow for Garnet and GLX
        /// </summary>
        public virtual void TCWF_CommentReviewNoComment()
        {
            LogInfo("--------------------------1.Login As IQF User and Create Document----------------------");
            CreateDesignDocCommentReviewDocument(UserType.IQFUser);
            LogoutToLoginPage();

            LogInfo("--------------------------2.Login As DOT User and enter no comment----------------------");
            EnterNoComment(UserType.DOTUser);
            LogoutToLoginPage();

            LogInfo("--------------------------3. Login As DOT Admin and Forward Comment----------------------");
            ForwardComment(UserType.DOTAdmin);

            LogInfo("--------------------------4. DEV Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }
    }

    internal class DesignDocumentWF_GLX : DesignDocumentWF
    {
        public DesignDocumentWF_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override void TCWF_CommentReviewNoComment()
        {
            LogInfo("--------------------------1.Login As IQF User and Create Document----------------------");
            CreateDesignDocCommentReviewDocument(UserType.IQFUser);
            LogoutToLoginPage();

            LogInfo("--------------------------2.Login As DOT User and enter no comment----------------------");
            EnterNoComment(UserType.DOTUser);
            LogoutToLoginPage();

            LogInfo("--------------------------3. Login As DOT Admin and Forward Comment----------------------");
            ForwardComment(UserType.DOTAdmin);
            LogoutToLoginPage();

            LogInfo("--------------------------4. DEV Admin verifies if record in closed tab ----------------------");

            //missing steps - Doc is not seen under Closed tab after previous step for GLX

            LogInfo("--------------------------5. DEV Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }

        public override void ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Pending_Resolution();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //ClickEnterBtnForRow();
            //WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Pending_Resolution();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //WaitForPageReady();
            //ClickEnterBtnForRow();
            //WaitForPageReady();
            //Thread.Sleep(2000);
            EnterComment(CommentType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            Thread.Sleep(2000);
            DesignDocCommentReview.ClickBtn_BackToList();
            WaitForPageReady();
        }
    }

    internal class DesignDocumentWF_Garnet : DesignDocumentWF
    {
        public DesignDocumentWF_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class DesignDocumentWF_SH249 : DesignDocumentWF
    {
        public DesignDocumentWF_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override void TCWF_CommentReviewRegularComment()
        {
            LogInfo("--------------------------1. Log in as IQF RecordsManager'----------------------");
            CreateDesignDocCommentReviewDocument(UserType.IQFRecordsMgr);
            LogoutToLoginPage();

            LogInfo("--------------------------2. Log in as IQF User, enters Comments----------------------");
            EnterRegularComment(UserType.IQFUser);
            LogoutToLoginPage();

            LogInfo("--------------------------3. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment(UserType.IQFRecordsMgr);//Workaround: using IQF Rcrds Mgr, instead of IQF Admin

            LogInfo("-------------------------4. Log in as IQF Admin, Enters,forwards Response and Resolution stampcode----------------------");
            EnterResponseCommentAndDisagreeResponseCode();

            LogInfo("--------------------------5. Log in as IQF Admin, Enters,forwards closing comment----------------------");
            EnterClosingCommentAndCode();

            LogInfo("--------------------------6. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }

        public override void TCWF_CommentReviewNoComment()
        {
            LogInfo("--------------------------1. Log in as IQF RecordsManager'----------------------");
            CreateDesignDocCommentReviewDocument(UserType.IQFRecordsMgr);
            LogoutToLoginPage();

            LogInfo("--------------------------2. Log in as IQF User, enter no Comments----------------------");
            EnterNoComment(UserType.IQFUser);
            LogoutToLoginPage();

            LogInfo("--------------------------3. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment(UserType.IQFRecordsMgr);//Workaround: using IQF Rcrds Mgr, instead of IQF Admin

            LogInfo("-------------------------4. Log in as IQF Admin, Enters,forwards Response and Resolution stampcode----------------------");
            EnterResponseCommentAndDisagreeResponseCode();

            LogInfo("--------------------------5. Log in as IQF Admin, Enters,forwards closing comment----------------------");
            EnterClosingCommentAndCode();

            LogInfo("--------------------------6. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }

        public override void EnterRegularComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Comment();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        public override void EnterNoComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Comment();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterNoComment();
        }

        public override void ForwardComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Comment();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
            DesignDocCommentReview.WaitForActiveCommentTab();
            WaitForPageReady();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            EnterComment(CommentType.CommentResponseInput);
            EnterComment(CommentType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterClosingCommentAndCode()
        {
            DesignDocCommentReview.ClickTab_Requires_Closing();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //ClickEnterBtnForRow();
            //WaitForPageReady();
            EnterComment(CommentType.CommentClosingInput);
            DesignDocCommentReview.SelectDDL_ClosingStamp();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }
    }

    internal class DesignDocumentWF_SGWay : DesignDocumentWF
    {
        public DesignDocumentWF_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override void TCWF_CommentReviewRegularComment()
        {
            LogInfo("--------------------------1. Log in as IQFRMgr and Create Document----------------------");
            CreateDesignDocCommentReviewDocument(UserType.IQFRecordsMgr);
            LogoutToLoginPage();

            LogInfo("--------------------------2. Log in as DOT User, enters Comments----------------------");
            EnterRegularComment(UserType.DOTUser);
            LogoutToLoginPage();

            LogInfo("--------------------------3. Log in as IQF User, enters Comments----------------------");
            EnterRegularComment(UserType.IQFUser);
            LogoutToLoginPage();

            LogInfo("--------------------------4. Log in as DOT Admin, forwards Comments----------------------");
            ForwardComment(UserType.DOTAdmin);
            LogoutToLoginPage();

            LogInfo("--------------------------5. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment(UserType.IQFAdmin);
            LogoutToLoginPage();

            LogInfo("--------------------------6. Log in as DEV User, enters Response and Disagree Response Code----------------------");
            EnterResponseAndDisagreeResponseCode(UserType.DEVUser);
            LogoutToLoginPage();

            LogInfo("--------------------------7. Log in as DEV Admin, forwards Response Comments----------------------");
            ForwardResponseComment(UserType.DEVAdmin);
            LogoutToLoginPage();

            LogInfo("--------------------------8. Log in as IQF Admin, Add Resolution Comment for Disagree workflow ----------------------");
            EnterResolutionCommentAndResolutionCodeforDisagreeResponse(UserType.IQFAdmin);

            LogInfo("--------------------------9. Log in as IQF Admin, forwards Resolution----------------------");
            ForwardResolutionCommentAndCodeForDisagreeResponse();

            LogInfo("--------------------------10. Log in as IQF Admin, Enters,forwards closing comment----------------------");
            EnterAndForwardClosingComment();

            LogInfo("--------------------------11. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }

        public override void TCWF_CommentReviewNoComment()
        {
            LogInfo("--------------------------No comment Workflow begins----------------------");
            LogInfo("--------------------------1. Log in as IQFRM'----------------------");
            CreateDesignDocCommentReviewDocument(UserType.IQFRecordsMgr);
            LogoutToLoginPage();

            LogInfo("--------------------------2. Log in as DOT User, enter no Comments----------------------");
            EnterNoComment(UserType.DOTUser);
            LogoutToLoginPage();

            LogInfo("--------------------------3. Log in as IQF User, enter no Comments----------------------");
            EnterNoComment(UserType.IQFUser);
            LogoutToLoginPage();

            LogInfo("--------------------------4. Log in as DOT Admin, forwards Comments----------------------");
            ForwardComment(UserType.DOTAdmin);
            LogoutToLoginPage();

            LogInfo("--------------------------5. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment(UserType.IQFAdmin);
            LogoutToLoginPage();

            LogInfo("--------------------------6. Log in as DEV User, enters Response and Agree Response Code----------------------");
            EnterResponseAndAgreeResponseCode(UserType.DEVUser);
            LogoutToLoginPage();

            LogInfo("--------------------------7. Log in as DEV Admin, forwards Response Comments----------------------");
            ForwardResponseComment(UserType.DEVAdmin);
            LogoutToLoginPage();

            LogInfo("--------------------------8. Log in as IQF Admin, Enters,forwards closing comment----------------------");
            EnterAndForwardClosingComment(UserType.IQFAdmin);

            LogInfo("--------------------------9. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }

        public override void EnterRegularComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        public override void EnterNoComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterNoComment();
        }

        public override void ForwardComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
            Thread.Sleep(2000);
        }

        public override void ForwardResponseComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.ClickBtnJs_SaveForward();
        }

        public override void EnterResponseAndDisagreeResponseCode(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            EnterResponseCommentAndDisagreeResponseCode();
        }

        public override void EnterResolutionCommentAndResolutionCodeforDisagreeResponse(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();

            EnterComment(CommentType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            DesignDocCommentReview.ClickCommentTabNumber(commentTabNumber);
            EnterComment(CommentType.CommentResolutionInput, commentTabNumber);
            DesignDocCommentReview.SelectDisagreeResolutionCode(commentTabNumber);
            DesignDocCommentReview.ClickBtn_SaveOnly();

            Thread.Sleep(2000);
            DesignDocCommentReview.ClickBtn_BackToList();
            WaitForPageReady();
        }

        public override void ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            EnterComment(CommentType.CommentResponseInput);
            DesignDocCommentReview.SelectDisagreeResponseCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            DesignDocCommentReview.ClickCommentTabNumber(commentTabNumber);
            EnterComment(CommentType.CommentResponseInput, commentTabNumber);
            DesignDocCommentReview.SelectDisagreeResponseCode(commentTabNumber);
            DesignDocCommentReview.ClickBtn_SaveOnly();
        }

        public override void EnterAndForwardClosingComment()
        {
            WaitForPageReady();
            DesignDocCommentReview.ClickTab_Pending_Closing();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            EnterClosingCommentAndCode();
        }

        public override void EnterClosingCommentAndCode()
        {
            WaitForPageReady();
            EnterComment(CommentType.CommentClosingInput);
            DesignDocCommentReview.SelectDDL_ClosingStamp();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            DesignDocCommentReview.ClickCommentTabNumber(commentTabNumber);
            EnterComment(CommentType.CommentClosingInput, commentTabNumber);
            DesignDocCommentReview.SelectDDL_ClosingStamp(commentTabNumber);
            DesignDocCommentReview.ClickBtn_SaveOnly();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterAndForwardClosingComment(UserType user)
        {
            WaitForPageReady();
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            EnterAndForwardClosingComment();
        }

        public override void EnterResponseAndAgreeResponseCode(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            //DesignDocCommentReview.FilterDocNumber();
            //TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterResponseCommentAndAgreeResponseCode();
        }
    }

    internal class DesignDocumentWF_I15Tech : DesignDocumentWF
    {
        public DesignDocumentWF_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class DesignDocumentWF_I15South : DesignDocumentWF
    {
        public DesignDocumentWF_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class DesignDocumentWF_LAX : DesignDocumentWF
    {
        public DesignDocumentWF_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}