using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;

namespace RKCIUIAutomation.Config
{    
    public class ConfigUtil
    {
        public string GetSiteUrl(TestEnv testEnv, Project project)
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

        internal string GetValueFromConfigManager(string siteUrlKey = "", string userTypeKey = "")
        {
            string sectionType = null;
            string key = null;

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

            var collection = ConfigurationManager.GetSection($"TestConfigs/{sectionType}") as NameValueCollection;
            return collection[$"{key}"];
        }
    }
}
