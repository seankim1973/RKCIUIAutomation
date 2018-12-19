using OpenQA.Selenium;
using RKCIUIAutomation.Config;

namespace RKCIUIAutomation.Page.Navigation
{
    #region PageNavigation Generic class

    public class PageNavigation : PageNavigation_Impl
    {
        public PageNavigation()
        {
        }

        public PageNavigation(IWebDriver driver) => this.Driver = driver;

        /// <summary>
        /// Common pageObjects and workflows are inherited from abstract _Impl class
        /// </summary>
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
        void RecordControl_QA_QaDIRs();

        void RecordControl_QA_QaNCR();

        void RecordControl_QC();

        void RecordControl_DIR_Count();

        // QA Search
        void QASearch_QA_Tests();

        void QASearch_QA_Test_Summary_Search();

        void QASearch_QA_Guide_Schedule_Summary_Report();

        void QASearch_Inspection_Deficiency_Log_Report();

        void QASearch_Daily_Inspection_Report();

        void QASearch_DIR_Summary_Report();

        void QASearch_DIR_Checklist_Search();

        void QASearch_Ncr_Log_View();

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

    #region PageNavigation Common Implementation class

    public abstract class PageNavigation_Impl : PageBase, IPageNavigation
    {
        internal NavMenu Navigate => new NavMenu(Driver);

        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IPageNavigation SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IPageNavigation instance = new PageNavigation(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using Navigation_SGWay instance ###### ");
                instance = new PageNavigation_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using Navigation_SH249 instance ###### ");
                instance = new PageNavigation_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using Navigation_Garnet instance ###### ");
                instance = new PageNavigation_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using Navigation_GLX instance ###### ");
                instance = new PageNavigation_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using Navigation_I15South instance ###### ");
                instance = new PageNavigation_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using Navigation_I15Tech instance ###### ");
                instance = new PageNavigation_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using Navigation_LAX instance ###### ");
                instance = new PageNavigation_LAX(driver);
            }

            return instance;
        }

        // QMS_Document / QMS_Document_Search
        /// <summary>
        /// for SG >QA Record Control>Qms Document, >QA Search>QMS Document Search
        /// for SH249 >QA Record Control>QMS Documents, >QA Search>QMS Document Search
        /// for I15Tech, I15South, GLX, Garnet >Project>Qms Document -no QMS Document Search menu item
        /// </summary>
        public virtual void Qms_Document() => Navigate.Menu(NavMenu.Project.Menu.Qms_Document);

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

        // Project>Administration>System Configuration>Equipment
        public virtual void SysConfig_Equipment_Makes() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Makes);

