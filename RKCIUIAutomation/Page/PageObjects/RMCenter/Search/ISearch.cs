using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter.Search
{
    interface ISearch
    {
        By GetPageTitleByLocator();
        void SelectDDL_DocumentType<T>(T itemIndexOrName);
        void SelectDDL_Status<T>(T itemIndexOrName);
        void SelectDDL_Category<T>(T itemIndexOrName);
        void SelectDDL_SegmentArea<T>(T itemIndexOrName);
        void EnterText_Title(string text);
        void EnterText_Attention(string text);
        void EnterText_OriginatorDocumentRef(string text);
        void EnterText_TransmittalNumber(string text);
        void EnterText_From(string text);
        void EnterText_Number(string text);
        void EnterText_MSLNumber(string text);
        void EnterDate_From(string fromDate);
        void EnterDate_To(string toDate);

        //Workflow Interface
        void PopulateAllSearchCriteriaFields();
    }
}
