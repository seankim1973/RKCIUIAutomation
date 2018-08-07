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
        ProjAdmin,
        SysAdmin,
        ProjUser,
        Bhoomi,
        IQFRecordsMgr,
        IQFUser,
        IQFAdmin,
        DOTUser,
        DOTAdmin,
        DEVUser,
        DEVAdmin
    }

    public enum Reporter
    {
        Html,
        Klov
    }
}