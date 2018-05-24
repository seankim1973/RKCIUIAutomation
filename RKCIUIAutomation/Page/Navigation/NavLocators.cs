using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.Navigation
{
    //Main Menu By Locators
    public class MainNav_By : NavEnums
    {
        //Main By Locators
        public static By Project { get; } = GetNavMenuByLocator(MainNav_e.Project);
        public static By QA_Lab = GetNavMenuByLocator(MainNav_e.QA_Lab);
        public static By QA_RecordControl = GetNavMenuByLocator(MainNav_e.QA_Record_Control);
        public static By QA_Engineer = GetNavMenuByLocator(MainNav_e.QA_Engineer);
        public static By Reports_Notices = GetNavMenuByLocator(MainNav_e.Reports_Notices);
        public static By QA_Search = GetNavMenuByLocator(MainNav_e.QA_Search);
        public static By QA_Field = GetNavMenuByLocator(MainNav_e.QA_Field);
        public static By Owner = GetNavMenuByLocator(MainNav_e.Owner);
        public static By Material_Mix_Codes = GetNavMenuByLocator(MainNav_e.Material_Mix_Codes);
        public static By RM_Center = GetNavMenuByLocator(MainNav_e.RM_Center);
        public static By QA_Inbox = GetNavMenuByLocator(MainNav_e.QA_Inbox);
        public static By DOT_Inbox = GetNavMenuByLocator(MainNav_e.DOT_Inbox);
        public static By Owner_Inbox = GetNavMenuByLocator(MainNav_e.Owner_Inbox);
        public static By Dev_Inbox = GetNavMenuByLocator(MainNav_e.Dev_Inbox);
        public static By RFI = GetNavMenuByLocator(MainNav_e.RFI);
        public static By ELVIS = GetNavMenuByLocator(MainNav_e.ELVIS);
    }

    //Project Menu By Locators
    public class Project_By : MainNav_By
    {
        public static By My_Details = GetNavMenuByLocator(Project_eClass.Project_e.My_Details);
        public static By Qms_Document = GetNavMenuByLocator(Project_eClass.Project_e.Qms_Document);
        public static By Administration = GetNavMenuByLocator(Project_eClass.Project_e.Administration);

        //Project >Administration Menu By Locators
        public class Administration_By
        {
            public static By Project_Details = GetNavMenuByLocator(Project_eClass.Administration_e.Project_Details);
            public static By Companies = GetNavMenuByLocator(Project_eClass.Administration_e.Companies);
            public static By Contracts = GetNavMenuByLocator(Project_eClass.Administration_e.Contracts);
            public static By User_Management = GetNavMenuByLocator(Project_eClass.Administration_e.User_Management);
            public static By Menu_Editor = GetNavMenuByLocator(Project_eClass.Administration_e.Menu_Editor);
            public static By System_Configuration = GetNavMenuByLocator(Project_eClass.Administration_e.System_Configuration);
            public static By Admin_Tools = GetNavMenuByLocator(Project_eClass.Administration_e.Admin_Tools);

            //Project >Administration >>User Management Menu By Locators
            public class UserManagement_By
            {
                public static By Roles = GetNavMenuByLocator(Project_eClass.Admin_UserManagement_e.Roles);
                public static By Users = GetNavMenuByLocator(Project_eClass.Admin_UserManagement_e.Users);
                public static By Access_Rights = GetNavMenuByLocator(Project_eClass.Admin_UserManagement_e.Access_Rights);
            }

            //Project >Administration >>System Configuration Menu By Locators
            public class SystemConfiguration_By
            {
                public static By Disciplines = GetNavMenuByLocator(Project_eClass.Admin_SystemConfiguration_e.Disciplines);
                public static By Submittal_Actions = GetNavMenuByLocator(Project_eClass.Admin_SystemConfiguration_e.Submittal_Actions);
                public static By Submittal_Requirements = GetNavMenuByLocator(Project_eClass.Admin_SystemConfiguration_e.Submittal_Requirements);
                public static By Submittal_Types = GetNavMenuByLocator(Project_eClass.Admin_SystemConfiguration_e.Submittal_Types);
                public static By CVL_Lists = GetNavMenuByLocator(Project_eClass.Admin_SystemConfiguration_e.CVL_Lists);
                public static By CVL_Lists_Items = GetNavMenuByLocator(Project_eClass.Admin_SystemConfiguration_e.CVL_Lists_Items);
                public static By Notifications = GetNavMenuByLocator(Project_eClass.Admin_SystemConfiguration_e.Notifications);
                public static By Sieves = GetNavMenuByLocator(Project_eClass.Admin_SystemConfiguration_e.Sieves);
                public static By Gradations = GetNavMenuByLocator(Project_eClass.Admin_SystemConfiguration_e.Gradations);
                public static By Equipment = GetNavMenuByLocator(Project_eClass.Admin_SystemConfiguration_e.Equipment);
                public static By Grade_Management = GetNavMenuByLocator(Project_eClass.Admin_SystemConfiguration_e.Grade_Management);

                //Project >Administration >>System Configuration >>Equipment Menu By Locators
                public class Equipment_By
                {
                    public static By Equipment_Makes = GetNavMenuByLocator(Project_eClass.Admin_SysConfig_Equipment_e.Equipment_Makes);
                    public static By Equipment_Types = GetNavMenuByLocator(Project_eClass.Admin_SysConfig_Equipment_e.Equipment_Types);
                    public static By Equipment_Models = GetNavMenuByLocator(Project_eClass.Admin_SysConfig_Equipment_e.Equipment_Models);
                }

                //Project >Administration >>System Configuration >>Grade Management Menu By Locators
                public class GradeManagement_By
                {
                    public static By Grade_Types = GetNavMenuByLocator(Project_eClass.Admin_SysConfig_GradeManagement_e.Grade_Types);
                    public static By Grades = GetNavMenuByLocator(Project_eClass.Admin_SysConfig_GradeManagement_e.Grades);
                }

                public class AdminTools_By
                {
                    public static By Locked_Records = GetNavMenuByLocator(Project_eClass.Admin_AdminTools_e.Locked_Records);
                }
            }
        }
    }

    //QA Lab Menu By Locators
    public class QALab_By : MainNav_By
    {
        public static By Technician_Random = GetNavMenuByLocator(QALab_e.Technician_Random);
        public static By BreakSheet_Creation = GetNavMenuByLocator(QALab_e.BreakSheet_Creation);
        public static By BreakSheet_Legacy = GetNavMenuByLocator(QALab_e.BreakSheet_Legacy);
        public static By Equipment_Management = GetNavMenuByLocator(QALab_e.Equipment_Management);
    }

    //QA Record Control Menu By Locators
    public class QARecordControl_By : MainNav_By
    {
        public static By QA_Test_Original_Report = GetNavMenuByLocator(QARecordControl_e.QA_Test_Original_Report);
        public static By QA_Test_All = GetNavMenuByLocator(QARecordControl_e.QA_Test_All);
        public static By QA_Test_Correction_Report = GetNavMenuByLocator(QARecordControl_e.QA_Test_Correction_Report);
        public static By QA_DIRs = GetNavMenuByLocator(QARecordControl_e.QA_DIRs);
        public static By General_NCR = GetNavMenuByLocator(QARecordControl_e.General_NCR);
        public static By General_CDR = GetNavMenuByLocator(QARecordControl_e.General_CDR);
        public static By Retaining_Wall_BackFill_Quantity_Tracker = GetNavMenuByLocator(QARecordControl_e.Retaining_Wall_BackFill_Quantity_Tracker);
        public static By Concrete_Paving_Quantity_Tracker = GetNavMenuByLocator(QARecordControl_e.Concrete_Paving_Quantity_Tracker);
        public static By MPL_Tracker = GetNavMenuByLocator(QARecordControl_e.MPL_Tracker);
        public static By Girder_Tracker = GetNavMenuByLocator(QARecordControl_e.Girder_Tracker);
    }

    //QA Engineer Menu By Locators
    public class QAEngineer_By : MainNav_By
    {
        public static By QA_Test_Lab_Supervisor_Review = GetNavMenuByLocator(QAEngineer_e.QA_Test_Lab_Supervisor_Review);
        public static By QA_Test_Field_Supervisor_Review = GetNavMenuByLocator(QAEngineer_e.QA_Test_Field_Supervisor_Review);
        public static By QA_Test_Authorization = GetNavMenuByLocator(QAEngineer_e.QA_Test_Authorization);
        public static By DIR_QA_Review_Approval = GetNavMenuByLocator(QAEngineer_e.DIR_QA_Review_Approval);
    }

    //Reports & Notices Menu By Locators
    public class ReportNotices_By : MainNav_By
    {
        public static By General_NCR = GetNavMenuByLocator(ReportsNotices_e.General_NCR);
        public static By General_DN = GetNavMenuByLocator(ReportsNotices_e.General_DN);
    }

    //QA Search Menu By Locators
    public class QASearch_By : MainNav_By
    {
        public static By QA_Tests = GetNavMenuByLocator(QASearch_e.QA_Tests);
        public static By QA_Test_Summary_Search = GetNavMenuByLocator(QASearch_e.QA_Test_Summary_Search);
        public static By QA_Guide_Schedule_Summary_Report = GetNavMenuByLocator(QASearch_e.QA_Guide_Schedule_Summary_Report);
        public static By Inspection_Deficiency_Log_Report = GetNavMenuByLocator(QASearch_e.Inspection_Deficiency_Log_Report);
        public static By Daily_Inspection_Report = GetNavMenuByLocator(QASearch_e.Daily_Inspection_Report);
        public static By DIR_Summary_Report = GetNavMenuByLocator(QASearch_e.DIR_Summary_Report);
    }

    //QA Field Menu By Locators
    public class QAField_By : MainNav_By
    {
        public static By QA_Test = GetNavMenuByLocator(QAField_e.QA_Test);
        public static By QA_DIRs = GetNavMenuByLocator(QAField_e.QA_DIRs);
        public static By QA_Technician_Random_Search = GetNavMenuByLocator(QAField_e.QA_Technician_Random_Search);
    }

    //Owner Menu By Locators
    public class Owner_By : MainNav_By
    {
        public static By Owner_DIRs = GetNavMenuByLocator(Owner_e.Owner_DIRs);
        public static By Owner_NCRs = GetNavMenuByLocator(Owner_e.Owner_NCRs);
    }

    //Material/Mix Codes Menu By Locators
    public class MaterialMixCodes_By : MainNav_By
    {
        public static By Mix_Design_PCC = GetNavMenuByLocator(MaterialMixCodes_e.Mix_Design_PCC);
        public static By Mix_Design_HMA = GetNavMenuByLocator(MaterialMixCodes_e.Mix_Design_HMA);
        public static By Sieve_Analyses_JMF = GetNavMenuByLocator(MaterialMixCodes_e.Sieve_Analyses_JMF);
        public static By Sieve_Analyses_IOC = GetNavMenuByLocator(MaterialMixCodes_e.Sieve_Analyses_IOC);
        public static By Material_Code_Concrete_Aggregate = GetNavMenuByLocator(MaterialMixCodes_e.Material_Code_Concrete_Aggregate);
        public static By Material_Code_Base_Aggregate = GetNavMenuByLocator(MaterialMixCodes_e.Material_Code_Base_Aggregate);
        public static By Material_Code_HMA_Aggregate = GetNavMenuByLocator(MaterialMixCodes_e.Material_Code_HMA_Aggregate);
        public static By Material_Code_Raw_Material = GetNavMenuByLocator(MaterialMixCodes_e.Material_Code_Raw_Material);
    }

    //RM Center Menu By Locators
    public class RMCenter_By : MainNav_By
    {
        public static By Search = GetNavMenuByLocator(RMCenter_e.Search);
        public static By Upload_QA_Submittal = GetNavMenuByLocator(RMCenter_e.Upload_QA_Submittal);
        public static By Upload_Owner_Submittal = GetNavMenuByLocator(RMCenter_e.Upload_Owner_Submittal);
        public static By Upload_DEV_Submittal = GetNavMenuByLocator(RMCenter_e.Upload_DEV_Submittal);
        public static By DOT_Project_Correspondence_Log = GetNavMenuByLocator(RMCenter_e.DOT_Project_Correspondence_Log);
        public static By Review_Revise_Submittal = GetNavMenuByLocator(RMCenter_e.Review_Revise_Submittal);
        public static By RFC_Management = GetNavMenuByLocator(RMCenter_e.RFC_Management);
        public static By Project_Correspondence_Log = GetNavMenuByLocator(RMCenter_e.Project_Correspondence_Log);
        public static By Project_Transmittal_Log = GetNavMenuByLocator(RMCenter_e.Project_Transmittal_Log);
        public static By Comment_Summary = GetNavMenuByLocator(RMCenter_e.Comment_Summary);
    }

    //QA Inbox Menu By Locators
    public class QAInbox_By : MainNav_By
    {
        public static By Pending_Comments = GetNavMenuByLocator(QAInbox_e.Pending_Comments);
        public static By My_Inbox = GetNavMenuByLocator(QAInbox_e.My_Inbox);
    }

    //DOT Inbox Menu By Locators
    public class DOTInbox_By : MainNav_By
    {
        public static By Pending_Comments = GetNavMenuByLocator(DOTInbox_e.Pending_Comments);
    }

    //Owner Inbox Menu By Locators
    public class OwnerInbox_By : MainNav_By
    {
        public static By Pending_Comments = GetNavMenuByLocator(OwnerInbox_e.Pending_Comments);
    }

    //Dev Inbox Menu By Locators
    public class DevInbox_By : MainNav_By
    {
        public static By Pending_Comments = GetNavMenuByLocator(DevInbox_e.Pending_Comments);
        public static By Pending_Resolution = GetNavMenuByLocator(DevInbox_e.Pending_Resolution);
        public static By Pending_Comments_Other = GetNavMenuByLocator(DevInbox_e.Pending_Comments_Other);
    }

    //RFI Inbox Menu By Locators
    public class RFI_By : MainNav_By
    {
        public static By List = GetNavMenuByLocator(RFI_e.List);
        public static By Create = GetNavMenuByLocator(RFI_e.Create);
    }

    //ELVIS Inbox Menu By Locators
    public class ELVIS_By : MainNav_By
    {
        public static By About = GetNavMenuByLocator(ELVIS_e.About);
    }

}
