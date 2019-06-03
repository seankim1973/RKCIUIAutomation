using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Tools;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using static NUnit.Framework.TestContext;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.StaticHelpers;

namespace RKCIUIAutomation.Base
{
    [TestFixture]
    [Parallelizable]
    public class BaseClass : BaseHook
    {
        public BaseClass()
        {
        }

        readonly TestPlatform defaultTestPlatform = TestPlatform.GridLocal;
        readonly BrowserType defaultBrowserType = BrowserType.Chrome;
        readonly TestEnv defaultTestEnvironment = TestEnv.Staging;         
        readonly TenantName defaultTenantName = TenantName.LAX;
        readonly Reporter defaultReporter = Reporter.Klov;
        readonly string defaultGridAddress = "";
        readonly bool enableHipTest = false;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string _testPlatform = Parameters.Get("Platform", $"{defaultTestPlatform}");
            string _browserType = Parameters.Get("Browser", $"{defaultBrowserType}");
            string _testEnv = Parameters.Get("TestEnv", $"{defaultTestEnvironment}");
            string _tenantName = Parameters.Get("Tenant", $"{defaultTenantName}");
            string _reporter = Parameters.Get("Reporter", $"{defaultReporter}");
            string _gridAddress = Parameters.Get("GridAddress", defaultGridAddress);
            bool _hiptest = Parameters.Get("Hiptest", enableHipTest);

            IConfigUtils config = ConfigUtil;
            testPlatform = config.GetTestRunEnv<TestPlatform>(_testPlatform);
            browserType = config.GetTestRunEnv<BrowserType>(_browserType);
            testEnv = config.GetTestRunEnv<TestEnv>(_testEnv);
            tenantName = config.GetTestRunEnv<TenantName>(_tenantName);
            reporter = config.GetTestRunEnv<Reporter>(_reporter);
            siteUrl = config.GetSiteUrl(testEnv, tenantName);
            hiptest = _hiptest;

            testPlatform = browserType == BrowserType.MicrosoftEdge
                ? testPlatform == TestPlatform.Local
                    ? TestPlatform.Windows
                    : testPlatform
                : testPlatform;

            SetReportPath(tenantName);
            SetGridAddress(testPlatform, _gridAddress);

            if (hiptest)
            {
                hipTestInstance = HipTestApi.HipTestInstance;
                hipTestRunTestCaseIDs = new List<int>();
                hipTestResults = new List<KeyValuePair<int, KeyValuePair<TestStatus, string>>>();
            }
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

            InitWebDriverInstance();
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
                        string stacktrace = result.StackTrace.HasValue()
                            ? (injMsg = injMsg.HasValue()
                                ? ""
                                : $"{injMsg}<br> ")
                            : $"<pre>{result.StackTrace}</pre>";
                        string screenshotName = BaseUtil.CaptureScreenshot();

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
                if (driver != null)
                {
                    reportInstance.Flush();

                    if (cookie != null)
                    {
                        driver.Manage().Cookies.AddCookie(cookie);
                    }

                    if (!driver.Title.Equals("Home Page"))
                    {
                        try
                        {
                            driver.FindElement(By.XPath("//a[text()=' Log out']")).Click();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    DismissDriverInstance(driver);
                }
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
        }
    }
}