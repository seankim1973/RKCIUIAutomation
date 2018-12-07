using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using RKCIUIAutomation.Config;
using System;
using System.IO;
using static RKCIUIAutomation.Base.BaseClass;

namespace RKCIUIAutomation.Base
{
    public class ExtentManager : BaseUtils
    {
        public static readonly string reportFilePath = $"{extentReportPath}\\extent_{tenantName.ToString()}.html";

        private static readonly Lazy<ExtentReports> _lazy;
        public static ExtentReports Instance { get { return _lazy.Value; } }

        private static ExtentHtmlReporter htmlReporter;
        private static KlovReporter klov;

        static ExtentManager()
        {
            try
            {
                Directory.CreateDirectory(extentReportPath);
                htmlReporter = new ExtentHtmlReporter(reportFilePath);
                htmlReporter = GetHtmlReporter();

                _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

                if (reporter == Reporter.Klov)
                {
                    GetKlovReporter();
                    klov.InitMongoDbConnection(GridVmIP, 27017);
                    Instance.AttachReporter(htmlReporter, klov);
                }
                else
                {
                    Instance.AttachReporter(htmlReporter);
                }

                Instance.AddSystemInfo("Tenant", tenantName.ToString());
                Instance.AddSystemInfo("Environment", testEnv.ToString());
                Instance.AddSystemInfo("URL", siteUrl);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        private static ExtentHtmlReporter GetHtmlReporter()
        {
            try
            {
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

                klov = new KlovReporter
                {
                    ProjectName = "RKCIUIAutomation",
                    ReportName = reportName,
                    KlovUrl = $"http://{GridVmIP}:8888"
                };
            }
            catch (Exception e)
            {
                log.Error($"Error in GetKlovReporter method:\n{e.Message}");
            }

            return klov;
        }
    }
}