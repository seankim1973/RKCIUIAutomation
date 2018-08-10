using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;
using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Page.PageHelper;

namespace RKCIUIAutomation.Page.Navigation
{
    public class NavMenu : PageBase
    {
        public NavMenu(IWebDriver driver) => this.Driver = driver;

        public void Menu<T>(T navEnum)
        {
            Enum mainNavEnum = null;
            Enum adminEnum = null;
            Enum userMgmtEnum = null;
            Enum sysConfigEnum = null;
            Enum adminToolsEnum = null;
            Enum sysConfigEquipEnum = null;
            Enum sysConfigGradeMgmtEnum = null;

            Actions builder;
            IWebElement element;

            var reflectedPageType = navEnum.GetType().ReflectedType;
            By clickLocator = null;

            if (reflectedPageType.Equals(typeof(Project)) || reflectedPageType.IsSubclassOf(typeof(Project)))
            {
                mainNavEnum = MainNav.Menu.Project;

                if (reflectedPageType.Equals(typeof(Project.Administration)) || reflectedPageType.IsSubclassOf(typeof(Project.Administration)))
                {
                    adminEnum = Project.SubMenu_Project.Administration;

                    if (reflectedPageType.Equals(typeof(Project.Administration.UserManagement)))
                    {
                        userMgmtEnum = Project.Administration.SubMenu_Administration.User_Management;
                    }
                    else if (reflectedPageType.Equals(typeof(Project.Administration.SystemConfiguration)) || reflectedPageType.IsSubclassOf(typeof(Project.Administration.SystemConfiguration)))
                    {
                        sysConfigEnum = Project.Administration.SubMenu_Administration.System_Configuration;

                        if (reflectedPageType.Equals(typeof(Project.Administration.SystemConfiguration.Equipment)))
                        {
                            sysConfigEquipEnum = Project.Administration.SystemConfiguration.SubMenu_SystemConfiguration.Equipment;
                        }
                        else if (reflectedPageType.Equals(typeof(Project.Administration.SystemConfiguration.GradeManagement)))
                        {
                            sysConfigGradeMgmtEnum = Project.Administration.SystemConfiguration.SubMenu_SystemConfiguration.Grade_Management;
                        }
                    }
                    else if (reflectedPageType.Equals(typeof(Project.Administration.AdminTools)))
                    {
                        adminToolsEnum = Project.Administration.SubMenu_Administration.Admin_Tools;
                    }
                }
            }
            else
            {
                if (reflectedPageType.Equals(typeof(QALab)))
                {
                    mainNavEnum = MainNav.Menu.QA_Lab;
                }
                else if (reflectedPageType.Equals(typeof(OV)))
                {
                    mainNavEnum = MainNav.Menu.OV;
                }
                else if (reflectedPageType.Equals(typeof(QARecordControl)))
                {
                    mainNavEnum = MainNav.Menu.QA_Record_Control;
                }
                else if (reflectedPageType.Equals(typeof(QAEngineer)))
                {
                    mainNavEnum = MainNav.Menu.QA_Engineer;
                }
                else if (reflectedPageType.Equals(typeof(ReportsNotices)))
                {
                    mainNavEnum = MainNav.Menu.Reports_Notices;
                }
                else if (reflectedPageType.Equals(typeof(QASearch)))
                {
                    mainNavEnum = MainNav.Menu.QA_Search;
                }
                else if (reflectedPageType.Equals(typeof(QAField)))
                {
                    mainNavEnum = MainNav.Menu.QA_Field;
                }
                else if (reflectedPageType.Equals(typeof(Owner)))
                {
                    mainNavEnum = MainNav.Menu.Owner;
                }
                else if (reflectedPageType.Equals(typeof(MaterialMixCodes)))
                {
                    mainNavEnum = MainNav.Menu.Material_Mix_Codes;
                }
                else if (reflectedPageType.Equals(typeof(ControlPoint)))
                {
                    mainNavEnum = MainNav.Menu.Control_Point;
                }
                else if (reflectedPageType.Equals(typeof(RMCenter)))
                {
                    mainNavEnum = MainNav.Menu.RM_Center;
                }
                else if (reflectedPageType.Equals(typeof(QAInbox)))
                {
                    mainNavEnum = MainNav.Menu.QA_Inbox;
                }
                else if (reflectedPageType.Equals(typeof(DOTInbox)))
                {
                    mainNavEnum = MainNav.Menu.DOT_Inbox;
                }
                else if (reflectedPageType.Equals(typeof(OwnerInbox)))
                {
                    mainNavEnum = MainNav.Menu.Owner_Inbox;
                }
                else if (reflectedPageType.Equals(typeof(DevInbox)))
                {
                    mainNavEnum = MainNav.Menu.Dev_Inbox;
                }
                else if (reflectedPageType.Equals(typeof(RFI)))
                {
                    mainNavEnum = MainNav.Menu.RFI;
                }
                else if (reflectedPageType.Equals(typeof(QCLab)))
                {
                    mainNavEnum = MainNav.Menu.QC_Lab;
                }
                else if (reflectedPageType.Equals(typeof(QCRecordControl)))
                {
                    mainNavEnum = MainNav.Menu.QC_Record_Control;
                }
                else if (reflectedPageType.Equals(typeof(QCEngineer)))
                {
                    mainNavEnum = MainNav.Menu.QC_Engineer;
                }
                else if (reflectedPageType.Equals(typeof(QCSearch)))
                {
                    mainNavEnum = MainNav.Menu.QC_Search;
                }
                else if (reflectedPageType.Equals(typeof(ELVIS)))
                {
                    mainNavEnum = MainNav.Menu.ELVIS;
                }
            }

            try
            {
                VerifyPageIsLoaded();
                JsHover(GetMainNavMenuByLocator(mainNavEnum));

                if (adminEnum != null)
                {
                    builder = new Actions(Driver);
                    element = Driver.FindElement(GetNavMenuByLocator(adminEnum));
                    builder.MoveToElement(element).Perform();
                    clickLocator = GetNavMenuByLocator(ConvertToEnumType(navEnum));

                    if (userMgmtEnum != null)
                    {
                        element = Driver.FindElement(GetNavMenuByLocator(userMgmtEnum));
                        builder.MoveToElement(element).Perform();
                    }
                    else if (sysConfigEnum != null)
                    {
                        element = Driver.FindElement(GetNavMenuByLocator(sysConfigEnum));
                        builder.MoveToElement(element).Perform();

                        if (sysConfigEquipEnum != null)
                        {
                            element = Driver.FindElement(GetNavMenuByLocator(sysConfigEquipEnum));
                            builder.MoveToElement(element).Perform();
                        }
                        else if (sysConfigGradeMgmtEnum != null)
                        {
                            element = Driver.FindElement(GetNavMenuByLocator(sysConfigGradeMgmtEnum));
                            builder.MoveToElement(element).Perform();
                        }
                    }
                    else if (adminToolsEnum != null)
                    {
                        element = Driver.FindElement(GetNavMenuByLocator(adminToolsEnum));
                        builder.MoveToElement(element).Perform();
                    }
                }
                else
                {
                    clickLocator = GetNavMenuByLocator(ConvertToEnumType(navEnum), ConvertToEnumType(mainNavEnum));
                }
            }
            catch (Exception e)
            {
                log.Error($"Exception occured during Menu Navigation", e);
            }
            finally
            {
                ClickElement(clickLocator);
                Thread.Sleep(2000);
            }
        }

