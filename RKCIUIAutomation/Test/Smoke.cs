﻿using NUnit.Framework;
using NUnit.Framework.Internal;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.Workflows;
using System.Collections.Generic;
using System.Threading;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Test.Smoke
{
    [TestFixture]
    public class Verify_LinkCoverage_Level1 : TestBase
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2188206)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Site Pages Load Successfully")]
        public void LinkCoverage_Level1()
        {
            LoginAs(UserType.Bhoomi);

            List<string> pageUrls = new List<string>();
            pageUrls = GetNavMenuUrlList();
            Report.Info($"URL Count: {pageUrls.Count}");

            int loopCount = 0;

            foreach (var url in pageUrls)
            {
                loopCount++;
                AddAssertionToList(VerifyUrlIsLoaded(url), $"Verify URL is Loaded - {url}");
            }
            Report.Info($"Assertion Loop Count: {loopCount}");
            AssertAll();
        }
    }

    /*
    [TestFixture]
    public class Verify_CommentReviewRegularComment : DesignDocumentWF
    {
        [Test]
        [Category(Component.DesignDoc_CommentReview)]
        [Property(TestCaseNumber, 2238579)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Comment Review Regular Comment")]
        public void CommentReviewRegularComment()
        {
            WF_DesignDocCommentReview.TCWF_CommentReviewRegularComment();
        }
    }

    [TestFixture]
    public class Verify_CommentReviewNoComment : DesignDocumentWF
    {
        [Test]
        [Category(Component.DesignDoc_CommentReview)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Comment Review No Comment")]
        public void CommentReviewNoComment()
        {
            WF_DesignDocCommentReview.TCWF_CommentReviewNoComment();
        }
    }

    [TestFixture]//Garnet GLX
    public class Test_CommentReviewRegularComment : DesignDocumentWF
    {
        //        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2238579)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void CommentReviewPartialFunctions()
        {
            LogInfo("--------------------------1.Login As IQF User and Create Document----------------------");
            //CreateDesignDocCommentReviewDocument(UserType.IQFUser);

            LoginAs(UserType.IQFUser);//testing glx comment review
            NavigateToPage.RMCenter_Design_Documents();
            Assert.True(VerifyPageTitle("Design Document"));
            DesignDocCommentReview.CreateDocument();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2.Login As DOT User and Enter Regular Comment----------------------");
            //EnterRegularComment(UserType.DOTUser);

            LoginAs(UserType.DOTUser);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Login As DOT Admin and Forward Comment----------------------");
            //ForwardComment(UserType.DOTAdmin);

            LoginAs(UserType.DOTAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------4.Login As DEV User, Enter Response and Disagree Response Code----------------------");
            //EnterResponseAndDisagreeResponseCode(UserType.DEVUser);

            LoginAs(UserType.DEVUser);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Response();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            EnterResponseCommentAndDisagreeResponseCode();
            //DesignDocCommentReview.EnterResponseCommentAndDisagreeResponseCode();  //moved to DesignDocumentWF
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------5. Login As DEV Admin and Forward Response Comment----------------------");
            //ForwardResponseComment(UserType.DEVAdmin);

            LoginAs(UserType.DEVAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Response();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.ClickBtnJs_SaveForward();
            DesignDocCommentReview.ClickBtn_BackToList();

            LogInfo("--------------------------6. DEV Admin enters Resolution='Disagree workflow'----------------------");
            //EnterResolutionCommentAndResolutionCodeforDisagreeResponse();

            DesignDocCommentReview.Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse(); //Moved to DesignDocumentWF class

            LogInfo("--------------------------7. DEV Admin forwards Resolution='Disagree workflow'----------------------");
            //ForwardResolutionCommentAndCodeForDisagreeResponse();

            DesignDocCommentReview.Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse(); //Moved to DesignDocumentWF class

            LogInfo("--------------------------8. DEV Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
            Thread.Sleep(5000);
        }
    }

    [TestFixture]//Garnet GLX
    public class Test_CommentReviewNoComment : DesignDocumentWF
    {
        //        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, "111")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void CommentReviewNoCommentGarnetGLX()
        {
            LogInfo("--------------------------1.Login As IQF User and Create Document----------------------");
            //CreateDesignDocCommentReviewDocument(UserType.IQFUser);

            LoginAs(UserType.IQFUser);//testing glx comment review
            NavigateToPage.RMCenter_Design_Documents();
            Assert.True(VerifyPageTitle("Design Document"));
            DesignDocCommentReview.CreateDocument();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2.Login As DOT User and enter no comment----------------------");
            //EnterNoComment(UserType.DOTUser);

            LoginAs(UserType.DOTUser);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterNoComment();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Login As DOT Admin and Forward Comment----------------------");
            //ForwardComment(UserType.DOTAdmin);

            LoginAs(UserType.DOTAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();

            LogInfo("--------------------------4. DEV Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
            Thread.Sleep(5000);
        }
    }

    [TestFixture]//SG
    public class Test_CommentReviewRegularComment_ForSG : DesignDocumentWF
    {
        //        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2238580)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void CommentReviewForSG()
        {
            LogInfo("--------------------------1. Log in as IQFRMgr and Create Document----------------------");
            CreateDesignDocCommentReviewDocument(UserType.IQFRecordsMgr);

            //LoginAs(UserType.IQFRecordsMgr);
            //NavigateToPage.RMCenter_Design_Documents();
            //Assert.True(VerifyPageTitle("Design Document"));
            //DesignDocCommentReview.CreateDocument();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2. Log in as DOT User, enters Comments----------------------");
            EnterRegularComment(UserType.DOTUser);

            //LoginAs(UserType.DOTUser);
            //NavigateToPage.RMCenter_Design_Documents();
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Log in as IQF User, enters Comments----------------------");
            EnterRegularComment(UserType.IQFUser);

            //LoginAs(UserType.IQFUser);
            //NavigateToPage.RMCenter_Design_Documents();
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------4. Log in as DOT Admin, forwards Comments----------------------");
            ForwardComment(UserType.DOTAdmin);

            //LoginAs(UserType.DOTAdmin);
            //NavigateToPage.RMCenter_Design_Documents();
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            //DesignDocCommentReview.ClickBtn_SaveForward();
            //Thread.Sleep(2000);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------5. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment(UserType.IQFAdmin);

            //LoginAs(UserType.IQFAdmin);
            //NavigateToPage.RMCenter_Design_Documents();
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            //DesignDocCommentReview.ClickBtn_SaveForward();
            //Thread.Sleep(2000);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------6. Log in as DEV User, enters Response and Disagree Response Code----------------------");
            EnterResponseAndDisagreeResponseCode(UserType.DEVUser);

            //LoginAs(UserType.DEVUser);
            //NavigateToPage.RMCenter_Design_Documents();
            //TableHelper.ClickTab(DesignDocument.TableTab.Requires_Response);
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //EnterResponseCommentAndDisagreeResponseCode();
            ////DesignDocCommentReview.EnterResponseCommentAndDisagreeResponseCode();  //<--moved to DesignDocumentWF
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------7. Log in as DEV Admin, forwards Response Comments----------------------");
            ForwardResponseComment(UserType.DEVAdmin);

            //LoginAs(UserType.DEVAdmin);
            //NavigateToPage.RMCenter_Design_Documents();
            //TableHelper.ClickTab(DesignDocument.TableTab.Requires_Response);
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //DesignDocCommentReview.ClickBtnJs_SaveForward();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------8. Log in as IQF Admin, Add Resolution Comment for Disagree workflow ----------------------");
            EnterResolutionCommentAndResolutionCodeforDisagreeResponse(UserType.IQFAdmin); //<-- method includes all steps prior to and after method: DesignDocCommentReview.Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse()

            //LoginAs(UserType.IQFAdmin);
            //NavigateToPage.RMCenter_Design_Documents();
            //DesignDocCommentReview.ClickTab_Requires_Resolution();
            ////TableHelper.ClickTab(DesignDocument.TableTab.Requires_Resolution);
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            ////DesignDocCommentReview.Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse();  //<-- replaced entire section with EnterResolutionCommentAndResolutionCodeforDisagreeResponse method
            //Thread.Sleep(2000);
            //DesignDocCommentReview.ClickBtn_BackToList();
            //WaitForPageReady();

            LogInfo("--------------------------9. Log in as IQF Admin, forwards Resolution----------------------");
            ForwardResolutionCommentAndCodeForDisagreeResponse();

            //DesignDocCommentReview.ClickTab_Requires_Resolution();
            ////TableHelper.ClickTab(DesignDocument.TableTab.Requires_Resolution);
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            //DesignDocCommentReview.ClickBtn_SaveForward();

            LogInfo("--------------------------10. Log in as IQF Admin, Enters,forwards closing comment----------------------");

            WaitForPageReady();
            EnterAndForwardClosingComment();

            //DesignDocCommentReview.ClickTab_Pending_Closing();
            ////TableHelper.ClickTab(DesignDocument.TableTab.Pending_Closing);
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //DesignDocCommentReview.EnterClosingCommentAndCode();

            LogInfo("--------------------------11. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
            Thread.Sleep(5000);
        }
    }

    [TestFixture]//SG
    public class Test_CommentReviewNoComment_ForSG : DesignDocumentWF
    {
        //        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, "222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void NoCommentWfCommentReviewForSG()
        {
            LogInfo("--------------------------No comment Workflow begins----------------------");
            LogInfo("--------------------------1. Log in as IQFRM'----------------------");
            CreateDesignDocCommentReviewDocument(UserType.IQFRecordsMgr);

            //LoginAs(UserType.IQFRecordsMgr);
            //NavigateToPage.RMCenter_Design_Documents();
            //Assert.True(VerifyPageTitle("Design Document"));
            //DesignDocCommentReview.CreateDocument();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2. Log in as DOT User, enter no Comments----------------------");
            EnterNoComment(UserType.DOTUser);

            //LoginAs(UserType.DOTUser);
            //NavigateToPage.RMCenter_Design_Documents();
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //DesignDocCommentReview.EnterNoComment();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Log in as IQF User, enter no Comments----------------------");
            EnterNoComment(UserType.IQFUser);

            //LoginAs(UserType.IQFUser);
            //NavigateToPage.RMCenter_Design_Documents();
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //DesignDocCommentReview.EnterNoComment();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------4. Log in as DOT Admin, forwards Comments----------------------");
            ForwardComment(UserType.DOTAdmin);

            //LoginAs(UserType.DOTAdmin);
            //NavigateToPage.RMCenter_Design_Documents();
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            //DesignDocCommentReview.ForwardComment();
            //Thread.Sleep(2000);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------5. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment(UserType.IQFAdmin);

            //LoginAs(UserType.IQFAdmin);
            //NavigateToPage.RMCenter_Design_Documents();
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            //DesignDocCommentReview.ForwardComment();
            //Thread.Sleep(2000);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------6. Log in as DEV User, enters Response and Agree Response Code----------------------");
            EnterResponseAndAgreeResponseCode(UserType.DEVUser);

            //LoginAs(UserType.DEVUser);
            //NavigateToPage.RMCenter_Design_Documents();
            //TableHelper.ClickTab(DesignDocument.TableTab.Requires_Response);
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //DesignDocCommentReview.EnterResponseCommentAndAgreeResponseCode();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------7. Log in as DEV Admin, forwards Response Comments----------------------");
            ForwardResponseComment(UserType.DEVAdmin);

            //LoginAs(UserType.DEVAdmin);
            //NavigateToPage.RMCenter_Design_Documents();
            //DesignDocCommentReview.ClickTab_Requires_Response();
            ////TableHelper.ClickTab(DesignDocument.TableTab.Requires_Response);
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //DesignDocCommentReview.ClickBtnJs_SaveForward();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------8. Log in as IQF Admin, Enters,forwards closing comment----------------------");

            WaitForPageReady();
            EnterAndForwardClosingComment(UserType.IQFAdmin);

            //LoginAs(UserType.IQFAdmin);
            //NavigateToPage.RMCenter_Design_Documents();
            //DesignDocCommentReview.ClickTab_Pending_Closing();
            ////TableHelper.ClickTab(DesignDocument.TableTab.Pending_Closing);
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //DesignDocCommentReview.EnterClosingCommentAndCode();

            LogInfo("--------------------------9. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
            Thread.Sleep(5000);
        }
    }

    [TestFixture]//SH249
    public class Test_CommentReviewRegularComment_ForSH249 : DesignDocumentWF
    {
        //        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2238577)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void CommentReviewForSH249()
        {
            LogInfo("--------------------------1. Log in as IQF RecordsManager'----------------------");
            CreateDesignDocCommentReviewDocument(UserType.IQFRecordsMgr);

            //LoginAs(UserType.IQFRecordsMgr);//testing glx comment review
            //NavigateToPage.RMCenter_Design_Documents();
            //Assert.True(VerifyPageTitle("Design Document"));
            //DesignDocCommentReview.CreateDocument();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2. Log in as IQF User, enters Comments----------------------");
            EnterRegularComment(UserType.IQFUser);

            //LoginAs(UserType.IQFUser);
            //NavigateToPage.RMCenter_Design_Documents();
            //TableHelper.ClickTab(DesignDocument.TableTab.Requires_Comment);
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment(UserType.IQFAdmin);

            //LoginAs(UserType.IQFAdmin);
            //NavigateToPage.RMCenter_Design_Documents();
            //TableHelper.ClickTab(DesignDocument.TableTab.Requires_Comment);
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            //DesignDocCommentReview.ClickBtn_SaveForward();
            //TableHelper.ClickEnterBtnForRow();

            LogInfo("-------------------------4. Log in as IQF Admin, Enters,forwards Response and Resolution stampcode----------------------");
            EnterResponseCommentAndDisagreeResponseCode();

            //DesignDocCommentReview.EnterResponseCommentAndDisagreeResponseCode(); //<-- moved to DesignDocumentWF
            TableHelper.ClickEnterBtnForRow();
            LogInfo("--------------------------5. Log in as IQF Admin, Enters,forwards closing comment----------------------");

            WaitForPageReady();
            DesignDocCommentReview.EnterClosingCommentAndCode();

            LogInfo("--------------------------6. IQF Admin verifies if record in closed tab ----------------------");

            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
            Thread.Sleep(5000);
        }
    }

    [TestFixture]//SH249
    public class Test_CommentReviewNoComment_ForSH249 : DesignDocumentWF
    {
        //        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, "333")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void CommentReviewNoCommentForSH249()
        {
            LogInfo("--------------------------1. Log in as IQF RecordsManager'----------------------");
            CreateDesignDocCommentReviewDocument(UserType.IQFRecordsMgr);

            //LoginAs(UserType.IQFRecordsMgr);//testing glx comment review
            //NavigateToPage.RMCenter_Design_Documents();
            //Assert.True(VerifyPageTitle("Design Document"));
            //DesignDocCommentReview.CreateDocument();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2. Log in as IQF User, enter no Comments----------------------");
            EnterNoComment(UserType.IQFUser);

            //LoginAs(UserType.IQFUser);
            //NavigateToPage.RMCenter_Design_Documents();
            //TableHelper.ClickTab(DesignDocument.TableTab.Requires_Comment);
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //DesignDocCommentReview.EnterNoComment();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Log in as IQF Admin, forwards Comments----------------------");
            ForwardComment(UserType.IQFAdmin);

            //LoginAs(UserType.IQFAdmin);
            //NavigateToPage.RMCenter_Design_Documents();
            //TableHelper.ClickTab(DesignDocument.TableTab.Requires_Comment);
            //DesignDocCommentReview.FilterTableByValue();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            //DesignDocCommentReview.ClickBtn_SaveForward();
            //TableHelper.ClickEnterBtnForRow();

            LogInfo("-------------------------4. Log in as IQF Admin, Enters,forwards Response and Resolution stampcode----------------------");
            EnterResponseCommentAndDisagreeResponseCode();

            //DesignDocCommentReview.EnterResponseCommentAndDisagreeResponseCode(); //<-- moved to DesignDocumentWF
            TableHelper.ClickEnterBtnForRow();

            LogInfo("--------------------------5. Log in as IQF Admin, Enters,forwards closing comment----------------------");

            WaitForPageReady();
            DesignDocCommentReview.EnterClosingCommentAndCode();

            LogInfo("--------------------------6. IQF Admin verifies if record in closed tab ----------------------");

            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
            Thread.Sleep(5000);
        }
    }
    */
}