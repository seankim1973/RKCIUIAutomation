using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter.Search
{
    public class Search_GLX : Search
    {
        public Search_GLX() { }

        public Search_GLX(IWebDriver driver) => Driver = driver;

        public Search_GLX GetGLXInstance(IWebDriver driver) => new Search_GLX(driver);

        public override void PopulateAllSearchCriteriaFields()
        {
            SelectDDL_DocumentType(1);
            EnterText_Title("GLX Test Title");
            EnterText_TransmittalNumber("GLX Test1234");
            EnterText_From("GLX From Test");
            EnterText_Attention("GLX Test Attention");
            SelectDDL_SegmentArea(1);
            EnterDate_From("1/1/2018");
            EnterDate_To("6/6/2018");
            SelectDDL_Category(1);
            EnterText_OriginatorDocumentRef("GLX Test Originator Ref.");
        }
    }
}
