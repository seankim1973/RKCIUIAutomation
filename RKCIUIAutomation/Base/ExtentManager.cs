using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using System;
using System.IO;

namespace RKCIUIAutomation.Base
{
    public class ExtentManager : BaseUtils
    {
        private static string reportFilePath = Directory.GetParent(GetCodeBasePath()) + "\\Report\\Extent.html";
        
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        public static ExtentReports Instance { get { return _lazy.Value; } }

        static ExtentManager()
        {
            var htmlReporter = new ExtentHtmlReporter(reportFilePath);
            htmlReporter.Configuration().ChartLocation = ChartLocation.Top;
            htmlReporter.Configuration().ChartVisibilityOnOpen = true;
            htmlReporter.Configuration().DocumentTitle = "RKCI UI Automation";
            htmlReporter.Configuration().ReportName = "RKCI Test Report";
            htmlReporter.Configuration().Theme = Theme.Standard;
            Instance.AttachReporter(htmlReporter);
            Console.Out.WriteLine("#### Created HTML Report at : \n" + reportFilePath);
        }

        private ExtentManager()
        {
        }
    }
}
