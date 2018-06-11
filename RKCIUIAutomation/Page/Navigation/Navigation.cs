using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.Navigation
{
    public abstract class Navigation : PageBase, INavigation
    {
        public abstract void AdminTools_Locked_Records();
        public abstract void Admin_Companies();
        public abstract void Admin_Contracts();
        public abstract void Admin_Menu_Editor();
        public abstract void Admin_Project_Details();
        public abstract void Admin_Suport();
        public abstract void Control_Point_Log();
        public abstract void Control_Point_Scheduler();
        public abstract void MaterialMixCodes_Material_Code_Base_Aggregate();
        public abstract void MaterialMixCodes_Material_Code_Concrete_Aggregate();
        public abstract void MaterialMixCodes_Material_Code_HMA_Aggregate();
        public abstract void MaterialMixCodes_Material_Code_Raw_Material();
        public abstract void MaterialMixCodes_Mix_Design_HMA();
        public abstract void MaterialMixCodes_Mix_Design_PCC();
        public abstract void MaterialMixCodes_Sieve_Analyses_IOC();
        public abstract void MaterialMixCodes_Sieve_Analyses_JMF();
        public abstract void My_Details();
        public abstract void Owner_DIRs();
        public abstract void Owner_NCRs();
        public abstract void QAField_Daily_Environmental_Inspection();
        public abstract void QAField_QA_DIRs();
        public abstract void QAField_QA_Technician_Random_Search();
        public abstract void QAField_QA_Test();
        public abstract void QAField_Weekly_Environmental_Inspection();
        public abstract void QAField_Weekly_Environmental_Monitoring();
        public abstract void QALab_BreakSheet_Creation();
        public abstract void QALab_BreakSheet_Legacy();
        public abstract void QALab_Equipment_Management();
        public abstract void QALab_Technician_Random();
        public abstract void QASearch_Daily_Inspection_Report();
        public abstract void QASearch_DIR_Checklist_Search();
        public abstract void QASearch_DIR_Summary_Report();
        public abstract void QASearch_Inspection_Deficiency_Log_Report();
        public abstract void QASearch_Ncr_Log_View();
        public abstract void QASearch_QA_Guide_Schedule_Summary_Report();
        public abstract void QASearch_QA_Tests();
        public abstract void QASearch_QA_Test_Summary_Search();
        public abstract void QASearch_QMS_Document_Search();
        public abstract void Qms_Document();
        public abstract void ReportsNotices_General_DN();
        public abstract void ReportsNotices_General_NCR();
        public abstract void RMCenter_Comment_Summary();
        public abstract void RMCenter_Design_Documents();
        public abstract void RMCenter_DOT_Project_Correspondence_Log();
        public abstract void RMCenter_Project_Correspondence_Log();
        public abstract void RMCenter_Project_Transmittal_Log();
        public abstract void RMCenter_Review_Revise_Submittal();
        public abstract void RMCenter_RFC_Management();
        public abstract void RMCenter_Search();
        public abstract void RMCenter_Upload_DEV_Submittal();
        public abstract void RMCenter_Upload_Owner_Submittal();
        public abstract void RMCenter_Upload_QA_Submittal();
        public abstract void SysConfig_CVL_Lists();
        public abstract void SysConfig_CVL_List_Items();
        public abstract void SysConfig_Disciplines();
        public abstract void SysConfig_Equipment_Makes();
        public abstract void SysConfig_Equipment_Models();
        public abstract void SysConfig_Equipment_Types();
        public abstract void SysConfig_Gradations();
        public abstract void SysConfig_Grades();
        public abstract void SysConfig_Grade_Types();
        public abstract void SysConfig_Notifications();
        public abstract void SysConfig_Sieves();
        public abstract void SysConfig_Submittal_Actions();
        public abstract void SysConfig_Submittal_Requirements();
        public abstract void SysConfig_Submittal_Types();
        public abstract void UserMgmt_Access_Rights();
        public abstract void UserMgmt_Roles();
        public abstract void UserMgmt_Users();

        public static T SetClass<T>() => (T)SetPageClassBasedOnTenant();
        private static object SetPageClassBasedOnTenant()
        {
            var instance = new Navigation_Impl();

            if (projectName == Config.ProjectName.SGWay)
            {
                LogInfo("###### using Navigation_SGWay instance");
                instance = new Navigation_SGWay();
            }
            else if (projectName == Config.ProjectName.SH249)
            {
                LogInfo("###### using Navigation_SH249 instance");
                instance = new Navigation_SH249();
            }
            else
                LogInfo("###### using Navigation_Impl instance");

            return instance;
        }
    }
}
