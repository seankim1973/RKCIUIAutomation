using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace RKCIUIAutomation.UnitTest
{
    [TestFixture]
    public class BaseTest : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
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
    public class Verify_CancelOutOfOwnerSubmittalPage : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property(TestCaseNumber, "ELVS1234")]
        [Property(Priority, "Priority 1")]
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
        [Property(TestCaseNumber, "ELVS3456")]
        [Property(Priority, "Priority 1")]
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
        [Property(TestCaseNumber, "ELVS3456")]
        [Property(Priority, "Priority 1")]
        [Description("Verify user can login successfully using project - user account")]
        public void Generic()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            EnterText(SubmittalDetails.Input_Name, "Test Name");
            EnterText(SubmittalDetails.Input_SubmittalTitle, "Test Title");
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Action, 1);
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Segment_Area, 1);
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Location, 1);
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
        [Property(TestCaseNumber, "ELVS8988")]
        [Property(Priority, "Priority 1")]
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
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
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
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
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
        }
    }

    [TestFixture]
    public class Test_Runs_One_Valid_Component : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify test runs/passes with only component1 (valid) property attribute is specified")]
        public void VerifyOnlyComponent1Specified()
        {
            LogInfo($"This test should be skipped due to Component1");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
        }
    }

    [TestFixture]
    public class Test_Skipped_One_Invalid_Component : TestBase
    {
        [Test]
        [Category(Component.Control_Point)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify test is skipped due to component1 not part of tenant's component list")]
        public void VerifyComponent1TestIsSkipped()
        {
            LogInfo($"This test should be skipped due to Component1");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
        }
    }

    [TestFixture]
    public class Test_Runs_Two_Valid_Components : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(Component2, Component.Submittals)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify test runs/passes with component1 and 2 properties are part of tenant's component list")]
        public void VerifyComponent2Test()
        {
            LogInfo($"Two components test - This test should pass");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
        }
    }

    [TestFixture]
    public class Test_Skipped_Invalid_Component2 : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(Component2, Component.Control_Point)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify test is skipped due to only component2 not part of tenant's component list")]
        public void VerifyComponent2TestIsSkipped()
        {
            LogInfo($"This test should be skipped due to Component2");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
        }
    }

    [TestFixture]
    public class Test_ComponentTestRuns : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
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
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
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
    public class Execute_JavaProgram : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void ExecuteJava(string grideNode)
        {
            var processInfo = new ProcessStartInfo("docker.exe", $"exec -ti {grideNode} sh -c \"java -jar /home/seluser/UploadFiles/sikulirestapi-1.0.jar\"")
            {
                CreateNoWindow = false,
                UseShellExecute = false
            };
            Process proc;

            if ((proc = Process.Start(processInfo)) == null)
            {
                throw new InvalidOperationException("??");
            }

            proc.WaitForExit();
            int exitCode = proc.ExitCode;
            proc.Close();
        }
    }

    [TestFixture]
    public class Test_DynamicNavigation : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
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
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void VerifyTableHelper()
        {
            LoginAs(UserType.IQFAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            //TableHelper.ClickTab(DesignDocument.TableTab.Pending_Response);
            //TableHelper.SortColumnAscending(DesignDocument.ColumnName.Action);
            //TableHelper.SortColumnDescending(DesignDocument.ColumnName.Action);
            //TableHelper.SortColumnToDefault(DesignDocument.ColumnName.Action);
            TableHelper.FilterColumn(DesignDocument.ColumnName.Number, "lJKOSBwPrNQupkvjPnJFEeVhEP");
            TableHelper.ClearFilters();
            //TableHelper.FilterColumn(DesignDocument.ColumnName.Number, "NDC-DQP-3.08_00018", FilterOperator.EqualTo, FilterLogic.Or, "ATM-PLC-T-00011_UTL-ATM_HDPE-RPLMNT");
            //TableHelper.ClickEnterBtnForRow();
            //TableHelper.ClickCommentTabNumber(2);
            //DesignDocCommentReview.ClickBtn_BackToList();

            //TableHelper.ClickTab(DesignDocument.TableTab.Requires_Resolution);
            //TableHelper.ClickTab(DesignDocument.TableTab.Pending_Closing);
            //TableHelper.ClickTab(DesignDocument.TableTab.Closed);
            //TableHelper.ClickTab(DesignDocument.TableTab.Requires_Comment);
            //NavigateToPage.RMCenter_Search();
            //TableHelper.GoToPageNumber(5);
            //TableHelper.GoToLastPage();
            //TableHelper.GoToPreviousPage();
            //TableHelper.GoToFirstPage();
            //TableHelper.GoToNextPage();
        }
    }

    [TestFixture]
    public class TestDynamicNavigationClass : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for QMS Documents page")]
        public void TestDynamicNavigation()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Qms_Document();
            Assert.True(VerifyPageTitle("QMS Documents"));
        }
    }

    [TestFixture]
    public class VerifyComponentTestRunsClass : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Navigation for sub menu items under Project")]
        public void VerifyComponentTestRuns()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.My_Details();
            AddAssertionToList(VerifyPageTitle("Account Details"));
            NavigateToPage.UserMgmt_Roles();
            AddAssertionToList(VerifyPageTitle("Roles"));
            NavigateToPage.SysConfig_Gradations();
            AddAssertionToList(VerifyPageTitle("Gradations"));
            AssertAll();
        }
    }

    [TestFixture]
    public class VerifySkipTestBasedOnComponent : TestBase
    {
        [Test]
        [Category(Component.Other), Property(Component2, Component.OV_Test)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Test is Skipped for non-I15 Tenants")]
        public void SkipTestBasedOnComponent()
        {
            LogInfo($"Test skip test - This test should run only for I15 Tenants");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QALab_BreakSheet_Forecast();
            NavigateToPage.QALab_Cylinder_PickUp_List();
            NavigateToPage.QALab_Early_Break_Calendar();
        }
    }

    [TestFixture]
    public class LatestTestClass : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Test Runs without specifying Component2 annotation")]
        public void LatestTest()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Search();
            //RMCenter_SearchPage.PopulateAllSearchCriteriaFields();
            Assert.True(VerifyPageTitle("RM Center Search"));
        }
    }

    [TestFixture]
    public class FailingTestClass : TestBase
    {
        [Test]
        [Category(Component.Search)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Test Fails as expected")]
        public void FailingTest()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Search();
            //RMCenter_SearchPage.PopulateAllSearchCriteriaFields();
            Assert.True(VerifyPageTitle("Failed Test"));

            /*
            //Use for quicker test failure
            By locator = By.XPath("//h3");
            string title = GetText(locator);
            Assert.True(title == "Failed Test");
            */
        }
    }

    [TestFixture]
    public class ScreenshotTestClass1 : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Test Fails with valid screenshot")]
        public void ProjDetails_ScreenshotTest()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Admin_Project_Details();
            Assert.True(VerifyPageTitle("Failed Test"));
        }
    }

    [TestFixture]
    public class ScreenshotTestClass2 : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Test Fails with valid screenshot")]
        public void MenuEditor_ScreenshotTest()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Admin_Menu_Editor();
            Assert.True(VerifyPageTitle("Failed Test"));
        }
    }

    [TestFixture]
    public class ScreenshotTestClass3 : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Test Fails with valid screenshot")]
        public void Contracts_ScreenshotTest()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Admin_Contracts();
            Assert.True(VerifyPageTitle("Failed Test"));
        }
    }

    [TestFixture]
    public class ScreenshotTestClass4 : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Test Fails with valid screenshot")]
        public void Companies_ScreenshotTest()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Admin_Companies();
            Assert.True(VerifyPageTitle("Failed Test"));
        }
    }


    [TestFixture]
    public class NCRTabNames : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Get NCR Tab Names")]
        public void Get_NCR_Tab_Names()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QARecordControl_General_NCR();

            IList<IWebElement> elements = new List<IWebElement>();
            elements = Driver.FindElements(By.XPath("//ul[@class='k-reset k-tabstrip-items']/li/span[text()]"));
            Console.WriteLine($"TENANT: {tenantName}");
            for (int i = 0; i < elements.Count; i++)
            {
                string elem = elements[i].Text;
                Console.WriteLine(elem);
            }
        }
    }
}