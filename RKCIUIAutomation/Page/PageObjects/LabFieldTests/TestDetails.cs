using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RKCIUIAutomation.Page.Action;

namespace RKCIUIAutomation.Page.PageObjects.LabFieldTests
{
    public class TestDetails
    {
        public static readonly By TestDetailsFormByLocator = By.Id("StartDiv");

        public static bool VerifyTestDetailsFormIsDisplayed()
        {
            return IsElementDisplayed(TestDetailsFormByLocator);
        }
        
    }
}
