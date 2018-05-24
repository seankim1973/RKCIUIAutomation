using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.PageObjects.Project
{
    public class Project : PageBase
    {
        public Project()
        { }
        public Project(IWebDriver driver) => Driver = driver;
    }
}
