﻿using AventStack.ExtentReports;
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
        public ExtentTest reportTestInstance = ExtentTestManager.GetTest();
        public IMarkup reportMarkup;

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

        public static string GetCodeBasePath()
        {
            Directory.SetCurrentDirectory(Directory.GetParent(TestContext.CurrentContext.TestDirectory).ToString());
            string baseDir = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            return baseDir;
            //Directory.SetCurrentDirectory(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString());
            //var currentDir = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            //return Directory.GetParent(currentDir).ToString();
        }

        public string CaptureScreenshot(string fileName)
        {
            string uniqueFileName = $"{fileName}{DateTime.Now.Second}.png";
            var screenshot = Driver.TakeScreenshot();
            screenshot.SaveAsFile($"{extentReportPath}\\errorscreenshots\\{uniqueFileName}", ScreenshotImageFormat.Png);
            return $"{screenshotReferencePath}{uniqueFileName}";
        }


        //ExtentReports Helpers
        public void LogInfo(string info)
        {
            reportTestInstance.Info(info);
            //reportTestInstance.Log(Status.Info, CreateReportMarkupLabel(info));
            Console.Out.WriteLine(info);
        }

        public void LogInfo(string info, Exception e)
        {
            reportTestInstance.Log(Status.Fatal, CreateReportMarkupLabel(info, ExtentColor.Red));
            reportTestInstance.Log(Status.Fatal, CreateReportMarkupCodeBlock(e));
            Console.Out.WriteLine(e.Message);
        }

        private IMarkup CreateReportMarkupLabel(string info, ExtentColor extentColor = ExtentColor.Blue)
        {
            return MarkupHelper.CreateLabel($"Info : {info}", extentColor);
        }

        private IMarkup CreateReportMarkupCodeBlock(Exception e)
        {
            return MarkupHelper.CreateCodeBlock($"Exception : \n{e.Message}");
        }
    }
}
