using OpenQA.Selenium;
using System;
using System.Threading;

using static RKCIUIAutomation.Base.WebDriverFactory;
using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Page.PageHelper;

namespace RKCIUIAutomation.Page.Navigation
{
    public class NavMenu : NavEnums
    {
        public NavMenu() { }
        public NavMenu(IWebDriver driver) => driver = Driver;

        Type pageType = null;
        Enum mainNavEnum = null;
        Enum adminByEnum = null;
        Enum subAdminEnum = null;
        Enum subAdminSysConfigEnum = null;

        private By SetNavClickLocator<T>(T navEnum) => GetNavMenuByLocator(ConvertToEnumType(navEnum));

        public void Menu<T>(T navEnum)
        {
            pageType = navEnum.GetType();

            if (pageType == typeof(Project_eClass.Project_e))
            {
                mainNavEnum = MainNav_e.Project;

                if (pageType == typeof(Project_eClass.Administration_e))
                {
                    adminByEnum = Project_eClass.Project_e.Administration;
                    
                    if (pageType == typeof(Project_eClass.Admin_UserManagement_e))
                    {
                        subAdminEnum = Project_eClass.Administration_e.User_Management;
                    }
                    else if (pageType == typeof(Project_eClass.Admin_SystemConfiguration_e))
                    {
                        subAdminEnum = Project_eClass.Administration_e.System_Configuration;

                        if (pageType == typeof(Project_eClass.Admin_SysConfig_Equipment_e))
                        {
                            subAdminSysConfigEnum = Project_eClass.Admin_SystemConfiguration_e.Equipment;
                        }
                        else if (pageType == typeof(Project_eClass.Admin_SysConfig_GradeManagement_e))
                        {
                            subAdminSysConfigEnum = Project_eClass.Admin_SystemConfiguration_e.Grade_Management;
                        }
                    }
                    else if (pageType == typeof(Project_eClass.Admin_AdminTools_e))
                    {
                        subAdminEnum = Project_eClass.Administration_e.Admin_Tools;
                    }
                }
            }
            else if (pageType == typeof(QALab_e))
            {
                mainNavEnum = MainNav_e.QA_Lab;
            }
            else if (pageType == typeof(QARecordControl_e))
            {
                mainNavEnum = MainNav_e.QA_Record_Control;
            }
            else if (pageType == typeof(QAEngineer_e))
            {
                mainNavEnum = MainNav_e.QA_Engineer;
            }
            else if (pageType == typeof(ReportsNotices_e))
            {
                mainNavEnum = MainNav_e.Reports_Notices;
            }
            else if (pageType == typeof(QASearch_e))
            {
                mainNavEnum = MainNav_e.QA_Search;
            }
            else if (pageType == typeof(QAField_e))
            {
                mainNavEnum = MainNav_e.QA_Field;
            }
            else if (pageType == typeof(Owner_e))
            {
                mainNavEnum = MainNav_e.Owner;
            }
            else if (pageType == typeof(MaterialMixCodes_e))
            {
                mainNavEnum = MainNav_e.Material_Mix_Codes;
            }
            else if (pageType == typeof(RMCenter_e))
            {
                mainNavEnum = MainNav_e.RM_Center;
            }
            else if (pageType == typeof(QAInbox_e))
            {
                mainNavEnum = MainNav_e.QA_Inbox;
            }
            else if (pageType == typeof(DOTInbox_e))
            {
                mainNavEnum = MainNav_e.DOT_Inbox;
            }
            else if (pageType == typeof(OwnerInbox_e))
            {
                mainNavEnum = MainNav_e.Owner_Inbox;
            }
            else if (pageType == typeof(DevInbox_e))
            {
                mainNavEnum = MainNav_e.Dev_Inbox;
            }
            else if (pageType == typeof(RFI_e))
            {
                mainNavEnum = MainNav_e.RFI;
            }
            else if (pageType == typeof(ELVIS_e))
            {
                mainNavEnum = MainNav_e.ELVIS;
            }


            Hover(GetNavMenuByLocator(mainNavEnum));

            if (adminByEnum != null)
            {
                Hover(GetNavMenuByLocator(adminByEnum));

                if (subAdminEnum != null)
                {
                    Hover(GetNavMenuByLocator(subAdminEnum));

                    if (subAdminSysConfigEnum != null)
                    {
                        Hover(GetNavMenuByLocator(subAdminSysConfigEnum));
                    }
                }
            }

            ClickElement(SetNavClickLocator(navEnum));
            Thread.Sleep(2000);
        }
    }
}
