using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static NUnit.Framework.TestContext;

namespace RKCIUIAutomation.Base
{
    [TestFixture]
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

        private string[] reportCategories;
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
            _tenantName = Parameters.Get("Tenant", $"{TenantName.SGWay}");
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
            string testClass = GetTestClassName();
            //string testSuite = GetTestSuiteName();
            var _suite = Regex.Split(GetType().Namespace, "\\.");
            string testSuite = _suite[_suite.Length -1];
            string testPriority = GetTestPriority();
            string testCaseNumber = GetTestCaseNumber();         
            string testComponent1 = GetTestComponent1();
            string testComponent2 = GetTestComponent2();
            string testDescription = GetTestDescription();

            reportCategories = new string[]
            {
                testEnv.ToString(),
                tenantName.ToString(),
                testSuite,
                testClass,
                testPriority,
                testCaseNumber,
                testComponent1,
                testComponent2
            };

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
                if (tenantComponents.Contains(testComponent2) || string.IsNullOrEmpty(testComponent2))
                {
                    Driver = GetWebDriver(testPlatform, browserType, testName);
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                    Driver.Manage().Window.Maximize();
                    Driver.Navigate().GoToUrl($"{siteUrl}/Account/LogIn");

                    LogTestDetails(tenantName, testEnv, siteUrl, browserType, testName,
                        testCaseNumber, testClass, testDescription, testPriority, testComponent1, testComponent2);
                    //testInstance.AssignCategory(reportCategories);
                    testInstance.AssignReportCategories(reportCategories);
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
            testInstance.AssignReportCategories(reportCategories);
            LogIgnore(msg);
            Assert.Ignore(msg);
        }

        private void LogTestDetails(TenantName projectName, TestEnv testEnv, string siteUrl, BrowserType browserType,
            string tcName, string tcNumber, string suite, string tcDesc, string priority, string component1, string component2)
        {
            var components = (string.IsNullOrEmpty(component2)) ? $": {component1}" : $"s: {component1}, {component2}";

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
            log.Info($"#  Suite: {suite}, Component{components}");
            log.Info($"#  Date & Time: {DateTime.Now.ToShortDateString()}  {DateTime.Now.ToShortTimeString()}");
            log.Info($"################################################################\n");
        }


        [TearDown]
        public void AfterTest()
        {
            try
            {
                ResultAdapter result = CurrentContext.Result;
                testStatus = result.CheckForTestStatusInjection();

                switch (testStatus)
                {
                    case TestStatus.Failed:
                        string stacktrace = string.IsNullOrEmpty(result.StackTrace) ? "" : $"<pre>{result.StackTrace}</pre>";
                        string screenshotName = CaptureScreenshot(GetTestName());
                       
                        if(reporter == Reporter.Klov)
                        {
                            /*Use when Klov Reporter bug is fixed
                            //Upload screenshot to MongoDB server
                            var screenshotPath = $"\\\\10.1.1.207\\errorscreenshots\\{screenshotName}";
                            testInstance.Fail($"Test Failed: <br> {stacktrace}").AddScreenCaptureFromPath(screenshotPath, screenshotName);
                            */

                            //Workaround due to bug in Klov Reporter
                            var screenshotRemotePath = $"http://10.1.1.207/errorscreenshots/{screenshotName}";
                            var detailsWithScreenshot = $"Test Failed:<br> {stacktrace}<br> <img data-featherlight=\"{screenshotRemotePath}\" class=\"step-img\" src=\"{screenshotRemotePath}\" data-src=\"{screenshotRemotePath}\" width=\"200\">";
                            testInstance.Fail(MarkupHelper.CreateLabel(detailsWithScreenshot, ExtentColor.Red));
                        }
                        else
                        {
                            //Attach screenshot to log
                            var screenshotPath = $"errorscreenshots/{screenshotName}";
                            testInstance.Fail($"Test Failed: <br> {stacktrace}", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath, screenshotName).Build());
                        }

                        cookie = new Cookie("zaleniumTestPassed", "false");
                        break;
                    case TestStatus.Passed:
                        testInstance.Pass(MarkupHelper.CreateLabel("Test Passed", ExtentColor.Green));
                        cookie = new Cookie("zaleniumTestPassed", "true");
                        break;
                    case TestStatus.Skipped:
                        testInstance.Skip(MarkupHelper.CreateLabel("Test Skipped", ExtentColor.Yellow));
                        break;
                    default:
                        testInstance.Debug(MarkupHelper.CreateLabel("Inconclusive Test Result", ExtentColor.Orange));
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
            catch (Exception e)
            {
                Console.WriteLine($"Exception occured for Failed TC in AfterTest method {e.Message}");
            }
        }
    }
}
