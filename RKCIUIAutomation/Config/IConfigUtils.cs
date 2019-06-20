using static RKCIUIAutomation.Tools.HipTestApi;

namespace RKCIUIAutomation.Config
{
    public interface IConfigUtils
    {
        string[] GetUserCredentials(UserType userType);
        void SetCurrentUserEmail(UserType userType);
        string GetCurrentUserEmail();
        string GetEncryptedPW(string decryptedPW);
        string GetDecryptedPW(string encryptedPW);
        string GetHipTestCreds(HipTestKey credType);
        string GetSiteUrl(TestEnv testEnv, TenantName tenant);
        TestRunEnv GetTestRunEnv<TestRunEnv>(string nunitArg);
    }
}