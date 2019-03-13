using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Test;
using System;
using System.Threading;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.DesignDocument;
using static RKCIUIAutomation.Page.Workflows.DesignDocumentWF;

namespace RKCIUIAutomation.Page.Workflows
{
    public class DesignDocumentWF : DesignDocumentWF_Impl
    {
        public DesignDocumentWF()
        {
        }

        public DesignDocumentWF(IWebDriver driver) => this.Driver = driver;

        public enum CR_Workflow
        {
            CreateComment,
            EnterComment,
            ForwardComment,
            EnterComment_DOT,
            ForwardComment_DOT,
            EnterResponse,
            ForwardResponse,
            EnterResolution,
            ForwardResolution,
            ClosingComment, 
            ForwardClosingComment
        }

        private bool AlreadyInDesignDocumentPage()
            => VerifyPageTitle("Design Document");

        internal void LoginToDesignDocuments(CR_Workflow workflow)
        {
            /**
            // IQF User - AtCRCreate@rkci.com - CreateComment & EnterComment(SG & SH249)
            // IQF Records Mgr - CreateComment(SG & SH249) & FwdComment(SH249)
            // DOT User - ATCRComment @rkci.com - EnterComment
            // DOT Admin - ATCRCommentAdmin@rkci.com - FwdComment
            // IQF Admin - FwdComment(SG) & EnterResolution(SG)
            // *Dev User - ATCRResponse@rkci.com - EnterResponse
            // *Dev Admin - ATCRResponseAdmin@rkci.com - FwdResponse    
            // *Dev User - ATCRVerify@rkci.com       
            // *Dev Admin - ATCRVerifyAdmin@rkci.com
            */
            var currentTenant = tenantName;
            UserType cdrUserAcct = UserType.Bhoomi;

            switch (currentTenant)
            {
                case TenantName.LAX:
                    switch (workflow)
                    {
                        case CR_Workflow.CreateComment:
                            cdrUserAcct = UserType.CR_Create;
                            break;
                        case CR_Workflow.EnterComment:
                            cdrUserAcct = UserType.CR_Comment;
                            break;
                        case CR_Workflow.ForwardComment:
                            cdrUserAcct = UserType.CR_CommentAdmin;
                            break;
                        case CR_Workflow.EnterResponse:
                            cdrUserAcct = UserType.CR_Response;
                            break;
                        case CR_Workflow.ForwardResponse:
                            cdrUserAcct = UserType.CR_ResponseAdmin;
                            break;
                        case CR_Workflow.ClosingComment:
                            cdrUserAcct = UserType.CR_Verify;
                            break;
                        case CR_Workflow.ForwardClosingComment:
                            cdrUserAcct = UserType.CR_VerifyAdmin;
                            break;
                    }
                    break;

                case TenantName.SGWay:
                    switch (workflow)
                    {
                        case CR_Workflow.CreateComment:
                            cdrUserAcct = UserType.IQFRecordsMgr;
                            break;
                        case CR_Workflow.EnterComment:
                            cdrUserAcct = UserType.IQFUser;
                            break;
                        case CR_Workflow.ForwardComment:
                            cdrUserAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.EnterComment_DOT:
                            cdrUserAcct = UserType.DOTUser;
                            break;
                        case CR_Workflow.ForwardComment_DOT:
                            cdrUserAcct = UserType.DOTAdmin;
                            break;
                        case CR_Workflow.EnterResponse:
                            cdrUserAcct = UserType.DEVUser;
                            break;
                        case CR_Workflow.ForwardResponse:
                            cdrUserAcct = UserType.DEVAdmin;
                            break;
                        case CR_Workflow.EnterResolution:
                            cdrUserAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.ForwardResolution:
                            cdrUserAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.ClosingComment:
                            cdrUserAcct = UserType.IQFAdmin;
                            break;
                    }
                    break;

                case TenantName.SH249:
                    switch (workflow)
                    {
                        case CR_Workflow.CreateComment:
                            cdrUserAcct = UserType.IQFRecordsMgr;
                            break;
                        case CR_Workflow.EnterComment:
                            cdrUserAcct = UserType.IQFUser;
                            break;
                        case CR_Workflow.ForwardComment:
                            cdrUserAcct = UserType.IQFRecordsMgr;
                            break;
                        case CR_Workflow.EnterResponse:
                            cdrUserAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.ForwardResponse:
                            cdrUserAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.EnterResolution:
                            cdrUserAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.ForwardResolution:
                            cdrUserAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.ClosingComment:
                            cdrUserAcct = UserType.IQFAdmin;
                            break;
                    }
                    break;

                default:
                    switch (workflow)
                    {
                        case CR_Workflow.CreateComment:
                            cdrUserAcct = UserType.IQFUser;
                            break;
                        case CR_Workflow.EnterComment:
                            cdrUserAcct = UserType.DOTUser;
                            break;
                        case CR_Workflow.ForwardComment:
                            cdrUserAcct = UserType.DOTAdmin;
                            break;
                        case CR_Workflow.EnterResponse:
                            cdrUserAcct = UserType.DEVUser;
                            break;
                        case CR_Workflow.ForwardResponse:
                            cdrUserAcct = UserType.DEVAdmin;
                            break;
                        case CR_Workflow.EnterResolution:
                            cdrUserAcct = UserType.DEVAdmin;
                            break;
                        case CR_Workflow.ForwardResolution:
                            cdrUserAcct = UserType.DEVAdmin;
                            break;
                    }
                    break;
            }

            LoginAs(cdrUserAcct);

            try
            {
                if (!AlreadyInDesignDocumentPage())
                {
                    NavigateToPage.RMCenter_Design_Documents();
                }
            }
            catch (Exception)
            {
            }
        }

    }

