using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using OpenQA.Selenium;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Base;

namespace RKCIUIAutomation.Test.Smoke
{
    [TestFixture]
    public class Verify_LinkCoverage_Level1 : TestBase
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2345")]
        [Property("Priority", "Priority 1")]
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
    public class Verify_CancelOutOfOwnerSubmittalPage : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property("TC#", "ELVS1234")]
        [Property("Priority", "Priority 1")]
        [Description("Verify user can cancel out of Owner Submittal Page Successfully")]
        public void VerifyCancelOutOfOwnerSubmittalPage()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Upload_Owner_Submittal();
            AddAssertionToList(VerifyPageTitle("New Submittal"));
            ClickCancel();
            NavigateToPage.RMCenter_Upload_DEV_Submittal();
            AddAssertionToList(VerifyPageTitle("Submittal Details"));
            AssertAll();
        }
    }

    [TestFixture]
    public class Verify_SampleTest : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property("TC#", "ELVS3456")]
        [Property("Priority", "Priority 1")]
        [Description("Verify user can login successfully using project - user account")]
        public void SampleTest()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Search();
            RMCenter_SearchPage.PopulateAllSearchCriteriaFields();
            Assert.True(VerifyPageTitle("RM Center Search"));
        }
    }

    [TestFixture]
    public class Test_Generic : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property("TC#", "ELVS3456")]
        [Property("Priority", "Priority 1")]
        [Description("Verify user can login successfully using project - user account")]
        public void Generic()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            EnterText(SubmittalDetails.Input_Name, "Test Name");
            EnterText(SubmittalDetails.Input_SubmittalTitle, "Test Title");
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Action, 1);
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Segment_Area, 1);
            //ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Location, 1);
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Feature, 1);
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Grade, 1);
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Supplier, 1);
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Specification, 1);
            EnterText(SubmittalDetails.Input_Quantity, "50");
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.QuantityUnit, 1);
            UploadFile("test.xlsx");
            ClickSubmitForward();

            Thread.Sleep(5000);
        }
    }

    [TestFixture]
    public class Test_RequiredFieldErrorsClickingSaveWithoutNameAndTitle : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property("TC#", "ELVS8988")]
        [Property("Priority", "Priority 1")]
        [Description("Verify proper required fields show error when clicking Save button without Submittal Name and Title")]
        public void VerifyRequiredFieldErrorsClickingSaveWithoutNameAndTitle()
        {
            LoginAs(UserType.ProjAdmin);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            ClickSave();
            AddAssertionToList(VerifyFieldErrorIsDisplayed(SubmittalDetails.Err_Name));
            AddAssertionToList(VerifyFieldErrorIsDisplayed(SubmittalDetails.Err_SubmittalTitle));

            /**uncomment Assert statement below to fail test*/
            //AddAssertionToList(VerifyFieldErrorIsDisplayed(SubmittalDetails.Err_DDListAction));

            AssertAll();
            Thread.Sleep(5000);
        }
    }

    [TestFixture]
    public class Test_RequiredFieldErrorsClickingSaveWithNameAndTitle : TestBase
    {
        [Test]
        [Category("")]
        [Property("TC#", "ELVS1111")]
        [Property("Priority", "Priority 1")]
        [Description("Verify proper required fields show error when clicking Save button with Submittal Name and Title")]
        public void VerifyRequiredFieldErrorsClickingSaveWithNameAndTitle()
        {
            LoginAs(UserType.ProjAdmin);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            EnterText(SubmittalDetails.Input_Name, "Test Name");
            EnterText(SubmittalDetails.Input_SubmittalTitle, "Test Title");
            ClickSave();
            Assert.True(VerifyFieldErrorIsDisplayed(SubmittalDetails.Err_DDListAction));

            Thread.Sleep(5000);
        }

    }

    [TestFixture]
    public class Test_SuccessMsgClickingSaveWithNameTitleAndActionDDL : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify success message is shown when clicking Save button with Submittal Name, Title and Action DDL")]
        public void VerifySuccessMsgClickingSaveWithNameTitleAndActionDDL()
        {
            LoginAs(UserType.ProjAdmin);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            EnterText(SubmittalDetails.Input_Name, "Test Name");
            EnterText(SubmittalDetails.Input_SubmittalTitle, "Test Title");
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Action, 1);
            ClickSave();
            Assert.True(VerifySuccessMessageIsDisplayed());

            Thread.Sleep(5000);
        }
    }

    [TestFixture]
    public class Test_ComponentTestIsSkipped : TestBase
    {
        [Test]
        [Category(Component.Control_Point)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void VerifyComponentTestIsSkipped()
        {
            LogInfo($"Control Point component test - This test should be skipped");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            Thread.Sleep(5000);
        }
    }

    [TestFixture]
    public class Test_ComponentTestRuns : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void VerifyComponentTestRuns()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.ProjAdmin);
            NavigateToPage.My_Details();
            Assert.True(VerifyPageTitle("Account Details"));
            NavigateToPage.UserMgmt_Roles();
            Assert.True(VerifyPageTitle("Roles"));
            NavigateToPage.SysConfig_Gradations();
            Assert.True(VerifyPageTitle("Gradations"));
        }
    }

    [TestFixture]
    public class Test_NewGWQALabMenu : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void NewGWQALabMenu()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QALab_BreakSheet_Forecast();
            NavigateToPage.QALab_Cylinder_PickUp_List();
            NavigateToPage.QALab_Early_Break_Calendar();
        }

    }

    [TestFixture]
    public class Test_DynamicNavigation : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void DynamicNavigation()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Qms_Document();           
            Assert.True(VerifyPageTitle("QMS Documents"));
        }
    }

    [TestFixture]
    public class Test_TableHelper : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void VerifyTableHelper()
        {
            LoginAs(UserType.IQFAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.ClickTab(DesignDocument.TableTab.Pending_Response);
            TableHelper.SortColumnAscending(DesignDocument.ColumnName.Action);
            TableHelper.SortColumnDescending(DesignDocument.ColumnName.Action);
            TableHelper.SortColumnToDefault(DesignDocument.ColumnName.Action);
            TableHelper.FilterColumn(DesignDocument.ColumnName.Number, "ATM-PLC-T-00011_UTL-ATM_HDPE-RPLMNT");
            TableHelper.ClearFilters();
            TableHelper.FilterColumn(DesignDocument.ColumnName.Number, "NDC-DQP-3.08_00018", FilterOperator.EqualTo, FilterLogic.Or, "ATM-PLC-T-00011_UTL-ATM_HDPE-RPLMNT");
            TableHelper.ClickEnterBtnForRow();
            TableHelper.ClickCommentTabNumber(2);
            DesignDocCommentReview.ClickBtn_BackToList();

            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Resolution);
            TableHelper.ClickTab(DesignDocument.TableTab.Pending_Closing);
            TableHelper.ClickTab(DesignDocument.TableTab.Closed);
            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Comment);
            NavigateToPage.RMCenter_Search();
            TableHelper.GoToPageNumber(5);
            TableHelper.GoToLastPage();
            TableHelper.GoToPreviousPage();
            TableHelper.GoToFirstPage();
            TableHelper.GoToNextPage();

            Thread.Sleep(10000);
        }
    }

    [TestFixture]
    public class Test_CommentReviewPartialFunctions : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
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
            DesignDocCommentReview.Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse();
            //Thread.Sleep(2000);
            //DesignDocCommentReview.ClickBtn_BackToList();
            //WaitForPageReady();

            LogInfo("--------------------------7. DEV Admin forwards Resolution='Disagree workflow'----------------------");
            DesignDocCommentReview.Workflow_ForwardResolutionCommentAndCodeForDisagreeResponse();

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

    [TestFixture]
    public class Test_CommentReviewForGLX : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void CommentReviewForGLX()
        {

            LogInfo("--------------------------1. Log in as IQF User'----------------------");
            LoginAs(UserType.IQFUser);//testing glx comment review
            NavigateToPage.RMCenter_Design_Documents();
            Assert.True(VerifyPageTitle("Design Document"));
            DesignDocCommentReview.CreateDocument();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------2. Log in as DOT User, enters Comments----------------------");
            LoginAs(UserType.DOTUser);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.SortColumnDescending(DesignDocument.ColumnName.Action);
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------3. Log in as DOT Admin, forwards Comments----------------------");
            LoginAs(UserType.DOTAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.SortColumnDescending(DesignDocument.ColumnName.Action);
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ForwardComment();
            Thread.Sleep(2000);
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------4. Log in as DEV User, enters Response----------------------");
            LoginAs(UserType.DEVUser);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Response);
            TableHelper.SortColumnDescending(DesignDocument.ColumnName.Action);
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterResponseCommentAndDisagreeResponseCode();
            ClickLogoutLink();
            ClickLoginLink();

            LogInfo("--------------------------5. Log in as DEV Admin, forwards Comments----------------------");
            LoginAs(UserType.DEVAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Response);
            TableHelper.SortColumnDescending(DesignDocument.ColumnName.Action);
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.ForwardResponseComment();
            DesignDocCommentReview.ClickBtn_BackToList();

            LogInfo("--------------------------6. Log in as DEV Admin, enters Resolution ='Disagree' workflow----------------------");
            TableHelper.ClickTab(DesignDocument.TableTab.Pending_Resolution);
            TableHelper.SortColumnDescending(DesignDocument.ColumnName.Action);
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.Workflow_EnterResolutionCommentAndResolutionCodeforDisagreeResponse();
            Thread.Sleep(2000);
            DesignDocCommentReview.ClickBtn_BackToList();
            WaitForPageReady();

            LogInfo("--------------------------2. Log in as DEV Admin, forwards Resolution----------------------");
            TableHelper.ClickTab(DesignDocument.TableTab.Pending_Resolution);
            TableHelper.SortColumnDescending(DesignDocument.ColumnName.Action);
            TableHelper.ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ForwardComment();

            LogInfo("--------------------------8. DEV Admin verifies if record in closed tab ----------------------");
            Assert.True(DesignDocCommentReview.VerifyItemStatusIsClosed());
            Thread.Sleep(5000);
        }
    }
}
