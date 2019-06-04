using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using log4net;
using log4net.Core;
using MiniGuids;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Tools;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.StaticHelpers;

namespace RKCIUIAutomation.Base
{
    public class BaseUtils : BaseClass, IBaseUtils
    {
        [ThreadStatic]
        internal static Hashtable Hashtable;

        private enum TestContextProperty
        {
            TestName,
            TestClass,
            TestComponent1,
            TestComponent2,
            TestDescription,
            TestPriority,
            TestCaseNumber
        }

        public static string CurrentTenantName { get; private set; }
        public static string DateString { get; private set; }
        public static string CodeBasePath { get; private set; }
        public static string BaseTempFolder { get; private set; }
        public static string ExtentReportPath { get; private set; }
        public static string ScreenshotSavePath { get; private set; }

        public BaseUtils()
        {
        }

        public BaseUtils(TenantName tenantName)
            => DetermineReportFilePath(tenantName);

        public BaseUtils(TestPlatform testPlatform, string gridAddress)
            => ConfigGridAddress(testPlatform, gridAddress);

        public BaseUtils(IWebDriver driver) => this.Driver = driver;

        public void SetCodeBasePath()
        {
            if (!CodeBasePath.HasValue())
            {
                Directory.SetCurrentDirectory(Directory.GetParent(TestContext.CurrentContext.TestDirectory).ToString());
                CodeBasePath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            }
        }

        public void SetExtentReportPath()
        {
            if (!ExtentReportPath.HasValue())
            {
                ExtentReportPath = $"{CodeBasePath}\\Report";
            }
        }

        public string GetDateString()
        {
            if (!DateString.HasValue())
            {
                string[] shortDate = (DateTime.Today.ToShortDateString()).Split('/');
                string month = shortDate[0];
                string date = shortDate[1];

                month = (month.Length > 1) ? month : $"0{month}";
                date = (date.Length > 1) ? date : $"0{date}";
                DateString = $"{month}{date}{shortDate[2]}";
            }

            return DateString;
        }

        public void SetScreenshotSavePath()
        {
            if (!ScreenshotSavePath.HasValue())
            {
                ScreenshotSavePath = $"{ExtentReportPath}\\errorscreenshots\\";
            }
        }

        public string GetBaseTempFolder()
        {
            if (!BaseTempFolder.HasValue())
            {
                BaseTempFolder = $"{CodeBasePath}\\Temp";
            }

            return BaseTempFolder;
        }

        public string GetTenantName()
        {
            if (!CurrentTenantName.HasValue())
            {
                CurrentTenantName = tenantName.ToString();
            }
            return CurrentTenantName;
        }

        public void DetermineReportFilePath(TenantName tenantName)
        {
            SetCodeBasePath();
            SetExtentReportPath();
            SetScreenshotSavePath();
        }

        public string CaptureScreenshot(string fileName = "")
        {
            IWebDriver _driver = Driver;

            string uniqueFileName = string.Empty;
            string fullFilePath = string.Empty;
            string klovPath = string.Empty;

            try
            {
                Directory.CreateDirectory(ScreenshotSavePath);
                uniqueFileName = $"{(fileName.HasValue() ? fileName : GetTestName())}{DateTime.Now.Second}_{GetTenantName()}.png";
                fullFilePath = $"{ScreenshotSavePath}{uniqueFileName}";

                if (reporter == Reporter.Klov)
                {
                    if (testPlatform == TestPlatform.Grid)
                    {
                        klovPath = @"\\10.1.1.207\errorscreenshots\";

                        ImpersonateUser impersonateUser = new ImpersonateUser(_driver);
                        impersonateUser.ScreenshotTool(ImpersonateUser.Task.SAVESCREENSHOT, $"{klovPath}{uniqueFileName}");
                    }
                    else if (testPlatform == TestPlatform.GridLocal)
                    {
                        klovPath = @"C:\Automation\klov\errorscreenshots\";
                        var screenshot = _driver.TakeScreenshot();
                        screenshot.SaveAsFile($"{klovPath}{uniqueFileName}");
                    }
                }
                else
                {
                    var screenshot = _driver.TakeScreenshot();
                    screenshot.SaveAsFile(fullFilePath);
                }
            }
            catch (Exception e)
            {
                log.Debug($"Exception occured: {e.Message}");
            }

            return uniqueFileName;
        }

