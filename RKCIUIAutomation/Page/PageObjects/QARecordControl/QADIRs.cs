using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QADIRs;
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
            [StringValue("TimeBegin1")] Time_Begin,
            [StringValue("TimeEnd1")] Time_End,
            [StringValue("DIREntries_0__AverageTemperature")] Average_Temperature,
            [StringValue("DIREntries_0__AreaID")] Area,
            [StringValue("DIREntries_0__SectionId")] Spec_Section,
            [StringValue("DIREntries_0__FeatureID")] Feature, //requires selection of Area DDL
            [StringValue("DIREntries_0__SubFeatureId")] SubFeature,//not required
            [StringValue("DIREntries_0__ContractorId")] Contractor,
            [StringValue("DIREntries_0__CrewForemanID")] Crew_Foreman, //requires selection of Contractor DDL
            [StringValue("DIREntries_0__SectionDescription")] Section_Description, //matches Spec Section selection
            [StringValue("DIREntries_0__DeficiencyDescription")] Deficiency_Description //not required
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
            [StringValue("DIR №")] DIR_No,
            [StringValue("Revision")] Revision,
            [StringValue("Created By")] Created_By,
            [StringValue("Sent By")] Sent_By,
            [StringValue("Sent Date")] Sent_Date,
            [StringValue("Locked By")] Locked_By,
            [StringValue("Lock Date")] Locked_Date,
            [StringValue("Report №")] Report,
            [StringValue("Action")] Action,
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
            [StringValue("Add")] Add,
            [StringValue("Delete")] Delete,
            [StringValue("Submit Revise")] Submit_Revise,
        }

        public enum RadioBtnsAndCheckboxes
        {
            [StringValue("InspectionType_I_0")] Inspection_Type_I,
            [StringValue("InspectionType_C_0")] Inspection_Type_C,
            [StringValue("InspectionPassFail_P_0")] Inspection_Result_P,
            [StringValue("Inspection_Result_E_0")] Inspection_Result_E,
            [StringValue("Inspection_Result_F_0")] Inspection_Result_F,
            [StringValue("Inspection_Result_NA_0")] Inspection_Result_NA,
            [StringValue("sendNotification1")] SendEmailNotification_Yes,
            [StringValue("sendNotification2")] SendEmailNotification_No,
            [StringValue("DIREntries_0__Deficiency_6")] AnyDeficiencies_Yes,
            [StringValue("DIREntries_0__Deficiency_0")] AnyDeficiencies_No
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

        void ClickBtn_Add();

        void ClickBtn_Delete();

        void FilterDirNumber(string DirNumber);

        void PopulateRequiredFields();

        string GetDirNumber(string DirNumberKey = "");

        bool VerifyAndCloseDirLockedMessage();

        bool VerifyDirExistsErrorMessage();

        void SelectDDL_TimeBegin(TimeBlock shiftStartTime = TimeBlock.AM_06_00);

        void SelectDDL_TimeEnd(TimeBlock shiftEndTime = TimeBlock.PM_04_00);

        void Enter_AverageTemp(int avgTemp = 80);

        void SelectDDL_Area(int ddListSelection = 1);

        void SelectDDL_SpecSection(int ddListSelection = 1);

        void SelectDDL_Feature(int ddListSelection = 1);

        void SelectDDL_Contractor(int ddListSelection = 1);

        void SelectDDL_CrewForeman(int ddListSelection = 1);

        bool VerifySectionDescription();

        bool VerifyDirIsDisplayed(TableTab tableTab, string dirNumber = "");

        void SelectChkbox_InspectionType_I(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionType_C(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionResult_P(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionResult_E(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionResult_F(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionResult_NA(bool toggleChkboxIfAlreadySelected = true);

        void SelectRdoBtn_SendEmailForRevise_Yes();

        void SelectRdoBtn_SendEmailForRevise_No();

        void SelectRdoBtn_AnyDeficiencies_Yes();

        void SelectRdoBtn_AnyDeficiencies_No();

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

        void EnterText_DeficiencyDescription();

        IList<string> GetRequiredFieldIDs();

        bool VerifyReqFieldErrorsForNewDir();

    }

    public abstract class QADIRs_Impl : TestBase, IQADIRs
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
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

        [ThreadStatic]
        internal static string dirNumber;

        [ThreadStatic]
        internal static string dirNumberKey;

        public virtual string GetDirNumber(string dirNumKey = "")
        {
            dirNumKey = dirNumKey.Equals("") ? dirNumberKey : dirNumKey;
            return GetVar(dirNumKey);
        }

        internal void StoreDirNumber()
        {
            dirNumber = GetAttribute(By.Id("DIRNO"), "value");
            dirNumberKey = $"{tenantName}{GetTestName()}_dirNumber";
            CreateVar(dirNumberKey, dirNumber);
            log.Debug($"#####Stored DIR Number - KEY: {dirNumberKey} || VALUE: {GetVar(dirNumberKey)}");
        }

        public virtual bool IsLoaded() => Driver.Title.Equals("DIR List - ELVIS PMC");

        public virtual void ClickBtn_CreateNew() => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Create_New));

        public virtual void ClickBtn_CreateRevision() => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Create_Revision));

        public virtual void ClickBtn_Refresh() => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Refresh));

        public virtual void ClickBtn_Cancel() => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Cancel));

        public virtual void ClickBtn_KickBack() => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Kick_Back));

        public virtual void ClickBtn_SubmitRevise() => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Submit_Revise));

        public virtual void ClickBtn_Send_To_Attachment() => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Send_To_Attachment));

        public virtual void ClickBtn_Save() => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Save));

        public virtual void ClickBtn_Save_Forward() => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Save_Forward));

        public virtual void ClickBtn_Add() => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Add));

        public virtual void ClickBtn_Delete() => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Delete));

        public virtual void ClickTab_Create_Revise() => ClickTab(TableTab.Create_Revise);

        public virtual void ClickTab_QC_Review() => ClickTab(TableTab.QC_Review);

        public virtual void ClickTab_Authorization() => ClickTab(TableTab.Authorization);

        public virtual void ClickTab_Creating() => ClickTab(TableTab.Creating);

        public virtual void ClickTab_Attachments() => ClickTab(TableTab.Attachments);

        public virtual void ClickTab_Revise() => ClickTab(TableTab.Revise);

        public virtual void ClickTab_To_Be_Closed() => ClickTab(TableTab.To_Be_Closed);

        public virtual void ClickTab_Closed() => ClickTab(TableTab.Closed);

        public virtual void ClickTab_Create_Packages() => ClickTab(TableTab.Create_Packages);

        public virtual void ClickTab_Packages() => ClickTab(TableTab.Packages);
        
        public virtual void FilterDirNumber(string DirNumber)
            => FilterTableColumnByValue(ColumnName.DIR_No, DirNumber);

        public virtual void PopulateRequiredFields()
        {
            SelectDDL_TimeBegin(TimeBlock.AM_06_00);
            SelectDDL_TimeEnd(TimeBlock.PM_04_00);
            Enter_AverageTemp(80);
            SelectDDL_Area();
            SelectDDL_SpecSection();
            SelectDDL_Feature();
            SelectDDL_Contractor();
            SelectDDL_CrewForeman();
            AddAssertionToList(VerifySectionDescription());
            SelectChkbox_InspectionType_I();
            SelectChkbox_InspectionResult_P();
            StoreDirNumber();
        }

        public virtual void SelectDDL_TimeBegin(TimeBlock shiftStartTime = TimeBlock.AM_06_00)
            => ExpandAndSelectFromDDList(InputFields.Time_Begin, (int)shiftStartTime);

        public virtual void SelectDDL_TimeEnd(TimeBlock shiftEndTime = TimeBlock.PM_04_00)
            => ExpandAndSelectFromDDList(InputFields.Time_End, (int)shiftEndTime);

        public virtual void Enter_AverageTemp(int avgTemp = 80)
            => EnterText(By.Id(InputFields.Average_Temperature.GetString()), avgTemp.ToString());

        public virtual void SelectDDL_Area(int ddListSelection = 1)
            => ExpandAndSelectFromDDList(InputFields.Area, ddListSelection);

        public virtual void SelectDDL_SpecSection(int ddListSelection = 1)
            => ExpandAndSelectFromDDList(InputFields.Spec_Section, ddListSelection);

        public virtual void SelectDDL_Feature(int ddListSelection = 1)
            => ExpandAndSelectFromDDList(InputFields.Feature, ddListSelection);

        public virtual void SelectDDL_Contractor(int ddListSelection = 1)
            => ExpandAndSelectFromDDList(InputFields.Contractor, ddListSelection);

        public virtual void SelectDDL_CrewForeman(int ddListSelection = 1)
            => ExpandAndSelectFromDDList(InputFields.Crew_Foreman, ddListSelection);

        public virtual bool VerifySectionDescription()
        {
            string sectionIdValue = GetTextFromDDL(InputFields.Spec_Section);
            string[] splitText = Regex.Split(sectionIdValue, "-");

            string sectionDesc = GetText(GetTextAreaFieldByLocator(InputFields.Section_Description));
            bool textMatch = sectionDesc.Equals($"{splitText[0]} - {splitText[1]}");
            LogInfo($"Spec Section DDList Selection: {sectionIdValue}<br>Section Description Text Value: {sectionDesc}", textMatch);
            return textMatch;
        }

        public virtual bool VerifyDirIsDisplayed(TableTab tableTab, string dirNumber = "")
        {
            bool isDisplayed = false;

            try
            {
                ClickTab(tableTab);
                string _dirNum = dirNumber.Equals("") ? GetDirNumber() : dirNumber;
                isDisplayed = VerifyRecordIsDisplayed(ColumnName.DIR_No, _dirNum);
                string logMsg = isDisplayed ? "Found" : "Unable to find";
                LogInfo($"{logMsg} record under {tableTab.GetString()} tab with DIR Number: {_dirNum}.", isDisplayed);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return isDisplayed;
        }

        public virtual void SelectChkbox_InspectionType_I(bool toggleChkboxIfAlreadySelected = true)
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Type_I, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_InspectionType_C(bool toggleChkboxIfAlreadySelected = true)
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Type_C, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_InspectionResult_P(bool toggleChkboxIfAlreadySelected = true)
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Result_P, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_InspectionResult_E(bool toggleChkboxIfAlreadySelected = true)
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Result_E, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_InspectionResult_F(bool toggleChkboxIfAlreadySelected = true)
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Result_F, toggleChkboxIfAlreadySelected);

        public virtual void SelectChkbox_InspectionResult_NA(bool toggleChkboxIfAlreadySelected = true)
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Inspection_Result_NA, toggleChkboxIfAlreadySelected);

        public virtual void SelectRdoBtn_SendEmailForRevise_Yes()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.SendEmailNotification_Yes);

        public virtual void SelectRdoBtn_SendEmailForRevise_No()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.SendEmailNotification_No);

        public virtual void SelectRdoBtn_AnyDeficiencies_Yes()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.AnyDeficiencies_Yes);

        public virtual void SelectRdoBtn_AnyDeficiencies_No()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.AnyDeficiencies_No);

        public virtual void EnterText_DeficiencyDescription()
            => EnterText(GetTextAreaFieldByLocator(InputFields.Deficiency_Description), "RKCI Automation Deficiency Description");

        public virtual bool VerifyAndCloseDirLockedMessage()
        {
            By msgBodyLocator = By.Id("dirLockedPopup");
            By msgTitleLocator = By.Id("dirLockedPopup_wnd_title");

            string expectedMsg = "Since you created this inspection report, you are not allowed to do QC Review or DIR Approval!";            
            string actualMsg = GetText(msgBodyLocator);

            bool msgMatch = actualMsg.Equals(expectedMsg);
            LogInfo($"Expected Msg: {expectedMsg}<br>Actual Msg: {actualMsg}", msgMatch);
            ClickElement(By.XPath("//span[@id='dirLockedPopup_wnd_title']/following-sibling::div/a"));
            return msgMatch;
        }

        public virtual bool VerifyDirExistsErrorMessage()
        {
            string expectedMsg = "This DIR No. exists in the database.";
            string actualMsg = GetText(By.Id("error"));
            bool msgMatch = actualMsg.Equals(expectedMsg);
            LogInfo($"Expected Msg: {expectedMsg}<br>Actual Msg: {actualMsg}", msgMatch);
            return msgMatch;
        }
        
        internal IList<string> TrimInputFieldIDs(IList<string> fieldIdList, string splitPattern)
        {
            IList<string> trimmedList = new List<string>();

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
                    else if (id.Contains("ID"))
                    {
                        newId = Regex.Split(id, "ID");
                        id = newId[0];
                    }
                    else if (id.Contains("PassFail"))
                    {
                        id = Regex.Replace(id, "PassFail", "Result");
                    }

                    id = !fieldId.Contains("Time") ? id.SplitCamelCase() : id;
                }
                else if (fieldId.Contains("Inspection"))
                {
                    id = Regex.Replace(fieldId, "PassFail", "Result");
                    id = id.SplitCamelCase();
                }
                else
                {
                    id = fieldId;
                }
                                
                trimmedList.Add(id);
            }

            return trimmedList;
        }

        public virtual IList<string> GetRequiredFieldIDs()
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
                "InspectionType",
                "InspectionPassFail"
            };

            return RequiredFieldIDs;
        }

        internal bool VerifyExpectedRequiredFields(IList<string> actualReqFieldIDs, IList<string> expectedReqFieldIDs = null)
        {
            IList<string> expectedIDs = expectedReqFieldIDs ?? QaRcrdCtrl_QaDIR.GetRequiredFieldIDs();

            int expectedCount = expectedIDs.Count;
            int actualCount = actualReqFieldIDs.Count;
            bool countsMatch = expectedCount.Equals(actualCount);

            LogInfo($"Expected ID Count: {expectedCount}<br>Actual ID Count: {actualCount}", countsMatch);

            bool reqFieldsMatch = false;

            if (countsMatch)
            {
                foreach (string id in actualReqFieldIDs)
                {
                    reqFieldsMatch = expectedIDs.Contains(id);
                    string logMsg = reqFieldsMatch ? "" : " not";
                    LogInfo($"Required field ID: {id} is{logMsg} an expected required field.", reqFieldsMatch);
                    if (!reqFieldsMatch)
                    {
                        break;
                    }
                }
            }

            return reqFieldsMatch;
        }

        private void CloseReqFieldErrorSummaryPopup() 
            => JsClickElement(By.XPath("//span[@id='ErrorSummaryWindow_wnd_title']/following-sibling::div/a[@aria-label='Close']"));

        public virtual bool VerifyReqFieldErrorsForNewDir()
        {
            IList<string> errorSummaryIDs = GetErrorSummaryIDs();
            CloseReqFieldErrorSummaryPopup();

            //IList<string> actualReqFieldIds = GetAttributes(By.XPath("//span[text()='*']"), "id");
            IList<string> trimmedActualIds = TrimInputFieldIDs(GetAttributes(By.XPath("//span[text()='*']"), "id"), "0_");

            bool actualMatchesExpected = VerifyExpectedRequiredFields(trimmedActualIds);
            int actualIdCount = trimmedActualIds.Count;
            int errorSummaryIdCount = errorSummaryIDs.Count;

            bool countsMatch = actualIdCount.Equals(errorSummaryIdCount);

            LogInfo($"Error Summary ID Count: {errorSummaryIdCount}<br>Actual ID Count: {actualIdCount}", countsMatch);
            if (countsMatch)
            {
                foreach (string id in trimmedActualIds)
                {
                    AddAssertionToList(errorSummaryIDs.Contains(id));
                }
            }

            return actualIdCount.Equals(errorSummaryIdCount);
        }

        private IList<string> GetErrorSummaryIDs()
        {
            IList<string> errorElements = GetTextForElements(By.XPath("//div[contains(@class,'validation-summary-errors')]/ul/li"));

            IList<string> extractedFieldNames = new List<string>();

            foreach (string error in errorElements)
            {
                string[] splitType = new string[2];            

                if (error.Contains("DIR:"))
                {
                    splitType = Regex.Split(error, "Shift ");
                }
                else if (error.Contains("Entry Number"))
                {
                    splitType = Regex.Split(error, "1: ");
                }

                string[] splitReq = Regex.Split(splitType[1], " Required");
                string fieldName = splitReq[0];
                extractedFieldNames.Add(fieldName);
            }

            return extractedFieldNames;
        }

    }

    public class QADIRs_Garnet : QADIRs
    {
        public QADIRs_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QADIRs_GLX : QADIRs
    {
        public QADIRs_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QADIRs_SH249 : QADIRs
    {
        public QADIRs_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QADIRs_SGWay : QADIRs
    {
        public QADIRs_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QADIRs_I15South : QADIRs
    {
        public QADIRs_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QADIRs_I15Tech : QADIRs
    {
        public QADIRs_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QADIRs_LAX : QADIRs
    {
        public QADIRs_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}