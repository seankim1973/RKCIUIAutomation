using OpenQA.Selenium;
using System;
using static RKCIUIAutomation.Page.Navigation.PageNav;

namespace RKCIUIAutomation.Page.Navigation
{

    public abstract class MainNavLocators : PageHelper
    {
        //Main Navigation Menu Locators
        public By Project = SetLocatorXpath(NavMainMenu.Project);
        public By QA_Lab = SetLocatorXpath(NavMainMenu.QA_Lab);
        public By QA_RecordControl = SetLocatorXpath(NavMainMenu.QA_Record_Control);
        public By QA_Engineer = SetLocatorXpath(NavMainMenu.QA_Engineer);
        public By Reports_Notices = SetLocatorXpath(NavMainMenu.Reports_Notices);
        public By QA_Search = SetLocatorXpath(NavMainMenu.QA_Search);
        public By QA_Field = SetLocatorXpath(NavMainMenu.QA_Field);
        public By Owner = SetLocatorXpath(NavMainMenu.Owner);
        public By Material_Mix_Codes = SetLocatorXpath(NavMainMenu.Material_Mix_Codes);
        public By RM_Center = SetLocatorXpath(NavMainMenu.RM_Center);
        public By QA_Inbox = SetLocatorXpath(NavMainMenu.QA_Inbox);
        public By DOT_Inbox = SetLocatorXpath(NavMainMenu.DOT_Inbox);
        public By Owner_Inbox = SetLocatorXpath(NavMainMenu.Owner_Inbox);
        public By Dev_Inbox = SetLocatorXpath(NavMainMenu.Dev_Inbox);
        public By RFI = SetLocatorXpath(NavMainMenu.RFI);
        public By ELVIS = SetLocatorXpath(NavMainMenu.ELVIS);
    }

    //Project Navigation Menu Locators
    public class ProjectNavLocators : MainNavLocators
    {
        public By My_Details = SetLocatorXpath(NavProject.My_Details);
        public By Qms_Document = SetLocatorXpath(NavProject.Qms_Document);
        public By Administration = SetLocatorXpath(NavProject.Administration);

        public class ProjectAdministrationLocators
        {
            public By Project_Details = SetLocatorXpath(NavProject_Administration.Project_Details);
            public By Companies = SetLocatorXpath(NavProject_Administration.Companies);
            public By Contracts = SetLocatorXpath(NavProject_Administration.Contracts);
            public By User_Management = SetLocatorXpath(NavProject_Administration.User_Management);
            public By Menu_Editor = SetLocatorXpath(NavProject_Administration.Menu_Editor);
            public By System_Configuration = SetLocatorXpath(NavProject_Administration.System_Configuration);
        }

        public class ProjectAdminUserManagementLocators
        {
            public By Roles = SetLocatorXpath(NavProject_Admin_UserManagement.Roles);
            public By Users = SetLocatorXpath(NavProject_Admin_UserManagement.Users);
            public By Access_Rights = SetLocatorXpath(NavProject_Admin_UserManagement.Access_Rights);
        }

        public class ProjectSystemConfigurationLocators
        {
            public By Disciplines = SetLocatorXpath(NavProject_Admin_SystemConfiguration.Disciplines);
            public By Submittal_Actions = SetLocatorXpath(NavProject_Admin_SystemConfiguration.Submittal_Actions);
            public By Submittal_Requirements = SetLocatorXpath(NavProject_Admin_SystemConfiguration.Submittal_Requirements);
            public By Submittal_Types = SetLocatorXpath(NavProject_Admin_SystemConfiguration.Submittal_Types);
            public By CVL_Lists = SetLocatorXpath(NavProject_Admin_SystemConfiguration.CVL_Lists);
            public By CVL_Lists_Items = SetLocatorXpath(NavProject_Admin_SystemConfiguration.CVL_Lists_Items);
            public By Notifications = SetLocatorXpath(NavProject_Admin_SystemConfiguration.Notifications);
            public By Sieves = SetLocatorXpath(NavProject_Admin_SystemConfiguration.Sieves);
            public By Gradations = SetLocatorXpath(NavProject_Admin_SystemConfiguration.Gradations);
            public By Equipment = SetLocatorXpath(NavProject_Admin_SystemConfiguration.Equipment);
            public By Grade_Management = SetLocatorXpath(NavProject_Admin_SystemConfiguration.Grade_Management);
        }

        public class ProjectSysConfigEquipmentLocators
        {
            public By Equipment_Makes = SetLocatorXpath(NavProject_Admin_SysConfig_Equipment.Equipment_Makes);
            public By Equipment_Types = SetLocatorXpath(NavProject_Admin_SysConfig_Equipment.Equipment_Types);
            public By Equipment_Models = SetLocatorXpath(NavProject_Admin_SysConfig_Equipment.Equipment_Models);
        }

        public class ProjectSysConfigGradeManagementLocators : MainNavLocators
        {
            public By Grade_Types = SetLocatorXpath(NavProject_Admin_SysConfig_GradeManagement.Grade_Types);
            public By Grades = SetLocatorXpath(NavProject_Admin_SysConfig_GradeManagement.Grades);
        }
    }


    //QA Lab Navigation Menu Locators
    public class QALabLocators : MainNavLocators
    {
        public By Technician_Random = SetLocatorXpath(QALab.Technician_Random);
        public By BreakSheet_Creation = SetLocatorXpath(QALab.BreakSheet_Creation);
        public By BreakSheet_Legacy = SetLocatorXpath(QALab.BreakSheet_Legacy);
        public By Equipment_Management = SetLocatorXpath(QALab.Equipment_Management);
    }
}
