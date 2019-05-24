using System;
using AventStack.ExtentReports.MarkupUtils;

namespace RKCIUIAutomation.Base
{
    public interface IReportLogger
    {
        void GetResults<T>(Enum element, ReportLogger.ValidationType validationType, T expected, T actual);
        void AssertIgnore(string msg);
        void Debug(string details, Exception exception = null);
        void Error(string details, bool takeScreenshot = true, Exception e = null);
        void ErrorWithScreenshot(string details = "", ExtentColor color = ExtentColor.Red, Exception e = null);
        void Fail(string details, Exception e = null);
        void Info(string details);
        void Info(string[][] detailsList, bool assertion);
        void Info<T>(string details, T assertion, Exception e = null);
        void Step(string testStep, bool logInfo = false, bool testResult = true);
    }
}