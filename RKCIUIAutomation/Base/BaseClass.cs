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
using System.Diagnostics;
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

        readonly TestPlatformType defaultTestPlatform = TestPlatformType.GridLocal;
        readonly BrowserType defaultBrowserType = BrowserType.Chrome;
        readonly TestEnvironmentType defaultTestEnvironment = TestEnvironmentType.Staging; //When TestEnv is set to PreProduction, TenantName value is ignored
        readonly TenantNameType defaultTenantName = TenantNameType.I15South;
        readonly ReporterType defaultReporter = ReporterType.Klov;
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
            testPlatform = config.GetTestRunEnv<TestPlatformType>(_testPlatform);
            browserType = config.GetTestRunEnv<BrowserType>(_browserType);
            testEnv = config.GetTestRunEnv<TestEnvironmentType>(_testEnv);
            tenantName = config.GetTestRunEnv<TenantNameType>(_tenantName);
            reporter = config.GetTestRunEnv<ReporterType>(_reporter);
            siteUrl = config.GetSiteUrl(testEnv, tenantName);
            hiptest = _hiptest;

            testPlatform = browserType == BrowserType.MicrosoftEdge
                ? testPlatform == TestPlatformType.Local
                    ? TestPlatformType.Windows
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

            TestStopwatch = new Stopwatch();
            TestStopwatch.Start();
        }

        [TearDown]
        public void AfterTest()
        {
            string zaleniumTestStatusCookieValue = string.Empty;

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

                        if (reporter == ReporterType.Klov)
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

                        zaleniumTestStatusCookieValue = "false";
                        break;

                    case TestStatus.Passed:
                        testInstance.Pass(MarkupHelper.CreateLabel("Test Passed", ExtentColor.Green));
                        zaleniumTestStatusCookieValue = "true";
                        break;

                    case TestStatus.Skipped:
                        testInstance.Skip(MarkupHelper.CreateLabel("Test Skipped", ExtentColor.Yellow));
                        break;

                    default:
                        testInstance.Debug(MarkupHelper.CreateLabel("Inconclusive Test Result", ExtentColor.Orange));
                        break;
                }

                TestStopwatch.Stop();

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
                try
                {
                    if (driver != null)
                    {
                        Report.Info($"TOTAL TEST TIME: {TestStopwatch.Elapsed.ToString()}");
                        reportInstance.Flush();

                        if (cookie != null)
                        {
                            AddCookieToCurrentPage("zaleniumTestPassed", zaleniumTestStatusCookieValue);
                        }

                        if (!driver.Title.Equals("Home Page"))
                        {
                            driver.FindElement(By.XPath("//a[text()=' Log out']")).Click();
                        }

                        DismissDriverInstance(driver);
                    }
                }
                catch (UnableToSetCookieException e)
                {
                    log.Debug(e.Message);
                }
                catch (NoSuchElementException e)
                {
                    log.Debug(e.Message);
                }
                catch (Exception e)
                {
                    log.Error($"{e.Message}\n{e.StackTrace}");
                }               
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            try
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
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }
        }
    }
}