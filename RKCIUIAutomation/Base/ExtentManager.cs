using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;
using System.IO;

namespace RKCIUIAutomation.Base
{
    public class ExtentManager : BaseUtils
    {
        public static readonly string reportFilePath = $"{extentReportPath}\\extent.html";
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());
        //private static readonly Lazy<KlovReporter> _lazyKlov = new Lazy<KlovReporter>(() => new KlovReporter());

        public static ExtentReports Instance { get { return _lazy.Value; } }
        //public static KlovReporter Klov { get { return _lazyKlov.Value; } }

        static ExtentManager()
        {            
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportFilePath);
            htmlReporter.LoadConfig($"{GetCodeBasePath()}\\extent-config.xml");

            if (BaseClass.testPlatform != Config.TestPlatform.Local)
            {
                KlovReporter Klov = new KlovReporter();
                Klov.InitMongoDbConnection("localhost", 27017);
                Klov.ProjectName = "RKCIUIAutomation";
                Klov.ReportName = "RKCI Elvis UI Automation";
                Klov.KlovUrl = "http://localhost:8088";
                Instance.AttachReporter(htmlReporter, Klov);
            } else
                Instance.AttachReporter(htmlReporter);
        }

        private ExtentManager() { }
    }
}
