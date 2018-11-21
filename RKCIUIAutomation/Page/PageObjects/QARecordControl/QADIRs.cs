using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
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
            [StringValue("DIREntries_0__HoldPointNo")] Control_Point_Number,
            [StringValue("DIREntries_0__HoldPointTypeID")] Control_Point_Type,
            [StringValue("DIREntries_0__SubFeatureId")] SubFeature,//not required
            [StringValue("DIREntries_0__ContractorId")] Contractor,
            [StringValue("DIREntries_0__CrewForemanID")] Crew_Foreman, //requires selection of Contractor DDL
            [StringValue("DIREntries_0__SectionDescription")] Section_Description, //matches Spec Section selection
            [StringValue("DIREntries_0__DeficiencyDescription")] Deficiency_Description, //not required
            [StringValue("DIREntries_0__DateReady")] Date_Ready, //LAX 
            [StringValue("DIREntries_0__DateCompleted")] Date_Completed, //LAX
            [StringValue("DIREntries_0__InspectionHours")] Total_Inspection_Time //LAX
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
            [StringValue("Approve")] Approve,
            [StringValue("Add")] Add,
            [StringValue("Delete")] Delete,
            [StringValue("Submit Revise")] Submit_Revise,
        }

        public enum RadioBtnsAndCheckboxes
        {
            [StringValue("InspectionType_I_0")] Inspection_Type_I,
            [StringValue("InspectionType_C_0")] Inspection_Type_C,
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

        void ClickBtn_Approve();

        void ClickBtn_Add();

        void ClickBtn_Delete();

        void FilterDirNumber(string DirNumber);

        void PopulateRequiredFields();

        string GetDirNumber(string DirNumberKey = "");



        void SelectDDL_TimeBegin(TimeBlock shiftStartTime = TimeBlock.AM_06_00);

        void SelectDDL_TimeEnd(TimeBlock shiftEndTime = TimeBlock.PM_04_00);

        void Enter_AverageTemp(int avgTemp = 80);

        void SelectDDL_Area(int ddListSelection = 1);

        void SelectDDL_SpecSection(int ddListSelection = 1);

        void SelectDDL_Feature(int ddListSelection = 1);

        void SelectDDL_ControlPointNumber(int ddListSelection = 1);

        void SelectDDL_Contractor(int ddListSelection = 1);

        void SelectDDL_Contractor<T>(T ddListSelection);

        void SelectDDL_CrewForeman(int ddListSelection = 1);

        void SelectChkbox_InspectionType_I(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionType_C(bool toggleChkboxIfAlreadySelected = true);

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

        void Enter_ReadyDate();

        void Enter_CompletedDate();

        void Enter_TotalInspectionTime();

        IList<string> GetExpectedRequiredFieldIDsList();

        bool VerifyAndCloseDirLockedMessage();

        bool VerifyDirExistsErrorMessage();

        bool VerifySectionDescription();

        IList<Enum> GetResultCheckBoxIDsList();

        IList<Enum> GetDeficienciesRdoBtnIDsList();

        bool VerifyDeficiencySelectionPopupMessages();

        bool VerifyDirIsDisplayed(TableTab tableTab, string dirNumber = "");

        bool VerifyReqFieldErrorsForNewDir(IList<string> expectedRequiredFieldIDs = null, bool verifyControPointReqFields = false);

        bool VerifyControlPointReqFieldErrors();

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

        public virtual void ClickBtn_Approve() => JsClickElement(GetSubmitButtonByLocator(SubmitButtons.Approve));

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

        //All SimpleWF Tenants have different required fields
        public virtual void PopulateRequiredFields()
        {
            //SelectDDL_TimeBegin(TimeBlock.AM_06_00);
            //SelectDDL_TimeEnd(TimeBlock.PM_04_00);
            //Enter_AverageTemp(80);
            //SelectDDL_Area();
            //SelectDDL_SpecSection();
            //SelectDDL_Feature();
            //SelectDDL_Contractor();
            //SelectDDL_CrewForeman();
            //EnterText_SectionDescription(GetTextFromDDL(QADIRs.InputFields.Spec_Section));
            //SelectChkbox_InspectionType_I();
            //SelectChkbox_InspectionResult_P();
            //Enter_ReadyDate();
            //Enter_CompletedDate();
            //Enter_TotalInspectionTime();
            //StoreDirNumber();
        }

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
        
        public virtual void SelectDDL_TimeBegin(TimeBlock shiftStartTime = TimeBlock.AM_06_00)
            => ExpandAndSelectFromDDList(InputFields.Time_Begin, (int)shiftStartTime);

        public virtual void SelectDDL_TimeEnd(TimeBlock shiftEndTime = TimeBlock.PM_04_00)
            => ExpandAndSelectFromDDList(InputFields.Time_End, (int)shiftEndTime);

        public virtual void Enter_AverageTemp(int avgTemp = 80)
        {
            string avgTempFieldId = InputFields.Average_Temperature.GetString();
            By clickLocator = By.XPath($"//input[@id='{avgTempFieldId}']/preceding-sibling::input");
            By locator = By.XPath($"//input[@id='{avgTempFieldId}']");
            ClickElement(clickLocator);
            EnterText(locator, avgTemp.ToString(), false);
        }

        public virtual void SelectDDL_Area(int ddListSelection = 1)
            => ExpandAndSelectFromDDList(InputFields.Area, ddListSelection);

        public virtual void SelectDDL_SpecSection(int ddListSelection = 1)
            => ExpandAndSelectFromDDList(InputFields.Spec_Section, ddListSelection);

        public virtual void SelectDDL_Feature(int ddListSelection = 1)
            => ExpandAndSelectFromDDList(InputFields.Feature, ddListSelection);

        public virtual void SelectDDL_ControlPointNumber(int ddListSelection = 1)
            => ExpandAndSelectFromDDList(InputFields.Control_Point_Number, ddListSelection);

        public virtual void SelectDDL_Contractor(int ddListSelection =1)
            => ExpandAndSelectFromDDList(InputFields.Contractor, ddListSelection);

        public virtual void SelectDDL_Contractor<T>(T ddListSelection)
            => ExpandAndSelectFromDDList(InputFields.Contractor, ddListSelection);

        public virtual void SelectDDL_CrewForeman(int ddListSelection = 1)
            => ExpandAndSelectFromDDList(InputFields.Crew_Foreman, ddListSelection);

        public virtual bool VerifySectionDescription()
        {
            bool textMatch = false;

            try
            {
                string sectionDesc = GetText(GetTextAreaFieldByLocator(InputFields.Section_Description));
                string specSectionDDListValue = GetTextFromDDL(InputFields.Spec_Section);
                specSectionDDListValue = specSectionDDListValue.Contains("-") 
                    ? Regex.Replace(specSectionDDListValue, "-", " - ") 
                    : specSectionDDListValue;

                textMatch = sectionDesc.Equals(specSectionDDListValue);
                LogInfo($"Spec Section DDList Selection: {specSectionDDListValue}<br>Section Description Text Value: {sectionDesc}", textMatch);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
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

                foreach (Enum resultChkBox in resultChkBoxIDs)
                {
                    SelectRadioBtnOrChkbox(resultChkBox, false);

                    foreach (Enum deficiencyRdoBtn in deficienciesRdoBtnIDs)
                    {
                        SelectRadioBtnOrChkbox(deficiencyRdoBtn);
                        resultTypeMsg = resultChkBox.Equals(RadioBtnsAndCheckboxes.Inspection_Result_P) ? "Pass" : "Engineer Decision";
                        expectedAlertMsg = $"Since {resultTypeMsg} checked, not allow to check any deficiency";
                        alertMsg = GetAlertMessage();
                        alertMsgMatch = alertMsg.Equals(expectedAlertMsg);
                        assertList.Add(alertMsgMatch);
                        LogInfo($"<br>Selected : Result ( {resultTypeMsg} ) - Deficiency ( {deficiencyRdoBtn.ToString()} )<br>Expected Alert Msg: {expectedAlertMsg}<br>Actual Alert Msg: {alertMsg}", alertMsgMatch);
                        AcceptAlertMessage();
                    }
                }

                SelectChkbox_InspectionResult_P(false); //Ensures Pass Result checkbox is selected
                SelectRdoBtn_Deficiencies_No();
                SelectChkbox_InspectionResult_P(); //Unchecks Pass Result checkbox

                foreach (Enum deficiencyRdoBtn in deficienciesRdoBtnIDs)
                {
                    SelectRadioBtnOrChkbox(deficiencyRdoBtn);

                    foreach (Enum resultChkBox in resultChkBoxIDs)
                    {
                        SelectRadioBtnOrChkbox(resultChkBox, false);
                        resultTypeMsg = resultChkBox.Equals(RadioBtnsAndCheckboxes.Inspection_Result_P) ? "pass" : "make engineer decision";
                        expectedAlertMsg = $"There is a deficiency checked in this entry, not allow to {resultTypeMsg}!";
                        alertMsg = GetAlertMessage();
                        alertMsgMatch = alertMsg.Equals(expectedAlertMsg);
                        assertList.Add(alertMsgMatch);
                        LogInfo($"<br>Selected : Deficiency ( {deficiencyRdoBtn.ToString()} ) - Result ( {resultTypeMsg} )<br>Expected Alert Msg: {expectedAlertMsg}<br>Actual Alert Msg: {alertMsg}", alertMsgMatch);
                        AcceptAlertMessage();
                    }
                }

                alertMsgExpected = assertList.Contains(false) ? false : true;
            }
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

        public virtual void SelectRdoBtn_Deficiencies_Yes()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Deficiencies_Yes);

        public virtual void SelectRdoBtn_Deficiencies_No()
            => SelectRadioBtnOrChkbox(RadioBtnsAndCheckboxes.Deficiencies_No);

        public virtual void EnterText_DeficiencyDescription(string desc = "")
            => EnterText(GetTextAreaFieldByLocator(InputFields.Deficiency_Description),
                desc = desc.Equals("") ? "RKCI Automation Deficiency Description" : desc);

        public virtual void EnterText_SectionDescription(string desc = "")
            => EnterText(GetTextAreaFieldByLocator(InputFields.Section_Description),
                desc = desc.Equals("") ? "RKCI Automation Section Description" : desc);

        //LAX
        public virtual void Enter_ReadyDate() 
            => EnterText(GetTextInputFieldByLocator(InputFields.Date_Ready), GetShortDateTime() );
        
        //LAX
        public virtual void Enter_CompletedDate() 
            => EnterText(GetTextInputFieldByLocator(InputFields.Date_Completed), GetShortDateTime());

        //LAX
        public virtual void Enter_TotalInspectionTime()
        {
            string inspectTimeFieldId = InputFields.Total_Inspection_Time.GetString();
            By clickLocator = By.XPath($"//input[@id='{inspectTimeFieldId}']/preceding-sibling::input");
            By locator = By.XPath($"//input[@id='{inspectTimeFieldId}']");
            ClickElement(clickLocator);
            EnterText(locator, "24", false);
        }

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
                                id = "ControlPoint No.";
                            }
                            else
                                id = "ControlPoint Type";
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
                        }

                        if (!fieldId.Contains("Time"))
                        {
                            if (!id.Contains(" "))
                            {
                                id = id.SplitCamelCase();
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

        internal bool VerifyExpectedRequiredFields(IList<string> actualRequiredFieldIDs, IList<string> expectedRequiredFieldIDs)
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
                        idTable[tblRowIndex] = new string[2] { $" |  {actualID} : ", $" {reqFieldsMatch.ToString()}" };
                    }
                }
                else
                {
                    LogInfo($"Expected and Actual Required Field Counts DO NOT MATCH:" +
                        $"<br>Expected Count: {expectedCount}<br>Actual Count: {actualCount}", countsMatch);
                }

                reqFieldsMatch = results.Contains(false) ? false : true;
                idTable[tblRowIndex +1] = new string[2] {"Total Required Fields:", (results.Count).ToString()};
                LogInfo(idTable, reqFieldsMatch);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return reqFieldsMatch;
        }

        private IList<string> GetErrorSummaryIDs()
        {
            IList<string> errorElements = null;
            IList<string> extractedFieldNames = null;

            try
            {
                errorElements = GetTextForElements(By.XPath("//div[contains(@class,'validation-summary-errors')]/ul/li"));
                extractedFieldNames = new List<string>();

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
                    log.Debug($"Adding Required Field Error Summary ID to Actuals list: {fieldName}");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
            finally
            {
                JsClickElement(By.XPath("//span[@id='ErrorSummaryWindow_wnd_title']/following-sibling::div/a[@aria-label='Close']"));
            }

            return extractedFieldNames;
        }
        
        internal IList<string> ControlPointReqFieldIDs = new List<string>()
        {
            InputFields.Control_Point_Number.GetString(),
            InputFields.Control_Point_Type.GetString()
        };

        public virtual bool VerifyReqFieldErrorsForNewDir(IList<string> expectedRequiredFieldIDs = null, bool verifyControPointReqFields = false)
        {
            IList<bool> reqFieldAssertList = null;

            bool requiredFieldsMatch = false;

            try
            {
                IList<string> errorSummaryIDs = GetErrorSummaryIDs();

                string controlPointBaseXpath = "//span[contains(@aria-owns,'HoldPoint')]";

                string reqFieldIdXPath = verifyControPointReqFields ? $"{controlPointBaseXpath}/input" : "//span[text()='*']";

                string splitPattern = verifyControPointReqFields ? "0__" : "0_";

                IList<string> trimmedActualIds = TrimInputFieldIDs(GetAttributes(By.XPath(reqFieldIdXPath), "id"), splitPattern);

                expectedRequiredFieldIDs = expectedRequiredFieldIDs ?? QaRcrdCtrl_QaDIR.GetExpectedRequiredFieldIDsList();

                bool actualMatchesExpected = VerifyExpectedRequiredFields(trimmedActualIds, expectedRequiredFieldIDs);

                if (actualMatchesExpected)
                {
                    reqFieldAssertList = new List<bool>();
                    int actualIdCount = trimmedActualIds.Count;
                    int expectedIdCount = expectedRequiredFieldIDs.Count;
                    int errorSummaryIdCount = errorSummaryIDs.Count;
                    bool countsMatch = actualIdCount.Equals(errorSummaryIdCount);
                    LogInfo($"Expected ID Count: {expectedIdCount}<br>Actual ID Count: {actualIdCount}<br>Required Field Error Summary ID Count: {errorSummaryIdCount}", countsMatch);

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

            return requiredFieldsMatch;
        }

        public virtual bool VerifyControlPointReqFieldErrors() => QaRcrdCtrl_QaDIR.VerifyReqFieldErrorsForNewDir(ControlPointReqFieldIDs, true);
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
            Enter_ReadyDate();
            Enter_CompletedDate();
            Enter_TotalInspectionTime();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyControlPointReqFieldErrors(), "VerifyControlPointReqFieldErrors");
            SelectDDL_ControlPointNumber();
            StoreDirNumber();
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
            Enter_ReadyDate();
            Enter_CompletedDate();
            Enter_TotalInspectionTime();
            ClickBtn_Save_Forward();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyControlPointReqFieldErrors());
            SelectDDL_ControlPointNumber();
            StoreDirNumber();
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
            SelectDDL_SpecSection();
            SelectDDL_Feature();
            SelectDDL_Contractor();
            SelectDDL_CrewForeman();
            SelectChkbox_InspectionType_C();
            SelectChkbox_InspectionResult_P();
            ClickBtn_Save_Forward();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyControlPointReqFieldErrors());
            SelectDDL_ControlPointNumber();
            StoreDirNumber();
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
            SelectDDL_Feature();
            SelectDDL_Contractor("LINXS");
            SelectDDL_CrewForeman();
            EnterText_SectionDescription(GetTextFromDDL(QADIRs.InputFields.Spec_Section));
            SelectChkbox_InspectionType_C();
            SelectChkbox_InspectionResult_P();
            Enter_ReadyDate();
            Enter_CompletedDate();
            Enter_TotalInspectionTime();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyControlPointReqFieldErrors());
            //SelectDDL_ControlPointNumber(); //Currently does not have values to choose in the drop down list
            SelectChkbox_InspectionType_I();
            StoreDirNumber();
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
    }
}