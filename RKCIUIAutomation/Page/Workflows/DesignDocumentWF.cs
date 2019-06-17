using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Test;
using System;
using System.Threading;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.DesignDocument;
using static RKCIUIAutomation.Page.Workflows.DesignDocumentWF;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page.Workflows
{
    public class DesignDocumentWF : DesignDocumentWF_Impl
    {
        public DesignDocumentWF()
        {
        }

        public DesignDocumentWF(IWebDriver driver) => this.Driver = driver;

        public T SetClass<T>(IWebDriver driver)
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
            return (T)instance;
        }

        public enum CR_Workflow
        {
            [StringValue("")] CreateComment,
            [StringValue("Requires Comment")] EnterComment,
            [StringValue("")] ForwardComment,
            [StringValue("")] EnterComment_DOT,
            [StringValue("")] ForwardComment_DOT,
            [StringValue("")] EnterResponse,
            [StringValue("")] ForwardResponse,
            [StringValue("Requires Resolution")] EnterResolution,
            [StringValue("")] ForwardResolution,
            [StringValue("Requires Closing")] ClosingComment,
            [StringValue("")] ForwardClosingComment
        }

        private bool AlreadyInDesignDocumentPage()
            => VerifyPageHeader("Design Document");
            
        // IQF User - AtCRCreate@rkci.com - CreateComment & EnterComment(SG & SH249)
        // IQF Records Mgr - CreateComment(SG & SH249) & FwdComment(SH249)
        // DOT User - ATCRComment @rkci.com - EnterComment
        // DOT Admin - ATCRCommentAdmin@rkci.com - FwdComment
        // IQF Admin - FwdComment(SG) & EnterResolution(SG)
        // *Dev User - ATCRResponse@rkci.com - EnterResponse
        // *Dev Admin - ATCRResponseAdmin@rkci.com - FwdResponse    
        // *Dev User - ATCRVerify@rkci.com       
        // *Dev Admin - ATCRVerifyAdmin@rkci.com
        public override void LogIntoDesignDocumentsPage(CR_Workflow workflow)
        {
            var currentTenant = tenantName;
            UserType userAcct = UserType.Bhoomi;

            switch (currentTenant)
            {
                case TenantName.LAX:
                    switch (workflow)
                    {
                        case CR_Workflow.CreateComment:
                            userAcct = UserType.CR_Create;
                            break;
                        case CR_Workflow.EnterComment:
                            userAcct = UserType.CR_Comment;
                            break;
                        case CR_Workflow.ForwardComment:
                            userAcct = UserType.CR_CommentAdmin;
                            break;
                        case CR_Workflow.EnterResponse:
                            userAcct = UserType.CR_Response;
                            break;
                        case CR_Workflow.ForwardResponse:
                            userAcct = UserType.CR_ResponseAdmin;
                            break;
                        case CR_Workflow.ClosingComment:
                            userAcct = UserType.CR_Verify;
                            break;
                        case CR_Workflow.ForwardClosingComment:
                            userAcct = UserType.CR_VerifyAdmin;
                            break;
                    }
                    break;

                case TenantName.SH249:
                    switch (workflow)
                    {
                        case CR_Workflow.CreateComment:
                            userAcct = UserType.IQFRecordsMgr;
                            break;
                        case CR_Workflow.EnterComment:
                            userAcct = UserType.IQFUser;
                            break;
                        case CR_Workflow.ForwardComment:
                            userAcct = UserType.IQFRecordsMgr;
                            break;
                        case CR_Workflow.EnterResponse:
                            userAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.ForwardResponse:
                            userAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.EnterResolution:
                            userAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.ForwardResolution:
                            userAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.ClosingComment:
                            userAcct = UserType.IQFAdmin;
                            break;
                    }
                    break;

                case TenantName.SGWay:
                    switch (workflow)
                    {
                        case CR_Workflow.CreateComment:
                            userAcct = UserType.IQFRecordsMgr;
                            break;
                        case CR_Workflow.EnterComment:
                            userAcct = UserType.IQFUser;
                            break;
                        case CR_Workflow.ForwardComment:
                            userAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.EnterComment_DOT:
                            userAcct = UserType.DOTUser;
                            break;
                        case CR_Workflow.ForwardComment_DOT:
                            userAcct = UserType.DOTAdmin;
                            break;
                        case CR_Workflow.EnterResponse:
                            userAcct = UserType.DEVUser;
                            break;
                        case CR_Workflow.ForwardResponse:
                            userAcct = UserType.DEVAdmin;
                            break;
                        case CR_Workflow.EnterResolution:
                            userAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.ForwardResolution:
                            userAcct = UserType.IQFAdmin;
                            break;
                        case CR_Workflow.ClosingComment:
                            userAcct = UserType.IQFAdmin;
                            break;
                    }
                    break;

                default:
                    switch (workflow)
                    {
                        case CR_Workflow.CreateComment:
                            userAcct = UserType.IQFUser;
                            break;
                        case CR_Workflow.EnterComment:
                            userAcct = UserType.DOTUser;
                            break;
                        case CR_Workflow.ForwardComment:
                            userAcct = UserType.DOTAdmin;
                            break;
                        case CR_Workflow.EnterResponse:
                            userAcct = UserType.DEVUser;
                            break;
                        case CR_Workflow.ForwardResponse:
                            userAcct = UserType.DEVAdmin;
                            break;
                        case CR_Workflow.EnterResolution:
                            userAcct = UserType.DEVAdmin;
                            break;
                        case CR_Workflow.ForwardResolution:
                            userAcct = UserType.DEVAdmin;
                            break;
                    }
                    break;
            }

            LoginAs(userAcct);

            bool alreadyInDesignDocumentPage = AlreadyInDesignDocumentPage();

            if (!alreadyInDesignDocumentPage)
            {
                NavigateToPage.RMCenter_Design_Documents();
            }

            DesignDocCommentReview.SetDesignDocStatus(workflow);

            WaitForPageReady();
        }

        //All Tenants
        public override void CreateCommentReviewDocument(CR_Workflow workflowType = CR_Workflow.CreateComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            AddAssertionToList_VerifyPageHeader("Design Document", "CreateCommentReviewDocument");
            //AddAssertionToList(VerifyPageHeader("Design Document"), "VerifyPageTitle(\"Design Document\")");
            DesignDocCommentReview.CreateDocument();
        }

        public override void FilterTableAndEditDoc(string docNumber = "")
        {
            DesignDocCommentReview.FilterDocNumber(docNumber);
            GridHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.VerifyDesignDocDetailsHeader();
            DesignDocCommentReview.WaitForActiveCommentTab();
        }

        //Garnet and GLX
        public override void EnterRegularComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        //Garnet and GLX
        public override void EnterNoComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterNoComment();
        }

        public override void ForwardComment(CR_Workflow workflowType = CR_Workflow.ForwardComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_SaveForward();
            DesignDocCommentReview.WaitForActiveCommentTab();
            WaitForPageReady();
        }

        public override void ForwardResponseComment(CR_Workflow workflowType = CR_Workflow.ForwardResponse)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterResponseAndDisagreeResponseCode(CR_Workflow workflowType = CR_Workflow.EnterResponse)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            EnterResponseCommentAndDisagreeResponseCode();
        }

        //Garnet
        public override void ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        //Garnet
        public override void EnterResolutionCommentAndResolutionCodeforDisagreeResponse(CR_Workflow workflowType = CR_Workflow.EnterResolution)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            Thread.Sleep(2000);
            DesignDocCommentReview.ClickBtn_BackToList();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            // Login as user to make response comment (All tenants - DevUser)
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.CommentResponseInput);
            DesignDocCommentReview.SelectDisagreeResponseCode(); //agree then different workflow
            DesignDocCommentReview.ClickBtn_SaveOnly();
        }

        public override void EnterResponseAndAgreeResponseCode(CR_Workflow workflowType = CR_Workflow.EnterResponse)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterResponseCommentAndAgreeResponseCode();
        }

        public override void EnterClosingCommentAndCode()
        {
        }

        public override void EnterAndForwardClosingComment(CR_Workflow workflowType = CR_Workflow.ClosingComment)
        {
        }

        /// <summary>
        /// Comment Review Regular Comment Test Case Workflow for Garnet and GLX
        /// </summary>
        public override void TCWF_CommentReviewRegularComment()
        {
            Report.Step("STEP: 1.Login As IQF User and Create Document");
            CreateCommentReviewDocument();//UserType.IQFUser
            LogoutToLoginPage();

            Report.Step("STEP: 2.Login As DOT User and Enter Regular Comment");
            EnterRegularComment(CR_Workflow.EnterComment_DOT);//UserType.DOTUser
            LogoutToLoginPage();

            Report.Step("STEP: 3. Login As DOT Admin and Forward Comment");
            ForwardComment(CR_Workflow.ForwardComment_DOT);//UserType.DOTAdmin
            LogoutToLoginPage();

            Report.Step("STEP: 4.Login As DEV User, Enter Response and Disagree Response Code");
            EnterResponseAndDisagreeResponseCode();//UserType.DEVUser
            LogoutToLoginPage();

            Report.Step("STEP: 5. Login As DEV Admin and Forward Response Comment");
            ForwardResponseComment();//UserType.DEVAdmin

            Report.Step("STEP: 6. DEV Admin enters Resolution='Disagree workflow'");
            EnterResolutionCommentAndResolutionCodeforDisagreeResponse();

            Report.Step("STEP: 7. DEV Admin forwards Resolution='Disagree workflow'");
            ForwardResolutionCommentAndCodeForDisagreeResponse();

            Report.Step("STEP: 8. DEV Admin verifies if record in closed tab");
            DesignDocCommentReview.VerifyItemStatusIsClosed(ReviewType.RegularComment);
        }

        /// <summary>
        /// Comment Review No Comment Test Case Workflow for Garnet and GLX
        /// </summary>
        public override void TCWF_CommentReviewNoComment()
        {
            Report.Step("STEP: 1.Login As IQF User and Create Document");
            CreateCommentReviewDocument();//UserType.IQFUser
            LogoutToLoginPage();

            Report.Step("STEP: 2.Login As DOT User and enter no comment");
            EnterNoComment(CR_Workflow.EnterComment_DOT);//UserType.DOTUser
            LogoutToLoginPage();

            Report.Step("STEP: 3. Login As DOT Admin and Forward Comment");
            ForwardComment(CR_Workflow.ForwardComment_DOT);//UserType.DOTAdmin

            Report.Step("STEP: 4. DEV Admin verifies if record in closed tab");
            DesignDocCommentReview.VerifyItemStatusIsClosed(ReviewType.NoComment);
        }

    }

    public interface IDesignDocumentWF
    {
        void LogIntoDesignDocumentsPage(CR_Workflow workflowType);

        void TCWF_CommentReviewRegularComment();

        void TCWF_CommentReviewNoComment();

        void CreateCommentReviewDocument(CR_Workflow workflowType = CR_Workflow.CreateComment);

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

        void EnterResponseCommentAndDisagreeResponseCode();
    }

    public abstract class DesignDocumentWF_Impl : TestBase, IDesignDocumentWF
    {
        public abstract void CreateCommentReviewDocument(CR_Workflow workflowType = CR_Workflow.CreateComment);
        public abstract void EnterAndForwardClosingComment(CR_Workflow workflowType = CR_Workflow.ClosingComment);
        public abstract void EnterClosingCommentAndCode();
        public abstract void EnterNoComment(CR_Workflow workflowType = CR_Workflow.EnterComment);
        public abstract void EnterRegularComment(CR_Workflow workflowType = CR_Workflow.EnterComment);
        public abstract void EnterResolutionCommentAndResolutionCodeforDisagreeResponse(CR_Workflow workflowType = CR_Workflow.EnterResolution);
        public abstract void EnterResponseAndAgreeResponseCode(CR_Workflow workflowType = CR_Workflow.EnterResponse);
        public abstract void EnterResponseAndDisagreeResponseCode(CR_Workflow workflowType = CR_Workflow.EnterResponse);
        public abstract void EnterResponseCommentAndDisagreeResponseCode();
        public abstract void FilterTableAndEditDoc(string docNumber = "");
        public abstract void ForwardComment(CR_Workflow workflowType = CR_Workflow.ForwardComment);
        public abstract void ForwardResolutionCommentAndCodeForDisagreeResponse();
        public abstract void ForwardResponseComment(CR_Workflow workflowType = CR_Workflow.ForwardResponse);
        public abstract void LogIntoDesignDocumentsPage(CR_Workflow workflowType);
        public abstract void TCWF_CommentReviewNoComment();
        public abstract void TCWF_CommentReviewRegularComment();
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
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Pending_Resolution();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.CommentResolutionInput);
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
            Report.Step("STEP: 1. Log in as IQF RecordsManager");
            CreateCommentReviewDocument();//UserType.IQFRecordsMgr
            LogoutToLoginPage();

            Report.Step("STEP: 2. Log in as IQF User, enters Comments");
            EnterRegularComment();//UserType.IQFUser
            LogoutToLoginPage();

            Report.Step("STEP: 3. Log in as IQF Admin, forwards Comments");
            ForwardComment();//UserType.IQFRecordsMgr  //Workaround: using IQF Rcrds Mgr, instead of IQF Admin

            Report.Step("STEP: 4. Log in as IQF Admin, Enters,forwards Response and Resolution stampcode");
            EnterResponseCommentAndDisagreeResponseCode();

            Report.Step("STEP: 5. Log in as IQF Admin, Enters,forwards closing comment");
            EnterClosingCommentAndCode();

            Report.Step("STEP: 6. IQF Admin verifies if record in closed tab");
            DesignDocCommentReview.VerifyItemStatusIsClosed(ReviewType.RegularComment);
        }

        public override void TCWF_CommentReviewNoComment()
        {
            Report.Step("STEP: 1. Log in as IQF RecordsManager");
            CreateCommentReviewDocument();//UserType.IQFRecordsMgr
            LogoutToLoginPage();

            Report.Step("STEP: 2. Log in as IQF User, enter no Comments");
            EnterNoComment();//UserType.IQFUser
            LogoutToLoginPage();

            Report.Step("STEP: 3. Log in as IQF Admin, forwards Comments");
            ForwardComment();//UserType.IQFRecordsMgr  //Workaround: using IQF Rcrds Mgr, instead of IQF Admin

            //LogInfo("-------------------------4. Log in as IQF Admin, Enters,forwards Response and Resolution stampcode----------------------");
            //EnterResponseCommentAndDisagreeResponseCode();

            Report.Step("STEP: 4. Log in as IQF Admin, Enters,forwards closing comment");
            EnterClosingCommentAndCode();

            Report.Step("STEP: 5. IQF Admin verifies if record in closed tab");
            DesignDocCommentReview.VerifyItemStatusIsClosed(ReviewType.NoComment);
        }

        public override void EnterRegularComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Comment();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        public override void EnterNoComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Comment();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterNoComment();
        }

        public override void ForwardComment(CR_Workflow workflowType = CR_Workflow.ForwardComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Comment();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ScrollToLastColumn();
            DesignDocCommentReview.ClickBtn_SaveForward();
            WaitForPageReady();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            WaitForPageReady();
            Thread.Sleep(10000); //workaround for delay after SaveForward from previous step
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_CommentsTblRow_Edit();
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.CommentResponseInput_InTable);
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.CommentResolutionInput_InTable);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_Update();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterClosingCommentAndCode()
        {
            WaitForPageReady();
            Thread.Sleep(10000); //workaround for delay after SaveForward from previous step
            DesignDocCommentReview.ClickTab_Requires_Closing();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_CommentsTblRow_Edit();
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.CommentClosingInput_InTable);
            DesignDocCommentReview.SelectDDL_ClosingStamp();
            DesignDocCommentReview.ClickBtn_Update();
            DesignDocCommentReview.ClickBtn_SaveForward();

            //log.Warn("!!!Clicked BackToList button - workaround for bug EPA-2887!!!");
            //DesignDocCommentReview.ClickBtn_BackToList();
        }

        public override void FilterTableAndEditDoc(string docNumber = "")
        {
            DesignDocCommentReview.FilterDocNumber(docNumber);
            GridHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.VerifyDesignDocDetailsHeader();
        }
    }

    internal class DesignDocumentWF_SGWay : DesignDocumentWF
    {
        public DesignDocumentWF_SGWay(IWebDriver driver) : base(driver)
        {
        }
        
        public override void TCWF_CommentReviewRegularComment()
        {
            Report.Step("STEP: 1. Log in as IQFRMgr and Create Document");
            CreateCommentReviewDocument();//UserType.IQFRecordsMgr
            LogoutToLoginPage();

            Report.Step("STEP: 2. Log in as DOT User, enters Comments");
            EnterRegularComment(CR_Workflow.EnterComment_DOT);//UserType.DOTUser
            LogoutToLoginPage();

            Report.Step("STEP: 3. Log in as IQF User, enters Comments");
            EnterRegularComment();//UserType.IQFUser
            LogoutToLoginPage();

            Report.Step("STEP: 4. Log in as DOT Admin, forwards Comments");
            ForwardComment(CR_Workflow.ForwardComment_DOT);//UserType.DOTAdmin
            LogoutToLoginPage();

            Report.Step("STEP: 5. Log in as IQF Admin, forwards Comments");
            ForwardComment();//UserType.IQFAdmin
            LogoutToLoginPage();

            Report.Step("STEP: 6. Log in as DEV User, enters Response and Disagree Response Code");
            EnterResponseAndDisagreeResponseCode();//UserType.DEVUser
            LogoutToLoginPage();

            Report.Step("STEP: 7. Log in as DEV Admin, forwards Response Comments");
            ForwardResponseComment();//UserType.DEVAdmin
            LogoutToLoginPage();

            Report.Step("STEP: 8. Log in as IQF Admin, Add Resolution Comment for Disagree workflow");
            EnterResolutionCommentAndResolutionCodeforDisagreeResponse();//UserType.IQFAdmin

            Report.Step("STEP: 9. Log in as IQF Admin, forwards Resolution");
            ForwardResolutionCommentAndCodeForDisagreeResponse();

            Report.Step("STEP: 10. Log in as IQF Admin, Enters,forwards closing comment");
            EnterAndForwardClosingComment();

            Report.Step("STEP: 11. IQF Admin verifies if record in closed tab");
            DesignDocCommentReview.VerifyItemStatusIsClosed(ReviewType.RegularComment);
        }
        
        public override void TCWF_CommentReviewNoComment()
        {
            Report.Step("STEP: 1. Log in as IQFRM");
            CreateCommentReviewDocument();//UserType.IQFRecordsMgr
            LogoutToLoginPage();

            Report.Step("STEP: 2. Log in as DOT User, enter no Comments");
            EnterNoComment(CR_Workflow.EnterComment_DOT);//UserType.DOTUser - store data for DOT User
            LogoutToLoginPage();

            Report.Step("STEP: 3. Log in as IQF User, enter no Comments");
            EnterNoComment();//UserType.IQFUser - store data for verification for IQF User & verify data in comments tab 1 (DOTUser data - readonly)
            LogoutToLoginPage();

            Report.Step("STEP: 4. Log in as DOT Admin, forwards Comments");
            ForwardComment(CR_Workflow.ForwardComment_DOT);//UserType.DOTAdmin - verify data in comments tab 1 (DOTUser data - editable data)
            LogoutToLoginPage();

            Report.Step("STEP: 5. Log in as IQF Admin, forwards Comments");
            ForwardComment();//UserType.IQFAdmin
            LogoutToLoginPage();

            Report.Step("STEP: 6. Log in as DEV User, enters Response and Agree Response Code");
            EnterResponseAndAgreeResponseCode();//UserType.DEVUser
            LogoutToLoginPage();

            Report.Step("STEP: 7. Log in as DEV Admin, forwards Response Comments");
            ForwardResponseComment();//UserType.DEVAdmin
            LogoutToLoginPage();

            Report.Step("STEP: 8. Log in as IQF Admin, Enters,forwards closing comment");
            EnterAndForwardClosingComment();//UserType.IQFAdmin

            Report.Step("STEP: 9. IQF Admin verifies if record in closed tab");
            DesignDocCommentReview.VerifyItemStatusIsClosed(ReviewType.NoComment);
        }
        
        public override void EnterRegularComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        public override void EnterNoComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterNoComment();
        }

        public override void ForwardComment(CR_Workflow workflowType = CR_Workflow.ForwardComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Comment();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_SaveForward();
            Thread.Sleep(2000);
            WaitForPageReady();
        }

        public override void ForwardResponseComment(CR_Workflow workflowType = CR_Workflow.ForwardResponse)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterResponseAndDisagreeResponseCode(CR_Workflow workflowType = CR_Workflow.EnterResponse)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Response();
            FilterTableAndEditDoc();
            EnterResponseCommentAndDisagreeResponseCode();
        }

        public override void EnterResolutionCommentAndResolutionCodeforDisagreeResponse(CR_Workflow workflowType = CR_Workflow.EnterResolution)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            DesignDocCommentReview.ClickCommentTabNumber(commentTabNumber);
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.CommentResolutionInput, commentTabNumber);
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
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.CommentResponseInput);
            DesignDocCommentReview.SelectDisagreeResponseCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            DesignDocCommentReview.ClickCommentTabNumber(commentTabNumber);
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.CommentResponseInput, commentTabNumber);
            DesignDocCommentReview.SelectDisagreeResponseCode(commentTabNumber);
            DesignDocCommentReview.ClickBtn_SaveOnly();
        }

        public override void EnterAndForwardClosingComment(CR_Workflow workflowType = CR_Workflow.ClosingComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Pending_Closing();
            FilterTableAndEditDoc();
            EnterClosingCommentAndCode();
        }

        public override void EnterClosingCommentAndCode()
        {
            WaitForPageReady();
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.CommentClosingInput);
            DesignDocCommentReview.SelectDDL_ClosingStamp();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            DesignDocCommentReview.ClickCommentTabNumber(commentTabNumber);
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.CommentClosingInput, commentTabNumber);
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
            GridHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.VerifyDesignDocDetailsHeader();
        }

        public override void EnterRegularComment(CR_Workflow workflowType = CR_Workflow.EnterComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Comment();
            FilterTableAndEditDoc();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
        }

        public override void ForwardComment(CR_Workflow workflowType = CR_Workflow.ForwardComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Comment();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ScrollToLastColumn();            
            DesignDocCommentReview.ClickBtn_SaveForward();
            WaitForPageReady();
        }

        public override void EnterResponseCommentAndDisagreeResponseCode()
        {
            LogIntoDesignDocumentsPage(CR_Workflow.EnterResponse);
            DesignDocCommentReview.ClickTab_Response();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_CommentsTblRow_Edit();
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.CommentResponseInput_InTable);
            DesignDocCommentReview.SelectDisagreeResponseCode(3);
            WaitForPageReady();
            //DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void ForwardResponseComment(CR_Workflow workflowType = CR_Workflow.ForwardResponse)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Response();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_CommentsTblRow_Edit();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterAndForwardClosingComment(CR_Workflow workflowType = CR_Workflow.ClosingComment)
        {
            LogIntoDesignDocumentsPage(workflowType);
            DesignDocCommentReview.ClickTab_Verification();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_CommentsTblRow_Edit();
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.VerifiedBy_InTable);
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.VerifiedDate_InTable);
            DesignDocCommentReview.EnterCommentResponse(CommentFieldType.VerificationNotes_InTable);
            DesignDocCommentReview.SelectDDL_VerificationCode();
            WaitForPageReady();
            LogoutToLoginPage();

            LogIntoDesignDocumentsPage(CR_Workflow.ForwardClosingComment);
            DesignDocCommentReview.ClickTab_Verification();
            FilterTableAndEditDoc();
            DesignDocCommentReview.ClickBtn_CommentsTblRow_Edit();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void TCWF_CommentReviewRegularComment()
        {
            Report.Step("STEP: 1. Log in as ATCRCreate User, upload and forward to comment");
            CreateCommentReviewDocument();
            LogoutToLoginPage();

            Report.Step("STEP: 2. Log in as ATCRComment User, enters Comment");
            EnterRegularComment();
            LogoutToLoginPage();

            Report.Step("STEP: 3. Log in as ATCRComment Admin, forwards Comments to response");
            ForwardComment();
            LogoutToLoginPage();

            Report.Step("STEP: 4. Log in as ATCRResponse User, enters Response and Resolution stampcode to verification");
            EnterResponseCommentAndDisagreeResponseCode();
            LogoutToLoginPage();

            Report.Step("STEP: 5. Log in as ATCRResponse Admin, enters Response and Resolution stampcode to verification");
            ForwardResponseComment();
            LogoutToLoginPage();

            Report.Step("STEP: 6. Log in as ATCRVerify, enter Closing Comment and forward as ATCRVerify Admin (Resolution='Disagree workflow')");
            EnterAndForwardClosingComment();

            Report.Step("STEP: 7. ATCRVerify Admin verifies if record in closed tab");
            DesignDocCommentReview.VerifyItemStatusIsClosed(ReviewType.RegularComment);
        }
    }
}