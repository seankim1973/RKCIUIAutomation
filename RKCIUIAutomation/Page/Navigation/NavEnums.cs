﻿using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.Navigation
{
    public class NavEnums : PageBase
    {
        public NavEnums()
        { }
        public NavEnums(IWebDriver driver) => Driver = driver;

        //Main Navigation Enums
        public class MainNav
        {
            public enum Menu
            {
                [StringValue("Project")] Project,
                [StringValue("QA Lab")] QA_Lab,
                [StringValue("QA Record Control")] QA_Record_Control,
                [StringValue("QA Engineer")] QA_Engineer,
                [StringValue("Reports & Notices")] Reports_Notices,
                [StringValue("QA Search")] QA_Search,
                [StringValue("QA Field")] QA_Field,
                [StringValue("Owner")] Owner,
                [StringValue("Material/Mix Codes")] Material_Mix_Codes,
                [StringValue("RM Center")] RM_Center,
                [StringValue("QA Inbox")] QA_Inbox,
                [StringValue("DOT Inbox")] DOT_Inbox,
                [StringValue("Owner Inbox")] Owner_Inbox,
                [StringValue("Dev Inbox")] Dev_Inbox,
                [StringValue("RFI")] RFI,
                [StringValue("ELVIS")] ELVIS
            }
        }

        //Project Menu Navigation Enums
        public class Project
        {
            public enum Menu
            {
                [StringValue("My Details")] My_Details,
                [StringValue("Qms Document")] Qms_Document,
                [StringValue("Administration")] Administration
            }

            //Project >>Administration Menu Navigation Enums
            public class Administration : Project
            {
                public new enum Menu
                {
                    [StringValue("Project Details")] Project_Details,
                    [StringValue("Companies")] Companies,
                    [StringValue("Contracts")] Contracts,
                    [StringValue("User Management")] User_Management,
                    [StringValue("Menu Editor")] Menu_Editor,
                    [StringValue("System Configuration")] System_Configuration,
                    [StringValue("Admin Tools")] Admin_Tools
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
                    public new enum Menu
                    {
                        [StringValue("Disciplines")] Disciplines,
                        [StringValue("Submittal Actions")] Submittal_Actions,
                        [StringValue("Submittal Requirements")] Submittal_Requirements,
                        [StringValue("Submittal Types")] Submittal_Types,
                        [StringValue("CVL Lists")] CVL_Lists,
                        [StringValue("CVL Lists Items")] CVL_Lists_Items,
                        [StringValue("Notifications")] Notifications,
                        [StringValue("Sieves")] Sieves,
                        [StringValue("Gradations")] Gradations,
                        [StringValue("Equipment")] Equipment,
                        [StringValue("Grade Management")] Grade_Management
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
                [StringValue("Equipment Management")] Equipment_Management
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
                [StringValue("Girder Tracker")] Girder_Tracker
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
                [StringValue("DIR QA Review/Approval")] DIR_QA_Review_Approval
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
                [StringValue("DIR Summary Report")] DIR_Summary_Report
            }
        }

        //QA Field Menu Navigation Enums
        public class QAField
        {
            public enum Menu
            {
                [StringValue("QA Test")] QA_Test,
                [StringValue("QA DIRs")] QA_DIRs,
                [StringValue("QA Technician Random Search")] QA_Technician_Random_Search
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

        //RM Center Menu Navigation Enums
        public class RMCenter
        {
            public enum Menu
            {
                [StringValue("Search")] Search,
                [StringValue("Upload QA Submittal")] Upload_QA_Submittal,
                [StringValue("Upload Owner Submittal")] Upload_Owner_Submittal,
                [StringValue("Upload DEV Submittal")] Upload_DEV_Submittal,
                [StringValue("DOT Project Correspondence Log")] DOT_Project_Correspondence_Log,
                [StringValue("Review / Revise Submittal")] Review_Revise_Submittal,
                [StringValue("RFC Management")] RFC_Management,
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
