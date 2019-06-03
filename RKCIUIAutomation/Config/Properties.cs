using RKCIUIAutomation.Page;
using RKCIUIAutomation.Test;
using System;

namespace RKCIUIAutomation.Config
{
    public enum TestEnv
    {
        [StringValue("Dev")] Dev,
        [StringValue("Test")] Testing,
        [StringValue("Stage")] Staging,
        [StringValue("Prod")] Production
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
        [StringValue("AT_Ncr Mgr")] NCRMgr,
        [StringValue("AT_Ncr Tech")] NCRTech,
        [StringValue("AT_Ncr DevConcur")] NCRDevConcur,
        [StringValue("AT_Ncr LawaConcur")] NCRLawaConcur,
        [StringValue("AT_Ncr Qpm")] NCRQpm,
        [StringValue("AT_Ncr Cqam")] NCRCqam,
        [StringValue("AT_Ncr Omqm")] NCROmqm,
        [StringValue("CDR Manager")] CDRMgr,
        [StringValue("AT_Cdr Tech")] CDRTech,
        [StringValue("AT_Dir Mgr")] DIRMgrQA,
        [StringValue("AT_Dir Tech")] DIRTechQA,
        [StringValue("AT_Dir Mgr QC")] DIRMgrQC,
        [StringValue("AT_Dir Tech QC")] DIRTechQC,
        [StringValue("AT_CR Create")] CR_Create,
        [StringValue("AT_CR Comment")] CR_Comment,
        [StringValue("AT_CR Comment Admin")] CR_CommentAdmin,
        [StringValue("AT_CR Response")] CR_Response,
        [StringValue("AT_CR Response Admin")] CR_ResponseAdmin,
        [StringValue("AT_CR Verify")] CR_Verify,
        [StringValue("AT_CR Verify Admin")] CR_VerifyAdmin,
        [StringValue("AT_Transmissions General")] TransmissionsGeneral
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