using NUnit.Framework;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static RKCIUIAutomation.Config.ProjectProperties;

namespace RKCIUIAutomation.Test
{
    [TestFixture]
    public class TestDynamicNavigationClass : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void TestDynamicNavigation()
        {
            LogInfo($"Other component test - This test should run");
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
    public class TestNewGWQALabMenuClass : TestBase
    {
        [Test]
        [Category(Component.Other), Property("Component2",Component.OV_Test)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void TestNewGWQALabMenu()
        {
            LogInfo($"Other component test - This test should run");
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
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void LatestTest()
        {
            LoginAs(UserType.ProjAdmin);
            NavigateToPage.RMCenter_Search();
            RMCenter_SearchPage.PopulateAllSearchCriteriaFields();
            Assert.True(VerifyPageTitle("RM Center Search"));
        }
    }

    [TestFixture]
    public class FailingTestClass : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Failing Test")]
        public void FailingTest()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.ProjAdmin);
            NavigateToPage.RMCenter_Search();
            RMCenter_SearchPage.PopulateAllSearchCriteriaFields();
            Assert.True(false);
            Thread.Sleep(15000);
        }
    }
}
