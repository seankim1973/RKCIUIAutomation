using System.Collections.Generic;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QATestAll_Common;

namespace RKCIUIAutomation.Page.PageObjects.QARecordControl
{
    public interface IQATestAll
    {
        IList<TestDetails_InputFieldType> AllInputFieldTypeList { get; set; }
        IList<TestDetails_InputFieldType> RequiredInputFieldTypeList { get; set; }

        void CreateNewTestRecord(WorkflowType workflowType, TestType testType = TestType.SingleSublot);
        void CreateRevisionTestRecord();
        void CreateRetestTestRecord();
        void ClickBtn_CreateNew();
        void ClickBtn_CreateRevision();
        void ClickBtn_CreateRetest();
        void ClickBtn_Cancel();
        void ClickBtn_Continue();
        void ClickBtn_Save();
        void ClickBtn_SaveEdit();

        IList<KeyValuePair<TestDetails_InputFieldType, string>> GetTestRecordKVPairsList();
        IList<TestDetails_InputFieldType> GetRequiredInputFieldTypeList();
        IList<TestDetails_InputFieldType> GetAllInputFieldTypeList();

        void CreateWorkflowType(CreateType createBtn, WorkflowType workflowType, TestType testType = TestType.SingleSublot);
        void PopulateFieldAndUpdateKVPairsList();

        void SelectTab_FieldRevise();
        void SelectTab_LabRevise();
        void SelectTab_FieldSupervisor();
        void SelectTab_LabSupervisor();
        void SelectTab_Authorization();
        void SelectTab_PendingClosing();
        void ClickBtn_AddRemoveTestMethods();
        void CheckForLINError();
    }
}