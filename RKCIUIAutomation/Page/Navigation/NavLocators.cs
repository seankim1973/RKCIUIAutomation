using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.Navigation
{
    //Main Menu By Locators
    public class MainNav_By : NavEnums
    {
        //Main By Locators
        public static By Project = SetATagLocatorXpath(MainNav_e.Project);
        public static By QA_Lab = SetATagLocatorXpath(MainNav_e.QA_Lab);
        public static By QA_RecordControl = SetATagLocatorXpath(MainNav_e.QA_Record_Control);
        public static By QA_Engineer = SetATagLocatorXpath(MainNav_e.QA_Engineer);
        public static By Reports_Notices = SetATagLocatorXpath(MainNav_e.Reports_Notices);
        public static By QA_Search = SetATagLocatorXpath(MainNav_e.QA_Search);
        public static By QA_Field = SetATagLocatorXpath(MainNav_e.QA_Field);
        public static By Owner = SetATagLocatorXpath(MainNav_e.Owner);
        public static By Material_Mix_Codes = SetATagLocatorXpath(MainNav_e.Material_Mix_Codes);
        public static By RM_Center = SetATagLocatorXpath(MainNav_e.RM_Center);
        public static By QA_Inbox = SetATagLocatorXpath(MainNav_e.QA_Inbox);
        public static By DOT_Inbox = SetATagLocatorXpath(MainNav_e.DOT_Inbox);
        public static By Owner_Inbox = SetATagLocatorXpath(MainNav_e.Owner_Inbox);
        public static By Dev_Inbox = SetATagLocatorXpath(MainNav_e.Dev_Inbox);
        public static By RFI = SetATagLocatorXpath(MainNav_e.RFI);
        public static By ELVIS = SetATagLocatorXpath(MainNav_e.ELVIS);
    }

    //Project Menu By Locators
    public class Project_By : MainNav_By
    {
        public static By My_Details = SetATagLocatorXpath(Project_eClass.Project_e.My_Details);
        public static By Qms_Document = SetATagLocatorXpath(Project_eClass.Project_e.Qms_Document);
        public static By Administration = SetATagLocatorXpath(Project_eClass.Project_e.Administration);

        //Project >Administration Menu By Locators
        public class Administration_By
        {
            public static By Project_Details = SetATagLocatorXpath(Project_eClass.Administration_e.Project_Details);
            public static By Companies = SetATagLocatorXpath(Project_eClass.Administration_e.Companies);
            public static By Contracts = SetATagLocatorXpath(Project_eClass.Administration_e.Contracts);
            public static By User_Management = SetATagLocatorXpath(Project_eClass.Administration_e.User_Management);
            public static By Menu_Editor = SetATagLocatorXpath(Project_eClass.Administration_e.Menu_Editor);
            public static By System_Configuration = SetATagLocatorXpath(Project_eClass.Administration_e.System_Configuration);
            public static By Admin_Tools = SetATagLocatorXpath(Project_eClass.Administration_e.Admin_Tools);

            //Project >Administration >>User Management Menu By Locators
            public class UserManagement_By
            {
                public static By Roles = SetATagLocatorXpath(Project_eClass.Admin_UserManagement_e.Roles);
                public static By Users = SetATagLocatorXpath(Project_eClass.Admin_UserManagement_e.Users);
                public static By Access_Rights = SetATagLocatorXpath(Project_eClass.Admin_UserManagement_e.Access_Rights);
            }

            //Project >Administration >>System Configuration Menu By Locators
            public class SystemConfiguration_By
            {
                public static By Disciplines = SetATagLocatorXpath(Project_eClass.Admin_SystemConfiguration_e.Disciplines);
                public static By Submittal_Actions = SetATagLocatorXpath(Project_eClass.Admin_SystemConfiguration_e.Submittal_Actions);
                public static By Submittal_Requirements = SetATagLocatorXpath(Project_eClass.Admin_SystemConfiguration_e.Submittal_Requirements);
                public static By Submittal_Types = SetATagLocatorXpath(Project_eClass.Admin_SystemConfiguration_e.Submittal_Types);
                public static By CVL_Lists = SetATagLocatorXpath(Project_eClass.Admin_SystemConfiguration_e.CVL_Lists);
                public static By CVL_Lists_Items = SetATagLocatorXpath(Project_eClass.Admin_SystemConfiguration_e.CVL_Lists_Items);
                public static By Notifications = SetATagLocatorXpath(Project_eClass.Admin_SystemConfiguration_e.Notifications);
                public static By Sieves = SetATagLocatorXpath(Project_eClass.Admin_SystemConfiguration_e.Sieves);
                public static By Gradations = SetATagLocatorXpath(Project_eClass.Admin_SystemConfiguration_e.Gradations);
                public static By Equipment = SetATagLocatorXpath(Project_eClass.Admin_SystemConfiguration_e.Equipment);
                public static By Grade_Management = SetATagLocatorXpath(Project_eClass.Admin_SystemConfiguration_e.Grade_Management);

                //Project >Administration >>System Configuration >>Equipment Menu By Locators
                public class Equipment_By
                {
                    public static By Equipment_Makes = SetATagLocatorXpath(Project_eClass.Admin_SysConfig_Equipment_e.Equipment_Makes);
                    public static By Equipment_Types = SetATagLocatorXpath(Project_eClass.Admin_SysConfig_Equipment_e.Equipment_Types);
                    public static By Equipment_Models = SetATagLocatorXpath(Project_eClass.Admin_SysConfig_Equipment_e.Equipment_Models);
                }

                //Project >Administration >>System Configuration >>Grade Management Menu By Locators
                public class GradeManagement_By
                {
                    public static By Grade_Types = SetATagLocatorXpath(Project_eClass.Admin_SysConfig_GradeManagement_e.Grade_Types);
                    public static By Grades = SetATagLocatorXpath(Project_eClass.Admin_SysConfig_GradeManagement_e.Grades);
                }

                public class AdminTools_By
                {
                    public static By Locked_Records = SetATagLocatorXpath(Project_eClass.Admin_AdminTools_e.Locked_Records);
                }
            }
        }
    }

    //QA Lab Menu By Locators
    public class QALab_By : MainNav_By
    {
        public static By Technician_Random = SetATagLocatorXpath(QALab_e.Technician_Random);
        public static By BreakSheet_Creation = SetATagLocatorXpath(QALab_e.BreakSheet_Creation);
        public static By BreakSheet_Legacy = SetATagLocatorXpath(QALab_e.BreakSheet_Legacy);
        public static By Equipment_Management = SetATagLocatorXpath(QALab_e.Equipment_Management);
    }

    //QA Record Control Menu By Locators
    public class QARecordControl_By : MainNav_By
    {
        public static By QA_Test_Original_Report = SetATagLocatorXpath(QARecordControl_e.QA_Test_Original_Report);
        public static By QA_Test_All = SetATagLocatorXpath(QARecordControl_e.QA_Test_All);
        public static By QA_Test_Correction_Report = SetATagLocatorXpath(QARecordControl_e.QA_Test_Correction_Report);
        public static By QA_DIRs = SetATagLocatorXpath(QARecordControl_e.QA_DIRs);
        public static By General_NCR = SetATagLocatorXpath(QARecordControl_e.General_NCR);
        public static By General_CDR = SetATagLocatorXpath(QARecordControl_e.General_CDR);
        public static By Retaining_Wall_BackFill_Quantity_Tracker = SetATagLocatorXpath(QARecordControl_e.Retaining_Wall_BackFill_Quantity_Tracker);
        public static By Concrete_Paving_Quantity_Tracker = SetATagLocatorXpath(QARecordControl_e.Concrete_Paving_Quantity_Tracker);
        public static By MPL_Tracker = SetATagLocatorXpath(QARecordControl_e.MPL_Tracker);
        public static By Girder_Tracker = SetATagLocatorXpath(QARecordControl_e.Girder_Tracker);
    }

    //QA Engineer Menu By Locators
    public class QAEngineer_By : MainNav_By
    {
        public static By QA_Test_Lab_Supervisor_Review = SetATagLocatorXpath(QAEngineer_e.QA_Test_Lab_Supervisor_Review);
        public static By QA_Test_Field_Supervisor_Review = SetATagLocatorXpath(QAEngineer_e.QA_Test_Field_Supervisor_Review);
        public static By QA_Test_Authorization = SetATagLocatorXpath(QAEngineer_e.QA_Test_Authorization);
        public static By DIR_QA_Review_Approval = SetATagLocatorXpath(QAEngineer_e.DIR_QA_Review_Approval);
    }

    //Reports & Notices Menu By Locators
    public class ReportNotices_By : MainNav_By
    {
        public static By General_NCR = SetATagLocatorXpath(ReportsNotices_e.General_NCR);
        public static By General_DN = SetATagLocatorXpath(ReportsNotices_e.General_DN);
    }

    //QA Search Menu By Locators
    public class QASearch_By : MainNav_By
    {
        public static By QA_Tests = SetATagLocatorXpath(QASearch_e.QA_Tests);
        public static By QA_Test_Summary_Search = SetATagLocatorXpath(QASearch_e.QA_Test_Summary_Search);
        public static By QA_Guide_Schedule_Summary_Report = SetATagLocatorXpath(QASearch_e.QA_Guide_Schedule_Summary_Report);
        public static By Inspection_Deficiency_Log_Report = SetATagLocatorXpath(QASearch_e.Inspection_Deficiency_Log_Report);
        public static By Daily_Inspection_Report = SetATagLocatorXpath(QASearch_e.Daily_Inspection_Report);
        public static By DIR_Summary_Report = SetATagLocatorXpath(QASearch_e.DIR_Summary_Report);
    }

    //QA Field Menu By Locators
    public class QAField_By : MainNav_By
    {
        public static By QA_Test = SetATagLocatorXpath(QAField_e.QA_Test);
        public static By QA_DIRs = SetATagLocatorXpath(QAField_e.QA_DIRs);
        public static By QA_Technician_Random_Search = SetATagLocatorXpath(QAField_e.QA_Technician_Random_Search);
    }

    //Owner Menu By Locators
    public class Owner_By : MainNav_By
    {
        public static By Owner_DIRs = SetATagLocatorXpath(Owner_e.Owner_DIRs);
        public static By Owner_NCRs = SetATagLocatorXpath(Owner_e.Owner_NCRs);
    }

    //Material/Mix Codes Menu By Locators
    public class MaterialMixCodes_By : MainNav_By
    {
        public static By Mix_Design_PCC = SetATagLocatorXpath(MaterialMixCodes_e.Mix_Design_PCC);
        public static By Mix_Design_HMA = SetATagLocatorXpath(MaterialMixCodes_e.Mix_Design_HMA);
        public static By Sieve_Analyses_JMF = SetATagLocatorXpath(MaterialMixCodes_e.Sieve_Analyses_JMF);
        public static By Sieve_Analyses_IOC = SetATagLocatorXpath(MaterialMixCodes_e.Sieve_Analyses_IOC);
        public static By Material_Code_Concrete_Aggregate = SetATagLocatorXpath(MaterialMixCodes_e.Material_Code_Concrete_Aggregate);
        public static By Material_Code_Base_Aggregate = SetATagLocatorXpath(MaterialMixCodes_e.Material_Code_Base_Aggregate);
        public static By Material_Code_HMA_Aggregate = SetATagLocatorXpath(MaterialMixCodes_e.Material_Code_HMA_Aggregate);
        public static By Material_Code_Raw_Material = SetATagLocatorXpath(MaterialMixCodes_e.Material_Code_Raw_Material);
    }

    //RM Center Menu By Locators
    public class RMCenter_By : MainNav_By
    {
        public static By Search = SetATagLocatorXpath(RMCenter_e.Search);
        public static By Upload_QA_Submittal = SetATagLocatorXpath(RMCenter_e.Upload_QA_Submittal);
        public static By Upload_Owner_Submittal = SetATagLocatorXpath(RMCenter_e.Upload_Owner_Submittal);
        public static By Upload_DEV_Submittal = SetATagLocatorXpath(RMCenter_e.Upload_DEV_Submittal);
        public static By DOT_Project_Correspondence_Log = SetATagLocatorXpath(RMCenter_e.DOT_Project_Correspondence_Log);
        public static By Review_Revise_Submittal = SetATagLocatorXpath(RMCenter_e.Review_Revise_Submittal);
        public static By RFC_Management = SetATagLocatorXpath(RMCenter_e.RFC_Management);
        public static By Project_Correspondence_Log = SetATagLocatorXpath(RMCenter_e.Project_Correspondence_Log);
        public static By Project_Transmittal_Log = SetATagLocatorXpath(RMCenter_e.Project_Transmittal_Log);
        public static By Comment_Summary = SetATagLocatorXpath(RMCenter_e.Comment_Summary);
    }

    //QA Inbox Menu By Locators
    public class QAInbox_By : MainNav_By
    {
        public static By Pending_Comments = SetATagLocatorXpath(QAInbox_e.Pending_Comments);
        public static By My_Inbox = SetATagLocatorXpath(QAInbox_e.My_Inbox);
    }

    //DOT Inbox Menu By Locators
    public class DOTInbox_By : MainNav_By
    {
        public static By Pending_Comments = SetATagLocatorXpath(DOTInbox_e.Pending_Comments);
    }

    //Owner Inbox Menu By Locators
    public class OwnerInbox_By : MainNav_By
    {
        public static By Pending_Comments = SetATagLocatorXpath(OwnerInbox_e.Pending_Comments);
    }

    //Dev Inbox Menu By Locators
    public class DevInbox_By : MainNav_By
    {
        public static By Pending_Comments = SetATagLocatorXpath(DevInbox_e.Pending_Comments);
        public static By Pending_Resolution = SetATagLocatorXpath(DevInbox_e.Pending_Resolution);
        public static By Pending_Comments_Other = SetATagLocatorXpath(DevInbox_e.Pending_Comments_Other);
    }

    //RFI Inbox Menu By Locators
    public class RFI_By : MainNav_By
    {
        public static By List = SetATagLocatorXpath(RFI_e.List);
        public static By Create = SetATagLocatorXpath(RFI_e.Create);
    }

    //ELVIS Inbox Menu By Locators
    public class ELVIS_By : MainNav_By
    {
        public static By About = SetATagLocatorXpath(ELVIS_e.About);
    }

}
