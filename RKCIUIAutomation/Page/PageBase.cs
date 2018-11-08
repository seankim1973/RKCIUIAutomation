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
        internal Hashtable Hashtable;

        internal Hashtable GetHashTable() => Hashtable ?? new Hashtable();

        public void CreateVar<T>(string key, T value)
        {
            try
            {
                Hashtable = GetHashTable();
                Hashtable.Add(key, value);
                log.Debug($"Added to HashTable: Key: {key.ToString()} : Value: {value.ToString()}");
            }
            catch (Exception e)
            {
                log.Error($"Error occured while adding to HashTable \n{e.Message}");
                throw;
            }
        }

        public void UpdateVar<T>(string key, T newValue)
        {
            Hashtable = GetHashTable();
            string logMsg = string.Empty;

            if (Hashtable.ContainsKey(key))
            {
                Hashtable[key] = newValue;
                logMsg = $"Added to Hashtable key: {key} : new value: {newValue}";
            }
            else
            {
                logMsg = $"Key: {key} does not exist in hashtable";
            }

            log.Debug(logMsg);
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