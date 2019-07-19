using AventStack.ExtentReports;
using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Base
{
    public class ExtentService : BaseUtils
    {
        public static readonly string reportFilePath = $"{ExtentReportPath}\\extent_{tenantName.ToString()}.html";

        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        public static ExtentReports Instance { get { return _lazy.Value; } }


        static ExtentService()
        {
            IExtentReporter reporterType = null;

            if (reporter == Reporter.Klov)
            {
                reporterType = GetKlovReporter();
            }
            else
            {
                reporterType = GetHtmlReporter();
            }

            Instance.AttachReporter(reporterType);
        }

        private ExtentService()
        {
        }

        private static IExtentReporter GetHtmlReporter()
        {
            ExtentV3HtmlReporter htmlReporter = null;

            try
            {
                Directory.CreateDirectory(ExtentReportPath);
                htmlReporter = new ExtentV3HtmlReporter(reportFilePath);
                htmlReporter.LoadConfig($"{CodeBasePath}\\extent-config.xml");
            }
            catch (Exception e)
            {
                log.Error($"Error in GetHtmlReporter method:\n{e.Message}");
                throw;
            }

            return htmlReporter;
        }

        private static IExtentReporter GetKlovReporter()
        {
            ExtentKlovReporter klovReporter = null;

            try
            {
                klovReporter = new ExtentKlovReporter(tenantName.ToString(), "ELVIS PMC");
                klovReporter.InitMongoDbConnection(GridVmIP, 27017);
                klovReporter.InitKlovServerConnection($"http://{GridVmIP}:8888");
            }
            catch (Exception e)
            {
                log.Error($"Error in GetKlovReporter method:\n{e.Message}");
                throw;
            }

            return klovReporter;
        }
    }

}
