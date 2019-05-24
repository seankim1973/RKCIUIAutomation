using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using log4net;
using log4net.Core;
using MiniGuids;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Tools;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.StaticHelpers;

namespace RKCIUIAutomation.Base
{
    public class BaseUtils : BaseClass, IBaseUtils
    {
        public string CurrentTenantName { get; private set; }
        public static string DateString { get; private set; }
        public string CodeBasePath { get; private set; }
        public static string BaseTempFolder { get; private set; }
        public string ExtentReportPath { get; private set; }
        public string ScreenshotSavePath { get; private set; }

        public BaseUtils()
        {
        }

        public BaseUtils(TenantName tenantName)
            => DetermineReportFilePath(tenantName);

        public BaseUtils(TestPlatform testPlatform, string gridAddress)
            => ConfigGridAddress(testPlatform, gridAddress);

        public BaseUtils(IWebDriver driver) => this.Driver = driver;

        public void SetCodeBasePath()
        {
            if (!CodeBasePath.HasValue())
            {
                Directory.SetCurrentDirectory(Directory.GetParent(TestContext.CurrentContext.TestDirectory).ToString());
                CodeBasePath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            }
        }

        public void SetExtentReportPath()
        {
            if (!ExtentReportPath.HasValue())
            {
                ExtentReportPath = $"{CodeBasePath}\\Report";
            }
        }

        //private string SetWinTempFolder()
        //{
        //    string cTemp = "C:\\Temp";
        //    if (!File.Exists(cTemp))
        //    {
        //        Directory.CreateDirectory(cTemp);
        //    }

        //    return cTemp;
        //}

        public string GetDateString()
        {
            if (!DateString.HasValue())
            {
                string[] shortDate = (DateTime.Today.ToShortDateString()).Split('/');
                string month = shortDate[0];
                string date = shortDate[1];

                month = (month.Length > 1) ? month : $"0{month}";
                date = (date.Length > 1) ? date : $"0{date}";
                DateString = $"{month}{date}{shortDate[2]}";
            }

            return DateString;
        }

        public void SetScreenshotSavePath()
        {
            if (!ScreenshotSavePath.HasValue())
            {
                ScreenshotSavePath = $"{ExtentReportPath}\\errorscreenshots\\";
            }
        }

        public string GetBaseTempFolder()
        {
            if (!BaseTempFolder.HasValue())
            {
                BaseTempFolder = $"{CodeBasePath}\\Temp";
            }

            return BaseTempFolder;
        }

        public string GetTenantName()
        {
            if (!CurrentTenantName.HasValue())
            {
                CurrentTenantName = tenantName.ToString();
            }
            return CurrentTenantName;
        }

        public void DetermineReportFilePath(TenantName tenantName)
        {
            SetCodeBasePath();
            SetExtentReportPath();
            SetScreenshotSavePath();
        }

        public string CaptureScreenshot(string fileName = "")
        {
            string uniqueFileName = string.Empty;
            string fullFilePath = string.Empty;
            string klovPath = string.Empty;

            try
            {
                Directory.CreateDirectory(ScreenshotSavePath);
                uniqueFileName = $"{(fileName.HasValue() ? fileName : GetTestName())}{DateTime.Now.Second}_{GetTenantName()}.png";
                fullFilePath = $"{ScreenshotSavePath}{uniqueFileName}";

                if (reporter == Reporter.Klov)
                {
                    if (testPlatform == TestPlatform.Grid)
                    {
                        klovPath = @"\\10.1.1.207\errorscreenshots\";

                        ImpersonateUser impersonateUser = new ImpersonateUser(driver);
                        impersonateUser.ScreenshotTool(ImpersonateUser.Task.SAVESCREENSHOT, $"{klovPath}{uniqueFileName}");
                    }
                    else if (testPlatform == TestPlatform.GridLocal)
                    {
                        klovPath = @"C:\Automation\klov\errorscreenshots\";
                        var screenshot = driver.TakeScreenshot();
                        screenshot.SaveAsFile($"{klovPath}{uniqueFileName}");
                    }
                }
                else
                {
                    var screenshot = driver.TakeScreenshot();
                    screenshot.SaveAsFile(fullFilePath);

                }
            }
            catch (Exception e)
            {
                log.Debug($"Exception occured: {e.Message}");
            }

            return uniqueFileName;
        }