        //Main Navigation Enums
        private class MainNav
        {
            internal enum Menu
            {
                [StringValue("Project")] Project,
                [StringValue("QA Lab")] QA_Lab,
                [StringValue("OV")] OV,
                [StringValue("QA Record Control")] QA_Record_Control,
                [StringValue("QA Engineer")] QA_Engineer,
                [StringValue("Reports & Notices")] Reports_Notices,
                [StringValue("QA Search")] QA_Search,
                [StringValue("QA Field")] QA_Field,
                [StringValue("Owner")] Owner,
                [StringValue("Material/Mix Codes")] Material_Mix_Codes,
                [StringValue("Control Point")] Control_Point,
                [StringValue("RM Center")] RM_Center,
                [StringValue("QA Inbox")] QA_Inbox,
                [StringValue("DOT Inbox")] DOT_Inbox,
                [StringValue("Owner Inbox")] Owner_Inbox,
                [StringValue("Dev Inbox")] Dev_Inbox,
                [StringValue("RFI")] RFI,
                [StringValue("QC Lab")] QC_Lab,
                [StringValue("QC Record Control")] QC_Record_Control,
                [StringValue("QC Engineer")] QC_Engineer,
                [StringValue("QC Search")] QC_Search,
                [StringValue("ELVIS")] ELVIS
            }
        }

