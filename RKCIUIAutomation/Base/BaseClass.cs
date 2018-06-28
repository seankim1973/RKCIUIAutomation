using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Config.ConfigUtil;
using static RKCIUIAutomation.Config.ProjectProperties;


namespace RKCIUIAutomation.Base
{
    [TestFixture]
    public class BaseClass : BaseUtils
    {
        public TestPlatform testPlatform;
        public BrowserType browserType;
        public TestEnv testEnv;
        public TenantName tenantName;

        private string siteUrl;       
        private TestStatus testStatus;
        private ResultState testResult;
        private Cookie cookie = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //TestExecutionContext testExecutionContext = new TestExecutionContext();
            //TestContext testContext = new TestContext(testExecutionContext);

            //var testName = testContext.Test.Name;

            testPlatform = configUtil.GetTestPlatform();
            browserType = configUtil.GetBrowserType();
            testEnv = configUtil.GetTestEnv();
            tenantName = configUtil.GetTenantName();

            DetermineFilePath(testPlatform.ToString());
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            log.Info($"ExtentReports HTML Test Report page created at {ExtentManager.reportFilePath}");

            ExtentManager.Instance.AddSystemInfo("Tenant", tenantName.ToString());
            ExtentManager.Instance.AddSystemInfo("Environment", testEnv.ToString());
            ExtentManager.Instance.AddSystemInfo("Browser", browserType.ToString());
            ExtentManager.Instance.Flush();
        }

        [SetUp]
        public void BeforeTest()
        {            
            string testName = GetTestName();
            string testSuite = GetTestSuiteName();            
            string testPriority = GetTestPriority();
            string testCaseNumber = GetTestCaseNumber();         
            string testComponent1 = GetTestComponent1();
            string testComponent2 = GetTestComponent2();
            string testDescription = GetTestDescription();

            driver = GetWebDriver(testPlatform, browserType);
            siteUrl = configUtil.GetSiteUrl(testEnv, tenantName);

            try
            {
                ExtentTestManager.CreateParentTest(testName);
                ExtentTestManager
                    .CreateTest($"<font size=3>TestCase# : {testCaseNumber}" +
                    $" - {testName}</font><br><font size=2>{testDescription}</font>");

                if (GetComponentsForProject(tenantName).Contains(testComponent1))
                {
                    if (GetComponentsForProject(tenantName).Contains(testComponent2) || testComponent2 == "Not Defined")
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                        driver.Manage().Window.Maximize();
                        driver.Navigate().GoToUrl(siteUrl);

                        string component2 = string.Empty;

                        if (testComponent2 != "Not Defined")
                        {
                            component2 = $"{testComponent2}";
                        }

                        LogTestDetails(tenantName, testEnv, siteUrl, browserType, testName,
                            testCaseNumber, testSuite, testDescription, testPriority, testComponent1, component2);

                        ExtentTestManager.GetTest()
                            .AssignCategory(testPriority)
                            .AssignCategory(testComponent1, component2)
                            .AssignCategory(testSuite);
                    }
                    else
                        SkipTest(testComponent2);
                }
                else
                    SkipTest(testComponent1);
            }
            catch (Exception e)
            {
                log.Error("Exception occured during BeforeTest method", e);
            }
        }

        private void SkipTest(string testComponent)
        {
            testStatus = TestStatus.Skipped;
            string msg = $"TEST SKIPPED : Project ({tenantName}) " +
                $"does not have implementation of the component ({testComponent}).";
            LogAssertIgnore(msg);
        }

        private void LogTestDetails(TenantName projectName, TestEnv testEnv, string siteUrl, BrowserType browserType,
            string tcName, string tcNumber, string suite, string tcDesc, string priority, string component1, string component2)
        {
            var comp2 = (string.IsNullOrEmpty(component2) || component2 == "Not Defined") ? string.Empty : $", {component2}";        
            string[] url = Regex.Split(siteUrl, "/Account");

            Console.WriteLine("");
            log.Info($"########################################################################");
            log.Info($"#                   RKCI ELVIS UI Test Automation");
            log.Info($"########################################################################");
            log.Info($"#  -->> Test Configuration <<--");
            log.Info($"#  Tenant: {projectName}  TestEnv: {testEnv}");
            log.Info($"#  Site URL: {url[0]}");
            log.Info($"#  Browser: {browserType}");
            log.Info($"#");
            log.Info($"#  -->> Test Case Details <<--");
            log.Info($"#  Name: {tcName}");
            log.Info($"#  Desription: {tcDesc}");
            log.Info($"#  TC#: {tcNumber}, {priority}");
            log.Info($"#  Suite: {suite}, Component(s): {component1}{comp2}");
            log.Info($"#");
            log.Info($"########################################################################\n");
        }


        [TearDown]
        public void AfterTest()
        {
            string screenshotPath = null;
            var stacktrace = string.Empty;

            if (testStatus != TestStatus.Skipped)
            {
                testResult = TestContext.CurrentContext.Result.Outcome;
                testStatus = TestContext.CurrentContext.Result.Outcome.Status;

                if (testStatus == TestStatus.Failed)
                {
                    stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                        ? "" : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);

                    screenshotPath = CaptureScreenshot(driver, GetTestName());
                    cookie = new Cookie("zaleniumTestPassed", "false");
                    ExtentTestManager.GetTest().Fail($"Test Failed:<br> {stacktrace}")
                        .AddScreenCaptureFromPath(screenshotPath);
                }
                else
                {
                    switch (testStatus)
                    {
                        case TestStatus.Passed:
                            ExtentTestManager.GetTest().Pass("Test Passed");
                            cookie = new Cookie("zaleniumTestPassed", "true");
                            break;
                        default:
                            ExtentTestManager.GetTest().Debug("Inconclusive Test Result");
                            break;
                    }
                }
            }
            else
            {
                ExtentTestManager.GetTest().Skip("Test Skipped");
            }

            if (cookie != null)
            {
                driver.Manage().Cookies.AddCookie(cookie);
            }

            if (driver != null)
            {
                driver.Close();
                driver = null;
            }
        }
    }
}
