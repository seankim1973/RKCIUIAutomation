using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.TableHelper;

namespace RKCIUIAutomation.Page.PageObjects.QARecordControl
{
    public class QATestAll : QATestAll_Impl
    {
        public QATestAll()
        {
        }

        public QATestAll(IWebDriver driver) => this.Driver = driver;

        public override T SetClass<T>(IWebDriver driver)
        {
            IQATestAll instance = new QATestAll(driver);
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

        public enum NewTest_InputFieldType
        {
            [StringValue("SelectedTechName", DDL)] TechnicianName,
            [StringValue("sampleDate", DATE)] LINDate,
            [StringValue("SequenceNumber", TEXT)] SN,
            [StringValue("TestTypeId", DDL)] TestType,
            [StringValue("SelectedStatusFlowTypeId", DDL)] WorkflowType
        }

        public enum TestDetails_InputFieldType
        {
            [StringValue("StatusFlowName", AUTOPOPULATED)] WorkflowType,
            //[StringValue("")] LIN,
            [StringValue("SequenceNumber", AUTOPOPULATED_TEXT)] LIN_SN,
            [StringValue("Lot_MaterialId", DDL)] MaterialCode,
            [StringValue("Lot_GradeId", AUTOPOPULATED_DDL)] GrdeClassType,
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
            [StringValue("StationOffsetDirection_R")] RadioBtn_Station_Offset_R,
        }

        public enum ButtonType
        {
            [StringValue("CreateNew")] CreateNew,
            [StringValue("CreateCorrection")] CreateRevision,
            [StringValue("CreateRetest")] CreateRetest,
            [StringValue("CloseSelectedTest_PendingClose")] CloseSelected,
            [StringValue("ViewSelectedTest_PendingClose")] ViewSelected,
            [StringValue("CancelTest")] Cancel,
            [StringValue("SubmitTest")] Save,
            [StringValue("SubmitTestAndContinue")] SavedEdit,
        }

        public enum GridTabType
        {
            [StringValue("Field Revise")] FieldRevise,
            [StringValue("Lab Revise")] LabRevise,
            [StringValue("Field Supervisor")] FieldSupervisor,
            [StringValue("Lab Supervisor")] LabSupervisor,
            [StringValue("Authorization")] Authorization,
            [StringValue("Pending Closing")] PendingClosing
        }

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
        {
            throw new System.NotImplementedException();
        }

        public override void ClickBtn_Save()
        {
            throw new System.NotImplementedException();
        }

        public override void ClickBtn_SaveEdit()
        {
            throw new System.NotImplementedException();
        }

        public override void PopulateFieldAndUpdateKVPairsList()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IQATestAll
    {
        void ClickBtn_CreateNew();
        void ClickBtn_CreateRevision();
        void ClickBtn_CreateRetest();
        void ClickBtn_Cancel();
        void ClickBtn_Save();
        void ClickBtn_SaveEdit();

        void PopulateFieldAndUpdateKVPairsList();

        void SelectTab_FieldRevise();
        void SelectTab_LabRevise();
        void SelectTab_FieldSupervisor();
        void SelectTab_LabSupervisor();
        void SelectTab_Authorization();
        void SelectTab_PendingClosing();


    }

    public abstract class QATestAll_Impl : PageBase, IQATestAll
    {
        public abstract void ClickBtn_Cancel();
        public abstract void ClickBtn_CreateNew();
        public abstract void ClickBtn_CreateRetest();
        public abstract void ClickBtn_CreateRevision();
        public abstract void ClickBtn_Save();
        public abstract void ClickBtn_SaveEdit();
        public abstract void PopulateFieldAndUpdateKVPairsList();
        public abstract void SelectTab_Authorization();
        public abstract void SelectTab_FieldRevise();
        public abstract void SelectTab_FieldSupervisor();
        public abstract void SelectTab_LabRevise();
        public abstract void SelectTab_LabSupervisor();
        public abstract void SelectTab_PendingClosing();
    }

    public class QATestAll_LAX : QATestAll
    {
        public QATestAll_LAX(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QATestAll_SH249 : QATestAll
    {
        public QATestAll_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QATestAll_SGWay : QATestAll
    {
        public QATestAll_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QATestAll_I15North : QATestAll
    {
        public QATestAll_I15North(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QATestAll_I15South : QATestAll
    {
        public QATestAll_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    public class QATestAll_I15Tech : QATestAll
    {
        public QATestAll_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }
}