        //Project Menu Navigation Enums
        public class Project
        {
            internal enum SubMenu_Project
            {
                [StringValue("Administration")] Administration,
            }
            public enum Menu
            {
                [StringValue("My Details")] My_Details,
                [StringValue("Qms Document")] Qms_Document,
                [StringValue("QMS Documents")] QMS_Documents
            }

            //Project >>Administration Menu Navigation Enums
            public class Administration : Project
            {
                internal enum SubMenu_Administration
                {
                    [StringValue("User Management")] User_Management,
                    [StringValue("System Configuration")] System_Configuration,
                    [StringValue("Admin Tools")] Admin_Tools
                }
                public new enum Menu
                {
                    [StringValue("Project Details")] Project_Details,
                    [StringValue("Companies")] Companies,
                    [StringValue("Contracts")] Contracts,
                    [StringValue("Menu Editor")] Menu_Editor,
                    [StringValue("Support")] Support
                }

                //Project >>Administration >>User Management Menu Navigation Enums
                public class UserManagement : Administration
                {
                    public new enum Menu
                    {
                        [StringValue("Roles")] Roles,
                        [StringValue("Users")] Users,
                        [StringValue("Access Rights")] Access_Rights
                    }
                }

                //Project >>Administration >>System Configuration Menu Navigation Enums
                public class SystemConfiguration : Administration
                {
                    internal enum SubMenu_SystemConfiguration
                    {
                        [StringValue("Equipment")] Equipment,
                        [StringValue("Grade Management")] Grade_Management
                    }

                    public new enum Menu
                    {
                        [StringValue("Disciplines")] Disciplines,
                        [StringValue("Submittal Actions")] Submittal_Actions,
                        [StringValue("Submittal Requirements")] Submittal_Requirements,
                        [StringValue("Submittal Types")] Submittal_Types,
                        [StringValue("CVL Lists")] CVL_Lists,
                        [StringValue("CVL List Items")] CVL_List_Items,
                        [StringValue("Notifications")] Notifications,
                        [StringValue("Sieves")] Sieves,
                        [StringValue("Gradations")] Gradations,
                    }

                    //Project >>Administration >>System Configuration >>Equipment Menu Navigation Enums
                    public class Equipment : SystemConfiguration
                    {
                        public new enum Menu
                        {
                            [StringValue("Equipment Makes")] Equipment_Makes,
                            [StringValue("Equipment Types")] Equipment_Types,
                            [StringValue("Equipment Models")] Equipment_Models
                        }
                    }

                    //Project >>Administration >>System Configuration >>Grade Management Menu Navigation Enums
                    public class GradeManagement : SystemConfiguration
                    {
                        public new enum Menu
                        {
                            [StringValue("Grade Types")] Grade_Types,
                            [StringValue("Grades")] Grades
                        }
                    }
                }

                //Project >>Administration >>Admin Tools Menu Navigation Enums
                public class AdminTools : Administration
                {
                    public new enum Menu
                    {
                        [StringValue("Locked Records")] Locked_Records
                    }
                }
            }
        }

        //QA Lab Menu Navigation Enums
        public class QALab
        {
            public enum Menu
            {
                [StringValue("Technician Random")] Technician_Random,
                [StringValue("BreakSheet Creation")] BreakSheet_Creation,
                [StringValue("BreakSheet Legacy")] BreakSheet_Legacy,
                [StringValue("Equipment Management")] Equipment_Management,
                [StringValue("BreakSheet Forecast")] BreakSheet_Forecast,
                [StringValue("Cylinder Pick-Up List")] Cylinder_PickUp_List,
                [StringValue("Early Break Calendar")] Early_Break_Calendar
            }
        }

        //OV Menu Navigation Enums
        public class OV
        {
            public enum Menu
            {
                [StringValue("Create OV Test")] Create_OV_Test,
                [StringValue("OV Tests")] OV_Tests
            }
        }

