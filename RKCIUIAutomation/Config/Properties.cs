﻿namespace RKCIUIAutomation.Config
{
    public enum TestEnv
    {
        Test,
        Stage,
        PreProd,
        Prod
    }

    public enum ProjectName
    {
        Garnet,
        Green_Line_Extension,
        I15_Southbound,
        I15_Tech_Corridor,
        SH249_Extension,
        Southern_Gateway,
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
        ProjUser
    }
}