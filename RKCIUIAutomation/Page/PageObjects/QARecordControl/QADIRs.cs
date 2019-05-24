﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RestSharp.Extensions;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QADIRs;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.TableHelper;

namespace RKCIUIAutomation.Page.PageObjects.QARecordControl
{
    #region DIR/IDR/DWR Generic Class

    public class QADIRs : QADIRs_Impl
    {
        public QADIRs()
        {
        }

        public QADIRs(IWebDriver driver) => this.Driver = driver;

        public enum InputFields
        {
            [StringValue("InspectDate")] Inspect_Date,
            [StringValue("TimeBegin1")] Time_Begin,
            [StringValue("TimeEnd1")] Time_End,
            [StringValue("DIREntries_0__AverageTemperature")] Average_Temperature,
            [StringValue("DIREntries_0__AreaID")] Area,
            [StringValue("DIREntries_0__DivisionID")] Division, //SH249
            [StringValue("DIREntries_0__BidItemCodeID")] Bid_Item_Code, //SH249
            [StringValue("DIREntries_0__SectionId")] Spec_Section,
            [StringValue("DIREntries_0__SpecSectionId")] Spec_Section_Paragraph,
            [StringValue("DIREntries_0__FeatureID")] Feature, //requires selection of Area DDL
            [StringValue("DIREntries_0__HoldPointNo")] Control_Point_Number,
            [StringValue("DIREntries_0__HoldPointTypeID")] Control_Point_Type,
            [StringValue("DIREntries_0__SubFeatureId")] SubFeature,//not required
            [StringValue("DIREntries_0__ContractorId")] Contractor,
            [StringValue("DIREntries_0__CrewID")] Crew, //SH249
            [StringValue("DIREntries_0__CrewForemanID")] Crew_Foreman, //requires selection of Contractor DDL
            [StringValue("DIREntries_0__SectionDescription")] Section_Description, //matches Spec Section selection
            [StringValue("DIREntries_0__DeficiencyDescription")] Deficiency_Description, //not required
            [StringValue("DIREntries_0__DateReady")] Date_Ready, //LAX
            [StringValue("DIREntries_0__DateCompleted")] Date_Completed, //LAX
            [StringValue("DIREntries_0__DateOnlyReady")] DateOnly_Ready, //SG
            [StringValue("DIREntries_0__TimeOnlyReady")] TimeOnly_Ready, //SG
            [StringValue("DIREntries_0__DateOnlyCompleted")] DateOnly_Completed, //SG
            [StringValue("DIREntries_0__TimeOnlyCompleted")] TimeOnly_Completed, //SG
            [StringValue("DIREntries_0__InspectionHours")] Total_Inspection_Time, //LAX
            [StringValue("DIREntries_0__EngineerComment")] Engineer_Comments
        }

        public enum TableTab
        {
            [StringValue("Creating")] Creating,
            [StringValue("Create/Revise")] Create_Revise,
            [StringValue("Attachments")] Attachments,
            [StringValue("QC Review")] QC_Review,
            [StringValue("Authorization")] Authorization,
            [StringValue("Revise")] Revise,
            [StringValue("To Be Closed")] To_Be_Closed,
            [StringValue("Closed")] Closed,
            [StringValue("Create Packages")] Create_Packages,
            [StringValue("Packages")] Packages
        }

        public enum ColumnName
        {
            [StringValue("DIRNO")] DIR_No,
            [StringValue("Revision")] Revision,
            [StringValue("CreatedBy")] Created_By,
            [StringValue("RevisedBy")] Sent_By,
            [StringValue("RevisedDate")] Sent_Date,
            [StringValue("LockedBy")] Locked_By,
            [StringValue("LockDate")] Locked_Date,
            [StringValue("Report")] Report,
            [StringValue("Action")] Action,
        }

        public enum PackagesColumnName
        {
            [StringValue("WeekStart", "Week Start")] Week_Start,
            [StringValue("WeekEnd", "Week End")] Week_End,
            [StringValue("PackageNumber", "Package number")] Package_Number,
            [StringValue("DIRsToString", "DIRs")] DIRs,
            [StringValue("DirsCount", "New DIR Count")] New_DIR_Count,
            [StringValue("DIRsToString", "New DIRs")] New_DIRs,
            [StringValue("RecreateRequired")] Recreate
        }

        public enum SubmitButtons
        {
            [StringValue("Create New")] Create_New,
            [StringValue("Create Revision")] Create_Revision,
            [StringValue("Refresh")] Refresh,
            [StringValue("Cancel")] Cancel,
            [StringValue("Kick Back")] Kick_Back,
            [StringValue("Send To Attachment")] Send_To_Attachment,
            [StringValue("Save")] Save,
            [StringValue("Save & Forward")] Save_Forward,
            [StringValue("Save & Edit")] Save_Edit,
            [StringValue("Approve")] Approve,
            [StringValue("Add")] Add,
            [StringValue("Delete")] Delete,
            [StringValue("Submit Revise")] Submit_Revise,
            [StringValue("No Error")] No_Error,
            [StringValue("Back To QC Review")] Back_To_QC_Review,
            [StringValue("Back To Field")] Back_To_Field,
            [StringValue("Revise")] Revise
        }

        public enum RadioBtnsAndCheckboxes
        {
            [StringValue("InspectionType_I_0")] Inspection_Type_I,
            [StringValue("InspectionType_C_0")] Inspection_Type_C,
            [StringValue("InspectionType_P_0")] Inspection_Type_P,
            [StringValue("InspectionType_H_0")] Inspection_Type_H,
            [StringValue("InspectionType_R_0")] Inspection_Type_R,
            [StringValue("DIREntries_0__InspectionTypeI")] Inspection_Type_I_forSG,
            [StringValue("DIREntries_0__InspectionTypeP")] Inspection_Type_P_forSG,
            [StringValue("DIREntries_0__InspectionTypeR")] Inspection_Type_R_forSG,
            [StringValue("InspectionPassFail_P_0")] Inspection_Result_P,
            [StringValue("InspectionPassFail_E_0")] Inspection_Result_E,
            [StringValue("InspectionPassFail_F_0")] Inspection_Result_F,
            [StringValue("InspectionPassFail_NA_0")] Inspection_Result_NA, //GLX
            [StringValue("sendNotification1")] SendEmailNotification_Yes,
            [StringValue("sendNotification2")] SendEmailNotification_No,
            [StringValue("DIREntries_0__Deficiency_6")] Deficiencies_Yes,
            [StringValue("DIREntries_0__Deficiency_0")] Deficiencies_No,
            [StringValue("DIREntries_0__Deficiency_1")] Deficiencies_CIF,
            [StringValue("DIREntries_0__Deficiency_3")] Deficiencies_CDR,
            [StringValue("DIREntries_0__Deficiency_2")] Deficiencies_NCR
        }

        public enum RequiredFieldType
        {
            Default,
            ControlPoint,
            EngineerComments
        }

        [ThreadStatic]
        internal static string dirNumber;

        [ThreadStatic]
        internal static string dirNumberKey;

        public override void SetDirNumber()
        {
            dirNumber = PageAction.GetAttribute(By.Id("DIRNO"), "value");
            dirNumberKey = $"DIR_varKey";
            CreateVar(dirNumberKey, dirNumber);
            log.Debug($"#####Stored DIR Number - KEY: {dirNumberKey} || VALUE: {GetVar(dirNumberKey)}");
        }

        internal string FormatInspectionTimesIDs(string id)
        {
            if (id.Equals("DateReady"))
            {
                id = "Ready";
            }
            else if (id.Equals("DateCompleted"))
            {
                id = "Completed Date";
            }
            else if (id.Equals("InspectionHours"))
            {
                id = "Total Inspection Time";
            }
            else if (id.Contains("Time"))
            {
                id = Regex.Replace(id, "Only", "");
            }

            return id;
        }

