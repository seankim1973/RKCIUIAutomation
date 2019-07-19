using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page.Navigation
{
    #region PageNavigation Generic class

    public class PageNavigation : PageNavigation_Impl
    {
        public PageNavigation()
        {
        }

        public PageNavigation(IWebDriver driver) => this.Driver = driver;

        public override T SetClass<T>(IWebDriver driver)
        {
            IPageNavigation instance = new PageNavigation(driver);

            if (tenantName == TenantNameType.SGWay)
            {
                log.Info($"###### using Navigation_SGWay instance ###### ");
                instance = new PageNavigation_SGWay(driver);
            }
            else if (tenantName == TenantNameType.SH249)
            {
                log.Info($"###### using Navigation_SH249 instance ###### ");
                instance = new PageNavigation_SH249(driver);
            }
            else if (tenantName == TenantNameType.Garnet)
            {
                log.Info($"###### using Navigation_Garnet instance ###### ");
                instance = new PageNavigation_Garnet(driver);
            }
            else if (tenantName == TenantNameType.GLX)
            {
                log.Info($"###### using Navigation_GLX instance ###### ");
                instance = new PageNavigation_GLX(driver);
            }
            else if (tenantName == TenantNameType.I15South)
            {
                log.Info($"###### using Navigation_I15South instance ###### ");
                instance = new PageNavigation_I15South(driver);
            }
            else if (tenantName == TenantNameType.I15Tech)
            {
                log.Info($"###### using Navigation_I15Tech instance ###### ");
                instance = new PageNavigation_I15Tech(driver);
            }
            else if (tenantName == TenantNameType.LAX)
            {
                log.Info($"###### using Navigation_LAX instance ###### ");
                instance = new PageNavigation_LAX(driver);
            }

            return (T)instance;
        }

        internal INavMenu Navigate => new NavMenu(driver);

        #region Project Menu
        // QMS_Document / QMS_Document_Search
        /// <summary>
        /// for SG >QA Record Control>Qms Document, >QA Search>QMS Document Search
        /// for SH249 >QA Record Control>QMS Documents, >QA Search>QMS Document Search
        /// for I15Tech, I15South, GLX, Garnet >Project>Qms Document -no QMS Document Search menu item
        /// </summary>
        public override void Qms_Document() => Navigate.Menu(NavMenu.Project.Menu.Qms_Document);

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

        // Project>Administration>System Configuration>Equipment
        public override void SysConfig_Equipment_Makes() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Makes);

        public override void SysConfig_Equipment_Models() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Models);

        public override void SysConfig_Equipment_Types() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Types);

        // Project>Administration>System Configuration>Grade Management
        public override void SysConfig_Grades() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.GradeManagement.Menu.Grades);

        public override void SysConfig_Grade_Types() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.GradeManagement.Menu.Grade_Types);
        #endregion Project Menu

        #region Control Point Menu
        public override void Control_Point_Log() => Navigate.Menu(NavMenu.ControlPoint.Menu.Control_Point_Log);

        public override void Control_Point_Scheduler() => Navigate.Menu(NavMenu.ControlPoint.Menu.Control_Point_Scheduler);
        #endregion Control Point Menu

        #region Material Mix Codes Menu
        public override void MaterialMixCodes_Material_Code_Base_Aggregate() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Base_Aggregate);

        public override void MaterialMixCodes_Material_Code_Concrete_Aggregate() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Concrete_Aggregate);

        public override void MaterialMixCodes_Material_Code_HMA_Aggregate() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_HMA_Aggregate);

        public override void MaterialMixCodes_Material_Code_Raw_Material() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Raw_Material);

        public override void MaterialMixCodes_Mix_Design_HMA() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Mix_Design_HMA);

        public override void MaterialMixCodes_Mix_Design_PCC() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Mix_Design_PCC);

        public override void MaterialMixCodes_Sieve_Analyses_IOC() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Sieve_Analyses_IOC);

        public override void MaterialMixCodes_Sieve_Analyses_JMF() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Sieve_Analyses_JMF);
        #endregion Material Mix Codes Menu override methods

        #region Owner Menu
        public override void Owner_DIRs() => Navigate.Menu(NavMenu.Owner.Menu.Owner_DIRs);

        public override void Owner_NCRs() => Navigate.Menu(NavMenu.Owner.Menu.Owner_NCRs);
        #endregion Owner Menu

        #region QA Engineer Menu
        public override void QAEngineer_QA_Test_Lab_Supervisor_Review() => Navigate.Menu(NavMenu.QAEngineer.Menu.QA_Test_Lab_Supervisor_Review);

        public override void QAEngineer_QA_Test_Field_Supervisor_Review() => Navigate.Menu(NavMenu.QAEngineer.Menu.QA_Test_Field_Supervisor_Review);

        public override void QAEngineer_QA_Test_Authorization() => Navigate.Menu(NavMenu.QAEngineer.Menu.QA_Test_Authorization);

        public override void QAEngineer_DIR_QA_Review_Approval() => Navigate.Menu(NavMenu.QAEngineer.Menu.DIR_QA_Review_Approval);

        public override void QA_Test_Proctor_Curve_Controller() => Navigate.Menu(NavMenu.QAEngineer.Menu.QA_Test_Proctor_Curve_Controller);
        #endregion QA Engineer Menu

        #region QA Field Menu
        public override void QAField_Daily_Environmental_Inspection() => Navigate.Menu(NavMenu.QAField.Menu.Daily_Environmental_Inspection);

        public override void QAField_QA_DIRs() => Navigate.Menu(NavMenu.QAField.Menu.QA_DIRs);

        public override void QAField_QA_Technician_Random_Search() => Navigate.Menu(NavMenu.QAField.Menu.QA_Technician_Random_Search);

        public override void QAField_QA_Test() => Navigate.Menu(NavMenu.QAField.Menu.QA_Test);

        public override void QAField_Weekly_Environmental_Inspection() => Navigate.Menu(NavMenu.QAField.Menu.Weekly_Environmental_Inspection);

        public override void QAField_Weekly_Environmental_Monitoring() => Navigate.Menu(NavMenu.QAField.Menu.Weekly_Environmental_Monitoring);
        #endregion QA Field Menu

        #region QA Lab Menu
        public override void QALab_BreakSheet_Creation() => Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Creation);

        public override void QALab_BreakSheet_Legacy() => Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Legacy);

        public override void QALab_Equipment_Management() => Navigate.Menu(NavMenu.QALab.Menu.Equipment_Management);

        public override void QALab_Technician_Random() => Navigate.Menu(NavMenu.QALab.Menu.Technician_Random);

        public override void QALab_BreakSheet_Forecast() => Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Forecast);

        public override void QALab_Cylinder_PickUp_List() => Navigate.Menu(NavMenu.QALab.Menu.Cylinder_PickUp_List);

        public override void QALab_Early_Break_Calendar() => Navigate.Menu(NavMenu.QALab.Menu.Early_Break_Calendar);
        #endregion QA Lab Menu

        #region OV Menu
        public override void OV_Create_OV_Test() => Navigate.Menu(NavMenu.OV.Menu.Create_OV_Test);

        public override void OV_OV_Test() => Navigate.Menu(NavMenu.OV.Menu.OV_Tests);
        #endregion OV Menu

        #region QA Records Control Menu
        // use Qms_Document() for QARecordControl_Qms_Document
        public override void QARecordControl_QA_Test_Original_Report() => Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_Original_Report);

        public override void QARecordControl_QA_Test_Correction_Report() => Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_Correction_Report);

        public override void QARecordControl_QA_Test_All() => Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_All);

        public override void QARecordControl_QA_DIRs() => Navigate.Menu(NavMenu.QARecordControl.Menu.QA_DIRs);

        public override void QARecordControl_General_NCR() => Navigate.Menu(NavMenu.QARecordControl.Menu.General_NCR);

        public override void QARecordControl_General_CDR() => Navigate.Menu(NavMenu.QARecordControl.Menu.General_CDR);

        public override void QARecordControl_Retaining_Wall_BackFill_Quantity_Tracker() => Navigate.Menu(NavMenu.QARecordControl.Menu.Retaining_Wall_BackFill_Quantity_Tracker);

        public override void QARecordControl_Concrete_Paving_Quantity_Tracker() => Navigate.Menu(NavMenu.QARecordControl.Menu.Concrete_Paving_Quantity_Tracker);

        public override void QARecordControl_MPL_Tracker() => Navigate.Menu(NavMenu.QARecordControl.Menu.MPL_Tracker);

        public override void QARecordControl_Girder_Tracker() => Navigate.Menu(NavMenu.QARecordControl.Menu.Girder_Tracker);

        public override void QARecordControl_QA_Test_Retest_Report() => Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_Retest_Report);

        public override void QARecordControl_Environmental_Document() => Navigate.Menu(NavMenu.QARecordControl.Menu.Environmental_Document);
        #endregion QA Records Control Menu

        #region Record Control Menu

        public override void RecordControl_QA_QaTest() => Navigate.Menu(NavMenu.RecordControl.QA.Menu.QA_Test);

        public override void RecordControl_QA_QaDIRs() => Navigate.Menu(NavMenu.RecordControl.QA.Menu.QA_DIR);

        public override void RecordControl_QA_QaNCR() => Navigate.Menu(NavMenu.RecordControl.QA.Menu.QA_NCR);

        public override void RecordControl_QA_QaDeficiency_Notice() => Navigate.Menu(NavMenu.RecordControl.QA.Menu.QA_Deficiency_Notice);

        public override void RecordControl_QC_QcTest_All() => Navigate.Menu(NavMenu.RecordControl.QC.Menu.QC_Test_All);

        public override void RecordControl_QC_QcDIRs() => Navigate.Menu(NavMenu.RecordControl.QC.Menu.QC_DIR);

        public override void RecordControl_QC_QcNCR() => Navigate.Menu(NavMenu.RecordControl.QC.Menu.QC_NCR);

        public override void RecordControl_QC_QcDeficiency_Notice() => Navigate.Menu(NavMenu.RecordControl.QC.Menu.QC_Deficiency_Notice);

        public override void RecordControl_Test_Count() => Navigate.Menu(NavMenu.RecordControl.Menu.Test_Count);

        public override void RecordControl_DIR_Count() => Navigate.Menu(NavMenu.RecordControl.Menu.DIR_Count);

        public override void RecordControl_Retaining_Wall_Backfill_Quantity_Tracker() => Navigate.Menu(NavMenu.RecordControl.Menu.Retaining_Wall_Backfill_Quantity_Tracker);

        public override void RecordControl_Concrete_Paving_Quantity_Tracker() => Navigate.Menu(NavMenu.RecordControl.Menu.Concrete_Paving_Quantity_Tracker);

        public override void RecordControl_MPL_Tracker() => Navigate.Menu(NavMenu.RecordControl.Menu.MPL_Tracker);

        public override void RecordControl_Girder_Tracker() => Navigate.Menu(NavMenu.RecordControl.Menu.Girder_Tracker);

        public override void RecordControl_Weekly_Environmental_Monitoring() => Navigate.Menu(NavMenu.RecordControl.Menu.Weekly_Environmental_Monitoring);

        public override void RecordControlDaily_Environmental_Inspection() => Navigate.Menu(NavMenu.RecordControl.Menu.Daily_Environmental_Inspection);

        public override void RecordControl_Weekly_Environmental_Inspection() => Navigate.Menu(NavMenu.RecordControl.Menu.Weekly_Environmental_Inspection);
        #endregion Record Control Menu - LAX

        #region QA Search
        public override void QASearch_Daily_Inspection_Report() => Navigate.Menu(NavMenu.QASearch.Menu.Daily_Inspection_Report);

        public override void QASearch_DIR_Checklist_Search() => Navigate.Menu(NavMenu.QASearch.Menu.DIR_Checklist_Search);

        public override void QASearch_Mix_Design_Summary_HMA() => Navigate.Menu(NavMenu.QASearch.Menu.Hma_Mix_Design_Summary);

        public override void QASearch_Mix_Design_Report_HMA() => Navigate.Menu(NavMenu.QASearch.Menu.Hma_Mix_Design_Report);

        public override void QASearch_Material_Traceability_Matrix() => Navigate.Menu(NavMenu.QASearch.Menu.Material_Traceability_Matrix_Search);

        public override void QASearch_CDR_Log_View() => Navigate.Menu(NavMenu.QASearch.Menu.CDR_Log_View);

        public override void QASearch_DIR_Summary_Report() => Navigate.Menu(NavMenu.QASearch.Menu.DIR_Summary_Report);

        public override void QASearch_Inspection_Deficiency_Log_Report() => Navigate.Menu(NavMenu.QASearch.Menu.Inspection_Deficiency_Log_Report);

        public override void QASearch_NCR_Log_View() => Navigate.Menu(NavMenu.QASearch.Menu.NCR_Log_View);

        public override void QASearch_QA_Guide_Schedule_Summary_Report() => Navigate.Menu(NavMenu.QASearch.Menu.QA_Guide_Schedule_Summary_Report);

        public override void QASearch_QA_Tests() => Navigate.Menu(NavMenu.QASearch.Menu.QA_Tests);

        public override void QASearch_QA_Test_Summary_Search() => Navigate.Menu(NavMenu.QASearch.Menu.QA_Test_Summary_Search);

        public override void QASearch_QMS_Document_Search() => Navigate.Menu(NavMenu.QASearch.Menu.QMS_Document_Search);

        public override void Environmental_Document_Search() => Navigate.Menu(NavMenu.QASearch.Menu.Environmental_Document_Search);

        public override void QAQO_Test_Proctor_Curve_Report() => Navigate.Menu(NavMenu.QASearch.Menu.QAQO_Test_Proctor_Curve_Report);

        public override void QAQO_Test_Proctor_Curve_Summary() => Navigate.Menu(NavMenu.QASearch.Menu.QAQO_Test_Proctor_Curve_Summary);
        #endregion QA Search

        #region Reports & Notices Menu
        public override void ReportsNotices_General_DN() => Navigate.Menu(NavMenu.ReportsNotices.Menu.General_DN);

        public override void ReportsNotices_General_NCR() => Navigate.Menu(NavMenu.ReportsNotices.Menu.General_NCR);
        #endregion Reports & Notices Menu

        #region RM Center Menu
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
        #endregion RM Center Menu

        #region RFI
        public override void RFI_List() => Navigate.Menu(NavMenu.RFI.Menu.List);

        public override void RFI_Create() => Navigate.Menu(NavMenu.RFI.Menu.Create);
        #endregion RFI

        #region QC Lab Menu
        public override void QCLab_BreakSheet_Creation() => Navigate.Menu(NavMenu.QCLab.Menu.BreakSheet_Creation);

        public override void QCLab_BreakSheet_Legacy() => Navigate.Menu(NavMenu.QCLab.Menu.BreakSheet_Legacy);

        public override void QCLab_Equipment_Management() => Navigate.Menu(NavMenu.QCLab.Menu.Equipment_Management);
        #endregion QC Lab Menu

        #region QC Record Control Menu
        public override void QCRecordControl_QC_Test_Original_Report() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_Test_Original_Report);

        public override void QCRecordControl_QC_Test_Correction_Report() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_Test_Correction_Report);

        public override void QCRecordControl_QC_Test_All() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_Test_All);

        public override void QCRecordControl_QC_DIRs() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_DIRs);

        public override void QCRecordControl_QC_NCR() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_NCR);

        public override void QCRecordControl_QC_CDR() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_CDR);
        #endregion QC Record Control Menu

        #region QC Engineer Menu
        public override void QCEngineer_QC_Test_Lab_Supervisor_Review() => Navigate.Menu(NavMenu.QCEngineer.Menu.QC_Test_Lab_Supervisor_Review);

        public override void QCEngineer_QC_Test_Authorization() => Navigate.Menu(NavMenu.QCEngineer.Menu.QC_Test_Authorization);
        #endregion QC Engineer Menu

        #region QC Search Menu
        public override void QCSearch_QC_Tests_Search() => Navigate.Menu(NavMenu.QCSearch.Menu.QC_Tests_Search);

        public override void QCSearch_QC_Test_Summary_Search() => Navigate.Menu(NavMenu.QCSearch.Menu.QC_Test_Summary_Search);

        public override void QCSearch_Daily_Inspection_Report() => Navigate.Menu(NavMenu.QCSearch.Menu.Daily_Inspection_Report);

        public override void QCSearch_DIR_Summary_Report() => Navigate.Menu(NavMenu.QCSearch.Menu.DIR_Summary_Report);
        #endregion QC Search Menu
    }

    #endregion PageNavigation Generic class

    #region PageNavigation Interface class

    public interface IPageNavigation
    {
        // Project Menu
        void My_Details();

        void Qms_Document();

        // Project>Administration
        void Admin_Project_Details();

        void Admin_Companies();

        void Admin_Contracts();

        void Admin_Menu_Editor();

        void Admin_Suport();

        // Project>Administration>Admin Tools
        void AdminTools_Locked_Records();

        // Project>Administration>User Management
        void UserMgmt_Roles();

        void UserMgmt_Users();

        void UserMgmt_Access_Rights();

        // Project>Administration>System Configuration
        void SysConfig_Disciplines();

        void SysConfig_Submittal_Actions();

        void SysConfig_Submittal_Requirements();

        void SysConfig_Submittal_Types();

        void SysConfig_CVL_Lists();

        void SysConfig_CVL_List_Items();

        void SysConfig_Notifications();

        void SysConfig_Sieves();

        void SysConfig_Gradations();

        // Project>Administration>System Configuration>Equipment
        void SysConfig_Equipment_Makes();

        void SysConfig_Equipment_Types();

        void SysConfig_Equipment_Models();

        // Project>Administration>System Configuration>Grade Management
        void SysConfig_Grades();

        void SysConfig_Grade_Types();

        // QA Lab Menu
        void QALab_BreakSheet_Creation();

        void QALab_BreakSheet_Legacy();

        void QALab_Equipment_Management();

        void QALab_Technician_Random();

        void QALab_BreakSheet_Forecast();

        void QALab_Cylinder_PickUp_List();

        void QALab_Early_Break_Calendar();

        // OV Menu
        void OV_Create_OV_Test();

        void OV_OV_Test();

        // QA Records Control Menu
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

        // QA Engineer Menu
        void QAEngineer_QA_Test_Lab_Supervisor_Review();

        void QAEngineer_QA_Test_Field_Supervisor_Review();

        void QAEngineer_QA_Test_Authorization();

        void QAEngineer_DIR_QA_Review_Approval();

        void QA_Test_Proctor_Curve_Controller();

        // Reports & Notices Menu
        void ReportsNotices_General_NCR();

        void ReportsNotices_General_DN();

        // Record Control Menu - LAX
        void RecordControl_QA_QaTest();

        void RecordControl_QA_QaDIRs();

        void RecordControl_QA_QaNCR();

        void RecordControl_QA_QaDeficiency_Notice();

        void RecordControl_QC_QcTest_All();

        void RecordControl_QC_QcDIRs();

        void RecordControl_QC_QcNCR();

        void RecordControl_QC_QcDeficiency_Notice();

        void RecordControl_Test_Count();

        void RecordControl_DIR_Count();

        void RecordControl_Retaining_Wall_Backfill_Quantity_Tracker();

        void RecordControl_Concrete_Paving_Quantity_Tracker();

        void RecordControl_MPL_Tracker();

        void RecordControl_Girder_Tracker();

        void RecordControl_Weekly_Environmental_Monitoring();

        void RecordControlDaily_Environmental_Inspection();

        void RecordControl_Weekly_Environmental_Inspection();

        // QA Search
        void QASearch_QA_Tests();

        void QASearch_QA_Test_Summary_Search();

        void QASearch_QA_Guide_Schedule_Summary_Report();

        void QASearch_Inspection_Deficiency_Log_Report();

        void QASearch_Daily_Inspection_Report();

        void QASearch_DIR_Summary_Report();

        void QASearch_DIR_Checklist_Search();

        void QASearch_Mix_Design_Summary_HMA();

        void QASearch_Mix_Design_Report_HMA();

        void QASearch_Material_Traceability_Matrix();

        void QASearch_CDR_Log_View();

        void QASearch_NCR_Log_View();

        void QASearch_QMS_Document_Search();

        void Environmental_Document_Search();

        void QAQO_Test_Proctor_Curve_Report();

        void QAQO_Test_Proctor_Curve_Summary();

        // QA Field Menu
        void QAField_QA_Test();

        void QAField_QA_DIRs();

        void QAField_QA_Technician_Random_Search();

        void QAField_Weekly_Environmental_Monitoring();

        void QAField_Daily_Environmental_Inspection();

        void QAField_Weekly_Environmental_Inspection();

        // Control Point Menu
        void Control_Point_Log();

        void Control_Point_Scheduler();

        // Owner Menu
        void Owner_DIRs();

        void Owner_NCRs();

        // Material Mix Code Menu
        void MaterialMixCodes_Mix_Design_PCC();

        void MaterialMixCodes_Mix_Design_HMA();

        void MaterialMixCodes_Sieve_Analyses_JMF();

        void MaterialMixCodes_Sieve_Analyses_IOC();

        void MaterialMixCodes_Material_Code_Base_Aggregate();

        void MaterialMixCodes_Material_Code_Concrete_Aggregate();

        void MaterialMixCodes_Material_Code_HMA_Aggregate();

        void MaterialMixCodes_Material_Code_Raw_Material();

        // RM Center Menu
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

        // RFI Menu
        void RFI_List();

        void RFI_Create();

        //QC Lab Menu
        void QCLab_BreakSheet_Creation();

        void QCLab_BreakSheet_Legacy();

        void QCLab_Equipment_Management();

        //QC Record Control Menu
        void QCRecordControl_QC_Test_Original_Report();

        void QCRecordControl_QC_Test_Correction_Report();

        void QCRecordControl_QC_Test_All();

        void QCRecordControl_QC_DIRs();

        void QCRecordControl_QC_NCR();

        void QCRecordControl_QC_CDR();

        //QC Engineer Menu
        void QCEngineer_QC_Test_Lab_Supervisor_Review();

        void QCEngineer_QC_Test_Authorization();

        // QC Search
        void QCSearch_QC_Tests_Search();

        void QCSearch_QC_Test_Summary_Search();

        void QCSearch_Daily_Inspection_Report();

        void QCSearch_DIR_Summary_Report();
    }

    #endregion PageNavigation Interface class

   
    public abstract class PageNavigation_Impl : PageBase, IPageNavigation
    {
        public abstract void AdminTools_Locked_Records();
        public abstract void Admin_Companies();
        public abstract void Admin_Contracts();
        public abstract void Admin_Menu_Editor();
        public abstract void Admin_Project_Details();
        public abstract void Admin_Suport();
        public abstract void Control_Point_Log();
        public abstract void Control_Point_Scheduler();
        public abstract void Environmental_Document_Search();
        public abstract void MaterialMixCodes_Material_Code_Base_Aggregate();
        public abstract void MaterialMixCodes_Material_Code_Concrete_Aggregate();
        public abstract void MaterialMixCodes_Material_Code_HMA_Aggregate();
        public abstract void MaterialMixCodes_Material_Code_Raw_Material();
        public abstract void MaterialMixCodes_Mix_Design_HMA();
        public abstract void MaterialMixCodes_Mix_Design_PCC();
        public abstract void MaterialMixCodes_Sieve_Analyses_IOC();
        public abstract void MaterialMixCodes_Sieve_Analyses_JMF();
        public abstract void My_Details();
        public abstract void OV_Create_OV_Test();
        public abstract void OV_OV_Test();
        public abstract void Owner_DIRs();
        public abstract void Owner_NCRs();
        public abstract void QAEngineer_DIR_QA_Review_Approval();
        public abstract void QAEngineer_QA_Test_Authorization();
        public abstract void QAEngineer_QA_Test_Field_Supervisor_Review();
        public abstract void QAEngineer_QA_Test_Lab_Supervisor_Review();
        public abstract void QAField_Daily_Environmental_Inspection();
        public abstract void QAField_QA_DIRs();
        public abstract void QAField_QA_Technician_Random_Search();
        public abstract void QAField_QA_Test();
        public abstract void QAField_Weekly_Environmental_Inspection();
        public abstract void QAField_Weekly_Environmental_Monitoring();
        public abstract void QALab_BreakSheet_Creation();
        public abstract void QALab_BreakSheet_Forecast();
        public abstract void QALab_BreakSheet_Legacy();
        public abstract void QALab_Cylinder_PickUp_List();
        public abstract void QALab_Early_Break_Calendar();
        public abstract void QALab_Equipment_Management();
        public abstract void QALab_Technician_Random();
        public abstract void QAQO_Test_Proctor_Curve_Report();
        public abstract void QAQO_Test_Proctor_Curve_Summary();
        public abstract void QARecordControl_Concrete_Paving_Quantity_Tracker();
        public abstract void QARecordControl_Environmental_Document();
        public abstract void QARecordControl_General_CDR();
        public abstract void QARecordControl_General_NCR();
        public abstract void QARecordControl_Girder_Tracker();
        public abstract void QARecordControl_MPL_Tracker();
        public abstract void QARecordControl_QA_DIRs();
        public abstract void QARecordControl_QA_Test_All();
        public abstract void QARecordControl_QA_Test_Correction_Report();
        public abstract void QARecordControl_QA_Test_Original_Report();
        public abstract void QARecordControl_QA_Test_Retest_Report();
        public abstract void QARecordControl_Retaining_Wall_BackFill_Quantity_Tracker();
        public abstract void QASearch_CDR_Log_View();
        public abstract void QASearch_Daily_Inspection_Report();
        public abstract void QASearch_DIR_Checklist_Search();
        public abstract void QASearch_DIR_Summary_Report();
        public abstract void QASearch_Inspection_Deficiency_Log_Report();
        public abstract void QASearch_Material_Traceability_Matrix();
        public abstract void QASearch_Mix_Design_Report_HMA();
        public abstract void QASearch_Mix_Design_Summary_HMA();
        public abstract void QASearch_NCR_Log_View();
        public abstract void QASearch_QA_Guide_Schedule_Summary_Report();
        public abstract void QASearch_QA_Tests();
        public abstract void QASearch_QA_Test_Summary_Search();
        public abstract void QASearch_QMS_Document_Search();
        public abstract void QA_Test_Proctor_Curve_Controller();
        public abstract void QCEngineer_QC_Test_Authorization();
        public abstract void QCEngineer_QC_Test_Lab_Supervisor_Review();
        public abstract void QCLab_BreakSheet_Creation();
        public abstract void QCLab_BreakSheet_Legacy();
        public abstract void QCLab_Equipment_Management();
        public abstract void QCRecordControl_QC_CDR();
        public abstract void QCRecordControl_QC_DIRs();
        public abstract void QCRecordControl_QC_NCR();
        public abstract void QCRecordControl_QC_Test_All();
        public abstract void QCRecordControl_QC_Test_Correction_Report();
        public abstract void QCRecordControl_QC_Test_Original_Report();
        public abstract void QCSearch_Daily_Inspection_Report();
        public abstract void QCSearch_DIR_Summary_Report();
        public abstract void QCSearch_QC_Tests_Search();
        public abstract void QCSearch_QC_Test_Summary_Search();
        public abstract void Qms_Document();
        public abstract void RecordControlDaily_Environmental_Inspection();
        public abstract void RecordControl_Concrete_Paving_Quantity_Tracker();
        public abstract void RecordControl_DIR_Count();
        public abstract void RecordControl_Girder_Tracker();
        public abstract void RecordControl_MPL_Tracker();
        public abstract void RecordControl_QA_QaDeficiency_Notice();
        public abstract void RecordControl_QA_QaDIRs();
        public abstract void RecordControl_QA_QaNCR();
        public abstract void RecordControl_QA_QaTest();
        public abstract void RecordControl_QC_QcDeficiency_Notice();
        public abstract void RecordControl_QC_QcDIRs();
        public abstract void RecordControl_QC_QcNCR();
        public abstract void RecordControl_QC_QcTest_All();
        public abstract void RecordControl_Retaining_Wall_Backfill_Quantity_Tracker();
        public abstract void RecordControl_Test_Count();
        public abstract void RecordControl_Weekly_Environmental_Inspection();
        public abstract void RecordControl_Weekly_Environmental_Monitoring();
        public abstract void ReportsNotices_General_DN();
        public abstract void ReportsNotices_General_NCR();
        public abstract void RFI_Create();
        public abstract void RFI_List();
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
    }


    #region Implementation specific to SGWay

    public class PageNavigation_SGWay : PageNavigation
    {
        public PageNavigation_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override void Qms_Document() => Navigate.Menu(NavMenu.QARecordControl.Menu.QMS_Document);

    }

    #endregion Implementation specific to SGWay

    #region Implementation specific to SH249

    public class PageNavigation_SH249 : PageNavigation
    {
        public PageNavigation_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override void Qms_Document() => Navigate.Menu(NavMenu.QARecordControl.Menu.QMS_Document);
    }

    #endregion Implementation specific to SH249

    #region Implementation specific to Garnet

    public class PageNavigation_Garnet : PageNavigation
    {
        public PageNavigation_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to Garnet

    #region Implementation specific to GLX

    public class PageNavigation_GLX : PageNavigation
    {
        public PageNavigation_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override void QARecordControl_General_CDR() => Navigate.Menu(NavMenu.DeficienciesAndAudits.Menu.General_Deficiency_Notice);

        public override void QARecordControl_QA_DIRs() => Navigate.Menu(NavMenu.QARecordControl.Menu.QA_IDRs);

        public override void QCRecordControl_QC_DIRs() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_IDRs);

        public override void QARecordControl_General_NCR() => Navigate.Menu(NavMenu.Owner.Menu.Owner_NCRs);

        public override void QASearch_Daily_Inspection_Report() => Navigate.Menu(NavMenu.QualitySearch.Menu.Inspector_Daily_Report);

        public override void QASearch_Material_Traceability_Matrix() => Navigate.Menu(NavMenu.QualitySearch.Menu.Material_Traceability_Matrix);

    }

    #endregion Implementation specific to GLX

    #region Implementation specific to I15South

    public class PageNavigation_I15South : PageNavigation
    {
        public PageNavigation_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15South

    #region Implementation specific to I15Tech

    public class PageNavigation_I15Tech : PageNavigation
    {
        public PageNavigation_I15Tech(IWebDriver driver) : base(driver)
        {

        }
    }

    #endregion Implementation specific to I15Tech

    #region Implementation specific to LAX

    public class PageNavigation_LAX : PageNavigation
    {
        public PageNavigation_LAX(IWebDriver driver) : base(driver)
        { }

        public override void QARecordControl_General_CDR() => Navigate.Menu(NavMenu.RecordControl.Menu.Deficiency_Notice);

        public override void QARecordControl_General_NCR() => Navigate.Menu(NavMenu.RecordControl.Menu.NCR);

        public override void QARecordControl_QA_DIRs() => Navigate.Menu(NavMenu.RecordControl.QA.Menu.QA_DIR);

        public override void QARecordControl_QA_Test_All() => Navigate.Menu(NavMenu.RecordControl.QA.Menu.QA_Test);

        public override void QCRecordControl_QC_CDR() => Navigate.Menu(NavMenu.RecordControl.QC.Menu.QC_Deficiency_Notice);

        public override void QCRecordControl_QC_NCR() => Navigate.Menu(NavMenu.RecordControl.QC.Menu.QC_NCR);

        public override void QCRecordControl_QC_DIRs() => Navigate.Menu(NavMenu.RecordControl.QC.Menu.QC_DIR);

        public override void Control_Point_Log() => Navigate.Menu(NavMenu.CheckPoint.Menu.Check_Point_Log);

        public override void Control_Point_Scheduler() => Navigate.Menu(NavMenu.CheckPoint.Menu.Check_Point_Scheduler);

        public override void QALab_Technician_Random() => Navigate.Menu(NavMenu.Lab.Menu.Technician_Random);

        public override void QALab_BreakSheet_Creation() => Navigate.Menu(NavMenu.Lab.Menu.BreakSheet_Creation);

        public override void QALab_BreakSheet_Legacy() => Navigate.Menu(NavMenu.Lab.Menu.BreakSheet_Legacy);

        public override void QALab_BreakSheet_Forecast() => Navigate.Menu(NavMenu.Lab.Menu.BreakSheet_Forecast);

        public override void QALab_Equipment_Management() => Navigate.Menu(NavMenu.Lab.Menu.Equipment_Management);

        public override void QCLab_BreakSheet_Creation() => Navigate.Menu(NavMenu.Lab.Menu.BreakSheet_Creation);

        public override void QCLab_BreakSheet_Legacy() => Navigate.Menu(NavMenu.Lab.Menu.BreakSheet_Legacy);

        public override void QCLab_Equipment_Management() => Navigate.Menu(NavMenu.Lab.Menu.Equipment_Management);

        public override void QASearch_QA_Tests() => Navigate.Menu(NavMenu.QualitySearch.QA.Menu.QA_Test_Search);

        public override void QASearch_QA_Test_Summary_Search() => Navigate.Menu(NavMenu.QualitySearch.QA.Menu.QA_Test_Summary_Search);

        public override void QASearch_Daily_Inspection_Report() => Navigate.Menu(NavMenu.QualitySearch.QA.Menu.QA_DIR_Search);

        public override void QASearch_DIR_Summary_Report() => Navigate.Menu(NavMenu.QualitySearch.QA.Menu.QA_DIR_Summary_Report);

        public override void QCSearch_QC_Tests_Search() => Navigate.Menu(NavMenu.QualitySearch.QC.Menu.QC_Test_Search);

        public override void QCSearch_QC_Test_Summary_Search() => Navigate.Menu(NavMenu.QualitySearch.QC.Menu.QC_Test_Summary_Search);

        public override void QCSearch_Daily_Inspection_Report() => Navigate.Menu(NavMenu.QualitySearch.QC.Menu.QC_Daily_Inspection_Report);

        public override void QCSearch_DIR_Summary_Report() => Navigate.Menu(NavMenu.QualitySearch.QC.Menu.QC_DIR_Summary_Report);

        public override void QASearch_NCR_Log_View() => Navigate.Menu(NavMenu.QualitySearch.Menu.NCR_Log_View);

        public override void QASearch_CDR_Log_View() => Navigate.Menu(NavMenu.QualitySearch.Menu.Deficiency_Log_View);

        public override void QASearch_Inspection_Deficiency_Log_Report() => Navigate.Menu(NavMenu.QualitySearch.Menu.Inspection_Deficiency_Log_Report);

        public override void QASearch_QA_Guide_Schedule_Summary_Report() => Navigate.Menu(NavMenu.QualitySearch.Menu.Guide_Schedule_Summary_Report);

        public override void QASearch_Mix_Design_Summary_HMA() => Navigate.Menu(NavMenu.QualitySearch.Menu.HMA_Mix_Design_Summary);

        public override void QASearch_Mix_Design_Report_HMA() => Navigate.Menu(NavMenu.QualitySearch.Menu.HMA_Mix_Design_Report);

        public override void QASearch_Material_Traceability_Matrix() => Navigate.Menu(NavMenu.QualitySearch.Menu.Material_Traceability_Matrix_Search);

        public override void QAQO_Test_Proctor_Curve_Report() => Navigate.Menu(NavMenu.QualitySearch.Menu.Test_Proctor_Curve_Report);

        public override void QAQO_Test_Proctor_Curve_Summary() => Navigate.Menu(NavMenu.QualitySearch.Menu.Test_Proctor_Curve_Summary);

        public override void RMCenter_Upload_DEV_Submittal() => Navigate.Menu(NavMenu.RMCenter.Menu.Upload_Design_Submittal);
    }
}

#endregion Implementation specific to LAX