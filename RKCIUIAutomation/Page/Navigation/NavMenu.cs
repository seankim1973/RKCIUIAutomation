using OpenQA.Selenium;
using System;
using System.Threading;

namespace RKCIUIAutomation.Page.Navigation
{
    public class NavMenu : NavEnums
    {
        public NavMenu() { }
        public NavMenu(IWebDriver driver) => Driver = driver;

        Type pageType = null;
        By mainNavByLocator = null;
        By adminByLocator = null;
        By subAdminByLocator = null;
        By subAdminSysConfigByLocator = null;

        private By SetNavClickLocator<T>(T navEnum) => GetNavMenuByLocator(ConvertToEnumType(navEnum));

        public void Menu<T>(T navEnum)
        {
            pageType = navEnum.GetType();
            
            if (pageType == typeof(Project_eClass.Project_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.Project);

                if (pageType == typeof(Project_eClass.Administration_e))
                {
                    adminByLocator = GetNavMenuByLocator(Project_eClass.Project_e.Administration);
                    
                    if (pageType == typeof(Project_eClass.Admin_UserManagement_e))
                    {
                        subAdminByLocator = GetNavMenuByLocator(Project_eClass.Administration_e.User_Management);
                    }
                    else if (pageType == typeof(Project_eClass.Admin_SystemConfiguration_e))
                    {
                        subAdminByLocator = GetNavMenuByLocator(Project_eClass.Administration_e.System_Configuration);

                        if (pageType == typeof(Project_eClass.Admin_SysConfig_Equipment_e))
                        {
                            subAdminSysConfigByLocator = GetNavMenuByLocator(Project_eClass.Admin_SystemConfiguration_e.Equipment);
                        }
                        else if (pageType == typeof(Project_eClass.Admin_SysConfig_GradeManagement_e))
                        {
                            subAdminSysConfigByLocator = GetNavMenuByLocator(Project_eClass.Admin_SystemConfiguration_e.Grade_Management);
                        }
                    }
                    else if (pageType == typeof(Project_eClass.Admin_AdminTools_e))
                    {
                        subAdminByLocator = GetNavMenuByLocator(Project_eClass.Administration_e.Admin_Tools);
                    }
                }
            }
            else if (pageType == typeof(QALab_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.QA_Lab);
            }
            else if (pageType == typeof(QARecordControl_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.QA_Record_Control);
            }
            else if (pageType == typeof(QAEngineer_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.QA_Engineer);
            }
            else if (pageType == typeof(ReportsNotices_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.Reports_Notices);
            }
            else if (pageType == typeof(QASearch_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.QA_Search);
            }
            else if (pageType == typeof(QAField_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.QA_Field);
            }
            else if (pageType == typeof(Owner_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.Owner);
            }
            else if (pageType == typeof(MaterialMixCodes_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.Material_Mix_Codes);
            }
            else if (pageType == typeof(RMCenter_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.RM_Center);
            }
            else if (pageType == typeof(QAInbox_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.QA_Inbox);
            }
            else if (pageType == typeof(DOTInbox_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.DOT_Inbox);
            }
            else if (pageType == typeof(OwnerInbox_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.Owner_Inbox);
            }
            else if (pageType == typeof(DevInbox_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.Dev_Inbox);
            }
            else if (pageType == typeof(RFI_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.RFI);
            }
            else if (pageType == typeof(ELVIS_e))
            {
                mainNavByLocator = GetNavMenuByLocator(MainNav_e.ELVIS);
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

            ClickElement(SetNavClickLocator(navEnum));
            Thread.Sleep(2000);
        }
    }
}
