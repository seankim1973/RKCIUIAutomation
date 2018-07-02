﻿namespace RKCIUIAutomation.Config
{
    public enum TestEnv
    {
        Test,
        Stage,
        PreProd,
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
        Edge,
        Safari
    }

    public enum UserType
    {
        ProjAdmin,
        SysAdmin,
        ProjUser,
        Bhoomi,
        IQFRecordsManager,
        IQFUse,
        IQFAdmin,
        DotUser,
        DotAdmin,
        DevUser,
        DevAdmin
    }
}