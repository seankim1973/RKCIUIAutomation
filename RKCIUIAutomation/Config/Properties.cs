﻿using RKCIUIAutomation.Test;
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
        DirQA,
        DirQC
    }

    public enum Reporter
    {
        Html,
        Klov
    }

    public class ConfigTestUsers
    {
        [ThreadStatic]
        internal UserType technicianUser;

        [ThreadStatic]
        internal UserType managerUser;

        public ConfigTestUsers()
        {
        }

        //public ConfigTestUsers(UserGroup userGroup)
        //{
        //    switch (userGroup)
        //    {
        //        case UserGroup.DirQA:
        //            technicianUser = UserType.DIRTechQA;
        //            managerUser = UserType.DIRMgrQA;
        //            break;
        //        case UserGroup.DirQC:
        //            technicianUser = UserType.DIRTechQC;
        //            managerUser = UserType.DIRMgrQC;
        //            break;
        //    }
        //}

        public void AssignUsersByGroup(UserGroup userGroup)
        {
            switch (userGroup)
            {
                case UserGroup.DirQA:
                    technicianUser = UserType.DIRTechQA;
                    managerUser = UserType.DIRMgrQA;
                    break;
                case UserGroup.DirQC:
                    technicianUser = UserType.DIRTechQC;
                    managerUser = UserType.DIRMgrQC;
                    break;
            }
        }
    }
}