using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using OpenQA.Selenium;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Base;
using System.Diagnostics;
using System;

namespace RKCIUIAutomation.Test.Smoke
{
    [TestFixture]
    public class Verify_LinkCoverage_Level1 : TestBase
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2345")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Site Pages Load Successfully")]
        public void LinkCoverage_Level1()
        {
            LoginAs(UserType.Bhoomi);

            List<string> pageUrls = new List<string>();
            pageUrls = GetNavMenuUrlList();

            foreach (var url in pageUrls)
            {
                AddAssertionToList(VerifyUrlIsLoaded(url));
            }

            AssertAll();
        }
    }

    [TestFixture]
    public class Test_CommentReviewPartialFunctions : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void CommentReviewPartialFunctions()
        {
            LogInfo("--------------------------1.Login As IQF User----------------------");
            LoginAs(UserType.IQFUser);//testing glx comment review
            NavigateToPage.RMCenter_Design_Documents();
            Assert.True(VerifyPageTitle("Design Document"));
            DesignDocCommentReview.CreateDocument();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2.Login As DOT User----------------------");
            LoginAs(UserType.DOTUser);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Login As DOT Admin----------------------");
            LoginAs(UserType.DOTAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ForwardComment();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------4.Login As DEV User----------------------");
            LoginAs(UserType.DEVUser);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Response();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterResponseCommentAndDisagreeResponseCode();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------5. Login As DEV Admin----------------------");
            LoginAs(UserType.DEVAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.ClickTab_Requires_Response();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.ForwardResponseComment();
            DesignDocCommentReview.ClickBtn_BackToList();

            LogInfo("--------------------------6. DEV Admin enters Resolution='Disagree workflow'----------------------");
            //DesignDocCommentReview.ClickTab_Requires_Resolution();
            //DesignDocCommentReview.SortTable_Descending();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            DesignDocCommentReview.Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse(); //<<-- converted steps into a single Workflow method in the interface class - TODO: remove commented steps after confirming implementation is logical
            //Thread.Sleep(2000);
            //DesignDocCommentReview.ClickBtn_BackToList();
            //WaitForPageReady();

            LogInfo("--------------------------7. DEV Admin forwards Resolution='Disagree workflow'----------------------");
            DesignDocCommentReview.Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse(); //<<-- converted steps into a single Workflow method in the interface class - TODO: remove commented steps after confirming implementation is logical

            //DesignDocCommentReview.ClickTab_Requires_Resolution();
            //DesignDocCommentReview.SortTable_Descending();
            //TableHelper.ClickEnterBtnForRow();
            //WaitForPageReady();
            //DesignDocCommentReview.ForwardComment();

            LogInfo("--------------------------8. DEV Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
            Thread.Sleep(5000);
        }
    }

    public class Test_CommentReviewNoCommentGarnetGLX : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void CommentReviewNoCommentGarnetGLX()
        {
            LogInfo("--------------------------1.Login As IQF User----------------------");
            LoginAs(UserType.IQFUser);//testing glx comment review
            NavigateToPage.RMCenter_Design_Documents();
            Assert.True(VerifyPageTitle("Design Document"));
            DesignDocCommentReview.CreateDocument();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2.Login As DOT User----------------------");
            LoginAs(UserType.DOTUser);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterNoComment();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Login As DOT Admin----------------------");
            LoginAs(UserType.DOTAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ForwardComment();
         
            LogInfo("--------------------------4. DEV Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
            Thread.Sleep(5000);
        }
    }

    [TestFixture]
    public class Test_CommentReviewForSG : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void CommentReviewForSG()
        {
          
            LogInfo("--------------------------1. Log in as IQFRM'----------------------");
            LoginAs(UserType.IQFRecordsMgr);
            NavigateToPage.RMCenter_Design_Documents();
            Assert.True(VerifyPageTitle("Design Document"));
            DesignDocCommentReview.CreateDocument();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2. Log in as DOT User, enters Comments----------------------");
            LoginAs(UserType.DOTUser);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Log in as IQF User, enters Comments----------------------");
            LoginAs(UserType.IQFUser);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------4. Log in as DOT Admin, forwards Comments----------------------");
            LoginAs(UserType.DOTAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ForwardComment();
            Thread.Sleep(2000);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------5. Log in as IQF Admin, forwards Comments----------------------");
            LoginAs(UserType.IQFAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ForwardComment();
            Thread.Sleep(2000);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------6. Log in as DEV User, enters Response----------------------");
            LoginAs(UserType.DEVUser);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Response);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterResponseCommentAndDisagreeResponseCode();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------7. Log in as DEV Admin, forwards Comments----------------------");
            LoginAs(UserType.DEVAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Response);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.ForwardResponseComment();
            ClickLogoutLink();
            ClickLoginLink();
      

            LogInfo("--------------------------8. Log in as IQF Admin,add resolution----------------------");
            LoginAs(UserType.IQFAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Resolution);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse();
            Thread.Sleep(2000);
            DesignDocCommentReview.ClickBtn_BackToList();
            WaitForPageReady();

            LogInfo("--------------------------9. Log in as IQF Admin, forwards Resolution----------------------");
            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Resolution);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ForwardComment();

            LogInfo("--------------------------10. Log in as IQF Admin, Enters,forwards closing comment----------------------");

            WaitForPageReady();
            TableHelper.ClickTab(DesignDocument.TableTab.Pending_Closing);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterClosingCommentAndCode();

            LogInfo("--------------------------11. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
            Thread.Sleep(5000);
         
           

        }
    }

    [TestFixture]
    public class Test_NoCommentWfCommentReviewForSG : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void NoCommentWfCommentReviewForSG()
        {
            LogInfo("--------------------------No comment Workflow begins----------------------");
            LogInfo("--------------------------1. Log in as IQFRM'----------------------");
            LoginAs(UserType.IQFRecordsMgr);
            NavigateToPage.RMCenter_Design_Documents();
            Assert.True(VerifyPageTitle("Design Document"));
            DesignDocCommentReview.CreateDocument();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2. Log in as DOT User, enters Comments----------------------");
            LoginAs(UserType.DOTUser);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterNoComment();

            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Log in as IQF User, enters Comments----------------------");
            LoginAs(UserType.IQFUser);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterNoComment();

            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------4. Log in as DOT Admin, forwards Comments----------------------");
            LoginAs(UserType.DOTAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ForwardComment();
            Thread.Sleep(2000);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------5. Log in as IQF Admin, forwards Comments----------------------");
            LoginAs(UserType.IQFAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ForwardComment();
            Thread.Sleep(2000);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------6. Log in as DEV User, enters Response----------------------");
            LoginAs(UserType.DEVUser);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Response);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterResponseCommentAndAgreeResponseCode();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------7. Log in as DEV Admin, forwards Comments----------------------");
            LoginAs(UserType.DEVAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Response);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.ForwardResponseComment();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------8. Log in as IQF Admin, Enters,forwards closing comment----------------------");

            WaitForPageReady();
            LoginAs(UserType.IQFAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.ClickTab(DesignDocument.TableTab.Pending_Closing);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterClosingCommentAndCode();

            LogInfo("--------------------------9. IQF Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
            Thread.Sleep(5000);
        }
    }
    [TestFixture]
    public class Test_CommentReviewForSH249 : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void CommentReviewForSH249()
        {

            LogInfo("--------------------------1. Log in as IQF RecordsManager'----------------------");
            LoginAs(UserType.IQFRecordsMgr);//testing glx comment review
            NavigateToPage.RMCenter_Design_Documents();
            Assert.True(VerifyPageTitle("Design Document"));
            DesignDocCommentReview.CreateDocument();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2. Log in as IQF User, enters Comments----------------------");
            LoginAs(UserType.IQFUser);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Comment);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Log in as IQF Admin, forwards Comments----------------------");
            LoginAs(UserType.IQFAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Comment);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ForwardComment();
            TableHelper.ClickEnterBtnForRow();
            LogInfo("-------------------------4. Log in as IQF Admin, Enters,forwards Response and Resolution stampcode----------------------");
            DesignDocCommentReview.EnterResponseCommentAndDisagreeResponseCode();
            TableHelper.ClickEnterBtnForRow();
            LogInfo("--------------------------5. Log in as IQF Admin, Enters,forwards closing comment----------------------");

            WaitForPageReady();
            DesignDocCommentReview.EnterClosingCommentAndCode();

            LogInfo("--------------------------6. IQF Admin verifies if record in closed tab ----------------------");

            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
            Thread.Sleep(5000);
        }
    }

        [TestFixture]
        public class Test_CommentReviewNoCommentForSH249 : TestBase
        {
            [Test]
            [Category(Component.Other)]
            [Property(TestCaseNumber, "ELVS2222")]
            [Property(Priority, "Priority 1")]
            [Description("Verify Component Name")]
            public void CommentReviewNoCommentForSH249()
            {

                LogInfo("--------------------------1. Log in as IQF RecordsManager'----------------------");
                LoginAs(UserType.IQFRecordsMgr);//testing glx comment review
                NavigateToPage.RMCenter_Design_Documents();
                Assert.True(VerifyPageTitle("Design Document"));
                DesignDocCommentReview.CreateDocument();
                ClickLogoutLink();
                ClickLoginLink();

                LogInfo("--------------------------2. Log in as IQF User, enters Comments----------------------");
                LoginAs(UserType.IQFUser);
                NavigateToPage.RMCenter_Design_Documents();
                TableHelper.ClickTab(DesignDocument.TableTab.Requires_Comment);
                DesignDocCommentReview.FilterTableByValue();
                TableHelper.ClickEnterBtnForRow();
                DesignDocCommentReview.EnterNoComment();
                ClickLogoutLink();
                ClickLoginLink();

                LogInfo("--------------------------3. Log in as IQF Admin, forwards Comments----------------------");
                LoginAs(UserType.IQFAdmin);
                NavigateToPage.RMCenter_Design_Documents();
                TableHelper.ClickTab(DesignDocument.TableTab.Requires_Comment);
                DesignDocCommentReview.FilterTableByValue();
                TableHelper.ClickEnterBtnForRow();
                WaitForPageReady();
                DesignDocCommentReview.ForwardComment();
                TableHelper.ClickEnterBtnForRow();
                LogInfo("-------------------------4. Log in as IQF Admin, Enters,forwards Response and Resolution stampcode----------------------");
                DesignDocCommentReview.EnterResponseCommentAndDisagreeResponseCode();
                TableHelper.ClickEnterBtnForRow();
                LogInfo("--------------------------5. Log in as IQF Admin, Enters,forwards closing comment----------------------");

                WaitForPageReady();
                DesignDocCommentReview.EnterClosingCommentAndCode();

                LogInfo("--------------------------6. IQF Admin verifies if record in closed tab ----------------------");

                Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
                Thread.Sleep(5000);
            }
        }
}
