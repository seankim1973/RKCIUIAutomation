using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.Search;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    #region Search Generic class

    public class Search : Search_Impl
    {
        public Search()
        {
        }

        public Search(IWebDriver driver) => this.Driver = driver;

        public enum SearchCriteria
        {
            [StringValue("SelectedType")] DocumentType,
            [StringValue("SelectedStatus")] Status,
            [StringValue("SelectedCategory")] Category,
            [StringValue("SelectedSegmentArea")] SegmentArea,
            [StringValue("Title")] Title,
            [StringValue("OriginatorDocumentRef")] OriginatorDocumentRef,
            [StringValue("Number")] Number,
            [StringValue("TransmittalNumber")] TransmittalNumber,
            [StringValue("From")] From,
            [StringValue("MSLNo")] MSLNo, //GLX
            [StringValue("OwnerNumber")] Owner_MSLNumber, //I15SB, SH249, SG
            [StringValue("Attention")] Attention,
            [StringValue("TransmittalFromDate")] TransmittalDate_From,
            [StringValue("TransmittalToDate")] TransmittalDate_To,
            [StringValue("SelectedSpecSection")] SpecSection,
            [StringValue("SelectedTransmissionResponseOwner")] OwnerResponse,
            [StringValue("DesignPackagesIdsNcr")] DesignPackages
        }

        public enum ColumnName
        {
            [StringValue("Number")] Number,
            [StringValue("TransmittalNo")] TransmittalNumber,
            [StringValue("TransmittalDate")]TransmittalDate,
            [StringValue("Title")]Title,
            [StringValue("OriginatorDocumentRef")] OriginatorDocumentRef,
            [StringValue("MSLNo")] MSLNo,
            [StringValue("SpecSection")] SpecSection,
            [StringValue("Attention")] Attention,
            [StringValue("From")] From,
            [StringValue("OwnerResponse")] OwnerResponse,
            [StringValue("OwnerResponseDateLocal")] OwnerResponseDate,
            [StringValue("OwnerResponseBy")] OwnerResponseBy
        }

    }

    #endregion Search Generic class

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

        /// <summary>
        /// Date value in string format (i.e. MM/DD/YYYY)
        /// </summary>
        /// <param name="date"></param>
        void EnterDate_From(string fromDate);

        /// <summary>
        /// Date value in string format (i.e. MM/DD/YYYY)
        /// </summary>
        /// <param name="toDate"></param>
        void EnterDate_To(string toDate);

        //Workflow Interface
        void PopulateAllSearchCriteriaFields();
    }

    #endregion Search Interface class

    #region Search Common Implementation class

    public abstract class Search_Impl : TestBase, ISearch
    {
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private ISearch SetPageClassBasedOnTenant(IWebDriver driver)
        {
            ISearch instance = new Search(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using Search_SGWay instance ###### ");
                instance = new Search_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using Search_SH249 instance ###### ");
                instance = new Search_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using Search_Garnet instance ###### ");
                instance = new Search_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using Search_GLX instance ###### ");
                instance = new Search_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using Search_I15South instance ###### ");
                instance = new Search_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using Search_I15Tech instance ###### ");
                instance = new Search_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using Search_LAX instance ###### ");
                instance = new Search_LAX(driver);
            }
            return instance;
        }


        //Page workflow common to all tenants
        public virtual void SelectDDL_DocumentType<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.DocumentType, itemIndexOrName);

        public virtual void SelectDDL_Status<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.Status, itemIndexOrName);

        public virtual void EnterText_Title(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Title), text);

        public virtual void EnterText_TransmittalNumber(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.TransmittalNumber), text);

        public virtual void EnterText_From(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.From), text);

        public virtual void EnterText_Attention(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Attention), text);

        public virtual void EnterDate_From(string fromDate) => EnterText(GetTextInputFieldByLocator(SearchCriteria.TransmittalDate_From), fromDate);

        public virtual void EnterDate_To(string toDate) => EnterText(GetTextInputFieldByLocator(SearchCriteria.TransmittalDate_To), toDate);

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

        //For I15SB, SH249, SG
        public virtual void EnterText_MSLNumber(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Owner_MSLNumber), text);

        //Used only in GLX
        public virtual void SelectDDL_Category<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.Category, itemIndexOrName);

        public virtual void SelectDDL_SegmentArea<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.SegmentArea, itemIndexOrName);

        public virtual void EnterText_OriginatorDocumentRef(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.OriginatorDocumentRef), text);

    }

    #endregion Search Common Implementation class

    /// <summary>
    /// Tenant specific implementation of RMCenter Search
    /// </summary>

    #region Implementation specific to SGWay

    public class Search_SGWay : Search
    {
        public Search_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to SGWay

    #region Implementation specific to SH249

    public class Search_SH249 : Search
    {
        public Search_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to SH249

    #region Implementation specific to Garnet

    public class Search_Garnet : Search
    {
        public Search_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to Garnet

    #region Implementation specific to GLX

    public class Search_GLX : Search
    {
        public Search_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override void EnterText_MSLNumber(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.MSLNo), text);

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

    #endregion Implementation specific to GLX

    #region Implementation specific to I15South

    public class Search_I15South : Search
    {
        public Search_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15South

    #region Implementation specific to I15Tech

    public class Search_I15Tech : Search
    {
        public Search_I15Tech(IWebDriver driver) : base(driver)
        {
        }

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

    #endregion Implementation specific to I15Tech

    #region Implementation specific to LAX

    public class Search_LAX : Search
    {
        public Search_LAX(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to LAX
}