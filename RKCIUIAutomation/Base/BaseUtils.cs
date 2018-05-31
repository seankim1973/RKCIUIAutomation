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
            Console.Out.WriteLine(msg);
        }
        public static void LogInfo(string details)
        {
            ExtentTestManager.GetTest().Info(details);
            Console.Out.WriteLine(details);
        }

        public static void LogInfo(string details, Exception e)
        {
            ExtentTestManager.GetTest().Log(Status.Fatal, CreateReportMarkupLabel(details, ExtentColor.Red));
            ExtentTestManager.GetTest().Log(Status.Fatal, CreateReportMarkupCodeBlock(e));
            Console.Out.WriteLine(e.Message);
        }

        public static void LogInfo(string details, bool assertion, Exception e = null)
        {
            if (assertion)
            {
                ExtentTestManager.GetTest().Log(Status.Pass, CreateReportMarkupLabel(details, ExtentColor.Green));
                Console.Out.WriteLine(details);
            }
            else
            {
                ExtentTestManager.GetTest().Log(Status.Fail, CreateReportMarkupLabel(details, ExtentColor.Red));
                Console.Out.WriteLine(e.Message);
            }     
        }

        private static IMarkup CreateReportMarkupLabel(string details, ExtentColor extentColor = ExtentColor.Blue)
        {
            return MarkupHelper.CreateLabel($"Info : {details}", extentColor);
        }

        private static IMarkup CreateReportMarkupCodeBlock(Exception e)
        {
            return MarkupHelper.CreateCodeBlock($"Exception : \n{e.Message}");
        }
    }
}