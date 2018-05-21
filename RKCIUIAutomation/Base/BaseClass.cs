using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RKCIUIAutomation.Base
{
    [TestFixture]
    public class BaseClass : BaseUtils
    {
        public static TestPlatform testPlatform;
        public static BrowserType browserType;
        public static TestEnv testEnv;
        public static Project projectSite;
        private Cookie cookie;

        string testComponent;
        string testDescription;
        string testPriority;
        string testCaseNumber;
        string testSuite;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var _testPlatform = TestContext.Parameters.Get("Platform", $"{TestPlatform.Linux}");
            var _browserType = TestContext.Parameters.Get("Browser", $"{BrowserType.Chrome}");
            var _testEnv = TestContext.Parameters.Get("TestEnv", $"{TestEnv.Test}");
            var _projectSite = TestContext.Parameters.Get("Project");

            testPlatform = (TestPlatform)Enum.Parse(typeof(TestPlatform), _testPlatform);
            browserType = (BrowserType)Enum.Parse(typeof(BrowserType), _browserType);
            testEnv = (TestEnv)Enum.Parse(typeof(TestEnv), _testEnv);
            projectSite = (Project)Enum.Parse(typeof(Project), _projectSite);

            DetermineFilePath(testPlatform.ToString());
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Console.Out.WriteLine($"ExtentReports HTML Test Report page created at {ExtentManager.reportFilePath}");

            ExtentManager.Instance.AddSystemInfo("Project", projectSite.ToString());
            ExtentManager.Instance.AddSystemInfo("Test Environment", testEnv.ToString());
            ExtentManager.Instance.AddSystemInfo("Browser", browserType.ToString());
            ExtentManager.Instance.Flush();

            Driver.Quit();
        }

        [SetUp]
        public void BeforeTest()
        {
            string siteUrl = GetSiteUrl(testEnv, projectSite);

            if (testPlatform == TestPlatform.Local)
            {
                Driver = GetLocalWebDriver(browserType);
            }
            else
            {
                Driver = GetRemoteWebDriver(testPlatform, browserType);
            }

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Driver.Manage().Window.Size = new Size(1680, 1050);
            Driver.Navigate().GoToUrl(siteUrl);

            try
            {
                var testInstance = TestContext.CurrentContext.Test;
                ExtentTestManager.CreateParentTest(testInstance.Name);

                testComponent = testInstance.Properties.Get("Category").ToString();
                testDescription = testInstance.Properties.Get("Description").ToString();
                testPriority = testInstance.Properties.Get("Priority").ToString();
                testCaseNumber = testInstance.Properties.Get("TC#").ToString();
                testSuite = GetTestContext(testInstance.FullName)[2];

                ExtentTestManager
                    .CreateTest($"<font size=3>TestCase# : {testCaseNumber} - {testInstance.Name}</font><br><font size=2>{testDescription}</font>")
                    .AssignCategory(testPriority)
                    .AssignCategory(testComponent)
                    .AssignCategory(testSuite);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            string screenshotPath = null;            

            switch (status)
            {
                case TestStatus.Failed:
                    screenshotPath = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                    logstatus = Status.Fail;
                    cookie = new Cookie("zaleniumTestPassed", "false");
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    cookie = new Cookie("zaleniumTestPassed", "true");
                    break;
            }

            Driver.Manage().Cookies.AddCookie(cookie);
            ExtentTestManager.GetTest().Log(logstatus, "Test ended with " + logstatus + stacktrace).AddScreenCaptureFromPath(screenshotPath);

            Driver.Close();
        }

    }
}