        public override IList<string> TrimInputFieldIDs(IList<string> fieldIdList, string splitPattern)
        {
            IList<string> trimmedList = null;

            try
            {
                trimmedList = new List<string>();

                foreach (string fieldId in fieldIdList)
                {
                    string[] split = new string[2];
                    string id = string.Empty;

                    if (fieldId.Contains(splitPattern))
                    {
                        split = Regex.Split(fieldId, splitPattern);
                        id = split[1];

                        string[] newId = new string[2];
                        if (id.Contains("Id"))
                        {
                            newId = Regex.Split(id, "Id");
                            id = newId[0];
                        }
                        else if (id.Contains("HoldPoint"))
                        {
                            if (id.Equals("HoldPointTypeID"))
                            {
                                bool complexWfTenant = tenantName == TenantName.SGWay || tenantName == TenantName.SH249 ? true : false;
                                id = complexWfTenant ? "HoldPoint Type" : "ControlPoint No.";
                            }
                            else
                                id = "ControlPoint Type";
                        }
                        else if (id.Equals("EngineerComment"))
                        {
                            id = $"{id}s";
                        }
                        else if (id.Contains("ID") && !id.Contains("HoldPoint"))
                        {
                            newId = Regex.Split(id, "ID");
                            id = newId[0];
                        }
                        else if (id.Contains("PassFail"))
                        {
                            id = Regex.Replace(id, "PassFail", "Result");
                        }
                        else if (id.Equals("DateReady") || id.Equals("DateCompleted") || id.Equals("InspectionHours"))
                        {
                            id = FormatInspectionTimesIDs(id);
                        }
                        else if (id.Contains("Only"))
                        {
                            id = Regex.Replace(id, "Only", "");
                            id = FormatInspectionTimesIDs(id);

                            if (!id.Contains(" "))
                            {
                                id = id.SplitCamelCase();
                            }
                        }

                        if (!fieldId.Contains("Time"))
                        {
                            if (!id.Contains(" "))
                            {
                                id = id.SplitCamelCase();

                                if (id.Equals("SpecSection") || id.Equals("Spec Section"))
                                {
                                    id = $"{id} Paragraph";
                                }
                            }
                        }
                    }
                    else if (fieldId.Equals("InspectionPassFail") || fieldId.Equals("InspectionType"))
                    {
                        id = fieldId.Equals("InspectionPassFail") ? Regex.Replace(fieldId, "PassFail", "Result") : fieldId;
                        id = id.SplitCamelCase();
                    }
                    else
                    {
                        id = fieldId;
                    }

                    trimmedList.Add(id);
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return trimmedList;
        }

        public override bool VerifyExpectedRequiredFields(IList<string> actualRequiredFieldIDs, IList<string> expectedRequiredFieldIDs)
        {
            int expectedCount = 0;
            int actualCount = 0;
            bool countsMatch = false;
            bool reqFieldsMatch = false;

            try
            {
                IList<string> trimmedExpectedIDs = TrimInputFieldIDs(expectedRequiredFieldIDs, "0__");
                IList<bool> results = new List<bool>();

                expectedCount = trimmedExpectedIDs.Count;
                actualCount = actualRequiredFieldIDs.Count;
                countsMatch = expectedCount.Equals(actualCount);

                int tblRowIndex = 0;
                string[][] idTable = new string[expectedCount + 2][];
                idTable[tblRowIndex] = new string[2] { $"|  Expected ID  | ", $" |  Found Matching Actual ID  | " };

                if (countsMatch)
                {
                    for (int i = 0; i < expectedCount; i++)
                    {
                        tblRowIndex++;
                        string actualID = actualRequiredFieldIDs[i];
                        reqFieldsMatch = trimmedExpectedIDs.Contains(actualID);
                        results.Add(reqFieldsMatch);

                        string tblRowNumber = tblRowIndex.ToString();
                        tblRowNumber = (tblRowNumber.Length == 1) ? $"0{tblRowNumber}" : tblRowNumber;
                        idTable[tblRowIndex] = new string[2] { $"{tblRowNumber}: {actualID} : ", $"{reqFieldsMatch.ToString()}" };
                    }
                }
                else
                {
                    Report.Info($"Expected and Actual Required Field Counts DO NOT MATCH:" +
                        $"<br>Expected Count: {expectedCount}<br>Actual Count: {actualCount}", countsMatch);
                }

                reqFieldsMatch = results.Contains(false) ? false : true;
                idTable[tblRowIndex + 1] = new string[2] { "Total Required Fields:", (results.Count).ToString() };
                Report.Info(idTable, reqFieldsMatch);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return reqFieldsMatch;
        }

        public override void CloseErrorSummaryPopupMsg()
            => PageAction.JsClickElement(By.XPath("//span[@id='ErrorSummaryWindow_wnd_title']/following-sibling::div/a[@aria-label='Close']"));

        public override IList<string> GetErrorSummaryIDs()
        {
            IList<string> errorElements = null;
            IList<string> extractedFieldNames = null;

            try
            {
                errorElements = PageAction.GetTextForElements(By.XPath("//div[contains(@class,'validation-summary-errors')]/ul/li"));
                extractedFieldNames = new List<string>();

                foreach (string error in errorElements)
                {
                    string[] splitType = new string[] { };

                    if (error.Contains("DIR:"))
                    {
                        splitType = Regex.Split(error, "Shift ");
                    }
                    else if (error.Contains("Entry Number"))
                    {
                        splitType = Regex.Split(error, "1: ");
                    }

                    string[] splitReq = new string[] { };
                    splitReq = Regex.Split(splitType[1], " Required");
                    string fieldName = splitReq[0];
                    extractedFieldNames.Add(fieldName);
                    log.Debug($"Adding Required Field Error Summary ID to Actuals list: {fieldName}");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return extractedFieldNames;
        }

        /// <summary>
        /// WeekStart[0], WeekEnd[1], NewDirCountOrPackageNumumber[2], NewDIRsOrDIRs[3]
        /// </summary>
        /// <param name="pkgsTab"></param>
        /// <param name="indexOfRow"></param>
        /// <returns></returns>
        internal string[] GetPackageDataForRow(TableTab pkgsTab, int indexOfRow = 1)
        {
            string rowXPath = $"//div[contains(@class,'k-state-active')]//tbody/tr[{indexOfRow}]/td";

            string weekStart = null;
            string weekEnd = null;
            string newDirCountOrPkgNum = null;
            string newDIRsOrDIRsToString = null;
            string[] pkgData = null;

            By locator(int indexOfCol) => By.XPath($"{rowXPath}[{indexOfCol.ToString()}]");

            try
            {
                weekStart = PageAction.GetText(locator(1));
                weekEnd = PageAction.GetText(locator(2));
                newDirCountOrPkgNum = PageAction.GetText(locator(3));
                newDIRsOrDIRsToString = pkgsTab.Equals(TableTab.Create_Packages)
                    ? PageAction.GetText(locator(5))
                    : PageAction.GetText(locator(4));

                pkgData = new string[4]
                {
                    weekStart,
                    weekEnd,
                    newDirCountOrPkgNum,
                    newDIRsOrDIRsToString
                };
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            return pkgData;
        }

        public override void Verify_Column_Filters_DirPackageTabs(TableTab pkgsTab, int indexOfRow = 1)
        {
            try
            {
                string[] pkgData = GetPackageDataForRow(pkgsTab, indexOfRow);

                PackagesColumnName columnName = PackagesColumnName.Week_Start;

                for (int i = 0; i < pkgData.Length; i++)
                {
                    string columnValue = pkgData[i];

                    switch (i)
                    {
                        //case 0:
                        //    columnName = PackagesColumnName.Week_Start;                          
                        //    break;
                        case 1:
                            columnName = PackagesColumnName.Week_End;
                            break;
                        case 2:
                            columnName = pkgsTab.Equals(TableTab.Create_Packages)
                                ? PackagesColumnName.New_DIR_Count
                                : PackagesColumnName.Package_Number;
                            break;
                        case 3:
                            columnName = pkgsTab.Equals(TableTab.Create_Packages)
                                ? PackagesColumnName.New_DIRs
                                : PackagesColumnName.DIRs;
                            break;
                    }

                    AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(columnName, columnValue, TableType.MultiTab, false), $"Verify Filter [{pkgsTab.ToString()} | {columnName.GetString()}] column Equals {columnValue}");

                    if (columnName.Equals(PackagesColumnName.New_DIRs) || (columnName.Equals(PackagesColumnName.DIRs)))
                    {
                        string[] arrayDirNums = new string[] { };
                        arrayDirNums = columnValue.Contains(",")
                            ? Regex.Split(columnValue, ", ")
                            : new string[1] { columnValue };

                        for (int a = 0; a < arrayDirNums.Length; a++)
                        {
                            columnValue = arrayDirNums[a];
                            AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(columnName, columnValue, TableType.MultiTab, false, FilterOperator.Contains), $"Verify Filter [{pkgsTab.ToString()} | {columnName.GetString()}] column Contains {columnValue}");
                        }
                    }


                    //ClearTableFilters();
                }

                //ClickCreateBtnForRow(indexOfRow.ToString());
                //AcceptAlertMessage();

                //QaRcrdCtrl_QaDIR.ClickTab_Packages();
                //AddAssertionToList(VerifyRecordIsDisplayed(PackagesColumnName.DIRs, newDIRs, TableType.MultiTab, false, FilterOperator.Contains), "Verify Package with DIRs List is Displayed");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }

        public override TOut GetDirPackagesDataForRow<TOut>(PackagesColumnName packagesColumnName, int rowIndex = 1)
        {
            BaseUtils baseUtils = new BaseUtils();
            string colName = packagesColumnName.GetString(true);
            string tblData = GridHelper.GetColumnValueForRow(rowIndex, colName);

            if (packagesColumnName == PackagesColumnName.New_DIRs || packagesColumnName == PackagesColumnName.DIRs)
            {
                string[] arrayArg = new string[] { };
                arrayArg = Regex.Split(tblData, ", ");
                return baseUtils.ConvertToType<TOut>(arrayArg);
            }
            else
            {
                return baseUtils.ConvertToType<TOut>(tblData);
            }
        }

        public override bool VerifySpecSectionDescriptionAutoPopulatedData()
        {
            string specSectionParagraphText = PageAction.GetTextFromDDL(InputFields.Spec_Section_Paragraph);
            string spectionDescriptionText = PageAction.GetText(By.Id(InputFields.Section_Description.GetString()));
            bool valuesMatch = specSectionParagraphText.HasValue() && spectionDescriptionText.HasValue()
                ? specSectionParagraphText.Contains(spectionDescriptionText)
                : false;

            Report.Info($"EXPECTED: {specSectionParagraphText}<br>ACTUAL: {spectionDescriptionText}", valuesMatch);
            return valuesMatch;
        }

        public override void PopulateRequiredFields()
        {
        }

        public override bool VerifyControlPointReqFieldErrors()
            => QaRcrdCtrl_QaDIR.VerifyReqFieldErrorsForNewDir(QaRcrdCtrl_QaDIR.GetExpectedHoldPointReqFieldIDsList(), RequiredFieldType.ControlPoint);

        public override bool VerifyEngineerCommentsReqFieldErrors()
            => QaRcrdCtrl_QaDIR.VerifyReqFieldErrorsForNewDir(QaRcrdCtrl_QaDIR.GetExpectedEngineerCommentsReqFieldIDsList(), RequiredFieldType.EngineerComments);

        public override bool VerifyDirNumberExistsInDbError()
        {
            By errorLocator = By.Id("error");
            IWebElement errorElem = null;
            bool isDisplayed = false;
            string logMsg = string.Empty;

            try
            {
                errorElem = PageAction.GetElement(errorLocator);
                isDisplayed = (bool)errorElem?.Displayed ? true : false;
                logMsg = isDisplayed ? "" : " not";
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            Report.Info($"'This DIR No. exists in the database' error message is{logMsg} displayed", isDisplayed);
            return isDisplayed;
        }

        public override bool VerifyDirRevisionInDetailsPage(string expectedDirRev)
        {
            By revLocator = By.XPath("//label[contains(text(),'Revision')]/parent::div");
            bool revAsExpected = false;
            string actualDirRev = string.Empty;
            string logMsg = string.Empty;

            try
            {
                actualDirRev = PageAction.GetText(revLocator);
                string[] splitActual = new string[2];
                splitActual = Regex.Split(actualDirRev, "Revision\r\n");
                actualDirRev = splitActual[1];
                revAsExpected = actualDirRev.Equals(expectedDirRev);
                var ifFalseLog = revAsExpected ? "" : $"<br>Actual DIR Rev: {actualDirRev}";
                logMsg = $"Expected DIR Rev: {expectedDirRev}{ifFalseLog}<br>Actual DIR Rev Matches Expected: {revAsExpected}";
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            Report.Info($"{logMsg} ", revAsExpected);
            return revAsExpected;
        }
    }

    #endregion DIR/IDR/DWR Generic Class


    public interface IQADIRs
    {
        bool IsLoaded();

        void ClickBtn_CreateNew();

        void ClickBtn_CreateRevision();

        void ClickBtn_Refresh();

        void ClickBtn_Cancel();

        void ClickBtn_KickBack();

        void ClickBtn_SubmitRevise();

        void ClickBtn_Send_To_Attachment();

        void ClickBtn_Save();

        void ClickBtn_Save_Forward();

        void ClickBtn_Save_Edit();

        void ClickBtn_Approve();

        void ClickBtn_Add();

        void ClickBtn_Delete();

        void ClickBtn_NoError();

        void ClickBtn_Revise();

        void ClickBtn_Back_To_QC_Review();

        void ClickBtn_Back_To_Field();

        void ClickBtn_Close_Selected();

        void ClickBtn_View_Selected();

        void FilterTable_CreatePackagesTab(int indexOfRow = 1);

        void FilterTable_PackagesTab(int indexOfRow = 1);

        void FilterDirNumber(string DirNumber);

        void PopulateRequiredFields();

        string GetDirNumber();

        void SetDirNumber();

        void SelectDDL_TimeBegin(TimeBlock shiftStartTime = TimeBlock.AM_06_00);

        void SelectDDL_TimeEnd(TimeBlock shiftEndTime = TimeBlock.PM_04_00);

        void Enter_AverageTemp(int avgTemp = 80);

        void SelectDDL_Area(int ddListSelection = 1);

        void SelectDDL_SpecSection(int ddListSelection = 1);

        void SelectDDL_SpecSectionParagraph(int ddListSelection = 1);

        void SelectDDL_Division(int ddListSelection = 1);

        void SelectDDL_BidItemCode(int ddListSelection = 1);

        void SelectDDL_Feature(int ddListSelection = 1);

        void SelectDDL_ControlPointNumber(int ddListSelection = 1);

        void SelectDDL_HoldPointType(int ddListSelection = 1);

        void SelectDDL_Contractor(int ddListSelection = 1);

        void SelectDDL_Contractor<T>(T ddListSelection);

        void SelectDDL_Crew(int ddListSelection = 1);

        void SelectDDL_CrewForeman(int ddListSelection = 1);

        void SelectChkbox_InspectionType_I(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionType_C(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionType_P(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionType_H(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionType_R(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionResult_P(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionResult_E(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionResult_F(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionResult_NA(bool toggleChkboxIfAlreadySelected = true);

        void SelectRdoBtn_SendEmailForRevise_Yes();

        void SelectRdoBtn_SendEmailForRevise_No();

        void SelectRdoBtn_Deficiencies_Yes();

        void SelectRdoBtn_Deficiencies_No();

        void ClickTab_Create_Revise();

        void ClickTab_QC_Review();

        void ClickTab_Authorization();

        void ClickTab_Creating();

        void ClickTab_Attachments();

        void ClickTab_Revise();

        void ClickTab_To_Be_Closed();

        void ClickTab_Closed();

        void ClickTab_Create_Packages();

        void ClickTab_Packages();

        void EnterText_DeficiencyDescription(string desc = "");

        void EnterText_SectionDescription(string desc = "");

        void EnterText_EngineerComments(string comment = "");

        void Enter_ReadyDateTime(string shortDate = "", TimeBlock shortTime = TimeBlock.AM_12_00);

        void Enter_CompletedDateTime(string shortDate = "", TimeBlock shortTime = TimeBlock.AM_12_00);

        void Enter_TotalInspectionTime();

        IList<string> GetExpectedRequiredFieldIDsList();

        bool VerifyAndCloseDirLockedMessage();

        bool VerifySectionDescription();

        IList<Enum> GetResultCheckBoxIDsList();

        IList<Enum> GetDeficienciesRdoBtnIDsList();

        bool VerifyDeficiencySelectionPopupMessages();

        bool VerifyDirIsDisplayed(TableTab tableTab, string dirNumber = "", bool noRecordsExpected = false);

        IList<string> GetExpectedHoldPointReqFieldIDsList();

        IList<string> GetExpectedEngineerCommentsReqFieldIDsList();

        bool VerifyReqFieldErrorsForNewDir(IList<string> expectedRequiredFieldIDs = null, RequiredFieldType requiredFieldType = RequiredFieldType.Default);

        bool VerifyControlPointReqFieldErrors();

        bool VerifyEngineerCommentsReqFieldErrors();

        bool VerifyDirNumberExistsInDbError();

        bool VerifyDirRevisionInDetailsPage(string expectedDirRev);

        string CreatePreviousFailingReport();

        bool VerifyPreviousFailingDirEntry(string previousDirNumber);

        string GetDirNumberForRow(string textInRowForAnyColumn);

        bool VerifyAutoSaveTimerRefresh();

        void RefreshAutoSaveTimer();

        bool Verify_Package_Download();

        bool Verify_Package_Created(string weekStart, string[] dirNumbers);

        string GetDirPackageWeekStartFromRow(int rowIndex = 1);

        string GetDirPackageWeekEndFromRow(int rowIndex = 1);

        string GetDirPackageNewDirCountFromRow(int rowIndex = 1);

        string GetDirPackageNumberFromRow(int rowIndex = 1);

        string[] GetDirPackageDirNumbersFromRow(PackagesColumnName NewDIRsOrDIRs, int rowIndex = 1);

        string CalculateDirPackageNumber(string weekStartDate);

        void EnterText_InspectionDate(string inspectDate);

        /// <summary>
        /// Returns TechID for [UserType]DIRTechQA by default
        /// </summary>
        /// <param name="dirNoTechFromDDL"></param>
        /// <returns></returns>
        string GetTechIdForDirUserAcct(bool selectUserFromDDList = false, UserType dirNoDDListTech = UserType.DIRTechQA);

        void VerifyRecreateBtnIsDisplayed(string packageNumber, string newDirNumber);

        void CloseErrorSummaryPopupMsg();

        IList<string> GetErrorSummaryIDs();

        IList<string> TrimInputFieldIDs(IList<string> fieldIdList, string splitPattern);

        bool VerifyExpectedRequiredFields(IList<string> actualRequiredFieldIDs, IList<string> expectedRequiredFieldIDs);

        void Verify_Column_Filters_DirPackageTabs(TableTab pkgsTab, int indexOfRow = 1);

        TOut GetDirPackagesDataForRow<TOut>(PackagesColumnName packagesColumnName, int rowIndex = 1);

        bool VerifySpecSectionDescriptionAutoPopulatedData();
    }

    public abstract class QADIRs_Impl : TestBase, IQADIRs
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Tenant'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        public IQADIRs SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IQADIRs instance = new QADIRs(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using QADIRs_SGWay instance ###### ");
                instance = new QADIRs_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using QADIRs_SH249 instance ###### ");
                instance = new QADIRs_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using QADIRs_Garnet instance ###### ");
                instance = new QADIRs_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using QADIRs_GLX instance ###### ");
                instance = new QADIRs_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using QADIRs_I15South instance ###### ");
                instance = new QADIRs_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using QADIRs_I15Tech instance ###### ");
                instance = new QADIRs_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using QADIRs_LAX instance ###### ");
                instance = new QADIRs_LAX(driver);
            }
            return instance;
        }

        //QADIRs QaDIRs_Base => new QADIRs(Driver);

        public virtual string GetDirNumber()
            => dirNumber;

        public abstract void SetDirNumber();
            //=> QaDIRs_Base.StoreDirNumber();

        public virtual bool IsLoaded()
            => Driver.Title.Equals("DIR List - ELVIS PMC");

        public virtual void ClickBtn_CreateNew()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Create_New));

        public virtual void ClickBtn_CreateRevision()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Create_Revision));

        public virtual void ClickBtn_Refresh()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Refresh, false));

        public virtual void ClickBtn_Cancel()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Cancel));

        public virtual void ClickBtn_KickBack()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Kick_Back, false));

        public virtual void ClickBtn_SubmitRevise()
           => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Submit_Revise, false));

        public virtual void ClickBtn_Send_To_Attachment()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Send_To_Attachment));