        //QA Record Control Menu Navigation Enums
        public class QARecordControl
        {
            public enum Menu
            {
                [StringValue("QA Test - Original Report")] QA_Test_Original_Report,
                [StringValue("QA Test - All")] QA_Test_All,
                [StringValue("QA Test Correction Report")] QA_Test_Correction_Report,
                [StringValue("QA DIRs")] QA_DIRs,
                [StringValue("General NCR")] General_NCR,
                [StringValue("General CDR")] General_CDR,
                [StringValue("Retaining Wall BackFill Quantity Tracker")] Retaining_Wall_BackFill_Quantity_Tracker,
                [StringValue("Concrete Paving Quantity Tracker")] Concrete_Paving_Quantity_Tracker,
                [StringValue("MPL Tracker")] MPL_Tracker,
                [StringValue("Girder Tracker")] Girder_Tracker,
                [StringValue("QMS Document")] QMS_Document,
                [StringValue("QA Test - Retest Report")] QA_Test_Retest_Report,
                [StringValue("Environmental Document")] Environmental_Document
            }
        }

        //QA Engineer Menu Navigation Enums
        public class QAEngineer
        {
            public enum Menu
            {
                [StringValue("QA Test - Lab Supervisor Review")] QA_Test_Lab_Supervisor_Review,
                [StringValue("QA Test - Field Supervisor Review")] QA_Test_Field_Supervisor_Review,
                [StringValue("QA Test - Authorization")] QA_Test_Authorization,
                [StringValue("DIR QA Review/Approval")] DIR_QA_Review_Approval,
                [StringValue("QA Test - Proctor Curve Controller")] QA_Test_Proctor_Curve_Controller
            }
        }

        //Report & Notices Menu Navigation Enums
        public class ReportsNotices
        {
            public enum Menu
            {
                [StringValue("General NCR")] General_NCR,
                [StringValue("General DN")] General_DN
            }
        }

        //QA Search Menu Navigation Enums
        public class QASearch
        {
            public enum Menu
            {
                [StringValue("QA Tests")] QA_Tests,
                [StringValue("QA Test Summary Search")] QA_Test_Summary_Search,
                [StringValue("QA Guide Schedule Summary Report")] QA_Guide_Schedule_Summary_Report,
                [StringValue("Inspection Deficiency Log Report")] Inspection_Deficiency_Log_Report,
                [StringValue("Daily Inspection Report")] Daily_Inspection_Report,
                [StringValue("DIR Summary Report")] DIR_Summary_Report,
                [StringValue("DIR Checklist Search")] DIR_Checklist_Search,
                [StringValue("Ncr Log View")] Ncr_Log_View,
                [StringValue("QMS Document Search")] QMS_Document_Search,
                [StringValue("Environmental Document Search")] Environmental_Document_Search,
                [StringValue("QA/QO Test - Proctor Curve Report")] QAQO_Test_Proctor_Curve_Report,
                [StringValue("QA/QO Test - Proctor Curve Summary")] QAQO_Test_Proctor_Curve_Summary
            }
        }

        //QA Field Menu Navigation Enums
        public class QAField
        {
            public enum Menu
            {
                [StringValue("QA Test")] QA_Test,
                [StringValue("QA DIRs")] QA_DIRs,
                [StringValue("QA Technician Random Search")] QA_Technician_Random_Search,
                [StringValue("Weekly Environmental Monitoring")] Weekly_Environmental_Monitoring,
                [StringValue("Daily Environmental Inspection")] Daily_Environmental_Inspection,
                [StringValue("Weekly Environmental Inspection")] Weekly_Environmental_Inspection
            }
        }

        //Owner Menu Navigation Enums
        public class Owner
        {
            public enum Menu
            {
                [StringValue("Owner_DIRs")] Owner_DIRs,
                [StringValue("Owner_NCRs")] Owner_NCRs
            }
        }

        //Material/Mix Codes Menu Navigation Enums
        public class MaterialMixCodes
        {
            public enum Menu
            {
                [StringValue("Mix Design - PCC")] Mix_Design_PCC,
                [StringValue("Mix Design - HMA")] Mix_Design_HMA,
                [StringValue("Sieve Analyses - JMF")] Sieve_Analyses_JMF,
                [StringValue("Sieve Analyses - IOC")] Sieve_Analyses_IOC,
                [StringValue("Material Code - Concrete Aggregate")] Material_Code_Concrete_Aggregate,
                [StringValue("Material Code - Base Aggregate")] Material_Code_Base_Aggregate,
                [StringValue("Material Code - HMA Aggregate")] Material_Code_HMA_Aggregate,
                [StringValue("Material Code - Raw Material")] Material_Code_Raw_Material
            }
        }

