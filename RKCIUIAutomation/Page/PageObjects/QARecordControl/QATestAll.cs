using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        internal void ToggleTestMethodCheckbox(Enum testMethodIdentifier)
        {
            string testMethodInputElemId = string.Empty;
            By chkbxLocator = By.XPath($"//input[@identifier='{testMethodIdentifier}']");
            testMethodInputElemId = GetAttribute(chkbxLocator, "id");
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

        public override void CheckForLINError()
        {
            By errorLINExists = By.XPath("//span[contains(@class, 'ValidationErrorMessage')][contains(text(), 'LIN exists')]");
            bool errorLINExistsIsDisplayed = true;

            do
            {
                GetElement(errorLINExists);

                By sequenceNumberLocator = By.XPath("//input[@id='SequenceNumber']");
                string sequenceNumber = GetText(sequenceNumberLocator, logReport: false);
                int newValue = int.Parse(sequenceNumber) + 1;

                if (newValue < 10)
                {
                    sequenceNumber = $"0{newValue}";
                }
                else
                {
                    sequenceNumber = newValue.ToString();
                }

                EnterText(sequenceNumberLocator, sequenceNumber);
                QATestMethod.ClickBtn_Save();
                try
                {
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