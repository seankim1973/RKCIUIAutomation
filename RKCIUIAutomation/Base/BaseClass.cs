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
using System.Threading;
using static NUnit.Framework.TestContext;

namespace RKCIUIAutomation.Base
{
    [TestFixture]
    [Parallelizable]
    public class BaseClass : BaseUtils
    {
        #region ExtentReports Details

        [ThreadStatic]
        public static ExtentReports reportInstance;

        [ThreadStatic]
        public static ExtentTest parentTest;

        [ThreadStatic]
        public static ExtentTest testInstance;

        [ThreadStatic]
        public static TestStatus testStatus;

        #endregion ExtentReports Details

        #region HipTest Details

        [ThreadStatic]
        public HipTestApi hipTestInstance;

        [ThreadStatic]
        public int hipTestRunId;

        [ThreadStatic]
        public string[] hipTestRunDetails;

        [ThreadStatic]
        public List<int> hipTestRunTestCaseIDs;

        [ThreadStatic]
        public List<KeyValuePair<int, List<int>>> hipTestRunData;

        [ThreadStatic]
        public List<KeyValuePair<int, KeyValuePair<TestStatus, string>>> hipTestResults;

        #endregion HipTest Details

        #region Test Environment Details

        [ThreadStatic]
        public static TestPlatform testPlatform;

        [ThreadStatic]
        public static BrowserType browserType;

        [ThreadStatic]
        public static TestEnv testEnv;

        [ThreadStatic]
        public static TenantName tenantName;

        [ThreadStatic]
        public static Reporter reporter;

        [ThreadStatic]
        public static string siteUrl;

        [ThreadStatic]
        public static bool hiptest;

        [ThreadStatic]
        public static string GridVmIP;

        #endregion Test Environment Details

        #region TestCase Details

        [ThreadStatic]
        private static string testName;

        [ThreadStatic]
        private static string testSuite;

        [ThreadStatic]
        private static string testPriority;

        [ThreadStatic]
        private static string testCaseNumber;

        [ThreadStatic]
        private static string testComponent1;

        [ThreadStatic]
        internal static string testComponent2;

        [ThreadStatic]
        private static string testDescription;

        [ThreadStatic]
        private static string[] testRunDetails;

        [ThreadStatic]
        private Cookie cookie = null;

        #endregion TestCase Details

        private ConfigUtils Configs = new ConfigUtils();

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string _testPlatform = Parameters.Get("Platform", $"{TestPlatform.GridLocal}");
            string _browserType = Parameters.Get("Browser", $"{BrowserType.Chrome}");
            string _testEnv = Parameters.Get("TestEnv", $"{TestEnv.Stage}");
            string _tenantName = Parameters.Get("Tenant", $"{TenantName.SH249}");
            string _reporter = Parameters.Get("Reporter", $"{Reporter.Klov}");
            string _gridAddress = Parameters.Get("GridAddress", "");
            bool _hiptest = Parameters.Get("Hiptest", false);

            testPlatform = Configs.GetTestRunEnv<TestPlatform>(_testPlatform);
            browserType = Configs.GetTestRunEnv<BrowserType>(_browserType);
            testEnv = Configs.GetTestRunEnv<TestEnv>(_testEnv);
            tenantName = Configs.GetTestRunEnv<TenantName>(_tenantName);
            reporter = Configs.GetTestRunEnv<Reporter>(_reporter);
            siteUrl = Configs.GetSiteUrl(testEnv, tenantName);
            hiptest = _hiptest;

            testPlatform = (browserType == BrowserType.MicrosoftEdge && testPlatform != TestPlatform.Local)
                ? TestPlatform.Windows
                : testPlatform;

            DetermineReportFilePath();
            GridVmIP = SetGridAddress(testPlatform, _gridAddress);

            if (hiptest)
            {
                hipTestInstance = HipTestApi.HipTestInstance;
                hipTestRunTestCaseIDs = new List<int>();
                hipTestResults = new List<KeyValuePair<int, KeyValuePair<TestStatus, string>>>();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (hiptest)
            {
                hipTestRunDetails = testRunDetails;

                hipTestRunId = hipTestInstance.CreateTestRun(hipTestRunTestCaseIDs, hipTestRunDetails);
                hipTestRunData = hipTestInstance.BuildTestRunSnapshotData(hipTestRunId);
                hipTestInstance.UpdateHipTestRunData(hipTestRunData, hipTestResults);
                hipTestInstance.SyncTestRun(hipTestRunId);
            }

            DismissAllDriverInstances();
        }

