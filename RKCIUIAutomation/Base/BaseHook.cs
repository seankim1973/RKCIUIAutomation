using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Base
{
    public class BaseHook : WebDriverFactory
    {
        [ThreadStatic]
        public static string pageTitle = string.Empty;

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
        internal static string testName;

        [ThreadStatic]
        internal static string testSuite;

        [ThreadStatic]
        internal static string testPriority;

        [ThreadStatic]
        internal static string testCaseNumber;

        [ThreadStatic]
        internal static string testComponent1;

        [ThreadStatic]
        internal static string testComponent2;

        [ThreadStatic]
        internal static string testDescription;

        [ThreadStatic]
        internal static string[] testRunDetails;

        [ThreadStatic]
        internal static Cookie cookie = null;

        [ThreadStatic]
        internal static string testDetails;

        public static void AddCookieToCurrentPage(string zaleniumCookieName, string cookieValue)
        {
            cookie = new Cookie(zaleniumCookieName, cookieValue);
            driver.Manage().Cookies.AddCookie(cookie);
        }


        #endregion TestCase Details

        internal static string tmpDevEnvIP = "http://10.0.70.68:3000";

        internal void GenerateTestRunDetails()
        {
            var _suite = Regex.Split(GetType().Namespace, "\\.");

            IBaseUtils baseUtil = BaseUtil;
            testName = baseUtil.GetTestName();
            testSuite = _suite[_suite.Length - 1];
            testPriority = baseUtil.GetTestPriority();
            testCaseNumber = baseUtil.GetTestCaseNumber();
            testComponent1 = baseUtil.GetTestComponent1();
            testComponent2 = baseUtil.GetTestComponent2();
            testDescription = baseUtil.GetTestDescription();

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

        internal void InitExtentTestInstance()
        {
            reportInstance = ExtentManager.GetReportInstance();
            testInstance = reportInstance.CreateTest($"Suite: {testSuite} | Tenant: {tenantName} | Env: {testEnv} | Test: {testName} | Hiptest TC# {testCaseNumber}");
            //testInstance = parentTest.CreateNode($"{testCaseNumber} {testName}");
        }

        /// <summary>
        /// Default wait time is set to 1 Second and only used when searching for WebElements using driver.findElement();
        /// <para>Interaction with WebElements should use helper methods in the PageInteraction class (e.g. ClickElement(), EnterText(), GetElement(), etc)</para>
        /// </summary>
        internal void InitWebDriverInstance()
        {
            try
            {
                ITenantProperties props = Factory.TenantProperty;
                props.ConfigTenantComponents(tenantName);

                if (props.TenantComponents.Contains(testComponent1))
                {
                    if (props.TenantComponents.Contains(testComponent2) || !testComponent2.HasValue())
                    {
                        testDetails = $"({testEnv}){tenantName} - {testName}";
                        Driver = SetWebDriver(testPlatform, browserType, testDetails, GridVmIP);
                        //1 Second default ImplicitWait time - !!!DO NOT CHANGE!!!
                        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
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
            catch (Exception)
            {
                //log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }
            finally
            {
                try
                {
                    driver = Driver;

                    if (cookie != null)
                    {
                        AddCookieToCurrentPage("zaleniumMessage", testDetails);
                    }
                }
                catch (UnhandledAlertException ae)
                {
                    log.Debug(ae.Message);
                }
                catch (Exception)
                {
                    //log.Error($"{e.Message}\n{e.StackTrace}");
                    throw;
                }
            }
        }

        internal void SkipTest(string testComponent = "", string[] reportCategories = null)
        {
            try
            {
                reportCategories = reportCategories ?? testRunDetails;
                var component = !testComponent2.HasValue()
                    ? testComponent1
                    : testComponent2;
                testComponent = testComponent.HasValue()
                    ? testComponent
                    : component;

                testInstance.AssignReportCategories(reportCategories);
                string msg = $"TEST SKIPPED : Tenant {tenantName} does not have implementation of component ({testComponent}).";
                Report.AssertIgnore(msg);
                StaticHelpers.InjectTestStatus(TestStatus.Skipped, msg);
                Assert.Ignore(msg);
            }
            catch (Exception)
            {
                throw;
            }
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

            string components = !_component2.HasValue()
                ? $": {_component1}"
                : $"s: {_component1}, {_component2}";

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
    }
}
