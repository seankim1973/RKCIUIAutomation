using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Tools;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static NUnit.Framework.TestContext;

namespace RKCIUIAutomation.Base
{
    [TestFixture]
    public class BaseClass : BaseUtils
    {
        //ExtentReports
        [ThreadStatic]
        public static ExtentReports reportInstance;
        [ThreadStatic]
        public static ExtentTest parentTest;
        [ThreadStatic]
        public static ExtentTest testInstance;
        [ThreadStatic]
        public static TestStatus testStatus;

        //Test Environment
        public static TestPlatform testPlatform;
        public static BrowserType browserType;
        public static TestEnv testEnv;
        public static TenantName tenantName;
        public static Reporter reporter;
        public static string siteUrl;
        public static bool hiptest;

        //TestCase Details
        private List<int> testCaseIDs;
        private string[] testRunDetails;
        private string testName;
        private string testSuite;
        private string testPriority;
        private string testCaseNumber;
        private string testComponent1;
        private string testComponent2;
        private string testDescription;

        private Cookie cookie = null;
        ConfigUtils Configs = new ConfigUtils();
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string _testPlatform = Parameters.Get("Platform", $"{TestPlatform.Grid}");
            string _browserType = Parameters.Get("Browser", $"{BrowserType.Chrome}");
            string _testEnv = Parameters.Get("TestEnv", $"{TestEnv.Stage}");
            string _tenantName = Parameters.Get("Tenant", $"{TenantName.GLX}");
            string _reporter = Parameters.Get("Reporter", $"{Reporter.Klov}");
            bool _hiptest = Parameters.Get("Hiptest", false);

            testPlatform = Configs.GetTestRunEnv<TestPlatform>(_testPlatform);
            browserType = Configs.GetTestRunEnv<BrowserType>(_browserType);
            testEnv = Configs.GetTestRunEnv<TestEnv>(_testEnv);
            tenantName = Configs.GetTestRunEnv<TenantName>(_tenantName);
            reporter = Configs.GetTestRunEnv<Reporter>(_reporter);
            siteUrl = Configs.GetSiteUrl(testEnv, tenantName);
            hiptest = _hiptest;

            testCaseIDs = (hiptest) ? new List<int> { } : null;
            testPlatform = (browserType == BrowserType.MicrosoftEdge && testPlatform != TestPlatform.Local) ? TestPlatform.Windows : testPlatform;
            DetermineReportFilePath();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (hiptest)
            {
                string testRunName = $"{tenantName}({testEnv})";
                HipTestApi hipTestApi = new HipTestApi();
                hipTestApi.CreateTestRun(testCaseIDs, testRunDetails);
            }
            
            if (Driver != null)
            {
                Driver.Quit();
            }
        }

        [SetUp]
        public void BeforeTest()
        {
            var _suite = Regex.Split(GetType().Namespace, "\\.");

            testName = GetTestName();
            testSuite = _suite[_suite.Length -1];
            testPriority = GetTestPriority();
            testCaseNumber = GetTestCaseNumber();         
            testComponent1 = GetTestComponent1();
            testComponent2 = GetTestComponent2();
            testDescription = GetTestDescription();

            if (hiptest)
            {
                testCaseIDs.Add(int.Parse(testCaseNumber));
            }

            testRunDetails = new string[]
            {
                testSuite,
                testPriority,
                testCaseNumber,
                testComponent1,
                testComponent2,
                testEnv.ToString(),
                tenantName.ToString()
            };
            
            reportInstance = ExtentManager.Instance;
            parentTest = (reporter == Reporter.Html) ? 
                reportInstance.CreateTest(testCaseNumber, testName, tenantName, testEnv) : null;
            testInstance = (reporter == Reporter.Html) ? 
                parentTest.CreateNode(testDescription) : testInstance = reportInstance.CreateTest(testCaseNumber, testName, tenantName, testEnv);

            List<string> tenantComponents = new List<string>();
            ProjectProperties props = new ProjectProperties();
            tenantComponents = props.GetComponentsForProject(tenantName);

            if (tenantComponents.Contains(testComponent1))
            {
                if (tenantComponents.Contains(testComponent2) || string.IsNullOrEmpty(testComponent2))
                {
                    Driver = GetWebDriver(testPlatform, browserType, testName);
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                    Driver.Manage().Window.Maximize();
                    Driver.Navigate().GoToUrl($"{siteUrl}/Account/LogIn");

                    LogTestDetails(testRunDetails);
                    testInstance.AssignReportCategories(testRunDetails);
                }
                else
                {
                    SkipTest(testComponent2, testRunDetails);
                }
            }
            else
            {
                SkipTest(testComponent1, testRunDetails);
            }
        }

        private void SkipTest(string testComponent, string[] reportCategories)
        {
            string msg = $"TEST SKIPPED : Tenant {tenantName} does not have implementation of component ({testComponent}).";
            testInstance.AssignReportCategories(reportCategories);
            LogIgnore(msg);
            Assert.Ignore(msg);
        }

        private void LogTestDetails(params string[] testDetails)
        {
            string _suite = testDetails[0];
            string _priority = testDetails[1];
            string _tcNumber = testDetails[2];
            string _component1 = testDetails[3];
            string _component2 = testDetails[4];
            string _testEnv = testDetails[5];
            string _tenantName = testDetails[6];

            string components = (string.IsNullOrEmpty(_component2)) ? $": {_component1}" : $"s: {_component1}, {_component2}";

            log.Info($"################################################################");
            log.Info($"#                   RKCI ELVIS UI Test Automation");
            log.Info($"################################################################");
            log.Info($"#  -->> Test Configuration <<--");
            log.Info($"#  Tenant: {_tenantName}  TestEnv: {_testEnv}");
            log.Info($"#  Site URL: {siteUrl}");
            log.Info($"#  Browser: {browserType.ToString()}");
            log.Info($"#");
            log.Info($"#  -->> Test Case Details <<--");
            log.Info($"#  Name: {testName}");
            log.Info($"#  Desription: {testDescription}");
            log.Info($"#  TC#: {_tcNumber}, {_priority}");
            log.Info($"#  Suite: {_suite}, Component{components}");
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
