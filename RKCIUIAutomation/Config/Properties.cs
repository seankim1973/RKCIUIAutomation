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
        Bhoomi,
        IQFRecordsMgr,
        IQFUser,
        IQFAdmin,
        DOTUser,
        DOTAdmin,
        DEVUser,
        DEVAdmin,
        NCRTech,
        CDRTech,
        CDRMgr,
        NCRMgr,
        DIRMgrQA,
        DIRMgrQC,
        DIRTechQA,
        DIRTechQC
    }

    public enum UserGroup
    {
        TechQA,
        MgrQA,
        TechQC,
        MgrQC
    }

    public enum Reporter
    {
        Html,
        Klov
    }
}