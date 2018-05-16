using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.Navigation
{
    //Main Menu By Locators
    public class MainNav_By : NavEnums
    {
        //Main By Locators
        public static By Project = SetLocatorXpath(MainNav_e.Project);
        public static By QA_Lab = SetLocatorXpath(MainNav_e.QA_Lab);
        public static By QA_RecordControl = SetLocatorXpath(MainNav_e.QA_Record_Control);
        public static By QA_Engineer = SetLocatorXpath(MainNav_e.QA_Engineer);
        public static By Reports_Notices = SetLocatorXpath(MainNav_e.Reports_Notices);
        public static By QA_Search = SetLocatorXpath(MainNav_e.QA_Search);
        public static By QA_Field = SetLocatorXpath(MainNav_e.QA_Field);
        public static By Owner = SetLocatorXpath(MainNav_e.Owner);
        public static By Material_Mix_Codes = SetLocatorXpath(MainNav_e.Material_Mix_Codes);
        public static By RM_Center = SetLocatorXpath(MainNav_e.RM_Center);
        public static By QA_Inbox = SetLocatorXpath(MainNav_e.QA_Inbox);
        public static By DOT_Inbox = SetLocatorXpath(MainNav_e.DOT_Inbox);
        public static By Owner_Inbox = SetLocatorXpath(MainNav_e.Owner_Inbox);
        public static By Dev_Inbox = SetLocatorXpath(MainNav_e.Dev_Inbox);
        public static By RFI = SetLocatorXpath(MainNav_e.RFI);
        public static By ELVIS = SetLocatorXpath(MainNav_e.ELVIS);
    }

    //Project Menu By Locators
    public class Project_By : MainNav_By
    {
        public static By My_Details = SetLocatorXpath(Project_e.My_Details);
        public static By Qms_Document = SetLocatorXpath(Project_e.Qms_Document);
        public static By Administration = SetLocatorXpath(Project_e.Administration);

        //Project >Administration Menu By Locators
        public class Administration_By
        {
            public static By Project_Details = SetLocatorXpath(Proj_Administration_e.Project_Details);
            public static By Companies = SetLocatorXpath(Proj_Administration_e.Companies);
            public static By Contracts = SetLocatorXpath(Proj_Administration_e.Contracts);
            public static By User_Management = SetLocatorXpath(Proj_Administration_e.User_Management);
            public static By Menu_Editor = SetLocatorXpath(Proj_Administration_e.Menu_Editor);
            public static By System_Configuration = SetLocatorXpath(Proj_Administration_e.System_Configuration);

            //Project >Administration >>User Management Menu By Locators
            public class UserManagement_By
            {
                public static By Roles = SetLocatorXpath(Proj_Admin_UserManagement_e.Roles);
                public static By Users = SetLocatorXpath(Proj_Admin_UserManagement_e.Users);
                public static By Access_Rights = SetLocatorXpath(Proj_Admin_UserManagement_e.Access_Rights);
            }

            //Project >Administration >>System Configuration Menu By Locators
            public class SystemConfiguration_By
            {
                public static By Disciplines = SetLocatorXpath(Proj_Admin_SystemConfiguration_e.Disciplines);
                public static By Submittal_Actions = SetLocatorXpath(Proj_Admin_SystemConfiguration_e.Submittal_Actions);
                public static By Submittal_Requirements = SetLocatorXpath(Proj_Admin_SystemConfiguration_e.Submittal_Requirements);
                public static By Submittal_Types = SetLocatorXpath(Proj_Admin_SystemConfiguration_e.Submittal_Types);
                public static By CVL_Lists = SetLocatorXpath(Proj_Admin_SystemConfiguration_e.CVL_Lists);
                public static By CVL_Lists_Items = SetLocatorXpath(Proj_Admin_SystemConfiguration_e.CVL_Lists_Items);
                public static By Notifications = SetLocatorXpath(Proj_Admin_SystemConfiguration_e.Notifications);
                public static By Sieves = SetLocatorXpath(Proj_Admin_SystemConfiguration_e.Sieves);
                public static By Gradations = SetLocatorXpath(Proj_Admin_SystemConfiguration_e.Gradations);
                public static By Equipment = SetLocatorXpath(Proj_Admin_SystemConfiguration_e.Equipment);
                public static By Grade_Management = SetLocatorXpath(Proj_Admin_SystemConfiguration_e.Grade_Management);

                //Project >Administration >>System Configuration >>Equipment Menu By Locators
                public class Equipment_By
                {
                    public static By Equipment_Makes = SetLocatorXpath(Proj_Admin_SysConfig_Equipment_e.Equipment_Makes);
                    public static By Equipment_Types = SetLocatorXpath(Proj_Admin_SysConfig_Equipment_e.Equipment_Types);
                    public static By Equipment_Models = SetLocatorXpath(Proj_Admin_SysConfig_Equipment_e.Equipment_Models);
                }

                //Project >Administration >>System Configuration >>Grade Management Menu By Locators
                public class GradeManagement_By
                {
                    public static By Grade_Types = SetLocatorXpath(Proj_Admin_SysConfig_GradeManagement_e.Grade_Types);
                    public static By Grades = SetLocatorXpath(Proj_Admin_SysConfig_GradeManagement_e.Grades);
                }
            }
        }
    }

    //QA Lab Menu By Locators
    public class QALab_By : MainNav_By
    {
        public static By Technician_Random = SetLocatorXpath(QALab_e.Technician_Random);
        public static By BreakSheet_Creation = SetLocatorXpath(QALab_e.BreakSheet_Creation);
        public static By BreakSheet_Legacy = SetLocatorXpath(QALab_e.BreakSheet_Legacy);
        public static By Equipment_Management = SetLocatorXpath(QALab_e.Equipment_Management);
    }

    //QA Record Control Menu By Locators
    public class QARecordControl_By : MainNav_By
    {
        public static By QA_Test_Original_Report = SetLocatorXpath(QARecordControl_e.QA_Test_Original_Report);
        public static By QA_Test_All = SetLocatorXpath(QARecordControl_e.QA_Test_All);
        public static By QA_Test_Correction_Report = SetLocatorXpath(QARecordControl_e.QA_Test_Correction_Report);
        public static By QA_DIRs = SetLocatorXpath(QARecordControl_e.QA_DIRs);
        public static By General_NCR = SetLocatorXpath(QARecordControl_e.General_NCR);
        public static By General_CDR = SetLocatorXpath(QARecordControl_e.General_CDR);
        public static By Retaining_Wall_BackFill_Quantity_Tracker = SetLocatorXpath(QARecordControl_e.Retaining_Wall_BackFill_Quantity_Tracker);
        public static By Concrete_Paving_Quantity_Tracker = SetLocatorXpath(QARecordControl_e.Concrete_Paving_Quantity_Tracker);
        public static By MPL_Tracker = SetLocatorXpath(QARecordControl_e.MPL_Tracker);
        public static By Girder_Tracker = SetLocatorXpath(QARecordControl_e.Girder_Tracker);
    }

    //QA Engineer Menu By Locators
    public class QAEngineer_By : MainNav_By
    {
        public static By QA_Test_Lab_Supervisor_Review = SetLocatorXpath(QAEngineer_e.QA_Test_Lab_Supervisor_Review);
        public static By QA_Test_Field_Supervisor_Review = SetLocatorXpath(QAEngineer_e.QA_Test_Field_Supervisor_Review);
        public static By QA_Test_Authorization = SetLocatorXpath(QAEngineer_e.QA_Test_Authorization);
        public static By DIR_QA_Review_Approval = SetLocatorXpath(QAEngineer_e.DIR_QA_Review_Approval);
    }

    //Reports & Notices Menu By Locators
    public class ReportNotices_By : MainNav_By
    {
        public static By General_NCR = SetLocatorXpath(ReportsNotices_e.General_NCR);
        public static By General_DN = SetLocatorXpath(ReportsNotices_e.General_DN);
    }

    //QA Search Menu By Locators
    public class QASearch_By : MainNav_By
    {
        public static By QA_Tests = SetLocatorXpath(QASearch_e.QA_Tests);
        public static By QA_Test_Summary_Search = SetLocatorXpath(QASearch_e.QA_Test_Summary_Search);
        public static By QA_Guide_Schedule_Summary_Report = SetLocatorXpath(QASearch_e.QA_Guide_Schedule_Summary_Report);
        public static By Inspection_Deficiency_Log_Report = SetLocatorXpath(QASearch_e.Inspection_Deficiency_Log_Report);
        public static By Daily_Inspection_Report = SetLocatorXpath(QASearch_e.Daily_Inspection_Report);
        public static By DIR_Summary_Report = SetLocatorXpath(QASearch_e.DIR_Summary_Report);
    }

    //QA Field Menu By Locators
    public class QAField_By : MainNav_By
    {
        public static By QA_Test = SetLocatorXpath(QAField_e.QA_Test);
        public static By QA_DIRs = SetLocatorXpath(QAField_e.QA_DIRs);
        public static By QA_Technician_Random_Search = SetLocatorXpath(QAField_e.QA_Technician_Random_Search);
    }

    //Owner Menu By Locators
    public class Owner_By : MainNav_By
    {
        public static By Owner_DIRs = SetLocatorXpath(Owner_e.Owner_DIRs);
        public static By Owner_NCRs = SetLocatorXpath(Owner_e.Owner_NCRs);
    }

    //Material/Mix Codes Menu By Locators
    public class MaterialMixCodes_By : MainNav_By
    {
        public static By Mix_Design_PCC = SetLocatorXpath(MaterialMixCodes_e.Mix_Design_PCC);
        public static By Mix_Design_HMA = SetLocatorXpath(MaterialMixCodes_e.Mix_Design_HMA);
        public static By Sieve_Analyses_JMF = SetLocatorXpath(MaterialMixCodes_e.Sieve_Analyses_JMF);
        public static By Sieve_Analyses_IOC = SetLocatorXpath(MaterialMixCodes_e.Sieve_Analyses_IOC);
        public static By Material_Code_Concrete_Aggregate = SetLocatorXpath(MaterialMixCodes_e.Material_Code_Concrete_Aggregate);
        public static By Material_Code_Base_Aggregate = SetLocatorXpath(MaterialMixCodes_e.Material_Code_Base_Aggregate);
        public static By Material_Code_HMA_Aggregate = SetLocatorXpath(MaterialMixCodes_e.Material_Code_HMA_Aggregate);
        public static By Material_Code_Raw_Material = SetLocatorXpath(MaterialMixCodes_e.Material_Code_Raw_Material);
    }

    //RM Center Menu By Locators
    public class RMCenter_By : MainNav_By
    {
        public static By Search = SetLocatorXpath(RMCenter_e.Search);
        public static By Upload_QA_Submittal = SetLocatorXpath(RMCenter_e.Upload_QA_Submittal);
        public static By Upload_Owner_Submittal = SetLocatorXpath(RMCenter_e.Upload_Owner_Submittal);
        public static By Upload_DEV_Submittal = SetLocatorXpath(RMCenter_e.Upload_DEV_Submittal);
        public static By DOT_Project_Correspondence_Log = SetLocatorXpath(RMCenter_e.DOT_Project_Correspondence_Log);
        public static By Review_Revise_Submittal = SetLocatorXpath(RMCenter_e.Review_Revise_Submittal);
        public static By RFC_Management = SetLocatorXpath(RMCenter_e.RFC_Management);
        public static By Project_Correspondence_Log = SetLocatorXpath(RMCenter_e.Project_Correspondence_Log);
        public static By Project_Transmittal_Log = SetLocatorXpath(RMCenter_e.Project_Transmittal_Log);
        public static By Comment_Summary = SetLocatorXpath(RMCenter_e.Comment_Summary);
    }

    //QA Inbox Menu By Locators
    public class QAInbox_By : MainNav_By
    {
        public static By Pending_Comments = SetLocatorXpath(QAInbox_e.Pending_Comments);
        public static By My_Inbox = SetLocatorXpath(QAInbox_e.My_Inbox);
    }

    //DOT Inbox Menu By Locators
    public class DOTInbox_By : MainNav_By
    {
        public static By Pending_Comments = SetLocatorXpath(DOTInbox_e.Pending_Comments);
    }

    //Owner Inbox Menu By Locators
    public class OwnerInbox_By : MainNav_By
    {
        public static By Pending_Comments = SetLocatorXpath(OwnerInbox_e.Pending_Comments);
    }

    //Dev Inbox Menu By Locators
    public class DevInbox_By : MainNav_By
    {
        public static By Pending_Comments = SetLocatorXpath(DevInbox_e.Pending_Comments);
        public static By Pending_Resolution = SetLocatorXpath(DevInbox_e.Pending_Resolution);
        public static By Pending_Comments_Other = SetLocatorXpath(DevInbox_e.Pending_Comments_Other);
    }

    //RFI Inbox Menu By Locators
    public class RFI_By : MainNav_By
    {
        public static By List = SetLocatorXpath(RFI_e.List);
        public static By Create = SetLocatorXpath(RFI_e.Create);
    }

    //ELVIS Inbox Menu By Locators
    public class ELVIS_By : MainNav_By
    {
        public static By About = SetLocatorXpath(ELVIS_e.About);
    }

}
