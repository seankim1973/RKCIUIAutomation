using static RKCIUIAutomation.Tools.HipTestApi;

namespace RKCIUIAutomation.Config
{
    public interface IConfigUtils
    {
        string GetHipTestCreds(HipTestKey credType);
        string GetSiteUrl(TestEnv testEnv, TenantName tenant);
        TestRunEnv GetTestRunEnv<TestRunEnv>(string nunitArg);
        string[] GetUser(UserType userType);
    }
}