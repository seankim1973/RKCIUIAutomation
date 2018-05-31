using System;
using System.Collections.Specialized;
using System.Configuration;

using static RKCIUIAutomation.Base.BaseUtils;

namespace RKCIUIAutomation.Config
{
    public static class ConfigUtil
    {
        public static string GetSiteUrl(TestEnv testEnv, ProjectName project)
        {
            string siteKey = $"{project}_{testEnv}";
            return GetValueFromConfigManager(siteUrlKey:siteKey);
        }

        //return string array of username[0] and password[1]
        public static string[] GetUser(UserType userType)
        {
            string userKey = $"{userType}";
            string[] usernamePassword = GetValueFromConfigManager(userTypeKey:userKey).Split(',');
            return usernamePassword;
        }

        private static string GetValueFromConfigManager(string siteUrlKey = "", string userTypeKey = "")
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
