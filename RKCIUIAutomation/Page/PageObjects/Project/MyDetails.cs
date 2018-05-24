using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.PageObjects.Project
{
    public class MyDetails : Project
    {
        public MyDetails()
        { }
        public MyDetails(IWebDriver driver) => Driver = driver;
        public static MyDetails MyDetailsPage { get => new MyDetails(Driver); set { } }

    }
}
