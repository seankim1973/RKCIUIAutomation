using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using log4net;
using log4net.Core;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Base.BaseClass;

namespace RKCIUIAutomation.Base
{
    public class BaseUtils : ConfigUtils
    {
        public static readonly ILog log = LogManager.GetLogger("");

        public static string extentReportPath = string.Empty;
        public static string fullTempFileName = string.Empty;
        private static string baseTempFolder = string.Empty;
        private static string fileName = string.Empty;
        private static string dateString = string.Empty;
        private static string screenshotSavePath = string.Empty;
        
        public BaseUtils(IWebDriver driver) => this.Driver = driver;

        public BaseUtils()
        {
            baseTempFolder = $"{GetCodeBasePath()}\\Temp";
            fileName = BaseClass.tenantName.ToString();
            dateString = GetDateString();
        }

        private string SetWinTempFolder()
        {
            string cTemp = "C:\\Temp";
            if (!File.Exists(cTemp))
            {
                Directory.CreateDirectory(cTemp);
            }

            return cTemp;
        }

        private string GetDateString()
        {
            string[] shortDate = (DateTime.Today.ToShortDateString()).Split('/');
            string month = shortDate[0];
            string date = shortDate[1];

            month = (month.Length > 1) ? month : $"0{month}";
            date = (date.Length > 1) ? date : $"0{date}";

            return $"{month}{date}{shortDate[2]}";
        }

        public static void DetermineReportFilePath()
        {
            extentReportPath = $"{GetCodeBasePath()}\\Report";
            screenshotSavePath = $"{extentReportPath}\\errorscreenshots\\";
        }

        public static string GetCodeBasePath()
        {
            Directory.SetCurrentDirectory(Directory.GetParent(TestContext.CurrentContext.TestDirectory).ToString());
            string baseDir = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            return baseDir;
        }

        public string CaptureScreenshot(string fileName)
        {
            string uniqueFileName = string.Empty;
            string fullFilePath = string.Empty;
            string klovPath = string.Empty;

            try
            {
                Directory.CreateDirectory(screenshotSavePath);
                uniqueFileName = $"{fileName}{DateTime.Now.Second}_{tenantName.ToString()}.png";
                fullFilePath = $"{screenshotSavePath}{uniqueFileName}";

                if (reporter == Reporter.Klov)
                {
                    if (testPlatform == TestPlatform.Grid)
                    {
                        klovPath = @"\\10.1.1.207\errorscreenshots\";

                        ImpersonateUser impersonateUser = new ImpersonateUser(Driver);
                        impersonateUser.ScreenshotTool(ImpersonateUser.Task.SAVESCREENSHOT, $"{klovPath}{uniqueFileName}");
                    }
                    else if (testPlatform == TestPlatform.GridLocal)
                    {
                        klovPath = @"C:\Automation\klov\errorscreenshots\";
                        var screenshot = Driver.TakeScreenshot();
                        screenshot.SaveAsFile($"{klovPath}{uniqueFileName}");
                    }
                }
                else
                {
                    var screenshot = Driver.TakeScreenshot();
                    screenshot.SaveAsFile(fullFilePath);

                }

                //if (testPlatform == TestPlatform.GridLocal || testPlatform == TestPlatform.Grid)
                //{
                //    if (reporter == Reporter.Klov)
                //    {
                //        klovPath = testPlatform == TestPlatform.Grid 
                //            ? @"\\10.1.1.207"
                //            : @"\\127.0.0.1";
                //        ImpersonateUser impersonateUser = new ImpersonateUser(Driver);
                //        impersonateUser.ScreenshotTool(ImpersonateUser.Task.SAVESCREENSHOT, $"{klovPath}\\errorscreenshots\\{uniqueFileName}");
                //    }
                //}
                //else
                //{
                //    var screenshot = Driver.TakeScreenshot();
                //    screenshot.SaveAsFile(fullFilePath);
                //}
            }
            catch (Exception e)
            {
                log.Debug($"Exception occured: {e.Message}");
            }

            return uniqueFileName;
        }

