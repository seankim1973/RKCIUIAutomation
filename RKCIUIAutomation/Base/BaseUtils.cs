

using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace RKCIUIAutomation.Base
{
    public class BaseUtils : WebDriverFactory
    {        
        public static string GetCodeBasePath()
        {
            Directory.SetCurrentDirectory(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString());
            return Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
        }

        public string CaptureScreenshot(string fileName)
        {
            string uniqueFileName = fileName + DateTime.Now.Ticks;
            var filePath = Directory.GetParent(GetCodeBasePath()) + "\\Report\\ErrorScreenshots\\" + uniqueFileName + ".png";
            var screenshot = Driver.TakeScreenshot();
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
            //Console.Out.WriteLine("#### BaseUtils Path : " + filePath);
            return filePath;
        }

    }
}
