namespace RKCIUIAutomation.Page.Navigation
{
    public class PageNav : PageBase
    {
        public enum NavMainMenu
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

        public enum NavProject
        {
            [StringValue("My Details")] My_Details,
            [StringValue("Qms Document")] Qms_Document,
            [StringValue("Administration")] Administration
        }

        public enum NavProject_Administration
        {
            [StringValue("Project Details")] Project_Details,
            [StringValue("Companies")] Companies,
            [StringValue("Contracts")] Contracts,
            [StringValue("User Management")] User_Management,
            [StringValue("Menu Editor")] Menu_Editor,
            [StringValue("System Configuration")] System_Configuration
        }

        public enum NavProject_Admin_UserManagement
        {
            [StringValue("Roles")] Roles,
            [StringValue("Users")] Users,
            [StringValue("Access Rights")] Access_Rights
        }

        public enum NavProject_Admin_SystemConfiguration
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

        public enum NavProject_Admin_SysConfig_Equipment
        {
            [StringValue("Equipment Makes")] Equipment_Makes,
            [StringValue("Equipment Types")] Equipment_Types,
            [StringValue("Equipment Models")] Equipment_Models
        }

        public enum NavProject_Admin_SysConfig_GradeManagement
        {
            [StringValue("Grade Types")] Grade_Types,
            [StringValue("Grades")] Grades
        }



        public enum QALab
        {
            [StringValue("Technician Random")] Technician_Random,
            [StringValue("BreakSheet Creation")] BreakSheet_Creation,
            [StringValue("BreakSheet Legacy")] BreakSheet_Legacy,
            [StringValue("Equipment Management")] Equipment_Management
        }

        public enum QARecordControl
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

        public enum QAEngineer
        {
            [StringValue("QA Test - Lab Supervisor Review")] QA_Test_Lab_Supervisor_Review,
            [StringValue("QA Test - Field Supervisor Review")] QA_Test_Field_Supervisor_Review,
            [StringValue("QA Test - Authorization")] QA_Test_Authorization,
            [StringValue("DIR QA Review/Approval")] DIR_QA_Review_Approval
        }

        public enum ReportsNotices
        {
            [StringValue("General NCR")] General_NCR,
            [StringValue("General DN")] General_DN
        }

        public enum QASearch
        {
            [StringValue("QA Test")] QA_Test,
            [StringValue("QA Test Summary Search")] QA_Test_Summary_Search,
            [StringValue("QA Guide Schedule Summary Report")] QA_Guide_Schedule_Summary_Report,
            [StringValue("Inspection Deficiency Log Report")] Inspection_Deficiency_Log_Report,
            [StringValue("Daily Inspection Report")] Daily_Inspection_Report,
            [StringValue("DIR Summary Report")] DIR_Summary_Report
        }

        public enum QAField
        {
            [StringValue("QA Test")] QA_Test,
            [StringValue("QA DIRs")] QA_DIRs,
            [StringValue("QA Technician Random Search")] QA_Technician_Random_Search
        }

        public enum Owner
        {
            [StringValue("Owner_DIRs")] Owner_DIRs,
            [StringValue("Owner_NCRs")] Owner_NCRs
        }

        public enum MaterialMixCodes
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

        public enum RMCenter
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

        public enum QAInbox
        {
            [StringValue("Pending Comments")] Pending_Comments,
            [StringValue("My Inbox (Not for comment)")] My_Inbox_Not_for_comment
        }

        public enum DOTInbox
        {
            [StringValue("Pending Comments")] Pending_Comments
        }

        public enum OwnerInbox
        {
            [StringValue("Pending Comments")] Pending_Comments
        }

        public enum DevInbox
        {
            [StringValue("Pending Comments")] Pending_Comments,
            [StringValue("Pending Resolution")] Pending_Resolution,
            [StringValue("Pending Comments Other")] Pending_Comments_Other
        }

        public enum RFI
        {
            [StringValue("List")] List,
            [StringValue("Create")] Create
        }

        public enum ELVIS
        {
            [StringValue("About")] About
        }
    }
}
