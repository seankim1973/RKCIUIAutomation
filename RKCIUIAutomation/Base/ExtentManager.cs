using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using System;
using System.IO;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Base.BaseClass;


namespace RKCIUIAutomation.Base
{
    public class ExtentManager : BaseUtils
    {
        private static ExtentHtmlReporter htmlReporter;
        private static KlovReporter klov;
        private static Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());
        public static ExtentReports Instance { get { return _lazy.Value; } set { } }

        public static readonly string reportFilePath = $"{extentReportPath}\\extent.html";
        
        static ExtentManager()
        {
            Directory.CreateDirectory(extentReportPath);
            htmlReporter = new ExtentHtmlReporter(reportFilePath);
            htmlReporter.LoadConfig($"{GetCodeBasePath()}\\extent-config.xml");
            
            if (testPlatform == TestPlatform.Local)
            {
                Instance.AttachReporter(htmlReporter);
            }
            else
            {
                klov = new KlovReporter();
                klov.InitMongoDbConnection("localhost", 27017);
                klov.ProjectName = "RKCIUIAutomation";
                klov.ReportName = $"{testEnv} {tenantName} - {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";
                klov.KlovUrl = "http://10.1.1.207:8888";
                Instance.AttachReporter(htmlReporter, klov);
            }
        }
    }
}
