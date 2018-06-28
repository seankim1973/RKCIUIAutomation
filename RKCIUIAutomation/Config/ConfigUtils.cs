using NUnit.Framework;
using System;
using System.Collections.Specialized;
using System.Configuration;

using static RKCIUIAutomation.Base.BaseUtils;

namespace RKCIUIAutomation.Config
{
    #pragma warning disable IDE0044 // Add readonly modifier
    public class ConfigUtil
    {
        public static ConfigUtil configUtil = new ConfigUtil();
        
        private string _testPlatform = TestContext.Parameters.Get("Platform", $"{TestPlatform.Local}");
        private string _browserType = TestContext.Parameters.Get("Browser", $"{BrowserType.Chrome}");
        private string _testEnv = TestContext.Parameters.Get("TestEnv", $"{TestEnv.Stage}");
        private string _tenantName = TestContext.Parameters.Get("Project", $"{TenantName.SGWay}");

        public TestPlatform GetTestPlatform() => (TestPlatform)Enum.Parse(typeof(TestPlatform), _testPlatform);
        public BrowserType GetBrowserType() => (BrowserType)Enum.Parse(typeof(BrowserType), _browserType);
        public TestEnv GetTestEnv() => (TestEnv)Enum.Parse(typeof(TestEnv), _testEnv);
        public TenantName GetTenantName() => (TenantName)Enum.Parse(typeof(TenantName), _tenantName);

        public string GetSiteUrl(TestEnv testEnv, TenantName project)
        {
            string siteKey = $"{project}_{testEnv}";
            return GetValueFromConfigManager(siteUrlKey:siteKey);
        }

        //return string array of username[0] and password[1]
        public string[] GetUser(UserType userType)
        {
            string userKey = $"{userType}";
            string[] usernamePassword = GetValueFromConfigManager(userTypeKey:userKey).Split(',');
            return usernamePassword;
        }

        private string GetValueFromConfigManager(string siteUrlKey = "", string userTypeKey = "")
        {
            string sectionType = null;
            string key = null;
            NameValueCollection collection = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(userTypeKey))
                {
                    sectionType = "UserType";
                    key = userTypeKey;
                }
                else if (!string.IsNullOrWhiteSpace(siteUrlKey))
                {
                    sectionType = "SiteUrl";
                    key = siteUrlKey;
                }

                collection = ConfigurationManager.GetSection($"TestConfigs/{sectionType}") as NameValueCollection;
            }
            catch (Exception e)
            {
                LogInfo($"Exception occured in GetValueFromConfigManager method - ", e);
            }
            return collection[$"{key}"];
        }



    }
}
