using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using log4net;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;

namespace RKCIUIAutomation.Base
{
    public class BaseUtils : ConfigUtils
    {        
        internal static readonly ILog log = LogManager.GetLogger("");

        public static string extentReportPath = string.Empty;
        public static string fullTempFileName = string.Empty;
        private static string baseTempFolder = string.Empty;
        private static string fileName = string.Empty;
        private static string dateString = string.Empty;
        private static string screenshotSavePath = string.Empty;


        public BaseUtils()
        {
            baseTempFolder = $"{GetCodeBasePath()}\\Temp";
            fileName = BaseClass.tenantName.ToString();
            dateString = GetDateString();
        }

        private string SetWinTempFolder()
        {
            string cTemp = "C:\\Temp";
            if (!File.Exists(cTemp))
            {
                Directory.CreateDirectory(cTemp);
            }

            return cTemp;
        }

        private string GetDateString()
        {
            string[] shortDate = (DateTime.Today.ToShortDateString()).Split('/');
            string month = shortDate[0];
            string date = shortDate[1];

            month = (month.Length > 1)?month : $"0{month}";
            date = (date.Length > 1)?date : $"0{date}";

            return $"{month}{date}{shortDate[2]}";
        }

        public static void DetermineReportFilePath()
        {
            extentReportPath = $"{GetCodeBasePath()}\\Report";
            string klovPath = $"{extentReportPath}\\errorscreenshots\\"; //TODO: <<--Temp until bug fix by ExtentReports.  >>-Use when bug fixed ->> "C:\\Automation\\klov-0.1.1\\upload\\reports\\";
            screenshotSavePath = (BaseClass.testPlatform == TestPlatform.Local) ?
                $"{extentReportPath}\\errorscreenshots\\" : klovPath; 
        }

        public static string GetCodeBasePath()
        {
            Directory.SetCurrentDirectory(Directory.GetParent(TestContext.CurrentContext.TestDirectory).ToString());
            string baseDir = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            return baseDir;
        }

        public static string CaptureScreenshot(IWebDriver driver, string fileName)
        {
            string uniqueFileName = $"{fileName}{DateTime.Now.Second}.png";
            Directory.CreateDirectory(screenshotSavePath);
            var screenshot = driver.TakeScreenshot();
            screenshot.SaveAsFile($"{screenshotSavePath}{uniqueFileName}", ScreenshotImageFormat.Png);

            string fileRef = "errorscreenshots/";//TODO: <<--Temp until bug fix by ExtentReports.  >>-Use when bug fixed ->> (BaseClass.testPlatform == TestPlatform.Local) ? "errorscreenshots/" : "upload/reports/";
            return $"{fileRef}{uniqueFileName}";
        }

