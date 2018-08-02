using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Base.BaseClass;
using AventStack.ExtentReports.Model;
using MongoDB.Driver;

namespace RKCIUIAutomation.Base
{
    public class ExtentManager : BaseUtils
    {
        public static readonly string reportFilePath = $"{extentReportPath}\\extent_{tenantName.ToString()}.html";
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        public static ExtentReports Instance { get { return _lazy.Value; } }
        public static ExtentHtmlReporter htmlReporter;
        public static KlovReporter klov;

        static ExtentManager()
        {
            htmlReporter = GetHtmlReporter();

            if (reporter == Reporter.Klov)
            {
                klov = GetKlovReporter();
                Instance.AttachReporter(htmlReporter, klov);
            }
            else
            {
                Instance.AttachReporter(htmlReporter);
                Instance.AddSystemInfo("Tenant", tenantName.ToString());
                Instance.AddSystemInfo("Environment", testEnv.ToString());
                Instance.AddSystemInfo("URL", siteUrl);
            }
        }

        private static ExtentHtmlReporter GetHtmlReporter()
        {
            try
            {
                Directory.CreateDirectory(extentReportPath);
                htmlReporter = new ExtentHtmlReporter(reportFilePath);
                htmlReporter.LoadConfig($"{GetCodeBasePath()}\\extent-config.xml");
            }
            catch (Exception e)
            {
                log.Error($"Error in GetHtmlReporter method:\n{e.Message}");
            }

            return htmlReporter;
        }

        private static KlovReporter GetKlovReporter()
        {
            try
            {
                string reportName = $"{tenantName.ToString()}({testEnv.ToString()}) - {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";

                klov = new KlovReporter();
                klov.InitMongoDbConnection(GridVmIP, 27017);
                
                klov.ProjectName = "RKCIUIAutomation";
                klov.ReportName = reportName;
                klov.KlovUrl = $"http://{GridVmIP}:8888"; 
            }
            catch (Exception e)
            {
                log.Error($"Error in GetKlovReporter method:\n{e.Message}");
            }

            return klov;
        }

        private ExtentManager() { }
    }
}
