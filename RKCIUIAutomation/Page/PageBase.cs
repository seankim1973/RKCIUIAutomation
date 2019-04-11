using MiniGuids;
using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Base;
using System;
using System.Collections;

namespace RKCIUIAutomation.Page
{
    public class PageBase : PageBaseHelper
    {
        public PageBase()
        {
        }

        public PageBase(IWebDriver driver) => this.Driver = driver;

        public PageHelper PageHelper => new PageHelper();
    }

    public class PageBaseHelper : TableHelper
    {
        public PageBaseHelper()
        {
        }

        public PageBaseHelper(IWebDriver driver) => this.Driver = driver;

        [ThreadStatic]
        internal static Hashtable Hashtable;

        internal Hashtable GetHashTable() => Hashtable ?? new Hashtable();

        public string GenerateRandomGuid()
        {
            MiniGuid guid = MiniGuid.NewGuid();
            return guid;
        }

        public void CreateVar<T>(T key, string value = "", bool withPrefix = true)
        {
            try
            {
                string logMsg = string.Empty;
                object argKey = null;

                Type argType = key.GetType();

                if (argType.Equals(typeof(Enum)))
                {
                    argKey = ConvertToType<Enum>(key);
                    argKey.ToString();
                }
                else
                {
                    argKey = ConvertToType<string>(key);
                }

                argKey = withPrefix
                    ? BaseHelper.GetEnvVarPrefix((string)argKey)
                    : argKey;

                value = value.HasValue()
                    ? value
                    : GenerateRandomGuid();

                Hashtable = GetHashTable();

                if (!HashKeyExists((string)argKey))
                {
                    Hashtable.Add(argKey, value);
                    logMsg = "Created";
                }
                else
                {
                    Hashtable[argKey] = value;
                    logMsg = "Updated";
                }

                log.Debug($"{logMsg} HashTable - Key: {argKey.ToString()} : Value: {value.ToString()}");
            }
            catch (Exception e)
            {
                log.Error($"Error occured while adding to HashTable \n{e.Message}");
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetVar<T>(T key, bool keyIncludesPrefix = false)
        {
            Type argType = key.GetType();
            string argKey = string.Empty;

            if (argType.Equals(typeof(Enum)))
            {
                argKey = (ConvertToType<Enum>(key)).ToString();
            }
            else
            {
                argKey = ConvertToType<string>(key);
            }

            argKey = keyIncludesPrefix
                ? argKey
                : BaseHelper.GetEnvVarPrefix(argKey);

            if (!HashKeyExists(argKey))
            {
                CreateVar(argKey, "", false);
            }

            Hashtable = GetHashTable();
            var varValue = Hashtable[argKey].ToString();
            log.Debug($"#####GetVar Key: {argKey} has Value: {varValue}");
            
            return varValue;
        }

        public bool HashKeyExists(string key)
        {
            Hashtable = GetHashTable();
            return Hashtable.ContainsKey(key);
        }
    }
}