using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;

namespace RKCIUIAutomation.Test.ParallelTests
{
    [TestFixture]
    [Parallelizable]
    public class TestDynamicNavigationClass : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for QMS Documents page")]
        public void TestDynamicNavigation()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Qms_Document();
            Assert.True(VerifyPageTitle("QMS Documents"));
        }
    }

    [TestFixture]
    [Parallelizable]
    public class VerifyComponentTestRunsClass : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
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
    [Parallelizable]
    public class VerifySkipTestBasedOnComponent : TestBase
    {
        [Test]
        [Category(Component.Other), Property("Component2",Component.OV_Test)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
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
    [Parallelizable]
    public class LatestTestClass : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
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
    [Parallelizable]
    public class FailingTestClass : TestBase
    {
        [Test]
        [Category(Component.Search)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Test Fails as expected")]
        public void FailingTest()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Search();
            //RMCenter_SearchPage.PopulateAllSearchCriteriaFields();
            By locator = By.XPath("//h3");
            string title = GetText(locator);
            Assert.True(title == "Failed Test");
            //Thread.Sleep(15000);
        }
    }

    [TestFixture]
    [Parallelizable]
    public class ScreenshotTestClass1 : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Test Fails with valid screenshot")]
        public void ProjDetails_ScreenshotTest()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Admin_Project_Details();
            Assert.True(VerifyPageTitle("Failed Test"));
        }
    }

    [TestFixture]
    [Parallelizable]
    public class ScreenshotTestClass2 : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Test Fails with valid screenshot")]
        public void MenuEditor_ScreenshotTest()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Admin_Menu_Editor();
            Assert.True(VerifyPageTitle("Failed Test"));
        }
    }

    [TestFixture]
    [Parallelizable]
    public class ScreenshotTestClass3 : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Test Fails with valid screenshot")]
        public void Contracts_ScreenshotTest()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Admin_Contracts();
            Assert.True(VerifyPageTitle("Failed Test"));
        }
    }

    [TestFixture]
    [Parallelizable]
    public class ScreenshotTestClass4 : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Test Fails with valid screenshot")]
        public void Companies_ScreenshotTest()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Admin_Companies();
            Assert.True(VerifyPageTitle("Failed Test"));
        }
    }
}
