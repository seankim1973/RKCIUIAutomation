using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.PageObjects
{
    public class RMCenter : PageBase
    {
        public class Search
        {
            public Search()
            { }

            public Search(IWebDriver driver) => Driver = driver;

            public static Search SearchPage { get => new Search(Driver); set { } }
        }
    }
}
