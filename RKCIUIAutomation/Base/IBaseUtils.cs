using System;
using AventStack.ExtentReports.MarkupUtils;
using RKCIUIAutomation.Config;

namespace RKCIUIAutomation.Base
{
    public interface IBaseUtils
    {
        string CaptureScreenshot(string fileName);
        OutType ConvertToType<OutType>(object objToConvert);
        void CreateVar<T>(T key, string value = "", bool withPrefix = true);
        void DetermineReportFilePath();
        string GenerateRandomGuid();
        string GetScreenshotSavePath();
        string GetExtentReportPath();
        string GetCodeBasePath();
        string GetBaseTempFolder();
        string GetDateString();
        string GetTenantName();
        void GetResults<T>(Enum element, BaseUtils.ValidationType validationType, T expected, T actual);
        string GetTestCaseNumber();
        string GetTestClassName();
        string GetTestComponent1();
        string GetTestComponent2();
        string GetTestDescription();
        string GetTestName();
        string GetTestPriority();
        string GetVar<T>(T key, bool keyIncludesPrefix = false);
        bool HashKeyExists(string key);
        void LogAssertIgnore(string msg);
        void LogDebug(string details, Exception exception = null);
        void LogError(string details, bool takeScreenshot = true, Exception e = null);
        void LogErrorWithScreenshot(string details = "", ExtentColor color = ExtentColor.Red, Exception e = null);
        void LogFail(string details, Exception e = null);
        void LogInfo(string details);
        void LogInfo(string[][] detailsList, bool assertion);
        void LogInfo<T>(string details, T assertion, Exception e = null);
        void LogStep(string testStep, bool logInfo = false, bool testResult = true);
        //void RunExternalExecutible(string executible, string cmdLineArgument);
        string SetGridAddress(TestPlatform platform, string gridIPv4Hostname = "");
        //void WriteToFile(string msg, string fileExt = ".txt", bool overwriteExisting = false);
    }
}