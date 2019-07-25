using AventStack.ExtentReports.MarkupUtils;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QATestAll_Common;

namespace RKCIUIAutomation.Page.PageObjects.QARecordControl
{
    public class QATestAll_Common : QATestAll
    {
        internal By LinLocator = By.XPath("//input[@id='Lot_LIN']");
        internal By errorLINRequired = By.XPath("//span[contains(@class, 'ValidationErrorMessage')][contains(text(), 'LIN required')]");
        internal By errorLINExists = By.XPath("//span[contains(@class, 'ValidationErrorMessage')][contains(text(), 'LIN exists')]");

        //SG & SH249
        internal string GenerateLIN()
        {
            string techID = "1109";
            string userAcct = GetCurrentUser();

            if (userAcct.Contains("TestTechMgr"))
            {
                techID = "1110";
            }

            string today = GetShortDate(formatWithZero: true);
            string[] splitDate = Regex.Split(today, "/");
            string mm = splitDate[0];
            string dd = splitDate[1];
            string yy = Regex.Split(splitDate[2], "20")[1];

            if (int.Parse(mm) < 10)
            {
                mm = $"0{mm}";
            }

            if (int.Parse(dd) < 10)
            {
                dd = $"0{dd}";
            }

            return $"{techID}{yy}{mm}{dd}00";
        }

        internal void ToggleTestMethodCheckbox<T>(T testMethodType)
        {
            string identifierAttribute = string.Empty;

            if(testMethodType is Enum)
            {
                identifierAttribute = ConvertToType<Enum>(testMethodType).GetString();
            }
            else if(testMethodType.GetType().Equals(typeof(string)))
            {
                identifierAttribute = ConvertToType<string>(testMethodType);
            }          

            By chkbxLocator = null;
            string testMethodInputElemId = string.Empty;
            
            try
            {
                chkbxLocator = By.XPath($"//input[@identifier='{identifierAttribute}']");
                testMethodInputElemId = GetAttributeForElement(chkbxLocator, "id");
            }
            catch (NoSuchElementException)
            {
                chkbxLocator = By.XPath($"//input[contains(@identifier,'{identifierAttribute}')]");
                testMethodInputElemId = GetAttributeForElement(chkbxLocator, "id");
            }

            SelectRadioBtnOrChkbox(testMethodInputElemId);
        }

        public QATestAll_Common()
        {
            testRecordKVPairsList = GetTestRecordKVPairsList();
        }

        public QATestAll_Common(IWebDriver driver) => this.Driver = driver;

        public override T SetClass<T>(IWebDriver driver)
        {
            IQATestAll instance = new QATestAll_Common(driver);
            if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using QATestAll_LAX instance ###### ");
                instance = new QATestAll_LAX(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using QATestAll_SH249 instance ###### ");
                instance = new QATestAll_SH249(driver);
            }
            else if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using QATestAll_SGWay instance ###### ");
                instance = new QATestAll_SGWay(driver);
            }
            else if (tenantName == TenantName.I15North)
            {
                log.Info($"###### using QATestAll_GLX instance ###### ");
                instance = new QATestAll_I15North(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using QATestAll_I15South instance ###### ");
                instance = new QATestAll_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using QATestAll_I15Tech instance ###### ");
                instance = new QATestAll_I15Tech(driver);
            }

            return (T)instance;
        }

        [ThreadStatic]
        public IList<KeyValuePair<TestDetails_InputFieldType, string>> testRecordKVPairsList;

        public override IList<TestDetails_InputFieldType> AllInputFieldTypeList { get; set; }
        public override IList<TestDetails_InputFieldType> RequiredInputFieldTypeList { get; set; }

        public enum CreateTest_InputFieldType
        {
            [StringValue("SelectedTechName", DDL)] TechnicianName,
            [StringValue("sampleDate", DATE)] LINDate,
            [StringValue("SequenceNumber", TEXT)] SN,
            [StringValue("TestTypeId", DDL)] TestType,
            [StringValue("SelectedStatusFlowTypeId", DDL)] WorkflowType,
            [StringValue("MaterialCategoryId")] MaterialCategory
        }

