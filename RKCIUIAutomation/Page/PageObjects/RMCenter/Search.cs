using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Page.PageHelper;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    #region Search Generic class
    public class Search : Search_Impl
    {
        /// <summary>
        /// Common pageObjects and workflows are inherited from abstract _Impl class
        /// </summary>
    }
    #endregion end of Search Generic class


    #region Search Interface class
    public interface ISearch
    {
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
    #endregion end of Search Interface class


    #region Search Common Implementation class
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
        private static ISearch SetPageClassBasedOnTenant()
        {
            ISearch instance = new Search();

            if (projectName == ProjectName.SGWay)
            {
                LogInfo($"###### using Search_SGWay instance ###### ");
                instance = new Search_SGWay();
            }
            else if (projectName == ProjectName.SH249)
            {
                LogInfo($"###### using Search_SH249 instance ###### ");
                instance = new Search_SH249();
            }
            else if (projectName == ProjectName.Garnet)
            {
                LogInfo($"###### using Search_Garnet instance ###### ");
                instance = new Search_Garnet();
            }
            else if (projectName == ProjectName.GLX)
            {
                LogInfo($"###### using Search_GLX instance ###### ");
                instance = new Search_GLX();
            }
            else if (projectName == ProjectName.I15South)
            {
                LogInfo($"###### using Search_I15South instance ###### ");
                instance = new Search_I15South();
            }
            else if (projectName == ProjectName.I15Tech)
            {
                LogInfo($"###### using Search_I15Tech instance ###### ");
                instance = new Search_I15Tech();
            }
            return instance;
        }
    }
    #endregion end of Search Common Implementation class




    /// <summary>
    /// Tenant specific implementation of RMCenter Search
    /// </summary>

    #region Implementation specific to SGWay
    public class Search_SGWay : Search
    {
    }
    #endregion


    #region Implementation specific to SH249
    public class Search_SH249 : Search
    {
    }
    #endregion


    #region Implementation specific to Garnet
    public class Search_Garnet : Search
    {
    }
    #endregion


    #region Implementation specific to GLX
    public class Search_GLX : Search
    {
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
    #endregion


    #region Implementation specific to I15South
    public class Search_I15South : Search
    {
    }
    #endregion


    #region Implementation specific to I15Tech
    public class Search_I15Tech : Search
    {
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
    #endregion

}
