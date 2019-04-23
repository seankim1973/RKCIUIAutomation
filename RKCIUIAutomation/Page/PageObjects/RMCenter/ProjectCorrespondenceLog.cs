using MiniGuids;
using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.ProjectCorrespondenceLog;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public class ProjectCorrespondenceLog : ProjectCorrespondenceLog_Impl
    {
        public ProjectCorrespondenceLog()
        {
        }

        public ProjectCorrespondenceLog(IWebDriver driver) => this.Driver = driver;

        //GLX and LAX - StringValue[0] = table tab name, StringValue[1] = Table content reference id
        public enum TableTab
        {
            [StringValue("Unsent Transmissions", "TransmissionGridNew")] UnsentTransmissions,
            [StringValue("Pending Transmissions", "TransmissionGridPending")] PendingTransmissions,
            [StringValue("Transmitted Records", "TransmissionGridForwarded")] TransmittedRecords
        }

        public enum EntryField
        {
            [StringValue("DocumentDate", "DATE")] Date,
            [StringValue("TransmittalNo", "TXT")] TransmittalNumber,
            [StringValue("SecurityClassificationId", "DDL")] SecurityClassification,
            [StringValue("Title", "TXT")] Title,
            [StringValue("From", "TXT")] From,
            [StringValue("AgencyFromId", "DDL")] AgencyFrom,
            [StringValue("Attention", "TXT")] Attention,
            [StringValue("AgencyToId", "DDL")] AgencyAttention,
            [StringValue("DocumentTypeCatogoryId", "DDL")] DocumentCategory,
            [StringValue("DocumentTypeId", "DDL")] DocumentType,
            [StringValue("OriginatorDocumentRef", "TXT")] OriginatorDocumentRef,
            [StringValue("Revision", "TXT")] Revision,
            [StringValue("SelectedTransmittedIds", "MULTIDDL")] Transmitted,
            [StringValue("SegmentId", "DDL")] Segment_Area,
            [StringValue("DesignPackagesIdsNcr", "MULTIDDL")] DesignPackages,
            [StringValue("CdrlNumber", "TXT")] CDRL,
            [StringValue("ResponseRequiredRadioButton_True", "RDOBTN")] ResponseRequired_Yes,
            [StringValue("ResponseRequiredRadioButton_False", "RDOBTN")] ResponseRequired_No,
            [StringValue("ResponseRequiredDate", "FUTUREDATE")] ResponseRequiredBy_Date,
            [StringValue("OwnerReponseId", "DDL")] OwnerResponse,
            [StringValue("OwnerResponseBy", "TXT")] OwnerResponseBy,
            [StringValue("OwnerResponseDate", "DATE")] OwnerResponseDate,
            [StringValue("SectionId", "DDL")] SpecSection,
            [StringValue("MSLNo", "TXT")] MSLNumber,
            [StringValue("AvailableAccessItems", "DDL")] Access,
            [StringValue("ViaId", "DDL")] Via,
            [StringValue("AllowReshare", "CHKBOX")] AllowResharing,
            [StringValue("TransmissionFiles", "UPLOAD")] Attachments
        }

        public enum ColumnName
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
            [StringValue("ViaId")] Via,
        }

        [ThreadStatic]
        internal static IList<TableTab> tenantTableTabs;

        [ThreadStatic]
        internal static IList<By> reqFieldLocators;

        [ThreadStatic]
        internal static IList<EntryField> tenantExpectedRequiredFields;

        [ThreadStatic]
        internal static IList<EntryField> tenantAllEntryFields;

        [ThreadStatic]
        internal static IList<EntryField> expectedEntryFieldsForTblColumns;

        [ThreadStatic]
        internal static IList<KeyValuePair<EntryField, string>> tenantAllEntryFieldKeyValuePairs;

        #region //Entry field override Action methods

        public override void EnterText_Date(string shortDate = "")
            => PopulateFieldValue(EntryField.Date, shortDate);

        public override void EnterText_TransmittalNumber(string value = "")
            => PopulateFieldValue(EntryField.TransmittalNumber, value);

        public override void EnterText_Title(string value = "")
            => PopulateFieldValue(EntryField.Title, value);

        public override void EnterText_From(string value = "")
            => PopulateFieldValue(EntryField.From, value);

        public override void EnterText_Attention(string value = "")
            => PopulateFieldValue(EntryField.Attention, value);

        public override void EnterText_OriginatorDocumentRef(string value = "")
            => PopulateFieldValue(EntryField.OriginatorDocumentRef, value);

        public override void EnterText_Revision(string value = "")
            => PopulateFieldValue(EntryField.Revision, value);

        public override void EnterText_CDRL(string value = "")
            => PopulateFieldValue(EntryField.CDRL, value);

        public override void EnterText_ResponseRequiredByDate(string value = "")
            => PopulateFieldValue(EntryField.ResponseRequiredBy_Date, value);

        public override void EnterText_OwnerResponseBy(string value = "")
            => PopulateFieldValue(EntryField.OwnerResponseBy, value);

        public override void EnterText_OwnerResponseDate(string value = "")
            => PopulateFieldValue(EntryField.OwnerResponseDate, value);

        public override void EnterText_MSLNumber(string value = "")
            => PopulateFieldValue(EntryField.MSLNumber, value);

        public override void SelectDDL_Access<T>(T indexOrName)
            => PopulateFieldValue(EntryField.Access, indexOrName);

        public override void SelectDDL_SpecSection<T>(T indexOrName)
            => PopulateFieldValue(EntryField.SpecSection, indexOrName);

        public override void SelectDDL_OwnerResponse<T>(T indexOrName)
            => PopulateFieldValue(EntryField.OwnerResponse, indexOrName);

        public override void SelectDDL_DesignPackages<T>(T indexOrName)
            => PopulateFieldValue(EntryField.DesignPackages, indexOrName);

        public override void SelectDDL_SegmentArea<T>(T indexOrName)
            => PopulateFieldValue(EntryField.Segment_Area, indexOrName);

        public override void SelectDDL_Transmitted<T>(T indexOrName)
            => PopulateFieldValue(EntryField.Transmitted, indexOrName);

        public override void SelectDDL_DocumentCategory<T>(T indexOrName)
            => PopulateFieldValue(EntryField.DocumentCategory, indexOrName);

        public override void SelectDDL_DocumentType<T>(T indexOrName)
            => PopulateFieldValue(EntryField.DocumentType, indexOrName);

        public override void SelectDDL_AgencyAttention<T>(T indexOrName)
            => PopulateFieldValue(EntryField.AgencyAttention, indexOrName);

        public override void SelectDDL_AgencyFrom<T>(T indexOrName)
            => PopulateFieldValue(EntryField.AgencyFrom, indexOrName);

        public override void SelectDDL_SecurityClassification<T>(T indexOrName)
            => PopulateFieldValue(EntryField.SecurityClassification, indexOrName);

        public override void SelectRdoBtn_ResponseRequired_Yes()
            => PopulateFieldValue(EntryField.ResponseRequired_Yes, "");

        public override void SelectRdoBtn_ResponseRequired_No()
            => PopulateFieldValue(EntryField.ResponseRequired_No, "");

        public override void SelectChkbox_AllowResharing()
            => PopulateFieldValue(EntryField.AllowResharing, "");

        public override void ClickBtn_AddAccessItem()
            => ClickElement(By.Id("AddAccessItem"));

        #endregion //Entry field override Action methods


        private string GetVarForEntryField(Enum fieldEnum)
            => GetVar(fieldEnum);

        private IList<string> GetAccessGroupsList()
            => GetTextForElements(By.XPath("//div[@id='AccessGroups']//ul/li/span[2]"));

        public string GetValueFromEntryField(EntryField entryField)
        {
            string fieldType = entryField.GetString(true);

            string fieldValue = string.Empty;

            if (fieldType.Equals(TEXT) || fieldType.Equals(DATE) || fieldType.Equals(FUTUREDATE))
            {
                fieldValue = GetAttribute(By.Id($"{entryField.GetString()}"), "value");
            }
            else if (fieldType.Equals(DDL) || fieldType.Equals(MULTIDDL))
            {
                if (fieldType.Equals(DDL))
                {
                    if (entryField.Equals(EntryField.Access))
                    {
                        fieldValue = string.Join("::", GetAccessGroupsList().ToArray());
                    }
                    else
                    {
                        fieldValue = GetTextFromDDL(entryField);
                    }
                }
                else
                {
                    fieldValue = string.Join("::", GetTextFromMultiSelectDDL(entryField).ToArray());
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
        private KeyValuePair<EntryField, string> PopulateFieldValue<T>(EntryField entryField, T indexOrText, bool useContains = false)
        {
            string fieldType = entryField.GetString(true);
            Type argType = indexOrText.GetType();
            object argValue = null;
            bool isValidArg = false;

            KeyValuePair<EntryField, string> fieldValuePair;
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
                                    ? GetShortDate()
                                    : GetFutureShortDate();
                            }
                            else
                            {
                                if (entryField.Equals(EntryField.Revision))
                                {
                                    argValue = $"A1";
                                }
                                else
                                {
                                    argValue = GetVarForEntryField(entryField);

                                    int argValueLength = ((string)argValue).Length;

                                    By inputLocator = GetInputFieldByLocator(entryField);
                                    int elemMaxLength = int.Parse(GetAttribute(inputLocator, "maxlength"));

                                    argValue = argValueLength > elemMaxLength
                                        ? ((string)argValue).Substring(0, elemMaxLength)
                                        : argValue;
                                }
                            }

                            fieldValue = (string)argValue;
                        }

                        EnterText(By.Id(entryField.GetString()), fieldValue);
                    }
                    else if (fieldType.Equals(DDL) || fieldType.Equals(MULTIDDL))
                    {
                        argValue = ((argType == typeof(string) && !((string)argValue).HasValue()) || (int)argValue < 1)
                            ? 1
                            : argValue;

                        ExpandAndSelectFromDDList(entryField, argValue, useContains, fieldType.Equals(MULTIDDL) ? true : false);

                        if (fieldType.Equals(DDL))
                        {
                            if (entryField.Equals(EntryField.Access))
                            {
                                SelectRadioBtnOrChkbox(EntryField.AllowResharing);
                                ClickBtn_AddAccessItem();
                                fieldValue = string.Join("::", GetAccessGroupsList().ToArray());
                            }
                            else
                            {
                                fieldValue = GetTextFromDDL(entryField);
                            }
                        }
                        else
                        {
                            fieldValue = string.Join("::", GetTextFromMultiSelectDDL(entryField).ToArray());
                        }
                    }
                    else if (fieldType.Equals(RDOBTN) || fieldType.Equals(CHKBOX))
                    {
                        SelectRadioBtnOrChkbox(entryField);
                    }
                }
                else
                {
                    LogError($"Argument type ({argType}) is not supported : {indexOrText.ToString()}");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return fieldValuePair = new KeyValuePair<EntryField, string>(entryField, fieldValue);
        }

        public override bool VerifyTransmissionDetailsRequiredFields()
        {
            WaitForPageReady();

            reqFieldLocators = ProjCorrespondenceLog.SetTenantRequiredFieldLocators();
            IList<string> actualReqFields = new List<string>();
            actualReqFields = GetAttributes(reqFieldLocators, "data-valmsg-for");

            tenantExpectedRequiredFields = ProjCorrespondenceLog.SetTenantRequiredFieldsList();

            IList<string> expectedReqFields = new List<string>();

            foreach (EntryField field in tenantExpectedRequiredFields)
            {
                expectedReqFields.Add(field.GetString());
            }

            return VerifyExpectedList(actualReqFields, expectedReqFields, "VerifyTransmissionDetailsRequiredFields");
        }

        private ColumnName GetMatchingColumnNameForEntryField(EntryField entryField)
        {
            ColumnName columnName = ColumnName.ID;

            switch (entryField)
            {
                case EntryField.Attention:
                    columnName = ColumnName.Attention;
                    break;
                case EntryField.Date:
                    columnName = ColumnName.Date;
                    break;
                case EntryField.DocumentType:
                    columnName = ColumnName.DocumentType;
                    break;
                case EntryField.From:
                    columnName = ColumnName.From;
                    break;
                case EntryField.MSLNumber:
                    columnName = ColumnName.MSLNumber;
                    break;
                case EntryField.OriginatorDocumentRef:
                    columnName = ColumnName.OriginatorRef;
                    break;
                case EntryField.Revision:
                    columnName = ColumnName.Revision;
                    break;
                case EntryField.Title:
                    columnName = ColumnName.Title;
                    break;
                case EntryField.TransmittalNumber:
                    columnName = ColumnName.TransmittalNumber;
                    break;
                case EntryField.Transmitted:
                    columnName = ColumnName.TransmittedTypes;
                    break;
                case EntryField.Via:
                    columnName = ColumnName.Via;
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

            expectedEntryFieldsForTblColumns = ProjCorrespondenceLog.SetTenantEntryFieldsForTableColumns();

            try
            {
                foreach (EntryField colEntryField in expectedEntryFieldsForTblColumns)
                {
                    var expectedType = colEntryField.GetType();
                    var entryFieldType = colEntryField.GetString(true);

                    ColumnName columnName = GetMatchingColumnNameForEntryField(colEntryField);
                    var expectedValue = (from kvp in tenantAllEntryFieldKeyValuePairs where kvp.Key == colEntryField select kvp.Value).FirstOrDefault();
                    expectedValueInTable = expectedType.Equals(entryFieldType == DATE) || expectedType.Equals(entryFieldType == FUTUREDATE)
                        ? GetShortDate(expectedValue, true)
                        : expectedValue;

                    actualValueInTable = GetColumnValueForRow("", columnName, ProjCorrespondenceLog.VerifyIsMultiTabGrid()).Trim();
                    Console.WriteLine($"COLUMN NAME: {columnName.ToString()} :: ACTUAL VALUE: {actualValueInTable}");
                    string exptedFieldName = $"Field Name : [{colEntryField.ToString()}]";
                    expectedValuesInTableList.Add($"{exptedFieldName}::{expectedValueInTable}");
                    actualValuesInTableList.Add(actualValueInTable);
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            result = VerifyExpectedList(actualValuesInTableList, expectedValuesInTableList, "VerifyTableColumnValues");

            return result;
        }

        public override string PopulateAllFields()
        {
            string transmittalNumber = string.Empty;

            tenantAllEntryFields = ProjCorrespondenceLog.SetTenantAllEntryFieldsList();
            tenantAllEntryFieldKeyValuePairs = new List<KeyValuePair<EntryField, string>>();

            foreach (EntryField field in tenantAllEntryFields)
            {
                KeyValuePair<EntryField, string> kvpFromEntry = new KeyValuePair<EntryField, string>();
                kvpFromEntry = PopulateFieldValue(field, "");
                log.Debug($"Added KeyValPair to expected table column values./nEntry Field: {kvpFromEntry.Key.ToString()} || Value: {kvpFromEntry.Value}");
                tenantAllEntryFieldKeyValuePairs.Add(kvpFromEntry);
            }

            transmittalNumber = (from kvp in tenantAllEntryFieldKeyValuePairs where kvp.Key == EntryField.TransmittalNumber select kvp.Value).FirstOrDefault();
            return transmittalNumber;
        }

        public override bool VerifyTransmittalLogIsDisplayed(string transmittalNumber, bool noRecordExpected = false)
            => VerifyRecordIsDisplayed(ColumnName.TransmittalNumber, transmittalNumber,
                ProjCorrespondenceLog.VerifyIsMultiTabGrid()
                    ? TableType.MultiTab
                    : TableType.Single,
                noRecordExpected);

        public override bool VerifyTransmittalLogIsDisplayedByGridColumnFilter()
        {
            bool result = false;
            string logMsg = string.Empty;
            IList<bool> resultsList = new List<bool>();

            expectedEntryFieldsForTblColumns = ProjCorrespondenceLog.SetTenantEntryFieldsForTableColumns();

            foreach (EntryField entryField in expectedEntryFieldsForTblColumns)
            {
                TableType tenantTableType = ProjCorrespondenceLog.VerifyIsMultiTabGrid()
                    ? TableType.MultiTab
                    : TableType.Single;

                ColumnName column = GetMatchingColumnNameForEntryField(entryField);

                //tenantAllEntryFieldKeyValuePairs list is generated by PopulateAllFields() method called by CreateNewAndPopulateFields() method
                string value = (from kvp in tenantAllEntryFieldKeyValuePairs where kvp.Key == entryField select kvp.Value).FirstOrDefault();

                bool isDisplayed = VerifyRecordIsDisplayed(column, value, tenantTableType);

                resultsList.Add(isDisplayed);

                logMsg = isDisplayed
                    ? ""
                    : " NOT";

                LogInfo($"Column '{column}' in the grid was{logMsg} filtered successfully by value {value}", isDisplayed);
                AddAssertionToList(isDisplayed, $"VerifyTransmittalLogIsDisplayedByGridColumnFilter [Column : {column}]");

                ClearTableFilters(tenantTableType);
                WaitForPageReady();
            }

            result = resultsList.Contains(false)
                ? false
                : true;

            return result;
        }

        public override bool VerifyTransmittalLogIsDisplayed(TableTab tableTab, string transmittalNumber, bool noRecordExpected = false)
        {
            bool isDisplayed = false;

            ClickTab(tableTab);
            WaitForPageReady();
            isDisplayed = VerifyRecordIsDisplayed(ColumnName.TransmittalNumber, transmittalNumber,
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
                WaitForPageReady();

                foreach (EntryField entryField in tenantAllEntryFields)
                {
                    string fieldType = entryField.GetString(true);

                    if (fieldType.Equals(RDOBTN) || fieldType.Equals(CHKBOX))
                    {
                        expectedValue = "selected";
                        actualValue = VerifyChkBoxRdoBtnSelection(entryField) ? "selected" : "Not Selected";
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

            result = VerifyExpectedList(actualValuesList, expectedValuesList, "VerifyTransmissionDetailsPageValues");
            return result;
        }

        public override void IterrateOverRemainingTableTabs_DetailsPageValues(string transmittalNumber, IList<TableTab> remainingTblTabs)
        {
            int totalTabCount = remainingTblTabs.Count;

            ClickSaveForward();
            AssertToList_VerifyPageHeader("Transmissions", "IterrateOverRemainingTableTabs_DetailsPageValues()");

            for (int i = 0; i < totalTabCount; i++)
            {
                TableTab currentTab = remainingTblTabs[i];

                AddAssertionToList(ProjCorrespondenceLog.VerifyTransmittalLogIsDisplayed(currentTab, transmittalNumber), $"VerifyTransmittalLogIsDisplayed under table ({currentTab})");
                AddAssertionToList(ProjCorrespondenceLog.VerifyTableColumnValues(), $"VerifyTableColumnValues under table {currentTab}");
                ProjCorrespondenceLog.ClickViewBtnForTransmissionsRow();
                AddAssertionToList(ProjCorrespondenceLog.VerifyTransmissionDetailsPageValues(), $"VerifyTransmissionDetailsPageValues under table ({currentTab})");

                int currentTabCount = i + 1;
                if (currentTabCount < totalTabCount)
                {
                    ClickSaveForward();
                }
            }
        }

        public override void IterrateOverRemainingTableTabs_GridColumnFilters(string transmittalNumber, IList<TableTab> remainingTblTabs)
        {
            int totalTabCount = remainingTblTabs.Count;

            AddAssertionToList(ProjCorrespondenceLog.VerifyTransmittalLogIsDisplayed(transmittalNumber), $"VerifyTransmittalLogIsDisplayed");
            ProjCorrespondenceLog.ClickViewBtnForTransmissionsRow();
            ClickSaveForward();
            AssertToList_VerifyPageHeader("Transmissions", "IterrateOverRemainingTableTabs_GridColumnFilters()");

            for (int i = 0; i < totalTabCount; i++)
            {
                TableTab currentTab = remainingTblTabs[i];
                AddAssertionToList(ProjCorrespondenceLog.VerifyTransmittalLogIsDisplayed(currentTab, transmittalNumber), $"VerifyTransmittalLogIsDisplayed");
                ClearTableFilters();
                ProjCorrespondenceLog.VerifyTransmittalLogIsDisplayedByGridColumnFilter();
                AddAssertionToList(ProjCorrespondenceLog.VerifyTransmittalLogIsDisplayed(transmittalNumber), $"VerifyTransmittalLogIsDisplayed");

                int currentTabCount = i + 1;
                if (currentTabCount < totalTabCount)
                {
                    ProjCorrespondenceLog.ClickViewBtnForTransmissionsRow();
                    ClickSaveForward();
                    AssertToList_VerifyPageHeader("Transmissions", "IterrateOverRemainingTableTabs_GridColumnFilters()");
                }
            }
        }

        public override bool VerifyTransmittalLocationBySearch()
        {
            ClickElement(By.Id("SearchButton"));
            bool isDisplayed = VerifyRecordIsDisplayed(ColumnName.Title, "RFC A - MOT Segment 1 Phase 0", TableType.Single);

            return isDisplayed;
        }
    }

    public interface IProjectCorrespondenceLog
    {
        void ClickViewBtnForTransmissionsRow();

        void VerifyTransmissionDetailsPageValuesInRemainingTableTabs(string transmittalNumber);

        void VerifyTransmissionDetailsGridFilterInRemainingTableTabs(string transmittalNumber);

        void IterrateOverRemainingTableTabs_DetailsPageValues(string transmittalNumber, IList<TableTab> remainingTblTabs);

        void IterrateOverRemainingTableTabs_GridColumnFilters(string transmittalNumber, IList<TableTab> remainingTblTabs);

        bool VerifyTransmissionDetailsPageValues();

        bool VerifyTableColumnValues();

        bool VerifyIsMultiTabGrid();

        /// <summary>
        /// Navigates into RM Center Project Correspondence Log page and call PopulateAllFields() method to create Transmission report and generate a List of KeyValuePairs for validation [tenantAllEntryFieldKeyValuePairs]
        /// </summary>
        /// <returns></returns>
        string CreateNewAndPopulateFields();

        IList<By> SetTenantRequiredFieldLocators();

        IList<EntryField> SetTenantRequiredFieldsList();

        IList<EntryField> SetTenantAllEntryFieldsList();

        IList<EntryField> SetTenantEntryFieldsForTableColumns();

        IList<TableTab> SetTenantTableTabsList();

        void LogintoCorrespondenceLogPage(UserType userType);

        bool VerifyTransmissionDetailsRequiredFields();

        bool VerifyTransmittalLogIsDisplayed(string transmittalNumber, bool noRecordExpected = false);

        bool VerifyTransmittalLogIsDisplayed(TableTab tableTab, string transmittalNumber, bool noRecordExpected = false);

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

    public abstract class ProjectCorrespondenceLog_Impl : TestBase, IProjectCorrespondenceLog
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IProjectCorrespondenceLog SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IProjectCorrespondenceLog instance = new ProjectCorrespondenceLog(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using ProjectCorrespondenceLog_SGWay instance ###### ");
                instance = new ProjectCorrespondenceLog_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using  ProjectCorrespondenceLog_SH249 instance ###### ");
                instance = new ProjectCorrespondenceLog_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using  ProjectCorrespondenceLog_Garnet instance ###### ");
                instance = new ProjectCorrespondenceLog_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using  ProjectCorrespondenceLog_GLX instance ###### ");
                instance = new ProjectCorrespondenceLog_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using  ProjectCorrespondenceLog_I15South instance ###### ");
                instance = new ProjectCorrespondenceLog_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using ProjectCorrespondenceLog_I15Tech instance ###### ");
                instance = new ProjectCorrespondenceLog_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using ProjectCorrespondenceLog_LAX instance ###### ");
                instance = new ProjectCorrespondenceLog_LAX(driver);
            }
            return instance;
        }


        #region //Entry field abstract Actions
        public abstract void IterrateOverRemainingTableTabs_GridColumnFilters(string transmittalNumber, IList<TableTab> remainingTblTabs);
        public abstract void IterrateOverRemainingTableTabs_DetailsPageValues(string transmittalNumber, IList<TableTab> remainingTblTabs);
        public abstract bool VerifyTransmittalLogIsDisplayed(string transmittalNumber, bool noRecordExpected = false);
        public abstract bool VerifyTransmittalLogIsDisplayed(TableTab tableTab, string transmittalNumber, bool noRecordExpected = false);

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
        public virtual bool VerifyIsMultiTabGrid() => false;

        public virtual void LogintoCorrespondenceLogPage(UserType userType)
        {
            LoginAs(userType);
            WaitForPageReady();
            NavigateToPage.RMCenter_Project_Correspondence_Log();
            AssertToList_VerifyPageHeader("Transmissions", "LogintoCorrespondenceLogPage()");
        }

        public virtual IList<EntryField> SetTenantRequiredFieldsList()
            => tenantExpectedRequiredFields;

        public virtual IList<EntryField> SetTenantAllEntryFieldsList()
            => tenantAllEntryFields;

        public virtual IList<EntryField> SetTenantEntryFieldsForTableColumns()
            => expectedEntryFieldsForTblColumns;

        public virtual IList<TableTab> SetTenantTableTabsList()
            => tenantTableTabs = new List<TableTab>() { };

        public virtual string CreateNewAndPopulateFields()
        {
            ClickNew();
            WaitForPageReady();
            ClickSaveForward();
            AddAssertionToList(VerifyTransmissionDetailsRequiredFields(), "VerifyTransmissionDetailsRequiredFields");
            string transmittalNumber = PopulateAllFields();
            UploadFile();
            ClickSave();
            AssertToList_VerifyPageHeader("Transmissions", "CreateNewAndPopulateFields()");
            return transmittalNumber;
        }

        public virtual void VerifyTransmissionDetailsPageValuesInRemainingTableTabs(string transmittalNumber)
        {
            LogInfo($"Test step skipped for {tenantName} - tenant does not have tabbed table grid");
        }

        public virtual void VerifyTransmissionDetailsGridFilterInRemainingTableTabs(string transmittalNumber)
        {
            LogInfo($"Test step skipped for {tenantName} - tenant does not have tabbed table grid");
        }

        //For tenants I15SB, I15Tech, SG, SH249
        public virtual void ClickViewBtnForTransmissionsRow()
            => ClickViewBtnForRow("", false, false);

        public virtual IList<By> SetTenantRequiredFieldLocators()
        {
            return reqFieldLocators = new List<By>()
            {
                By.XPath("//span[contains(text(),'Required')]"),
            };
        }

        public abstract bool VerifyTransmittalLogIsDisplayedByGridColumnFilter();
        public abstract bool VerifyTransmittalLocationBySearch();
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
            => ClickViewBtnForRow("", true, false);

        public override IList<EntryField> SetTenantRequiredFieldsList()
        {
            return tenantExpectedRequiredFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.SecurityClassification,
                EntryField.Title,
                EntryField.DocumentType,
                EntryField.Transmitted,
                EntryField.Attachments
            };
        }

        public override IList<EntryField> SetTenantAllEntryFieldsList()
        {
            return tenantAllEntryFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.SecurityClassification,
                EntryField.Title,
                EntryField.From,
                EntryField.AgencyFrom,
                EntryField.Attention,
                EntryField.AgencyAttention,
                EntryField.DocumentCategory,
                EntryField.DocumentType,
                EntryField.OriginatorDocumentRef,
                EntryField.Revision,
                EntryField.Transmitted,
                EntryField.Segment_Area,
                EntryField.DesignPackages,
                EntryField.CDRL,
                EntryField.ResponseRequired_Yes,
                EntryField.ResponseRequiredBy_Date,
                EntryField.SpecSection,
                EntryField.MSLNumber,
                EntryField.Access
            };
        }

        public override IList<EntryField> SetTenantEntryFieldsForTableColumns()
        {
            return expectedEntryFieldsForTblColumns = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.OriginatorDocumentRef,
                EntryField.Revision,
                EntryField.Transmitted,
                EntryField.MSLNumber
            };
        }

        public override IList<TableTab> SetTenantTableTabsList()
            => tenantTableTabs = new List<TableTab>()
            {
                TableTab.PendingTransmissions,
                TableTab.TransmittedRecords
            };

        public override void VerifyTransmissionDetailsPageValuesInRemainingTableTabs(string transmittalNumber)
        {
            tenantTableTabs = SetTenantTableTabsList();
            IterrateOverRemainingTableTabs_DetailsPageValues(transmittalNumber, tenantTableTabs);
        }

        public override void VerifyTransmissionDetailsGridFilterInRemainingTableTabs(string transmittalNumber)
        {
            tenantTableTabs = SetTenantTableTabsList();
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
            WaitForPageReady();
            NavigateToPage.RMCenter_Project_Transmittal_Log();
        }

        public override IList<By> SetTenantRequiredFieldLocators()
        {
            return reqFieldLocators = new List<By>()
            {
                By.XPath("//span[contains(text(),'Required')]"),
                By.XPath("//span[contains(text(),'required')]")
            };
        }

        public override IList<EntryField> SetTenantRequiredFieldsList()
        {
            return tenantExpectedRequiredFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.Title,
                EntryField.From,
                EntryField.DocumentType,
                EntryField.Via,
                EntryField.Attachments
            };
        }

        public override IList<EntryField> SetTenantAllEntryFieldsList()
        {
            return tenantAllEntryFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.Via
            };
        }

        public override IList<EntryField> SetTenantEntryFieldsForTableColumns()
        {
            return expectedEntryFieldsForTblColumns = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.Via
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
            WaitForPageReady();
            NavigateToPage.RMCenter_Project_Transmittal_Log();
        }

        public override IList<By> SetTenantRequiredFieldLocators()
        {
            return reqFieldLocators = new List<By>()
            {
                By.XPath("//span[contains(text(),'Required')]"),
                By.XPath("//span[contains(text(),'required')]")
            };
        }

        public override IList<EntryField> SetTenantRequiredFieldsList()
        {
            return tenantExpectedRequiredFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.Attachments
            };
        }

        public override IList<EntryField> SetTenantAllEntryFieldsList()
        {
            return tenantAllEntryFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.MSLNumber,
                EntryField.Via
            };
        }

        public override IList<EntryField> SetTenantEntryFieldsForTableColumns()
        {
            return expectedEntryFieldsForTblColumns = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.MSLNumber,
                EntryField.Via
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

        public override IList<EntryField> SetTenantRequiredFieldsList()
        {
            return tenantExpectedRequiredFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                //EntryField.DocumentCategory,
                EntryField.DocumentType,
                EntryField.Attachments
            };
        }

        public override IList<EntryField> SetTenantAllEntryFieldsList()
        {
            return tenantAllEntryFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentCategory,
                EntryField.DocumentType
            };
        }

        public override IList<EntryField> SetTenantEntryFieldsForTableColumns()
        {
            return expectedEntryFieldsForTblColumns = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType
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


        public override IList<EntryField> SetTenantRequiredFieldsList()
        {
            return tenantExpectedRequiredFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                //EntryField.DocumentCategory,
                EntryField.DocumentType,
                EntryField.Attachments,
                EntryField.Transmitted
            };
        }

        public override IList<EntryField> SetTenantAllEntryFieldsList()
        {
            return tenantAllEntryFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentCategory,
                EntryField.DocumentType,
                EntryField.Transmitted
            };
        }

        public override IList<EntryField> SetTenantEntryFieldsForTableColumns()
        {
            return expectedEntryFieldsForTblColumns = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.Transmitted
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
            => ClickViewBtnForRow("", true, false);

        public override IList<EntryField> SetTenantRequiredFieldsList()
        {
            return tenantExpectedRequiredFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.SecurityClassification,
                EntryField.Title,
                EntryField.DocumentType,
                EntryField.Transmitted,
                EntryField.Attachments
            };
        }

        public override IList<EntryField> SetTenantAllEntryFieldsList()
        {
            return tenantAllEntryFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.SecurityClassification,
                EntryField.Title,
                EntryField.From,
                EntryField.AgencyFrom,
                EntryField.Attention,
                EntryField.AgencyAttention,
                EntryField.DocumentCategory,
                EntryField.DocumentType,
                EntryField.OriginatorDocumentRef,
                EntryField.Revision,
                EntryField.Transmitted,
                EntryField.Segment_Area,
                EntryField.Access
            };
        }

        public override IList<EntryField> SetTenantEntryFieldsForTableColumns()
        {
            return expectedEntryFieldsForTblColumns = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.OriginatorDocumentRef,
                EntryField.Revision,
                EntryField.Transmitted,
            };
        }

        public override IList<TableTab> SetTenantTableTabsList()
            => tenantTableTabs = new List<TableTab>()
            {
                TableTab.TransmittedRecords
            };

        public override void VerifyTransmissionDetailsPageValuesInRemainingTableTabs(string transmittalNumber)
        {
            tenantTableTabs = SetTenantTableTabsList();
            IterrateOverRemainingTableTabs_DetailsPageValues(transmittalNumber, tenantTableTabs);
        }

        public override void VerifyTransmissionDetailsGridFilterInRemainingTableTabs(string transmittalNumber)
        {
            tenantTableTabs = SetTenantTableTabsList();
            IterrateOverRemainingTableTabs_GridColumnFilters(transmittalNumber, tenantTableTabs);
        }

    }

    #endregion Implementation specific to LAX

}