        public enum TestDetails_InputFieldType
        {
            [StringValue("StatusFlowName", AUTOPOPULATED)] WorkflowType,
            [StringValue("//div[contains(@class, 'SmallerLabelForId')]", XPATH_TEXT)] LotId,
            [StringValue("LinBase", AUTOPOPULATED_TEXT)] LIN,
            [StringValue("//input[@id='LIN']", XPATH_TEXT)] LINforRevision,
            [StringValue("//input[@id='LinNew']", XPATH_TEXT)] LINforRetest,
            [StringValue("//label[contains(text(),'Revision')]/following-sibling::div", XPATH_TEXT)] Revision,
            [StringValue("Lot_LabId", AUTOPOPULATED_TEXT)] LabId,
            [StringValue("//input[@id='Lot_ReportTypeId']/parent::span", XPATH_TEXT),] ReportType,
            [StringValue("SampleByDiv", AUTOPOPULATED_TEXT)] SampleBy,
            [StringValue("SampleDateDiv", AUTOPOPULATED_TEXT)] SampleDate,
            [StringValue("SequenceNumber", AUTOPOPULATED_TEXT)] LIN_SN,
            [StringValue("Lot_MaterialId", DDL)] MaterialCode,
            [StringValue("Lot_GradeId", AUTOPOPULATED_DDL)] GradeClassType,
            [StringValue("Lot_MaterialDescription", AUTOPOPULATED_TEXT)] MaterialDescription,
            [StringValue("Lot_SupplierId", AUTOPOPULATED_DDL)] Supplier,
            [StringValue("Lot_SegmentId", DDL)] Segment,
            [StringValue("Lot_RoadwayId", DDL)] Roadway,
            [StringValue("Lot_DirectionId", DDL)] Direction,
            [StringValue("Lot_FeatureId", DDL)] Feature,
            [StringValue("Lot_SampleLocation", TEXT)] SampleLocation,
            [StringValue("Lot_StructureId", DDL)] Structure,
            [StringValue("Lot_DirNo", TEXT)] DirReference,
            [StringValue("Lot_DirEntry", NUMBER)] Entry,
            [StringValue("Lot_Miscellaneous", TEXT)] Other,
            [StringValue("Lot_LotSubs_0__LotSubSamples_0__SampleTypeId", DDL)] SampleType,
            [StringValue("Lot_LotSubs_0__LotSubSamples_0__Random1", NUMBER)] RandomNumber1,
            [StringValue("Lot_LotSubs_0__LotSubSamples_0__Random2", NUMBER)] RandomNumber2,
            [StringValue("Lot_LotSubs_0__LotSubSamples_0__SplitSampleID", TEXT)] SplitSample,
            [StringValue("Lot_LotSubs_0__LotSubSamples_0__FIDId", DDL)] FID,
            [StringValue("Lot_LotSubs_0__StationLimitBegin", NUMBER)] SublotStationLimits_Begin,
            [StringValue("Lot_LotSubs_0__StationLimitEnd", NUMBER)] SublotStationLimits_End,
            [StringValue("Lot_LotSubs_0__StationLimitBeginOffset", NUMBER)] SublotStationLimits_BeginOffset,
            [StringValue("Lot_LotSubs_0__StationLimitEndOffset", NUMBER)] SublotStationLimits_EndOffset,
            [StringValue("Lot_LotSubs_0__LotSubSamples_0__Station", NUMBER)] Station,
            [StringValue("Lot_LotSubs_0__LotSubSamples_0__StationOffset", NUMBER)] Station_Offset,
            [StringValue("StationLimitBeginOffsetDirection_-1")] RadioBtn_SublotStationLimits_BeginOffset_L,
            [StringValue("StationLimitBeginOffsetDirection_1")] RadioBtn_SublotStationLimits_BeginOffset_R,
            [StringValue("StationLimitEndOffsetDirection_-1")] RadioBtn_SublotStationLimits_EndOffset_L,
            [StringValue("StationLimitEndOffsetDirection_1")] RadioBtn_SublotStationLimits_EndOffset_R,
            [StringValue("StationOffsetDirection_-1")] RadioBtn_Station_Offset_L,
            [StringValue("StationOffsetDirection_R")] RadioBtn_Station_Offset_R
        }

        public enum ButtonType
        {
            [StringValue("CreateNew")] CreateNew,
            [StringValue("CreateCorrection")] CreateRevision,
            [StringValue("CreateRetest")] CreateRetest,
            [StringValue("Continue1Button")] Continue,
            [StringValue("CloseSelectedTest_PendingClose")] CloseSelected,
            [StringValue("ViewSelectedTest_PendingClose")] ViewSelected,
            [StringValue("CancelTest")] Cancel,
            [StringValue("SubmitTest")] Save,
            [StringValue("SubmitTestAndContinue")] SaveEdit,
            [StringValue("AddRemoveTestMethodsButton")] AddRemoveTestMethods,
            [StringValue("ReviewReviseToSupervisor")] ToSupervisor
        }

        //SH249, SG
        public enum MaterialCategoryType
        {
            [StringValue("HMA Mixture & Aggregate")] HmaMixtureAggregate, //F series
            [StringValue("PCC Concrete & Aggregate")] PccConcreteAggregate, //A series
            [StringValue("Soil & Base")] SoilBase //E series
        }

        //I15SB, I15NB, I15Tech, LAX
        public enum GridTabType
        {
            [StringValue("Field Revise")] FieldRevise,
            [StringValue("Lab Revise")] LabRevise,
            [StringValue("Field Supervisor")] FieldSupervisor,
            [StringValue("Lab Supervisor")] LabSupervisor,
            [StringValue("Authorization")] Authorization,
            [StringValue("Pending Closing")] PendingClosing
        }

        //I15SB, I15NB, I15Tech, LAX
        public enum ColumnNameType
        {
            [StringValue("LIN")] LIN,
            [StringValue("LabId")] LabId,
            [StringValue("SampleBy")] SampleBy,
            [StringValue("SampleDate")] SampleDate,
            [StringValue("TestNames")] TestMethod,
            [StringValue("RevisedDate")] RevisedDate,
            [StringValue("RevisedBy")] RevisedBy
        }

        //I15SB, I15NB, I15Tech, LAX
        public enum TestType
        {
            None, //SH249 & SG
            [StringValue("Single Sublot")] SingleSublot,
            [StringValue("Multi Sublot")] MultiSublot
        }

        //I15SB, I15NB, I15Tech, LAX
        public enum CreateType
        {
            New,
            Revision,
            Retest
        }

        //All Tenants
        public enum WorkflowType
        {
            [StringValue("A1")] A1,
            [StringValue("A2")] A2,
            [StringValue("E1")] E1,
            [StringValue("E2")] E2,
            [StringValue("E3")] E3,
            [StringValue("F1")] F1,
            [StringValue("F2")] F2,
            [StringValue("F3")] F3
        }

