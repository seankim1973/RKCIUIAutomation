using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using RKCIUIAutomation.Tools;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.TableHelper;

namespace RKCIUIAutomation.UnitTest
{
    [TestFixture]
    public class Verify_UnitTest_LinkCoverage_Level1 : TestBase
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2188206)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Site Pages Load Successfully")]
        public void UnitTest_LinkCoverage_Level1()
        {
            LoginAs(UserType.Bhoomi);

            string url = "http://stage.laxapm.elvispmc.com/Project/Random";

            AddAssertionToList(VerifyUrlIsLoaded(url));

            AssertAll();
        }
    }

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
            Report.Step($"Other component test - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Qms_Document();
            Assert.True(VerifyPageHeader("QMS Documents"));
        }

        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Get SubMenu Names")]
        public void GetSubMenuNames()
        {
            LoginAs(UserType.DIRMgrQA);
            NavigateToPage.QARecordControl_QA_DIRs();
            VerifyPageHeader("Test");
            NavigateToPage.QASearch_Daily_Inspection_Report();
            VerifyPageHeader("Test");

            //try
            //{
            //    JsHover(By.XPath("//a[text()='Quality Search']"));
            //    IList<IWebElement> elements = Driver.FindElements(By.XPath("//a[text()='Quality Search']/following-sibling::ul/li/a"));

            //    foreach (IWebElement elem in elements)
            //    {
            //        string subMenu = elem.Text;
            //        Console.WriteLine(subMenu);
            //    }
            //}
            //catch (Exception e)
            //{
            //    log.Error(e.Message);
            //    throw;
            //}

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
            AddAssertionToList(VerifyPageHeader("New Submittal"));
            ClickCancel();
            NavigateToPage.RMCenter_Upload_DEV_Submittal();
            AddAssertionToList(VerifyPageHeader("Submittal Details"));
            AssertAll();
        }
    }

    [TestFixture]
    public class Verify_SampleTest : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "Priority 1")]
        [Description("Verify user can login successfully using project - user account")]
        public void SampleTest()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QARecordControl_General_CDR();
            //NavigateToPage.RMCenter_Search();
            //RMCenter_SearchPage.PopulateAllSearchCriteriaFields();
            //Assert.True(VerifyPageTitle("RM Center Search"));
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
            LoginAs(UserType.Bhoomi);
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
            LoginAs(UserType.Bhoomi);
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
            LoginAs(UserType.Bhoomi);
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
            Report.Step($"This test should be skipped due to Component1");
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
            Report.Step($"This test should be skipped due to Component1");
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
            Report.Step($"Two components test - This test should pass");
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
            Report.Step($"This test should be skipped due to Component2");
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
            Report.Step($"Other component test - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.My_Details();
            Assert.True(VerifyPageHeader("Account Details"));
            NavigateToPage.UserMgmt_Roles();
            Assert.True(VerifyPageHeader("Roles"));
            NavigateToPage.SysConfig_Gradations();
            Assert.True(VerifyPageHeader("Gradations"));
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
            Report.Step($"Other component test - This test should run");
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
            Report.Step($"Other component test - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Qms_Document();
            Assert.True(VerifyPageHeader("QMS Documents"));
        }
    }

    [TestFixture]
    public class Test_GridHelper : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Component Name")]
        public void VerifyGridHelper()
        {
            LoginAs(UserType.IQFAdmin);
            NavigateToPage.RMCenter_Design_Documents();
            GridHelper.ClickTab(DesignDocument.TableTab.Pending_Response);
            GridHelper.SortColumnAscending(DesignDocument.ColumnName.Action);
            GridHelper.SortColumnDescending(DesignDocument.ColumnName.Action);
            GridHelper.SortColumnToDefault(DesignDocument.ColumnName.Action);
            GridHelper.FilterColumn(DesignDocument.ColumnName.Number, "AJhZSDRCBLSKvmwANBPbkiVSWn", TableType.MultiTab);
            GridHelper.ClearTableFilters();
            GridHelper.FilterColumn(DesignDocument.ColumnName.Title, "GarnetCommentReviewRegularComment_DsgnDocTtl", TableType.MultiTab, FilterOperator.EqualTo, FilterLogic.Or, "ATM-PLC-T-00011_UTL-ATM_HDPE-RPLMNT");
            GridHelper.ClickEnterBtnForRow();
            //GridHelper.ClickCommentTabNumber(2);
            //DesignDocCommentReview.ClickBtn_BackToList();

            //GridHelper.ClickTab(DesignDocument.TableTab.Requires_Resolution);
            //GridHelper.ClickTab(DesignDocument.TableTab.Pending_Closing);
            //GridHelper.ClickTab(DesignDocument.TableTab.Closed);
            //GridHelper.ClickTab(DesignDocument.TableTab.Requires_Comment);
            //NavigateToPage.RMCenter_Search();
            //GridHelper.GoToPageNumber(5);
            //GridHelper.GoToLastPage();
            //GridHelper.GoToPreviousPage();
            //GridHelper.GoToFirstPage();
            //GridHelper.GoToNextPage();
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
            Assert.True(VerifyPageHeader("QMS Documents"));
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
            Report.Step($"Other component test - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.My_Details();
            AddAssertionToList(VerifyPageHeader("Account Details"));
            NavigateToPage.UserMgmt_Roles();
            AddAssertionToList(VerifyPageHeader("Roles"));
            NavigateToPage.SysConfig_Gradations();
            AddAssertionToList(VerifyPageHeader("Gradations"));
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
            Report.Step($"Test skip test - This test should run only for I15 Tenants");
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
            Assert.True(VerifyPageHeader("RM Center Search"));
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
            Assert.True(VerifyPageHeader("Failed Test"));

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
            Assert.True(VerifyPageHeader("Failed Test"));
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
            Assert.True(VerifyPageHeader("Failed Test"));
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
            Assert.True(VerifyPageHeader("Failed Test"));
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
            Assert.True(VerifyPageHeader("Failed Test"));
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

    [TestFixture]
    public class CDRTabNames : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Get CDR Tab Names")]
        public void Get_CDR_Tab_Names()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QARecordControl_General_CDR();

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

    [TestFixture]
    public class DIRTabNames : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Get DIR Tab Names")]
        public void Get_DIR_Tab_Names()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QARecordControl_QA_DIRs();

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

    
    [TestFixture]
    public class FilterDirNumberColumn : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Get DIR Tab Names")]
        public void Filter_Dir_Number_Column()
        {
            LoginAs(UserType.DIRMgrQC);
            NavigateToPage.QCRecordControl_QC_DIRs();
            string dirNumber = "RKLIZ1181017";

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(QADIRs.TableTab.Create_Revise, dirNumber));
        }
    }

    [TestFixture]
    public class FilterNcrNumberColumn : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Get DIR Tab Names")]
        public void Filter_Ncr_Number_Column()
        {
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
            string ncrDesc = "DlEamxKSEhxuBTbyRKGQtuoiDO";

            AddAssertionToList(WF_QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayedInReview(ncrDesc), "UNIT TEST VerifyNCRDocIsDisplayedInReview");
        }
    }

    public class Cipher
    {
        [Test]
        public void EncryptDecryptPW()
        {
            string pw = "";

            var encrypted = ConfigUtil.GetEncryptedPW(pw);
            Console.WriteLine($"ENCRYPTED : {encrypted}");
            var decrypted = ConfigUtil.GetDecryptedPW(encrypted);
            Console.WriteLine($"DECRYPTED : {decrypted}");
            Assert.True(decrypted.Equals(pw));
        }
    }

    public class QueryDB : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Get DIR Tab Names")]
        public void GetDirDataFromDB()
        {
            string dirNumber = "3333181211";
            string dirRev = "B";
            DirDbData data = new DirDbData();

            DirDbAccess db = new DirDbAccess();
            data = db.GetDirData(dirNumber, dirRev);
            Console.WriteLine($"BEFORE: {data.DirData}");

            db.SetDIR_DIRNO_IsDeleted(dirNumber);
            data = db.GetDirData(dirNumber, dirRev);
            Console.WriteLine($"AFTER: {data.DirData}");

            db.SetDIR_DIRNO_IsDeleted(dirNumber, dirRev, false);
            data = db.GetDirData(dirNumber, dirRev);
            Console.WriteLine($"Set IsDeleted to 1: {data.DirData}");
        }
    }
}