        public string SetGridAddress(TestPlatform platform, string gridIPv4Hostname = "")
        {
            string gridIPv4 = gridIPv4Hostname.Equals("")
                ? platform == TestPlatform.GridLocal
                    ? "127.0.0.1"
                    : "10.1.1.207"
                : gridIPv4Hostname;

            return gridIPv4;
        }

        //ExtentReports Loggers

        private static void LevelLogger(Level logLevel, string logDetails)
        {
            if (logLevel == Level.Debug)
            {
                log.Debug(logDetails);
            }
            else if (logLevel == Level.Warn)
            {
                log.Warn(logDetails);
            }
            else if (logLevel == Level.Error)
            {
                log.Error(logDetails);
            }
            else
            {
                log.Info(logDetails);
            }
        }

        private static void CheckForLineBreaksInLogMsg(Level logLevel, string logDetails, Exception e = null)
        {
            if (logDetails.Contains("<br>"))
            {
                string[] detailsBr = Regex.Split(logDetails, "<br>");
                for (int i = 0; i < detailsBr.Length; i++)
                {
                    string detail = detailsBr[i];
                    LevelLogger(logLevel, detail);
                }
            }
            else
            {
                LevelLogger(logLevel, logDetails);
            }

            if (e != null)
            {
                testInstance.Error(CreateReportMarkupCodeBlock(e));
                LevelLogger(Level.Error, e.StackTrace);
            }
        }

        public void LogAssertIgnore(string msg)
        {
            testInstance.Skip(CreateReportMarkupLabel(msg, ExtentColor.Orange));
            CheckForLineBreaksInLogMsg(Level.Debug, msg);
        }

        public void LogFail(string details, Exception e = null)
        {
            testInstance.Fail(CreateReportMarkupLabel(details, ExtentColor.Red));
            CheckForLineBreaksInLogMsg(Level.Error, details, e);
        }

        public void LogError(string details, bool takeScreenshot = true, Exception e = null)
        {
            if (takeScreenshot)
            {
                LogErrorWithScreenshot(details, ExtentColor.Red, e);
            }
            else
            {
                testInstance.Error(CreateReportMarkupLabel(details, ExtentColor.Red));
                CheckForLineBreaksInLogMsg(Level.Error, details, e);
            }
        }

        public void LogDebug(string details, Exception exception = null)
        {
            if (details.Contains(">>>") || details.Contains("Unable"))
            {
                testInstance.Debug(CreateReportMarkupLabel(details, ExtentColor.Orange));
            }
            else if (details.Contains("--->"))
            {
                testInstance.Debug(CreateReportMarkupLabel(details, ExtentColor.Indigo));
            }
            else
                testInstance.Debug(CreateReportMarkupLabel(details, ExtentColor.Grey));

            CheckForLineBreaksInLogMsg(Level.Debug, details, exception);
        }

        public void LogErrorWithScreenshot(string details = "", ExtentColor color = ExtentColor.Red, Exception e = null)
        {
            string localScreenshotPath = @"C:\Automation\klov\errorscreenshots\";
            string screenshotName = CaptureScreenshot(GetTestName());
            var screenshotRefPath = reporter == Reporter.Klov
                ? testPlatform == TestPlatform.GridLocal
                    ? $"http://127.0.0.1/errorscreenshots/{screenshotName}"
                    : $"http://10.1.1.207/errorscreenshots/{screenshotName}"
                : $"{localScreenshotPath}{screenshotName}";
            var detailsWithScreenshot = $"Error Screenshot: {details}<br> <img data-featherlight=\"{screenshotRefPath}\" class=\"step-img\" src=\"{screenshotRefPath}\" data-src=\"{screenshotRefPath}\" width=\"200\">";

            //testInstance = color.Equals(ExtentColor.Red)
            //    ? testInstance.Error(CreateReportMarkupLabel(detailsWithScreenshot, color))
            //    : testInstance.Warning(CreateReportMarkupLabel(detailsWithScreenshot, color));

            testInstance = reporter == Reporter.Klov
                ? color.Equals(ExtentColor.Red)
                    ? testInstance.Error(CreateReportMarkupLabel(detailsWithScreenshot, color))
                    : testInstance.Warning(CreateReportMarkupLabel(detailsWithScreenshot, color))
                : color.Equals(ExtentColor.Red)
                    ? testInstance.Error($"Test Failed: <br> {details}", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotRefPath, screenshotName).Build())
                    : testInstance.Warning($"Test Failed: <br> {details}", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotRefPath, screenshotName).Build());