        public enum A_TestMethodType
        {
            [StringValue("AASHTO-T11")] AASHTO_T11,
            [StringValue("AASHTO-T27")] AASHTO_T27,
            [StringValue("DB000")] DB000,
            [StringValue("ASTM-C31")] ASTM_C31,
            [StringValue("ASTM-C39")] ASTM_C39,
            [StringValue("AASHTO-T111")] AASHTO_T111,
            [StringValue("ASTM-C117")] ASTM_C117,
            [StringValue("ASTM-C136")] ASTM_C136,
            [StringValue("ASTM-D6913")] ASTM_D6913,
            [StringValue("T176")] T176,
            [StringValue("Tex-0000")] Tex_0000,
            [StringValue("Tex-203-F")] Tex_203_F,
            [StringValue("Tex-401-A")] Tex_401_A,
            [StringValue("Tex-402-A")] Tex_402_A,
            [StringValue("Tex-406-A")] Tex_406_A,
            [StringValue("Tex-408-A")] Tex_408_A,
            [StringValue("Tex-413-A")] Tex_413_A,
            [StringValue("Tex-418 Truck Log")] Tex_418_TruckLog,
            [StringValue("Tex-418-A")] Tex_418_A,
            [StringValue("Tex-423-A")] Tex_423_A,
            [StringValue("Tex-423II-A")] Tex_423II_A,
            [StringValue("Tex-424-A")] Tex_424_A,
            [StringValue("Tex-436-A")] Tex_436_A,
            [StringValue("Tex-460-A")] Tex_460_A,
            [StringValue("Tex-418DM-A")] Tex_418DM_A
        }

