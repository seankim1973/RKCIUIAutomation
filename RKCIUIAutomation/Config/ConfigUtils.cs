﻿using RKCIUIAutomation.Page;
using System;
using System.Collections.Specialized;
using System.Configuration;
using static RKCIUIAutomation.Base.BaseUtils;
using static RKCIUIAutomation.Tools.HipTestApi;

namespace RKCIUIAutomation.Config
{
    public class ConfigUtils : ProjectProperties
    {
        public TestRunEnv GetTestRunEnv<TestRunEnv>(string nunitArg) => (TestRunEnv)Enum.Parse(typeof(TestRunEnv), nunitArg);
        
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

        public string GetHipTestCreds(HipTestKey credType)
        {
            string credKey = $"{credType}2";
            return GetValueFromConfigManager(hiptestKey: credKey);
        }

        private string GetValueFromConfigManager(string hiptestKey = "", string siteUrlKey = "", string userTypeKey = "")
        {
            string sectionType = null;
            string key = null;
            NameValueCollection collection = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(hiptestKey))
                {
                    sectionType = "HipTest";
                    key = hiptestKey;
                }
                else if (!string.IsNullOrWhiteSpace(userTypeKey))
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
            return collection[key];
        }
    }
}
