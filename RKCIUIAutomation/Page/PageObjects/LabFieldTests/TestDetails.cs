using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RKCIUIAutomation.Page.Action;

namespace RKCIUIAutomation.Page.PageObjects.LabFieldTests
{
    public class TestDetails : PageBase
    {
        public TestDetails(IWebDriver driver) => this.driver = driver;

        public readonly By TestDetailsFormByLocator = By.Id("StartDiv");

        public bool VerifyTestDetailsFormIsDisplayed() => IsElementDisplayed(TestDetailsFormByLocator);

    }
}
