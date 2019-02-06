using RKCIUIAutomation.Page;
using RKCIUIAutomation.Test;
using System;

namespace RKCIUIAutomation.Config
{
    public enum TestEnv
    {
        Test,
        Stage,
        Prod
    }

    public enum TenantName
    {
        Garnet,
        GLX,
        I15South,
        I15Tech,
        LAX,
        SH249,
        SGWay,
    }

    public enum TestPlatform
    {
        Grid,
        GridLocal,
        Windows,
        Mac,
        Android,
        IOS,
        Local,
        Linux
    }

    public enum BrowserType
    {
        Chrome,
        Firefox,
        MicrosoftEdge,
        Safari
    }

    public enum UserType
    {
        [StringValue("Bhoomi")]Bhoomi,
        [StringValue("IQF Records Manager")] IQFRecordsMgr,
        [StringValue("IQF User")] IQFUser,
        [StringValue("IQF Admin")] IQFAdmin,
        [StringValue("DOT User")] DOTUser,
        [StringValue("DOT Admin")] DOTAdmin,
        [StringValue("DEV User")] DEVUser,
        [StringValue("DEV Admin")] DEVAdmin,
        [StringValue("NCR Manager")] NCRMgr,
        [StringValue("NCR Technician")] NCRTech,
        [StringValue("CDR Manager")] CDRMgr,
        [StringValue("CDR Technician")] CDRTech,
        [StringValue("AT_DIR Mgr")] DIRMgrQA,
        [StringValue("AT_DIR Tech")] DIRTechQA,
        [StringValue("AT_DIR Mgr QC")] DIRMgrQC,
        [StringValue("AT_DIR Tech QC")] DIRTechQC
    }

    public enum UserGroup
    {
        DirQA,
        DirQC
    }

    public enum Reporter
    {
        Html,
        Klov
    }
}