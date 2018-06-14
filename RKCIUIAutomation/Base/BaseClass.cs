using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System;
using static RKCIUIAutomation.Config.ConfigUtil;
using static RKCIUIAutomation.Config.ProjectProperties;


namespace RKCIUIAutomation.Base
{
    [TestFixture]
    public class BaseClass : BaseUtils
    {
        public static TestPlatform testPlatform;
        public static BrowserType browserType;
        public static TestEnv testEnv;
        public static ProjectName projectName;

        private Cookie cookie = null;
        private static string tenantName;
        private TestStatus testStatus;

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
            var _projectName = TestContext.Parameters.Get("Project");

            testPlatform = (TestPlatform)Enum.Parse(typeof(TestPlatform), _testPlatform);
            browserType = (BrowserType)Enum.Parse(typeof(BrowserType), _browserType);
            testEnv = (TestEnv)Enum.Parse(typeof(TestEnv), _testEnv);
            projectName = (ProjectName)Enum.Parse(typeof(ProjectName), _projectName);
            tenantName = projectName.ToString();
            DetermineFilePath(testPlatform.ToString());
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            log.Info($"ExtentReports HTML Test Report page created at {ExtentManager.reportFilePath}");

            ExtentManager.Instance.AddSystemInfo("Project", tenantName);
            ExtentManager.Instance.AddSystemInfo("Test Environment", testEnv.ToString());
            ExtentManager.Instance.AddSystemInfo("Browser", browserType.ToString());
            ExtentManager.Instance.Flush();

            Driver.Quit();
        }

        [SetUp]
        public void BeforeTest()
        {
            TestContext.TestAdapter testInstance = TestContext.CurrentContext.Test;
            try
            {
                testComponent = testInstance.Properties.Get("Category").ToString();
                testDescription = testInstance.Properties.Get("Description").ToString();
                testPriority = testInstance.Properties.Get("Priority").ToString();
                testCaseNumber = testInstance.Properties.Get("TC#").ToString();
                testSuite = GetTestContext(testInstance.FullName)[2];

                ExtentTestManager.CreateParentTest(testInstance.Name);
                ExtentTestManager
                    .CreateTest($"<font size=3>TestCase# : {testCaseNumber} - {testInstance.Name}</font><br><font size=2>{testDescription}</font>")
                    .AssignCategory(testPriority)
                    .AssignCategory(testComponent)
                    .AssignCategory(testSuite);

                if (GetComponentsForProject(projectName).Contains(testComponent))
                {
                    switch (testPlatform)
                    {
                        case TestPlatform.Local:
                            Driver = GetLocalWebDriver(browserType);
                            break;
                        //TODO - case for appium (mobile) ?
                        default:
                            Driver = GetRemoteWebDriver(testPlatform, browserType);
                            break;
                    }

                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                    Driver.Manage().Window.Maximize();
                    Driver.Navigate().GoToUrl(GetSiteUrl(testEnv, projectName));
                }
                else
                {
                    testStatus = TestStatus.Skipped;
                    string msg = $"TEST SKIPPED : Project ({projectName}) does not have implementation of the component ({testComponent}).";
                    LogSkipped(msg);
                }
            }
            catch (Exception e)
            {
                LogInfo("Exception occured during BeforeTest method", e);
            }
        }

        [TearDown]
        public void AfterTest()
        {
            Status logstatus;
            string screenshotPath = null;
            var stacktrace = string.Empty;

            try
            {
                testStatus = TestContext.CurrentContext.Result.Outcome.Status;

                if (testStatus != TestStatus.Passed || testStatus != TestStatus.Skipped)
                {    
                    stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                        ? ""
                        : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace); 
                }
            }
            catch (Exception e)
            {
                LogInfo($"Exception occured during AfterTest method", e);
            }
            finally
            {
                switch (testStatus)
                {
                    case TestStatus.Failed:
                        screenshotPath = CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                        logstatus = Status.Fail;
                        cookie = new Cookie("zaleniumTestPassed", "false");
                        break;
                    case TestStatus.Passed:
                        logstatus = Status.Pass;
                        cookie = new Cookie("zaleniumTestPassed", "true");
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        break;
                    default:
                        logstatus = Status.Warning;
                        break;
                }

                try
                {
                    if (cookie != null)
                    {
                        Driver.Manage().Cookies.AddCookie(cookie); 
                    }
                }
                finally
                {
                    ExtentTestManager.GetTest().Log(logstatus, $"Test ended with {logstatus} {stacktrace}").AddScreenCaptureFromPath(screenshotPath);
                    Driver.Close();
                }
            }
        }
    }
}
