using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter.Search
{
    public abstract class Search_Impl : PageBase, ISearch
    {
        private static ProjectName tenant;

        public Search_Impl() { }
        public Search_Impl(IWebDriver driver)
        {
            Driver = driver;
            SetPageClassBasedOnTenant(driver);
        }

        public abstract void EnterDate_From(string fromDate);
        public abstract void EnterDate_To(string toDate);
        public abstract void EnterText_Attention(string text);
        public abstract void EnterText_From(string text);
        public abstract void EnterText_MSLNumber(string text);
        public abstract void EnterText_Number(string text);
        public abstract void EnterText_OriginatorDocumentRef(string text);
        public abstract void EnterText_Title(string text);
        public abstract void EnterText_TransmittalNumber(string text);
        public abstract void PopulateAllSearchCriteriaFields();
        public abstract void SelectDDL_Category<T>(T itemIndexOrName);
        public abstract void SelectDDL_DocumentType<T>(T itemIndexOrName);
        public abstract void SelectDDL_SegmentArea<T>(T itemIndexOrName);
        public abstract void SelectDDL_Status<T>(T itemIndexOrName);

        public static object SetPageClassBasedOnTenant(IWebDriver driver)
        {
            object classInstance;
            tenant = projectName;

            if (tenant == ProjectName.GLX)
            {
                Console.WriteLine("###### using Search_GLX instance");
                Search_GLX instance = new Search_GLX();
                classInstance = instance.GetGLXInstance(driver);
            }
            else if (tenant == ProjectName.I15Tech)
            {
                Console.WriteLine("###### using Search_I15Tech instance");
                Search_I15Tech instance = new Search_I15Tech();
                classInstance = instance.GetI15TechInstance(driver);
            }
            else
            {
                Console.WriteLine("###### using Search_Base instance");
                Search instance = new Search();
                classInstance = instance.GetCommonInstance(driver);
            }

            return classInstance;
        }
    }
}
