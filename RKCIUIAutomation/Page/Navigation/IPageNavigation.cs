using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.Navigation
{
    public interface IPageNavigation
    {
        #region Project Menu
        void My_Details();
        void Qms_Document();
        #endregion

        #region Project>Administration
        void Admin_Project_Details();
        void Admin_Companies();
        void Admin_Contracts();
        void Admin_Menu_Editor();
        void Admin_Suport();
        #endregion

        #region Project>Administration>Admin Tools
        void AdminTools_Locked_Records();
        #endregion

        #region Project>Administration>User Management
        void UserMgmt_Roles();
        void UserMgmt_Users();
        void UserMgmt_Access_Rights();
        #endregion

        #region Project>Administration>System Configuration
        void SysConfig_Disciplines();
        void SysConfig_Submittal_Actions();
        void SysConfig_Submittal_Requirements();
        void SysConfig_Submittal_Types();
        void SysConfig_CVL_Lists();
        void SysConfig_CVL_List_Items();
        void SysConfig_Notifications();
        void SysConfig_Sieves();
        void SysConfig_Gradations();
        #endregion

        #region Project>Administration>System Configuration>Equipment
        void SysConfig_Equipment_Makes();
        void SysConfig_Equipment_Types();
        void SysConfig_Equipment_Models();
        #endregion

        #region Project>Administration>System Configuration>Grade Management
        void SysConfig_Grades();
        void SysConfig_Grade_Types();
        #endregion

        #region QA Lab Menu
        void QALab_BreakSheet_Creation();
        void QALab_BreakSheet_Legacy();
        void QALab_Equipment_Management();
        void QALab_Technician_Random();
        void QALab_BreakSheet_Forecast();
        void QALab_Cylinder_PickUp_List();
        void QALab_Early_Break_Calendar();
        #endregion

        #region OV Menu
        void OV_Create_OV_Test();
        void OV_OV_Test();
        #endregion

        #region QA Records Control Menu
        void QARecordControl_QA_Test_Original_Report();
        void QARecordControl_QA_Test_Correction_Report();
        void QARecordControl_QA_Test_All();
        void QARecordControl_QA_DIRs();
        void QARecordControl_General_NCR();
        void QARecordControl_General_CDR();
        void QARecordControl_Retaining_Wall_BackFill_Quantity_Tracker();
        void QARecordControl_Concrete_Paving_Quantity_Tracker();
        void QARecordControl_MPL_Tracker();
        void QARecordControl_Girder_Tracker();
        void QARecordControl_QA_Test_Retest_Report();
        void QARecordControl_Environmental_Document();    
        //using Qms_Document() method instead QARecordControl_Qms_Document();
        #endregion

        #region QA Engineer Menu
        void QAEngineer_QA_Test_Lab_Supervisor_Review();
        void QAEngineer_QA_Test_Field_Supervisor_Review();
        void QAEngineer_QA_Test_Authorization();
        void QAEngineer_DIR_QA_Review_Approval();
        #endregion

        #region Reports & Notices Menu
        void ReportsNotices_General_NCR();
        void ReportsNotices_General_DN();
        #endregion

        #region QA Search
        void QASearch_QA_Tests();
        void QASearch_QA_Test_Summary_Search();
        void QASearch_QA_Guide_Schedule_Summary_Report();
        void QASearch_Inspection_Deficiency_Log_Report();
        void QASearch_Daily_Inspection_Report();
        void QASearch_DIR_Summary_Report();
        void QASearch_DIR_Checklist_Search();
        void QASearch_Ncr_Log_View();
        void QASearch_QMS_Document_Search();
        #endregion

        #region QA Field Menu
        void QAField_QA_Test();
        void QAField_QA_DIRs();
        void QAField_QA_Technician_Random_Search();
        void QAField_Weekly_Environmental_Monitoring();
        void QAField_Daily_Environmental_Inspection();
        void QAField_Weekly_Environmental_Inspection();
        #endregion

        #region Control Point Menu
        void Control_Point_Log();
        void Control_Point_Scheduler();
        #endregion

        #region Owner Menu
        void Owner_DIRs();
        void Owner_NCRs();
        #endregion

        #region Material Mix Code Menu
        void MaterialMixCodes_Mix_Design_PCC();
        void MaterialMixCodes_Mix_Design_HMA();
        void MaterialMixCodes_Sieve_Analyses_JMF();
        void MaterialMixCodes_Sieve_Analyses_IOC();
        void MaterialMixCodes_Material_Code_Base_Aggregate();
        void MaterialMixCodes_Material_Code_Concrete_Aggregate();
        void MaterialMixCodes_Material_Code_HMA_Aggregate();
        void MaterialMixCodes_Material_Code_Raw_Material();
        #endregion

        #region RM Center Menu
        void RMCenter_Search();
        void RMCenter_Design_Documents();
        void RMCenter_Upload_QA_Submittal();
        void RMCenter_Upload_Owner_Submittal();
        void RMCenter_Upload_DEV_Submittal();
        void RMCenter_DOT_Project_Correspondence_Log();
        void RMCenter_Review_Revise_Submittal();
        void RMCenter_RFC_Management();
        void RMCenter_Project_Transmittal_Log();
        void RMCenter_Project_Correspondence_Log();
        void RMCenter_Comment_Summary();
        #endregion

        #region RFI
        void RFI_List();
        void RFI_Create();
        #endregion
    }
}
