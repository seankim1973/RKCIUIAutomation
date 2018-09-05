using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using static NUnit.Framework.TestContext;

namespace RKCIUIAutomation.Base
{
    [TestFixture]
    [Parallelizable]
    public class BaseClass : BaseUtils
    {
        [ThreadStatic]
        public static ExtentReports reportInstance;

        [ThreadStatic]
        public static ExtentTest parentTest;

        [ThreadStatic]
        public static ExtentTest testInstance;

        [ThreadStatic]
        public static TestStatus testStatus;

        [ThreadStatic]
        public static string nodeHost;

        public static TestPlatform testPlatform;
        public static BrowserType browserType;
        public static TestEnv testEnv;
        public static TenantName tenantName;
        public static Reporter reporter;
        private static string _testPlatform;
        private static string _browserType;
        private static string _testEnv;
        private static string _tenantName;
        private static string _reporter;
        public static string siteUrl;


        private Cookie cookie = null;

        ConfigUtils Configs = new ConfigUtils();
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testPlatform = Parameters.Get("Platform", $"{TestPlatform.Grid}");
            _browserType = Parameters.Get("Browser", $"{BrowserType.Chrome}");
            _testEnv = Parameters.Get("TestEnv", $"{TestEnv.Stage}");
            _tenantName = Parameters.Get("Tenant", $"{TenantName.SH249}");
            _reporter = Parameters.Get("Reporter", $"{Reporter.Klov}");

            testPlatform = Configs.GetTestPlatform(_testPlatform);
            browserType = Configs.GetBrowserType(_browserType);
            testEnv = Configs.GetTestEnv(_testEnv);
            tenantName = Configs.GetTenantName(_tenantName);
            siteUrl = Configs.GetSiteUrl(testEnv, tenantName);
            reporter = Configs.GetReporter(_reporter);

            testPlatform = (browserType == BrowserType.MicrosoftEdge && testPlatform != TestPlatform.Local) ? TestPlatform.Windows : testPlatform;

            DetermineReportFilePath();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //log.Info(userName);
            //log.Info(displayUrl);
            //log.Info($"ExtentReports HTML Test Report page created at {ExtentManager.reportFilePath}");
            
            if (Driver != null)
            {
                Driver.Quit();
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

            reportInstance = ExtentManager.Instance;

            if (reporter == Reporter.Html)
            {
                parentTest = reportInstance.CreateTest(testCaseNumber, testName, tenantName, testEnv);
                testInstance = parentTest.CreateNode(testDescription);
            }
            else
            {
                testInstance = reportInstance.CreateTest(testCaseNumber, testName, tenantName, testEnv);
            }

            ProjectProperties props = new ProjectProperties();
            List<string> tenantComponents = new List<string>();

            tenantComponents = props.GetComponentsForProject(tenantName);

            if (tenantComponents.Contains(testComponent1))
            {
                if (tenantComponents.Contains(testComponent2) || testComponent2 == "Not Defined")
                {
                    Driver = GetWebDriver(testPlatform, browserType, testName);
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                    Driver.Manage().Window.Maximize();
                    Driver.Navigate().GoToUrl($"{siteUrl}/Account/LogIn");

                    string component2 = string.Empty;

                    if (testComponent2 != "Not Defined")
                    {
                        component2 = $"{testComponent2}";
                    }

                    LogTestDetails(tenantName, testEnv, siteUrl, browserType, testName,
                        testCaseNumber, testSuite, testDescription, testPriority, testComponent1, component2);

                    testInstance
                        .AssignCategory(tenantName.ToString())
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

            log.Info($"################################################################");
            log.Info($"#                   RKCI ELVIS UI Test Automation");
            log.Info($"################################################################");
            log.Info($"#  -->> Test Configuration <<--");
            log.Info($"#  Tenant: {projectName}  TestEnv: {testEnv}");
            log.Info($"#  Site URL: {siteUrl}");
            log.Info($"#  Browser: {browserType}");
            log.Info($"#");
            log.Info($"#  -->> Test Case Details <<--");
            log.Info($"#  Name: {tcName}");
            log.Info($"#  Desription: {tcDesc}");
            log.Info($"#  TC#: {tcNumber}, {priority}");
            log.Info($"#  Suite: {suite}, Component(s): {component1}{comp2}");
            log.Info($"#  Date & Time: {DateTime.Now.ToShortDateString()}  {DateTime.Now.ToShortTimeString()}");
            log.Info($"################################################################\n");
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
                    string stacktrace = string.IsNullOrEmpty(result.StackTrace) ? "" : $"<pre>{result.StackTrace}</pre>";
                    string screenshotPath = CaptureScreenshot(GetTestName());
                    testInstance.Fail($"Test Failed:<br> {stacktrace}")
                        .AddScreenCaptureFromPath(screenshotPath);
                    cookie = new Cookie("zaleniumTestPassed", "false");
                    break;
                case TestStatus.Passed:
                    testInstance.Pass("Test Passed");
                    cookie = new Cookie("zaleniumTestPassed", "true");
                    break;
                case TestStatus.Skipped:
                    testInstance.Skip("Test Skipped");
                    break;
                default:
                    testInstance.Debug("Inconclusive Test Result");
                    break;
            }
            
            reportInstance.Flush();

            if (Driver != null)
            {
                if (cookie != null)
                {
                    Driver.Manage().Cookies.AddCookie(cookie);
                }
                Driver.FindElement(By.XPath("//a[text()=' Log out']"))?.Click();
                Driver.Close();
            }            
        }
    }
}