        //ExtentReports Loggers
        public static void LogIgnore(string msg)
        {
            ExtentTestManager.GetTestNode().Skip(CreateReportMarkupLabel(msg, ExtentColor.Orange));
            log.Debug(msg);
        }
        public static void LogFail(string details, Exception e = null)
        {
            ExtentTestManager.GetTestNode().Fail(CreateReportMarkupLabel(details, ExtentColor.Red));
            log.Error(details);

            if (e != null)
            {
                ExtentTestManager.GetTestNode().Error(CreateReportMarkupCodeBlock(e));
                log.Error(e.Message);
            }
        }
        public void LogError(string details, bool takeScreenshot = true, Exception e = null)
        {
            if (takeScreenshot)
            {
                LogErrorWithScreenshot();
            }
            else
            {
                ExtentTestManager.GetTestNode().Error(CreateReportMarkupLabel(details, ExtentColor.Red));               
            }
            log.Error(details);

            if (e != null)
            {
                ExtentTestManager.GetTestNode().Error(CreateReportMarkupCodeBlock(e));
                log.Error(e.Message);
            }
        }
        public static void LogDebug(string details)
        {
            if (details.Contains(">>>"))
            {
                ExtentTestManager.GetTestNode().Debug(CreateReportMarkupLabel(details, ExtentColor.Orange));
            }
            else
                ExtentTestManager.GetTestNode().Debug(CreateReportMarkupLabel(details, ExtentColor.Grey));

            log.Debug(details);
        }
        public void LogErrorWithScreenshot()
        {
            string screenshotPath = CaptureScreenshot(driver, GetTestName());
            ExtentTestManager.GetTestNode().Error($"Error Screenshot:", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
        }
        public static void LogInfo(string details)
        {
            string[] detailsBr = null;

            if (details.Contains("<br>"))
            {
                ExtentTestManager.GetTestNode().Info(CreateReportMarkupLabel(details, ExtentColor.Orange));
                detailsBr = Regex.Split(details, "<br>&nbsp;&nbsp;");
                for (int i = 0; i < detailsBr.Length; i++)
                {
                    log.Info(detailsBr[i]);
                }
            }
            else if (details.Contains("#####"))
            {
                ExtentTestManager.GetTestNode().Info(CreateReportMarkupLabel(details));
            }
            else if (details.Contains(">>>"))
            {
                ExtentTestManager.GetTestNode().Info(CreateReportMarkupLabel(details, ExtentColor.Lime));
            }
            else
            {
                ExtentTestManager.GetTestNode().Info(details);
                log.Info(details);
            }
        }
        public static void LogInfo(string details, Exception e)
        {
            string[] detailsBr = null;

            ExtentTestManager.GetTestNode().Debug(CreateReportMarkupLabel(details, ExtentColor.Orange));
            if (details.Contains("<br>"))
            {
                detailsBr = Regex.Split(details, "<br>&nbsp;&nbsp;");
                for (int i = 0; i < detailsBr.Length; i++)
                {
                    log.Info(detailsBr[i]);
                }
            }
            else
            {
                log.Debug(details);
            }
                       
            if(e != null)
            {
                ExtentTestManager.GetTestNode().Debug(CreateReportMarkupLabel(e.Message, ExtentColor.Grey));
                log.Debug(e.Message);
            }
        }

        public void LogInfo(string details, bool assertion, Exception e = null)
        {
            bool hasPgBreak = false;
            string[] detailsBr = null;

            if (details.Contains("<br>"))
            {
                detailsBr = Regex.Split(details, "<br>&nbsp;&nbsp;");
                hasPgBreak = true;
            }

            if (assertion)
            {
                ExtentTestManager.GetTestNode().Pass(CreateReportMarkupLabel(details, ExtentColor.Green));
                if (hasPgBreak)
                {
                    for (int i = 0; i < detailsBr.Length; i++)
                    {
                        log.Info(detailsBr[i]);
                    }
                }
                else
                    log.Info(details);
            }
            else
            {
                ExtentTestManager.GetTestNode().Fail(CreateReportMarkupLabel(details, ExtentColor.Red));
                LogErrorWithScreenshot();
                if (hasPgBreak)
                {
                    for (int i = 0; i < detailsBr.Length; i++)
                    {
                        log.Fatal(detailsBr[i]);
                    }
                }
                else
                    log.Fatal(details);

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


        //Helper methods to gather Test Context Details
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
                fullTempFileName = $"{baseTempFolder}\\{fileName}({dateString})";

                Directory.CreateDirectory(baseTempFolder);
                string path = $"{fullTempFileName}{fileExt}";

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
                    if (msg.Contains("<br>"))
                    {
                        string[] message = Regex.Split(msg, "<br>&nbsp;&nbsp;");
                        sw.WriteLine(message[0]);
                        sw.WriteLine(message[1]);
                    }
                    else
                    {
                        sw.WriteLine(msg);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static PageBaseHelper pgbHelper = new PageBaseHelper();
        public static void InjectTestStatus(TestStatus status, string logMsg)
        {
            string testName = GetTestName();
            pgbHelper.CreateVar($"{testName}_msgKey", logMsg);
            pgbHelper.CreateVar($"{testName}_statusKey", status.ToString());
        }

        public static void CheckForTestStatusInjection()
        {
            string testName = GetTestName();
            string logMessage = pgbHelper.GetVar($"{testName}_msgKey");
            string injStatus = pgbHelper.GetVar($"{testName}_statusKey");

            switch (injStatus)
            {
                case "Failed":
                    Assert.Fail(logMessage);
                    break;
            }
        }
    }

    public static class BaseHelper
    {
        public static string SplitCamelCase(this string str, bool removeUnderscore = true)
        {
            string value = (removeUnderscore == true) ? Regex.Replace(str, @"_", "") : str;
            return Regex.Replace(Regex.Replace(value, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }
    }
}