        public virtual void SysConfig_Equipment_Models() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Models);

        public virtual void SysConfig_Equipment_Types() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Types);

        // Project>Administration>System Configuration>Grade Management
        public virtual void SysConfig_Grades() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.GradeManagement.Menu.Grades);

        public virtual void SysConfig_Grade_Types() => Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.GradeManagement.Menu.Grade_Types);

        // Control Point Menu
        public virtual void Control_Point_Log() => Navigate.Menu(NavMenu.ControlPoint.Menu.Control_Point_Log);

        public virtual void Control_Point_Scheduler() => Navigate.Menu(NavMenu.ControlPoint.Menu.Control_Point_Scheduler);

        // Material Mix Codes Menu
        public virtual void MaterialMixCodes_Material_Code_Base_Aggregate() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Base_Aggregate);

        public virtual void MaterialMixCodes_Material_Code_Concrete_Aggregate() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Concrete_Aggregate);

        public virtual void MaterialMixCodes_Material_Code_HMA_Aggregate() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_HMA_Aggregate);

        public virtual void MaterialMixCodes_Material_Code_Raw_Material() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Raw_Material);

        public virtual void MaterialMixCodes_Mix_Design_HMA() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Mix_Design_HMA);

        public virtual void MaterialMixCodes_Mix_Design_PCC() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Mix_Design_PCC);

        public virtual void MaterialMixCodes_Sieve_Analyses_IOC() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Sieve_Analyses_IOC);

        public virtual void MaterialMixCodes_Sieve_Analyses_JMF() => Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Sieve_Analyses_JMF);

        // Owner Menu
        public virtual void Owner_DIRs() => Navigate.Menu(NavMenu.Owner.Menu.Owner_DIRs);

        public virtual void Owner_NCRs() => Navigate.Menu(NavMenu.Owner.Menu.Owner_NCRs);

        // QA Engineer Menu
        public virtual void QAEngineer_QA_Test_Lab_Supervisor_Review() => Navigate.Menu(NavMenu.QAEngineer.Menu.QA_Test_Lab_Supervisor_Review);

        public virtual void QAEngineer_QA_Test_Field_Supervisor_Review() => Navigate.Menu(NavMenu.QAEngineer.Menu.QA_Test_Field_Supervisor_Review);

        public virtual void QAEngineer_QA_Test_Authorization() => Navigate.Menu(NavMenu.QAEngineer.Menu.QA_Test_Authorization);

        public virtual void QAEngineer_DIR_QA_Review_Approval() => Navigate.Menu(NavMenu.QAEngineer.Menu.DIR_QA_Review_Approval);

        public virtual void QA_Test_Proctor_Curve_Controller() => Navigate.Menu(NavMenu.QAEngineer.Menu.QA_Test_Proctor_Curve_Controller);

        // QA Field Menu
        public virtual void QAField_Daily_Environmental_Inspection() => Navigate.Menu(NavMenu.QAField.Menu.Daily_Environmental_Inspection);

        public virtual void QAField_QA_DIRs() => Navigate.Menu(NavMenu.QAField.Menu.QA_DIRs);

        public virtual void QAField_QA_Technician_Random_Search() => Navigate.Menu(NavMenu.QAField.Menu.QA_Technician_Random_Search);

        public virtual void QAField_QA_Test() => Navigate.Menu(NavMenu.QAField.Menu.QA_Test);

        public virtual void QAField_Weekly_Environmental_Inspection() => Navigate.Menu(NavMenu.QAField.Menu.Weekly_Environmental_Inspection);

        public virtual void QAField_Weekly_Environmental_Monitoring() => Navigate.Menu(NavMenu.QAField.Menu.Weekly_Environmental_Monitoring);

        // QA Lab Menu
        public virtual void QALab_BreakSheet_Creation() => Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Creation);

        public virtual void QALab_BreakSheet_Legacy() => Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Legacy);

        public virtual void QALab_Equipment_Management() => Navigate.Menu(NavMenu.QALab.Menu.Equipment_Management);

        public virtual void QALab_Technician_Random() => Navigate.Menu(NavMenu.QALab.Menu.Technician_Random);

        public virtual void QALab_BreakSheet_Forecast() => Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Forecast);

        public virtual void QALab_Cylinder_PickUp_List() => Navigate.Menu(NavMenu.QALab.Menu.Cylinder_PickUp_List);

        public virtual void QALab_Early_Break_Calendar() => Navigate.Menu(NavMenu.QALab.Menu.Early_Break_Calendar);

        // OV Menu
        public virtual void OV_Create_OV_Test() => Navigate.Menu(NavMenu.OV.Menu.Create_OV_Test);

        public virtual void OV_OV_Test() => Navigate.Menu(NavMenu.OV.Menu.OV_Tests);

        // QA Records Control Menu
        // use Qms_Document() for QARecordControl_Qms_Document
        public virtual void QARecordControl_QA_Test_Original_Report() => Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_Original_Report);

        public virtual void QARecordControl_QA_Test_Correction_Report() => Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_Correction_Report);

        public virtual void QARecordControl_QA_Test_All() => Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_All);

        public virtual void QARecordControl_QA_DIRs() => Navigate.Menu(NavMenu.QARecordControl.Menu.QA_DIRs);

        public virtual void QARecordControl_General_NCR() => Navigate.Menu(NavMenu.QARecordControl.Menu.General_NCR);

        public virtual void QARecordControl_General_CDR() => Navigate.Menu(NavMenu.QARecordControl.Menu.General_CDR);

        public virtual void QARecordControl_Retaining_Wall_BackFill_Quantity_Tracker() => Navigate.Menu(NavMenu.QARecordControl.Menu.Retaining_Wall_BackFill_Quantity_Tracker);

        public virtual void QARecordControl_Concrete_Paving_Quantity_Tracker() => Navigate.Menu(NavMenu.QARecordControl.Menu.Concrete_Paving_Quantity_Tracker);

        public virtual void QARecordControl_MPL_Tracker() => Navigate.Menu(NavMenu.QARecordControl.Menu.MPL_Tracker);

        public virtual void QARecordControl_Girder_Tracker() => Navigate.Menu(NavMenu.QARecordControl.Menu.Girder_Tracker);

        public virtual void QARecordControl_QA_Test_Retest_Report() => Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_Retest_Report);

        public virtual void QARecordControl_Environmental_Document() => Navigate.Menu(NavMenu.QARecordControl.Menu.Environmental_Document);

        // Record Control Menu - LAX
        public virtual void RecordControl_QA_QaDIRs() => Navigate.Menu(NavMenu.RecordControl.QA.Menu.QA_DIRs);

        public virtual void RecordControl_QA_QaNCR() => Navigate.Menu(NavMenu.RecordControl.QA.Menu.QA_NCR);

        public virtual void RecordControl_QC() => Navigate.Menu(NavMenu.RecordControl.Menu.QC);

        public virtual void RecordControl_DIR_Count() => Navigate.Menu(NavMenu.RecordControl.Menu.DIR_Count);


        // QA Search
        public virtual void QASearch_Daily_Inspection_Report() => Navigate.Menu(NavMenu.QASearch.Menu.Daily_Inspection_Report);

        public virtual void QASearch_DIR_Checklist_Search() => Navigate.Menu(NavMenu.QASearch.Menu.DIR_Checklist_Search);

        public virtual void QASearch_DIR_Summary_Report() => Navigate.Menu(NavMenu.QASearch.Menu.DIR_Summary_Report);

        public virtual void QASearch_Inspection_Deficiency_Log_Report() => Navigate.Menu(NavMenu.QASearch.Menu.Inspection_Deficiency_Log_Report);

        public virtual void QASearch_Ncr_Log_View() => Navigate.Menu(NavMenu.QASearch.Menu.Ncr_Log_View);

        public virtual void QASearch_QA_Guide_Schedule_Summary_Report() => Navigate.Menu(NavMenu.QASearch.Menu.QA_Guide_Schedule_Summary_Report);

        public virtual void QASearch_QA_Tests() => Navigate.Menu(NavMenu.QASearch.Menu.QA_Tests);

        public virtual void QASearch_QA_Test_Summary_Search() => Navigate.Menu(NavMenu.QASearch.Menu.QA_Test_Summary_Search);

        public virtual void QASearch_QMS_Document_Search() => Navigate.Menu(NavMenu.QASearch.Menu.QMS_Document_Search);

        public virtual void Environmental_Document_Search() => Navigate.Menu(NavMenu.QASearch.Menu.Environmental_Document_Search);

        public virtual void QAQO_Test_Proctor_Curve_Report() => Navigate.Menu(NavMenu.QASearch.Menu.QAQO_Test_Proctor_Curve_Report);

        public virtual void QAQO_Test_Proctor_Curve_Summary() => Navigate.Menu(NavMenu.QASearch.Menu.QAQO_Test_Proctor_Curve_Summary);

        // Reports & Notices Menu
        public virtual void ReportsNotices_General_DN() => Navigate.Menu(NavMenu.ReportsNotices.Menu.General_DN);

        public virtual void ReportsNotices_General_NCR() => Navigate.Menu(NavMenu.ReportsNotices.Menu.General_NCR);

        // RM Center Menu
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

        // RFI
        public virtual void RFI_List() => Navigate.Menu(NavMenu.RFI.Menu.List);

        public virtual void RFI_Create() => Navigate.Menu(NavMenu.RFI.Menu.Create);

        //QC Lab Menu
        public virtual void QCLab_BreakSheet_Creation() => Navigate.Menu(NavMenu.QCLab.Menu.BreakSheet_Creation);

        public virtual void QCLab_BreakSheet_Legacy() => Navigate.Menu(NavMenu.QCLab.Menu.BreakSheet_Legacy);

        public virtual void QCLab_Equipment_Management() => Navigate.Menu(NavMenu.QCLab.Menu.Equipment_Management);

        //QC Record Control Menu
        public virtual void QCRecordControl_QC_Test_Original_Report() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_Test_Original_Report);

        public virtual void QCRecordControl_QC_Test_Correction_Report() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_Test_Correction_Report);

        public virtual void QCRecordControl_QC_Test_All() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_Test_All);

        public virtual void QCRecordControl_QC_DIRs() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_DIRs);

        public virtual void QCRecordControl_QC_NCR() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_NCR);

        public virtual void QCRecordControl_QC_CDR() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_CDR);

        //QC Engineer Menu
        public virtual void QCEngineer_QC_Test_Lab_Supervisor_Review() => Navigate.Menu(NavMenu.QCEngineer.Menu.QC_Test_Lab_Supervisor_Review);

        public virtual void QCEngineer_QC_Test_Authorization() => Navigate.Menu(NavMenu.QCEngineer.Menu.QC_Test_Authorization);

        //QC Search Menu
        public virtual void QCSearch_QC_Tests_Search() => Navigate.Menu(NavMenu.QCSearch.Menu.QC_Tests_Search);

        public virtual void QCSearch_QC_Test_Summary_Search() => Navigate.Menu(NavMenu.QCSearch.Menu.QC_Test_Summary_Search);

        public virtual void QCSearch_Daily_Inspection_Report() => Navigate.Menu(NavMenu.QCSearch.Menu.Daily_Inspection_Report);

        public virtual void QCSearch_DIR_Summary_Report() => Navigate.Menu(NavMenu.QCSearch.Menu.DIR_Summary_Report);

    }

    #endregion PageNavigation Common Implementation class

    /// <summary>
    /// Tenant specific implementation of PageNavigation
    /// </summary>
    ///

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

        public override void QARecordControl_General_CDR() => Navigate.Menu(NavMenu.QARecordControl.Menu.General_Deficiency_Notice);

        public override void QARecordControl_QA_DIRs() => Navigate.Menu(NavMenu.QARecordControl.Menu.QA_IDRs);

        public override void QCRecordControl_QC_DIRs() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_IDRs);

        public override void QASearch_Daily_Inspection_Report() => Navigate.Menu(NavMenu.QualitySearch.Menu.Inspector_Daily_Report);


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

        public override void QARecordControl_General_CDR() => Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Deficiency_Notice);

        public override void QCRecordControl_QC_CDR() => Navigate.Menu(NavMenu.QCRecordControl.Menu.QC_Deficiency_Notice);

        public override void QARecordControl_QA_DIRs() => Navigate.Menu(NavMenu.RecordControl.QA.Menu.QA_DIRs);

    }
}

    #endregion Implementation specific to LAX
