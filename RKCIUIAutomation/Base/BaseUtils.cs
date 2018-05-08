using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Configuration;
using System.IO;

namespace RKCIUIAutomation.Base
{
    public class BaseUtils : WebDriverFactory
    {
        public static string iisExtentPath = "C:\\inetpub\\wwwroot\\extentreport";
        //public static string reportFilePath = $"{GetCodeBasePath()}\\Report";
        public static string screenshotFullPath = $"{iisExtentPath}\\errorscreenshots\\";
        public static string GetCodeBasePath()
        {
            Directory.SetCurrentDirectory(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString());
            var currentDir = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            return Directory.GetParent(currentDir).ToString();
        }

        public string CaptureScreenshot(string fileName)
        {
            string uniqueFileName = $"{fileName}{DateTime.Now.Second}.png";
            var screenshotSavePath = $"{screenshotFullPath}{uniqueFileName}";
            var screenshot = Driver.TakeScreenshot();
            screenshot.SaveAsFile(screenshotSavePath, ScreenshotImageFormat.Png);
            return $"/extentreport/errorscreenshots/{uniqueFileName}";
        }

    }
}
