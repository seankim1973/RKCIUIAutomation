using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Page.PageHelper;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter.Search
{
    public class Search : Search_Impl, ISearch
    {
        public Search() { }

        public Search(IWebDriver driver) => Driver = driver;
        public Search GetCommonInstance(IWebDriver driver) => new Search(driver);

        private enum SearchCriteria
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

        public static By Txt_PageTitle { get; } = By.XPath("//h3");

        //Page workflow common to all tenants
        public override void SelectDDL_DocumentType<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.DocumentType, itemIndexOrName);
        public override void SelectDDL_Status<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.Status, itemIndexOrName);
        public override void EnterText_Title(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Title), text);
        public override void EnterText_TransmittalNumber(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.TransmittalNumber), text);
        public override void EnterText_From(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.From), text);
        public override void EnterText_Attention(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Attention), text);

        //Not used in tenant(s): GLX
        public override void EnterText_Number(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Number), text);
        //Not used in tenant(s): GLX, I15Tech
        public override void EnterText_MSLNumber(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.MSLNumber), text);


        //Used only in GLX
        public override void SelectDDL_Category<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.Category, itemIndexOrName);
        public override void SelectDDL_SegmentArea<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.SegmentArea, itemIndexOrName);
        public override void EnterText_OriginatorDocumentRef(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.OriginatorDocumentRef), text);

        /// <summary>
        /// Date value in string format (i.e. MM/DD/YYYY)
        /// </summary>
        /// <param name="date"></param>
        public override void EnterDate_From(string fromDate) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Transmittal_FromDate), fromDate);

        /// <summary>
        /// Date value in string format (i.e. MM/DD/YYYY)
        /// </summary>
        /// <param name="toDate"></param>
        public override void EnterDate_To(string toDate) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Transmittal_ToDate), toDate);

        public override void PopulateAllSearchCriteriaFields()
        {
            SelectDDL_DocumentType(1);
            EnterText_Number("Common Test Number");
            EnterText_TransmittalNumber("Common Test Transmittal Number");
            EnterText_Title("Common Test Title");
            EnterText_From("From Common Test");
            EnterText_MSLNumber("Common Test MSL Number");
            EnterText_Attention("Attention Common Test");
        }

        
    }
}
