using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.Project
{
    public class MyDetails : PageBase
    {
        public MyDetails()
        {

        }
        public MyDetails(IWebDriver driver)
        {

        }
        public static MyDetails MyDetailsPage { get => new MyDetails(Driver); set { } }

    }
}
