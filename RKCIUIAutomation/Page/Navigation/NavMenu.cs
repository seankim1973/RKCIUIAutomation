using OpenQA.Selenium;
using System;

namespace RKCIUIAutomation.Page.Navigation
{
    public class NavMenu : NavEnums
    {
        public NavMenu()
        { }
        public NavMenu(IWebDriver driver) => Driver = driver;

        private Object ConvertType(object navEnum, Type pageType) => Convert.ChangeType(navEnum, pageType);

        Type pageType = null;
        By mainNavByLocator = null;
        By adminByLocator = null;
        By subAdminByLocator = null;
        By subAdminSysConfigByLocator = null;
        By clickByLocator = null;
        //Object switchObj;

        public void Menu<TNav>(TNav navEnum)
        {
            pageType = navEnum.GetType();
            
            if (pageType == typeof(Project_eClass.Project_e))
            {
                mainNavByLocator = MainNav_By.Project;
                var project = (Project_eClass.Project_e)ConvertType(navEnum, pageType);
                switch (project)
                {
                    case Project_eClass.Project_e.My_Details:
                        clickByLocator = Project_By.My_Details;
                        break;
                    case Project_eClass.Project_e.Qms_Document:
                        clickByLocator = Project_By.Qms_Document;
                        break;
                }

                if (pageType == typeof(Project_eClass.Administration_e))
                {
                    adminByLocator = Project_By.Administration;
                    var administration = (Project_eClass.Administration_e)ConvertType(navEnum, pageType);
                    switch (administration)
                    {
                        case Project_eClass.Administration_e.Project_Details:
                            clickByLocator = Project_By.Administration_By.Project_Details;
                            break;
                        case Project_eClass.Administration_e.Companies:
                            clickByLocator = Project_By.Administration_By.Companies;
                            break;
                        case Project_eClass.Administration_e.Contracts:
                            clickByLocator = Project_By.Administration_By.Contracts;
                            break;
                        case Project_eClass.Administration_e.Menu_Editor:
                            clickByLocator = Project_By.Administration_By.Menu_Editor;
                            break;
                    }

                    if (pageType == typeof(Project_eClass.Admin_UserManagement_e))
                    {
                        subAdminByLocator = Project_By.Administration_By.User_Management;
                        var userMgmt = (Project_eClass.Admin_UserManagement_e)ConvertType(navEnum, pageType);
                        switch (userMgmt)
                        {
                            case Project_eClass.Admin_UserManagement_e.Roles:
                                clickByLocator = Project_By.Administration_By.UserManagement_By.Roles;
                                break;
                            case Project_eClass.Admin_UserManagement_e.Users:
                                clickByLocator = Project_By.Administration_By.UserManagement_By.Users;
                                break;
                            case Project_eClass.Admin_UserManagement_e.Access_Rights:
                                clickByLocator = Project_By.Administration_By.UserManagement_By.Access_Rights;
                                break;
                        }
                    }
                    else if (pageType == typeof(Project_eClass.Admin_SystemConfiguration_e))
                    {
                        subAdminByLocator = Project_By.Administration_By.System_Configuration;
                        var sysConfig = (Project_eClass.Admin_SystemConfiguration_e)ConvertType(navEnum, pageType);
                        switch (sysConfig)
                        {
                            case Project_eClass.Admin_SystemConfiguration_e.Disciplines:
                                clickByLocator = Project_By.Administration_By.SystemConfiguration_By.Disciplines;
                                break;
                            case Project_eClass.Admin_SystemConfiguration_e.Submittal_Actions:
                                clickByLocator = Project_By.Administration_By.SystemConfiguration_By.Submittal_Actions;
                                break;
                            case Project_eClass.Admin_SystemConfiguration_e.Submittal_Requirements:
                                clickByLocator = Project_By.Administration_By.SystemConfiguration_By.Submittal_Requirements;
                                break;
                            case Project_eClass.Admin_SystemConfiguration_e.Submittal_Types:
                                clickByLocator = Project_By.Administration_By.SystemConfiguration_By.Submittal_Types;
                                break;
                            case Project_eClass.Admin_SystemConfiguration_e.CVL_Lists:
                                clickByLocator = Project_By.Administration_By.SystemConfiguration_By.CVL_Lists;
                                break;
                            case Project_eClass.Admin_SystemConfiguration_e.CVL_Lists_Items:
                                clickByLocator = Project_By.Administration_By.SystemConfiguration_By.CVL_Lists_Items;
                                break;
                            case Project_eClass.Admin_SystemConfiguration_e.Notifications:
                                clickByLocator = Project_By.Administration_By.SystemConfiguration_By.Notifications;
                                break;
                            case Project_eClass.Admin_SystemConfiguration_e.Sieves:
                                clickByLocator = Project_By.Administration_By.SystemConfiguration_By.Sieves;
                                break;
                            case Project_eClass.Admin_SystemConfiguration_e.Gradations:
                                clickByLocator = Project_By.Administration_By.SystemConfiguration_By.Gradations;
                                break;
                        }

                        if (pageType == typeof(Project_eClass.Admin_SysConfig_Equipment_e))
                        {
                            subAdminSysConfigByLocator = Project_By.Administration_By.SystemConfiguration_By.Equipment;
                            var sysConfigEquip = (Project_eClass.Admin_SysConfig_Equipment_e)ConvertType(navEnum, pageType);
                            switch (sysConfigEquip)
                            {
                                case Project_eClass.Admin_SysConfig_Equipment_e.Equipment_Makes:
                                    clickByLocator = Project_By.Administration_By.SystemConfiguration_By.Equipment_By.Equipment_Makes;
                                    break;
                                case Project_eClass.Admin_SysConfig_Equipment_e.Equipment_Types:
                                    clickByLocator = Project_By.Administration_By.SystemConfiguration_By.Equipment_By.Equipment_Types;
                                    break;
                                case Project_eClass.Admin_SysConfig_Equipment_e.Equipment_Models:
                                    clickByLocator = Project_By.Administration_By.SystemConfiguration_By.Equipment_By.Equipment_Models;
                                    break;
                            }
                        }
                        else if (pageType == typeof(Project_eClass.Admin_SysConfig_GradeManagement_e))
                        {
                            subAdminSysConfigByLocator = Project_By.Administration_By.SystemConfiguration_By.Grade_Management;
                            var sysConfigEquip = (Project_eClass.Admin_SysConfig_GradeManagement_e)ConvertType(navEnum, pageType);
                            switch (sysConfigEquip)
                            {
                                case Project_eClass.Admin_SysConfig_GradeManagement_e.Grade_Types:
                                    clickByLocator = Project_By.Administration_By.SystemConfiguration_By.GradeManagement_By.Grade_Types;
                                    break;
                                case Project_eClass.Admin_SysConfig_GradeManagement_e.Grades:
                                    clickByLocator = Project_By.Administration_By.SystemConfiguration_By.GradeManagement_By.Grades;
                                    break;
                            }
                        }
                    }
                    else if (pageType == typeof(Project_eClass.Admin_AdminTools_e))
                    {
                        subAdminByLocator = Project_By.Administration_By.Admin_Tools;
                        clickByLocator = Project_By.Administration_By.Admin_Tools;
                    }
                }
            }
            else if (pageType == typeof(QALab_e))
            {
                mainNavByLocator = MainNav_By.QA_Lab;
            }
            else if (pageType == typeof(QARecordControl_e))
            {
                mainNavByLocator = MainNav_By.QA_RecordControl;
            }
            else if (pageType == typeof(QAEngineer_e))
            {
                mainNavByLocator = MainNav_By.QA_Engineer;
            }
            else if (pageType == typeof(ReportsNotices_e))
            {
                mainNavByLocator = MainNav_By.Reports_Notices;
            }
            else if (pageType == typeof(QASearch_e))
            {
                mainNavByLocator = MainNav_By.QA_Search;
            }
            else if (pageType == typeof(QAField_e))
            {
                mainNavByLocator = MainNav_By.QA_Field;
            }
            else if (pageType == typeof(Owner_e))
            {
                mainNavByLocator = MainNav_By.Owner;
            }
            else if (pageType == typeof(MaterialMixCodes_e))
            {
                mainNavByLocator = MainNav_By.Material_Mix_Codes;
            }
            else if (pageType == typeof(RMCenter_e))
            {
                mainNavByLocator = MainNav_By.RM_Center;
                var rmCenter = (RMCenter_e)ConvertType(navEnum, pageType);

                switch (rmCenter)
                {
                    case RMCenter_e.Search:
                        clickByLocator = RMCenter_By.Search;
                        break;
                    case RMCenter_e.Upload_QA_Submittal:
                        clickByLocator = RMCenter_By.Upload_QA_Submittal;
                        break;
                    case RMCenter_e.Upload_Owner_Submittal:
                        clickByLocator = RMCenter_By.Upload_DEV_Submittal;
                        break;
                    case RMCenter_e.Upload_DEV_Submittal:
                        clickByLocator = RMCenter_By.Upload_DEV_Submittal;
                        break;
                    case RMCenter_e.DOT_Project_Correspondence_Log:
                        clickByLocator = RMCenter_By.DOT_Project_Correspondence_Log;
                        break;
                    case RMCenter_e.Review_Revise_Submittal:
                        clickByLocator = RMCenter_By.Review_Revise_Submittal;
                        break;
                    case RMCenter_e.RFC_Management:
                        clickByLocator = RMCenter_By.RFC_Management;
                        break;
                    case RMCenter_e.Project_Transmittal_Log:
                        clickByLocator = RMCenter_By.Project_Transmittal_Log;
                        break;
                    case RMCenter_e.Project_Correspondence_Log:
                        clickByLocator = RMCenter_By.Project_Correspondence_Log;
                        break;
                }
            }
            else if (pageType == typeof(QAInbox_e))
            {
                mainNavByLocator = MainNav_By.QA_Inbox;
            }
            else if (pageType == typeof(DOTInbox_e))
            {
                mainNavByLocator = MainNav_By.DOT_Inbox;
            }
            else if (pageType == typeof(OwnerInbox_e))
            {
                mainNavByLocator = MainNav_By.Owner_Inbox;
            }
            else if (pageType == typeof(DevInbox_e))
            {
                mainNavByLocator = MainNav_By.Dev_Inbox;
            }
            else if (pageType == typeof(RFI_e))
            {
                mainNavByLocator = MainNav_By.RFI;
            }
            else if (pageType == typeof(ELVIS_e))
            {
                mainNavByLocator = MainNav_By.ELVIS;
            }


            Hover(mainNavByLocator);

            if (adminByLocator != null)
            {
                Hover(adminByLocator);

                if (subAdminByLocator != null)
                {
                    Hover(subAdminByLocator);

                    if (subAdminSysConfigByLocator != null)
                    {
                        Hover(subAdminSysConfigByLocator);
                    }
                }
            }

            ClickElement(clickByLocator);
        }
    }
}
