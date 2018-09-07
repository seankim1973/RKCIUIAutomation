using OpenQA.Selenium;
using System;
using System.Collections;

namespace RKCIUIAutomation.Page
{
    public class PageBase : PageBaseHelper
    {
        public PageHelper PageHelper => new PageHelper();
        public PageBaseHelper HashMap => new PageBaseHelper();
    }

    public class PageBaseHelper : TableHelper
    {
        private Hashtable Hashtable { get; set; }
        private Hashtable GetHashTable() => Hashtable ?? new Hashtable();
      
        public void CreateVar<T>(string key, T value)
        {
            Hashtable = GetHashTable();
            Hashtable.Add(key, value);
        }

        public string GetVar(string key)
        {
            Hashtable = GetHashTable();
            var varValue = string.Empty;

            if (Hashtable.ContainsKey(key))
            {
                varValue = Hashtable[key].ToString();
            }
            else
            {
                log.Debug($"GetVar Key does not exist: {key}");
            }

            return varValue;
        }
    }
}