    public interface IDesignDocumentWF
    {
        void TCWF_CommentReviewRegularComment();

        void TCWF_CommentReviewNoComment();

        void CreateDesignDocCommentReviewDocument(CR_Workflow workflowType = CR_Workflow.CreateComment);

        void EnterRegularComment(CR_Workflow workflowType = CR_Workflow.EnterComment);

        void EnterNoComment(CR_Workflow workflowType = CR_Workflow.EnterComment);

        void ForwardComment(CR_Workflow workflowType = CR_Workflow.ForwardComment);

        void ForwardResponseComment(CR_Workflow workflowType = CR_Workflow.ForwardResponse);

        void EnterResponseAndDisagreeResponseCode(CR_Workflow workflowType = CR_Workflow.EnterResponse);

        void EnterResponseAndAgreeResponseCode(CR_Workflow workflowType = CR_Workflow.EnterResponse);

        void EnterResolutionCommentAndResolutionCodeforDisagreeResponse(CR_Workflow workflowType = CR_Workflow.EnterResolution);

        void EnterAndForwardClosingComment(CR_Workflow workflowType = CR_Workflow.ClosingComment);

        void EnterClosingCommentAndCode();

        void ForwardResolutionCommentAndCodeForDisagreeResponse();

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
        public virtual void CreateDesignDocCommentReviewDocument(CR_Workflow workflowType = CR_Workflow.CreateComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            AddAssertionToList(VerifyPageTitle("Design Document"), "VerifyPageTitle(\"Design Document\")");
            DesignDocCommentReview.CreateDocument();
        }

        //Garnet and GLX
        public virtual void EnterRegularComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        //Garnet and GLX
        public virtual void EnterNoComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterNoComment();
        }

        public virtual void ForwardComment(CR_Workflow workflowType = CR_Workflow.ForwardComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_SaveForward();
            DesignDocCommentReview.WaitForActiveCommentTab();
            WaitForPageReady();
        }

