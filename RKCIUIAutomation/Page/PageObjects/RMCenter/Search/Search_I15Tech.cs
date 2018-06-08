using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter.Search
{
    public class Search_I15Tech : Search
    {
        public Search_I15Tech() { }
        public Search_I15Tech(IWebDriver driver) => Driver = driver;
        public Search_I15Tech GetI15TechInstance(IWebDriver driver) => new Search_I15Tech(driver);

        public override void PopulateAllSearchCriteriaFields()
        {
            SelectDDL_DocumentType(1);
            EnterText_Number("I15Tech Test Number");
            EnterText_From("From I15Tech Test");
            EnterText_Title("I15Tech Test Title");
            EnterText_Attention("I15Tech Attention");
            EnterText_TransmittalNumber("I15Tech Transmittal Number");
        }
    }
}
