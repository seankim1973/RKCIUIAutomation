using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.Navigation
{
    public class Navigation_Impl : Navigation, INavigation
    {
        internal NavMenu Navigate = new NavMenu(Driver);

        //Project Menu
        public override void My_Details() => Navigate.Menu(NavMenu.Project.Menu.My_Details);

        //Project>Administration
        public override void Admin_Companies() => Navigate.Menu(NavMenu.Project.Administration.Menu.Companies);
        public override void Admin_Contracts() => Navigate.Menu(NavMenu.Project.Administration.Menu.Contracts);
        public override void Admin_Menu_Editor() => Navigate.Menu(NavMenu.Project.Administration.Menu.Menu_Editor);
        public override void Admin_Project_Details() => Navigate.Menu(NavMenu.Project.Administration.Menu.Project_Details);
        public override void Admin_Suport() => Navigate.Menu(NavMenu.Project.Administration.Menu.Support);

        //Project>Admin Tools
        public override void AdminTools_Locked_Records() => Navigate.Menu(NavMenu.Project.Administration.AdminTools.Menu.Locked_Records);

        //Project>Administration>User Management
        public override void UserMgmt_Access_Rights() => Navigate.Menu(NavMenu.Project.Administration.UserManagement.Menu.Access_Rights);
        public override void UserMgmt_Roles() => Navigate.Menu(NavMenu.Project.Administration.UserManagement.Menu.Roles);
        public override void UserMgmt_Users() => Navigate.Menu(NavMenu.Project.Administration.UserManagement.Menu.Users);

        //Project>Administration>System Configuration
        public override void SysConfig_CVL_Lists() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.CVL_Lists);
        public override void SysConfig_CVL_List_Items() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.CVL_List_Items);
        public override void SysConfig_Disciplines() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Disciplines);
        public override void SysConfig_Gradations() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Gradations);
        public override void SysConfig_Notifications() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Notifications);
        public override void SysConfig_Sieves() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Sieves);
        public override void SysConfig_Submittal_Actions() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Submittal_Actions);
        public override void SysConfig_Submittal_Requirements() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Submittal_Requirements);
        public override void SysConfig_Submittal_Types() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Submittal_Types);

        //Project>Administration>System Configuration>Equipment
        public override void SysConfig_Equipment_Makes() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Makes);
        public override void SysConfig_Equipment_Models() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Models);
        public override void SysConfig_Equipment_Types() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Types);

        //Project>Administration>System Configuration>Grade Management
        public override void SysConfig_Grades() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.GradeManagement.Menu.Grades);
        public override void SysConfig_Grade_Types() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.GradeManagement.Menu.Grade_Types);

        //Control Point Menu
        public override void Control_Point_Log() => Navigate.Menu(NavMenu.ControlPoint.Menu.Control_Point_Log);
        public override void Control_Point_Scheduler() => Navigate.Menu(NavMenu.ControlPoint.Menu.Control_Point_Scheduler);

        //Material Mix Codes Menu
        public override void MaterialMixCodes_Material_Code_Base_Aggregate() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Base_Aggregate);
        public override void MaterialMixCodes_Material_Code_Concrete_Aggregate() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Concrete_Aggregate);
        public override void MaterialMixCodes_Material_Code_HMA_Aggregate() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_HMA_Aggregate);
        public override void MaterialMixCodes_Material_Code_Raw_Material() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Raw_Material);
        public override void MaterialMixCodes_Mix_Design_HMA() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Mix_Design_HMA);
        public override void MaterialMixCodes_Mix_Design_PCC() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Mix_Design_PCC);
        public override void MaterialMixCodes_Sieve_Analyses_IOC() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Sieve_Analyses_IOC);    
        public override void MaterialMixCodes_Sieve_Analyses_JMF() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Sieve_Analyses_JMF);

        //Owner Menu
        public override void Owner_DIRs() => Navigate.Menu(NavMenu.Owner.Menu.Owner_DIRs);
        public override void Owner_NCRs() => Navigate.Menu(NavMenu.Owner.Menu.Owner_NCRs);

        //QA Field Menu
        public override void QAField_Daily_Environmental_Inspection() => Navigate.Menu(NavMenu.QAField.Menu.Daily_Environmental_Inspection);
        public override void QAField_QA_DIRs() => Navigate.Menu(NavMenu.QAField.Menu.QA_DIRs);
        public override void QAField_QA_Technician_Random_Search() => Navigate.Menu(NavMenu.QAField.Menu.QA_Technician_Random_Search);
        public override void QAField_QA_Test() => Navigate.Menu(NavMenu.QAField.Menu.QA_Test);
        public override void QAField_Weekly_Environmental_Inspection() => Navigate.Menu(NavMenu.QAField.Menu.Weekly_Environmental_Inspection);
        public override void QAField_Weekly_Environmental_Monitoring() => Navigate.Menu(NavMenu.QAField.Menu.Weekly_Environmental_Monitoring);

        //QA Lab Menu
        public override void QALab_BreakSheet_Creation() => Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Creation);
        public override void QALab_BreakSheet_Legacy() => Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Legacy);
        public override void QALab_Equipment_Management() => Navigate.Menu(NavMenu.QALab.Menu.Equipment_Management);
        public override void QALab_Technician_Random() => Navigate.Menu(NavMenu.QALab.Menu.Technician_Random);

        //QA Search
        public override void QASearch_Daily_Inspection_Report() => Navigate.Menu(NavMenu.QASearch.Menu.Daily_Inspection_Report);
        public override void QASearch_DIR_Checklist_Search() => Navigate.Menu(NavMenu.QASearch.Menu.DIR_Checklist_Search);
        public override void QASearch_DIR_Summary_Report() => Navigate.Menu(NavMenu.QASearch.Menu.DIR_Summary_Report);
        public override void QASearch_Inspection_Deficiency_Log_Report() => Navigate.Menu(NavMenu.QASearch.Menu.Inspection_Deficiency_Log_Report);
        public override void QASearch_Ncr_Log_View() => Navigate.Menu(NavMenu.QASearch.Menu.Ncr_Log_View);
        public override void QASearch_QA_Guide_Schedule_Summary_Report() => Navigate.Menu(NavMenu.QASearch.Menu.QA_Guide_Schedule_Summary_Report);
        public override void QASearch_QA_Tests() => Navigate.Menu(NavMenu.QASearch.Menu.QA_Tests);
        public override void QASearch_QA_Test_Summary_Search() => Navigate.Menu(NavMenu.QASearch.Menu.QA_Test_Summary_Search);
        public override void QASearch_QMS_Document_Search() => Navigate.Menu(NavMenu.QASearch.Menu.QMS_Document_Search);


        public override void Qms_Document() => Navigate.Menu(NavMenu.Project.Menu.Qms_Document);

        //SG >QA Record Control>Qms Document, >QA Search>QMS Document Search
        //SH249 >Project>QMS Documents, >QA Search>QMS Document Search
        //I15Tech, I15South, GLX, Garnet >Project>Qms Document -no QMS Document Search menu item


        //Reports & Notices Menu
        public override void ReportsNotices_General_DN() => Navigate.Menu(NavMenu.ReportsNotices.Menu.General_DN);
        public override void ReportsNotices_General_NCR() => Navigate.Menu(NavMenu.ReportsNotices.Menu.General_NCR);

        //RM Center Menu
        public override void RMCenter_Comment_Summary() => Navigate.Menu(NavMenu.RMCenter.Menu.Comment_Summary);
        public override void RMCenter_Design_Documents() => Navigate.Menu(NavMenu.RMCenter.Menu.Design_Documents);
        public override void RMCenter_DOT_Project_Correspondence_Log() => Navigate.Menu(NavMenu.RMCenter.Menu.DOT_Project_Correspondence_Log);
        public override void RMCenter_Project_Correspondence_Log() => Navigate.Menu(NavMenu.RMCenter.Menu.Project_Correspondence_Log);
        public override void RMCenter_Project_Transmittal_Log() => Navigate.Menu(NavMenu.RMCenter.Menu.Project_Transmittal_Log);
        public override void RMCenter_Review_Revise_Submittal() => Navigate.Menu(NavMenu.RMCenter.Menu.Review_Revise_Submittal);
        public override void RMCenter_RFC_Management() => Navigate.Menu(NavMenu.RMCenter.Menu.RFC_Management);
        public override void RMCenter_Search() => Navigate.Menu(NavMenu.RMCenter.Menu.Search);
        public override void RMCenter_Upload_DEV_Submittal() => Navigate.Menu(NavMenu.RMCenter.Menu.Upload_DEV_Submittal);
        public override void RMCenter_Upload_Owner_Submittal() => Navigate.Menu(NavMenu.RMCenter.Menu.Upload_Owner_Submittal);
        public override void RMCenter_Upload_QA_Submittal() => Navigate.Menu(NavMenu.RMCenter.Menu.Upload_QA_Submittal);

    }

    public class Navigation_SGWay : Navigation_Impl
    {
        public override void Qms_Document() => Navigate.Menu(NavMenu.QARecordControl.Menu.Qms_Document);
    }


    public class Navigation_SH249 : Navigation_Impl
    {
        public override void Qms_Document() => Navigate.Menu(NavMenu.Project.Menu.QMS_Documents);

    }

}
