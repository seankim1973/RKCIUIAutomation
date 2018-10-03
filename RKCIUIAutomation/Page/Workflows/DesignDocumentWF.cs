﻿using NUnit.Framework;
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

        void EnterResolutionCommentAndResolutionCodeforDisagreeResponse(UserType user);

        void ForwardResolutionCommentAndCodeForDisagreeResponse();

        void EnterAndForwardClosingComment();

        void EnterAndForwardClosingComment(UserType user);
    }

    public abstract class DesignDocumentWF_Impl : TestBase, IDesignDocumentWF
    {
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IDesignDocumentWF SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IDesignDocumentWF instance = new DesignDocumentWF(driver);

            if (tenantName == TenantName.SGWay)
            {
                LogInfo($"###### using DesignDocumentWF_SGWay instance ###### ");
                instance = new DesignDocumentWF_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                LogInfo($"###### using DesignDocumentWF_SH249 instance ###### ");
                instance = new DesignDocumentWF_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                LogInfo($"###### using DesignDocumentWF_Garnet instance ###### ");
                instance = new DesignDocumentWF_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                LogInfo($"###### using DesignDocumentWF_GLX instance ###### ");
                instance = new DesignDocumentWF_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                LogInfo($"###### using DesignDocumentWF_I15South instance ###### ");
                instance = new DesignDocumentWF_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                LogInfo($"###### using DesignDocumentWF_I15Tech instance ###### ");
                instance = new DesignDocumentWF_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                LogInfo($"###### using DesignDocumentWF_LAX instance ###### ");
                instance = new DesignDocumentWF_LAX(driver);
            }
            return instance;
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
            DesignDocCommentReview.FilterTableByValue();
            //DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        //Garnet and GLX
        public virtual void EnterNoComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            //DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterNoComment();
        }

        public virtual void ForwardComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            //DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public virtual void ForwardResponseComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Response();
            DesignDocCommentReview.FilterTableByValue();
            //DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.ClickBtnJs_SaveForward();
            DesignDocCommentReview.ClickBtn_BackToList();
        }

        public virtual void EnterResponseAndDisagreeResponseCode(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Response();
            DesignDocCommentReview.FilterTableByValue();
            //DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            EnterResponseCommentAndDisagreeResponseCode();
        }

        //Garnet
        public virtual void ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            DesignDocCommentReview.FilterTableByValue();
            //DesignDocCommentReview.SortTable_Descending();
            ClickEnterBtnForRow();
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
            DesignDocCommentReview.FilterTableByValue();
            //DesignDocCommentReview.SortTable_Descending();
            WaitForPageReady();
            ClickEnterBtnForRow();
            WaitForPageReady();
            Thread.Sleep(2000);
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
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2.Login As DOT User and Enter Regular Comment----------------------");
            EnterRegularComment(UserType.DOTUser);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Login As DOT Admin and Forward Comment----------------------");
            ForwardComment(UserType.DOTAdmin);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------4.Login As DEV User, Enter Response and Disagree Response Code----------------------");
            EnterResponseAndDisagreeResponseCode(UserType.DEVUser);
            ClickLogoutLink();
            ClickLoginLink();

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
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2.Login As DOT User and enter no comment----------------------");
            EnterNoComment(UserType.DOTUser);
            ClickLogoutLink();
            ClickLoginLink();

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

        public override void ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Pending_Resolution();
            DesignDocCommentReview.FilterTableByValue();
            //DesignDocCommentReview.SortTable_Descending();
            ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {            
            DesignDocCommentReview.ClickTab_Pending_Resolution();
            DesignDocCommentReview.FilterTableByValue();
            //DesignDocCommentReview.SortTable_Descending();
            WaitForPageReady();
            ClickEnterBtnForRow();
            WaitForPageReady();
            Thread.Sleep(2000);
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
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2. Log in as IQF User, enters Comments----------------------");
            EnterRegularComment(UserType.IQFUser);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment(UserType.IQFAdmin);

            LogInfo("-------------------------4. Log in as IQF Admin, Enters,forwards Response and Resolution stampcode----------------------");
            EnterResponseCommentAndDisagreeResponseCode();
            TableHelper.ClickEnterBtnForRow(); //?

            LogInfo("--------------------------5. Log in as IQF Admin, Enters,forwards closing comment----------------------");
            DesignDocCommentReview.EnterClosingCommentAndCode();

            LogInfo("--------------------------6. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }

        public override void TCWF_CommentReviewNoComment()
        {
            LogInfo("--------------------------1. Log in as IQF RecordsManager'----------------------");
            CreateDesignDocCommentReviewDocument(UserType.IQFRecordsMgr);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2. Log in as IQF User, enter no Comments----------------------");
            EnterNoComment(UserType.IQFUser);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment(UserType.IQFAdmin);

            LogInfo("-------------------------4. Log in as IQF Admin, Enters,forwards Response and Resolution stampcode----------------------");
            EnterResponseCommentAndDisagreeResponseCode();
            TableHelper.ClickEnterBtnForRow();

            LogInfo("--------------------------5. Log in as IQF Admin, Enters,forwards closing comment----------------------");
            DesignDocCommentReview.EnterClosingCommentAndCode();

            LogInfo("--------------------------6. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }

        public override void EnterRegularComment(UserType user)
        {
            LoginAs(UserType.IQFUser);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Comment();
            //TableHelper.ClickTab(TableTab.Requires_Comment);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        public override void EnterNoComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Comment();
            //TableHelper.ClickTab(TableTab.Requires_Comment);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterNoComment();
        }

        public override void ForwardComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Comment();
            //TableHelper.ClickTab(TableTab.Requires_Comment);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
            TableHelper.ClickEnterBtnForRow();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            //This will add response and resolution both together for 249 tenant.
            EnterComment(CommentType.CommentResponseInput);
            EnterComment(CommentType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
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
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2. Log in as DOT User, enters Comments----------------------");
            EnterRegularComment(UserType.DOTUser);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Log in as IQF User, enters Comments----------------------");
            EnterRegularComment(UserType.IQFUser);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------4. Log in as DOT Admin, forwards Comments----------------------");
            ForwardComment(UserType.DOTAdmin);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------5. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment(UserType.IQFAdmin);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------6. Log in as DEV User, enters Response and Disagree Response Code----------------------");
            EnterResponseAndDisagreeResponseCode(UserType.DEVUser);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------7. Log in as DEV Admin, forwards Response Comments----------------------");
            ForwardResponseComment(UserType.DEVAdmin);
            ClickLogoutLink();
            ClickLoginLink();

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
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2. Log in as DOT User, enter no Comments----------------------");
            EnterNoComment(UserType.DOTUser);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Log in as IQF User, enter no Comments----------------------");
            EnterNoComment(UserType.IQFUser);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------4. Log in as DOT Admin, forwards Comments----------------------");
            ForwardComment(UserType.DOTAdmin);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------5. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment(UserType.IQFAdmin);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------6. Log in as DEV User, enters Response and Agree Response Code----------------------");
            EnterResponseAndAgreeResponseCode(UserType.DEVUser);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------7. Log in as DEV Admin, forwards Response Comments----------------------");
            ForwardResponseComment(UserType.DEVAdmin);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------8. Log in as IQF Admin, Enters,forwards closing comment----------------------");
            EnterAndForwardClosingComment(UserType.IQFAdmin);

            LogInfo("--------------------------9. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }

        public override void EnterRegularComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        public override void EnterNoComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterNoComment();
        }

        public override void ForwardComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
            Thread.Sleep(2000);
        }

        public override void ForwardResponseComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Response();
            //TableHelper.ClickTab(DesignDocument.TableTab.Requires_Response);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.ClickBtnJs_SaveForward();
        }

        public override void EnterResponseAndDisagreeResponseCode(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Response();
            //TableHelper.ClickTab(TableTab.Requires_Response);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            EnterResponseCommentAndDisagreeResponseCode();
        }

        public override void EnterResolutionCommentAndResolutionCodeforDisagreeResponse(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();

            EnterComment(CommentType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
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
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            EnterComment(CommentType.CommentResponseInput);
            DesignDocCommentReview.SelectDisagreeResponseCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterComment(CommentType.CommentResponseInput, commentTabNumber);
            DesignDocCommentReview.SelectDisagreeResponseCode(commentTabNumber);
            DesignDocCommentReview.ClickBtn_SaveOnly();
        }

        public override void EnterAndForwardClosingComment()
        {
            WaitForPageReady();
            DesignDocCommentReview.ClickTab_Pending_Closing();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterClosingCommentAndCode();
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
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
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