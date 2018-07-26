using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static NUnit.Framework.TestContext;


namespace RKCIUIAutomation.Base
{
    [TestFixture]
    [Parallelizable]
    public class BaseClass : BaseUtils
    {
        public static TestPlatform testPlatform;
        public static BrowserType browserType;
        public static TestEnv testEnv;
        public static TenantName tenantName;
        public static string userName = string.Empty;
        private static string _testPlatform;
        private static string _browserType;
        private static string _testEnv;
        private static string _tenantName;
        private string siteUrl;
        private string displayUrl;
        private TestStatus testStatus;
        private Cookie cookie = null;
                
        ConfigUtils Configs = new ConfigUtils();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testPlatform = Parameters.Get("Platform", $"{TestPlatform.Local}");
            _browserType = Parameters.Get("Browser", $"{BrowserType.Chrome}");
            _testEnv = Parameters.Get("TestEnv", $"{TestEnv.Stage}");
            _tenantName = Parameters.Get("Tenant", $"{TenantName.GLX}");

            testPlatform = Configs.GetTestPlatform(_testPlatform);
            browserType = Configs.GetBrowserType(_browserType);
            testEnv = Configs.GetTestEnv(_testEnv);
            tenantName = Configs.GetTenantName(_tenantName);
            
            DetermineReportFilePath();
            ExtentTestManager.CreateParentTest(GetType().Name, tenantName, testEnv, browserType);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //log.Info(userName);
            //log.Info(displayUrl);
            //log.Info($"ExtentReports HTML Test Report page created at {ExtentManager.reportFilePath}");
            ExtentManager.Instance.Flush();

            if (driver != null)
            {
                driver.Quit();
            }
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
            
            ExtentTestManager
                .CreateTest($"TestCase# : {testCaseNumber} {testName} - {testDescription}");

            ProjectProperties props = new ProjectProperties();
            List<string> tenantComponents = new List<string>();

            tenantComponents = props.GetComponentsForProject(tenantName);

            if (tenantComponents.Contains(testComponent1))
            {
                if (tenantComponents.Contains(testComponent2) || testComponent2 == "Not Defined")
                {
                    driver = GetWebDriver(testPlatform, browserType, testName);
                    siteUrl = Configs.GetSiteUrl(testEnv, tenantName);

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
                {
                    SkipTest(testComponent2);
                }
            }
            else
            {
                SkipTest(testComponent1);
            }
        }

        private void SkipTest(string testComponent)
        {
            string msg = $"TEST SKIPPED : Tenant {tenantName} does not have implementation of component ({testComponent}).";
            LogIgnore(msg);
            Assert.Ignore(msg);
        }

        private void LogTestDetails(TenantName projectName, TestEnv testEnv, string siteUrl, BrowserType browserType,
            string tcName, string tcNumber, string suite, string tcDesc, string priority, string component1, string component2)
        {
            var comp2 = (string.IsNullOrEmpty(component2) || component2 == "Not Defined") ? string.Empty : $", {component2}";        
            string[] url = Regex.Split(siteUrl, "/Account");
            displayUrl = url[0];

            log.Info($"#################################################");
            log.Info($"#                   RKCI ELVIS UI Test Automation");
            log.Info($"#################################################");
            log.Info($"#  -->> Test Configuration <<--");
            log.Info($"#  Tenant: {projectName}  TestEnv: {testEnv}");
            log.Info($"#  Site URL: {displayUrl}");
            log.Info($"#  Browser: {browserType}");
            log.Info($"#");
            log.Info($"#  -->> Test Case Details <<--");
            log.Info($"#  Name: {tcName}");
            log.Info($"#  Desription: {tcDesc}");
            log.Info($"#  TC#: {tcNumber}, {priority}");
            log.Info($"#  Suite: {suite}, Component(s): {component1}{comp2}");
            log.Info($"#  Date & Time: {DateTime.Now.ToShortDateString()}  {DateTime.Now.ToShortTimeString()}");
            log.Info($"#################################################\n");
        }


        [TearDown]
        public void AfterTest()
        {         
            ResultAdapter result = CurrentContext.Result;
            testStatus = result.Outcome.Status;
            CheckForTestStatusInjection();

            switch (testStatus)
            {
                case TestStatus.Failed:
                    string stacktrace = string.IsNullOrEmpty(result.StackTrace)
                        ? "" : string.Format("<pre>{0}</pre>", result.StackTrace);
                    string screenshotPath = CaptureScreenshot(driver, GetTestName());
                    cookie = new Cookie("zaleniumTestPassed", "false");
                    driver.Manage().Cookies.AddCookie(cookie);
                    ExtentTestManager.GetTest().Fail($"Test Failed:<br> {stacktrace}")
                        .AddScreenCaptureFromPath(screenshotPath);
                    break;
                case TestStatus.Passed:
                    ExtentTestManager.GetTest().Pass("Test Passed");
                    cookie = new Cookie("zaleniumTestPassed", "true");
                    driver.Manage().Cookies.AddCookie(cookie);
                    break;
                case TestStatus.Skipped:
                    ExtentTestManager.GetTest().Skip("Test Skipped");
                    break;
                default:
                    ExtentTestManager.GetTest().Debug("Inconclusive Test Result");
                    break;
            }

            if (driver != null)
            {
                driver.FindElement(By.XPath("//a[text()=' Log out']"))?.Click();
                driver.Close();
            }
        }
    }
}
