using AventStack.ExtentReports;
using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.Reporter;
using RKCIUIAutomation.Config;
using System;
using System.IO;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Base
{
    public class ExtentManager : BaseUtils
    {
        public static readonly string reportFilePath = $"{ExtentReportPath}\\extent_{tenantName.ToString()}.html";

        private static readonly Lazy<ExtentReports> _lazy;

        public static ExtentReports GetReportInstance()
        { return _lazy.Value; }

        public static ExtentKlovReporter Klov { get; set; }
        public static ExtentHtmlReporter HtmlReporter { get; set; }

        static ExtentManager()
        {
            try
            {
                Directory.CreateDirectory(ExtentReportPath);
                HtmlReporter = new ExtentHtmlReporter(reportFilePath);
                HtmlReporter = (ExtentHtmlReporter)GetHtmlReporter();

                _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

                IExtentReporter reporterType = null;

                if (reporter == Reporter.Klov)
                {
                    reporterType = GetKlovReporter(GridVmIP);
                }
                else
                {
                    reporterType = HtmlReporter;
                }

                //GetReportInstance().AddSystemInfo("Tenant", tenantName.ToString());
                //GetReportInstance().AddSystemInfo("Environment", testEnv.ToString());
                //GetReportInstance().AddSystemInfo("URL", siteUrl);

                var reportInstance = GetReportInstance();
                reportInstance.AttachReporter(reporterType);
                reportInstance.AddSystemInfo("Tenant", tenantName.ToString());
                reportInstance.AddSystemInfo("Environment", testEnv.ToString());
                reportInstance.AddSystemInfo("URL", siteUrl);

            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        private static IExtentReporter GetHtmlReporter()
        {
            try
            {
                HtmlReporter.LoadConfig($"{CodeBasePath}\\extent-config.xml");
            }
            catch (Exception e)
            {
                log.Error($"Error in GetHtmlReporter method:\n{e.Message}");
                throw;
            }

            return HtmlReporter;
        }

        private static IExtentReporter GetKlovReporter(string gridVmIP)
        {
            try
            {
                string reportName = $"{tenantName.ToString()}({testEnv.ToString()}) - {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";

                Klov = new ExtentKlovReporter("RKCIUIAutomation", reportName);
                Klov.InitMongoDbConnection(gridVmIP, 27017);
                Klov.InitKlovServerConnection($"http://{gridVmIP}:8888");
            }
            catch (Exception e)
            {
                log.Error($"Error in GetKlovReporter method:\n{e.Message}");
                throw;
            }

            return Klov;
        }
    }
}