        //Control Point Menu Navigation Enums
        public class ControlPoint
        {
            public enum Menu
            {
                [StringValue("Control Point Scheduler")] Control_Point_Scheduler,
                [StringValue("Control Point Log")] Control_Point_Log
            }
        }

        //RM Center Menu Navigation Enums
        public class RMCenter
        {
            public enum Menu
            {
                [StringValue("Search")] Search,
                [StringValue("Design Documents")] Design_Documents,
                [StringValue("Upload QA Submittal")] Upload_QA_Submittal,
                [StringValue("Upload Owner Submittal")] Upload_Owner_Submittal,
                [StringValue("Upload DEV Submittal")] Upload_DEV_Submittal,
                [StringValue("DOT Project Correspondence Log")] DOT_Project_Correspondence_Log,
                [StringValue("Review / Revise Submittal")] Review_Revise_Submittal,
                [StringValue("RFC Management ")] RFC_Management,
                [StringValue("Project Correspondence Log")] Project_Correspondence_Log,
                [StringValue("Project Transmittal Log")] Project_Transmittal_Log,
                [StringValue("Comment Summary")] Comment_Summary
            }
        }

        //QA Inbox Menu Navigation Enums
        public class QAInbox
        {
            public enum Menu
            {
                [StringValue("Pending Comments")] Pending_Comments,
                [StringValue("My Inbox (Not for comment)")] My_Inbox
            }
        }

        //DOT Inbox Menu Navigation Enums
        public class DOTInbox
        {
            public enum Menu
            {
                [StringValue("Pending Comments")] Pending_Comments
            }
        }

        //Owner Inbox Menu Navigation Enums
        public class OwnerInbox
        {
            public enum Menu
            {
                [StringValue("Pending Comments")] Pending_Comments
            }
        }

        //Dev Inbox Menu Navigation Enums
        public class DevInbox
        {
            public enum Menu
            {
                [StringValue("Pending Comments")] Pending_Comments,
                [StringValue("Pending Resolution")] Pending_Resolution,
                [StringValue("Pending Comments Other")] Pending_Comments_Other
            }
        }

        //RFI Menu Navigation Enums
        public class RFI
        {
            public enum Menu
            {
                [StringValue("List")] List,
                [StringValue("Create")] Create
            }
        }

        //QC Lab Menu Navigation Enums
        public class QCLab
        {
            public enum Menu
            {
                [StringValue("BreakSheet Creation")] BreakSheet_Creation,
                [StringValue("BreakSheet Legacy")] BreakSheet_Legacy,
                [StringValue("Equipment Management")] Equipment_Management
            }
        }


        //QC Record Control Menu Navigation Enums
        public class QCRecordControl
        {
            public enum Menu
            {
                [StringValue("QC Test - Original Report")] QC_Test_Original_Report,
                [StringValue("QC Test - All")] QC_Test_All,
                [StringValue("QC Test Correction Report")] QC_Test_Correction_Report,
                [StringValue("QC DIRs")] QC_DIRs               
            }
        }

        //QC Engineer Menu Navigation Enums
        public class QCEngineer
        {
            public enum Menu
            {
                [StringValue("QC Test - Lab Supervisor Review")] QC_Test_Lab_Supervisor_Review,
                [StringValue("QC Test  - Authorization")] QC_Test_Authorization
            }
        }


        //QC Search Menu Navigation Enums
        public class QCSearch
        {
            public enum Menu
            {
                [StringValue("QC Tests Search")] QC_Tests_Search,
                [StringValue("QC Test Summary Search")] QC_Test_Summary_Search,
                [StringValue("Daily Inspection Report")] Daily_Inspection_Report,
                [StringValue("DIR Summary Report")] DIR_Summary_Report
            }
        }

        //ELVIS Menu Navigation Enums
        public class ELVIS
        {
            public enum Menu
            {
                [StringValue("About")] About
            }
        }
    }
}
