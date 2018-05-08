﻿namespace RKCIUIAutomation.Config
{
    public enum TestEnv
    {
        Test,
        Stage,
        PreProd,
        Prod
    }

    public enum Project
    {
        Garnet,
        Green_Line_Extension,
        I15_Southbound,
        I15_Tech_Corridor,
        SH249,
        SMF202,
        Southern_Gateway,
        Tappan_Zee
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
        ProjManager,
        ProjUser
    }
}
