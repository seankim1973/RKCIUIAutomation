using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Configuration;
using System.IO;

namespace RKCIUIAutomation.Base
{
    public class BaseUtils : WebDriverFactory
    {
        public static string extentReportPath = null;
        public static string screenshotReferencePath = null;

        public void DetermineFilePath(string _testPlatform)
        {
            if (_testPlatform.Equals("Local"))
            {
                extentReportPath = $"{GetCodeBasePath()}\\Report";
                screenshotReferencePath = "errorscreenshots/";
            } else
            {
                extentReportPath = "C:\\inetpub\\wwwroot\\extentreport";
                screenshotReferencePath = "errorscreenshots\\";
            }
        }

        public static string GetCodeBasePath()
        {
            Directory.SetCurrentDirectory(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString());
            var currentDir = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            return Directory.GetParent(currentDir).ToString();
        }

        public string CaptureScreenshot(string fileName)
        {
            string uniqueFileName = $"{fileName}{DateTime.Now.Second}.png";
            var screenshotSavePath = $"{extentReportPath}\\errorscreenshots\\{uniqueFileName}";
            var screenshot = Driver.TakeScreenshot();
            screenshot.SaveAsFile(screenshotSavePath, ScreenshotImageFormat.Png);
            return $"{screenshotReferencePath}{uniqueFileName}";
        }

    }
}
