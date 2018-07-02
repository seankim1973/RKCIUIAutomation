using NUnit.Framework;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System.Threading;
using static RKCIUIAutomation.Config.ProjectProperties;

namespace RKCIUIAutomation
{
    //[TestFixture]
    [Category("TestFixture Category")]
    public class UnitTest1 : TestBase
    {
        //[Test]
        [Category("Test Category")]
        public void TestMethod1()
        {
            //driver.Navigate().GoToUrl("http://www.google.com");
            Thread.Sleep(5000);
            Assert.True(true);
        }



        //[Test(Description = "test description - inline")]
        public void Testmethod2()
        {

        }

        //[Test, Description("test description - comma seperated")]
        //public void testmethod3()
        //{
        //}

        //[Testcase(Testname = "test0004")]
        //public void testmethod4()
        //{
        //}
    }

    [TestFixture]
    public class BaseTest : TestBase
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
}