        private void GenerateTestRunDetails()
        {
            var _suite = Regex.Split(GetType().Namespace, "\\.");

            testName = GetTestName();
            testSuite = _suite[_suite.Length - 1];
            testPriority = GetTestPriority();
            testCaseNumber = GetTestCaseNumber();
            testComponent1 = GetTestComponent1();
            testComponent2 = GetTestComponent2();
            testDescription = GetTestDescription();

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
        }

        private void InitExtentTestInstance()
        {
            reportInstance = ExtentManager.GetReportInstance();
            parentTest = (reporter == Reporter.Html) ?
                reportInstance.CreateTest(testCaseNumber, testName, tenantName, testEnv) : null;
            testInstance = (reporter == Reporter.Html) ?
                parentTest.CreateNode(testDescription) : testInstance = reportInstance.CreateTest(testCaseNumber, testName, tenantName, testEnv);
        }

        private IWebDriver InitWebDriverInstance()
        {
            List<string> tenantComponents = new List<string>();
            ProjectProperties props = new ProjectProperties();
            tenantComponents = props.GetComponentsForProject(tenantName);

            if (tenantComponents.Contains(testComponent1))
            {
                if (tenantComponents.Contains(testComponent2) || string.IsNullOrEmpty(testComponent2))
                {
                    string testDetails = $"({testEnv}){tenantName} - {testName}";
                    Driver = GetWebDriver(testPlatform, browserType, testDetails, GridVmIP);
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(45);
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

            return Driver;
        }

        [SetUp]
        public void BeforeTest()
        {
            GenerateTestRunDetails();

            if (hiptest)
            {
                hipTestRunTestCaseIDs.Add(int.Parse(testCaseNumber));
            }

            InitExtentTestInstance();

            Driver = InitWebDriverInstance();
        }

        private void SkipTest(string testComponent = "", string[] reportCategories = null)
        {
            //string component = string.IsNullOrEmpty(testComponent2) ? testComponent1 : testComponent2;
            reportCategories = reportCategories ?? testRunDetails;
            var component = string.IsNullOrEmpty(testComponent2) ? testComponent1 : testComponent2;
            testComponent = testComponent.Equals("") ? component : testComponent;
            testInstance.AssignReportCategories(reportCategories);
            string msg = $"TEST SKIPPED : Tenant {tenantName} does not have implementation of component ({testComponent}).";
            LogAssertIgnore(msg);
            BaseHelper.InjectTestStatus(TestStatus.Skipped, msg);
            Assert.Ignore(msg);
        }

        private void LogTestDetails(string[] testDetails)
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
            log.Info($"#  Description: {testDescription}");
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
                List<object> testResults = result.CheckForTestStatusInjection();
                testStatus = (TestStatus)testResults[0];

                switch (testStatus)
                {
                    case TestStatus.Failed:
                        string injMsg = (string)testResults[1];
                        string stacktrace = string.IsNullOrEmpty(result.StackTrace) ? (injMsg = string.IsNullOrEmpty(injMsg) ? "" : $"{injMsg}<br> ") : $"<pre>{result.StackTrace}</pre>";
                        string screenshotName = CaptureScreenshot(GetTestName());

                        if (reporter == Reporter.Klov)
                        {
                            /*Use when Klov Reporter bug is fixed
                            //Upload screenshot to MongoDB server
                            var screenshotPath = $"\\\\10.1.1.207\\errorscreenshots\\{screenshotName}";
                            testInstance.Fail($"Test Failed: <br> {stacktrace}").AddScreenCaptureFromPath(screenshotPath, screenshotName);
                            */

                            //Workaround due to bug in Klov Reporter
                            var screenshotRemotePath = $"http://{GridVmIP}/errorscreenshots/{screenshotName}";
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

                if (hiptest)
                {
                    var resultDesc = new KeyValuePair<TestStatus, string>(testStatus, testDescription);
                    var tcResultPair = new KeyValuePair<int, KeyValuePair<TestStatus, string>>(int.Parse(testCaseNumber), resultDesc);
                    hipTestResults.Add(tcResultPair);
                }
            }
            catch (Exception e)
            {
                log.Error($"Exception occured for Failed TC in AfterTest method {e.Message}");
            }
            finally
            {
                if (Driver != null)
                {
                    reportInstance.Flush();

                    if (cookie != null)
                    {
                        Driver.Manage().Cookies.AddCookie(cookie);
                    }

                    if (!Driver.Title.Equals("Home Page"))
                    {
                        try
                        {
                            Driver.FindElement(By.XPath("//a[text()=' Log out']")).Click();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    DismissDriverInstance(Driver);
                }
            }
        }
    }
}