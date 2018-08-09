using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Base.BaseClass;
using AventStack.ExtentReports.Model;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace RKCIUIAutomation.Base
{
    public class ExtentManager : BaseUtils
    {
        public static readonly string reportFilePath = $"{extentReportPath}\\extent_{tenantName.ToString()}.html";

        [ThreadStatic]
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());
        public static ExtentReports Instance { get { return _lazy.Value; } }

        [ThreadStatic]
        private static readonly Lazy<KlovReporter> _klov = new Lazy<KlovReporter>(() => new KlovReporter());
        private static KlovReporter Klov { get { return _klov.Value; } }

        private static ExtentHtmlReporter htmlReporter;

        [MethodImpl(MethodImplOptions.Synchronized)]
        static ExtentManager()
        {
            Directory.CreateDirectory(extentReportPath);
            htmlReporter = new ExtentHtmlReporter(reportFilePath);
            htmlReporter = GetHtmlReporter();

            if (reporter == Reporter.Klov)
            {
                GetKlovReporter();
                Klov.InitMongoDbConnection(GridVmIP, 27017);
                Instance.AttachReporter(htmlReporter, Klov);
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
                htmlReporter.LoadConfig($"{GetCodeBasePath()}\\extent-config.xml");
            }
            catch (Exception e)
            {
                log.Error($"Error in GetHtmlReporter method:\n{e.Message}");
            }

            return htmlReporter;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static KlovReporter GetKlovReporter()
        {
            try
            {
                string reportName = $"{tenantName.ToString()}({testEnv.ToString()}) - {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";
                Klov.ProjectName = "RKCIUIAutomation";
                Klov.ReportName = reportName;
                Klov.KlovUrl = $"http://{GridVmIP}:8888"; 
            }
            catch (Exception e)
            {
                log.Error($"Error in GetKlovReporter method:\n{e.Message}");
            }

            return Klov;
        }

        private ExtentManager() { }
    }
}
