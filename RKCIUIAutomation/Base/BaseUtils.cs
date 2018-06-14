using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RKCIUIAutomation.Base
{
    public class BaseUtils : WebDriverFactory
    {
        public static string extentReportPath = $"{GetCodeBasePath()}\\Report";
        public static string screenshotReferencePath = null;
        internal static readonly string methodName = TestContext.CurrentContext.Test.MethodName;
        internal static readonly ILog log = LogManager.GetLogger("");

        public void DetermineFilePath(string _testPlatform)
        {
            if (_testPlatform.Equals("Local"))
            {
                screenshotReferencePath = "errorscreenshots/";
            } else
            {
                //extentReportPath = "C:\\inetpub\\wwwroot\\extentreport";
                screenshotReferencePath = "errorscreenshots\\";
            }
        }

        public string[] GetTestContext(string fullTestName)
        {
            string[] testNameArray = fullTestName.Split('.');
            return testNameArray;
        }

        public static string GetCodeBasePath()
        {
            Directory.SetCurrentDirectory(Directory.GetParent(TestContext.CurrentContext.TestDirectory).ToString());
            string baseDir = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            return baseDir;
        }

        public static string CaptureScreenshot(string fileName)
        {
            string uniqueFileName = $"{fileName}{DateTime.Now.Second}.png";
            var screenshot = Driver.TakeScreenshot();
            screenshot.SaveAsFile($"{extentReportPath}\\errorscreenshots\\{uniqueFileName}", ScreenshotImageFormat.Png);
            return $"{screenshotReferencePath}{uniqueFileName}";
        }


        //ExtentReports Helpers
        public static void LogSkipped(string msg)
        {
            ExtentTestManager.GetTest().Log(Status.Skip, CreateReportMarkupLabel(msg, ExtentColor.Orange));
            log.Warn(msg);
            Assert.Warn(msg);
        }

        public static void LogError(string details)
        {
            ExtentTestManager.GetTest().Error(CreateReportMarkupLabel(details, ExtentColor.Red));
        }

        public static void LogInfo(string details)
        {
            if (details.Contains("#####"))
            {
                ExtentTestManager.GetTest().Info(CreateReportMarkupLabel(details));
            }
            else
                ExtentTestManager.GetTest().Info(details);
            log.Info(details);    
        }
        public static void LogErrorWithScreenshot()
        {
            string screenshotPath = CaptureScreenshot(methodName);
            ExtentTestManager.GetTest().Error($"Error Screenshot:", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
        }

        public static void LogInfo(string details, Exception e)
        {
            ExtentTestManager.GetTest().Debug(CreateReportMarkupLabel(details, ExtentColor.Orange));           
            if(e != null)
            {
                ExtentTestManager.GetTest().Debug(CreateReportMarkupLabel(e.Message, ExtentColor.Grey));
                log.Debug(e.Message);
            }
        }
        public static void LogInfo(string details, bool assertion, Exception e = null)
        {
            if (assertion)
            {
                ExtentTestManager.GetTest().Pass(CreateReportMarkupLabel(details, ExtentColor.Green));

                if (details.Contains("<br>"))
                {
                    string[] result = Regex.Split(details, "<br>&nbsp;&nbsp;");
                    log.Info(result[0]);
                    log.Info(result[1]);
                }
                else
                    log.Info(details);
            }
            else
            {
                ExtentTestManager.GetTest().Fail(CreateReportMarkupLabel(details, ExtentColor.Red));
                LogErrorWithScreenshot();

                if (e != null)
                {
                    log.Fatal(e.Message);
                }
            }
        }

        private static IMarkup CreateReportMarkupLabel(string details, ExtentColor extentColor = ExtentColor.Blue)
        {
            return MarkupHelper.CreateLabel(details, extentColor);
        }
        private static IMarkup CreateReportMarkupCodeBlock(Exception e)
        {
            return MarkupHelper.CreateCodeBlock($"Exception: {e.Message}");
        }
    }
}