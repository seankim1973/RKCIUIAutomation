using RKCIUIAutomation.Config;

namespace RKCIUIAutomation.Base
{
    public interface IBaseUtils
    {
        string CaptureScreenshot(string fileName = "");
        OutType ConvertToType<OutType>(object objToConvert);
        void CreateVar<T>(T key, string value = "", bool withPrefix = true);
        void DetermineReportFilePath(TenantNameType tenantName);
        string GenerateRandomGuid();
        void SetScreenshotSavePath();
        void SetExtentReportPath();
        void SetCodeBasePath();
        string GetBaseTempFolder();
        string GetDateString();
        string GetTenantName();
        string GetTestCaseNumber();
        string GetTestClassName();
        string GetTestComponent1();
        string GetTestComponent2();
        string GetTestDescription();
        string GetTestName();
        string GetTestPriority();
        string GetVar<T>(T key, bool keyIncludesPrefix = false);
        bool HashKeyExists(string key);
        void ConfigGridAddress(TestPlatformType platform, string gridIPv4Hostname = "");
    }
}