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
            [StringValue("DIREntries_0__SectionDescription")] Section_Description //matches Spec Section selection
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
            [StringValue("Send To Attachment")] Send_To_Attachment,
            [StringValue("Save")] Save,
            [StringValue("Save & Forward")] Save_Forward,
            [StringValue("Add")] Add,
            [StringValue("Delete")] Delete,
        }

        public enum RadioBtnsAndCheckboxes
        {
            [StringValue("InspectionType_I_0")] Inspection_Type_I,
            [StringValue("InspectionType_C_0")] Inspection_Type_C,
            [StringValue("InspectionPassFail_P_0")] Inspection_Result_P,
            [StringValue("Inspection_Result_E_0")] Inspection_Result_E,
            [StringValue("Inspection_Result_F_0")] Inspection_Result_F,
            [StringValue("Inspection_Result_NA_0")] Inspection_Result_NA,
        }
    }

    #endregion DIR/IDR/DWR Generic Class

    public interface IQADIRs
    {
        bool IsLoaded();

        void FilterDirNumber(string DirNumber);

        void PopulateRequiredFields();

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

        void SelectChkbox_InspectionType_I(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionType_C(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionResult_P(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionResult_E(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionResult_F(bool toggleChkboxIfAlreadySelected = true);

        void SelectChkbox_InspectionResult_NA(bool toggleChkboxIfAlreadySelected = true);

        bool VerifyReqFieldErrorsForNewDir();

    }

    public abstract class QADIRs_Impl : TestBase, IQADIRs
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public T SetClass<T>() => (T)SetPageClassBasedOnTenant();

        public IQADIRs SetPageClassBasedOnTenant()
        {
            IQADIRs instance = new QADIRs(Driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using QADIRs_SGWay instance ###### ");
                instance = new QADIRs_SGWay(Driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using QADIRs_SH249 instance ###### ");
                instance = new QADIRs_SH249(Driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using QADIRs_Garnet instance ###### ");
                instance = new QADIRs_Garnet(Driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using QADIRs_GLX instance ###### ");
                instance = new QADIRs_GLX(Driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using QADIRs_I15South instance ###### ");
                instance = new QADIRs_I15South(Driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using QADIRs_I15Tech instance ###### ");
                instance = new QADIRs_I15Tech(Driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using QADIRs_LAX instance ###### ");
                instance = new QADIRs_LAX(Driver);
            }
            return instance;
        }

        [ThreadStatic]
        internal static string dirNumber;

        [ThreadStatic]
        internal static string dirNumberKey;

        internal string GetDirNumber() => GetVar(dirNumberKey);

        internal void StoreDirNumber()
        {
            dirNumber = GetAttribute(By.Id("DIRNO"), "value");
            dirNumberKey = $"{tenantName}{GetTestName()}_dirNumber";
            CreateVar(dirNumberKey, dirNumber);
            log.Debug($"#####Stored DIR Number - KEY: {dirNumberKey} || VALUE: {GetVar(dirNumberKey)}");
        }

        private By GetSubmitBtnLocator(SubmitButtons buttonName)
        {
            string buttonValue = buttonName.GetString();
            By locator = By.XPath($"//input[@value='{buttonValue}']");
            return locator;
        }

        public virtual bool IsLoaded() => Driver.Title.Equals("DIR List - ELVIS PMC");

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

        public virtual bool VerifyReqFieldErrorsForNewDir()
        {
            IList<string> reqFieldIds = GetAttributes(By.XPath("//span[text()='*']"), "id");

            IList<string> extractedIds = new List<string>();

            foreach (string fieldId in reqFieldIds)
            {

                string[] split = Regex.Split(fieldId, "0_");
                string id = split[1];

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

                id = id.SplitCamelCase();
                extractedIds.Add(id);
            }

            IList<string> errorSummaryIDs = GetErrorSummaryIDs();

            int extractedIdsCount = extractedIds.Count;
            int errorSummaryIdsCount = errorSummaryIDs.Count;

            if (extractedIdsCount.Equals(errorSummaryIdsCount))
            {
                foreach (string id in extractedIds)
                {
                    AddAssertionToList(errorSummaryIDs.Contains(id));
                }
            }

            return extractedIdsCount.Equals(errorSummaryIdsCount);
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