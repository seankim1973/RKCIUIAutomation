using OpenQA.Selenium;
using RKCIUIAutomation.Page;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using static RKCIUIAutomation.Base.BaseUtils;
using static RKCIUIAutomation.Tools.HipTestApi;

namespace RKCIUIAutomation.Config
{
    public class ConfigUtils : ProjectProperties
    {
        public ConfigUtils()
        {
        }

        public ConfigUtils(IWebDriver driver) => this.Driver = driver;

        public TestRunEnv GetTestRunEnv<TestRunEnv>(string nunitArg) => (TestRunEnv)Enum.Parse(typeof(TestRunEnv), nunitArg);

        public string GetSiteUrl(TestEnv testEnv, TenantName project)
        {
            string siteKey = $"{project}_{testEnv.GetString()}";
            return GetValueFromConfigManager(siteUrlKey: siteKey);
        }

        //return string array of username[0] and password[1]
        public string[] GetUser(UserType userType)
        {
            string userKey = $"{userType}";
            string[] usernamePassword = GetValueFromConfigManager(userTypeKey: userKey).Split(',');
            return usernamePassword;
        }

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

        internal string GetEncryptedPW(string decryptedPW)
            => Encrypt(decryptedPW);

        internal string GetDecryptedPW(string encryptedPW)
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