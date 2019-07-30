using MiniGuids;
using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.ProjectCorrespondenceLog;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.TableHelper;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public class ProjectCorrespondenceLog : ProjectCorrespondenceLog_Impl
    {
        public ProjectCorrespondenceLog()
        {
            tenantAllEntryFieldKeyValuePairs = GetTenantEntryFieldKVPairsList();
        }

        public ProjectCorrespondenceLog(IWebDriver driver)
        {
            this.Driver = driver;
            tenantTableTabs = GetTenantTableTabsList();
            reqFieldLocators = GetTenantRequiredFieldLocators();
            tenantAllEntryFields = GetTenantAllEntryFieldsList();
            tenantExpectedRequiredFields = GetTenantRequiredFieldsList();
            expectedEntryFieldsForTblColumns = GetTenantEntryFieldsForTableColumns();
        }

        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Tenant'
        /// </summary>
        public override T SetClass<T>(IWebDriver driver)
        {
            IProjectCorrespondenceLog instance = new ProjectCorrespondenceLog(driver);

            if (tenantName == TenantNameType.SGWay)
            {
                log.Info($"###### using ProjectCorrespondenceLog_SGWay instance ###### ");
                instance = new ProjectCorrespondenceLog_SGWay(driver);
            }
            else if (tenantName == TenantNameType.SH249)
            {
                log.Info($"###### using  ProjectCorrespondenceLog_SH249 instance ###### ");
                instance = new ProjectCorrespondenceLog_SH249(driver);
            }
            else if (tenantName == TenantNameType.Garnet)
            {
                log.Info($"###### using  ProjectCorrespondenceLog_Garnet instance ###### ");
                instance = new ProjectCorrespondenceLog_Garnet(driver);
            }
            else if (tenantName == TenantNameType.GLX)
            {
                log.Info($"###### using  ProjectCorrespondenceLog_GLX instance ###### ");
                instance = new ProjectCorrespondenceLog_GLX(driver);
            }
            else if (tenantName == TenantNameType.I15South)
            {
                log.Info($"###### using  ProjectCorrespondenceLog_I15South instance ###### ");
                instance = new ProjectCorrespondenceLog_I15South(driver);
            }
            else if (tenantName == TenantNameType.I15Tech)
            {
                log.Info($"###### using ProjectCorrespondenceLog_I15Tech instance ###### ");
                instance = new ProjectCorrespondenceLog_I15Tech(driver);
            }
            else if (tenantName == TenantNameType.LAX)
            {
                log.Info($"###### using ProjectCorrespondenceLog_LAX instance ###### ");
                instance = new ProjectCorrespondenceLog_LAX(driver);
            }
            return (T)instance;
        }


        //GLX and LAX - StringValue[0] = table tab name, StringValue[1] = Table content reference id
        public enum TableTabType
        {
            [StringValue("Unsent Transmissions", "TransmissionGridNew")] UnsentTransmissions,
            [StringValue("Pending Transmissions", "TransmissionGridPending")] PendingTransmissions,
            [StringValue("Transmitted Records", "TransmissionGridForwarded")] TransmittedRecords
        }

        public enum EntryFieldType
        {
            [StringValue("DocumentDate", DATE)] Date,
            [StringValue("TransmittalNo", TEXT)] TransmittalNumber,
            [StringValue("SecurityClassificationId", DDL)] SecurityClassification,
            [StringValue("Title", TEXT)] Title,
            [StringValue("From", TEXT)] From,
            [StringValue("AgencyFromId", DDL)] AgencyFrom,
            [StringValue("Attention", TEXT)] Attention,
            [StringValue("AgencyToId", DDL)] AgencyAttention,
            [StringValue("DocumentTypeCatogoryId", DDL)] DocumentCategory,
            [StringValue("DocumentTypeId", DDL)] DocumentType,
            [StringValue("OriginatorDocumentRef", TEXT)] OriginatorDocumentRef,
            [StringValue("Revision", TEXT)] Revision,
            [StringValue("SelectedTransmittedIds", MULTIDDL)] Transmitted,
            [StringValue("SegmentId", DDL)] Segment_Area,
            [StringValue("DesignPackagesIdsNcr", MULTIDDL)] DesignPackages,
            [StringValue("CdrlNumber", TEXT)] CDRL,
            [StringValue("ResponseRequiredRadioButton_True", RDOBTN)] ResponseRequired_Yes,
            [StringValue("ResponseRequiredRadioButton_False", RDOBTN)] ResponseRequired_No,
            [StringValue("ResponseRequiredDate", FUTUREDATE)] ResponseRequiredBy_Date,
            [StringValue("OwnerReponseId", DDL)] OwnerResponse,
            [StringValue("OwnerResponseBy", TEXT)] OwnerResponseBy,
            [StringValue("OwnerResponseDate", DATE)] OwnerResponseDate,
            [StringValue("SectionId", DDL)] SpecSection,
            [StringValue("MSLNo", TEXT)] MSLNumber,
            [StringValue("AvailableAccessItems", DDL)] Access,
            [StringValue("ViaId", DDL)] Via,
            [StringValue("AllowReshare", CHKBOX)] AllowResharing,
            [StringValue("TransmissionFiles", UPLOAD)] Attachments
        }

        public enum ColumnNameType
        {
            [StringValue("Id")] ID,
            [StringValue("MSLNo")] MSLNumber,
            [StringValue("TransmittalNo")] TransmittalNumber,
            [StringValue("DocumentDateDisplay")] Date,
            [StringValue("Attention")] Attention,
            [StringValue("From")] From,
            [StringValue("Title")] Title,
            [StringValue("DocumentType.Key")] DocumentType,
            [StringValue("OriginatorDocumentRef")] OriginatorRef,
            [StringValue("Revision")] Revision,
            [StringValue("TransmittedTypeNames")] TransmittedTypes,
            [StringValue("ViaType.DisplayName")] Via,
        }

        [ThreadStatic]
        internal static IList<TableTabType> tenantTableTabs;

        [ThreadStatic]
        internal static IList<By> reqFieldLocators;

        [ThreadStatic]
        internal static IList<EntryFieldType> tenantExpectedRequiredFields;

        [ThreadStatic]
        internal static IList<EntryFieldType> tenantAllEntryFields;

        [ThreadStatic]
        internal static IList<EntryFieldType> expectedEntryFieldsForTblColumns;

        [ThreadStatic]
        internal static IList<KeyValuePair<EntryFieldType, string>> tenantAllEntryFieldKeyValuePairs;

        #region //Entry field override Action methods

        public override void EnterText_Date(string shortDate = "")
            => PopulateFieldValue(EntryFieldType.Date, shortDate);

        public override void EnterText_TransmittalNumber(string value = "")
            => PopulateFieldValue(EntryFieldType.TransmittalNumber, value);

        public override void EnterText_Title(string value = "")
            => PopulateFieldValue(EntryFieldType.Title, value);

        public override void EnterText_From(string value = "")
            => PopulateFieldValue(EntryFieldType.From, value);

        public override void EnterText_Attention(string value = "")
            => PopulateFieldValue(EntryFieldType.Attention, value);

        public override void EnterText_OriginatorDocumentRef(string value = "")
            => PopulateFieldValue(EntryFieldType.OriginatorDocumentRef, value);

        public override void EnterText_Revision(string value = "")
            => PopulateFieldValue(EntryFieldType.Revision, value);

        public override void EnterText_CDRL(string value = "")
            => PopulateFieldValue(EntryFieldType.CDRL, value);

        public override void EnterText_ResponseRequiredByDate(string value = "")
            => PopulateFieldValue(EntryFieldType.ResponseRequiredBy_Date, value);

        public override void EnterText_OwnerResponseBy(string value = "")
            => PopulateFieldValue(EntryFieldType.OwnerResponseBy, value);

        public override void EnterText_OwnerResponseDate(string value = "")
            => PopulateFieldValue(EntryFieldType.OwnerResponseDate, value);

        public override void EnterText_MSLNumber(string value = "")
            => PopulateFieldValue(EntryFieldType.MSLNumber, value);

        public override void SelectDDL_Access<T>(T indexOrName)
            => PopulateFieldValue(EntryFieldType.Access, indexOrName);

        public override void SelectDDL_SpecSection<T>(T indexOrName)
            => PopulateFieldValue(EntryFieldType.SpecSection, indexOrName);

        public override void SelectDDL_OwnerResponse<T>(T indexOrName)
            => PopulateFieldValue(EntryFieldType.OwnerResponse, indexOrName);

        public override void SelectDDL_DesignPackages<T>(T indexOrName)
            => PopulateFieldValue(EntryFieldType.DesignPackages, indexOrName);

        public override void SelectDDL_SegmentArea<T>(T indexOrName)
            => PopulateFieldValue(EntryFieldType.Segment_Area, indexOrName);

        public override void SelectDDL_Transmitted<T>(T indexOrName)
            => PopulateFieldValue(EntryFieldType.Transmitted, indexOrName);

        public override void SelectDDL_DocumentCategory<T>(T indexOrName)
            => PopulateFieldValue(EntryFieldType.DocumentCategory, indexOrName);

        public override void SelectDDL_DocumentType<T>(T indexOrName)
            => PopulateFieldValue(EntryFieldType.DocumentType, indexOrName);

        public override void SelectDDL_AgencyAttention<T>(T indexOrName)
            => PopulateFieldValue(EntryFieldType.AgencyAttention, indexOrName);

        public override void SelectDDL_AgencyFrom<T>(T indexOrName)
            => PopulateFieldValue(EntryFieldType.AgencyFrom, indexOrName);

        public override void SelectDDL_SecurityClassification<T>(T indexOrName)
            => PopulateFieldValue(EntryFieldType.SecurityClassification, indexOrName);

        public override void SelectRdoBtn_ResponseRequired_Yes()
            => PopulateFieldValue(EntryFieldType.ResponseRequired_Yes, "");

        public override void SelectRdoBtn_ResponseRequired_No()
            => PopulateFieldValue(EntryFieldType.ResponseRequired_No, "");

        public override void SelectChkbox_AllowResharing()
            => PopulateFieldValue(EntryFieldType.AllowResharing, "");

        public override void ClickBtn_AddAccessItem()
            => PageAction.ClickElement(By.Id("AddAccessItem"));

        #endregion //Entry field override Action methods


        private string GetVarForEntryField(Enum fieldEnum)
            => GetVar(fieldEnum);

        private IList<string> GetAccessGroupsList()
            => PageAction.GetTextForElements(By.XPath("//div[@id='AccessGroups']//ul/li/span[2]"));

        public string GetValueFromEntryField(EntryFieldType entryField)
        {
            string fieldType = entryField.GetString(true);

            string fieldValue = string.Empty;

            if (fieldType.Equals(TEXT) || fieldType.Equals(DATE) || fieldType.Equals(FUTUREDATE))
            {
                fieldValue = PageAction.GetAttributeForElement(By.Id($"{entryField.GetString()}"), "value");
            }
            else if (fieldType.Equals(DDL) || fieldType.Equals(MULTIDDL))
            {
                if (fieldType.Equals(DDL))
                {
                    if (entryField.Equals(EntryFieldType.Access))
                    {
                        fieldValue = string.Join("::", GetAccessGroupsList().ToArray());
                    }
                    else
                    {
                        fieldValue = PageAction.GetTextFromDDL(entryField);
                    }
                }
                else
                {
                    fieldValue = string.Join("::", PageAction.GetTextFromMultiSelectDDL(entryField).ToArray());
                }
            }

            return fieldValue;
        }

        /// <summary>
        /// For &lt;T&gt;indexOrText argument, provide a (one-based index) integer value or a string value of a drop-down list item to be selected, OR a string value to be entered in a text field
        /// <para>
        /// Use (bool)useContains arg when selecting a DDList item with partial value for [T](string)indexOrText
        /// </para>
        /// <para>
        /// (bool)useContains arg defaults to false and is ignored if arg indexOrText is an Integer
        /// </para>
        /// <para>
        /// When a field is a DATE or FUTUREDATE type, the current short date will be entered by default.  Set futureDate boolean argument to true to set the Date field for the next day
        /// </para>
        ///</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entryField"></param>
        /// <param name="indexOrText"></param>
        private KeyValuePair<EntryFieldType, string> PopulateFieldValue<T>(EntryFieldType entryField, T indexOrText, bool useContains = false)
        {
            string fieldType = entryField.GetString(true);
            Type argType = indexOrText.GetType();
            object argValue = null;
            bool isValidArg = false;

            KeyValuePair<EntryFieldType, string> fieldValuePair;
            string fieldValue = string.Empty;

            try
            {
                if (argType == typeof(string))
                {
                    isValidArg = true;
                    argValue = ConvertToType<string>(indexOrText);
                }
                else if (argType == typeof(int))
                {
                    isValidArg = true;
                    argValue = ConvertToType<int>(indexOrText);
                }

                if (isValidArg)
                {
                    if (fieldType.Equals(TEXT) || fieldType.Equals(DATE) || fieldType.Equals(FUTUREDATE))
                    {
                        if (!((string)argValue).HasValue())
                        {
                            if (fieldType.Equals(DATE) || fieldType.Equals(FUTUREDATE))
                            {
                                argValue = fieldType.Equals(DATE)
                                    ? GetShortDate(formatWithZero:true)
                                    : GetFutureShortDate();
                            }
                            else
                            {
                                if (entryField.Equals(EntryFieldType.Revision))
                                {
                                    argValue = $"A1";
                                }
                                else
                                {
                                    //argValue = GetVarForEntryField(entryField);
                                    argValue = GetVar(entryField);
                                    int argValueLength = ((string)argValue).Length;

                                    By inputLocator = GetInputFieldByLocator(entryField);
                                    int elemMaxLength = int.Parse(PageAction.GetAttributeForElement(inputLocator, "maxlength"));

                                    argValue = argValueLength > elemMaxLength
                                        ? ((string)argValue).Substring(0, elemMaxLength)
                                        : argValue;
                                }
                            }

                            fieldValue = (string)argValue;
                        }

                        PageAction.EnterText(By.Id(entryField.GetString()), fieldValue);
                    }
                    else if (fieldType.Equals(DDL) || fieldType.Equals(MULTIDDL))
                    {
                        if ((argType == typeof(string) && !((string)argValue).HasValue()) || (int)argValue < 1)
                        {
                            argValue = 1;
                        }

                        bool isMultiselectDDList = false;

                        if (fieldType.Equals(MULTIDDL))
                        {
                            isMultiselectDDList = true;
                        }

                        PageAction.ExpandAndSelectFromDDList(entryField, argValue, useContains, isMultiselectDDList);

                        if (fieldType.Equals(DDL))
                        {
                            if (entryField.Equals(EntryFieldType.Access))
                            {
                                PageAction.SelectRadioBtnOrChkbox(EntryFieldType.AllowResharing);
                                ClickBtn_AddAccessItem();
                                fieldValue = string.Join("::", GetAccessGroupsList().ToArray());
                            }
                            else
                            {
                                fieldValue = PageAction.GetTextFromDDL(entryField);
                            }
                        }
                        else
                        {
                            fieldValue = string.Join("::", PageAction.GetTextFromMultiSelectDDL(entryField).ToArray());
                        }
                    }
                    else if (fieldType.Equals(RDOBTN) || fieldType.Equals(CHKBOX))
                    {
                        PageAction.SelectRadioBtnOrChkbox(entryField);
                    }
                }
                else
                {
                    log.Error($"Argument type ({argType}) is not supported : {indexOrText.ToString()}");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return fieldValuePair = new KeyValuePair<EntryFieldType, string>(entryField, fieldValue);
        }

        public override bool VerifyTransmissionDetailsRequiredFields()
        {
            PageAction.WaitForPageReady();

            reqFieldLocators = GetTenantRequiredFieldLocators();

            IList<string> actualReqFields = new List<string>();
            actualReqFields = PageAction.GetAttributeForElements(reqFieldLocators, "data-valmsg-for");

            tenantExpectedRequiredFields = GetTenantRequiredFieldsList();

            IList<string> expectedReqFields = new List<string>();

            foreach (EntryFieldType field in tenantExpectedRequiredFields)
            {
                expectedReqFields.Add(field.GetString());
            }

            return PageAction.VerifyExpectedList(actualReqFields, expectedReqFields, "VerifyTransmissionDetailsRequiredFields");
        }

        private ColumnNameType GetMatchingColumnNameTypeForEntryFieldType(EntryFieldType entryField)
        {
            ColumnNameType columnName = ColumnNameType.ID;

            switch (entryField)
            {
                case EntryFieldType.Attention:
                    columnName = ColumnNameType.Attention;
                    break;
                case EntryFieldType.Date:
                    columnName = ColumnNameType.Date;
                    break;
                case EntryFieldType.DocumentType:
                    columnName = ColumnNameType.DocumentType;
                    break;
                case EntryFieldType.From:
                    columnName = ColumnNameType.From;
                    break;
                case EntryFieldType.MSLNumber:
                    columnName = ColumnNameType.MSLNumber;
                    break;
                case EntryFieldType.OriginatorDocumentRef:
                    columnName = ColumnNameType.OriginatorRef;
                    break;
                case EntryFieldType.Revision:
                    columnName = ColumnNameType.Revision;
                    break;
                case EntryFieldType.Title:
                    columnName = ColumnNameType.Title;
                    break;
                case EntryFieldType.TransmittalNumber:
                    columnName = ColumnNameType.TransmittalNumber;
                    break;
                case EntryFieldType.Transmitted:
                    columnName = ColumnNameType.TransmittedTypes;
                    break;
                case EntryFieldType.Via:
                    columnName = ColumnNameType.Via;
                    break;
            }

            return columnName;
        }

        public override bool VerifyTableColumnValues()
        {
            bool result = false;
            string actualValueInTable = string.Empty;
            string expectedValueInTable = string.Empty;

            IList<string> expectedValuesInTableList = new List<string>();
            IList<string> actualValuesInTableList = new List<string>();

            //expectedEntryFieldsForTblColumns = ProjCorrespondenceLog.GetTenantEntryFieldsForTableColumns();

            try
            {
                foreach (EntryFieldType colEntryField in expectedEntryFieldsForTblColumns)
                {
                    //var expectedType = colEntryField.GetType();
                    var entryFieldType = colEntryField.GetString(true);

                    ColumnNameType columnName = GetMatchingColumnNameTypeForEntryFieldType(colEntryField);
                    var expectedValue = (from kvp in tenantAllEntryFieldKeyValuePairs where kvp.Key == colEntryField select kvp.Value).FirstOrDefault();
                    expectedValueInTable = entryFieldType.Equals(DATE) || entryFieldType.Equals(FUTUREDATE)
                        ? GetShortDate(expectedValue, true)
                        : colEntryField.Equals(EntryFieldType.DocumentType) || colEntryField.Equals(EntryFieldType.Via)
                            ? expectedValue.ReplaceSpacesWithUnderscores()
                            : expectedValue;

                    actualValueInTable = GridHelper.GetColumnValueForRow("", columnName, ProjCorrespondenceLog.VerifyIsMultiTabGrid()).Trim();
                    Console.WriteLine($"COLUMN NAME: {columnName.ToString()} :: ACTUAL VALUE: {actualValueInTable}");
                    string exptedFieldName = $"Field Name : [{colEntryField.ToString()}]";
                    expectedValuesInTableList.Add($"{exptedFieldName}::{expectedValueInTable}");
                    actualValuesInTableList.Add(actualValueInTable);
                }
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }

            result = PageAction.VerifyExpectedList(actualValuesInTableList, expectedValuesInTableList, "VerifyTableColumnValues");

            return result;
        }

        public override string PopulateAllFields()
        {
            string transmittalNumber = string.Empty;

            //tenantAllEntryFields = ProjCorrespondenceLog.GetTenantAllEntryFieldsList();
            //tenantAllEntryFieldKeyValuePairs = new List<KeyValuePair<EntryField, string>>();

            foreach (EntryFieldType field in tenantAllEntryFields)
            {
                KeyValuePair<EntryFieldType, string> kvpFromEntry = new KeyValuePair<EntryFieldType, string>();
                kvpFromEntry = PopulateFieldValue(field, "");
                log.Debug($"Added KeyValPair to expected table column values./nEntry Field: {kvpFromEntry.Key.ToString()} || Value: {kvpFromEntry.Value}");
                tenantAllEntryFieldKeyValuePairs.Add(kvpFromEntry);
            }

            transmittalNumber = (from kvp in tenantAllEntryFieldKeyValuePairs where kvp.Key == EntryFieldType.TransmittalNumber select kvp.Value).FirstOrDefault();
            return transmittalNumber;
        }

        public override bool VerifyTransmittalLogIsDisplayed(string transmittalNumber, bool noRecordExpected = false)
            => GridHelper.VerifyRecordIsDisplayed(ColumnNameType.TransmittalNumber, transmittalNumber,
                VerifyIsMultiTabGrid()
                    ? TableType.MultiTab
                    : TableType.Single,
                noRecordExpected);

        public override bool VerifyTransmittalLogIsDisplayedByGridColumnFilter()
        {
            bool result = false;
            string logMsg = string.Empty;
            IList<bool> resultsList = new List<bool>();

            expectedEntryFieldsForTblColumns = GetTenantEntryFieldsForTableColumns();

            foreach (EntryFieldType entryField in expectedEntryFieldsForTblColumns)
            {
                TableType tenantTableType = VerifyIsMultiTabGrid()
                    ? TableType.MultiTab
                    : TableType.Single;

                ColumnNameType column = GetMatchingColumnNameTypeForEntryFieldType(entryField);

                //tenantAllEntryFieldKeyValuePairs list is generated by PopulateAllFields() method called by CreateNewAndPopulateFields() method
                string value = (from kvp in tenantAllEntryFieldKeyValuePairs where kvp.Key == entryField select kvp.Value).FirstOrDefault();

                if (entryField.Equals(EntryFieldType.DocumentType) || entryField.Equals(EntryFieldType.Via))
                {
                    value = value.ReplaceSpacesWithUnderscores();
                }

                bool isDisplayed = GridHelper.VerifyRecordIsDisplayed(column, value, tenantTableType);

                resultsList.Add(isDisplayed);

                logMsg = isDisplayed
                    ? ""
                    : " NOT";

                Report.Info($"Column '{column}' in the grid was{logMsg} filtered successfully by value {value}", isDisplayed);
                TestUtility.AddAssertionToList(isDisplayed, $"VerifyTransmittalLogIsDisplayedByGridColumnFilter [Column : {column}]");

                GridHelper.ClearTableFilters(tenantTableType);
                PageAction.WaitForPageReady();
            }

            result = resultsList.Contains(false)
                ? false
                : true;

            return result;
        }

        public override bool VerifyTransmittalLogIsDisplayed(TableTabType tableTab, string transmittalNumber, bool noRecordExpected = false)
        {
            bool isDisplayed = false;

            GridHelper.ClickTab(tableTab);
            PageAction.WaitForPageReady();
            isDisplayed = GridHelper.VerifyRecordIsDisplayed(ColumnNameType.TransmittalNumber, transmittalNumber,
                ProjCorrespondenceLog.VerifyIsMultiTabGrid()
                    ? TableType.MultiTab
                    : TableType.Single,
                noRecordExpected);

            return isDisplayed;
        }

        public override bool VerifyTransmissionDetailsPageValues()
        {
            bool result = false;
            string actualValue = string.Empty;
            string expectedValue = string.Empty;

            IList<string> expectedValuesList = new List<string>();
            IList<string> actualValuesList = new List<string>();

            try
            {
                PageAction.WaitForPageReady();

                foreach (EntryFieldType entryField in tenantAllEntryFields)
                {
                    string fieldType = entryField.GetString(true);

                    if (fieldType.Equals(RDOBTN) || fieldType.Equals(CHKBOX))
                    {
                        expectedValue = "selected";
                        actualValue = PageAction.VerifyChkBoxRdoBtnSelection(entryField) ? "selected" : "Not Selected";
                    }
                    else
                    {
                        expectedValue = (from kvp in tenantAllEntryFieldKeyValuePairs where kvp.Key == entryField select kvp.Value).FirstOrDefault();
                        actualValue = GetValueFromEntryField(entryField);
                    }

                    string exptedFieldName = $"Field Name : {entryField.ToString()}";
                    expectedValuesList.Add($"{exptedFieldName}::{expectedValue}");
                    actualValuesList.Add(actualValue);
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            result = PageAction.VerifyExpectedList(actualValuesList, expectedValuesList, "VerifyTransmissionDetailsPageValues");
            return result;
        }

        public override void IterrateOverRemainingTableTabs_DetailsPageValues(string transmittalNumber, IList<TableTabType> remainingTblTabs)
        {
            int totalTabCount = remainingTblTabs.Count;

            PageAction.ClickSaveForward();
            TestUtility.AddAssertionToList_VerifyPageHeader("Transmissions", "IterrateOverRemainingTableTabs_DetailsPageValues()");

            for (int i = 0; i < totalTabCount; i++)
            {
                TableTabType currentTab = remainingTblTabs[i];

                TestUtility.AddAssertionToList(ProjCorrespondenceLog.VerifyTransmittalLogIsDisplayed(currentTab, transmittalNumber), $"VerifyTransmittalLogIsDisplayed under table ({currentTab})");
                TestUtility.AddAssertionToList(ProjCorrespondenceLog.VerifyTableColumnValues(), $"VerifyTableColumnValues under table {currentTab}");
                ProjCorrespondenceLog.ClickViewBtnForTransmissionsRow();
                TestUtility.AddAssertionToList(ProjCorrespondenceLog.VerifyTransmissionDetailsPageValues(), $"VerifyTransmissionDetailsPageValues under table ({currentTab})");

                int currentTabCount = i + 1;
                if (currentTabCount < totalTabCount)
                {
                    PageAction.ClickSaveForward();
                }
            }
        }

        public override void IterrateOverRemainingTableTabs_GridColumnFilters(string transmittalNumber, IList<TableTabType> remainingTblTabs)
        {
            int totalTabCount = remainingTblTabs.Count;

            TestUtility.AddAssertionToList(ProjCorrespondenceLog.VerifyTransmittalLogIsDisplayed(transmittalNumber), $"VerifyTransmittalLogIsDisplayed");
            ProjCorrespondenceLog.ClickViewBtnForTransmissionsRow();
            PageAction.ClickSaveForward();
            TestUtility.AddAssertionToList_VerifyPageHeader("Transmissions", "IterrateOverRemainingTableTabs_GridColumnFilters()");

            for (int i = 0; i < totalTabCount; i++)
            {
                TableTabType currentTab = remainingTblTabs[i];
                TestUtility.AddAssertionToList(ProjCorrespondenceLog.VerifyTransmittalLogIsDisplayed(currentTab, transmittalNumber), $"VerifyTransmittalLogIsDisplayed");
                GridHelper.ClearTableFilters();
                ProjCorrespondenceLog.VerifyTransmittalLogIsDisplayedByGridColumnFilter();
                TestUtility.AddAssertionToList(ProjCorrespondenceLog.VerifyTransmittalLogIsDisplayed(transmittalNumber), $"VerifyTransmittalLogIsDisplayed");

                int currentTabCount = i + 1;
                if (currentTabCount < totalTabCount)
                {
                    ProjCorrespondenceLog.ClickViewBtnForTransmissionsRow();
                    PageAction.ClickSaveForward();
                    TestUtility.AddAssertionToList_VerifyPageHeader("Transmissions", "IterrateOverRemainingTableTabs_GridColumnFilters()");
                }
            }
        }

        public override bool VerifyTransmittalLocationBySearch()
        {
            PageAction.ClickElement(By.Id("SearchButton"));
            bool isDisplayed = GridHelper.VerifyRecordIsDisplayed(ColumnNameType.Title, "RFC A - MOT Segment 1 Phase 0", TableType.Single);

            return isDisplayed;
        }

        public override IList<KeyValuePair<EntryFieldType, string>> GetTenantEntryFieldKVPairsList()
        {
            if (tenantAllEntryFieldKeyValuePairs == null)
            {
                tenantAllEntryFieldKeyValuePairs = new List<KeyValuePair<EntryFieldType, string>>() { };
            }
            return tenantAllEntryFieldKeyValuePairs;
        }

        public override bool VerifyIsMultiTabGrid() => false;

        public override void LogintoCorrespondenceLogPage(UserType userType)
        {
            LoginAs(userType);
            PageAction.WaitForPageReady();
            NavigateToPage.RMCenter_Project_Correspondence_Log();
            TestUtility.AddAssertionToList_VerifyPageHeader("Transmissions", "LogintoCorrespondenceLogPage()");
        }

        public override string CreateNewAndPopulateFields()
        {
            PageAction.ClickNew();
            PageAction.WaitForPageReady();
            PageAction.ClickSaveForward();
            TestUtility.AddAssertionToList(VerifyTransmissionDetailsRequiredFields(), "VerifyTransmissionDetailsRequiredFields");
            string transmittalNumber = PopulateAllFields();
            PageAction.UploadFile();
            PageAction.ClickSave();
            TestUtility.AddAssertionToList_VerifyPageHeader("Transmissions", "CreateNewAndPopulateFields()");
            return transmittalNumber;
        }

        public override void VerifyTransmissionDetailsPageValuesInRemainingTableTabs(string transmittalNumber)
        {
            Report.Step($"Test step skipped for {tenantName} - tenant does not have tabbed table grid");
        }

        public override void VerifyTransmissionDetailsGridFilterInRemainingTableTabs(string transmittalNumber)
        {
            Report.Step($"Test step skipped for {tenantName} - tenant does not have tabbed table grid");
        }

        //For tenants I15SB, I15Tech, SG, SH249
        public override void ClickViewBtnForTransmissionsRow()
            => GridHelper.ClickViewBtnForRow("", false, false);

        public override IList<By> GetTenantRequiredFieldLocators()
        {
            return reqFieldLocators = new List<By>()
            {
                By.XPath("//span[contains(text(),'Required')]"),
            };
        }

        public override IList<EntryFieldType> GetTenantRequiredFieldsList()
            => tenantExpectedRequiredFields;

        public override IList<EntryFieldType> GetTenantAllEntryFieldsList()
            => tenantAllEntryFields;

        public override IList<EntryFieldType> GetTenantEntryFieldsForTableColumns()
            => expectedEntryFieldsForTblColumns;

        public override IList<TableTabType> GetTenantTableTabsList()
            => tenantTableTabs;
    }

    public interface IProjectCorrespondenceLog
    {
        IList<KeyValuePair<EntryFieldType, string>> GetTenantEntryFieldKVPairsList();

        void ClickViewBtnForTransmissionsRow();

        void VerifyTransmissionDetailsPageValuesInRemainingTableTabs(string transmittalNumber);

        void VerifyTransmissionDetailsGridFilterInRemainingTableTabs(string transmittalNumber);

        void IterrateOverRemainingTableTabs_DetailsPageValues(string transmittalNumber, IList<TableTabType> remainingTblTabs);

        void IterrateOverRemainingTableTabs_GridColumnFilters(string transmittalNumber, IList<TableTabType> remainingTblTabs);

        bool VerifyTransmissionDetailsPageValues();

        bool VerifyTableColumnValues();

        bool VerifyIsMultiTabGrid();

        /// <summary>
        /// Navigates into RM Center Project Correspondence Log page and call PopulateAllFields() method to create Transmission report and generate a List of KeyValuePairs for validation [tenantAllEntryFieldKeyValuePairs]
        /// </summary>
        /// <returns></returns>
        string CreateNewAndPopulateFields();

        IList<By> GetTenantRequiredFieldLocators();

        IList<EntryFieldType> GetTenantRequiredFieldsList();

        IList<EntryFieldType> GetTenantAllEntryFieldsList();

        IList<EntryFieldType> GetTenantEntryFieldsForTableColumns();

        IList<TableTabType> GetTenantTableTabsList();

        void LogintoCorrespondenceLogPage(UserType userType);

        bool VerifyTransmissionDetailsRequiredFields();

        bool VerifyTransmittalLogIsDisplayed(string transmittalNumber, bool noRecordExpected = false);

        bool VerifyTransmittalLogIsDisplayed(TableTabType tableTab, string transmittalNumber, bool noRecordExpected = false);

        bool VerifyTransmittalLogIsDisplayedByGridColumnFilter();
        string PopulateAllFields();
        void EnterText_Date(string shortDate = "");
        void EnterText_TransmittalNumber(string value = "");
        void EnterText_Title(string value = "");
        void EnterText_From(string value = "");
        void EnterText_Attention(string value = "");
        void EnterText_OriginatorDocumentRef(string value = "");
        void EnterText_Revision(string value = "");
        void EnterText_CDRL(string value = "");
        void EnterText_ResponseRequiredByDate(string value = "");
        void EnterText_OwnerResponseBy(string value = "");
        void EnterText_OwnerResponseDate(string value = "");
        void EnterText_MSLNumber(string value = "");

        void SelectDDL_Access<T>(T indexOrName = default(T));
        void SelectDDL_SpecSection<T>(T indexOrName = default(T));
        void SelectDDL_OwnerResponse<T>(T indexOrName = default(T));
        void SelectDDL_DesignPackages<T>(T indexOrName = default(T));
        void SelectDDL_SegmentArea<T>(T indexOrName = default(T));
        void SelectDDL_Transmitted<T>(T indexOrName = default(T));
        void SelectDDL_DocumentCategory<T>(T indexOrName = default(T));
        void SelectDDL_DocumentType<T>(T indexOrName = default(T));
        void SelectDDL_AgencyAttention<T>(T indexOrName = default(T));
        void SelectDDL_AgencyFrom<T>(T indexOrName = default(T));
        void SelectDDL_SecurityClassification<T>(T indexOrName = default(T));

        void SelectRdoBtn_ResponseRequired_Yes();
        void SelectRdoBtn_ResponseRequired_No();
        void SelectChkbox_AllowResharing();

        void ClickBtn_AddAccessItem();
        bool VerifyTransmittalLocationBySearch();
    }

    #region Common Workflow Implementation class

    public abstract class ProjectCorrespondenceLog_Impl : PageBase, IProjectCorrespondenceLog
    {
        #region //Entry field abstract Actions
        public abstract void IterrateOverRemainingTableTabs_GridColumnFilters(string transmittalNumber, IList<TableTabType> remainingTblTabs);
        public abstract void IterrateOverRemainingTableTabs_DetailsPageValues(string transmittalNumber, IList<TableTabType> remainingTblTabs);
        public abstract bool VerifyTransmittalLogIsDisplayed(string transmittalNumber, bool noRecordExpected = false);
        public abstract bool VerifyTransmittalLogIsDisplayed(TableTabType tableTab, string transmittalNumber, bool noRecordExpected = false);

        /// <summary>
        /// This method will generate List of KeyValuePair for tenantAllEntryFieldKeyValuePairs field variable - (Key: EntryField enum and Value: entry field data in string format) to be used for validation.
        /// </summary>
        /// <returns></returns>
        public abstract string PopulateAllFields();
        public abstract void EnterText_Date(string shortDate = "");
        public abstract void EnterText_TransmittalNumber(string value = "");
        public abstract void EnterText_Title(string value = "");
        public abstract void EnterText_From(string value = "");
        public abstract void EnterText_Attention(string value = "");
        public abstract void EnterText_OriginatorDocumentRef(string value = "");
        public abstract void EnterText_Revision(string value = "");
        public abstract void EnterText_CDRL(string value = "");
        public abstract void EnterText_ResponseRequiredByDate(string value = "");
        public abstract void EnterText_OwnerResponseBy(string value = "");
        public abstract void EnterText_OwnerResponseDate(string value = "");
        public abstract void EnterText_MSLNumber(string value = "");
        public abstract void SelectDDL_Access<T>(T indexOrName = default(T));
        public abstract void SelectDDL_SpecSection<T>(T indexOrName = default(T));
        public abstract void SelectDDL_OwnerResponse<T>(T indexOrName = default(T));
        public abstract void SelectDDL_DesignPackages<T>(T indexOrName = default(T));
        public abstract void SelectDDL_SegmentArea<T>(T indexOrName = default(T));
        public abstract void SelectDDL_Transmitted<T>(T indexOrName = default(T));
        public abstract void SelectDDL_DocumentCategory<T>(T indexOrName = default(T));
        public abstract void SelectDDL_DocumentType<T>(T indexOrName = default(T));
        public abstract void SelectDDL_AgencyAttention<T>(T indexOrName = default(T));
        public abstract void SelectDDL_AgencyFrom<T>(T indexOrName = default(T));
        public abstract void SelectDDL_SecurityClassification<T>(T indexOrName = default(T));
        public abstract void SelectRdoBtn_ResponseRequired_Yes();
        public abstract void SelectRdoBtn_ResponseRequired_No();
        public abstract void SelectChkbox_AllowResharing();
        public abstract void ClickBtn_AddAccessItem();
        public abstract bool VerifyTableColumnValues();
        public abstract bool VerifyTransmissionDetailsRequiredFields();
        public abstract bool VerifyTransmissionDetailsPageValues();
        #endregion //Entry field abstract Actions

        //Table Grid Type - returns true if table has multiple tabs
        //Returns false valid for I15SB, I15Tech, SH249, & SG
        public abstract bool VerifyIsMultiTabGrid();
        public abstract void LogintoCorrespondenceLogPage(UserType userType);
        public abstract IList<EntryFieldType> GetTenantRequiredFieldsList();
        public abstract IList<EntryFieldType> GetTenantAllEntryFieldsList();
        public abstract IList<EntryFieldType> GetTenantEntryFieldsForTableColumns();
        public abstract IList<TableTabType> GetTenantTableTabsList();

        public abstract string CreateNewAndPopulateFields();
        public abstract void VerifyTransmissionDetailsPageValuesInRemainingTableTabs(string transmittalNumber);
        public abstract void VerifyTransmissionDetailsGridFilterInRemainingTableTabs(string transmittalNumber);

        //For tenants I15SB, I15Tech, SG, SH249
        public abstract void ClickViewBtnForTransmissionsRow();
        public abstract IList<By> GetTenantRequiredFieldLocators();
        public abstract bool VerifyTransmittalLocationBySearch();
        public abstract bool VerifyTransmittalLogIsDisplayedByGridColumnFilter();
        public abstract IList<KeyValuePair<EntryFieldType, string>> GetTenantEntryFieldKVPairsList();
    }

    #endregion Common Workflow Implementation class

    /// <summary>
    /// Tenant specific implementation of ProjectCorrespondenceLog
    /// </summary>

    #region Implementation specific to Garnet

    public class ProjectCorrespondenceLog_Garnet : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to Garnet


    #region Implementation specific to GLX

    public class ProjectCorrespondenceLog_GLX : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override bool VerifyIsMultiTabGrid() => true;

        public override void ClickViewBtnForTransmissionsRow()
            => GridHelper.ClickViewBtnForRow("", true, false);

        public override IList<EntryFieldType> GetTenantRequiredFieldsList()
        {
            return tenantExpectedRequiredFields = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.SecurityClassification,
                EntryFieldType.Title,
                EntryFieldType.DocumentType,
                EntryFieldType.Transmitted,
                EntryFieldType.Attachments
            };
        }

        public override IList<EntryFieldType> GetTenantAllEntryFieldsList()
        {
            return tenantAllEntryFields = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.TransmittalNumber,
                EntryFieldType.SecurityClassification,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.AgencyFrom,
                EntryFieldType.Attention,
                EntryFieldType.AgencyAttention,
                EntryFieldType.DocumentCategory,
                EntryFieldType.DocumentType,
                EntryFieldType.OriginatorDocumentRef,
                EntryFieldType.Revision,
                EntryFieldType.Transmitted,
                EntryFieldType.Segment_Area,
                EntryFieldType.DesignPackages,
                EntryFieldType.CDRL,
                EntryFieldType.ResponseRequired_Yes,
                EntryFieldType.ResponseRequiredBy_Date,
                EntryFieldType.SpecSection,
                EntryFieldType.MSLNumber,
                EntryFieldType.Access
            };
        }

        public override IList<EntryFieldType> GetTenantEntryFieldsForTableColumns()
        {
            return expectedEntryFieldsForTblColumns = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.TransmittalNumber,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.Attention,
                EntryFieldType.DocumentType,
                EntryFieldType.OriginatorDocumentRef,
                EntryFieldType.Revision,
                EntryFieldType.Transmitted,
                EntryFieldType.MSLNumber
            };
        }

        public override IList<TableTabType> GetTenantTableTabsList()
        {
            return tenantTableTabs = new List<TableTabType>()
            {
                TableTabType.PendingTransmissions,
                TableTabType.TransmittedRecords
            };
        }

        public override void VerifyTransmissionDetailsPageValuesInRemainingTableTabs(string transmittalNumber)
        {
            //tenantTableTabs = GetTenantTableTabsList();
            IterrateOverRemainingTableTabs_DetailsPageValues(transmittalNumber, tenantTableTabs);
        }

        public override void VerifyTransmissionDetailsGridFilterInRemainingTableTabs(string transmittalNumber)
        {
            //tenantTableTabs = GetTenantTableTabsList();
            IterrateOverRemainingTableTabs_GridColumnFilters(transmittalNumber, tenantTableTabs);
        }

    }

    #endregion Implementation specific to GLX


    #region Implementation specific to SH249

    public class ProjectCorrespondenceLog_SH249 : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override void LogintoCorrespondenceLogPage(UserType userType)
        {
            LoginAs(userType);
            PageAction.WaitForPageReady();
            NavigateToPage.RMCenter_Project_Transmittal_Log();
        }

        public override IList<By> GetTenantRequiredFieldLocators()
        {
            return reqFieldLocators = new List<By>()
            {
                By.XPath("//span[contains(text(),'Required')]"),
                By.XPath("//span[contains(text(),'required')]")
            };
        }

        public override IList<EntryFieldType> GetTenantRequiredFieldsList()
        {
            return tenantExpectedRequiredFields = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.DocumentType,
                EntryFieldType.Via,
                EntryFieldType.Attachments
            };
        }

        public override IList<EntryFieldType> GetTenantAllEntryFieldsList()
        {
            return tenantAllEntryFields = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.TransmittalNumber,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.Attention,
                EntryFieldType.DocumentType,
                EntryFieldType.Via
            };
        }

        public override IList<EntryFieldType> GetTenantEntryFieldsForTableColumns()
        {
            return expectedEntryFieldsForTblColumns = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.TransmittalNumber,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.Attention,
                EntryFieldType.DocumentType,
                EntryFieldType.Via
            };
        }

    }

    #endregion Implementation specific to SH249


    #region Implementation specific to SGWay

    public class ProjectCorrespondenceLog_SGWay : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override void LogintoCorrespondenceLogPage(UserType userType)
        {
            LoginAs(userType);
            PageAction.WaitForPageReady();
            NavigateToPage.RMCenter_Project_Transmittal_Log();
        }

        public override IList<By> GetTenantRequiredFieldLocators()
        {
            return reqFieldLocators = new List<By>()
            {
                By.XPath("//span[contains(text(),'Required')]"),
                By.XPath("//span[contains(text(),'required')]")
            };
        }

        public override IList<EntryFieldType> GetTenantRequiredFieldsList()
        {
            return tenantExpectedRequiredFields = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.Attention,
                EntryFieldType.DocumentType,
                EntryFieldType.Attachments
            };
        }

        public override IList<EntryFieldType> GetTenantAllEntryFieldsList()
        {
            return tenantAllEntryFields = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.TransmittalNumber,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.Attention,
                EntryFieldType.DocumentType,
                EntryFieldType.MSLNumber,
                EntryFieldType.Via
            };
        }

        public override IList<EntryFieldType> GetTenantEntryFieldsForTableColumns()
        {
            return expectedEntryFieldsForTblColumns = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.TransmittalNumber,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.Attention,
                EntryFieldType.DocumentType,
                EntryFieldType.MSLNumber,
                EntryFieldType.Via
            };
        }

    }

    #endregion Implementation specific to SGWay


    #region Implementation specific to I15South

    public class ProjectCorrespondenceLog_I15South : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_I15South(IWebDriver driver) : base(driver)
        {
        }

        public override IList<EntryFieldType> GetTenantRequiredFieldsList()
        {
            return tenantExpectedRequiredFields = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.Attention,
                //EntryField.DocumentCategory,
                EntryFieldType.DocumentType,
                EntryFieldType.Attachments
            };
        }

        public override IList<EntryFieldType> GetTenantAllEntryFieldsList()
        {
            return tenantAllEntryFields = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.TransmittalNumber,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.Attention,
                EntryFieldType.DocumentCategory,
                EntryFieldType.DocumentType
            };
        }

        public override IList<EntryFieldType> GetTenantEntryFieldsForTableColumns()
        {
            return expectedEntryFieldsForTblColumns = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.TransmittalNumber,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.Attention,
                EntryFieldType.DocumentType
            };
        }

    }

    #endregion Implementation specific to I15South


    #region Implementation specific to I15Tech

    public class ProjectCorrespondenceLog_I15Tech : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_I15Tech(IWebDriver driver) : base(driver)
        {
        }


        public override IList<EntryFieldType> GetTenantRequiredFieldsList()
        {
            return tenantExpectedRequiredFields = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.Attention,
                //EntryField.DocumentCategory,
                EntryFieldType.DocumentType,
                EntryFieldType.Attachments,
                EntryFieldType.Transmitted
            };
        }

        public override IList<EntryFieldType> GetTenantAllEntryFieldsList()
        {
            return tenantAllEntryFields = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.TransmittalNumber,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.Attention,
                EntryFieldType.DocumentCategory,
                EntryFieldType.DocumentType,
                EntryFieldType.Transmitted
            };
        }

        public override IList<EntryFieldType> GetTenantEntryFieldsForTableColumns()
        {
            return expectedEntryFieldsForTblColumns = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.TransmittalNumber,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.Attention,
                EntryFieldType.DocumentType,
                EntryFieldType.Transmitted
            };
        }

    }

    #endregion Implementation specific to I15Tech


    #region Implementation specific to LAX

    public class ProjectCorrespondenceLog_LAX : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_LAX(IWebDriver driver) : base(driver)
        {
        }

        public override bool VerifyIsMultiTabGrid() => true;

        public override void ClickViewBtnForTransmissionsRow()
            => GridHelper.ClickViewBtnForRow("", true, false);

        public override IList<EntryFieldType> GetTenantRequiredFieldsList()
        {
            return tenantExpectedRequiredFields = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.SecurityClassification,
                EntryFieldType.Title,
                EntryFieldType.DocumentType,
                EntryFieldType.Transmitted,
                EntryFieldType.Attachments
            };
        }

        public override IList<EntryFieldType> GetTenantAllEntryFieldsList()
        {
            return tenantAllEntryFields = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.TransmittalNumber,
                EntryFieldType.SecurityClassification,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.AgencyFrom,
                EntryFieldType.Attention,
                EntryFieldType.AgencyAttention,
                EntryFieldType.DocumentCategory,
                EntryFieldType.DocumentType,
                EntryFieldType.OriginatorDocumentRef,
                EntryFieldType.Revision,
                EntryFieldType.Transmitted,
                EntryFieldType.Segment_Area,
                EntryFieldType.Access
            };
        }

        public override IList<EntryFieldType> GetTenantEntryFieldsForTableColumns()
        {
            return expectedEntryFieldsForTblColumns = new List<EntryFieldType>()
            {
                EntryFieldType.Date,
                EntryFieldType.TransmittalNumber,
                EntryFieldType.Title,
                EntryFieldType.From,
                EntryFieldType.Attention,
                EntryFieldType.DocumentType,
                EntryFieldType.OriginatorDocumentRef,
                EntryFieldType.Revision,
                EntryFieldType.Transmitted,
            };
        }

        public override IList<TableTabType> GetTenantTableTabsList()
        {
            return tenantTableTabs = new List<TableTabType>()
            {
                TableTabType.TransmittedRecords
            };
        }

        public override void VerifyTransmissionDetailsPageValuesInRemainingTableTabs(string transmittalNumber)
        {
            //tenantTableTabs = GetTenantTableTabsList();
            IterrateOverRemainingTableTabs_DetailsPageValues(transmittalNumber, tenantTableTabs);
        }

        public override void VerifyTransmissionDetailsGridFilterInRemainingTableTabs(string transmittalNumber)
        {
            //tenantTableTabs = GetTenantTableTabsList();
            IterrateOverRemainingTableTabs_GridColumnFilters(transmittalNumber, tenantTableTabs);
        }

    }

    #endregion Implementation specific to LAX

}