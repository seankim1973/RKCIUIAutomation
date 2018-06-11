using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.IO;

namespace RKCIUIAutomation.Base
{
    public class BaseUtils : WebDriverFactory
    {
        public static string extentReportPath = $"{GetCodeBasePath()}\\Report";
        public static string screenshotReferencePath = null;

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

        public string CaptureScreenshot(string fileName)
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
            Console.WriteLine(msg);
            Assert.Warn(msg);
        }
        public static void LogInfo(string details)
        {
            ExtentTestManager.GetTest().Info(details);
            if (details.Contains("<br>"))
            {
                var value = details.Split('<');
                Console.WriteLine($" {value[0]}");
                value = details.Split('>');
                Console.WriteLine(value[1]);
            }
            else
                Console.WriteLine(details);
        }
        public static void LogInfo(string details, Exception e)
        {
            ExtentTestManager.GetTest().Log(Status.Info, CreateReportMarkupLabel(details, ExtentColor.Orange));
            ExtentTestManager.GetTest().Log(Status.Info, CreateReportMarkupCodeBlock(e));
            Console.WriteLine(e.Message);
        }
        public static void LogInfo(string details, bool assertion, Exception e = null)
        {
            if (assertion)
            {
                ExtentTestManager.GetTest().Log(Status.Pass, CreateReportMarkupLabel(details, ExtentColor.Green));
                Console.WriteLine(details);
            }
            else
            {
                ExtentTestManager.GetTest().Log(Status.Fail, CreateReportMarkupLabel(details, ExtentColor.Red));
                Console.WriteLine(e.Message);
            }     
        }

        private static IMarkup CreateReportMarkupLabel(string details, ExtentColor extentColor = ExtentColor.Blue)
        {
            return MarkupHelper.CreateLabel($"Info : {details}", extentColor);
        }
        private static IMarkup CreateReportMarkupCodeBlock(Exception e)
        {
            return MarkupHelper.CreateCodeBlock($"Exception: {e.Message}");
        }
    }
}