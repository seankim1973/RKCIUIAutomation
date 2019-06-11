using System;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System.Collections.Generic;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.Search;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.ProjectCorrespondenceLog;
using ColumnName = RKCIUIAutomation.Page.PageObjects.RMCenter.Search.ColumnName;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    #region Search Generic class

    public class Search : Search_Impl
    {
        public Search()
        {
        }

        public Search(IWebDriver driver)
        {
            this.Driver = driver;
            tenantSearchCriteriaFields = SetTenantSearchCriteriaFields();
            tenantSearchGridColumnNames = SetTenantSearchGridColumnNames();
        }

        public override T SetClass<T>(IWebDriver driver)
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
            return (T)instance;
        }


        [ThreadStatic]
        public static IList<SearchCriteria> tenantSearchCriteriaFields;

        [ThreadStatic]
        public static IList<ColumnName> tenantSearchGridColumnNames;


        public enum SearchCriteria
        {
            [StringValue("", "")] NoSelection,
            [StringValue("SelectedType", DDL)] DocumentType,
            [StringValue("SelectedStatus", DDL)] Status,
            [StringValue("SelectedCategory", DDL)] Category,
            [StringValue("SelectedSegmentArea", DDL)] SegmentArea,
            [StringValue("Title", TEXT)] Title,
            [StringValue("OriginatorDocumentRef" , TEXT)] OriginatorDocumentRef,
            [StringValue("Number", TEXT)] Number,
            [StringValue("TransmittalNumber", TEXT)] TransmittalNumber,
            [StringValue("From", TEXT)] From,
            [StringValue("MSLNo", TEXT)] MSLNo, //GLX
            [StringValue("OwnerNumber", TEXT)] Owner_MSLNumber, //I15SB, SH249, SG
            [StringValue("Attention", TEXT)] Attention,
            [StringValue("TransmittalFromDate", DATE)] TransmittalDate_From,
            [StringValue("TransmittalToDate", DATE)] TransmittalDate_To,
            [StringValue("SelectedSpecSection", DDL)] SpecSection,
            [StringValue("SelectedTransmissionResponseOwner", DDL)] OwnerResponse,
            [StringValue("DesignPackagesIdsNcr", MULTIDDL)] DesignPackages
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

        public override void EnterDate_From(string fromDate)
            => PageAction.EnterText(GetTextInputFieldByLocator(SearchCriteria.TransmittalDate_From), fromDate);

        public override void EnterDate_To(string toDate)
            => PageAction.EnterText(GetTextInputFieldByLocator(SearchCriteria.TransmittalDate_To), toDate);

        public override void ClickBtn_Search()
            => PageAction.JsClickElement(By.Id("SearchButton"));

        public override void ClickBtn_Clear()
            => PageAction.JsClickElement(By.Id("ClearButton"));

        public override bool VerifySearchResultByCriteria(string transmittalNumber, IList<KeyValuePair<EntryField, string>> entryFieldValuesList)
        {
            bool isDisplayed = false;
            string logMsg = string.Empty;

            try
            {
                SearchCriteria criteria;              
                IList<bool> resultsList = new List<bool>();

                foreach (KeyValuePair<EntryField, string> kvPair in entryFieldValuesList)
                {
                    criteria = GetMatchingSearchCriteriaForEntryField(kvPair.Key);

                    if (tenantSearchCriteriaFields.Contains(criteria))
                    {
                        if (!criteria.Equals(SearchCriteria.NoSelection))
                        {
                            PopulateCriteriaByType(criteria, kvPair.Value);
                            ClickBtn_Search();
                            //PageAction.WaitForLoading();
                            PageAction.WaitForPageReady();
                            bool searchResult = GridHelper.VerifyRecordIsDisplayed(ColumnName.TransmittalNumber, transmittalNumber, TableHelper.TableType.Single);
                            resultsList.Add(searchResult);

                            logMsg = $"Search by Criteria '{criteria}'";
                            Report.Info($"{logMsg}  was {(searchResult ? "" : "NOT ")}successful", searchResult);
                            TestUtility.AddAssertionToList(searchResult, logMsg);

                            ClickBtn_Clear();
                            //PageAction.WaitForLoading();
                            PageAction.WaitForPageReady();
                        }
                    }
                }

                isDisplayed = resultsList.Contains(false)
                    ? false
                    : true;
            }
            catch (Exception)
            {
                log.Error(logMsg);
            }

            return isDisplayed;
        }

        private SearchCriteria GetMatchingSearchCriteriaForEntryField(EntryField entryField)
        {
            SearchCriteria criteria = SearchCriteria.NoSelection;

            switch (entryField)
            {
                case EntryField.Attention:
                    criteria = SearchCriteria.Attention;
                    break;
                case EntryField.Date:
                    criteria = SearchCriteria.TransmittalDate_From;
                    break;
                case EntryField.DesignPackages:
                    criteria = SearchCriteria.DesignPackages;
                    break;
                case EntryField.DocumentType:
                    criteria = SearchCriteria.DocumentType;
                    break;
                case EntryField.DocumentCategory:
                    criteria = SearchCriteria.Category;
                    break;
                case EntryField.From:
                    criteria = SearchCriteria.From;
                    break;
                case EntryField.MSLNumber:
                    criteria = SearchCriteria.MSLNo;
                    break;
                case EntryField.OriginatorDocumentRef:
                    criteria = SearchCriteria.OriginatorDocumentRef;
                    break;
                case EntryField.Segment_Area:
                    criteria = SearchCriteria.SegmentArea;
                    break;
                case EntryField.SpecSection:
                    criteria = SearchCriteria.SpecSection;
                    break;
                case EntryField.Title:
                    criteria = SearchCriteria.Title;
                    break;
                case EntryField.TransmittalNumber:
                    criteria = SearchCriteria.TransmittalNumber;
                    break;
            }

            return criteria;
        }

        private void PopulateCriteriaByType(SearchCriteria criteria, string fieldValue)
        {
            string fieldType = criteria.GetString(true);

            if (fieldType.Equals(TEXT))
            {
                if (criteria.Equals(SearchCriteria.MSLNo))
                {
                    EnterCriteria_MSLNumber(fieldValue);
                }
                else if (criteria.Equals(SearchCriteria.DocumentType))
                {
                    SelectDDL_DocumentType(fieldValue);
                    EnterCriteria_Number(fieldValue);
                }
                else
                {
                    PageAction.EnterText(By.Id(criteria.GetString()), fieldValue);
                }
            }
            else if (fieldType.Equals(DATE))
            {
                EnterDate_From(GetPastShortDate(fieldValue));
                EnterDate_To(fieldValue);
            }
            else if (fieldType.Equals(DDL) || fieldType.Equals(MULTIDDL))
            {
                fieldValue = criteria.Equals(SearchCriteria.DocumentType)
                    ? $"-- {fieldValue}"
                    : fieldValue;
                PageAction.ExpandAndSelectFromDDList(criteria, fieldValue, true, fieldType.Equals(MULTIDDL) ? true : false);
            }
        }

    }

    #endregion Search Generic class

    #region Search Interface class

    public interface ISearch
    {
        IList<SearchCriteria> SetTenantSearchCriteriaFields();

        IList<ColumnName> SetTenantSearchGridColumnNames();

        bool VerifySearchResultByCriteria(string transmittalNumber, IList<KeyValuePair<EntryField, string>> entryFieldValuesList);

        void ClickBtn_Search();

        void ClickBtn_Clear();

        void SelectDDL_DocumentType<T>(T itemIndexOrName);

        void SelectDDL_Status<T>(T itemIndexOrName);

        void SelectDDL_Category<T>(T itemIndexOrName);

        void SelectDDL_SegmentArea<T>(T itemIndexOrName);

        void EnterText_Title(string text);

        void EnterText_Attention(string text);

        void EnterText_OriginatorDocumentRef(string text);

        void EnterText_TransmittalNumber(string text);

        void EnterText_From(string text);

        void EnterCriteria_Number(string text);

        void EnterCriteria_MSLNumber(string text);

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

    public abstract class Search_Impl : PageBase, ISearch
    {
        //Page workflow common to all tenants
        public virtual void SelectDDL_DocumentType<T>(T itemIndexOrName) => PageAction.ExpandAndSelectFromDDList(SearchCriteria.DocumentType, itemIndexOrName);

        public virtual void SelectDDL_Status<T>(T itemIndexOrName) => PageAction.ExpandAndSelectFromDDList(SearchCriteria.Status, itemIndexOrName);

        public virtual void EnterText_Title(string text) => PageAction.EnterText(GetTextInputFieldByLocator(SearchCriteria.Title), text);

        public virtual void EnterText_TransmittalNumber(string text) => PageAction.EnterText(GetTextInputFieldByLocator(SearchCriteria.TransmittalNumber), text);

        public virtual void EnterText_From(string text) => PageAction.EnterText(GetTextInputFieldByLocator(SearchCriteria.From), text);

        public virtual void EnterText_Attention(string text) => PageAction.EnterText(GetTextInputFieldByLocator(SearchCriteria.Attention), text);

        public abstract void EnterDate_From(string fromDate);

        public abstract void EnterDate_To(string toDate);

        public virtual void PopulateAllSearchCriteriaFields()
        {
        }

        //Not used in tenant(s): GLX & LAX
        public virtual void EnterCriteria_Number(string text)
            => PageAction.EnterText(GetTextInputFieldByLocator(SearchCriteria.Number), $"{text.ReplaceSpacesWithUnderscores()}0000");

        //For I15SB, SH249, SG
        public virtual void EnterCriteria_MSLNumber(string text)
            => PageAction.EnterText(GetTextInputFieldByLocator(SearchCriteria.Owner_MSLNumber), text);

        //Used only in GLX
        public virtual void SelectDDL_Category<T>(T itemIndexOrName) => PageAction.ExpandAndSelectFromDDList(SearchCriteria.Category, itemIndexOrName);

        public virtual void SelectDDL_SegmentArea<T>(T itemIndexOrName) => PageAction.ExpandAndSelectFromDDList(SearchCriteria.SegmentArea, itemIndexOrName);

        public virtual void EnterText_OriginatorDocumentRef(string text) => PageAction.EnterText(GetTextInputFieldByLocator(SearchCriteria.OriginatorDocumentRef), text);


        public abstract bool VerifySearchResultByCriteria(string transmittalNumber, IList<KeyValuePair<EntryField, string>> entryFieldValuesList);

        public abstract void ClickBtn_Search();

        public abstract void ClickBtn_Clear();

        public virtual IList<SearchCriteria> SetTenantSearchCriteriaFields()
            => tenantSearchCriteriaFields;

        public virtual IList<ColumnName> SetTenantSearchGridColumnNames()
            => tenantSearchGridColumnNames;
    }

    #endregion Search Common Implementation class

    /// <summary>
    /// Tenant specific implementation of RMCenter Search
    /// </summary>
    /// 
    
    #region Implementation specific to Garnet

    public class Search_Garnet : Search
    {
        public Search_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to Garnet

    #region Implementation specific to SGWay

    public class Search_SGWay : Search
    {
        public Search_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override IList<SearchCriteria> SetTenantSearchCriteriaFields()
        {
            if (tenantSearchCriteriaFields == null)
            {
                tenantSearchCriteriaFields = new List<SearchCriteria>()
                {
                    SearchCriteria.Attention,
                    SearchCriteria.DocumentType,
                    SearchCriteria.From,
                    SearchCriteria.Number,
                    SearchCriteria.Owner_MSLNumber,
                    SearchCriteria.Status,
                    SearchCriteria.Title,
                    SearchCriteria.TransmittalNumber
                };
            }
            return tenantSearchCriteriaFields;
        }

        public override IList<ColumnName> SetTenantSearchGridColumnNames()
        {
            if (tenantSearchGridColumnNames == null)
            {
                tenantSearchGridColumnNames = new List<ColumnName>()
                {
                    ColumnName.Attention,
                    ColumnName.From,
                    ColumnName.MSLNo,
                    ColumnName.Number,
                    ColumnName.Title,
                    ColumnName.TransmittalNumber
                };
            }
            return tenantSearchGridColumnNames;
        }
    }

    #endregion Implementation specific to SGWay

    #region Implementation specific to SH249

    public class Search_SH249 : Search
    {
        public Search_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override IList<SearchCriteria> SetTenantSearchCriteriaFields()
        {
            if (tenantSearchCriteriaFields == null)
            {
                tenantSearchCriteriaFields = new List<SearchCriteria>()
                {
                    SearchCriteria.Attention,
                    SearchCriteria.DocumentType,
                    SearchCriteria.From,
                    SearchCriteria.Number,
                    SearchCriteria.Owner_MSLNumber,
                    SearchCriteria.Status,
                    SearchCriteria.Title,
                    SearchCriteria.TransmittalNumber
                };
            }
            return tenantSearchCriteriaFields;
        }

        public override IList<ColumnName> SetTenantSearchGridColumnNames()
        {
            if (tenantSearchGridColumnNames == null)
            {
                tenantSearchGridColumnNames = new List<ColumnName>()
                {
                    ColumnName.Attention,
                    ColumnName.From,
                    ColumnName.MSLNo,
                    ColumnName.Number,
                    ColumnName.Title,
                    ColumnName.TransmittalNumber
                };
            }
            return tenantSearchGridColumnNames;
        }
    }

    #endregion Implementation specific to SH249

    #region Implementation specific to GLX

    public class Search_GLX : Search
    {
        public Search_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override void EnterCriteria_Number(string text)
            => log.Info("Search Criteria field 'Number' does not exist for Tenant GLX");

        public override void EnterCriteria_MSLNumber(string text)
            => PageAction.EnterText(GetTextInputFieldByLocator(SearchCriteria.MSLNo), text);

        public override void PopulateAllSearchCriteriaFields()
        {

        }

        public override IList<SearchCriteria> SetTenantSearchCriteriaFields()
        {
            if (tenantSearchCriteriaFields == null)
            {
                tenantSearchCriteriaFields = new List<SearchCriteria>()
                {
                    SearchCriteria.Attention,
                    SearchCriteria.Category,
                    SearchCriteria.DesignPackages,
                    SearchCriteria.DocumentType,
                    SearchCriteria.From,
                    SearchCriteria.MSLNo,
                    SearchCriteria.OriginatorDocumentRef,
                    SearchCriteria.OwnerResponse,
                    SearchCriteria.SegmentArea,
                    SearchCriteria.SpecSection,
                    SearchCriteria.Title,
                    SearchCriteria.TransmittalDate_From,
                    SearchCriteria.TransmittalNumber
                };
            }
            return tenantSearchCriteriaFields;
        }

        public override IList<ColumnName> SetTenantSearchGridColumnNames()
        {
            if (tenantSearchGridColumnNames == null)
            {
                tenantSearchGridColumnNames = new List<ColumnName>()
                {
                    ColumnName.Attention,
                    ColumnName.From,
                    ColumnName.OriginatorDocumentRef,
                    ColumnName.OwnerResponse,
                    ColumnName.OwnerResponseBy,
                    ColumnName.OwnerResponseDate,
                    ColumnName.SpecSection,
                    ColumnName.Title,
                    ColumnName.TransmittalDate,
                    ColumnName.TransmittalNumber
                };
            }
            return tenantSearchGridColumnNames;
        }
    }

    #endregion Implementation specific to GLX

    #region Implementation specific to I15South

    public class Search_I15South : Search
    {
        public Search_I15South(IWebDriver driver) : base(driver)
        {
        }

        public override IList<SearchCriteria> SetTenantSearchCriteriaFields()
        {
            if (tenantSearchCriteriaFields == null)
            {
                tenantSearchCriteriaFields = new List<SearchCriteria>()
                {
                    SearchCriteria.Attention,
                    SearchCriteria.DocumentType,
                    SearchCriteria.From,
                    SearchCriteria.Number,
                    SearchCriteria.Owner_MSLNumber,
                    SearchCriteria.Status,
                    SearchCriteria.Title,
                    SearchCriteria.TransmittalNumber
                };
            }
            return tenantSearchCriteriaFields;
        }
        public override IList<ColumnName> SetTenantSearchGridColumnNames()
        {
            if (tenantSearchGridColumnNames == null)
            {
                tenantSearchGridColumnNames = new List<ColumnName>()
                {
                    ColumnName.Attention,
                    ColumnName.From,
                    ColumnName.MSLNo,
                    ColumnName.Number,
                    ColumnName.Title,
                    ColumnName.TransmittalNumber
                };
            }
            return tenantSearchGridColumnNames;
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
        }

        public override IList<SearchCriteria> SetTenantSearchCriteriaFields()
        {
            if (tenantSearchCriteriaFields == null)
            {
                tenantSearchCriteriaFields = new List<SearchCriteria>()
                {
                    SearchCriteria.Attention,
                    SearchCriteria.DocumentType,
                    SearchCriteria.From,
                    SearchCriteria.Number,
                    SearchCriteria.Status,
                    SearchCriteria.Title,
                    SearchCriteria.TransmittalDate_From,
                    SearchCriteria.TransmittalNumber
                };
            }
            return tenantSearchCriteriaFields;
        }

        public override IList<ColumnName> SetTenantSearchGridColumnNames()
        {
            if (tenantSearchGridColumnNames == null)
            {
                tenantSearchGridColumnNames = new List<ColumnName>()
                {
                    ColumnName.Attention,
                    ColumnName.From,
                    ColumnName.Number,
                    ColumnName.Title,
                    ColumnName.TransmittalDate,
                    ColumnName.TransmittalNumber
                };
            }
            return tenantSearchGridColumnNames;
        }
    }

    #endregion Implementation specific to I15Tech

    #region Implementation specific to LAX

    public class Search_LAX : Search
    {
        public Search_LAX(IWebDriver driver) : base(driver)
        {
        }

        public override void EnterCriteria_Number(string text)
            => log.Info("Search Criteria field 'Number' does not exist for Tenant LAX");

        public override IList<SearchCriteria> SetTenantSearchCriteriaFields()
        {
            if (tenantSearchCriteriaFields == null)
            {
                tenantSearchCriteriaFields = new List<SearchCriteria>()
                {
                    SearchCriteria.Attention,
                    SearchCriteria.Category,
                    SearchCriteria.DocumentType,
                    SearchCriteria.From,
                    SearchCriteria.OriginatorDocumentRef,
                    SearchCriteria.SegmentArea,
                    SearchCriteria.Status,
                    SearchCriteria.Title,
                    SearchCriteria.TransmittalDate_From,
                    SearchCriteria.TransmittalNumber
                };
            }
            return tenantSearchCriteriaFields;
        }

        public override IList<ColumnName> SetTenantSearchGridColumnNames()
        {
            if (tenantSearchGridColumnNames == null)
            {
                tenantSearchGridColumnNames = new List<ColumnName>()
                {
                    ColumnName.Number,
                    ColumnName.OriginatorDocumentRef,
                    ColumnName.Title,
                    ColumnName.TransmittalDate,
                    ColumnName.TransmittalNumber
                };
            }
            return tenantSearchGridColumnNames;
        }
    }

    #endregion Implementation specific to LAX
}