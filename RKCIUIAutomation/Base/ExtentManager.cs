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

        private static KlovReporter klov;
        private static ExtentHtmlReporter htmlReporter;

        static ExtentManager()
        {
            try
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
                    klov.InitMongoDbConnection(GridVmIP, 27017);
                    klov.ProjectName = "RKCIUIAutomation";
                    klov.ReportName = $"{testEnv} {tenantName} - {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";
                    klov.KlovUrl = $"http://{GridVmIP}:8888";
                    Instance.AttachReporter(htmlReporter, klov);
                    klov.Start();
                }
            }
            catch (Exception e)
            {
                log.Error($"Error in ExtentManager constructor:\n{e.Message}");
            }
        }
    }
}
