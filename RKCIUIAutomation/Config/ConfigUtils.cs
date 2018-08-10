using System;
using System.Collections.Specialized;
using System.Configuration;
using static RKCIUIAutomation.Base.BaseUtils;

namespace RKCIUIAutomation.Config
{
    public class ConfigUtils : ProjectProperties
    {
        public TestPlatform GetTestPlatform(string nunitArg) => (TestPlatform)Enum.Parse(typeof(TestPlatform), nunitArg);
        public BrowserType GetBrowserType(string nunitArg) => (BrowserType)Enum.Parse(typeof(BrowserType), nunitArg);
        public TestEnv GetTestEnv(string nunitArg) => (TestEnv)Enum.Parse(typeof(TestEnv), nunitArg);
        public TenantName GetTenantName(string nunitArg) => (TenantName)Enum.Parse(typeof(TenantName), nunitArg);
        public Reporter GetReporter(string nunitArg) => (Reporter)Enum.Parse(typeof(Reporter), nunitArg);

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
                log.Error($"Exception occured in GetValueFromConfigManager method - ", e);
            }
            return collection[$"{key}"];
        }
    }
}
