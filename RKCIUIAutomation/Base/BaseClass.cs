using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System;

namespace RKCIUIAutomation.Base
{
    [TestFixture]
    public class BaseClass : BaseUtils
    {
        private TestPlatform testPlatform;
        private BrowserType browserType;
        private TestEnv testEnv;
        private Project projectSite;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ExtentTestManager.CreateParentTest(GetType().Name);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExtentManager.Instance.Flush();
        }

        [SetUp]
        public void BeforeTest()
        {
            var _testPlatform = TestContext.Parameters.Get("Platform", $"{TestPlatform.Linux}");
            var _browserType = TestContext.Parameters.Get("Browser", $"{BrowserType.Chrome}");
            var _testEnv = TestContext.Parameters.Get("TestEnv", $"{TestEnv.Test}");
            var _projectSite = TestContext.Parameters.Get("Project");

            testPlatform = (TestPlatform)Enum.Parse(typeof(TestPlatform), _testPlatform);
            browserType = (BrowserType)Enum.Parse(typeof(BrowserType), _browserType);
            testEnv = (TestEnv)Enum.Parse(typeof(TestEnv), _testEnv);
            projectSite = (Project)Enum.Parse(typeof(Project), _projectSite);

            string siteUrl = GetSiteUrl(testEnv, projectSite);

            if (testPlatform == TestPlatform.Local)
            {
                Driver = GetLocalWebDriver(browserType);
                Driver.Navigate().GoToUrl(siteUrl);
            }
            else
            {
                Driver = GetRemoteWebDriver(testPlatform, browserType);
                Driver.Navigate().GoToUrl(siteUrl);
            }

            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
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
            Cookie cookie = null;

            switch (status)
            {
                case TestStatus.Failed:
                    cookie = new Cookie("zaleniumTestPassed", "false");
                    screenshotPath = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    cookie = new Cookie("zaleniumTestPassed", "true");
                    logstatus = Status.Pass;
                    break;
            }
                        
            ExtentTestManager.GetTest().Log(logstatus, "Test ended with " + logstatus + stacktrace).AddScreenCaptureFromPath(screenshotPath);

            Driver.Manage().Cookies.AddCookie(cookie);
            Driver.Quit();
        }

    }
}
