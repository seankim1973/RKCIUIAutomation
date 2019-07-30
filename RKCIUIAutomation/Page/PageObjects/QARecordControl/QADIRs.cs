using OpenQA.Selenium;
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

        public override T SetClass<T>(IWebDriver driver)
        {
            IQADIRs instance = new QADIRs(driver);

            if (tenantName == TenantNameType.SGWay)
            {
                log.Info($"###### using QADIRs_SGWay instance ###### ");
                instance = new QADIRs_SGWay(driver);
            }
            else if (tenantName == TenantNameType.SH249)
            {
                log.Info($"###### using QADIRs_SH249 instance ###### ");
                instance = new QADIRs_SH249(driver);
            }
            else if (tenantName == TenantNameType.Garnet)
            {
                log.Info($"###### using QADIRs_Garnet instance ###### ");
                instance = new QADIRs_Garnet(driver);
            }
            else if (tenantName == TenantNameType.GLX)
            {
                log.Info($"###### using QADIRs_GLX instance ###### ");
                instance = new QADIRs_GLX(driver);
            }
            else if (tenantName == TenantNameType.I15South)
            {
                log.Info($"###### using QADIRs_I15South instance ###### ");
                instance = new QADIRs_I15South(driver);
            }
            else if (tenantName == TenantNameType.I15Tech)
            {
                log.Info($"###### using QADIRs_I15Tech instance ###### ");
                instance = new QADIRs_I15Tech(driver);
            }
            else if (tenantName == TenantNameType.LAX)
            {
                log.Info($"###### using QADIRs_LAX instance ###### ");
                instance = new QADIRs_LAX(driver);
            }
            return (T)instance;
        }


        public enum InputFieldType
        {
            [StringValue("InspectDate")] Inspect_Date,
            [StringValue("TimeBegin1")] Time_Begin,
            [StringValue("TimeEnd1")] Time_End,
            [StringValue("DIREntries_0__AverageTemperature")] Average_Temperature,
            [StringValue("DIREntries_0__AreaID")] Area,
            [StringValue("DIREntries_0__MultiStructureIDs")] Location,
            [StringValue("fidDisplay")] FID,
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

        public enum GridTabType
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

        public enum ColumnNameType
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

        public enum PackagesColumnNameType
        {
            [StringValue("WeekStart", "Week Start")] Week_Start,
            [StringValue("WeekEnd", "Week End")] Week_End,
            [StringValue("PackageNumber", "Package number")] Package_Number,
            [StringValue("DIRsToString", "DIRs")] DIRs,
            [StringValue("DirsCount", "New DIR Count")] New_DIR_Count,
            [StringValue("DIRsToString", "New DIRs")] New_DIRs,
            [StringValue("RecreateRequired")] Recreate
        }

        public enum ButtonType
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

        public enum RadioBtnCheckboxType
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
            dirNumber = PageAction.GetAttributeForElement(By.Id("DIRNO"), "value");
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
                        else if (id.Contains("IDs"))
                        {
                            newId = Regex.Split(id, "IDs");
                            id = newId[0];

                            if (id.Equals("MultiStructure"))
                            {
                                id = "Location";
                            }
                        }
                        else if (id.Contains("HoldPoint"))
                        {
                            if (id.Equals("HoldPointTypeID"))
                            {
                                if (TenantProperty.TenantComponents.Contains(Component.DIR_WF_Complex))
                                //if (tenantName == TenantName.SGWay || tenantName == TenantName.SH249)
                                {
                                    id = "HoldPoint Type";
                                }
                                else
                                {
                                    id = "ControlPoint No.";
                                }
                            }
                            else
                            {
                                id = "ControlPoint Type";
                            }
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
                log.Error($"{e.Message}\n{e.StackTrace}");
            }

            return extractedFieldNames;
        }

        /// <summary>
        /// WeekStart[0], WeekEnd[1], NewDirCountOrPackageNumumber[2], NewDIRsOrDIRs[3]
        /// </summary>
        /// <param name="pkgsTab"></param>
        /// <param name="indexOfRow"></param>
        /// <returns></returns>
        internal string[] GetPackageDataForRow(GridTabType pkgsTab, int indexOfRow = 1)
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
                newDIRsOrDIRsToString = pkgsTab.Equals(GridTabType.Create_Packages)
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

        public override void Verify_Column_Filters_DirPackageTabs(GridTabType pkgsTab, int indexOfRow = 1)
        {
            try
            {
                string[] pkgData = GetPackageDataForRow(pkgsTab, indexOfRow);

                PackagesColumnNameType columnName = PackagesColumnNameType.Week_Start;

                for (int i = 0; i < pkgData.Length; i++)
                {
                    string columnValue = pkgData[i];

                    switch (i)
                    {
                        //case 0:
                        //    columnName = PackagesColumnName.Week_Start;                          
                        //    break;
                        case 1:
                            columnName = PackagesColumnNameType.Week_End;
                            break;
                        case 2:
                            columnName = pkgsTab.Equals(GridTabType.Create_Packages)
                                ? PackagesColumnNameType.New_DIR_Count
                                : PackagesColumnNameType.Package_Number;
                            break;
                        case 3:
                            columnName = pkgsTab.Equals(GridTabType.Create_Packages)
                                ? PackagesColumnNameType.New_DIRs
                                : PackagesColumnNameType.DIRs;
                            break;
                    }

                    TestUtility.AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(columnName, columnValue, TableType.MultiTab, false), $"Verify Filter [{pkgsTab.ToString()} | {columnName.GetString()}] column Equals {columnValue}");

                    if (columnName.Equals(PackagesColumnNameType.New_DIRs) || (columnName.Equals(PackagesColumnNameType.DIRs)))
                    {
                        string[] arrayDirNums = new string[] { };
                        arrayDirNums = columnValue.Contains(",")
                            ? Regex.Split(columnValue, ", ")
                            : new string[1] { columnValue };

                        for (int a = 0; a < arrayDirNums.Length; a++)
                        {
                            columnValue = arrayDirNums[a];
                            TestUtility.AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(columnName, columnValue, TableType.MultiTab, false, FilterOperator.Contains), $"Verify Filter [{pkgsTab.ToString()} | {columnName.GetString()}] column Contains {columnValue}");
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

        public override TOut GetDirPackagesDataForRow<TOut>(PackagesColumnNameType packagesColumnName, int rowIndex = 1)
        {
            BaseUtils baseUtils = new BaseUtils();
            string colName = packagesColumnName.GetString(true);
            string tblData = GridHelper.GetColumnValueForRow(rowIndex, colName);

            if (packagesColumnName == PackagesColumnNameType.New_DIRs || packagesColumnName == PackagesColumnNameType.DIRs)
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
            string specSectionParagraphText = GetTextFromDDL(InputFieldType.Spec_Section_Paragraph);
            string spectionDescriptionText = GetText(By.Id(InputFieldType.Section_Description.GetString()));
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

        public override bool VerifyDirNumberExistsInDbErrorIsDisplayed(bool errorIsExpected = false)
        {
            bool result = false;
            bool errorIsDisplayed = false;
            string logMsg = string.Empty;
            By errorLocator = By.Id("error");
            IWebElement elem = null;

            try
            {
                elem = PageAction.GetElement(errorLocator);
                errorIsDisplayed = elem.Displayed;

                if (errorIsExpected) //error is displayed
                {
                    logMsg = "'This DIR No. exists in the database' error message is displayed as expected.";
                    result = true;
                }
                else
                {
                    logMsg = "'This DIR No. exists in the database' error message is displayed but is not expected.";

                }
            }
            catch (NoSuchElementException) //error is not displayed
            {
                if (errorIsExpected)
                {
                    logMsg = "'This DIR No. exists in the database' error message is expected, but is not displayed";
                }
                else
                {
                    logMsg = "'This DIR No. exists in the database' error message is not displayed as expected.";
                    result = true;
                }

                throw;
            }
            finally
            {
                Report.Info(logMsg, result);
            }
          
            return errorIsDisplayed;
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

        public override string GetDirNumber()
            => dirNumber;

        public override bool IsLoaded()
            => driver.Title.Equals("DIR List - ELVIS PMC");

        public override void ClickBtn_CreateNew(bool useWorkaroundIfDirExists = false)
        {
            bool errorIsDisplayed = false;
            string logMsg = string.Empty;

            try
            {
                PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Create_New));

                if (useWorkaroundIfDirExists)
                {
                    logMsg = "Workaround is enabled for when 'This DIR No. exists in database' error is displayed.";

                    try
                    {
                        VerifyDirNumberExistsInDbErrorIsDisplayed();
                        errorIsDisplayed = true;
                    }
                    catch (NoSuchElementException)
                    {
                        logMsg = $"{logMsg}\nNo error is displayed. Test will continue normally without using workaround.";
                        Report.Info($"{logMsg}");
                    }

                    if (errorIsDisplayed)
                    {
                        try
                        {
                            string currentUserEmail = ConfigUtil.GetCurrentUserEmail();
                            GridHelper.FilterTableColumnByValue(ColumnNameType.Created_By, currentUserEmail);
                            GridHelper.VerifyNoRecordMessageIsDisplayed();
                            //TODO check database for closed DIR and set IsDeleted = 1
                            logMsg = $"{logMsg}\nCould not edit any existing records, because filtering grid by current user email returned no records.";
                            Report.Error(logMsg);
                            throw new ArgumentException(logMsg);
                        }
                        catch (NoSuchElementException)
                        {
                            GridHelper.ClickEditBtnForRow();
                            Report.Debug($"{logMsg}\nFiltered grid by current user email and continuing test with old existing record.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }
        }

        public override void ClickBtn_CreateRevision()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Create_Revision));

        public override void ClickBtn_Refresh()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Refresh, false));

        public override void ClickBtn_Cancel()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Cancel));

        public override void ClickBtn_KickBack()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Kick_Back, false));

        public override void ClickBtn_SubmitRevise()
           => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Submit_Revise, false));

        public override void ClickBtn_Send_To_Attachment()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Send_To_Attachment));

        public override void ClickBtn_Save()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Save));

        public override void ClickBtn_Save_Forward()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Save_Forward));

        public override void ClickBtn_Save_Edit()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Save_Edit));

        public override void ClickBtn_Approve()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Approve));

        public override void ClickBtn_Add()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Add));

        public override void ClickBtn_Delete()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Delete));

        public override void ClickBtn_NoError()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.No_Error));

        public override void ClickBtn_Back_To_QC_Review()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Back_To_QC_Review, false));

        public override void ClickBtn_Back_To_Field()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Back_To_Field, false));

        public override void ClickBtn_Revise()
            => PageAction.JsClickElement(GetSubmitButtonByLocator(ButtonType.Revise, false));

        public override void ClickBtn_Close_Selected()
            => PageAction.JsClickElement(By.XPath("//button[text()='Close Selected']"));

        public override void ClickBtn_View_Selected()
            => PageAction.JsClickElement(By.XPath("//button[text()='View Selected']"));

        public override void ClickTab_Create_Revise()
            => GridHelper.ClickTab(GridTabType.Create_Revise);

        public override void ClickTab_QC_Review()
            => GridHelper.ClickTab(GridTabType.QC_Review);

        public override void ClickTab_Authorization()
            => GridHelper.ClickTab(GridTabType.Authorization);

        public override void ClickTab_Creating()
            => GridHelper.ClickTab(GridTabType.Creating);

        public override void ClickTab_Attachments()
            => GridHelper.ClickTab(GridTabType.Attachments);

        public override void ClickTab_Revise()
            => GridHelper.ClickTab(GridTabType.Revise);

        public override void ClickTab_To_Be_Closed()
            => GridHelper.ClickTab(GridTabType.To_Be_Closed);

        public override void ClickTab_Closed()
            => GridHelper.ClickTab(GridTabType.Closed);

        public override void ClickTab_Create_Packages()
            => GridHelper.ClickTab(GridTabType.Create_Packages);

        public override void ClickTab_Packages()
            => GridHelper.ClickTab(GridTabType.Packages);

        public override void FilterDirNumber(string DirNumber)
            => GridHelper.FilterTableColumnByValue(ColumnNameType.DIR_No, DirNumber);


        //All SimpleWF Tenants have different required fields
        public override IList<string> GetExpectedRequiredFieldIDsList()
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

        public override string CreatePreviousFailingReport()
        {
            string logMsg = string.Empty;
            string nonFixedFailedDirNumber = string.Empty;
            bool modalIsDisplayed = false;
            IPageInteraction pgAction = PageAction;

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
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }

            Report.Info(logMsg, modalIsDisplayed);
            return nonFixedFailedDirNumber;
        }

        public override bool VerifyPreviousFailingDirEntry(string previousDirNumber)
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
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }

            Report.Info($"Previous Failing DIR table {logMsg} DIR Number {previousDirNumber}", isDisplayed);
            return isDisplayed;
        }

        public override void SelectDDL_TimeBegin(TimeBlock shiftStartTime = TimeBlock.AM_06_00)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Time_Begin, (int)shiftStartTime);

        public override void SelectDDL_TimeEnd(TimeBlock shiftEndTime = TimeBlock.PM_04_00)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Time_End, (int)shiftEndTime);

        public override void Enter_AverageTemp(int avgTemp = 80)
        {
            string avgTempFieldId = InputFieldType.Average_Temperature.GetString();
            By clickLocator = By.XPath($"//input[@id='{avgTempFieldId}']/preceding-sibling::input");
            By locator = By.XPath($"//input[@id='{avgTempFieldId}']");
            PageAction.ClickElement(clickLocator);
            PageAction.EnterText(locator, avgTemp.ToString(), false);
        }

        public override void SelectDDL_Area(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Area, ddListSelection);

        public override void SelectDDL_SpecSection(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Spec_Section, ddListSelection);

        public override void SelectDDL_SpecSectionParagraph(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Spec_Section_Paragraph, ddListSelection);

        public override void SelectDDL_Division(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Division, ddListSelection);

        public override void SelectDDL_BidItemCode(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Bid_Item_Code, ddListSelection);

        public override void SelectDDL_Location(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Location, ddListSelection, false, true);

        public override void SelectDDL_FID(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.FID, ddListSelection, false, true);

        public override void SelectDDL_Feature(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Feature, ddListSelection);

        public override void SelectDDL_ControlPointNumber(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Control_Point_Number, ddListSelection);

        public override void SelectDDL_HoldPointType(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Control_Point_Type, ddListSelection);

        public override void SelectDDL_Contractor(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Contractor, ddListSelection);

        public override void SelectDDL_Contractor<T>(T ddListSelection)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Contractor, ddListSelection);

        public override void SelectDDL_Crew(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Crew, ddListSelection);

        public override void SelectDDL_CrewForeman(int ddListSelection = 1)
            => PageAction.ExpandAndSelectFromDDList(InputFieldType.Crew_Foreman, ddListSelection);

        public override bool VerifySectionDescription()
        {
            bool textMatch = false;

            try
            {
                string sectionDesc = PageAction.GetText(GetTextAreaFieldByLocator(InputFieldType.Section_Description));
                string specSectionDDListValue = PageAction.GetTextFromDDL(InputFieldType.Spec_Section);
                specSectionDDListValue = specSectionDDListValue.Contains("-")
                    ? Regex.Replace(specSectionDDListValue, "-", " - ")
                    : specSectionDDListValue;

                textMatch = sectionDesc.Equals(specSectionDDListValue);
                Report.Info($"Spec Section DDList Selection: {specSectionDDListValue}<br>Section Description Text Value: {sectionDesc}", textMatch);
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }
            return textMatch;
        }

        public override bool VerifyDirIsDisplayed(GridTabType tableTab, string dirNumber = "", bool noRecordsExpected = false)
        {
            bool resultIsAsExpected = false;
            bool isDisplayed = false;
            string logMsg = string.Empty;

            try
            {
                GridHelper.ClickTab(tableTab);

                if (!dirNumber.HasValue())
                {
                    dirNumber = GetDirNumber();
                }

                isDisplayed = GridHelper.VerifyRecordIsDisplayed(ColumnNameType.DIR_No, dirNumber, TableType.MultiTab, noRecordsExpected);

                if (isDisplayed)
                {
                    logMsg = "Found";

                    if (!noRecordsExpected)
                    {
                        resultIsAsExpected = true;
                    }
                }
                else
                {
                    logMsg = "Unable to find";

                    if (noRecordsExpected)
                    {
                        resultIsAsExpected = true;
                    }
                }

                Report.Info($"{logMsg} record under {tableTab.GetString()} tab with DIR Number: {dirNumber}.", resultIsAsExpected);
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }

            return resultIsAsExpected;
        }

        public override IList<Enum> GetResultCheckBoxIDsList()
        {
            IList<Enum> resultChkBoxIDs = new List<Enum>()
            {
                RadioBtnCheckboxType.Inspection_Result_P,
                RadioBtnCheckboxType.Inspection_Result_E,
            };

            return resultChkBoxIDs;
        }

        //I15SB, I15Tech
        public override IList<Enum> GetDeficienciesRdoBtnIDsList()
        {
            IList<Enum> deficienciesRdoBtnIDs = new List<Enum>()
            {
                RadioBtnCheckboxType.Deficiencies_Yes,
                RadioBtnCheckboxType.Deficiencies_CIF,
                RadioBtnCheckboxType.Deficiencies_CDR,
                RadioBtnCheckboxType.Deficiencies_NCR
            };

            return deficienciesRdoBtnIDs;
        }

        public override bool VerifyDeficiencySelectionPopupMessages()
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
                            resultTypeMsg = resultChkBox.Equals(RadioBtnCheckboxType.Inspection_Result_P) ? "Pass" : "Engineer Decision";
                            expectedAlertMsg = $"Since {resultTypeMsg} checked, not allow to check any deficiency";
                            alertMsg = PageAction.AcceptAlertMessage(); //GetAlertMessage();
                            alertMsgMatch = alertMsg.Equals(expectedAlertMsg);
                            assertList.Add(alertMsgMatch);
                            Report.Info($"Selected : Result ( {resultTypeMsg} ) - Deficiency ( {deficiencyRdoBtn.ToString()} )<br>Expected Alert Msg: {expectedAlertMsg}<br>Actual Alert Msg: {alertMsg}", alertMsgMatch);
                        }
                    }
                }
                catch (UnhandledAlertException ae)
                {
                    log.Debug(ae.Message);
                }
                catch (Exception e)
                {
                    log.Error($"{e.Message}\n{e.StackTrace}");
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
                            resultTypeMsg = resultChkBox.Equals(RadioBtnCheckboxType.Inspection_Result_P) ? "pass" : "make engineer decision";
                            expectedAlertMsg = $"There is a deficiency checked in this entry, not allow to {resultTypeMsg}!";
                            alertMsg = PageAction.AcceptAlertMessage(); //GetAlertMessage();
                            alertMsgMatch = alertMsg.Equals(expectedAlertMsg);
                            assertList.Add(alertMsgMatch);
                            Report.Info($"Selected : Deficiency ( {deficiencyRdoBtn.ToString()} ) - Result ( {resultTypeMsg} )<br>Expected Alert Msg: {expectedAlertMsg}<br>Actual Alert Msg: {alertMsg}", alertMsgMatch);
                        }
                    }
                }
                catch (UnhandledAlertException ae)
                {
                    log.Debug(ae.Message);
                }
                catch (Exception e)
                {
                    log.Error($"{e.Message}\n{e.StackTrace}");
                }

                alertMsgExpected = assertList.Contains(false)
                    ? false
                    : true;
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }
            finally
            {
                SelectRdoBtn_Deficiencies_No();
                SelectChkbox_InspectionResult_P();
            }

            return alertMsgExpected;
        }

        public override void SelectChkbox_InspectionType_I(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Inspection_Type_I, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_InspectionType_C(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Inspection_Type_C, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_InspectionType_P(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Inspection_Type_P, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_InspectionType_H(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Inspection_Type_H, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_InspectionType_R(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Inspection_Type_R, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_InspectionResult_P(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Inspection_Result_P, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_InspectionResult_E(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Inspection_Result_E, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_InspectionResult_F(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Inspection_Result_F, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_InspectionResult_NA(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Inspection_Result_NA, toggleChkboxIfAlreadySelected);

        public override void SelectRdoBtn_SendEmailForRevise_Yes()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.SendEmailNotification_Yes);

        public override void SelectRdoBtn_SendEmailForRevise_No()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.SendEmailNotification_No);

        public override void SelectRdoBtn_Deficiencies_Yes()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Deficiencies_Yes);

        public override void SelectRdoBtn_Deficiencies_No()
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Deficiencies_No);

        public override void EnterText_DeficiencyDescription(string desc = "")
            => PageAction.EnterText(GetTextAreaFieldByLocator(InputFieldType.Deficiency_Description),
                desc = desc.Equals("") ? "RKCI Automation Deficiency Description" : desc);

        public override void EnterText_SectionDescription(string desc = "")
            => PageAction.EnterText(GetTextAreaFieldByLocator(InputFieldType.Section_Description),
                desc = desc.Equals("") ? "RKCI Automation Section Description" : desc);

        public override void EnterText_EngineerComments(string comment = "")
            => PageAction.EnterText(GetTextAreaFieldByLocator(InputFieldType.Engineer_Comments),
                comment = comment.Equals("") ? "RKCI Automation Engineer Comment" : comment);

        public override void Enter_ReadyDateTime(string shortDate = "", TimeBlock shortTime = TimeBlock.AM_12_00)
            => PageAction.EnterText(GetTextInputFieldByLocator(InputFieldType.Date_Ready), GetShortDateTime(shortDate, shortTime));

        public override void Enter_CompletedDateTime(string shortDate = "", TimeBlock shortTime = TimeBlock.AM_12_00)
            => PageAction.EnterText(GetTextInputFieldByLocator(InputFieldType.Date_Completed), GetShortDateTime(shortDate, shortTime));

        public override void Enter_TotalInspectionTime()
        {
            string inspectTimeFieldId = InputFieldType.Total_Inspection_Time.GetString();
            By clickLocator = By.XPath($"//input[@id='{inspectTimeFieldId}']/preceding-sibling::input");
            By locator = By.XPath($"//input[@id='{inspectTimeFieldId}']");
            PageAction.ClickElement(clickLocator);
            PageAction.EnterText(locator, "24", false);
        }

        public override bool VerifyAndCloseDirLockedMessage()
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

        public override IList<string> GetExpectedHoldPointReqFieldIDsList()
        {
            IList<string> ControlPointReqFieldIDs = new List<string>()
            {
                InputFieldType.Control_Point_Number.GetString(),
                InputFieldType.Control_Point_Type.GetString()
            };

            return ControlPointReqFieldIDs;
        }

        public override IList<string> GetExpectedEngineerCommentsReqFieldIDsList()
        {
            IList<string> EngineerCommentsReqFieldID = new List<string>()
            {
                InputFieldType.Engineer_Comments.GetString()
            };

            return EngineerCommentsReqFieldID;
        }

        public override bool VerifyReqFieldErrorsForNewDir(IList<string> expectedRequiredFieldIDs = null, RequiredFieldType RequiredFieldType = RequiredFieldType.Default)
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
                        if (expectedRequiredFieldIDs == null)
                        {
                            expectedRequiredFieldIDs = QaRcrdCtrl_QaDIR.GetExpectedHoldPointReqFieldIDsList();
                        }
                        break;
                    case RequiredFieldType.EngineerComments:
                        reqFieldIdXPath = $"//textarea[@id='{InputFieldType.Engineer_Comments.GetString()}']";
                        splitPattern = "0__";
                        if (expectedRequiredFieldIDs == null)
                        {
                            expectedRequiredFieldIDs = QaRcrdCtrl_QaDIR.GetExpectedEngineerCommentsReqFieldIDsList();
                        }
                        break;
                    default:
                        reqFieldIdXPath = "//span[text()='*']";
                        splitPattern = "0_";
                        if (expectedRequiredFieldIDs == null)
                        {
                            expectedRequiredFieldIDs = QaRcrdCtrl_QaDIR.GetExpectedRequiredFieldIDsList();
                        }
                        break;
                }

                IList<string> trimmedActualIds = TrimInputFieldIDs(PageAction.GetAttributeForElements(By.XPath(reqFieldIdXPath), "id"), splitPattern);

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
                            TestUtility.AddAssertionToList(summaryIDsContainID, $"{id} contained in Error Summary list");
                            reqFieldAssertList.Add(summaryIDsContainID);
                        }
                    }
                }

                if (!reqFieldAssertList.Contains(false))
                {
                    requiredFieldsMatch = true;
                }
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }
            finally
            {
                CloseErrorSummaryPopupMsg();
            }

            return requiredFieldsMatch;
        }

        public override void FilterTable_CreatePackagesTab(int indexOfRow = 1)
            => Verify_Column_Filters_DirPackageTabs(GridTabType.Create_Packages, indexOfRow);

        public override void FilterTable_PackagesTab(int indexOfRow = 1)
            => Verify_Column_Filters_DirPackageTabs(GridTabType.Packages, indexOfRow);

        public override string GetDirNumberForRow(string textInRowForAnyColumn)
            => GridHelper.GetColumnValueForRow(textInRowForAnyColumn, "DIR №");

        //GLX, SH249, SG
        //NO REFRESH btn, use Save & Edit btn - LAX, I15SB, I15Tech
        public override bool VerifyAutoSaveTimerRefresh()
        {
            Report.Step("VerifyAutoSaveTimerRefresh");

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
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }

            Report.Info($"PreRefresh Timer Value {preRefreshTime}<br>PostRefresh Timer Value {postRefreshTime}<br>AutoSave Timer Refreshed Successfully: {timerRefreshedAsExpected}", timerRefreshedAsExpected);
            return timerRefreshedAsExpected;
        }

        //GLX, SH249, SG
        public override void RefreshAutoSaveTimer()
            => ClickBtn_Refresh();

        public override bool Verify_Package_Download()
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
                        fileName = $"{GridHelper.GetColumnValueForRow(i, PackagesColumnNameType.Package_Number.GetString(true))}.zip";
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

        public override string GetDirPackageWeekStartFromRow(int rowIndex = 1)
            => GetDirPackagesDataForRow<string>(PackagesColumnNameType.Week_Start, rowIndex).Trim();

        public override string GetDirPackageWeekEndFromRow(int rowIndex = 1)
            => GetDirPackagesDataForRow<string>(PackagesColumnNameType.Week_End, rowIndex).Trim();

        public override string GetDirPackageNewDirCountFromRow(int rowIndex = 1)
            => GetDirPackagesDataForRow<string>(PackagesColumnNameType.New_DIR_Count, rowIndex).Trim();

        public override string GetDirPackageNumberFromRow(int rowIndex = 1)
            => GetDirPackagesDataForRow<string>(PackagesColumnNameType.Package_Number, rowIndex).Trim();

        public override string[] GetDirPackageDirNumbersFromRow(PackagesColumnNameType NewDIRsOrDIRs, int rowIndex = 1)
            => GetDirPackagesDataForRow<string[]>(NewDIRsOrDIRs, rowIndex);

        public override bool Verify_Package_Created(string weekStart, string[] dirNumbers)
        {
            bool pkgIsCreated = false;
            try
            {
                QaRcrdCtrl_QaDIR.ClickTab_Packages();
                string expectedPkgNum = CalculateDirPackageNumber(weekStart);
                pkgIsCreated = GridHelper.VerifyRecordIsDisplayed(PackagesColumnNameType.Package_Number, expectedPkgNum);
                string actualPkgNum = GetDirPackageNumberFromRow();

                bool pkgNumAsExpected = actualPkgNum.Equals(expectedPkgNum);
                string logMsg(string lineBrk) => pkgNumAsExpected
                    ? $"Package Number is as expected {actualPkgNum}"
                    : $"Package Number is not as expected!{lineBrk}EXPECTED: {expectedPkgNum}{lineBrk}ACTUAL: {actualPkgNum}";
                Report.Info(logMsg("<br>"), pkgNumAsExpected);
                TestUtility.AddAssertionToList(pkgNumAsExpected, logMsg("\n"));
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }

            return pkgIsCreated;
        }

        public override string CalculateDirPackageNumber(string weekStartDate)
        {
            string[] wkStartSplit = new string[] { };
            wkStartSplit = Regex.Split(weekStartDate, "/");
            string mm = wkStartSplit[0];
            string dd = wkStartSplit[1];
            string yyyy = wkStartSplit[2];
            return $"IQF-DIR-{yyyy}{mm}{dd}-1";
        }

        public override void EnterText_InspectionDate(string inspectDate)
            => PageAction.EnterText(By.Id(InputFieldType.Inspect_Date.GetString()), inspectDate);

        public override string GetTechIdForDirUserAcct(bool selectUserFromDDList = false, UserType dirNoDDListTech = UserType.DIRTechQA)
        {
            if (selectUserFromDDList)
            {
                PageAction.ExpandAndSelectFromDDList("TechID", dirNoDDListTech.GetString(), true);
            }

            return PageAction.GetAttributeForElement(By.Id("TechID"), "value");
        }

        public override void VerifyRecreateBtnIsDisplayed(string packageNumber, string newDirNumber)
        {
            string logMsg = string.Empty;
            string btnIsDisplayedMsg = "Record was not displayed";
            string dirsContainsMsg = string.Empty;
            string trimedPkgNumber = packageNumber.TrimEnd('1', '2');
            bool recreateBtnIsDisplayed = false;
            bool dirNumbersContainsNewDIR = false;

            bool recordIsDisplayed = GridHelper.VerifyRecordIsDisplayed(PackagesColumnNameType.Package_Number, trimedPkgNumber, TableType.MultiTab, false, FilterOperator.Contains);

            TestUtility.AddAssertionToList(recordIsDisplayed, $"VerifyRecreateBtnIsDisplayed: Verify Record PackageNumber({packageNumber}) is displayed");

            if (recordIsDisplayed)
            {
                recreateBtnIsDisplayed = PageAction.ElementIsDisplayed(GridHelper.GetTableBtnLocator(TableButton.Recreate_Package, 1, true, false));
                btnIsDisplayedMsg = recreateBtnIsDisplayed
                    ? ""
                    : " NOT";
                if (recreateBtnIsDisplayed)
                {
                    GridHelper.ClickRecreateBtnForRow();

                    string pkgDIRs = GridHelper.GetColumnValueForRow(1, PackagesColumnNameType.DIRs.GetString(true));
                    string newPkgNumber = GridHelper.GetColumnValueForRow(1, PackagesColumnNameType.Package_Number.GetString(true));

                    List<string> dirNumbers = new List<string>(Regex.Split(pkgDIRs, ", "));
                    dirNumbersContainsNewDIR = dirNumbers.Contains(newDirNumber);
                    dirsContainsMsg = dirNumbersContainsNewDIR
                        ? $""
                        : $" DOES NOT";

                    string expectedNewPkgNumber = $"{trimedPkgNumber}2";
                    TestUtility.AddAssertionToList(newPkgNumber.Equals(expectedNewPkgNumber), $"VerifyRecreateBtnIsDisplayed: Verify Expected Package Number after Recreate");

                }

                logMsg = $"Recreate button is{btnIsDisplayedMsg} displayed.<br>New DIRs column{dirsContainsMsg} contain new DIR number {newDirNumber}.";
            }

            TestUtility.AddAssertionToList(recreateBtnIsDisplayed, $"VerifyRecreateBtnIsDisplayed: Verify Recreate button is displayed");
            TestUtility.AddAssertionToList(dirNumbersContainsNewDIR, $"VerifyRecreateBtnIsDisplayed: Verify DIRs column shows new DIR");

            Report.Info(logMsg, new bool[] { recreateBtnIsDisplayed, dirNumbersContainsNewDIR });
        }
    }

    #endregion DIR/IDR/DWR Generic Class


    public interface IQADIRs
    {
        bool IsLoaded();

        void ClickBtn_CreateNew(bool useWorkaroundIfDirExists = false);

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

        void SelectDDL_Location(int ddListSelection = 1);

        void SelectDDL_FID(int ddListSelection = 1);

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

        bool VerifyDirIsDisplayed(GridTabType tableTab, string dirNumber = "", bool noRecordsExpected = false);

        IList<string> GetExpectedHoldPointReqFieldIDsList();

        IList<string> GetExpectedEngineerCommentsReqFieldIDsList();

        bool VerifyReqFieldErrorsForNewDir(IList<string> expectedRequiredFieldIDs = null, RequiredFieldType requiredFieldType = RequiredFieldType.Default);

        bool VerifyControlPointReqFieldErrors();

        bool VerifyEngineerCommentsReqFieldErrors();

        bool VerifyDirNumberExistsInDbErrorIsDisplayed(bool errorIsExpected = false);

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

        string[] GetDirPackageDirNumbersFromRow(PackagesColumnNameType NewDIRsOrDIRs, int rowIndex = 1);

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

        void Verify_Column_Filters_DirPackageTabs(GridTabType pkgsTab, int indexOfRow = 1);

        TOut GetDirPackagesDataForRow<TOut>(PackagesColumnNameType packagesColumnName, int rowIndex = 1);

        bool VerifySpecSectionDescriptionAutoPopulatedData();
    }

    public abstract class QADIRs_Impl : PageBase, IQADIRs
    {
        public abstract void SetDirNumber();
        //All SimpleWF Tenants have different required fields
        public abstract void PopulateRequiredFields();
        public abstract IList<string> GetErrorSummaryIDs();
        public abstract IList<string> TrimInputFieldIDs(IList<string> fieldIdList, string splitPattern);
        public abstract bool VerifyExpectedRequiredFields(IList<string> actualRequiredFieldIDs, IList<string> expectedRequiredFieldIDs);
        public abstract void CloseErrorSummaryPopupMsg();
        public abstract void Verify_Column_Filters_DirPackageTabs(GridTabType pkgsTab, int indexOfRow = 1);
        public abstract TOut GetDirPackagesDataForRow<TOut>(PackagesColumnNameType packagesColumnName, int rowIndex = 1);
        public abstract bool VerifySpecSectionDescriptionAutoPopulatedData();
        public abstract bool VerifyControlPointReqFieldErrors();
        public abstract bool VerifyEngineerCommentsReqFieldErrors();
        public abstract bool VerifyDirNumberExistsInDbErrorIsDisplayed(bool errorIsExpected = false);
        public abstract bool VerifyDirRevisionInDetailsPage(string expectedDirRev);
        public abstract bool IsLoaded();
        public abstract void ClickBtn_CreateNew(bool useWorkaroundIfDirExists = false);
        public abstract void ClickBtn_CreateRevision();
        public abstract void ClickBtn_Refresh();
        public abstract void ClickBtn_Cancel();
        public abstract void ClickBtn_KickBack();
        public abstract void ClickBtn_SubmitRevise();
        public abstract void ClickBtn_Send_To_Attachment();
        public abstract void ClickBtn_Save();
        public abstract void ClickBtn_Save_Forward();
        public abstract void ClickBtn_Save_Edit();
        public abstract void ClickBtn_Approve();
        public abstract void ClickBtn_Add();
        public abstract void ClickBtn_Delete();
        public abstract void ClickBtn_NoError();
        public abstract void ClickBtn_Revise();
        public abstract void ClickBtn_Back_To_QC_Review();
        public abstract void ClickBtn_Back_To_Field();
        public abstract void ClickBtn_Close_Selected();
        public abstract void ClickBtn_View_Selected();
        public abstract void FilterTable_CreatePackagesTab(int indexOfRow = 1);
        public abstract void FilterTable_PackagesTab(int indexOfRow = 1);
        public abstract void FilterDirNumber(string DirNumber);
        public abstract string GetDirNumber();
        public abstract void SelectDDL_TimeBegin(TimeBlock shiftStartTime = TimeBlock.AM_06_00);
        public abstract void SelectDDL_TimeEnd(TimeBlock shiftEndTime = TimeBlock.PM_04_00);
        public abstract void Enter_AverageTemp(int avgTemp = 80);
        public abstract void SelectDDL_Area(int ddListSelection = 1);
        public abstract void SelectDDL_SpecSection(int ddListSelection = 1);
        public abstract void SelectDDL_SpecSectionParagraph(int ddListSelection = 1);
        public abstract void SelectDDL_Division(int ddListSelection = 1);
        public abstract void SelectDDL_BidItemCode(int ddListSelection = 1);
        public abstract void SelectDDL_Location(int ddListSelection = 1);
        public abstract void SelectDDL_FID(int ddListSelection = 1);
        public abstract void SelectDDL_Feature(int ddListSelection = 1);
        public abstract void SelectDDL_ControlPointNumber(int ddListSelection = 1);
        public abstract void SelectDDL_HoldPointType(int ddListSelection = 1);
        public abstract void SelectDDL_Contractor(int ddListSelection = 1);
        public abstract void SelectDDL_Contractor<T>(T ddListSelection);
        public abstract void SelectDDL_Crew(int ddListSelection = 1);
        public abstract void SelectDDL_CrewForeman(int ddListSelection = 1);
        public abstract void SelectChkbox_InspectionType_I(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectChkbox_InspectionType_C(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectChkbox_InspectionType_P(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectChkbox_InspectionType_H(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectChkbox_InspectionType_R(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectChkbox_InspectionResult_P(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectChkbox_InspectionResult_E(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectChkbox_InspectionResult_F(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectChkbox_InspectionResult_NA(bool toggleChkboxIfAlreadySelected = true);
        public abstract void SelectRdoBtn_SendEmailForRevise_Yes();
        public abstract void SelectRdoBtn_SendEmailForRevise_No();
        public abstract void SelectRdoBtn_Deficiencies_Yes();
        public abstract void SelectRdoBtn_Deficiencies_No();
        public abstract void ClickTab_Create_Revise();
        public abstract void ClickTab_QC_Review();
        public abstract void ClickTab_Authorization();
        public abstract void ClickTab_Creating();
        public abstract void ClickTab_Attachments();
        public abstract void ClickTab_Revise();
        public abstract void ClickTab_To_Be_Closed();
        public abstract void ClickTab_Closed();
        public abstract void ClickTab_Create_Packages();
        public abstract void ClickTab_Packages();
        public abstract void EnterText_DeficiencyDescription(string desc = "");
        public abstract void EnterText_SectionDescription(string desc = "");
        public abstract void EnterText_EngineerComments(string comment = "");
        public abstract void Enter_ReadyDateTime(string shortDate = "", TimeBlock shortTime = TimeBlock.AM_12_00);
        public abstract void Enter_CompletedDateTime(string shortDate = "", TimeBlock shortTime = TimeBlock.AM_12_00);
        public abstract void Enter_TotalInspectionTime();
        public abstract IList<string> GetExpectedRequiredFieldIDsList();
        public abstract bool VerifyAndCloseDirLockedMessage();
        public abstract bool VerifySectionDescription();
        public abstract IList<Enum> GetResultCheckBoxIDsList();
        public abstract IList<Enum> GetDeficienciesRdoBtnIDsList();
        public abstract bool VerifyDeficiencySelectionPopupMessages();
        public abstract bool VerifyDirIsDisplayed(GridTabType tableTab, string dirNumber = "", bool noRecordsExpected = false);
        public abstract IList<string> GetExpectedHoldPointReqFieldIDsList();
        public abstract IList<string> GetExpectedEngineerCommentsReqFieldIDsList();
        public abstract bool VerifyReqFieldErrorsForNewDir(IList<string> expectedRequiredFieldIDs = null, RequiredFieldType requiredFieldType = RequiredFieldType.Default);
        public abstract string CreatePreviousFailingReport();
        public abstract bool VerifyPreviousFailingDirEntry(string previousDirNumber);
        public abstract string GetDirNumberForRow(string textInRowForAnyColumn);
        public abstract bool VerifyAutoSaveTimerRefresh();
        public abstract void RefreshAutoSaveTimer();
        public abstract bool Verify_Package_Download();
        public abstract bool Verify_Package_Created(string weekStart, string[] dirNumbers);
        public abstract string GetDirPackageWeekStartFromRow(int rowIndex = 1);
        public abstract string GetDirPackageWeekEndFromRow(int rowIndex = 1);
        public abstract string GetDirPackageNewDirCountFromRow(int rowIndex = 1);
        public abstract string GetDirPackageNumberFromRow(int rowIndex = 1);
        public abstract string[] GetDirPackageDirNumbersFromRow(PackagesColumnNameType NewDIRsOrDIRs, int rowIndex = 1);
        public abstract string CalculateDirPackageNumber(string weekStartDate);
        public abstract void EnterText_InspectionDate(string inspectDate);
        public abstract string GetTechIdForDirUserAcct(bool selectUserFromDDList = false, UserType dirNoDDListTech = UserType.DIRTechQA);
        public abstract void VerifyRecreateBtnIsDisplayed(string packageNumber, string newDirNumber);
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
                InputFieldType.Time_Begin.GetString(),
                InputFieldType.Time_End.GetString(),
                InputFieldType.Division.GetString(),
                InputFieldType.Bid_Item_Code.GetString(),
                InputFieldType.Feature.GetString(),
                InputFieldType.Crew.GetString(),
                InputFieldType.Crew_Foreman.GetString(),
                "Inspection Type",
                "Inspection Result",
                InputFieldType.Date_Ready.GetString(),
                InputFieldType.Date_Completed.GetString(),
                InputFieldType.Total_Inspection_Time.GetString()
            };

            return RequiredFieldIDs;
        }

        public override IList<Enum> GetDeficienciesRdoBtnIDsList()
        {
            IList<Enum> deficienciesRdoBtnIDs = new List<Enum>()
            {
                RadioBtnCheckboxType.Deficiencies_Yes,
                //RadioBtnsAndCheckboxes.Deficiencies_CIF,
                RadioBtnCheckboxType.Deficiencies_CDR,
                RadioBtnCheckboxType.Deficiencies_NCR
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
            PageAction.EnterText(GetTextInputFieldByLocator(InputFieldType.DateOnly_Ready), shortDate);
            PageAction.ExpandAndSelectFromDDList(InputFieldType.TimeOnly_Ready, (int)shortTime);
        }

        public override void Enter_CompletedDateTime(string shortDate = "", TimeBlock shortTime = TimeBlock.AM_12_00)
        {
            shortDate = shortDate.Equals("") ? GetShortDate() : shortDate;
            PageAction.EnterText(GetTextInputFieldByLocator(InputFieldType.DateOnly_Completed), shortDate);
            PageAction.ExpandAndSelectFromDDList(InputFieldType.TimeOnly_Completed, (int)shortTime);
        }

        public override void SelectChkbox_InspectionType_I(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Inspection_Type_I_forSG, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_InspectionType_P(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Inspection_Type_P_forSG, toggleChkboxIfAlreadySelected);

        public override void SelectChkbox_InspectionType_R(bool toggleChkboxIfAlreadySelected = true)
            => PageAction.SelectRadioBtnOrChkbox(RadioBtnCheckboxType.Inspection_Type_R_forSG, toggleChkboxIfAlreadySelected);

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
            TestUtility.AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyControlPointReqFieldErrors(), "VerifyControlPointReqFieldErrors");
            SelectDDL_HoldPointType();
        }

        public override IList<string> GetExpectedRequiredFieldIDsList()
        {
            IList<string> RequiredFieldIDs = new List<string>()
            {
                InputFieldType.Division.GetString(),
                InputFieldType.Bid_Item_Code.GetString(),
                InputFieldType.Feature.GetString(),
                InputFieldType.Crew.GetString(),
                InputFieldType.Crew_Foreman.GetString(),
                "Inspection Type",
                "Inspection Result",
                InputFieldType.DateOnly_Ready.GetString(),
                InputFieldType.TimeOnly_Ready.GetString(),
                InputFieldType.DateOnly_Completed.GetString(),
                InputFieldType.TimeOnly_Completed.GetString(),
                InputFieldType.Total_Inspection_Time.GetString()
            };

            return RequiredFieldIDs;
        }

        public override IList<string> GetExpectedHoldPointReqFieldIDsList()
        {
            IList<string> ControlPointReqFieldIDs = new List<string>()
            {
                InputFieldType.Control_Point_Type.GetString()
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
            TestUtility.AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyControlPointReqFieldErrors(), "VerifyControlPointReqFieldErrors");
            SelectDDL_ControlPointNumber();
            SelectDDL_Feature();
        }

        public override IList<string> GetExpectedRequiredFieldIDsList()
        {
            IList<string> RequiredFieldIDs = new List<string>()
            {
                InputFieldType.Time_Begin.GetString(),
                InputFieldType.Time_End.GetString(),
                InputFieldType.Spec_Section.GetString(),
                InputFieldType.Section_Description.GetString(),
                InputFieldType.Feature.GetString(),
                InputFieldType.Contractor.GetString(),
                InputFieldType.Crew_Foreman.GetString(),
                "Inspection Type",
                "Inspection Result",
                InputFieldType.Date_Ready.GetString(),
                InputFieldType.Date_Completed.GetString(),
                InputFieldType.Total_Inspection_Time.GetString()
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
            TestUtility.AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyControlPointReqFieldErrors(), "VerifyControlPointReqFieldErrors");
            SelectDDL_ControlPointNumber();
            SelectDDL_Feature();
        }

        public override IList<string> GetExpectedRequiredFieldIDsList()
        {
            IList<string> RequiredFieldIDs = new List<string>()
            {
                InputFieldType.Time_Begin.GetString(),
                InputFieldType.Time_End.GetString(),
                InputFieldType.Area.GetString(),
                InputFieldType.Spec_Section.GetString(),
                InputFieldType.Section_Description.GetString(),
                InputFieldType.Feature.GetString(),
                InputFieldType.Contractor.GetString(),
                InputFieldType.Crew_Foreman.GetString(),
                "Inspection Type",
                "Inspection Result",
                InputFieldType.Date_Ready.GetString(),
                InputFieldType.Date_Completed.GetString(),
                InputFieldType.Total_Inspection_Time.GetString()
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
                InputFieldType.Time_Begin.GetString(),
                InputFieldType.Time_End.GetString(),
                InputFieldType.Average_Temperature.GetString(),
                InputFieldType.Area.GetString(),
                InputFieldType.Spec_Section.GetString(),
                InputFieldType.Feature.GetString(),
                InputFieldType.Contractor.GetString(),
                InputFieldType.Crew_Foreman.GetString(),
                InputFieldType.Section_Description.GetString(),
                "Inspection Type",
                "Inspection Result"
            };

            return RequiredFieldIDs;
        }

        public override IList<Enum> GetDeficienciesRdoBtnIDsList()
        {
            IList<Enum> deficienciesRdoBtnIDs = new List<Enum>()
            {
                RadioBtnCheckboxType.Deficiencies_Yes
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
            TestUtility.AddAssertionToList(VerifySpecSectionDescriptionAutoPopulatedData(), "VerifySpecSectionDescriptionAutoPopulatedData");
            SelectDDL_Location();
            SelectDDL_FID();
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
                InputFieldType.Time_Begin.GetString(),
                InputFieldType.Time_End.GetString(),
                InputFieldType.Area.GetString(),
                InputFieldType.Average_Temperature.GetString(),
                InputFieldType.Spec_Section.GetString(),
                InputFieldType.Spec_Section_Paragraph.GetString(),
                InputFieldType.Section_Description.GetString(),
                InputFieldType.Feature.GetString(),
                InputFieldType.Crew_Foreman.GetString(),
                InputFieldType.Contractor.GetString(),
                "Inspection Type",
                "Inspection Result",
                InputFieldType.Date_Ready.GetString(),
                InputFieldType.Date_Completed.GetString(),
                InputFieldType.Total_Inspection_Time.GetString(),
                InputFieldType.Location.GetString()
            };

            return RequiredFieldIDs;
        }

        public override IList<Enum> GetDeficienciesRdoBtnIDsList()
        {
            IList<Enum> deficienciesRdoBtnIDs = new List<Enum>()
            {
                RadioBtnCheckboxType.Deficiencies_Yes
            };

            return deficienciesRdoBtnIDs;
        }

        public override void RefreshAutoSaveTimer()
            => ClickBtn_Save_Edit();
    }
}