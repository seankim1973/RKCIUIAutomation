using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using log4net;
using log4net.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using RKCIUIAutomation.Config;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace RKCIUIAutomation.Base
{
    public class BaseUtils : ConfigUtils
    {
        internal static readonly ILog log = LogManager.GetLogger("");
  
        public static string extentReportPath = string.Empty;
        //public static string screenshotReferencePath = "errorscreenshots/";
        public static string fullTempFileName = string.Empty;
        private static string baseTempFolder = string.Empty;
        private static string fileName = string.Empty;
        private static string dateString = string.Empty;

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
            if (BaseClass.testPlatform.ToString() == "Local")
            {
                extentReportPath = $"{GetCodeBasePath()}\\Report";
                //screenshotReferencePath = "errorscreenshots/";
            }
            else
            {
                extentReportPath = "C:\\inetpub\\wwwroot\\extentreport";
                //screenshotReferencePath = "errorscreenshots/";
            }
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
            var screenshot = driver.TakeScreenshot();
            string screenshotFolderPath = $"{extentReportPath}\\errorscreenshots\\";
            Directory.CreateDirectory(screenshotFolderPath);
            screenshot.SaveAsFile($"{screenshotFolderPath}{uniqueFileName}", ScreenshotImageFormat.Png);
            return $"{"errorscreenshots/"}{uniqueFileName}";
        }

        //ExtentReports Loggers
        public static void LogIgnore(string msg)
        {
            ExtentTestManager.GetTest().Skip(CreateReportMarkupLabel(msg, ExtentColor.Orange));
            log.Debug(msg);
        }
        public void LogError(string details, bool takeScreenshot = true, Exception e = null)
        {
            ExtentTestManager.GetTest().Error(CreateReportMarkupLabel(details, ExtentColor.Red));
            log.Error(details);

            if (takeScreenshot)
            {
                LogErrorWithScreenshot();          
            }
            
            if (e != null)
            {
                log.Fatal(e.Message);
            }
        }
        public static void LogDebug(string details)
        {
            if (details.Contains(">>>"))
            {
                ExtentTestManager.GetTest().Debug(CreateReportMarkupLabel(details, ExtentColor.Orange));
            }
            else
                ExtentTestManager.GetTest().Debug(CreateReportMarkupLabel(details, ExtentColor.Grey));

            log.Debug(details);
        }
        public void LogErrorWithScreenshot()
        {
            string screenshotPath = CaptureScreenshot(driver, GetTestName());
            ExtentTestManager.GetTest().Error($"Error Screenshot:", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
        }
        public static void LogInfo(string details)
        {
            if (details.Contains("#####"))
            {
                ExtentTestManager.GetTest().Info(CreateReportMarkupLabel(details));
            }
            else if (details.Contains(">>>"))
            {
                ExtentTestManager.GetTest().Info(CreateReportMarkupLabel(details, ExtentColor.Lime));
            }
            else
                ExtentTestManager.GetTest().Info(details);
            log.Info(details);    
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
                ExtentTestManager.GetTest().Pass(CreateReportMarkupLabel(details, ExtentColor.Green));
                if (hasPgBreak)
                {
                    log.Info(detailsBr[0]);
                    log.Info(detailsBr[1]);
                }
                else
                    log.Info(details);
            }
            else
            {
                ExtentTestManager.GetTest().Fail(CreateReportMarkupLabel(details, ExtentColor.Red));
                LogErrorWithScreenshot();
                if (hasPgBreak)
                {
                    log.Fatal(detailsBr[0]);
                    log.Fatal(detailsBr[1]);
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