        public enum E_TestMethodType
        {
            [StringValue("AASHTO-T11")] AASHTO_T11,
            [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
            [StringValue("AASHTO-T27")] AASHTO_T27,
            [StringValue("AASHTO-T288")] AASHTO_T288,
            [StringValue("AASHTO-T289")] AASHTO_T289,
            [StringValue("ASTM-C117")] ASTM_C117,
            [StringValue("ASTM-C136")] ASTM_C136,
            [StringValue("ASTM-D2216")] ASTM_D2216,
            [StringValue("ASTM-D4318")] ASTM_D4318,
            [StringValue("ASTM-D6913")] ASTM_D6913,
            [StringValue("CT226")] CT226,
            [StringValue("DB000")] DB000,
            [StringValue("T255/T265")] T255_T265,
            [StringValue("T290")] T290,
            [StringValue("T89/T90")] T89_T90,
            [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
            [StringValue("AASHTO-M145-S")] AASHTO_M145_S,
            [StringValue("ASTM-C136 - Standard Test Method for Sieve Analysis of Fine and Coarse Aggregates (Split Sieve)")] ASTM_C136_SplitSieve,
            [StringValue("CT202")] CT202,
            [StringValue("ASTM-D2419-14")] ASTM_D2419_14,
            [StringValue("CT217")] CT217,
            [StringValue("T176")] T176,
            [StringValue("AASHTO-T99/T180")] AASHTO_T99_T180,
            [StringValue("ASTM-D1557")] ASTM_D1557,
            [StringValue("ASTM-D4718")] ASTM_D4718,
            [StringValue("ASTM-D6951")] ASTM_D6951,
            [StringValue("Tex-0000")] Tex_0000,
            [StringValue("Tex-101-E-Part-III")] Tex_101_E_Part_III,
            [StringValue("Tex-103-E")] Tex_103_E,
            [StringValue("Tex-104/105/106-E")] Tex_104_105_106_E,
            [StringValue("Tex-107-E")] Tex_107_E,
            [StringValue("Tex-110-E")] Tex_110_E,
            [StringValue("Tex-111-E")] Tex_111_E,
            [StringValue("Tex-115-E")] Tex_115_E,
            [StringValue("Tex-116-E")] Tex_116_E,
            [StringValue("Tex-117-E")] Tex_117_E,
            [StringValue("Tex-128-E")] Tex_128_E,
            [StringValue("Tex-129-E")] Tex_129_E,
            [StringValue("Tex-140-E")] Tex_140_E,
            [StringValue("Tex-145-E")] Tex_145_E,
            [StringValue("Tex-148-E")] Tex_148_E,
            [StringValue("Tex-124-E")] Tex_124_E,
            [StringValue("Tex-203-F")] Tex_203_F,
            [StringValue("Tex-113-E")] Tex_113_E,
            [StringValue("Tex-114-E")] Tex_114_E,
            [StringValue("Tex-120-E")] Tex_120_E,
            [StringValue("Tex-121-E")] Tex_121_E,
            [StringValue("AASHTO-T310")] AASHTO_T310

        }

        public enum F_TestMethodType
        {
            [StringValue("AASHTO-T329")] AASHTO_T329,
            [StringValue("DB000")] DB000,
            [StringValue("Tex-0000")] Tex_0000,
            [StringValue("Tex-200/342-F")] Tex_200_342_F,
            [StringValue("Tex-200-F")] Tex_200_F,
            [StringValue("Tex-207-F")] Tex_207_F,
            [StringValue("Tex-227-F")] Tex_227_F,
            [StringValue("Tex-228-F")] Tex_228_F,
            [StringValue("Tex-236-F")] Tex_236_F
        }

        #region clickMethods
        public override void ClickBtn_CreateNew()
            => ClickElementByID(ButtonType.CreateNew);

        public override void ClickBtn_CreateRetest()
            => ClickElementByID(ButtonType.CreateRetest);

        public override void ClickBtn_CreateRevision()
            => ClickElementByID(ButtonType.CreateRevision);

        public override void SelectTab_Authorization()
            => GridHelper.ClickTab(GridTabType.Authorization);

        public override void SelectTab_FieldRevise()
            => GridHelper.ClickTab(GridTabType.FieldRevise);

        public override void SelectTab_FieldSupervisor()
            => GridHelper.ClickTab(GridTabType.FieldSupervisor);

        public override void SelectTab_LabRevise()
            => GridHelper.ClickTab(GridTabType.LabRevise);

        public override void SelectTab_LabSupervisor()
            => GridHelper.ClickTab(GridTabType.LabSupervisor);

        public override void SelectTab_PendingClosing()
            => GridHelper.ClickTab(GridTabType.PendingClosing);

        public override void ClickBtn_Cancel()
            => ClickElementByID(ButtonType.Cancel);

        public override void ClickBtn_Continue()
            => ClickElementByID(ButtonType.Continue);

        public override void ClickBtn_Save()
            => ClickElementByID(ButtonType.Save);

        public override void ClickBtn_SaveEdit()
            => ClickElementByID(ButtonType.SaveEdit);

        private readonly string availableTestMethodModalDivXPath = "//div[@id='AvailableTestsWindow']/parent::div[contains(@style, 'display: block')]";
        private By AvailableTestMethodModalSubmitBtnXPath(string buttonName)
            => By.XPath($"{availableTestMethodModalDivXPath}//button[text()='{buttonName}']");

        public override void ClickModalBtn_Save()
        {
            ClickElement(AvailableTestMethodModalSubmitBtnXPath("Save"));
            WaitForAvailableTestsModalToClear();
        }

        public override void ClickModalBtn_Cancel()
            => ClickElement(AvailableTestMethodModalSubmitBtnXPath("Cancel"));

        public override void ClickModalBtn_Close()
            => ClickElement(By.XPath($"{availableTestMethodModalDivXPath}//a[@aria-label='Close']"));
        #endregion

        public override void PopulateFieldAndUpdateKVPairsList()
        {
            throw new System.NotImplementedException();
        }

        //I15SB, 
        public override IList<TestDetails_InputFieldType> GetRequiredInputFieldTypeList()
            => RequiredInputFieldTypeList;

        public override IList<TestDetails_InputFieldType> GetAllInputFieldTypeList()
            => AllInputFieldTypeList;

        public override IList<KeyValuePair<TestDetails_InputFieldType, string>> GetTestRecordKVPairsList()
        {
            if(testRecordKVPairsList == null)
            {
                testRecordKVPairsList = new List<KeyValuePair<TestDetails_InputFieldType, string>>();
            }

            return testRecordKVPairsList;
        }

        //SH249 & SG
        internal void SelectMaterialCategory(WorkflowType workflowType)
        {
            MaterialCategoryType materialCategory = MaterialCategoryType.HmaMixtureAggregate;

            string testSeries = workflowType.GetString();

            if (testSeries.Contains("A"))
            {
                materialCategory = MaterialCategoryType.PccConcreteAggregate;
            }
            else if (testSeries.Contains("E"))
            {
                materialCategory = MaterialCategoryType.SoilBase;
            }

            ExpandAndSelectFromDDList(CreateTest_InputFieldType.MaterialCategory, materialCategory.GetString(), true);
        }

        //I15SB, I15NB, I15Tech, LAX
        internal void ClickCreateButton(CreateType createType)
        {
            switch (createType)
            {
                case CreateType.New:
                    ClickBtn_CreateNew();
                    break;
                case CreateType.Revision:
                    ClickBtn_CreateRevision();
                    break;
                case CreateType.Retest:
                    ClickBtn_CreateRetest();
                    break;
            }
        }

        //I15SB, I15NB, I15Tech, LAX
        public override void CreateWorkflowType(CreateType createType, WorkflowType workflowType, TestType testType = TestType.SingleSublot)
        {
            NavigateToPage.QARecordControl_QA_Test_All();

            if (testType == TestType.MultiSublot)
            {
                ExpandAndSelectFromDDList(CreateTest_InputFieldType.TestType, testType.GetString());
            }

            ExpandAndSelectFromDDList(CreateTest_InputFieldType.WorkflowType, workflowType.GetString(), true);
            ClickCreateButton(createType);
        }


        public override void CreateNewTestRecord(WorkflowType workflowType, TestType testType = TestType.SingleSublot)
        {
            try
            {
                CreateWorkflowType(CreateType.New, workflowType, testType);
            }
            catch (Exception e)
            {
                Report.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }
        }

        public override void CreateRevisionTestRecord()
        {
            try
            {
                //TODO
            }
            catch (Exception e)
            {
                Report.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }
        }

        public override void CreateRetestTestRecord()
        {
            try
            {
                //TODO
            }
            catch (Exception e)
            {
                Report.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }
        }

        public override void ClickBtn_AddRemoveTestMethods()
            => PageAction.ClickElementByID(ButtonType.AddRemoveTestMethods);

        int LastKnownSequenceNumber { get; set; }

        public override void CheckForLINError()
        {
            By sequenceNumberLocator = By.XPath("//input[@id='SequenceNumber']");
            By errorLINExists = By.XPath("//span[contains(@class, 'ValidationErrorMessage')][contains(text(), 'LIN exists')]");
            bool errorLINExistsIsDisplayed = true;
            string sequenceNumber = string.Empty;

            do
            {
                if (LastKnownSequenceNumber > 1)
                {
                    LastKnownSequenceNumber = LastKnownSequenceNumber + 1;

                    if (LastKnownSequenceNumber < 10)
                    {
                        sequenceNumber = $"0{LastKnownSequenceNumber}";
                    }
                    else
                    {
                        sequenceNumber = LastKnownSequenceNumber.ToString();
                    }
                }
                else
                {
                    GetElement(errorLINExists);

                    sequenceNumber = GetText(sequenceNumberLocator, logReport: false);
                    int newValue = int.Parse(sequenceNumber) + 1;

                    if (newValue < 10)
                    {
                        sequenceNumber = $"0{newValue}";
                    }
                    else
                    {
                        sequenceNumber = newValue.ToString();
                    }
                }

                LastKnownSequenceNumber = int.Parse(sequenceNumber);

                EnterText(sequenceNumberLocator, sequenceNumber);
                QATestMethod.ClickBtn_Save();

                try
                {
                    GetElement(errorLINExists);
                    LastKnownSequenceNumber = int.Parse(GetText(sequenceNumberLocator, logReport: false));
                }
                catch (NoSuchElementException)
                {
                    errorLINExistsIsDisplayed = false;
                }

            } while (errorLINExistsIsDisplayed);

            throw new NoSuchElementException();
        }

        public override void AddTestMethod<T>(T AvailableTestMethodType)
        {
            try
            {
                IList<string> testMethodList = new List<string>();

                if (AvailableTestMethodType is Enum)
                {
                    testMethodList.Add(ConvertToType<Enum>(AvailableTestMethodType).GetString());
                }
                else if (AvailableTestMethodType.GetType().Equals(typeof(List<Enum>)))
                {
                    var testMethodEnumList = ConvertToType<List<Enum>>(AvailableTestMethodType);

                    foreach (var enumItem in testMethodEnumList)
                    {
                        testMethodList.Add(ConvertToType<Enum>(enumItem).GetString());
                    }
                }
                else if (AvailableTestMethodType.GetType().Equals(typeof(string)))
                {
                    testMethodList.Add(ConvertToType<string>(AvailableTestMethodType));
                }
                else if (AvailableTestMethodType.GetType().Equals(typeof(List<string>)))
                {
                    ((List<string>)testMethodList).AddRange(ConvertToType<List<string>>(AvailableTestMethodType));
                }

                foreach (string testMethod in testMethodList)
                {
                    try
                    {
                        ToggleTestMethodCheckbox(testMethod);
                    }
                    catch (NoSuchElementException)
                    {
                        Report.Debug($"Unable to locator TestMethod {testMethod} in Available Test List");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void WaitForAvailableTestsModalToClear()
        {
            bool modalIsDisplayed = true;

            do
            {
                try
                {
                    IWebElement modalWindow = driver.FindElement(By.XPath(availableTestMethodModalDivXPath));
                    modalIsDisplayed = modalWindow.Displayed;
                    Thread.Sleep(2000);
                }
                catch (NoSuchElementException)
                {
                    modalIsDisplayed = false;
                }
            }
            while (modalIsDisplayed);
        }

        public override void GatherTestMethodInputFieldAttributeDetails<T>(T testMethodIdentifier, string workflowType)
        {
            Type argType = testMethodIdentifier.GetType();

            IList<string> testMethodIdentifiersList = new List<string>();

            if (testMethodIdentifier is Enum)
            {
                testMethodIdentifiersList.Add(ConvertToType<Enum>(testMethodIdentifier).GetString());
            }
            else if (argType.Equals(typeof(List<Enum>)))
            {
                var enumList = ConvertToType<List<Enum>>(testMethodIdentifier);

                foreach (Enum item in enumList)
                {
                    testMethodIdentifiersList.Add(item.GetString());
                }
            }
            else if (argType.Equals(typeof(string)))
            {
                testMethodIdentifiersList.Add(ConvertToType<string>(testMethodIdentifier));
            }
            else if (argType.Equals(typeof(List<string>)))
            {
                ((List<string>)testMethodIdentifiersList).AddRange(ConvertToType<List<string>>(testMethodIdentifier));
            }
            else
            {
                log.Error($"Unsupported parameter type : {argType}");
            }

            foreach (string identifier in testMethodIdentifiersList)
            {
                string testMethodIdentifierInputDivXPath = string.Empty;
                IList<IWebElement> textareaFieldElementsList = null;
                IList<IWebElement> inputFieldElementsList = null;
                IList<IWebElement> checkboxFieldElementsList = null;
                IList<IWebElement> textboxFieldElementsList = null;

                testMethodIdentifierInputDivXPath = $"//input[contains(@id, 'TestMethod_DisplayName')][@value='{identifier}']";
                string testMethodTestFormDivXPath = $"{testMethodIdentifierInputDivXPath}//ancestor::div[contains(@class, 'ElvisTestForm')]";
                string headerDescriptionXPath = $"{testMethodTestFormDivXPath}//span[contains(@id, 'header-description')]";
                string testMethodContainerFluidDivXPath = $"{testMethodTestFormDivXPath}//div[@class='container-fluid']";

                string testMethodDivHeader = GetText(By.XPath(headerDescriptionXPath), logReport: false);

                string logMsg = string.Empty;
                logMsg = $"\n============ BEGINING of TestMethod HEADER: ({workflowType}) {testMethodDivHeader} ============";
                Console.WriteLine(logMsg);
                Report.Info(logMsg, ExtentColor.Yellow, false);

                textboxFieldElementsList = GetElements(By.XPath($"{testMethodContainerFluidDivXPath}//input[contains(@class, 'k-textbox')]"));
                if (textboxFieldElementsList.Any())
                {
                    foreach (IWebElement textboxFieldElem in textboxFieldElementsList)
                    {
                        try
                        {
                            string textboxFieldId = textboxFieldElem.GetAttribute("id");
                            Console.WriteLine($">>>> TEXTBOX ID : {textboxFieldId}");
                            string textboxFieldLabel = string.Empty;

                            try
                            {
                                textboxFieldLabel = GetText(By.XPath($"//input[@id='{textboxFieldId}']/preceding-sibling::label"), logReport: false);
                            }
                            catch (NoSuchElementException)
                            {
                                textboxFieldLabel = GetText(By.XPath($"//input[@id='{textboxFieldId}']/parent::div/preceding-sibling::label"), logReport: false);
                            }
                            Console.WriteLine($">>>> TEXTBOX LABEL : {textboxFieldLabel}");
                            Report.Info($"TEXTBOX Attributes:<br> -- LABEL : {textboxFieldLabel}<br> -- ID : {textboxFieldId}", ExtentColor.Blue, false);
                        }
                        catch (Exception e)
                        {
                            log.Error($"{e.Message}\n{e.StackTrace}");
                        }

                        Console.WriteLine("#########################################\n");
                    }
                }

                checkboxFieldElementsList = GetElements(By.XPath($"{testMethodContainerFluidDivXPath}//input[@class='k-checkbox']"));
                if (checkboxFieldElementsList.Any())
                {
                    foreach (IWebElement checkboxFieldElem in checkboxFieldElementsList)
                    {
                        try
                        {
                            string checkboxFieldId = checkboxFieldElem.GetAttribute("id");
                            Console.WriteLine($">>>> CHECKBOX ID : {checkboxFieldId}");
                            string checkboxFieldLabel = GetText(By.XPath($"//input[@id='{checkboxFieldId}']/following-sibling::label"), logReport: false);
                            Console.WriteLine($">>>> CHECKBOX LABEL : {checkboxFieldLabel}");
                            Report.Info($"CHECKBOX Attributes:<br> -- LABEL : {checkboxFieldLabel}<br> -- ID : {checkboxFieldId}", ExtentColor.Blue, false);
                        }
                        catch (Exception e)
                        {
                            log.Error($"{e.Message}\n{e.StackTrace}");
                        }

                        Console.WriteLine("#########################################\n");
                    }
                }

                inputFieldElementsList = GetElements(By.XPath($"{testMethodContainerFluidDivXPath}//input[@data-role]"));
                if (inputFieldElementsList.Any())
                {
                    foreach (IWebElement inputFieldElem in inputFieldElementsList)
                    {
                        try
                        {
                            string inputFieldId = inputFieldElem.GetAttribute("id");
                            Console.WriteLine($">>>> INPUT FIELD ID : {inputFieldId}");
                            string inputFieldDataRole = inputFieldElem.GetAttribute("data-role");
                            Console.WriteLine($">>>> INPUT FIELD DATA-ROLE : {inputFieldDataRole}");

                            string labelParentXPath = "/parent::span";

                            if (inputFieldDataRole.Equals("dropdownlist"))
                            {
                                labelParentXPath = string.Empty;
                            }

                            string inputFieldLabel = string.Empty;

                            By siblingLabelLocator = By.XPath($"//input[@id='{inputFieldId}']/parent::span{labelParentXPath}/preceding-sibling::label");
                            By siblingLabelWithParentDivLocator = By.XPath($"//input[@id='{inputFieldId}']/parent::span{labelParentXPath}/parent::div/preceding-sibling::label");
                            
                            try
                            {
                                IWebElement inputFieldLabelElem = driver.FindElement(siblingLabelLocator)
                                ?? driver.FindElement(siblingLabelWithParentDivLocator);
                                inputFieldLabel = inputFieldLabelElem.Text;

                                //inputFieldLabel = GetText(By.XPath($"//input[@id='{inputFieldId}']/parent::span{labelParentXPath}/preceding-sibling::label"), logReport: false);
                            }
                            catch (NoSuchElementException)
                            {
                                //try
                                //{
                                //    inputFieldLabel = GetText(By.XPath($"//input[@id='{inputFieldId}']/parent::span{labelParentXPath}/parent::div/preceding-sibling::label"), logReport: false);
                                //}
                                //catch (NoSuchElementException)
                                //{
                                //    inputFieldLabel = "No Direct Parent Label";
                                //}

                                inputFieldLabel = "No Direct Parent Label";
                            }
                            Console.WriteLine($">>>> INPUT FIELD LABEL : {inputFieldLabel}");
                            Report.Info($"INPUT FIELD Attributes:<br> -- LABEL : {inputFieldLabel}<br> -- ID : {inputFieldId}<br> -- DATA-ROLE : {inputFieldDataRole}", ExtentColor.Blue, false);
                        }
                        catch (Exception e)
                        {
                            log.Error($"{e.Message}\n{e.StackTrace}");
                        }

                        Console.WriteLine("#########################################\n");
                    }
                }

                textareaFieldElementsList = GetElements(By.XPath($"{testMethodContainerFluidDivXPath}//textarea"));
                if (textareaFieldElementsList.Any())
                {
                    foreach (IWebElement textareaFieldElem in textareaFieldElementsList)
                    {
                        try
                        {
                            string textareaFieldId = textareaFieldElem.GetAttribute("id");
                            Console.WriteLine($">>>> TEXTAREA FIELD ID : {textareaFieldId}");
                            string textareaFieldLabel = GetText(By.XPath($"//textarea[@id='{textareaFieldId}']/preceding-sibling::label"), logReport: false);
                            Console.WriteLine($">>>> TEXTAREA FIELD LABEL : {textareaFieldLabel}");
                            Report.Info($"TEXTAREA FIELD Attributes:<br> -- LABEL : {textareaFieldLabel}<br> -- ID : {textareaFieldId}", ExtentColor.Blue, false);
                        }
                        catch (NoSuchElementException)
                        {
                            log.Error($"!!!!!!!!!! NoSuchElementException FOR {textareaFieldElem} !!!!!!!!!!");
                        }

                        Console.WriteLine("#########################################\n");
                    }
                }

                logMsg = $"============ END of TestMethod HEADER: ({workflowType}) {testMethodDivHeader} ============\n";               
                Console.WriteLine(logMsg);
                Report.Info(logMsg, ExtentColor.Yellow, false);
            }
        }
    }

    public abstract class QATestAll : PageBase, IQATestAll
    {
        public abstract IList<TestDetails_InputFieldType> AllInputFieldTypeList { get; set; }
        public abstract IList<TestDetails_InputFieldType> RequiredInputFieldTypeList { get; set; }

        public abstract void ClickBtn_Cancel();
        public abstract void ClickBtn_CreateNew();
        public abstract void ClickBtn_CreateRetest();
        public abstract void ClickBtn_CreateRevision();
        public abstract void ClickBtn_Continue();
        public abstract void ClickBtn_Save();
        public abstract void ClickBtn_SaveEdit();
        public abstract IList<TestDetails_InputFieldType> GetRequiredInputFieldTypeList();
        public abstract IList<TestDetails_InputFieldType> GetAllInputFieldTypeList();
        public abstract void PopulateFieldAndUpdateKVPairsList();
        public abstract void SelectTab_Authorization();
        public abstract void SelectTab_FieldRevise();
        public abstract void SelectTab_FieldSupervisor();
        public abstract void SelectTab_LabRevise();
        public abstract void SelectTab_LabSupervisor();
        public abstract void SelectTab_PendingClosing();
        public abstract IList<KeyValuePair<TestDetails_InputFieldType, string>> GetTestRecordKVPairsList();
        public abstract void CreateWorkflowType(CreateType createType, WorkflowType workflowType, TestType testType = TestType.SingleSublot);
        public abstract void CreateNewTestRecord(WorkflowType workflowType, TestType testType = TestType.SingleSublot);
        public abstract void CreateRevisionTestRecord();
        public abstract void CreateRetestTestRecord();
        public abstract void ClickBtn_AddRemoveTestMethods();
        public abstract void CheckForLINError();
        public abstract void AddTestMethod<T>(T AvailableTestMethodType);
        public abstract void GatherTestMethodInputFieldAttributeDetails<T>(T testMethodIdentifier, string workflowType);
        public abstract void ClickModalBtn_Save();
        public abstract void ClickModalBtn_Cancel();
        public abstract void ClickModalBtn_Close();
    }


    public class QATestAll_SH249 : QATestAll_Common
    {
        public QATestAll_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override void ClickBtn_CreateNew()
            => ClickBtn_Continue();

        public override void ClickBtn_CreateRetest()
            => ClickElement(By.XPath("//input[@value='Create Revision']"));

        public override void ClickBtn_CreateRevision()
            => ClickElement(By.XPath("//input[@value='Create Retest']"));

        public override void CreateWorkflowType(CreateType createType, WorkflowType workflowType, TestType testType = TestType.SingleSublot)
        {
            switch (createType)
            {
                case CreateType.New:
                    NavigateToPage.QARecordControl_QA_Test_Original_Report();
                    SelectMaterialCategory(workflowType);
                    ExpandAndSelectFromDDList(CreateTest_InputFieldType.WorkflowType, workflowType.GetString(), true);
                    break;
                case CreateType.Revision:
                    NavigateToPage.QARecordControl_QA_Test_Correction_Report();
                    //Enter Original LIN
                    break;
                case CreateType.Retest:
                    NavigateToPage.QARecordControl_QA_Test_Retest_Report();
                    //Enter Retested LIN
                    //Enter New LIN
                    break;
            }

            ClickCreateButton(createType);
        }

        public override void CheckForLINError()
        {
            bool errorLINExistsIsDisplayed = true;
            string LinNumber = string.Empty;

            LinNumber = GenerateLIN();

            try
            {
                GetElement(errorLINRequired);
                EnterText(LinLocator, LinNumber);
                QATestMethod.ClickBtn_Save();
            }
            catch (NoSuchElementException)
            {
                LinNumber = GetText(LinLocator, logReport: false);
            }

            do
            {
                try
                {
                    GetElement(errorLINExists);
                    LinNumber = GetText(LinLocator, logReport: false);
                    LinNumber = (long.Parse(LinNumber) + 1).ToString();
                    EnterText(LinLocator, LinNumber);
                    QATestMethod.ClickBtn_Save();
                    GetElement(errorLINExists);
                }
                catch (NoSuchElementException)
                {
                    errorLINExistsIsDisplayed = false;
                    throw;
                }

            } while (errorLINExistsIsDisplayed);

            throw new NoSuchElementException();
        }
    }

    public class QATestAll_SGWay : QATestAll_Common
    {
        public QATestAll_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override void ClickBtn_CreateNew()
            => ClickElementByID(ButtonType.Continue);

        public override void ClickBtn_CreateRetest()
            => ClickElement(By.XPath("//input[@value='Create Revision']"));

        public override void ClickBtn_CreateRevision()
            => ClickElement(By.XPath("//input[@value='Create Retest']"));

        public override void CreateWorkflowType(CreateType createType, WorkflowType workflowType, TestType testType = TestType.SingleSublot)
        {
            switch (createType)
            {
                case CreateType.New:
                    NavigateToPage.QARecordControl_QA_Test_Original_Report();
                    SelectMaterialCategory(workflowType);
                    ExpandAndSelectFromDDList(CreateTest_InputFieldType.WorkflowType, workflowType.GetString(), true);
                    break;
                case CreateType.Revision:
                    NavigateToPage.QARecordControl_QA_Test_Correction_Report();
                    //Enter Original LIN
                    break;
                case CreateType.Retest:
                    NavigateToPage.QARecordControl_QA_Test_Retest_Report();
                    //Enter Retested LIN
                    //Enter New LIN
                    break;
            }

            ClickCreateButton(createType);
        }

        public override void CheckForLINError()
        {
            bool errorLINExistsIsDisplayed = true;
            string LinNumber = string.Empty;
            LinNumber = GenerateLIN();

            try
            {
                GetElement(errorLINRequired);
                EnterText(LinLocator, LinNumber);
                QATestMethod.ClickBtn_Save();
            }
            catch (NoSuchElementException)
            {
                LinNumber = GetText(LinLocator, logReport: false);
            }

            do
            {
                try
                {
                    GetElement(errorLINExists);
                    LinNumber = GetText(LinLocator, logReport: false);
                    LinNumber = (long.Parse(LinNumber) + 1).ToString();
                    EnterText(LinLocator, LinNumber);
                    QATestMethod.ClickBtn_Save();
                    GetElement(errorLINExists);
                }
                catch (NoSuchElementException)
                {
                    errorLINExistsIsDisplayed = false;
                }

            } while (errorLINExistsIsDisplayed);

            throw new NoSuchElementException();
        }
    }

    public class QATestAll_LAX : QATestAll_Common
    {
        public QATestAll_LAX(IWebDriver driver) : base(driver)
        {
        }


    }

    public class QATestAll_I15North : QATestAll_Common
    {
        public QATestAll_I15North(IWebDriver driver) : base(driver)
        {
        }

        public override IList<TestDetails_InputFieldType> RequiredInputFieldTypeList
            => new List<TestDetails_InputFieldType>()
            {
                TestDetails_InputFieldType.MaterialCode,
                TestDetails_InputFieldType.GradeClassType,
                TestDetails_InputFieldType.Supplier,
                TestDetails_InputFieldType.Segment,
                TestDetails_InputFieldType.Roadway,
                TestDetails_InputFieldType.Feature,
                TestDetails_InputFieldType.Structure,
                TestDetails_InputFieldType.SampleType
            };

        public override IList<TestDetails_InputFieldType> AllInputFieldTypeList
            => new List<TestDetails_InputFieldType>()
            {
                TestDetails_InputFieldType.WorkflowType,
                TestDetails_InputFieldType.LIN,
                TestDetails_InputFieldType.LIN_SN,
                TestDetails_InputFieldType.Revision,
                TestDetails_InputFieldType.LabId,
                TestDetails_InputFieldType.ReportType,
                TestDetails_InputFieldType.SampleBy,
                TestDetails_InputFieldType.SampleDate,
                TestDetails_InputFieldType.MaterialCode,
            };

    }

    public class QATestAll_I15South : QATestAll_Common
    {
        public QATestAll_I15South(IWebDriver driver) : base(driver)
        {
        }

        public override IList<TestDetails_InputFieldType> RequiredInputFieldTypeList
            => new List<TestDetails_InputFieldType>()
            {
                TestDetails_InputFieldType.MaterialCode,
                TestDetails_InputFieldType.GradeClassType,
                TestDetails_InputFieldType.Supplier,
                TestDetails_InputFieldType.Segment,
                TestDetails_InputFieldType.Roadway,
                TestDetails_InputFieldType.Feature,
                TestDetails_InputFieldType.Structure,
                TestDetails_InputFieldType.SampleType
            };

        public override IList<TestDetails_InputFieldType> AllInputFieldTypeList
            => new List<TestDetails_InputFieldType>()
            {
                TestDetails_InputFieldType.WorkflowType,
                TestDetails_InputFieldType.LIN,
                TestDetails_InputFieldType.LIN_SN,
                TestDetails_InputFieldType.Revision,
                TestDetails_InputFieldType.LabId,
                TestDetails_InputFieldType.ReportType,
                TestDetails_InputFieldType.SampleBy,
                TestDetails_InputFieldType.SampleDate,
                TestDetails_InputFieldType.MaterialCode,
            };

    }

    public class QATestAll_I15Tech : QATestAll_Common
    {
        public QATestAll_I15Tech(IWebDriver driver) : base(driver)
        {
        }

        public override IList<TestDetails_InputFieldType> RequiredInputFieldTypeList
            => new List<TestDetails_InputFieldType>()
            {
                TestDetails_InputFieldType.MaterialCode,
                TestDetails_InputFieldType.GradeClassType,
                TestDetails_InputFieldType.Supplier,
                TestDetails_InputFieldType.Segment,
                TestDetails_InputFieldType.Roadway,
                TestDetails_InputFieldType.Feature,
                TestDetails_InputFieldType.Structure,
                TestDetails_InputFieldType.SampleType
            };

        public override IList<TestDetails_InputFieldType> AllInputFieldTypeList
            => new List<TestDetails_InputFieldType>()
            {
                TestDetails_InputFieldType.WorkflowType,
                TestDetails_InputFieldType.LIN,
                TestDetails_InputFieldType.LIN_SN,
                TestDetails_InputFieldType.Revision,
                TestDetails_InputFieldType.LabId,
                TestDetails_InputFieldType.ReportType,
                TestDetails_InputFieldType.SampleBy,
                TestDetails_InputFieldType.SampleDate,
                TestDetails_InputFieldType.MaterialCode,
            };

    }
}