        public void ConfigGridAddress(TestPlatform platform, string gridIPv4Hostname = "")
        {
            GridVmIP = gridIPv4Hostname.Equals("")
                ? platform == TestPlatform.GridLocal || platform == TestPlatform.Local
                    ? "127.0.0.1"
                    : "10.1.1.207"
                : gridIPv4Hostname;
        }

        //ExtentReports Loggers

        private void LevelLogger(Level logLevel, string logDetails)
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

        private void CheckForLineBreaksInLogMsg(Level logLevel, string logDetails, Exception e = null)
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
            string screenshotName = CaptureScreenshot();
            var screenshotRefPath = reporter == Reporter.Klov
                ? testPlatform == TestPlatform.GridLocal
                    ? $"http://127.0.0.1/errorscreenshots/{screenshotName}"
                    : $"http://10.1.1.207/errorscreenshots/{screenshotName}"
                : $"{localScreenshotPath}{screenshotName}";
            var detailsWithScreenshot = $"Error Screenshot: {details}<br> <img data-featherlight=\"{screenshotRefPath}\" class=\"step-img\" src=\"{screenshotRefPath}\" data-src=\"{screenshotRefPath}\" width=\"200\">";

            testInstance = reporter == Reporter.Klov
                ? color.Equals(ExtentColor.Red)
                    ? testInstance.Error(CreateReportMarkupLabel(detailsWithScreenshot, color))
                    : testInstance.Warning(CreateReportMarkupLabel(detailsWithScreenshot, color))
                : color.Equals(ExtentColor.Red)
                    ? testInstance.Error($"Test Failed: <br> {details}", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotRefPath, screenshotName).Build())
                    : testInstance.Warning($"Test Failed: <br> {details}", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotRefPath, screenshotName).Build());

            CheckForLineBreaksInLogMsg(Level.Error, details, e);
        }

        public void LogInfo(string details)
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
            else if (details.Contains("skipped"))
            {
                testInstance.Info(CreateReportMarkupLabel(details, ExtentColor.Yellow));
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


        [ThreadStatic]
        private static Cookie cookie;

        //public List<string> TenantComponents { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void LogStep(string testStep, bool logInfo = false, bool testResult = true)
        {
            try
            {
                string logMsg = $"TestStep: {testStep}";

                ExtentColor logLabelColor = testResult
                    ? ExtentColor.Grey
                    : ExtentColor.Red;

                testInstance.Info(CreateReportMarkupLabel(logMsg, logLabelColor));
                CheckForLineBreaksInLogMsg(Level.Info, logMsg);
                cookie = new Cookie("zaleniumMessage", testStep);
                driver.Manage().Cookies.AddCookie(cookie);

                if (logInfo)
                {                   
                    if (testResult)
                    {
                        LogInfo(testStep);
                    }
                    else
                    {
                        LogInfo(testStep, testResult);
                    }
                }
                else
                {
                    if (testResult.Equals(false))
                    {
                        LogInfo(testStep, testResult);
                    }
                }
            }
            catch (UnableToSetCookieException)
            {
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
        }

        public void LogInfo<T>(string details, T assertion, Exception e = null)
        {
            object resultObj = null;
            int resultGauge = 0;
            Type assertionType = assertion.GetType();
            //BaseUtils baseUtils = new BaseUtils();

            if (assertionType == typeof(bool))
            {
                //resultObj = baseUtils.ConvertToType<bool>(assertion);
                resultObj = ConvertToType<bool>(assertion);
                resultGauge = (bool)resultObj
                    ? resultGauge + 1
                    : resultGauge - 1;
            }
            else if (assertionType == typeof(bool[]))
            {
                //resultObj = baseUtils.ConvertToType<bool[]>(assertion);
                resultObj = ConvertToType<bool[]>(assertion);
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
            //BaseUtils baseUtils = new BaseUtils();
            string Should = string.Empty;
            string Is = string.Empty;

            if (argType == typeof(string))
            {
                //baseUtils.ConvertToType<string>(expected);
                //baseUtils.ConvertToType<string>(actual);
                ConvertToType<string>(expected);
                ConvertToType<string>(actual);

                Should = isResultExpected ? "" : "";
            }
            else if (argType == typeof(int))
            {
                //baseUtils.ConvertToType<int>(expected);
                //baseUtils.ConvertToType<int>(actual);
                ConvertToType<int>(expected);
                ConvertToType<int>(actual);
            }
            else if (argType == typeof(bool))
            {
                //Should = baseUtils.ConvertToType<bool>(expected)
                Should = ConvertToType<bool>(expected)
                    ? "Should be selected"
                    : "Should Not be selected";
                //Is = baseUtils.ConvertToType<bool>(actual)
                Is = ConvertToType<bool>(actual)
                    ? "Is selected"
                    : "Is Not selected";
            }

            string logMsg = $" [Result {resultLogMsg[0]} expectations] {Should}{resultLogMsg[1]} {Is}";
            LogInfo($"{expectedHeader}: {expected}<br>{actualHeader}: {actual}<br>{element.ToString()} {logMsg} ", isResultExpected);
        }

        private IMarkup CreateReportMarkupLabel(string details, ExtentColor extentColor = ExtentColor.Blue)
            => MarkupHelper.CreateLabel(details, extentColor);

        private IMarkup CreateReportMarkupCodeBlock(Exception e)
            => MarkupHelper.CreateCodeBlock($"Exception: {e.StackTrace}");

        //Helper methods to gather Test Context Details
        public string GetTestName()
            => GetTestContextProperty(TestContextProperty.TestName);

        public string GetTestComponent1()
            => GetTestContextProperty(TestContextProperty.TestComponent1);

        public string GetTestComponent2()
            => GetTestContextProperty(TestContextProperty.TestComponent2);

        public string GetTestDescription()
            => GetTestContextProperty(TestContextProperty.TestDescription);

        public string GetTestPriority()
            => GetTestContextProperty(TestContextProperty.TestPriority);

        public string GetTestCaseNumber()
            => GetTestContextProperty(TestContextProperty.TestCaseNumber);

        public string GetTestClassName()
            => GetTestContextProperty(TestContextProperty.TestClass);

        private string GetTestContextProperty(TestContextProperty testContextProperty)
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
                string fileName = BaseUtility().GetTenantName();
                string dateString = BaseUtility().GetDateString();
                string tmpFolder = BaseUtility().GetBaseTempFolder();
                string fullFilePath = $"{tmpFolder}\\{fileName}({dateString})";

                Directory.CreateDirectory(tmpFolder);
                string path = $"{fullFilePath}{fileExt}";

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
                    if (msg.HasValue())
                    {
                        if (msg.Contains("<br>"))
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
            }
            catch (Exception e)
            {
                log.Error(e.Message);
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
                log.Error($"Error occurred in ConvertToType method:\n{e.Message}");
                throw;
            }
        }


        [ThreadStatic]
        internal static Hashtable Hashtable;

        internal Hashtable GetHashTable() => Hashtable ?? new Hashtable();

        public string GenerateRandomGuid()
        {
            MiniGuid guid = MiniGuid.NewGuid();
            return guid;
        }

        public void CreateVar<T>(T key, string value = "", bool withPrefix = true)
        {
            try
            {
                string logMsg = string.Empty;
                string argKey = BaseUtility().ConvertToType<string>(key);
                argKey = withPrefix
                    ? GetEnvVarPrefix(argKey)
                    : argKey;

                value = value.HasValue()
                    ? value
                    : GenerateRandomGuid();

                Hashtable = GetHashTable();

                if (!HashKeyExists(argKey))
                {
                    Hashtable.Add(argKey, value);
                    logMsg = "Created";
                }
                else
                {
                    Hashtable[argKey] = value;
                    logMsg = "Updated";
                }

                log.Debug($"{logMsg} HashTable - Key: {argKey} : Value: {value}");
            }
            catch (Exception e)
            {
                log.Error($"Error occured while adding to HashTable \n{e.Message}");
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetVar<T>(T key, bool keyIncludesPrefix = false)
        {
            string argKey = ConvertToType<string>(key);
            argKey = keyIncludesPrefix
                ? argKey
                : GetEnvVarPrefix(argKey);

            if (!HashKeyExists(argKey))
            {
                CreateVar(argKey, "", false);
            }

            Hashtable = GetHashTable();
            var varValue = Hashtable[argKey].ToString();
            log.Debug($"#####GetVar Key: {argKey} has Value: {varValue}");

            return varValue;
        }

        public bool HashKeyExists(string key)
        {
            Hashtable = GetHashTable();
            return Hashtable.ContainsKey(key);
        }


    }
}