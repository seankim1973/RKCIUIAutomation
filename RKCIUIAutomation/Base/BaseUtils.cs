using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using RKCIUIAutomation.Page;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public static void LogAssertIgnore(string msg)
        {
            ExtentTestManager.GetTest().Debug(CreateReportMarkupLabel(msg, ExtentColor.Orange));
            Assert.Ignore(msg);
        }

        public static void LogError(string details)
        {
            ExtentTestManager.GetTest().Error(CreateReportMarkupLabel(details, ExtentColor.Red));
            log.Error(details);
        }

        public static void LogDebug(string details)
        {
            ExtentTestManager.GetTest().Debug(CreateReportMarkupLabel(details, ExtentColor.Grey));
            log.Debug(details);
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
            log.Debug(details);
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


        public static string GetTestName() => GetTestContextProperty(TestContextProperty.TestName);
        public static string GetTestComponent1() => GetTestContextProperty(TestContextProperty.TestComponent1);
        public static string GetTestComponent2() => GetTestContextProperty(TestContextProperty.TestComponent2);
        public static string GetTestDescription() => GetTestContextProperty(TestContextProperty.TestDescription);
        public static string GetTestPriority() => GetTestContextProperty(TestContextProperty.TestPriority);
        public static string GetTestCaseNumber() => GetTestContextProperty(TestContextProperty.TestCaseNumber);
        public static string GetTestSuiteName() => GetTestContextProperty(TestContextProperty.TestSuite);


        private static string GetTestContextProperty(TestContextProperty testContextProperty)
        {
            string context = string.Empty;

            TestContext.TestAdapter testInstance = TestContext.CurrentContext.Test;

            switch (testContextProperty)
            {
                case TestContextProperty.TestName:
                    return testInstance.Name;
                case TestContextProperty.TestSuite:
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

            var prop = testInstance.Properties.Get(context) ?? "Not Defined";

            //Console.WriteLine($"########### {property.ToString()}");

            //if (prop == null || prop.ToString() == "Not Defined")
            //{
            //    LogDebug($" - Test Context Property is not assigned to Test Case method");
            //}
            //else
            //{
            //    property = prop.ToString();
            //}

            return prop.ToString();
        }

        private enum TestContextProperty
        {
            TestName,
            TestSuite,
            TestComponent1,
            TestComponent2,
            TestDescription,
            TestPriority,
            TestCaseNumber
        }
    }
}