            CheckForLineBreaksInLogMsg(Level.Error, details, e);
        }

        public static void LogInfo(string details)
        {
            if (details.Contains("<br>"))
            {
                testInstance.Info(CreateReportMarkupLabel(details, ExtentColor.Orange));
            }
            else if (details.Contains("#####"))
            {
                testInstance.Info(CreateReportMarkupLabel(details));
            }
            else if (details.Contains(">>>"))
            {
                testInstance.Info(CreateReportMarkupLabel(details, ExtentColor.Lime));
            }
            else if (details.Contains("Found"))
            {
                testInstance.Info(CreateReportMarkupLabel(details, ExtentColor.Green));
            }
            else
            {
                testInstance.Info(details);
            }

            CheckForLineBreaksInLogMsg(Level.Info, details);
        }

        public void LogInfo(string[][] detailsList, bool assertion)
        {
            IMarkup markupTable = MarkupHelper.CreateTable(detailsList);
            testInstance = assertion
                ? testInstance.Pass(markupTable)
                : testInstance.Fail(markupTable);
        }

        public void LogStep(string testStep)
        {
            string logMsg = $"TestStep: {testStep}";
            testInstance.Info(CreateReportMarkupLabel(logMsg, ExtentColor.Brown));
            CheckForLineBreaksInLogMsg(Level.Info, logMsg);
        }

        public void LogInfo<T>(string details, T assertion, Exception e = null)
        {
            object resultObj = null;
            int resultGauge = 0;
            Type assertionType = assertion.GetType();
            BaseUtils baseUtils = new BaseUtils();

            if (assertionType == typeof(bool))
            {
                resultObj = baseUtils.ConvertToType<bool>(assertion);
                resultGauge = (bool)resultObj
                    ? resultGauge + 1
                    : resultGauge - 1;
            }
            else if (assertionType == typeof(bool[]))
            {
                resultObj = baseUtils.ConvertToType<bool[]>(assertion);
                resultObj = new bool[] { };

                foreach (bool obj in (bool[])resultObj)
                {
                    resultGauge = obj
                        ? resultGauge + 1
                        : resultGauge - 1;
                }
            }

            if (resultGauge >= 1)
            {
                testInstance.Pass(CreateReportMarkupLabel(details, ExtentColor.Green));
                CheckForLineBreaksInLogMsg(Level.Info, details);
            }
            else if (resultGauge <= -1)
            {
                LogErrorWithScreenshot(details);
            }
            else if (resultGauge == 0)
            {
                LogErrorWithScreenshot(details, ExtentColor.Orange);
            }

            if (e != null)
            {
                log.Error(e.StackTrace);
            }
        }

        public enum ValidationType
        {
            Value,
            Selection
        }

        //TODO: Generic Result Calculator and Logger
        public void GetResults<T>(Enum element, ValidationType validationType, T expected, T actual)
        {
            string expectedHeader = string.Empty;
            string actualHeader = string.Empty;

            switch (validationType)
            {
                case ValidationType.Value:
                    expectedHeader = "Expected Value";
                    actualHeader = "Actual Value";
                    break;

                case ValidationType.Selection:
                    expectedHeader = "(Expected) Should Be Selected";
                    actualHeader = "(Actual) Is Selected";
                    break;
            }

            bool isResultExpected = actual.Equals(expected)
                ? true
                : false;

            string[] resultLogMsg = isResultExpected
                ? new string[]
                {
                    "meets",
                    " and"
                }
                : new string[]
                {
                    "does not meet",
                    ", but"
                };

            Type argType = expected.GetType();
            BaseUtils baseUtils = new BaseUtils();
            string Should = string.Empty;
            string Is = string.Empty;

            if (argType == typeof(string))
            {
                baseUtils.ConvertToType<string>(expected);
                baseUtils.ConvertToType<string>(actual);

                Should = isResultExpected ? "" : "";
            }
            else if (argType == typeof(int))
            {
                baseUtils.ConvertToType<int>(expected);
                baseUtils.ConvertToType<int>(actual);
            }
            else if (argType == typeof(bool))
            {
                Should = baseUtils.ConvertToType<bool>(expected)
                    ? "Should be selected"
                    : "Should Not be selected";
                Is = baseUtils.ConvertToType<bool>(actual)
                    ? "Is selected"
                    : "Is Not selected";
            }

            string logMsg = $" [Result {resultLogMsg[0]} expectations] {Should}{resultLogMsg[1]} {Is}";
            LogInfo($"{expectedHeader}: {expected}<br>{actualHeader}: {actual}<br>{element.ToString()} {logMsg} ", isResultExpected);
        }

        private static IMarkup CreateReportMarkupLabel(string details, ExtentColor extentColor = ExtentColor.Blue)
            => MarkupHelper.CreateLabel(details, extentColor);

        private static IMarkup CreateReportMarkupCodeBlock(Exception e)
            => MarkupHelper.CreateCodeBlock($"Exception: {e.StackTrace}");

        //Helper methods to gather Test Context Details
        public static string GetTestName()
            => GetTestContextProperty(TestContextProperty.TestName);

        public static string GetTestComponent1()
            => GetTestContextProperty(TestContextProperty.TestComponent1);

        public static string GetTestComponent2()
            => GetTestContextProperty(TestContextProperty.TestComponent2);

        public static string GetTestDescription()
            => GetTestContextProperty(TestContextProperty.TestDescription);

        public static string GetTestPriority()
            => GetTestContextProperty(TestContextProperty.TestPriority);

        public static string GetTestCaseNumber()
            => GetTestContextProperty(TestContextProperty.TestCaseNumber);

        public static string GetTestClassName()
            => GetTestContextProperty(TestContextProperty.TestClass);

        private static string GetTestContextProperty(TestContextProperty testContextProperty)
        {
            TestContext.TestAdapter testInstance = TestContext.CurrentContext.Test;
            string context = string.Empty;

            switch (testContextProperty)
            {
                case TestContextProperty.TestName:
                    return testInstance.Name;

                case TestContextProperty.TestClass:
                    return (testInstance.FullName).Split('.')[2];

                case TestContextProperty.TestComponent1:
                    context = "Category";
                    break;

                case TestContextProperty.TestComponent2:
                    context = "Component2";
                    break;

                case TestContextProperty.TestDescription:
                    context = "Description";
                    break;

                case TestContextProperty.TestPriority:
                    context = "Priority";
                    break;

                case TestContextProperty.TestCaseNumber:
                    context = "TC#";
                    break;
            }

            var prop = testInstance.Properties.Get(context) ?? string.Empty;
            return prop.ToString();
        }

        private enum TestContextProperty
        {
            TestName,
            TestClass,
            TestComponent1,
            TestComponent2,
            TestDescription,
            TestPriority,
            TestCaseNumber
        }

        //Helper methods for working with files
        public static void RunExternalExecutible(string executible, string cmdLineArgument)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(executible)
            {
                Arguments = cmdLineArgument,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            Process.Start(startInfo);
        }

        /// <summary>
        /// Location to project Temp folder with Tenant name as filename
        /// -- Specify file type extention (i.e. - .xml)
        /// </summary>
        public static void WriteToFile(string msg, string fileExt = ".txt", bool overwriteExisting = false)
        {
            try
            {
                fullTempFileName = $"{baseTempFolder}\\{fileName}({dateString})";

                Directory.CreateDirectory(baseTempFolder);
                string path = $"{fullTempFileName}{fileExt}";

                if (overwriteExisting == true)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }

                StreamWriter streamWriter = File.Exists(path) ? File.AppendText(path) : File.CreateText(path);
                using (StreamWriter sw = streamWriter)
                {
                    if (!string.IsNullOrEmpty(msg) && msg.Contains("<br>"))
                    {
                        string[] message = Regex.Split(msg, "<br>");
                        sw.WriteLine(message[0]);
                        sw.WriteLine(message[1]);
                    }
                    else
                    {
                        sw.WriteLine(msg);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public OutType ConvertToType<OutType>(object objToConvert)
        {
            try
            {
                Type inputType = objToConvert.GetType();
                return (OutType)Convert.ChangeType(objToConvert, typeof(OutType));
            }
            catch (Exception e)
            {
                log.Error($"Error occured in ConvertToType method:\n{e.Message}");
                throw;
            }
        }
    }

    public static class BaseHelper
    {
        private static PageBaseHelper pgbHelper = new PageBaseHelper();

        public static string SplitCamelCase(this string str, bool removeUnderscore = true)
        {
            string value = (removeUnderscore == true) ? Regex.Replace(str, @"_", "") : str;
            return Regex.Replace(Regex.Replace(value, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        public static void AssignReportCategories(this ExtentTest testInstance, string[] category)
        {
            for (int i = 0; i < category.Length; i++)
            {
                testInstance
                    .AssignCategory(category[i]);
            }
        }

        /// <summary>
        /// Allows for test cases to continue running when an error, which is not related to the objective of the test case, occurs but impacts the overall result of the test case.
        /// Used in conjection with CheckForTestStatusInjection method, which is part of the TearDown attribute in the BaseClass.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="logMsg"></param>
        public static void InjectTestStatus(TestStatus status, string logMsg)
        {
            string testName = BaseUtils.GetTestName();
            string tcNumber = BaseUtils.GetTestCaseNumber();
            var prefix = $"{tcNumber}{testEnv}{tenantName}{testName}";
            pgbHelper.CreateVar($"{prefix}_msgKey", logMsg);
            pgbHelper.CreateVar($"{prefix}_statusKey", status.ToString());
        }

        /// <summary>
        /// Used in conjunction with InjectTestStatus method.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static List<object> CheckForTestStatusInjection(this TestContext.ResultAdapter result)
        {
            PageHelper pageHelper = new PageHelper();
            List<object> testResults = new List<object>();

            TestStatus _testStatus = TestStatus.Inconclusive;

            string testName = BaseUtils.GetTestName();
            string tcNumber = BaseUtils.GetTestCaseNumber();
            var prefix = $"{tcNumber}{testEnv}{tenantName}{testName}";
            var injStatusKey = $"{prefix}_statusKey";
            var injMsgKey = $"{prefix}_msgKey";

            string injStatus = string.Empty;
            string injMsg = string.Empty;

            if (pgbHelper.HashKeyExists(injStatusKey))
            {
                injStatus = pgbHelper.GetVar(injStatusKey);
                injMsg = pgbHelper.GetVar(injMsgKey);

                switch (injStatus)
                {
                    case "Warning":
                        _testStatus = TestStatus.Warning;
                        break;

                    case "Failed":
                        _testStatus = TestStatus.Failed;
                        break;

                    case "Skipped":
                        _testStatus = TestStatus.Skipped;
                        break;

                    default:
                        _testStatus = TestStatus.Inconclusive;
                        break;
                }
            }
            else
            {
                _testStatus = result.Outcome.Status;
            }

            testResults.Add(_testStatus);
            testResults.Add(injMsg);

            return testResults;
        }
    }
}