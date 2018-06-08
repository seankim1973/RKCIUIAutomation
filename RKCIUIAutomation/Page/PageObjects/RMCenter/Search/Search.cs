using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter.Search
{
    public abstract class Search : PageBase, ISearch
    {
        private static ProjectName tenant;

        //public Search() { }
        //public Search(IWebDriver driver) => Driver = driver;
        public static T SetClass<T>() => (T)SetPageClassBasedOnTenant();

        public abstract By GetPageTitleByLocator();
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

        private static object SetPageClassBasedOnTenant()
        {
            tenant = projectName;
            
            var instance = new Search_Impl();

            if (tenant == ProjectName.GLX)
            {
                Console.WriteLine("###### using Search_GLX instance");
                instance = new Search_GLX();
            }
            else if (tenant == ProjectName.I15Tech)
            {
                Console.WriteLine("###### using Search_I15Tech instance");
                instance = new Search_I15Tech();
            }
            else
                Console.WriteLine("###### using Search_Impl (Common) instance");

            return instance;
        }
        
        internal enum SearchCriteria
        {
            [StringValue("SelectedType")] DocumentType,
            [StringValue("SelectedStatus")] Status,
            [StringValue("SelectedCategory")] Category, // +GLX
            [StringValue("SelectedSegmentArea")] SegmentArea, //+GLX
            [StringValue("Title")] Title,
            [StringValue("OriginatorDocumentRef")] OriginatorDocumentRef, //+GLX
            [StringValue("Number")] Number, //-GLX
            [StringValue("TransmittalNumber")] TransmittalNumber,
            [StringValue("From")] From,
            [StringValue("OwnerNumber")] MSLNumber, //-GLX, -I15Tech
            [StringValue("Attention")] Attention,
            [StringValue("TransmittalFromDate")] Transmittal_FromDate, //+GLX
            [StringValue("TransmittalToDate")] Transmittal_ToDate //+GLX
        }
    }
}