        public void ConfigGridAddress(TestPlatform platform, string gridIPv4Hostname = "")
        {
            GridVmIP = gridIPv4Hostname.Equals("")
                ? platform == TestPlatform.GridLocal || platform == TestPlatform.Local
                    ? "127.0.0.1"
                    : "10.1.1.207"
                : gridIPv4Hostname;
        }

        //Helper methods to gather Test Context Details
        public string GetTestName()
            => GetTestContextProperty(TestContextProperty.TestName);

        public string GetTestComponent1()
            => GetTestContextProperty(TestContextProperty.TestComponent1);

        public string GetTestComponent2()
            => GetTestContextProperty(TestContextProperty.TestComponent2);

        public string GetTestDescription()
            => GetTestContextProperty(TestContextProperty.TestDescription);

        public string GetTestPriority()
            => GetTestContextProperty(TestContextProperty.TestPriority);

        public string GetTestCaseNumber()
            => GetTestContextProperty(TestContextProperty.TestCaseNumber);

        public string GetTestClassName()
            => GetTestContextProperty(TestContextProperty.TestClass);

        private string GetTestContextProperty(TestContextProperty testContextProperty)
        {
            TestContext.TestAdapter testInstance = TestContext.CurrentContext.Test;
            string context = string.Empty;

            switch (testContextProperty)
            {
                case TestContextProperty.TestName:
                    return testInstance.Name;

                case TestContextProperty.TestClass:
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

            var prop = testInstance.Properties.Get(context) ?? string.Empty;
            return prop.ToString();
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

        public OutType ConvertToType<OutType>(object objToConvert)
        {
            try
            {
                Type inputType = objToConvert.GetType();
                return (OutType)Convert.ChangeType(objToConvert, typeof(OutType));
            }
            catch (Exception e)
            {
                log.Error($"Error occurred in ConvertToType method:\n{e.Message}");
                throw;
            }
        }

        private Hashtable GetHashTable() => Hashtable ?? new Hashtable();

        public string GenerateRandomGuid()
        {
            MiniGuid guid = MiniGuid.NewGuid();
            return guid;
        }

        public void CreateVar<T>(T key, string value = "", bool withPrefix = true)
        {
            try
            {
                string logMsg = string.Empty;
                string argKey = BaseUtil.ConvertToType<string>(key);
                argKey = withPrefix
                    ? GetEnvVarPrefix(argKey)
                    : argKey;

                value = value.HasValue()
                    ? value
                    : GenerateRandomGuid();

                Hashtable = GetHashTable();

                if (!HashKeyExists(argKey))
                {
                    Hashtable.Add(argKey, value);
                    logMsg = "Created";
                }
                else
                {
                    Hashtable[argKey] = value;
                    logMsg = "Updated";
                }

                log.Debug($"{logMsg} HashTable - Key: {argKey} : Value: {value}");
            }
            catch (Exception e)
            {
                log.Error($"Error occured while adding to HashTable \n{e.Message}");
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetVar<T>(T key, bool keyIncludesPrefix = false)
        {
            string argKey = ConvertToType<string>(key);
            argKey = keyIncludesPrefix
                ? argKey
                : GetEnvVarPrefix(argKey);

            if (!HashKeyExists(argKey))
            {
                CreateVar(argKey, "", false);
            }

            Hashtable = GetHashTable();
            var varValue = Hashtable[argKey].ToString();
            log.Debug($"#####GetVar Key: {argKey} has Value: {varValue}");

            return varValue;
        }

        public bool HashKeyExists(string key)
        {
            Hashtable = GetHashTable();
            return Hashtable.ContainsKey(key);
        }
    }

}