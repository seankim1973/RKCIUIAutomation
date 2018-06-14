using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Page.PageHelper;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter.Search
{
    public abstract class Search_Impl : PageBase, ISearch
    {       
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

        //Page workflow common to all tenants
        public virtual void SelectDDL_DocumentType<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.DocumentType, itemIndexOrName);
        public virtual void SelectDDL_Status<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.Status, itemIndexOrName);
        public virtual void EnterText_Title(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Title), text);
        public virtual void EnterText_TransmittalNumber(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.TransmittalNumber), text);
        public virtual void EnterText_From(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.From), text);
        public virtual void EnterText_Attention(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Attention), text);

        /// <summary>
        /// Date value in string format (i.e. MM/DD/YYYY)
        /// </summary>
        /// <param name="date"></param>
        public virtual void EnterDate_From(string fromDate) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Transmittal_FromDate), fromDate);

        /// <summary>
        /// Date value in string format (i.e. MM/DD/YYYY)
        /// </summary>
        /// <param name="toDate"></param>
        public virtual void EnterDate_To(string toDate) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Transmittal_ToDate), toDate);

        public virtual void PopulateAllSearchCriteriaFields()
        {
            SelectDDL_DocumentType(1);
            EnterText_Number("Common Test Number");
            EnterText_TransmittalNumber("Common Test Transmittal Number");
            EnterText_Title("Common Test Title");
            EnterText_From("From Common Test");
            EnterText_MSLNumber("Common Test MSL Number");
            EnterText_Attention("Attention Common Test");
        }

        //Not used in tenant(s): GLX
        public virtual void EnterText_Number(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Number), text);

        //Not used in tenant(s): GLX, I15Tech
        public virtual void EnterText_MSLNumber(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.MSLNumber), text);

        //Used only in GLX
        public virtual void SelectDDL_Category<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.Category, itemIndexOrName);
        public virtual void SelectDDL_SegmentArea<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.SegmentArea, itemIndexOrName);
        public virtual void EnterText_OriginatorDocumentRef(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.OriginatorDocumentRef), text);

        public static T SetClass<T>() => (T)SetPageClassBasedOnTenant();
        private static object SetPageClassBasedOnTenant()
        {            
            var instance = new Search();

            if (projectName == ProjectName.GLX)
            {
                LogInfo("###### using Search_GLX instance");
                instance = new Search_GLX();
            }
            else if (projectName == ProjectName.I15Tech)
            {
                LogInfo("###### using Search_I15Tech instance");
                instance = new Search_I15Tech();
            }
            else
                LogInfo("###### using Search_Impl (Common) instance");

            return instance;
        }
    }
}
