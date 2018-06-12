using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.Navigation
{
    public abstract class PageNavigation : PageBase, IPageNavigation
    {
        //public abstract void AdminTools_Locked_Records();
        //public abstract void Admin_Companies();
        //public abstract void Admin_Contracts();
        //public abstract void Admin_Menu_Editor();
        //public abstract void Admin_Project_Details();
        //public abstract void Admin_Suport();
        //public abstract void Control_Point_Log();
        //public abstract void Control_Point_Scheduler();
        //public abstract void MaterialMixCodes_Material_Code_Base_Aggregate();
        //public abstract void MaterialMixCodes_Material_Code_Concrete_Aggregate();
        //public abstract void MaterialMixCodes_Material_Code_HMA_Aggregate();
        //public abstract void MaterialMixCodes_Material_Code_Raw_Material();
        //public abstract void MaterialMixCodes_Mix_Design_HMA();
        //public abstract void MaterialMixCodes_Mix_Design_PCC();
        //public abstract void MaterialMixCodes_Sieve_Analyses_IOC();
        //public abstract void MaterialMixCodes_Sieve_Analyses_JMF();
        //public abstract void My_Details();
        //public abstract void Owner_DIRs();
        //public abstract void Owner_NCRs();
        //public abstract void QAField_Daily_Environmental_Inspection();
        //public abstract void QAField_QA_DIRs();
        //public abstract void QAField_QA_Technician_Random_Search();
        //public abstract void QAField_QA_Test();
        //public abstract void QAField_Weekly_Environmental_Inspection();
        //public abstract void QAField_Weekly_Environmental_Monitoring();
        //public abstract void QALab_BreakSheet_Creation();
        //public abstract void QALab_BreakSheet_Legacy();
        //public abstract void QALab_Equipment_Management();
        //public abstract void QALab_Technician_Random();
        //public abstract void QASearch_Daily_Inspection_Report();
        //public abstract void QASearch_DIR_Checklist_Search();
        //public abstract void QASearch_DIR_Summary_Report();
        //public abstract void QASearch_Inspection_Deficiency_Log_Report();
        //public abstract void QASearch_Ncr_Log_View();
        //public abstract void QASearch_QA_Guide_Schedule_Summary_Report();
        //public abstract void QASearch_QA_Tests();
        //public abstract void QASearch_QA_Test_Summary_Search();
        //public abstract void QASearch_QMS_Document_Search();
        //public abstract void Qms_Document();
        //public abstract void ReportsNotices_General_DN();
        //public abstract void ReportsNotices_General_NCR();
        //public abstract void RMCenter_Comment_Summary();
        //public abstract void RMCenter_Design_Documents();
        //public abstract void RMCenter_DOT_Project_Correspondence_Log();
        //public abstract void RMCenter_Project_Correspondence_Log();
        //public abstract void RMCenter_Project_Transmittal_Log();
        //public abstract void RMCenter_Review_Revise_Submittal();
        //public abstract void RMCenter_RFC_Management();
        //public abstract void RMCenter_Search();
        //public abstract void RMCenter_Upload_DEV_Submittal();
        //public abstract void RMCenter_Upload_Owner_Submittal();
        //public abstract void RMCenter_Upload_QA_Submittal();
        //public abstract void SysConfig_CVL_Lists();
        //public abstract void SysConfig_CVL_List_Items();
        //public abstract void SysConfig_Disciplines();
        //public abstract void SysConfig_Equipment_Makes();
        //public abstract void SysConfig_Equipment_Models();
        //public abstract void SysConfig_Equipment_Types();
        //public abstract void SysConfig_Gradations();
        //public abstract void SysConfig_Grades();
        //public abstract void SysConfig_Grade_Types();
        //public abstract void SysConfig_Notifications();
        //public abstract void SysConfig_Sieves();
        //public abstract void SysConfig_Submittal_Actions();
        //public abstract void SysConfig_Submittal_Requirements();
        //public abstract void SysConfig_Submittal_Types();
        //public abstract void UserMgmt_Access_Rights();
        //public abstract void UserMgmt_Roles();
        //public abstract void UserMgmt_Users();

        internal NavMenu Navigate = new NavMenu(Driver);

        //Project Menu
        public virtual void My_Details() => Navigate.Menu(NavMenu.Project.Menu.My_Details);

        //Project>Administration
        public virtual void Admin_Companies() => Navigate.Menu(NavMenu.Project.Administration.Menu.Companies);
        public virtual void Admin_Contracts() => Navigate.Menu(NavMenu.Project.Administration.Menu.Contracts);
        public virtual void Admin_Menu_Editor() => Navigate.Menu(NavMenu.Project.Administration.Menu.Menu_Editor);
        public virtual void Admin_Project_Details() => Navigate.Menu(NavMenu.Project.Administration.Menu.Project_Details);
        public virtual void Admin_Suport() => Navigate.Menu(NavMenu.Project.Administration.Menu.Support);

        //Project>Admin Tools
        public virtual void AdminTools_Locked_Records() => Navigate.Menu(NavMenu.Project.Administration.AdminTools.Menu.Locked_Records);

        //Project>Administration>User Management
        public virtual void UserMgmt_Access_Rights() => Navigate.Menu(NavMenu.Project.Administration.UserManagement.Menu.Access_Rights);
        public virtual void UserMgmt_Roles() => Navigate.Menu(NavMenu.Project.Administration.UserManagement.Menu.Roles);
        public virtual void UserMgmt_Users() => Navigate.Menu(NavMenu.Project.Administration.UserManagement.Menu.Users);

        //Project>Administration>System Configuration
        public virtual void SysConfig_CVL_Lists() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.CVL_Lists);
        public virtual void SysConfig_CVL_List_Items() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.CVL_List_Items);
        public virtual void SysConfig_Disciplines() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Disciplines);
        public virtual void SysConfig_Gradations() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Gradations);
        public virtual void SysConfig_Notifications() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Notifications);
        public virtual void SysConfig_Sieves() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Sieves);
        public virtual void SysConfig_Submittal_Actions() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Submittal_Actions);
        public virtual void SysConfig_Submittal_Requirements() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Submittal_Requirements);
        public virtual void SysConfig_Submittal_Types() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Submittal_Types);

        //Project>Administration>System Configuration>Equipment
        public virtual void SysConfig_Equipment_Makes() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Makes);
        public virtual void SysConfig_Equipment_Models() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Models);
        public virtual void SysConfig_Equipment_Types() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Types);

        //Project>Administration>System Configuration>Grade Management
        public virtual void SysConfig_Grades() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.GradeManagement.Menu.Grades);
        public virtual void SysConfig_Grade_Types() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.GradeManagement.Menu.Grade_Types);

        //Control Point Menu
        public virtual void Control_Point_Log() => Navigate.Menu(NavMenu.ControlPoint.Menu.Control_Point_Log);
        public virtual void Control_Point_Scheduler() => Navigate.Menu(NavMenu.ControlPoint.Menu.Control_Point_Scheduler);

        //Material Mix Codes Menu
        public virtual void MaterialMixCodes_Material_Code_Base_Aggregate() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Base_Aggregate);
        public virtual void MaterialMixCodes_Material_Code_Concrete_Aggregate() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Concrete_Aggregate);
        public virtual void MaterialMixCodes_Material_Code_HMA_Aggregate() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_HMA_Aggregate);
        public virtual void MaterialMixCodes_Material_Code_Raw_Material() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Raw_Material);
        public virtual void MaterialMixCodes_Mix_Design_HMA() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Mix_Design_HMA);
        public virtual void MaterialMixCodes_Mix_Design_PCC() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Mix_Design_PCC);
        public virtual void MaterialMixCodes_Sieve_Analyses_IOC() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Sieve_Analyses_IOC);
        public virtual void MaterialMixCodes_Sieve_Analyses_JMF() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Sieve_Analyses_JMF);

        //Owner Menu
        public virtual void Owner_DIRs() => Navigate.Menu(NavMenu.Owner.Menu.Owner_DIRs);
        public virtual void Owner_NCRs() => Navigate.Menu(NavMenu.Owner.Menu.Owner_NCRs);

        //QA Field Menu
        public virtual void QAField_Daily_Environmental_Inspection() => Navigate.Menu(NavMenu.QAField.Menu.Daily_Environmental_Inspection);
        public virtual void QAField_QA_DIRs() => Navigate.Menu(NavMenu.QAField.Menu.QA_DIRs);
        public virtual void QAField_QA_Technician_Random_Search() => Navigate.Menu(NavMenu.QAField.Menu.QA_Technician_Random_Search);
        public virtual void QAField_QA_Test() => Navigate.Menu(NavMenu.QAField.Menu.QA_Test);
        public virtual void QAField_Weekly_Environmental_Inspection() => Navigate.Menu(NavMenu.QAField.Menu.Weekly_Environmental_Inspection);
        public virtual void QAField_Weekly_Environmental_Monitoring() => Navigate.Menu(NavMenu.QAField.Menu.Weekly_Environmental_Monitoring);

        //QA Lab Menu
        public virtual void QALab_BreakSheet_Creation() => Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Creation);
        public virtual void QALab_BreakSheet_Legacy() => Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Legacy);
        public virtual void QALab_Equipment_Management() => Navigate.Menu(NavMenu.QALab.Menu.Equipment_Management);
        public virtual void QALab_Technician_Random() => Navigate.Menu(NavMenu.QALab.Menu.Technician_Random);
        public virtual void QALab_BreakSheet_Forecast() => Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Forecast);
        public virtual void QALab_Cylinder_PickUp_List() => Navigate.Menu(NavMenu.QALab.Menu.Cylinder_PickUp_List);
        public virtual void QALab_Early_Break_Calendar() => Navigate.Menu(NavMenu.QALab.Menu.Early_Break_Calendar);

        //QA Search
        public virtual void QASearch_Daily_Inspection_Report() => Navigate.Menu(NavMenu.QASearch.Menu.Daily_Inspection_Report);
        public virtual void QASearch_DIR_Checklist_Search() => Navigate.Menu(NavMenu.QASearch.Menu.DIR_Checklist_Search);
        public virtual void QASearch_DIR_Summary_Report() => Navigate.Menu(NavMenu.QASearch.Menu.DIR_Summary_Report);
        public virtual void QASearch_Inspection_Deficiency_Log_Report() => Navigate.Menu(NavMenu.QASearch.Menu.Inspection_Deficiency_Log_Report);
        public virtual void QASearch_Ncr_Log_View() => Navigate.Menu(NavMenu.QASearch.Menu.Ncr_Log_View);
        public virtual void QASearch_QA_Guide_Schedule_Summary_Report() => Navigate.Menu(NavMenu.QASearch.Menu.QA_Guide_Schedule_Summary_Report);
        public virtual void QASearch_QA_Tests() => Navigate.Menu(NavMenu.QASearch.Menu.QA_Tests);
        public virtual void QASearch_QA_Test_Summary_Search() => Navigate.Menu(NavMenu.QASearch.Menu.QA_Test_Summary_Search);
        public virtual void QASearch_QMS_Document_Search() => Navigate.Menu(NavMenu.QASearch.Menu.QMS_Document_Search);

        /// <summary>
        /// SG >QA Record Control>Qms Document, >QA Search>QMS Document Search
        /// SH249 >Project>QMS Documents, >QA Search>QMS Document Search
        /// I15Tech, I15South, GLX, Garnet >Project>Qms Document -no QMS Document Search menu item
        /// </summary>
        public virtual void Qms_Document() => Navigate.Menu(NavMenu.Project.Menu.Qms_Document);

        //Reports & Notices Menu
        public virtual void ReportsNotices_General_DN() => Navigate.Menu(NavMenu.ReportsNotices.Menu.General_DN);
        public virtual void ReportsNotices_General_NCR() => Navigate.Menu(NavMenu.ReportsNotices.Menu.General_NCR);

        //RM Center Menu
        public virtual void RMCenter_Comment_Summary() => Navigate.Menu(NavMenu.RMCenter.Menu.Comment_Summary);
        public virtual void RMCenter_Design_Documents() => Navigate.Menu(NavMenu.RMCenter.Menu.Design_Documents);
        public virtual void RMCenter_DOT_Project_Correspondence_Log() => Navigate.Menu(NavMenu.RMCenter.Menu.DOT_Project_Correspondence_Log);
        public virtual void RMCenter_Project_Correspondence_Log() => Navigate.Menu(NavMenu.RMCenter.Menu.Project_Correspondence_Log);
        public virtual void RMCenter_Project_Transmittal_Log() => Navigate.Menu(NavMenu.RMCenter.Menu.Project_Transmittal_Log);
        public virtual void RMCenter_Review_Revise_Submittal() => Navigate.Menu(NavMenu.RMCenter.Menu.Review_Revise_Submittal);
        public virtual void RMCenter_RFC_Management() => Navigate.Menu(NavMenu.RMCenter.Menu.RFC_Management);
        public virtual void RMCenter_Search() => Navigate.Menu(NavMenu.RMCenter.Menu.Search);
        public virtual void RMCenter_Upload_DEV_Submittal() => Navigate.Menu(NavMenu.RMCenter.Menu.Upload_DEV_Submittal);
        public virtual void RMCenter_Upload_Owner_Submittal() => Navigate.Menu(NavMenu.RMCenter.Menu.Upload_Owner_Submittal);
        public virtual void RMCenter_Upload_QA_Submittal() => Navigate.Menu(NavMenu.RMCenter.Menu.Upload_QA_Submittal);


        public static T SetClass<T>() => (T)SetPageClassBasedOnTenant();
        private static object SetPageClassBasedOnTenant()
        {
            var instance = new PageNavigation_Impl();

            if (projectName == Config.ProjectName.SGWay)
            {
                LogInfo("###### using Navigation_SGWay instance");
                instance = new PageNavigation_SGWay();
            }
            else if (projectName == Config.ProjectName.SH249)
            {
                LogInfo("###### using Navigation_SH249 instance");
                instance = new PageNavigation_SH249();
            }
            else
                LogInfo("###### using Navigation_Impl instance");

            return instance;
        }
    }
}
