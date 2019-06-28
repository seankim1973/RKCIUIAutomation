using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Page;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using static RKCIUIAutomation.Tools.HipTestApi;
using static RKCIUIAutomation.Base.Factory;
using RestSharp.Extensions;

namespace RKCIUIAutomation.Config
{
    public class ConfigUtils : BaseUtils, IConfigUtils
    {
        public ConfigUtils()
        {
        }

        [ThreadStatic]
        static string _currentUserEmail = string.Empty;

        public ConfigUtils(IWebDriver driver) => this.Driver = driver;

        public TestRunEnv GetTestRunEnv<TestRunEnv>(string nUnitParam)
        => (TestRunEnv)Enum.Parse(typeof(TestRunEnv), nUnitParam);

        public string GetSiteUrl(TestEnv testEnv, TenantName tenant)
        {
            string siteUrl = string.Empty;

            if (testEnv.Equals(TestEnv.Dev))
            {
                siteUrl = BaseClass.tmpDevEnvIP;
            }
            else
            {
                string siteKey = $"{tenant}_{testEnv.GetString()}";
                siteUrl = GetValueFromConfigManager(siteUrlKey: siteKey);
            }

            return siteUrl;
        }

        //return string array of username[0] and password[1]
        public string[] GetUserCredentials(UserType userType)
        {
            string userKey = $"{userType}";
            string[] usernamePassword = GetValueFromConfigManager(userTypeKey: userKey).Split(',');
            return usernamePassword;
        }

        public void SetCurrentUserEmail(UserType userType)
        {
            string[] credentials = GetUserCredentials(userType);
            _currentUserEmail = credentials[0];
        }

        public string GetCurrentUserEmail()
            => _currentUserEmail;

        public string GetHipTestCreds(HipTestKey credType)
            => GetValueFromConfigManager(hiptestKey: $"{credType}");

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

        public string GetEncryptedPW(string decryptedPW)
            => Encrypt(decryptedPW);

        public string GetDecryptedPW(string encryptedPW)
            => Decrypt(encryptedPW);

        private string GetCryptoHash() => GetValueFromConfigManager(userTypeKey: "Hash");

        private string Encrypt(string decryptedPw)
        {
            string encryptedPw = string.Empty;

            byte[] data = Encoding.UTF8.GetBytes(decryptedPw);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(GetCryptoHash()));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    encryptedPw = Convert.ToBase64String(results, 0, results.Length);
                }
            }

            return encryptedPw;
        }

        private string Decrypt(string encryptedPw)
        {
            string decryptedPw = string.Empty;

            byte[] data = Convert.FromBase64String(encryptedPw);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(GetCryptoHash()));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    decryptedPw = Encoding.UTF8.GetString(results, 0, results.Length);
                }
            }

            return decryptedPw;
        }
    }
}