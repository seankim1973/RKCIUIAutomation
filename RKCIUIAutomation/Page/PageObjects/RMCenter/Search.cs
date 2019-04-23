﻿using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System.Collections.Generic;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.Search;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.ProjectCorrespondenceLog;
using System;
using ColumnName = RKCIUIAutomation.Page.PageObjects.RMCenter.Search.ColumnName;
using RKCIUIAutomation.Base;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    #region Search Generic class

    public class Search : Search_Impl
    {
        public Search()
        {
            //tenantSearchCriteriaFields = RMCenterSearch.SetTenantSearchCriteriaFields();
            //tenantSearchGridColumnNames = RMCenterSearch.SetTenantSearchGridColumnNames();
        }

        public Search(IWebDriver driver) => this.Driver = driver;

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
            => EnterText(GetTextInputFieldByLocator(SearchCriteria.TransmittalDate_From), fromDate);

        public override void EnterDate_To(string toDate)
            => EnterText(GetTextInputFieldByLocator(SearchCriteria.TransmittalDate_To), toDate);

        public override void ClickBtn_Search()
            => JsClickElement(By.Id("SearchButton"));

        public override void ClickBtn_Clear()
            => JsClickElement(By.Id("ClearButton"));

        public override bool VerifySearchResultByCriteria(string transmittalNumber)
        {
            bool isDisplayed = false;
            string logMsg = string.Empty;

            try
            {
                SearchCriteria criteria;              
                IList<bool> resultsList = new List<bool>();

                foreach (KeyValuePair<EntryField, string> kvPair in tenantAllEntryFieldKeyValuePairs)
                {
                    criteria = GetMatchingSearchCriteriaForEntryField(kvPair.Key);

                    if (criteria != SearchCriteria.NoSelection)
                    {
                        PopulateCriteriaByType(criteria, kvPair.Value);
                        ClickBtn_Search();
                        WaitForLoading();
                        bool searchResult = VerifyRecordIsDisplayed(ColumnName.TransmittalNumber, transmittalNumber, TableType.Single);
                        resultsList.Add(searchResult);

                        logMsg = $"Search by Criteria '{criteria}'";
                        LogInfo($"{logMsg}  was {(searchResult ? "" : "NOT ")}successful", searchResult);
                        AddAssertionToList(searchResult, logMsg);

                        ClickBtn_Clear();
                        WaitForLoading();
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
            SearchCriteria criteria;

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
                default:
                    criteria = SearchCriteria.NoSelection;
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
                    RMCenterSearch.EnterCriteria_MSLNumber(fieldValue);
                }
                else if (criteria.Equals(SearchCriteria.DocumentType))
                {
                    RMCenterSearch.SelectDDL_DocumentType(fieldValue);
                    RMCenterSearch.EnterCriteria_Number(fieldValue);
                }
                else
                {
                    EnterText(By.Id(criteria.GetString()), fieldValue);
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
                ExpandAndSelectFromDDList(criteria, fieldValue, true, fieldType.Equals(MULTIDDL) ? true : false);
            }
        }

    }

    #endregion Search Generic class

    #region Search Interface class

    public interface ISearch
    {
        IList<SearchCriteria> SetTenantSearchCriteriaFields();

        IList<ColumnName> SetTenantSearchGridColumnNames();

        bool VerifySearchResultByCriteria(string transmittalNumber);

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

        public abstract void EnterDate_From(string fromDate);

        public abstract void EnterDate_To(string toDate);

        public virtual void PopulateAllSearchCriteriaFields()
        {
        }

        //Not used in tenant(s): GLX & LAX
        public virtual void EnterCriteria_Number(string text)
            => EnterText(GetTextInputFieldByLocator(SearchCriteria.Number), $"{text.ReplaceSpacesWithUnderscores()}0000");

        //For I15SB, SH249, SG
        public virtual void EnterCriteria_MSLNumber(string text)
            => EnterText(GetTextInputFieldByLocator(SearchCriteria.Owner_MSLNumber), text);

        //Used only in GLX
        public virtual void SelectDDL_Category<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.Category, itemIndexOrName);

        public virtual void SelectDDL_SegmentArea<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.SegmentArea, itemIndexOrName);

        public virtual void EnterText_OriginatorDocumentRef(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.OriginatorDocumentRef), text);


        public abstract bool VerifySearchResultByCriteria(string transmittalNumber);

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

    #region Implementation specific to GLX

    public class Search_GLX : Search
    {
        public Search_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override void EnterCriteria_Number(string text)
            => log.Info("Search Criteria field 'Number' does not exist for Tenant GLX");

        public override void EnterCriteria_MSLNumber(string text)
            => EnterText(GetTextInputFieldByLocator(SearchCriteria.MSLNo), text);

        public override void PopulateAllSearchCriteriaFields()
        {

        }

        public override IList<SearchCriteria> SetTenantSearchCriteriaFields()
            => tenantSearchCriteriaFields = new List<SearchCriteria>()
            {
                SearchCriteria.Category,

            };
        public override IList<ColumnName> SetTenantSearchGridColumnNames()
            => tenantSearchGridColumnNames = new List<ColumnName>()
            {

            };
        
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
    }

    #endregion Implementation specific to LAX
}