        public virtual void ForwardResponseComment(CR_Workflow workflowType = CR_Workflow.ForwardResponse)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public virtual void EnterResponseAndDisagreeResponseCode(CR_Workflow workflowType = CR_Workflow.EnterResponse)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            EnterResponseCommentAndDisagreeResponseCode();
        }

        //Garnet
        public virtual void ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        //Garnet
        public virtual void EnterResolutionCommentAndResolutionCodeforDisagreeResponse(CR_Workflow workflowType = CR_Workflow.EnterResolution)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterTextInCommentField(CommentType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            Thread.Sleep(2000);
            DesignDocCommentReview.ClickBtn_BackToList();
        }

        public virtual void EnterResponseCommentAndDisagreeResponseCode()
        {
            // Login as user to make response comment (All tenants - DevUser)
            DesignDocCommentReview.EnterTextInCommentField(CommentType.CommentResponseInput);
            DesignDocCommentReview.SelectDisagreeResponseCode(); //agree then different workflow
            DesignDocCommentReview.ClickBtn_SaveOnly();
        }

        public virtual void EnterResponseAndAgreeResponseCode(CR_Workflow workflowType = CR_Workflow.EnterResponse)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterResponseCommentAndAgreeResponseCode();
        }

        public virtual void EnterClosingCommentAndCode()
        {
        }

        public virtual void EnterAndForwardClosingComment(CR_Workflow workflowType = CR_Workflow.ClosingComment)
        {
        }

        /// <summary>
        /// Comment Review Regular Comment Test Case Workflow for Garnet and GLX
        /// </summary>
        public virtual void TCWF_CommentReviewRegularComment()
        {
            LogInfo("--------------------------1.Login As IQF User and Create Document----------------------");
            CreateDesignDocCommentReviewDocument();//UserType.IQFUser
            LogoutToLoginPage();

            LogInfo("--------------------------2.Login As DOT User and Enter Regular Comment----------------------");
            EnterRegularComment(CR_Workflow.EnterComment_DOT);//UserType.DOTUser
            LogoutToLoginPage();

            LogInfo("--------------------------3. Login As DOT Admin and Forward Comment----------------------");
            ForwardComment(CR_Workflow.ForwardComment_DOT);//UserType.DOTAdmin
            LogoutToLoginPage();

            LogInfo("--------------------------4.Login As DEV User, Enter Response and Disagree Response Code----------------------");
            EnterResponseAndDisagreeResponseCode();//UserType.DEVUser
            LogoutToLoginPage();

            LogInfo("--------------------------5. Login As DEV Admin and Forward Response Comment----------------------");
            ForwardResponseComment();//UserType.DEVAdmin

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
            CreateDesignDocCommentReviewDocument();//UserType.IQFUser
            LogoutToLoginPage();

            LogInfo("--------------------------2.Login As DOT User and enter no comment----------------------");
            EnterNoComment(CR_Workflow.EnterComment_DOT);//UserType.DOTUser
            LogoutToLoginPage();

            LogInfo("--------------------------3. Login As DOT Admin and Forward Comment----------------------");
            ForwardComment(CR_Workflow.ForwardComment_DOT);//UserType.DOTAdmin

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
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterResolutionCommentAndResolutionCodeforDisagreeResponse(CR_Workflow workflowType = CR_Workflow.EnterResolution)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Pending_Resolution();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterTextInCommentField(CommentType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            Thread.Sleep(2000);
            DesignDocCommentReview.ClickBtn_BackToList();
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
            CreateDesignDocCommentReviewDocument();//UserType.IQFRecordsMgr
            LogoutToLoginPage();

            LogInfo("--------------------------2. Log in as IQF User, enters Comments----------------------");
            EnterRegularComment();//UserType.IQFUser
            LogoutToLoginPage();

            LogInfo("--------------------------3. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment();//UserType.IQFRecordsMgr  //Workaround: using IQF Rcrds Mgr, instead of IQF Admin

            LogInfo("-------------------------4. Log in as IQF Admin, Enters,forwards Response and Resolution stampcode----------------------");
            EnterResponseCommentAndDisagreeResponseCode();

            LogInfo("--------------------------5. Log in as IQF Admin, Enters,forwards closing comment----------------------");
            EnterClosingCommentAndCode();

            LogInfo("--------------------------6. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }

        #region
        public override void TCWF_CommentReviewNoComment()
        {
            LogInfo("--------------------------1. Log in as IQF RecordsManager'----------------------");
            CreateDesignDocCommentReviewDocument();//UserType.IQFRecordsMgr
            LogoutToLoginPage();

            LogInfo("--------------------------2. Log in as IQF User, enter no Comments----------------------");
            EnterNoComment();//UserType.IQFUser
            LogoutToLoginPage();

            LogInfo("--------------------------3. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment();//UserType.IQFRecordsMgr  //Workaround: using IQF Rcrds Mgr, instead of IQF Admin

            LogInfo("-------------------------4. Log in as IQF Admin, Enters,forwards Response and Resolution stampcode----------------------");
            EnterResponseCommentAndDisagreeResponseCode();

            LogInfo("--------------------------5. Log in as IQF Admin, Enters,forwards closing comment----------------------");
            EnterClosingCommentAndCode();

            LogInfo("--------------------------6. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }

        public override void EnterRegularComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Comment();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        public override void EnterNoComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Comment();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterNoComment();
        }

        public override void ForwardComment(CR_Workflow workflowType = CR_Workflow.ForwardComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Comment();
            FilterTableAndEditDoc();

            DesignDocument ddBase = new DesignDocument();
            ddBase.ScrollToLastColumn();

            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            DesignDocWF.LoginToDesignDocuments(CR_Workflow.EnterResponse);
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_CommentsTblRow_Edit();
            DesignDocCommentReview.EnterTextInCommentField(CommentType_InTable.CommentResponseInput);
            DesignDocCommentReview.EnterTextInCommentField(CommentType_InTable.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_Update();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterClosingCommentAndCode()
        {
            DesignDocWF.LoginToDesignDocuments(CR_Workflow.ClosingComment);
            DesignDocCommentReview.ClickTab_Requires_Closing();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_CommentsTblRow_Edit();
            DesignDocCommentReview.EnterTextInCommentField(CommentType_InTable.CommentClosingInput);
            DesignDocCommentReview.SelectDDL_ClosingStamp();
            DesignDocCommentReview.ClickBtn_Update();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void FilterTableAndEditDoc(string docNumber = "")
        {
            DesignDocCommentReview.FilterDocNumber(docNumber);
            ClickEnterBtnForRow();
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
            CreateDesignDocCommentReviewDocument();//UserType.IQFRecordsMgr
            LogoutToLoginPage();

            LogInfo("--------------------------2. Log in as DOT User, enters Comments----------------------");
            EnterRegularComment(CR_Workflow.EnterComment_DOT);//UserType.DOTUser
            LogoutToLoginPage();

            LogInfo("--------------------------3. Log in as IQF User, enters Comments----------------------");
            EnterRegularComment();//UserType.IQFUser
            LogoutToLoginPage();

            LogInfo("--------------------------4. Log in as DOT Admin, forwards Comments----------------------");
            ForwardComment(CR_Workflow.ForwardComment_DOT);//UserType.DOTAdmin
            LogoutToLoginPage();

            LogInfo("--------------------------5. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment();//UserType.IQFAdmin
            LogoutToLoginPage();

            LogInfo("--------------------------6. Log in as DEV User, enters Response and Disagree Response Code----------------------");
            EnterResponseAndDisagreeResponseCode();//UserType.DEVUser
            LogoutToLoginPage();

            LogInfo("--------------------------7. Log in as DEV Admin, forwards Response Comments----------------------");
            ForwardResponseComment();//UserType.DEVAdmin
            LogoutToLoginPage();

            LogInfo("--------------------------8. Log in as IQF Admin, Add Resolution Comment for Disagree workflow ----------------------");
            EnterResolutionCommentAndResolutionCodeforDisagreeResponse();//UserType.IQFAdmin

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
            CreateDesignDocCommentReviewDocument();//UserType.IQFRecordsMgr
            LogoutToLoginPage();

            LogInfo("--------------------------2. Log in as DOT User, enter no Comments----------------------");
            EnterNoComment(CR_Workflow.EnterComment_DOT);//UserType.DOTUser
            LogoutToLoginPage();

            LogInfo("--------------------------3. Log in as IQF User, enter no Comments----------------------");
            EnterNoComment();//UserType.IQFUser
            LogoutToLoginPage();

            LogInfo("--------------------------4. Log in as DOT Admin, forwards Comments----------------------");
            ForwardComment(CR_Workflow.ForwardComment_DOT);//UserType.DOTAdmin
            LogoutToLoginPage();

            LogInfo("--------------------------5. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment();//UserType.IQFAdmin
            LogoutToLoginPage();

            LogInfo("--------------------------6. Log in as DEV User, enters Response and Agree Response Code----------------------");
            EnterResponseAndAgreeResponseCode();//UserType.DEVUser
            LogoutToLoginPage();

            LogInfo("--------------------------7. Log in as DEV Admin, forwards Response Comments----------------------");
            ForwardResponseComment();//UserType.DEVAdmin
            LogoutToLoginPage();

            LogInfo("--------------------------8. Log in as IQF Admin, Enters,forwards closing comment----------------------");
            EnterAndForwardClosingComment();//UserType.IQFAdmin

            LogInfo("--------------------------9. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }

        public override void EnterRegularComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        public override void EnterNoComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterNoComment();
        }

        public override void ForwardComment(CR_Workflow workflowType = CR_Workflow.ForwardComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Comment();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_SaveForward();
            Thread.Sleep(2000);
        }

        public override void ForwardResponseComment(CR_Workflow workflowType = CR_Workflow.ForwardResponse)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterResponseAndDisagreeResponseCode(CR_Workflow workflowType = CR_Workflow.EnterResponse)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            EnterResponseCommentAndDisagreeResponseCode();
        }

        public override void EnterResolutionCommentAndResolutionCodeforDisagreeResponse(CR_Workflow workflowType = CR_Workflow.EnterResponse)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterTextInCommentField(CommentType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            DesignDocCommentReview.ClickCommentTabNumber(commentTabNumber);
            DesignDocCommentReview.EnterTextInCommentField(CommentType.CommentResolutionInput, commentTabNumber);
            DesignDocCommentReview.SelectDisagreeResolutionCode(commentTabNumber);
            DesignDocCommentReview.ClickBtn_SaveOnly();
            DesignDocCommentReview.ClickBtn_BackToList();
        }

        public override void ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            DesignDocCommentReview.EnterTextInCommentField(CommentType.CommentResponseInput);
            DesignDocCommentReview.SelectDisagreeResponseCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            DesignDocCommentReview.ClickCommentTabNumber(commentTabNumber);
            DesignDocCommentReview.EnterTextInCommentField(CommentType.CommentResponseInput, commentTabNumber);
            DesignDocCommentReview.SelectDisagreeResponseCode(commentTabNumber);
            DesignDocCommentReview.ClickBtn_SaveOnly();
        }

        public override void EnterAndForwardClosingComment(CR_Workflow workflowType = CR_Workflow.ClosingComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Pending_Closing();
            FilterTableAndEditDoc();
            EnterClosingCommentAndCode();
        }

        public override void EnterClosingCommentAndCode()
        {
            WaitForPageReady();
            DesignDocCommentReview.EnterTextInCommentField(CommentType.CommentClosingInput);
            DesignDocCommentReview.SelectDDL_ClosingStamp();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            DesignDocCommentReview.ClickCommentTabNumber(commentTabNumber);
            DesignDocCommentReview.EnterTextInCommentField(CommentType.CommentClosingInput, commentTabNumber);
            DesignDocCommentReview.SelectDDL_ClosingStamp(commentTabNumber);
            DesignDocCommentReview.ClickBtn_SaveOnly();
            DesignDocCommentReview.ClickBtn_SaveForward();
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

        public override void FilterTableAndEditDoc(string docNumber = "")
        {
            DesignDocCommentReview.FilterDocNumber(docNumber);
            ClickEnterBtnForRow();
        }

        public override void EnterRegularComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Comment();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        public override void ForwardComment(CR_Workflow workflowType = CR_Workflow.ForwardComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Comment();
            FilterTableAndEditDoc();

            DesignDocument ddBase = new DesignDocument();
            ddBase.ScrollToLastColumn();
            
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            DesignDocWF.LoginToDesignDocuments(CR_Workflow.EnterResponse);
            DesignDocCommentReview.ClickTab_Response();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_CommentsTblRow_Edit();
            DesignDocCommentReview.EnterTextInCommentField(CommentType_InTable.CommentResponseInput);
            DesignDocCommentReview.SelectDisagreeResponseCode(3);
            WaitForPageReady();
            //DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void ForwardResponseComment(CR_Workflow workflowType = CR_Workflow.ForwardResponse)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Response();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_CommentsTblRow_Edit();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterAndForwardClosingComment(CR_Workflow workflowType = CR_Workflow.ClosingComment)
        {
            DesignDocWF.LoginToDesignDocuments(workflowType);
            DesignDocCommentReview.ClickTab_Verification();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_CommentsTblRow_Edit();
            DesignDocCommentReview.EnterTextInCommentField(CommentType_InTable.VerifiedBy);
            DesignDocCommentReview.EnterTextInCommentField(PkgComments_TblHeader.VerifiedDate);
            DesignDocCommentReview.EnterTextInCommentField(CommentType_InTable.VerificationNotes);
            DesignDocCommentReview.SelectDDL_VerificationCode();
            WaitForPageReady();
            LogoutToLoginPage();

            DesignDocWF.LoginToDesignDocuments(CR_Workflow.ForwardClosingComment);
            DesignDocCommentReview.ClickTab_Verification();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_CommentsTblRow_Edit();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        #endregion
        public override void TCWF_CommentReviewRegularComment()
        {
            LogInfo("--------------------------1. Log in as ATCRCreate User, upload and forward to comment'----------------------");
            CreateDesignDocCommentReviewDocument();
            LogoutToLoginPage();

            LogInfo("--------------------------2. Log in as ATCRComment User, enters Comment----------------------");
            EnterRegularComment();
            LogoutToLoginPage();

            LogInfo("--------------------------3. Log in as ATCRComment Admin, forwards Comments to response----------------------");
            ForwardComment();
            LogoutToLoginPage();

            LogInfo("-------------------------4. Log in as ATCRResponse User, enters Response and Resolution stampcode to verification----------------------");
            EnterResponseCommentAndDisagreeResponseCode();
            LogoutToLoginPage();

            LogInfo("-------------------------5. Log in as ATCRResponse Admin, enters Response and Resolution stampcode to verification----------------------");
            ForwardResponseComment();
            LogoutToLoginPage();

            LogInfo("--------------------------8. Log in as Enter Closing Comment as ATCRVerify and forward as ATCRVerify Admin (Resolution='Disagree workflow')----------------------");
            EnterAndForwardClosingComment();

            LogInfo("--------------------------9. ATCRVerify Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
        }
    }
}