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

        public static ExtentReports GetReportInstance()
        { return _lazy.Value; }
        public static KlovReporter Klov { get; set; }
        public static ExtentHtmlReporter HtmlReporter { get; set; }

        static ExtentManager()
        {
            try
            {
                Directory.CreateDirectory(extentReportPath);
                HtmlReporter = new ExtentHtmlReporter(reportFilePath);
                HtmlReporter = GetHtmlReporter();

                _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

                if (reporter == Reporter.Klov)
                {
                    GetReportInstance().AttachReporter(HtmlReporter, GetKlovReporter(GridVmIP));
                }
                else
                {
                    GetReportInstance().AttachReporter(HtmlReporter);
                }

                GetReportInstance().AddSystemInfo("Tenant", tenantName.ToString());
                GetReportInstance().AddSystemInfo("Environment", testEnv.ToString());
                GetReportInstance().AddSystemInfo("URL", siteUrl);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
        }

        private static ExtentHtmlReporter GetHtmlReporter()
        {
            try
            {
                HtmlReporter.LoadConfig($"{GetCodeBasePath()}\\extent-config.xml");
            }
            catch (Exception e)
            {
                log.Error($"Error in GetHtmlReporter method:\n{e.Message}");
                throw e;
            }

            return HtmlReporter;
        }

        private static KlovReporter GetKlovReporter(string gridVmIP)
        {
            try
            {
                string reportName = $"{tenantName.ToString()}({testEnv.ToString()}) - {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";

                Klov = new KlovReporter
                {
                    ProjectName = "RKCIUIAutomation",
                    ReportName = reportName,
                    KlovUrl = $"http://{gridVmIP}:8888"
                };

                Klov.InitMongoDbConnection(gridVmIP, 27017);
            }
            catch (Exception e)
            {
                log.Error($"Error in GetKlovReporter method:\n{e.Message}");
                throw e;
            }

            return Klov;
        }
    }
}