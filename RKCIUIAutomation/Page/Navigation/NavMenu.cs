using OpenQA.Selenium;
using System;
using System.Threading;

using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Page.PageHelper;

namespace RKCIUIAutomation.Page.Navigation
{
    public class NavMenu : NavEnums
    {
        public NavMenu() { }
        public NavMenu(IWebDriver driver) => Driver = driver;

        Type pageType = null;
        Enum mainNavEnum = null;
        Enum adminByEnum = null;
        Enum subAdminEnum = null;
        Enum subAdminSysConfigEnum = null;
        bool projectSubMenu = false;
        private By SetNavClickLocator<T>(T navEnum) => GetNavMenuByLocator(ConvertToEnumType(navEnum));

        public void Menu<T>(T navEnum)
        {
            pageType = navEnum.GetType();
            var reflectedPageType = pageType.ReflectedType;

            if (reflectedPageType.Equals(typeof(Project)) ||  reflectedPageType.IsSubclassOf(typeof(Project)))
            {
                projectSubMenu = true;
                mainNavEnum = MainNav.Menu.Project;

                if (reflectedPageType.Equals(typeof(Project.Administration)) || reflectedPageType.IsSubclassOf(typeof(Project.Administration)))
                {
                    adminByEnum = Project.Menu.Administration;

                    if (reflectedPageType.Equals(typeof(Project.Administration.UserManagement)) || reflectedPageType.IsSubclassOf(typeof(Project.Administration.UserManagement)))
                    {
                        subAdminEnum = Project.Administration.Menu.User_Management;
                    }
                    else if (reflectedPageType.Equals(typeof(Project.Administration.SystemConfiguration)) || reflectedPageType.IsSubclassOf(typeof(Project.Administration.SystemConfiguration)))
                    {
                        subAdminEnum = Project.Administration.Menu.System_Configuration;

                        if (reflectedPageType.Equals(typeof(Project.Administration.SystemConfiguration.Equipment)))
                        {
                            subAdminSysConfigEnum = Project.Administration.SystemConfiguration.Menu.Equipment;
                        }
                        else if (reflectedPageType.Equals(typeof(Project.Administration.SystemConfiguration.GradeManagement)))
                        {
                            subAdminSysConfigEnum = Project.Administration.SystemConfiguration.Menu.Grade_Management;
                        }
                    }
                    else if (reflectedPageType.Equals(typeof(Project.Administration.AdminTools)))
                    {
                        subAdminEnum = Project.Administration.Menu.Admin_Tools;
                    }
                }
            }
            else if (reflectedPageType.Equals(typeof(QALab)))
            {
                mainNavEnum = MainNav.Menu.QA_Lab;
            }
            else if (reflectedPageType.Equals( typeof(QARecordControl)))
            {
                mainNavEnum = MainNav.Menu.QA_Record_Control;
            }
            else if (reflectedPageType.Equals( typeof(QAEngineer)))
            {
                mainNavEnum = MainNav.Menu.QA_Engineer;
            }
            else if (reflectedPageType.Equals( typeof(ReportsNotices)))
            {
                mainNavEnum = MainNav.Menu.Reports_Notices;
            }
            else if (reflectedPageType.Equals( typeof(QASearch)))
            {
                mainNavEnum = MainNav.Menu.QA_Search;
            }
            else if (reflectedPageType.Equals( typeof(QAField)))
            {
                mainNavEnum = MainNav.Menu.QA_Field;
            }
            else if (reflectedPageType.Equals( typeof(Owner)))
            {
                mainNavEnum = MainNav.Menu.Owner;
            }
            else if (reflectedPageType.Equals( typeof(MaterialMixCodes)))
            {
                mainNavEnum = MainNav.Menu.Material_Mix_Codes;
            }
            else if (reflectedPageType.Equals( typeof(RMCenter)))
            {
                mainNavEnum = MainNav.Menu.RM_Center;
            }
            else if (reflectedPageType.Equals( typeof(QAInbox)))
            {
                mainNavEnum = MainNav.Menu.QA_Inbox;
            }
            else if (reflectedPageType.Equals( typeof(DOTInbox)))
            {
                mainNavEnum = MainNav.Menu.DOT_Inbox;
            }
            else if (reflectedPageType.Equals( typeof(OwnerInbox)))
            {
                mainNavEnum = MainNav.Menu.Owner_Inbox;
            }
            else if (reflectedPageType.Equals( typeof(DevInbox)))
            {
                mainNavEnum = MainNav.Menu.Dev_Inbox;
            }
            else if (reflectedPageType.Equals( typeof(RFI)))
            {
                mainNavEnum = MainNav.Menu.RFI;
            }
            else if (reflectedPageType.Equals( typeof(ELVIS)))
            {
                mainNavEnum = MainNav.Menu.ELVIS;
            }


            if (projectSubMenu)
            {
                Hover(GetNavMenuByLocator(mainNavEnum));
                //SetProjectNavMenuDisplayAttribute(mainNavEnum);
                //Thread.Sleep(2000);
                if (adminByEnum != null)
                {
                    Hover(GetNavMenuByLocator(adminByEnum));
                    //ClickElement(GetNavMenuByLocator(adminByEnum));
                    //SetProjectNavMenuDisplayAttribute(adminByEnum);
                    //Thread.Sleep(2000);
                    if (subAdminEnum != null)
                    {
                        Hover(GetNavMenuByLocator(subAdminEnum));
                        //ClickElement(GetNavMenuByLocator(subAdminEnum));
                        //SetProjectNavMenuDisplayAttribute(subAdminEnum);
                        //Thread.Sleep(2000);
                        if (subAdminSysConfigEnum != null)
                        {
                            Hover(GetNavMenuByLocator(subAdminSysConfigEnum));
                            //ClickElement(GetNavMenuByLocator(subAdminSysConfigEnum));
                            //SetProjectNavMenuDisplayAttribute(subAdminSysConfigEnum);
                            //Thread.Sleep(2000);
                        }
                    }
                }
            }
            else
            {
                Hover(GetNavMenuByLocator(mainNavEnum));
            }

            ClickElement(SetNavClickLocator(navEnum));
        }
    }
}
