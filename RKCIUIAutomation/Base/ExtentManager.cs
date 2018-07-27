using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;
using RKCIUIAutomation.Config;
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
            SetInstance();
        }

        private static void SetInstance()
        {
            try
            {
                if (testPlatform == TestPlatform.Local)
                {
                    var htmlReporter = new ExtentHtmlReporter(reportFilePath);
                    htmlReporter.LoadConfig($"{GetCodeBasePath()}\\extent-config.xml");
                    Instance.AttachReporter(htmlReporter);
                }
                else
                {
                    string reportName = $"{testEnv.ToString()}{tenantName.ToString()} - {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";

                    var klov = new KlovReporter();
                    klov.InitMongoDbConnection(GridVmIP, 27017);
                    klov.ProjectName = "RKCIUIAutomation";
                    klov.ReportName = reportName;
                    klov.KlovUrl = $"http://{GridVmIP}:8888";
                    Instance.AttachReporter(klov);
                }
            }
            catch (Exception e)
            {
                log.Error($"Error in ExtentManager constructor:\n{e.Message}");
            }
        }

        private ExtentManager() { }
    }
}
