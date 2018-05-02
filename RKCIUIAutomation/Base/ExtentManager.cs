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
        private static string screenshotPath = Directory.GetParent(GetCodeBasePath()) + "\\Report";
        
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        public static ExtentReports Instance { get { return _lazy.Value; } }

        static ExtentManager()
        {
            Console.Out.WriteLine("#### ExtentMgr : " + screenshotPath);

            var htmlReporter = new ExtentHtmlReporter(screenshotPath + "\\Extent.html");
            htmlReporter.Configuration().ChartLocation = ChartLocation.Top;
            htmlReporter.Configuration().ChartVisibilityOnOpen = true;
            htmlReporter.Configuration().DocumentTitle = "RKCI UI Automation";
            htmlReporter.Configuration().ReportName = "RKCI Test Report";
            htmlReporter.Configuration().Theme = Theme.Standard;

            Instance.AttachReporter(htmlReporter);
        }

        private ExtentManager()
        {
        }
    }
}
