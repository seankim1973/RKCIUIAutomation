using OpenQA.Selenium;
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

        public void CreateVar<T>(string key, T value)
        {
            string logMsg;

            try
            {
                Hashtable = GetHashTable();
                if (!HashKeyExists(key))
                {
                    Hashtable.Add(key, value);
                    logMsg = "Added to";
                }
                else
                {
                    Hashtable[key] = value;
                    logMsg = "Updated value for existing key in";
                }

                log.Debug($"{logMsg} HashTable - Key: {key.ToString()} : Value: {value.ToString()}");
            }
            catch (Exception e)
            {
                log.Error($"Error occured while adding to HashTable \n{e.Message}");
                throw;
            }
        }

        public string GetVar(string key)
        {
            Hashtable = GetHashTable();
            var varValue = string.Empty;

            if (Hashtable.ContainsKey(key))
            {
                varValue = Hashtable[key].ToString();
                log.Debug($"Found GetVar Key: {key} with Value: {varValue}");
            }
            else
            {
                log.Debug($"GetVar Key does not exist: {key}");
            }

            return varValue;
        }

        public bool HashKeyExists(string key)
        {
            Hashtable = GetHashTable();
            return Hashtable.ContainsKey(key);
        }
    }
}