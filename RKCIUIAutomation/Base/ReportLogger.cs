using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using log4net;
using log4net.Core;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Base
{
    public class ReportLogger : BaseUtils, IReportLogger
    {
        public ReportLogger()
        {
        }

        public ReportLogger(IWebDriver driver) => this.Driver = driver;

        //[ThreadStatic]
        //private static Cookie cookie;

        public enum ValidationType
        {
            Value,
            Selection
        }

        private void CreateStdOutLog(Level logLevel, string logDetails)
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

        private void CheckForLineBreaksInLogMsgForStdOutLogger(Level logLevel, string logDetails, Exception e = null)
        {
            if (logDetails.Contains("<br>"))
            {
                string[] detailsBr = Regex.Split(logDetails, "<br>");
                for (int i = 0; i < detailsBr.Length; i++)
                {
                    string detail = detailsBr[i];
                    CreateStdOutLog(logLevel, detail);
                }
            }
            else
            {
                CreateStdOutLog(logLevel, logDetails);
            }

            if (e != null)
            {
                testInstance.Error(CreateReportMarkupCodeBlock(e));
                CreateStdOutLog(Level.Error, e.StackTrace);
            }
        }

        private IMarkup CreateReportMarkupLabel(string details, ExtentColor extentColor = ExtentColor.Blue)
            => MarkupHelper.CreateLabel(details, extentColor);

        private IMarkup CreateReportMarkupCodeBlock(Exception e)
            => MarkupHelper.CreateCodeBlock($"Exception: {e.Message}");

        public void AssertIgnore(string msg)
        {
            testInstance.Skip(CreateReportMarkupLabel(msg, ExtentColor.Orange));
            CheckForLineBreaksInLogMsgForStdOutLogger(Level.Debug, msg);
        }

        public void Fail(string details, Exception e = null)
        {
            testInstance.Fail(CreateReportMarkupLabel(details, ExtentColor.Red));
            CheckForLineBreaksInLogMsgForStdOutLogger(Level.Error, details, e);
        }

        public void Debug(string details, Exception exception = null)
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

            CheckForLineBreaksInLogMsgForStdOutLogger(Level.Debug, details, exception);
        }

        public void Error(string details, bool takeScreenshot = true, Exception e = null)
        {
            if (takeScreenshot)
            {
                ErrorWithScreenshot(details, ExtentColor.Red, e);
            }
            else
            {
                testInstance.Error(CreateReportMarkupLabel(details, ExtentColor.Red));
                CheckForLineBreaksInLogMsgForStdOutLogger(Level.Error, details, e);
            }
        }

        public void ErrorWithScreenshot(string details = "", ExtentColor color = ExtentColor.Red, Exception e = null)
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

            CheckForLineBreaksInLogMsgForStdOutLogger(Level.Error, details, e);
        }

        public void Info(string details)
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

            CheckForLineBreaksInLogMsgForStdOutLogger(Level.Info, details);
        }

        public void Info(string[][] detailsList, bool assertion)
        {
            IMarkup markupTable = MarkupHelper.CreateTable(detailsList);
            testInstance = assertion
                ? testInstance.Pass(markupTable)
                : testInstance.Fail(markupTable);
        }

        public void Info<T>(string details, T assertion, Exception e = null)
        {
            object resultObj = null;
            int resultGauge = 0;
            Type assertionType = assertion.GetType();

            if (assertionType == typeof(bool))
            {
                resultObj = ConvertToType<bool>(assertion);
                resultGauge = (bool)resultObj
                    ? resultGauge + 1
                    : resultGauge - 1;
            }
            else if (assertionType == typeof(bool[]))
            {
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
                CheckForLineBreaksInLogMsgForStdOutLogger(Level.Info, details);
            }
            else if (resultGauge <= -1)
            {
                ErrorWithScreenshot(details);
            }
            else if (resultGauge == 0)
            {
                ErrorWithScreenshot(details, ExtentColor.Orange);
            }

            if (e != null)
            {
                log.Error(e.StackTrace);
            }
        }

        public void Step(string testStep, bool createStdOutLog = false, bool testResult = true)
        {
            try
            {
                string logMsg = $"TestStep: {testStep}";

                ExtentColor logLabelColor = testResult
                    ? ExtentColor.Grey
                    : ExtentColor.Red;

                testInstance.Info(CreateReportMarkupLabel(logMsg, logLabelColor));
                CheckForLineBreaksInLogMsgForStdOutLogger(Level.Info, logMsg);
                cookie = new Cookie("zaleniumMessage", testStep);
                driver.Manage().Cookies.AddCookie(cookie);

                if (createStdOutLog)
                {
                    if (testResult)
                    {
                        Info(testStep);
                    }
                    else
                    {
                        Info(testStep, testResult);
                    }
                }
                else
                {
                    if (testResult.Equals(false))
                    {
                        Info(testStep, testResult);
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
            string Should = string.Empty;
            string Is = string.Empty;

            if (argType == typeof(string))
            {
                ConvertToType<string>(expected);
                ConvertToType<string>(actual);

                Should = isResultExpected ? "" : "";
            }
            else if (argType == typeof(int))
            {
                ConvertToType<int>(expected);
                ConvertToType<int>(actual);
            }
            else if (argType == typeof(bool))
            {
                Should = ConvertToType<bool>(expected)
                    ? "Should be selected"
                    : "Should Not be selected";

                Is = ConvertToType<bool>(actual)
                    ? "Is selected"
                    : "Is Not selected";
            }

            string logMsg = $" [Result {resultLogMsg[0]} expectations] {Should}{resultLogMsg[1]} {Is}";
            Info($"{expectedHeader}: {expected}<br>{actualHeader}: {actual}<br>{element.ToString()} {logMsg} ", isResultExpected);
        }

    }
}
