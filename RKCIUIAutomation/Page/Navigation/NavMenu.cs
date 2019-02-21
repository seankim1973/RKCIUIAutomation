using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using RKCIUIAutomation.Base;
using System;

namespace RKCIUIAutomation.Page.Navigation
{
    public class NavMenu : PageBase
    {
        public NavMenu(IWebDriver driver) => this.Driver = driver;

        public void Menu<T>(T navEnum)
        {
            Enum mainNavEnum = null;
            Enum subOfMainNavEnum = null;
            Enum childOfSubMenuEnum = null;
            Enum subOfChildMenuEnum = null;

            BaseUtils baseUtils = new BaseUtils();
            Type reflectedPageType = null;
            By clickLocator = null;
            IWebElement element;
            Actions builder;

            try
            {
                try
                {
                    WaitForPageReady();
                }
                catch (Exception)
                {
                }

                reflectedPageType = navEnum.GetType().ReflectedType;

                if (reflectedPageType.Equals(typeof(Project)) || reflectedPageType.IsSubclassOf(typeof(Project)))
                {
                    mainNavEnum = MainNav.Menu.Project;

                    if (reflectedPageType.Equals(typeof(Project.Administration)) || reflectedPageType.IsSubclassOf(typeof(Project.Administration)))
                    {
                        subOfMainNavEnum = Project.SubMenu_Project.Administration;

                        if (reflectedPageType.Equals(typeof(Project.Administration.UserManagement)))
                        {
                            childOfSubMenuEnum = Project.Administration.SubMenu_Administration.User_Management;
                        }
                        else if (reflectedPageType.Equals(typeof(Project.Administration.SystemConfiguration)) || reflectedPageType.IsSubclassOf(typeof(Project.Administration.SystemConfiguration)))
                        {
                            childOfSubMenuEnum = Project.Administration.SubMenu_Administration.System_Configuration;

                            if (reflectedPageType.Equals(typeof(Project.Administration.SystemConfiguration.Equipment)))
                            {
                                subOfChildMenuEnum = Project.Administration.SystemConfiguration.SubMenu_SystemConfiguration.Equipment;
                            }
                            else if (reflectedPageType.Equals(typeof(Project.Administration.SystemConfiguration.GradeManagement)))
                            {
                                subOfChildMenuEnum = Project.Administration.SystemConfiguration.SubMenu_SystemConfiguration.Grade_Management;
                            }
                        }
                        else if (reflectedPageType.Equals(typeof(Project.Administration.AdminTools)))
                        {
                            childOfSubMenuEnum = Project.Administration.SubMenu_Administration.Admin_Tools;
                        }
                    }
                }
                else if (reflectedPageType.Equals(typeof(QALab)))
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
                else if (reflectedPageType.Equals(typeof(QualitySearch)) || reflectedPageType.IsSubclassOf(typeof(QualitySearch)))
                {
                    mainNavEnum = MainNav.Menu.Quality_Search;

                    if (reflectedPageType.Equals(typeof(QualitySearch.QA)))
                    {
                        subOfMainNavEnum = QualitySearch.SubMenu_QualitySearch.QA;
                    }
                    else if (reflectedPageType.Equals(typeof(QualitySearch.QC)))
                    {
                        subOfMainNavEnum = QualitySearch.SubMenu_QualitySearch.QC;
                    }
                }
                else if (reflectedPageType.Equals(typeof(DeficienciesAndAudits)))
                {
                    mainNavEnum = MainNav.Menu.Deficiencies_and_Audits;
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
                else if (reflectedPageType.Equals(typeof(RecordControl)) || reflectedPageType.IsSubclassOf(typeof(RecordControl)))
                {
                    mainNavEnum = MainNav.Menu.Record_Control;

                    if (reflectedPageType.Equals(typeof(RecordControl.QA)))
                    {
                        subOfMainNavEnum = RecordControl.SubMenu_RecordControl.QA;
                    }
                    else if (reflectedPageType.Equals(typeof(RecordControl.QC)))
                    {
                        subOfMainNavEnum = RecordControl.SubMenu_RecordControl.QC;
                    }
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

                //VerifyPageIsLoaded();
                JsHover(GetMainNavMenuByLocator(mainNavEnum));
                builder = new Actions(Driver);

                if (subOfMainNavEnum != null)
                {
                    By subOfMainNavLocator = GetNavMenuByLocator(subOfMainNavEnum, mainNavEnum);
                    element = Driver.FindElement(subOfMainNavLocator);
                    builder.MoveToElement(element).Perform();

                    if (childOfSubMenuEnum != null)
                    {
                        By childOfSubMenuLocator = GetNavMenuByLocator(childOfSubMenuEnum, subOfMainNavEnum);
                        element = Driver.FindElement(childOfSubMenuLocator);
                        builder.MoveToElement(element).Perform();

                        if (subOfChildMenuEnum != null)
                        {
                            By subOfChildMenuLocator = GetNavMenuByLocator(subOfChildMenuEnum, childOfSubMenuEnum);
                            element = Driver.FindElement(subOfChildMenuLocator);
                            builder.MoveToElement(element).Perform();
                        }
                    }

                    clickLocator = GetNavMenuByLocator(baseUtils.ConvertToType<Enum>(navEnum));
                }
                else
                {
                    clickLocator = GetNavMenuByLocator(baseUtils.ConvertToType<Enum>(navEnum), baseUtils.ConvertToType<Enum>(mainNavEnum));
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            finally
            {
                JsClickElement(clickLocator);

                try
                {
                    WaitForPageReady();
                }
                catch (Exception e)
                {
                    log.Error(e.Message);
                }
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
                [StringValue("Quality Search")] Quality_Search,
                [StringValue("Deficiencies and Audits")] Deficiencies_and_Audits,
                [StringValue("Lab")] Lab,
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
                [StringValue("Record Control")] Record_Control,
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

        //Lab Menu Navigation Enums
        public class Lab
        {
            public enum Menu
            {
                [StringValue("Technician Random")] Technician_Random,
                [StringValue("BreakSheet Creation")] BreakSheet_Creation,
                [StringValue("BreakSheet Legacy")] BreakSheet_Legacy,
                [StringValue("Equipment Management")] Equipment_Management,
                [StringValue("BreakSheet Forecast")] BreakSheet_Forecast
            }
        }

        //Record Control Menu Navigation Enums
        public class QARecordControl
        {
            public enum Menu
            {
                [StringValue("QA Test - Original Report")] QA_Test_Original_Report,
                [StringValue("QA Test - All")] QA_Test_All,
                [StringValue("QA Test Correction Report")] QA_Test_Correction_Report,
                [StringValue("QA DIRs")] QA_DIRs,
                [StringValue("QA IDRs")] QA_IDRs,
                [StringValue("General NCR")] General_NCR,
                [StringValue("General CDR")] General_CDR,
                [StringValue("General Deficiency Notice")] General_Deficiency_Notice,
                [StringValue("QA Deficiency Notice")] QA_Deficiency_Notice,
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
                [StringValue("PCC Mix Design Report")] PCC_Mix_Design_Report,
                [StringValue("Hma Mix Design Summary")] Hma_Mix_Design_Summary,
                [StringValue("Daily Inspection Report")] Daily_Inspection_Report,
                [StringValue("Hma Mix Design Report")] Hma_Mix_Design_Report,
                [StringValue("DIR Summary Report")] DIR_Summary_Report,
                [StringValue("DIR Checklist Search")] DIR_Checklist_Search,
                [StringValue("Material Traceability Matrix Search")] Material_Traceability_Matrix_Search,
                [StringValue("NCR Log View")] NCR_Log_View,
                [StringValue("CDR Log View")] CDR_Log_View,
                [StringValue("QMS Document Search")] QMS_Document_Search,
                [StringValue("Environmental Document Search")] Environmental_Document_Search,
                [StringValue("QA/QO Test - Proctor Curve Report")] QAQO_Test_Proctor_Curve_Report,
                [StringValue("QA/QO Test - Proctor Curve Summary")] QAQO_Test_Proctor_Curve_Summary
            }
        }

        //Quality Search Menu Navigation Enums
        public class QualitySearch
        {
            internal enum SubMenu_QualitySearch
            {
                [StringValue("QA")] QA,
                [StringValue("QC")] QC
            }

            public enum Menu
            {
                [StringValue("Test Summary")] Test_Summary,
                [StringValue("Tests")] Tests,
                [StringValue("Guide Schedule Summary Report")] Guide_Schedule_Summary_Report,
                [StringValue("Inspection Deficiency Log Report")] Inspection_Deficiency_Log_Report,
                [StringValue("Inspector Daily Report")] Inspector_Daily_Report,
                [StringValue("IDR Summary Report")] IDR_Summary_Report,
                [StringValue("Mix Design Summary - HMA")] Mix_Design_Summary_HMA,
                [StringValue("Mix Design Report - HMA")] Mix_Design_Report_HMA,
                [StringValue("NCR Log View")] NCR_Log_View,
                [StringValue("CDR Log View")] CDR_Log_View,
                [StringValue("QA/QO: Test - Proctor Curve Summary")] QA_QO_Test_Proctor_Curve_Summary, //GLX
                [StringValue("QA/QO: Test - Proctor Curve Report")] QA_QO_Test_Proctor_Curve_Report, //GLX
                [StringValue("Deficiency Log View")] Deficiency_Log_View,
                [StringValue("HMA Mix Design Summary")] HMA_Mix_Design_Summary,
                [StringValue("HMA Mix Design Report")] HMA_Mix_Design_Report,
                [StringValue("PCC Mix Design Summary")] PCC_Mix_Design_Summary,
                [StringValue("Material Traceability Matrix")] Material_Traceability_Matrix, //LAX
                [StringValue("Material Traceability Matrix Search")] Material_Traceability_Matrix_Search, //GLX
                [StringValue("Test - Proctor Curve Summary")] Test_Proctor_Curve_Summary,
                [StringValue("Test - Proctor Curve Report")] Test_Proctor_Curve_Report
            }

            public class QA : QualitySearch
            {
                public new enum Menu
                {
                    [StringValue("QA Test Search")] QA_Test_Search,
                    [StringValue("QA Test Summary Search")] QA_Test_Summary_Search,
                    [StringValue("QA Daily Inspection Report")] QA_Daily_Inspection_Report,
                    [StringValue("QA DIR Summary Report")] QA_DIR_Summary_Report
                }
            }

            public class QC : QualitySearch
            {
                public new enum Menu
                {
                    [StringValue("QC Test Search")] QC_Test_Search,
                    [StringValue("QC Test Summary Search")] QC_Test_Summary_Search,
                    [StringValue("QC Daily Inspection Report")] QC_Daily_Inspection_Report,
                    [StringValue("QC DIR Summary Report")] QC_DIR_Summary_Report
                }
            }
        }

        public class DeficienciesAndAudits
        {
            public enum Menu
            {
                [StringValue("General Deficiency Notice")] General_Deficiency_Notice
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
                [StringValue("Owner DIRs")] Owner_DIRs,
                [StringValue("Owner NCRs")] Owner_NCRs
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

        //Record Control Menu Navigation Enums
        public class RecordControl
        {
            internal enum SubMenu_RecordControl
            {
                [StringValue("QA")] QA,
                [StringValue("QC")] QC
            }

            public enum Menu
            {
                [StringValue("Test Count")] Test_Count,
                [StringValue("DIR Count")] DIR_Count,
                [StringValue("Retaining Wall Backfill Quantity Tracker")] Retaining_Wall_Backfill_Quantity_Tracker,
                [StringValue("Concrete Paving Quantity Tracker")] Concrete_Paving_Quantity_Tracker,
                [StringValue("MPL Tracker")] MPL_Tracker,
                [StringValue("Girder Tracker")] Girder_Tracker,
                [StringValue("Weekly Environmental Monitoring")] Weekly_Environmental_Monitoring,
                [StringValue("Daily Environmental Inspection")] Daily_Environmental_Inspection,
                [StringValue("Weekly Environmental Inspection")] Weekly_Environmental_Inspection,
            }

            public class QA : RecordControl
            {
                public new enum Menu
                {
                    [StringValue("QA Test - All")] QA_Test_All,
                    [StringValue("QA DIR")] QA_DIR,
                    [StringValue("QA NCR")] QA_NCR,
                    [StringValue("QA Deficiency Notice")] QA_Deficiency_Notice,
                }
            }

            public class QC : RecordControl
            {
                public new enum Menu
                {
                    [StringValue("QC Test - All")] QC_Test_All,
                    [StringValue("QC DIR")] QC_DIR,
                    [StringValue("QC NCR")] QC_NCR,
                    [StringValue("QC Deficiency Notice")] QC_Deficiency_Notice,
                }
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
                [StringValue("QC DIRs")] QC_DIRs,
                [StringValue("QC IDRs")] QC_IDRs,
                [StringValue("QC NCR")] QC_NCR,
                [StringValue("QC CDR")] QC_CDR,
                [StringValue("QC Deficiency Notice")] QC_Deficiency_Notice
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