        public virtual void ClickBtn_Save()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Save));

        public virtual void ClickBtn_Save_Forward()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Save_Forward));

        public virtual void ClickBtn_Save_Edit()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Save_Edit));
        
        public virtual void ClickBtn_Approve()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Approve));

        public virtual void ClickBtn_Add()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Add));

        public virtual void ClickBtn_Delete()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Delete));

        public virtual void ClickBtn_NoError()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.No_Error));

        public virtual void ClickBtn_Back_To_QC_Review()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Back_To_QC_Review, false));

        public virtual void ClickBtn_Back_To_Field()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Back_To_Field, false));

        public virtual void ClickBtn_Revise()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Revise, false));

        public virtual void ClickBtn_Close_Selected()
            => PageAction.JsClickElement(By.XPath("//button[text()='Close Selected']"));

        public virtual void ClickBtn_View_Selected()
            => PageAction.JsClickElement(By.XPath("//button[text()='View Selected']"));

        public virtual void ClickTab_Create_Revise()
            => GridHelper.ClickTab(TableTab.Create_Revise);

        public virtual void ClickTab_QC_Review()
            => GridHelper.ClickTab(TableTab.QC_Review);

        public virtual void ClickTab_Authorization()
            => GridHelper.ClickTab(TableTab.Authorization);

        public virtual void ClickTab_Creating()
            => GridHelper.ClickTab(TableTab.Creating);

        public virtual void ClickTab_Attachments()
            => GridHelper.ClickTab(TableTab.Attachments);

        public virtual void ClickTab_Revise()
            => GridHelper.ClickTab(TableTab.Revise);

        public virtual void ClickTab_To_Be_Closed()
            => GridHelper.ClickTab(TableTab.To_Be_Closed);

        public virtual void ClickTab_Closed()
            => GridHelper.ClickTab(TableTab.Closed);

        public virtual void ClickTab_Create_Packages()
            => GridHelper.ClickTab(TableTab.Create_Packages);

        public virtual void ClickTab_Packages()
            => GridHelper.ClickTab(TableTab.Packages);

        public virtual void FilterDirNumber(string DirNumber)
            => GridHelper.FilterTableColumnByValue(ColumnName.DIR_No, DirNumber);

        //All SimpleWF Tenants have different required fields
        public abstract void PopulateRequiredFields();

        //All SimpleWF Tenants have different required fields
        public virtual IList<string> GetExpectedRequiredFieldIDsList()
        {
            IList<string> RequiredFieldIDs = new List<string>()
            {
                //InputFields.Time_Begin.GetString(),
                //InputFields.Time_End.GetString(),
                //InputFields.Area.GetString(),
                //InputFields.Average_Temperature.GetString(),
                //InputFields.Spec_Section.GetString(),
                //InputFields.Section_Description.GetString(),
                //InputFields.Feature.GetString(),
                //InputFields.Crew_Foreman.GetString(),
                //InputFields.Contractor.GetString(),
                //"InspectionType",
                //"InspectionPassFail",
                //InputFields.Date_Ready.GetString(),
                //InputFields.Date_Completed.GetString(),
                //InputFields.Total_Inspection_Time.GetString()
            };

            return RequiredFieldIDs;
        }

        public virtual string CreatePreviousFailingReport()
        {
            string logMsg = string.Empty;
            string nonFixedFailedDirNumber = string.Empty;
            bool modalIsDisplayed = false;
            IAction pgAction = PageAction;

            try
            {
                pgAction.ClickElement(By.Id("lnkPreviousFailingReports"));
                string modalXPath = "//div[contains(@style, 'opacity: 1')]";
                IWebElement modal = pgAction.GetElement(By.XPath(modalXPath));
                modalIsDisplayed = (bool)modal?.Displayed;

                if (modalIsDisplayed)
                {
                    string firstNonFixedFailedDirXpath = $"{modalXPath}//div[@class='col-md-3'][1]//div[@class='checkbox-inline']/span";
                    string nonfixedFailedDirVal = pgAction.GetText(By.XPath(firstNonFixedFailedDirXpath));
                    string[] splitVal = new string[2];
                    splitVal = Regex.Split(nonfixedFailedDirVal, "-");
                    nonFixedFailedDirNumber = splitVal[0];
                    string nonFixedFailedDirEntry = splitVal[1];

                    string newBtnXPath = $"{modalXPath}//a[contains(@class,'k-grid-add')]";
                    string previousDirNoFieldXPath = $"{modalXPath}//input[@id='DirNO']";
                    string previousDirEntryFieldXPath = $"{modalXPath}//input[@id='EntryNo']";
                    string updateBtnXPath = $"{modalXPath}//a[contains(text(),'Update')]";
                    string modalCloseXPath = $"{modalXPath}//a[@aria-label='Close']";

                    pgAction.JsClickElement(By.XPath(newBtnXPath));
                    pgAction.EnterText(By.XPath(previousDirNoFieldXPath), nonFixedFailedDirNumber);
                    pgAction.EnterText(By.XPath(previousDirEntryFieldXPath), nonFixedFailedDirEntry);
                    pgAction.JsClickElement(By.XPath(updateBtnXPath));
                    pgAction.JsClickElement(By.XPath(modalCloseXPath));

                    logMsg = $"Entered Non-Fixed Failed DIR No. {nonFixedFailedDirNumber} and DIR Entry {nonFixedFailedDirEntry}";
                }
                else
                {
                    logMsg = "Previous Failing Report popup window is not displayed";
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            Report.Info(logMsg, modalIsDisplayed);
            return nonFixedFailedDirNumber;
        }

        public virtual bool VerifyPreviousFailingDirEntry(string previousDirNumber)
        {
            bool isDisplayed = false;
            string logMsg = string.Empty;

            try
            {
                string previousFailedDirTblXPath = $"//div[@id='PreviousFailedReportListGrid0']";
                By previousDirTblDataLocator = By.XPath($"{previousFailedDirTblXPath}//tbody/tr/td[text()='{previousDirNumber}']");
                PageAction.ScrollToElement(By.XPath($"{previousFailedDirTblXPath}/ancestor::div[@id='border']/following-sibling::div[1]"));
                isDisplayed = PageAction.ElementIsDisplayed(previousDirTblDataLocator);
                logMsg = isDisplayed ? "displayed" : "did not display";
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            Report.Info($"Previous Failing DIR table {logMsg} DIR Number {previousDirNumber}", isDisplayed);
            return isDisplayed;
        }

        public virtual void SelectDDL_TimeBegin(TimeBlock shiftStartTime = TimeBlock.AM_06_00)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Time_Begin, (int)shiftStartTime);

        public virtual void SelectDDL_TimeEnd(TimeBlock shiftEndTime = TimeBlock.PM_04_00)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Time_End, (int)shiftEndTime);

        public virtual void Enter_AverageTemp(int avgTemp = 80)
        {
            string avgTempFieldId = InputFields.Average_Temperature.GetString();
            By clickLocator = By.XPath($"//input[@id='{avgTempFieldId}']/preceding-sibling::input");
            By locator = By.XPath($"//input[@id='{avgTempFieldId}']");
            PageAction.ClickElement(clickLocator);
            PageAction.EnterText(locator, avgTemp.ToString(), false);
        }

        public virtual void SelectDDL_Area(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Area, ddListSelection);

        public virtual void SelectDDL_SpecSection(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Spec_Section, ddListSelection);

        public virtual void SelectDDL_SpecSectionParagraph(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Spec_Section_Paragraph, ddListSelection);

        public virtual void SelectDDL_Division(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Division, ddListSelection);

        public virtual void SelectDDL_BidItemCode(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Bid_Item_Code, ddListSelection);

        public virtual void SelectDDL_Feature(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Feature, ddListSelection);

        public virtual void SelectDDL_ControlPointNumber(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Control_Point_Number, ddListSelection);

        public virtual void SelectDDL_HoldPointType(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Control_Point_Type, ddListSelection);

        public virtual void SelectDDL_Contractor(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Contractor, ddListSelection);

        public virtual void SelectDDL_Contractor<T>(T ddListSelection)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Contractor, ddListSelection);

        public virtual void SelectDDL_Crew(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Crew, ddListSelection);

        public virtual void SelectDDL_CrewForeman(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFields.Crew_Foreman, ddListSelection);

        public virtual bool VerifySectionDescription()
        {
            bool textMatch = false;

            try
            {
                string sectionDesc = PageAction.GetText(GetTextAreaFieldByLocator(InputFields.Section_Description));
                string specSectionDDListValue = PageAction.GetTextFromDDL(InputFields.Spec_Section);
                specSectionDDListValue = specSectionDDListValue.Contains("-")
                    ? Regex.Replace(specSectionDDListValue, "-", " - ")
                    : specSectionDDListValue;

                textMatch = sectionDesc.Equals(specSectionDDListValue);
                Report.Info($"Spec Section DDList Selection: {specSectionDDListValue}<br>Section Description Text Value: {sectionDesc}", textMatch);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
            return textMatch;
        }

        public virtual bool VerifyDirIsDisplayed(TableTab tableTab, string dirNumber = "", bool noRecordsExpected = false)
        {
            bool isDisplayed = false;

            try
            {
                GridHelper.ClickTab(tableTab);
                string _dirNum = dirNumber.HasValue()
                    ? dirNumber
                    : GetDirNumber();

                isDisplayed = GridHelper.VerifyRecordIsDisplayed(ColumnName.DIR_No, _dirNum, TableType.MultiTab, noRecordsExpected);

                string logMsg = isDisplayed ? "Found" : "Unable to find";
                Report.Info($"{logMsg} record under {tableTab.GetString()} tab with DIR Number: {_dirNum}.", noRecordsExpected ? !isDisplayed : isDisplayed);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            return isDisplayed;
        }

        public virtual IList<Enum> GetResultCheckBoxIDsList()
        {
            IList<Enum> resultChkBoxIDs = new List<Enum>()
            {
                RadioBtnsAndCheckboxes.Inspection_Result_P,
                RadioBtnsAndCheckboxes.Inspection_Result_E,
            };

            return resultChkBoxIDs;
        }

        //I15SB, I15Tech
        public virtual IList<Enum> GetDeficienciesRdoBtnIDsList()
        {
            IList<Enum> deficienciesRdoBtnIDs = new List<Enum>()
            {
                RadioBtnsAndCheckboxes.Deficiencies_Yes,
                RadioBtnsAndCheckboxes.Deficiencies_CIF,
                RadioBtnsAndCheckboxes.Deficiencies_CDR,
                RadioBtnsAndCheckboxes.Deficiencies_NCR
            };

            return deficienciesRdoBtnIDs;
        }

        public virtual bool VerifyDeficiencySelectionPopupMessages()
        {
            IList<Enum> resultChkBoxIDs = QaRcrdCtrl_QaDIR.GetResultCheckBoxIDsList();

            IList<Enum> deficienciesRdoBtnIDs = QaRcrdCtrl_QaDIR.GetDeficienciesRdoBtnIDsList();

            string resultTypeMsg = string.Empty;
            string expectedAlertMsg = string.Empty;
            string alertMsg = string.Empty;
            bool alertMsgMatch = false;
            bool alertMsgExpected = false;
            IList<bool> assertList = new List<bool>();

            //Select Pass Result chkBx first, then Deficiency RdoBtn - "Since Pass checked, not allow to check any deficiency";
            //Select Deficiency RdoBtn, then select Pass Result chkBx - "There is a deficiency checked in this entry, not allow to pass!"

            //Select E Result chkBx first, then Deficiency RdoBtn - "Since Engineer Decision checked, not allow to check any deficiency"
            //Select Deficiency RdoBtn, then select E Result chkBx - "There is a deficiency checked in this entry, not allow to make engineer decision!"

            try
            {
                SelectRdoBtn_Deficiencies_No();
                SelectChkbox_InspectionResult_P(false);

                try
                {
                    foreach (Enum resultChkBox in resultChkBoxIDs)
                    {
                        PageAction.SelectRadioBtnOrChkbox(resultChkBox, false);

                        foreach (Enum deficiencyRdoBtn in deficienciesRdoBtnIDs)
                        {
                            PageAction.SelectRadioBtnOrChkbox(deficiencyRdoBtn);
                            resultTypeMsg = resultChkBox.Equals(RadioBtnsAndCheckboxes.Inspection_Result_P) ? "Pass" : "Engineer Decision";
                            expectedAlertMsg = $"Since {resultTypeMsg} checked, not allow to check any deficiency";
                            alertMsg = PageAction.AcceptAlertMessage(); //GetAlertMessage();
                            alertMsgMatch = alertMsg.Equals(expectedAlertMsg);
                            assertList.Add(alertMsgMatch);
                            Report.Info($"Selected : Result ( {resultTypeMsg} ) - Deficiency ( {deficiencyRdoBtn.ToString()} )<br>Expected Alert Msg: {expectedAlertMsg}<br>Actual Alert Msg: {alertMsg}", alertMsgMatch);
                        }
                    }
                }
                catch (UnhandledAlertException)
                {
                }

                SelectChkbox_InspectionResult_P(false); //Ensures Pass Result checkbox is selected
                SelectRdoBtn_Deficiencies_No();
                SelectChkbox_InspectionResult_P(); //Unchecks Pass Result checkbox

                try
                {
                    foreach (Enum deficiencyRdoBtn in deficienciesRdoBtnIDs)
                    {
                        PageAction.SelectRadioBtnOrChkbox(deficiencyRdoBtn);

                        foreach (Enum resultChkBox in resultChkBoxIDs)
                        {
                            PageAction.SelectRadioBtnOrChkbox(resultChkBox, false);
                            resultTypeMsg = resultChkBox.Equals(RadioBtnsAndCheckboxes.Inspection_Result_P) ? "pass" : "make engineer decision";
                            expectedAlertMsg = $"There is a deficiency checked in this entry, not allow to {resultTypeMsg}!";
                            alertMsg = PageAction.AcceptAlertMessage(); //GetAlertMessage();
                            alertMsgMatch = alertMsg.Equals(expectedAlertMsg);
                            assertList.Add(alertMsgMatch);
                            Report.Info($"Selected : Deficiency ( {deficiencyRdoBtn.ToString()} ) - Result ( {resultTypeMsg} )<br>Expected Alert Msg: {expectedAlertMsg}<br>Actual Alert Msg: {alertMsg}", alertMsgMatch);
                        }
                    }
                }
                catch (UnhandledAlertException)
                {
                }

                alertMsgExpected = assertList.Contains(false)
                    ? false
                    : true;
            }
            catch (UnhandledAlertException)
            { }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
            finally
            {
                SelectRdoBtn_Deficiencies_No();
                SelectChkbox_InspectionResult_P();
            }

            return alertMsgExpected;
        }

        public virtual void SelectChkbox_InspectionType_I(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Type_I, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_InspectionType_C(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Type_C, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_InspectionType_P(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Type_P, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_InspectionType_H(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Type_H, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_InspectionType_R(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Type_R, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_InspectionResult_P(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Result_P, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_InspectionResult_E(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Result_E, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_InspectionResult_F(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Result_F, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_InspectionResult_NA(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Result_NA, toggleChkboxIfAlreadySelected);

        public virtual void SelectRdoBtn_SendEmailForRevise_Yes()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.SendEmailNotification_Yes);

        public virtual void SelectRdoBtn_SendEmailForRevise_No()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.SendEmailNotification_No);

        public virtual void SelectRdoBtn_Deficiencies_Yes()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Deficiencies_Yes);

        public virtual void SelectRdoBtn_Deficiencies_No()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Deficiencies_No);

        public virtual void EnterText_DeficiencyDescription(string desc = "")
            => PageAction.EnterText(GetTextAreaFieldByLocator(InputFields.Deficiency_Description),
                desc = desc.Equals("") ? "RKCI Automation Deficiency Description" : desc);

        public virtual void EnterText_SectionDescription(string desc = "")
            => PageAction.EnterText(GetTextAreaFieldByLocator(InputFields.Section_Description),
                desc = desc.Equals("") ? "RKCI Automation Section Description" : desc);

        public virtual void EnterText_EngineerComments(string comment = "")
            => PageAction.EnterText(GetTextAreaFieldByLocator(InputFields.Engineer_Comments),
                comment = comment.Equals("") ? "RKCI Automation Engineer Comment" : comment);

        public virtual void Enter_ReadyDateTime(string shortDate = "", TimeBlock shortTime = TimeBlock.AM_12_00)
            => PageAction.EnterText(GetTextInputFieldByLocator(InputFields.Date_Ready), GetShortDateTime(shortDate, shortTime));

        public virtual void Enter_CompletedDateTime(string shortDate = "", TimeBlock shortTime = TimeBlock.AM_12_00)
            => PageAction.EnterText(GetTextInputFieldByLocator(InputFields.Date_Completed), GetShortDateTime(shortDate, shortTime));

        public virtual void Enter_TotalInspectionTime()
        {
            string inspectTimeFieldId = InputFields.Total_Inspection_Time.GetString();
            By clickLocator = By.XPath($"//input[@id='{inspectTimeFieldId}']/preceding-sibling::input");
            By locator = By.XPath($"//input[@id='{inspectTimeFieldId}']");
            PageAction.ClickElement(clickLocator);
            PageAction.EnterText(locator, "24", false);
        }

        public virtual bool VerifyAndCloseDirLockedMessage()
        {
            By msgBodyLocator = By.Id("dirLockedPopup");
            By msgTitleLocator = By.Id("dirLockedPopup_wnd_title");

            string expectedMsg = "Since you created this inspection report, you are not allowed to do QC Review or DIR Approval!";
            string actualMsg = PageAction.GetText(msgBodyLocator);

            bool msgMatch = actualMsg.Equals(expectedMsg);
            Report.Info($"Expected Msg: {expectedMsg}<br>Actual Msg: {actualMsg}", msgMatch);
            PageAction.ClickElement(By.XPath("//span[@id='dirLockedPopup_wnd_title']/following-sibling::div/a"));
            return msgMatch;
        }

        public virtual IList<string> GetExpectedHoldPointReqFieldIDsList()
        {
            IList<string> ControlPointReqFieldIDs = new List<string>()
            {
                InputFields.Control_Point_Number.GetString(),
                InputFields.Control_Point_Type.GetString()
            };

            return ControlPointReqFieldIDs;
        }

        public virtual IList<string> GetExpectedEngineerCommentsReqFieldIDsList()
        {
            IList<string> EngineerCommentsReqFieldID = new List<string>()
            {
                InputFields.Engineer_Comments.GetString()
            };

            return EngineerCommentsReqFieldID;
        }

        public virtual bool VerifyReqFieldErrorsForNewDir(IList<string> expectedRequiredFieldIDs = null, RequiredFieldType RequiredFieldType = RequiredFieldType.Default)
        {
            IList<bool> reqFieldAssertList = null;
            IList<string> errorSummaryIDs = null;
            bool requiredFieldsMatch = false;

            try
            {
                errorSummaryIDs = new List<string>();
                errorSummaryIDs = GetErrorSummaryIDs();

                string reqFieldIdXPath = string.Empty;
                string splitPattern = string.Empty;

                switch (RequiredFieldType)
                {
                    case RequiredFieldType.ControlPoint:
                        reqFieldIdXPath = "//span[contains(@aria-owns,'HoldPoint')]/input";
                        splitPattern = "0__";
                        expectedRequiredFieldIDs = expectedRequiredFieldIDs ?? QaRcrdCtrl_QaDIR.GetExpectedHoldPointReqFieldIDsList();
                        break;

                    case RequiredFieldType.EngineerComments:
                        reqFieldIdXPath = $"//textarea[@id='{InputFields.Engineer_Comments.GetString()}']";
                        splitPattern = "0__";
                        expectedRequiredFieldIDs = expectedRequiredFieldIDs ?? QaRcrdCtrl_QaDIR.GetExpectedEngineerCommentsReqFieldIDsList();
                        break;

                    default:
                        reqFieldIdXPath = "//span[text()='*']";
                        splitPattern = "0_";
                        expectedRequiredFieldIDs = expectedRequiredFieldIDs ?? QaRcrdCtrl_QaDIR.GetExpectedRequiredFieldIDsList();
                        break;
                }

                IList<string> trimmedActualIds = TrimInputFieldIDs(PageAction.GetAttributes(By.XPath(reqFieldIdXPath), "id"), splitPattern);

                bool actualMatchesExpected = VerifyExpectedRequiredFields(trimmedActualIds, expectedRequiredFieldIDs);

                if (actualMatchesExpected)
                {
                    reqFieldAssertList = new List<bool>();
                    int actualIdCount = trimmedActualIds.Count;
                    int expectedIdCount = expectedRequiredFieldIDs.Count;
                    int errorSummaryIdCount = errorSummaryIDs.Count;
                    bool countsMatch = actualIdCount.Equals(errorSummaryIdCount);
                    Report.Info($"Expected ID Count: {expectedIdCount}<br>Actual ID Count: {actualIdCount}<br>Required Field Error Summary ID Count: {errorSummaryIdCount}", countsMatch);

                    if (countsMatch)
                    {
                        bool summaryIDsContainID = false;

                        foreach (string id in trimmedActualIds)
                        {
                            summaryIDsContainID = errorSummaryIDs.Contains(id);
                            AddAssertionToList(summaryIDsContainID, $"{id} contained in Error Summary list");
                            reqFieldAssertList.Add(summaryIDsContainID);
                        }
                    }
                }

                requiredFieldsMatch = reqFieldAssertList.Contains(false) ? false : true;
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
            finally
            {
                CloseErrorSummaryPopupMsg();
            }

            return requiredFieldsMatch;
        }

        public virtual void FilterTable_CreatePackagesTab(int indexOfRow = 1)
            => Verify_Column_Filters_DirPackageTabs(TableTab.Create_Packages, indexOfRow);

        public virtual void FilterTable_PackagesTab(int indexOfRow = 1)
            => Verify_Column_Filters_DirPackageTabs(TableTab.Packages, indexOfRow);

        public virtual string GetDirNumberForRow(string textInRowForAnyColumn)
            => GridHelper.GetColumnValueForRow(textInRowForAnyColumn, "DIR №");

        //GLX, SH249, SG
        //NO REFRESH btn, use Save & Edit btn - LAX, I15SB, I15Tech
        public virtual bool VerifyAutoSaveTimerRefresh()
        {
            Report.Step("STEP: VerifyAutoSaveTimerRefresh");

            bool timerRefreshedAsExpected = false;
            string preRefreshTime = string.Empty;
            string postRefreshTime = string.Empty;

            try
            {
                By clockAutoSaveLocator = By.XPath("//span[@id='clockAutoSave']");

                Thread.Sleep(10000);
                preRefreshTime = PageAction.GetText(clockAutoSaveLocator);
                QaRcrdCtrl_QaDIR.RefreshAutoSaveTimer();
                postRefreshTime = PageAction.GetText(clockAutoSaveLocator);

                timerRefreshedAsExpected = int.Parse(Regex.Replace(postRefreshTime, ":", "")) > int.Parse(Regex.Replace(preRefreshTime, ":", ""));
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            Report.Info($"PreRefresh Timer Value {preRefreshTime}<br>PostRefresh Timer Value {postRefreshTime}<br>AutoSave Timer Refreshed Successfully: {timerRefreshedAsExpected}", timerRefreshedAsExpected);
            return timerRefreshedAsExpected;
        }

        //GLX, SH249, SG
        public virtual void RefreshAutoSaveTimer()
            => ClickBtn_Refresh();

        public virtual bool Verify_Package_Download()
        {
            bool isEnabled = false;
            bool fileDownloaded = false;
            string logMsg = string.Empty;
            string fileName = string.Empty;

            try
            {
                for (int i = 1; i < 10; i++)
                {
                    By downloadBtnLocator = GridHelper.GetTableBtnLocator(TableButton.Download, i);
                    isEnabled = PageAction.GetElement(downloadBtnLocator).Enabled;

                    if (isEnabled)
                    {
                        fileName = $"{GridHelper.GetColumnValueForRow(i, PackagesColumnName.Package_Number.GetString(true))}.zip";
                        string fullFilePath = $"C:\\Automation\\Downloads\\{fileName}";
                        //delete if file already exists in download folder
                        if (File.Exists(fullFilePath))
                        {
                            File.Delete(fullFilePath);
                            Report.Step("Deleted existing file in the Downloads folder");
                        }

                        GridHelper.ClickDownloadBtnForRow(i);
                        PageAction.WaitForPageReady();
                        fileDownloaded = File.Exists(fullFilePath);
                        //cleanup - delete downloaded file
                        if (fileDownloaded)
                        {
                            File.Delete(fullFilePath);
                            Report.Step("Deleted downloaded file in the Downloads folder");
                        }

                        break;
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
            logMsg = fileDownloaded
                ? "Success"
                : "Failed";

            Report.Info($"Download for DIR Package ({fileName}) {logMsg}", fileDownloaded);

            return fileDownloaded;
        }

        public virtual string GetDirPackageWeekStartFromRow(int rowIndex = 1)
            => GetDirPackagesDataForRow<string>(PackagesColumnName.Week_Start, rowIndex).Trim();

        public virtual string GetDirPackageWeekEndFromRow(int rowIndex = 1)
            => GetDirPackagesDataForRow<string>(PackagesColumnName.Week_End, rowIndex).Trim();

        public virtual string GetDirPackageNewDirCountFromRow(int rowIndex = 1)
            => GetDirPackagesDataForRow<string>(PackagesColumnName.New_DIR_Count, rowIndex).Trim();

        public virtual string GetDirPackageNumberFromRow(int rowIndex = 1)
            => GetDirPackagesDataForRow<string>(PackagesColumnName.Package_Number, rowIndex).Trim();

        public virtual string[] GetDirPackageDirNumbersFromRow(PackagesColumnName NewDIRsOrDIRs, int rowIndex = 1)
            => GetDirPackagesDataForRow<string[]>(NewDIRsOrDIRs, rowIndex);

        public virtual bool Verify_Package_Created(string weekStart, string[] dirNumbers)
        {
            bool pkgIsCreated = false;
            try
            {
                QaRcrdCtrl_QaDIR.ClickTab_Packages();
                string expectedPkgNum = CalculateDirPackageNumber(weekStart);
                pkgIsCreated = GridHelper.VerifyRecordIsDisplayed(PackagesColumnName.Package_Number, expectedPkgNum);
                string actualPkgNum = GetDirPackageNumberFromRow();

                bool pkgNumAsExpected = actualPkgNum.Equals(expectedPkgNum);
                string logMsg(string lineBrk) => pkgNumAsExpected
                    ? $"Package Number is as expected {actualPkgNum}"
                    : $"Package Number is not as expected!{lineBrk}EXPECTED: {expectedPkgNum}{lineBrk}ACTUAL: {actualPkgNum}";
                Report.Info(logMsg("<br>"), pkgNumAsExpected);
                AddAssertionToList(pkgNumAsExpected, logMsg("\n"));
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
            
            return pkgIsCreated;
        }

        public virtual string CalculateDirPackageNumber(string weekStartDate)
        {
            string[] wkStartSplit = new string[] { };
            wkStartSplit = Regex.Split(weekStartDate, "/");
            string mm = wkStartSplit[0];
            string dd = wkStartSplit[1];
            string yyyy = wkStartSplit[2];
            return $"IQF-DIR-{yyyy}{mm}{dd}-1";
        }

        public virtual void EnterText_InspectionDate(string inspectDate)
            => PageAction.EnterText(By.Id(InputFields.Inspect_Date.GetString()), inspectDate);

        public virtual string GetTechIdForDirUserAcct(bool selectUserFromDDList = false, UserType dirNoDDListTech = UserType.DIRTechQA)
        {
            if (selectUserFromDDList)
            {
                PageAction.ExpandAndSelectFromDDList("TechID", dirNoDDListTech.GetString(), true);
            }

            return PageAction.GetAttribute(By.Id("TechID"), "value");
        }

        public virtual void VerifyRecreateBtnIsDisplayed(string packageNumber, string newDirNumber)
        {
            string logMsg = string.Empty;
            string btnIsDisplayedMsg = "Record was not displayed";
            string dirsContainsMsg = string.Empty;
            string trimedPkgNumber = packageNumber.TrimEnd('1', '2');
            bool recreateBtnIsDisplayed = false;
            bool dirNumbersContainsNewDIR = false;

            bool recordIsDisplayed = GridHelper.VerifyRecordIsDisplayed(PackagesColumnName.Package_Number, trimedPkgNumber, TableType.MultiTab, false, FilterOperator.Contains);

            AddAssertionToList(recordIsDisplayed, $"VerifyRecreateBtnIsDisplayed: Verify Record PackageNumber({packageNumber}) is displayed");

            if (recordIsDisplayed)
            {
                recreateBtnIsDisplayed = PageAction.ElementIsDisplayed(GridHelper.GetTableBtnLocator(TableButton.Recreate_Package, 1, true, false));
                btnIsDisplayedMsg = recreateBtnIsDisplayed
                    ? ""
                    : " NOT";
                if (recreateBtnIsDisplayed)
                {
                    GridHelper.ClickRecreateBtnForRow();
                    PageAction.AcceptAlertMessage();
                    PageAction.AcceptAlertMessage();

                    string pkgDIRs = GridHelper.GetColumnValueForRow(1, PackagesColumnName.DIRs.GetString(true));
                    string newPkgNumber = GridHelper.GetColumnValueForRow(1, PackagesColumnName.Package_Number.GetString(true));

                    List<string> dirNumbers = new List<string>(Regex.Split(pkgDIRs, ", "));
                    dirNumbersContainsNewDIR = dirNumbers.Contains(newDirNumber);
                    dirsContainsMsg = dirNumbersContainsNewDIR
                        ? $""
                        : $" DOES NOT";

                    string expectedNewPkgNumber = $"{trimedPkgNumber}2";
                    AddAssertionToList(newPkgNumber.Equals(expectedNewPkgNumber), $"VerifyRecreateBtnIsDisplayed: Verify Expected Package Number after Recreate");

                }

                logMsg = $"Recreate button is{btnIsDisplayedMsg} displayed.<br>New DIRs column{dirsContainsMsg} contain new DIR number {newDirNumber}.";
            }

            AddAssertionToList(recreateBtnIsDisplayed, $"VerifyRecreateBtnIsDisplayed: Verify Recreate button is displayed");
            AddAssertionToList(dirNumbersContainsNewDIR, $"VerifyRecreateBtnIsDisplayed: Verify DIRs column shows new DIR");

            Report.Info(logMsg, new bool[] { recreateBtnIsDisplayed, dirNumbersContainsNewDIR });
        }

        public abstract IList<string> GetErrorSummaryIDs();
        public abstract IList<string> TrimInputFieldIDs(IList<string> fieldIdList, string splitPattern);
        public abstract bool VerifyExpectedRequiredFields(IList<string> actualRequiredFieldIDs, IList<string> expectedRequiredFieldIDs);
        public abstract void CloseErrorSummaryPopupMsg();
        public abstract void Verify_Column_Filters_DirPackageTabs(TableTab pkgsTab, int indexOfRow = 1);
        public abstract TOut GetDirPackagesDataForRow<TOut>(PackagesColumnName packagesColumnName, int rowIndex = 1);
        public abstract bool VerifySpecSectionDescriptionAutoPopulatedData();
        public abstract bool VerifyControlPointReqFieldErrors();
        public abstract bool VerifyEngineerCommentsReqFieldErrors();
        public abstract bool VerifyDirNumberExistsInDbError();
        public abstract bool VerifyDirRevisionInDetailsPage(string expectedDirRev);
    }

    //Tenant Specific Classes

    public class QADIRs_Garnet : QADIRs
    {
        public QADIRs_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QADIRs_SH249 : QADIRs
    {
        public QADIRs_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override void PopulateRequiredFields()
        {
            SelectDDL_TimeBegin(TimeBlock.AM_06_00);
            SelectDDL_TimeEnd(TimeBlock.PM_04_00);
            SelectDDL_Division();
            SelectDDL_BidItemCode();
            SelectDDL_Feature();
            SelectDDL_Crew();
            SelectDDL_CrewForeman();
            SelectChkbox_InspectionType_I();
            SelectChkbox_InspectionResult_P();
            Enter_ReadyDateTime();
            Enter_CompletedDateTime("", TimeBlock.PM_12_00);
            Enter_TotalInspectionTime();
        }

        public override IList<string> GetExpectedRequiredFieldIDsList()
        {
            IList<string> RequiredFieldIDs = new List<string>()
            {
                InputFields.Time_Begin.GetString(),
                InputFields.Time_End.GetString(),
                InputFields.Division.GetString(),
                InputFields.Bid_Item_Code.GetString(),
                InputFields.Feature.GetString(),
                InputFields.Crew.GetString(),
                InputFields.Crew_Foreman.GetString(),
                "Inspection Type",
                "Inspection Result",
                InputFields.Date_Ready.GetString(),
                InputFields.Date_Completed.GetString(),
                InputFields.Total_Inspection_Time.GetString()
            };

            return RequiredFieldIDs;
        }

        public override IList<Enum> GetDeficienciesRdoBtnIDsList()
        {
            IList<Enum> deficienciesRdoBtnIDs = new List<Enum>()
            {
                RadioBtnsAndCheckboxes.Deficiencies_Yes,
                //RadioBtnsAndCheckboxes.Deficiencies_CIF,
                RadioBtnsAndCheckboxes.Deficiencies_CDR,
                RadioBtnsAndCheckboxes.Deficiencies_NCR
            };

            return deficienciesRdoBtnIDs;
        }
    }

    public class QADIRs_SGWay : QADIRs
    {
        public QADIRs_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override void Enter_ReadyDateTime(string shortDate = "", TimeBlock shortTime = TimeBlock.AM_12_00)
        {
            shortDate = shortDate.Equals("") ? GetShortDate() : shortDate;
            PageAction.EnterText(GetTextInputFieldByLocator(InputFields.DateOnly_Ready), shortDate);
            PageAction.ExpandAndSelectFromDDList(InputFields.TimeOnly_Ready, (int)shortTime);
        }

        public override void Enter_CompletedDateTime(string shortDate = "", TimeBlock shortTime = TimeBlock.AM_12_00)
        {
            shortDate = shortDate.Equals("") ? GetShortDate() : shortDate;
            PageAction.EnterText(GetTextInputFieldByLocator(InputFields.DateOnly_Completed), shortDate);
            PageAction.ExpandAndSelectFromDDList(InputFields.TimeOnly_Completed, (int)shortTime);
        }

        public override void SelectChkbox_InspectionType_I(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Type_I_forSG, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_InspectionType_P(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Type_P_forSG, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_InspectionType_R(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Type_R_forSG, toggleChkboxIfAlreadySelected);

        public override void PopulateRequiredFields()
        {
            SelectDDL_Division();
            SelectDDL_BidItemCode();
            SelectDDL_Feature();
            SelectDDL_Crew();
            SelectDDL_CrewForeman();
            SelectChkbox_InspectionType_I();
            SelectChkbox_InspectionType_P();
            SelectChkbox_InspectionType_H();
            SelectChkbox_InspectionType_R();
            SelectChkbox_InspectionResult_P();
            Enter_ReadyDateTime();
            Enter_CompletedDateTime("", TimeBlock.PM_12_00);
            Enter_TotalInspectionTime();

            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyControlPointReqFieldErrors(), "VerifyControlPointReqFieldErrors");
            SelectDDL_HoldPointType();
        }

        public override IList<string> GetExpectedRequiredFieldIDsList()
        {
            IList<string> RequiredFieldIDs = new List<string>()
            {
                InputFields.Division.GetString(),
                InputFields.Bid_Item_Code.GetString(),
                InputFields.Feature.GetString(),
                InputFields.Crew.GetString(),
                InputFields.Crew_Foreman.GetString(),
                "Inspection Type",
                "Inspection Result",
                InputFields.DateOnly_Ready.GetString(),
                InputFields.TimeOnly_Ready.GetString(),
                InputFields.DateOnly_Completed.GetString(),
                InputFields.TimeOnly_Completed.GetString(),
                InputFields.Total_Inspection_Time.GetString()
            };

            return RequiredFieldIDs;
        }

        public override IList<string> GetExpectedHoldPointReqFieldIDsList()
        {
            IList<string> ControlPointReqFieldIDs = new List<string>()
            {
                InputFields.Control_Point_Type.GetString()
            };

            return ControlPointReqFieldIDs;
        }
    }

    public class QADIRs_I15South : QADIRs
    {
        public QADIRs_I15South(IWebDriver driver) : base(driver)
        {
        }

        public override void PopulateRequiredFields()
        {
            SelectDDL_TimeBegin(TimeBlock.AM_06_00);
            SelectDDL_TimeEnd(TimeBlock.PM_04_00);
            SelectDDL_SpecSection();
            SelectDDL_Feature();
            SelectDDL_Contractor(1);
            SelectDDL_CrewForeman();
            SelectChkbox_InspectionType_C();
            SelectChkbox_InspectionResult_P();
            Enter_ReadyDateTime();
            Enter_CompletedDateTime();
            Enter_TotalInspectionTime();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyControlPointReqFieldErrors(), "VerifyControlPointReqFieldErrors");
            SelectDDL_ControlPointNumber();
            SelectDDL_Feature();
        }

        public override IList<string> GetExpectedRequiredFieldIDsList()
        {
            IList<string> RequiredFieldIDs = new List<string>()
            {
                InputFields.Time_Begin.GetString(),
                InputFields.Time_End.GetString(),
                InputFields.Spec_Section.GetString(),
                InputFields.Section_Description.GetString(),
                InputFields.Feature.GetString(),
                InputFields.Contractor.GetString(),
                InputFields.Crew_Foreman.GetString(),
                "Inspection Type",
                "Inspection Result",
                InputFields.Date_Ready.GetString(),
                InputFields.Date_Completed.GetString(),
                InputFields.Total_Inspection_Time.GetString()
            };

            return RequiredFieldIDs;
        }

        public override void RefreshAutoSaveTimer() => ClickBtn_Save_Edit();
    }

    public class QADIRs_I15Tech : QADIRs
    {
        public QADIRs_I15Tech(IWebDriver driver) : base(driver)
        {
        }

        public override void PopulateRequiredFields()
        {
            SelectDDL_TimeBegin(TimeBlock.AM_06_00);
            SelectDDL_TimeEnd(TimeBlock.PM_04_00);
            SelectDDL_Area();
            SelectDDL_SpecSection();
            SelectDDL_Feature();
            SelectDDL_Contractor();
            SelectDDL_CrewForeman();
            SelectChkbox_InspectionType_C();
            SelectChkbox_InspectionResult_P();
            Enter_ReadyDateTime();
            Enter_CompletedDateTime();
            Enter_TotalInspectionTime();
            ClickBtn_Save_Forward();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyControlPointReqFieldErrors(), "VerifyControlPointReqFieldErrors");
            SelectDDL_ControlPointNumber();
            SelectDDL_Feature();
        }

        public override IList<string> GetExpectedRequiredFieldIDsList()
        {
            IList<string> RequiredFieldIDs = new List<string>()
            {
                InputFields.Time_Begin.GetString(),
                InputFields.Time_End.GetString(),
                InputFields.Area.GetString(),
                InputFields.Spec_Section.GetString(),
                InputFields.Section_Description.GetString(),
                InputFields.Feature.GetString(),
                InputFields.Contractor.GetString(),
                InputFields.Crew_Foreman.GetString(),
                "Inspection Type",
                "Inspection Result",
                InputFields.Date_Ready.GetString(),
                InputFields.Date_Completed.GetString(),
                InputFields.Total_Inspection_Time.GetString()
            };

            return RequiredFieldIDs;
        }

        public override void RefreshAutoSaveTimer() => ClickBtn_Save_Edit();
    }

    public class QADIRs_GLX : QADIRs
    {
        public QADIRs_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override void PopulateRequiredFields()
        {
            SelectDDL_TimeBegin(TimeBlock.AM_06_00);
            SelectDDL_TimeEnd(TimeBlock.PM_04_00);
            Enter_AverageTemp(80);
            SelectDDL_Area();
            Thread.Sleep(1000); //workaround for page refresh after Area selection
            SelectDDL_SpecSection();
            SelectDDL_Feature();
            SelectDDL_Contractor();
            SelectDDL_CrewForeman();
            //EnterText_SectionDescription();  //auto-populates field with selection of SpecSection DDList
            SelectChkbox_InspectionType_I();
            SelectChkbox_InspectionResult_P();
            // --- Selecting InspectionType_C and selecting from availale ControlPoint DDList clears (required)Feature DDList without any available selections
            //ClickBtn_Save_Forward();
            //AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyControlPointReqFieldErrors(), "VerifyControlPointReqFieldErrors");
            //SelectDDL_ControlPointNumber(2);
        }

        public override IList<string> GetExpectedRequiredFieldIDsList()
        {
            IList<string> RequiredFieldIDs = new List<string>()
            {
                InputFields.Time_Begin.GetString(),
                InputFields.Time_End.GetString(),
                InputFields.Average_Temperature.GetString(),
                InputFields.Area.GetString(),
                InputFields.Spec_Section.GetString(),
                InputFields.Feature.GetString(),
                InputFields.Contractor.GetString(),
                InputFields.Crew_Foreman.GetString(),
                InputFields.Section_Description.GetString(),
                "Inspection Type",
                "Inspection Result"
            };

            return RequiredFieldIDs;
        }

        public override IList<Enum> GetDeficienciesRdoBtnIDsList()
        {
            IList<Enum> deficienciesRdoBtnIDs = new List<Enum>()
            {
                RadioBtnsAndCheckboxes.Deficiencies_Yes
            };

            return deficienciesRdoBtnIDs;
        }

        public override void RefreshAutoSaveTimer() => ClickBtn_Save_Edit();

    }

    public class QADIRs_LAX : QADIRs
    {
        public QADIRs_LAX(IWebDriver driver) : base(driver)
        {
        }

        public override void PopulateRequiredFields()
        {
            SelectDDL_TimeBegin(TimeBlock.AM_06_00);
            SelectDDL_TimeEnd(TimeBlock.PM_04_00);
            Enter_AverageTemp(80);
            SelectDDL_Area();
            SelectDDL_SpecSection();
            SelectDDL_SpecSectionParagraph();
            AddAssertionToList(VerifySpecSectionDescriptionAutoPopulatedData(), "VerifySpecSectionDescriptionAutoPopulatedData");
            SelectDDL_Feature();
            SelectDDL_Contractor("LINXS");
            SelectDDL_CrewForeman();
            SelectChkbox_InspectionType_I();
            SelectChkbox_InspectionResult_P();
            Enter_ReadyDateTime();
            Enter_CompletedDateTime();
            Enter_TotalInspectionTime();

            //<---- Currently does not have values to choose in the drop down list
            //QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            //AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyControlPointReqFieldErrors(), "VerifyControlPointReqFieldErrors");
            //SelectDDL_ControlPointNumber(); 
        }

        public override IList<string> GetExpectedRequiredFieldIDsList()
        {
            IList<string> RequiredFieldIDs = new List<string>()
            {
                InputFields.Time_Begin.GetString(),
                InputFields.Time_End.GetString(),
                InputFields.Area.GetString(),
                InputFields.Average_Temperature.GetString(),
                InputFields.Spec_Section.GetString(),
                InputFields.Spec_Section_Paragraph.GetString(),
                InputFields.Section_Description.GetString(),
                InputFields.Feature.GetString(),
                InputFields.Crew_Foreman.GetString(),
                InputFields.Contractor.GetString(),
                "Inspection Type",
                "Inspection Result",
                InputFields.Date_Ready.GetString(),
                InputFields.Date_Completed.GetString(),
                InputFields.Total_Inspection_Time.GetString()
            };

            return RequiredFieldIDs;
        }

        public override IList<Enum> GetDeficienciesRdoBtnIDsList()
        {
            IList<Enum> deficienciesRdoBtnIDs = new List<Enum>()
            {
                RadioBtnsAndCheckboxes.Deficiencies_Yes
            };

            return deficienciesRdoBtnIDs;
        }

        public override void RefreshAutoSaveTimer()
            => ClickBtn_Save_Edit();
    }
}