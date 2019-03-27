using MiniGuids;
using OpenQA.Selenium;
using RestSharp.Extensions;
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

        public void CreateVar(string key, string value = "")
        {
            string logMsg;

            value = value.HasValue()
                ? value
                : GenerateRandomGuid();

            try
            {
                Hashtable = GetHashTable();
                if (!HashKeyExists(key))
                {
                    Hashtable.Add(key, value);
                    logMsg = "Created";
                }
                else
                {
                    Hashtable[key] = value;
                    logMsg = "Updated";
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

            if (!Hashtable.ContainsKey(key))
            {
                CreateVar(key);
            }

            varValue = Hashtable[key].ToString();
            log.Debug($"#####GetVar Key: {key} has Value: {varValue}");
            
            return varValue;
        }

        public bool HashKeyExists(string key)
        {
            Hashtable = GetHashTable();
            return Hashtable.ContainsKey(key);
        }
    }
}