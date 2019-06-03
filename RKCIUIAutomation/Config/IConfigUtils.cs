using static RKCIUIAutomation.Tools.HipTestApi;

namespace RKCIUIAutomation.Config
{
    public interface IConfigUtils
    {
        string[] GetUser(UserType userType);
        string GetEncryptedPW(string decryptedPW);
        string GetDecryptedPW(string encryptedPW);
        string GetHipTestCreds(HipTestKey credType);
        string GetSiteUrl(TestEnv testEnv, TenantName tenant);
        TestRunEnv GetTestRunEnv<TestRunEnv>(string nunitArg);
    }
}