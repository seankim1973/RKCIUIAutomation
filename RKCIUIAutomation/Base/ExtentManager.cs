using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using System;
using System.IO;
using static RKCIUIAutomation.Base.BaseClass;

namespace RKCIUIAutomation.Base
{
    public class ExtentManager : BaseUtils
    {
        public static readonly string reportFilePath = $"{extentReportPath}\\extent.html";
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        public static ExtentReports Instance { get { return _lazy.Value; } }

        static ExtentManager()
        {
            Directory.CreateDirectory(extentReportPath);

            //ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportFilePath);
            //htmlReporter.LoadConfig($"{GetCodeBasePath()}\\extent-config.xml");
            var reporter = (testPlatform == Config.TestPlatform.Local) ? UseHtmlReporter() : UseKlovReporter();
            Instance.AttachReporter(reporter);
        }

        private static IExtentReporter UseHtmlReporter()
        {
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportFilePath);
            htmlReporter.LoadConfig($"{GetCodeBasePath()}\\extent-config.xml");
            return htmlReporter;
        }

        private static IExtentReporter UseKlovReporter()
        {
            var klov = new KlovReporter();
            klov.InitMongoDbConnection("localhost", 27017);
            klov.ProjectName = "RKCIUIAutomation";
            klov.ReportName = $"{testEnv} {tenantName} - {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";
            klov.KlovUrl = "http://localhost";
            return klov;
        }
    }
}
